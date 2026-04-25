using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        private bool IsPostgresMode
        {
            get { return DataContextFactory.IsPostgresConnectionString(DataContextFactory.conexaoUser); }
        }

        private void CarregarProdutos()
        {
            if (IsPostgresMode)
            {
                this.tbprodutosBindingSource.DataSource = DataContextFactory.ListarProdutosPostgres();
            }
            else
            {
                this.tbprodutosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;
            }
        }

        private void fm_cadastroProduto_Load(object sender, EventArgs e)
        {
            CarregarProdutos();
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
            if (string.IsNullOrWhiteSpace(txt_descrição.Text) || string.IsNullOrWhiteSpace(txt_valor.Text))
            {
                MessageBox.Show("Descrição e Valor são obrigatorios!");
            }
            else
            {
                decimal valorProduto;
                if (!decimal.TryParse(txt_valor.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out valorProduto))
                {
                    MessageBox.Show("Valor do produto inválido!");
                    txt_valor.Focus();
                    return;
                }

                if (IsPostgresMode)
                {
                    if (txt_codPro.Text == "")
                    {
                        int? usuarioLogado = DataContextFactory.usu != null ? (int?)DataContextFactory.usu.id_usuario : null;
                        DataContextFactory.CriarProdutoPostgres(txt_descrição.Text.Trim(), valorProduto, usuarioLogado);
                        MessageBox.Show("Salvo com sucesso!");
                    }
                    else
                    {
                        DataContextFactory.AtualizarProdutoPostgres(Convert.ToInt32(txt_codPro.Text), txt_descrição.Text.Trim(), valorProduto);
                        MessageBox.Show("Alterado com sucesso!");
                    }

                    CarregarProdutos();
                }
                else
                {
                    if (txt_codPro.Text == "")
                    {
                        this.tbprodutosBindingSource.AddNew();
                        if (this.produtoCorrente == null)
                        {
                            MessageBox.Show("Não foi possível iniciar um novo produto.");
                            return;
                        }

                        this.produtoCorrente.desc_prod = txt_descrição.Text;
                        this.produtoCorrente.val_prod = valorProduto;
                        this.produtoCorrente.usuario = DataContextFactory.usu.id_usuario;
                        this.tbprodutosBindingSource.EndEdit();
                        DataContextFactory.DataContext.SubmitChanges();
                        MessageBox.Show("Salvo com sucesso!");
                    }
                    else
                    {
                        if (this.produtoCorrente == null)
                        {
                            MessageBox.Show("Selecione um produto valido!");
                            return;
                        }

                        this.produtoCorrente.desc_prod = txt_descrição.Text;
                        this.produtoCorrente.val_prod = valorProduto;

                        this.tbprodutosBindingSource.EndEdit();
                        DataContextFactory.DataContext.SubmitChanges();
                        MessageBox.Show("Alterado com sucesso!");
                    }
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
                if (string.IsNullOrWhiteSpace(txt_codPro.Text))
                {
                    MessageBox.Show("Selecione um produto valido!");
                    return;
                }

                try
                {
                    if (IsPostgresMode)
                    {
                        DataContextFactory.ExcluirProdutoPostgres(Convert.ToInt32(txt_codPro.Text));
                    }
                    else
                    {
                        this.tbprodutosBindingSource.RemoveCurrent();
                        DataContextFactory.DataContext.SubmitChanges();
                    }

                    CarregarProdutos();
                    clik();
                    MessageBox.Show("Produto excluido com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impesivel excluir, o item esta atribuido a uma nota de venda/compra! Erro: " + ex.Message);
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            clik();
        }

        private void clik()
        {
            var atual = this.produtoCorrente;
            if (atual == null)
            {
                txt_descrição.Text = string.Empty;
                txt_valor.Text = string.Empty;
                txt_codPro.Text = string.Empty;
                return;
            }

            txt_descrição.Text = atual.desc_prod;
            txt_valor.Text = Convert.ToString(atual.val_prod);
            txt_codPro.Text = Convert.ToString(atual.id_prod);
        }

        public tb_produtos produtoCorrente
        {
            get
            {
                return this.tbprodutosBindingSource.Current as tb_produtos;
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
