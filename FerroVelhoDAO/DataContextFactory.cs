using System;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using FerroVelho;

namespace FerroVelhoDAO
{
    public class DataContextFactory
    {
        public string _usuarioId;
        public string _usuarioPermissao;
        public string _usuarioNome;
        public string _nome;
        public string _tel;
        public string _endereco;
        private string _conexaoUser;

        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(_conexaoUser);
            con.Open();
            return con;

        }

        public void FU_lerCabecario()
        {
            try
            {
                XElement XML = XElement.Load(@"..\..\cabeçalho.xml");

                _nome = XML.Element("Nome").Value;
                _tel = XML.Element("Telefone").Value;
                _endereco = XML.Element("Endereco").Value;

                XML = null;
            }
            catch
            {
                _nome = "";
                _tel = "";
                _endereco = "";
            }
        }

        public void FU_lerConfiguracao()
        {
            try
            {
                XElement XML = XElement.Load(@"..\..\configuração.xml");
                _conexaoUser = XML.Element("dataUser").Value;
                XML = null;
            }
            catch
            {
                _conexaoUser = "";
            }

        }

        public ValidacaoEnum ValidarLoginUsuario(string nomeUsuario, string senhaUsuario)
        {
            try
            {
                string[] arrPar = new string[] { "@nome_usuario", "@senha_usuario" };
                string[] arrVal = new string[] { nomeUsuario, senhaUsuario };
                DataTable dt = GetDataTableBySP("s_tb_usuario_login", arrPar, arrVal);
                                
                if (dt.Rows.Count > 0)
                {
                    _usuarioId = dt.Rows[0]["id_usuario"].ToString();
                    _usuarioNome = dt.Rows[0]["nome_usuario"].ToString();
                    _usuarioPermissao = dt.Rows[0]["permi_usuario"].ToString();
                    return ValidacaoEnum.SUCESSO;
                }
                else
                {
                    return ValidacaoEnum.USUARIO_SENHA_INVALIDO;
                }
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

                SqlConnection conn = GetConnection();

                SqlCommand cmd = new SqlCommand(storedProcedure, conn);
                cmd.CommandTimeout = 45000;
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

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

    }


}
