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
                List<string> filePaths = new List<string>();

                if (attachments.HasFile)
                {
                    if (Session["travellerName"] != null)
                    {
                        string empFname = Session["travellerName"].ToString();
                        string subFolder = "otherFiles";
                        string path = Path.Combine(empFname, subFolder);
                        string folderPath = Path.Combine(Server.MapPath("/PDFs/travelArrangements"), path);

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

                                        filePaths.Add(savePath);

                                        Console.WriteLine("File uploaded successfully: " + filename);
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

                        DisplayPDFs(folderPath);
                        Response.Write("<script>alert('File uploaded successfully.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

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

        private void DisplayPDFs(string folderPath)
        {
            attached.Style["display"] = "block";
            deleteFiles.Style["display"] = "block";
            deleteFiles.Style["Width"] = "100px";
            attached.Style["Width"] = "100px";

            // Get all PDF files in the folder
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
            string empFname = Session["travellerName"].ToString();

            // Create HTML to display PDFs in iframes
            StringBuilder html = new StringBuilder();
            foreach (string pdfFile in pdfFiles)
            {
                FileInfo fileInfo = new FileInfo(pdfFile);
                // Check if the file was created or modified today
                if (fileInfo.CreationTime.Date == DateTime.Today || fileInfo.LastWriteTime.Date == DateTime.Today)
                {
                    string fileName = Path.GetFileName(pdfFile);
                    string pdfPath = "/PDFs/travelArrangements/" + empFname + "/" + "otherFiles" + "/" + fileName;
                    html.Append("<iframe src='" + pdfPath + "' style='width:50%; height:600px;'></iframe>");
                }
            }

            // Display the HTML content in a placeholder or another container on your page
            pdfPlaceholder.Controls.Add(new LiteralControl(html.ToString()));
        }

        protected void deleteFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["travellerName"] != null)
                {
                    deleteFiles.Style["display"] = "none";
                    attached.Style["display"] = "none";

                    string empFname = Session["travellerName"].ToString();
                    string folderPath = Server.MapPath("/PDFs/travelArrangements/" + empFname + "/" + "otherFiles");

                    if (Directory.Exists(folderPath))
                    {
                        // Get all PDF files in the folder
                        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

                        // Delete PDFs uploaded today
                        foreach (string pdfFile in pdfFiles)
                        {
                            FileInfo fileInfo = new FileInfo(pdfFile);
                            if (fileInfo.CreationTime.Date == DateTime.Today)
                            {
                                File.Delete(pdfFile);
                            }
                        }




                        // Display success message
                        Response.Write("<script>alert('PDFs uploaded today have been deleted.')</script>");

                        // Clear the PDF display
                        pdfPlaceholder.Controls.Clear();
                    }
                    else
                    {
                        // Display message if folder does not exist
                        Response.Write("<script>alert('No files to delete.')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write("<script>alert('An error occurred while deleting files.')</script>");
                Console.WriteLine("Error deleting files: " + ex.Message);
            }
        }

        protected void sendEmail_Click(object sender, EventArgs e)
        {
            loadDetailsForEmail();

        }



    }
}