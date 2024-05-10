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
    public partial class TravelRequests : System.Web.UI.Page
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
                if (status == null)
                {
                    DisplayAllRequests(); //method to display all the requests
                }
                else
                {
                    DisplayRequests(); //method for the clicked requests from dashboard
                  
                }
            }

        }
        protected void travelRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnRequestID = e.Row.FindControl("btnRequestID") as Button;
                if (btnRequestID != null)
                {
                    // Set the text of the button from the value of travelRequestID
                    string firstData = DataBinder.Eval(e.Row.DataItem, "travelRequestID").ToString();
                    btnRequestID.Text = firstData;

                }
            }
        }

        private void DisplayRequests()
        {
            string status = Session["reqStatus"]?.ToString();


            if (!string.IsNullOrEmpty(status))
            {
                // Construct the SQL query using parameterized queries to prevent SQL injection
                //string query = "SELECT trave travelReqStatus, travelType, travelFname + ' ' + ISNULL(travelMname, '') + ' ' + travelLname AS FullName,  travelDestination, travelDU, travelProjectCode, travelDateSubmitted FROM travelRequest WHERE travelUserID = @UserID AND travelReqStatus = @Status";
                string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
                        tr.travelFname + ' ' + ISNULL(tr.travelMname, '') + ' ' + tr.travelLname AS FullName,  
                        CASE 
                            WHEN tr.travelOptions = 'One Way' THEN rt.routeOTo 
                            WHEN tr.travelOptions = 'Round trip' THEN rt.routeR1To
                            WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1To
                            ELSE tr.travelDestination                             
                        END AS travelDestination, 
                        tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
                FROM travelRequest tr
                LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
                WHERE tr.travelReqStatus = @Status";

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
                        travelRequests.DataSource = reader;
                        travelRequests.DataBind();

                        // Close the reader
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        // Log the exception or display a user-friendly error message
                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
                        Response.Write("<script>alert('An error occurred during retrieval of Travel Request records. Please try again.')</script>");
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
            try
            {
                // Construct the SQL query using parameterized queries to prevent SQL injection
                string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
                        tr.travelFname + ' ' + ISNULL(tr.travelMname, '') + ' ' + tr.travelLname AS FullName,  
                        CASE 
                            WHEN tr.travelOptions = 'One Way' THEN rt.routeOTo 
                            WHEN tr.travelOptions = 'Round trip' THEN rt.routeR1To
                            WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1To
                            ELSE tr.travelDestination                             
                        END AS travelDestination, 
                        tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
                FROM travelRequest tr
                LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
                WHERE tr.travelReqStatus != 'Draft'";

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
                        travelRequests.DataSource = reader;
                        travelRequests.DataBind();

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
            catch
            {

            }

        }
        protected void viewDetails_Click(object sender, EventArgs e)
        {
            //Get the GridViewRow that contains the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            //Get the order ID from the first cell in the row
            string requestID = btn.Text;

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
                                string status = reader["travelReqStatus"].ToString();

                                if (status == "Arranged")
                                {
                                    Session["clickedRequest"] = requestID;
                                    //redirect to the next page after clicking the view button
                                    Response.Redirect("arrangedRequest.aspx");

                                } else if (status == "Processing")
                                {
                                    Session["clickedRequest"] = requestID;
                                    //redirect to the next page after clicking the view button
                                    Response.Redirect("TravelArrangements.aspx");
                                }
                                else
                                {
                                    string type = reader["travelType"].ToString();

                                    if (type == "Domestic Travel")
                                    {
                                        //redirect to the next page after clicking the view button
                                        Response.Redirect("RequestDetails.aspx");
                                    }
                                    else if (type == "International Travel")
                                    {
                                        //redirect to the next page after clicking the view button
                                        Response.Redirect("RequestDetails.aspx");
                                    }
                                    else if (type == "Visa Request")
                                    {
                                        Session["clickedVRequest"] = requestID;
                                        //redirect to the next page after clicking the view button
                                        Response.Redirect("VisaRequests.aspx");
                                    }
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
    }
}