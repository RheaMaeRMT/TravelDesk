using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO route (routeID, routeTravelID, routeOFrom, routeOTo, routeR1From, routeR1To, routeR2From, routeR2To, routeM1From, routeM1To, routeM1FromDate, routeM1ToDate, routeM2From, routeM2To, routeM2FromDate, routeM2ToDate, routeM3From, routeM3To, routeM3FromDate, routeM3ToDate,  routeM4From, routeM4To, routeM4FromDate, routeM4ToDate,  routeM5From, routeM5To,  routeM5FromDate, routeM5ToDate )"
                                + "VALUES (@ID, @travelID, @onewayFrom, @onewayTo, @round1From, @round1To, @round2From, @round2To, @m1from, @m1to, @m1fromDate, @m1ToDate,  @m2from, @m2to, @m2fromDate, @m2ToDate,  @m3from, @m3to, @m3fromDate, @m3ToDate,  @m4from, @m4to, @m4fromDate, @m4ToDate,  @m5from, @m5to, @m5fromDate, @m5ToDate )";

                            // Execute the insertion into anotherTable
                            ctr = cmd.ExecuteNonQuery();

                            if (ctr >= 1)
                            {
                                Response.Write("<script>alert ('Travel Request Submitted!') </script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('An error occurred. Please try again.')</script>");

                            }

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