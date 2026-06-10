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
    public partial class fm_estoque : Form
    {
        public fm_estoque()
        {
            InitializeComponent();
        }

        private void fm_estoque_Load(object sender, EventArgs e)
        {
            load();
        }
        private void load()
        {
            this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;
            caregarEstoque();
            bt_corrigir.Enabled = true;
            lb_texto.Visible = false;
            txt_novoSaldo.Visible = false;
            bt_confirmar.Visible = false;
            txt_desc.Enabled = true;
            txt_codProd.Enabled = true;
            dg_produtos.Enabled = true;
            bt_cancelar.Visible = false;
            txt_novoSaldo.Text = "";
        }

        
        private void txt_codProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back))
            {
                return;
            }
            e.Handled = true;
        }

        private void txt_novoSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }



        public void caregarEstoque()
        {            
            
            foreach (DataGridViewRow dg in dg_produtos.Rows)
            {
                int idProduto = Convert.ToInt32(dg.Cells[0].Value);
                
                decimal saldo = calcular(idProduto);

                dg.Cells[2].Value = saldo;
            }
        }

        public decimal calcular(int id )
        {
            List<tb_itemc> itensC = new List<tb_itemc>();
            itensC = DataContextFactory.DataContext.tb_itemc.Where(x => x.id_prod == id).ToList();
            decimal totalC = 0;
            foreach (tb_itemc item in itensC)
            {
                totalC = totalC + item.quant_item;
            }

            List<tb_itemv> itensV = new List<tb_itemv>();
            itensV = DataContextFactory.DataContext.tb_itemv.Where(x => x.id_prod == id).ToList();
            decimal totalV = 0;
            foreach (tb_itemv item in itensV)
            {
                totalV = totalV + Convert.ToDecimal(item.quant_item);
            }
            decimal saldo = totalC - totalV;
            return saldo;
        }

        private void bt_corrigir_Click(object sender, EventArgs e)
        {
            bt_corrigir.Enabled = false;
            lb_texto.Visible = true;
            bt_cancelar.Visible = true;
            txt_novoSaldo.Visible = true;
            bt_confirmar.Visible = true;
            dg_produtos.Enabled = false;
            txt_codProd.Enabled = false;
            txt_desc.Enabled = false;

        }
        
        private void bt_confirmar_Click(object sender, EventArgs e)
        {
            string id = this.produtoCorrente.id_prod.ToString();
            string des = this.produtoCorrente.desc_prod;
            decimal saldo = Convert.ToDecimal(dg_produtos[2, dg_produtos.CurrentRow.Index].Value);
            decimal novosaldo = Convert.ToDecimal(txt_novoSaldo.Text);
                        

            if (saldo > novosaldo)
            {
                decimal saida = saldo - novosaldo ;

                if (MessageBox.Show("Para alterar: " + des + " de: " + saldo + " Kg, para: " + novosaldo + " Kg, é necessario saida de: " + saida + " Kg, deseja continuar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {                    
                    fm_outrasSaidas fm = new fm_outrasSaidas();                    
                    fm.receberSaida(id, saida);
                    fm.ShowDialog();
                }                
            }

            if (saldo < novosaldo)
            {
                decimal saida = novosaldo - saldo;

                if (MessageBox.Show("Para alterar: " + des + " de: " + saldo + " para: " + novosaldo + " é necessario entrada de: " + saida + " Kg, deseja continuar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    fm_outrasEntradas fm = new fm_outrasEntradas();                    
                    fm.receberEntrada(id, saida);
                    fm.ShowDialog();
                }                
            }

            load();            
        }

        public tb_produtos produtoCorrente
        {
            get
            {
                return (tb_produtos)this.tb_produtosBindingSource.Current;
            }
        }

        private void txt_novoSaldo_Leave(object sender, EventArgs e)
        {
            if (txt_novoSaldo.Text != "")
            {
                txt_novoSaldo.Text = Convert.ToDecimal(txt_novoSaldo.Text).ToString("N3");
            }
            else
            {
                txt_novoSaldo.Text = (0).ToString("N3");
            }
        }                

        private void txt_desc_KeyUp(object sender, KeyEventArgs e)
        {
            this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos.Where(x => x.desc_prod.StartsWith(txt_desc.Text));
            txt_codProd.Text = "";
            caregarEstoque();
        }

        private void txt_codProd_KeyUp(object sender, KeyEventArgs e)
            {
            if (txt_codProd.Text == "")
            {
                this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;
                txt_desc.Text = "";
                caregarEstoque();
            }
            else
            {
                this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos.Where(x => x.id_prod == (Convert.ToInt32(txt_codProd.Text)));
                txt_desc.Text = "";
                caregarEstoque();
            }
            
        }

        private void bt_cancelar_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}
