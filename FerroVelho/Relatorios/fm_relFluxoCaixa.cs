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

namespace FerroVelho.Relatorios
{
    public partial class fm_relFluxoCaixa : Form
    {
        public fm_relFluxoCaixa()
        {
            InitializeComponent();
        }

        DateTime comeco, inicio, fim;

        private void fm_relFluxoCaixa_Load(object sender, EventArgs e)
        {
            dt_fim.Value = DateTime.Now;

            pesquisa();
        }

        private void bt_pesquisa_Click(object sender, EventArgs e)
        {
            pesquisa();
        }

        private void bt_imprimir_Click(object sender, EventArgs e)
        {
            pesquisa();
            imprimir();
        }

        string comand;
        decimal sInicio;
        private void pesquisa()
        {
            comeco = dt_inicio.MinDate;
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;

            comando.CommandText = "SELECT a.Entrada - b.Saida as total " +
                "FROM (SELECT sum(tb_caixa.valor_caixa) As Entrada " +
                "FROM tb_caixa " +
                "WHERE tb_caixa.data_caixa between '" + comeco + "' and '" + inicio.AddDays(-1) + "')a, " +
                "(SELECT sum(tb_compra.valor_nota) As Saida " +
                "FROM tb_compra " +
                "WHERE tb_compra.data_compra between '" + comeco + "' and '" + inicio.AddDays(-1) + "')b";

            sInicio = DataContextFactory.FiltrarValor(comando);

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            comand = "SELECT a.Data, a.Entrada, a.Saida " +
                "FROM (SELECT e.Data, sum(e.Entrada) as Entrada, sum(e.Saida) as Saida " +
                "FROM (SELECT tb_caixa.data_caixa as Data, sum(tb_caixa.valor_caixa) As Entrada, 0 as Saida " +
                "FROM tb_caixa " +
                "GROUP BY tb_caixa.data_caixa " +
                "union all " +
                "SELECT tb_compra.data_compra as Data, 0 as Entrada, sum(tb_compra.valor_nota) As Saida " +
                "FROM tb_compra " +
                "GROUP BY tb_compra.data_compra)e " +
                "GROUP BY e.Data)a " +
                "WHERE a.Data between '" + inicio + "' and '" + fim + "'" +
                "order by Data";
            DataTable dt = DataContextFactory.Filtrar(comand);
            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = dt.TableName;

            calcular();
        }

        
        private void calcular()
        {

            decimal inic, entrada, saida, saldo;
            saldo = sInicio;
            
            foreach (DataGridViewRow dg in dataGridView1.Rows)
            {
                dg.Cells[0].Value = saldo;
                inic = Convert.ToDecimal(dg.Cells[0].Value);
                entrada = Convert.ToDecimal(dg.Cells[3].Value);
                saida = Convert.ToDecimal(dg.Cells[4].Value);
                saldo = inic + entrada - saida;

                dg.Cells[1].Value = saldo;                

            }


        }

        private void imprimir()
        {
            MessageBox.Show("Em Manutenção!");
        }



    }
}
