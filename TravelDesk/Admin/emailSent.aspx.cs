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
    public partial class emailSent : System.Web.UI.Page
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
                changeReqStatus();
            }
        }
        private void changeReqStatus()
        {
            try
            {
                if (Session["clickedRequest"] != null)
                {
                    string requestId = Session["clickedRequest"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE travelRequest SET travelProcessStat = @newStatus WHERE travelRequestID = @ID";

                            // Set parameters for updating request status
                            cmd.Parameters.AddWithValue("@newStatus", "Email Sent");
                            cmd.Parameters.AddWithValue("@ID", requestId);

                            // Execute the update query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Fetch employee's first name and last name
                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT travelFname, travelLname FROM travelRequest WHERE travelRequestID = @ID";
                                cmd.Parameters.AddWithValue("@ID", requestId);

                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string firstName = reader["travelFname"].ToString();
                                        string lastName = reader["travelLname"].ToString();

                                        // Display alert message with employee's name
                                        string alertMessage = "Email has been sent to " + firstName + " " + lastName;
                                        Response.Write("<script>alert('" + alertMessage + "'); window.location.href = 'billingInformation.aspx'; </script>");
                                    }
                                }
                            }
                            else
                            {
                                // No rows were affected, meaning no matching travel request ID was found
                                Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                            }
                        }
                    }
                }
                else
                {
                    // Session is expired, redirect to login page
                    Response.Write("<script>alert('Session Expired! Please login again.'); window.location.href = '../LoginPage.aspx'; </script>");
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request status update", ex);
                Response.Write("<script>alert('An error occurred during travel request status update. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


        }
    }
}