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
    public partial class billingInformation : System.Web.UI.Page
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
                        changeReqStatus();
                        populateEmployeeDetails();

                    }
                }
                else
                {
                    Response.Write("<script> window.location.href = '../LoginPage.aspx'; </script>");

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
                            cmd.Parameters.AddWithValue("@newStatus", "Billing");
                            cmd.Parameters.AddWithValue("@ID", requestId);

                            Session["requestStatus"] = "Billing";
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
                                    string status = reader["travelProcessStat"].ToString();


                                    // Display or use the retrieved request details

                                    //empID.Text = employeeID;
                                    //empFName.Text = employeeFname + " " + employeeMname + " " + employeeLname;
                                    //empLevel.Text = employeeLevel;

                                    //empEmail.Text = email;
                                    //empMobile.Text = employeePhone;

                                    //empCode.Text = employeeProjCode;
                                    //empFacility.Text = travelFacility;
                                    //empDeptUnit.Text = DU;
                                    billingLabel.Text = "In-Progress:" + " " + status;


                                    //if (!string.IsNullOrEmpty(employeeBirth))
                                    //{
                                    //    //Parse the date string into a DateTime object
                                    //    DateTime arrivalDateTime;
                                    //    if (DateTime.TryParse(employeeBirth, out arrivalDateTime))
                                    //    {
                                    //        //Format the DateTime object into the desired format
                                    //        string formattedArrivalDate = arrivalDateTime.ToString("MM/dd/yyyy");

                                    //        //Assign the formatted date to the TextBox
                                    //        empBdate.Text = formattedArrivalDate;
                                    //    }
                                    //}

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
                Response.Write("<script>alert('An error occurred while retrieving the employee details.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }
        }

        protected void calculate_Click(object sender, EventArgs e)
        {
            try
            {

                // CHARGES
                string hotelCharges = hotelChargesTotal.Text;
                string planeCharges = planeFaresTotal.Text;
                string transferCharges = transfersTotal.Text;
                string perDiemInput = perDiem.Text;

                // Parse and sum the inputs
                decimal hotelBill = ParseInput(hotelCharges);
                decimal planeBill = ParseInput(planeCharges);
                decimal perDiemTotal = ParseInput(perDiemInput);
                decimal transferBill = ParseInput(transferCharges);

                // Display the total expenses
                decimal totalExpenses = hotelBill + planeBill + perDiemTotal + transferBill;
                totalExpensesTxt.Text = "₱" + totalExpenses.ToString("N2");

                totalBlock.Style["display"] = "block";
                calculate.Style["display"] = "none";
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


        // Helper method to parse the input string and handle both "1000" and "1,000" formats
        private decimal ParseInput(string input)
        {
            // Remove the peso sign if present
            input = input.Replace("₱", "");

            // Remove commas
            input = input.Replace(",", "");

            // Parse the input string into a decimal
            decimal value;
            if (decimal.TryParse(input, out value))
            {
                return value;
            }
            else
            {
                // Handle invalid input (e.g., display error message)
                return 0; // Or handle it based on your requirements
            }
        }

        protected void submitArrangement_Click(object sender, EventArgs e)
        {
            string request = Session["clickedRequest"].ToString();

            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "B" + random;

            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelBilling (billingID, billHotel, billPlane,billTransfers, billPerDiem, billTotal, billTravelID)"
                                            + "VALUES("
                                            + "@ID,"
                                            + "@hotel,"
                                            + "@plane,"
                                            + "@transfers,"
                                            + "@perDiem,"
                                            + "@total,"
                                            + "@travelID)";

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@hotel", hotelChargesTotal.Text);
                        cmd.Parameters.AddWithValue("@plane", planeFaresTotal.Text);
                        cmd.Parameters.AddWithValue("@transfers", totalTransferCharges.Text);
                        cmd.Parameters.AddWithValue("@perDiem", perDiem.Text);
                        cmd.Parameters.AddWithValue("@total", totalExpensesTxt.Text);
                        cmd.Parameters.AddWithValue("@travelID", request);


                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            updateRequestStatus();
                           

                        }
                    }
                }



            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }

        }
        private void updateRequestStatus()
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
                            cmd.Parameters.AddWithValue("@newStatus", "Completed");
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
                                        string alertMessage = "Billing Information for Travel Request from " + firstName + " " + lastName + " has been successfully processed";
                                        Response.Write("<script>alert('" + alertMessage + "'); window.location.href = 'AdminDashboard.aspx'; </script>");
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

        protected void hotelTotal_Click(object sender, EventArgs e)
        {

                      
            //hotel charges
            string hotelc1 = hotelCharges.Text;
            string hotelc2 = hotelCharges2.Text;
            string hotelc3 = hotelCharges3.Text;
            string hotelc4 = hotelCharges4.Text;
            string hotelc5 = hotelCharges5.Text;

            

            // Parse and sum the inputs
            decimal HotelBill1 = ParseInput(hotelc1);
            decimal HotelBill2 = ParseInput(hotelc2);
            decimal HotelBill3 = ParseInput(hotelc3);
            decimal HotelBill4 = ParseInput(hotelc4);
            decimal HotelBill5 = ParseInput(hotelc5);



            decimal totalHotelCharges = HotelBill1 + HotelBill2 + HotelBill3 + HotelBill4 + HotelBill5;
            hotelChargesTotal.Text = "₱" + totalHotelCharges.ToString("N2");

            hotelTotal.Style["display"] = "none";
            Button5.Style["display"] = "none";

            if (hotel2.Visible)
            {
                hotel2.Style["display"] = "block";
                Button1.Style["display"] = "none";

            }
            if (hotel3.Visible)
            {
                hotel3.Style["display"] = "block";
                Button2.Style["display"] = "none";

            }
            if (hotel4.Visible)
            {
                hotel4.Style["display"] = "block";
                Button3.Style["display"] = "none";

            }
            if (hotel5.Visible)
            {
                hotel5.Style["display"] = "block";
                Button4.Style["display"] = "none";

            }




        }


        protected void calculatePlaneFare_Click(object sender, EventArgs e)
        {
            string plane1 = planeFare.Text;
            string plane2 = planeFare2.Text;
            string plane3 = planeFare3.Text;
            string plane4 = planeFare4.Text;
            string plane5 = planeFare5.Text;

            decimal planeBill1 = ParseInput(plane1);
            decimal planeBill2 = ParseInput(plane2);
            decimal planeBill3 = ParseInput(plane3);
            decimal planeBill4 = ParseInput(plane4);
            decimal planeBill5 = ParseInput(plane5);

            decimal totalPlaneFare = planeBill1 + planeBill2 + planeBill3 + planeBill4 + planeBill5;
            planeFaresTotal.Text = "₱" + totalPlaneFare.ToString("N2");

        }

        

        protected void totalTransferCharges_Click(object sender, EventArgs e)
        {
            string transfer1 = trans1.Text;
            string transfer2 = trans2.Text;
            string transfer3 = trans3.Text;
            string transfer4 = trans4.Text;
            string transfer5 = trans5.Text;

            decimal transBill1 = ParseInput(transfer1);
            decimal transBill2 = ParseInput(transfer2);
            decimal transBill3 = ParseInput(transfer3);
            decimal transBill4 = ParseInput(transfer4);
            decimal transBill5 = ParseInput(transfer5);

            decimal totalTransfers = transBill1 + transBill2 + transBill3 + transBill4 + transBill5;
            transfersTotal.Text = "₱" + totalTransfers.ToString("N2");


        }
    }
}