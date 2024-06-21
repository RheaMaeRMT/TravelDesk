using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Admin
{
    public partial class VisaRequestDetails : System.Web.UI.Page
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
                DisplayRequest();
            }
        }
        private void DisplayRequest()
        {
            try
            {
                // Get the ID of the clicked request from the session
                string requestId = Session["clickedVRequest"].ToString();

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
                                        SELECT * FROM travelRequest WHERE travelRequestID = @RequestId AND travelDraftStat = 'No'";


                            cmd.Parameters.AddWithValue("@RequestId", requestId);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    uploadBlock.Style["display"] = "block";
                                    pdfViewer.Style["display"] = "block";


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
                                    string passport = reader["travelPassportPath"].ToString();
                                    string remarks = reader["travelRemarks"].ToString();
                                    string travelDestination = reader["travelDestination"].ToString();
                                    string estimatedTravel = reader["travelEstdate"].ToString();


                                    Session["travellerName"] = empFname + " " + empMname + " " + empLname;
                                    Session["userEmail"] = empEmail;

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
                                    pdfViewer.Src = proof;
                                    passportViewer.Src = passport;
                                    employeePurpose.Text = travelPurpose;
                                    employeeDestination.Text = travelDestination;
                                    estTravelDate.Text = estimatedTravel;

                                    if (!string.IsNullOrEmpty(estimatedTravel))
                                    {
                                        // Parse the date string into a DateTime object
                                        DateTime arrivalDateTime;
                                        if (DateTime.TryParse(estimatedTravel, out arrivalDateTime))
                                        {
                                            // Format the DateTime object into the desired format
                                            string formattedArrivalDate = arrivalDateTime.ToString("MMMM dd, yyyy");

                                            // Assign the formatted date to the TextBox
                                            estTravelDate.Text = formattedArrivalDate;
                                        }
                                    }

                                    string status = reader["travelReqStatus"].ToString();

                                    Session["currentStatus"] = status;

                                    //////PROCEED TO GET THE STATUS AND DISPLAY IN TRACKING
                                    ////getTrackingStatus();
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

        protected void sendToTraveller_Click(object sender, EventArgs e)
        {
            loadDetailsForEmail();
            updateProcessStatus();

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

        private void loadDetailsForEmail()
        {
            if (Session["travellerName"] != null && Session["userEmail"] != null)
            {

                string receiverEmail = Session["userEmail"].ToString();
                string name = Session["travellerName"].ToString();

                // Retrieve drive links from session
                List<string> driveLinks = (List<string>)Session["UploadedDriveLinks"] ?? new List<string>();

                // Assign drive links to the LINKS list
                LINKS.AddRange(driveLinks);

                // Ensure the formatted links are encoded to be used in JavaScript
                string formattedLinks = driveLinks.Count > 0 ? HttpUtility.JavaScriptStringEncode(GetFormattedLinks(driveLinks)) : "";

                // Pass the email details including the uploaded file links to the JavaScript function
                string script = $"<script>sendEmailWithDriveLinks('{receiverEmail}', '{name}', '{formattedLinks}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SendEmailScript", script);


            }
            Session.Remove("UploadedDriveLinks");
        }

        private void updateProcessStatus()
        {
            try
            {
                if (Session["clickedVRequest"] != null)
                {
                    string requestId = Session["clickedVRequest"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE travelRequest SET travelProcessStat = @newStatus, travelReqStatus = @status WHERE travelRequestID = @ID";

                            // Set parameters for updating request status
                            cmd.Parameters.AddWithValue("@status", "Requirements Sent");
                            cmd.Parameters.AddWithValue("@newStatus", "Email Sent");
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



        private List<string> LINKS = new List<string>();
        private string UploadFileToGoogleDrive(HttpPostedFile postedFile)
        {
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
                string parentFolderId = "1DUDq7VGgqtwTjfX7oaIKmB7BxsYG-VO1"; // Replace with your parent folder ID

                // Get the subfolder name from the session
                string subfolderName = Session["travellerName"].ToString();

                // Ensure the subfolder exists (create if necessary)
                string subfolderId = CreateSubfolderIfNotExists(service, parentFolderId, subfolderName);

                // Upload the file to the subfolder
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(postedFile.FileName),
                    Parents = new List<string> { subfolderId } // Ensure the file is uploaded to the subfolder
                };

                FilesResource.CreateMediaUpload request;
                using (var stream = postedFile.InputStream)
                {
                    request = service.Files.Create(fileMetadata, stream, postedFile.ContentType);
                    request.Fields = "id, webContentLink"; // Request the ID and web content link of the uploaded file
                    request.Upload();
                }

                var file = request.ResponseBody;

                // Log file upload success
                Console.WriteLine("File uploaded successfully to Google Drive. File ID: " + file.Id);

                // Construct the Google Drive link
                string driveLink = file.WebContentLink;
                Console.WriteLine("Google Drive link: " + driveLink);

                LINKS.Add(driveLink);

                // Log the current list of links
                Console.WriteLine("Current LINKS: " + string.Join(", ", LINKS));

                // Return the Google Drive link
                return driveLink;
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

        protected void attachFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (requirementFiles.HasFile)
                {
                    if (Session["travellerName"].ToString() != null)
                    {
                        string empFname = Session["travellerName"].ToString();
                        //string subFolder = "otherFiles";
                        string folderPath = Server.MapPath("~/PDFs/VisaRequest/VisaRequirements/" + empFname);

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        HttpFileCollection attachmentsCollection = Request.Files;
                        List<string> driveLinks = new List<string>();

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

                                        try
                                        {
                                            string driveLink = UploadFileToGoogleDrive(attachment);
                                            if (!string.IsNullOrEmpty(driveLink))
                                            {
                                                driveLinks.Add(driveLink);
                                            }
                                            else
                                            {
                                                Response.Write("<script>alert('Failed to upload " + filename + " to Google Drive.')</script>");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Response.Write("<script>alert('Error uploading " + filename + " to Google Drive: " + ex.Message + "')</script>");
                                        }
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

                        ListUploadedFiles(folderPath);

                        // Print the Google Drive links to the console (for debugging)
                        foreach (var link in driveLinks)
                        {
                            Console.WriteLine("Uploaded to Google Drive: " + link);
                        }

                        if (driveLinks.Count > 0)
                        {
                            Session["UploadedDriveLinks"] = driveLinks;
                        }

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
    }
}