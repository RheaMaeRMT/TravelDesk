using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script> window.location.href = '../LoginPage.aspx'; </script>");

            }
            else
            {
                int approvedCount = populateDashboardApproved();
                approved.Text = approvedCount.ToString();

                int pendingCount = populateDashboardPending();
                pending.Text = pendingCount.ToString();

                int completedCount = populateDashboardCompleted();
                completed.Text = completedCount.ToString();

                int processingCount = populateDashboardProcessing();
                processing.Text = processingCount.ToString();

            }
        }
        private int populateDashboardApproved()
        {

            int countApproved = 0;

            try
            {
                string currentManager = Session["userName"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentManager)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest TR INNER JOIN users U ON TR.travelUserID = U.userID WHERE TR.travelApprovalStat = 'auto-approved' ";
                            cmd.Parameters.AddWithValue("@userManager", currentManager);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countApproved = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return countApproved;



        }
        private int populateDashboardPending()
        {
            int countPending = 0;

            try
            {
                string currentManager = Session["userName"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentManager)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest TR INNER JOIN users U ON TR.travelUserID = U.userID WHERE TR.travelApprovalStat = 'pending' ";
                            cmd.Parameters.AddWithValue("@userManager", currentManager);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countPending = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return countPending;

        }
        private int populateDashboardCompleted()
        {
            int count = 0;

            try
            {
                string currentManager = Session["userName"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentManager)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest TR INNER JOIN users U ON TR.travelUserID = U.userID WHERE TR.travelApprovalStat = 'completed' ";
                            cmd.Parameters.AddWithValue("@userManager", currentManager);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                count = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return count;

        }
        private int populateDashboardProcessing()
        {
            int count = 0;

            try
            {
                string currentManager = Session["userName"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentManager)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest TR INNER JOIN users U ON TR.travelUserID = U.userID WHERE TR.travelApprovalStat = 'processing' ";
                            cmd.Parameters.AddWithValue("@userManager", currentManager);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                count = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return count;

        }

    }
}