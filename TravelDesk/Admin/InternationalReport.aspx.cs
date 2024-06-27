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
	public partial class InternationalReport : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string travelType = "International Travel"; // or "International Travel"
                LoadReport(travelType);
            }

        }

        private void LoadReport(string travelType)
        {
            string reportPath = Server.MapPath("~/Admin/Reports/InternationalReport.rdlc");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTravelReport", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add the travelType parameter
                cmd.Parameters.Add(new SqlParameter("@TravelType", travelType));

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