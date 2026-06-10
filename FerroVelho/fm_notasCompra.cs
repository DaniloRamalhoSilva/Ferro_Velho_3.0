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
    public partial class fm_notasCompra : Form
    {
        public fm_notasCompra()
        {
            InitializeComponent();
        }

        private void fm_notasCompra_Load(object sender, EventArgs e)
        {
            dt_fim.Value = DateTime.Now;
            dt_inicio.Value = DateTime.Now;
            consulta();
        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            consulta();
        }

        private void consulta()
        {
            DateTime dataI = dt_inicio.Value;
            DateTime dataF = dt_fim.Value;

            string comando = "SELECT * FROM tb_compra " +
                "WHERE tb_compra.data_compra between '" + dataI.Date + "' AND '" + dataF.Date.Add(new TimeSpan(23, 59, 59)) + "'";
            DataTable dt = DataContextFactory.Filtrar(comando);
            tb_vendaDataGridView.DataSource = dt;
            tb_vendaDataGridView.DataMember = dt.TableName;
            if (tb_vendaDataGridView.Rows.Count >= 1)
            {
                tb_vendaDataGridView.CurrentCell = tb_vendaDataGridView.Rows[tb_vendaDataGridView.Rows.Count - 1].Cells[0];
            }
            carregaItem();
        }

        private void tb_vendaDataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            carregaItem();
        }
        
        private void carregaItem()
        {
            try
            {
                int id = Convert.ToInt32(tb_vendaDataGridView.CurrentRow.Cells[0].Value);
                this.tb_itemcBindingSource.DataSource = DataContextFactory.DataContext.tb_itemc.Where(x => x.id_compra == id);
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string comando = "SELECT * FROM tb_compra " +
                "WHERE tb_compra.id_compra = " + Convert.ToInt32(txt_notaFiscal.Text);
                DataTable dt = DataContextFactory.Filtrar(comando);
                tb_vendaDataGridView.DataSource = dt;
                tb_vendaDataGridView.DataMember = dt.TableName;

                carregaItem();

                if (dt == null)
                {
                    MessageBox.Show("Nota fiscal: " + txt_notaFiscal.Text + " inexistente");
                    this.tb_itemcBindingSource.Clear();
                }
            }
            catch
            {
                string comando = "SELECT * FROM tb_compra ";
                DataTable dt = DataContextFactory.Filtrar(comando);
                tb_vendaDataGridView.DataSource = dt;
                tb_vendaDataGridView.DataMember = dt.TableName;
                if (tb_vendaDataGridView.Rows.Count >= 1)
                {
                    tb_vendaDataGridView.CurrentCell = tb_vendaDataGridView.Rows[tb_vendaDataGridView.Rows.Count - 1].Cells[0];
                }
                carregaItem();
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            SqlCommand comando;
            try
            {
                int id = Convert.ToInt32(tb_vendaDataGridView.CurrentRow.Cells[0].Value);
                comando = new SqlCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "Delete from tb_compra WHERE id_compra=@id";
                comando.Parameters.AddWithValue("@id", id);
                DataContextFactory.CRUD(comando);

                consulta();
                MessageBox.Show("Excluido com sucesso!");
            }
            catch
            {
                MessageBox.Show("Selecione uma nota fiscal! Erro:" + e);
            }

        }

        private void tb_vendaDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }


        private void tb_itemcDataGridView_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1)
            {
                e.Value = ((tb_produtos)e.Value).desc_prod;
            }
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tb_vendaDataGridView.CurrentRow.Cells[0].Value);
            fm_menulPrincipal fm = new fm_menulPrincipal();
            fm.imprimirNF(id);
        }

        
    }
}
