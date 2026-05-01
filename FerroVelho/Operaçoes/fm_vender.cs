using FerroVelhoDAO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho
{
    public partial class fm_vender : Form
    {
        public fm_vender()
        {
            InitializeComponent();
        }

        private void fm_vender_Load(object sender, EventArgs e)
        {
            this.tb_produtosBindingSource.DataSource = DataContextFactory.ListarProdutosApi(false, true);

            cb_desProd.DataSource = tb_produtosBindingSource;
            cb_desProd.DisplayMember = "desc_prod";
            cb_desProd.ValueMember = "cod_prod";
            cb_desProd.SelectedIndex = -1;
            txt_codProd.Focus();
        }

        private void txt_codProd_Leave(object sender, EventArgs e)
        {
            if (txt_codProd.Text != "")
            {
                cb_desProd.SelectedValue = txt_codProd.Text.Trim();
                txt_valProd.Text = (0).ToString("N2");

                calcula();
            }
            else
            {
                cb_desProd.SelectedIndex = -1;
                txt_valProd.Text = (0).ToString("N2");
                calcula();
            }
        }

        private void cb_desProd_Leave(object sender, EventArgs e)
        {
            if (this.produtoCorrente == null)
            {
                return;
            }

            txt_codProd.Text = this.produtoCorrente.cod_prod;
            txt_valProd.Text = (0).ToString("N2");

            calcula();

        }

        private void txt_valProd_Leave(object sender, EventArgs e)
        {
            txt_valProd.Text = Convert.ToDecimal(txt_valProd.Text).ToString("N2");
            calcula();
        }

        private void txt_quant_Leave(object sender, EventArgs e)
        {
            calcula();
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
                return (tb_venda)this.tb_vendaBindingSource.Current;
            }
        }

        public tb_itemv itemCorrente
        {
            get
            {
                return (tb_itemv)this.tb_itemvBindingSource.Current;
            }
        }

        private void calcula()
        {
            decimal quant;

            if (txt_quant.Text == "")
            {
                quant = 0;
            }
            else
            {
                quant = Convert.ToDecimal(txt_quant.Text);
            }
            txt_quant.Text = quant.ToString("N3");
            decimal valor = Convert.ToDecimal(txt_valProd.Text);
            decimal sobTot = valor * quant;

            txt_subTot.Text = sobTot.ToString("N2");
            decimal total = 0;

            foreach (DataGridViewRow dg in dg_venda.Rows)
            {
                total = total + Convert.ToDecimal(dg.Cells[3].Value);
            }

            labelTotal.Text = total.ToString("N2");

            if (txt_codProd.Text != "")
            {
                decimal saldo = DataContextFactory.CalcularSaldoProdutoApi(txt_codProd.Text.Trim());

                lb_saldo.Text = saldo.ToString("N3");
            }
            else
            {
                lb_saldo.Text = "0,00";
            }

        }

        private void txt_codProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txt_valProd.Focus();
                e.Handled = true;
            }
        }

        private void txt_valProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
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
                calcula();
                clicar();
            }
            e.Handled = true;
        }

        private void bt_novoItem_Click(object sender, EventArgs e)
        {
            clicar();
        }

        private void clicar()
        {
            if (txt_quant.Text != "" && txt_valProd.Text != "" && cb_desProd.Text != "" && txt_codProd.Text != "")
            {
                if (Convert.ToDecimal(lb_saldo.Text) >= Convert.ToDecimal(txt_quant.Text))
                {
                    if (txt_nNota.Text == "")
                    {
                        novaNota();
                        novoItem();
                    }
                    else
                    {
                        novoItem();
                    }
                    bt_finalCompra.Enabled = true;
                    this.tb_itemvBindingSource.DataSource = DataContextFactory.ListarItensVendaApi(this.vendaCorrente.id_venda);

                    calcula();
                    txt_codProd.Text = "";
                    txt_valProd.Text = (0).ToString("N2");
                    txt_quant.Text = (0).ToString("N3");
                    txt_subTot.Text = (0).ToString("N2");
                    lb_saldo.Text = (0).ToString("N3");
                    cb_desProd.SelectedIndex = -1;
                    txt_codProd.Focus();
                }
                else
                {
                    MessageBox.Show("Saldo Insuficiente");
                    txt_quant.Focus();
                }
            }
            else
            {
                MessageBox.Show("Todos os campos são obrigatorios!");
                txt_quant.Focus();
            }
        }

        private void novaNota()
        {
            var venda = DataContextFactory.CriarVendaApi(DateTime.Now, DataContextFactory.usu.id_usuario, 0m);
            this.tb_vendaBindingSource.DataSource = venda;
            txt_nNota.Text = Convert.ToString(venda.id_venda);
            bt_finalCompra.Enabled = true;
        }

        private void novoItem()
        {
            DataContextFactory.InserirItemVendaApi(
                this.produtoCorrente.cod_prod,
                this.vendaCorrente.id_venda,
                Convert.ToDecimal(txt_quant.Text),
                Convert.ToDecimal(txt_subTot.Text),
                Convert.ToDecimal(txt_valProd.Text));
        }

        private void dg_venda_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 0)
            {
                e.Value = ((tb_produtos)e.Value).desc_prod;
            }

        }

        private void bt_finalCompra_Click(object sender, EventArgs e)
        {
            this.vendaCorrente.valor_nota = Convert.ToDecimal(labelTotal.Text);
            this.vendaCorrente.usuario = DataContextFactory.usu.id_usuario;
            DataContextFactory.AtualizarVendaApi(this.vendaCorrente.id_venda, this.vendaCorrente.valor_nota, this.vendaCorrente.usuario ?? 0);

            if (checkBox1.Checked == true)
            {
                imprimirNF(vendaCorrente.id_venda);
            }
            imprimirNF(vendaCorrente.id_venda);

            limpar();
            this.tb_itemvBindingSource.DataSource = new List<tb_itemv>();


            txt_quant.Focus();
        }

        private void limpar()
        {
            txt_codProd.Text = "";
            txt_nNota.Text = "";
            txt_valProd.Text = (0).ToString("N2");
            txt_quant.Text = (0).ToString("N3");
            txt_subTot.Text = (0).ToString("N2");
            labelTotal.Text = (0).ToString("N2");
            cb_desProd.SelectedIndex = -1;
            bt_finalCompra.Enabled = false;
            checkBox1.Checked = false;
        }

        private void bt_excluir_Click(object sender, EventArgs e)
        {
            try
            {
                string aki = itemCorrente.tb_produtos != null ? itemCorrente.tb_produtos.desc_prod : itemCorrente.cod_prod;

                if (MessageBox.Show("Realmente deseja excuir: " + aki, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataContextFactory.ExcluirItemVendaApi(itemCorrente.id_item);
                    this.tb_itemvBindingSource.DataSource = DataContextFactory.ListarItensVendaApi(this.vendaCorrente.id_venda);

                    MessageBox.Show("Produto excluido com sucesso!");
                    calcula();
                    txt_quant.Focus();
                }
                if (dg_venda.RowCount == 0)
                {
                    DataContextFactory.ExcluirVendaApi(this.vendaCorrente.id_venda);
                    this.tb_vendaBindingSource.DataSource = null;

                    limpar();
                    txt_quant.Focus();
                    bt_finalCompra.Enabled = false;

                }
            }
            catch
            {
                MessageBox.Show("Selecione um item valido!");
            }
        }

        private void fm_vender_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txt_nNota.Text != "")
            {
                MessageBox.Show("Imposivel sair venda em andamento!");
                e.Cancel = true;
            }
        }

        // Codigo referente a impressão do cupom fiscal >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimirNF(int nf)
        {
            var imp = DataContextFactory.BuscarImpressoraApi(1);
            if (imp != null)
            {
                this.tb_impressoraBindingSource.DataSource = new List<tb_impressora> { imp };
            }
            

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\Report1.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData(nf)));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            Export(report);
            Print();
        }

        private DataTable LoadSalesData(int nf)
        {
            return DataContextFactory.CarregarRelatorioVendaApi(nf);
        }


        public tb_impressora impressoraCorrente
        {
            get
            {
                return (tb_impressora)this.tb_impressoraBindingSource.Current;
            }
        }

        public void Export(LocalReport report)
        {
            string deviceInfo =
               @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3.3in</PageWidth>                
                <MarginTop>0.1in</MarginTop>
                <MarginLeft>0.05in</MarginLeft>
                <MarginRight>0.05in</MarginRight>
                <MarginBottom>0.1in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            PrintDialog printDlg = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            
            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                //printDoc.PrinterSettings.PrinterName = impressoraCorrente.nome_impressora;

                printDoc.PrinterSettings.PrinterName = printDlg.PrinterSettings.PrinterName;

                if (!printDoc.PrinterSettings.IsValid)
                {
                    throw new Exception("Error: cannot find the default printer.");
                }
                else
                {
                    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                    m_currentPageIndex = 0;
                    printDoc.Print();
                }
            }

        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            ev.Graphics.DrawImage(pageImage, adjustedRect);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}

