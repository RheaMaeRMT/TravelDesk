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
    public partial class domesticRequestDetails : System.Web.UI.Page
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
            fieldsNotEditable();
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
        private void fieldsNotEditable()
        {
            employeeLocation.ReadOnly = true;
            employeeID.ReadOnly = true;
            employeeName.ReadOnly = true;
            employeeDesignation.ReadOnly = true;
            employeeLevel.ReadOnly = true;
            employeeVoip.ReadOnly = true;
            employeePhone.ReadOnly = true;
            employeeProjCode.ReadOnly = true;
            employeeFacility.ReadOnly = true;
            employeeDeparture.ReadOnly = true;
            employeeReturn.ReadOnly = true;
            employeePurpose.Enabled = false; // Disable dropdownlist
            employeeDestination.ReadOnly = true;
            employeeOthers.ReadOnly = true;
            employeeApproval.Enabled = false; // Disable dropdownlist
            employeeManager.ReadOnly = true;
            employeeRemarks.ReadOnly = true;

        }


        protected void updateRequest_Click(object sender, EventArgs e)
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
                                    // GET THE VALUE OF THE STATUS
                                    string approvalStatus = reader["travelApprovalStat"].ToString();
                                    if (approvalStatus == "auto-approved" || approvalStatus == "processing" || approvalStatus == "completed")
                                    {
                                        Response.Write("<script>alert('Update Unavailable: This travel request is already " + approvalStatus + " !');</script>");
                                    }
                                    else if (approvalStatus == "pending")
                                    {
                                        Response.Redirect("modifyDomesticRequests.aspx");

                                    }

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
    }
}