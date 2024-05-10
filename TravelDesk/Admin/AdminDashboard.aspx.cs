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
    public partial class AdminDashboard : System.Web.UI.Page
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
                //POPULATE NUMBERS FOR TRAVEL REQUESTS
                int approvedCount = populateDashboardApproved();
                Approved.Text = approvedCount.ToString();

                int pendingCount = populateDashboardProcessing();
                Processing.Text = pendingCount.ToString();

                int completedCount = populateDashboardCompleted();
                Processed.Text = completedCount.ToString();

                int processingCount = populateDashboardArranged();
                Arranged.Text = processingCount.ToString();

                //POPULATE NUMBERS FOR VISA REQUESTS
                int visaPendingCount = populateVisaPending();
                visaPending.Text = visaPendingCount.ToString();

                int visaProcessingCount = populateVisaProcessing();
                visaProcessing.Text = visaProcessingCount.ToString();

                int visaCompletedCount = populateVisaCompleted();
                visaCompleted.Text = visaCompletedCount.ToString();

                int visaGrantedCount = populateVisaGranted();
                visaGranted.Text = visaGrantedCount.ToString();

            }


        }

        //POPULATE NUMBERS IN THE DASHBOARD - VISA REQUEST
        private int populateVisaPending()
        {

            int countApproved = 0;

            try
            {

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelType = 'Visa Request' AND travelReqStatus = 'Pending'";

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countApproved = Convert.ToInt32(result);
                            }
                        }
                    }


            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during visa requests enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return countApproved;



        }
        private int populateVisaProcessing()
        {

            int countApproved = 0;

            try
            {

                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelType = 'Visa Request' AND travelReqStatus = 'Processing'";

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            countApproved = Convert.ToInt32(result);
                        }
                    }
                }


            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during visa requests enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return countApproved;



        }
        private int populateVisaCompleted()
        {
            int countApproved = 0;

            try
            {

                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelType = 'Visa Request' AND travelReqStatus = 'Completed'";

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            countApproved = Convert.ToInt32(result);
                        }
                    }
                }


            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during visa requests enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return countApproved;
        }
        private int populateVisaGranted()
        {

            int countApproved = 0;

            try
            {

                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelType = 'Visa Request' AND travelReqStatus = 'Granted'";

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            countApproved = Convert.ToInt32(result);
                        }
                    }
                }


            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during visa requests enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


            return countApproved;



        }




        //POPULATE NUMBERS IN THE DASHBOARD - TRAVEL REQUEST
        private int populateDashboardApproved()
        {

            int countApproved = 0;

            try
            {
                string currentUser = Session["userID"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentUser)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelReqStatus = 'Approved'";
                            cmd.Parameters.AddWithValue("@UserID", currentUser);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countApproved = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
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


            return countApproved;



        }
        private int populateDashboardProcessing()
        {
            int countApproved = 0;

            try
            {
                string currentUser = Session["userID"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentUser)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelReqStatus = 'Processing'";
                            cmd.Parameters.AddWithValue("@UserID", currentUser);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countApproved = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
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


            return countApproved;


        }
        private int populateDashboardCompleted()
        {
            int countCompleted = 0;

            try
            {
                string currentManager = Session["userID"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentManager)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelReqStatus = 'Processed'";
                            cmd.Parameters.AddWithValue("@UserID", currentManager);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countCompleted = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
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


            return countCompleted;

        }
        private int populateDashboardArranged()
        {
            int countCompleted = 0;

            try
            {
                string currentManager = Session["userID"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentManager)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelReqStatus = 'Arranged'";
                            cmd.Parameters.AddWithValue("@UserID", currentManager);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countCompleted = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
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


            return countCompleted;

        }

        private int populateDashboardCancelled()
        {
            int countCancelled = 0;

            try
            {
                string currentManager = Session["userID"]?.ToString(); // Null-conditional operator added

                if (!string.IsNullOrEmpty(currentManager)) // Check if currentDU is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT COUNT(*) FROM travelRequest WHERE travelReqStatus = 'Cancelled'";
                            cmd.Parameters.AddWithValue("@UserID", currentManager);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                countCancelled = Convert.ToInt32(result);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
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


            return countCancelled;

        }
        //END POPULATE NUMBERS IN THE DASHBOARD

        //to get the status clicked from the dashboard
        protected void approved_Click(object sender, EventArgs e)
        {
            // Cast the sender object to a Button
            Button clickedButton = (Button)sender;

            string clicked = clickedButton.ID;


            if (clicked == "Approved")
            {
                string status = clicked;
                Session["reqStatus"] = status;

                Response.Write("<script> window.location.href = 'TravelRequests.aspx'; </script>");


            }
            else if (clicked == "Completed")
            {
                string status = clicked;
                Session["reqStatus"] = status;

                Response.Write("<script> window.location.href = 'TravelRequests.aspx'; </script>");

            }
            else if (clicked == "Processing")
            {
                string status = clicked;
                Session["reqStatus"] = status;

                Response.Write("<script>window.location.href = 'TravelRequests.aspx'; </script>");

            }
            else if (clicked == "Arranged")
            {
                string status = clicked;
                Session["reqStatus"] = status;
                
                Response.Write("<script>window.location.href = 'TravelRequests.aspx'; </script>");

            }


        }

        protected void visaPending_Click(object sender, EventArgs e)
        {
            // Cast the sender object to a Button
            Button clickedButton = (Button)sender;

            string clicked = clickedButton.ID;


            if (clicked == "visaPending")
            {
                string status = "Pending";
                Session["VreqStatus"] = status;

                Response.Write("<script> window.location.href = 'VisaApplication.aspx'; </script>");


            }
            else if (clicked == "visaProcessing")
            {
                string status = "Processing";
                Session["VreqStatus"] = status;

                Response.Write("<script>window.location.href = 'VisaApplication.aspx'; </script>");

            }
            else if (clicked == "visaCompleted")
            {
                string status = "Completed";
                Session["VreqStatus"] = status;

                Response.Write("<script>window.location.href = 'VisaApplication.aspx'; </script>");

            }
            else if (clicked == "visaGranted")
            {
                string status = "Granted";
                Session["VreqStatus"] = status;

                Response.Write("<script>window.location.href = 'VisaApplication.aspx'; </script>");

            }

        }
    }
}