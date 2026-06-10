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
    public partial class fm_recurcos : Form
    {
        public fm_recurcos()
        {
            InitializeComponent();
        }

        private void txt_valRecur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void fm_recurcos_Load(object sender, EventArgs e)
        {
            atualizar();           
        }
        
        public decimal calcular()
        {
            decimal totalSaida;
            decimal totalEntrada;

            try{ totalSaida = Convert.ToDecimal(DataContextFactory.DataContext.tb_itemc.Sum(x => x.subTot_item));}
            catch{totalSaida = 0;}

            try{totalEntrada = Convert.ToDecimal(DataContextFactory.DataContext.tb_caixa.Sum(x => x.valor_caixa));}
            catch{totalEntrada = 0;}

            decimal saldo = totalEntrada - totalSaida;
            
            return saldo;            
        }

        public void atualizar()
        {
            decimal saldo = calcular();
            lb_saldo.Text = saldo.ToString("N2");            
        }

        string valorImp, desc;
        DateTime data = DateTime.Now;


        private void bt_add_Click(object sender, EventArgs e)
        {
            if (txt_valRecur.Text != "")
            {                
                decimal valor = Convert.ToDecimal(txt_valRecur.Text);
                valorImp = txt_valRecur.Text;
                desc = "Valor adicionado em caixa: ";

                this.tb_caixaBindingSource.DataSource = DataContextFactory.DataContext.tb_caixa;

                this.tb_caixaBindingSource.AddNew();
                this.saldoCorrente.data_caixa = DateTime.Now;
                this.saldoCorrente.valor_caixa = valor;
                this.saldoCorrente.usuario = DataContextFactory.usu.id_usuario;
                this.tb_caixaBindingSource.EndEdit();
                DataContextFactory.DataContext.SubmitChanges();
                atualizar();
                txt_valRecur.Text = (0).ToString("N2");
                MessageBox.Show("Valor adicionado com sucesso!");
                imprimir();

            }
            else
            {
                MessageBox.Show("Compo valor é obrigatorio");
            }            
                        
        }

        public tb_caixa saldoCorrente
        {
            get
            {
                return (tb_caixa)this.tb_caixaBindingSource.Current;
            }
        }

        private void txt_valRecur_Leave(object sender, EventArgs e)
        {
            if (txt_valRecur.Text != "")
            {
                txt_valRecur.Text = Convert.ToDecimal(txt_valRecur.Text).ToString("N2");
            }
            else
            {
                txt_valRecur.Text = (0).ToString("N2");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_valRet.Text != "")
            {

                decimal valor = Convert.ToDecimal(txt_valRet.Text);
                valorImp = txt_valRet.Text;
                desc = "Valor retirado do caixa: ";

                valor = valor * (-1);
                this.tb_caixaBindingSource.DataSource = DataContextFactory.DataContext.tb_caixa;

                this.tb_caixaBindingSource.AddNew();
                this.saldoCorrente.data_caixa = DateTime.Now;
                this.saldoCorrente.valor_caixa = valor;
                this.saldoCorrente.usuario = DataContextFactory.usu.id_usuario;
                this.tb_caixaBindingSource.EndEdit();
                DataContextFactory.DataContext.SubmitChanges();
                atualizar();
                txt_valRet.Text = (0).ToString("N2");
                MessageBox.Show("Valor retirado com sucesso!");
                imprimir();
            }
            else
            {
                MessageBox.Show("Compo valor é obrigatorio");
            }
        }

        private void txt_valRet_Leave(object sender, EventArgs e)
        {
            if (txt_valRet.Text != "")
            {
                txt_valRet.Text = Convert.ToDecimal(txt_valRet.Text).ToString("N2");
            }
            else
            {
                txt_valRet.Text = (0).ToString("N2");
            }
        }

        // Codigo referente a impressão do cupom fiscal >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimir()
        {
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);
            
            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\Report3.rdlc";
            
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("data", Convert.ToString(data)));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("descricao", desc));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("valor", valorImp));

            Export(report);
            Print();
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

            printDoc.PrinterSettings.PrinterName = impressoraCorrente.nome_impressora;

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
