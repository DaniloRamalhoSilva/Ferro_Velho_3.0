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
    public partial class fm_movRecursos : Form
    {
        public fm_movRecursos()
        {
            InitializeComponent();
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

        string comand;        

        private void pesquisa()
        {           
            inicio = dt_inicio.Value;
            fim = dt_fim.Value;

            comand = "Select a.Data, Sum(a.Valor) as Valor, a.Descricao, tb_usuario.nome_usuario as Usuario " +
                "from(Select CONVERT(DATE, tb_caixa.data_caixa) as Data, tb_caixa.valor_caixa as Valor, tb_caixa.desc_caixa as Descricao, tb_caixa.usuario " +
                "From tb_caixa " +
                "where tb_caixa.id_cliente IS NOT NULL " +
                "union all " +
                "Select CONVERT(DATE, tb_caixa.data_caixa) as Data, tb_caixa.valor_caixa as Valor, tb_caixa.desc_caixa as Descricao, tb_caixa.usuario " +
                "From tb_caixa " +
                "where tb_caixa.id_cliente IS NULL " +
                "union all " +
                "Select CONVERT(DATE, tb_compra.data_compra) as Data, tb_compra.valor_nota * -1 as Valor, 'Compras' as Descricao, tb_compra.usuario " +
                "From tb_compra)a " +
                "inner join tb_usuario On a.usuario = tb_usuario.id_usuario " +
                "where a.Data between '" + inicio + "' and '" + fim + "' " +
                "group by CONVERT(DATE, a.Data), a.Descricao, tb_usuario.nome_usuario " +
                "order by a.Data";

            DataTable dt = DataContextFactory.Filtrar(comand);
            dg_recurso.DataSource = dt;
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

        private void imprimir()
        {
            MessageBox.Show("Em Manutenção!");
        }
    }
}
