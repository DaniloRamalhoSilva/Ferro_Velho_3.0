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
        private bool novoProduto;

        public fm_cadastroProduto()
        {
            InitializeComponent();
        }

        private void CarregarProdutos()
        {
            idprodDataGridViewTextBoxColumn.DataPropertyName = "cod_prod";
            this.tbprodutosBindingSource.DataSource = DataContextFactory.ListarProdutosApi();
        }

        private void fm_cadastroProduto_Load(object sender, EventArgs e)
        {
            CarregarProdutos();
            clik();
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            novoProduto = true;
            txt_codPro.Enabled = true;
            txt_descrição.Enabled = true;
            txt_valor.Enabled = true;
            txt_descrição.Text = "";
            txt_codPro.Text = "";
            txt_valor.Text = "";

            txt_codPro.Focus();

            btn_cancelar.Visible = true;
            btn_salvar.Visible = true;
            btn_alterar.Visible = false;
            btn_excluir.Visible = false;
            btn_novo.Visible = false;

            dataGridView1.Enabled = false;   
                                   
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            novoProduto = false;
            txt_codPro.Enabled = false;
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
                novoProduto = false;
                txt_codPro.Enabled = true;
                txt_descrição.Enabled = true;
                txt_valor.Enabled = true;
                txt_codPro.Focus();

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
            else if (string.IsNullOrWhiteSpace(txt_codPro.Text))
            {
                MessageBox.Show("Código do produto é obrigatório!");
                txt_codPro.Focus();
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

                try
                {
                    if (novoProduto)
                    {
                        int? usuarioLogado = DataContextFactory.usu != null ? (int?)DataContextFactory.usu.id_usuario : null;
                        DataContextFactory.CriarProdutoApi(txt_codPro.Text.Trim(), txt_descrição.Text.Trim(), valorProduto, usuarioLogado);
                        MessageBox.Show("Salvo com sucesso!");
                    }
                    else
                    {
                        if (this.produtoCorrente == null)
                        {
                            MessageBox.Show("Selecione um produto valido!");
                            return;
                        }

                        DataContextFactory.AtualizarProdutoApi(this.produtoCorrente.id_prod, txt_codPro.Text.Trim(), txt_descrição.Text.Trim(), valorProduto);
                        MessageBox.Show("Alterado com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível salvar o produto. " + ex.Message);
                    return;
                }

                CarregarProdutos();

                novoProduto = false;
                txt_codPro.Enabled = false;
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
                    if (this.produtoCorrente == null)
                    {
                        MessageBox.Show("Selecione um produto valido!");
                        return;
                    }

                    DataContextFactory.ExcluirProdutoApi(this.produtoCorrente.id_prod);

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
            txt_codPro.Text = atual.cod_prod ?? string.Empty;
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
            if (e.KeyChar == 13)
            {
                txt_descrição.Focus();
                e.Handled = true;
            }
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

