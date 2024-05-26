using System;
using System.Data;
using System.Linq;

namespace FerroVelho.Classes
{
    internal class Produto
    {
        private static FerroVelhoDAO.DataContextFactory _DAO = new FerroVelhoDAO.DataContextFactory();

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Valor {  get; set; }
        public string IdUsuario { get; set; }

        public DataTable DadaTableProdutos 
        { 
            get => _DAO.GetDataTableBySP("s_tb_produtos");
        }


        public void GetProdutoId(string id)
        {
            DataRow produto = DadaTableProdutos.AsEnumerable().FirstOrDefault(row => row["ProductID"] == id);
            Id = produto["id_prod"].ToString();
            Name = produto["desc_prod"].ToString();
            Valor = Convert.ToDecimal(produto["val_prod"]);
            IdUsuario = produto["usuario"].ToString();
        }

    }
}
