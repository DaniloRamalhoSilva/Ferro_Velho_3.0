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
    public partial class fm_notasCompra : Form
    {
        private static readonly CultureInfo BrazilianCulture = CultureInfo.GetCultureInfo("pt-BR");

        public fm_notasCompra()
        {
            InitializeComponent();
            ConfigureGridFormatting();
        }

        private void fm_notasCompra_Load(object sender, EventArgs e)
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

            DataTable dt = DataContextFactory.ListarComprasApi(dataI.Date, dataF.Date.Add(new TimeSpan(23, 59, 59)), null);

            tb_vendaDataGridView.DataSource = dt;
            tb_vendaDataGridView.DataMember = dt.TableName;
            if (tb_vendaDataGridView.Rows.Count >= 1)
            {
                tb_vendaDataGridView.CurrentCell = tb_vendaDataGridView.Rows[tb_vendaDataGridView.Rows.Count - 1].Cells[0];
            }
            carregaItem();
        }

        private void ConfigureGridFormatting()
        {
            datacompraDataGridViewTextBoxColumn.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            datacompraDataGridViewTextBoxColumn.DefaultCellStyle.FormatProvider = BrazilianCulture;
        }
         
        private void carregaItem()
        {
            int id;
            if (!TryGetNotaSelecionadaId(out id))
            {
                this.tb_itemcBindingSource.Clear();
                return;
            }

            this.tb_itemcBindingSource.DataSource = DataContextFactory.ListarItensCompraApi(id);
        }

        private bool TryGetNotaSelecionadaId(out int id)
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
                DataTable dt = DataContextFactory.ListarComprasApi(null, null, Convert.ToInt32(txt_notaFiscal.Text));

                tb_vendaDataGridView.DataSource = dt;
                tb_vendaDataGridView.DataMember = dt.TableName;

                carregaItem();

                if (dt == null)
                {
                    MessageBox.Show("Nota fiscal: " + txt_notaFiscal.Text + " inexistente");
                    this.tb_itemcBindingSource.Clear();
                }
            }
            catch
            {
                DataTable dt = DataContextFactory.ListarComprasApi(null, null, null);

                tb_vendaDataGridView.DataSource = dt;
                tb_vendaDataGridView.DataMember = dt.TableName;
                if (tb_vendaDataGridView.Rows.Count >= 1)
                {
                    tb_vendaDataGridView.CurrentCell = tb_vendaDataGridView.Rows[tb_vendaDataGridView.Rows.Count - 1].Cells[0];
                }
                carregaItem();
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (!TryGetNotaSelecionadaId(out id))
                {
                    MessageBox.Show("Selecione uma nota fiscal!");
                    return;
                }

                DataContextFactory.ExcluirCompraApi(id);

                consulta();
                MessageBox.Show("Excluido com sucesso!");
            }
            catch
            {
                MessageBox.Show("Selecione uma nota fiscal! Erro:" + e );
            }
            
        }
                
        private void tb_itemcDataGridView_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1)
            {
                e.Value = ((tb_produtos)e.Value).desc_prod;
            }

            if (e.Value != null && e.ColumnIndex == 5)
            {
                e.Value = ((tb_usuario)e.Value).nome_usuario;
            }
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            int id;
            if (!TryGetNotaSelecionadaId(out id))
            {
                MessageBox.Show("Selecione uma nota fiscal!");
                return;
            }

            fm_menulPrincipal fm = new fm_menulPrincipal();
            fm.imprimirNF(id);
        }

        private void tb_vendaDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            int clienteId;
            if (TryGetValorLinhaAtual(6, out clienteId))
            {
                lb_cliente.Text = clienteDAO(clienteId);
            }
            else
            {
                lb_cliente.Text = "Não informado";
            }

            int usuarioId;
            if (TryGetValorLinhaAtual(5, out usuarioId))
            {
                lb_usuario.Text = usuarioDAO(usuarioId);
            }
            else
            {
                lb_usuario.Text = "Erro";
            }

            carregaItem();
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  DAO <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        private string usuarioDAO(int id)
        {
            return DataContextFactory.BuscarNomeUsuarioApi(id);
        }

        private string clienteDAO(int id)
        {
            return DataContextFactory.BuscarNomeClienteApi(id);
        }

    }
}

