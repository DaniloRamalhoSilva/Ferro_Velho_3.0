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
    public partial class fm_relLucro : Form
    {
        public fm_relLucro()
        {
            InitializeComponent();
        }

        private string comando1, comando2;
        private DateTime inicio, fim;
        private int tipoR;

        private void fm_relLucro_Load(object sender, EventArgs e)
        {
            dt_fim.Value = DateTime.Now;
            dt_inicio.Value = DateTime.Now;
                       
            preencrer();
            tipo();
        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            preencrer();
        }

        private DataTable pesquisa1()
        {
           comando1 = "SELECT a.id_prod as codigo, tb_produtos.desc_prod as descricao, sum(a.PesoC) as pesoC, sum(a.TotalC) as Compra, sum(a.PesoV) as pesoV, sum(a.TotalV) as Venda, sum(a.TotalV) - sum(a.TotalC) as lucro, sum(a.PesoC) - sum(a.PesoV) as peso " +
                "from(SELECT tb_itemc.id_prod, sum(tb_itemc.quant_item) As PesoC, sum(tb_itemc.subTot_item) As TotalC, 0 as pesoV, 0 as totalV " +
                "FROM tb_itemc " +
                "INNER JOIN tb_compra ON tb_itemc.id_compra = tb_compra.id_compra " +
                "where tb_compra.data_compra between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) +
                "' GROUP BY tb_itemc.id_prod " +
                "union all " +
                "SELECT tb_itemv.id_prod, 0 As PesoC, 0 As TotalC, sum(tb_itemv.quant_item) as pesoV, sum(tb_itemv.subTot_item) as totalV " +
                "FROM tb_itemv " +
                "INNER JOIN tb_venda ON tb_itemv.id_venda = tb_venda.id_venda " +
                "where tb_venda.data_venda between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) +
                "' GROUP BY tb_itemv.id_prod)a " +
                "INNER JOIN tb_produtos ON a.id_prod = tb_produtos.id_prod " +
                "GROUP BY a.id_prod, tb_produtos.desc_prod";
            DataTable dt1 = DataContextFactory.Filtrar(comando1);
            return dt1;
           
        }

        private DataTable pesquisa2()
        {            
            comando2 = "SELECT sum(e.pesoC) as pesoCT, sum(e.Compra) as compraT, sum(e.pesoV) as pesoVT, sum(e.Venda) as vendaT, sum(e.pesoC) - sum(e.pesoV) as pesoT, sum(e.Venda) - sum(e.Compra) as lucroT " +
                "from(SELECT a.id_prod as codigo, tb_produtos.desc_prod as descricao, sum(a.PesoC) as pesoC, sum(a.TotalC) as Compra, sum(a.PesoV) as pesoV, sum(a.TotalV) as Venda, sum(a.TotalV) - sum(a.TotalC) as lucro, sum(a.PesoC) - sum(a.PesoV) as peso " +
                "from(SELECT tb_itemc.id_prod, sum(tb_itemc.quant_item) As PesoC, sum(tb_itemc.subTot_item) As TotalC, 0 as pesoV, 0 as totalV " +
                "FROM tb_itemc " +
                "INNER JOIN tb_compra ON tb_itemc.id_compra = tb_compra.id_compra " +
                "where tb_compra.data_compra between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) +
                "' GROUP BY tb_itemc.id_prod " +
                "union all " +
                "SELECT tb_itemv.id_prod, 0 As PesoC, 0 As TotalC, sum(tb_itemv.quant_item) as pesoV, sum(tb_itemv.subTot_item) as totalV " +
                "FROM tb_itemv " +
                "INNER JOIN tb_venda ON tb_itemv.id_venda = tb_venda.id_venda " +
                "where tb_venda.data_venda between '" + inicio.Date.Add(new TimeSpan(00, 00, 00)) + "' and '" + fim.Date.Add(new TimeSpan(23, 59, 59)) +
                "' GROUP BY tb_itemv.id_prod)a " +
                "INNER JOIN tb_produtos ON a.id_prod = tb_produtos.id_prod " +
                "GROUP BY a.id_prod, tb_produtos.desc_prod)e";
            DataTable dt2 = DataContextFactory.Filtrar(comando2);
            return dt2;
        }

        private void preencrer()
        {            
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            DataTable dt1 = pesquisa1();
            DataTable dt2 = pesquisa2();

            dataGridView1.DataSource = dt1;
            dataGridView1.DataMember = dt1.TableName;
            dataGridView2.DataSource = dt1;
            dataGridView2.DataMember = dt1.TableName;
            dataGridView3.DataSource = dt2;
            dataGridView3.DataMember = dt2.TableName;
            dataGridView4.DataSource = dt2;
            dataGridView4.DataMember = dt2.TableName;

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            preencrer();
            imprimirNF();
        }

        private void bt_pagImp_Click(object sender, EventArgs e)
        {
            preencrer();
            fm_impFinanceiro fm = new fm_impFinanceiro(tipoR, pesquisa1(), pesquisa2(), inicio.ToString("dd-MM-yyyy"), fim.ToString("dd-MM-yyyy"));
            fm.Show();
        }

        private void bt_detalhe_Click(object sender, EventArgs e)
        {
            tipo();
        }

        private void tipo()
        {
            if (bt_detalhe.Text == "Simples")
            {
                bt_detalhe.Text = "Detalhado";
                this.Size = new Size(672, 600);
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                dataGridView4.Visible = false;
                dataGridView3.Visible = true;
                tipoR = 1;
            }
            else
            {
                bt_detalhe.Text = "Simples";
                this.Size = new Size(949, 600);
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView4.Visible = true;
                dataGridView3.Visible = false;
                tipoR = 2;
            }
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimirNF()
        {
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_financeiroSimp.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData2()));
            report.DataSources.Add(new ReportDataSource("DataSet2", LoadSalesData1()));
            report.DataSources.Add(new ReportDataSource("DataSet2", LoadSalesData1()));
            report.DataSources.Add(new ReportDataSource("DataSet2", LoadSalesData1()));
            report.DataSources.Add(new ReportDataSource("DataSet2", LoadSalesData1()));
            report.DataSources.Add(new ReportDataSource("DataSet2", LoadSalesData1()));
            report.DataSources.Add(new ReportDataSource("DataSet2", LoadSalesData1()));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataInicio", dt_inicio.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataFim", dt_fim.Text));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            Export(report);
            Print();
        }

        private DataTable LoadSalesData1()
        {           
            DataTable dt = DataContextFactory.Filtrar(comando1);

            return dt;
        }

        private DataTable LoadSalesData2()
        {
            DataTable dt = DataContextFactory.Filtrar(comando2);

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
