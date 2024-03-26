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
            employeeDeparture.Text = string.Empty;
            employeeDepartureDate.Text = string.Empty;
            employeeReturn.Text = string.Empty;
            employeePurpose.Text = string.Empty;
            employeeApproval.ClearSelection();
            employeeManager.Text = string.Empty;
            employeeRemarks.Text = string.Empty;
            employeeArrival.Text = string.Empty;
            otherspecified.Text = string.Empty;


        }

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
                    if (File.Exists(Path.Combine(saveDIR, filename)))
                    {
                        uploadStatus.InnerText = "File already exist";
                    }
                    else
                    {
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
                                uploadStatus.InnerText = "Your file was uploaded successfully.";

                                // Write session values to the console
                                Console.WriteLine("imgPath: " + Session["imgPath"]);
                                Console.WriteLine("filename: " + Session["filename"]);
                            }
                            else
                            {
                                uploadStatus.InnerText = "Your file was not uploaded because image size is more than 4MB";
                            }
                        }
                        else
                        {
                            uploadStatus.InnerText = "Invalid File Upload. Please upload an image as a proof of your travel approval";
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

        protected void submitRequests_Click(object sender, EventArgs e)
        {
            Response.Write("<script>console.log('Button clicked');</script>");

        }
        protected void submitRequest_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "TR" + random;


            try
            {

                using (var db = new SqlConnection(connectionString))
                {

                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelLocation, travelEmpID, travelFname, travelMname, travelLname, travelLevel, travelMobilenum, travelProjectCode, travelHomeFacility, travelDeparture, travelReturn, travelPurpose, travelApprovalStat, travelManager, travelRemarks, travelDestination, travelOthers, travelType, travelOptions, travelUserID, travelProofname, travelProofPath)"
                            + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @level, @mobile, @projCode, @facility, @departure, @return, @purpose, @approvalStat, @manager, @remarks, @destination, @others, @type, @options, @userID, @proofname, @proofpath)";

                        string approvalStat = (employeeApproval.SelectedItem.Text == "YES") ? "auto-approved" : "Pending Approval";
                        string filename = Session["filename"].ToString();
                        string imgPath = Session["imgPath"].ToString();

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@location", homeFacility.Text);
                        cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                        cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                        cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                        cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                        cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                        cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                        cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                        cmd.Parameters.AddWithValue("@facility", employeeDeparture.Text);
                        cmd.Parameters.AddWithValue("@departure", employeeDepartureDate.Text);
                        cmd.Parameters.AddWithValue("@return", employeeReturn.Text);
                        cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                        cmd.Parameters.AddWithValue("@approvalStat", approvalStat);
                        cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                        cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                        cmd.Parameters.AddWithValue("@destination", employeeArrival.Text);
                        cmd.Parameters.AddWithValue("@others", otherspecified.Text);
                        cmd.Parameters.AddWithValue("@type", "Domestic");
                        cmd.Parameters.AddWithValue("@options", "Domestic");
                        cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                        cmd.Parameters.AddWithValue("@proofname", filename);
                        cmd.Parameters.AddWithValue("@proofpath", imgPath);



                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert ('Domestic Travel Request Submitted!'); window.location.href = 'ListofRequests.aspx'; </script>");
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert ('Domestic Travel Request Submitted!'); window.location.href = 'ListofRequests.aspx'; </script>");

        }
    }
}