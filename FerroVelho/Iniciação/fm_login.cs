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
    public partial class fm_login : Form
    {
        public bool logar = false;
        public int use;
        

        public fm_login()
        {
            InitializeComponent();
        }           

        private tb_usuario usuarioCorrente
        {
            get
            {
                return (tb_usuario)this.tb_usuarioBindingSource.Current;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clicar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txt_senha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                clicar();
            }
        }

        private void clicar()
        {
            try
            {
                DataContextFactory.TestarConexaoApi();
                var usuarioApi = DataContextFactory.ValidarLoginApi(txt_nome.Text, txt_senha.Text);
                if (usuarioApi != null)
                {
                    DataContextFactory.DefinirUsuarioAutenticado(usuarioApi);
                    logar = true;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Usuario ou senha invalido");
                }
            }
            catch
            {
                MessageBox.Show("Não foi possível acessar a API de usuários.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fm_configuracao fmc = new fm_configuracao();
                fmc.ShowDialog();
            }
        }

        private void fm_login_Load(object sender, EventArgs e)
        {
            DataContextFactory.FU_lerConfiguracao();
            DataContextFactory.FU_lerCabecario();
        }
    }
}

