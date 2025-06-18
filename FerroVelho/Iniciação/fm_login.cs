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
                var cont = DataContextFactory.DataContext.tb_usuario.Count(x => x.nome_usuario == txt_nome.Text && x.senha_usuario == txt_senha.Text && x.ativo);

                if (cont > 0)
                {
                    this.tb_usuarioBindingSource.DataSource = DataContextFactory.DataContext.tb_usuario.Where(x => x.nome_usuario == txt_nome.Text && x.senha_usuario == txt_senha.Text && x.ativo);
                    logar = true;

                    DataContextFactory.usu = usuarioCorrente;

                    this.Dispose();

                }
                else
                {
                    MessageBox.Show("Usuario ou senha invalido");
                }
            }
            catch
            {
                MessageBox.Show("String de conexão: User inexistente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fm_configuracao fmc = new fm_configuracao();
                fmc.ShowDialog();
            }

            try
            {
                var cont = DataContextFactory.Conectar();
            }
            catch
            {
                MessageBox.Show("String de conexão: Impressao inexistente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
