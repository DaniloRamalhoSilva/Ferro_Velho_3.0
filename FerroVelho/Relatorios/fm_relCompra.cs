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

            string comando = " SELECT tb_itemc.id_prod, tb_produtos.desc_prod, sum(tb_itemc.quant_item) As Peso, sum(tb_itemc.subTot_item) As Total " +
                "FROM tb_itemc " +
                "INNER JOIN tb_produtos ON tb_itemc.id_prod = tb_produtos.id_prod " +
                "INNER JOIN tb_compra ON tb_itemc.id_compra = tb_compra.id_compra " +
                "WHERE tb_compra.data_compra between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) +
                "' GROUP BY tb_itemc.id_prod, tb_produtos.desc_prod";
            DataTable dt = DataContextFactory.Filtrar(comando);
            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = dt.TableName;
        }
                
        private void caixa()
        {
            comeco = dt_inicio.MinDate;
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;

            comando.CommandText = "SELECT sum(valor_caixa)  * -1 as total " +
                "FROM tb_caixa " +
                "WHERE data_caixa between '" + comeco + "' and '" + inicio.Date.Add(new TimeSpan(00, 00, -01)) + "' and valor_caixa < 0 ";
            decimal sInicioP = DataContextFactory.FiltrarValor(comando);

            comando.CommandText = "SELECT sum(valor_caixa) as total " +
                "FROM tb_caixa " +
                "WHERE data_caixa between '" + comeco + "' and '" + inicio.Date.Add(new TimeSpan(00, 00, -01)) + "' and valor_caixa > 0 ";
            decimal sInicioN = DataContextFactory.FiltrarValor(comando);


            comando.CommandText = "SELECT sum(tb_itemc.subTot_item) as total " +
                "FROM tb_itemc " +
                "INNER JOIN tb_compra ON tb_itemc.id_compra = tb_compra.id_compra " +
                "WHERE tb_compra.data_compra between '" + comeco + "' and '" + inicio.Date.Add(new TimeSpan(00, 00, -01)) + "'";
            decimal cInicio = DataContextFactory.FiltrarValor(comando);

            comando.CommandText = "SELECT sum(tb_compra.desconto_compra) as total " +
                "from tb_compra " +
                "WHERE tb_compra.data_compra between '" + comeco + "' and '" + inicio.Date.Add(new TimeSpan(00, 00, -01)) + "'";
            decimal descontoInicio = DataContextFactory.FiltrarValor(comando);

            comando.CommandText = "SELECT sum(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota)  as total " +
                "from tb_compra " +
                "WHERE tb_compra.data_compra between '" + comeco + "' and '" + inicio.Date.Add(new TimeSpan(00, 00, -01)) + "'";
            decimal credito= DataContextFactory.FiltrarValor(comando);


            decimal totInicio = sInicioN - sInicioP - cInicio + descontoInicio + credito;



            comando.CommandText = "SELECT sum(valor_caixa) * -1 as total " +
               "FROM tb_caixa " +
               "WHERE data_caixa between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) + "' and valor_caixa < 0 ";
            sInicioP = DataContextFactory.FiltrarValor(comando);

            comando.CommandText = "SELECT sum(valor_caixa) as total " +
               "FROM tb_caixa " +
               "WHERE data_caixa between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) + "' and valor_caixa > 0 ";
            sInicioN = DataContextFactory.FiltrarValor(comando);

            comando.CommandText = "SELECT sum(tb_itemc.subTot_item) as total " +
                "FROM tb_itemc " +
                "INNER JOIN tb_compra ON tb_itemc.id_compra = tb_compra.id_compra " +
                "WHERE tb_compra.data_compra between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) + "'";
            cInicio = DataContextFactory.FiltrarValor(comando);

            comando.CommandText = "SELECT sum(tb_compra.desconto_compra) as total " +
                "from tb_compra " +
                "WHERE tb_compra.data_compra between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) + "'";
            descontoInicio = DataContextFactory.FiltrarValor(comando);

            comando.CommandText = "SELECT sum(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota)  as total " +
                "from tb_compra " +
                "WHERE tb_compra.data_compra between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) + "'";
            credito = DataContextFactory.FiltrarValor(comando);

            decimal saldo = totInicio + sInicioN - sInicioP - cInicio + descontoInicio + credito;

            lb_total.Text = cInicio.ToString("C2");

            lb_inicial.Text = totInicio.ToString("C2");
            lb_entrada.Text = sInicioN.ToString("C2");
            lb_saida.Text = sInicioP.ToString("C2");
            lb_gastoCompra.Text = (cInicio - descontoInicio - credito).ToString("C2");
            lb_saldo.Text = saldo.ToString("C2");
            
            lb_adiantamento.Text = descontoInicio.ToString("C2");
            lb_credito.Text = credito.ToString("C2");

            lb_TgastoCompra.Text = (cInicio - descontoInicio - credito).ToString("C2");           

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
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);

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

            string comando = " SELECT tb_itemc.id_prod, tb_produtos.desc_prod, sum(tb_itemc.quant_item) As Peso, sum(tb_itemc.subTot_item) As Total " +
                "FROM tb_itemc " +
                "INNER JOIN tb_produtos ON tb_itemc.id_prod = tb_produtos.id_prod " +
                "INNER JOIN tb_compra ON tb_itemc.id_compra = tb_compra.id_compra " +
                "WHERE tb_compra.data_compra between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) +
                "' GROUP BY tb_itemc.id_prod, tb_produtos.desc_prod";
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
