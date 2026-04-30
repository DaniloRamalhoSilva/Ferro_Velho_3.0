using System;

namespace FerroVelhoDAO
{
    public class tb_impressora
    {
        public int id_impressora { get; set; }
        public string nome_impressora { get; set; }
    }

    public class tb_produtos
    {
        public int id_prod { get; set; }
        public int empresa_cod { get; set; }
        public string cod_prod { get; set; }
        public string desc_prod { get; set; }
        public decimal? val_prod { get; set; }
        public int? usuario { get; set; }
    }

    public class tb_tipoUsuario
    {
        public int id_tipoUsuario { get; set; }
        public string desc_tipoUsuario { get; set; }
    }

    public class tb_usuario
    {
        public int id_usuario { get; set; }
        public int empresa_cod { get; set; }
        public string nome_usuario { get; set; }
        public string senha_usuario { get; set; }
        public int permi_usuario { get; set; }
        public bool ativo { get; set; }
        public tb_tipoUsuario tb_tipoUsuario { get; set; }
    }

    public class tb_cliente
    {
        public int id_cliente { get; set; }
        public string cpf_cliente { get; set; }
        public string nome_cliente { get; set; }
        public string tel_cliente { get; set; }
        public decimal Saldo { get; set; }
    }

    public class tb_compra
    {
        public int id_compra { get; set; }
        public DateTime data_compra { get; set; }
        public decimal desconto_compra { get; set; }
        public decimal subtot_compra { get; set; }
        public decimal valor_nota { get; set; }
        public int? id_cliente { get; set; }
        public int usuario { get; set; }
    }

    public class tb_venda
    {
        public int id_venda { get; set; }
        public DateTime data_venda { get; set; }
        public decimal valor_nota { get; set; }
        public int? usuario { get; set; }
    }

    public class tb_itemc
    {
        public int id_item { get; set; }
        public int id_prod { get; set; }
        public string cod_prod { get; set; }
        public int id_compra { get; set; }
        public decimal quant_item { get; set; }
        public decimal subTot_item { get; set; }
        public decimal valor_item { get; set; }
        public tb_produtos tb_produtos { get; set; }
        public tb_compra tb_compra { get; set; }
    }

    public class tb_itemv
    {
        public int id_item { get; set; }
        public int? id_prod { get; set; }
        public string cod_prod { get; set; }
        public int? id_venda { get; set; }
        public decimal? quant_item { get; set; }
        public decimal? subTot_item { get; set; }
        public decimal? valr_item { get; set; }
        public tb_produtos tb_produtos { get; set; }
        public tb_venda tb_venda { get; set; }
    }

    public class tb_caixa
    {
        public int Id_caixa { get; set; }
        public DateTime data_caixa { get; set; }
        public decimal valor_caixa { get; set; }
        public int? usuario { get; set; }
        public string desc_caixa { get; set; }
        public int? id_cliente { get; set; }
        public tb_cliente tb_cliente { get; set; }
        public tb_usuario tb_usuario { get; set; }
    }

    public class tb_aCliente
    {
        public int id_aCliente { get; set; }
        public DateTime data__aCliente { get; set; }
        public decimal valor_aCliente { get; set; }
        public int usuario { get; set; }
        public string desc_aCliente { get; set; }
        public int id_cliente { get; set; }
        public tb_cliente tb_cliente { get; set; }
        public tb_usuario tb_usuario { get; set; }
    }
}
