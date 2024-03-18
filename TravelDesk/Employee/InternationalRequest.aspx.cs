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

        protected void submitRequestbtn_Click(object sender, EventArgs e)
        {

            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "TR" + employeeID.Text + random;


            try
            {

                uploadImage();


                using (var db = new SqlConnection(connectionString))
                {

                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelLocation, travelEmpID, travelFname, travelDesignation, travelLevel, travelVoip, travelMobilenum, travelProjectCode, travelHomeFacility, travelDeparture, travelReturn, travelPurpose, travelApprovalStat, travelManager, travelRemarks, travelDestination, travelOthers, travelType, travelOptions, travelUserID, travelProofname, travelProofPath)"
                            + "VALUES (@ID, @location, @empID, @empName, @designation, @level, @voip, @mobile, @projCode, @facility, @departure, @return, @purpose, @approvalStat, @manager, @remarks, @destination, @others, @type, @options, @userID, @proofname, @proofpath)";

                        string approvalStat = (employeeApproval.SelectedItem.Text == "YES") ? "auto-approved" : "Pending Approval";
                        string filename = Session["filename"].ToString();
                        string imgPath = Session["imgPath"].ToString();

                        cmd.Parameters.AddWithValue("@ID", ID);
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
                        cmd.Parameters.AddWithValue("@approvalStat", approvalStat);
                        cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                        cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                        cmd.Parameters.AddWithValue("@destination", employeeDestination.Text);
                        cmd.Parameters.AddWithValue("@others", employeeOthers.Text);
                        cmd.Parameters.AddWithValue("@type", "International");
                        cmd.Parameters.AddWithValue("@options", flightOptions.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
                        cmd.Parameters.AddWithValue("@proofname", filename);
                        cmd.Parameters.AddWithValue("@proofpath", imgPath);

                        

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
            employeeName.Text = string.Empty;
            employeeDesignation.Text = string.Empty;
            employeeLevel.Text = string.Empty;
            employeeVoip.Text = string.Empty;
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
            employeeOthers.Text = string.Empty;
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

        private void uploadImage()
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
    }
}