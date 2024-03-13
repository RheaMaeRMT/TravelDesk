using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        //protected void submitBtn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Read form inputs
        //        string location = locationtxtbx.Text;
        //        string name = empNametxtbox.Text;
        //        string designation = empDesignation.Text;
        //        int empID, level;
        //        if (!int.TryParse(empIDtxtbox.Text, out empID) || !int.TryParse(empLeveltxtbox.Text, out level))
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid input for employee ID or level.');", true);
        //            return;
        //        }

        //        string voip = empvoiptxtbox.Text;
        //        string phone = empNumtxtbox.Text;
        //        string projCode = empProjCode.Text;
        //        string facility = empFacility.Text;
        //        string destination = empDestination.Text;
        //        string departure = empDeparture.Text;
        //        string returndate = empReturn.Text;
        //        string purpose = empPurpose.Text;
        //        string proof = DropDownList1.SelectedItem.Text;
        //        string manager = empManagername.Text;
        //        string others = empOthers.Text;
        //        string remarks = empRemarks.Text;
        //        string attachment = empAttachment.HasFile ? "uploaded" : "none"; //check if an attachment is uploaded
        //        string approvalStat = (attachment == "uploaded") ? "auto-approved" : "pending";



        //        // Generate unique travel request ID
        //        string travelID = $"TR{empID}{Guid.NewGuid().ToString("N").Substring(0, 8)}";

        //        // Validate date inputs
        //        if (!DateTime.TryParse(departure, out _) || !DateTime.TryParse(returndate, out _))
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid date format for departure or return date.');", true);
        //            return;
        //        }

        //        using (var db = new SqlConnection(connectionString))
        //        {
        //            db.Open();
        //            using (var cmd = db.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.Text;

        //                cmd.CommandText = "INSERT INTO travelRequest (travelRequestID,travelLocation,travelEmpID,travelFname, travelDesignation, travelLevel, " +
        //                    "travelVoip, traveMobilenum, travelProjectCode, travelHomeFacility, travelDepature, travelReturn, travelPurpose, travelApprovalStat, " +
        //                    "travelProof, travelManager, travelRemarks, travelDestination, travelOthers)"
        //                                    + "VALUES("
        //                                    + "@RequestID,"
        //                                    + "@Location,"
        //                                    + "@EmpID,"
        //                                    + "@Fname,"
        //                                    + "@Designation,"
        //                                    + "@Level," 
        //                                    + "@Voip,"
        //                                    + "@Mobilenum,"
        //                                    + "@ProjectCode,"
        //                                    + "@HomeFacility,"
        //                                    + "@Depature,"
        //                                    + "@Return," 
        //                                    + "@Purpose,"
        //                                    + "@ApprovalStat,"
        //                                    + "@Proof,"
        //                                    + "@Manager," 
        //                                    + "@Remarks,"
        //                                    + "@Destination,"
        //                                    + "@Others)";

        //                // Add parameters
        //                cmd.Parameters.AddWithValue("@RequestID", travelID);
        //                cmd.Parameters.AddWithValue("@Location", locationtxtbx.Text);
        //                cmd.Parameters.AddWithValue("@EmpID", empIDtxtbox.Text);
        //                cmd.Parameters.AddWithValue("@Fname", empNametxtbox.Text);
        //                cmd.Parameters.AddWithValue("@Designation", empDesignation.Text);
        //                cmd.Parameters.AddWithValue("@Level", empLeveltxtbox.Text);
        //                cmd.Parameters.AddWithValue("@Voip", empvoiptxtbox.Text);
        //                cmd.Parameters.AddWithValue("@Mobilenum", empNumtxtbox.Text);
        //                cmd.Parameters.AddWithValue("@ProjectCode", empProjCode.Text);
        //                cmd.Parameters.AddWithValue("@HomeFacility", empFacility.Text);
        //                cmd.Parameters.AddWithValue("@Depature", empDeparture.Text);
        //                cmd.Parameters.AddWithValue("@Return", empReturn.Text);
        //                cmd.Parameters.AddWithValue("@Purpose", empPurpose.Text);
        //                cmd.Parameters.AddWithValue("@ApprovalStat", approvalStat);
        //                cmd.Parameters.AddWithValue("@Proof", DropDownList1.SelectedItem.Text);
        //                cmd.Parameters.AddWithValue("@Manager", empManagername.Text);
        //                cmd.Parameters.AddWithValue("@Remarks", empRemarks.Text);
        //                cmd.Parameters.AddWithValue("@Destination", empDestination.Text);
        //                cmd.Parameters.AddWithValue("@Others", empOthers.Text);

        //                var ctr = cmd.ExecuteNonQuery();

        //                if (ctr >= 1)
        //                {
        //                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Domestic Travel Request Submitted');", true);

        //                }
        //                else
        //                {
        //                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Something went wrong with your input data.');", true);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception for debugging
        //        Console.WriteLine($"Error: {ex.Message}");
        //        // Log the exception using a logging framework
        //        // Log.Error($"Error: {ex.Message}", ex);
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('! Error: {ex.Message}');", true);
        //    }
        //}

        protected void enrollBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO employees (employeeID,employeeName,employeeDU,employeePhone)"
                                            + "VALUES("
                                            + "@ID,"
                                            + "@name,"
                                            + "@du,"
                                            + "@phone)";

                        cmd.Parameters.AddWithValue("@ID", employeeID.Text);
                        cmd.Parameters.AddWithValue("@name", employeeName.Text);
                        cmd.Parameters.AddWithValue("@du", employeeDU.Text);
                        cmd.Parameters.AddWithValue("@phone", employeePhone.Text);

                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert ('New Approver Successfully Added!') </script>");

                        }
                    }
                }



            }
            catch
            {

            }

        }
    }
}
