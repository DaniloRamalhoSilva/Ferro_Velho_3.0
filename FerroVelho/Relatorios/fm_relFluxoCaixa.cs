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

        decimal sInicio;

        private void pesquisa()
        {
            comeco = dt_inicio.MinDate;
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            sInicio = DataContextFactory.CalcularSaldoInicialFluxoCaixaApi(inicio.Date);
            DataTable dtApi = DataContextFactory.CarregarFluxoCaixaApi(inicio.Date, fim.Date);
            dataGridView1.DataSource = dtApi;
            dataGridView1.DataMember = dtApi.TableName;
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

