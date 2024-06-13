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
                       CASE 
                            WHEN tr.travelOptions = 'One Way' THEN FORMAT(rt.routeODate, 'MMMM dd')
                            WHEN tr.travelOptions = 'Round trip' THEN FORMAT(rt.routeRdepart, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeRreturn, 'MMMM dd, yyyy')
                            WHEN tr.travelOptions = 'Multiple' THEN FORMAT(rt.routeM1ToDate, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeM2ToDate, 'MMMM dd, yyyy')
                            WHEN tr.travelType = 'Visa Request' THEN FORMAT(travelEstdate, 'MMMM dd')
                        END AS travelDates, 
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
                        CASE 
                            WHEN tr.travelOptions = 'One Way' THEN FORMAT(rt.routeODate, 'MMMM dd')
                            WHEN tr.travelOptions = 'Round trip' THEN FORMAT(rt.routeRdepart, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeRreturn, 'MMMM dd, yyyy')
                            WHEN tr.travelOptions = 'Multiple' THEN FORMAT(rt.routeM1ToDate, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeM2ToDate, 'MMMM dd, yyyy')
                            WHEN tr.travelType = 'Visa Request' THEN FORMAT(travelEstdate, 'MMMM dd')
                        END AS travelDates, 
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
                                    string type = reader["travelType"].ToString();
                                    

                                     string processStat = reader["travelProcessStat"].ToString();

                                    if (type == "Domestic Travel" || type == "International Travel")
                                    {
                                        if (status == "Completed")
                                        {
                                            Session["clickedRequest"] = requestID;
                                            string empID = reader["travelEmpID"].ToString();
                                            Session["employeeID"] = empID;

                                            //redirect to the next page after clicking the view button
                                            Response.Redirect("arrangedRequest.aspx");

                                        }
                                        else if (status == "In-progress")
                                        {

                                                Session["requestStatus"] = status;
                                                Session["processStat"] = processStat;
                                                Response.Redirect("RequestDetails.aspx");

                                            //if (processStat != null)
                                            //{
                                            //    if (processStat == "Email Sent" || processStat == "Billing")
                                            //    {
                                            //        Response.Redirect("billingInformation.aspx");
                                            //    }
                                            //    else if (processStat == "Arranged")
                                            //    {
                                            //        Session["clickedRequest"] = requestID;
                                            //        string empID = reader["travelEmpID"].ToString();
                                            //        Session["employeeID"] = empID;

                                            //        //redirect to the next page
                                            //        Response.Redirect("arrangedRequest.aspx");
                                            //    }
                                            //    else
                                            //    {
                                            //        Session["clickedRequest"] = requestID;
                                            //        string empID = reader["travelEmpID"].ToString();
                                            //        Session["employeeID"] = empID;
                                            //        //redirect to the next page 
                                            //        Response.Redirect("TravelArrangements.aspx");

                                            //    }
                                            //}
                                            
                                        }
                                        else
                                        {
                                            Session["requestStatus"] = status;
                                            //redirect to the next page after clicking the view button

                                            Response.Redirect("RequestDetails.aspx");
                                        }

                                    }
                                    else if (type == "Visa Request")
                                    {
                                        Session["clickedVRequest"] = requestID;
                                         Session["visaStatus"] = status;
                                         Session["processStat"] = processStat;

                                    ;                                        //redirect to the next page after clicking the view button
                                    Response.Redirect("VisaRequests.aspx");
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

        protected void travelRequests_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Retrieve the DataTable from the GridView's DataSource
            DataTable dt = travelRequests.DataSource as DataTable;

            if (dt != null)
            {
                // Sort the DataTable based on the clicked column
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                // Rebind the sorted data to the GridView
                travelRequests.DataSource = dt;
                travelRequests.DataBind();
            }
        }
        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending
            string sortDirection = "ASC";

            // Retrieve the sort expression and sort direction from ViewState
            string sortExpression = ViewState["SortExpression"] as string;
            string sortDir = ViewState["SortDirection"] as string;

            // If the clicked column is the same as the previously sorted column, reverse the sort direction
            if (sortExpression != null && sortExpression.Equals(column))
            {
                if (sortDir != null && sortDir.Equals("ASC"))
                {
                    sortDirection = "DESC";
                }
            }

            // Store the current sort expression and sort direction in ViewState
            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = sortDirection;

            return sortDirection;
        }
    }
}