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

namespace FerroVelho.Relatorios
{
    public partial class fm_relVenda : Form
    {
        public fm_relVenda()
        {
            InitializeComponent();
        }
        DateTime inicio, fim;

        private void fm_relVenda_Load(object sender, EventArgs e)
        {
            dt_fim.Value = DateTime.Now;
            dt_inicio.Value = DateTime.Now;

            pesquisa();
        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void bt_imprimir_Click_1(object sender, EventArgs e)
        {
            pesquisa();            
            imprimirNF();
        }

        string comando;

        private void pesquisa()
        {
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            comando = "SELECT tb_itemv.id_prod, tb_produtos.desc_prod, sum(tb_itemv.quant_item) As Peso, sum(tb_itemv.subTot_item) As Total " +
                "FROM tb_itemv " +
                "INNER JOIN tb_produtos ON tb_itemv.id_prod = tb_produtos.id_prod " +
                "INNER JOIN tb_venda ON tb_itemv.id_venda = tb_venda.id_venda  " +
                "WHERE tb_venda.data_venda between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) +
                "' GROUP BY tb_itemv.id_prod, tb_produtos.desc_prod";
            DataTable dt = DataContextFactory.Filtrar(comando);
            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = dt.TableName;

            SqlCommand comand = new SqlCommand();
            comand.CommandType = CommandType.Text;
            comand.CommandText = "SELECT sum(tb_itemv.subTot_item) as total " +
                "FROM tb_itemv " +
                "INNER JOIN tb_venda ON tb_itemv.id_venda = tb_venda.id_venda " +
                "WHERE tb_venda.data_venda between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) + "'";
            decimal cInicio = DataContextFactory.FiltrarValor(comand);

            lb_total.Text = cInicio.ToString("C2");
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimirNF()
        {
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_venda.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData()));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataInicio", dt_inicio.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataFim", dt_fim.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            Export(report);
            Print();
        }

        private DataTable LoadSalesData()
        {            
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
