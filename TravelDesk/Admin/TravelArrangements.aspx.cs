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
                if (Session["clickedRequest"].ToString() != null)
                {
                    string request = Session["clickedRequest"].ToString();

                    if (!string.IsNullOrEmpty(request))
                    {
                        //string status = Session["reqStatus"].ToString();
                        //if (status != "Processing")
                        //{

                        //}


                        DisplayRequest();
                        populateEmployeeDetails();

                    }
                }
                else
                {
                    Response.Write("<script> window.location.href = '../LoginPage.aspx'; </script>");

                }

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
                                    string userID = reader["travelUserID"].ToString();
                                    string mobile = reader["travelMobilenum"].ToString();
                                    string level = reader["travelLevel"].ToString();

                                    // Retrieve other request details as needed

                                    // Display or use the retrieved request details
                                    employeeID.Text = userID;
                                    employeeName.Text = name;
                                    homeFacility.Text = facility;
                                    employeePhone.Text = mobile;
                                    employeeLevel.Text = level;

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
                                            rt.routeOFrom, rt.routeOTo, 
                                            rt.routeR1From, rt.routeR1To,
                                            rt.routeR2From, rt.routeR2To, 
                                            rt.routeM1From, rt.routeM1FromDate, 
                                            rt.routeM1To, rt.routeM1ToDate, 
                                            rt.routeM2From, rt.routeM2FromDate, 
                                            rt.routeM2To, rt.routeM2ToDate, 
                                            rt.routeM3From, rt.routeM3FromDate, 
                                            rt.routeM3To, rt.routeM3ToDate,
                                            rt.routeM4From, rt.routeM4FromDate, 
                                            rt.routeM4To, rt.routeM4ToDate, 
                                            rt.routeM5From, rt.routeM5FromDate, 
                                            rt.routeM5To, rt.routeM5ToDate
                                                                                                                                   
                                          FROM travelRequest tr
                                          LEFT JOIN route rt ON tr.travelRequestID = rt.routeTravelID
                                          WHERE tr.travelRequestID = @RequestId AND travelDraftStat = 'No'";


                            cmd.Parameters.AddWithValue("@RequestId", requestId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    approvalBlock.Style["display"] = "block";
                                    uploadBlock.Style["display"] = "block";
                                    pdfViewer.Style["display"] = "block";


                                    // Retrieve the request details from the reader
                                    string travelFacility = reader["travelHomeFacility"].ToString();
                                    string empID = reader["travelEmpID"].ToString();
                                    string empFname = reader["travelFname"].ToString();
                                    string empMname = reader["travelMname"].ToString();
                                    string empLname = reader["travelLname"].ToString();
                                    string empProjCode = reader["travelProjectCode"].ToString();
                                    string empPhone = reader["travelMobilenum"].ToString();
                                    string empLevel = reader["travelLevel"].ToString();
                                    string travelPurpose = reader["travelPurpose"].ToString();
                                    string travelDepartureDate = reader["travelDeparture"].ToString();
                                    string travelArrivalDate = reader["travelReturn"].ToString();
                                    string travelFrom = reader["travelFrom"].ToString();
                                    string travelTo = reader["travelTo"].ToString();
                                    string flight = reader["travelOptions"].ToString();
                                    string manager = reader["travelManager"].ToString();
                                    string proof = reader["travelProofPath"].ToString();
                                    string remarks = reader["travelRemarks"].ToString();


                                    //FOR FLIGHT DETAILS - ROUTE
                                    string oneFrom = reader["routeOFrom"] != DBNull.Value ? reader["routeOFrom"].ToString() : "";
                                    string oneTo = reader["routeOTo"] != DBNull.Value ? reader["routeOTo"].ToString() : "";
                                    string r1From = reader["routeR1From"] != DBNull.Value ? reader["routeR1From"].ToString() : "";
                                    string r1To = reader["routeR1To"] != DBNull.Value ? reader["routeR1To"].ToString() : "";
                                    string r2From = reader["routeR2From"] != DBNull.Value ? reader["routeR2From"].ToString() : "";
                                    string r2To = reader["routeR2To"] != DBNull.Value ? reader["routeR2To"].ToString() : "";
                                    string mul1From = reader["routeM1From"] != DBNull.Value ? reader["routeM1From"].ToString() : "";
                                    string mul1FromDate = reader["routeM1FromDate"] != DBNull.Value ? reader["routeM1FromDate"].ToString() : "";
                                    string mul1To = reader["routeM1To"] != DBNull.Value ? reader["routeM1To"].ToString() : "";
                                    string mul1ToDate = reader["routeM1ToDate"] != DBNull.Value ? reader["routeM1ToDate"].ToString() : "";
                                    string mul2From = reader["routeM2From"] != DBNull.Value ? reader["routeM2From"].ToString() : "";
                                    string mul2FromDate = reader["routeM2FromDate"] != DBNull.Value ? reader["routeM2FromDate"].ToString() : "";
                                    string mul2To = reader["routeM2To"] != DBNull.Value ? reader["routeM2To"].ToString() : "";
                                    string mul2ToDate = reader["routeM2ToDate"] != DBNull.Value ? reader["routeM2ToDate"].ToString() : "";
                                    string mul3From = reader["routeM3From"] != DBNull.Value ? reader["routeM3From"].ToString() : "";
                                    string mul3FromDate = reader["routeM3FromDate"] != DBNull.Value ? reader["routeM3FromDate"].ToString() : "";
                                    string mul3To = reader["routeM3To"] != DBNull.Value ? reader["routeM3To"].ToString() : "";
                                    string mul3ToDate = reader["routeM3ToDate"] != DBNull.Value ? reader["routeM3ToDate"].ToString() : "";
                                    string mul4From = reader["routeM4From"] != DBNull.Value ? reader["routeM4From"].ToString() : "";
                                    string mul4FromDate = reader["routeM4FromDate"] != DBNull.Value ? reader["routeM4FromDate"].ToString() : "";
                                    string mul4To = reader["routeM4To"] != DBNull.Value ? reader["routeM4To"].ToString() : "";
                                    string mul4ToDate = reader["routeM4ToDate"] != DBNull.Value ? reader["routeM4ToDate"].ToString() : "";
                                    string mul5From = reader["routeM5From"] != DBNull.Value ? reader["routeM5From"].ToString() : "";
                                    string mul5FromDate = reader["routeM5FromDate"] != DBNull.Value ? reader["routeM5FromDate"].ToString() : "";
                                    string mul5To = reader["routeM5To"] != DBNull.Value ? reader["routeM5To"].ToString() : "";
                                    string mul5ToDate = reader["routeM5ToDate"] != DBNull.Value ? reader["routeM5ToDate"].ToString() : "";

                                    // Display or use the retrieved request details
                                    TextBox3.Text = travelFacility;
                                    TextBox4.Text = empID;
                                    employeeFName.Text = empFname;
                                    employeeMName.Text = empMname;
                                    employeeLName.Text = empLname;
                                    employeeProjCode.Text = empProjCode;
                                    TextBox5.Text = empPhone;
                                    TextBox6.Text = empLevel;
                                    employeeManager.Text = manager;
                                    pdfViewer.Src = proof;
                                    flightOptions.Text = flight;
                                    employeePurpose.Text = travelPurpose;
                                    employeeFrom.Text = travelFrom;
                                    employeeTo.Text = travelTo;
                                    employeeRemarks.Text = remarks;

                                    if (!string.IsNullOrEmpty(travelArrivalDate))
                                    {
                                        // Parse the date string into a DateTime object
                                        DateTime arrivalDateTime;
                                        if (DateTime.TryParse(travelArrivalDate, out arrivalDateTime))
                                        {
                                            // Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                            // Assign the formatted date to the TextBox
                                            employeeArrivalDate.Text = formattedArrivalDate;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(travelDepartureDate))
                                    {
                                        // Parse the date string into a DateTime object
                                        DateTime arrivalDateTime;
                                        if (DateTime.TryParse(travelDepartureDate, out arrivalDateTime))
                                        {
                                            // Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                            // Assign the formatted date to the TextBox
                                            employeeDepartureDate.Text = formattedArrivalDate;
                                        }
                                    }




                                    if (!string.IsNullOrEmpty(oneFrom))
                                    {
                                        oneWaynput.Style["display"] = "block";
                                        onewayFrom.Text = oneFrom;
                                        onewayTo.Text = oneTo;
                                    }
                                    else if (!string.IsNullOrEmpty(r1From))
                                    {
                                        roundTripInput.Style["display"] = "block";
                                        round1From.Text = r1From;
                                        round1To.Text = r1To;
                                        round2From.Text = r2From;
                                        round2To.Text = r2To;
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

                                            if (!string.IsNullOrEmpty(mul1FromDate))
                                            {
                                                // Parse the date string into a DateTime object
                                                DateTime arrivalDateTime;
                                                if (DateTime.TryParse(mul1FromDate, out arrivalDateTime))
                                                {
                                                    // Format the DateTime object into the desired format
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                    // Assign the formatted date to the TextBox
                                                    TextBox11.Text = formattedArrivalDate;
                                                }
                                            }
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
                                            if (!string.IsNullOrEmpty(mul2FromDate))
                                            {
                                                // Parse the date string into a DateTime object
                                                DateTime arrivalDateTime;
                                                if (DateTime.TryParse(mul2FromDate, out arrivalDateTime))
                                                {
                                                    // Format the DateTime object into the desired format
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                    // Assign the formatted date to the TextBox
                                                    TextBox10.Text = formattedArrivalDate;
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

                                                if (!string.IsNullOrEmpty(mul3FromDate))
                                                {
                                                    // Parse the date string into a DateTime object
                                                    DateTime arrivalDateTime;
                                                    if (DateTime.TryParse(mul3FromDate, out arrivalDateTime))
                                                    {
                                                        // Format the DateTime object into the desired format
                                                        string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                        // Assign the formatted date to the TextBox
                                                        TextBox16.Text = formattedArrivalDate;
                                                    }
                                                }
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

                                                    if (!string.IsNullOrEmpty(mul4FromDate))
                                                    {
                                                        // Parse the date string into a DateTime object
                                                        DateTime arrivalDateTime;
                                                        if (DateTime.TryParse(mul4FromDate, out arrivalDateTime))
                                                        {
                                                            // Format the DateTime object into the desired format
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                            // Assign the formatted date to the TextBox
                                                            TextBox28.Text = formattedArrivalDate;
                                                        }
                                                    }
                                                    if (!string.IsNullOrEmpty(mul4To))
                                                    {
                                                        // Parse the date string into a DateTime object
                                                        DateTime arrivalDateTime;
                                                        if (DateTime.TryParse(mul4To, out arrivalDateTime))
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

                                                    if (!string.IsNullOrEmpty(mul5FromDate))
                                                    {
                                                        // Parse the date string into a DateTime object
                                                        DateTime arrivalDateTime;
                                                        if (DateTime.TryParse(mul5FromDate, out arrivalDateTime))
                                                        {
                                                            // Format the DateTime object into the desired format
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                                            // Assign the formatted date to the TextBox
                                                            TextBox20.Text = formattedArrivalDate;
                                                        }
                                                    }
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

        protected void submitArrangement_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "TA" + random;

            try
            {

                if (Session["userID"] != null && Session["clickedRequest"] != null)
                {
                    // Session values are not null, proceed with inserting into the database
                    string userID = Session["userID"].ToString();
                    string requestId = Session["clickedRequest"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO TravelArrangement (arrangeID, arrangeUserID, arrangeTravelID, arrangeAirline, arrangeAccomodations, arrangeHotelName , arrangeHotelAdd, arrangeHotelFrom, arrangeHotelTo, " +
                                "arrangeTransfer1, arrangeTransfer1Date, arrangeTransfer2, arrangeTransfer2Date, arrangeTransfer3, arrangeTransfer3Date, arrangeTransfer4, arrangeTransfer4Date, arrangeTransfer5, arrangeTransfer5Date, " +
                                "arrangeRequirements, arrangeNotes, arrangeDateCreated, routeM1From, routeM1FromDate, routeM1To, routeM1ToDate, routeM2From, routeM2FromDate, routeM2To, routeM2ToDate, routeM3From, routeM3FromDate, routeM3To, routeM3ToDate, routeM4From, routeM4FromDate, routeM4To, routeM4ToDate, routeM5From, routeM5FromDate, routeM5To, routeM5ToDate)"
                                + "VALUES (@ID, @userID, @travelID, @airline, @accomodations, @hotelName, @hotelAddress, @hotelFrom, @hotelTo, @transfer1, @transfer1Date, @transfer2, @transfer2Date, @transfer3, @transfer3Date, @transfer4, @transfer4Date, @transfer5, @transfer5Date, @requirements, @notes, @dateCreated," +
                                "@r1From, @r1FromDate, @r1To, @r1ToDate, @r2From, @r2FromDate, @r2To, @r2ToDate, @r3From, @r3FromDate, @r3To, @r3ToDate, @r4From, @r4FromDate, @r4To, @r4ToDate, @r5From, @r5FromDate, @r5To, @r5ToDate)";

                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue("@userID", userID);
                            cmd.Parameters.AddWithValue("@travelID", requestId);
                            cmd.Parameters.AddWithValue("@airline", airline.Text);
                            cmd.Parameters.AddWithValue("@accomodations", accomodations.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@hotelName", hotel.Text);
                            cmd.Parameters.AddWithValue("@hotelAddress", hotelAddress.Text);
                            cmd.Parameters.AddWithValue("@hotelFrom", string.IsNullOrEmpty(durationFrom.Text) ? (object)DBNull.Value : durationFrom.Text);
                            cmd.Parameters.AddWithValue("@hotelTo", string.IsNullOrEmpty(durationTo.Text) ? (object)DBNull.Value : durationTo.Text);
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
                            cmd.Parameters.AddWithValue("@requirements", ""); // Initialize the parameter outside the loop
                            cmd.Parameters.AddWithValue("@notes", additionalNotes.Text);
                            cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);
                            cmd.Parameters.AddWithValue("@r1From", r1From.Text);
                            cmd.Parameters.AddWithValue("@r1FromDate", string.IsNullOrEmpty(r1FromDate.Text) ? (object)DBNull.Value : r1FromDate.Text);
                            cmd.Parameters.AddWithValue("@r1To", r1To.Text);
                            cmd.Parameters.AddWithValue("@r1ToDate", string.IsNullOrEmpty(r1ToDate.Text) ? (object)DBNull.Value : r1ToDate.Text);
                            cmd.Parameters.AddWithValue("@r2From", r2From.Text);
                            cmd.Parameters.AddWithValue("@r2FromDate", string.IsNullOrEmpty(r2FromDate.Text) ? (object)DBNull.Value : r2FromDate.Text);
                            cmd.Parameters.AddWithValue("@r2To", r2To.Text);
                            cmd.Parameters.AddWithValue("@r2ToDate", string.IsNullOrEmpty(r2ToDate.Text) ? (object)DBNull.Value : r2ToDate.Text);
                            cmd.Parameters.AddWithValue("@r3From", r3From.Text);
                            cmd.Parameters.AddWithValue("@r3FromDate", string.IsNullOrEmpty(r3FromDate.Text) ? (object)DBNull.Value : r3FromDate.Text);
                            cmd.Parameters.AddWithValue("@r3To", r3To.Text);
                            cmd.Parameters.AddWithValue("@r3ToDate", string.IsNullOrEmpty(r3ToDate.Text) ? (object)DBNull.Value : r3ToDate.Text);
                            cmd.Parameters.AddWithValue("@r4From", r4From.Text);
                            cmd.Parameters.AddWithValue("@r4FromDate", string.IsNullOrEmpty(r4FromDate.Text) ? (object)DBNull.Value : r4FromDate.Text);
                            cmd.Parameters.AddWithValue("@r4To", r4To.Text);
                            cmd.Parameters.AddWithValue("@r4ToDate", string.IsNullOrEmpty(r4ToDate.Text) ? (object)DBNull.Value : r4ToDate.Text);
                            cmd.Parameters.AddWithValue("@r5From", r5From.Text);
                            cmd.Parameters.AddWithValue("@r5FromDate", string.IsNullOrEmpty(r5FromDate.Text) ? (object)DBNull.Value : r5FromDate.Text);
                            cmd.Parameters.AddWithValue("@r5To", r5To.Text);
                            cmd.Parameters.AddWithValue("@r5ToDate", string.IsNullOrEmpty(r5ToDate.Text) ? (object)DBNull.Value : r5ToDate.Text);


                            //for the REQUIREMENTS
                            foreach (ListItem item in requirements.Items)
                            {
                                if (item.Selected)
                                {
                                    // Append the selected requirements to the parameter value
                                    cmd.Parameters["@requirements"].Value += item.Value + ", ";
                                }
                            }

                            // Execute the command after setting all parameters
                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                //UPDATE STATUS TO "ARRANGED"
                                updateRequestStat();
                                //Response.Write("<script>alert ('Travel Arrangement Submitted!'); window.location.href = 'AdminDashboard.aspx'; </script>");

                            }
                            else
                            {
                                Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                            }
                        }
                    }
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
                            cmd.Parameters.AddWithValue("@r1ToDate", string.IsNullOrEmpty(r1ToDate.Text) ? (object)DBNull.Value : r1ToDate.Text);
                            cmd.Parameters.AddWithValue("@r2From", r2From.Text);
                            cmd.Parameters.AddWithValue("@r2FromDate", string.IsNullOrEmpty(r2FromDate.Text) ? (object)DBNull.Value : r2FromDate.Text);
                            cmd.Parameters.AddWithValue("@r2To", r2To.Text);
                            cmd.Parameters.AddWithValue("@r2ToDate", string.IsNullOrEmpty(r2ToDate.Text) ? (object)DBNull.Value : r2ToDate.Text);
                            cmd.Parameters.AddWithValue("@r3From", r3From.Text);
                            cmd.Parameters.AddWithValue("@r3FromDate", string.IsNullOrEmpty(r3FromDate.Text) ? (object)DBNull.Value : r3FromDate.Text);
                            cmd.Parameters.AddWithValue("@r3To", r3To.Text);
                            cmd.Parameters.AddWithValue("@r3ToDate", string.IsNullOrEmpty(r3ToDate.Text) ? (object)DBNull.Value : r3ToDate.Text);
                            cmd.Parameters.AddWithValue("@r4From", r4From.Text);
                            cmd.Parameters.AddWithValue("@r4FromDate", string.IsNullOrEmpty(r4FromDate.Text) ? (object)DBNull.Value : r4FromDate.Text);
                            cmd.Parameters.AddWithValue("@r4To", r4To.Text);
                            cmd.Parameters.AddWithValue("@r4ToDate", string.IsNullOrEmpty(r4ToDate.Text) ? (object)DBNull.Value : r4ToDate.Text);
                            cmd.Parameters.AddWithValue("@r5From", r5From.Text);
                            cmd.Parameters.AddWithValue("@r5FromDate", string.IsNullOrEmpty(r5FromDate.Text) ? (object)DBNull.Value : r5FromDate.Text);
                            cmd.Parameters.AddWithValue("@r5To", r5To.Text);
                            cmd.Parameters.AddWithValue("@r5ToDate", string.IsNullOrEmpty(r5ToDate.Text) ? (object)DBNull.Value : r5ToDate.Text);


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
                            cmd.Parameters.AddWithValue("@newStatus", "Arranged");
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
    }
}