
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СoalApp
{
    public partial class ReportingForm : Form
    {
        public ReportingForm()
        {
            InitializeComponent();
        }

        private void ReportingForm_Load(object sender, EventArgs e)
        {                   
            reportViewer1.LocalReport.SetParameters(new ReportParameter("DataParameters", DateTime.Now.ToString("dd.MM.yyyy HH:mm")));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("ClientParameters", "Кайнов Александр Владимирович"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("TelParameters", "+79134482364"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("AddressParameters", "sdfdsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsddfsdfsdfsdfsdfsdfdsfdsfsdfsdfsdfsdfsdfsdff"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("ProviderParameters", "ОООО ываываываыва"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("MarkParameters", "ываыва-ываы"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("CountParameters", "1123"));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("SummaParameters", "1231231".ToString()));


           this.reportViewer1.RefreshReport();
        }
    }
}
