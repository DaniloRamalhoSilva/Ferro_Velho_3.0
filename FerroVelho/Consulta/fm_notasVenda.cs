using FerroVelhoDAO;
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

namespace FerroVelho
{
    public partial class fm_notasVenda : Form
    {
        private static readonly CultureInfo BrazilianCulture = CultureInfo.GetCultureInfo("pt-BR");

        public fm_notasVenda()
        {
            InitializeComponent();
            ConfigureGridFormatting();
        }

        private void fm_notasVenda_Load(object sender, EventArgs e)
        {
            dt_fim.Value = DateTime.Now;
            dt_inicio.Value = DateTime.Now;
            consulta();
        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            consulta();
        }

        private void consulta()
        {
            DateTime dataI = dt_inicio.Value;
            DateTime dataF = dt_fim.Value;

            DataTable dt = DataContextFactory.ListarVendasApi(dataI.Date, dataF.Date.Add(new TimeSpan(23, 59, 59)), null);

            tb_vendaDataGridView.DataSource = dt;
            tb_vendaDataGridView.DataMember = dt.TableName;
            if (tb_vendaDataGridView.Rows.Count >= 1)
            {
                tb_vendaDataGridView.CurrentCell = tb_vendaDataGridView.Rows[tb_vendaDataGridView.Rows.Count - 1].Cells[0];
            }
            AtualizarUsuarioSelecionado();
            carregaItem();
        }

        private void ConfigureGridFormatting()
        {
            dataGridViewTextBoxColumn2.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridViewTextBoxColumn2.DefaultCellStyle.FormatProvider = BrazilianCulture;
        }

        private void carregaItem()
        {
            int id;
            if (!TryGetVendaSelecionadaId(out id))
            {
                this.tb_itemvBindingSource.Clear();
                return;
            }

            this.tb_itemvBindingSource.DataSource = DataContextFactory.ListarItensVendaApi(id);
        }

        private bool TryGetVendaSelecionadaId(out int id)
        {
            return TryGetValorLinhaAtual(0, out id);
        }

        private bool TryGetValorLinhaAtual(int coluna, out int valor)
        {
            valor = 0;

            if (tb_vendaDataGridView.CurrentRow == null ||
                tb_vendaDataGridView.CurrentRow.IsNewRow ||
                tb_vendaDataGridView.CurrentRow.Cells.Count <= coluna)
            {
                return false;
            }

            object cellValue = tb_vendaDataGridView.CurrentRow.Cells[coluna].Value;
            if (cellValue == null || cellValue == DBNull.Value)
            {
                return false;
            }

            return int.TryParse(cellValue.ToString(), out valor);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = DataContextFactory.ListarVendasApi(null, null, Convert.ToInt32(txt_notaFiscal.Text));

                tb_vendaDataGridView.DataSource = dt;
                tb_vendaDataGridView.DataMember = dt.TableName;

                carregaItem();

                if (dt == null)
                {
                    MessageBox.Show("Nota fiscal: " + txt_notaFiscal.Text + " inexistente");
                    this.tb_itemvBindingSource.Clear();
                }
            }
            catch
            {
                DataTable dt = DataContextFactory.ListarVendasApi(null, null, null);

                tb_vendaDataGridView.DataSource = dt;
                tb_vendaDataGridView.DataMember = dt.TableName;
                if (tb_vendaDataGridView.Rows.Count >= 1)
                {
                    tb_vendaDataGridView.CurrentCell = tb_vendaDataGridView.Rows[tb_vendaDataGridView.Rows.Count - 1].Cells[0];
                }
                AtualizarUsuarioSelecionado();
                carregaItem();
            }

        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (!TryGetVendaSelecionadaId(out id))
                {
                    MessageBox.Show("Selecione uma nota fiscal!");
                    return;
                }

                DataContextFactory.ExcluirVendaApi(id);

                consulta();
                MessageBox.Show("Excluido com sucesso!");
            }
            catch 
            {
                MessageBox.Show("Selecione uma nota fiscal! Erro: " );
            }

        }

        private void tb_itemvDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1)
            {
                e.Value = ((tb_produtos)e.Value).desc_prod;
            }
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            int id;
            if (!TryGetVendaSelecionadaId(out id))
            {
                MessageBox.Show("Selecione uma nota fiscal!");
                return;
            }

            fm_vender fm = new fm_vender();
            fm.imprimirNF(id);
        }

        private void tb_vendaDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            AtualizarUsuarioSelecionado();
            carregaItem();
        }

        private void AtualizarUsuarioSelecionado()
        {
            int usuarioId;
            if (TryGetValorLinhaAtual(3, out usuarioId))
            {
                nome_usuarioLabel2.Text = DataContextFactory.BuscarNomeUsuarioApi(usuarioId);
            }
            else
            {
                nome_usuarioLabel2.Text = "Não informado";
            }
        }
        

    }
}

