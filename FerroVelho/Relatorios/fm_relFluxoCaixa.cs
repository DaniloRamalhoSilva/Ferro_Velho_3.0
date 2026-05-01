using FerroVelhoDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            calcular(dtApi);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dtApi;
        }

        
        private void calcular(DataTable fluxoCaixa)
        {
            decimal saldo = sInicio;
            
            foreach (DataRow linha in fluxoCaixa.Rows)
            {
                linha["Inicio"] = saldo;
                saldo = saldo + ValorDecimal(linha["Entrada"]) - ValorDecimal(linha["Saida"]);
                linha["Saldo"] = saldo;
            }
        }

        private static decimal ValorDecimal(object valor)
        {
            if (valor == null || valor == DBNull.Value)
            {
                return 0m;
            }

            return Convert.ToDecimal(valor, CultureInfo.InvariantCulture);
        }

        private void imprimir()
        {
            MessageBox.Show("Em Manutenção!");
        }



    }
}

