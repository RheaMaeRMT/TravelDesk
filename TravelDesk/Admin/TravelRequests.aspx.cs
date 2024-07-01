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

        //private void DisplayRequests()
        //{
        //    string status = Session["reqStatus"]?.ToString();
        //    statusLabel.Text = "List of Requests:" + " " + status;

        //    if (!string.IsNullOrEmpty(status))
        //    {
        //        if (status == "ALL")
        //        {
        //            DisplayAllRequests();
        //        }
        //        else
        //        {
        //            // Construct the SQL query using parameterized queries to prevent SQL injection
        //            //string query = "SELECT trave travelReqStatus, travelType, travelFname + ' ' + ISNULL(travelMname, '') + ' ' + travelLname AS FullName,  travelDestination, travelDU, travelProjectCode, travelDateSubmitted FROM travelRequest WHERE travelUserID = @UserID AND travelReqStatus = @Status";
        //            string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
        //                tr.travelFname + ' ' + ISNULL(tr.travelMname, '') + ' ' + tr.travelLname AS FullName,  
        //                CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN rt.routeOTo 
        //                    WHEN tr.travelOptions = 'Round trip' THEN rt.routeR1To
        //                    WHEN tr.travelOptions = 'Multiple' THEN rt.routeM1To
        //                    ELSE tr.travelDestination                             
        //                END AS travelDestination, 
        //               CASE 
        //                    WHEN tr.travelOptions = 'One Way' THEN FORMAT(rt.routeODate, 'MMMM dd')
        //                    WHEN tr.travelOptions = 'Round trip' THEN FORMAT(rt.routeRdepart, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeRreturn, 'MMMM dd, yyyy')
        //                    WHEN tr.travelOptions = 'Multiple' THEN FORMAT(rt.routeM1ToDate, 'MMMM dd') + ' ' + '-' + ' ' + FORMAT(rt.routeM2ToDate, 'MMMM dd, yyyy')
        //                    WHEN tr.travelType = 'Visa Request' THEN FORMAT(travelEstdate, 'MMMM dd')
        //                END AS travelDates, 
        //                tr.travelDU, tr.travelProjectCode, tr.travelDateSubmitted 
        //        FROM travelRequest tr
        //        LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
        //        WHERE tr.travelReqStatus = @Status";



        //            // Set up the database connection and command
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                // Add parameters
        //                command.Parameters.AddWithValue("@Status", status);

        //                try
        //                {
        //                    // Open the connection
        //                    connection.Open();

        //                    // Execute the query
        //                    SqlDataReader reader = command.ExecuteReader();

        //                    // Bind the reader result to the GridView
        //                    travelRequests.DataSource = reader;
        //                    travelRequests.DataBind();

        //                    // Close the reader
        //                    reader.Close();
        //                }
        //                catch (SqlException ex)
        //                {
        //                    // Log the exception or display a user-friendly error message
        //                    // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //                    Response.Write("<script>alert('An error occurred during retrieval of Travel Request records. Please try again.')</script>");
        //                    // Log additional information from the SQL exception
        //                    for (int i = 0; i < ex.Errors.Count; i++)
        //                    {
        //                        Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //                    }
        //                }
        //            }
        //        }

        //    }

        //    // Remove the reqStatus session variable after displaying the requests
        //    Session.Remove("reqStatus");
        //}
        //private void DisplayAllRequests()
        //{
        //    statusLabel.Text = "List of Requests: ALL ";

        //    try
        //    {

        //        // Construct the SQL query using parameterized queries to prevent SQL injection
        //        string query = @"SELECT tr.travelRequestID, tr.travelReqStatus, tr.travelType, 
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
        //            WHERE tr.travelReqStatus != 'Draft'";

        //        // Set up the database connection and command
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {

        //            try
        //            {
        //                // Open the connection
        //                connection.Open();

        //                // Execute the query
        //                SqlDataReader reader = command.ExecuteReader();

        //                // Bind the reader result to the GridView
        //                travelRequests.DataSource = reader;
        //                travelRequests.DataBind();

        //                // Close the reader
        //                reader.Close();
        //            }
        //            catch (SqlException ex)
        //            {
        //                // Log the exception or display a user-friendly error message
        //                // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //                Response.Write("<script>alert('An error occurred during route request enrollment. Please try again.')</script>");
        //                // Log additional information from the SQL exception
        //                for (int i = 0; i < ex.Errors.Count; i++)
        //                {
        //                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }


        //}

        private void DisplayRequests(string sortExpression = null, string sortDirection = null)
        {
            string status = Session["reqStatus"]?.ToString();
            statusLabel.Text = "List of Requests: " + status;

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "ALL")
                {
                    DisplayAllRequests(sortExpression, sortDirection);
                }
                else
                {
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

                    Session["newStatus"] = status.ToString();
                    string newStat = Session["newStatus"].ToString();
                    if (!string.IsNullOrEmpty(sortExpression))
                    {
                        query += $" ORDER BY {sortExpression} {sortDirection}";
                    }

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            travelRequests.DataSource = reader;
                            travelRequests.DataBind();
                            reader.Close();
                        }
                        catch (SqlException ex)
                        {
                            Response.Write("<script>alert('An error occurred during retrieval of Travel Request records. Please try again.')</script>");
                            for (int i = 0; i < ex.Errors.Count; i++)
                            {
                                Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                            }
                        }
                    }
                }
            }
            else
            {
                DisplayAllRequests(sortExpression, sortDirection);
            }
            Session.Remove("reqStatus");
        }

        private void DisplayAllRequests(string sortExpression = null, string sortDirection = null)
        {
            statusLabel.Text = "List of Requests: ALL ";

            try
            {
                // Construct the SQL query
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

                // Append sorting
                if (!string.IsNullOrEmpty(sortExpression))
                {
                    query += $" ORDER BY {sortExpression} {sortDirection}";
                }

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
                        travelRequests.DataSource = reader;
                        travelRequests.DataBind();

                        // Close the reader
                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        // Log the exception or display a user-friendly error message
                        Response.Write("<script>alert('An error occurred during route request enrollment. Please try again.')</script>");
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                        }
                    }
                }
            }
            catch
            {
                // Handle exception
            }
        }

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
                                            Session["requestStatus"] = status;
                                            Session["clickedRequest"] = requestID;
                                            string empID = reader["travelEmpID"].ToString();
                                            Session["employeeID"] = empID;

                                            //redirect to the next page after clicking the view button
                                            Response.Redirect("RequestDetails.aspx");

                                        }
                                        else if (status == "In-progress" || status == "Email Sent")
                                        {

                                                Session["requestStatus"] = status;
                                                Session["processStat"] = processStat;
                                                Response.Redirect("RequestDetails.aspx");                                            
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
                                        Session["processStat"] = "";

                                       //redirect to the next page after clicking the view button
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


        protected void sortData_Click(object sender, EventArgs e)
        {
            string sort = sortbyRequest.Text; // Assuming sortData.Text contains the sorting criteria
            //string status = Session["reqStatus"]?.ToString();
            string esc = "List of Requests:";
            string currentStat = statusLabel.Text;
            string status = currentStat.Replace(esc, "").Trim();


            if (status == "ALL")
            {
                //SORTING OF ALL REQUESTS
                if (sort == "All") 
                {
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
                    WHERE tr.travelReqStatus != 'Draft' ";

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
                                travelRequests.DataSource = reader;
                                travelRequests.DataBind();

                                // Close the reader
                                reader.Close();
                            }
                            catch (SqlException ex)
                            {
                                // Log the exception or display a user-friendly error message
                                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                                Response.Write("<script>alert('An error occurred during sorting of ALL requests - ALL SORT. Please try again.')</script>");
                                // Log additional information from the SQL exception
                                for (int i = 0; i < ex.Errors.Count; i++)
                                {
                                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
                    }

                }
                else
                {
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
                    WHERE tr.travelReqStatus != 'Draft' AND tr.travelType = @sort";

                        // Set up the database connection and command
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters
                            command.Parameters.AddWithValue("@sort", sort);

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
                                Response.Write("<script>alert('An error occurred STATUS - SPECIFIC. Please try again.')</script>");
                                // Log additional information from the SQL exception
                                for (int i = 0; i < ex.Errors.Count; i++)
                                {
                                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
                    }

                }
            } 
            else
            {

                //SORTING OF STATUS-SPECIFIC REQUEST
                if (sort == "All")
                {
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
                    WHERE tr.travelReqStatus != 'Draft' AND tr.travelReqStatus = @status";

                        // Set up the database connection and command
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters
                            command.Parameters.AddWithValue("@status", status);

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
                                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                                // Log additional information from the SQL exception
                                for (int i = 0; i < ex.Errors.Count; i++)
                                {
                                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
                    }

                }
                else
                {
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
                    WHERE tr.travelReqStatus != 'Draft' AND tr.travelReqStatus = @status AND tr.travelType = @sort";

                        // Set up the database connection and command
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters
                            command.Parameters.AddWithValue("@sort", sort);
                            command.Parameters.AddWithValue("@status", status);

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
                                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                                // Log additional information from the SQL exception
                                for (int i = 0; i < ex.Errors.Count; i++)
                                {
                                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions
                        Response.Write("<script>alert('An unexpected error occurred. Please try again later.')</script>");
                    }

                }
            }
        }

    }
}