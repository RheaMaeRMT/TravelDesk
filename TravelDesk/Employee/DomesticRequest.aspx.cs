using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

            Random rand = new Random();
            int random = rand.Next(100000, 999999);
            string ID = "TR" + random;


            try
            {

                if (Session["filename"] != null && Session["imgPath"] != null && Session["userID"] != null)
                {
                    // Session values are not null, proceed with inserting into the database
                    string filename = Session["filename"].ToString();
                    string imgPath = Session["imgPath"].ToString();
                    string userID = Session["userID"].ToString();

                    using (var db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname , travelLevel, travelMobilenum, travelProjectCode, travelFrom, travelDeparture, travelReturn, travelPurpose, travelReqStatus, travelManager, travelRemarks, travelTo, travelOthers, travelType, travelOptions, travelUserID, travelProofname, travelProofPath)"
                                + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @level, @mobile, @projCode, @from, @departure, @return, @purpose, @reqStatus, @manager, @remarks, @destination, @others, @type, @options, @userID, @proofname, @proofpath)";

                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue("@location", homeFacility.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@empID", employeeID.Text);
                            cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
                            cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
                            cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
                            cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                            cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
                            cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
                            cmd.Parameters.AddWithValue("@from", employeeFrom.Text);
                            cmd.Parameters.AddWithValue("@departure", employeeDepartureDate.Text);
                            cmd.Parameters.AddWithValue("@return", employeeArrivalDate.Text);
                            cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
                            cmd.Parameters.AddWithValue("@reqStatus", "Approved");
                            cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                            cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                            cmd.Parameters.AddWithValue("@destination", employeeTo.Text);
                            cmd.Parameters.AddWithValue("@others", otherspecified.Text);
                            cmd.Parameters.AddWithValue("@type", "Domestic");
                            cmd.Parameters.AddWithValue("@options", flightOptions.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@userID", userID);
                            cmd.Parameters.AddWithValue("@proofname", filename);
                            cmd.Parameters.AddWithValue("@proofpath", imgPath);

                            var ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                //Response.Write("<script>alert ('Domestic Travel Request Submitted!'); window.location.href = 'ListofRequests.aspx'; </script>");
                                insertRoute(ID);
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
                    Response.Write("<script>alert('Session Expired! Please login again.')</script>");
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
        //protected void submitRequestbtn_Click(object sender, EventArgs e)
        //{
        //    Response.Write("<script>console.log('Button clicked');</script>");


        //    Random rand = new Random();
        //    int random = rand.Next(100000, 999999);
        //    string ID = "TR" + employeeID.Text + random;


        //    try
        //    {


        //        using (var db = new SqlConnection(connectionString))
        //        {

        //            db.Open();
        //            using (var cmd = db.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelHomeFacility, travelEmpID, travelFname, travelMname, travelLname, travelLevel, travelMobilenum, travelProjectCode, travelFrom, travelDeparture, travelReturn, travelPurpose, travelApprovalStat, travelManager, travelRemarks, travelTo, travelOthers, travelType, travelOptions, travelUserID, travelProofname, travelProofPath)" //
        //                    + "VALUES (@ID, @location, @empID, @empFName, @empMName, @empLName, @level, @mobile, @projCode, @facility, @departure, @return, @purpose, @approvalStat, @manager, @remarks, @destination, @others, @type,  @options, @userID, @proofname, @proofpath)"; //

        //                string approvalStat = (employeeApproval.SelectedItem.Text == "YES") ? "auto-approved" : "Pending Approval";
        //                string filename = Session["filename"].ToString();
        //                string imgPath = Session["imgPath"].ToString();

        //cmd.Parameters.AddWithValue("@ID", ID);
        //                cmd.Parameters.AddWithValue("@location", homeFacility.SelectedItem.Text);
        //                cmd.Parameters.AddWithValue("@empID", employeeID.Text);
        //                cmd.Parameters.AddWithValue("@empFName", employeeFName.Text);
        //                cmd.Parameters.AddWithValue("@empMName", employeeMName.Text);
        //                cmd.Parameters.AddWithValue("@empLName", employeeLName.Text);
        //                cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
        //                cmd.Parameters.AddWithValue("@mobile", employeePhone.Text);
        //                cmd.Parameters.AddWithValue("@projCode", employeeProjCode.Text);
        //                cmd.Parameters.AddWithValue("@facility", employeeFrom.Text);
        //                cmd.Parameters.AddWithValue("@departure", employeeDepartureDate.Text);
        //                cmd.Parameters.AddWithValue("@return", employeeArrivalDate.Text);
        //                cmd.Parameters.AddWithValue("@purpose", employeePurpose.Text);
        //                cmd.Parameters.AddWithValue("@approvalStat", approvalStat);
        //                cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
        //                cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
        //                cmd.Parameters.AddWithValue("@destination", employeeTo.Text);
        //                cmd.Parameters.AddWithValue("@others", otherspecified.Text);
        //                cmd.Parameters.AddWithValue("@type", "Domestic");
        //                cmd.Parameters.AddWithValue("@options", flightOptions.SelectedItem.Text);
        //                cmd.Parameters.AddWithValue("@userID", Session["userID"].ToString());
        //                cmd.Parameters.AddWithValue("@proofname", filename);
        //                cmd.Parameters.AddWithValue("@proofpath", imgPath);



        //                var ctr = cmd.ExecuteNonQuery();

        //                if (ctr >= 1)
        //                {
        //                    Response.Write("<script>alert('Domestic Travel Request Submitted!')</script>");
        //                    //insertRoute(ID);
        //                }
        //                else
        //                {
        //                    Response.Write("<script>alert('An error occurred. Please try again.')</script>");

        //                }
        //            }
        //        }



        //    }
        //    catch (SqlException ex)
        //    {
        //        // Log the exception or display a user-friendly error message
        //        // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //        Response.Write("<script>alert('An error occurred during travel request enrollment. Please try again.')</script>");
        //        // Log additional information from the SQL exception
        //        for (int i = 0; i < ex.Errors.Count; i++)
        //        {
        //            Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
        //        }
        //    }

        //}
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
            employeeDepartureDate.Text = string.Empty;
            employeeFrom.Text = string.Empty;
            employeePurpose.Text = string.Empty;
            employeeApproval.ClearSelection();
            employeeManager.Text = string.Empty;
            employeeRemarks.Text = string.Empty;
            employeeTo.Text = string.Empty;
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
                    if (File.Exists(System.IO.Path.Combine(saveDIR, filename)))
                    {
                        Response.Write("<script>alert('File already exist')</script>");
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
                                displayContents();
                                
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
                    Response.Write("<script>alert('Upload Failed: Try again')</script>");

                }
            }
            catch (Exception ex)
            {
                Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre><script>alert('" + ex.Message + "');</script>");
            }

        }
        private void displayContents()
        {
            // Check the selected option and update the visibility of input fields accordingly
            string selectedOption = flightOptions.SelectedItem.Value;

            if (selectedOption == "One Way")
            {
                oneWaynput.Style["display"] = "block";
                roundTripInput.Style["display"] = "none";
                multipleInput.Style["display"] = "none";
            }
            else if (selectedOption == "Roundtrip")
            {
                oneWaynput.Style["display"] = "none";
                roundTripInput.Style["display"] = "block";
                multipleInput.Style["display"] = "none";
            }
            else if (selectedOption == "multiple")
            {
                oneWaynput.Style["display"] = "none";
                roundTripInput.Style["display"] = "none";
                multipleInput.Style["display"] = "block";
            }

            string otherSelected = employeePurpose.SelectedValue;

            if (otherSelected == "Others")
            {
                Label14.Style["display"] = "block";
                otherspecified.Style["display"] = "block";
            }
        }

        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    string saveDIR = Server.MapPath("/approvalProofs");
        //    try
        //    {
        //        if (employeeUpload.HasFile)
        //        {
        //            string filename = Server.HtmlEncode(employeeUpload.FileName);
        //            string extension = System.IO.Path.GetExtension(filename);
        //            int filesize = employeeUpload.PostedFile.ContentLength;
        //            if (File.Exists(Path.Combine(saveDIR, filename)))
        //            {
        //                uploadStatus.InnerText = "File already exist";
        //            }
        //            else
        //            {
        //                if ((extension == ".jpg") || (extension == ".jpeg") || (extension == ".png") || (extension == ".JPG") || (extension == ".JPEG") || (extension == ".PNG"))
        //                {
        //                    if (filesize < 4100000)
        //                    {
        //                        string savePath = Path.Combine(saveDIR, filename);
        //                        employeeUpload.SaveAs(savePath);
        //                        productImage.Visible = true;
        //                        productImage.ImageUrl = Path.Combine("/approvalProofs/", filename);
        //                        Session["imgPath"] = Path.Combine("/approvalProofs/", filename);
        //                        Session["filename"] = filename;
        //                        uploadStatus.InnerText = "Your file was uploaded successfully.";

        //                        // Write session values to the console
        //                        Console.WriteLine("imgPath: " + Session["imgPath"]);
        //                        Console.WriteLine("filename: " + Session["filename"]);
        //                    }
        //                    else
        //                    {
        //                        uploadStatus.InnerText = "Your file was not uploaded because image size is more than 4MB";
        //                    }
        //                }
        //                else
        //                {
        //                    uploadStatus.InnerText = "Invalid File Upload. Please upload an image as a proof of your travel approval";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            uploadStatus.InnerText = "Upload Failed: Try again";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre style='background: white;'>" + ex.ToString() + "</pre><script>alert('" + ex.Message + "');</script>");
        //    }

        //}

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
                        cmd.Parameters.AddWithValue("@mul1ToDate", string.IsNullOrEmpty(TextBox12.Text) ? (object)DBNull.Value : TextBox12.Text);
                        cmd.Parameters.AddWithValue("@mul2From", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@mul2To", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mul2FromDate", string.IsNullOrEmpty(TextBox13.Text) ? (object)DBNull.Value : TextBox13.Text);
                        cmd.Parameters.AddWithValue("@mul2ToDate", string.IsNullOrEmpty(TextBox14.Text) ? (object)DBNull.Value : TextBox14.Text);
                        cmd.Parameters.AddWithValue("@mul3From", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@mul3To", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@mul3FromDate", string.IsNullOrEmpty(TextBox16.Text) ? (object)DBNull.Value : TextBox16.Text);
                        cmd.Parameters.AddWithValue("@mul3ToDate", string.IsNullOrEmpty(TextBox18.Text) ? (object)DBNull.Value : TextBox18.Text);
                        cmd.Parameters.AddWithValue("@mul4From", TextBox27.Text);
                        cmd.Parameters.AddWithValue("@mul4To", TextBox29.Text);
                        cmd.Parameters.AddWithValue("@mul4FromDate", string.IsNullOrEmpty(TextBox28.Text) ? (object)DBNull.Value : TextBox28.Text);
                        cmd.Parameters.AddWithValue("@mul4ToDate", string.IsNullOrEmpty(TextBox30.Text) ? (object)DBNull.Value : TextBox30.Text);
                        cmd.Parameters.AddWithValue("@mul5From", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@mul5To", TextBox21.Text);
                        cmd.Parameters.AddWithValue("@mul5FromDate", string.IsNullOrEmpty(TextBox20.Text) ? (object)DBNull.Value : TextBox20.Text);
                        cmd.Parameters.AddWithValue("@mul5ToDate", string.IsNullOrEmpty(TextBox22.Text) ? (object)DBNull.Value : TextBox22.Text);


                        // Execute the insertion into anotherTable
                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {

                            Response.Write("<script>alert('Domestic Travel Request Submitted!')</script>");


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

        //private void insertRoute(string ID)
        //{
        //    try
        //    {
        //        // Generate a unique route ID using GUID
        //        string routeID = "R" + Guid.NewGuid().ToString().Substring(0, 6);

        //        using (var db = new SqlConnection(connectionString))
        //        {
        //            db.Open();
        //            using (var cmd = db.CreateCommand())
        //            {
        //                // Use parameterized query to prevent SQL injection
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = @"INSERT INTO route 
        //                            (routeID, routeTravelID, routeOFrom, routeOTo, 
        //                            routeR1From, routeR1To, routeR2From, routeR2To, 
        //                            routeM1From, routeM1To, routeM1FromDate, routeM1ToDate, 
        //                            routeM2From, routeM2To, routeM2FromDate, routeM2ToDate, 
        //                            routeM3From, routeM3To, routeM3FromDate, routeM3ToDate,  
        //                            routeM4From, routeM4To, routeM4FromDate, routeM4ToDate,  
        //                            routeM5From, routeM5To, routeM5FromDate, routeM5ToDate)
        //                            VALUES 
        //                            (@ID, @routeTravelID, @onewayFrom, @onewayTo, 
        //                            @round1From, @round1To, @round2From, @round2To, 
        //                            @mul1From, @mul1To, @mul1FromDate, @mul1ToDate,  
        //                            @mul2From, @mul2To, @mul2FromDate, @mul2ToDate,  
        //                            @mul3From, @mul3To, @mul3FromDate, @mul3ToDate,  
        //                            @mul4From, @mul4To, @mul4FromDate, @mul4ToDate,  
        //                            @mul5From, @mul5To, @mul5FromDate, @mul5ToDate)";

        //                // Set parameter values
        //                cmd.Parameters.AddWithValue("@ID", routeID);
        //                cmd.Parameters.AddWithValue("@routeTravelID", ID);
        //                cmd.Parameters.AddWithValue("@onewayFrom", onewayFrom.Text);
        //                cmd.Parameters.AddWithValue("@onewayTo", onewayTo.Text);
        //                cmd.Parameters.AddWithValue("@round1From", round1From.Text);
        //                cmd.Parameters.AddWithValue("@round1To", round1To.Text);
        //                cmd.Parameters.AddWithValue("@round2From", round2From.Text);
        //                cmd.Parameters.AddWithValue("@round2To", round2To.Text);

        //                //FIRST
        //                cmd.Parameters.AddWithValue("@mul1From", TextBox7.Text);
        //                cmd.Parameters.AddWithValue("@mul1To", TextBox8.Text);
        //                cmd.Parameters.AddWithValue("@mul1FromDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox11.Text);
        //                cmd.Parameters.AddWithValue("@mul1ToDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox12.Text);

        //                //SECOND
        //                cmd.Parameters.AddWithValue("@mul2From", TextBox9.Text);
        //                cmd.Parameters.AddWithValue("@mul2To", TextBox10.Text);
        //                cmd.Parameters.AddWithValue("@mul2FromDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox13.Text);
        //                cmd.Parameters.AddWithValue("@mul2ToDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox14.Text);

        //                //THIRD
        //                cmd.Parameters.AddWithValue("@mul3From", TextBox15.Text);
        //                cmd.Parameters.AddWithValue("@mul3To", TextBox17.Text);
        //                cmd.Parameters.AddWithValue("@mul3FromDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox16.Text);
        //                cmd.Parameters.AddWithValue("@mul3ToDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox18.Text);

        //                //FOURTH
        //                cmd.Parameters.AddWithValue("@mul4From", TextBox27.Text);
        //                cmd.Parameters.AddWithValue("@mul4To", TextBox29.Text);
        //                cmd.Parameters.AddWithValue("@mul4FromDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox28.Text);
        //                cmd.Parameters.AddWithValue("@mul4ToDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox30.Text);

        //                //FIFTH
        //                cmd.Parameters.AddWithValue("@mul5From", TextBox19.Text);
        //                cmd.Parameters.AddWithValue("@mul5To", TextBox21.Text);
        //                cmd.Parameters.AddWithValue("@mul5FromDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox20.Text);
        //                cmd.Parameters.AddWithValue("@mul5ToDate", string.IsNullOrEmpty(TextBox11.Text) ? DBNull.Value : (object)TextBox22.Text);

        //                // Execute the insertion into the route table
        //                int rowsAffected = cmd.ExecuteNonQuery();

        //                if (rowsAffected >= 1)
        //                {
        //                    // Use ClientScript.RegisterStartupScript to display client-side alerts
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Domestic Travel Request Submitted!');", true);
        //                }
        //                else
        //                {
        //                    // Use ClientScript.RegisterStartupScript to display client-side alerts
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('An error occurred for the route details. Please try again.');", true);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or display a user-friendly error message
        //        // Example: Log.Error("An error occurred during travel request enrollment", ex);
        //        // Use ClientScript.RegisterStartupScript to display client-side alerts
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "exception", "alert('An error occurred during route request enrollment. Please try again.');", true);
        //    }
        //}



        //protected void addMoreButton_Click(object sender, EventArgs e)
        //{
        //    //// Create new input fields for the 3rd destination
        //    //Label label36 = new Label();
        //    //label36.ID = "Label36";
        //    //label36.Text = "3rd Destination:";

        //    //Label label37 = new Label();
        //    //label37.ID = "Label37";
        //    //label37.Text = "3. Departing From";

        //    //TextBox textBox15 = new TextBox();
        //    //textBox15.ID = "TextBox15";
        //    //textBox15.Width = 343;
        //    //textBox15.CssClass = "auto-style11";

        //    //RequiredFieldValidator validator29 = new RequiredFieldValidator();
        //    //validator29.ID = "RequiredFieldValidator29";
        //    //validator29.ErrorMessage = "*";
        //    //validator29.CssClass = "required";
        //    //validator29.ControlToValidate = "TextBox15";

        //    //Label label38 = new Label();
        //    //label38.ID = "Label38";
        //    //label38.Text = "Date";
        //    //label38.Style.Add("margin-left", "30px");

        //    //TextBox textBox16 = new TextBox();
        //    //textBox16.ID = "TextBox16";
        //    //textBox16.TextMode = TextBoxMode.Date;
        //    //textBox16.Width = 100;

        //    //RequiredFieldValidator validator30 = new RequiredFieldValidator();
        //    //validator30.ID = "RequiredFieldValidator30";
        //    //validator30.ErrorMessage = "*";
        //    //validator30.CssClass = "required";
        //    //validator30.ControlToValidate = "TextBox16";

        //    //Label label39 = new Label();
        //    //label39.ID = "Label39";
        //    //label39.Text = "Departing To";
        //    //label39.Style.Add("padding-left", "150px");

        //    //TextBox textBox17 = new TextBox();
        //    //textBox17.ID = "TextBox17";
        //    //textBox17.Width = 260;
        //    //textBox17.Style.Add("margin-left", "80px");

        //    //RequiredFieldValidator validator32 = new RequiredFieldValidator();
        //    //validator32.ID = "RequiredFieldValidator32";
        //    //validator32.ErrorMessage = "*";
        //    //validator32.CssClass = "required";
        //    //validator32.ControlToValidate = "TextBox17";

        //    //Label label40 = new Label();
        //    //label40.ID = "Label40";
        //    //label40.Text = "Date";
        //    //label40.Style.Add("margin-left", "30px");

        //    //TextBox textBox18 = new TextBox();
        //    //textBox18.ID = "TextBox18";
        //    //textBox18.TextMode = TextBoxMode.Date;
        //    //textBox18.Width = 100;

        //    //RequiredFieldValidator validator33 = new RequiredFieldValidator();
        //    //validator33.ID = "RequiredFieldValidator33";
        //    //validator33.ErrorMessage = "*";
        //    //validator33.CssClass = "required";
        //    //validator33.ControlToValidate = "TextBox18";

        //    //// Create new input fields for the 4th destination
        //    //Label label51 = new Label();
        //    //label51.ID = "Label51";
        //    //label51.Text = "4th Destination:";

        //    //Label label52 = new Label();
        //    //label52.ID = "Label52";
        //    //label52.Text = "4. Departing From";

        //    //TextBox textBox27 = new TextBox();
        //    //textBox27.ID = "TextBox27";
        //    //textBox27.Width = 343;
        //    //textBox27.CssClass = "auto-style11";

        //    //Label label53 = new Label();
        //    //label53.ID = "Label53";
        //    //label53.Text = "Date";
        //    //label53.Style.Add("margin-left", "30px");

        //    //TextBox textBox28 = new TextBox();
        //    //textBox28.ID = "TextBox28";
        //    //textBox28.TextMode = TextBoxMode.Date;
        //    //textBox28.Width = 100;

        //    //Label label54 = new Label();
        //    //label54.ID = "Label54";
        //    //label54.Text = "Departing To";
        //    //label54.Style.Add("padding-left", "150px");

        //    //TextBox textBox29 = new TextBox();
        //    //textBox29.ID = "TextBox29";
        //    //textBox29.Width = 260;
        //    //textBox29.Style.Add("margin-left", "80px");

        //    //Label label55 = new Label();
        //    //label55.ID = "Label55";
        //    //label55.Text = "Date";
        //    //label55.Style.Add("margin-left", "30px");

        //    //TextBox textBox30 = new TextBox();
        //    //textBox30.ID = "TextBox30";
        //    //textBox30.TextMode = TextBoxMode.Date;
        //    //textBox30.Width = 100;

        //    //// Create new input fields for the 5th destination
        //    //Label label41 = new Label();
        //    //label41.ID = "Label41";
        //    //label41.Text = "5th Destination:";

        //    //Label label42 = new Label();
        //    //label42.ID = "Label42";
        //    //label42.Text = "5. Departing From";

        //    //TextBox textBox19 = new TextBox();
        //    //textBox19.ID = "TextBox19";
        //    //textBox19.Width = 343;
        //    //textBox19.CssClass = "auto-style11";

        //    //Label label43 = new Label();
        //    //label43.ID = "Label43";
        //    //label43.Text = "Date";
        //    //label43.Style.Add("margin-left", "30px");

        //    //TextBox textBox20 = new TextBox();
        //    //textBox20.ID = "TextBox20";
        //    //textBox20.TextMode = TextBoxMode.Date;
        //    //textBox20.Width = 100;

        //    //Label label44 = new Label();
        //    //label44.ID = "Label44";
        //    //label44.Text = "Departing To";
        //    //label44.Style.Add("padding-left", "150px");

        //    //TextBox textBox21 = new TextBox();
        //    //textBox21.ID = "TextBox21";
        //    //textBox21.Width = 260;
        //    //textBox21.Style.Add("margin-left", "80px");

        //    //Label label45 = new Label();
        //    //label45.ID = "Label45";
        //    //label45.Text = "Date";
        //    //label45.Style.Add("margin-left", "30px");

        //    //TextBox textBox22 = new TextBox();
        //    //textBox22.ID = "TextBox22";
        //    //textBox22.TextMode = TextBoxMode.Date;
        //    //textBox22.Width = 100;

        //    //// Add the new input fields to the panel
        //    //Panel destinationsPanel = (Panel)FindControl("destinationsContainer");
        //    //destinationsPanel.Controls.Add(label36);
        //    //destinationsPanel.Controls.Add(new LiteralControl("<br />"));
        //    //destinationsPanel.Controls.Add(label37);
        //    //destinationsPanel.Controls.Add(textBox15);
        //    //destinationsPanel.Controls.Add(validator29);
        //    //destinationsPanel.Controls.Add(label38);
        //    //destinationsPanel.Controls.Add(textBox16);
        //    //destinationsPanel.Controls.Add(validator30);
        //    //destinationsPanel.Controls.Add(new LiteralControl("<br />"));
        //    //destinationsPanel.Controls.Add(label39);
        //    //destinationsPanel.Controls.Add(textBox17);
        //    //destinationsPanel.Controls.Add(validator32);
        //    //destinationsPanel.Controls.Add(label40);
        //    //destinationsPanel.Controls.Add(textBox18);
        //    //destinationsPanel.Controls.Add(validator33);
        //    //destinationsPanel.Controls.Add(label51);
        //    //destinationsPanel.Controls.Add(new LiteralControl("<br />"));
        //    //destinationsPanel.Controls.Add(label52);
        //    //destinationsPanel.Controls.Add(textBox27);
        //    //destinationsPanel.Controls.Add(label53);
        //    //destinationsPanel.Controls.Add(textBox28);
        //    //destinationsPanel.Controls.Add(label54);
        //    //destinationsPanel.Controls.Add(textBox29);
        //    //destinationsPanel.Controls.Add(label55);
        //    //destinationsPanel.Controls.Add(textBox30);
        //    //destinationsPanel.Controls.Add(new LiteralControl("<br />"));
        //    //destinationsPanel.Controls.Add(label41);
        //    //destinationsPanel.Controls.Add(new LiteralControl("<br />"));
        //    //destinationsPanel.Controls.Add(label42);
        //    //destinationsPanel.Controls.Add(textBox19);
        //    //destinationsPanel.Controls.Add(label43);
        //    //destinationsPanel.Controls.Add(textBox20);
        //    //destinationsPanel.Controls.Add(label44);
        //    //destinationsPanel.Controls.Add(textBox21);
        //    //destinationsPanel.Controls.Add(label45);
        //    //destinationsPanel.Controls.Add(textBox22);

        //    //int maxNumberOfDestinations = 5;
        //    //// Find the next hidden input field
        //    //for (int i = 1; i <= maxNumberOfDestinations; i++)
        //    //{
        //    //    Panel destinationPanel = (Panel)FindControl("destination" + i);
        //    //    if (destinationPanel != null && !destinationPanel.Visible)
        //    //    {
        //    //        destinationPanel.Visible = true;
        //    //        break; // Exit the loop after displaying one input field
        //    //    }
        //    //}
        //}
    }
}