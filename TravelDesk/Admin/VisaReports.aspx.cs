using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Admin
{
    public partial class VisaReports : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }

        }
        private void LoadReport()
        {
            string reportPath = Server.MapPath("~/Admin/Reports/VisaRequestReport.rdlc");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetVisaReport", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

           

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds, "TravelData");

                ReportViewer1.LocalReport.ReportPath = reportPath;
                ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["TravelData"]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
        }

    }
}