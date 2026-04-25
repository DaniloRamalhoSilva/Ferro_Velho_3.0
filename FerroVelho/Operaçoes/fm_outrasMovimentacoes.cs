using FerroVelhoDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho.Operaçoes
{
    public partial class fm_outrasMovimentacoes : Form
    {
        public fm_outrasMovimentacoes()
        {
            InitializeComponent();
        }

        private void fm_outrasMovimentacoes_Load(object sender, EventArgs e)
        {
            txt_data.Value = DateTime.Now;
        }

        private void bt_select_Click(object sender, EventArgs e)
        {
            fm_cadCliente fm = new fm_cadCliente(0);
            tb_cliente cliente = new tb_cliente();
            fm.ShowDialog();
            cliente = fm.Propriedade;

            if (cliente != null)
            {
                txt_id.Text = cliente.id_cliente.ToString();
                txt_nome.Text = cliente.nome_cliente;
                txt_cnpj.Text = cliente.cpf_cliente;
                txt_tel.Text = cliente.tel_cliente;

                txt_valor.Text = 0.ToString("N2");
                txt_obs.Text = "";                

                txt_valor.Focus();
            }

        }

        private void bt_salvar_Click(object sender, EventArgs e)
        {
            if (txt_nome.Text != "")
            {
                if (txt_valor.Text != "")
                {
                    decimal valor = Convert.ToDecimal(txt_valor.Text);
                    tb_aCliente aCliente = new tb_aCliente();

                    aCliente.data__aCliente = txt_data.Value;
                    aCliente.desc_aCliente = txt_obs.Text;
                    aCliente.usuario = DataContextFactory.usu.id_usuario;
                    aCliente.valor_aCliente = valor;
                    aCliente.id_cliente = Convert.ToInt32(txt_id.Text);

                    Inserir(aCliente);
                    if (MessageBox.Show("Deseja Imprimir Comprovante?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        decimal valorC;
                        if (valor <0)
                        {
                            valorC = valor * -1;
                        }
                        else
                        {
                            valorC = valor;
                        }
                        Dao dao = new Dao();
                        dao.imprimir(txt_data.Value, txt_obs.Text, valorC.ToString(), txt_nome.Text, txt_tel.Text, txt_cnpj.Text);
                    }
                    MessageBox.Show("Salvo com sucesso!");
                    limpar();

                }
                else
                {
                    MessageBox.Show("Informe um valor valido!");
                    txt_valor.Focus();
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente!");
            }
        }

        private void limpar()
        {
            txt_id.Text = "";
            txt_nome.Text = "";
            txt_cnpj.Text = "";
            txt_tel.Text = "";
            txt_obs.Text = "";
            txt_valor.Text = 0.ToString("N2");
        }

        private void txt_valor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                txt_obs.Focus();
            }
            e.Handled = true;
        }

        private void txt_valor_Leave(object sender, EventArgs e)
        {
            decimal valor;
            try
            {
                valor = Convert.ToDecimal(txt_valor.Text);
            }
            catch
            {
                valor = 0;
            }

            txt_valor.Text = valor.ToString("N2");
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Cliente DAO <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        SqlCommand comando;

        public void Inserir(tb_aCliente aCliente)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into tb_aCliente(data__aCliente, desc_aCliente, usuario, valor_aCliente, id_cliente) values(@data__aCliente, @desc_aCliente, @usuario, @valor_aCliente, @id_cliente)";
            comando.Parameters.AddWithValue("@data__aCliente", aCliente.data__aCliente);
            comando.Parameters.AddWithValue("@desc_aCliente", aCliente.desc_aCliente);
            comando.Parameters.AddWithValue("@usuario", aCliente.usuario);
            comando.Parameters.AddWithValue("@valor_aCliente", aCliente.valor_aCliente);
            comando.Parameters.AddWithValue("@id_cliente", aCliente.id_cliente);

            DataContextFactory.CRUD(comando);
        }


    }
}
