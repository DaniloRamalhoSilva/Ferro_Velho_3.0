using FerroVelhoDAO;
using Microsoft.Reporting.WinForms;
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
    public partial class fm_impEstoque2 : Form
    {
        public fm_impEstoque2()
        {
            InitializeComponent();
        }

        private void fm_impEstoque2_Load(object sender, EventArgs e)
        {
            imprimir();
            this.reportViewer1.RefreshReport();
        }

        public void imprimir()
        {
            
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData()));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));

        }

        private DataTable LoadSalesData()
        {
            string comando = "SELECT e.IdProduto as id_prod, tb_produtos.desc_prod, sum(e.Entrada) - sum(e.Saida) as qunt_est " +
                "FROM (SELECT tb_itemc.id_prod as IdProduto, sum(tb_itemc.quant_item) as Entrada, 0 As Saida " +
                "FROM tb_itemc " +
                "GROUP BY tb_itemc.id_prod " +
                "union all " +
                "SELECT tb_itemv.id_prod as IdProduto, 0 as Entrada, sum(tb_itemv.quant_item) As Saida " +
                "FROM tb_itemv " +
                "GROUP BY tb_itemv.id_prod)e " +
                "INNER JOIN tb_produtos ON e.IdProduto = tb_produtos.id_prod " +
                "GROUP BY tb_produtos.desc_prod, e.IdProduto";

            DataTable dt = DataContextFactory.Filtrar(comando);
            return dt;
        }
    }
}
