using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace FerroVelhoDAO
{
    public sealed class PostgresLoginResult
    {
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public int PermissaoUsuario { get; set; }
        public bool Ativo { get; set; }
        public string DescricaoPermissao { get; set; }
    }

    public static class PostgresConnectionService
    {
        public static bool TestarConexao(string connectionString)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT 1", conn))
                {
                    cmd.ExecuteScalar();
                }
                return true;
            }
        }

        public static PostgresLoginResult ValidarLogin(string connectionString, string nomeUsuario, string senhaUsuario)
        {
            const string sql = @"
SELECT
  u.id_usuario,
  u.nome_usuario,
  u.senha_usuario,
  u.permi_usuario,
  u.ativo,
  t.desc_tipousuario
FROM dbo.tb_usuario u
LEFT JOIN dbo.tb_tipousuario t ON t.id_tipousuario = u.permi_usuario
WHERE u.nome_usuario = @nome
  AND u.senha_usuario = @senha
  AND u.ativo = TRUE
LIMIT 1;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nome", nomeUsuario ?? string.Empty);
                cmd.Parameters.AddWithValue("@senha", senhaUsuario ?? string.Empty);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new PostgresLoginResult
                    {
                        UsuarioId = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                        NomeUsuario = reader.GetString(reader.GetOrdinal("nome_usuario")),
                        SenhaUsuario = reader.IsDBNull(reader.GetOrdinal("senha_usuario"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("senha_usuario")),
                        PermissaoUsuario = reader.GetInt32(reader.GetOrdinal("permi_usuario")),
                        Ativo = reader.GetBoolean(reader.GetOrdinal("ativo")),
                        DescricaoPermissao = reader.IsDBNull(reader.GetOrdinal("desc_tipousuario"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("desc_tipousuario"))
                    };
                }
            }
        }

        public static List<tb_produtos> ListarProdutos(string connectionString)
        {
            const string sql = @"
SELECT id_prod, desc_prod, val_prod, usuario
FROM dbo.tb_produtos
ORDER BY desc_prod;";

            var itens = new List<tb_produtos>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new tb_produtos
                        {
                            id_prod = reader.GetInt32(reader.GetOrdinal("id_prod")),
                            desc_prod = reader.IsDBNull(reader.GetOrdinal("desc_prod"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("desc_prod")),
                            val_prod = reader.IsDBNull(reader.GetOrdinal("val_prod"))
                                ? (decimal?)null
                                : reader.GetDecimal(reader.GetOrdinal("val_prod")),
                            usuario = reader.IsDBNull(reader.GetOrdinal("usuario"))
                                ? (int?)null
                                : reader.GetInt32(reader.GetOrdinal("usuario"))
                        };

                        itens.Add(item);
                    }
                }
            }

            return itens;
        }

        public static tb_produtos CriarProduto(string connectionString, string descricao, decimal valor, int? usuario)
        {
            const string sql = @"
INSERT INTO dbo.tb_produtos (desc_prod, val_prod, usuario)
VALUES (@desc_prod, @val_prod, @usuario)
RETURNING id_prod, desc_prod, val_prod, usuario;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@desc_prod", (object)(descricao ?? string.Empty));
                cmd.Parameters.AddWithValue("@val_prod", valor);
                cmd.Parameters.AddWithValue("@usuario", (object)usuario ?? DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new tb_produtos
                    {
                        id_prod = reader.GetInt32(reader.GetOrdinal("id_prod")),
                        desc_prod = reader.IsDBNull(reader.GetOrdinal("desc_prod"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("desc_prod")),
                        val_prod = reader.IsDBNull(reader.GetOrdinal("val_prod"))
                            ? (decimal?)null
                            : reader.GetDecimal(reader.GetOrdinal("val_prod")),
                        usuario = reader.IsDBNull(reader.GetOrdinal("usuario"))
                            ? (int?)null
                            : reader.GetInt32(reader.GetOrdinal("usuario"))
                    };
                }
            }
        }

        public static void AtualizarProduto(string connectionString, int idProd, string descricao, decimal valor)
        {
            const string sql = @"
UPDATE dbo.tb_produtos
SET desc_prod = @desc_prod,
    val_prod = @val_prod
WHERE id_prod = @id_prod;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_prod", idProd);
                cmd.Parameters.AddWithValue("@desc_prod", (object)(descricao ?? string.Empty));
                cmd.Parameters.AddWithValue("@val_prod", valor);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ExcluirProduto(string connectionString, int idProd)
        {
            const string sql = "DELETE FROM dbo.tb_produtos WHERE id_prod = @id_prod;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_prod", idProd);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static tb_impressora BuscarImpressoraPorId(string connectionString, int idImpressora)
        {
            const string sql = @"
SELECT id_impressora, nome_impressora
FROM dbo.tb_impressora
WHERE id_impressora = @id
LIMIT 1;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idImpressora);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new tb_impressora
                    {
                        id_impressora = reader.GetInt32(reader.GetOrdinal("id_impressora")),
                        nome_impressora = reader.IsDBNull(reader.GetOrdinal("nome_impressora"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("nome_impressora"))
                    };
                }
            }
        }

        private static decimal ObterSomaPorCliente(string connectionString, string sql, int idCliente)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                conn.Open();

                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return 0m;
                }

                return Convert.ToDecimal(result);
            }
        }

        public static decimal CalcularValorDevedor(string connectionString, int idCliente)
        {
            var pag = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_compra.desconto_compra), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente;",
                idCliente);

            var credito = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente;",
                idCliente);

            var adianta = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_caixa.valor_caixa), 0) FROM dbo.tb_caixa WHERE tb_caixa.id_cliente = @id_cliente;",
                idCliente);

            var acerto = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_acliente.valor_acliente), 0) FROM dbo.tb_acliente WHERE tb_acliente.id_cliente = @id_cliente;",
                idCliente);

            var total = adianta + pag + acerto + credito;
            if (total > 0m)
            {
                return 0m;
            }

            return total * -1m;
        }

        public static decimal CalcularValorCredito(string connectionString, int idCliente)
        {
            var pag = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_compra.desconto_compra), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente;",
                idCliente);

            var credito = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente;",
                idCliente);

            var adianta = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_caixa.valor_caixa), 0) FROM dbo.tb_caixa WHERE tb_caixa.id_cliente = @id_cliente;",
                idCliente);

            var acerto = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_acliente.valor_acliente), 0) FROM dbo.tb_acliente WHERE tb_acliente.id_cliente = @id_cliente;",
                idCliente);

            var total = adianta + pag + credito + acerto;
            if (total < 0m)
            {
                return 0m;
            }

            return total;
        }

        public static tb_compra CriarCompra(string connectionString, DateTime dataCompra, int usuario, decimal descontoCompra, decimal subtotCompra, decimal valorNota)
        {
            const string sql = @"
INSERT INTO dbo.tb_compra (data_compra, usuario, desconto_compra, subtot_compra, valor_nota)
VALUES (@data_compra, @usuario, @desconto_compra, @subtot_compra, @valor_nota)
RETURNING id_compra, data_compra, usuario, desconto_compra, subtot_compra, valor_nota, id_cliente;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@data_compra", dataCompra);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@desconto_compra", descontoCompra);
                cmd.Parameters.AddWithValue("@subtot_compra", subtotCompra);
                cmd.Parameters.AddWithValue("@valor_nota", valorNota);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new tb_compra
                    {
                        id_compra = reader.GetInt32(reader.GetOrdinal("id_compra")),
                        data_compra = reader.GetDateTime(reader.GetOrdinal("data_compra")),
                        usuario = reader.IsDBNull(reader.GetOrdinal("usuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("usuario")),
                        desconto_compra = reader.IsDBNull(reader.GetOrdinal("desconto_compra")) ? 0m : reader.GetDecimal(reader.GetOrdinal("desconto_compra")),
                        subtot_compra = reader.IsDBNull(reader.GetOrdinal("subtot_compra")) ? 0m : reader.GetDecimal(reader.GetOrdinal("subtot_compra")),
                        valor_nota = reader.IsDBNull(reader.GetOrdinal("valor_nota")) ? 0m : reader.GetDecimal(reader.GetOrdinal("valor_nota")),
                        id_cliente = reader.IsDBNull(reader.GetOrdinal("id_cliente")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_cliente"))
                    };
                }
            }
        }

        public static void AtualizarCompraValores(string connectionString, int idCompra, decimal descontoCompra, decimal subtotCompra, decimal valorNota, int? idCliente)
        {
            const string sql = @"
UPDATE dbo.tb_compra
SET desconto_compra = @desconto_compra,
    subtot_compra = @subtot_compra,
    valor_nota = @valor_nota,
    id_cliente = @id_cliente
WHERE id_compra = @id_compra;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                cmd.Parameters.AddWithValue("@desconto_compra", descontoCompra);
                cmd.Parameters.AddWithValue("@subtot_compra", subtotCompra);
                cmd.Parameters.AddWithValue("@valor_nota", valorNota);
                cmd.Parameters.AddWithValue("@id_cliente", (object)idCliente ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void InserirItemCompra(string connectionString, int idProd, int idCompra, decimal quantItem, decimal subTotItem, decimal valorItem)
        {
            const string sql = @"
INSERT INTO dbo.tb_itemc (id_prod, id_compra, quant_item, subtot_item, valor_item)
VALUES (@id_prod, @id_compra, @quant_item, @subtot_item, @valor_item);";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_prod", idProd);
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                cmd.Parameters.AddWithValue("@quant_item", quantItem);
                cmd.Parameters.AddWithValue("@subtot_item", subTotItem);
                cmd.Parameters.AddWithValue("@valor_item", valorItem);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<tb_itemc> ListarItensCompra(string connectionString, int idCompra)
        {
            const string sql = @"
SELECT
  i.id_item,
  i.id_prod,
  i.id_compra,
  i.quant_item,
  i.subtot_item,
  i.valor_item,
  p.desc_prod,
  p.val_prod,
  p.usuario
FROM dbo.tb_itemc i
INNER JOIN dbo.tb_produtos p ON p.id_prod = i.id_prod
WHERE i.id_compra = @id_compra
ORDER BY i.id_item;";

            var itens = new List<tb_itemc>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prod = new tb_produtos
                        {
                            id_prod = reader.GetInt32(reader.GetOrdinal("id_prod")),
                            desc_prod = reader.IsDBNull(reader.GetOrdinal("desc_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("desc_prod")),
                            val_prod = reader.IsDBNull(reader.GetOrdinal("val_prod")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("val_prod")),
                            usuario = reader.IsDBNull(reader.GetOrdinal("usuario")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("usuario"))
                        };

                        var item = new tb_itemc
                        {
                            id_item = reader.GetInt32(reader.GetOrdinal("id_item")),
                            id_prod = reader.GetInt32(reader.GetOrdinal("id_prod")),
                            id_compra = reader.GetInt32(reader.GetOrdinal("id_compra")),
                            quant_item = reader.GetDecimal(reader.GetOrdinal("quant_item")),
                            subTot_item = reader.GetDecimal(reader.GetOrdinal("subtot_item")),
                            valor_item = reader.GetDecimal(reader.GetOrdinal("valor_item"))
                        };

                        item.tb_produtos = prod;
                        itens.Add(item);
                    }
                }
            }

            return itens;
        }

        public static void ExcluirItemCompra(string connectionString, int idItem)
        {
            const string sql = "DELETE FROM dbo.tb_itemc WHERE id_item = @id_item;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_item", idItem);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ExcluirCompra(string connectionString, int idCompra)
        {
            const string sql = "DELETE FROM dbo.tb_compra WHERE id_compra = @id_compra;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static decimal CalcularSaldoProduto(string connectionString, int idProd)
        {
            const string sqlEntrada = "SELECT COALESCE(SUM(quant_item), 0) FROM dbo.tb_itemc WHERE id_prod = @id_prod;";
            const string sqlSaida = "SELECT COALESCE(SUM(quant_item), 0) FROM dbo.tb_itemv WHERE id_prod = @id_prod;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                decimal entrada;
                using (var cmd = new NpgsqlCommand(sqlEntrada, conn))
                {
                    cmd.Parameters.AddWithValue("@id_prod", idProd);
                    entrada = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
                }

                decimal saida;
                using (var cmd = new NpgsqlCommand(sqlSaida, conn))
                {
                    cmd.Parameters.AddWithValue("@id_prod", idProd);
                    saida = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
                }

                return entrada - saida;
            }
        }

        public static tb_venda CriarVenda(string connectionString, DateTime dataVenda, int usuario, decimal valorNota)
        {
            const string sql = @"
INSERT INTO dbo.tb_venda (data_venda, valor_nota, usuario)
VALUES (@data_venda, @valor_nota, @usuario)
RETURNING id_venda, data_venda, valor_nota, usuario;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@data_venda", dataVenda);
                cmd.Parameters.AddWithValue("@valor_nota", valorNota);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new tb_venda
                    {
                        id_venda = reader.GetInt32(reader.GetOrdinal("id_venda")),
                        data_venda = reader.GetDateTime(reader.GetOrdinal("data_venda")),
                        valor_nota = reader.GetDecimal(reader.GetOrdinal("valor_nota")),
                        usuario = reader.IsDBNull(reader.GetOrdinal("usuario")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("usuario"))
                    };
                }
            }
        }

        public static void AtualizarVenda(string connectionString, int idVenda, decimal valorNota, int usuario)
        {
            const string sql = @"
UPDATE dbo.tb_venda
SET valor_nota = @valor_nota,
    usuario = @usuario
WHERE id_venda = @id_venda;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_venda", idVenda);
                cmd.Parameters.AddWithValue("@valor_nota", valorNota);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void InserirItemVenda(string connectionString, int idProd, int idVenda, decimal quantItem, decimal subTotItem, decimal valrItem)
        {
            const string sql = @"
INSERT INTO dbo.tb_itemv (id_prod, id_venda, quant_item, subtot_item, valr_item)
VALUES (@id_prod, @id_venda, @quant_item, @subtot_item, @valr_item);";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_prod", idProd);
                cmd.Parameters.AddWithValue("@id_venda", idVenda);
                cmd.Parameters.AddWithValue("@quant_item", quantItem);
                cmd.Parameters.AddWithValue("@subtot_item", subTotItem);
                cmd.Parameters.AddWithValue("@valr_item", valrItem);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<tb_itemv> ListarItensVenda(string connectionString, int idVenda)
        {
            const string sql = @"
SELECT
  i.id_item,
  i.id_prod,
  i.id_venda,
  i.quant_item,
  i.subtot_item,
  i.valr_item,
  p.desc_prod,
  p.val_prod,
  p.usuario
FROM dbo.tb_itemv i
INNER JOIN dbo.tb_produtos p ON p.id_prod = i.id_prod
WHERE i.id_venda = @id_venda
ORDER BY i.id_item;";

            var itens = new List<tb_itemv>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_venda", idVenda);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prod = new tb_produtos
                        {
                            id_prod = reader.GetInt32(reader.GetOrdinal("id_prod")),
                            desc_prod = reader.IsDBNull(reader.GetOrdinal("desc_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("desc_prod")),
                            val_prod = reader.IsDBNull(reader.GetOrdinal("val_prod")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("val_prod")),
                            usuario = reader.IsDBNull(reader.GetOrdinal("usuario")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("usuario"))
                        };

                        var item = new tb_itemv
                        {
                            id_item = reader.GetInt32(reader.GetOrdinal("id_item")),
                            id_prod = reader.IsDBNull(reader.GetOrdinal("id_prod")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_prod")),
                            id_venda = reader.IsDBNull(reader.GetOrdinal("id_venda")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_venda")),
                            quant_item = reader.IsDBNull(reader.GetOrdinal("quant_item")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("quant_item")),
                            subTot_item = reader.IsDBNull(reader.GetOrdinal("subtot_item")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("subtot_item")),
                            valr_item = reader.IsDBNull(reader.GetOrdinal("valr_item")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("valr_item"))
                        };

                        item.tb_produtos = prod;
                        itens.Add(item);
                    }
                }
            }

            return itens;
        }

        public static void ExcluirItemVenda(string connectionString, int idItem)
        {
            const string sql = "DELETE FROM dbo.tb_itemv WHERE id_item = @id_item;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_item", idItem);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ExcluirVenda(string connectionString, int idVenda)
        {
            const string sql = "DELETE FROM dbo.tb_venda WHERE id_venda = @id_venda;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_venda", idVenda);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable CarregarDadosRelatorioVenda(string connectionString, int idVenda)
        {
            const string sql = @"
SELECT
  iv.quant_item,
  iv.subtot_item,
  iv.valr_item,
  p.desc_prod,
  v.data_venda,
  iv.id_prod,
  iv.id_venda,
  u.nome_usuario,
  v.usuario
FROM dbo.tb_itemv iv
INNER JOIN dbo.tb_venda v ON iv.id_venda = v.id_venda
INNER JOIN dbo.tb_produtos p ON iv.id_prod = p.id_prod
INNER JOIN dbo.tb_usuario u ON v.usuario = u.id_usuario
WHERE iv.id_venda = @id_venda;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@id_venda", idVenda);
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }
    }
}
