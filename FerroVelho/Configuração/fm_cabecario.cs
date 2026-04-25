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
    public partial class fm_cabecario : Form
    {
        public fm_cabecario()
        {
            InitializeComponent();
        }

        private void fm_cabecario_Load(object sender, EventArgs e)
        {
            load();
        }

        private void bt_salvar_Click(object sender, EventArgs e)
        {
            DataContextFactory.GravarCabecario(txt_empressa.Text, txt_telefone.Text, txt_endereco.Text);
            txt_empressa.Text = "";
            txt_endereco.Text = "";
            txt_telefone.Text = "";
            txt_empressa.Enabled = false;
            txt_endereco.Enabled = false;
            txt_telefone.Enabled = false;
            bt_alterar.Visible = true;
            bt_salvar.Visible = false;
            load();
        }

        private void bt_alterar_Click(object sender, EventArgs e)
        {
            txt_empressa.Enabled = true;
            txt_endereco.Enabled = true;
            txt_telefone.Enabled = true;
            bt_alterar.Visible = false;
            bt_salvar.Visible = true;
        }

        private void load()
        {
            DataContextFactory.FU_lerCabecario();

            txt_empressa.Text = DataContextFactory.nome;
            txt_telefone.Text = DataContextFactory.tel;
            txt_endereco.Text = DataContextFactory.endereco;
        }
    }
}
