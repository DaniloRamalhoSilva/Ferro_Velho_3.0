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
    public partial class fm_outrasSaidas : Form
    {
        public fm_outrasSaidas()
        {
            InitializeComponent();
        }

        private void fm_outrasSaidas_Load(object sender, EventArgs e)
        {
            this.tb_produtosBindingSource.DataSource = DataContextFactory.ListarProdutosApi();

            cb_desProd.DataSource = tb_produtosBindingSource;
            cb_desProd.DisplayMember = "desc_prod";
            cb_desProd.ValueMember = "cod_prod";

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
                cb_desProd.SelectedValue = txt_codProd.Text.Trim();
            }
            
        }

        private void txt_codProd_Leave(object sender, EventArgs e)
        {
            if (txt_codProd.Text != "")
            {
                cb_desProd.SelectedValue = txt_codProd.Text.Trim();
                decimal saldo = DataContextFactory.CalcularSaldoProdutoApi(txt_codProd.Text.Trim());
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
            if (this.produtoCorrente == null)
            {
                return;
            }

            txt_codProd.Text = this.produtoCorrente.cod_prod;
            decimal saldo = DataContextFactory.CalcularSaldoProdutoApi(txt_codProd.Text.Trim());
            lb_saldo.Text = saldo.ToString("N3");
            
        }

        public tb_produtos produtoCorrente
        {
            get
            {
                return (tb_produtos)this.tb_produtosBindingSource.Current;
            }
        }

        public tb_venda vendaCorrente
        {
            get
            {
                return (tb_venda)this.tb_saidaBindingSource.Current;
            }
        }

        public tb_itemv itemCorrente
        {
            get
            {
                return (tb_itemv)this.tb_itemsBindingSource.Current;
            }
        }

        private void txt_codProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_quant.Focus();
                e.Handled = true;
            }
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
            var venda = DataContextFactory.CriarVendaApi(DateTime.Now, DataContextFactory.usu.id_usuario, 0m);
            this.tb_saidaBindingSource.DataSource = venda;
        }

        private void novoItem()
        {
            DataContextFactory.InserirItemVendaApi(
                this.produtoCorrente.cod_prod,
                this.vendaCorrente.id_venda,
                Convert.ToDecimal(txt_quant.Text),
                0m,
                0m);
        }

        private void txt_quant_Leave(object sender, EventArgs e)
        {
            decimal quant;
            if (txt_quant.Text == "")
            {quant = 0;}
            else
            {quant = Convert.ToDecimal(txt_quant.Text);}
            txt_quant.Text = quant.ToString("N3");
        }


        public void receberSaida(string id, decimal saida)
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
                MessageBox.Show("Saida adicionado com sucesso!");                
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

