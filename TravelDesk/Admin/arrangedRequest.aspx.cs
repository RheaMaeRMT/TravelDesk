using iTextSharp.text;
using iTextSharp.text.pdf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Text;
using System.Web.UI;
using MimeKit;
using System.Text.RegularExpressions;
using System.Web;

namespace TravelDesk.Admin
{
    public partial class arrangedRequest : System.Web.UI.Page
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
                // Check if the session variable "clickedRequest" is not null
                if (Session["clickedRequest"] != null)
                { 
                    // Access the session variable and convert it to a string
                    string request = Session["clickedRequest"].ToString();

                    if (!string.IsNullOrEmpty(request))
                    {
                        populateEmployeeDetails();
                        DisplayArrangement();
                        //checkIFexported();
                        
                    }
                }             
                else
                {
                    Response.Write("<script> window.location.href = '../LoginPage.aspx'; </script>");

                }

            }
        }
        private void checkIFexported()
        {
            // Retrieve ID and name for filename
            string ID = Session["clickedRequest"].ToString();
            string name = employeeName.Text;

            // Construct filename
            string filename = name + "_" + ID + ".pdf";

            // Save the PDF to the folder /PDFs/travelArrangements
            string folderPath = Server.MapPath("~/PDFs/travelArrangements");
            string filePath = Path.Combine(folderPath, filename);
            // Check if the file exists
            if (File.Exists(filePath))
            {
                exportPDF.Style["display"] = "none";
                sendEmailbtn.Style["display"] = "block";
            }
            else
            {
                exportPDF.Style["display"] = "block";
                sendEmailbtn.Style["display"] = "none";

            }

        }
        private void populateEmployeeDetails()
        {
            try
            {
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
                            cmd.CommandText = "SELECT * FROM travelRequest WHERE travelRequestID = @RequestId";
                            cmd.Parameters.AddWithValue("@RequestId", requestId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Retrieve the request details from the reader
                                    string facility = reader["travelHomeFacility"].ToString();
                                    string name = reader["travelFname"].ToString() + " " + reader["travelMname"].ToString() + " " + reader["travelLname"].ToString();
                                    string userID = reader["travelEmpID"].ToString();
                                    string mobile = reader["travelMobilenum"].ToString();
                                    string level = reader["travelLevel"].ToString();
                                    string email = reader["travelEmail"].ToString();
                                    string status = reader["travelProcessStat"].ToString();
                                    // Retrieve other request details as needed

                                    // Display or use the retrieved request details
                                    employeeID.Text = userID;
                                    employeeName.Text = name;
                                    homeFacility.Text = facility;
                                    employeePhone.Text = mobile;
                                    employeeLevel.Text = level;
                                    employeeEmail.Text = email + "?";
                                    travellerName.Text = " Travel Arrangement for " + name;
                                    statusLabel.Text = "In-Progress:" + " " + status;

                                    Session["userEmail"] = email;
                                    Session["travellerName"] = name;

                                    // Assign other request details to corresponding controls
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
        private void DisplayArrangement()
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
                           cmd.CommandText = "SELECT * FROM travelArranged WHERE arrangedTravelReqID = @travelID";
                           cmd.Parameters.AddWithValue("@travelID", requestId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {

                                    // Retrieve the request details from the reader
                                    string accomodationID = reader["arrangedAccomodationID"].ToString();
                                    string flightID = reader["arrangedFlightID"].ToString();
                                    string transfersID = reader["arrangedTransferID"].ToString();
                                    string travelrequirements = reader["arrangedRequirements"].ToString();
                                    string noted = reader["arrangedNotes"].ToString();
                                    string addRemarks = reader["arrangedRemarks"].ToString();

                                    //SAVE THE IDs to the SESSION
                                    Session["accomodation"] = accomodationID;
                                    Session["flight"] = flightID;
                                    Session["transfers"] = transfersID;

                                    // Display or use the retrieved request details
                                    requirements.Text = travelrequirements;
                                    additionalNotes.Text = noted;
                                    remarks.Text = addRemarks;

                                    getAccomodationDetails();
                                    getFlightDetails();
                                    getTransfersDetails();
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

        private void getAccomodationDetails()
        {
            if (Session["clickedRequest"] != null && Session["accomodation"] != null)
            {
                string accomodation = Session["accomodation"].ToString();

                // Query the database to retrieve the request details based on the ID
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM travelAccomodation WHERE arrangedAccID = @travelID";
                        cmd.Parameters.AddWithValue("@travelID", accomodation);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Check if there are any rows returned by the query
                            {
                                string accomodationType = reader["arrangeAccomodation"].ToString();
                                //1ST HOTEL
                                string Name = reader["arrangeHotelName"] != DBNull.Value ? reader["arrangeHotelName"].ToString() : "";
                                string Address = reader["arrangeHotelAdd"] != DBNull.Value ? reader["arrangeHotelAdd"].ToString() : "";
                                string from = reader["arrangeHotelFrom"] != DBNull.Value ? reader["arrangeHotelFrom"].ToString() : "";
                                string to = reader["arrangeHotelTo"] != DBNull.Value ? reader["arrangeHotelTo"].ToString() : "";
                                string contact = reader["arrangeHotelPhone"] != DBNull.Value ? reader["arrangeHotelPhone"].ToString() : "";

                                //2ND HOTEL
                                string Name2 = reader["arrangeHotel2Name"] != DBNull.Value ? reader["arrangeHotel2Name"].ToString() : "";
                                string Address2 = reader["arrangeHotel2Add"] != DBNull.Value ? reader["arrangeHotel2Add"].ToString() : "";
                                string from2 = reader["arrangeHotel2From"] != DBNull.Value ? reader["arrangeHotel2From"].ToString() : "";
                                string to2 = reader["arrangeHotel2To"] != DBNull.Value ? reader["arrangeHotel2To"].ToString() : "";
                                string contact2 = reader["arrangeHotel2Phone"] != DBNull.Value ? reader["arrangeHotel2Phone"].ToString() : "";

                                //3RD HOTEL
                                string Name3 = reader["arrangeHotel3Name"] != DBNull.Value ? reader["arrangeHotel3Name"].ToString() : "";
                                string Address3 = reader["arrangeHotel3Add"] != DBNull.Value ? reader["arrangeHotel3Add"].ToString() : "";
                                string from3 = reader["arrangeHotel3From"] != DBNull.Value ? reader["arrangeHotel3From"].ToString() : "";
                                string to3 = reader["arrangeHotel3To"] != DBNull.Value ? reader["arrangeHotel3To"].ToString() : "";
                                string contact3 = reader["arrangeHotel3Phone"] != DBNull.Value ? reader["arrangeHotel3Phone"].ToString() : "";

                                //4TH HOTEL
                                string Name4 = reader["arrangeHotel4Name"] != DBNull.Value ? reader["arrangeHotel4Name"].ToString() : "";
                                string Address4 = reader["arrangeHotel4Add"] != DBNull.Value ? reader["arrangeHotel4Add"].ToString() : "";
                                string from4 = reader["arrangeHotel4From"] != DBNull.Value ? reader["arrangeHotel4From"].ToString() : "";
                                string to4 = reader["arrangeHotel4To"] != DBNull.Value ? reader["arrangeHotel4To"].ToString() : "";
                                string contact4 = reader["arrangeHotel4Phone"] != DBNull.Value ? reader["arrangeHotel4Phone"].ToString() : "";

                                //5TH HOTEL
                                string Name5 = reader["arrangeHotel5Name"] != DBNull.Value ? reader["arrangeHotel5Name"].ToString() : "";
                                string Address5 = reader["arrangeHotel5Add"] != DBNull.Value ? reader["arrangeHotel5Add"].ToString() : "";
                                string from5 = reader["arrangeHotel5From"] != DBNull.Value ? reader["arrangeHotel5From"].ToString() : "";
                                string to5 = reader["arrangeHotel5To"] != DBNull.Value ? reader["arrangeHotel5To"].ToString() : "";
                                string contact5 = reader["arrangeHotel5Phone"] != DBNull.Value ? reader["arrangeHotel5Phone"].ToString() : "";



                                accomodations.Text = accomodationType;

                                if (accomodationType == "c/o Traveller")
                                {
                                    careofEmployee.Style["display"] = "block";
                                    employeeHotel.Text = Name;
                                }
                                else
                                {
                                    hotelAccomodations.Style["display"] = "block";
                                    hotel.Text = Name;
                                    hotelAddress.Text = Address;
                                    hotelContact.Text = contact;

                                    if (!string.IsNullOrEmpty(from))
                                    {
                                        //Parse the date string into a DateTime object
                                        DateTime fromDate;
                                        DateTime toDate;

                                        if (DateTime.TryParse(from, out fromDate))
                                        {
                                            //Format the DateTime object into the desired format
                                            string formattedFromDate = fromDate.ToString("MMMM dd");

                                            if (DateTime.TryParse(to, out toDate))
                                            {
                                                //Format the DateTime object into the desired format
                                                string formattedToDate = toDate.ToString("MMMM dd");

                                                //Assign the formatted date to the TextBox
                                                durationFrom.Text = formattedFromDate + " - " + formattedToDate;
                                            }

                                        }

                                    }
                                    if (!string.IsNullOrEmpty(Name2))
                                    {
                                        hotel2Acc.Style["display"] = "block";
                                        hotel2.Text = Name2;
                                        hotelAddress2.Text = Address2;
                                        hotelContact2.Text = contact2;

                                        if (!string.IsNullOrEmpty(from2))
                                        {
                                            //Parse the date string into a DateTime object
                                            DateTime fromDate;
                                            DateTime toDate;

                                            if (DateTime.TryParse(from2, out fromDate))
                                            {
                                                //Format the DateTime object into the desired format
                                                string formattedFromDate = fromDate.ToString("MMMM dd");

                                                if (DateTime.TryParse(from2, out toDate))
                                                {
                                                    //Format the DateTime object into the desired format
                                                    string formattedToDate = toDate.ToString("MMMM dd");

                                                    //Assign the formatted date to the TextBox
                                                    durationFrom2.Text = formattedFromDate + " - " + formattedToDate;
                                                }

                                            }

                                        }
                                    }
                                    else
                                    {
                                        hotel2Acc.Style["display"] = "none";

                                    }
                                    if (!string.IsNullOrEmpty(Name3))
                                    {
                                        hotel3Acc.Style["display"] = "block";
                                        hotel3.Text = Name3;
                                        hotelAddress3.Text = Address3;
                                        hotelContact3.Text = contact3;

                                        if (!string.IsNullOrEmpty(from3))
                                        {
                                            //Parse the date string into a DateTime object
                                            DateTime fromDate;
                                            DateTime toDate;

                                            if (DateTime.TryParse(from3, out fromDate))
                                            {
                                                //Format the DateTime object into the desired format
                                                string formattedFromDate = fromDate.ToString("MMMM dd");

                                                if (DateTime.TryParse(from3, out toDate))
                                                {
                                                    //Format the DateTime object into the desired format
                                                    string formattedToDate = toDate.ToString("MMMM dd");

                                                    //Assign the formatted date to the TextBox
                                                    durationFrom3.Text = formattedFromDate + " - " + formattedToDate;
                                                }

                                            }

                                        }
                                    }
                                    if (!string.IsNullOrEmpty(Name4))
                                    {
                                        hotel4Acc.Style["display"] = "block";
                                        hotel4.Text = Name4;
                                        hotelAddress4.Text = Address4;
                                        hotelContact4.Text = contact4;

                                        if (!string.IsNullOrEmpty(from4))
                                        {
                                            //Parse the date string into a DateTime object
                                            DateTime fromDate;
                                            DateTime toDate;

                                            if (DateTime.TryParse(from4, out fromDate))
                                            {
                                                //Format the DateTime object into the desired format
                                                string formattedFromDate = fromDate.ToString("MMMM dd");

                                                if (DateTime.TryParse(from4, out toDate))
                                                {
                                                    //Format the DateTime object into the desired format
                                                    string formattedToDate = toDate.ToString("MMMM dd");

                                                    //Assign the formatted date to the TextBox
                                                    durationFrom3.Text = formattedFromDate + " - " + formattedToDate;
                                                }

                                            }

                                        }
                                    }
                                    if (!string.IsNullOrEmpty(Name5))
                                    {
                                        hotel5Acc.Style["display"] = "block";
                                        hotel5.Text = Name5;
                                        hotelAddress5.Text = Address5;
                                        hotelContact5.Text = contact5;

                                        if (!string.IsNullOrEmpty(from5))
                                        {
                                            //Parse the date string into a DateTime object
                                            DateTime fromDate;
                                            DateTime toDate;

                                            if (DateTime.TryParse(from5, out fromDate))
                                            {
                                                //Format the DateTime object into the desired format
                                                string formattedFromDate = fromDate.ToString("MMMM dd");

                                                if (DateTime.TryParse(from5, out toDate))
                                                {
                                                    //Format the DateTime object into the desired format
                                                    string formattedToDate = toDate.ToString("MMMM dd");

                                                    //Assign the formatted date to the TextBox
                                                    durationFrom5.Text = formattedFromDate + " - " + formattedToDate;
                                                }

                                            }

                                        }
                                    }


                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Something went wrong: Accomodations ')</script>");
                            }
                        }

                    }
                }
            }
        }
        private void getFlightDetails()
        {
            if (Session["clickedRequest"] != null && Session["flight"] != null)
            {
                string flight = Session["flight"].ToString();

                // Query the database to retrieve the request details based on the ID
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM travelFlight WHERE travelFlightID = @travelID";
                        cmd.Parameters.AddWithValue("@travelID", flight);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string airline = reader["travelAirline"].ToString();
                                string mul1Flight = reader["routeM1Flight"] != DBNull.Value ? reader["routeM1Flight"].ToString() : "";
                                string mul1From = reader["routeM1From"] != DBNull.Value ? reader["routeM1From"].ToString() : "";
                                string mul1FromDate = reader["routeM1FromDate"] != DBNull.Value ? reader["routeM1FromDate"].ToString() : "";
                                string mul1To = reader["routeM1To"] != DBNull.Value ? reader["routeM1To"].ToString() : "";
                                string mul1ETD = reader["routeM1ETD"] != DBNull.Value ? reader["routeM1ETD"].ToString() : "";
                                string mul1ETA = reader["routeM1ETA"] != DBNull.Value ? reader["routeM1ETA"].ToString() : "";

                                string mul2Flight = reader["routeM2Flight"] != DBNull.Value ? reader["routeM2Flight"].ToString() : "";
                                string mul2From = reader["routeM2From"] != DBNull.Value ? reader["routeM2From"].ToString() : "";
                                string mul2FromDate = reader["routeM2FromDate"] != DBNull.Value ? reader["routeM2FromDate"].ToString() : "";
                                string mul2To = reader["routeM2To"] != DBNull.Value ? reader["routeM2To"].ToString() : "";
                                string mul2ETD = reader["routeM2ETD"] != DBNull.Value ? reader["routeM2ETD"].ToString() : "";
                                string mul2ETA = reader["routeM2ETA"] != DBNull.Value ? reader["routeM2ETA"].ToString() : "";

                                string mul3Flight = reader["routeM3Flight"] != DBNull.Value ? reader["routeM3Flight"].ToString() : "";
                                string mul3From = reader["routeM3From"] != DBNull.Value ? reader["routeM3From"].ToString() : "";
                                string mul3FromDate = reader["routeM3FromDate"] != DBNull.Value ? reader["routeM3FromDate"].ToString() : "";
                                string mul3To = reader["routeM3To"] != DBNull.Value ? reader["routeM3To"].ToString() : "";
                                string mul3ETD = reader["routeM3ETD"] != DBNull.Value ? reader["routeM3ETD"].ToString() : "";
                                string mul3ETA = reader["routeM3ETA"] != DBNull.Value ? reader["routeM3ETA"].ToString() : "";

                                string mul4Flight = reader["routeM4Flight"] != DBNull.Value ? reader["routeM4Flight"].ToString() : "";
                                string mul4From = reader["routeM4From"] != DBNull.Value ? reader["routeM4From"].ToString() : "";
                                string mul4FromDate = reader["routeM4FromDate"] != DBNull.Value ? reader["routeM4FromDate"].ToString() : "";
                                string mul4To = reader["routeM4To"] != DBNull.Value ? reader["routeM4To"].ToString() : "";
                                string mul4ETD = reader["routeM4ETD"] != DBNull.Value ? reader["routeM4ETD"].ToString() : "";
                                string mul4ETA = reader["routeM4ETA"] != DBNull.Value ? reader["routeM4ETA"].ToString() : "";

                                string mul5Flight = reader["routeM5Flight"] != DBNull.Value ? reader["routeM5Flight"].ToString() : "";
                                string mul5From = reader["routeM5From"] != DBNull.Value ? reader["routeM5From"].ToString() : "";
                                string mul5FromDate = reader["routeM5FromDate"] != DBNull.Value ? reader["routeM5FromDate"].ToString() : "";
                                string mul5To = reader["routeM5To"] != DBNull.Value ? reader["routeM5To"].ToString() : "";
                                string mul5ETD = reader["routeM5ETD"] != DBNull.Value ? reader["routeM5ETD"].ToString() : "";
                                string mul5ETA = reader["routeM5ETA"] != DBNull.Value ? reader["routeM5ETA"].ToString() : "";
                                


                                //DISPLAY TO TEXTBOXES
                                bookedairline.Text = airline;
                                //ROUTE DISPLAY START
                                r1From.Text = mul1From;
                                r1To.Text = mul1To;
                                r1Flight.Text = mul1Flight;

                                if (r1ETA.Text != null && r1ETD != null)
                                {
                                    DateTime arrivalDateTime;
                                    DateTime departDateTime;
                                    if (DateTime.TryParse(mul1ETA, out arrivalDateTime))
                                    {
                                        string formattedArrivalDate = arrivalDateTime.ToString("HHmm");
                                        r1ETA.Text = formattedArrivalDate;

                                    }
                                    if (DateTime.TryParse(mul1ETD, out departDateTime))
                                    {
                                        string formattedArrivalDate = departDateTime.ToString("HHmm");
                                        r1ETD.Text = formattedArrivalDate;

                                    }

                                }

                                if (!string.IsNullOrEmpty(mul1FromDate))
                                {
                                    // Parse the date string into a DateTime object
                                    DateTime arrivalDateTime;
                                    if (DateTime.TryParse(mul1FromDate, out arrivalDateTime))
                                    {
                                        // Format the DateTime object into the desired format
                                        string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                        // Assign the formatted date to the TextBox
                                        r1FromDate.Text = formattedArrivalDate;
                                    }
                                }

                                if (!string.IsNullOrEmpty(mul2From) && (!string.IsNullOrEmpty(mul2To)))
                                {
                                    additional2routeFields.Style["display"] = "block";

                                    if (!string.IsNullOrEmpty(mul2From) && (!string.IsNullOrEmpty(mul2To)))
                                    {
                                        r2From.Text = mul2From;
                                        r2To.Text = mul2To;
                                        r2Flight.Text = mul2Flight;

                                        if (r2ETA.Text != null && r2ETD != null)
                                        {
                                            DateTime arrivalDateTime;
                                            DateTime departDateTime;
                                            if (DateTime.TryParse(mul2ETA, out arrivalDateTime))
                                            {
                                                string formattedArrivalDate = arrivalDateTime.ToString("HHmm");
                                                r2ETA.Text = formattedArrivalDate;

                                            }
                                            if (DateTime.TryParse(mul2ETD, out departDateTime))
                                            {
                                                string formattedArrivalDate = departDateTime.ToString("HHmm");
                                                r2ETD.Text = formattedArrivalDate;

                                            }

                                        }

                                        if (!string.IsNullOrEmpty(mul2FromDate))
                                        {
                                            // Parse the date string into a DateTime object
                                            DateTime arrivalDateTime;
                                            if (DateTime.TryParse(mul2FromDate, out arrivalDateTime))
                                            {
                                                // Format the DateTime object into the desired format
                                                string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                // Assign the formatted date to the TextBox
                                                r2FromDate.Text = formattedArrivalDate;
                                            }
                                        }


                                        if (!string.IsNullOrEmpty(mul3From) && (!string.IsNullOrEmpty(mul3To)))
                                        {
                                            additional3routeFields.Style["display"] = "block";
                                            r3From.Text = mul3From;
                                            //TextBox16.Text = mul3FromDate;
                                            r3To.Text = mul3To;
                                            r3Flight.Text = mul3Flight;

                                            if (r3ETA.Text != null && r3ETD != null)
                                            {
                                                DateTime arrivalDateTime;
                                                DateTime departDateTime;
                                                if (DateTime.TryParse(mul3ETA, out arrivalDateTime))
                                                {
                                                    string formattedArrivalDate = arrivalDateTime.ToString("HHmm");
                                                    r3ETA.Text = formattedArrivalDate;

                                                }
                                                if (DateTime.TryParse(mul3ETD, out departDateTime))
                                                {
                                                    string formattedArrivalDate = departDateTime.ToString("HHmm");
                                                    r3ETD.Text = formattedArrivalDate;

                                                }

                                            }

                                            if (!string.IsNullOrEmpty(mul3FromDate))
                                            {
                                                // Parse the date string into a DateTime object
                                                DateTime arrivalDateTime;
                                                if (DateTime.TryParse(mul3FromDate, out arrivalDateTime))
                                                {
                                                    // Format the DateTime object into the desired format
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                    // Assign the formatted date to the TextBox
                                                    r3FromDate.Text = formattedArrivalDate;
                                                }
                                            }


                                            if (!string.IsNullOrEmpty(mul4From) && (!string.IsNullOrEmpty(mul4To)))
                                            {
                                                additional4routeFields.Style["display"] = "block";
                                                r4From.Text = mul4From;
                                                //TextBox28.Text = mul4FromDate;
                                                r4To.Text = mul4To;
                                                r4Flight.Text = mul4Flight;

                                                if (r4ETA.Text != null && r4ETD != null)
                                                {
                                                    DateTime arrivalDateTime;
                                                    DateTime departDateTime;
                                                    if (DateTime.TryParse(mul4ETA, out arrivalDateTime))
                                                    {
                                                        string formattedArrivalDate = arrivalDateTime.ToString("HHmm");
                                                        r4ETA.Text = formattedArrivalDate;

                                                    }
                                                    if (DateTime.TryParse(mul4ETD, out departDateTime))
                                                    {
                                                        string formattedArrivalDate = departDateTime.ToString("HHmm");
                                                        r4ETD.Text = formattedArrivalDate;

                                                    }

                                                }

                                                if (!string.IsNullOrEmpty(mul4FromDate))
                                                {
                                                    // Parse the date string into a DateTime object
                                                    DateTime arrivalDateTime;
                                                    if (DateTime.TryParse(mul4FromDate, out arrivalDateTime))
                                                    {
                                                        // Format the DateTime object into the desired format
                                                        string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                        // Assign the formatted date to the TextBox
                                                        r4FromDate.Text = formattedArrivalDate;
                                                    }
                                                }

                                            }
                                            if (!string.IsNullOrEmpty(mul5From) && (!string.IsNullOrEmpty(mul5To)))
                                            {
                                                additional5routeFields.Style["display"] = "block";
                                                r5From.Text = mul5From;
                                                //TextBox20.Text = mul5FromDate;
                                                r5To.Text = mul5To;
                                                r5Flight.Text = mul1Flight;

                                                if (r5ETA.Text != null && r5ETD != null)
                                                {
                                                    DateTime arrivalDateTime;
                                                    DateTime departDateTime;
                                                    if (DateTime.TryParse(mul5ETA, out arrivalDateTime))
                                                    {
                                                        string formattedArrivalDate = arrivalDateTime.ToString("HHmm");
                                                        r5ETA.Text = formattedArrivalDate;

                                                    }
                                                    if (DateTime.TryParse(mul5ETD, out departDateTime))
                                                    {
                                                        string formattedArrivalDate = departDateTime.ToString("HHmm");
                                                        r5ETD.Text = formattedArrivalDate;

                                                    }

                                                }

                                                if (!string.IsNullOrEmpty(mul5FromDate))
                                                {
                                                    // Parse the date string into a DateTime object
                                                    DateTime arrivalDateTime;
                                                    if (DateTime.TryParse(mul5FromDate, out arrivalDateTime))
                                                    {
                                                        // Format the DateTime object into the desired format
                                                        string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                        // Assign the formatted date to the TextBox
                                                        r5FromDate.Text = formattedArrivalDate;
                                                    }
                                                }

                                            }


                                        }

                                    }
                                }
                                //ROUTE DISPLAY END
                            }
                            else
                            {
                                Response.Write("<script>alert('Something went wrong: Flight .')</script>");

                            }


                        }

                    }
                }
            }

        }
        private void getTransfersDetails()
        {
            if (Session["clickedRequest"] != null && Session["transfers"] != null)
            {
                string transfers = Session["transfers"].ToString();

                // Query the database to retrieve the request details based on the ID
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM travelTransfers WHERE arrangeTransferID = @travelID";
                        cmd.Parameters.AddWithValue("@travelID", transfers);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //TRANSFERS
                                string t1 = reader["arrangeTransfer1"] != DBNull.Value ? reader["arrangeTransfer1"].ToString() : "";
                                string t1Date = reader["arrangeTransfer1Date"] != DBNull.Value ? reader["arrangeTransfer1Date"].ToString() : "";

                                string t2 = reader["arrangeTransfer2"] != DBNull.Value ? reader["arrangeTransfer2"].ToString() : "";
                                string t2Date = reader["arrangeTransfer2Date"] != DBNull.Value ? reader["arrangeTransfer2Date"].ToString() : "";

                                string t3 = reader["arrangeTransfer3"] != DBNull.Value ? reader["arrangeTransfer3"].ToString() : "";
                                string t3Date = reader["arrangeTransfer3Date"] != DBNull.Value ? reader["arrangeTransfer3Date"].ToString() : "";

                                string t4 = reader["arrangeTransfer4"] != DBNull.Value ? reader["arrangeTransfer4"].ToString() : "";
                                string t4Date = reader["arrangeTransfer4Date"] != DBNull.Value ? reader["arrangeTransfer4Date"].ToString() : "";

                                string t5 = reader["arrangeTransfer5"] != DBNull.Value ? reader["arrangeTransfer5"].ToString() : "";
                                string t5Date = reader["arrangeTransfer5Date"] != DBNull.Value ? reader["arrangeTransfer5Date"].ToString() : "";
                               

                                    //TRANSFERS DISPLAY
                                    if (!string.IsNullOrEmpty(t1))
                                    {
                                        transfer1.Text = t1;

                                        if (!string.IsNullOrEmpty(t1Date))
                                        {
                                            // Parse the date string into a DateTime object
                                            DateTime arrivalDateTime;
                                            if (DateTime.TryParse(t1Date, out arrivalDateTime))
                                            {
                                                // Format the DateTime object into the desired format
                                                string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                // Assign the formatted date to the TextBox
                                                transfer1Date.Text = formattedArrivalDate;
                                            }
                                        }


                                        if (!string.IsNullOrEmpty(t2))
                                        {
                                            transfers2.Style["display"] = "block";
                                            transfer2.Text = t2;

                                            if (!string.IsNullOrEmpty(t2Date))
                                            {
                                                // Parse the date string into a DateTime object
                                                DateTime arrivalDateTime;
                                                if (DateTime.TryParse(t2Date, out arrivalDateTime))
                                                {
                                                    // Format the DateTime object into the desired format
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                    // Assign the formatted date to the TextBox
                                                    transfer2Date.Text = formattedArrivalDate;
                                                }
                                            }

                                            //check the transfer 3 is null
                                            if (!string.IsNullOrEmpty(t3))
                                            {
                                                transfers3.Style["display"] = "block";
                                                transfer3.Text = t3;

                                                //DISPLAY 3 DATE
                                                if (!string.IsNullOrEmpty(t3Date))
                                                {
                                                    // Parse the date string into a DateTime object
                                                    DateTime arrivalDateTime;
                                                    if (DateTime.TryParse(t3Date, out arrivalDateTime))
                                                    {
                                                        // Format the DateTime object into the desired format
                                                        string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                        // Assign the formatted date to the TextBox
                                                        transfer3Date.Text = formattedArrivalDate;
                                                    }
                                                }


                                                //check the transfer 4 is null
                                                if (!string.IsNullOrEmpty(t4))
                                                {
                                                    transfers4.Style["display"] = "block";
                                                    transfer4.Text = t4;


                                                    //DISPLAY 4 DATE
                                                    if (!string.IsNullOrEmpty(t4Date))
                                                    {
                                                        // Parse the date string into a DateTime object
                                                        DateTime arrivalDateTime;
                                                        if (DateTime.TryParse(t4Date, out arrivalDateTime))
                                                        {
                                                            // Format the DateTime object into the desired format
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                            // Assign the formatted date to the TextBox
                                                            transfer4Date.Text = formattedArrivalDate;
                                                        }
                                                    }


                                                }

                                                //check the transfer 5 is null
                                                if (!string.IsNullOrEmpty(t5))
                                                {
                                                    transfers5.Style["display"] = "block";
                                                    transfer5.Text = t5;

                                                    //DISPLAY 5 DATE
                                                    if (!string.IsNullOrEmpty(t5Date))
                                                    {
                                                        // Parse the date string into a DateTime object
                                                        DateTime arrivalDateTime;
                                                        if (DateTime.TryParse(t5Date, out arrivalDateTime))
                                                        {
                                                            // Format the DateTime object into the desired format
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                            // Assign the formatted date to the TextBox
                                                            transfer5Date.Text = formattedArrivalDate;
                                                        }
                                                    }


                                                }


                                            }
                                        }
                                        

                                    }
                                    else
                                    {
                                        transfers1.Style["display"] = "none";
                                        transfer1Date.Style["display"] = "none";
                                    } 
                            }
                            else
                            {
                                Response.Write("<script>alert('Something went wrong: Transfers.')</script>");

                            }
                        }

                    }
                }
            }

        }

        //private void displayRequestModal()
        //{
        //    try
        //    {
        //        // Get the ID of the clicked request from the session
        //        string requestId = Session["clickedRequest"].ToString();

        //        if (!string.IsNullOrEmpty(requestId))
        //        {
        //            // Query the database to retrieve the request details based on the ID
        //            using (var db = new SqlConnection(connectionString))
        //            {
        //                db.Open();
        //                using (var cmd = db.CreateCommand())
        //                {
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.CommandText = @"
        //                                SELECT tr.*, 
        //                                    rt.routeOFrom, rt.routeOTo, 
        //                                    rt.routeR1From, rt.routeR1To,
        //                                    rt.routeR2From, rt.routeR2To, 
        //                                    rt.routeM1From, rt.routeM1FromDate, 
        //                                    rt.routeM1To, rt.routeM1ToDate, 
        //                                    rt.routeM2From, rt.routeM2FromDate, 
        //                                    rt.routeM2To, rt.routeM2ToDate, 
        //                                    rt.routeM3From, rt.routeM3FromDate, 
        //                                    rt.routeM3To, rt.routeM3ToDate,
        //                                    rt.routeM4From, rt.routeM4FromDate, 
        //                                    rt.routeM4To, rt.routeM4ToDate, 
        //                                    rt.routeM5From, rt.routeM5FromDate, 
        //                                    rt.routeM5To, rt.routeM5ToDate

        //                                  FROM travelRequest tr
        //                                  LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
        //                                  WHERE tr.travelRequestID = @RequestId AND travelDraftStat = 'No'";


        //                    cmd.Parameters.AddWithValue("@RequestId", requestId);

        //                    using (var reader = cmd.ExecuteReader())
        //                    {
        //                        if (reader.Read())
        //                        {
        //                            approvalBlock.Style["display"] = "block";
        //                            uploadBlock.Style["display"] = "block";
        //                            pdfViewer.Style["display"] = "block";


        //                            // Retrieve the request details from the reader
        //                            string travelFacility = reader["travelHomeFacility"].ToString();
        //                            string empID = reader["travelEmpID"].ToString();
        //                            string empFname = reader["travelFname"].ToString();
        //                            string empMname = reader["travelMname"].ToString();
        //                            string empLname = reader["travelLname"].ToString();
        //                            string empProjCode = reader["travelProjectCode"].ToString();
        //                            string empPhone = reader["travelMobilenum"].ToString();
        //                            string empLevel = reader["travelLevel"].ToString();
        //                            string travelPurpose = reader["travelPurpose"].ToString();
        //                            string travelDepartureDate = reader["travelDeparture"].ToString();
        //                            string travelArrivalDate = reader["travelReturn"].ToString();
        //                            string travelFrom = reader["travelFrom"].ToString();
        //                            string travelTo = reader["travelTo"].ToString();
        //                            string flight = reader["travelOptions"].ToString();
        //                            string manager = reader["travelManager"].ToString();
        //                            string proof = reader["travelProofPath"].ToString();
        //                            string remarks = reader["travelRemarks"].ToString();


        //                            //FOR FLIGHT DETAILS - ROUTE
        //                            string oneFrom = reader["routeOFrom"] != DBNull.Value ? reader["routeOFrom"].ToString() : "";
        //                            string oneTo = reader["routeOTo"] != DBNull.Value ? reader["routeOTo"].ToString() : "";
        //                            string r1From = reader["routeR1From"] != DBNull.Value ? reader["routeR1From"].ToString() : "";
        //                            string r1To = reader["routeR1To"] != DBNull.Value ? reader["routeR1To"].ToString() : "";
        //                            string r2From = reader["routeR2From"] != DBNull.Value ? reader["routeR2From"].ToString() : "";
        //                            string r2To = reader["routeR2To"] != DBNull.Value ? reader["routeR2To"].ToString() : "";
        //                            string mul1From = reader["routeM1From"] != DBNull.Value ? reader["routeM1From"].ToString() : "";
        //                            string mul1FromDate = reader["routeM1FromDate"] != DBNull.Value ? reader["routeM1FromDate"].ToString() : "";
        //                            string mul1To = reader["routeM1To"] != DBNull.Value ? reader["routeM1To"].ToString() : "";
        //                            string mul1ToDate = reader["routeM1ToDate"] != DBNull.Value ? reader["routeM1ToDate"].ToString() : "";
        //                            string mul2From = reader["routeM2From"] != DBNull.Value ? reader["routeM2From"].ToString() : "";
        //                            string mul2FromDate = reader["routeM2FromDate"] != DBNull.Value ? reader["routeM2FromDate"].ToString() : "";
        //                            string mul2To = reader["routeM2To"] != DBNull.Value ? reader["routeM2To"].ToString() : "";
        //                            string mul2ToDate = reader["routeM2ToDate"] != DBNull.Value ? reader["routeM2ToDate"].ToString() : "";
        //                            string mul3From = reader["routeM3From"] != DBNull.Value ? reader["routeM3From"].ToString() : "";
        //                            string mul3FromDate = reader["routeM3FromDate"] != DBNull.Value ? reader["routeM3FromDate"].ToString() : "";
        //                            string mul3To = reader["routeM3To"] != DBNull.Value ? reader["routeM3To"].ToString() : "";
        //                            string mul3ToDate = reader["routeM3ToDate"] != DBNull.Value ? reader["routeM3ToDate"].ToString() : "";
        //                            string mul4From = reader["routeM4From"] != DBNull.Value ? reader["routeM4From"].ToString() : "";
        //                            string mul4FromDate = reader["routeM4FromDate"] != DBNull.Value ? reader["routeM4FromDate"].ToString() : "";
        //                            string mul4To = reader["routeM4To"] != DBNull.Value ? reader["routeM4To"].ToString() : "";
        //                            string mul4ToDate = reader["routeM4ToDate"] != DBNull.Value ? reader["routeM4ToDate"].ToString() : "";
        //                            string mul5From = reader["routeM5From"] != DBNull.Value ? reader["routeM5From"].ToString() : "";
        //                            string mul5FromDate = reader["routeM5FromDate"] != DBNull.Value ? reader["routeM5FromDate"].ToString() : "";
        //                            string mul5To = reader["routeM5To"] != DBNull.Value ? reader["routeM5To"].ToString() : "";
        //                            string mul5ToDate = reader["routeM5ToDate"] != DBNull.Value ? reader["routeM5ToDate"].ToString() : "";

        //                            // Display or use the retrieved request details
        //                            TextBox3.Text = travelFacility;
        //                            TextBox4.Text = empID;
        //                            employeeFName.Text = empFname;
        //                            employeeMName.Text = empMname;
        //                            employeeLName.Text = empLname;
        //                            employeeProjCode.Text = empProjCode;
        //                            TextBox5.Text = empPhone;
        //                            TextBox6.Text = empLevel;
        //                            employeeManager.Text = manager;
        //                            pdfViewer.Src = proof;
        //                            flightOptions.Text = flight;
        //                            employeePurpose.Text = travelPurpose;
        //                            employeeFrom.Text = travelFrom;
        //                            employeeTo.Text = travelTo;
        //                            employeeRemarks.Text = remarks;

        //                            if (!string.IsNullOrEmpty(travelArrivalDate))
        //                            {
        //                                // Parse the date string into a DateTime object
        //                                DateTime arrivalDateTime;
        //                                if (DateTime.TryParse(travelArrivalDate, out arrivalDateTime))
        //                                {
        //                                    // Format the DateTime object into the desired format
        //                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                    // Assign the formatted date to the TextBox
        //                                    employeeArrivalDate.Text = formattedArrivalDate;
        //                                }
        //                            }
        //                            if (!string.IsNullOrEmpty(travelDepartureDate))
        //                            {
        //                                // Parse the date string into a DateTime object
        //                                DateTime arrivalDateTime;
        //                                if (DateTime.TryParse(travelDepartureDate, out arrivalDateTime))
        //                                {
        //                                    // Format the DateTime object into the desired format
        //                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                    // Assign the formatted date to the TextBox
        //                                    employeeDepartureDate.Text = formattedArrivalDate;
        //                                }
        //                            }




        //                            if (!string.IsNullOrEmpty(oneFrom))
        //                            {
        //                                oneWaynput.Style["display"] = "block";
        //                                onewayFrom.Text = oneFrom;
        //                                onewayTo.Text = oneTo;
        //                            }
        //                            else if (!string.IsNullOrEmpty(r1From))
        //                            {
        //                                roundTripInput.Style["display"] = "block";
        //                                round1From.Text = r1From;
        //                                round1To.Text = r1To;
        //                                round2From.Text = r2From;
        //                                round2To.Text = r2To;
        //                            }
        //                            else if (!string.IsNullOrEmpty(mul1From) && (!string.IsNullOrEmpty(mul1To)))
        //                            {
        //                                multipleInput.Style["display"] = "block";
        //                                if (!string.IsNullOrEmpty(mul2From) && (!string.IsNullOrEmpty(mul2To)))
        //                                {
        //                                    TextBox7.Text = mul1From;
        //                                    //TextBox11.Text = mul1FromDate;
        //                                    TextBox8.Text = mul1To;
        //                                    //TextBox12.Text = mul1ToDate;
        //                                    TextBox9.Text = mul2From;
        //                                    //TextBox13.Text = mul2FromDate;
        //                                    TextBox10.Text = mul2To;
        //                                    //TextBox14.Text = mul2ToDate;

        //                                    if (!string.IsNullOrEmpty(mul1FromDate))
        //                                    {
        //                                        // Parse the date string into a DateTime object
        //                                        DateTime arrivalDateTime;
        //                                        if (DateTime.TryParse(mul1FromDate, out arrivalDateTime))
        //                                        {
        //                                            // Format the DateTime object into the desired format
        //                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                            // Assign the formatted date to the TextBox
        //                                            TextBox11.Text = formattedArrivalDate;
        //                                        }
        //                                    }
        //                                    if (!string.IsNullOrEmpty(mul1ToDate))
        //                                    {
        //                                        // Parse the date string into a DateTime object
        //                                        DateTime arrivalDateTime;
        //                                        if (DateTime.TryParse(mul1ToDate, out arrivalDateTime))
        //                                        {
        //                                            // Format the DateTime object into the desired format
        //                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                            // Assign the formatted date to the TextBox
        //                                            TextBox12.Text = formattedArrivalDate;
        //                                        }
        //                                    }
        //                                    if (!string.IsNullOrEmpty(mul2FromDate))
        //                                    {
        //                                        // Parse the date string into a DateTime object
        //                                        DateTime arrivalDateTime;
        //                                        if (DateTime.TryParse(mul2FromDate, out arrivalDateTime))
        //                                        {
        //                                            // Format the DateTime object into the desired format
        //                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                            // Assign the formatted date to the TextBox
        //                                            TextBox10.Text = formattedArrivalDate;
        //                                        }
        //                                    }
        //                                    if (!string.IsNullOrEmpty(mul2ToDate))
        //                                    {
        //                                        // Parse the date string into a DateTime object
        //                                        DateTime arrivalDateTime;
        //                                        if (DateTime.TryParse(mul2ToDate, out arrivalDateTime))
        //                                        {
        //                                            // Format the DateTime object into the desired format
        //                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                            // Assign the formatted date to the TextBox
        //                                            TextBox14.Text = formattedArrivalDate;
        //                                        }
        //                                    }


        //                                    if (!string.IsNullOrEmpty(mul3From) && (!string.IsNullOrEmpty(mul3To)))
        //                                    {
        //                                        additionalFields.Style["display"] = "block";
        //                                        TextBox15.Text = mul3From;
        //                                        //TextBox16.Text = mul3FromDate;
        //                                        TextBox17.Text = mul3To;
        //                                        //TextBox18.Text = mul3ToDate;

        //                                        if (!string.IsNullOrEmpty(mul3FromDate))
        //                                        {
        //                                            // Parse the date string into a DateTime object
        //                                            DateTime arrivalDateTime;
        //                                            if (DateTime.TryParse(mul3FromDate, out arrivalDateTime))
        //                                            {
        //                                                // Format the DateTime object into the desired format
        //                                                string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                                // Assign the formatted date to the TextBox
        //                                                TextBox16.Text = formattedArrivalDate;
        //                                            }
        //                                        }
        //                                        if (!string.IsNullOrEmpty(mul3ToDate))
        //                                        {
        //                                            // Parse the date string into a DateTime object
        //                                            DateTime arrivalDateTime;
        //                                            if (DateTime.TryParse(mul3ToDate, out arrivalDateTime))
        //                                            {
        //                                                // Format the DateTime object into the desired format
        //                                                string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                                // Assign the formatted date to the TextBox
        //                                                TextBox18.Text = formattedArrivalDate;
        //                                            }
        //                                        }


        //                                        if (!string.IsNullOrEmpty(mul4From) && (!string.IsNullOrEmpty(mul4To)))
        //                                        {
        //                                            destination4.Style["display"] = "block";
        //                                            TextBox27.Text = mul4From;
        //                                            //TextBox28.Text = mul4FromDate;
        //                                            TextBox29.Text = mul4To;
        //                                            //TextBox30.Text = mul4ToDate;

        //                                            if (!string.IsNullOrEmpty(mul4FromDate))
        //                                            {
        //                                                // Parse the date string into a DateTime object
        //                                                DateTime arrivalDateTime;
        //                                                if (DateTime.TryParse(mul4FromDate, out arrivalDateTime))
        //                                                {
        //                                                    // Format the DateTime object into the desired format
        //                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                                    // Assign the formatted date to the TextBox
        //                                                    TextBox28.Text = formattedArrivalDate;
        //                                                }
        //                                            }
        //                                            if (!string.IsNullOrEmpty(mul4To))
        //                                            {
        //                                                // Parse the date string into a DateTime object
        //                                                DateTime arrivalDateTime;
        //                                                if (DateTime.TryParse(mul4To, out arrivalDateTime))
        //                                                {
        //                                                    // Format the DateTime object into the desired format
        //                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                                    // Assign the formatted date to the TextBox
        //                                                    TextBox30.Text = formattedArrivalDate;
        //                                                }
        //                                            }

        //                                        }
        //                                        if (!string.IsNullOrEmpty(mul5From) && (!string.IsNullOrEmpty(mul5To)))
        //                                        {
        //                                            destination5.Style["display"] = "block";
        //                                            TextBox19.Text = mul5From;
        //                                            //TextBox20.Text = mul5FromDate;
        //                                            TextBox21.Text = mul5To;
        //                                            //TextBox22.Text = mul5ToDate;

        //                                            if (!string.IsNullOrEmpty(mul5FromDate))
        //                                            {
        //                                                // Parse the date string into a DateTime object
        //                                                DateTime arrivalDateTime;
        //                                                if (DateTime.TryParse(mul5FromDate, out arrivalDateTime))
        //                                                {
        //                                                    // Format the DateTime object into the desired format
        //                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                                    // Assign the formatted date to the TextBox
        //                                                    TextBox20.Text = formattedArrivalDate;
        //                                                }
        //                                            }
        //                                            if (!string.IsNullOrEmpty(mul5ToDate))
        //                                            {
        //                                                // Parse the date string into a DateTime object
        //                                                DateTime arrivalDateTime;
        //                                                if (DateTime.TryParse(mul5ToDate, out arrivalDateTime))
        //                                                {
        //                                                    // Format the DateTime object into the desired format
        //                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

        //                                                    // Assign the formatted date to the TextBox
        //                                                    TextBox22.Text = formattedArrivalDate;
        //                                                }
        //                                            }

        //                                        }


        //                                    }

        //                                }
        //                            }

        //                            // Assign other request details to corresponding controls
        //                        }
        //                        else
        //                        {
        //                            // Handle the case where no request with the given ID is found
        //                            Response.Write("<script>alert('No request found with the specified ID.')</script>");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // Handle the case where the request ID stored in the session is null or empty
        //            Response.Write("<script>alert('Invalid request ID.')</script>");
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        // Log the exception or display a user-friendly error message
        //        // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //        Response.Write("<script>alert('An error occurred while retrieving the request details.')</script>");
        //        // Log additional information from the SQL exception
        //        for (int i = 0; i < ex.Errors.Count; i++)
        //        {
        //            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //        }
        //    }

        //}

        protected void confirmArrangement_Click(object sender, EventArgs e)
        {
            Response.Write("<script> window.location.href = 'billingInformation.aspx'; </script>");
        }

        void AddRowToTable(PdfPTable table, string label, string value)
        {
            // Create and add the label cell
            PdfPCell labelCell = new PdfPCell(new Phrase(label));
            labelCell.HorizontalAlignment = Element.ALIGN_LEFT;
            labelCell.Border = Rectangle.NO_BORDER; // Remove border for cleaner look
            labelCell.PaddingRight = 5; // Reduce right padding to bring cells closer

            // Create and add the value cell
            PdfPCell valueCell = new PdfPCell(new Phrase(value));
            valueCell.HorizontalAlignment = Element.ALIGN_LEFT;
            valueCell.Border = Rectangle.NO_BORDER; // Remove border for cleaner look
            valueCell.PaddingLeft = -80; // Remove left padding to bring cells closer

            // Add cells to the table
            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }

        //START OF PDF FOR ARRANGEMENT
        protected void exportasPdf_Click(object sender, EventArgs e)
        {


            Response.Write("<script>alert('EXPORT DONE.')</script>");

        }

        protected Task exportasPdfAsync()
        {

            // Create a new MemoryStream to hold the PDF
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new Document
                using (Document doc = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                    // Open the Document for writing
                    doc.Open();

                    //travel arrangement header
                    BaseColor customColor = new BaseColor(9, 66, 106);
                    BaseColor whiteColor = BaseColor.WHITE;
                    Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, whiteColor);

                    Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, whiteColor);

                    // HEADER
                    AddSectionSeparatorHeader(doc, "Travel Arrangement for " + employeeName.Text, headerFont, customColor);
                    AddHeaderSection(doc);

                    //SPACE PURPOSES
                    Font headerSpace = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.WHITE);
                    AddEmptySeparator(doc, "Travel Arrangement for " + employeeName.Text, headerSpace);
                    EmptySeparatorforSpace(doc);

                    // Add content to the Document
                    AddSectionSeparator(doc, "Employee Information");
                    AddEmployeeInformationSection(doc);

                    AddSectionSeparator(doc, "Hotel Accommodations");
                    AddHotelAccommodationsSection(doc);

                    AddSectionSeparator(doc, "Flight Details");
                    AddFlightDetailsSection(doc);

                    AddSectionSeparator(doc, "Car/Airport Transfers");
                    AddTransfersSection(doc);

                    AddSectionSeparator(doc, "Additional Remarks");
                    AddRemarksSection(doc);

                    AddSectionSeparator(doc, "Others");
                    AddOthersSection(doc);

                    //SPACE PURPOSES
                    Font footerSpace = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.WHITE);
                    AddEmptySeparator(doc, "Travel Arrangement for " + employeeName.Text, footerSpace);
                    EmptySeparatorforSpace(doc);
                    AddEmptySeparator(doc, "Travel Arrangement for " + employeeName.Text, footerSpace);
                    EmptySeparatorforSpace(doc);

                    //FOOTER
                    AddSectionFooter(doc, "Feel free to provide feedback for any concerns. Have a safe trip!", footerFont, customColor);
                    AddFooter(doc);

                    // Close the Document
                    doc.Close();
                }


                // Convert the MemoryStream to a byte array
                byte[] pdfBytes = ms.ToArray();

                // Store PDF bytes in session
                Session["pdfBytes"] = pdfBytes;

                // Set a flag indicating that PDF generation and download are complete
                Session["pdfDownloadComplete"] = true;

                // Retrieve ID and name for filename
                string ID = Session["clickedRequest"].ToString();
                string recipientEmail = Session["userEmail"].ToString();
                
                string name = employeeName.Text;

                // Construct filename
                string filename = name + "_" + ID + ".pdf";

                // Create a directory path using empFname
                string folderPath = Path.Combine(Server.MapPath("/PDFs/travelArrangements"), name);

                // Check if the directory exists, if not, create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, filename);
                File.WriteAllBytes(filePath, pdfBytes);


                // Clear the response
                Response.Clear();

                // Set content type and header for file download
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);

                Session["arrangementPath"] = filePath;



                // Write the PDF bytes to the response
                Response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);

                // End the response
                Response.Flush();


            }

            return Task.CompletedTask;

        }


        public class FileParameter
        {
            public byte[] File { get; private set; }
            public string FileName { get; private set; }

            public FileParameter(byte[] file, string fileName)
            {
                File = file;
                FileName = fileName;
            }
        }


        // Helper method to add a section separator with a title
        private void AddSectionSeparator(Document doc, string sectionTitle)
        {
            PdfPTable separatorTable = new PdfPTable(1);
            separatorTable.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell(new Phrase(sectionTitle));
            cell.BackgroundColor = new BaseColor(200, 200, 200); // Adjust color as needed
            cell.Padding = 5;
            separatorTable.AddCell(cell);
            doc.Add(separatorTable);
        }
        private void AddSectionSeparatorHeader(Document doc, string sectionTitle, Font font, BaseColor customColor)
        {
            PdfPTable separatorTable = new PdfPTable(1);
            separatorTable.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell(new Phrase(sectionTitle, font)); // Use custom font
            cell.BackgroundColor = customColor; // Use custom color
            cell.HorizontalAlignment = Element.ALIGN_LEFT; // Center-align text
            cell.Padding = 5;
            separatorTable.AddCell(cell);
            doc.Add(separatorTable);
        }
        private void AddHeaderSection(Document doc)
        {

        }
        private void AddEmptySeparator(Document doc, string title, Font font)
        {
            PdfPTable emptySeparatorTable = new PdfPTable(1);
            emptySeparatorTable.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell(new Phrase(title, font)); // Use the provided font
            BaseColor whiteColor = BaseColor.WHITE; // Define white color
            cell.BackgroundColor = whiteColor; // Set cell background color to white
            cell.BorderColor = whiteColor; // Set cell border color to white
            cell.Padding = 5; // Adjust padding as needed
            emptySeparatorTable.AddCell(cell);
            doc.Add(emptySeparatorTable);
        }

        private void EmptySeparatorforSpace(Document doc)
        {

        }


        private void AddEmployeeInformationSection(Document doc)
        {
            // Add employee information section content// Add employee details
            PdfPTable employeeTable = new PdfPTable(2);
            employeeTable.TotalWidth = 500f; // Adjust width as needed
            employeeTable.LockedWidth = true;
            employeeTable.SpacingBefore = 10f;
            employeeTable.SpacingAfter = 10f;
            employeeTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Add rows for employee details
            AddRowToTable(employeeTable, "Traveller Name:", employeeName.Text);
            AddRowToTable(employeeTable, "Employee ID:", employeeID.Text);
            AddRowToTable(employeeTable, "Home Facility:", homeFacility.Text);
            AddRowToTable(employeeTable, "Mobile Number:", employeePhone.Text);

            // Add employee table to document
            doc.Add(employeeTable);
        }

        private void AddHotelAccommodationsSection(Document doc)
        {
            //Add hotel accommodations details
            PdfPTable hotelAccommodationsTable = new PdfPTable(2);
            hotelAccommodationsTable.TotalWidth = 500f; // Adjust width as needed
            hotelAccommodationsTable.LockedWidth = true;
            hotelAccommodationsTable.SpacingBefore = 10f;
            hotelAccommodationsTable.SpacingAfter = 10f;
            hotelAccommodationsTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Add rows for hotel accommodations details if they are not null or empty
            if (!string.IsNullOrEmpty(hotel.Text))
                AddRowToTable(hotelAccommodationsTable, "Hotel Name:", hotel.Text);

            if (!string.IsNullOrEmpty(employeeHotel.Text))
                AddRowToTable(hotelAccommodationsTable, "Hotel Name:", employeeHotel.Text);

            if (!string.IsNullOrEmpty(hotelAddress.Text))
                AddRowToTable(hotelAccommodationsTable, "Address:", hotelAddress.Text);

            if (!string.IsNullOrEmpty(hotelContact.Text))
                AddRowToTable(hotelAccommodationsTable, "Contact Number:", hotelContact.Text);

            if (!string.IsNullOrEmpty(durationFrom.Text))
            {

               AddRowToTable(hotelAccommodationsTable, "Duration of Stay:", durationFrom.Text);
                
            }

            // Add hotel accommodations table to document
            doc.Add(hotelAccommodationsTable);
        }

        private void AddFlightDetailsSection(Document doc)
        {
            // Add flight details
            PdfPTable flightDetailsTable = new PdfPTable(2);
            flightDetailsTable.TotalWidth = 500f; // Adjust width as needed
            flightDetailsTable.LockedWidth = true;
            flightDetailsTable.SpacingBefore = 10f;
            flightDetailsTable.SpacingAfter = 10f;
            flightDetailsTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Add rows for flight details
            AddRowToTable(flightDetailsTable, "Airline:", bookedairline.Text);

            if (additional1routeFields.Visible)
            {
                DateTime arrivalDateTime;
                if (DateTime.TryParse(r1FromDate.Text, out arrivalDateTime))
                {
                    string formattedArrivalDate = arrivalDateTime.ToString("ddMMMM");
                    AddFlightScheduleRow(flightDetailsTable, "", r1Flight.Text,"  ", formattedArrivalDate, "  ", r1From.Text, " ", r1To.Text, "      ", r1ETA.Text, " ", r1ETD.Text);
                }
            }
           
            // Add additional flight schedule details if available
            if (additional2routeFields.Visible)
            {
                DateTime arrivalDateTime;
                if (DateTime.TryParse(r2FromDate.Text, out arrivalDateTime))
                {
                    string formattedArrivalDate = arrivalDateTime.ToString("ddMMMM");
                    AddFlightScheduleRow(flightDetailsTable, "", r2Flight.Text, "  ", formattedArrivalDate, "  ", r2From.Text, " ", r2To.Text, "      ", r2ETA.Text, " ", r2ETD.Text);
                }
            }

            if (additional3routeFields.Visible)
            {
                DateTime arrivalDateTime;
                if (DateTime.TryParse(r3FromDate.Text, out arrivalDateTime))
                {
                    string formattedArrivalDate = arrivalDateTime.ToString("ddMMMM");
                    AddFlightScheduleRow(flightDetailsTable, "", r3Flight.Text, "  ", formattedArrivalDate, "  ", r3From.Text, " ", r3To.Text, "      ", r3ETA.Text, " ", r3ETD.Text);
                }
            }
            if (additional4routeFields.Visible)
            {
                DateTime arrivalDateTime;
                if (DateTime.TryParse(r4FromDate.Text, out arrivalDateTime))
                {
                    string formattedArrivalDate = arrivalDateTime.ToString("ddMMMM");
                    AddFlightScheduleRow(flightDetailsTable, "", r4Flight.Text, "  ", formattedArrivalDate, "  ", r4From.Text, " ", r4To.Text, "     ", r4ETA.Text, " ", r4ETD.Text);
                }
            }
            if (additional5routeFields.Visible)
            {
                DateTime arrivalDateTime;
                if (DateTime.TryParse(r5FromDate.Text, out arrivalDateTime))
                {
                    string formattedArrivalDate = arrivalDateTime.ToString("ddMMMM");
                    AddFlightScheduleRow(flightDetailsTable, "", r5Flight.Text, "  ", formattedArrivalDate, "  ", r5From.Text, " ", r5To.Text, "     ", r5ETA.Text, " ", r5ETD.Text);
                }
            }
            // Add flight details table to document
            doc.Add(flightDetailsTable);
        }

        //Helper method to add flight schedule row with conditional dash
        private void AddFlightScheduleRow(PdfPTable table, string label, string flightNumber, string text, string fromDate, string text1, string from, string text2, string to, string text3, string eta, string text4, string etd)
        {
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
            {
                // Format the flight schedule details including flight number, date, from, to, ETA, and ETD
                string formattedSchedule = $"{flightNumber} {text} {fromDate}{text1} {from} {text2} {to} {text3} {eta} {text4} {etd}";
                AddRowToTable(table, label, formattedSchedule);
            }
        }

        private void AddRemarksSection (Document doc)
        {
            // Add transfers details
            PdfPTable remarksTable = new PdfPTable(2);
            remarksTable.TotalWidth = 500f; // Adjust width as needed
            remarksTable.LockedWidth = true;
            remarksTable.SpacingBefore = 10f;
            remarksTable.SpacingAfter = 10f;
            remarksTable.HorizontalAlignment = Element.ALIGN_LEFT;

            AddRowToTable(remarksTable, "Remarks:", remarks.Text);

            // Add employee table to document
            doc.Add(remarksTable);

        }
        private void AddTransfersSection(Document doc)
        {
            // Add transfers details
            PdfPTable transfersTable = new PdfPTable(2);
            transfersTable.TotalWidth = 500f; // Adjust width as needed
            transfersTable.LockedWidth = true;
            transfersTable.SpacingBefore = 10f;
            transfersTable.SpacingAfter = 10f;
            transfersTable.HorizontalAlignment = Element.ALIGN_LEFT;

            DateTime transfer1DateTime;
            if (DateTime.TryParse(transfer1Date.Text, out transfer1DateTime))
            {
                string formattedTransfer2Date = transfer1DateTime.ToString("ddMMMM");
                AddTransfersRow(transfersTable, "Transfers:", formattedTransfer2Date, "-", transfer1.Text);

            }

            if (transfers2.Visible)
            {
                DateTime transfer2DateTime;
                if (DateTime.TryParse(transfer2Date.Text, out transfer2DateTime))
                {
                    string formattedTransfer2Date = transfer2DateTime.ToString("ddMMMM");
                    AddTransfersRow(transfersTable, "", formattedTransfer2Date, "-", transfer2.Text);
                }
            }

            if (transfers3.Visible)
            {
                DateTime transfer3DateTime;
                if (DateTime.TryParse(transfer3Date.Text, out transfer3DateTime))
                {
                    string formattedTransfer3Date = transfer3DateTime.ToString("ddMMMM");
                    AddTransfersRow(transfersTable, "", formattedTransfer3Date, "-", transfer3.Text);
                }
            }

            if (transfers4.Visible)
            {
                DateTime transfer4DateTime;
                if (DateTime.TryParse(transfer4Date.Text, out transfer4DateTime))
                {
                    string formattedTransfer4Date = transfer4DateTime.ToString("ddMMMM");
                    AddTransfersRow(transfersTable, "", formattedTransfer4Date, "-", transfer4.Text);
                }
            }

            if (transfers5.Visible)
            {
                DateTime transfer5DateTime;
                if (DateTime.TryParse(transfer5Date.Text, out transfer5DateTime))
                {
                    string formattedTransfer5Date = transfer5DateTime.ToString("ddMMMM");
                    AddTransfersRow(transfersTable, "", formattedTransfer5Date, "-", transfer5.Text);
                }
            }



            doc.Add(transfersTable);
     
        }
        private void AddTransfersRow(PdfPTable table, string label, string date, string text, string instruction)
        {
            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(instruction))
            {
                AddRowToTable(table, label, $"{date} {text} {instruction} ");
            }
        }

        private void AddOthersSection(Document doc)
        {
            // Add others details
            PdfPTable othersTable = new PdfPTable(2);
            othersTable.TotalWidth = 500f; // Adjust width as needed
            othersTable.LockedWidth = true;
            othersTable.SpacingBefore = 10f;
            othersTable.SpacingAfter = 10f;
            othersTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Add rows for others details
            AddRowToTable(othersTable, "Travel Requirements:", requirements.Text);
            AddRowToTable(othersTable, "Additional Notes:", ParseTextWithLinks(additionalNotes.Text));

            // Add others table to document
            doc.Add(othersTable);
        }

        // Method to parse text with hyperlinks
        private string ParseTextWithLinks(string text)
        {
            // Regex pattern to match URLs
            string pattern = @"((http|https):\/\/[^\s]+)";

            // Replace URLs with clickable links
            string formattedText = Regex.Replace(text, pattern, "$1");

            return formattedText;
        }

        private void AddSectionFooter(Document doc, string sectionTitle, Font font, BaseColor customColor)
        {
            PdfPTable separatorTable = new PdfPTable(1);
            separatorTable.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell(new Phrase(sectionTitle, font)); // Use custom font
            cell.BackgroundColor = customColor; // Use custom color
            cell.HorizontalAlignment = Element.ALIGN_LEFT; // Center-align text
            cell.Padding = 5;
            separatorTable.AddCell(cell);
            doc.Add(separatorTable);
        }
        private void AddFooter(Document doc)
        {

        }
        //END OF PDF FOR ARRANGEMENT




        //SMTP
        private async Task SendPdfByEmail(byte[] pdfBytes, string filename, string recipientEmail)
        {

            try
            {
                string fromEmail = "trinidadarheamae28@gmail.com";
                string fromPass = "sjheppyymyvzxyid";
                string receiver = "CRosalejos@innodata.com";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(receiver);
                mail.Subject = "Travel Arrangement Copy";
                mail.Body = "Please find your travel arrangement PDF attached.";
                mail.IsBodyHtml = true;

                Attachment attachment = new Attachment(new MemoryStream(pdfBytes), filename);
                mail.Attachments.Add(attachment);

                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPass);

                    await smtpClient.SendMailAsync(mail);
                }

                // Alert the user
                Response.Write("<script>alert('Travel Arrangement has been sent.')</script>");
            }
            catch (SmtpException smtpEx)
            {
                // Handle SmtpException
                Debug.WriteLine("SMTP Exception occurred: " + smtpEx.Message);
                // Log other details...
                Response.Write("<script>alert('Error sending email. Please try again later.');</script>");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Debug.WriteLine("An error occurred: " + ex.Message);
                // Log other details...
                Response.Write("<script>alert('Error sending email. Please try again later.');</script>");
            }


        }


        protected async Task SendPdfByEmail(string filePath, string recipientEmail)
        {
            // Email sending code using SMTP
            // You can use libraries like System.Net.Mail or MailKit for sending emails

            // Example using System.Net.Mail:
            using (SmtpClient smtpClient = new SmtpClient("smtp.example.com"))
            {
                // Configure SMTP client settings

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("rheawithmaebizz@gmail.com");
                    mailMessage.To.Add(recipientEmail);
                    mailMessage.Subject = "Travel Arrangement PDF";
                    mailMessage.Body = "Please find the attached PDF for your travel arrangement.";

                    // Attach the PDF file
                    Attachment attachment = new Attachment(filePath);
                    mailMessage.Attachments.Add(attachment);

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }

        protected void sendFile_Click(object sender, EventArgs e)
        {
            // Get the file path from Session
            string filePath = Session["arrangementPath"] as string;

            // Check if filePath is not null and the file exists
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                // Read the file content
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64File = Convert.ToBase64String(fileBytes);

                // Construct the email data
                var emailData = new
                {
                    service_id = "service_c83sdsd",
                    template_id = "template_j5shg1l",
                    user_id = "Vpn9etH0X1UpOj3u_",
                    template_params = new
                    {
                        to_email = employeeEmail.Text,
                        subject = "Travel Arrangement",
                        message = "Safe Travels " + employeeName + "!<br>Your travel request has been arranged and processed. Please see attached file for a copy of your travel arrangement.",
                        attachment = base64File, // Base64 encoded file content
                        attachmentFileName = "travel_arrangement.pdf" // Name of the attached file
                    }
                };

                // Convert the email data to JSON
                var jsonData = JsonConvert.SerializeObject(emailData);

                // Make HTTP POST request to EmailJS API endpoint
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("https://api.emailjs.com/api/v1.0/email/send", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Email sent successfully
                        Response.Write("<script>alert('Email sent successfully.');</script>");
                    }
                    else
                    {
                        // Failed to send email
                        Response.Write("<script>alert('Error sending email. Please try again later.');</script>");
                    }
                }
            }
            else
            {
                // Handle case when file path is not valid or file does not exist
                Response.Write("<script>alert('Invalid file path. Please try again.');</script>");
            }


            ////SMTP
            //string fromMail = "rheawithmaebizz@gmail.com";
            //string fromPassword = "cufuftiioimiyagy";

            //// Get the file path from Session
            //string filePath = Session["arrangementPath"] as string;

            //// Check if filePath is not null and the file exists
            //if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            //{
            //    MailMessage message = new MailMessage();
            //    message.From = new MailAddress(fromMail);
            //    message.Subject = "Travel Arrangement";
            //    message.To.Add(new MailAddress(employeeEmail.Text));

            //    // Construct HTML email body
            //    string body = "<html><title>Verify your Identity</title><body>Safe Travels " + employeeName + "!<br>Your travel request has been arranged and processed. Please see attached file for a copy of your travel arrangement.</body></html>";
            //    message.Body = body;
            //    message.IsBodyHtml = true;

            //    // Attach the PDF file
            //    Attachment attachment = new Attachment(filePath);
            //    message.Attachments.Add(attachment);

            //    var smtpClient = new SmtpClient("smtp.gmail.com")
            //    {
            //        Port = 587,
            //        Credentials = new NetworkCredential(fromMail, fromPassword),
            //        EnableSsl = true,
            //    };

            //    try
            //    {
            //        smtpClient.Send(message);
            //        // Success message
            //        Response.Write("<script>alert('Email sent successfully.');</script>");
            //    }
            //    catch (Exception ex)
            //    {
            //        // Log the exception for debugging
            //        Debug.WriteLine("Error sending email: " + ex.ToString());
            //        // Show user-friendly error message
            //        Response.Write("<script>alert('Error sending email. Please try again later.');</script>");
            //    }
            //}
            //else
            //{
            //    // Handle case when file path is not valid or file does not exist
            //    Response.Write("<script>alert('Invalid file path. Please try again.');</script>");
            //}
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script> window.location.href = 'TravelRequests.aspx'; </script>");

        }

        protected void confirmExport_Click(object sender, EventArgs e)
        {
            Page.RegisterAsyncTask(new PageAsyncTask(exportasPdfAsync));
        }

        protected void sendToEmail_Click(object sender, EventArgs e)
        {
            Response.Write("<script> window.location.href = 'sendToEmail.aspx'; </script>");

        }

        //protected void sendFile_Click(object sender, EventArgs e)
        //{
        //    if (Session["arrangementPath"] != null)
        //    {
        //        Retrieve the file path from the session

        //       string filePath = Session["arrangementPath"].ToString();

        //        Get the employee's email address from the label
        //        string receiver = employeeEmail.Text;

        //        Create a new MailMessage object
        //       MailMessage mail = new MailMessage();

        //        Set the sender's email address
        //        mail.From = new MailAddress("rheawithmaebizz@gmail.com");

        //        Set the recipient's email address (the employee's email)
        //        mail.To.Add(receiver);

        //        Set the subject of the email
        //        mail.Subject = "Travel Arrangement PDF";

        //        Set the body of the email
        //        mail.Body = "Please find attached the travel arrangement PDF.";

        //        Create an attachment from the file path
        //       Attachment attachment = new Attachment(filePath);

        //        Add the attachment to the email
        //        mail.Attachments.Add(attachment);

        //        Create an SMTP client to send the email
        //        SmtpClient smtpClient = new SmtpClient("smtp.yourserver.com");

        //        Specify your SMTP credentials if required
        //        smtpClient.Credentials = new System.Net.NetworkCredential("rheawithmaebizz@gmail.com", "cufuftiioimiyagy");

        //        Specify the SMTP port(e.g., 587)
        //        smtpClient.Port = 587;

        //        Send the email
        //        smtpClient.Send(mail);

        //        Dispose the attachment
        //        attachment.Dispose();

        //        Optionally, you can show a success message or perform other actions after sending the email
        //        Response.Write("Email sent successfully!");
        //    }
        //}
    }
}