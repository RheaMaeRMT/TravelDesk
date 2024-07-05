using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TravelDesk.Employee
{
    public partial class DomesticRequest : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

            }

            checkStatus();
        }

       private void checkStatus()
        {
            if (Session["requestStatus"] != null)
            {
                string statusDraft = Session["requestStatus"].ToString();

                if (statusDraft == "Draft")
                {
                    displayRequestDetails();
                }
            }

        }
        private void displayRequestDetails()
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
                                          WHERE tr.travelRequestID = @RequestId";

                            cmd.Parameters.AddWithValue("@RequestId", clickedRequest);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
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
                                    string remarks = reader["travelRemarks"].ToString();
                                    string status = reader["travelReqStatus"].ToString();



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
                                            string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

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
                                                string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

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

                                        //if (!string.IsNullOrEmpty(r1return))
                                        //{
                                        //    // Parse the date string into a DateTime object
                                        //    DateTime arrivalDateTime;
                                        //    if (DateTime.TryParse(r1return, out arrivalDateTime))
                                        //    {
                                        //        // Format the DateTime object into the desired format
                                        //        string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                        //        // Assign the formatted date to the TextBox
                                        //        roun2.Text = formattedArrivalDate;
                                        //    }
                                        //}
                                        //if (!string.IsNullOrEmpty(r1depart))
                                        //{
                                        //    // Parse the date string into a DateTime object
                                        //    DateTime arrivalDateTime;
                                        //    if (DateTime.TryParse(r1depart, out arrivalDateTime))
                                        //    {
                                        //        // Format the DateTime object into the desired format
                                        //        string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                        //        // Assign the formatted date to the TextBox
                                        //        round2departure.Text = formattedArrivalDate;
                                        //    }
                                        //}
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
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

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
                                                    string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

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
                                                        string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

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
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

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
                                                            string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                                            // Assign the formatted date to the TextBox
                                                            TextBox22.Text = formattedArrivalDate;
                                                        }
                                                    }

                                                }


                                            }

                                        }
                                    }


                                    Session["currentStatus"] = status;
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

        protected void submitRequestbtn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int random = rand.Next(50, 999);

            //CHECK IF NEW REQUEST OR FROM DRAFTS
            if (Session["requestStatus"] != null)
            {
                string statusDraft = Session["requestStatus"].ToString();

                if (statusDraft == "Draft")
                {
                    //UPDATE REQUEST DETAILS HERE
                }
            }
            try
            {

                string levelText = employeeLevel.Text;
                int level;

                if (int.TryParse(levelText, out level))
                {
                    if (level <= 9)
                    {
                        //FILENAME IS NEEDED SINCE LEVEL IS <9 
                        if (Session["filename"] != null)
                        {
                            if (Session["userID"] != null)
                            {
                                // Session values are not null, proceed with inserting into the database
                                string filename = Session["filename"].ToString();
                                string imgPath = Session["pdfPath"].ToString();
                                string userID = Session["userID"].ToString();

                                using (var db = new SqlConnection(connectionString))
                                {
                                    db.Open();
                                    using (var cmd = db.CreateCommand())
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname, travelBdate, travelDU, travelEmail, travelLevel, travelMobilenum, travelProjectCode, travelPurpose, travelReqStatus, travelRemarks, travelType, travelOptions, travelUserID, travelProofname, travelProofPath, travelDateCreated, travelDraftStat)"
                                            + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @empBdate, @empDu, @empEmail, @level, @mobile, @projCode, @purpose, @reqStatus, @remarks, @type, @options, @userID, @proofname, @proofpath, @created, @draftStat)";


                                        //FOR THE UNIQUE REQUEST ID
                                        string fname = employeeFName.Text;
                                        string lname = employeeLName.Text;
                                        string emp = employeeID.Text;
                                        // using the first letter of employeeFName and employeeLName
                                        char firstNameInitial = fname[0];
                                        char lastNameInitial = lname[0];
                                        char lastID = emp[2];

                                        // Concatenate the first initials into a string
                                        string Name = firstNameInitial.ToString() + lastNameInitial.ToString() + lastID.ToString();

                                        string ID = "DR" + levelText + random + Name;
                                        cmd.Parameters.AddWithValue("@ID", ID);

                                        string location = homeFacility.Text;

                                        // Check if the textbox with ID "othersFacility" is displayed
                                        if (location == "Others")
                                        {
                                            // If displayed, use its value as @location
                                            cmd.Parameters.AddWithValue("@location", othersFacility.Text);
                                        }
                                        else
                                        {
                                            // If hidden, use the selected item in the homeFacility dropdown
                                            cmd.Parameters.AddWithValue("@location", homeFacility.SelectedItem.Text);
                                        }

                                        cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                                        cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                                        cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                                        cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                                        cmd.Parameters.AddWithValue("@empBdate", employeeBdate.Text);
                                        cmd.Parameters.AddWithValue("@empDu", employeeDU.Text);
                                        cmd.Parameters.AddWithValue("@empEmail", employeeEmail.Text);
                                        cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                                        cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                                        cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                                        string purpose = employeePurpose.Text;
                                        if (purpose == "Others")
                                        {
                                            cmd.Parameters.AddWithValue("@purpose", otherspecified.Text);

                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);

                                        }
                                        cmd.Parameters.AddWithValue("@reqStatus", "New");
                                        cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);                                       
                                        cmd.Parameters.AddWithValue("@type", "Domestic Travel");
                                        cmd.Parameters.AddWithValue("@options", flightOptions.SelectedValue);
                                        cmd.Parameters.AddWithValue("@userID", userID);
                                        cmd.Parameters.AddWithValue("@proofname", filename);
                                        cmd.Parameters.AddWithValue("@proofpath", imgPath);
                                        cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
                                        cmd.Parameters.AddWithValue("@draftStat", "No");

                                        var ctr = cmd.ExecuteNonQuery();

                                        if (ctr >= 1)
                                        {

                                            insertRoute(ID);
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
                        else
                        {
                            // Session values are null
                            Response.Write("<script>alert('Invalid File upload. Please try again')</script>");
                        }
                    }
                    //PROCEED WITH INSERTION WITHOUT FILE UPLOAD
                    else
                    {
                        if (Session["userID"] != null)
                        {
                            // Session values are not null, proceed with inserting into the database
                            string filename = Session["filename"] != null ? Session["filename"].ToString() : string.Empty;
                            string imgPath = Session["pdfPath"] != null ? Session["pdfPath"].ToString() : string.Empty;
                            string userID = Session["userID"].ToString();

                            using (var db = new SqlConnection(connectionString))
                            {
                                db.Open();
                                using (var cmd = db.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname, travelBdate, travelDU, travelEmail, travelLevel, travelMobilenum, travelProjectCode, travelPurpose, travelReqStatus, travelRemarks, travelType, travelOptions, travelUserID, travelProofname, travelProofPath, travelDateCreated, travelDraftStat)"
                                        + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @empBdate, @empDu, @empEmail, @level, @mobile, @projCode, @purpose, @reqStatus, @remarks, @type, @options, @userID, @proofname, @proofpath, @created, @draftStat)";

                                    //FOR THE UNIQUE REQUEST ID
                                    string fname = employeeFName.Text;
                                    string lname = employeeLName.Text;
                                    string emp = employeeID.Text;
                                    // using the first letter of employeeFName and employeeLName
                                    char firstNameInitial = fname[0];
                                    char lastNameInitial = lname[0];
                                    char lastID = emp[2];

                                    // Concatenate the first initials into a string
                                    string Name = firstNameInitial.ToString() + lastNameInitial.ToString() + lastID.ToString();

                                    string ID = "DR" + levelText + random + Name;
                                    cmd.Parameters.AddWithValue("@ID", ID);

                                    string location = homeFacility.Text;

                                    // Check if the textbox with ID "othersFacility" is displayed
                                    if (location == "Others")
                                    {
                                        // If displayed, use its value as @location
                                        cmd.Parameters.AddWithValue("@location", othersFacility.Text);
                                    }
                                    else
                                    {
                                        // If hidden, use the selected item in the homeFacility dropdown
                                        cmd.Parameters.AddWithValue("@location", homeFacility.SelectedItem.Text);
                                    }

                                    cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                                    cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                                    cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                                    cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                                    cmd.Parameters.AddWithValue("@empBdate", employeeBdate.Text);
                                    cmd.Parameters.AddWithValue("@empDu", employeeDU.Text);
                                    cmd.Parameters.AddWithValue("@empEmail", employeeEmail.Text);
                                    cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                                    cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                                    cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);

                                    string purpose = employeePurpose.Text;
                                    if (purpose == "Others")
                                    {
                                        cmd.Parameters.AddWithValue("@purpose", otherspecified.Text);

                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);

                                    }
                                    cmd.Parameters.AddWithValue("@reqStatus", "New");
                                    cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                                    cmd.Parameters.AddWithValue("@type", "Domestic Travel");
                                    cmd.Parameters.AddWithValue("@options", flightOptions.SelectedValue);
                                    cmd.Parameters.AddWithValue("@userID", userID);
                                    cmd.Parameters.AddWithValue("@proofname", filename);
                                    cmd.Parameters.AddWithValue("@proofpath", imgPath);
                                    cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
                                    cmd.Parameters.AddWithValue("@draftStat", "Yes");

                                    var ctr = cmd.ExecuteNonQuery();

                                    if (ctr >= 1)
                                    {

                                        insertRoute(ID);
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
                }
                else
                {

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

        }

        //UPLOAD IMAGE
        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    string saveDIR = Server.MapPath("/approvalProofs");
        //    try
        //    {
        //        if (employeeUpload.HasFile)
        //        {
        //            string filename = Server.HtmlEncode(employeeUpload.FileName);
        //            string extension = System.IO.Path.GetExtension(filename);
        //            int filesize = employeeUpload.PostedFile.ContentLength;
        //            if (File.Exists(System.IO.Path.Combine(saveDIR, filename)))
        //            {
        //                Response.Write("<script>alert('File already exists.')</script>");
        //                //uploadBlock.Style["dipsplay"] = "block";

        //            }
        //            else
        //            {
        //                if ((extension == ".jpg") || (extension == ".jpeg") || (extension == ".png") || (extension == ".JPG") || (extension == ".JPEG") || (extension == ".PNG"))
        //                {
        //                    if (filesize < 4100000)
        //                    {
        //                        string savePath = System.IO.Path.Combine(saveDIR, filename);
        //                        employeeUpload.SaveAs(savePath);
        //                        productImage.Visible = true;
        //                        productImage.ImageUrl = System.IO.Path.Combine("/approvalProofs/", filename);
        //                        Session["imgPath"] = System.IO.Path.Combine("/approvalProofs/", filename);
        //                        Session["filename"] = filename;

        //                        Response.Write("<script>alert('Your file was uploaded successfully.')</script>");

        //                        // Log success message to the console
        //                        Console.WriteLine("File uploaded successfully: " + filename);
        //                        Console.WriteLine("Image path: " + Session["imgPath"]);

        //                        // Display contents after successful upload
        //                        displayContents();

        //                    }
        //                    else
        //                    {
        //                        Response.Write("<script>alert('Your file was not uploaded because the image size is more than 4MB.')</script>");
        //                        uploadBlock.Style["dipsplay"] = "block";
        //                    }
        //                }
        //                else
        //                {
        //                    Response.Write("<script>alert('Invalid File Upload. Please upload an image as proof of your travel approval.')</script>");
        //                    uploadBlock.Style["dipsplay"] = "block";

        //                }
        //            }
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Upload failed: No file selected.')</script>");
        //            uploadBlock.Style["dipsplay"] = "block";

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

        protected void btnUpload_Click(object sender, EventArgs e) //UPLOAD PDF
        {
            try
            {
                if (Session["userName"] != null)
                {
                    string employeeFirstName = Session["userName"].ToString();

                    // Concatenate the first and last name to create a unique folder name
                    string folderName = employeeFirstName;

                    // Create a directory path using the concatenated folder name
                    string saveDIR = System.IO.Path.Combine(Server.MapPath("/PDFs/TravelRequest/approvalProofs"), folderName);

                    // Check if the directory exists, if not, create it
                    if (!Directory.Exists(saveDIR))
                    {
                        Directory.CreateDirectory(saveDIR);
                    }

                    if (employeeUpload.HasFile)
                    {
                        string filename = Server.HtmlEncode(folderName + "_" + employeeUpload.FileName);
                        string extension = System.IO.Path.GetExtension(filename).ToLower(); // Convert extension to lowercase for comparison
                        int filesize = employeeUpload.PostedFile.ContentLength;

                       

                        //}
                        if (extension == ".pdf") // Allow only PDF files
                        {
                            if (filesize < 4100000)
                            {
                                string savePath = System.IO.Path.Combine(saveDIR, filename);

                                // Save the uploaded file
                                employeeUpload.SaveAs(savePath);

                                // Store file path in session
                                Session["pdfPath"] = System.IO.Path.Combine("/PDFs/TravelRequest/approvalProofs/", folderName, filename);
                                Session["filename"] = filename;

                                string pdfPath = Session["pdfPath"].ToString();

                                // Show the PDF viewer
                                pdfViewer.Attributes["src"] = pdfPath;
                                pdfBlock.Style["display"] = "block";

                                // Disable the RequiredFieldValidator
                                RequiredFieldValidator29.Enabled = false;
                                DisableRouteRequiredFieldValidators();

                                Response.Write("<script>alert('Your file was uploaded successfully.')</script>");
                                uploadBlock.Style["display"] = "none";

                                string path = Session["pdfPath"].ToString();
                                Session["filePath"] = path;

                                // Log success message to the console
                                Console.WriteLine("File uploaded successfully: " + filename);
                                Console.WriteLine("PDF path: " + Session["imgPath"]);

                                // Display contents after successful upload
                                displayContents();
                            }
                            else
                            {
                                Response.Write("<script>alert('Your file was not uploaded because the file size is more than 4MB.')</script>");
                                uploadBlock.Style["display"] = "block";
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid File Upload. Please upload a PDF file as proof of your travel approval.')</script>");
                            uploadBlock.Style["display"] = "block";
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Upload failed: No file selected.')</script>");
                        uploadBlock.Style["display"] = "block";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception to the console
                Console.WriteLine("Error uploading file: " + ex.Message);

                // Display error message in an alert box and in the console
                Response.Write("<script>alert('An error occurred while uploading the file. Please try again.')</script>");
                Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre>");
            }
        }

        protected void reviewRequest_Click(object sender, EventArgs e)
        {
            
        }
        private void displayContents()
        {
            // Check the selected option and update the visibility of input fields accordingly
            string selectedOption = flightOptions.SelectedItem.Value;

            if (selectedOption == "One Way")
            {
                oneWaynput.Style["display"] = "block";
                roundTripInput.Style["display"] = "none";
                multipleInput.Style["display"] = "none";
            }
            else if (selectedOption == "Roundtrip")
            {
                oneWaynput.Style["display"] = "none";
                roundTripInput.Style["display"] = "block";
                multipleInput.Style["display"] = "none";
            }
            else if (selectedOption == "multiple")
            {
                oneWaynput.Style["display"] = "none";
                roundTripInput.Style["display"] = "none";
                multipleInput.Style["display"] = "block";
                additionalFields.Style["display"] = "block";
            }

            string otherSelected = employeePurpose.SelectedValue;

            if (otherSelected == "Others")
            {
                Label14.Style["display"] = "block";
                otherspecified.Style["display"] = "block";
            }
        }

        private void insertRoute(string ID)
        {
            Random ranID = new Random();
            int random = ranID.Next(100000, 999999);


            try
            {

                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO route (routeID, routeTravelID, routeOFrom, routeOTo, routeODate, routeR1From, routeR1To, routeRdepart, routeRreturn, routeM1From, routeM1To, routeM1ToDate, routeM2From, routeM2To, routeM2ToDate, routeM3From, routeM3To, routeM3ToDate,  routeM4From, routeM4To, routeM4ToDate,  routeM5From, routeM5To, routeM5ToDate )"
                            + "VALUES (@ID, @routeTravelID, @onewayFrom, @onewayTo, @onewayDate, @round1From, @round1To, @departDate, @returnDate, @mul1From, @mul1To, @mul1ToDate,  @mul2From, @mul2To, @mul2ToDate,  @mul3From, @mul3To, @mul3ToDate,  @mul4From, @mul4To, @mul4ToDate,  @mul5From, @mul5To, @mul5ToDate )";

                        cmd.Parameters.AddWithValue("@ID", "R" + random);
                        cmd.Parameters.AddWithValue("@routeTravelID", ID);


                        //ONE WAY
                        cmd.Parameters.AddWithValue("@onewayFrom", onewayFrom.Text);
                        cmd.Parameters.AddWithValue("@onewayTo", onewayTo.Text);
                        cmd.Parameters.AddWithValue("@onewayDate", onewayDate.Text);

                        //ROUND TRIP
                        cmd.Parameters.AddWithValue("@round1From", round1From.Text);
                        cmd.Parameters.AddWithValue("@round1To", round1To.Text);
                        cmd.Parameters.AddWithValue("@departDate", roundDepart.Text);
                        cmd.Parameters.AddWithValue("@returnDate", roundReturn.Text);

                        //MULTIPLE
                        cmd.Parameters.AddWithValue("@mul1From", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@mul1To", TextBox8.Text);
                        cmd.Parameters.AddWithValue("@mul1ToDate", string.IsNullOrEmpty(TextBox12.Text) ? (object)DBNull.Value : TextBox12.Text);

                        cmd.Parameters.AddWithValue("@mul2From", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@mul2To", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mul2ToDate", string.IsNullOrEmpty(TextBox14.Text) ? (object)DBNull.Value : TextBox14.Text);

                        cmd.Parameters.AddWithValue("@mul3From", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@mul3To", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@mul3ToDate", string.IsNullOrEmpty(TextBox18.Text) ? (object)DBNull.Value : TextBox18.Text);

                        cmd.Parameters.AddWithValue("@mul4From", TextBox27.Text);
                        cmd.Parameters.AddWithValue("@mul4To", TextBox29.Text);
                        cmd.Parameters.AddWithValue("@mul4ToDate", string.IsNullOrEmpty(TextBox30.Text) ? (object)DBNull.Value : TextBox30.Text);

                        cmd.Parameters.AddWithValue("@mul5From", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@mul5To", TextBox21.Text);
                        cmd.Parameters.AddWithValue("@mul5ToDate", string.IsNullOrEmpty(TextBox22.Text) ? (object)DBNull.Value : TextBox22.Text);


                        // Execute the insertion into anotherTable
                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            //INSERT INTO TRAVELREQUEST TABLE - DATESUBMITTED
                            insertDateSubmitted(ID);
                        }
                        else
                        {
                            Response.Write("<script>alert('An error occurred for the route details. Please try again.')</script>");

                        }
                    }
                }

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

        private void insertDateSubmitted(string ID)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE travelRequest SET travelDateSubmitted = @dateSubmitted, travelDraftStat = @draftStat WHERE travelRequestID = @ID";

                        // Set parameters
                        cmd.Parameters.AddWithValue("@dateSubmitted", DateTime.Now); //date the request is submitted
                        cmd.Parameters.AddWithValue("@draftStat", "No");
                        cmd.Parameters.AddWithValue("@ID", ID);

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            //remove the session to get the new files uploaded incase same session different request
                            Session.Remove("filePath");
                            Session.Remove("filename");

                            // The update was successful
                            Response.Write("<script>alert ('Travel Request Submitted!'); window.location.href = 'EmployeeDashboard.aspx'; </script>");

                        }
                        else
                        {
                            // No rows were affected, meaning no matching travel request ID was found
                            Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during insertion of date submitted and draft state. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }
        }

        protected void saveAsDraft_Click(object sender, EventArgs e)
        {

            Random rand = new Random();
            int random = rand.Next(50, 999);


            try
            {

                if (Session["userID"] != null)
                {
                    // Session values are not null, proceed with inserting into the database
                    string filename = Session["filename"] != null ? Session["filename"].ToString() : null;
                    string imgPath = Session["pdfPath"] != null ? Session["pdfPath"].ToString() : null;
                    string userID = Session["userID"].ToString();

                    string levelText = employeeLevel.Text;
                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname, travelBdate, travelDU, travelEmail, travelLevel, travelMobilenum, travelProjectCode, travelPurpose, travelReqStatus, travelRemarks, travelType, travelOptions, travelUserID, travelProofname, travelProofPath, travelDateCreated, travelDraftStat)"
                                + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @empBdate, @empDu, @empEmail, @level, @mobile, @projCode, @purpose, @reqStatus, @remarks, @type, @options, @userID, @proofname, @proofpath, @created, @draftStat)";


                            string ID = "DR" + random + "DRAFT";
                            cmd.Parameters.AddWithValue("@ID", ID);

                            string location = homeFacility.Text;

                            // Check if the textbox with ID "othersFacility" is displayed
                            if (location == "Others")
                            {
                                // If displayed, use its value as @location
                                cmd.Parameters.AddWithValue("@location", othersFacility.Text);
                            }
                            else
                            {
                                // If hidden, use the selected item in the homeFacility dropdown
                                cmd.Parameters.AddWithValue("@location", homeFacility.SelectedItem.Text);
                            }

                            cmd.Parameters.AddWithValue("@empID", string.IsNullOrEmpty(employeeID.Text) ? DBNull.Value : (object)employeeID.Text);
                            cmd.Parameters.AddWithValue("@empFName", string.IsNullOrEmpty(employeeFName.Text) ? DBNull.Value : (object)employeeFName.Text);
                            cmd.Parameters.AddWithValue("@empMName", string.IsNullOrEmpty(employeeMName.Text) ? DBNull.Value : (object)employeeMName.Text);
                            cmd.Parameters.AddWithValue("@empLName", string.IsNullOrEmpty(employeeLName.Text) ? DBNull.Value : (object)employeeLName.Text);
                            cmd.Parameters.AddWithValue("@empBdate", string.IsNullOrEmpty(employeeBdate.Text) ? DBNull.Value : (object)employeeBdate.Text);
                            cmd.Parameters.AddWithValue("@empDu", string.IsNullOrEmpty(employeeDU.Text) ? DBNull.Value : (object)employeeDU.Text);
                            cmd.Parameters.AddWithValue("@empEmail", string.IsNullOrEmpty(employeeEmail.Text) ? DBNull.Value : (object)employeeEmail.Text);
                            cmd.Parameters.AddWithValue("@level", string.IsNullOrEmpty(employeeLevel.Text) ? DBNull.Value : (object)employeeLevel.Text);
                            cmd.Parameters.AddWithValue("@mobile", string.IsNullOrEmpty(employeePhone.Text) ? DBNull.Value : (object)employeePhone.Text);
                            cmd.Parameters.AddWithValue("@projCode", string.IsNullOrEmpty(employeeProjCode.Text) ? DBNull.Value : (object)employeeProjCode.Text);
                            string purpose = employeePurpose.Text;
                            if (purpose == "Others")
                            {
                                cmd.Parameters.AddWithValue("@purpose", otherspecified.Text);

                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);

                            }
                            cmd.Parameters.AddWithValue("@reqStatus", "Draft");
                            cmd.Parameters.AddWithValue("@remarks", string.IsNullOrEmpty(employeeRemarks.Text) ? DBNull.Value : (object)employeeRemarks.Text);
                            cmd.Parameters.AddWithValue("@type", "Domestic");
                            cmd.Parameters.AddWithValue("@options", flightOptions.SelectedItem == null ? DBNull.Value : (object)flightOptions.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@userID", string.IsNullOrEmpty(userID) ? DBNull.Value : (object)userID);
                            cmd.Parameters.AddWithValue("@proofname", string.IsNullOrEmpty(filename) ? DBNull.Value : (object)filename);
                            cmd.Parameters.AddWithValue("@proofpath", string.IsNullOrEmpty(imgPath) ? DBNull.Value : (object)imgPath);
                            cmd.Parameters.AddWithValue("@created", DateTime.Now); // Always add the current datetime
                            cmd.Parameters.AddWithValue("@draftStat", "Yes");

                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                insertDraftRoute(ID);
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
                Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }
        }
        protected void DisableAllRequiredFieldValidators()
        {
            RequiredFieldValidator30.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
            RequiredFieldValidator88.Enabled = false;
            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator20.Enabled = false;
            RequiredFieldValidator16.Enabled = false;
            RequiredFieldValidator3.Enabled = false;
            RequiredFieldValidator57.Enabled = false;
            RequiredFieldValidator25.Enabled = false;
            RequiredFieldValidator27.Enabled = false;
            RequiredFieldValidator29.Enabled = false;

            DisableRouteRequiredFieldValidators();
        }

        protected void DisableRouteRequiredFieldValidators()
        {
            // Enable validators associated with additional fields block
            RequiredFieldValidator50.Enabled = false;
            RequiredFieldValidator32.Enabled = false;
            RequiredFieldValidator9.Enabled = false;
            RequiredFieldValidator10.Enabled = false;
            RequiredFieldValidator11.Enabled = false;
            RequiredFieldValidator12.Enabled = false;
            RequiredFieldValidator14.Enabled = false;
            RequiredFieldValidator15.Enabled = false;
            RequiredFieldValidator19.Enabled = false;

            // Disable validators associated with one-way block
            RequiredFieldValidator13.Enabled = false;
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator6.Enabled = false;

            // Disable validators associated with roundtrip block
            RequiredFieldValidator17.Enabled = false;
            RequiredFieldValidator18.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
            RequiredFieldValidator7.Enabled = false;

            // Disable validators associated with multiple block
            RequiredFieldValidator21.Enabled = false;
            RequiredFieldValidator22.Enabled = false;
            RequiredFieldValidator26.Enabled = false;
            RequiredFieldValidator23.Enabled = false;
            RequiredFieldValidator24.Enabled = false;
            RequiredFieldValidator28.Enabled = false;

        }

        private void insertDraftRoute(string ID)
        {
            Random ranID = new Random();
            int random = ranID.Next(100000, 999999);


            try
            {

                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO route (routeID, routeTravelID, routeOFrom, routeOTo, routeODate, routeR1From, routeR1To, routeRdepart, routeRreturn, routeM1From, routeM1To, routeM1ToDate, routeM2From, routeM2To, routeM2ToDate, routeM3From, routeM3To, routeM3ToDate,  routeM4From, routeM4To, routeM4ToDate,  routeM5From, routeM5To, routeM5ToDate )"
                            + "VALUES (@ID, @routeTravelID, @onewayFrom, @onewayTo, @onewayDate, @round1From, @round1To, @departDate, @returnDate, @mul1From, @mul1To, @mul1ToDate,  @mul2From, @mul2To, @mul2ToDate,  @mul3From, @mul3To, @mul3ToDate,  @mul4From, @mul4To, @mul4ToDate,  @mul5From, @mul5To, @mul5ToDate )";

                        cmd.Parameters.AddWithValue("@ID", "R" + random);
                        cmd.Parameters.AddWithValue("@routeTravelID", ID);

                        if (oneWaynput.Style["display"] == "none")
                        {
                            //DISABLE THE VALIDATORS SINCE ONE WAY IS NOT CHOSEN
                            RequiredFieldValidator13.Enabled = false;
                            RequiredFieldValidator4.Enabled = false;
                            RequiredFieldValidator6.Enabled = false;
                        }
                        if (roundTripInput.Style["display"] == "none")
                        {
                            RequiredFieldValidator17.Enabled = false;
                            RequiredFieldValidator18.Enabled = false;
                            RequiredFieldValidator8.Enabled = false;
                            RequiredFieldValidator7.Enabled = false;
                        }
                        if (multipleInput.Style["display"] == "none")
                        {
                            RequiredFieldValidator21.Enabled = false;
                            RequiredFieldValidator22.Enabled = false;
                            RequiredFieldValidator26.Enabled = false;
                        }
                        if (additionalFields.Style["display"] == "none")
                        {
                            RequiredFieldValidator50.Enabled = false;
                            RequiredFieldValidator32.Enabled = false;
                            RequiredFieldValidator9.Enabled = false;
                            RequiredFieldValidator10.Enabled = false;
                            RequiredFieldValidator11.Enabled = false;
                            RequiredFieldValidator12.Enabled = false;
                            RequiredFieldValidator14.Enabled = false;
                            RequiredFieldValidator15.Enabled = false;
                            RequiredFieldValidator19.Enabled = false;

                        }

                        //ONE WAY
                        cmd.Parameters.AddWithValue("@onewayFrom", onewayFrom.Text);
                        cmd.Parameters.AddWithValue("@onewayTo", onewayTo.Text);
                        cmd.Parameters.AddWithValue("@onewayDate", onewayDate.Text);

                        //ROUND TRIP
                        cmd.Parameters.AddWithValue("@round1From", round1From.Text);
                        cmd.Parameters.AddWithValue("@round1To", round1To.Text);
                        cmd.Parameters.AddWithValue("@departDate", roundDepart.Text);
                        cmd.Parameters.AddWithValue("@returnDate", roundReturn.Text);

                        //MULTIPLE
                        cmd.Parameters.AddWithValue("@mul1From", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@mul1To", TextBox8.Text);
                        cmd.Parameters.AddWithValue("@mul1ToDate", string.IsNullOrEmpty(TextBox12.Text) ? (object)DBNull.Value : TextBox12.Text);

                        cmd.Parameters.AddWithValue("@mul2From", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@mul2To", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mul2ToDate", string.IsNullOrEmpty(TextBox14.Text) ? (object)DBNull.Value : TextBox14.Text);

                        cmd.Parameters.AddWithValue("@mul3From", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@mul3To", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@mul3ToDate", string.IsNullOrEmpty(TextBox18.Text) ? (object)DBNull.Value : TextBox18.Text);

                        cmd.Parameters.AddWithValue("@mul4From", TextBox27.Text);
                        cmd.Parameters.AddWithValue("@mul4To", TextBox29.Text);
                        cmd.Parameters.AddWithValue("@mul4ToDate", string.IsNullOrEmpty(TextBox30.Text) ? (object)DBNull.Value : TextBox30.Text);

                        cmd.Parameters.AddWithValue("@mul5From", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@mul5To", TextBox21.Text);
                        cmd.Parameters.AddWithValue("@mul5ToDate", string.IsNullOrEmpty(TextBox22.Text) ? (object)DBNull.Value : TextBox22.Text);


                        // Execute the insertion into anotherTable
                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Session.Remove("filePath");
                            Session.Remove("filename");
                            Response.Write("<script>alert ('Request Saved! You can access your DRAFTS in the Requests'); window.location.href = 'EmployeeDashboard.aspx'; </script>");

                        }
                        else
                        {
                            Response.Write("<script>alert('An error occurred for the route details. Please try again.')</script>");

                        }
                    }
                }

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
        private bool CheckKeywordsInPDF(string filePath)
        {
            using (PdfReader reader = new PdfReader(filePath))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string textContent = PdfTextExtractor.GetTextFromPage(reader, i);

                    // Define the regular expression pattern for case-insensitive matching of keywords
                    string pattern = @"\b(approve(d)?|approved)\b";

                    // Perform the regular expression match with case-insensitive option
                    if (Regex.IsMatch(textContent, pattern, RegexOptions.IgnoreCase))
                    {
                        return true; // Keywords found
                    }
                }
            }
            return false; // Keywords not found
        }
        protected void reUpload_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the file path from the session
                //string filePath = Session["pdfPath"] as string;
                string filePath = Session["filePath"].ToString();

                // Check if the file path is not null or empty
                if (!string.IsNullOrEmpty(filePath))
                {
                    // Delete the file from the server
                    File.Delete(Server.MapPath(filePath));

                    // Clear the session variables
                    Session.Remove("filePath");
                    Session.Remove("filename");

                    // Optionally, provide feedback to the user
                    Response.Write("<script>alert('File deleted. Please upload a new proof to proceed.')</script>");
                    uploadBlock.Style["display"] = "block";
                    pdfBlock.Style["display"] = "none";

                }
                else
                {
                    // If the file path is null or empty, display an error message
                    Response.Write("<script>alert('No file to delete.')</script>");
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during reupload of proof. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }
        }

    }
}