using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Employee
{
    public partial class visaRequestDetails : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

            }
            else
            {
                DisplayRequest();
            }

        }
        private void DisplayRequest()
        {
            try
            {
                // Get the ID of the clicked request from the session
                string requestId = Session["clickedRequest"].ToString();

                if (!string.IsNullOrEmpty(requestId))
                {
                    // Query the database to retrieve the request details based on the ID
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"
                                        SELECT * FROM travelRequest WHERE travelRequestID = @RequestId AND travelDraftStat = 'No'";


                            cmd.Parameters.AddWithValue("@RequestId", requestId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    uploadBlock.Style["display"] = "block";
                                    pdfViewer.Style["display"] = "block";


                                    // Retrieve the request details from the reader
                                    string travelFacility = reader["travelHomeFacility"].ToString();
                                    string empID = reader["travelEmpID"].ToString();
                                    string empFname = reader["travelFname"].ToString();
                                    string empMname = reader["travelMname"].ToString();
                                    string empLname = reader["travelLname"].ToString();
                                    string empEmail = reader["travelEmail"].ToString();
                                    string empBdate = reader["travelBdate"].ToString();
                                    string empDU = reader["travelDU"].ToString();
                                    string empProjCode = reader["travelProjectCode"].ToString();
                                    string empPhone = reader["travelMobilenum"].ToString();
                                    string empLevel = reader["travelLevel"].ToString();
                                    string travelPurpose = reader["travelPurpose"].ToString();
                                    string flight = reader["travelOptions"].ToString();

                                    string proof = reader["travelProofPath"].ToString();
                                    string passport = reader["travelPassportPath"].ToString();
                                    string remarks = reader["travelRemarks"].ToString();
                                    string travelDestination = reader["travelDestination"].ToString();
                                    string estimatedTravel = reader["travelEstdate"].ToString();




                                    // Display or use the retrieved request details
                                    homeFacility.Text = travelFacility;
                                    employeeID.Text = empID;
                                    employeeFName.Text = empFname;
                                    employeeMName.Text = empMname;
                                    employeeLName.Text = empLname;
                                    employeeEmail.Text = empEmail;
                                    employeeDU.Text = empDU;
                                    employeeProjCode.Text = empProjCode;
                                    employeePhone.Text = empPhone;
                                    employeeLevel.Text = empLevel;
                                    pdfViewer.Src = proof;
                                    passportViewer.Src = passport;
                                    employeePurpose.Text = travelPurpose;
                                    employeeDestination.Text = travelDestination;
                                    estTravelDate.Text = estimatedTravel;

                                    if (!string.IsNullOrEmpty(estimatedTravel))
                                    {
                                        // Parse the date string into a DateTime object
                                        DateTime arrivalDateTime;
                                        if (DateTime.TryParse(estimatedTravel, out arrivalDateTime))
                                        {
                                            // Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                            // Assign the formatted date to the TextBox
                                            estTravelDate.Text = formattedArrivalDate;
                                        }
                                    }

                                    string status = reader["travelReqStatus"].ToString();

                                    Session["currentStatus"] = status;

                                    //PROCEED TO GET THE STATUS AND DISPLAY IN TRACKING
                                    getTrackingStatus();
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
                else
                {
                    // Handle the case where the request ID stored in the session is null or empty
                    Response.Write("<script>alert('Invalid request ID.')</script>");
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred while retrieving the request details.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }
        }

        private void getTrackingStatus()
        {
            string currentStat = Session["currentStatus"].ToString();
            currentStatus.Text = currentStat;

            // Set boolean variables based on the value of currentStat
            bool requestSubmitted = currentStat == "New";
            bool processing = currentStat == "In-progress";
            bool completed = currentStat == "Completed";
            bool closed = currentStat == "Closed";

            // Generate the script block with the values
            string script = @"
        <script>
            // Set the status of each stage (true for completed, false for uncompleted)
            var approved = " + requestSubmitted.ToString().ToLower() + @"; // Set value from server-side
            var processing = " + processing.ToString().ToLower() + @"; // Set value from server-side
            var arranged = " + completed.ToString().ToLower() + @"; // Set value from server-side
            var completed = " + closed.ToString().ToLower() + @"; // Set value from server-side

            // Update the appearance of circles based on the status
            if (approved) {
                document.getElementById('requestSubmittedCircle').classList.add('completed');
            }
            if (processing) {
                document.getElementById('requestSubmittedCircle').classList.add('completed');
                document.getElementById('processingCircle').classList.add('completed');
                document.querySelectorAll('.line')[0].classList.add('completed');
            }
            if (arranged) {
                document.getElementById('requestSubmittedCircle').classList.add('completed');
                document.getElementById('processingCircle').classList.add('completed');
                document.querySelectorAll('.line')[0].classList.add('completed');
                document.querySelectorAll('.line')[1].classList.add('completed');
                document.getElementById('arrangedCircle').classList.add('completed');
            }
            if (completed) {
                document.getElementById('requestSubmittedCircle').classList.add('completed');
                document.getElementById('processingCircle').classList.add('completed');
                document.querySelectorAll('.line')[0].classList.add('completed');
                document.querySelectorAll('.line')[1].classList.add('completed');
                document.getElementById('arrangedCircle').classList.add('completed');
                document.querySelectorAll('.line')[2].classList.add('completed');
                document.getElementById('completedCircle').classList.add('completed');
            }
        </script>
    ";

            // Inject the script into the page
            ClientScript.RegisterStartupScript(this.GetType(), "trackingStatus", script);
        }


        protected void updateRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string clickedRequest = Session["clickedRequest"]?.ToString(); // Null-conditional operator added


                if (!string.IsNullOrEmpty(clickedRequest)) // Check if clickedRequest is not null or empty
                {
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM travelRequest WHERE travelRequestID = @request";
                            cmd.Parameters.AddWithValue("@request", clickedRequest);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // GET THE VALUE OF THE STATUS
                                    string approvalStatus = reader["travelApprovalStat"].ToString();
                                    if (approvalStatus == "Approved" || approvalStatus == "Processing" || approvalStatus == "Completed" || approvalStatus == "Arranged")
                                    {
                                        Response.Write("<script>alert('Update Unavailable: This travel request is already " + approvalStatus + " !');</script>");
                                    }
                                    else if (approvalStatus == "Pending")
                                    {
                                        Response.Redirect("modifyDomesticRequests.aspx");

                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    // Redirect to login page if clickedRequest is null or empty
                    Response.Write("<script>alert('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception
                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }

        }
    }
}