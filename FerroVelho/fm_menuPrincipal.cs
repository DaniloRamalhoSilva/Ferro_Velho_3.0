using FerroVelho.Relatorios;
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho
{
    public partial class fm_menulPrincipal : Form
    {
        
        public int usu;

        public fm_menulPrincipal()
        {
            InitializeComponent();           
        }

        private void fm_menulPrincipal_Load(object sender, EventArgs e)
        {
            txt_operador.Text = DataContextFactory.usu.nome_usuario;
            if (DataContextFactory.usu.tb_tipoUsuario.id_tipoUsuario == 2)
            {
                operacaoToolStripMenuItem.Visible = true;
                cadastroToolStripMenuItem.Visible = true;
                cadastroToolStripMenuItem.Visible = true;
                consultaToolStripMenuItem.Visible = true;
                relatorioToolStripMenuItem.Visible = true;
                comfiguraçoesToolStripMenuItem.Visible = true;
            }
            else
            {
                cadastroToolStripMenuItem.Visible = false;
                cadastroToolStripMenuItem.Visible = false;
                consultaToolStripMenuItem.Visible = false;
                relatorioToolStripMenuItem.Visible = false;
                comfiguraçoesToolStripMenuItem.Visible = false;
                venderToolStripMenuItem.Visible = false;
                outrasEntradasToolStripMenuItem.Visible = false;
                outrasSaidasToolStripMenuItem.Visible = false;
                addRecurçosToolStripMenuItem.Visible = false;
            }
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_cadastroProduto cadastroProduto = new fm_cadastroProduto();
            cadastroProduto.ShowDialog();
        }
        
        private void comprarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;           
        }

        private void impressorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
          fm_cofigImpressaora cofigImpressaora = new fm_cofigImpressaora();
          cofigImpressaora.ShowDialog();
        }

        private void venderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_nNota.Text == "")
            {
                groupBox2.Visible = false;
                fm_vender vender = new fm_vender();
                vender.ShowDialog();
            }
            else
            {
                MessageBox.Show("Finalize a compra em andamento!");
            }
        }

        private void estoqueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fm_estoque fm_Estoque = new fm_estoque();
            fm_Estoque.ShowDialog();
        }

        private void outrasSaidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_outrasSaidas fm = new fm_outrasSaidas();
            fm.ShowDialog();
        }

        private void outrasEntradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_outrasEntradas fm = new fm_outrasEntradas();
            fm.ShowDialog();
        }            

        private void addRecurçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_recurcos fm = new fm_recurcos();
            fm.ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_cadUsuario fm = new fm_cadUsuario();
            fm.ShowDialog();
        }

        private void saldoCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_relFluxoCaixa fm = new fm_relFluxoCaixa();
            fm.ShowDialog();
        }

        private void compraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fm_notasCompra fm = new fm_notasCompra();
            fm.ShowDialog();
        }

        private void vendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fm_notasVenda fm = new fm_notasVenda();
            fm.ShowDialog();
        }

        private void vendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_relVenda fm = new fm_relVenda();
            fm.ShowDialog();
        }

        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_relCompra fm = new fm_relCompra();
            fm.ShowDialog();
        }

        private void estoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_repEstoque fm = new fm_repEstoque();
            fm.ShowDialog();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_relProduto fm = new fm_relProduto();
            fm.ShowDialog();
        }

        private void financeiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_relLucro fm = new fm_relLucro();
            fm.ShowDialog();
        }

        private void bt_calculadora_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void cabeçalhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_cabecario fm = new fm_cabecario();
            fm.ShowDialog();
        }


        // Codigos referente a compra >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        

        private void groupBox2_Layout(object sender, LayoutEventArgs e)
        {
            this.tb_produtosBindingSource.DataSource = DataContextFactory.DataContext.tb_produtos;            
            cb_desProd.DataSource = tb_produtosBindingSource;
            cb_desProd.DisplayMember = "desc_prod";
            cb_desProd.ValueMember = "id_prod";
            cb_desProd.SelectedIndex = -1;
            txt_quant.Focus();            
        }
               
        private void cb_desProd_Leave(object sender, EventArgs e)
        {           
            txt_codProd.Text = Convert.ToString(this.produtoCorrente.id_prod);
            txt_valProd.Text = (Convert.ToDecimal(this.produtoCorrente.val_prod)).ToString("N2");

            calcula();
        }
               
        private void txt_codProd_Leave(object sender, EventArgs e)
        {
            if (txt_codProd.Text != "" )
            {
                cb_desProd.SelectedValue = Convert.ToInt32(txt_codProd.Text);
                txt_valProd.Text = (Convert.ToDecimal(this.produtoCorrente.val_prod)).ToString("N2");

                calcula();
            }
            else
            {
                cb_desProd.SelectedIndex = -1;
                txt_valProd.Text = (0).ToString("N2");
                calcula();
            }
            
        }

        private void txt_quant_Leave(object sender, EventArgs e)
        {            
            calcula();
        }

        private void txt_valProd_Leave(object sender, EventArgs e)
        {
            try
            {
                txt_valProd.Text = Convert.ToDecimal(txt_valProd.Text).ToString("N2");
                calcula();
            }
            catch
            {
                MessageBox.Show("Digite um valor valido!");
                txt_valProd.Text = (0).ToString("N2");
            }
            
            
        }

        private void bt_fechar_Click(object sender, EventArgs e)
        {
            if(txt_nNota.Text == "")
            {
                limpar();
                groupBox2.Visible = false;
            }
            else
            {
                MessageBox.Show("Imposivel sair compra em andamento!");
            }
            
        }
        private void clicar()
        {
            if (txt_quant.Text != "" && txt_valProd.Text != "" && cb_desProd.Text != "" && txt_codProd.Text != "")
            {
                if (txt_nNota.Text == "")
                {
                    novaNota();
                    novoItem();
                }
                else
                {
                    novoItem();
                }
                dg_compra.DataSource = this.tbitemcBindingSource;
                this.tbitemcBindingSource.DataSource = DataContextFactory.DataContext.tb_itemc.Where(x => x.id_compra == this.compraCorrente.id_compra);
                calcula();

                this.tb_compraBindingSource.EndEdit();
                this.compraCorrente.valor_nota = Convert.ToDecimal(labelTotal.Text);
                DataContextFactory.DataContext.SubmitChanges();                

                txt_codProd.Text = "";
                txt_valProd.Text = (0).ToString("N2");
                txt_quant.Text = "";
                txt_subTot.Text = (0).ToString("N2");
                cb_desProd.SelectedIndex = -1;
                txt_quant.Focus();

            }
            else
            {
                MessageBox.Show("Todos os campos são obrigatorios!");
                txt_quant.Focus();
            }
        }
        
        private void bt_novoItem_Click_1(object sender, EventArgs e)
        {
            
            clicar();           
        }        

        private void bt_excluir_Click(object sender, EventArgs e)
        {
            try
            {
                string aki = ((tb_produtos)dg_compra[0, dg_compra.CurrentRow.Index].Value).desc_prod;
                if (MessageBox.Show("Realmente deseja excuir: " + aki, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.tbitemcBindingSource.RemoveCurrent();
                    DataContextFactory.DataContext.SubmitChanges();
                    MessageBox.Show("Produto excluido com sucesso!");
                    calcula();
                    txt_quant.Focus();
                }

                if (dg_compra.RowCount == 0)
                {
                    this.tb_compraBindingSource.RemoveCurrent();
                    DataContextFactory.DataContext.SubmitChanges();
                    limpar();
                    txt_quant.Focus();
                    bt_finalCompra.Enabled = false;                    
                }
            }
            catch
            {
                MessageBox.Show("Selecione um item valido!");
            }
        }

        private void novaNota()
        {
            this.tb_compraBindingSource.DataSource = DataContextFactory.DataContext.tb_compra;

            this.tb_compraBindingSource.AddNew(); 
            this.compraCorrente.data_compra = DateTime.Now;            
            this.compraCorrente.usuario = DataContextFactory.usu.id_usuario;
            this.tb_compraBindingSource.EndEdit(); 
            DataContextFactory.DataContext.SubmitChanges();

            txt_nNota.Text = Convert.ToString(compraCorrente.id_compra);
            bt_finalCompra.Enabled = true;
            
        }

        private void novoItem()
        {
            fm_recurcos fm = new fm_recurcos();
            decimal saldo = fm.calcular();

            if (saldo >= Convert.ToDecimal(txt_subTot.Text))
            {
                this.tbitemcBindingSource.DataSource = DataContextFactory.DataContext.tb_itemc;

                this.tbitemcBindingSource.AddNew();

                this.itemCorrente.id_prod = this.produtoCorrente.id_prod;
                this.itemCorrente.id_compra = this.compraCorrente.id_compra;
                this.itemCorrente.quant_item = Convert.ToDecimal(txt_quant.Text);
                this.itemCorrente.subTot_item = Convert.ToDecimal(txt_subTot.Text);
                this.itemCorrente.valor_item = Convert.ToDecimal(txt_valProd.Text);
                
                this.tbitemcBindingSource.EndEdit();
                DataContextFactory.DataContext.SubmitChanges();
                
            }
            else
            {
                MessageBox.Show("Saldo insuficiente para essa compra! Necessário almentar valor em caixa");
            }
        }
                        
        public tb_compra compraCorrente
        {
            get
            {
                return (tb_compra)this.tb_compraBindingSource.Current;
            }
        }

        public tb_itemc itemCorrente
        {
            get
            {
                return (tb_itemc)this.tbitemcBindingSource.Current;
            }
        }       
               
        public tb_produtos produtoCorrente
        {
            get
            {
                return (tb_produtos)this.tb_produtosBindingSource.Current;
            }
        }
               
        private void calcula()
        {
            decimal quant;

            if (txt_quant.Text == "")
            {
                quant = 0;
            }
            else
            {
                try
                {
                    quant = Convert.ToDecimal(txt_quant.Text);
                }
                catch
                {
                    MessageBox.Show("Digite um valor valido!");
                    quant = 0;
                }
                
            }
            txt_quant.Text = quant.ToString("N1");
            decimal valor = Convert.ToDecimal(txt_valProd.Text);
            decimal sobTot = valor * quant;

            txt_subTot.Text = sobTot.ToString("N2");
            decimal total = 0;

            foreach (DataGridViewRow dg in dg_compra.Rows)
            {
                total = total + Convert.ToDecimal(dg.Cells[3].Value);                
            }
            labelTotal.Text = total.ToString("N2");
        }
 
        private void dg_compra_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.Value != null && e.ColumnIndex == 0)
            {
                e.Value = ((tb_produtos)e.Value).desc_prod;
            }

        }
        
        private void finalizar()
        {            
            if (checkBox1.Checked == true)
            {
                imprimirNF(compraCorrente.id_compra);
            }

            imprimirNF(compraCorrente.id_compra);

            limpar();
            dg_compra.DataSource = null;
            dg_compra.Refresh();           
            txt_quant.Focus();
        }

        private void bt_finalCompra_Click(object sender, EventArgs e)
        {            
            finalizar();
        }

        private void limpar()
        {
            txt_codProd.Text = "";
            txt_nNota.Text = "";
            txt_valProd.Text = (0).ToString("N2");
            txt_quant.Text = "";
            txt_subTot.Text = (0).ToString("N2");            
            labelTotal.Text = (0).ToString("N2");
            cb_desProd.SelectedIndex = -1;
            bt_finalCompra.Enabled = false;
            txt_nome.Text = "";
            txt_telefone.Text = "";
            txt_cpf.Text = "";

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                lb_nome.Visible = true;
                lb_telefone.Visible = true;
                lb_cpf.Visible = true;
                txt_nome.Visible = true;
                txt_telefone.Visible = true;
                txt_cpf.Visible = true;
            }
            else
            {
                lb_nome.Visible = false;
                lb_telefone.Visible = false;
                lb_cpf.Visible = false;
                txt_nome.Visible = false;
                txt_telefone.Visible = false;
                txt_cpf.Visible = false;
                txt_nome.Text = "";
                txt_telefone.Text = "";
                txt_cpf.Text = "";
            }
        }

        private void txt_valProd_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                calcula();
                clicar();
                txt_quant.Text = "";
            }
            e.Handled = true;
        }               
        
        private void txt_quant_KeyPress_1(object sender, KeyPressEventArgs e)
        {            
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                txt_codProd.Focus();
            }            
            e.Handled = true;
        }

        private void txt_codProd_KeyPress_1(object sender, KeyPressEventArgs e)
        {            
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                txt_valProd.Focus();
            }            
            e.Handled = true;
        }

        private void fm_menulPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txt_nNota.Text == "")
            {
                limpar();               
            }
            else
            {
                MessageBox.Show("Imposivel sair compra em andamento!");
                e.Cancel = true;
            }
        }

        private void txt_quant_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyData == Keys.End || e.KeyData == Keys.PageDown || e.KeyData == Keys.PageUp)
            {
                if (bt_finalCompra.Enabled == false)
                {
                    MessageBox.Show("Nenhum produto adicionado!");
                }
                else
                {
                    finalizar();
                }
                
            }
        }
        private void txt_codProd_KeyDown(object sender, KeyEventArgs e)
            {
            if (e.KeyData == Keys.End || e.KeyData == Keys.PageDown || e.KeyData == Keys.PageUp)
            {
                if (bt_finalCompra.Enabled == false)
                {
                    MessageBox.Show("Nenhum produto adicionado!");
                }
                else
                {
                    finalizar();
                }

            }
        }

        private void txt_valProd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.End || e.KeyData == Keys.PageDown || e.KeyData == Keys.PageUp)
            {
                if (bt_finalCompra.Enabled == false)
                {
                    MessageBox.Show("Nenhum produto adicionado!");
                }
                else
                {
                    finalizar();
                }

            }
        }

        private void txt_quant_Click(object sender, EventArgs e)
        {
            select();
        }

        private void select()
        {
            txt_quant.SelectAll();
        }

        // Codigo referente a impressão do cupom fiscal >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimirNF(int nf)
        {
            this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);
                       
            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\Report2.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData(nf)));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            if (txt_nome.Text != "")
            {
                report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("cliente", "Cliente: " + txt_nome.Text));
            }
            if (txt_telefone.Text != "")
            {
                report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("telefone", "Tel.: " + txt_telefone.Text));
            }
            if (txt_cpf.Text != "")
            {
                report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("cpf", "CPF: " + txt_cpf.Text));
            }           
            Export(report);
            Print();
        }

        private DataTable LoadSalesData(int nf)
        {
            string comando = "SELECT tb_itemc.quant_item, tb_itemc.subTot_item, tb_itemc.valor_item, tb_produtos.desc_prod, tb_compra.data_compra, tb_itemc.id_prod, tb_itemc.id_compra, tb_usuario.nome_usuario, tb_compra.usuario FROM tb_itemc INNER JOIN tb_compra ON tb_itemc.id_compra = tb_compra.id_compra INNER JOIN tb_produtos ON tb_itemc.id_prod = tb_produtos.id_prod INNER JOIN tb_usuario ON tb_compra.usuario = tb_usuario.id_usuario WHERE tb_itemc.id_compra = " + nf;
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
