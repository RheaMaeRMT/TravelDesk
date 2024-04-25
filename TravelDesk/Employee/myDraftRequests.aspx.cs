using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Employee
{
    public partial class myDraftRequests : System.Web.UI.Page
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
                DisplayRequests();
            }

        }
        private void DisplayRequests()
        {
            string userID = Session["userID"]?.ToString();
            string status = Session["reqStatus"]?.ToString();

            if (!string.IsNullOrEmpty(status) && (!string.IsNullOrEmpty(userID)))
            {
                // Construct the SQL query using parameterized queries to prevent SQL injection
                string query = "SELECT travelType, travelRequestID, travelUserID, travelFname + ' ' + ISNULL(travelMname, '') + ' ' + travelLname AS FullName,  travelHomeFacility, travelProjectCode, travelDU, travelRemarks, travelOptions, travelPurpose, travelDateSubmitted FROM travelRequest WHERE travelUserID = @UserID AND travelReqStatus = @status ";

                // Set up the database connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@status", status);

                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query
                        SqlDataReader reader = command.ExecuteReader();

                        //// Bind the reader result to the GridView
                        //travelRequests.DataSource = reader;
                        //travelRequests.DataBind();

                        // Close the reader
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        // Log the exception or display a user-friendly error message
                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
                        Response.Write("<script>alert('An error occurred during route request enrollment. Please try again.')</script>");
                        // Log additional information from the SQL exception
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Something went wrong. Please try again.')</script>");
            }
        }

        protected void viewDetails_Click(object sender, EventArgs e)
        {
            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            string requestID = row.Cells[2].Text;

            Console.WriteLine(requestID);

            Session["clickedRequest"] = requestID;

            if (!string.IsNullOrEmpty(requestID))
            {
                // Query the database to retrieve the request details based on the ID
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM travelRequest WHERE travelRequestID = @RequestId";
                        cmd.Parameters.AddWithValue("@RequestId", requestID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Retrieve the request details from the reader
                                string status = reader["travelReqStatus"].ToString();
                                

                                //check the status
                                if (status == "Draft")
                                {
                                    Session["clickedRequest"] = requestID;
                                    //redirect to the details page after clicking the view button
                                    Response.Redirect("domesticRequestDetails.aspx");
                                }
                                else 
                                {
                                    Response.Write("<script>alert('An error occured while retrieving your draft. Please try again.')</script>");
                                }
                            }
                            else
                            {
                                // Handle the case where no request with the given ID is found
                                Response.Write("<script>alert('No request found with the specified ID.')</script>");
                            }
                        }
                    }
                }
            }
        }

        protected void travelRequests_Click(object sender, EventArgs e)
        {
            string clickedDraft = "travelRequests";
            Session["clickedDraft"] = clickedDraft;

            string status = "Draft";
            Session["reqStatus"] = status;
            Response.Write("<script>window.location.href = 'myTravelRequests.aspx'; </script>");

        }

        protected void visaRequests_Click(object sender, EventArgs e)
        {
            string clickedDraft = "visaRequests";
            Session["clickedDraft"] = clickedDraft;

            string status = "Draft";
            Session["reqStatus"] = status;
            Response.Write("<script>window.location.href = 'myVisaRequests.aspx'; </script>");
        }
    }
}