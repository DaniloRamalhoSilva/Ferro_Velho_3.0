using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FerroVelhoDAO
{
    public class DataContextFactory
    {
        private static FerroVelhoDataContext dataContext;
        public static tb_usuario usu;
        public static string nome;
        public static string tel;
        public static string endereco;
        public static string conexaoUser;
        public static string conexaoImp;

        public static bool IsPostgresConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return false;
            }

            var cs = connectionString.Trim();
            return cs.IndexOf("Host=", StringComparison.OrdinalIgnoreCase) >= 0
                || cs.IndexOf("Username=", StringComparison.OrdinalIgnoreCase) >= 0
                || cs.IndexOf("SSL Mode=", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool IsSqlServerConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return false;
            }

            var cs = connectionString.Trim();
            return cs.IndexOf("Data Source=", StringComparison.OrdinalIgnoreCase) >= 0
                || cs.IndexOf("Server=", StringComparison.OrdinalIgnoreCase) >= 0
                || cs.IndexOf("Initial Catalog=", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static tb_usuario ValidarLoginPostgres(string nomeUsuario, string senhaUsuario)
        {
            var login = PostgresConnectionService.ValidarLogin(conexaoUser, nomeUsuario, senhaUsuario);
            if (login == null)
            {
                return null;
            }

            var tipo = new tb_tipoUsuario
            {
                id_tipoUsuario = login.PermissaoUsuario,
                desc_tipoUsuario = login.DescricaoPermissao
            };

            var usuario = new tb_usuario
            {
                id_usuario = login.UsuarioId,
                nome_usuario = login.NomeUsuario,
                senha_usuario = login.SenhaUsuario,
                permi_usuario = login.PermissaoUsuario,
                ativo = login.Ativo
            };

            usuario.tb_tipoUsuario = tipo;
            return usuario;
        }

        public static bool TestarConexaoPostgres(string connectionString)
        {
            return PostgresConnectionService.TestarConexao(connectionString);
        }

        public static List<tb_produtos> ListarProdutosPostgres()
        {
            return PostgresConnectionService.ListarProdutos(conexaoUser);
        }

        public static tb_impressora BuscarImpressoraPostgres(int idImpressora)
        {
            return PostgresConnectionService.BuscarImpressoraPorId(conexaoImp, idImpressora);
        }

        public static decimal CalcularValorDevedorPostgres(int idCliente)
        {
            return PostgresConnectionService.CalcularValorDevedor(conexaoImp, idCliente);
        }

        public static decimal CalcularValorCreditoPostgres(int idCliente)
        {
            return PostgresConnectionService.CalcularValorCredito(conexaoImp, idCliente);
        }

        public static tb_compra CriarCompraPostgres(DateTime dataCompra, int usuario, decimal descontoCompra, decimal subtotCompra, decimal valorNota)
        {
            return PostgresConnectionService.CriarCompra(conexaoImp, dataCompra, usuario, descontoCompra, subtotCompra, valorNota);
        }

        public static void AtualizarCompraPostgres(int idCompra, decimal descontoCompra, decimal subtotCompra, decimal valorNota, int? idCliente)
        {
            PostgresConnectionService.AtualizarCompraValores(conexaoImp, idCompra, descontoCompra, subtotCompra, valorNota, idCliente);
        }

        public static void InserirItemCompraPostgres(int idProd, int idCompra, decimal quantItem, decimal subTotItem, decimal valorItem)
        {
            PostgresConnectionService.InserirItemCompra(conexaoImp, idProd, idCompra, quantItem, subTotItem, valorItem);
        }

        public static List<tb_itemc> ListarItensCompraPostgres(int idCompra)
        {
            return PostgresConnectionService.ListarItensCompra(conexaoImp, idCompra);
        }

        public static void ExcluirItemCompraPostgres(int idItem)
        {
            PostgresConnectionService.ExcluirItemCompra(conexaoImp, idItem);
        }

        public static void ExcluirCompraPostgres(int idCompra)
        {
            PostgresConnectionService.ExcluirCompra(conexaoImp, idCompra);
        }

        public static FerroVelhoDataContext DataContext
        {
            get
            {
                if (dataContext == null)
                    dataContext = new FerroVelhoDataContext();

                // LINQ-to-SQL usa SqlClient; nao pode receber string de conexao PostgreSQL.
                if (IsSqlServerConnectionString(conexaoUser))
                {
                    dataContext.Connection.ConnectionString = conexaoUser;
                }

                return dataContext;
            } 
        }

        public static SqlConnection Conectar()
        {
            SqlConnection con = new SqlConnection(conexaoImp);
            con.Open();
            return con;

        }

        public static DataTable GetDataTableBySP(string storedProcedure, object[] arrParametros = null, object[] arrParametrosValores = null, bool enviarDbNullValue = true)
        {
            try
            {
                DataTable dt = new DataTable();

                SqlConnection conn = Conectar();

                SqlCommand cmd = new SqlCommand(storedProcedure, conn);
                cmd.CommandTimeout = 45000;
                cmd.CommandType = CommandType.StoredProcedure;
                //conn.Open();

                if (arrParametros != null && arrParametrosValores != null && arrParametros.Length == arrParametrosValores.Length)
                {
                    for (int i = 0; i < arrParametros.Length; i++)
                    {
                        if (arrParametrosValores[i] != null)
                        {
                            if (arrParametrosValores[i].GetType() != typeof(byte[]))
                                cmd.Parameters.Add(new SqlParameter(arrParametros[i].ToString(), arrParametrosValores[i].ToString()));
                            else
                                cmd.Parameters.Add(new SqlParameter(arrParametros[i].ToString(), arrParametrosValores[i]));
                        }
                        else if (enviarDbNullValue)
                            cmd.Parameters.Add(new SqlParameter(arrParametros[i].ToString(), DBNull.Value));
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                conn.Close();

                if (cmd != null) cmd.Dispose();
                if (da != null) da.Dispose();
                if (conn != null) conn.Close();
                if (conn != null) conn.Dispose();

                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                e.Data["storedProcedure"] = storedProcedure;
                e.Data["arrParametros"] = arrParametros != null ? string.Join(",", arrParametros) : null;
                e.Data["arrParametrosValores"] = arrParametrosValores != null ? string.Join(",", arrParametrosValores) : null;
                e.Data["enviarDbNullValue"] = enviarDbNullValue;

                throw;
            }
        }


        public static DataTable Filtrar(string comando)
        {
            SqlConnection con = Conectar();
            SqlDataAdapter da = new SqlDataAdapter("", con);
            da.SelectCommand.CommandText = comando;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
            
        }

        public static decimal FiltrarValor (SqlCommand comando)
        {

            SqlConnection con = Conectar();
            comando.Connection = con;
            SqlDataReader dr = comando.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            try
            {
                dr.Read();
                decimal aki = (decimal)dr["total"];
                con.Close();
                return aki;
                
            }
            catch
            {
                con.Close();
                return 0;
                
            }
            

        }

        public static void CRUD(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            comando.ExecuteNonQuery();
            con.Close();

        }

        public static SqlDataReader CRUDID(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            SqlDataReader dr = comando.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return dr;
        }

        public static SqlDataReader Selecionar(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            SqlDataReader dr = comando.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return dr;
        }

        public static void GravarCabecario(string nome, string tel, string endereco)
        {
            XmlTextWriter STW_Arquivo;
            STW_Arquivo = new XmlTextWriter(@"..\..\cabeçalho.xml", Encoding.UTF8);
            STW_Arquivo.WriteStartElement("cabecario");
            STW_Arquivo.WriteElementString("Nome", nome.Trim());
            STW_Arquivo.WriteElementString("Telefone", tel.Trim());
            STW_Arquivo.WriteElementString("Endereco", endereco.Trim());
            STW_Arquivo.WriteEndElement();
            STW_Arquivo.Close();
        }

        public static void GravarConecxao(string dataUser, string dataImp)
        {
            XmlTextWriter STW_Arquivo;
            STW_Arquivo = new XmlTextWriter(@"..\..\configuração.xml", Encoding.UTF8);
            STW_Arquivo.WriteStartElement("configConexao");
            STW_Arquivo.WriteElementString("dataUser", dataUser.Trim());
            STW_Arquivo.WriteElementString("dataImp", dataImp.Trim());


            STW_Arquivo.WriteEndElement();
            STW_Arquivo.Close();
        }

        public static void FU_lerCabecario()
        {
            try
            {
                XElement XML = XElement.Load(@"..\..\cabeçalho.xml");

                nome = XML.Element("Nome").Value;
                tel = XML.Element("Telefone").Value;
                endereco = XML.Element("Endereco").Value;

                XML = null;
            }
            catch
            {
                nome = "";
                tel = "";
                endereco = "";
            }
        }

        public static void FU_lerConfiguracao()
        {
            try
            {
                XElement XML = XElement.Load(@"..\..\configuração.xml");

                conexaoUser = XML.Element("dataUser").Value;
                conexaoImp = XML.Element("dataImp").Value;

                XML = null;
            }
            catch
            {
                conexaoUser = "";
                conexaoImp = "";
            }

        }



    }   

}
