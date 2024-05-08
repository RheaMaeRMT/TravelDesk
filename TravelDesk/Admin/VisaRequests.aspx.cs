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
    public partial class VisaRequests : System.Web.UI.Page
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
                DisplayVisaReq();
            }
        }

        private void DisplayVisaReq()
        {
            try
            {
                // Get the ID of the clicked request from the session
                string requestId = Session["clickedVRequest"].ToString();

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
                                        SELECT * FROM travelVisa WHERE visaReqID = @RequestId AND visaDraftStat = 'No'";


                            cmd.Parameters.AddWithValue("@RequestId", requestId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    uploadBlock.Style["display"] = "block";
                                    pdfViewer.Style["display"] = "block";


                                    // Retrieve the request details from the reader
                                    string employeeID = reader["visaEmpID"].ToString();
                                    string employeeFname = reader["visaFname"].ToString();
                                    string employeeMname = reader["visaMname"].ToString();
                                    string employeeLname = reader["visaLname"].ToString();
                                    string employeeEmail = reader["visaEmail"].ToString();
                                    string employeeBdate = reader["visaBdate"].ToString();
                                    string employeeDU = reader["visaDU"].ToString();
                                    string employeePhone = reader["visaMobile"].ToString();
                                    string employeeLevel = reader["visaLevel"].ToString();
                                    string travelPurpose = reader["visaPurpose"].ToString();


                                    string proof = reader["visaApprovalPath"].ToString();
                                    string passport = reader["visaPassportPath"].ToString();
                                    string destination = reader["visaDestination"].ToString();
                                    string estTravelDate = reader["visaEstTravelDate"].ToString();
                                    string status = reader["visaReqStatus"].ToString();


                                    // Display or use the retrieved request details
                                    travellerName.Text = employeeFname + " " + employeeMname + " " + employeeLname + " - Visa Application Request" ;

                                    empID.Text = employeeID;
                                    empFName.Text = employeeFname + " " + employeeMname + " " + employeeLname;
                                    empLevel.Text = employeeLevel;
                                    empEmail.Text = employeeEmail;
                                    empMobile.Text = employeePhone;
                                    empDeptUnit.Text = employeeDU;

                                    employeePurpose.Text = travelPurpose;
                                    empDestination.Text = destination;
                                    pdfViewer.Src = proof;
                                    passportViewer.Src = passport;

                                    if (!string.IsNullOrEmpty(employeeBdate))
                                    {
                                        // Parse the date string into a DateTime object
                                        DateTime arrivalDateTime;
                                        if (DateTime.TryParse(employeeBdate, out arrivalDateTime))
                                        {
                                            // Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                            // Assign the formatted date to the TextBox
                                            empBdate.Text = formattedArrivalDate;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(estTravelDate))
                                    {
                                        // Parse the date string into a DateTime object
                                        DateTime arrivalDateTime;
                                        if (DateTime.TryParse(estTravelDate, out arrivalDateTime))
                                        {
                                            // Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                            // Assign the formatted date to the TextBox
                                            EmpestTravelDate.Text = formattedArrivalDate;
                                        }
                                    }


                                    Session["VreqStatus"] = status;

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
            Session.Remove("VreqStatus");

        }
        private void getTrackingStatus()
        {
            string currentStat = Session["VreqStatus"].ToString();
            currentStatus.Text = currentStat;

            // Set boolean variables based on the value of currentStat
            bool requestSubmitted = currentStat == "Pending";
            bool processing = currentStat == "Processing";
            bool granted = currentStat == "Granted";
            bool completed = currentStat == "Completed";

            // Generate the script block with the values
            string script = @"
        <script>
            // Set the status of each stage (true for completed, false for uncompleted)
            var approved = " + requestSubmitted.ToString().ToLower() + @"; // Set value from server-side
            var processing = " + processing.ToString().ToLower() + @"; // Set value from server-side
            var arranged = " + granted.ToString().ToLower() + @"; // Set value from server-side
            var completed = " + completed.ToString().ToLower() + @"; // Set value from server-side

            // Update the appearance of circles based on the status
            if (approved) {
                document.getElementById('requestSubmittedCircle').classList.add('completed');
            }
            if (processing) {
                document.getElementById('requestSubmittedCircle').classList.add('completed');
                document.getElementById('processingCircle').classList.add('completed');
                document.querySelectorAll('.line')[0].classList.add('completed');
            }
            if (completed) {
                document.getElementById('requestSubmittedCircle').classList.add('completed');
                document.getElementById('processingCircle').classList.add('completed');
                document.querySelectorAll('.line')[0].classList.add('completed');
                document.querySelectorAll('.line')[1].classList.add('completed');
                document.getElementById('arrangedCircle').classList.add('completed');
            }
            if (arranged) {
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

    }
}