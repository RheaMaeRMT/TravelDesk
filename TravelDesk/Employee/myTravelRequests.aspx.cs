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
    public partial class myTravelRequests : System.Web.UI.Page
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
                        viewDrafts.Visible = true;
                        DisplayRequests();
                    }

                }
                else
                {
                    DisplayAllRequests();
                }
            }
            
        }

        private void DisplayRequests(string sortExpression = null, string sortDirection = null)
        {
            string userID = Session["userID"]?.ToString();
            string status = Session["reqStatus"]?.ToString();
            string status2 = Session["reqStatus2"]?.ToString();
            statusLabel.Text = "List of Requests: " + status;

            viewDrafts.Style["display"] = "none";

            if (!string.IsNullOrEmpty(status) && (!string.IsNullOrEmpty(userID)))
            {
                // Base query
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
                   END AS travelDates,                     tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
            FROM travelRequest tr
            LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
            WHERE tr.travelUserID = @UserID 
                AND (tr.travelReqStatus = @Status";

                

                // Modify query if status2 is not null
                if (!string.IsNullOrEmpty(status2))
                {
                    query += " OR tr.travelReqStatus = @Status2";
                }

                // Close the condition grouping
                query += ")";

                if (!string.IsNullOrEmpty(sortExpression))
                {
                    query += $" ORDER BY {sortExpression} {sortDirection}";
                }

                // Set up the database connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Status", status);

                    if (!string.IsNullOrEmpty(status2))
                    {
                        command.Parameters.AddWithValue("@Status2", status2);
                    }

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
                        Response.Write("<script>alert('An error occurred during retrieval of Travel Request records. Please try again.')</script>");
                        // Log additional information from the SQL exception
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                        }
                    }
                }
            }
            // Remove the reqStatus session variables after displaying the requests
            Session.Remove("reqStatus");
            Session.Remove("reqStatus2");
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

        //protected void sortData_Click(object sender, EventArgs e)
        //{
        //    string sort = sortbyRequest.Text; // Assuming sortData.Text contains the sorting criteria
        //    //string status = Session["reqStatus"]?.ToString();
        //    string userID = Session["userID"].ToString();

        //    string status = Session["reqStatus"].ToString();

        //    //SORTING ALL REQUESTS
        //    if (status == null)
        //    {
        //        if (sort == "All")
        //        {
        //            try
        //            {
        //                // Construct the SQL query using parameterized queries to prevent SQL injection
        //                string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
        //                tr.travelFname + ' ' + ISNULL(tr.travelMname, '') + ' ' + tr.travelLname AS FullName,  
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN rt.routeOTo 
        //                    WHEN tr.travelOptions = 'Round trip' THEN rt.routeR1To
        //                    WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1To
        //                    ELSE tr.travelDestination                             
        //                END AS travelDestination, 
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN FORMAT(rt.routeODate, 'MMMM dd')
        //                    WHEN tr.travelOptions = 'Round trip' THEN FORMAT(rt.routeRdepart, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeRreturn, 'MMMM dd, yyyy')
        //                    WHEN tr.travelOptions = 'Multiple' THEN FORMAT(rt.routeM1ToDate, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeM2ToDate, 'MMMM dd, yyyy')
        //                    WHEN tr.travelType = 'Visa Request' THEN FORMAT(travelEstdate, 'MMMM dd')
        //                END AS travelDates, 
        //                tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
        //            FROM travelRequest tr
        //            LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
        //            WHERE tr.travelReqStatus != 'Draft' 
        //            AND tr.travelUserID = @userID";

        //                // Set up the database connection and command
        //                using (SqlConnection connection = new SqlConnection(connectionString))
        //                using (SqlCommand command = new SqlCommand(query, connection))
        //                {
        //                    // Add parameters
        //                    command.Parameters.AddWithValue("@userID", userID);

        //                    try
        //                    {
        //                        // Open the connection
        //                        connection.Open();

        //                        // Execute the query
        //                        SqlDataReader reader = command.ExecuteReader();

        //                        // Bind the reader result to the GridView
        //                        travelRequests.DataSource = reader;
        //                        travelRequests.DataBind();

        //                        // Close the reader
        //                        reader.Close();
        //                    }
        //                    catch (SqlException ex)
        //                    {
        //                        // Log the exception or display a user-friendly error message
        //                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //                        Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
        //                        // Log additional information from the SQL exception
        //                        for (int i = 0; i < ex.Errors.Count; i++)
        //                        {
        //                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle any other exceptions
        //                Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
        //            }

        //        }
        //        else
        //        {
        //            try
        //            {
        //                // Construct the SQL query using parameterized queries to prevent SQL injection
        //                string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
        //                tr.travelFname + ' ' + ISNULL(tr.travelMname, '') + ' ' + tr.travelLname AS FullName,  
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN rt.routeOTo 
        //                    WHEN tr.travelOptions = 'Round trip' THEN rt.routeR1To
        //                    WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1To
        //                    ELSE tr.travelDestination                             
        //                END AS travelDestination, 
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN FORMAT(rt.routeODate, 'MMMM dd')
        //                    WHEN tr.travelOptions = 'Round trip' THEN FORMAT(rt.routeRdepart, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeRreturn, 'MMMM dd, yyyy')
        //                    WHEN tr.travelOptions = 'Multiple' THEN FORMAT(rt.routeM1ToDate, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeM2ToDate, 'MMMM dd, yyyy')
        //                    WHEN tr.travelType = 'Visa Request' THEN FORMAT(travelEstdate, 'MMMM dd')
        //                END AS travelDates, 
        //                tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
        //            FROM travelRequest tr
        //            LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
        //            WHERE tr.travelReqStatus != 'Draft' 
        //            AND tr.travelUserID = @userID 
        //            AND tr.travelType = @sort";

        //                // Set up the database connection and command
        //                using (SqlConnection connection = new SqlConnection(connectionString))
        //                using (SqlCommand command = new SqlCommand(query, connection))
        //                {
        //                    // Add parameters
        //                    command.Parameters.AddWithValue("@sort", sort);
        //                    command.Parameters.AddWithValue("@userID", userID);

        //                    try
        //                    {
        //                        // Open the connection
        //                        connection.Open();

        //                        // Execute the query
        //                        SqlDataReader reader = command.ExecuteReader();

        //                        // Bind the reader result to the GridView
        //                        travelRequests.DataSource = reader;
        //                        travelRequests.DataBind();

        //                        // Close the reader
        //                        reader.Close();
        //                    }
        //                    catch (SqlException ex)
        //                    {
        //                        // Log the exception or display a user-friendly error message
        //                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //                        Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
        //                        // Log additional information from the SQL exception
        //                        for (int i = 0; i < ex.Errors.Count; i++)
        //                        {
        //                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle any other exceptions
        //                Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
        //            }

        //        }

        //    }
        //    else //SORTING STATUS-SPECIFIC REQUESTS
        //    {
        //        if (sort == "All")
        //        {
        //            try
        //            {
        //                // Construct the SQL query using parameterized queries to prevent SQL injection
        //                string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
        //                tr.travelFname + ' ' + ISNULL(tr.travelMname, '') + ' ' + tr.travelLname AS FullName,  
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN rt.routeOTo 
        //                    WHEN tr.travelOptions = 'Round trip' THEN rt.routeR1To
        //                    WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1To
        //                    ELSE tr.travelDestination                             
        //                END AS travelDestination, 
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN FORMAT(rt.routeODate, 'MMMM dd')
        //                    WHEN tr.travelOptions = 'Round trip' THEN FORMAT(rt.routeRdepart, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeRreturn, 'MMMM dd, yyyy')
        //                    WHEN tr.travelOptions = 'Multiple' THEN FORMAT(rt.routeM1ToDate, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeM2ToDate, 'MMMM dd, yyyy')
        //                    WHEN tr.travelType = 'Visa Request' THEN FORMAT(travelEstdate, 'MMMM dd')
        //                END AS travelDates, 
        //                tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
        //            FROM travelRequest tr
        //            LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
        //            WHERE tr.travelReqStatus != 'Draft' 
        //            AND tr.travelUserID = @userID 
        //            AND tr.travelReqStatus = @status";

        //                // Set up the database connection and command
        //                using (SqlConnection connection = new SqlConnection(connectionString))
        //                using (SqlCommand command = new SqlCommand(query, connection))
        //                {
        //                    // Add parameters
        //                    command.Parameters.AddWithValue("@status", status);
        //                    command.Parameters.AddWithValue("@userID", userID);

        //                    try
        //                    {
        //                        // Open the connection
        //                        connection.Open();

        //                        // Execute the query
        //                        SqlDataReader reader = command.ExecuteReader();

        //                        // Bind the reader result to the GridView
        //                        travelRequests.DataSource = reader;
        //                        travelRequests.DataBind();

        //                        // Close the reader
        //                        reader.Close();
        //                    }
        //                    catch (SqlException ex)
        //                    {
        //                        // Log the exception or display a user-friendly error message
        //                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //                        Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
        //                        // Log additional information from the SQL exception
        //                        for (int i = 0; i < ex.Errors.Count; i++)
        //                        {
        //                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle any other exceptions
        //                Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
        //            }

        //        }
        //        else
        //        {
        //            try
        //            {
        //                // Construct the SQL query using parameterized queries to prevent SQL injection
        //                string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
        //                tr.travelFname + ' ' + ISNULL(tr.travelMname, '') + ' ' + tr.travelLname AS FullName,  
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN rt.routeOTo 
        //                    WHEN tr.travelOptions = 'Round trip' THEN rt.routeR1To
        //                    WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1To
        //                    ELSE tr.travelDestination                             
        //                END AS travelDestination, 
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN FORMAT(rt.routeODate, 'MMMM dd')
        //                    WHEN tr.travelOptions = 'Round trip' THEN FORMAT(rt.routeRdepart, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeRreturn, 'MMMM dd, yyyy')
        //                    WHEN tr.travelOptions = 'Multiple' THEN FORMAT(rt.routeM1ToDate, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeM2ToDate, 'MMMM dd, yyyy')
        //                    WHEN tr.travelType = 'Visa Request' THEN FORMAT(travelEstdate, 'MMMM dd')
        //                END AS travelDates, 
        //                tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
        //            FROM travelRequest tr
        //            LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
        //            WHERE tr.travelReqStatus != 'Draft' 
        //            AND tr.travelUserID = @userID 
        //            AND tr.travelReqStatus = @status
        //            AND tr.travelType = @sort";

        //                // Set up the database connection and command
        //                using (SqlConnection connection = new SqlConnection(connectionString))
        //                using (SqlCommand command = new SqlCommand(query, connection))
        //                {
        //                    // Add parameters
        //                    command.Parameters.AddWithValue("@sort", sort);
        //                    command.Parameters.AddWithValue("@userID", userID);
        //                    command.Parameters.AddWithValue("@status", status);

        //                    try
        //                    {
        //                        // Open the connection
        //                        connection.Open();

        //                        // Execute the query
        //                        SqlDataReader reader = command.ExecuteReader();

        //                        // Bind the reader result to the GridView
        //                        travelRequests.DataSource = reader;
        //                        travelRequests.DataBind();

        //                        // Close the reader
        //                        reader.Close();
        //                    }
        //                    catch (SqlException ex)
        //                    {
        //                        // Log the exception or display a user-friendly error message
        //                        // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //                        Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
        //                        // Log additional information from the SQL exception
        //                        for (int i = 0; i < ex.Errors.Count; i++)
        //                        {
        //                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle any other exceptions
        //                Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
        //            }

        //        }
        //    }
        //}


        protected void travelRequests_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = GetSortDirection(e.SortExpression);
            string status = Session["newStatus"]?.ToString();

            if (!string.IsNullOrEmpty(status) && status != "ALL")
            {
                DisplayRequests(e.SortExpression, sortDirection);
            }
            else
            {
                DisplayAllRequests(e.SortExpression, sortDirection);
            }
        }

        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }


        private void DisplayAllRequests(string sortExpression = null, string sortDirection = null)
        {
            string userID = Session["userID"]?.ToString();

            if (!string.IsNullOrEmpty(userID))
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
                    WHERE tr.travelUserID = @UserID 
                    AND tr.travelReqStatus != 'Draft'";

                // Append sorting
                if (!string.IsNullOrEmpty(sortExpression))
                {
                    query += $" ORDER BY {sortExpression} {sortDirection}";
                }

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
        }
        protected void viewDrafts_Click(object sender, EventArgs e)
        {
            Session["reqStatus"] = "Draft";
            Response.Write("<script>window.location.href = 'myDraftTravelRequests.aspx'; </script>");

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

                                if (type == "Domestic Travel")
                                {
                                    //redirect to the next page after clicking the view button
                                  
                                    Response.Redirect("domesticRequestDetails.aspx");
                                }
                                else if (type == "International Travel")
                                {
                                    //redirect to the next page after clicking the view button
                                    Response.Redirect("internationalRequestDetails.aspx");
                                }
                                else if (type == "Visa Request")
                                {
                                    //redirect to the next page after clicking the view button
                                    Response.Redirect("visaRequestDetails.aspx");
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