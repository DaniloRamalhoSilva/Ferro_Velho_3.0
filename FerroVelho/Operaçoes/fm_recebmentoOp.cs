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
    public partial class fm_recebmentoOp : Form
    {
        public fm_recebmentoOp()
        {
            InitializeComponent();
        }

        private void fm_recebmentoOp_Load(object sender, EventArgs e)
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

                Dao dao = new Dao();

                decimal saldo = dao.valorDevedor(cliente.id_cliente);

                if (saldo > 0)
                {
                    txt_valor.Text = saldo.ToString("N2");
                    txt_obs.Text = "Recebimento";
                }
                else
                {
                    txt_valor.Text = 0.ToString("N2");
                    txt_obs.Text = "Recebimento";
                }

                txt_valor.Focus();
            }
        }

        private void bt_salvar_Click(object sender, EventArgs e)
        {
            if (txt_nome.Text != "")
            {
                if (txt_valor.Text != "" && Convert.ToDecimal(txt_valor.Text) > 0)
                {
                    decimal valor = Convert.ToDecimal(txt_valor.Text);
                    tb_caixa caixa = new tb_caixa();

                    caixa.data_caixa = txt_data.Value;
                    caixa.desc_caixa = txt_obs.Text;
                    caixa.usuario = DataContextFactory.usu.id_usuario;
                    caixa.valor_caixa = valor;
                    caixa.id_cliente = Convert.ToInt32(txt_id.Text);

                    Inserir(caixa);
                    if (MessageBox.Show("Deseja Imprimir Comprovante?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Dao dao = new Dao();
                        dao.imprimir(txt_data.Value, txt_obs.Text, txt_valor.Text, txt_nome.Text, txt_tel.Text, txt_cnpj.Text);
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

        SqlCommand comando;

        public void Inserir(tb_caixa caixa)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into tb_caixa(data_caixa, desc_caixa, usuario, valor_caixa, id_cliente) values(@data_caixa, @desc_caixa, @usuario, @valor_caixa, @id_cliente)";
            comando.Parameters.AddWithValue("@data_caixa", caixa.data_caixa);
            comando.Parameters.AddWithValue("@desc_caixa", caixa.desc_caixa);
            comando.Parameters.AddWithValue("@usuario", caixa.usuario);
            comando.Parameters.AddWithValue("@valor_caixa", caixa.valor_caixa);
            comando.Parameters.AddWithValue("@id_cliente", caixa.id_cliente);

            DataContextFactory.CRUD(comando);
        }


    }
}
