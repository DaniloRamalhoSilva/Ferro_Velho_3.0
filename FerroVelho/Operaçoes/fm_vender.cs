using FerroVelhoDAO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

            this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;
            cb_desProd.DataSource = tb_produtosBindingSource;
            cb_desProd.DisplayMember = "desc_prod";
            cb_desProd.ValueMember = "id_prod";
            cb_desProd.SelectedIndex = -1;
            txt_codProd.Focus();
        }

        private void txt_codProd_Leave(object sender, EventArgs e)
        {
            if (txt_codProd.Text != "")
            {
                cb_desProd.SelectedValue = Convert.ToInt32(txt_codProd.Text);
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
            txt_codProd.Text = Convert.ToString(this.produtoCorrente.id_prod);
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
                fm_estoque fm = new fm_estoque();
                decimal saldo = fm.calcular(Convert.ToInt32(txt_codProd.Text));
                lb_saldo.Text = saldo.ToString("N3");
            }
            else
            {
                lb_saldo.Text = "0,00";
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
                txt_valProd.Focus();
            }
            e.Handled = true;
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
                    this.tb_itemvBindingSource.DataSource = DataContextFactory.DataContext.tb_itemv.Where(x => x.id_venda == this.vendaCorrente.id_venda);

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
            this.tb_vendaBindingSource.DataSource = DataContextFactory.DataContext.tb_venda;
            this.tb_vendaBindingSource.AddNew();
            this.vendaCorrente.data_venda = DateTime.Now;
            this.vendaCorrente.usuario = DataContextFactory.usu.id_usuario;
            this.tb_vendaBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();
            txt_nNota.Text = Convert.ToString(vendaCorrente.id_venda);
            bt_finalCompra.Enabled = true;
        }

        private void novoItem()
        {
            this.tb_itemvBindingSource.DataSource = DataContextFactory.DataContext.tb_itemv;

            this.tb_itemvBindingSource.AddNew();

            this.itemCorrente.id_prod = this.produtoCorrente.id_prod;
            this.itemCorrente.id_venda = this.vendaCorrente.id_venda;
            this.itemCorrente.quant_item = Convert.ToDecimal(txt_quant.Text);
            this.itemCorrente.subTot_item = Convert.ToDecimal(txt_subTot.Text);
            this.itemCorrente.valr_item = Convert.ToDecimal(txt_valProd.Text);

            this.tb_itemvBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();
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
            this.tb_vendaBindingSource.EndEdit();
            this.vendaCorrente.valor_nota = Convert.ToDecimal(labelTotal.Text);
            this.vendaCorrente.usuario = DataContextFactory.usu.id_usuario;
            DataContextFactory.DataContext.SubmitChanges();

            if (checkBox1.Checked == true)
            {
                imprimirNF(vendaCorrente.id_venda);
            }
            imprimirNF(vendaCorrente.id_venda);

            limpar();
            this.tb_itemvBindingSource.DataSource = DataContextFactory.DataContext.tb_itemv.Where(x => x.id_venda == this.vendaCorrente.id_venda + 1);


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
                string aki = ((tb_produtos)dg_venda[0, dg_venda.CurrentRow.Index].Value).desc_prod;
                if (MessageBox.Show("Realmente deseja excuir: " + aki, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.tb_itemvBindingSource.RemoveCurrent();
                    DataContextFactory.DataContext.SubmitChanges();
                    MessageBox.Show("Produto excluido com sucesso!");
                    calcula();
                    txt_quant.Focus();
                }
                if (dg_venda.RowCount == 0)
                {
                    this.tb_vendaBindingSource.RemoveCurrent();
                    DataContextFactory.DataContext.SubmitChanges();
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
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);
            

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
            string comando = "SELECT tb_itemv.quant_item, tb_itemv.subTot_item, tb_itemv.valr_item, tb_produtos.desc_prod, tb_venda.data_venda, tb_itemv.id_prod, tb_itemv.id_venda, tb_usuario.nome_usuario, tb_venda.usuario FROM tb_itemv INNER JOIN tb_venda ON tb_itemv.id_venda = tb_venda.id_venda INNER JOIN tb_produtos ON tb_itemv.id_prod = tb_produtos.id_prod INNER JOIN tb_usuario ON tb_venda.usuario = tb_usuario.id_usuario WHERE tb_itemv.id_venda =" + nf;
            DataTable dt = DataContextFactory.Filtrar(comando);

            return dt;
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
