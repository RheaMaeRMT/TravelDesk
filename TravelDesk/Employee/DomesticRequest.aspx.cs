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


        }
        protected void submitRequestbtn_Click(object sender, EventArgs e)
        {

            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "TR" + random;


            try
            {

                if (Session["filename"] != null && Session["userID"] != null && Session["travelType"] != null)
                {
                    // Session values are not null, proceed with inserting into the database
                    string filename = Session["filename"].ToString();
                    string imgPath = Session["pdfPath"].ToString();
                    string userID = Session["userID"].ToString();
                    string type = Session["travelType"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname , travelEmail, travelLevel, travelMobilenum, travelProjectCode, travelFrom, travelDeparture, travelReturn, travelPurpose, travelReqStatus, travelManager, travelRemarks, travelTo, travelOthers, travelType, travelOptions, travelUserID, travelProofname, travelProofPath, travelDateCreated, travelDraftStat)"
                                + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @empEmail, @level, @mobile, @projCode, @from, @departure, @return, @purpose, @reqStatus, @manager, @remarks, @destination, @others, @type, @options, @userID, @proofname, @proofpath, @created, @draftStat)";

                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue("@location", homeFacility.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                            cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                            cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                            cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                            cmd.Parameters.AddWithValue("@empEmail", employeeEmail.Text);
                            cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                            cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                            cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                            cmd.Parameters.AddWithValue("@from", employeeFrom.Text);
                            cmd.Parameters.AddWithValue("@departure", employeeDepartureDate.Text);
                            cmd.Parameters.AddWithValue("@return", employeeArrivalDate.Text);
                            cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                            cmd.Parameters.AddWithValue("@reqStatus", "Approved");
                            cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                            cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                            cmd.Parameters.AddWithValue("@destination", employeeTo.Text);
                            cmd.Parameters.AddWithValue("@others", otherspecified.Text);
                            cmd.Parameters.AddWithValue("@type", type);
                            cmd.Parameters.AddWithValue("@options", flightOptions.SelectedItem.Text);
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
        private void ClearScreen()
        {
            homeFacility.Text = string.Empty;
            employeeID.Text = string.Empty;
            employeeFName.Text = string.Empty;
            employeeMName.Text = string.Empty;
            employeeLevel.Text = string.Empty;
            employeeLName.Text = string.Empty;
            employeePhone.Text = string.Empty;
            employeeProjCode.Text = string.Empty;
            employeeDepartureDate.Text = string.Empty;
            employeeFrom.Text = string.Empty;
            employeePurpose.Text = string.Empty;
            employeeApproval.ClearSelection();
            employeeManager.Text = string.Empty;
            employeeRemarks.Text = string.Empty;
            employeeTo.Text = string.Empty;
            otherspecified.Text = string.Empty;


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
            string saveDIR = Server.MapPath("/approvalProofs");
            try
            {
                if (Session["userName"] != null)
                {
                    string name = Session["userName"].ToString();

                    if (employeeUpload.HasFile)
                    {
                        string filename = Server.HtmlEncode(employeeUpload.FileName) + name;
                        string extension = System.IO.Path.GetExtension(filename).ToLower(); // Convert extension to lowercase for comparison
                        int filesize = employeeUpload.PostedFile.ContentLength;
                        if (File.Exists(System.IO.Path.Combine(saveDIR, filename)))
                        {
                            Response.Write("<script>alert('File already exists. Please upload a valid proof of approval for this travel request.')</script>");
                            uploadBlock.Style["display"] = "block";


                        }
                        else
                        {
                            if (extension == ".pdf") // Allow only PDF files
                            {
                                if (filesize < 4100000)
                                {
                                    string savePath = System.IO.Path.Combine(saveDIR, filename);

                                    // Save the uploaded file
                                    employeeUpload.SaveAs(savePath);

                                    // Store file path in session
                                    Session["pdfPath"] = System.IO.Path.Combine("/approvalProofs/", filename);
                                    Session["filename"] = filename;

                                    // Check if the uploaded PDF contains the required keywords
                                    if (CheckKeywordsInPDF(savePath))
                                    {
                                        // Show the PDF viewer
                                        pdfViewer.Attributes["src"] = Session["pdfPath"].ToString();
                                        pdfBlock.Style["display"] = "block";

                                        Response.Write("<script>alert('Your file was uploaded successfully.')</script>");

                                        string path = Session["pdfPath"].ToString();
                                        Session["filePath"] = path;
                                    }
                                    else
                                    {
                                        // Delete the invalid file
                                        File.Delete(savePath);

                                        Response.Write("<script>alert('It seems like your uploaded file is not valid. Please try again.')</script>");
                                        uploadBlock.Style["display"] = "block";
                                    }

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
                                uploadBlock.Style["dipsplay"] = "block";

                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Upload failed: No file selected.')</script>");
                        uploadBlock.Style["dipsplay"] = "block";

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
                        cmd.CommandText = "INSERT INTO route (routeID, routeTravelID, routeOFrom, routeOTo, routeR1From, routeR1To, routeR2From, routeR2To, routeM1From, routeM1To, routeM1FromDate, routeM1ToDate, routeM2From, routeM2To, routeM2FromDate, routeM2ToDate, routeM3From, routeM3To, routeM3FromDate, routeM3ToDate,  routeM4From, routeM4To, routeM4FromDate, routeM4ToDate,  routeM5From, routeM5To,  routeM5FromDate, routeM5ToDate )"
                            + "VALUES (@ID, @routeTravelID, @onewayFrom, @onewayTo, @round1From, @round1To, @round2From, @round2To, @mul1From, @mul1To, @mul1FromDate, @mul1ToDate,  @mul2From, @mul2To, @mul2FromDate, @mul2ToDate,  @mul3From, @mul3To, @mul3FromDate, @mul3ToDate,  @mul4From, @mul4To, @mul4FromDate, @mul4ToDate,  @mul5From, @mul5To, @mul5FromDate, @mul5ToDate )";

                        cmd.Parameters.AddWithValue("@ID", "R" + random);
                        cmd.Parameters.AddWithValue("@routeTravelID", ID);
                        cmd.Parameters.AddWithValue("@onewayFrom", onewayFrom.Text);
                        cmd.Parameters.AddWithValue("@onewayTo", onewayTo.Text);
                        cmd.Parameters.AddWithValue("@round1From", round1From.Text);
                        cmd.Parameters.AddWithValue("@round1To", round1To.Text);
                        cmd.Parameters.AddWithValue("@round2From", round2From.Text);
                        cmd.Parameters.AddWithValue("@round2To", round2To.Text);
                        cmd.Parameters.AddWithValue("@mul1From", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@mul1To", TextBox8.Text);
                        cmd.Parameters.AddWithValue("@mul1FromDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox11.Text);
                        cmd.Parameters.AddWithValue("@mul1ToDate", string.IsNullOrEmpty(TextBox12.Text) ? (object)DBNull.Value : TextBox12.Text);
                        cmd.Parameters.AddWithValue("@mul2From", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@mul2To", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mul2FromDate", string.IsNullOrEmpty(TextBox13.Text) ? (object)DBNull.Value : TextBox13.Text);
                        cmd.Parameters.AddWithValue("@mul2ToDate", string.IsNullOrEmpty(TextBox14.Text) ? (object)DBNull.Value : TextBox14.Text);
                        cmd.Parameters.AddWithValue("@mul3From", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@mul3To", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@mul3FromDate", string.IsNullOrEmpty(TextBox16.Text) ? (object)DBNull.Value : TextBox16.Text);
                        cmd.Parameters.AddWithValue("@mul3ToDate", string.IsNullOrEmpty(TextBox18.Text) ? (object)DBNull.Value : TextBox18.Text);
                        cmd.Parameters.AddWithValue("@mul4From", TextBox27.Text);
                        cmd.Parameters.AddWithValue("@mul4To", TextBox29.Text);
                        cmd.Parameters.AddWithValue("@mul4FromDate", string.IsNullOrEmpty(TextBox28.Text) ? (object)DBNull.Value : TextBox28.Text);
                        cmd.Parameters.AddWithValue("@mul4ToDate", string.IsNullOrEmpty(TextBox30.Text) ? (object)DBNull.Value : TextBox30.Text);
                        cmd.Parameters.AddWithValue("@mul5From", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@mul5To", TextBox21.Text);
                        cmd.Parameters.AddWithValue("@mul5FromDate", string.IsNullOrEmpty(TextBox20.Text) ? (object)DBNull.Value : TextBox20.Text);
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
            int random = rand.Next(100000, 999999);
            string ID = "TR" + random;


            try
            {

                if (Session["userID"] != null)
                {
                    // Session values are not null, proceed with inserting into the database
                    string filename = Session["filename"] != null ? Session["filename"].ToString() : null;
                    string imgPath = Session["pdfPath"] != null ? Session["pdfPath"].ToString() : null;
                    string userID = Session["userID"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname , travelLevel, travelMobilenum, travelProjectCode, travelFrom, travelDeparture, travelReturn, travelPurpose, travelReqStatus, travelManager, travelRemarks, travelTo, travelOthers, travelType, travelOptions, travelUserID, travelProofname, travelProofPath, travelDateCreated, travelDraftStat)"
                                + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @level, @mobile, @projCode, @from, @departure, @return, @purpose, @reqStatus, @manager, @remarks, @destination, @others, @type, @options, @userID, @proofname, @proofpath, @created, @draftStat)";

                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue("@location", homeFacility.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                            cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                            cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                            cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                            cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                            cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                            cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                            cmd.Parameters.AddWithValue("@from", employeeFrom.Text);
                            cmd.Parameters.AddWithValue("@departure", employeeDepartureDate.Text);
                            cmd.Parameters.AddWithValue("@return", employeeArrivalDate.Text);
                            cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                            cmd.Parameters.AddWithValue("@reqStatus", "Draft");
                            cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                            cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                            cmd.Parameters.AddWithValue("@destination", employeeTo.Text);
                            cmd.Parameters.AddWithValue("@others", otherspecified.Text);
                            cmd.Parameters.AddWithValue("@type", "Domestic");
                            cmd.Parameters.AddWithValue("@options", flightOptions.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@userID", userID);
                            cmd.Parameters.AddWithValue("@proofname", filename != null ? filename : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@proofpath", imgPath != null ? imgPath : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
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
                        cmd.CommandText = "INSERT INTO route (routeID, routeTravelID, routeOFrom, routeOTo, routeR1From, routeR1To, routeR2From, routeR2To, routeM1From, routeM1To, routeM1FromDate, routeM1ToDate, routeM2From, routeM2To, routeM2FromDate, routeM2ToDate, routeM3From, routeM3To, routeM3FromDate, routeM3ToDate,  routeM4From, routeM4To, routeM4FromDate, routeM4ToDate,  routeM5From, routeM5To,  routeM5FromDate, routeM5ToDate )"
                            + "VALUES (@ID, @routeTravelID, @onewayFrom, @onewayTo, @round1From, @round1To, @round2From, @round2To, @mul1From, @mul1To, @mul1FromDate, @mul1ToDate,  @mul2From, @mul2To, @mul2FromDate, @mul2ToDate,  @mul3From, @mul3To, @mul3FromDate, @mul3ToDate,  @mul4From, @mul4To, @mul4FromDate, @mul4ToDate,  @mul5From, @mul5To, @mul5FromDate, @mul5ToDate )";

                        cmd.Parameters.AddWithValue("@ID", "R" + random);
                        cmd.Parameters.AddWithValue("@routeTravelID", ID);
                        cmd.Parameters.AddWithValue("@onewayFrom", onewayFrom.Text);
                        cmd.Parameters.AddWithValue("@onewayTo", onewayTo.Text);
                        cmd.Parameters.AddWithValue("@round1From", round1From.Text);
                        cmd.Parameters.AddWithValue("@round1To", round1To.Text);
                        cmd.Parameters.AddWithValue("@round2From", round2From.Text);
                        cmd.Parameters.AddWithValue("@round2To", round2To.Text);
                        cmd.Parameters.AddWithValue("@mul1From", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@mul1To", TextBox8.Text);
                        cmd.Parameters.AddWithValue("@mul1FromDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox11.Text);
                        cmd.Parameters.AddWithValue("@mul1ToDate", string.IsNullOrEmpty(TextBox12.Text) ? (object)DBNull.Value : TextBox12.Text);
                        cmd.Parameters.AddWithValue("@mul2From", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@mul2To", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mul2FromDate", string.IsNullOrEmpty(TextBox13.Text) ? (object)DBNull.Value : TextBox13.Text);
                        cmd.Parameters.AddWithValue("@mul2ToDate", string.IsNullOrEmpty(TextBox14.Text) ? (object)DBNull.Value : TextBox14.Text);
                        cmd.Parameters.AddWithValue("@mul3From", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@mul3To", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@mul3FromDate", string.IsNullOrEmpty(TextBox16.Text) ? (object)DBNull.Value : TextBox16.Text);
                        cmd.Parameters.AddWithValue("@mul3ToDate", string.IsNullOrEmpty(TextBox18.Text) ? (object)DBNull.Value : TextBox18.Text);
                        cmd.Parameters.AddWithValue("@mul4From", TextBox27.Text);
                        cmd.Parameters.AddWithValue("@mul4To", TextBox29.Text);
                        cmd.Parameters.AddWithValue("@mul4FromDate", string.IsNullOrEmpty(TextBox28.Text) ? (object)DBNull.Value : TextBox28.Text);
                        cmd.Parameters.AddWithValue("@mul4ToDate", string.IsNullOrEmpty(TextBox30.Text) ? (object)DBNull.Value : TextBox30.Text);
                        cmd.Parameters.AddWithValue("@mul5From", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@mul5To", TextBox21.Text);
                        cmd.Parameters.AddWithValue("@mul5FromDate", string.IsNullOrEmpty(TextBox20.Text) ? (object)DBNull.Value : TextBox20.Text);
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