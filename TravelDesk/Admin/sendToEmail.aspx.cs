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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.Text.RegularExpressions;
using System.Data;

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

            }
            if (!IsPostBack)
            {
                // Set the default message in the emailMessage TextBox
                string defaultMessage = @"Attached are the following:

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
- Liquidation must be submitted to Stanly Ortiz within seven working days from the date of arrival to the home facility. Please provide the liquidation form, boarding pass and TAF.";

                emailMessage.Text = defaultMessage;

                // Store the default message in a session variable
                Session["DefaultMessage"] = defaultMessage;
            }
        }


        private void loadDetailsForEmail()
        {
            if (Session["travellerName"] != null && Session["userEmail"] != null)
            {
                // Retrieve the current value of the emailMessage TextBox
                string message = emailMessage.Text;
                // Retrieve the default message from the session variable
                string defaultMessage = Session["DefaultMessage"].ToString();

                // Compare the current message with the default message to see if it has been edited
                if (message != defaultMessage)
                {
                    // If the message has been edited, use the edited message
                    message = HttpUtility.JavaScriptStringEncode(message);
                }
                else
                {
                    // Use the default message
                    message = HttpUtility.JavaScriptStringEncode(defaultMessage);
                }

                string receiverEmail = Session["userEmail"].ToString();
                string name = Session["travellerName"].ToString();

                // Retrieve drive links from session
                List<string> driveLinks = (List<string>)Session["UploadedDriveLinks"] ?? new List<string>();

                // Assign drive links to the LINKS list
                LINKS.AddRange(driveLinks);

                // Ensure the formatted links are encoded to be used in JavaScript
                string formattedLinks = driveLinks.Count > 0 ? HttpUtility.JavaScriptStringEncode(GetFormattedLinks(driveLinks)) : "";

                // Pass the email details including the uploaded file links to the JavaScript function
                string script = $"<script>sendEmailWithDriveLinks('{receiverEmail}', '{message}', '{name}', '{formattedLinks}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SendEmailScript", script);


            }
            Session.Remove("UploadedDriveLinks");
        }

        private string GetFormattedLinks(List<string> driveLinks)
        {
            StringBuilder formattedLinks = new StringBuilder();

            for (int i = 0; i < driveLinks.Count; i++)
            {
                string link = driveLinks[i];
                string fileName = GetFileNameFromLink(link);
                formattedLinks.AppendLine($"{i + 1}. {link}");
            }

            return formattedLinks.ToString();
        }

        private string GetFileNameFromLink(string link)
        {
            Uri uri = new Uri(link);
            string[] segments = uri.Segments;
            string fileName = segments[segments.Length - 1];

            if (fileName.Contains("?"))
            {
                fileName = fileName.Substring(0, fileName.IndexOf("?"));
            }

            return fileName;
        }


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

                    sb.AppendFormat("<li>{0} <button type='button' onclick=\"deleteFile('{1}')\">X</button></li>", fileName, filePath.Replace("\\", "\\\\"));

                }
                sb.Append("</ul>");
                fileListPlaceholder.Controls.Add(new Literal { Text = sb.ToString() });
            }
        }

        private List<string> LINKS = new List<string>();

        private List<string> UploadFilesToGoogleDrive(string folderPath)
        {
            List<string> driveLinks = new List<string>();

            try
            {
                // Path to the service account JSON key file
                var serviceAccountCredentialFilePath = @"C:\Users\HR-OJT\source\repos\InnodataTravelDesk\TravelDesk\App_Data\service-account.json";

                // Authenticate with the service account
                GoogleCredential credential;
                using (var stream = new FileStream(serviceAccountCredentialFilePath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(DriveService.Scope.DriveFile);
                }

                // Create the Drive service
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "TravelDesk",
                });

                // ID of the parent folder to upload to
                string parentFolderId = "1QAIZ0j7KeaUaB0zbT1Pm9Qb2ITGCh1ti"; // Replace with your parent folder ID

                // Get the subfolder name from the session
                string subfolderName = Session["travellerName"].ToString();

                // Ensure the subfolder exists (create if necessary)
                string subfolderId = CreateSubfolderIfNotExists(service, parentFolderId, subfolderName);

                // Get all PDF files from the specified folder
                string[] fileEntries = Directory.GetFiles(folderPath, "*.pdf");

                foreach (string filePath in fileEntries)
                {
                    var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = Path.GetFileName(filePath),
                        Parents = new List<string> { subfolderId } // Ensure the file is uploaded to the subfolder
                    };

                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        var request = service.Files.Create(fileMetadata, fileStream, MimeMapping.GetMimeMapping(filePath));
                        request.Fields = "id, webContentLink"; // Request the ID and web content link of the uploaded file
                        request.Upload();

                        var file = request.ResponseBody;

                        // Log file upload success
                        Console.WriteLine("File uploaded successfully to Google Drive. File ID: " + file.Id);

                        // Construct the Google Drive link
                        string driveLink = file.WebContentLink;
                        Console.WriteLine("Google Drive link: " + driveLink);

                        driveLinks.Add(driveLink);
                        LINKS.Add(driveLink);
                    }
                }

                // Log the current list of links
                Console.WriteLine("Current LINKS: " + string.Join(", ", LINKS));

                // Return the list of Google Drive links
                return driveLinks;
            }
            catch (Google.GoogleApiException gEx)
            {
                // Log detailed Google API error
                Console.WriteLine("Google API Error uploading file to Google Drive: " + gEx.Message);
                Console.WriteLine("Google API Error Details: " + gEx.Error);
                return null;
            }
            catch (Exception ex)
            {
                // Log detailed error
                Console.WriteLine("Error uploading file to Google Drive: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return null;
            }
        }

        // Custom class to simulate HttpPostedFile
        public class SimulatedHttpPostedFile : HttpPostedFileBase
        {
            private string fileName;
            private string contentType;
            private Stream fileContent;

            public SimulatedHttpPostedFile(string fileName, string contentType, Stream fileContent)
            {
                this.fileName = fileName;
                this.contentType = contentType;
                this.fileContent = fileContent;
            }

            public override int ContentLength
            {
                get { return (int)fileContent.Length; }
            }

            public override string FileName
            {
                get { return fileName; }
            }

            public override string ContentType
            {
                get { return contentType; }
            }

            public override Stream InputStream
            {
                get { return fileContent; }
            }
        }
        private string CreateSubfolderIfNotExists(DriveService service, string parentFolderId, string subfolderName)
        {
            // Check if the subfolder already exists
            var listRequest = service.Files.List();
            listRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' and trashed = false and name = '{subfolderName}' and '{parentFolderId}' in parents";
            listRequest.Fields = "files(id, name)";
            var existingFolders = listRequest.Execute().Files;

            if (existingFolders.Count > 0)
            {
                // Subfolder already exists
                return existingFolders[0].Id;
            }

            // Create the subfolder
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = subfolderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentFolderId }
            };

            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file.Id;
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
                        string folderPath = Server.MapPath("~/PDFs/travelArrangements/" + empFname);

                        // Create the directory if it doesn't exist
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        HttpFileCollection attachmentsCollection = Request.Files;

                        // Save uploaded files to the server
                        for (int i = 0; i < attachmentsCollection.Count; i++)
                        {
                            HttpPostedFile attachment = attachmentsCollection[i];

                            if (attachment.ContentLength > 0)
                            {
                                string filename = Server.HtmlEncode(Path.GetFileName(attachment.FileName));
                                string extension = Path.GetExtension(filename).ToLower();

                                // Check if the file is a PDF
                                if (extension == ".pdf")
                                {
                                    // Check file size (less than 4MB)
                                    if (attachment.ContentLength < 4100000)
                                    {
                                        string savePath = Path.Combine(folderPath, filename);
                                        attachment.SaveAs(savePath); // Save the file
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

                        // Upload all files in the folderPath to Google Drive
                        List<string> driveLinks = UploadFilesToGoogleDrive(folderPath);

                        if (driveLinks != null && driveLinks.Count > 0)
                        {
                            Session["UploadedDriveLinks"] = driveLinks;
                        }

                        ListUploadedFiles(folderPath); // List all files in the directory after upload

                        Response.Write("<script>alert('Files uploaded successfully.')</script>");
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

        protected void sendEmail_Click(object sender, EventArgs e)
        {
            updateProcessStatus();
            loadDetailsForEmail();
            attachArrangementPDF();

        }

        private void attachArrangementPDF()
        {
            try
            {
                if (Session["travellerName"] == null)
                {
                    Response.Write("<script>alert('Traveller name is missing.')</script>");
                    return;
                }

                string empFname = Session["travellerName"].ToString();
                string folderPath = Server.MapPath("~/PDFs/travelArrangements/" + empFname);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                HttpFileCollection attachmentsCollection = Request.Files;

                if (attachmentsCollection.Count == 0)
                {
                    Response.Write("<script>alert('No files uploaded.')</script>");
                    return;
                }

                for (int i = 0; i < attachmentsCollection.Count; i++)
                {
                    HttpPostedFile attachment = attachmentsCollection[i];

                    if (attachment.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(attachment.FileName); // Ensure the file name is safe
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
                                Response.Write("<script>alert('File " + Server.HtmlEncode(filename) + " was not uploaded because the file size is more than 4MB.')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid File Upload. Please upload a PDF file.')</script>");
                        }
                    }
                }

                // Upload all files in the folderPath to Google Drive
                List<string> driveLinks = UploadFilesToGoogleDrive(folderPath);

                if (driveLinks != null && driveLinks.Count > 0)
                {
                    Session["UploadedDriveLinks"] = driveLinks;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('An error occurred: " + Server.HtmlEncode(ex.Message) + "')</script>");
            }
        }

        private void updateProcessStatus()
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
                            cmd.CommandText = "UPDATE travelRequest SET travelReqStatus = @status, travelProcessStat = @newStatus WHERE travelRequestID = @ID";

                            // Set parameters for updating request status
                            cmd.Parameters.AddWithValue("@newStatus", "Email Sent");
                            cmd.Parameters.AddWithValue("@status", "Requirements Sent");
                            cmd.Parameters.AddWithValue("@ID", requestId);

                            Session["processStat"] = "Email Sent";
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

        //DELETE PDF FILE (1 BY 1)
        protected void DeleteFileButton_Click(object sender, EventArgs e)
        {
            string filePath = hiddenFilePath.Value;
            DeleteFile(filePath);

            // Refresh the file list after deletion
            string empFname = Session["travellerName"].ToString();
            string subFolder = "otherFiles";
            string path = Path.Combine(empFname, subFolder);
            string folderPath = Path.Combine(Server.MapPath("~/PDFs/travelArrangements/"), empFname);
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



    }
}