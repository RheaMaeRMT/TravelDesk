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
using System.Web.UI.WebControls;

namespace TravelDesk.Employee
{
    public partial class InternationalRequest : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

            }
            //else if (Session["userID"] != null && (Session["userName"] != null))
            //{
  
            //}              


        }
        protected void submit_Click(object sender, EventArgs e)
        {
            // Add message to the Output window in Visual Studio
            System.Diagnostics.Debug.WriteLine("Submit button clicked.");

            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "TR" + random;

            Session["currentID"] = ID;
            try
            {


                using (var db = new SqlConnection(connectionString))
                {

                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelLocation, travelEmpID, travelFname, travelMName, travelLName, travelLevel, travelMobilenum, travelProjectCode, travelHomeFacility, travelDeparture, travelReturn, travelPurpose, travelApprovalStat, travelManager, travelRemarks, travelDestination, travelOthers, travelType, travelOptions, travelUserID, travelProofname, travelProofPath)"
                            + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @level, @mobile, @projCode, @facility, @departure, @return, @purpose, @approvalStat, @manager, @remarks, @destination, @others, @type, @options, @userID, @proofname, @proofpath)";

                        string approvalStat = (employeeApproval.SelectedItem.Text == "YES") ? "auto-approved" : "Pending Approval";
                        string filename = Session["filename"].ToString();
                        string imgPath = Session["imgPath"].ToString();

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@location", employeeLocation.Text);
                        cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                        cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                        cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                        cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                        cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                        cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                        cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                        cmd.Parameters.AddWithValue("@facility", employeeFacility.Text);
                        cmd.Parameters.AddWithValue("@departure", employeeDeparture.Text);
                        cmd.Parameters.AddWithValue("@return", employeeReturn.Text);
                        cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                        cmd.Parameters.AddWithValue("@approvalStat", approvalStat);
                        cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                        cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                        cmd.Parameters.AddWithValue("@destination", employeeDestination.Text);
                        cmd.Parameters.AddWithValue("@others", otherspecified.Text);
                        cmd.Parameters.AddWithValue("@type", "International");
                        cmd.Parameters.AddWithValue("@options", flightOptions.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                        cmd.Parameters.AddWithValue("@proofname", filename);
                        cmd.Parameters.AddWithValue("@proofpath", imgPath);



                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert('Checking your uploaded file')</script>");
                            insertRoute(ID);

                        }
                        else
                        {
                            Response.Write("<script>alert('An error occurred. Please try again.')</script>");

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

        //insert route details in the table
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
                        cmd.Parameters.AddWithValue("@mul1ToDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox12.Text);
                        cmd.Parameters.AddWithValue("@mul2From", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@mul2To", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mul2FromDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox13.Text);
                        cmd.Parameters.AddWithValue("@mul2ToDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox14.Text);
                        cmd.Parameters.AddWithValue("@mul3From", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@mul3To", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@mul3FromDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox16.Text);
                        cmd.Parameters.AddWithValue("@mul3ToDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox18.Text);
                        cmd.Parameters.AddWithValue("@mul4From", TextBox27.Text);
                        cmd.Parameters.AddWithValue("@mul4To", TextBox29.Text);
                        cmd.Parameters.AddWithValue("@mul4FromDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox28.Text);
                        cmd.Parameters.AddWithValue("@mul4ToDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox30.Text);
                        cmd.Parameters.AddWithValue("@mul5From", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@mul5To", TextBox21.Text);
                        cmd.Parameters.AddWithValue("@mul5FromDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox20.Text);
                        cmd.Parameters.AddWithValue("@mul5ToDate", string.IsNullOrEmpty(TextBox11.Text) ? (object)DBNull.Value : TextBox22.Text);


                        // Execute the insertion into anotherTable
                       var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert ('International Travel Request Submitted!'); window.location.href = 'ListofRequests.aspx'; </script>");

                            //checkPdfFile();
                           

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
        private void ClearScreen()
        {
            employeeLocation.Text = string.Empty;
           employeeID.Text = string.Empty;
            employeeFName.Text = string.Empty;
            employeeMName.Text = string.Empty;
            employeeLName.Text = string.Empty;
            employeeLevel.Text = string.Empty;
            employeePhone.Text = string.Empty;
            employeeProjCode.Text = string.Empty;
            employeeFacility.Text = string.Empty;
            employeeDeparture.Text = string.Empty;
            employeeReturn.Text = string.Empty;
            employeePurpose.Text = string.Empty;
            employeeApproval.ClearSelection();
            employeeManager.Text = string.Empty;
            employeeRemarks.Text = string.Empty;
            employeeDestination.Text = string.Empty;
            otherspecified.Text = string.Empty;
            flightOptions.ClearSelection();
           onewayFrom.Text = string.Empty;
            onewayTo.Text = string.Empty;
            round1From.Text = string.Empty;
            round1To.Text = string.Empty;
            round2From.Text = string.Empty;
            round2To.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox8.Text = string.Empty;
            TextBox11.Text = string.Empty;
            TextBox12.Text = string.Empty;
            TextBox9.Text = string.Empty;
            TextBox10.Text = string.Empty;
            TextBox13.Text = string.Empty;
            TextBox14.Text = string.Empty;
            TextBox15.Text = string.Empty;
            TextBox17.Text = string.Empty;
            TextBox16.Text = string.Empty;
            TextBox18.Text = string.Empty;
            TextBox27.Text = string.Empty;
            TextBox29.Text = string.Empty;
            TextBox28.Text = string.Empty;
            TextBox30.Text = string.Empty;
            TextBox19.Text = string.Empty;
            TextBox21.Text = string.Empty;
            TextBox20.Text = string.Empty;
            TextBox22.Text = string.Empty;


        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{

        //    string saveDIR = Server.MapPath("/approvalProofs");

        //    try
        //    {
        //        if (employeeUpload.HasFile)
        //        {
        //            string filename = Server.HtmlEncode(employeeUpload.FileName);
        //            string extension = System.IO.Path.GetExtension(filename).ToLower();
        //            int filesize = employeeUpload.PostedFile.ContentLength;

        //            if (extension == ".pdf")
        //            {
        //                if (filesize < 5100000)
        //                {
        //                    string savePath = System.IO.Path.Combine(saveDIR, filename);
        //                    employeeUpload.SaveAs(savePath);

        //                    // Store file path in session
        //                    Session["pdfPath"] = System.IO.Path.Combine("/approvalProofs/", filename);
        //                    Session["filename"] = filename;

        //                    //// Set the source of the iframe to the PDF file path
        //                    //pdfViewer.Attributes["src"] = Session["pdfPath"].ToString();

        //                    //// Show the PDF viewer
        //                    //pdfViewer.Style["display"] = "block";

        //                    Response.Write("<script>alert('Your file was uploaded successfully.')</script>");
        //                }
        //                else
        //                {
        //                    Response.Write("<script>alert('Your file was not uploaded because its size is more than 5MB.')</script>");
        //                }
        //            }
        //            else
        //            {
        //                Response.Write("<script>alert('Invalid File Upload. Please upload a PDF file as a proof of your travel approval.')</script>");
        //            }

        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Upload Failed: Try again')</script>");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre><script>alert('" + ex.Message + "');</script>");
        //    }
        //}
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string saveDIR = Server.MapPath("/approvalProofs");
            try
            {
                if (employeeUpload.HasFile)
                {
                    string filename = Server.HtmlEncode(employeeUpload.FileName);
                    string extension = System.IO.Path.GetExtension(filename);
                    int filesize = employeeUpload.PostedFile.ContentLength;
                    if (File.Exists(System.IO.Path.Combine(saveDIR, filename)))
                    {
                        uploadStatus.InnerText = "File already exist";
                    }
                    else
                    {
                        if ((extension == ".jpg") || (extension == ".jpeg") || (extension == ".png") || (extension == ".JPG") || (extension == ".JPEG") || (extension == ".PNG"))
                        {
                            if (filesize < 4100000)
                            {
                                string savePath = System.IO.Path.Combine(saveDIR, filename);
                                employeeUpload.SaveAs(savePath);
                                productImage.Visible = true;
                                productImage.ImageUrl = System.IO.Path.Combine("/approvalProofs/", filename);
                                Session["imgPath"] = System.IO.Path.Combine("/approvalProofs/", filename);
                                Session["filename"] = filename;


                                Response.Write("<script>alert('Your file was uploaded successfully.')</script>");

                                // Write session values to the console
                                Console.WriteLine("imgPath: " + Session["imgPath"]);
                                Console.WriteLine("filename: " + Session["filename"]);
                            }
                            else
                            {
                                Response.Write("<script>alert('Your file was not uploaded because image size is more than 4MB')</script>");

                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid File Upload. Please upload an image as a proof of your travel approval')</script>");
                        }
                    }
                }
                else
                {
                    uploadStatus.InnerText = "Upload Failed: Try again";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre><script>alert('" + ex.Message + "');</script>");
            }

        }

        private void checkPdfFile()
        {
            string currentPDFPath = Session["pdfPath"]?.ToString();
            string ID = Session["currentID"]?.ToString();

            if (!string.IsNullOrEmpty(currentPDFPath) && !string.IsNullOrEmpty(ID))
            {
                try
                {
                    // Open the PDF file
                    PdfReader reader = new PdfReader(currentPDFPath);

                    // Extract text from each page of the PDF
                    string extractedText = "";
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        extractedText += PdfTextExtractor.GetTextFromPage(reader, i);
                    }

                    // Close the PDF reader
                    reader.Close();

                    // Search for the words "approved" and "legible" using regular expressions
                    bool containsApproved = Regex.IsMatch(extractedText, @"\bapproved\b", RegexOptions.IgnoreCase);
                    bool containsLegible = Regex.IsMatch(extractedText, @"\blegible\b", RegexOptions.IgnoreCase);

                    // Check if the PDF contains the words
                    if (containsApproved || containsLegible)
                    {
                        Response.Write("<script>alert ('International Travel Request Submitted!'); window.location.href = 'ListofRequests.aspx'; </script>");

                    }
                    else
                    {
                        Response.Write("<script>alert('The file you uploaded is not valid. Travel Request not submitted. Please upload your Valid Managers Approval and try again.')</script>");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Response.Write("<script>alert('An error occurred while processing the PDF file: " + ex.Message + "')</script>");
                }
            }
            else
            {
                // Handle case where session variable is empty or null
                Console.WriteLine("No PDF file uploaded!");
            }
        }

        protected void draftButton_Click(object sender, EventArgs e)
        {

        }
        // Recursive method to find a control by ID
        ////public Control FindControlRecursive(Control control, string id)
        ////{
        ////    if (control.ID == id)
        ////    {
        ////        return control;
        ////    }

        ////    foreach (Control childControl in control.Controls)
        ////    {
        ////        Control foundControl = FindControlRecursive(childControl, id);
        ////        if (foundControl != null)
        ////        {
        ////            return foundControl;
        ////        }
        ////    }

        ////    return null;
        ////}

    }
}