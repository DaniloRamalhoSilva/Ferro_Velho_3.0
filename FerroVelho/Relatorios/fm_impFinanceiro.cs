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
    public partial class fm_impFinanceiro : Form
    {
        DataTable dt1, dt2;
        int tipoR;
        string dtInicio, dtFim;

        public fm_impFinanceiro(int tipoRF, DataTable dt1F, DataTable dt2F, string dtInicioF, string dtFimF)
        {
            dt1 = dt1F;
            dt2 = dt2F;
            tipoR = tipoRF;
            dtInicio = dtInicioF;
            dtFim = dtFimF;
            InitializeComponent();
        }        

        private void fm_impFinanceiro_Load(object sender, EventArgs e)
        {
            if (tipoR == 1)
            {
                this.reportViewer1.Visible = true;
                this.reportViewer2.Visible = false;
                imprimir1();
                this.reportViewer1.RefreshReport();
            }
            else
            {
                this.reportViewer2.Visible = true;
                this.reportViewer1.Visible = false;
                imprimir2();
                this.reportViewer2.RefreshReport();
            }
                        
        }

        public void imprimir1()
        {    
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt1));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataInicio", Convert.ToString(dtInicio)));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataFim", Convert.ToString(dtFim)));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));

        }

        public void imprimir2()
        {            
            this.reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt1));
            this.reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));
            this.reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataInicio", Convert.ToString(dtInicio)));
            this.reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("dataFim", Convert.ToString(dtFim)));
            this.reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("empressa", DataContextFactory.nome));
            this.reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("tel", DataContextFactory.tel));
            this.reportViewer2.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("end", DataContextFactory.endereco));

        }

    }
}
