﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        protected void uploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["travellerName"] != null)
                {
                    string empFname = Session["travellerName"].ToString();

                    // Create a directory path using empFname
                    string folderPath = Path.Combine(Server.MapPath("/PDFs/travelArrangements"), empFname);

                    // Check if the directory exists, if not, create it
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Loop through the uploaded files
                    HttpFileCollection attachmentsCollection = Request.Files;
                    if (attachmentsCollection.Count > 0)
                    {
                        for (int i = 0; i < attachmentsCollection.Count; i++)
                        {
                            HttpPostedFile attachment = attachmentsCollection[i];

                            if (attachment.ContentLength > 0)
                            {
                                string filename = Server.HtmlEncode(empFname + "_" + System.IO.Path.GetFileName(attachment.FileName));
                                string extension = System.IO.Path.GetExtension(filename).ToLower();

                                if (extension == ".pdf")
                                {
                                    if (attachment.ContentLength < 4100000)
                                    {
                                        string savePath = System.IO.Path.Combine(folderPath, filename);
                                        attachment.SaveAs(savePath);

                                        Session["pdfPath_" + i] = savePath;
                                        Session["filename_" + i] = filename;

                                        // Log success message to the console
                                        Console.WriteLine("File uploaded successfully: " + filename);
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('File " + filename + " was not uploaded because the file size is more than 4MB.')</script>");
                                        //uploadBlock.Style["display"] = "block";
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid File Upload. Please upload a PDF file.')</script>");
                                    //uploadBlock.Style["display"] = "block";
                                }
                            }
                        }
                        // Display the uploaded PDF files
                        DisplayPDFs(folderPath);

                        Response.Write("<script>alert('All files uploaded successfully.')</script>");
                        //uploadBlock.Style["display"] = "none";
                    }
                    else
                    {
                        Response.Write("<script>alert('Upload failed: No files selected.')</script>");
                        attachFiles.Style["display"] = "block";
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
        private void DisplayPDFs(string folderPath)
        {
            attachFiles.Style["display"] = "none";
            // Get all PDF files in the folder
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
            string empFname = Session["travellerName"].ToString();


            // Create HTML to display PDFs in iframes
            StringBuilder html = new StringBuilder();
            foreach (string pdfFile in pdfFiles)
            {
                string fileName = Path.GetFileName(pdfFile);
                string pdfPath = "/PDFs/travelArrangements/" + empFname + "/" + fileName;
                html.Append("<iframe src='" + pdfPath + "' style='width:50%; height:600px;'></iframe>");
            }

            // Display the HTML content in a placeholder or another container on your page
            pdfPlaceholder.Controls.Add(new LiteralControl(html.ToString()));
        }

    }
}