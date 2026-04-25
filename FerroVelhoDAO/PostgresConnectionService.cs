using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NpgsqlTypes;

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
SELECT id_prod, cod_prod, desc_prod, val_prod, usuario
FROM dbo.tb_produtos
WHERE COALESCE(excluido, FALSE) = FALSE
ORDER BY cod_prod;";

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
                            cod_prod = reader.IsDBNull(reader.GetOrdinal("cod_prod"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("cod_prod")),
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

        public static tb_produtos CriarProduto(string connectionString, string codigo, string descricao, decimal valor, int? usuario)
        {
            const string sql = @"
INSERT INTO dbo.tb_produtos (cod_prod, desc_prod, val_prod, usuario)
VALUES (@cod_prod, @desc_prod, @val_prod, @usuario)
RETURNING id_prod, cod_prod, desc_prod, val_prod, usuario;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@cod_prod", (object)(codigo ?? string.Empty));
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
                        cod_prod = reader.IsDBNull(reader.GetOrdinal("cod_prod"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("cod_prod")),
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

        public static void AtualizarProduto(string connectionString, int idProd, string codigo, string descricao, decimal valor)
        {
            const string sql = @"
UPDATE dbo.tb_produtos
SET cod_prod = @cod_prod,
    desc_prod = @desc_prod,
    val_prod = @val_prod
WHERE id_prod = @id_prod
  AND COALESCE(excluido, FALSE) = FALSE;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_prod", idProd);
                cmd.Parameters.AddWithValue("@cod_prod", (object)(codigo ?? string.Empty));
                cmd.Parameters.AddWithValue("@desc_prod", (object)(descricao ?? string.Empty));
                cmd.Parameters.AddWithValue("@val_prod", valor);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ExcluirProduto(string connectionString, int idProd)
        {
            const string sql = @"
UPDATE dbo.tb_produtos
SET excluido = TRUE
WHERE id_prod = @id_prod;";

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

        public static void SalvarImpressora(string connectionString, int idImpressora, string nomeImpressora)
        {
            const string sql = @"
INSERT INTO dbo.tb_impressora (id_impressora, nome_impressora)
VALUES (@id_impressora, @nome_impressora)
ON CONFLICT (id_impressora)
DO UPDATE SET nome_impressora = EXCLUDED.nome_impressora;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_impressora", idImpressora);
                cmd.Parameters.AddWithValue("@nome_impressora", (object)nomeImpressora ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<tb_tipoUsuario> ListarTiposUsuario(string connectionString)
        {
            const string sql = @"
SELECT id_tipousuario, desc_tipousuario
FROM dbo.tb_tipousuario
ORDER BY id_tipousuario;";

            var tipos = new List<tb_tipoUsuario>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipos.Add(new tb_tipoUsuario
                        {
                            id_tipoUsuario = reader.GetInt32(reader.GetOrdinal("id_tipousuario")),
                            desc_tipoUsuario = reader.IsDBNull(reader.GetOrdinal("desc_tipousuario"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("desc_tipousuario"))
                        });
                    }
                }
            }

            return tipos;
        }

        public static List<tb_usuario> ListarUsuarios(string connectionString, bool incluirInativos)
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
WHERE @incluir_inativos OR u.ativo = TRUE
ORDER BY u.nome_usuario;";

            var usuarios = new List<tb_usuario>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@incluir_inativos", incluirInativos);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tipo = new tb_tipoUsuario
                        {
                            id_tipoUsuario = reader.GetInt32(reader.GetOrdinal("permi_usuario")),
                            desc_tipoUsuario = reader.IsDBNull(reader.GetOrdinal("desc_tipousuario"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("desc_tipousuario"))
                        };

                        var usuario = new tb_usuario
                        {
                            id_usuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                            nome_usuario = reader.IsDBNull(reader.GetOrdinal("nome_usuario"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("nome_usuario")),
                            senha_usuario = reader.IsDBNull(reader.GetOrdinal("senha_usuario"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("senha_usuario")),
                            permi_usuario = reader.GetInt32(reader.GetOrdinal("permi_usuario")),
                            ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
                        };

                        usuario.tb_tipoUsuario = tipo;
                        usuarios.Add(usuario);
                    }
                }
            }

            return usuarios;
        }

        public static bool ExisteUsuarioPorNome(string connectionString, string nomeUsuario, int? excetoIdUsuario)
        {
            const string sql = @"
SELECT COUNT(1)
FROM dbo.tb_usuario
WHERE UPPER(nome_usuario) = UPPER(@nome_usuario)
  AND (@exceto_id IS NULL OR id_usuario <> @exceto_id);";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nome_usuario", (object)(nomeUsuario ?? string.Empty));
                cmd.Parameters.Add("@exceto_id", NpgsqlDbType.Integer).Value = excetoIdUsuario.HasValue
                    ? (object)excetoIdUsuario.Value
                    : DBNull.Value;

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public static void CriarUsuario(string connectionString, string nomeUsuario, string senhaUsuario, int permissaoUsuario)
        {
            const string sql = @"
INSERT INTO dbo.tb_usuario (nome_usuario, senha_usuario, permi_usuario, ativo)
VALUES (@nome_usuario, @senha_usuario, @permi_usuario, TRUE);";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nome_usuario", (object)(nomeUsuario ?? string.Empty));
                cmd.Parameters.AddWithValue("@senha_usuario", (object)senhaUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@permi_usuario", permissaoUsuario);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void AtualizarUsuario(string connectionString, int idUsuario, string nomeUsuario, string senhaUsuario, int permissaoUsuario)
        {
            const string sql = @"
UPDATE dbo.tb_usuario
SET nome_usuario = @nome_usuario,
    senha_usuario = @senha_usuario,
    permi_usuario = @permi_usuario
WHERE id_usuario = @id_usuario;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                cmd.Parameters.AddWithValue("@nome_usuario", (object)(nomeUsuario ?? string.Empty));
                cmd.Parameters.AddWithValue("@senha_usuario", (object)senhaUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@permi_usuario", permissaoUsuario);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DefinirUsuarioAtivo(string connectionString, int idUsuario, bool ativo)
        {
            const string sql = "UPDATE dbo.tb_usuario SET ativo = @ativo WHERE id_usuario = @id_usuario;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                cmd.Parameters.AddWithValue("@ativo", ativo);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<tb_cliente> ListarClientes(string connectionString, string filtroCampo, string filtroValor)
        {
            var filtroSql = string.Empty;
            if (!string.IsNullOrWhiteSpace(filtroValor))
            {
                if (string.Equals(filtroCampo, "nome", StringComparison.OrdinalIgnoreCase))
                {
                    filtroSql = "  AND c.nome_cliente ILIKE @filtro";
                }
                else if (string.Equals(filtroCampo, "cpf", StringComparison.OrdinalIgnoreCase))
                {
                    filtroSql = "  AND c.cpf_cliente LIKE @filtro";
                }
                else if (string.Equals(filtroCampo, "tel", StringComparison.OrdinalIgnoreCase))
                {
                    filtroSql = "  AND c.tel_cliente LIKE @filtro";
                }
            }

            var sql = @"
SELECT
  c.id_cliente,
  c.cpf_cliente,
  c.nome_cliente,
  c.tel_cliente,
  COALESCE(SUM(a.valor), 0) AS saldo
FROM dbo.tb_cliente c
LEFT JOIN (
  SELECT valor_caixa AS valor, id_cliente
  FROM dbo.tb_caixa
  WHERE valor_caixa <> 0 AND id_cliente IS NOT NULL

  UNION ALL

  SELECT valor_acliente AS valor, id_cliente
  FROM dbo.tb_acliente
  WHERE valor_acliente <> 0

  UNION ALL

  SELECT desconto_compra AS valor, id_cliente
  FROM dbo.tb_compra
  WHERE desconto_compra <> 0 AND id_cliente IS NOT NULL
    AND COALESCE(excluido, FALSE) = FALSE

  UNION ALL

  SELECT subtot_compra - desconto_compra - valor_nota AS valor, id_cliente
  FROM dbo.tb_compra
  WHERE subtot_compra - desconto_compra - valor_nota <> 0 AND id_cliente IS NOT NULL
    AND COALESCE(excluido, FALSE) = FALSE
) a ON a.id_cliente = c.id_cliente
WHERE COALESCE(c.excluido, FALSE) = FALSE
" + filtroSql + @"
GROUP BY c.id_cliente, c.cpf_cliente, c.nome_cliente, c.tel_cliente
ORDER BY c.nome_cliente;";

            var clientes = new List<tb_cliente>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                if (!string.IsNullOrWhiteSpace(filtroValor))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtroValor + "%");
                }

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new tb_cliente
                        {
                            id_cliente = reader.GetInt32(reader.GetOrdinal("id_cliente")),
                            cpf_cliente = reader.IsDBNull(reader.GetOrdinal("cpf_cliente"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("cpf_cliente")),
                            nome_cliente = reader.IsDBNull(reader.GetOrdinal("nome_cliente"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("nome_cliente")),
                            tel_cliente = reader.IsDBNull(reader.GetOrdinal("tel_cliente"))
                                ? string.Empty
                                : reader.GetString(reader.GetOrdinal("tel_cliente")),
                            Saldo = reader.IsDBNull(reader.GetOrdinal("saldo"))
                                ? 0m
                                : reader.GetDecimal(reader.GetOrdinal("saldo"))
                        });
                    }
                }
            }

            return clientes;
        }

        public static void InserirCliente(string connectionString, string nomeCliente, string cpfCliente, string telCliente)
        {
            const string sql = @"
INSERT INTO dbo.tb_cliente (nome_cliente, cpf_cliente, tel_cliente)
VALUES (@nome_cliente, @cpf_cliente, @tel_cliente);";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nome_cliente", (object)(nomeCliente ?? string.Empty));
                cmd.Parameters.AddWithValue("@cpf_cliente", (object)cpfCliente ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tel_cliente", (object)telCliente ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void AtualizarCliente(string connectionString, int idCliente, string nomeCliente, string cpfCliente, string telCliente)
        {
            const string sql = @"
UPDATE dbo.tb_cliente
SET nome_cliente = @nome_cliente,
    cpf_cliente = @cpf_cliente,
    tel_cliente = @tel_cliente
WHERE id_cliente = @id_cliente
  AND COALESCE(excluido, FALSE) = FALSE;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                cmd.Parameters.AddWithValue("@nome_cliente", (object)(nomeCliente ?? string.Empty));
                cmd.Parameters.AddWithValue("@cpf_cliente", (object)cpfCliente ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tel_cliente", (object)telCliente ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void ExcluirCliente(string connectionString, int idCliente)
        {
            const string sql = @"
UPDATE dbo.tb_cliente
SET excluido = TRUE
WHERE id_cliente = @id_cliente;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_cliente", idCliente);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable CarregarMovimentacaoCliente(string connectionString, int idCliente, bool resumido)
        {
            var sql = resumido
                ? @"
SELECT a.data AS ""Data"", a.valor AS ""Valor"", a.obs AS ""Obs""
FROM (
  SELECT data_caixa AS data, valor_caixa AS valor, desc_caixa AS obs
  FROM dbo.tb_caixa
  WHERE id_cliente = @id_cliente AND valor_caixa <> 0

  UNION ALL

  SELECT data_acliente AS data, valor_acliente AS valor, desc_acliente AS obs
  FROM dbo.tb_acliente
  WHERE id_cliente = @id_cliente AND valor_acliente <> 0

  UNION ALL

  SELECT data_compra AS data, desconto_compra AS valor, 'Pg na Nota: ' || id_compra::text AS obs
  FROM dbo.tb_compra
  WHERE id_cliente = @id_cliente AND desconto_compra <> 0
    AND COALESCE(excluido, FALSE) = FALSE

  UNION ALL

  SELECT data_compra AS data, subtot_compra - desconto_compra - valor_nota AS valor, 'Credito na Nota: ' || id_compra::text AS obs
  FROM dbo.tb_compra
  WHERE id_cliente = @id_cliente AND subtot_compra - desconto_compra - valor_nota <> 0
    AND COALESCE(excluido, FALSE) = FALSE
) a
ORDER BY a.data;"
                : @"
SELECT
  a.data AS ""Data"",
  a.valor_nota AS ""Valor Nota"",
  a.pagamento AS ""Recebido do cliente"",
  a.valor_pago AS ""Pago ao cliente"",
  a.credito AS ""credito"",
  a.obs AS ""Observacao""
FROM (
  SELECT data_caixa AS data, 0.00 AS valor_nota, 0.00 AS pagamento, valor_caixa * -1 AS valor_pago, 0.00 AS credito, desc_caixa AS obs
  FROM dbo.tb_caixa
  WHERE id_cliente = @id_cliente AND valor_caixa <= 0

  UNION ALL

  SELECT data_caixa AS data, 0.00 AS valor_nota, valor_caixa AS pagamento, 0.00 AS valor_pago, 0.00 AS credito, desc_caixa AS obs
  FROM dbo.tb_caixa
  WHERE id_cliente = @id_cliente AND valor_caixa > 0

  UNION ALL

  SELECT data_acliente AS data, 0.00 AS valor_nota, 0.00 AS pagamento, valor_acliente * -1 AS valor_pago, 0.00 AS credito, desc_acliente AS obs
  FROM dbo.tb_acliente
  WHERE id_cliente = @id_cliente AND valor_acliente <= 0

  UNION ALL

  SELECT data_acliente AS data, 0.00 AS valor_nota, valor_acliente AS pagamento, 0.00 AS valor_pago, 0.00 AS credito, desc_acliente AS obs
  FROM dbo.tb_acliente
  WHERE id_cliente = @id_cliente AND valor_acliente > 0

  UNION ALL

  SELECT data_compra AS data, subtot_compra AS valor_nota, desconto_compra AS pagamento, valor_nota AS valor_pago, subtot_compra - desconto_compra - valor_nota AS credito, 'Nota: ' || id_compra::text AS obs
  FROM dbo.tb_compra
  WHERE id_cliente = @id_cliente
    AND COALESCE(excluido, FALSE) = FALSE
) a
ORDER BY a.data;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public static string BuscarNomeUsuario(string connectionString, int idUsuario)
        {
            const string sql = "SELECT nome_usuario FROM dbo.tb_usuario WHERE id_usuario = @id_usuario;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                conn.Open();

                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? string.Empty : Convert.ToString(result);
            }
        }

        public static string BuscarNomeCliente(string connectionString, int idCliente)
        {
            const string sql = "SELECT nome_cliente FROM dbo.tb_cliente WHERE id_cliente = @id_cliente AND COALESCE(excluido, FALSE) = FALSE;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                conn.Open();

                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? string.Empty : Convert.ToString(result);
            }
        }

        public static DataTable ListarCompras(string connectionString, DateTime? inicio, DateTime? fim, int? idCompra)
        {
            const string sql = @"
SELECT id_compra, data_compra, subtot_compra, desconto_compra, valor_nota, usuario, id_cliente
FROM dbo.tb_compra
WHERE (@id_compra IS NULL OR id_compra = @id_compra)
  AND (@inicio IS NULL OR data_compra >= @inicio)
  AND (@fim IS NULL OR data_compra <= @fim)
  AND COALESCE(excluido, FALSE) = FALSE
ORDER BY id_compra;";

            return PreencherNotas(connectionString, sql, inicio, fim, idCompra, "@id_compra");
        }

        public static DataTable ListarVendas(string connectionString, DateTime? inicio, DateTime? fim, int? idVenda)
        {
            const string sql = @"
SELECT id_venda, data_venda, valor_nota, usuario
FROM dbo.tb_venda
WHERE (@id_venda IS NULL OR id_venda = @id_venda)
  AND (@inicio IS NULL OR data_venda >= @inicio)
  AND (@fim IS NULL OR data_venda <= @fim)
  AND COALESCE(excluido, FALSE) = FALSE
ORDER BY id_venda;";

            return PreencherNotas(connectionString, sql, inicio, fim, idVenda, "@id_venda");
        }

        private static DataTable PreencherNotas(string connectionString, string sql, DateTime? inicio, DateTime? fim, int? idNota, string idParametro)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.Add(new NpgsqlParameter(idParametro, NpgsqlTypes.NpgsqlDbType.Integer)
                {
                    Value = (object)idNota ?? DBNull.Value
                });
                cmd.Parameters.Add(new NpgsqlParameter("@inicio", NpgsqlTypes.NpgsqlDbType.Timestamp)
                {
                    Value = (object)inicio ?? DBNull.Value
                });
                cmd.Parameters.Add(new NpgsqlParameter("@fim", NpgsqlTypes.NpgsqlDbType.Timestamp)
                {
                    Value = (object)fim ?? DBNull.Value
                });

                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public static DataTable CarregarEstoqueAtual(string connectionString)
        {
            const string sql = @"
SELECT
  e.cod_prod AS id_prod,
  e.cod_prod,
  p.desc_prod,
  SUM(e.entrada) - SUM(e.saida) AS qunt_est
FROM (
  SELECT i.cod_prod, SUM(i.quant_item) AS entrada, 0::numeric AS saida
  FROM dbo.tb_itemc i
  INNER JOIN dbo.tb_compra c ON i.id_compra = c.id_compra
  WHERE COALESCE(c.excluido, FALSE) = FALSE
    AND COALESCE(i.excluido, FALSE) = FALSE
  GROUP BY i.cod_prod

  UNION ALL

  SELECT i.cod_prod, 0::numeric AS entrada, SUM(i.quant_item) AS saida
  FROM dbo.tb_itemv i
  INNER JOIN dbo.tb_venda v ON i.id_venda = v.id_venda
  WHERE COALESCE(v.excluido, FALSE) = FALSE
    AND COALESCE(i.excluido, FALSE) = FALSE
  GROUP BY i.cod_prod
) e
INNER JOIN dbo.tb_produtos p ON e.cod_prod = p.cod_prod
WHERE COALESCE(p.excluido, FALSE) = FALSE
GROUP BY p.desc_prod, e.cod_prod
ORDER BY e.cod_prod;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public static DataTable CarregarResumoCompraProdutos(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT
  i.cod_prod AS id_prod,
  i.cod_prod,
  p.desc_prod,
  SUM(i.quant_item) AS ""Peso"",
  SUM(i.subtot_item) AS ""Total""
FROM dbo.tb_itemc i
INNER JOIN dbo.tb_produtos p ON i.cod_prod = p.cod_prod
INNER JOIN dbo.tb_compra c ON i.id_compra = c.id_compra
WHERE c.data_compra BETWEEN @inicio AND @fim
  AND COALESCE(c.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE
  AND COALESCE(p.excluido, FALSE) = FALSE
GROUP BY i.cod_prod, p.desc_prod
ORDER BY i.cod_prod;";

            return PreencherPeriodo(connectionString, sql, inicio, fim);
        }

        public static DataTable CarregarRelatorioCompraItens(string connectionString, int idCompra)
        {
            const string sql = @"
SELECT
  i.cod_prod AS id_prod,
  i.cod_prod,
  i.quant_item,
  i.subtot_item AS ""subTot_item"",
  i.valor_item,
  p.desc_prod
FROM dbo.tb_itemc i
INNER JOIN dbo.tb_compra c ON i.id_compra = c.id_compra
INNER JOIN dbo.tb_produtos p ON i.cod_prod = p.cod_prod
WHERE i.id_compra = @id_compra
  AND COALESCE(c.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE
  AND COALESCE(p.excluido, FALSE) = FALSE
ORDER BY i.id_item;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public static DataTable CarregarRelatorioCompraCabecalho(string connectionString, int idCompra)
        {
            const string sql = @"
SELECT
  c.id_compra,
  c.data_compra,
  c.desconto_compra,
  c.subtot_compra,
  c.valor_nota,
  c.id_cliente,
  c.usuario,
  u.nome_usuario
FROM dbo.tb_compra c
INNER JOIN dbo.tb_usuario u ON c.usuario = u.id_usuario
WHERE c.id_compra = @id_compra
  AND COALESCE(c.excluido, FALSE) = FALSE;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public static DataTable CarregarRelatorioCompraCliente(string connectionString, int idCompra)
        {
            const string sql = @"
SELECT
  cl.id_cliente,
  cl.nome_cliente,
  cl.tel_cliente,
  cl.cpf_cliente
FROM dbo.tb_compra c
LEFT JOIN dbo.tb_cliente cl ON c.id_cliente = cl.id_cliente
WHERE c.id_compra = @id_compra
  AND COALESCE(c.excluido, FALSE) = FALSE;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public static DataTable CalcularResumoCompraCaixa(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT
  COALESCE((SELECT SUM(valor_caixa) * -1 FROM dbo.tb_caixa WHERE data_caixa < @inicio AND valor_caixa < 0), 0) AS saida_antes,
  COALESCE((SELECT SUM(valor_caixa) FROM dbo.tb_caixa WHERE data_caixa < @inicio AND valor_caixa > 0), 0) AS entrada_antes,
  COALESCE((SELECT SUM(i.subtot_item) FROM dbo.tb_itemc i INNER JOIN dbo.tb_compra c ON i.id_compra = c.id_compra WHERE c.data_compra < @inicio AND COALESCE(c.excluido, FALSE) = FALSE AND COALESCE(i.excluido, FALSE) = FALSE), 0) AS compra_antes,
  COALESCE((SELECT SUM(desconto_compra) FROM dbo.tb_compra WHERE data_compra < @inicio AND COALESCE(excluido, FALSE) = FALSE), 0) AS desconto_antes,
  COALESCE((SELECT SUM(subtot_compra - desconto_compra - valor_nota) FROM dbo.tb_compra WHERE data_compra < @inicio AND COALESCE(excluido, FALSE) = FALSE), 0) AS credito_antes,
  COALESCE((SELECT SUM(valor_caixa) * -1 FROM dbo.tb_caixa WHERE data_caixa BETWEEN @inicio AND @fim AND valor_caixa < 0), 0) AS saida_periodo,
  COALESCE((SELECT SUM(valor_caixa) FROM dbo.tb_caixa WHERE data_caixa BETWEEN @inicio AND @fim AND valor_caixa > 0), 0) AS entrada_periodo,
  COALESCE((SELECT SUM(i.subtot_item) FROM dbo.tb_itemc i INNER JOIN dbo.tb_compra c ON i.id_compra = c.id_compra WHERE c.data_compra BETWEEN @inicio AND @fim AND COALESCE(c.excluido, FALSE) = FALSE AND COALESCE(i.excluido, FALSE) = FALSE), 0) AS compra_periodo,
  COALESCE((SELECT SUM(desconto_compra) FROM dbo.tb_compra WHERE data_compra BETWEEN @inicio AND @fim AND COALESCE(excluido, FALSE) = FALSE), 0) AS desconto_periodo,
  COALESCE((SELECT SUM(subtot_compra - desconto_compra - valor_nota) FROM dbo.tb_compra WHERE data_compra BETWEEN @inicio AND @fim AND COALESCE(excluido, FALSE) = FALSE), 0) AS credito_periodo;";

            return PreencherPeriodo(connectionString, sql, inicio, fim);
        }

        public static DataTable CarregarResumoVendaProdutos(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT
  i.cod_prod AS id_prod,
  i.cod_prod,
  p.desc_prod,
  SUM(i.quant_item) AS ""Peso"",
  SUM(i.subtot_item) AS ""Total""
FROM dbo.tb_itemv i
INNER JOIN dbo.tb_produtos p ON i.cod_prod = p.cod_prod
INNER JOIN dbo.tb_venda v ON i.id_venda = v.id_venda
WHERE v.data_venda BETWEEN @inicio AND @fim
  AND COALESCE(v.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE
  AND COALESCE(p.excluido, FALSE) = FALSE
GROUP BY i.cod_prod, p.desc_prod
ORDER BY i.cod_prod;";

            return PreencherPeriodo(connectionString, sql, inicio, fim);
        }

        public static decimal CalcularTotalVendaProdutos(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT COALESCE(SUM(i.subtot_item), 0)
FROM dbo.tb_itemv i
INNER JOIN dbo.tb_venda v ON i.id_venda = v.id_venda
WHERE v.data_venda BETWEEN @inicio AND @fim
  AND COALESCE(v.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE;";

            return ExecutarDecimalPeriodo(connectionString, sql, inicio, fim);
        }

        public static DataTable CarregarLucroDetalhado(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT
  a.cod_prod AS codigo,
  p.desc_prod AS descricao,
  SUM(a.pesoc) AS ""pesoC"",
  SUM(a.totalc) AS ""Compra"",
  SUM(a.pesov) AS ""pesoV"",
  SUM(a.totalv) AS ""Venda"",
  SUM(a.totalv) - SUM(a.totalc) AS lucro,
  SUM(a.pesoc) - SUM(a.pesov) AS peso
FROM (
  SELECT i.cod_prod, SUM(i.quant_item) AS pesoc, SUM(i.subtot_item) AS totalc, 0::numeric AS pesov, 0::numeric AS totalv
  FROM dbo.tb_itemc i
  INNER JOIN dbo.tb_compra c ON i.id_compra = c.id_compra
  WHERE c.data_compra BETWEEN @inicio AND @fim
    AND COALESCE(c.excluido, FALSE) = FALSE
    AND COALESCE(i.excluido, FALSE) = FALSE
  GROUP BY i.cod_prod

  UNION ALL

  SELECT i.cod_prod, 0::numeric AS pesoc, 0::numeric AS totalc, SUM(i.quant_item) AS pesov, SUM(i.subtot_item) AS totalv
  FROM dbo.tb_itemv i
  INNER JOIN dbo.tb_venda v ON i.id_venda = v.id_venda
  WHERE v.data_venda BETWEEN @inicio AND @fim
    AND COALESCE(v.excluido, FALSE) = FALSE
    AND COALESCE(i.excluido, FALSE) = FALSE
  GROUP BY i.cod_prod
) a
INNER JOIN dbo.tb_produtos p ON a.cod_prod = p.cod_prod
WHERE COALESCE(p.excluido, FALSE) = FALSE
GROUP BY a.cod_prod, p.desc_prod
ORDER BY a.cod_prod;";

            return PreencherPeriodo(connectionString, sql, inicio, fim);
        }

        public static DataTable CarregarLucroTotal(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT
  SUM(e.""pesoC"") AS ""pesoCT"",
  SUM(e.""Compra"") AS ""compraT"",
  SUM(e.""pesoV"") AS ""pesoVT"",
  SUM(e.""Venda"") AS ""vendaT"",
  SUM(e.""pesoC"") - SUM(e.""pesoV"") AS ""pesoT"",
  SUM(e.""Venda"") - SUM(e.""Compra"") AS ""lucroT""
FROM (
  SELECT
    a.cod_prod AS codigo,
    p.desc_prod AS descricao,
    SUM(a.pesoc) AS ""pesoC"",
    SUM(a.totalc) AS ""Compra"",
    SUM(a.pesov) AS ""pesoV"",
    SUM(a.totalv) AS ""Venda""
  FROM (
    SELECT i.cod_prod, SUM(i.quant_item) AS pesoc, SUM(i.subtot_item) AS totalc, 0::numeric AS pesov, 0::numeric AS totalv
    FROM dbo.tb_itemc i
    INNER JOIN dbo.tb_compra c ON i.id_compra = c.id_compra
    WHERE c.data_compra BETWEEN @inicio AND @fim
      AND COALESCE(c.excluido, FALSE) = FALSE
      AND COALESCE(i.excluido, FALSE) = FALSE
    GROUP BY i.cod_prod

    UNION ALL

    SELECT i.cod_prod, 0::numeric AS pesoc, 0::numeric AS totalc, SUM(i.quant_item) AS pesov, SUM(i.subtot_item) AS totalv
    FROM dbo.tb_itemv i
    INNER JOIN dbo.tb_venda v ON i.id_venda = v.id_venda
    WHERE v.data_venda BETWEEN @inicio AND @fim
      AND COALESCE(v.excluido, FALSE) = FALSE
      AND COALESCE(i.excluido, FALSE) = FALSE
    GROUP BY i.cod_prod
  ) a
  INNER JOIN dbo.tb_produtos p ON a.cod_prod = p.cod_prod
  WHERE COALESCE(p.excluido, FALSE) = FALSE
  GROUP BY a.cod_prod, p.desc_prod
) e;";

            return PreencherPeriodo(connectionString, sql, inicio, fim);
        }

        public static DataTable CarregarFluxoCaixa(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT
  e.data::date AS ""Data"",
  SUM(e.entrada) AS ""Entrada"",
  SUM(e.saida) AS ""Saida""
FROM (
  SELECT data_caixa AS data, SUM(valor_caixa) AS entrada, 0::numeric AS saida
  FROM dbo.tb_caixa
  WHERE valor_caixa > 0
  GROUP BY data_caixa

  UNION ALL

  SELECT data_caixa AS data, 0::numeric AS entrada, SUM(valor_caixa) * -1 AS saida
  FROM dbo.tb_caixa
  WHERE valor_caixa < 0
  GROUP BY data_caixa

  UNION ALL

  SELECT data_compra AS data, 0::numeric AS entrada, SUM(valor_nota) AS saida
  FROM dbo.tb_compra
  WHERE COALESCE(excluido, FALSE) = FALSE
  GROUP BY data_compra
) e
WHERE e.data::date BETWEEN @inicio::date AND @fim::date
GROUP BY e.data::date
ORDER BY e.data::date;";

            return PreencherPeriodo(connectionString, sql, inicio.Date, fim.Date);
        }

        public static decimal CalcularSaldoInicialFluxoCaixa(string connectionString, DateTime inicio)
        {
            const string sql = @"
SELECT
  COALESCE((SELECT SUM(valor_caixa) FROM dbo.tb_caixa WHERE data_caixa < @inicio), 0)
  - COALESCE((SELECT SUM(valor_nota) FROM dbo.tb_compra WHERE data_compra < @inicio AND COALESCE(excluido, FALSE) = FALSE), 0);";

            return ExecutarDecimalAte(connectionString, sql, inicio.Date);
        }

        public static DataTable CarregarEstoquePeriodo(string connectionString, DateTime inicio, DateTime fim, string codigoProduto)
        {
            const string sql = @"
WITH estoque AS (
  SELECT cod_prod, SUM(inicio) AS inicio, 0::numeric AS entrada, 0::numeric AS saida
  FROM (
    SELECT i.cod_prod, SUM(i.quant_item) AS inicio
    FROM dbo.tb_itemc i
    INNER JOIN dbo.tb_compra c ON c.id_compra = i.id_compra
    WHERE c.data_compra < @inicio
      AND COALESCE(c.excluido, FALSE) = FALSE
      AND COALESCE(i.excluido, FALSE) = FALSE
    GROUP BY i.cod_prod

    UNION ALL

    SELECT i.cod_prod, -SUM(i.quant_item) AS inicio
    FROM dbo.tb_itemv i
    INNER JOIN dbo.tb_venda v ON v.id_venda = i.id_venda
    WHERE v.data_venda < @inicio
      AND COALESCE(v.excluido, FALSE) = FALSE
      AND COALESCE(i.excluido, FALSE) = FALSE
    GROUP BY i.cod_prod
  ) x
  GROUP BY cod_prod

  UNION ALL

  SELECT i.cod_prod, 0::numeric AS inicio, SUM(i.quant_item) AS entrada, 0::numeric AS saida
  FROM dbo.tb_itemc i
  INNER JOIN dbo.tb_compra c ON c.id_compra = i.id_compra
  WHERE c.data_compra BETWEEN @inicio AND @fim
    AND COALESCE(c.excluido, FALSE) = FALSE
    AND COALESCE(i.excluido, FALSE) = FALSE
  GROUP BY i.cod_prod

  UNION ALL

  SELECT i.cod_prod, 0::numeric AS inicio, 0::numeric AS entrada, SUM(i.quant_item) AS saida
  FROM dbo.tb_itemv i
  INNER JOIN dbo.tb_venda v ON v.id_venda = i.id_venda
  WHERE v.data_venda BETWEEN @inicio AND @fim
    AND COALESCE(v.excluido, FALSE) = FALSE
    AND COALESCE(i.excluido, FALSE) = FALSE
  GROUP BY i.cod_prod
)
SELECT
  e.cod_prod AS codigo,
  UPPER(p.desc_prod) AS descricao,
  COALESCE(SUM(e.inicio), 0) AS inicio,
  COALESCE(SUM(e.entrada), 0) AS entrada,
  COALESCE(SUM(e.saida), 0) AS saida,
  COALESCE(SUM(e.inicio), 0) + COALESCE(SUM(e.entrada), 0) - COALESCE(SUM(e.saida), 0) AS saldo
FROM estoque e
INNER JOIN dbo.tb_produtos p ON p.cod_prod = e.cod_prod
WHERE (@cod_prod IS NULL OR e.cod_prod = @cod_prod)
  AND COALESCE(p.excluido, FALSE) = FALSE
GROUP BY p.desc_prod, e.cod_prod
ORDER BY e.cod_prod;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fim", fim);
                cmd.Parameters.Add(new NpgsqlParameter("@cod_prod", NpgsqlTypes.NpgsqlDbType.Varchar)
                {
                    Value = string.IsNullOrWhiteSpace(codigoProduto) ? (object)DBNull.Value : codigoProduto.Trim()
                });

                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public static DataTable CarregarProdutosDataTable(string connectionString)
        {
            const string sql = "SELECT cod_prod AS id_prod, cod_prod, desc_prod, val_prod, usuario FROM dbo.tb_produtos WHERE COALESCE(excluido, FALSE) = FALSE ORDER BY cod_prod;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        private static DataTable PreencherPeriodo(string connectionString, string sql, DateTime inicio, DateTime fim)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fim", fim);

                conn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        private static decimal ExecutarDecimalPeriodo(string connectionString, string sql, DateTime inicio, DateTime fim)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fim", fim);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0m : Convert.ToDecimal(result);
            }
        }

        private static decimal ExecutarDecimalAte(string connectionString, string sql, DateTime inicio)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0m : Convert.ToDecimal(result);
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
                "SELECT COALESCE(SUM(tb_compra.desconto_compra), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente AND COALESCE(tb_compra.excluido, FALSE) = FALSE;",
                idCliente);

            var credito = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente AND COALESCE(tb_compra.excluido, FALSE) = FALSE;",
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
                "SELECT COALESCE(SUM(tb_compra.desconto_compra), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente AND COALESCE(tb_compra.excluido, FALSE) = FALSE;",
                idCliente);

            var credito = ObterSomaPorCliente(
                connectionString,
                "SELECT COALESCE(SUM(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota), 0) FROM dbo.tb_compra WHERE tb_compra.id_cliente = @id_cliente AND COALESCE(tb_compra.excluido, FALSE) = FALSE;",
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

        public static void InserirItemCompra(string connectionString, string codigoProduto, int idCompra, decimal quantItem, decimal subTotItem, decimal valorItem)
        {
            const string sql = @"
INSERT INTO dbo.tb_itemc (id_prod, cod_prod, id_compra, quant_item, subtot_item, valor_item)
SELECT p.id_prod, p.cod_prod, @id_compra, @quant_item, @subtot_item, @valor_item
FROM dbo.tb_produtos p
WHERE p.cod_prod = @cod_prod
  AND COALESCE(p.excluido, FALSE) = FALSE;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@cod_prod", (object)(codigoProduto ?? string.Empty));
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                cmd.Parameters.AddWithValue("@quant_item", quantItem);
                cmd.Parameters.AddWithValue("@subtot_item", subTotItem);
                cmd.Parameters.AddWithValue("@valor_item", valorItem);

                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException("Produto nao encontrado para o codigo informado.");
                }
            }
        }

        public static List<tb_itemc> ListarItensCompra(string connectionString, int idCompra)
        {
            const string sql = @"
SELECT
  i.id_item,
  i.id_prod,
  i.cod_prod,
  i.id_compra,
  i.quant_item,
  i.subtot_item,
  i.valor_item,
  p.cod_prod AS produto_cod_prod,
  p.desc_prod,
  p.val_prod,
  p.usuario
FROM dbo.tb_itemc i
INNER JOIN dbo.tb_produtos p ON p.cod_prod = i.cod_prod
INNER JOIN dbo.tb_compra c ON c.id_compra = i.id_compra
WHERE i.id_compra = @id_compra
  AND COALESCE(c.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE
  AND COALESCE(p.excluido, FALSE) = FALSE
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
                            cod_prod = reader.IsDBNull(reader.GetOrdinal("produto_cod_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("produto_cod_prod")),
                            desc_prod = reader.IsDBNull(reader.GetOrdinal("desc_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("desc_prod")),
                            val_prod = reader.IsDBNull(reader.GetOrdinal("val_prod")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("val_prod")),
                            usuario = reader.IsDBNull(reader.GetOrdinal("usuario")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("usuario"))
                        };

                        var item = new tb_itemc
                        {
                            id_item = reader.GetInt32(reader.GetOrdinal("id_item")),
                            id_prod = reader.GetInt32(reader.GetOrdinal("id_prod")),
                            cod_prod = reader.IsDBNull(reader.GetOrdinal("cod_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("cod_prod")),
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
            const string sql = @"
UPDATE dbo.tb_itemc
SET excluido = TRUE
WHERE id_item = @id_item;";

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
            const string sql = @"
UPDATE dbo.tb_compra
SET excluido = TRUE
WHERE id_compra = @id_compra;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id_compra", idCompra);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static decimal CalcularSaldoProduto(string connectionString, string codigoProduto)
        {
            const string sqlEntrada = @"
SELECT COALESCE(SUM(i.quant_item), 0)
FROM dbo.tb_itemc i
INNER JOIN dbo.tb_compra c ON c.id_compra = i.id_compra
WHERE i.cod_prod = @cod_prod
  AND COALESCE(c.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE;";
            const string sqlSaida = @"
SELECT COALESCE(SUM(i.quant_item), 0)
FROM dbo.tb_itemv i
INNER JOIN dbo.tb_venda v ON v.id_venda = i.id_venda
WHERE i.cod_prod = @cod_prod
  AND COALESCE(v.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                decimal entrada;
                using (var cmd = new NpgsqlCommand(sqlEntrada, conn))
                {
                    cmd.Parameters.AddWithValue("@cod_prod", (object)(codigoProduto ?? string.Empty));
                    entrada = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
                }

                decimal saida;
                using (var cmd = new NpgsqlCommand(sqlSaida, conn))
                {
                    cmd.Parameters.AddWithValue("@cod_prod", (object)(codigoProduto ?? string.Empty));
                    saida = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
                }

                return entrada - saida;
            }
        }

        public static decimal CalcularSaldoCaixa(string connectionString)
        {
            const string sql = @"
SELECT
  COALESCE((SELECT SUM(i.subtot_item) FROM dbo.tb_itemc i INNER JOIN dbo.tb_compra c ON c.id_compra = i.id_compra WHERE COALESCE(c.excluido, FALSE) = FALSE AND COALESCE(i.excluido, FALSE) = FALSE), 0) AS total_saida,
  COALESCE((SELECT SUM(valor_caixa) FROM dbo.tb_caixa), 0) AS total_entrada,
  COALESCE((SELECT SUM(desconto_compra) FROM dbo.tb_compra WHERE COALESCE(excluido, FALSE) = FALSE), 0) AS desconto,
  COALESCE((SELECT SUM(subtot_compra - desconto_compra - valor_nota) FROM dbo.tb_compra WHERE COALESCE(excluido, FALSE) = FALSE), 0) AS credito;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return 0m;
                    }

                    var totalSaida = reader.GetDecimal(reader.GetOrdinal("total_saida"));
                    var totalEntrada = reader.GetDecimal(reader.GetOrdinal("total_entrada"));
                    var desconto = reader.GetDecimal(reader.GetOrdinal("desconto"));
                    var credito = reader.GetDecimal(reader.GetOrdinal("credito"));

                    return totalEntrada - totalSaida + desconto + credito;
                }
            }
        }

        public static void InserirCaixa(string connectionString, DateTime dataCaixa, string descricao, int usuario, decimal valorCaixa, int? idCliente)
        {
            const string sql = @"
INSERT INTO dbo.tb_caixa (data_caixa, desc_caixa, usuario, valor_caixa, id_cliente)
VALUES (@data_caixa, @desc_caixa, @usuario, @valor_caixa, @id_cliente);";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@data_caixa", dataCaixa);
                cmd.Parameters.AddWithValue("@desc_caixa", (object)descricao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@valor_caixa", valorCaixa);
                cmd.Parameters.AddWithValue("@id_cliente", (object)idCliente ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void InserirAcertoCliente(string connectionString, DateTime dataAcerto, string descricao, int usuario, decimal valorAcerto, int idCliente)
        {
            const string sql = @"
INSERT INTO dbo.tb_acliente (data_acliente, desc_acliente, usuario, valor_acliente, id_cliente)
VALUES (@data_acliente, @desc_acliente, @usuario, @valor_acliente, @id_cliente);";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@data_acliente", dataAcerto);
                cmd.Parameters.AddWithValue("@desc_acliente", (object)descricao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@valor_acliente", valorAcerto);
                cmd.Parameters.AddWithValue("@id_cliente", idCliente);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable CarregarMovimentacaoRecursos(string connectionString, DateTime inicio, DateTime fim)
        {
            const string sql = @"
SELECT
  a.data AS ""Data"",
  SUM(a.valor) AS ""Valor"",
  a.descricao AS ""Descricao"",
  u.nome_usuario AS ""Usuario""
FROM (
  SELECT c.data_caixa::date AS data, c.valor_caixa AS valor, c.desc_caixa AS descricao, c.usuario
  FROM dbo.tb_caixa c
  WHERE c.id_cliente IS NOT NULL

  UNION ALL

  SELECT c.data_caixa::date AS data, c.valor_caixa AS valor, c.desc_caixa AS descricao, c.usuario
  FROM dbo.tb_caixa c
  WHERE c.id_cliente IS NULL

  UNION ALL

  SELECT co.data_compra::date AS data, co.valor_nota * -1 AS valor, 'Compras' AS descricao, co.usuario
  FROM dbo.tb_compra co
  WHERE COALESCE(co.excluido, FALSE) = FALSE
) a
INNER JOIN dbo.tb_usuario u ON a.usuario = u.id_usuario
WHERE a.data BETWEEN @inicio AND @fim
GROUP BY a.data, a.descricao, u.nome_usuario
ORDER BY a.data;";

            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var da = new NpgsqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio.Date);
                cmd.Parameters.AddWithValue("@fim", fim.Date);
                conn.Open();
                da.Fill(dt);
            }

            return dt;
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

        public static void InserirItemVenda(string connectionString, string codigoProduto, int idVenda, decimal quantItem, decimal subTotItem, decimal valrItem)
        {
            const string sql = @"
INSERT INTO dbo.tb_itemv (id_prod, cod_prod, id_venda, quant_item, subtot_item, valr_item)
SELECT p.id_prod, p.cod_prod, @id_venda, @quant_item, @subtot_item, @valr_item
FROM dbo.tb_produtos p
WHERE p.cod_prod = @cod_prod
  AND COALESCE(p.excluido, FALSE) = FALSE;";

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@cod_prod", (object)(codigoProduto ?? string.Empty));
                cmd.Parameters.AddWithValue("@id_venda", idVenda);
                cmd.Parameters.AddWithValue("@quant_item", quantItem);
                cmd.Parameters.AddWithValue("@subtot_item", subTotItem);
                cmd.Parameters.AddWithValue("@valr_item", valrItem);

                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException("Produto nao encontrado para o codigo informado.");
                }
            }
        }

        public static List<tb_itemv> ListarItensVenda(string connectionString, int idVenda)
        {
            const string sql = @"
SELECT
  i.id_item,
  i.id_prod,
  i.cod_prod,
  i.id_venda,
  i.quant_item,
  i.subtot_item,
  i.valr_item,
  p.cod_prod AS produto_cod_prod,
  p.desc_prod,
  p.val_prod,
  p.usuario
FROM dbo.tb_itemv i
INNER JOIN dbo.tb_produtos p ON p.cod_prod = i.cod_prod
INNER JOIN dbo.tb_venda v ON v.id_venda = i.id_venda
WHERE i.id_venda = @id_venda
  AND COALESCE(v.excluido, FALSE) = FALSE
  AND COALESCE(i.excluido, FALSE) = FALSE
  AND COALESCE(p.excluido, FALSE) = FALSE
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
                            cod_prod = reader.IsDBNull(reader.GetOrdinal("produto_cod_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("produto_cod_prod")),
                            desc_prod = reader.IsDBNull(reader.GetOrdinal("desc_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("desc_prod")),
                            val_prod = reader.IsDBNull(reader.GetOrdinal("val_prod")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("val_prod")),
                            usuario = reader.IsDBNull(reader.GetOrdinal("usuario")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("usuario"))
                        };

                        var item = new tb_itemv
                        {
                            id_item = reader.GetInt32(reader.GetOrdinal("id_item")),
                            id_prod = reader.IsDBNull(reader.GetOrdinal("id_prod")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_prod")),
                            cod_prod = reader.IsDBNull(reader.GetOrdinal("cod_prod")) ? string.Empty : reader.GetString(reader.GetOrdinal("cod_prod")),
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
            const string sql = @"
UPDATE dbo.tb_itemv
SET excluido = TRUE
WHERE id_item = @id_item;";

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
            const string sql = @"
UPDATE dbo.tb_venda
SET excluido = TRUE
WHERE id_venda = @id_venda;";

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
  iv.subtot_item AS ""subTot_item"",
  iv.valr_item,
  p.desc_prod,
  v.data_venda,
  iv.cod_prod AS id_prod,
  iv.cod_prod,
  iv.id_venda,
  u.nome_usuario,
  v.usuario
FROM dbo.tb_itemv iv
INNER JOIN dbo.tb_venda v ON iv.id_venda = v.id_venda
INNER JOIN dbo.tb_produtos p ON iv.cod_prod = p.cod_prod
INNER JOIN dbo.tb_usuario u ON v.usuario = u.id_usuario
WHERE iv.id_venda = @id_venda
  AND COALESCE(v.excluido, FALSE) = FALSE
  AND COALESCE(iv.excluido, FALSE) = FALSE
  AND COALESCE(p.excluido, FALSE) = FALSE;";

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
