using FerroVelhoDAO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
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
    public class Dao
    {
        public decimal valorDevedor(int id)
        {
            decimal adianta, pag, acerto, credito, total;
            SqlCommand comando;

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_compra.desconto_compra) as total " +
                "From tb_compra " +
                "where tb_compra.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            pag = DataContextFactory.FiltrarValor(comando);

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota) as total " +
                "From tb_compra " +
                "where tb_compra.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            credito = DataContextFactory.FiltrarValor(comando);

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_caixa.valor_caixa)  as total " +
                "From tb_caixa " +
                "where tb_caixa.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            adianta = DataContextFactory.FiltrarValor(comando);

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_aCliente.valor_aCliente)  as total " +
                "From tb_aCliente " +
                "where tb_aCliente.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            acerto = DataContextFactory.FiltrarValor(comando);

            total = adianta + pag + acerto + credito;

            if (total > 0)
            {
                total = 0;
            }
            else
            {
                total = total * -1;
            }
            return total;
        }

        public decimal valorCredito(int id)
        {
            decimal adianta, pag, acerto, credito, total;
            SqlCommand comando;

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_compra.desconto_compra) as total " +
                "From tb_compra " +
                "where tb_compra.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            pag = DataContextFactory.FiltrarValor(comando);

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota) as total " +
                "From tb_compra " +
                "where tb_compra.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            credito = DataContextFactory.FiltrarValor(comando);

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_caixa.valor_caixa)  as total " +
                "From tb_caixa " +
                "where tb_caixa.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            adianta = DataContextFactory.FiltrarValor(comando);

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select sum(tb_aCliente.valor_aCliente)  as total " +
                "From tb_aCliente " +
                "where tb_aCliente.id_cliente = @id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", id);

            acerto = DataContextFactory.FiltrarValor(comando);

            total = adianta + pag + credito + acerto;

            if (total < 0)
            {
                total = 0;
            }
           
            return total;
        }

        // Codigo referente a impressão do cupom fiscal >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimir(DateTime data, string desc, string valor, string cliente, string telefone, string cpf )
        {            

            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\rel_comprovante.rdlc";
            //report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData(nf)));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("data", Convert.ToString(data)));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("descricao", desc));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("valor", valor));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("usuario", DataContextFactory.usu.nome_usuario));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("cliente", cliente));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("telefone", telefone));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("cpf", cpf));
            Export(report);
            Print();
        }

        //private DataTable LoadSalesData(int nf)
        //{
        //    string comando = "SELECT tb_itemv.quant_item, tb_itemv.subTot_item, tb_itemv.valr_item, tb_produtos.desc_prod, tb_venda.data_venda, tb_itemv.id_prod, tb_itemv.id_venda, tb_usuario.nome_usuario, tb_venda.usuario FROM tb_itemv INNER JOIN tb_venda ON tb_itemv.id_venda = tb_venda.id_venda INNER JOIN tb_produtos ON tb_itemv.id_prod = tb_produtos.id_prod INNER JOIN tb_usuario ON tb_venda.usuario = tb_usuario.id_usuario WHERE tb_itemv.id_venda =" + nf;
        //    DataTable dt = DataContextFactory.Filtrar(comando);

        //    return dt;
        //}


        //public tb_impressora impressoraCorrente
        //{
        //    get
        //    {
        //        return (tb_impressora)this.tb_impressoraBindingSource.Current;
        //    }
        //}

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
