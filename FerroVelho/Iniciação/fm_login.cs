using FerroVelhoDAO;
using System;
using System.Windows.Forms;


namespace FerroVelho
{
    public partial class fm_login : Form
    {
        #region [ VARIAVEIS GLOBAL ]

        // Cria a conexão com a DAL
        private static DataContextFactory _DAO = new DataContextFactory();

        #endregion

        #region [ CONSTRUTOR ]

        public fm_login()
        {
            InitializeComponent();
        }

        #endregion

        #region [ MÉTODOS PRIVADOS ]

        private void Logar()
        {
            ValidacaoEnum validacaoEnum = _DAO.ValidarLoginUsuario(txt_nome.Text.Trim(), txt_senha.Text.Trim());

            switch (validacaoEnum)
            {
                case ValidacaoEnum.SUCESSO:
                    break;
                case ValidacaoEnum.USUARIO_SENHA_INVALIDO:
                    MessageBox.Show(Comum.ObterDescricaoEnum(ValidacaoEnum.USUARIO_SENHA_INVALIDO), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ValidacaoEnum.SEM_CONEXA_BANCO_DADOS:
                    MessageBox.Show(Comum.ObterDescricaoEnum(ValidacaoEnum.SEM_CONEXA_BANCO_DADOS), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fm_configuracao fmc = new fm_configuracao();
                    fmc.ShowDialog();
                    break;
                case ValidacaoEnum.ERRO:
                    MessageBox.Show(Comum.ObterDescricaoEnum(ValidacaoEnum.ERRO), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show(Comum.ObterDescricaoEnum(ValidacaoEnum.ERRO_DEFUL), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

            }
        }

        #endregion

        #region  [EVENTOS ]

        private void fm_login_Load(object sender, EventArgs e)
        {
            DataContextFactory.FU_lerConfiguracao();
            DataContextFactory.FU_lerCabecario();
        }

        private void btn_logar_Click(object sender, EventArgs e)
        {
            Logar();
        }
                
        private void txt_senha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Logar();
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion
    }
}
