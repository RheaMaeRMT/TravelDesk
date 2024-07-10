using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
                // Load data for both "International Travel" and "Visa Request"
                LoadReport("International Travel", "Visa Request");
            }
        }

        private void LoadReport(string travelType1, string travelType2)
        {
            string reportPath = Server.MapPath("~/Admin/Reports/InternationalReport.rdlc");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTravelReport", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Use a DataTable to store the combined result
                DataTable dt = new DataTable();

                // Fetch data for the first travel type
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@TravelType", travelType1));
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                da1.Fill(dt);

                // Fetch data for the second travel type
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@TravelType", travelType2));
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                da2.Fill(dt);

                // Bind the combined data to the report viewer
                ReportViewer1.LocalReport.ReportPath = reportPath;
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
        }


    }
}