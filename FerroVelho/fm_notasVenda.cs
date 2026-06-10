using FerroVelhoDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho
{
    public partial class fm_notasVenda : Form
    {
        public fm_notasVenda()
        {
            InitializeComponent();
        }

        private void fm_notasVenda_Load(object sender, EventArgs e)
        {
            this.tb_vendaBindingSource.DataSource = DataContextFactory.DataContext.tb_venda;
            dt_fim.Value = DateTime.Now;
            dt_inicio.Value = DateTime.Now;
        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            DateTime dataI = dt_inicio.Value;
            DateTime dataF = dt_fim.Value;

            this.tb_vendaBindingSource.DataSource = DataContextFactory.DataContext.tb_venda.Where(x => x.data_venda >= dataI && x.data_venda <= dataF);

        }

        private void tb_vendaBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (vendaCorrente != null)
            {
                this.tb_itemvBindingSource.DataSource = DataContextFactory.DataContext.tb_itemv.Where(x => x.id_venda == vendaCorrente.id_venda);
            }
                           
        }

        public tb_venda vendaCorrente
        {
            get
            {
                return (tb_venda)this.tb_vendaBindingSource.Current;
            }
        }

        private void tb_itemvDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1)
            {
                e.Value = ((tb_produtos)e.Value).desc_prod;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.tb_vendaBindingSource.DataSource = DataContextFactory.DataContext.tb_venda.Where(x => x.id_venda == Convert.ToInt32(txt_notaFiscal.Text));
                if (vendaCorrente == null)
                {
                    MessageBox.Show("Nota fiscal: " + txt_notaFiscal.Text + " inexistente");
                    this.tb_itemvBindingSource.Clear();
                }                
            }
            catch
            {
                this.tb_vendaBindingSource.DataSource = DataContextFactory.DataContext.tb_venda;
            }
            
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            this.tb_vendaBindingSource.RemoveCurrent();
            DataContextFactory.DataContext.SubmitChanges();
            MessageBox.Show("Excluido com sucesso!");
            
        }

        private void tb_vendaDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            fm_vender fm = new fm_vender();
            fm.imprimirNF(vendaCorrente.id_venda);
        }
    }
}
