using FerroVelhoDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho
{
    public partial class fm_outrasEntradas : Form
    {
        public fm_outrasEntradas()
        {
            InitializeComponent();
        }

        private void fm_outrasEntradas_Load(object sender, EventArgs e)
        {
            this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;
            cb_desProd.DataSource = tb_produtosBindingSource;
            cb_desProd.DisplayMember = "desc_prod";
            cb_desProd.ValueMember = "id_prod";

            if (txt_codProd.Enabled != false)
            {                
                txt_quant.Enabled = true;
                txt_codProd.Enabled = true;
                cb_desProd.Enabled = true;             
                cb_desProd.SelectedIndex = -1;
                txt_codProd.Focus();
            }
            else
            {
                cb_desProd.SelectedValue = Convert.ToInt32(txt_codProd.Text);
            }            
            
        }

        private void txt_codProd_Leave(object sender, EventArgs e)
        {
            if (txt_codProd.Text != "")
            {
                cb_desProd.SelectedValue = Convert.ToInt32(txt_codProd.Text);
                fm_estoque fm = new fm_estoque();
                decimal saldo = fm.calcular(Convert.ToInt32(txt_codProd.Text));
                lb_saldo.Text = saldo.ToString("N3");
            }
            else
            {
                cb_desProd.SelectedIndex = -1;
                lb_saldo.Text = "0,00";
            }
        }

        private void cb_desProd_Leave(object sender, EventArgs e)
        {
            txt_codProd.Text = Convert.ToString(this.produtoCorrente.id_prod);
            fm_estoque fm = new fm_estoque();
            decimal saldo = fm.calcular(Convert.ToInt32(txt_codProd.Text));
            lb_saldo.Text = saldo.ToString("N3");

        }

        public tb_produtos produtoCorrente
        {
            get
            {
                return (tb_produtos)this.tb_produtosBindingSource.Current;
            }
        }

        public tb_compra entradaCorrente
        {
            get
            {
                return (tb_compra)this.tb_entradaBindingSource.Current;
            }
        }

        public tb_itemc itemCorrente
        {
            get
            {
                return (tb_itemc)this.tb_itemeBindingSource.Current;
            }
        }

        private void txt_codProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                txt_quant.Focus();
            }
            e.Handled = true;
        }

        private void txt_quant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                bt_finalCompra.Focus();
            }
            e.Handled = true;
        }              

        private void novaNota()
        {
            this.tb_entradaBindingSource.DataSource = DataContextFactory.DataContext.tb_compra;
            this.tb_entradaBindingSource.AddNew();
            this.entradaCorrente.valor_nota = Convert.ToDecimal("0,00");
            this.entradaCorrente.data_compra = DateTime.Now;
            this.entradaCorrente.usuario = DataContextFactory.usu.id_usuario;
            this.tb_entradaBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();

        }

        private void novoItem()
        {
            this.tb_itemeBindingSource.DataSource = DataContextFactory.DataContext.tb_itemc;

            this.tb_itemeBindingSource.AddNew();

            this.itemCorrente.id_prod = this.produtoCorrente.id_prod;
            this.itemCorrente.id_compra = this.entradaCorrente.id_compra;
            this.itemCorrente.quant_item = Convert.ToDecimal(txt_quant.Text);
            this.itemCorrente.subTot_item = Convert.ToDecimal("0,00");
            this.itemCorrente.valor_item = Convert.ToDecimal("0,00");

            this.tb_itemeBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();
        }

        private void txt_quant_Leave(object sender, EventArgs e)
        {
            decimal quant;
            if (txt_quant.Text == "")
            { quant = 0; }
            else
            { quant = Convert.ToDecimal(txt_quant.Text); }
            txt_quant.Text = quant.ToString("N3");
        }


        public void receberEntrada(string id, decimal saida)
        {
            txt_codProd.Text = id;            
            txt_quant.Text = saida.ToString();
            txt_quant.Enabled = false;
            txt_codProd.Enabled = false;
            cb_desProd.Enabled = false;

        }

        private void bt_finalCompra_Click_1(object sender, EventArgs e)
        {
            if (txt_quant.Text != "" && cb_desProd.Text != "" && txt_codProd.Text != "")
            {
                novaNota();
                novoItem();
                txt_codProd.Text = "";
                txt_quant.Text = (0).ToString("N3");
                cb_desProd.SelectedIndex = -1;
                MessageBox.Show("Entrada adicionado com sucesso!");                
                this.Close();
            }
            else
            {
                MessageBox.Show("Todos os campos são obrigatorios!");
                txt_quant.Focus();
            }
        }
    }
}
