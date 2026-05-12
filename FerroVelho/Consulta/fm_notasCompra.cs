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
        private Panel painelAlterarDataCompra;
        private DateTimePicker dt_alterar_data_compra;
        private Button btn_confirmar_alterar_data_compra;
        private Button btn_cancelar_alterar_data_compra;

        public fm_notasCompra()
        {
            InitializeComponent();
            ConfigureGridFormatting();
            ConfigureAlterarDataCompraPanel();
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

        private void ConfigureAlterarDataCompraPanel()
        {
            painelAlterarDataCompra = new Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = SystemColors.ControlLightLight,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(btn_alterar_data_compra.Left, btn_alterar_data_compra.Bottom + 4),
                Size = new Size(btn_alterar_data_compra.Width, 105),
                Visible = false
            };

            Label lbNovaData = new Label
            {
                AutoSize = true,
                Location = new Point(10, 10),
                Text = "Nova data:"
            };

            dt_alterar_data_compra = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(10, 29),
                MaxDate = DateTime.Today,
                Size = new Size(120, 20)
            };

            btn_confirmar_alterar_data_compra = new Button
            {
                Location = new Point(10, 63),
                Size = new Size(105, 28),
                Text = "Confirmar",
                UseVisualStyleBackColor = true
            };
            btn_confirmar_alterar_data_compra.Click += btn_confirmar_alterar_data_compra_Click;

            btn_cancelar_alterar_data_compra = new Button
            {
                Location = new Point(125, 63),
                Size = new Size(105, 28),
                Text = "Cancelar",
                UseVisualStyleBackColor = true
            };
            btn_cancelar_alterar_data_compra.Click += btn_cancelar_alterar_data_compra_Click;

            painelAlterarDataCompra.Controls.Add(lbNovaData);
            painelAlterarDataCompra.Controls.Add(dt_alterar_data_compra);
            painelAlterarDataCompra.Controls.Add(btn_confirmar_alterar_data_compra);
            painelAlterarDataCompra.Controls.Add(btn_cancelar_alterar_data_compra);

            Controls.Add(painelAlterarDataCompra);
            painelAlterarDataCompra.BringToFront();
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
            return TryGetValorLinha(tb_vendaDataGridView.CurrentRow, coluna, out valor);
        }

        private bool TryGetValorLinha(DataGridViewRow row, int coluna, out int valor)
        {
            valor = 0;

            if (row == null ||
                row.IsNewRow ||
                row.Cells.Count <= coluna)
            {
                return false;
            }

            object cellValue = row.Cells[coluna].Value;
            if (cellValue == null || cellValue == DBNull.Value)
            {
                return false;
            }

            return int.TryParse(cellValue.ToString(), out valor);
        }

        private bool TryGetDataCompraLinha(DataGridViewRow row, out DateTime dataCompra)
        {
            dataCompra = DateTime.MinValue;

            if (row == null ||
                row.IsNewRow ||
                row.Cells.Count <= datacompraDataGridViewTextBoxColumn.Index)
            {
                return false;
            }

            object cellValue = row.Cells[datacompraDataGridViewTextBoxColumn.Index].Value;
            if (cellValue == null || cellValue == DBNull.Value)
            {
                return false;
            }

            if (cellValue is DateTime)
            {
                dataCompra = ((DateTime)cellValue);
                return true;
            }

            string texto = Convert.ToString(cellValue, CultureInfo.CurrentCulture);
            return DateTime.TryParse(texto, BrazilianCulture, DateTimeStyles.AssumeLocal, out dataCompra) ||
                DateTime.TryParse(texto, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dataCompra) ||
                DateTime.TryParse(texto, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dataCompra);
        }

        private bool TryGetComprasGridParaAlteracao(out List<int> idsCompra, out DateTime dataCompraAtual)
        {
            idsCompra = new List<int>();
            dataCompraAtual = DateTime.MinValue;
            bool possuiDataReferencia = false;
            DateTime dataReferencia = DateTime.MinValue;

            foreach (DataGridViewRow row in tb_vendaDataGridView.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                int idCompra;
                if (!TryGetValorLinha(row, idcompraDataGridViewTextBoxColumn.Index, out idCompra))
                {
                    MessageBox.Show("A grid possui uma compra sem número de nota válido.");
                    return false;
                }

                DateTime dataCompra;
                if (!TryGetDataCompraLinha(row, out dataCompra))
                {
                    MessageBox.Show("A grid possui uma compra sem data válida.");
                    return false;
                }

                if (!possuiDataReferencia)
                {
                    dataReferencia = dataCompra.Date;
                    possuiDataReferencia = true;
                }
                else if (dataReferencia != dataCompra.Date)
                {
                    MessageBox.Show("Não é possível alterar compras de datas diferentes.");
                    return false;
                }

                idsCompra.Add(idCompra);
            }

            if (idsCompra.Count == 0)
            {
                MessageBox.Show("Não há compras na grid para alterar.");
                return false;
            }

            dataCompraAtual = dataReferencia;
            return true;
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

        private void btn_alterar_data_compra_Click(object sender, EventArgs e)
        {
            if (painelAlterarDataCompra.Visible)
            {
                painelAlterarDataCompra.Visible = false;
                return;
            }

            List<int> idsCompra;
            DateTime dataCompraAtual;
            if (!TryGetComprasGridParaAlteracao(out idsCompra, out dataCompraAtual))
            {
                return;
            }

            dt_alterar_data_compra.MaxDate = DateTime.Today;
            dt_alterar_data_compra.Value = dataCompraAtual.Date > DateTime.Today
                ? DateTime.Today
                : dataCompraAtual.Date;

            painelAlterarDataCompra.Visible = true;
            painelAlterarDataCompra.BringToFront();
        }

        private void btn_cancelar_alterar_data_compra_Click(object sender, EventArgs e)
        {
            painelAlterarDataCompra.Visible = false;
        }

        private void btn_confirmar_alterar_data_compra_Click(object sender, EventArgs e)
        {
            DateTime novaDataCompra = dt_alterar_data_compra.Value.Date;
            if (novaDataCompra > DateTime.Today)
            {
                MessageBox.Show("A data selecionada não pode ser futura.");
                return;
            }

            List<int> idsCompra;
            DateTime dataCompraAtual;
            if (!TryGetComprasGridParaAlteracao(out idsCompra, out dataCompraAtual))
            {
                return;
            }

            try
            {
                DataContextFactory.AlterarDataComprasApi(idsCompra, novaDataCompra);
                painelAlterarDataCompra.Visible = false;
                consulta();
                MessageBox.Show("Data da compra alterada com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar data da compra: " + ex.Message);
            }
        }
    }
}

