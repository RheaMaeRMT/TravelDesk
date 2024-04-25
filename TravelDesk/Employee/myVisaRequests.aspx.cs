using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Employee
{
    public partial class myVisaRequests : System.Web.UI.Page
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
                string status = Session["reqStatus"]?.ToString();
                if (!string.IsNullOrEmpty(status))
                {
                    if (status == "Draft")
                    {
                        Console.WriteLine("STATUS", status);
                        DisplayRequest();
                    }
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
            string status = Session["reqStatus"]?.ToString();

            if (!string.IsNullOrEmpty(status) && (!string.IsNullOrEmpty(userID)))
            {
                // Construct the SQL query using parameterized queries to prevent SQL injection
                string query = "SELECT  visaReqID, visaFname + ' ' + ISNULL(visaMname, '') + ' ' + visaFname AS FullName, visaEmail, visaDU, visaPurpose, visaDestination, visaEstTravelDate, visaReqCreated FROM travelVisa WHERE visaUserID = @UserID AND visaReqStatus = @Status";

                // Set up the database connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@UserID", userID);
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
            Session.Remove("reqStatus");
        }
        private void DisplayAllRequests()
        {
            string userID = Session["userID"]?.ToString();

            if ((!string.IsNullOrEmpty(userID)))
            {
                // Construct the SQL query using parameterized queries to prevent SQL injection
                string query = "SELECT visaReqID, visaFname + ' ' + ISNULL(visaMname, '') + ' ' + visaFname AS FullName, visaEmail, visaDU, visaPurpose, visaDestination, visaEstTravelDate, visaReqCreated FROM travelVisa WHERE visaUserID = @UserID AND visaReqStatus != 'Draft' ";

                // Set up the database connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@UserID", userID);

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
            Session.Remove("reqStatus");
        }

        protected void viewDrafts_Click(object sender, EventArgs e)
        {
            Session["reqStatus"] = "Draft";
            Response.Write("<script>window.location.href = 'myDraftRequests.aspx'; </script>");

        }
    }
}