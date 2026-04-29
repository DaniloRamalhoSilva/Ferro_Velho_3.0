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
    public partial class fm_cadUsuario : Form
    {
        private bool mostrarInativos = false;
        public fm_cadUsuario()
        {
            InitializeComponent();
        }

        private void fm_cadUsuario_Load(object sender, EventArgs e)
        {
            this.tb_tipoUsuarioBindingSource.DataSource = DataContextFactory.ListarTiposUsuarioApi();

            CarregaUsuarios();
            if (this.usuarioCorrente != null)
            {
                clik();
            }                        
        }

        private void CarregaUsuarios()
        {
            this.tb_usuarioBindingSource.DataSource = DataContextFactory.ListarUsuariosApi(mostrarInativos);
        }

        private void bt_salvar_Click(object sender, EventArgs e)
        {
            if (txt_nome.Text == "" || txt_senha.Text == "" || txt_confSenha.Text == "" || cb_tipo.Text == "")
            {
                MessageBox.Show("Todos os campos são obrigatorios");
            }
            else
            {
                
                if (txt_senha.Text != txt_confSenha.Text)
                {
                    MessageBox.Show("Senhas diferentes, verifique!");
                    txt_confSenha.Focus();
                }
                else
                {
                    if (lb_idUsuario.Text == "")
                    {
                        if (DataContextFactory.ExisteUsuarioApi(txt_nome.Text, null))
                        {
                            MessageBox.Show("Usuario " + txt_nome.Text + " ja exite, tente novamente!");
                            
                        }
                        else
                        {
                            DataContextFactory.CriarUsuarioApi(txt_nome.Text, txt_senha.Text, tipoCorrente.id_tipoUsuario);
                            CarregaUsuarios();
                            MessageBox.Show("Usuario cadastrado com sucesso!");
                        }
                        
                    }
                    else
                    {
                        if (DataContextFactory.ExisteUsuarioApi(txt_nome.Text, this.usuarioCorrente.id_usuario))
                        {
                            MessageBox.Show("Usuario " + txt_nome.Text + " ja exite, tente novamente!");
                        }
                        else
                        {
                            DataContextFactory.AtualizarUsuarioApi(
                                this.usuarioCorrente.id_usuario,
                                txt_nome.Text,
                                txt_senha.Text,
                                tipoCorrente.id_tipoUsuario);
                            CarregaUsuarios();
                            MessageBox.Show("Usuario alterado com sucesso!");
                        }

                    }

                    txt_nome.Enabled = false;
                    txt_senha.Enabled = false;
                    txt_confSenha.Enabled = false;
                    cb_tipo.Enabled = false;

                    bt_salvar.Visible = false;
                    btn_cancelar.Visible = false;
                    btn_alterar.Visible = true;                    
                    btn_excluir.Visible = true;
                    btn_novo.Visible = true;
                    DataGridView1.Enabled = true;
                    clik();

                }
            }
        }
        
        public tb_usuario usuarioCorrente
        {
            get
            {
                return (tb_usuario)this.tb_usuarioBindingSource.Current;
            }
        }

        public tb_tipoUsuario tipoCorrente
        {
            get
            {
                return (tb_tipoUsuario)this.tb_tipoUsuarioBindingSource.Current;
            }
        }

        
        private void clik()
        {
            txt_nome.Text = this.usuarioCorrente.nome_usuario;
            txt_senha.Text =this.usuarioCorrente.senha_usuario;
            txt_confSenha.Text = "";
            cb_tipo.SelectedValue = this.usuarioCorrente.permi_usuario;
            btn_reativar.Visible = !this.usuarioCorrente.ativo;
            btn_alterar.Enabled = this.usuarioCorrente.ativo;
            btn_excluir.Enabled = this.usuarioCorrente.ativo;
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            lb_idUsuario.Text = "";
            txt_nome.Text = "";
            txt_senha.Text = "";
            txt_confSenha.Text = "";
            cb_tipo.SelectedIndex = -1;

            txt_nome.Enabled = true;
            txt_senha.Enabled = true;
            txt_confSenha.Enabled = true;
            cb_tipo.Enabled = true;
            
            txt_nome.Focus();

            btn_cancelar.Visible = true;
            bt_salvar.Visible = true;
            btn_alterar.Visible = false;            
            btn_excluir.Visible = false;
            btn_novo.Visible = false;

            DataGridView1.Enabled = false;
            btn_reativar.Visible = false;
        }

        private void DataGridView1_Click_1(object sender, EventArgs e)
        {
            if (this.usuarioCorrente != null)
            {
                clik();
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            if (this.usuarioCorrente != null)
            {
                clik();
            }

            txt_nome.Enabled = false;
            txt_senha.Enabled = false;
            txt_confSenha.Enabled = false;
            cb_tipo.Enabled = false;

            bt_salvar.Visible = false;
            btn_cancelar.Visible = false;
            btn_alterar.Visible = true;
            btn_excluir.Visible = true;
            btn_novo.Visible = true;

            DataGridView1.Enabled = true;            
        }

        private void btn_alterar_Click(object sender, EventArgs e)
        {
            if (lb_idUsuario.Text == "")
            {
                MessageBox.Show("Selecione um produto valido!");
            }
            else
            {
                txt_nome.Enabled = true;
                txt_senha.Enabled = true;
                txt_confSenha.Enabled = true;
                cb_tipo.Enabled = true;

                txt_nome.Focus();

                btn_cancelar.Visible = true;
                bt_salvar.Visible = true;
                btn_alterar.Visible = false;
                btn_excluir.Visible = false;
                btn_novo.Visible = false;

                DataGridView1.Enabled = false;
                btn_reativar.Visible = false;
            }
            
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (lb_idUsuario.Text == "")
            {
                MessageBox.Show("Não e permitido excluir o master!");
            }
            else
            {
                if (MessageBox.Show("Realmente deseja excuir", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.usuarioCorrente.ativo = false;
                    DataContextFactory.DefinirUsuarioAtivoApi(this.usuarioCorrente.id_usuario, false);

                    CarregaUsuarios();
                    if (this.usuarioCorrente != null)
                    {
                        clik();
                    }
                    MessageBox.Show("Usuario excluido com sucesso!");
                }
            }
        }

        private void btn_reativar_Click(object sender, EventArgs e)
        {
            if (lb_idUsuario.Text == "")
            {
                MessageBox.Show("Selecione um produto valido!");
            }
            else
            {
                this.usuarioCorrente.ativo = true;
                DataContextFactory.DefinirUsuarioAtivoApi(this.usuarioCorrente.id_usuario, true);

                CarregaUsuarios();
                if (this.usuarioCorrente != null)
                {
                    clik();
                }
                MessageBox.Show("Usuario reativado com sucesso!");
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1)
            {
                e.Value = ((tb_tipoUsuario)e.Value).desc_tipoUsuario;
            }
        }

        private void btn_exibirInativos_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mostrarInativos = !mostrarInativos;
            btn_exibirInativos.Text = mostrarInativos ? "Ocultar Inativos" : "Exibir Inativos";
            CarregaUsuarios();
            if (this.usuarioCorrente != null)
            {
                clik();
            }
        }
    }
        
    
}

