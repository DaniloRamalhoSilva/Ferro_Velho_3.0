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

namespace FerroVelho.Relatorios
{
    public partial class fm_relCompra : Form
    {
        public fm_relCompra()
        {
            InitializeComponent();
        }

        DateTime comeco, inicio, fim;
        
        private void fm_relCompra_Load(object sender, EventArgs e)
        {
            dt_fim.Value = DateTime.Now;
            dt_inicio.Value = DateTime.Now;                                    

            pesquisa();
            caixa();

        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            pesquisa();
            caixa();
        }

        private void pesquisa()
        {
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            DataTable dtApi = DataContextFactory.CarregarResumoCompraProdutosApi(
                inicio.Date.Add(new TimeSpan(00, 00, 00)),
                fim.Date.Add(new TimeSpan(23, 59, 59)));
            dataGridView1.DataSource = dtApi;
            dataGridView1.DataMember = dtApi.TableName;
        }
                
        private void caixa()
        {
            comeco = dt_inicio.MinDate;
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            DataTable resumo = DataContextFactory.CalcularResumoCompraCaixaApi(
                inicio.Date.Add(new TimeSpan(00, 00, 00)),
                fim.Date.Add(new TimeSpan(23, 59, 59)));
            if (resumo.Rows.Count == 0)
            {
                return;
            }

            DataRow row = resumo.Rows[0];
            decimal saidaAntesPg = Convert.ToDecimal(row["saida_antes"]);
            decimal entradaAntesPg = Convert.ToDecimal(row["entrada_antes"]);
            decimal compraAntesPg = Convert.ToDecimal(row["compra_antes"]);
            decimal descontoAntesPg = Convert.ToDecimal(row["desconto_antes"]);
            decimal creditoAntesPg = Convert.ToDecimal(row["credito_antes"]);
            decimal totalInicioPg = entradaAntesPg - saidaAntesPg - compraAntesPg + descontoAntesPg + creditoAntesPg;

            decimal saidaPeriodoPg = Convert.ToDecimal(row["saida_periodo"]);
            decimal entradaPeriodoPg = Convert.ToDecimal(row["entrada_periodo"]);
            decimal compraPeriodoPg = Convert.ToDecimal(row["compra_periodo"]);
            decimal descontoPeriodoPg = Convert.ToDecimal(row["desconto_periodo"]);
            decimal creditoPeriodoPg = Convert.ToDecimal(row["credito_periodo"]);

            decimal saldoPg = totalInicioPg + entradaPeriodoPg - saidaPeriodoPg - compraPeriodoPg + descontoPeriodoPg + creditoPeriodoPg;

            lb_total.Text = compraPeriodoPg.ToString("C2");
            lb_inicial.Text = totalInicioPg.ToString("C2");
            lb_entrada.Text = entradaPeriodoPg.ToString("C2");
            lb_saida.Text = saidaPeriodoPg.ToString("C2");
            lb_gastoCompra.Text = (compraPeriodoPg - descontoPeriodoPg - creditoPeriodoPg).ToString("C2");
            lb_saldo.Text = saldoPg.ToString("C2");
            lb_adiantamento.Text = descontoPeriodoPg.ToString("C2");
            lb_credito.Text = creditoPeriodoPg.ToString("C2");
            lb_TgastoCompra.Text = (compraPeriodoPg - descontoPeriodoPg - creditoPeriodoPg).ToString("C2");

        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            pesquisa();
            caixa();
            imprimirNF();
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimirNF()
        {
            var imp = DataContextFactory.BuscarImpressoraApi(1);
            if (imp != null)
            {
                this.tb_impressoraBindingSource.DataSource = new List<tb_impressora> { imp };
            }

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_compra.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData()));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataInicio", dt_inicio.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataFim", dt_fim.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sInicio", lb_inicial.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sEntrada", lb_entrada.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sSaida", lb_saida.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sGastoCompra", lb_gastoCompra.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("sSaldo", lb_saldo.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            Export(report);
            Print();
        }

        private DataTable LoadSalesData()
        {
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            return DataContextFactory.CarregarResumoCompraProdutosApi(
                inicio.Date.Add(new TimeSpan(00, 00, 00)),
                fim.Date.Add(new TimeSpan(23, 59, 59)));
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

