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
    public partial class fm_repEstoque : Form
    {
        public fm_repEstoque()
        {
            InitializeComponent();
        }
        
        private void fm_repEstoque_Load(object sender, EventArgs e)
        {
            dt_fim.Value = DateTime.Now;
            dt_inicio.Value = DateTime.Now;

            pesquisa();
            DataTable dt = DataContextFactory.Filtrar(comando);
            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = dt.TableName;

        }


        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            pesquisa();
            DataTable dt = DataContextFactory.Filtrar(comando);
            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = dt.TableName;
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {                        
            imprimir();                    
        }

        DateTime comeco, inicio, fim;
        string comando, Pinicio, Pcomeço1, Pcomeço2, Pfim;
        public DataTable Propriedade { get; set; }


        public DataTable pesquisa()
        {
            inicio = dt_inicio.MinDate;
            comeco = dt_inicio.Value;
            fim = dt_fim.Value;

            Pinicio = inicio.ToString();
            Pcomeço1 = comeco.Date.Add(new TimeSpan(00, 00, -01)).ToString();
            Pcomeço2 = comeco.Date.Add(new TimeSpan(00, 00, 00)).ToString();
            Pfim = fim.ToString();

            comando = "SELECT e.IdProduto as Codigo, tb_produtos.desc_prod as Descrição, sum(e.Inicio) as Inicio, sum(e.Entrada) as Entrada, sum(e.Saida) as Saida, sum(e.Inicio) + sum(e.Entrada) - sum(e.Saida) as Estoque " +
                "FROM(SELECT a.IdProduto, sum(a.Entrada) - sum(a.Saida) as Inicio, 0 as Entrada, 0 As Saida " +
                "FROM(SELECT tb_compra.data_compra as data, tb_itemc.id_prod as IdProduto, sum(tb_itemc.quant_item) as Entrada, 0 As Saida " +
                "FROM tb_itemc " +
                "INNER JOIN tb_compra on tb_itemc.id_compra = tb_compra.id_compra " +
                "where tb_compra.data_compra between '" + Pinicio + "' and '" + Pcomeço1 + "' " + 
                "GROUP BY tb_compra.data_compra, tb_itemc.id_prod " +
                "union all " +
                "SELECT tb_venda.data_venda as data, tb_itemv.id_prod as IdProduto, 0 as Entrada, sum(tb_itemv.quant_item) As Saida " +
                "FROM tb_itemv " +
                "INNER JOIN tb_venda on tb_itemv.id_venda = tb_venda.id_venda " +
                "where tb_venda.data_venda between '" + Pinicio + "' and '" + Pcomeço1 + "' " +
                "GROUP BY tb_venda.data_venda, tb_itemv.id_prod)a " +
                "GROUP BY a.IdProduto " +
                "union all " +
                "SELECT a.IdProduto, 0 as Inicio, sum(a.Entrada) as Entrada, sum(a.Saida) As Saida " +
                "FROM(SELECT tb_compra.data_compra as data, tb_itemc.id_prod as IdProduto, sum(tb_itemc.quant_item) as Entrada, 0 As Saida " +
                "FROM tb_itemc " +
                "INNER JOIN tb_compra on tb_itemc.id_compra = tb_compra.id_compra " +
                "where tb_compra.data_compra between '" + Pcomeço2 + "' and '" + Pfim + "' " +
                "GROUP BY tb_compra.data_compra, tb_itemc.id_prod " +
                "union all " +
                "SELECT tb_venda.data_venda as data, tb_itemv.id_prod as IdProduto, 0 as Entrada, sum(tb_itemv.quant_item) As Saida " +
                "FROM tb_itemv " +
                "INNER JOIN tb_venda on tb_itemv.id_venda = tb_venda.id_venda " +
                "where tb_venda.data_venda between '" + Pcomeço2 + "' and '" + Pfim + "' " +
                "GROUP BY tb_venda.data_venda, tb_itemv.id_prod)a " +
                "GROUP BY a.IdProduto)e " +
                "INNER JOIN tb_produtos ON e.IdProduto = tb_produtos.id_prod " +
                "GROUP BY e.IdProduto, tb_produtos.desc_prod";
            DataTable dt = DataContextFactory.Filtrar(comando);            
            return dt;
        }



        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>     IMPRIMIR                <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


        private void bt_relImp_Click(object sender, EventArgs e)
        {
            fm_impEstoque fm = new fm_impEstoque(pesquisa());
            fm.Show();
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimir()
        {
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_estoque.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData()));            
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
