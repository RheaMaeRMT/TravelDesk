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
    public partial class VisaBilling : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script> window.location.href = '../LoginPage.aspx'; </script>");

            }
        }


        protected void submitVisaFee_Click(object sender, EventArgs e)
        {
            string visaFee = txtVisaFee.Text;

            string request = Session["clickedRequest"].ToString();

            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "B" + random;

            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelBilling (billingID, VisaFee, billTotal, billTravelID)"
                                            + "VALUES("
                                            + "@ID,"
                                            + "@visaFee,"
                                            + "@total,"
                                            + "@travelID)";

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@visaFee", visaFee);
                        cmd.Parameters.AddWithValue("@total", visaFee);
                        cmd.Parameters.AddWithValue("@travelID", request);


                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            updateRequestStatus();

                        }
                    }
                }



            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }

        }

        private void updateRequestStatus()
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
                            cmd.CommandText = "UPDATE travelRequest SET travelReqStatus = @newStatus WHERE travelRequestID = @ID, travelReqCompleted = @date";

                            // Set parameters for updating request status
                            cmd.Parameters.AddWithValue("@newStatus", "Completed");
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);
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
                                        string alertMessage = "Billing Information for Visa Request from " + firstName + " " + lastName + " has been successfully processed";
                                        Response.Write("<script>alert('" + alertMessage + "'); window.location.href = 'AdminDashboard.aspx'; </script>");
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