using FerroVelhoDAO;
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

private int m_currentPageIndex;
        private IList<Stream> m_streams;

        #endregion

        #region [ CONSTRUTOR ]

        public fm_repEstoque()
        {
            InitializeComponent();
            dt_inicio.Value = DateTime.Now;
            dt_fim.Value = DateTime.Now;            
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
            DataTable dt = DataContextFactory.GetDataTableBySP("s_tb_impressora", arrPar, arrVal);

            this.tb_impressoraBindingSource.DataSource = dt;

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_estoque.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            Export(report);
            Print();
        }

        private DataTable Pesquisa()
        {
            DateTime dataInicio = dt_inicio.Value;
            DateTime dataFim = dt_fim.Value;

            string[] arrPar = new string[] { "@dataInicial", "@dataFinal", "@id_prod" };
            string[] arrVal = new string[] { dataInicio.ToString("dd/MM/yyyy 00:00:00.00"), dataFim.ToString("dd/MM/yyyy 23:59:59.99"), string.IsNullOrEmpty(txt_codProd.Text)? null :  txt_codProd.Text.Trim() };
            DataTable dt = DataContextFactory.GetDataTableBySP("s_tb_estoque", arrPar, arrVal);

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
            txt_codProd.Focus();
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

        private void txt_codProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                Pesquisa();
            }
            e.Handled = true;
        }

        #endregion
    }
}
