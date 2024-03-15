using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Configuration;
using System.Web.UI;

namespace TravelDesk.Employee
{
    public partial class DomesticRequest : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Your Page_Load code, if any
        }


        protected void submitRequestbtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO travelRequest (travelRequestID, travelLocation, travelEmpID, travelFname, travelDesignation, travelLevel, travelVoip, travelMobilenum, travelProjectCode, travelHomeFacility, travelDeparture, travelReturn, travelPurpose, travelApprovalStat, travelManager, travelRemarks, travelDestination, travelOthers)"
                            + "VALUES (@ID, @location, @empID, @empName, @designation, @level, @voip, @mobile, @projCode, @facility, @departure, @return, @purpose, @approvalStat, @manager, @remarks, @destination, @others)";


                        cmd.Parameters.AddWithValue("@ID", "TR" + employeeID.Text + employeeDesignation.Text);
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
                        cmd.Parameters.AddWithValue("@approvalStat", employeeApproval.SelectedItem.Text);                       
                        cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                        cmd.Parameters.AddWithValue("@remarks", employeeRemarks.Text);
                        cmd.Parameters.AddWithValue("@destination", employeeDestination.Text);
                        cmd.Parameters.AddWithValue("@others", employeeOthers.Text);


                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert ('Travel Request Submitted!') </script>");

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
    }
}
