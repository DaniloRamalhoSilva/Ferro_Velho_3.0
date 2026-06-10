using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using FerroVelho;

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

        public string _usuarioId;
        public string _usuarioPermissao;
        public string _usuarioNome;
        public string _nome;
        public string _tel;
        public string _endereco;

        public static FerroVelhoDataContext DataContext
        {
            get
            {
                if (dataContext == null)
                {
                    dataContext = new FerroVelhoDataContext();
                }

                dataContext.Connection.ConnectionString = conexaoUser;
                return dataContext;
            }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(conexaoUser);
        }

        public static SqlConnection Conectar()
        {
            string connectionString = string.IsNullOrWhiteSpace(conexaoImp) ? conexaoUser : conexaoImp;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public static DataTable Filtrar(string comando)
        {
            using (SqlConnection con = Conectar())
            using (SqlDataAdapter da = new SqlDataAdapter(comando, con))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static decimal FiltrarValor(SqlCommand comando)
        {
            using (SqlConnection con = Conectar())
            {
                comando.Connection = con;
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    try
                    {
                        dr.Read();
                        return (decimal)dr["total"];
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public static void CRUD(SqlCommand comando)
        {
            using (SqlConnection con = Conectar())
            {
                comando.Connection = con;
                comando.ExecuteNonQuery();
            }
        }

        public static SqlDataReader CRUDID(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            return comando.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader Selecionar(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            return comando.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static void GravarCabecario(string nome, string tel, string endereco)
        {
            using (XmlTextWriter arquivo = new XmlTextWriter(@"..\..\cabeçalho.xml", Encoding.UTF8))
            {
                arquivo.WriteStartElement("cabecario");
                arquivo.WriteElementString("Nome", nome.Trim());
                arquivo.WriteElementString("Telefone", tel.Trim());
                arquivo.WriteElementString("Endereco", endereco.Trim());
                arquivo.WriteEndElement();
            }
        }

        public static void GravarConecxao(string dataUser, string dataImp)
        {
            using (XmlTextWriter arquivo = new XmlTextWriter(@"..\..\configuração.xml", Encoding.UTF8))
            {
                arquivo.WriteStartElement("configConexao");
                arquivo.WriteElementString("dataUser", dataUser.Trim());
                arquivo.WriteElementString("dataImp", dataImp.Trim());
                arquivo.WriteEndElement();
            }
        }

        public static void FU_lerCabecario()
        {
            try
            {
                XElement xml = XElement.Load(@"..\..\cabeçalho.xml");

                nome = xml.Element("Nome").Value;
                tel = xml.Element("Telefone").Value;
                endereco = xml.Element("Endereco").Value;
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
                XElement xml = XElement.Load(@"..\..\configuração.xml");

                conexaoUser = xml.Element("dataUser").Value;
                conexaoImp = xml.Element("dataImp").Value;
            }
            catch
            {
                conexaoUser = "";
                conexaoImp = "";
            }
        }

        public ValidacaoEnum ValidarLoginUsuario(string nomeUsuario, string senhaUsuario)
        {
            try
            {
                string[] arrPar = new string[] { "@nome_usuario", "@senha_usuario" };
                string[] arrVal = new string[] { nomeUsuario, senhaUsuario };
                DataTable dt = GetDataTableBySP("s_tb_usuario_login", arrPar, arrVal);

                if (dt.Rows.Count == 0)
                {
                    return ValidacaoEnum.USUARIO_SENHA_INVALIDO;
                }

                DataRow usuario = dt.Rows[0];
                _usuarioId = usuario["id_usuario"].ToString();
                _usuarioNome = usuario["nome_usuario"].ToString();
                _usuarioPermissao = usuario["permi_usuario"].ToString();

                usu = new tb_usuario
                {
                    id_usuario = Convert.ToInt32(_usuarioId),
                    nome_usuario = _usuarioNome,
                    senha_usuario = senhaUsuario,
                    permi_usuario = Convert.ToInt32(_usuarioPermissao)
                };

                return ValidacaoEnum.SUCESSO;
            }
            catch
            {
                return ValidacaoEnum.SEM_CONEXA_BANCO_DADOS;
            }
        }

        public virtual DataTable GetDataTableBySP(string storedProcedure, object[] arrParametros = null, object[] arrParametrosValores = null, bool enviarDbNullValue = true)
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandTimeout = 45000;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (arrParametros != null && arrParametrosValores != null && arrParametros.Length == arrParametrosValores.Length)
                    {
                        for (int i = 0; i < arrParametros.Length; i++)
                        {
                            if (arrParametrosValores[i] != null)
                            {
                                if (arrParametrosValores[i].GetType() != typeof(byte[]))
                                {
                                    cmd.Parameters.Add(new SqlParameter(arrParametros[i].ToString(), arrParametrosValores[i].ToString()));
                                }
                                else
                                {
                                    cmd.Parameters.Add(new SqlParameter(arrParametros[i].ToString(), arrParametrosValores[i]));
                                }
                            }
                            else if (enviarDbNullValue)
                            {
                                cmd.Parameters.Add(new SqlParameter(arrParametros[i].ToString(), DBNull.Value));
                            }
                        }
                    }

                    conn.Open();
                    da.Fill(dt);
                }

                return dt;
            }
            catch (Exception e)
            {
                e.Data["storedProcedure"] = storedProcedure;
                e.Data["arrParametros"] = arrParametros != null ? string.Join(",", arrParametros) : null;
                e.Data["arrParametrosValores"] = arrParametrosValores != null ? string.Join(",", arrParametrosValores) : null;
                e.Data["enviarDbNullValue"] = enviarDbNullValue;

                throw;
            }
        }
    }
}
