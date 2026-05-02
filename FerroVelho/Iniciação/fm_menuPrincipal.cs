using FerroVelho.Operaçoes;
using FerroVelho.Relatorios;
using FerroVelhoDAO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            if (DataContextFactory.usu.tb_tipoUsuario.id_tipoUsuario == 1)
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

            if (groupBox2.Visible)
            {
                CarregarProdutosCompra(false);
            }
        }
        
        private void comprarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            CarregarProdutosCompra(true);
            txt_quant.Focus();
            guia = 0;
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
            fm.Show();
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

        private void bt_calculadora_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void cabeçalhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_cabecario fm = new fm_cabecario();
            fm.ShowDialog();
        }

        private void financeiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_relLucro fm = new fm_relLucro();
            fm.ShowDialog();
        }

        private void movimentaçãoCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_movRecursos fm = new fm_movRecursos();
            fm.ShowDialog();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_cadCliente fm = new fm_cadCliente(0);
            fm.ShowDialog();
        }

        //private void adiantamentoToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    fm_adiantamentoOp fm = new fm_adiantamentoOp();
        //    fm.ShowDialog();
        //}
        
        private void pagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_adiantamentoOp fm = new fm_adiantamentoOp();
            fm.ShowDialog();
        }

        private void recebimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_recebmentoOp fm = new fm_recebmentoOp();
            fm.ShowDialog();
        }

        private void adiantamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_cadCliente fm = new fm_cadCliente(1);
            fm.ShowDialog();
        }

        private void outrasMovimentaçoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fm_outrasMovimentacoes fm = new fm_outrasMovimentacoes();
            fm.ShowDialog();
        }

        // Codigos referente a compra >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        int guia;

        private void CarregarProdutosCompra(bool limparSelecao)
        {
            string codigoAtual = limparSelecao ? string.Empty : txt_codProd.Text.Trim();
            var produtos = DataContextFactory.ListarProdutosApi();

            this.tb_produtosBindingSource.DataSource = produtos;

            cb_desProd.DataSource = tb_produtosBindingSource;
            cb_desProd.DisplayMember = "desc_prod";
            cb_desProd.ValueMember = "cod_prod";

            if (!string.IsNullOrWhiteSpace(codigoAtual)
                && produtos.Any(x => string.Equals(x.cod_prod, codigoAtual, StringComparison.Ordinal)))
            {
                cb_desProd.SelectedValue = codigoAtual;
                return;
            }

            cb_desProd.SelectedIndex = -1;
            if (!limparSelecao && !string.IsNullOrWhiteSpace(codigoAtual))
            {
                txt_codProd.Text = string.Empty;
                txt_valProd.Text = (0).ToString("N2");
                calcula();
            }
        }

        private void groupBox2_Layout(object sender, LayoutEventArgs e)
        {
            if (groupBox2.Visible && !(this.tb_produtosBindingSource.DataSource is List<tb_produtos>))
            {
                CarregarProdutosCompra(true);
                txt_quant.Focus();
                guia = 0;
            }
        }
               
        private void cb_desProd_Leave(object sender, EventArgs e)
        {
            if (this.produtoCorrente == null)
            {
                return;
            }

            txt_codProd.Text = this.produtoCorrente.cod_prod;
            txt_valProd.Text = (Convert.ToDecimal(this.produtoCorrente.val_prod)).ToString("N2");

            calcula();
            
        }
               
        private void txt_codProd_Leave(object sender, EventArgs e)
        {
            if (txt_codProd.Text != "" )
            {
                cb_desProd.SelectedValue = txt_codProd.Text.Trim();
                if (this.produtoCorrente == null)
                {
                    MessageBox.Show("Produto não encontrado!");
                    txt_valProd.Text = (0).ToString("N2");
                    return;
                }

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

                    txt_nNota.Text = compraCorrente.id_compra.ToString();
                    bt_finalCompra.Enabled = true;
                }
                else
                {
                    novoItem();
                }
                dg_compra.DataSource = this.tbitemcBindingSource;
                this.tbitemcBindingSource.DataSource = DataContextFactory.ListarItensCompraApi(this.compraCorrente.id_compra);
                calcula();
                
                upDateValor();

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

        private void upDateValor()
        {
            this.compraCorrente.valor_nota = Convert.ToDecimal(labelTotal.Text);
            this.compraCorrente.desconto_compra = Convert.ToDecimal(lb_desconto.Text);
            this.compraCorrente.subtot_compra = Convert.ToDecimal(lb_subtotal.Text);
            
            int? idCliente = null;
            if (clienteCorrente != null && clienteCorrente.id_cliente != 0)
            {
                this.compraCorrente.id_cliente = clienteCorrente.id_cliente;
                idCliente = clienteCorrente.id_cliente;
            }

            DataContextFactory.AtualizarCompraApi(
                compraCorrente.id_compra,
                compraCorrente.desconto_compra,
                compraCorrente.subtot_compra,
                compraCorrente.valor_nota,
                idCliente);
        }
        
        private void bt_novoItem_Click_1(object sender, EventArgs e)
        {            
            clicar();            
        }        

        private void bt_excluir_Click(object sender, EventArgs e)
        {
            try
            {
                string aki = itemCorrente.tb_produtos != null ? itemCorrente.tb_produtos.desc_prod : itemCorrente.cod_prod;

                if (MessageBox.Show("Realmente deseja excuir: " + aki, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataContextFactory.ExcluirItemCompraApi(itemCorrente.id_item);
                    this.tbitemcBindingSource.DataSource = DataContextFactory.ListarItensCompraApi(this.compraCorrente.id_compra);

                    MessageBox.Show("Produto excluido com sucesso!");
                    calcula();
                                        
                    upDateValor();

                    txt_quant.Focus();
                }

                if (dg_compra.RowCount == 0)
                {
                    DataContextFactory.ExcluirCompraApi(compraCorrente.id_compra);
                    this.tb_compraBindingSource.DataSource = null;

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
            tb_compra tb_Compra = DataContextFactory.CriarCompraApi(
                DateTime.Now,
                DataContextFactory.usu.id_usuario,
                0,
                0,
                Convert.ToDecimal(labelTotal.Text));

            this.tb_compraBindingSource.DataSource = tb_Compra;
                        
        }

        private void novoItem()
        {
            fm_recurcos fm = new fm_recurcos();
            decimal saldo = fm.calcular();

            if (saldo >= Convert.ToDecimal(txt_subTot.Text))
            {

                tb_itemc tb_Itemc = new tb_itemc();

                tb_Itemc.id_prod = produtoCorrente.id_prod;
                tb_Itemc.id_compra = compraCorrente.id_compra;
                tb_Itemc.quant_item = Convert.ToDecimal(txt_quant.Text);
                tb_Itemc.subTot_item = Convert.ToDecimal(txt_subTot.Text);
                tb_Itemc.valor_item = Convert.ToDecimal(txt_valProd.Text);

                DataContextFactory.InserirItemCompraApi(
                    produtoCorrente.cod_prod,
                    tb_Itemc.id_compra,
                    tb_Itemc.quant_item,
                    tb_Itemc.subTot_item,
                    tb_Itemc.valor_item);

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

        public tb_cliente clienteCorrente
        {
            get
            {
                return (tb_cliente)this.tb_clienteBindingSource.Current;
            }
        }

        private void calcula()
        {
            decimal quant, desconto;

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
                    MessageBox.Show("Digite um valor valido! ");
                    quant = 0;
                    txt_quant.Focus();
                }                
            }
            if (lb_desconto.Text == "")
            {
                desconto = 0;
            }
            else
            {
                try
                {
                    desconto = Convert.ToDecimal(lb_desconto.Text);
                }
                catch
                {
                    MessageBox.Show("Digite um valor valido!");
                    desconto = 0;
                    lb_desconto.Focus();
                }
            }

            decimal valor = Convert.ToDecimal(txt_valProd.Text);
            decimal sobTotItem = valor * quant;
            decimal subTotalNOta = 0;            
            decimal total;

            foreach (DataGridViewRow dg in dg_compra.Rows)
            {
                subTotalNOta = subTotalNOta + Convert.ToDecimal(dg.Cells[3].Value);                
            }
             
            total = subTotalNOta - desconto;

            txt_quant.Text = quant.ToString("N1");
            txt_subTot.Text = sobTotItem.ToString("N2");
            lb_subtotal.Text = subTotalNOta.ToString("N2");            
            labelTotal.Text = total.ToString("N2");
            lb_desconto.Text = desconto.ToString("N2");

            if (txt_nNota.Text != "")
            {
                verificaCredito();
            }            
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

            limpar();
            dg_compra.DataSource = null;
            dg_compra.Refresh();
            tb_cliente cliente = new tb_cliente();
            tb_clienteBindingSource.DataSource = cliente;
            chek_addCredito.Checked = false;
            chek_addCredito.Enabled = false;
            txt_quant.Focus();
        }

        private void finalizarCompra()
        {
            if (Convert.ToDecimal(labelTotal.Text) < 0)
            {
                MessageBox.Show("Valor do desconto não pode ser maior que o valor dos produtos!");
                lb_desconto.Focus();
            }
            else
            {
                guia = 1;
                upDateValor();
                finalizar();
                guia = 0;
            }            
        }

        private void bt_finalCompra_Click(object sender, EventArgs e)
        {
            finalizarCompra();
        }

        private void limpar()
        {

            txt_codProd.Text = "";
            txt_nNota.Text = "";
            txt_valProd.Text = (0).ToString("N2");
            txt_quant.Text = "";
            txt_subTot.Text = (0).ToString("N2");            
            labelTotal.Text = (0).ToString("N2");
            lb_desconto.Text = (0).ToString("N2");
            lb_subtotal.Text = (0).ToString("N2");
            saldo = 0;
            cb_desProd.SelectedIndex = -1;
            bt_finalCompra.Enabled = false;
            txt_nome.Text = "";
            txt_telefone.Text = "";
            txt_cpf.Text = "";
            checkBox2.Checked = false;            

        }

        decimal saldo;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                fm_cadCliente fm = new fm_cadCliente(0);
                tb_cliente cliente = new tb_cliente();
                fm.ShowDialog();
                cliente = fm.Propriedade;

                if (cliente != null)
                {
                    txt_nome.Text = cliente.nome_cliente;
                    txt_telefone.Text = cliente.tel_cliente;
                    txt_cpf.Text = cliente.cpf_cliente;
                    txt_idCliente.Text = cliente.id_cliente.ToString();

                    tb_clienteBindingSource.DataSource = cliente;

                    lb_nome.Visible = true;
                    lb_telefone.Visible = true;
                    lb_cpf.Visible = true;
                    txt_nome.Visible = true;
                    txt_telefone.Visible = true;
                    txt_cpf.Visible = true;
                    chek_addCredito.Enabled = true;

                    Dao dao = new Dao();

                    saldo = dao.valorDevedor(clienteCorrente.id_cliente);
                    lb_desconto.Text = saldo.ToString("N2");
                    calcula();
                    if (txt_nNota.Text != "")
                    {
                        upDateValor();
                    }
                }
                else
                {
                    checkBox2.Checked = false;
                }
                                
            }
            else
            {
                if (guia != 1)
                {
                    tb_cliente cliente = new tb_cliente();
                    tb_clienteBindingSource.DataSource = cliente;

                    chek_addCredito.Enabled = false;

                    lb_nome.Visible = false;
                    lb_telefone.Visible = false;
                    lb_cpf.Visible = false;
                    txt_nome.Visible = false;
                    txt_telefone.Visible = false;
                    txt_cpf.Visible = false;
                    txt_nome.Text = "";
                    txt_telefone.Text = "";
                    txt_cpf.Text = "";
                    txt_idCliente.Text = "";
                    lb_desconto.Text = (0).ToString("N2");
                    calcula();
                    if (txt_nNota.Text != "")
                    {
                        upDateValor();
                    }
                }
                
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
            if (e.KeyChar == 13)
            {
                txt_valProd.Focus();
                e.Handled = true;
            }            
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
            if (e.KeyData == Keys.End)
            {
                if (bt_finalCompra.Enabled == false)
                {
                    MessageBox.Show("Nenhum produto adicionado!");
                }
                else
                {
                    finalizarCompra();
                }
                
            }
        }
        private void txt_codProd_KeyDown(object sender, KeyEventArgs e)
            {
            if (e.KeyData == Keys.End)
            {
                if (bt_finalCompra.Enabled == false)
                {
                    MessageBox.Show("Nenhum produto adicionado!");
                }
                else
                {
                    finalizarCompra();
                }

            }
        }

        private void txt_valProd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.End)
            {
                if (bt_finalCompra.Enabled == false)
                {
                    MessageBox.Show("Nenhum produto adicionado!");
                }
                else
                {
                    finalizarCompra();
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
        
        private void lb_desconto_Leave(object sender, EventArgs e)
        {
            if (clienteCorrente.id_cliente != 0)
            {                
                if (saldo >= Convert.ToDecimal(lb_desconto.Text))
                {
                    calcula();
                }
                else
                {
                    MessageBox.Show("O cliente " + clienteCorrente.nome_cliente + " so possui " + saldo.ToString("C2") + " de saldo devedor!");
                    lb_desconto.Text = saldo.ToString("N2");
                }
            }
            else
            {
                calcula();
            }
            
        }

        private void chek_addCredito_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Convert.ToDecimal(labelTotal.Text) >= 0 )
            {
                if (Convert.ToDecimal(lb_desconto.Text) < saldo)
                {
                    MessageBox.Show("Não permitido credito pois o cliente " + clienteCorrente.nome_cliente + " ainda possui saldo devedor!");
                    lb_desconto.Text = saldo.ToString("N2");
                    chek_addCredito.Checked = false;
                    calcula();
                }
                else
                {
                    calcula();
                    verificaCredito();
                }
                
            }
            else
            {
                chek_addCredito.Checked = false;
                calcula();
                MessageBox.Show("Não e permitido adicionar cretito menor a R$ 0,00 ");
                lb_desconto.Focus();
            }
            
        }

        private void verificaCredito()
        {
            if (chek_addCredito.Checked == true)
            {
                labelTotal.Text = 0.ToString("N2");
                upDateValor();
            }
            else
            {
                if (guia != 1)
                {                    
                    upDateValor();
                }
            }
        }

        // Codigo referente a impressão do cupom fiscal >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        public void imprimirNF(int nf)
        {
            var imp = DataContextFactory.BuscarImpressoraApi(1);
            if (imp != null)
            {
                this.tb_impressoraBindingSource.DataSource = new List<tb_impressora> { imp };
            }
                       
            LocalReport report = new LocalReport();
            report.ReportPath = @"..\..\Report2.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData1(nf)));
            report.DataSources.Add(new ReportDataSource("DataSet2", LoadSalesData2(nf)));
            report.DataSources.Add(new ReportDataSource("DataSet3", LoadSalesData3(nf)));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            report.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
                                   
            Export(report);
            Print(guia);
        }

        private DataTable LoadSalesData1(int nf)
        {
            return DataContextFactory.CarregarRelatorioCompraItensApi(nf);
        }

        private DataTable LoadSalesData2(int nf)
        {
            return DataContextFactory.CarregarRelatorioCompraCabecalhoApi(nf);
        }

        private DataTable LoadSalesData3(int nf)
        {
            return DataContextFactory.CarregarRelatorioCompraClienteApi(nf);
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

        public void Print(int guia)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            
            PrintDocument printDoc = new PrintDocument();

            if (guia == 1)
            {
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
            else
            {
                PrintDialog printDlg = new PrintDialog();
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

