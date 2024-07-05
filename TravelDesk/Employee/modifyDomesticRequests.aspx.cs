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
    public partial class modifyDomesticRequests : System.Web.UI.Page
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
                displayRequestDetails();
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

        protected void uploadNew_Click(object sender, EventArgs e)
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
                                Session["imgPath"] = path;



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

        protected void saveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        // If a new image is uploaded, the SQL command includes the proof path parameter(@proofpath)
                        if (!string.IsNullOrEmpty(Session["imgPath"]?.ToString()))
                        {
                            cmd.CommandType = CommandType.Text;
                            // Construct the SQL update command excluding the travelRequestID
                            cmd.CommandText = "UPDATE travelRequest SET "
                                + "travelHomeFacility = @location, "
                                + "travelFname = @empFName, "
                                + "travelMname = @empMName, "
                                + "travelLname = @empLName, "
                                + "travelBdate = @empBdate, "
                                + "travelDU = @empDu, "
                                + "travelEmail = @empEmail, "
                                + "travelLevel = @level, "
                                + "travelMobilenum = @mobile, "
                                + "travelProjectCode = @projCode, "
                                + "travelPurpose = @purpose, "
                                + "travelReqStatus = @reqStatus, "
                                + "travelRemarks = @remarks, "
                                + "travelType = @type, "
                                + "travelOptions = @options, "
                                + "travelUserID = @userID, "
                                + "travelProofname = @proofname, "
                                + "travelProofPath = @proofpath, "
                                + "travelDateCreated = @created, "
                                + "travelDraftStat = @draftStat "
                                + "WHERE travelEmpID = @empID";


                            string clickedRequest = Session["clickedRequest"]?.ToString(); // Null-conditional operator added
                            string filename = Session["filename"].ToString();
                            string imgPath = Session["imgPath"].ToString();

                            // Add parameters excluding the travelRequestID
                            cmd.Parameters.AddWithValue("@location", homeFacility.Text);
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
                            cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                            cmd.Parameters.AddWithValue("@proofname", filename);
                            cmd.Parameters.AddWithValue("@proofpath", imgPath);
                            cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
                            cmd.Parameters.AddWithValue("@draftStat", "No");
                            cmd.Parameters.AddWithValue("@empID", clickedRequest);

                            // Execute the update command
                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                Response.Write("<script>alert('Domestic Travel Request Updated!');</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('An error occurred. Please try again.');</script>");
                            }
                        }
                        else
                        {
                            cmd.CommandType = CommandType.Text;
                            // Construct the SQL update command excluding the travelRequestID
                            cmd.CommandText = "UPDATE travelRequest SET "
                                + "travelHomeFacility = @location, "
                                + "travelFname = @empFName, "
                                + "travelMname = @empMName, "
                                + "travelLname = @empLName, "
                                + "travelBdate = @empBdate, "
                                + "travelDU = @empDu, "
                                + "travelEmail = @empEmail, "
                                + "travelLevel = @level, "
                                + "travelMobilenum = @mobile, "
                                + "travelProjectCode = @projCode, "
                                + "travelPurpose = @purpose, "
                                + "travelReqStatus = @reqStatus, "
                                + "travelRemarks = @remarks, "
                                + "travelType = @type, "
                                + "travelOptions = @options, "
                                + "travelUserID = @userID, "
                                + "travelProofname = @proofname, "
                                + "travelProofPath = @proofpath, "
                                + "travelDateCreated = @created, "
                                + "travelDraftStat = @draftStat "
                                + "WHERE travelRequestID = @empID";


                            string clickedRequest = Session["clickedRequest"]?.ToString(); // Null-conditional operator added
                            string filename = Session["filename"].ToString();
                            string imgPath = Session["imgPath"].ToString();

                            // Add parameters excluding the travelRequestID
                            cmd.Parameters.AddWithValue("@location", homeFacility.Text);
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
                            cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                            cmd.Parameters.AddWithValue("@proofname", filename);
                            cmd.Parameters.AddWithValue("@proofpath", imgPath);
                            cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
                            cmd.Parameters.AddWithValue("@draftStat", "No");
                            cmd.Parameters.AddWithValue("@empID", clickedRequest);


                            // Execute the update command
                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                Response.Write("<script>alert('Domestic Travel Request Updated!');</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('An error occurred. Please try again.');</script>");
                            }
                        }

                    }
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



    }
}