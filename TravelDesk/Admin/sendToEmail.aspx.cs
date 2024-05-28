using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using Microsoft.Graph.Models;

namespace TravelDesk.Admin
{

    public partial class sendToEmail : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script> window.location.href = '../LoginPage.aspx'; </script>");
            }
            else
            {

                string email = Session["userEmail"].ToString();
                travellerEmail.Text = email;
                string message = @"
                            Dear Traveler,

                            Attached are the following:
                            1. Travel Advance Form (TAF)
                            2. E-ticket receipt
                            3. Hotel confirmation
                            4. Summary of travel arrangements
                            For per diem processing, kindly fill out and submit signed TAF and approved request (through Acumatica) to Ms. Emelina Hortel.

                            Per diem: P3,200 x 2.5 days = P

                            The per diem is now inclusive of the accommodation and roundtrip airport transportation allowances.

                            You may now choose, book and pay for your preferred accommodation. You may still book with our accredited hotels or from online booking platforms such as Airbnb, Agoda, etc.

                            You may want to check available unit at Entrata Tower 1, same building where Manila office is located, through the online booking platforms.

                            Manila office address: Units 1617 and 1618, Entrata Tower 1 Condominium, 2609 Civic Drive, Filinvest Corporate City, Muntinlupa City 1781

                            Please refer to the summary of travel arrangements and let me know if I missed anything.

                            You may check status of your flight at https://www.cebupacificair.com/flight-status

                            You may check status of your flight at https://www.philippineairlines.com/en/ph/home#flight-status-tab

                            You may check status of your flight at https://www.airasia.com/flightstatus/en/GB
                            ** Changes are not allowed once checked-in or if Air Asia flight is within 48hrs

                            **REMINDERS**

                            - Effective April 2022, only one baggage piece per 20kg will be allowed for check-in. You may purchase maximum of 2 pieces per weight option
                            - Only one hand carry bag is allowed per person, but you may bring an extra if it's a laptop bag, food that cannot be checked in, or a small bag that can fit under the seat.
                            - Web and mobile check-in
                                 Cebu Pacific: from 7 days up to 4 hours before your flight
                                 Philippine Airlines: 24hrs to 1hr before your flight
                                 Air Asia: 48hrs to 30min before your flight
                            - Go to the Bag Drop Counter before counter closure to check-in your bags.
                            - Please be at the airport at least two hours prior to departure
                            - Liquidation must be submitted to Stanly Ortiz within seven working days from the date of arrival to the home facility. Please provide the liquidation form, boarding pass and TAF.
 
                            Please do not hesitate to reach out to me if you have any questions or require further information.";

                emailMessage.Text = message;
            }

        }


        //private void DownloadPdf()
        //{
        //    // Retrieve PDF bytes from session
        //    byte[] pdfBytes = (byte[])Session["pdfBytes"];

        //    // Retrieve ID and name for filename
        //    string ID = Session["clickedRequest"].ToString();
        //    string name = Session["travellerName"].ToString();
        //    string filename = name + "_" + ID + ".pdf";

        //    // Set content type and header for file download
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);

        //    // Write the PDF bytes to the response
        //    Response.BinaryWrite(pdfBytes);

        //    // End the response
        //    Response.End();
        //}

        private void loadDetailsForEmail()
        {

            if (Session["travellerName"] != null && Session["userEmail"] != null)
            {
                // Retrieve the dynamically generated file paths
                string[] filePaths = GetUploadedFilePaths();
                // Pass the file paths to the client-side JavaScript function
                string receiverEmail = Session["userEmail"].ToString(); // Example email address, replace with actual dynamic value
                string message = emailMessage.Text; // Example message content, replace with actual dynamic value

                string name = Session["travellerName"].ToString();
                string script = $"<script>sendEmail('{receiverEmail}', '{message}','{name}', {JsonConvert.SerializeObject(filePaths)});</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SendEmailScript", script);

            }
            else
            {
                Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

            }

        }

        // Method to retrieve dynamically generated file paths
        private string[] GetUploadedFilePaths()
        {
            List<string> filePaths = new List<string>();

            if (Session["travellerName"] != null)
            {
                string empFname = Session["travellerName"].ToString();
                string subFolder = "otherFiles";
                string folderPath1 = Path.Combine(Server.MapPath("/PDFs/travelArrangements"), empFname, subFolder);
                string folderPath2 = Path.Combine(Server.MapPath("/PDFs/travelArrangements"), empFname);

                if (Directory.Exists(folderPath1))
                {
                    // Get all PDF files in the first folder
                    string[] pdfFiles1 = Directory.GetFiles(folderPath1, "*.pdf");
                    filePaths.AddRange(pdfFiles1);
                }

                if (Directory.Exists(folderPath2))
                {
                    // Get all PDF files in the second folder
                    string[] pdfFiles2 = Directory.GetFiles(folderPath2, "*.pdf");
                    filePaths.AddRange(pdfFiles2);
                }
            }
            else
            {
                // Session expired
                Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");
            }

            return filePaths.ToArray();
        }
        protected void uploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (attachments.HasFile)
                {
                    if (Session["travellerName"] != null)
                    {
                        string empFname = Session["travellerName"].ToString();
                        string subFolder = "otherFiles";
                        string path = Path.Combine(empFname, subFolder);
                        string folderPath = Server.MapPath("/PDFs/travelArrangements/" + empFname + "/" + "otherFiles");


                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        HttpFileCollection attachmentsCollection = Request.Files;

                        for (int i = 0; i < attachmentsCollection.Count; i++)
                        {
                            HttpPostedFile attachment = attachmentsCollection[i];

                            if (attachment.ContentLength > 0)
                            {
                                string filename = Server.HtmlEncode(empFname + "_" + Path.GetFileName(attachment.FileName));
                                string extension = Path.GetExtension(filename).ToLower();

                                if (extension == ".pdf")
                                {
                                    if (attachment.ContentLength < 4100000)
                                    {
                                        string savePath = Path.Combine(folderPath, filename);
                                        attachment.SaveAs(savePath);
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('File " + filename + " was not uploaded because the file size is more than 4MB.')</script>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid File Upload. Please upload a PDF file.')</script>");
                                }
                            }
                        }

                        ListUploadedFiles(folderPath); // Call the method to list the filenames
                        Response.Write("<script>alert('File uploaded successfully.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('No files inserted. Please attach the PDF file you want to upload.')</script>");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading file: " + ex.Message);
                Response.Write("<script>alert('An error occurred while uploading the file. Please try again.')</script>");
                Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre>");
            }
        }

        //private void DisplayPDFs(string folderPath)
        //{
        //    attached.Style["display"] = "block";
        //    deleteFiles.Style["display"] = "block";
        //    deleteFiles.Style["Width"] = "100px";
        //    attached.Style["Width"] = "100px";

        //    // Get all PDF files in the folder
        //    string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        //    string empFname = Session["travellerName"].ToString();

        //    // Create HTML to display PDFs in iframes
        //    StringBuilder html = new StringBuilder();
        //    foreach (string pdfFile in pdfFiles)
        //    {
        //        FileInfo fileInfo = new FileInfo(pdfFile);
        //        // Check if the file was created or modified today
        //        if (fileInfo.CreationTime.Date == DateTime.Today || fileInfo.LastWriteTime.Date == DateTime.Today)
        //        {
        //            string fileName = Path.GetFileName(pdfFile);
        //            string pdfPath = "/PDFs/travelArrangements/" + empFname + "/" + "otherFiles" + "/" + fileName;
        //            html.Append("<iframe src='" + pdfPath + "' style='width:50%; height:600px;'></iframe>");
        //        }
        //    }

        //    // Display the HTML content in a placeholder or another container on your page
        //    pdfPlaceholder.Controls.Add(new LiteralControl(html.ToString()));
        //}
        protected void ListUploadedFiles(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string[] fileEntries = Directory.GetFiles(folderPath, "*.pdf");
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");

                foreach (string filePath in fileEntries)
                {
                    string fileName = Path.GetFileName(filePath);
                    sb.AppendFormat("<li>{0} <button type='button' onclick=\"deleteFile('{1}')\">X</button></li>", fileName, filePath);
                }

                sb.Append("</ul>");
                fileListPlaceholder.Controls.Add(new Literal { Text = sb.ToString() });
            }
        }
        protected void DeleteFileButton_Click(object sender, EventArgs e)
        {
            string filePath = hiddenFilePath.Value;
            DeleteFile(filePath);

            // Refresh the file list after deletion
            string empFname = Session["travellerName"].ToString();
            string subFolder = "otherFiles";
            string path = Path.Combine(empFname, subFolder);
            string folderPath = Path.Combine(Server.MapPath("/PDFs/travelArrangements/"), path);
            ListUploadedFiles(folderPath);
        }


        protected void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Response.Write("<script>alert('File deleted successfully.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('File not found.')</script>");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting file: " + ex.Message);
                Response.Write("<script>alert('An error occurred while deleting the file. Please try again.')</script>");
                Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre>");
            }
        }


        protected void sendEmail_Click(object sender, EventArgs e)
        {
            loadDetailsForEmail();

        }



    }
}