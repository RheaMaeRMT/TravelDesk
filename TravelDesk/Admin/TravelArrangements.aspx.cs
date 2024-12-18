﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Admin
{
    public partial class TravelArrangements : System.Web.UI.Page
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
                        // Display the request and populate employee details
                        DisplayRequest();
                        populateEmployeeDetails();
                        checkifArranged();

                    }
                    else
                    {
                        // Redirect to the login page if the session variable is empty or null
                        Response.Write("<script>alert('Session Expired. Please login again.'); window.location.href = '../LoginPage.aspx';</script>");
                    }
                }
                else
                {
                    // Redirect to the login page if the session variable is null
                    Response.Write("<script>alert('Session Expired. Please login again.');window.location.href = '../LoginPage.aspx';</script>");
                }
            }
        }
        private void checkifArranged()
        {

            if (Session["employeeID"] != null)
            {
                string employeeID = Session["employeeID"].ToString();
                string requestId = Session["clickedRequest"].ToString();

                // Query the database to retrieve the request details based on the ID
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM travelArranged WHERE arrangedUserID = @empID AND arrangedTravelReqID = @requestID";
                        cmd.Parameters.AddWithValue("@empID", employeeID);
                        cmd.Parameters.AddWithValue("@requestID", requestId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Check if there are any rows returned by the query
                            {
                                //THE REQUEST IS ALREADY ARRANGED, REDIRECT TO ARRANGEDREQUEST PAGE
                                Response.Write("<script> window.location.href = 'arrangedRequest.aspx'; </script>");

                            }
                            else
                            {
                                //REQUEST IS NOT YET ARRANGED, PROCEED TO TRAVEL ARRANGEMENT PAGE
                                Response.Write("<script> window.location.href = 'TravelArrangements.aspx'; </script>");

                            }
                        }

                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Session Expired. Please login again.');window.location.href = '../LoginPage.aspx';</script>");

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
                                    string travelFacility = reader["travelHomeFacility"].ToString();
                                    string employeeID = reader["travelEmpID"].ToString();
                                    string employeeFname = reader["travelFname"].ToString();
                                    string employeeMname = reader["travelMname"].ToString();
                                    string employeeLname = reader["travelLname"].ToString();
                                    string employeeProjCode = reader["travelProjectCode"].ToString();
                                    string employeePhone = reader["travelMobilenum"].ToString();
                                    string employeeLevel = reader["travelLevel"].ToString();
                                    string travelPurpose = reader["travelPurpose"].ToString();
                                    string proof = reader["travelProofPath"].ToString();
                                    string remarks = reader["travelRemarks"].ToString();
                                    string employeeBirth = reader["travelBdate"].ToString();
                                    string flight = reader["travelOptions"].ToString();
                                    string email = reader["travelEmail"].ToString();
                                    string DU = reader["travelDU"].ToString();

                                    Session["employeeID"] = employeeID;
                                    // Display or use the retrieved request details
                                    
                                    empID.Text = employeeID;
                                    empFName.Text = employeeFname + " " + employeeMname + " " + employeeLname;
                                    empLevel.Text = employeeLevel;

                                    empEmail.Text = email;
                                    empMobile.Text = employeePhone;

                                    empCode.Text = employeeProjCode;
                                    empFacility.Text = travelFacility;
                                    empDeptUnit.Text = DU;

                                    pdfViewer.Src = proof;
                                    employeePurpose.Text = travelPurpose;
                                    employeeRemarks.Text = remarks;
                                    flightOptions.Text = flight;

                                    ArrangementLabel.Text = "Travel Arrangement for" + " " + employeeFname + " " + employeeMname + " " + employeeLname;

                                    if (!string.IsNullOrEmpty(employeeBirth))
                                    {
                                        //Parse the date string into a DateTime object
                                       DateTime arrivalDateTime;
                                        if (DateTime.TryParse(employeeBirth, out arrivalDateTime))
                                        {
                                            //Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                            //Assign the formatted date to the TextBox
                                            empBdate.Text = formattedArrivalDate;
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
                Response.Write("<script>alert('An error occurred while retrieving the employee details.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
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

        protected void submitArrangement_Click(object sender, EventArgs e)
        {


            try
            {

                if (Session["userID"] != null && Session["clickedRequest"] != null)
                {
                    saveArrangementDetails();
                }
                else
                {
                    // Session values are null
                    Response.Write("<script>alert('Session Expired! Please login again.')</script>");
                }


            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during travel ARRANGEMENT. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }

        }
        private void saveArrangementDetails()
        {
            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "A" + random;

            // Session values are not null, proceed with inserting into the database
            string empID = Session["employeeID"].ToString();
            string requestId = Session["clickedRequest"].ToString();

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                try
                {
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelArranged (arrangedID, arrangedUserID, arrangedTravelReqID, arrangedRequirements, arrangedNotes, arrangedDateCreated, arrangedRemarks) " +
                            "VALUES (@ID, @userID, @travelID, @requirements, @notes, @dateCreated, @remarks)";

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@userID", empID);
                        cmd.Parameters.AddWithValue("@travelID", requestId);
                        cmd.Parameters.AddWithValue("@requirements", ""); // Initialize the parameter outside the loop
                        cmd.Parameters.AddWithValue("@notes", additionalNotes.Text);
                        cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);
                        cmd.Parameters.AddWithValue("@remarks", remarks.Text);

                        //for the REQUIREMENTS                            
                        List<string> selectedRequirements = new List<string>();  // Create a list to hold the selected requirements                     
                        foreach (ListItem item in requirements.Items)    // Iterate through the items and add the selected ones to the list
                        {
                            if (item.Selected)
                            {
                                selectedRequirements.Add(item.Value);
                            }
                        }
                        string requirementsValue = string.Join(", ", selectedRequirements); // Join the selected items into a single string, separated by ", "                           
                        cmd.Parameters["@requirements"].Value = requirementsValue; // Assign the joined string to the parameter

                        Session["arrangementID"] = ID;
                        cmd.ExecuteNonQuery();

                    }

                    Session["clickedRequest"] = requestId;
                    // Insert into travelAccomodation table
                    saveAccomodationsDetails();

                }
                catch (SqlException ex)
                {

                    Response.Write("<script>alert('An error occurred during travel ARRANGEMENT. Please try again.')</script>");
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                    }
                }

            }

        }
        private void saveAccomodationsDetails()
        {
            string accomodation = accomodations.Text;

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                try
                {
                    Random rand = new Random();
                    int random = rand.Next(100000, 999999);
                    string acc = "ID" + random + "A";

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;

                        if (accomodation == "c/o Traveler")
                        {
                            string hotelName = coTraveller.Text;

                            cmd.CommandText = "INSERT INTO travelAccomodation (arrangedAccID, arrangeEmpID, arrangeAccomodation, arrangeHotelName, arrangeHotelAdd, arrangeHotelPhone, arrangeHotelFrom, arrangeHotelTo, " +
                                          "arrangeHotel2Name, arrangeHotel2Add, arrangeHotel2Phone, arrangeHotel2From, arrangeHotel2To, " +
                                          "arrangeHotel3Name, arrangeHotel3Add, arrangeHotel3Phone, arrangeHotel3From, arrangeHotel3To, " +
                                          "arrangeHotel4Name, arrangeHotel4Add, arrangeHotel4Phone, arrangeHotel4From, arrangeHotel4To, " +
                                          "arrangeHotel5Name, arrangeHotel5Add, arrangeHotel5Phone, arrangeHotel5From, arrangeHotel5To) " +
                                          "VALUES (@ID, @empID, @accomodations, @hotelName, @hotelAddress, @contact, @from, @to, " +
                                          "@hotel2Name, @hotel2Add, @hotel2Phone, @hotel2From, @hotel2To, " +
                                          "@hotel3Name, @hotel3Add, @hotel3Phone, @hotel3From, @hotel3To, " +
                                          "@hotel4Name, @hotel4Add, @hotel4Phone, @hotel4From, @hotel4To, " +
                                          "@hotel5Name, @hotel5Add, @hotel5Phone, @hotel5From, @hotel5To)";


                            cmd.Parameters.AddWithValue("@accomodations", hotelName);
                            cmd.Parameters.AddWithValue("@hotelName", accomodation);
                            cmd.Parameters.AddWithValue("@hotelAddress", hotelAddress.Text);
                            cmd.Parameters.AddWithValue("@contact", hotelPhone.Text);
                            cmd.Parameters.AddWithValue("@from", durationFrom.Text);
                            cmd.Parameters.AddWithValue("@to", durationTo.Text);

                            cmd.Parameters.AddWithValue("@hotel2Name", string.IsNullOrEmpty(hotelname2.Text) ? DBNull.Value : (object)hotelname2.Text);
                            cmd.Parameters.AddWithValue("@hotel2Add", string.IsNullOrEmpty(address2.Text) ? DBNull.Value : (object)address2.Text);
                            cmd.Parameters.AddWithValue("@hotel2Phone", string.IsNullOrEmpty(phone2.Text) ? DBNull.Value : (object)phone2.Text);
                            cmd.Parameters.AddWithValue("@hotel2From", string.IsNullOrEmpty(from2.Text) ? DBNull.Value : (object)from2.Text);
                            cmd.Parameters.AddWithValue("@hotel2To", string.IsNullOrEmpty(to2.Text) ? DBNull.Value : (object)to2.Text);

                            cmd.Parameters.AddWithValue("@hotel3Name", string.IsNullOrEmpty(hotelname3.Text) ? DBNull.Value : (object)hotelname3.Text);
                            cmd.Parameters.AddWithValue("@hotel3Add", string.IsNullOrEmpty(address3.Text) ? DBNull.Value : (object)address3.Text);
                            cmd.Parameters.AddWithValue("@hotel3Phone", string.IsNullOrEmpty(phone3.Text) ? DBNull.Value : (object)phone3.Text);
                            cmd.Parameters.AddWithValue("@hotel3From", string.IsNullOrEmpty(from3.Text) ? DBNull.Value : (object)from3.Text);
                            cmd.Parameters.AddWithValue("@hotel3To", string.IsNullOrEmpty(to3.Text) ? DBNull.Value : (object)to3.Text);

                            cmd.Parameters.AddWithValue("@hotel4Name", string.IsNullOrEmpty(hotelname4.Text) ? DBNull.Value : (object)hotelname4.Text);
                            cmd.Parameters.AddWithValue("@hotel4Add", string.IsNullOrEmpty(address4.Text) ? DBNull.Value : (object)address4.Text);
                            cmd.Parameters.AddWithValue("@hotel4Phone", string.IsNullOrEmpty(phone4.Text) ? DBNull.Value : (object)phone4.Text);
                            cmd.Parameters.AddWithValue("@hotel4From", string.IsNullOrEmpty(from4.Text) ? DBNull.Value : (object)from4.Text);
                            cmd.Parameters.AddWithValue("@hotel4To", string.IsNullOrEmpty(to4.Text) ? DBNull.Value : (object)to4.Text);

                            cmd.Parameters.AddWithValue("@hotel5Name", string.IsNullOrEmpty(hotelname5.Text) ? DBNull.Value : (object)hotelname5.Text);
                            cmd.Parameters.AddWithValue("@hotel5Add", string.IsNullOrEmpty(address5.Text) ? DBNull.Value : (object)address5.Text);
                            cmd.Parameters.AddWithValue("@hotel5Phone", string.IsNullOrEmpty(phone5.Text) ? DBNull.Value : (object)phone5.Text);
                            cmd.Parameters.AddWithValue("@hotel5From", string.IsNullOrEmpty(from5.Text) ? DBNull.Value : (object)from5.Text);
                            cmd.Parameters.AddWithValue("@hotel5To", string.IsNullOrEmpty(to5.Text) ? DBNull.Value : (object)to5.Text);

                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO travelAccomodation (arrangedAccID, arrangeEmpID, arrangeAccomodation, arrangeHotelName, arrangeHotelAdd, arrangeHotelPhone, arrangeHotelFrom, arrangeHotelTo, " +
                                          "arrangeHotel2Name, arrangeHotel2Add, arrangeHotel2Phone, arrangeHotel2From, arrangeHotel2To, " +
                                          "arrangeHotel3Name, arrangeHotel3Add, arrangeHotel3Phone, arrangeHotel3From, arrangeHotel3To, " +
                                          "arrangeHotel4Name, arrangeHotel4Add, arrangeHotel4Phone, arrangeHotel4From, arrangeHotel4To, " +
                                          "arrangeHotel5Name, arrangeHotel5Add, arrangeHotel5Phone, arrangeHotel5From, arrangeHotel5To) " +
                                          "VALUES (@ID, @empID, @accomodations, @hotelName, @hotelAddress, @contact, @from, @to, " +
                                          "@hotel2Name, @hotel2Add, @hotel2Phone, @hotel2From, @hotel2To, " +
                                          "@hotel3Name, @hotel3Add, @hotel3Phone, @hotel3From, @hotel3To, " +
                                          "@hotel4Name, @hotel4Add, @hotel4Phone, @hotel4From, @hotel4To, " +
                                          "@hotel5Name, @hotel5Add, @hotel5Phone, @hotel5From, @hotel5To)";

                            cmd.Parameters.AddWithValue("@accomodations", accomodations.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@hotelName", hotel.Text);
                            cmd.Parameters.AddWithValue("@hotelAddress", hotelAddress.Text);
                            cmd.Parameters.AddWithValue("@contact", hotelPhone.Text);
                            cmd.Parameters.AddWithValue("@from", durationFrom.Text);
                            cmd.Parameters.AddWithValue("@to", durationTo.Text);

                            cmd.Parameters.AddWithValue("@hotel2Name", string.IsNullOrEmpty(hotelname2.Text) ? DBNull.Value : (object)hotelname2.Text);
                            cmd.Parameters.AddWithValue("@hotel2Add", string.IsNullOrEmpty(address2.Text) ? DBNull.Value : (object)address2.Text);
                            cmd.Parameters.AddWithValue("@hotel2Phone", string.IsNullOrEmpty(phone2.Text) ? DBNull.Value : (object)phone2.Text);
                            cmd.Parameters.AddWithValue("@hotel2From", string.IsNullOrEmpty(from2.Text) ? DBNull.Value : (object)from2.Text);
                            cmd.Parameters.AddWithValue("@hotel2To", string.IsNullOrEmpty(to2.Text) ? DBNull.Value : (object)to2.Text);

                            cmd.Parameters.AddWithValue("@hotel3Name", string.IsNullOrEmpty(hotelname3.Text) ? DBNull.Value : (object)hotelname3.Text);
                            cmd.Parameters.AddWithValue("@hotel3Add", string.IsNullOrEmpty(address3.Text) ? DBNull.Value : (object)address3.Text);
                            cmd.Parameters.AddWithValue("@hotel3Phone", string.IsNullOrEmpty(phone3.Text) ? DBNull.Value : (object)phone3.Text);
                            cmd.Parameters.AddWithValue("@hotel3From", string.IsNullOrEmpty(from3.Text) ? DBNull.Value : (object)from3.Text);
                            cmd.Parameters.AddWithValue("@hotel3To", string.IsNullOrEmpty(to3.Text) ? DBNull.Value : (object)to3.Text);

                            cmd.Parameters.AddWithValue("@hotel4Name", string.IsNullOrEmpty(hotelname4.Text) ? DBNull.Value : (object)hotelname4.Text);
                            cmd.Parameters.AddWithValue("@hotel4Add", string.IsNullOrEmpty(address4.Text) ? DBNull.Value : (object)address4.Text);
                            cmd.Parameters.AddWithValue("@hotel4Phone", string.IsNullOrEmpty(phone4.Text) ? DBNull.Value : (object)phone4.Text);
                            cmd.Parameters.AddWithValue("@hotel4From", string.IsNullOrEmpty(from4.Text) ? DBNull.Value : (object)from4.Text);
                            cmd.Parameters.AddWithValue("@hotel4To", string.IsNullOrEmpty(to4.Text) ? DBNull.Value : (object)to4.Text);

                            cmd.Parameters.AddWithValue("@hotel5Name", string.IsNullOrEmpty(hotelname5.Text) ? DBNull.Value : (object)hotelname5.Text);
                            cmd.Parameters.AddWithValue("@hotel5Add", string.IsNullOrEmpty(address5.Text) ? DBNull.Value : (object)address5.Text);
                            cmd.Parameters.AddWithValue("@hotel5Phone", string.IsNullOrEmpty(phone5.Text) ? DBNull.Value : (object)phone5.Text);
                            cmd.Parameters.AddWithValue("@hotel5From", string.IsNullOrEmpty(from5.Text) ? DBNull.Value : (object)from5.Text);
                            cmd.Parameters.AddWithValue("@hotel5To", string.IsNullOrEmpty(to5.Text) ? DBNull.Value : (object)to5.Text);
                        }

                        cmd.Parameters.AddWithValue("@ID", acc);
                        cmd.Parameters.AddWithValue("@empID", empID.Text);

                        cmd.ExecuteNonQuery();

                        Session["accomodationID"] = acc;
                        Session["empID"] = empID.Text;
                        saveFlightDetails();
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write("<script>alert('An error occurred during insertion of ACCOMODATION DETAILS. Please try again.')</script>");
                    foreach (SqlError error in ex.Errors)
                    {
                        Response.Write($"<script>alert('SQL Error {error.Number}: {error.Message}')</script>");
                    }
                }
            }
        }

        private void saveFlightDetails()
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                try
                {
                    Random rand = new Random();
                    int random = rand.Next(100000, 999999);
                    string flight = "F" + random + "ID";

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelFlight (travelFlightID, travelAirline, travelClass, travelDates, travelRoute, " +
                                          "routeM1Flight, routeM1From, routeM1FromDate, routeM1To, routeM1ETA, routeM1ETD, " +
                                          "routeM2Flight, routeM2From, routeM2FromDate, routeM2To, routeM2ETA, routeM2ETD, " +
                                          "routeM3Flight, routeM3From, routeM3FromDate, routeM3To, routeM3ETA, routeM3ETD, " +
                                          "routeM4Flight, routeM4From, routeM4FromDate, routeM4To, routeM4ETA, routeM4ETD, " +
                                          "routeM5Flight, routeM5From, routeM5FromDate, routeM5To, routeM5ETA, routeM5ETD) " +
                                          "VALUES (@ID, @airline, @travelClass, @travelDates, @routes, " +
                                          "@r1f, @r1From, @r1FromDate, @r1To, @r1A, @r1D, " +
                                          "@r2f, @r2From, @r2FromDate, @r2To, @r2A, @r2D, " +
                                          "@r3f, @r3From, @r3FromDate, @r3To, @r3A, @r3D, " +
                                          "@r4f, @r4From, @r4FromDate, @r4To, @r4A, @r4D, " +
                                          "@r5f, @r5From, @r5FromDate, @r5To, @r5A, @r5D)";
                       
                        // FOR THE ROUTE
                        List<string> routeParts = new List<string>();

                        if (r1From != null && !string.IsNullOrEmpty(r1From.Text))
                        {
                            routeParts.Add(r1From.Text);

                            if (r1To != null && !string.IsNullOrEmpty(r1To.Text))
                            {
                                routeParts.Add(r1To.Text);
                            }

                            if (r2From != null && !string.IsNullOrEmpty(r2From.Text))
                            {
                                if (!routeParts.Contains(r2From.Text)) // Avoid duplicate entries
                                {
                                    routeParts.Add(r2From.Text);
                                }

                                if (r2To != null && !string.IsNullOrEmpty(r2To.Text))
                                {
                                    routeParts.Add(r2To.Text);
                                }

                                if (r3From != null && !string.IsNullOrEmpty(r3From.Text))
                                {
                                    if (!routeParts.Contains(r3From.Text)) // Avoid duplicate entries
                                    {
                                        routeParts.Add(r3From.Text);
                                    }

                                    if (r3To != null && !string.IsNullOrEmpty(r3To.Text))
                                    {
                                        routeParts.Add(r3To.Text);
                                    }

                                    if (r4From != null && !string.IsNullOrEmpty(r4From.Text))
                                    {
                                        if (!routeParts.Contains(r4From.Text)) // Avoid duplicate entries
                                        {
                                            routeParts.Add(r4From.Text);
                                        }

                                        if (r4To != null && !string.IsNullOrEmpty(r4To.Text))
                                        {
                                            routeParts.Add(r4To.Text);
                                        }

                                        if (r5From != null && !string.IsNullOrEmpty(r5From.Text))
                                        {
                                            if (!routeParts.Contains(r5From.Text)) // Avoid duplicate entries
                                            {
                                                routeParts.Add(r5From.Text);
                                            }

                                            if (r5To != null && !string.IsNullOrEmpty(r5To.Text))
                                            {
                                                routeParts.Add(r5To.Text);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        string route = string.Join(" - ", routeParts);


                        cmd.Parameters.AddWithValue("@ID", flight);
                        cmd.Parameters.AddWithValue("@airline", airline.Text);
                        cmd.Parameters.AddWithValue("@travelClass", travelClass.Text);
                        cmd.Parameters.AddWithValue("@routes", route);

                        // Handling the travel dates
                        string travelDates = string.Empty;

                        if (!string.IsNullOrEmpty(r1FromDate?.Text))
                        {
                            if (DateTime.TryParseExact(r1FromDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate1))
                            {
                                string formattedR1FromDate = fromDate1.ToString("MMM d", CultureInfo.InvariantCulture);
                                travelDates = formattedR1FromDate;

                                if (!string.IsNullOrEmpty(r2FromDate?.Text))
                                {
                                    if (DateTime.TryParseExact(r2FromDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate2))
                                    {
                                        string formattedR2FromDate = fromDate2.ToString("MMM d", CultureInfo.InvariantCulture);
                                        travelDates = formattedR1FromDate + "-" + formattedR2FromDate;
                                    }
                                    else
                                    {
                                        throw new FormatException("The second date string was not recognized as a valid DateTime.");
                                    }
                                }
                            }
                            else
                            {
                                throw new FormatException("The first date string was not recognized as a valid DateTime.");
                            }
                        }
                        else
                        {
                            throw new ArgumentNullException("r1FromDate cannot be null or empty.");
                        }

                        // Add the travel dates parameter
                        cmd.Parameters.AddWithValue("@travelDates", travelDates);

                        // Adding other route parameters
                        AddRouteParameters(cmd, "@r1", r1Flight, r1From, r1FromDate, r1To, r1ETA, r1ETD);
                        AddRouteParameters(cmd, "@r2", r2Flight, r2From, r2FromDate, r2To, r2ETA, r2ETD);
                        AddRouteParameters(cmd, "@r3", r3Flight, r3From, r3FromDate, r3To, r3ETA, r3ETD);
                        AddRouteParameters(cmd, "@r4", r4Flight, r4From, r4FromDate, r4To, r4ETA, r4ETD);
                        AddRouteParameters(cmd, "@r5", r5Flight, r5From, r5FromDate, r5To, r5ETA, r5ETD);

                        cmd.ExecuteNonQuery();

                        Session["flightID"] = flight;
                        saveTransfersDetails();
                    }
                }
                catch (SqlException ex)
                {
                    // Log the exception or display a user-friendly error message
                    Response.Write("<script>alert('An error occurred during insertion of FLIGHT DETAILS. Please try again.')</script>");
                    // Log additional information from the SQL exception
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                    }
                }
            }
        }

        private void AddRouteParameters(SqlCommand cmd, string prefix, TextBox flight, TextBox from, TextBox fromDate, TextBox to, TextBox eta, TextBox etd)
        {
            cmd.Parameters.AddWithValue(prefix + "f", string.IsNullOrEmpty(flight?.Text) ? (object)DBNull.Value : Regex.Replace(flight.Text, @"\s+", ""));
            cmd.Parameters.AddWithValue(prefix + "From", string.IsNullOrEmpty(from?.Text) ? (object)DBNull.Value : from.Text);
            cmd.Parameters.AddWithValue(prefix + "FromDate", string.IsNullOrEmpty(fromDate?.Text) ? (object)DBNull.Value : fromDate.Text);
            cmd.Parameters.AddWithValue(prefix + "To", string.IsNullOrEmpty(to?.Text) ? (object)DBNull.Value : to.Text);
            cmd.Parameters.AddWithValue(prefix + "A", string.IsNullOrEmpty(eta?.Text) ? (object)DBNull.Value : eta.Text);
            cmd.Parameters.AddWithValue(prefix + "D", string.IsNullOrEmpty(etd?.Text) ? (object)DBNull.Value : etd.Text);
        }

        private void saveTransfersDetails()
        {

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                try
                {
                    Random rand = new Random();
                    int random = rand.Next(100000, 999999);
                    string transfer = "T" + random + "ID";

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelTransfers (arrangeTransferID, arrangeTransfer1, arrangeTransfer1Date, arrangeTransfer2, arrangeTransfer2Date, arrangeTransfer3, arrangeTransfer3Date, arrangeTransfer4, arrangeTransfer4Date, arrangeTransfer5, arrangeTransfer5Date)"
                            + "VALUES (@ID, @transfer1, @transfer1Date, @transfer2, @transfer2Date, @transfer3, @transfer3Date, @transfer4, @transfer4Date, @transfer5, @transfer5Date)";

                        cmd.Parameters.AddWithValue("@ID", transfer);
                        cmd.Parameters.AddWithValue("@transfer1", transfer1.Text);
                        cmd.Parameters.AddWithValue("@transfer1Date", string.IsNullOrEmpty(transfer1Date.Text) ? (object)DBNull.Value : transfer1Date.Text);
                        cmd.Parameters.AddWithValue("@transfer2", transfer2.Text);
                        cmd.Parameters.AddWithValue("@transfer2Date", string.IsNullOrEmpty(transfer2Date.Text) ? (object)DBNull.Value : transfer2Date.Text);
                        cmd.Parameters.AddWithValue("@transfer3", transfer3.Text);
                        cmd.Parameters.AddWithValue("@transfer3Date", string.IsNullOrEmpty(transfer3Date.Text) ? (object)DBNull.Value : transfer3Date.Text);
                        cmd.Parameters.AddWithValue("@transfer4", transfer4.Text);
                        cmd.Parameters.AddWithValue("@transfer4Date", string.IsNullOrEmpty(transfer4Date.Text) ? (object)DBNull.Value : transfer4Date.Text);
                        cmd.Parameters.AddWithValue("@transfer5", transfer5.Text);
                        cmd.Parameters.AddWithValue("@transfer5Date", string.IsNullOrEmpty(transfer5Date.Text) ? (object)DBNull.Value : transfer5Date.Text);
                        cmd.ExecuteNonQuery();

                        Session["transferID"] = transfer;
                        updateTravelArranged();
                    }

                }
                catch (SqlException ex)
                {
                    // Log the exception or display a user-friendly error message
                    // Example: Log.Error("An error occurred during travel request enrollment", ex);
                    Response.Write("<script>alert('An error occurred during insertion of TRANSFERS DETAILS. Please try again.')</script>");
                    // Log additional information from the SQL exception
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                    }
                }

            }
        }

        private void updateTravelRoute()
        {
            try
            {
                if (Session["clickedRequest"] != null)
                {
                    string requestId = Session["clickedRequest"].ToString();
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE route SET " +
                                "routeM1From = @r1From, " +
                                "routeM1FromDate = @r1FromDate, " +
                                "routeM1To = @r1To," +
                                "routeM1ToDate = @r1ToDate," +
                                "routeM2From = @r2From, " +
                                "routeM2FromDate = @r2FromDate, " +
                                "routeM2To = @r2To," +
                                "routeM2ToDate = @r2ToDate," +
                                "routeM3From = @r3From, " +
                                "routeM3FromDate = @r3FromDate, " +
                                "routeM3To = @r3To," +
                                "routeM3ToDate = @r3ToDate," +
                                "routeM4From = @r4From, " +
                                "routeM4FromDate = @r4FromDate, " +
                                "routeM4To = @r4To," +
                                "routeM4ToDate = @r4ToDate," +
                                "routeM5From = @r5From, " +
                                "routeM5FromDate = @r5FromDate, " +
                                "routeM5To = @r5To," +
                                "routeM5ToDate = @r5ToDate " +
                                "WHERE routeTravelID = @ID";

                            cmd.Parameters.AddWithValue("@ID", requestId);
                            cmd.Parameters.AddWithValue("@r1From", r1From.Text);
                            cmd.Parameters.AddWithValue("@r1FromDate", string.IsNullOrEmpty(r1FromDate.Text) ? (object)DBNull.Value : r1FromDate.Text);
                            cmd.Parameters.AddWithValue("@r1To", r1To.Text);
                            //cmd.Parameters.AddWithValue("@r1ToDate", string.IsNullOrEmpty(r1ToDate.Text) ? (object)DBNull.Value : r1ToDate.Text);
                            cmd.Parameters.AddWithValue("@r2From", r2From.Text);
                            cmd.Parameters.AddWithValue("@r2FromDate", string.IsNullOrEmpty(r2FromDate.Text) ? (object)DBNull.Value : r2FromDate.Text);
                            cmd.Parameters.AddWithValue("@r2To", r2To.Text);
                            //cmd.Parameters.AddWithValue("@r2ToDate", string.IsNullOrEmpty(r2ToDate.Text) ? (object)DBNull.Value : r2ToDate.Text);
                            cmd.Parameters.AddWithValue("@r3From", r3From.Text);
                            cmd.Parameters.AddWithValue("@r3FromDate", string.IsNullOrEmpty(r3FromDate.Text) ? (object)DBNull.Value : r3FromDate.Text);
                            cmd.Parameters.AddWithValue("@r3To", r3To.Text);
                            //cmd.Parameters.AddWithValue("@r3ToDate", string.IsNullOrEmpty(r3ToDate.Text) ? (object)DBNull.Value : r3ToDate.Text);
                            cmd.Parameters.AddWithValue("@r4From", r4From.Text);
                            cmd.Parameters.AddWithValue("@r4FromDate", string.IsNullOrEmpty(r4FromDate.Text) ? (object)DBNull.Value : r4FromDate.Text);
                            cmd.Parameters.AddWithValue("@r4To", r4To.Text);
                            //cmd.Parameters.AddWithValue("@r4ToDate", string.IsNullOrEmpty(r4ToDate.Text) ? (object)DBNull.Value : r4ToDate.Text);
                            cmd.Parameters.AddWithValue("@r5From", r5From.Text);
                            cmd.Parameters.AddWithValue("@r5FromDate", string.IsNullOrEmpty(r5FromDate.Text) ? (object)DBNull.Value : r5FromDate.Text);
                            cmd.Parameters.AddWithValue("@r5To", r5To.Text);
                            //cmd.Parameters.AddWithValue("@r5ToDate", string.IsNullOrEmpty(r5ToDate.Text) ? (object)DBNull.Value : r5ToDate.Text);


                            // Execute the update query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {

                                // The update was successful
                                Response.Write("<script>alert ('Travel Arrangement Submitted!'); window.location.href = 'AdminDashboard.aspx'; </script>");

                            }
                            else
                            {
                                // No rows were affected, meaning no matching travel request ID was found
                                Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                            }
                        }
                    }


                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during update route. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }
        }
       
        private void updateTravelArranged()
        {
            if (Session["arrangementID"] != null && Session["accomodationID"] != null && Session["flightID"] != null && Session["transferID"] != null)
            {
                string arrangementID = Session["arrangementID"].ToString();
                string accomodationID = Session["accomodationID"].ToString();
                string flight = Session["flightID"].ToString();
                string transfer = Session["transferID"].ToString();


                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE travelArranged SET arrangedAccomodationID = @accomodationID, arrangedFlightID = @flightID, arrangedTransferID = @transferID WHERE arrangedID = @ID";

                        // Set parameters for updating request status
                        cmd.Parameters.AddWithValue("@ID", arrangementID);
                        cmd.Parameters.AddWithValue("@accomodationID", accomodationID);
                        cmd.Parameters.AddWithValue("@flightID", flight);
                        cmd.Parameters.AddWithValue("@transferID", transfer);

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            updateRequestStat();
                        }
                        else
                        {
                            // No rows were affected, meaning no matching travel request ID was found
                            Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                        }
                    }
                }


            }
        }
        private void updateRequestStat()
        {
            try
            {
                if (Session["clickedRequest"] != null)
                {
                    string requestId = Session["clickedRequest"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE travelRequest SET travelReqStatus = @newStatus WHERE travelRequestID = @ID";

                            // Set parameters for updating request status
                            cmd.Parameters.AddWithValue("@newStatus", "In-progress");
                            cmd.Parameters.AddWithValue("@ID", requestId);

                            // Execute the update query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Fetch employee's first name and last name
                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT travelFname, travelLname FROM travelRequest WHERE travelRequestID = @ID";
                                cmd.Parameters.AddWithValue("@ID", requestId);

                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string firstName = reader["travelFname"].ToString();
                                        string lastName = reader["travelLname"].ToString();

                                        //change process status into ARRANGED
                                        changeReqStatus();

                                        // Display alert message with employee's name
                                        string alertMessage = "Travel Arrangement for " + firstName + " " + lastName + " has been submitted";
                                        Response.Write("<script>alert('" + alertMessage + "'); window.location.href = 'arrangedRequest.aspx'; </script>");
                                    }
                                }
                            }
                            else
                            {
                                // No rows were affected, meaning no matching travel request ID was found
                                Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                            }
                        }
                    }
                }
                else
                {
                    // Session is expired, redirect to login page
                    Response.Write("<script>alert('Session Expired! Please login again.'); window.location.href = '../LoginPage.aspx'; </script>");
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request status update", ex);
                Response.Write("<script>alert('An error occurred during travel request status update. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }
        }
        private void changeReqStatus()
        {
            try
            {
                if (Session["clickedRequest"] != null)
                {
                    string requestId = Session["clickedRequest"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE travelRequest SET travelProcessStat = @newStatus WHERE travelRequestID = @ID";

                            // Set parameters for updating request status
                            cmd.Parameters.AddWithValue("@newStatus", "Arranged Request");
                            cmd.Parameters.AddWithValue("@ID", requestId);

                            // Execute the update query
                            int rowsAffected = cmd.ExecuteNonQuery();

                        }
                    }
                }
                else
                {
                    // Session is expired, redirect to login page
                    Response.Write("<script>alert('Session Expired! Please login again.'); window.location.href = '../LoginPage.aspx'; </script>");
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request status update", ex);
                Response.Write("<script>alert('An error occurred during travel request status update. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }


        }

        protected void getsavedHotels_Click(object sender, EventArgs e)
        {
            string employeeID = empID.Text;

            if (employeeID != null)
            {

                // Query the database to retrieve the request details based on the ID
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM travelAccomodation WHERE arrangeEmpID = @empID";
                        cmd.Parameters.AddWithValue("@empID", employeeID);

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



                                    hotelAccomodations.Style["display"] = "block";
                                    hotel.Text = Name;
                                    hotelAddress.Text = Address;
                                    hotelPhone.Text = contact;

                                    if (!string.IsNullOrEmpty(Name2))
                                    {
                                        hotel2.Style["display"] = "block";
                                        hotelname2.Text = Name2;
                                        address2.Text = Address2;
                                        phone2.Text = contact2;
                                        Button5.Style["display"] = "none";

                                    }
                                    else
                                    {
                                        hotel2.Style["display"] = "none";

                                    }
                                    if (!string.IsNullOrEmpty(Name3))
                                    {
                                        hotel3.Style["display"] = "block";
                                        hotelname3.Text = Name3;
                                        address3.Text = Address3;
                                        phone3.Text = contact3;
                                        Button6.Style["display"] = "none";

                                    }
                                    if (!string.IsNullOrEmpty(Name4))
                                    {
                                        hotel4.Style["display"] = "block";
                                        hotelname4.Text = Name4;
                                        address4.Text = Address4;
                                        phone4.Text = contact4;
                                        Button7.Style["display"] = "none";

                                    }
                                    if (!string.IsNullOrEmpty(Name5))
                                    {
                                        hotel5.Style["display"] = "block";
                                        hotelname5.Text = Name5;
                                        address5.Text = Address5;
                                        phone5.Text = contact5;
                                        Button8.Style["display"] = "none";

                                    }
                                Response.Write("<script>alert('Traveler's saved Hotel Accomodations Retrieved')</script>");
                                getsavedHotels.Style["display"] = "none";
                            }
                            else
                            {
                                Response.Write("<script>alert('No Hotel Accomodations Exist for Traveler')</script>");
                                hotelAccomodations.Style["display"] = "block";
                                getsavedHotels.Style["display"] = "none";

                            }
                        }

                    }
                }
            }


        }

        protected void accomodations_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //protected void uploadButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string empFname = empFName.Text.Trim();

        //        // Create a directory path using empFname
        //        string folderPath = Path.Combine(Server.MapPath("/PDFs/travelArrangements"), empFname);

        //        // Check if the directory exists, if not, create it
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }

        //        // Loop through the uploaded files
        //        HttpFileCollection attachmentsCollection = Request.Files;
        //        if (attachmentsCollection.Count > 0)
        //        {
        //            for (int i = 0; i < attachmentsCollection.Count; i++)
        //            {
        //                HttpPostedFile attachment = attachmentsCollection[i];

        //                if (attachment.ContentLength > 0)
        //                {
        //                    string filename = Server.HtmlEncode(empFname + "_" + System.IO.Path.GetFileName(attachment.FileName));
        //                    string extension = System.IO.Path.GetExtension(filename).ToLower();

        //                    if (extension == ".pdf")
        //                    {
        //                        if (attachment.ContentLength < 4100000)
        //                        {
        //                            string savePath = System.IO.Path.Combine(folderPath, filename);
        //                            attachment.SaveAs(savePath);

        //                            Session["pdfPath_" + i] = savePath;
        //                            Session["filename_" + i] = filename;

        //                            // Log success message to the console
        //                            Console.WriteLine("File uploaded successfully: " + filename);
        //                        }
        //                        else
        //                        {
        //                            Response.Write("<script>alert('File " + filename + " was not uploaded because the file size is more than 4MB.')</script>");
        //                            uploadBlock.Style["display"] = "block";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Response.Write("<script>alert('Invalid File Upload. Please upload a PDF file.')</script>");
        //                        uploadBlock.Style["display"] = "block";
        //                    }
        //                }
        //            }
        //            // Display the uploaded PDF files
        //            DisplayPDFs(folderPath);

        //            Response.Write("<script>alert('All files uploaded successfully.')</script>");
        //            uploadBlock.Style["display"] = "none";
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Upload failed: No files selected.')</script>");
        //            uploadBlock.Style["display"] = "block";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception to the console
        //        Console.WriteLine("Error uploading file: " + ex.Message);

        //        // Display error message in an alert box and in the console
        //        Response.Write("<script>alert('An error occurred while uploading the file. Please try again.')</script>");
        //        Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre>");
        //    }
        //}
        //private void DisplayPDFs(string folderPath)
        //{
        //    uploadAttachments.Style["display"] = "none";
        //    // Get all PDF files in the folder
        //    string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

        //    // Create HTML to display PDFs in iframes
        //    StringBuilder html = new StringBuilder();
        //    foreach (string pdfFile in pdfFiles)
        //    {
        //        string fileName = Path.GetFileName(pdfFile);
        //        string pdfPath = "/PDFs/travelArrangements/" + empFName.Text.Trim() + "/" + fileName;
        //        html.Append("<iframe src='" + pdfPath + "' style='width:50%; height:300px;'></iframe>");
        //    }

        //    // Display the HTML content in a placeholder or another container on your page
        //    pdfPlaceholder.Controls.Add(new LiteralControl(html.ToString()));
        //}

    }
}