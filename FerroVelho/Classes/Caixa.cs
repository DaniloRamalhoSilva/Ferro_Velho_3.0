using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerroVelho.Classes
{
    internal class Caixa
    {
        private static FerroVelhoDAO.DataContextFactory _DAO = new FerroVelhoDAO.DataContextFactory();

        public decimal? Saldo;
        public decimal? Entrada;
        public decimal? Saida;
        public decimal? Descricaoo;

        public static decimal GetSaldo()
        {
            string saldoString = _DAO.GetDataTableBySP("i_tb_compra").Rows[0]["s_tb_caixa_saldo"].ToString();

            return Comum.ConverteStringDecima(saldoString);
        }

    }
}
