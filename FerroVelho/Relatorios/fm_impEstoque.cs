using FerroVelhoDAO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho.Relatorios
{
    public partial class fm_impEstoque : Form
    {
        DataTable dt2;

        public fm_impEstoque(DataTable dt)
        {
            InitializeComponent();
            dt2 = dt;
        }

        private void fm_impEstoque_Load(object sender, EventArgs e)
        {
            imprimir();
            this.reportViewer1.RefreshReport();
        }     
        
        public void imprimir()
        {                       

            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt2));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));
            
        }     
                

    }
}
