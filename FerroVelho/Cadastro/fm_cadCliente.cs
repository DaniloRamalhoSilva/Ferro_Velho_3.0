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

namespace FerroVelho
{
    public partial class fm_cadCliente : Form
    {
        public fm_cadCliente(int Vguia)
        {
            InitializeComponent();
            guia = Vguia;
            txt_descricao.Focus();
        }

        int guia;

        public tb_cliente Propriedade { get; set; }

        public tb_cliente clienteCorrente
        {
            get
            {
                return (tb_cliente)this.tb_clienteBindingSource.Current;
            }
        }

        private void fm_cadCliente_Load(object sender, EventArgs e)
        {
            tipo();
            load();
            //preencherDevedor();
        }

        private void tipo()
        {
            if (guia == 0)
            {
                groupBox1.Visible = false;
                dataGridView1.Size = new Size(607, 482);
            }
            else
            {
                groupBox1.Visible = true;
                dataGridView1.Size = new Size(736, 222);
                btn_alterar.Visible = false;
                btn_cancelar.Visible = false;
                btn_excluir.Visible = false;
                btn_novo.Visible = false;
                btn_salvar.Visible = false;
                bt_select.Visible = false;                
            }
        }

        private void load()
        {
            tb_clienteBindingSource.DataSource = BuscarTudo();            
            
            txt_cnpj.Text = "";
            txt_descricao.Text = "";
            txt_tel.Text = "";
            txt_id.Text = "";
        }

        //private void preencherDevedor()
        //{
        //    foreach (DataGridViewRow dg in dataGridView1.Rows)
        //    {
        //        dg.Cells[4].Value = valorDevedor(Convert.ToInt32(dg.Cells[0].Value));
        //    }
        //}
                

        private void clik()
        {  
            try
            {
                txt_descricao.Text = this.clienteCorrente.nome_cliente;
                txt_cnpj.Text = this.clienteCorrente.cpf_cliente;
                txt_tel.Text = this.clienteCorrente.tel_cliente;
                txt_id.Text = this.clienteCorrente.id_cliente.ToString();
            }
            catch
            {
                txt_cnpj.Text = "";
                txt_descricao.Text = "";
                txt_tel.Text = "";
                txt_id.Text = "";
            }
        }

        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> CRUD >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private void btn_novo_Click(object sender, EventArgs e)
        {
            txt_tel.Text = "";
            txt_descricao.Text = "";
            txt_cnpj.Text = "";
            txt_id.Text = "";

            txt_descricao.Focus();

            btn_cancelar.Visible = true;
            btn_salvar.Visible = true;
            btn_alterar.Visible = false;
            btn_excluir.Visible = false;
            btn_novo.Visible = false;
            bt_select.Visible = false;

            dataGridView1.Enabled = false;
        }
        
        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            btn_salvar.Visible = false;
            btn_alterar.Visible = true;
            btn_excluir.Visible = true;
            btn_novo.Visible = true;
            bt_select.Visible = true;

            txt_tel.Text = "";
            txt_descricao.Text = "";
            txt_cnpj.Text = "";
            txt_id.Text = "";

            dataGridView1.Enabled = true;

            load();
            //preencherDevedor();
        }

        private void btn_alterar_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "")
            {
                MessageBox.Show("Selecione um item valido!");
            }
            else
            {
                txt_descricao.Focus();

                btn_cancelar.Visible = true;
                btn_salvar.Visible = true;
                btn_alterar.Visible = false;
                btn_excluir.Visible = false;
                btn_novo.Visible = false;
                bt_select.Visible = false;

                dataGridView1.Enabled = false;
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "")
            {
                MessageBox.Show("Selecione um item valido!");
            }
            else
            {
                if (MessageBox.Show("Realmente deseja excuir", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tb_cliente cliente = new tb_cliente();

                    cliente.id_cliente = Convert.ToInt32(txt_id.Text);
                    cliente.nome_cliente = txt_descricao.Text;
                    cliente.cpf_cliente = txt_cnpj.Text;
                    cliente.tel_cliente = txt_tel.Text;
                    try
                    {
                        Deletar(cliente);
                        load();
                        //preencherDevedor();
                        MessageBox.Show("Excluido com sucesso!");
                    }
                    catch
                    {
                        MessageBox.Show("Este Cliente possui movimentação e não pode ser excluido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            tb_cliente cliente = new tb_cliente();
           
            if (txt_descricao.Text == "" )
            {
                MessageBox.Show("Nome é obrigatorios!");
            }
            else
            {
                txt_cnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                txt_tel.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                cliente.nome_cliente = txt_descricao.Text;
                cliente.cpf_cliente = txt_cnpj.Text;
                cliente.tel_cliente = txt_tel.Text;

                if (txt_id.Text == "")
                {
                    Inserir(cliente);
                    MessageBox.Show("Salvo com sucesso!");
                }
                else
                {
                    cliente.id_cliente = Convert.ToInt32(txt_id.Text);
                    Alterar(cliente);
                    MessageBox.Show("Alterado com sucesso!");
                }

                txt_cnpj.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                txt_tel.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                txt_tel.Text = "";
                txt_descricao.Text = "";
                txt_cnpj.Text = "";
                txt_id.Text = "";

                btn_salvar.Visible = false;
                btn_cancelar.Visible = false;
                btn_alterar.Visible = true;
                btn_excluir.Visible = true;
                btn_novo.Visible = true;
                bt_select.Visible = true;
                dataGridView1.Enabled = true;
                load();
                //preencherDevedor();
            }
        }
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clik();
        }

        private void txt_descricao_KeyUp(object sender, KeyEventArgs e)
        {
            if (btn_salvar.Visible != true)
            {                
                if (txt_descricao.Text == "")
                {
                    tb_clienteBindingSource.DataSource = BuscarTudo();
                }
                else
                {
                    tb_clienteBindingSource.DataSource = BuscaDesc(txt_descricao.Text);
                }
                txt_cnpj.Text = "";
                txt_tel.Text = "";
                txt_id.Text = "";
                //preencherDevedor();
            }                       
        }

        private void txt_cnpj_KeyUp(object sender, KeyEventArgs e)
        {
            if (btn_salvar.Visible != true)
            {
                txt_cnpj.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                if (txt_cnpj.Text == "")
                {
                    tb_clienteBindingSource.DataSource = BuscarTudo();
                }
                else
                {
                    tb_clienteBindingSource.DataSource = BuscaCPF(txt_cnpj.Text);
                }

                txt_descricao.Text = "";
                txt_tel.Text = "";
                txt_id.Text = "";
                txt_cnpj.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                //preencherDevedor();
            }
               
        }

        private void txt_tel_KeyUp(object sender, KeyEventArgs e)
        {
            if (btn_salvar.Visible != true)
            {
                txt_tel.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (txt_tel.Text == "")
                {
                    tb_clienteBindingSource.DataSource = BuscarTudo();
                }
                else
                {
                    tb_clienteBindingSource.DataSource = BuscaTel(txt_tel.Text);
                }

                txt_descricao.Text = "";
                txt_cnpj.Text = "";
                txt_id.Text = "";
                txt_tel.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                //preencherDevedor();
            }                
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 3)
            {
                e.Value = formateTel(e.Value.ToString());
            }
            if (e.Value != null && e.ColumnIndex == 1)
            {
                e.Value = formateCPF(e.Value.ToString());
            }
            if (e.Value != null && e.ColumnIndex == 4)
            {
                if (Convert.ToDecimal(e.Value) > 0)
                { e.CellStyle.ForeColor = Color.Green; e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); }

                if (Convert.ToDecimal(e.Value) < 0)
                { e.CellStyle.ForeColor = Color.Red; e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); }

            }
        }

        private string formateTel(string texto)
        {
            if (texto.Length == 11)
            {
                texto = "(" + texto.Substring(0, 2) + ")" + texto.Substring(2, 5) + "-" + texto.Substring(7); 
            }
            if (texto.Length == 10)
            {
                texto = "(" + texto.Substring(0, 2) + ")" + texto.Substring(2, 4) + "-" + texto.Substring(6);
            }
            return texto;
        }

        private string formateCPF(string texto)
        {
            if (texto.Length == 11)
            {
                texto = texto.Substring(0, 3) + "." + texto.Substring(3, 3) + "." + texto.Substring(6, 3) + "-" + texto.Substring(10);
            }            
            return texto;
        }


        private void bt_select_Click(object sender, EventArgs e)
        {
            if (clienteCorrente != null && txt_id.Text != "")
            {
                Propriedade = clienteCorrente;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nenhum cliente selecionado");
            }
            
        }        

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                carregaItem(id);
            }
            catch
            {

            }
            
        }        

        private void dg_adPg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (checkBox_AdPg.Checked == true)
            {
                if (e.Value != null && e.ColumnIndex == 1)
                {
                    if (Convert.ToDecimal(e.Value) < 0)
                    {e.CellStyle.ForeColor = Color.Red; e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); }
                    else
                    {e.CellStyle.ForeColor = Color.Green; e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); }
                }
            }
            else
            {
                
                if (e.Value != null && e.ColumnIndex == 2)
                {                    
                    if (Convert.ToDecimal(e.Value) > 0)
                    { e.CellStyle.ForeColor = Color.Green; e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); }                    
                }

                if (e.Value != null && e.ColumnIndex == 3)
                {
                    decimal nota = Convert.ToDecimal(dg_adPg.Rows[e.RowIndex].Cells[1].Value.ToString());
                    decimal pago = Convert.ToDecimal(e.Value);
                    decimal receb = Convert.ToDecimal(dg_adPg.Rows[e.RowIndex].Cells[2].Value.ToString());
                    if (pago > 0 && pago != nota && receb == 0)
                    { e.CellStyle.ForeColor = Color.Red; e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); }
                }

                if (e.Value != null && e.ColumnIndex == 4)
                {
                    if (Convert.ToDecimal(e.Value) > 0 )
                    { e.CellStyle.ForeColor = Color.Orange;  }
                }
            }
            dg_adPg.Rows[dg_adPg.CurrentCell.RowIndex].Selected = false;
        }

        private void formatarDatagrid()
        {
            foreach (DataGridViewColumn column in dg_adPg.Columns)
            {
                if (column.DataPropertyName == "Data")
                { column.Width = 120;  }
                if (column.DataPropertyName == "Valor")
                { column.Width = 100;  }
                if (column.DataPropertyName == "Valor Nota")
                { column.Width = 80;  }
                if (column.DataPropertyName == "Adiantamento")
                { column.Width = 80;  }
                if (column.DataPropertyName == "Pagamento")
                { column.Width = 80;  }
                if (column.DataPropertyName == "Valor pago")
                { column.Width = 80;  }
            
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }            
        }

        private void checkBox_AdPg_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AdPg.Checked == true)
            {checkBoxCompleto.Checked = false;}
            else
            {checkBoxCompleto.Checked = true;}            

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            carregaItem(id);
        }

        private void checkBoxCompleto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCompleto.Checked == true)
            { checkBox_AdPg.Checked = false; }
            else
            { checkBox_AdPg.Checked = true; }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            carregaItem(id);
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Cliente DAO <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //private decimal valorDevedor(int id)
        //{
        //    decimal adianta, pag, credito, total;
        //    SqlCommand comando;

        //    comando = new SqlCommand();
        //    comando.CommandType = CommandType.Text;
        //    comando.CommandText = "Select sum(tb_compra.desconto_compra) as total " +
        //        "From tb_compra " +
        //        "where tb_compra.id_cliente = @id_cliente";
        //    comando.Parameters.AddWithValue("@id_cliente", id);

        //    pag = DataContextFactory.FiltrarValor(comando);

        //    comando = new SqlCommand();
        //    comando.CommandType = CommandType.Text;
        //    comando.CommandText = "Select sum(tb_caixa.valor_caixa) as total " +
        //        "From tb_caixa " +
        //        "where tb_caixa.id_cliente = @id_cliente";
        //    comando.Parameters.AddWithValue("@id_cliente", id);

        //    adianta = DataContextFactory.FiltrarValor(comando);

        //    comando = new SqlCommand();
        //    comando.CommandType = CommandType.Text;
        //    comando.CommandText = "Select sum( tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota) as total " +
        //        "From tb_compra " +
        //        "where tb_compra.id_cliente = @id_cliente";
        //    comando.Parameters.AddWithValue("@id_cliente", id);

        //    credito = DataContextFactory.FiltrarValor(comando);

        //    total = adianta + pag + credito;
        //    return total;
        //}

        private void carregaItem(int id)
        {
            string comando;
            if (checkBox_AdPg.Checked == true)
            {
                comando = "select a.Data, a.Valor as Valor, a.Obs " +
                    "from(Select tb_caixa.data_caixa as Data, tb_caixa.valor_caixa as Valor, tb_caixa.desc_caixa as Obs " +
                    "From tb_caixa " +
                    "where tb_caixa.id_cliente =" + id + "  AND tb_caixa.valor_caixa != 0 " +
                    "union all " +
                    "Select tb_aCliente.data__aCliente as Data, tb_aCliente.valor_aCliente as Valor, tb_aCliente.desc_aCliente as Obs " +
                    "From tb_aCliente " +
                    "where tb_aCliente.id_cliente =" + id + "  AND tb_aCliente.valor_aCliente != 0 " +
                    "union all " +
                    "Select tb_compra.data_compra as Data, tb_compra.desconto_compra as Valor, 'Pg na Nota: ' + CAST(tb_compra.id_compra as Varchar(10)) as Obs " +
                    "From tb_compra " +
                    "where tb_compra.id_cliente =" + id + "  AND tb_compra.desconto_compra != 0 " +
                    "union all " +
                    "Select tb_compra.data_compra as Data, tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota as Valor, 'Credito na Nota: ' + CAST(tb_compra.id_compra as Varchar(10)) as Obs " +
                    "From tb_compra " +
                    "where tb_compra.id_cliente =" + id + "  and tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota != 0 " +
                    ")a " +
                    "order by a.Data";
            }
            else
            {
                comando = "select a.Data, a.[Valor Nota], a.Pagamento as [Recebido do cliente], a.[Valor pago] as [Pago ao cliente], a.credito, a.Obs as Observação " +
                    "from(Select tb_caixa.data_caixa as Data, 0.00 as [Valor Nota], 0.00 as Pagamento, tb_caixa.valor_caixa * (-1) as [Valor pago], 0 as credito, tb_caixa.desc_caixa as Obs " +
                    "From tb_caixa " +
                    "where tb_caixa.id_cliente =" + id + " and tb_caixa.valor_caixa <= 0 " +
                    "union all " +
                    "Select tb_caixa.data_caixa as Data, 0.00 as [Valor Nota], tb_caixa.valor_caixa as Pagamento, 0 as [Valor pago], 0 as credito, tb_caixa.desc_caixa as Obs " +
                    "From tb_caixa " +
                    "where tb_caixa.id_cliente =" + id + " and tb_caixa.valor_caixa > 0 " +
                    "union all " +
                    "Select tb_aCliente.data__aCliente as Data, 0.00 as [Valor Nota], 0.00 as Pagamento, tb_aCliente.valor_aCliente * (-1) as [Valor pago], 0 as credito, tb_aCliente.desc_aCliente as Obs " +
                    "From tb_aCliente " +
                    "where tb_aCliente.id_cliente =" + id + " and tb_aCliente.valor_aCliente <= 0 " +
                    "union all " +
                    "Select tb_aCliente.data__aCliente as Data, 0.00 as [Valor Nota], tb_aCliente.valor_aCliente as Pagamento, 0 as [Valor pago], 0 as credito, tb_aCliente.desc_aCliente as Obs " +
                    "From tb_aCliente " +
                    "where tb_aCliente.id_cliente =" + id + " and tb_aCliente.valor_aCliente > 0 " +
                    "union all " +
                    "Select tb_compra.data_compra as Data, tb_compra.subtot_compra as [Valor Nota], tb_compra.desconto_compra as Pagamento, tb_compra.valor_nota as [Valor pago], tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota as credito, 'Nota: ' + CAST(tb_compra.id_compra as Varchar(10)) as Obs " +
                    "From tb_compra " +
                    "where tb_compra.id_cliente =" + id + " )a " +
                    "order by a.Data";
                
            }
            DataTable dt = DataContextFactory.Filtrar(comando);
            
            dg_adPg.DataSource = dt;
            formatarDatagrid();
        }

        SqlCommand comando;

        public IList<tb_cliente> BuscarTudo()
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente,  sum(a.Valor) as Saldo " +
                "from(Select  tb_caixa.valor_caixa as Valor, tb_cliente.id_cliente " +
                "From tb_caixa " +
                "inner join tb_cliente on tb_caixa.id_cliente = tb_cliente.id_cliente " +
                "where tb_caixa.valor_caixa != 0 " +
                "union all " +
                "Select  tb_aCliente.valor_aCliente as Valor, tb_cliente.id_cliente " +
                "From tb_aCliente " +
                "inner join tb_cliente on tb_aCliente.id_cliente = tb_cliente.id_cliente " +
                "where tb_aCliente.valor_aCliente != 0 " +
                "union all " +
                "Select  tb_compra.desconto_compra as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.desconto_compra != 0 " +
                "union all " +
                "Select  tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota != 0 " +
                "union all " +
                "Select 0 as Valor, tb_cliente.id_cliente " +
                "from tb_cliente)a " +
                "inner join tb_cliente on a.id_cliente = tb_cliente.id_cliente " +
                "group by a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente";
            SqlDataReader dr = DataContextFactory.Selecionar(comando);
            IList<tb_cliente> Cliente = new List<tb_cliente>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tb_cliente cliente = new tb_cliente();
                    cliente.id_cliente = (int)dr["id_cliente"];
                    cliente.nome_cliente = (string)dr["nome_cliente"];
                    try
                    {
                        cliente.cpf_cliente = (string)dr["cpf_cliente"];
                        cliente.tel_cliente = (string)dr["tel_cliente"];
                    }
                    catch
                    {
                        cliente.cpf_cliente = "";
                        cliente.tel_cliente = "";
                    }
                    cliente.Saldo = (decimal)dr["Saldo"];

                    Cliente.Add(cliente);
                }
            }
            else
            {
                Cliente = null;
            }
            dr.Close();
            return Cliente;
        }

        public void Deletar(tb_cliente cliente)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from tb_cliente where id_cliente=@id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", cliente.id_cliente);
            DataContextFactory.CRUD(comando);

        }

        public void Inserir(tb_cliente cliente)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into tb_cliente(nome_cliente, cpf_cliente, tel_cliente) values(@nome_cliente, @cpf_cliente, @tel_cliente)";
            comando.Parameters.AddWithValue("@nome_cliente", cliente.nome_cliente);
            comando.Parameters.AddWithValue("@cpf_cliente", cliente.cpf_cliente);
            comando.Parameters.AddWithValue("@tel_cliente", cliente.tel_cliente);

            DataContextFactory.CRUD(comando);
        }

        public void Alterar(tb_cliente cliente)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE  tb_cliente SET nome_cliente=@nome_cliente, cpf_cliente=@cpf_cliente, tel_cliente=@tel_cliente WHERE id_cliente=@id_cliente";
            comando.Parameters.AddWithValue("@id_cliente", cliente.id_cliente);
            comando.Parameters.AddWithValue("@nome_cliente", cliente.nome_cliente);
            comando.Parameters.AddWithValue("@cpf_cliente", cliente.cpf_cliente);
            comando.Parameters.AddWithValue("@tel_cliente", cliente.tel_cliente);

            DataContextFactory.CRUD(comando);
        }

        public IList<tb_cliente> BuscaDesc(string desc)
        {            
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente,  sum(a.Valor) as Saldo " +
                "from(Select  tb_caixa.valor_caixa as Valor, tb_cliente.id_cliente " +
                "From tb_caixa " +
                "inner join tb_cliente on tb_caixa.id_cliente = tb_cliente.id_cliente " +
                "where tb_caixa.valor_caixa != 0 " +
                "union all " +
                "Select  tb_aCliente.valor_aCliente as Valor, tb_cliente.id_cliente " +
                "From tb_aCliente " +
                "inner join tb_cliente on tb_aCliente.id_cliente = tb_cliente.id_cliente " +
                "where tb_aCliente.valor_aCliente != 0 " +
                "union all " +
                "Select  tb_compra.desconto_compra as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.desconto_compra != 0 " +
                "union all " +
                "Select  tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota != 0 " +
                "union all " +
                "Select 0 as Valor, tb_cliente.id_cliente " +
                "from tb_cliente)a " +
                "inner join tb_cliente on a.id_cliente = tb_cliente.id_cliente " +
                "where nome_cliente Like @nome_cliente " +
                "group by a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente";
            comando.Parameters.AddWithValue("@nome_cliente", desc + "%");
            SqlDataReader dr = DataContextFactory.Selecionar(comando);
            IList<tb_cliente> Cliente = new List<tb_cliente>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tb_cliente cliente = new tb_cliente();
                    cliente.id_cliente = (int)dr["id_cliente"];
                    cliente.nome_cliente = (string)dr["nome_cliente"];
                    try
                    {
                        cliente.cpf_cliente = (string)dr["cpf_cliente"];
                        cliente.tel_cliente = (string)dr["tel_cliente"];
                    }
                    catch
                    {
                        cliente.cpf_cliente = "";
                        cliente.tel_cliente = "";
                    }
                    cliente.Saldo = (decimal)dr["Saldo"];

                    Cliente.Add(cliente);
                }
            }
            else
            {
                Cliente = null;
            }
            dr.Close();
            return Cliente;
        }

        public IList<tb_cliente> BuscaCPF(string desc)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente,  sum(a.Valor) as Saldo " +
                "from(Select  tb_caixa.valor_caixa as Valor, tb_cliente.id_cliente " +
                "From tb_caixa " +
                "inner join tb_cliente on tb_caixa.id_cliente = tb_cliente.id_cliente " +
                "where tb_caixa.valor_caixa != 0 " +
                "union all " +
                "Select  tb_aCliente.valor_aCliente as Valor, tb_cliente.id_cliente " +
                "From tb_aCliente " +
                "inner join tb_cliente on tb_aCliente.id_cliente = tb_cliente.id_cliente " +
                "where tb_aCliente.valor_aCliente != 0 " +
                "union all " +
                "Select  tb_compra.desconto_compra as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.desconto_compra != 0 " +
                "union all " +
                "Select  tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota != 0 " +
                "union all " +
                "Select 0 as Valor, tb_cliente.id_cliente " +
                "from tb_cliente)a " +
                "inner join tb_cliente on a.id_cliente = tb_cliente.id_cliente " +
                "where cpf_cliente Like @cpf_cliente " +
                "group by a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente";
            comando.Parameters.AddWithValue("@cpf_cliente", desc + "%");
            SqlDataReader dr = DataContextFactory.Selecionar(comando);
            IList<tb_cliente> Cliente = new List<tb_cliente>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tb_cliente cliente = new tb_cliente();
                    cliente.id_cliente = (int)dr["id_cliente"];
                    cliente.nome_cliente = (string)dr["nome_cliente"];
                    try
                    {
                        cliente.cpf_cliente = (string)dr["cpf_cliente"];
                        cliente.tel_cliente = (string)dr["tel_cliente"];
                    }
                    catch
                    {
                        cliente.cpf_cliente = "";
                        cliente.tel_cliente = "";
                    }
                    cliente.Saldo = (decimal)dr["Saldo"];

                    Cliente.Add(cliente);
                }
            }
            else
            {
                Cliente = null;
            }
            dr.Close();
            return Cliente;
        }

        public IList<tb_cliente> BuscaTel(string desc)
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente,  sum(a.Valor) as Saldo " +
                "from(Select  tb_caixa.valor_caixa as Valor, tb_cliente.id_cliente " +
                "From tb_caixa " +
                "inner join tb_cliente on tb_caixa.id_cliente = tb_cliente.id_cliente " +
                "where tb_caixa.valor_caixa != 0 " +
                "union all " +
                "Select  tb_aCliente.valor_aCliente as Valor, tb_cliente.id_cliente " +
                "From tb_aCliente " +
                "inner join tb_cliente on tb_aCliente.id_cliente = tb_cliente.id_cliente " +
                "where tb_aCliente.valor_aCliente != 0 " +
                "union all " +
                "Select  tb_compra.desconto_compra as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.desconto_compra != 0 " +
                "union all " +
                "Select  tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota as Valor, tb_cliente.id_cliente " +
                "From tb_compra " +
                "inner join tb_cliente on tb_compra.id_cliente = tb_cliente.id_cliente " +
                "where tb_compra.subtot_compra - tb_compra.desconto_compra - tb_compra.valor_nota != 0 " +
                "union all " +
                "Select 0 as Valor, tb_cliente.id_cliente " +
                "from tb_cliente)a " +
                "inner join tb_cliente on a.id_cliente = tb_cliente.id_cliente " +
                "where tel_cliente Like @tel_cliente " +
                "group by a.id_cliente, tb_cliente.cpf_cliente, tb_cliente.nome_cliente, tb_cliente.tel_cliente";
            comando.Parameters.AddWithValue("@tel_cliente", desc + "%");
            SqlDataReader dr = DataContextFactory.Selecionar(comando);
            IList<tb_cliente> Cliente = new List<tb_cliente>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tb_cliente cliente = new tb_cliente();
                    cliente.id_cliente = (int)dr["id_cliente"];
                    cliente.nome_cliente = (string)dr["nome_cliente"];
                    try
                    {
                        cliente.cpf_cliente = (string)dr["cpf_cliente"];
                        cliente.tel_cliente = (string)dr["tel_cliente"];
                    }
                    catch
                    {
                        cliente.cpf_cliente = "";
                        cliente.tel_cliente = "";
                    }
                    cliente.Saldo = (decimal)dr["Saldo"];

                    Cliente.Add(cliente);
                }
            }
            else
            {
                Cliente = null;
            }
            dr.Close();
            return Cliente;
        }

        
    }
}
