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
    public partial class internationalRequestDetails : System.Web.UI.Page
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
                                        SELECT tr.*, 
                                            rt.routeOFrom, rt.routeOTo, rt.routeODate,
                                            rt.routeR1From, rt.routeR1To, 
                                            rt.routeRdepart, rt.routeRreturn,
                                            rt.routeM1From, rt.routeM1To, rt.routeM1ToDate, 
                                            rt.routeM2From, rt.routeM2To, rt.routeM2ToDate, 
                                            rt.routeM3From, rt.routeM3To, rt.routeM3ToDate,
                                            rt.routeM4From, rt.routeM4To, rt.routeM4ToDate, 
                                            rt.routeM5From, rt.routeM5To, rt.routeM5ToDate
                                                                                                                                   
                                          FROM travelRequest tr
                                          LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
                                          WHERE tr.travelRequestID = @RequestId AND travelDraftStat = 'No'";


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



                                    //FOR FLIGHT DETAILS - ROUTE
                                    string oneFrom = reader["routeOFrom"] != DBNull.Value ? reader["routeOFrom"].ToString() : "";
                                    string oneTo = reader["routeOTo"] != DBNull.Value ? reader["routeOTo"].ToString() : "";
                                    string oneDate = reader["routeODate"] != DBNull.Value ? reader["routeODate"].ToString() : "";

                                    string r1From = reader["routeR1From"] != DBNull.Value ? reader["routeR1From"].ToString() : "";
                                    string r1To = reader["routeR1To"] != DBNull.Value ? reader["routeR1To"].ToString() : "";
                                    string r1depart = reader["routeRdepart"] != DBNull.Value ? reader["routeRdepart"].ToString() : "";
                                    string r1return = reader["routeRreturn"] != DBNull.Value ? reader["routeRreturn"].ToString() : "";


                                    string mul1From = reader["routeM1From"] != DBNull.Value ? reader["routeM1From"].ToString() : "";
                                    string mul1To = reader["routeM1To"] != DBNull.Value ? reader["routeM1To"].ToString() : "";
                                    string mul1ToDate = reader["routeM1ToDate"] != DBNull.Value ? reader["routeM1ToDate"].ToString() : "";

                                    string mul2From = reader["routeM2From"] != DBNull.Value ? reader["routeM2From"].ToString() : "";
                                    string mul2To = reader["routeM2To"] != DBNull.Value ? reader["routeM2To"].ToString() : "";
                                    string mul2ToDate = reader["routeM2ToDate"] != DBNull.Value ? reader["routeM2ToDate"].ToString() : "";

                                    string mul3From = reader["routeM3From"] != DBNull.Value ? reader["routeM3From"].ToString() : "";
                                    string mul3To = reader["routeM3To"] != DBNull.Value ? reader["routeM3To"].ToString() : "";
                                    string mul3ToDate = reader["routeM3ToDate"] != DBNull.Value ? reader["routeM3ToDate"].ToString() : "";

                                    string mul4From = reader["routeM4From"] != DBNull.Value ? reader["routeM4From"].ToString() : "";
                                    string mul4To = reader["routeM4To"] != DBNull.Value ? reader["routeM4To"].ToString() : "";
                                    string mul4ToDate = reader["routeM4ToDate"] != DBNull.Value ? reader["routeM4ToDate"].ToString() : "";

                                    string mul5From = reader["routeM5From"] != DBNull.Value ? reader["routeM5From"].ToString() : "";
                                    string mul5To = reader["routeM5To"] != DBNull.Value ? reader["routeM5To"].ToString() : "";
                                    string mul5ToDate = reader["routeM5ToDate"] != DBNull.Value ? reader["routeM5ToDate"].ToString() : "";

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
                                    flightOptions.Text = flight;
                                    employeePurpose.Text = travelPurpose;
                                    employeeRemarks.Text = remarks;

                                    if (!string.IsNullOrEmpty(empBdate))
                                    {
                                        // Parse the date string into a DateTime object
                                        DateTime arrivalDateTime;
                                        if (DateTime.TryParse(empBdate, out arrivalDateTime))
                                        {
                                            // Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                            // Assign the formatted date to the TextBox
                                            employeeBdate.Text = formattedArrivalDate;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(oneFrom))
                                    {
                                        oneWaynput.Style["display"] = "block";
                                        onewayFrom.Text = oneFrom;
                                        onewayTo.Text = oneTo;

                                        if (!string.IsNullOrEmpty(oneDate))
                                        {
                                            // Parse the date string into a DateTime object
                                            DateTime arrivalDateTime;
                                            if (DateTime.TryParse(oneDate, out arrivalDateTime))
                                            {
                                                // Format the DateTime object into the desired format
                                                string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                // Assign the formatted date to the TextBox
                                                onewayDate.Text = formattedArrivalDate;
                                            }
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(r1From))
                                    {
                                        roundTripInput.Style["display"] = "block";
                                        round1From.Text = r1From;
                                        round1To.Text = r1To;

                                        if (!string.IsNullOrEmpty(r1return))
                                        {
                                            // Parse the date string into a DateTime object
                                            DateTime arrivalDateTime;
                                            if (DateTime.TryParse(r1return, out arrivalDateTime))
                                            {
                                                // Format the DateTime object into the desired format
                                                string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                // Assign the formatted date to the TextBox
                                                round2return.Text = formattedArrivalDate;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(r1depart))
                                        {
                                            // Parse the date string into a DateTime object
                                            DateTime arrivalDateTime;
                                            if (DateTime.TryParse(r1depart, out arrivalDateTime))
                                            {
                                                // Format the DateTime object into the desired format
                                                string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                // Assign the formatted date to the TextBox
                                                round2departure.Text = formattedArrivalDate;
                                            }
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(mul1From) && (!string.IsNullOrEmpty(mul1To)))
                                    {
                                        multipleInput.Style["display"] = "block";
                                        if (!string.IsNullOrEmpty(mul2From) && (!string.IsNullOrEmpty(mul2To)))
                                        {
                                            TextBox7.Text = mul1From;
                                            //TextBox11.Text = mul1FromDate;
                                            TextBox8.Text = mul1To;
                                            //TextBox12.Text = mul1ToDate;
                                            TextBox9.Text = mul2From;
                                            //TextBox13.Text = mul2FromDate;
                                            TextBox10.Text = mul2To;
                                            //TextBox14.Text = mul2ToDate;

                                            if (!string.IsNullOrEmpty(mul1ToDate))
                                            {
                                                // Parse the date string into a DateTime object
                                                DateTime arrivalDateTime;
                                                if (DateTime.TryParse(mul1ToDate, out arrivalDateTime))
                                                {
                                                    // Format the DateTime object into the desired format
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                    // Assign the formatted date to the TextBox
                                                    TextBox12.Text = formattedArrivalDate;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(mul2ToDate))
                                            {
                                                // Parse the date string into a DateTime object
                                                DateTime arrivalDateTime;
                                                if (DateTime.TryParse(mul2ToDate, out arrivalDateTime))
                                                {
                                                    // Format the DateTime object into the desired format
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                    // Assign the formatted date to the TextBox
                                                    TextBox14.Text = formattedArrivalDate;
                                                }
                                            }


                                            if (!string.IsNullOrEmpty(mul3From) && (!string.IsNullOrEmpty(mul3To)))
                                            {
                                                additionalFields.Style["display"] = "block";
                                                TextBox15.Text = mul3From;
                                                //TextBox16.Text = mul3FromDate;
                                                TextBox17.Text = mul3To;
                                                //TextBox18.Text = mul3ToDate;


                                                if (!string.IsNullOrEmpty(mul3ToDate))
                                                {
                                                    // Parse the date string into a DateTime object
                                                    DateTime arrivalDateTime;
                                                    if (DateTime.TryParse(mul3ToDate, out arrivalDateTime))
                                                    {
                                                        // Format the DateTime object into the desired format
                                                        string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                        // Assign the formatted date to the TextBox
                                                        TextBox18.Text = formattedArrivalDate;
                                                    }
                                                }


                                                if (!string.IsNullOrEmpty(mul4From) && (!string.IsNullOrEmpty(mul4To)))
                                                {
                                                    destination4.Style["display"] = "block";
                                                    TextBox27.Text = mul4From;
                                                    //TextBox28.Text = mul4FromDate;
                                                    TextBox29.Text = mul4To;
                                                    //TextBox30.Text = mul4ToDate;

                                                    if (!string.IsNullOrEmpty(mul4ToDate))
                                                    {
                                                        // Parse the date string into a DateTime object
                                                        DateTime arrivalDateTime;
                                                        if (DateTime.TryParse(mul4ToDate, out arrivalDateTime))
                                                        {
                                                            // Format the DateTime object into the desired format
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                            // Assign the formatted date to the TextBox
                                                            TextBox30.Text = formattedArrivalDate;
                                                        }
                                                    }

                                                }
                                                if (!string.IsNullOrEmpty(mul5From) && (!string.IsNullOrEmpty(mul5To)))
                                                {
                                                    destination5.Style["display"] = "block";
                                                    TextBox19.Text = mul5From;
                                                    //TextBox20.Text = mul5FromDate;
                                                    TextBox21.Text = mul5To;
                                                    //TextBox22.Text = mul5ToDate;

                                                    if (!string.IsNullOrEmpty(mul5ToDate))
                                                    {
                                                        // Parse the date string into a DateTime object
                                                        DateTime arrivalDateTime;
                                                        if (DateTime.TryParse(mul5ToDate, out arrivalDateTime))
                                                        {
                                                            // Format the DateTime object into the desired format
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                            // Assign the formatted date to the TextBox
                                                            TextBox22.Text = formattedArrivalDate;
                                                        }
                                                    }

                                                }


                                            }

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
            bool requestSubmitted = currentStat == "Approved";
            bool processing = currentStat == "Processing";
            bool arranged = currentStat == "Arranged";
            bool completed = currentStat == "Completed";

            // Generate the script block with the values
            string script = @"
        <script>
            // Set the status of each stage (true for completed, false for uncompleted)
            var approved = " + requestSubmitted.ToString().ToLower() + @"; // Set value from server-side
            var processing = " + processing.ToString().ToLower() + @"; // Set value from server-side
            var arranged = " + arranged.ToString().ToLower() + @"; // Set value from server-side
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