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
                            cmd.CommandText = "SELECT * FROM travelRequest WHERE travelRequestID = @request";
                            cmd.Parameters.AddWithValue("@request", clickedRequest);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate textboxes with data from the travelRequest table
                                    employeeLocation.Text = reader["travelLocation"].ToString();
                                    employeeID.Text = reader["travelEmpID"].ToString();
                                    employeeName.Text = reader["travelFname"].ToString();
                                    employeeDesignation.Text = reader["travelDesignation"].ToString();
                                    employeeLevel.Text = reader["travelLevel"].ToString();
                                    employeeVoip.Text = reader["travelVoip"].ToString();
                                    employeePhone.Text = reader["travelMobilenum"].ToString();
                                    employeeProjCode.Text = reader["travelProjectCode"].ToString();
                                    employeeFacility.Text = reader["travelHomeFacility"].ToString();
                                    employeeDeparture.Text = Convert.ToDateTime(reader["travelDeparture"]).ToString("yyyy-MM-dd");
                                    employeeReturn.Text = Convert.ToDateTime(reader["travelReturn"]).ToString("yyyy-MM-dd");
                                    employeePurpose.SelectedValue = reader["travelPurpose"].ToString();
                                    employeeDestination.Text = reader["travelDestination"].ToString();
                                    employeeOthers.Text = reader["travelOthers"].ToString();
                                    string approvalStatus = reader["travelApprovalStat"].ToString();
                                    if (approvalStatus == "auto-approved" || approvalStatus == "processing" || approvalStatus == "completed")
                                    {
                                        employeeApproval.SelectedValue = "1"; // YES
                                        statusRequest.Text = approvalStatus;
                                    }
                                    else if (approvalStatus == "pending")
                                    {
                                        employeeApproval.SelectedValue = "0"; // NO
                                        statusRequest.Text = approvalStatus;
                                    }

                                    employeeManager.Text = reader["travelManager"].ToString();

                                    // Set the ImageUrl property to the value of travelProofPath
                                    string imagePath = reader["travelProofPath"].ToString();
                                    if (!string.IsNullOrEmpty(imagePath))
                                    {
                                        productImage.Visible = true;
                                        productImage.ImageUrl = imagePath;
                                    }
                                    else
                                    {
                                        productImage.Visible = false;
                                    }

                                    employeeRemarks.Text = reader["travelRemarks"].ToString();
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
            string saveDIR = Server.MapPath("/approvalProofs");
            try
            {
                if (employeeUpload.HasFile)
                {
                    string filename = Server.HtmlEncode(employeeUpload.FileName);
                    string extension = System.IO.Path.GetExtension(filename);
                    int filesize = employeeUpload.PostedFile.ContentLength;

                    if ((extension == ".jpg") || (extension == ".jpeg") || (extension == ".png") || (extension == ".JPG") || (extension == ".JPEG") || (extension == ".PNG"))
                    {
                        if (filesize < 4100000)
                        {
                            string savePath = Path.Combine(saveDIR, filename);
                            employeeUpload.SaveAs(savePath);
                            productImage.Visible = true;
                            productImage.ImageUrl = Path.Combine("/approvalProofs/", filename);
                            Session["imgPath"] = Path.Combine("/approvalProofs/", filename);
                            Session["filename"] = filename;

                            // Write session values to the console
                            Console.WriteLine("imgPath: " + Session["imgPath"]);
                            Console.WriteLine("filename: " + Session["filename"]);

                            Response.Write("<script>alert ('New Proof of Approval uploaded!'); window.location.href = window.location.href; </script>");

                        }
                        else
                        {
                            Response.Write("<script>alert ('Your file was not uploaded because image size is more than 4MB'); window.location.reload(); </script>");

                        }
                    }
                    else
                    {
                        Response.Write("<script>alert ('Invalid File Upload. Please upload an image as a proof of your travel approval'); window.location.reload(); </script>");

                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre><script>alert('" + ex.Message + "');</script>");
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
                            cmd.CommandText = "UPDATE travelRequest SET travelLocation = @location, travelEmpID = @empID, travelFname = @empName, travelDesignation = @designation, travelLevel = @level, travelVoip = @voip, travelMobilenum = @mobile, travelProjectCode = @projCode, travelHomeFacility = @facility, travelDeparture = @departure, travelReturn = @return, travelPurpose = @purpose, travelManager = @manager, travelRemarks = @remarks, travelDestination = @destination, travelOthers = @others, travelType = @type, travelOptions = @options, travelUserID = @userID, travelProofname = @proofname, travelProofPath = @proofpath WHERE travelRequestID = @ID";

                            string clickedRequest = Session["clickedRequest"]?.ToString(); // Null-conditional operator added
                            string filename = Session["filename"].ToString();
                            string imgPath = Session["imgPath"].ToString();

                            // Add parameters excluding the travelRequestID
                            cmd.Parameters.AddWithValue("@location", employeeLocation.Text);
                            cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                            cmd.Parameters.AddWithValue("@empName", employeeName.Text);
                            cmd.Parameters.AddWithValue("@designation", employeeDesignation.Text);
                            cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                            cmd.Parameters.AddWithValue("@voip", employeeVoip.Text);
                            cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                            cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                            cmd.Parameters.AddWithValue("@facility", employeeFacility.Text);
                            cmd.Parameters.AddWithValue("@departure", employeeDeparture.Text);
                            cmd.Parameters.AddWithValue("@return", employeeReturn.Text);
                            cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                            cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                            cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                            cmd.Parameters.AddWithValue("@destination", employeeDestination.Text);
                            cmd.Parameters.AddWithValue("@others", employeeOthers.Text);
                            cmd.Parameters.AddWithValue("@type", "Domestic");
                            cmd.Parameters.AddWithValue("@options", "Domestic");
                            cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                            cmd.Parameters.AddWithValue("@proofname", filename);
                            cmd.Parameters.AddWithValue("@proofpath", imgPath);
                            cmd.Parameters.AddWithValue("@ID", clickedRequest);


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
                            cmd.CommandText = "UPDATE travelRequest SET travelLocation = @location, travelEmpID = @empID, travelFname = @empName, travelDesignation = @designation, travelLevel = @level, travelVoip = @voip, travelMobilenum = @mobile, travelProjectCode = @projCode, travelHomeFacility = @facility, travelDeparture = @departure, travelReturn = @return, travelPurpose = @purpose, travelManager = @manager, travelRemarks = @remarks, travelDestination = @destination, travelOthers = @others, travelType = @type, travelOptions = @options, travelUserID = @userID, travelProofname = @proofname, travelProofPath = @proofpath WHERE travelRequestID = @ID";


                            // Add parameters excluding the travelRequestID
                            cmd.Parameters.AddWithValue("@location", employeeLocation.Text);
                            cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                            cmd.Parameters.AddWithValue("@empName", employeeName.Text);
                            cmd.Parameters.AddWithValue("@designation", employeeDesignation.Text);
                            cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                            cmd.Parameters.AddWithValue("@voip", employeeVoip.Text);
                            cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                            cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                            cmd.Parameters.AddWithValue("@facility", employeeFacility.Text);
                            cmd.Parameters.AddWithValue("@departure", employeeDeparture.Text);
                            cmd.Parameters.AddWithValue("@return", employeeReturn.Text);
                            cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                            cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                            cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                            cmd.Parameters.AddWithValue("@destination", employeeDestination.Text);
                            cmd.Parameters.AddWithValue("@others", employeeOthers.Text);
                            cmd.Parameters.AddWithValue("@type", "Domestic");
                            cmd.Parameters.AddWithValue("@options", "Domestic");
                            cmd.Parameters.AddWithValue("@proofname", Session["filename"].ToString());
                            cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());

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

    }
}