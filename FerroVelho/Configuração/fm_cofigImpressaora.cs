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
    public partial class fm_cofigImpressaora : Form
    {
        public fm_cofigImpressaora()
        {
            InitializeComponent();
        }

        private void fm_cofigImpressaora_Load(object sender, EventArgs e)
        {
            caregarImpressora();
            cb_ImpCupomFiscal.Items.Clear();
            cb_ImpRelatorio.Items.Clear();

            foreach(var impressora in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cb_ImpCupomFiscal.Items.Add(impressora);
                cb_ImpRelatorio.Items.Add(impressora);                
            }

        }

        private void caregarImpressora()
        {
            try
            {
                this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);
                lb_impCupomFiscal.Text = this.impressoraCorrente.nome_impressora;
            }
            catch
            {
                lb_impCupomFiscal.Text = "Nenhuma Selecionada";
            }

            try
            {
                this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 2);
                lb_impRelatorio.Text = this.impressoraCorrente.nome_impressora;
            }
            catch
            {
                lb_impRelatorio.Text = "Nenhuma Selecionada";
            }            

        }

        public tb_impressora impressoraCorrente
        {
            get
            {
                return (tb_impressora)this.tb_impressoraBindingSource.Current;
            }
        }

        private void btn_impCupomFiscal_Click(object sender, EventArgs e)
        {
            if (lb_impCupomFiscal.Text == "Nenhuma Selecionada")
            {
                this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora;
                this.tb_impressoraBindingSource.AddNew();
            }
            else
            {
                this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 1);

            }
            this.impressoraCorrente.nome_impressora = cb_ImpCupomFiscal.Text;

            this.tb_impressoraBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();
            MessageBox.Show("Alterado com sucesso!");
            caregarImpressora();
        }

        private void btn_impRelatorio_Click(object sender, EventArgs e)
        {
            if (lb_impRelatorio.Text == "Nenhuma Selecionada")
            {
                this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora;
                this.tb_impressoraBindingSource.AddNew();
            }
            else
            {
                this.tb_impressoraBindingSource.DataSource = DataContextFactory.DataContext.tb_impressora.Where(x => x.id_impressora == 2);

            }
            this.impressoraCorrente.nome_impressora = cb_ImpRelatorio.Text;

            this.tb_impressoraBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();
            MessageBox.Show("Alterado com sucesso!");
            caregarImpressora();
        }
    }
}
