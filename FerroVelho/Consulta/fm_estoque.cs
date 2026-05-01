using FerroVelho.Relatorios;
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
            idprodDataGridViewTextBoxColumn.DataPropertyName = "cod_prod";
            this.tb_produtosBindingSource.DataSource = DataContextFactory.ListarProdutosApi(false, true);

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
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
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
                decimal saldo = DataContextFactory.CalcularSaldoProdutoApi(Convert.ToString(dg.Cells[0].Value));

                dg.Cells[2].Value = saldo;
            }
        }

        public decimal calcular(int id )
        {
            return DataContextFactory.CalcularSaldoProdutoApi(Convert.ToString(id));
        }

        public decimal calcular(string codigo)
        {
            return DataContextFactory.CalcularSaldoProdutoApi(codigo);
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
            string id = this.produtoCorrente.cod_prod;
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
            this.tb_produtosBindingSource.DataSource = DataContextFactory.ListarProdutosApi(false, true)
                .Where(x => (x.desc_prod ?? string.Empty).StartsWith(txt_desc.Text, StringComparison.CurrentCultureIgnoreCase))
                .ToList();

            txt_codProd.Text = "";
            caregarEstoque();
        }

        private void txt_codProd_KeyUp(object sender, KeyEventArgs e)
            {
            if (txt_codProd.Text == "")
            {
                this.tb_produtosBindingSource.DataSource = DataContextFactory.ListarProdutosApi(false, true);

                txt_desc.Text = "";
                caregarEstoque();
            }
            else
            {
                this.tb_produtosBindingSource.DataSource = DataContextFactory.ListarProdutosApi(false, true)
                    .Where(x => (x.cod_prod ?? string.Empty).StartsWith(txt_codProd.Text, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();

                txt_desc.Text = "";
                caregarEstoque();
            }
            
        }

        private void bt_cancelar_Click(object sender, EventArgs e)
        {
            load();
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimir()
        {
            var imp = DataContextFactory.BuscarImpressoraApi(1);
            if (imp != null)
            {
                this.tb_impressoraBindingSource.DataSource = new List<tb_impressora> { imp };
            }

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_estoque2.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData()));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            Export(report);
            Print();
        }

        private DataTable LoadSalesData()
        {
            return DataContextFactory.CarregarEstoqueAtualApi();
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
            PrintDocument printDoc = new PrintDocument();
            PrintDialog printDlg = new PrintDialog();

            if(printDlg.ShowDialog() == DialogResult.OK)
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

        private void bt_visuImpre_Click(object sender, EventArgs e)
        {
            fm_impEstoque2 fm = new fm_impEstoque2();
            fm.Show();
        }
    }
}

