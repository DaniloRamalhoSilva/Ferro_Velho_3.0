using System;
using System.Collections.Generic;
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
    }
}
