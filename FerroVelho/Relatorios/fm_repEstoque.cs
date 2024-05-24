using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FerroVelho.Relatorios
{
    public partial class fm_repEstoque : Form
    {
        #region [ VARIAVEIS GLOBAL ]

        // Cria a conexão com a DAL
        private static FerroVelhoDAO.DataContextFactory _DAO = new FerroVelhoDAO.DataContextFactory();
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        #endregion

        #region [ CONSTRUTOR ]

        public fm_repEstoque()
        {
            InitializeComponent();
        }

        #endregion

        #region [ MÉTODOS PRIVADOS ]

        private void AbrirTelaImpressao()
        {
            DataTable dt = Pesquisa();
            fm_impEstoque fm = new fm_impEstoque(dt);
            fm.Show();
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
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

        public void Imprimir()
        {
            string[] arrPar = new string[] { "@id_impressora" };
            string[] arrVal = new string[] { "1" };
            DataTable dt = _DAO.GetDataTableBySP("s_tb_impressora", arrPar, arrVal);

            this.tb_impressoraBindingSource.DataSource = dt;

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_estoque.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", _DAO._nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", _DAO._tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", _DAO._endereco));
            Export(report);
            Print();
        }

        private DataTable Pesquisa()
        {
            DateTime dataInicio = dt_inicio.Value;
            DateTime dataFim = dt_fim.Value;

            string inicio = dataInicio.Date.Add(new TimeSpan(00, 00, -01)).ToString("dd/MM/yyyy HH:mm:ss.ff");
            string fim = dataFim.Date.Add(new TimeSpan(24, 00, -01)).ToString("dd/MM/yyyy HH:mm:ss.ff");

            string[] arrPar = new string[] { "@dataInicial", "@dataFinal" };
            string[] arrVal = new string[] { inicio, fim };
            DataTable dt = _DAO.GetDataTableBySP("s_tb_estoque", arrPar, arrVal);

            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = dt.TableName;

            return dt;
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


        #endregion

        #region  [EVENTOS ]

        private void fm_repEstoque_Load(object sender, EventArgs e)
        {
            Pesquisa();
        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            Pesquisa();
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void bt_relImp_Click(object sender, EventArgs e)
        {
            AbrirTelaImpressao();
        }

        #endregion

    }
}
