using System;
using System.Collections.Generic;

namespace FerroVelho.Classes
{
    internal class NotaCompra
    {
        private static FerroVelhoDAO.DataContextFactory _DAO = new FerroVelhoDAO.DataContextFactory();

        public string Id { get; set; }
        public string DataCompra { get; set; }
        public string ValorNota { get; set; }
        public string UsuarioId { get; set; }
        public string DescontoCompra { get; set; }
        public string SubtotalCompra { get; set; }
        public string IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemCompra> ItensCompras { get; set; } = new List<ItemCompra>();


        public ValidacaoEnum SalvarCompra()
        {
            ValidacaoEnum validacaoEnum = ValidarSaldoCaixa();
            if (validacaoEnum == ValidacaoEnum.SUCESSO)
            {
                string[] arrPar = new string[] { "@valor_nota", "@id_usuario", "@desconto_compra", "@subtot_compra", "@id_cliente" };
                string[] arrVal = new string[] { ValorNota, _DAO._usuarioId, Comum.TrataMoedaSQL(DescontoCompra, true), Comum.TrataMoedaSQL(SubtotalCompra, true), IdCliente };
                Id = _DAO.GetDataTableBySP("i_tb_compra", arrPar, arrVal).Rows[0]["id_compra"].ToString();

                foreach (ItemCompra itenCompra in ItensCompras)
                {
                    itenCompra.IdCompra = Id;
                    string[] arrPar2 = new string[] { "@id_prod", "@id_compra", "quant_item", "@subTot_item", "@valor_item" };
                    string[] arrVal2 = new string[] { itenCompra.IdProduto, itenCompra.IdCompra, itenCompra.Quantidade, Comum.TrataMoedaSQL(itenCompra.SubTotal, true), Comum.TrataMoedaSQL(itenCompra.Valor, true) };
                    _DAO.GetDataTableBySP("i_tb_itemc", arrPar2, arrVal2);
                }

                return ValidacaoEnum.SUCESSO;
            }
            else
            {
                return validacaoEnum;
            }
        } 

        private ValidacaoEnum ValidarSaldoCaixa()
        {
            decimal saldoCaixa = Caixa.GetSaldo();

            if (saldoCaixa > Convert.ToDecimal(ValorNota))
                return ValidacaoEnum.SUCESSO;
            else
                return ValidacaoEnum.SALDO_INSIFICIENTE;    

        }


    }

    internal class ItemCompra
    {
        public static FerroVelhoDAO.DataContextFactory _DAO = new FerroVelhoDAO.DataContextFactory();

        public string Id { get; set; }
        public string IdProduto { get; set; }
        public string IdCompra { get; set; }
        public string Quantidade { get; set; }
        public string SubTotal { get; set; }
        public string Valor { get; set; }

    }
}
