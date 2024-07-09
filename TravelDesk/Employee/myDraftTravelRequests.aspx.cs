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
    public partial class myDraftTravelRequests : System.Web.UI.Page
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
                    if (status != null)
                    {
                        Console.WriteLine("STATUS", status);
                        DisplayRequests();
                    }
                }
            }
        }

        private void DisplayRequests()
        {
            string userID = Session["userID"]?.ToString();
            string status = Session["reqStatus"]?.ToString();

            ////viewDrafts.Style["display"] = "none";

            if (!string.IsNullOrEmpty(status) && (!string.IsNullOrEmpty(userID)))
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
                        CASE 
                            WHEN tr.travelOptions = 'One Way' THEN rt.routeODate 
                            WHEN tr.travelOptions = 'Round trip' THEN rt.routeRdepart
                            WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1ToDate   
                            WHEN tr.travelType = 'Visa Request' THEN travelEstdate
                        END AS travelDates, 
                        tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
                    FROM travelRequest tr
                    LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
                    WHERE tr.travelUserID = @UserID 
                    AND tr.travelReqStatus = 'Draft'";

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
        ////protected void viewDrafts_Click(object sender, EventArgs e)
        ////{
        ////    Session["reqStatus"] = "Draft";
        ////    Response.Write("<script>window.location.href = 'myDraftRequests.aspx'; </script>");

        ////}

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
                                string type = reader["travelType"].ToString();
                                string status = reader["travelReqStatus"].ToString();

                                

                                if (type == "Domestic Travel")
                                {
                                    if (status == "Draft")
                                    {
                                        Session["requestStatus"] = status;
                                        //redirect to the next page after clicking the view button
                                        Response.Redirect("domesticRequestDetails.aspx");

                                    }
                                }
                                else if (type == "International Travel")
                                {
                                    //redirect to the next page after clicking the view button
                                    if (status == "Draft")
                                    {
                                        Session["requestStatus"] = status;
                                        //redirect to the next page after clicking the view button
                                        Response.Redirect("internationalRequestDetails.aspx");

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