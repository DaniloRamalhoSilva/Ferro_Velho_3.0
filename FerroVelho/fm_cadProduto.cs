using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FerroVelhoDAO;

namespace FerroVelho
{
    public partial class fm_cadastroProduto : Form
    {
        public fm_cadastroProduto()
        {
            InitializeComponent();
        }

        private void fm_cadastroProduto_Load(object sender, EventArgs e)
        {
            
            this.tbprodutosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;
            clik();
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            
            txt_descrição.Enabled = true;
            txt_valor.Enabled = true;
            txt_descrição.Text = "";
            txt_codPro.Text = "";
            txt_valor.Text = "";

            txt_descrição.Focus();

            btn_cancelar.Visible = true;
            btn_salvar.Visible = true;
            btn_alterar.Visible = false;
            btn_excluir.Visible = false;
            btn_novo.Visible = false;

            dataGridView1.Enabled = false;   
                                   
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            txt_descrição.Enabled = false;
            txt_valor.Enabled = false;
            
            btn_salvar.Visible = false;
            btn_alterar.Visible = true;
            btn_excluir.Visible = true;
            btn_novo.Visible = true;
                        
            dataGridView1.Enabled = true;
            clik();
        }

        private void btn_alterar_Click(object sender, EventArgs e)
        {
            if (txt_codPro.Text == "")
            {
                MessageBox.Show("Selecione um produto valido!");
            }
            else
            {
                txt_descrição.Enabled = true;
                txt_valor.Enabled = true;
                txt_descrição.Focus();

                btn_cancelar.Visible = true;
                btn_salvar.Visible = true;
                btn_alterar.Visible = false;
                btn_excluir.Visible = false;
                btn_novo.Visible = false;

                dataGridView1.Enabled = false;
            }            

        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (txt_descrição.Text == "" && txt_valor.Text == "")
            {
                MessageBox.Show("Descrição e Valor são obrigatorios!");
            }
            else
            {
                if (txt_codPro.Text == "")
                {

                    this.tbprodutosBindingSource.AddNew();

                    this.produtoCorrente.desc_prod = txt_descrição.Text;
                    this.produtoCorrente.val_prod = Convert.ToDecimal(txt_valor.Text);
                    this.produtoCorrente.usuario = DataContextFactory.usu.id_usuario;
                    this.tbprodutosBindingSource.EndEdit();
                    DataContextFactory.DataContext.SubmitChanges();
                    MessageBox.Show("Salvo com sucesso!");
                }
                else
                {
                    this.produtoCorrente.desc_prod = txt_descrição.Text;
                    this.produtoCorrente.val_prod = Convert.ToDecimal(txt_valor.Text);

                    this.tbprodutosBindingSource.EndEdit();
                    DataContextFactory.DataContext.SubmitChanges();
                    MessageBox.Show("Alterado com sucesso!");
                }

                txt_descrição.Enabled = false;
                txt_valor.Enabled = false;

                btn_salvar.Visible = false;
                btn_cancelar.Visible = false;
                btn_alterar.Visible = true;
                btn_excluir.Visible = true;
                btn_novo.Visible = true;
                dataGridView1.Enabled = true;
                clik();
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show ("Realmente deseja excuir", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.tbprodutosBindingSource.RemoveCurrent();
                    DataContextFactory.DataContext.SubmitChanges();
                    clik();
                    MessageBox.Show("Produto excluido com sucesso!");
                }
                catch
                {
                    MessageBox.Show("Impesivel excluir, o item esta atribuido a uma nota de venda/compra! Erro:" + e);
                }
                
            }
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            clik();
        }

        private void clik()
        {
            try
            {
                txt_descrição.Text = this.produtoCorrente.desc_prod;
                txt_valor.Text = Convert.ToString(this.produtoCorrente.val_prod);
                txt_codPro.Text = Convert.ToString(this.produtoCorrente.id_prod);
            }
            catch
            {
                txt_valor.Text = "";
                txt_codPro.Text = "";
            }
            
        }

        public tb_produtos produtoCorrente
        {
            get
            {
                return (tb_produtos)this.tbprodutosBindingSource.Current;
            }
        }

        private void txt_codPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back))
            {
                return;
            }
            e.Handled = true;
        }

        private void txt_valor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        
    }
}
