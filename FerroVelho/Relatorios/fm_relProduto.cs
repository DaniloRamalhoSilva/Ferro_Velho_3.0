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
    public partial class fm_relProduto : Form
    {
        public fm_relProduto()
        {
            InitializeComponent();
        }

        private void fm_relProduto_Load(object sender, EventArgs e)
        {

            this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;

            this.reportViewer1.RefreshReport();
            
            
        }

       
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimirNF();
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimirNF()
        {
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_produtos.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData()));            
            Export(report);
            Print();
        }

        private DataTable LoadSalesData()
        {
            string comando = "SELECT * FROM tb_produtos";
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
