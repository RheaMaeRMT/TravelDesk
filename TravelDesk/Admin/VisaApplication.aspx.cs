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
    public partial class VisaApplication : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script> window.location.href = '../LoginPage.aspx'; </script>");

            }
            if (!IsPostBack)
            {
                string status = Session["VreqStatus"]?.ToString();
                if (!string.IsNullOrEmpty(status))
                {
                    DisplayRequest();
                }
                else
                {
                    DisplayAllRequests();
                }
            }


        }
        private void DisplayRequest()
        {
            string userID = Session["userID"]?.ToString();
            string status = Session["VreqStatus"]?.ToString();

            if (!string.IsNullOrEmpty(status) && (!string.IsNullOrEmpty(userID)))
            {
                // Construct the SQL query using parameterized queries to prevent SQL injection
                string query = "SELECT visaReqStatus, visaReqID, visaFname + ' ' + ISNULL(visaMname, '') + ' ' + visaLname AS FullName, visaPurpose, visaDestination, visaEstTravelDate, visaDU, visaBdate, visaEmail, visaLevel, visaReqSubmitted FROM travelVisa WHERE visaReqStatus = @Status";

                // Set up the database connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Status", status);

                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the reader result to the GridView
                        visaRequests.DataSource = reader;
                        visaRequests.DataBind();

                        // Close the reader
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        // Log the exception or display a user-friendly error message
                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
                        Response.Write("<script>alert('An error occurred during retrieval of Draft Visa Request records. Please try again.')</script>");
                        // Log additional information from the SQL exception
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                        }
                    }
                }
            }
            // Remove the reqStatus session variable after displaying the requests
            Session.Remove("VreqStatus");
        }

        private void DisplayAllRequests()
        {
            string userID = Session["userID"]?.ToString();

            if ((!string.IsNullOrEmpty(userID)))
            {
                // Construct the SQL query using parameterized queries to prevent SQL injection
                string query = "SELECT visaReqStatus, visaReqID, visaFname + ' ' + ISNULL(visaMname, '') + ' ' + visaLname AS FullName, visaPurpose, visaDestination, visaEstTravelDate, visaDU, visaBdate, visaEmail, visaLevel, visaReqSubmitted FROM travelVisa WHERE visaReqStatus != 'Draft' ";

                // Set up the database connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query
                        SqlDataReader reader = command.ExecuteReader();

                        // Bind the reader result to the GridView
                        visaRequests.DataSource = reader;
                        visaRequests.DataBind();

                        // Close the reader
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        // Log the exception or display a user-friendly error message
                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
                        Response.Write("<script>alert('An error occurred during retrieval of Draft Visa Request records. Please try again.')</script>");
                        // Log additional information from the SQL exception
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                        }
                    }
                }
            }
            // Remove the reqStatus session variable after displaying the requests
            Session.Remove("VreqStatus");
        }

        protected void viewDetails_Click(object sender, EventArgs e)
        {
            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            string requestID = row.Cells[2].Text;

            Console.WriteLine(requestID);

            Session["clickedVRequest"] = requestID;

            if (!string.IsNullOrEmpty(requestID))
            {
                // Query the database to retrieve the request details based on the ID
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM travelVisa WHERE visaReqID = @RequestId";
                        cmd.Parameters.AddWithValue("@RequestId", requestID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Retrieve the request details from the reader
                                string status = reader["visaReqStatus"].ToString();

                                //check the status
                                if (status != null)
                                {
                                    Session["VreqStatus"] = status;
                                    Response.Redirect("VisaRequests.aspx");
                                }
                                //else if (status == "Processing")
                                //{
                                //    Response.Redirect("VisaRequests.aspx");

                                //}
                                else
                                {
                                    //redirect to the next page after clicking the view button
                                    Response.Redirect("VisaRequests.aspx");
                                }


                            }
                            else
                            {
                                // Handle the case where no request with the given ID is found
                                Response.Write("<script>alert('No request found with the specified ID.'); </script>");
                            }
                        }
                    }
                }
            }
        }
    }
}