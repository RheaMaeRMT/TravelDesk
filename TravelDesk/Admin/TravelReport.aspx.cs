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
    public partial class TravelReport : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string travelType = "Domestic Travel"; // or "International Travel"
                LoadReport(travelType);
            }

        }

        //private void LoadReport(string travelType)
        //{
        //    string reportPath = Server.MapPath("~/Admin/Reports/TravelReport.rdlc");

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("GetTravelReport", conn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        // Add the travelType parameter
        //        cmd.Parameters.Add(new SqlParameter("@TravelType", travelType));

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        System.Data.DataSet ds = new System.Data.DataSet();
        //        da.Fill(ds, "TravelData");

        //        ReportViewer1.LocalReport.ReportPath = reportPath;
        //        ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["TravelData"]);
        //        ReportViewer1.LocalReport.DataSources.Clear();
        //        ReportViewer1.LocalReport.DataSources.Add(rds);
        //        ReportViewer1.LocalReport.Refresh();
        //    }
        //}



        private void LoadReport(string travelType)
        {
            string reportPath = Server.MapPath("~/Admin/Reports/TravelReport.rdlc");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Parse start and end dates
                DateTime? startDate = null;
                DateTime? endDate = null;
                if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                {
                    if (!DateTime.TryParseExact(txtStartDate.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartDate))
                    {
                        // Handle invalid start date input if needed
                        // Example: Display an error message
                        return;
                    }
                    startDate = parsedStartDate;

                    if (!DateTime.TryParseExact(txtEndDate.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndDate))
                    {
                        // Handle invalid end date input if needed
                        // Example: Display an error message
                        return;
                    }
                    endDate = parsedEndDate;
                }

                // Fetch data using stored procedure
                SqlCommand cmd = new SqlCommand("GetTravelReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TravelType", travelType);
                cmd.Parameters.AddWithValue("@StartDate", startDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "TravelData");

                // Bind data to ReportViewer
                ReportViewer1.LocalReport.ReportPath = reportPath;
                ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["TravelData"]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadReport("Domestic Travel");
        }
    }
}