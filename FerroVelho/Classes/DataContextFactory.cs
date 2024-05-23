using System.Data.SqlClient;
using System.Xml.Linq;

namespace FerroVelho.Classes
{
    internal class DataContextFactory
    {
        private static string _nome;
        private static string _tel;
        private static string _endereco;
        private static string _conexaoUser;

        public static SqlConnection Conectar()
        {
            SqlConnection con = new SqlConnection(_conexaoUser);
            con.Open();
            return con;

        }

        public static void FU_lerCabecario()
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

        public static void FU_lerConfiguracao()
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

    }
}
