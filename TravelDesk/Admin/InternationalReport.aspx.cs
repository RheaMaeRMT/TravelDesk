using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

        //private void LoadReport(string travelType1, string travelType2)
        //{
        //    string reportPath = Server.MapPath("~/Admin/Reports/InternationalReport.rdlc");

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("GetTravelReport", conn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        // Use a DataTable to store the combined result
        //        DataTable dt = new DataTable();

        //        // Fetch data for the first travel type
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.Add(new SqlParameter("@TravelType", travelType1));
        //        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //        da1.Fill(dt);

        //        // Fetch data for the second travel type
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.Add(new SqlParameter("@TravelType", travelType2));
        //        SqlDataAdapter da2 = new SqlDataAdapter(cmd);
        //        da2.Fill(dt);

        //        // Bind the combined data to the report viewer
        //        ReportViewer1.LocalReport.ReportPath = reportPath;
        //        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        //        ReportViewer1.LocalReport.DataSources.Clear();
        //        ReportViewer1.LocalReport.DataSources.Add(rds);
        //        ReportViewer1.LocalReport.Refresh();
        //    }
        //}

        private void LoadReport(string travelType1, string travelType2)
        {
            string reportPath = Server.MapPath("~/Admin/Reports/InternationalReport.rdlc");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Parse start and end dates
                DateTime startDate;
                DateTime endDate;
                if (!DateTime.TryParseExact(txtStartDate.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                {
                    // Handle invalid start date input if needed
                    // Example: Display an error message
                    return;
                }
                if (!DateTime.TryParseExact(txtEndDate.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                {
                    // Handle invalid end date input if needed
                    // Example: Display an error message
                    return;
                }

                // Fetch data for the first travel type
                DataTable dt1 = new DataTable();
                SqlCommand cmd1 = new SqlCommand("GetTravelReport", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@TravelType", travelType1);
                cmd1.Parameters.AddWithValue("@StartDate", startDate);
                cmd1.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);

                // Fetch data for the second travel type
                DataTable dt2 = new DataTable();
                SqlCommand cmd2 = new SqlCommand("GetTravelReport", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@TravelType", travelType2);
                cmd2.Parameters.AddWithValue("@StartDate", startDate);
                cmd2.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);

                // Merge the two DataTables into one
                DataTable mergedDt = new DataTable();
                mergedDt.Merge(dt1);
                mergedDt.Merge(dt2);

                // Bind the merged data to the report viewer
                ReportViewer1.LocalReport.ReportPath = reportPath;
                ReportDataSource rds = new ReportDataSource("DataSet1", mergedDt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadReport("International Travel", "Visa Request");
        }




    }
}