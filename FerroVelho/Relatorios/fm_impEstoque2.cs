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
            return DataContextFactory.CarregarEstoqueAtualApi();
        }
    }
}

