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
    public partial class fm_configuracao : Form
    {
        public fm_configuracao()
        {
            InitializeComponent();
        }

        private void fm_configuracao_Load(object sender, EventArgs e)
        {
            load();
        }

        private void bt_salvar_Click(object sender, EventArgs e)
        {
            DataContextFactory.GravarConecxao(txt_datauser.Text, txt_datauImpressao.Text);
            txt_datauser.Text = "";
            txt_datauImpressao.Text = "";
            load();
        }

        private void bt_alterar_Click(object sender, EventArgs e)
        {
            txt_datauser.Enabled = true;
            txt_datauImpressao.Enabled = true;
            bt_alterar.Visible = false;
            bt_salvar.Visible = true;
        }

        private void load()
        {
            DataContextFactory.FU_lerConfiguracao();
            txt_datauser.Text = DataContextFactory.conexaoUser;
            txt_datauImpressao.Text = DataContextFactory.conexaoImp;
            txt_datauser.Enabled = false;
            txt_datauImpressao.Enabled = false;
            bt_alterar.Visible = true;
            bt_salvar.Visible = false;
        }
    }
}
