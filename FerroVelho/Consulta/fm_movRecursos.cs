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

namespace FerroVelho
{
    public partial class fm_movRecursos : Form
    {
        private static readonly CultureInfo BrazilianCulture = CultureInfo.GetCultureInfo("pt-BR");

        public fm_movRecursos()
        {
            InitializeComponent();
            ConfigureGridFormatting();
        }

        DateTime inicio, fim;

        private void fm_movRecursos_Load(object sender, EventArgs e)
        {
            dt_inicio.Value = DateTime.Now;
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

        private void pesquisa()
        {           
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            dg_recurso.DataSource = DataContextFactory.CarregarMovimentacaoRecursosApi(inicio, fim);
        }

        private void dg_recurso_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 1 )
            {
                if (Convert.ToDecimal(e.Value) < 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Green;
                }                
            }
        }

        private void ConfigureGridFormatting()
        {
            Column1.DefaultCellStyle.Format = "C2";
            Column1.DefaultCellStyle.FormatProvider = BrazilianCulture;
        }

        private void imprimir()
        {
            MessageBox.Show("Em Manutenção!");
        }
    }
}

