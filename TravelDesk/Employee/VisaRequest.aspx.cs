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
    public partial class VisaRequest : System.Web.UI.Page
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
            int random = rand.Next(100, 999);

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
                            if (Session["passportName"] != null)
                            {
                                if (Session["userID"] != null)
                                {
                                    // Session values are not null, proceed with inserting into the database
                                    string filename = Session["filename"].ToString();
                                    string imgPath = Session["pdfPath"].ToString();
                                    string passportName = Session["passportName"].ToString();
                                    string passportPath = Session["passportPath"].ToString();
                                    string userID = Session["userID"].ToString();

                                    using (var db = new SqlConnection(connectionString))
                                    {
                                        db.Open();
                                        using (var cmd = db.CreateCommand())
                                        {
                                            cmd.CommandType = CommandType.Text;
                                            cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname, travelDU, travelEmail, travelLevel, travelMobilenum, travelProjectCode, travelPurpose,travelDestination, travelEstdate, travelReqStatus, travelType, travelUserID, travelProofname, travelProofPath, travelPassportPath, travelPassportName, travelDateCreated, travelDateSubmitted, travelDraftStat )"
                                                + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @empDu, @empEmail, @level, @mobile, @projCode, @purpose, @destination, @estDate, @reqStatus, @type,  @userID, @proofname, @proofpath, @passportPath, @passportName, @created, @submitted, @draftStat )";

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

                                            string ID = "VR" + levelText + random + Name;
                                            cmd.Parameters.AddWithValue("@ID", ID);
                                            cmd.Parameters.AddWithValue("@location", homeFacility.Text);
                                            cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                                            cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                                            cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                                            cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                                            cmd.Parameters.AddWithValue("@empDu", employeeDU.Text);
                                            cmd.Parameters.AddWithValue("@empEmail", employeeEmail.Text);
                                            cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                                            cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                                            cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                                            cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                                            cmd.Parameters.AddWithValue("@destination", destination.Text);
                                            cmd.Parameters.AddWithValue("@estDate", estTravelDate.Text);
                                            cmd.Parameters.AddWithValue("@reqStatus", "Pending");
                                            cmd.Parameters.AddWithValue("@type", "Visa Request");
                                            cmd.Parameters.AddWithValue("@userID", userID);
                                            cmd.Parameters.AddWithValue("@proofname", filename);
                                            cmd.Parameters.AddWithValue("@proofpath", imgPath);
                                            cmd.Parameters.AddWithValue("@passportPath", passportPath);
                                            cmd.Parameters.AddWithValue("@passportName", passportName);
                                            cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
                                            cmd.Parameters.AddWithValue("@submitted", DateTime.Now); //date the request is submitted
                                            cmd.Parameters.AddWithValue("@draftStat", "No");

                                            var ctr = cmd.ExecuteNonQuery();

                                            if (ctr >= 1)
                                            {
                                                Response.Write("<script>alert ('Request for VISA successfully Submitted!'); window.location.href = 'EmployeeDashboard.aspx'; </script>");

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
                                    Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");

                                }

                            }
                            else
                            {
                                Response.Write("<script>alert('Something went wrong while retrieving your passport file. Please ensure that you uploaded a valid PDF of your passport and try again.')</script>");
                            }
                        }
                        else
                        {
                            // Session values are null
                            Response.Write("<script>alert('Something went wrong while retrieving your manager approval. Please ensure that you uploaded a valid PDF of your manager approval and try again.')</script>");
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
                            string passportName = Session["passportName"] != null ? Session["passportName"].ToString() : string.Empty;
                            string passportPath = Session["passportPath"] != null ? Session["passportPath"].ToString() : string.Empty;

                            string userID = Session["userID"].ToString();

                            using (var db = new SqlConnection(connectionString))
                            {
                                db.Open();
                                using (var cmd = db.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname, travelDU, travelEmail, travelLevel, travelMobilenum, travelProjectCode, travelPurpose,travelDestination, travelEstdate, travelReqStatus, travelType, travelUserID, travelProofname, travelProofPath, travelPassportPath, travelPassportName, travelDateCreated, travelDateSubmitted, travelDraftStat )"
                                        + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @empDu, @empEmail, @level, @mobile, @projCode, @purpose, @destination, @estDate, @reqStatus, @type,  @userID, @proofname, @proofpath, @passportPath, @passportName, @created, @submitted, @draftStat )";

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

                                    string ID = "VR" + levelText + random + Name;
                                    cmd.Parameters.AddWithValue("@ID", ID);
                                    cmd.Parameters.AddWithValue("@location", homeFacility.Text);
                                    cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                                    cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                                    cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                                    cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                                    cmd.Parameters.AddWithValue("@empDu", employeeDU.Text);
                                    cmd.Parameters.AddWithValue("@empEmail", employeeEmail.Text);
                                    cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                                    cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                                    cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                                    cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                                    cmd.Parameters.AddWithValue("@destination", destination.Text);
                                    cmd.Parameters.AddWithValue("@estDate", estTravelDate.Text);
                                    cmd.Parameters.AddWithValue("@reqStatus", "Pending");
                                    cmd.Parameters.AddWithValue("@type", "Visa Request");
                                    cmd.Parameters.AddWithValue("@userID", userID);
                                    cmd.Parameters.AddWithValue("@proofname", filename);
                                    cmd.Parameters.AddWithValue("@proofpath", imgPath);
                                    cmd.Parameters.AddWithValue("@passportPath", passportPath);
                                    cmd.Parameters.AddWithValue("@passportName", passportName);
                                    cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
                                    cmd.Parameters.AddWithValue("@submitted", DateTime.Now); //date the request is submitted
                                    cmd.Parameters.AddWithValue("@draftStat", "No");

                                    var ctr = cmd.ExecuteNonQuery();

                                    if (ctr >= 1)
                                    {
                                        Response.Write("<script>alert ('Request for VISA successfully Submitted!'); window.location.href = 'EmployeeDashboard.aspx'; </script>");

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
                            Response.Write("<script>alert ('Session Expired!'); window.location.href = '../LoginPage.aspx'; </script>");
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
                Response.Write("<script>alert('An error occurred during submitting your VISA request. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    // Check if the error is "String or binary data would be truncated"
                    if (ex.Errors[i].Number == 8152)
                    {
                        // Log or handle the error related to truncation
                        // For example, log the column causing the truncation
                        string columnName = ex.Errors[i].Message.Split('\'')[1];
                        Response.Write("<script>alert('Data too long for column: " + columnName + "')</script>");
                        // Alternatively, you could adjust the data being inserted to fit within the column size
                        // Example: truncate the data or increase the column size in the database schema
                    }
                    else
                    {
                        // Log other SQL errors
                        Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                    }
                }
            }



        }

        protected void approvalUpload_Click(object sender, EventArgs e)
        {
            string saveDIR = Server.MapPath("/PDFs/VisaRequest/approvalProofs");
            try
            {
                if (Session["userName"] != null)
                {

                    string name = employeeFName.Text + employeeLName.Text;

                    if (employeeUpload.HasFile)
                    {
                        string filename = Server.HtmlEncode(name + "_" + employeeUpload.FileName);
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
                                    Session["pdfPath"] = System.IO.Path.Combine("/PDFs/VisaRequest/approvalProofs/", filename);
                                    Session["filename"] = filename;

                                    string pdfPath = Session["pdfPath"].ToString();

                                    // Show the PDF viewer
                                    pdfViewer.Attributes["src"] = pdfPath;
                                    pdfBlock.Style["display"] = "block";

                                    // Disable the RequiredFieldValidator
                                    RequiredFieldValidator29.Enabled = false;

                                    Response.Write("<script>alert('Your file was uploaded successfully.')</script>");
                                    uploadBlock.Style["display"] = "none";

                                    string path = Session["pdfPath"].ToString();
                                    Session["filePath"] = path;



                                    // Log success message to the console
                                    Console.WriteLine("File uploaded successfully: " + filename);
                                    Console.WriteLine("PDF path: " + Session["imgPath"]);


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

        protected void uploadPassportbtn_Click(object sender, EventArgs e)
        {
            string saveDIR = Server.MapPath("/PDFs/VisaRequest/employeePassports");
            try
            {
                if (Session["userName"] != null)
                {

                    string name = employeeFName.Text + employeeLName.Text;

                    if (passportUpload.HasFile)
                    {
                        string filename = Server.HtmlEncode(name + "_" + passportUpload.FileName);
                        string extension = System.IO.Path.GetExtension(filename).ToLower(); // Convert extension to lowercase for comparison
                        int filesize = employeeUpload.PostedFile.ContentLength;
                        if (File.Exists(System.IO.Path.Combine(saveDIR, filename)))
                        {
                            Response.Write("<script>alert('File already exists. Please upload a valid proof of approval for this travel request.')</script>");
                            uploadPassport.Style["display"] = "block";


                        }
                        else
                        {
                            if (extension == ".pdf") // Allow only PDF files
                            {
                                if (filesize < 4100000)
                                {
                                    string savePath = System.IO.Path.Combine(saveDIR, filename);

                                    // Save the uploaded file
                                    passportUpload.SaveAs(savePath);

                                    // Store file path in session
                                    Session["passportPath"] = System.IO.Path.Combine("/PDFs/VisaRequest/employeePassports/", filename);
                                    Session["passportName"] = filename;

                                    string pdfPath = Session["passportPath"].ToString();

                                    // Show the PDF viewer
                                    passportViewer.Attributes["src"] = pdfPath;
                                    passportBlock.Style["display"] = "block";

                                    // Disable the RequiredFieldValidator
                                    RequiredFieldValidator29.Enabled = false;


                                    Response.Write("<script>alert('Your file was uploaded successfully.')</script>");
                                    uploadPassport.Style["display"] = "none";

                                    string path = Session["passportPath"].ToString();
                                    Session["filePassportPath"] = path;



                                    // Log success message to the console
                                    Console.WriteLine("File uploaded successfully: " + filename);
                                    Console.WriteLine("PDF path: " + Session["imgPath"]);

                                }
                                else
                                {
                                    Response.Write("<script>alert('Your file was not uploaded because the file size is more than 4MB.')</script>");
                                    uploadPassport.Style["display"] = "block";
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid File Upload. Please upload a PDF file as proof of your travel approval.')</script>");
                                uploadPassport.Style["display"] = "block";

                            }

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

        protected void passportReupload_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the file path from the session
                //string filePath = Session["pdfPath"] as string;
                string filePath = Session["filePassportPath"].ToString();

                // Check if the file path is not null or empty
                if (!string.IsNullOrEmpty(filePath))
                {
                    // Delete the file from the server
                    File.Delete(Server.MapPath(filePath));

                    // Clear the session variables
                    Session.Remove("passportPath");
                    Session.Remove("passportName");

                    // Optionally, provide feedback to the user
                    Response.Write("<script>alert('File deleted. Please upload a new copy of your passport to proceed.')</script>");
                    uploadPassport.Style["display"] = "block";
                    passportBlock.Style["display"] = "none";

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

        protected void saveAsDraft_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int random = rand.Next(100, 999);

            try
            {
                if (Session["userID"] != null)
                {
                    // Session values are not null, proceed with inserting into the database
                    string filename = Session["filename"] != null ? Session["filename"].ToString() : null;
                    string imgPath = Session["pdfPath"] != null ? Session["pdfPath"].ToString() : null;
                    string passportName = Session["passportName"] != null ? Session["passportName"].ToString() : null;
                    string passportPath = Session["passportPath"] != null ? Session["passportPath"].ToString() : null;

                    string levelText = employeeLevel.Text;
                    string userID = Session["userID"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO travelVisa (visaReqID, visaEmpID, visaFname, visaLname,  visaMname, visaDU, visaEmail, visaLevel, visaMobile, visaPurpose, visaDestination, visaEstTravelDate, visaReqStatus, visaApprovalPath, visaApprovalName, visaPassportPath, visaPassportName, visaUserID, visaReqCreated, visaDraftStat)"
                                + "VALUES (@ID, @empID, @empFName, @empLName,  @empMName, @empDu, @empEmail, @level, @mobile, @purpose, @destination, @estDate, @reqStatus, @proofpath, @proofname, @passportPath, @passportName, @userID, @created, @draftStat)";


                            string ID = "VR" + random + "DRAFT";
                            cmd.Parameters.AddWithValue("@ID", ID);


                            cmd.Parameters.AddWithValue("@empID", string.IsNullOrEmpty(employeeID.Text) ? DBNull.Value : (object)employeeID.Text);
                            cmd.Parameters.AddWithValue("@empFName", string.IsNullOrEmpty(employeeFName.Text) ? DBNull.Value : (object)employeeFName.Text);
                            cmd.Parameters.AddWithValue("@empMName", string.IsNullOrEmpty(employeeMName.Text) ? DBNull.Value : (object)employeeMName.Text);
                            cmd.Parameters.AddWithValue("@empLName", string.IsNullOrEmpty(employeeLName.Text) ? DBNull.Value : (object)employeeLName.Text);
                            cmd.Parameters.AddWithValue("@empDu", string.IsNullOrEmpty(employeeDU.Text) ? DBNull.Value : (object)employeeDU.Text);
                            cmd.Parameters.AddWithValue("@empEmail", string.IsNullOrEmpty(employeeEmail.Text) ? DBNull.Value : (object)employeeEmail.Text);
                            cmd.Parameters.AddWithValue("@level", string.IsNullOrEmpty(employeeLevel.Text) ? DBNull.Value : (object)employeeLevel.Text);
                            cmd.Parameters.AddWithValue("@mobile", string.IsNullOrEmpty(employeePhone.Text) ? DBNull.Value : (object)employeePhone.Text);
                            cmd.Parameters.AddWithValue("@purpose", string.IsNullOrEmpty(employeePurpose.Text) ? DBNull.Value : (object)employeePurpose.Text);
                            cmd.Parameters.AddWithValue("@destination", string.IsNullOrEmpty(destination.Text) ? DBNull.Value : (object)destination.Text);
                            cmd.Parameters.AddWithValue("@estDate", string.IsNullOrEmpty(estTravelDate.Text) ? DBNull.Value : (object)estTravelDate.Text);
                            cmd.Parameters.AddWithValue("@reqStatus", "Draft");
                            cmd.Parameters.AddWithValue("@proofpath", string.IsNullOrEmpty(imgPath) ? DBNull.Value : (object)imgPath);
                            cmd.Parameters.AddWithValue("@proofname", string.IsNullOrEmpty(filename) ? DBNull.Value : (object)filename);
                            cmd.Parameters.AddWithValue("@passportPath", string.IsNullOrEmpty(passportPath) ? DBNull.Value : (object)passportPath);
                            cmd.Parameters.AddWithValue("@passportName", string.IsNullOrEmpty(passportName) ? DBNull.Value : (object)passportName);
                            cmd.Parameters.AddWithValue("@userID", string.IsNullOrEmpty(userID) ? DBNull.Value : (object)userID);
                            cmd.Parameters.AddWithValue("@created", DateTime.Now); //date the request is created regardless if submitted or as draft
                            cmd.Parameters.AddWithValue("@draftStat", "Yes");

                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {

                                Session.Remove("filePath");
                                Session.Remove("pdfPath");
                                Session.Remove("passportName");
                                Session.Remove("passportPath");

                                Response.Write("<script>alert ('Request Saved! You can access your DRAFTS in the Requests'); window.location.href = 'EmployeeDashboard.aspx'; </script>");

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
                    Response.Write("<script>alert('Session Expired! Please login again.'); window.location.href = '../LoginPage.aspx'; </script>");

                }

            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred during submitting your VISA request. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    // Check if the error is "String or binary data would be truncated"
                    if (ex.Errors[i].Number == 8152)
                    {
                        // Log or handle the error related to truncation
                        // For example, log the column causing the truncation
                        string columnName = ex.Errors[i].Message.Split('\'')[1];
                        Response.Write("<script>alert('Data too long for column: " + columnName + "')</script>");
                        // Alternatively, you could adjust the data being inserted to fit within the column size
                        // Example: truncate the data or increase the column size in the database schema
                    }
                    else
                    {
                        // Log other SQL errors
                        Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                    }
                }
            }
        }
    }
}