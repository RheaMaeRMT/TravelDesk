using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Approver
{
    public partial class ApproverManageEmployees : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void enrollBtn_Click(object sender, EventArgs e)
        {
            Random ranID = new Random();
            int random = ranID.Next(100000, 999999);

            string userID = "TD" + random + "E";

            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO users (userID, userRole, userEmail,userDU, userPhone, userLevel, userManager, userName,userComID, userPassword)"
                                            + "VALUES("
                                            + "@ID,"
                                            + "@role,"
                                            + "@email,"
                                            + "@DU,"
                                            + "@phone,"
                                            + "@level,"
                                            + "@manager,"
                                            + "@name,"
                                            + "@companyID,"
                                            + "@password)";

                        cmd.Parameters.AddWithValue("@ID", userID);
                        cmd.Parameters.AddWithValue("@role", "Employee");
                        cmd.Parameters.AddWithValue("@email", employeeEmail.Text);
                        cmd.Parameters.AddWithValue("@DU", employeeDU.Text);
                        cmd.Parameters.AddWithValue("@phone", employeePhone.Text);
                        cmd.Parameters.AddWithValue("@level", employeeLevel.Text);
                        cmd.Parameters.AddWithValue("@manager", employeeManager.Text);
                        cmd.Parameters.AddWithValue("@name", employeeName.Text);
                        cmd.Parameters.AddWithValue("@companyID", employeeCompanyID.Text);
                        cmd.Parameters.AddWithValue("@password", employeeCompanyID.Text);

                        var ctr = cmd.ExecuteNonQuery();
                        if (ctr >= 1)
                        {
                            // JavaScript to display the success modal and populate employee email and company ID
                            string script = @"
                                    <script>
                                        // Display the success modal after a successful operation
                                        $('#successModal').modal('show');
                                        // Populate the employee email and company ID in the modal
                                        $('#employeeEmaildone').text('" + employeeEmail.Text + @"');
                                        $('#employeeCompanyIDdone').text('" + employeeCompanyID.Text + @"');
                                        // Hide the modal when the page is reloaded
                                        $(window).on('beforeunload', function(){
                                            $('#successModal').modal('hide');
                                        });
                                    </script>";
                            // Register the JavaScript to be executed on the client side
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", script);

                            // Optionally, you can also clear the form inputs
                            ClearScreen();
                        }

                    }
                }



            }
            catch (SqlException ex)
            {
                // Log the exception or display a user-friendly error message
                // Example: Log.Error("An error occurred during travel request enrollment", ex);
                Response.Write("<script>alert('An error occurred. Please try again.')</script>");
                // Log additional information from the SQL exception
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Response.Write("<script>alert('SQL Error " + i + ": " + ex.Errors[i].Number + " - " + ex.Errors[i].Message + "')</script>");
                }
            }

        }
        private void ClearScreen()
        {
            employeeEmail.Text = string.Empty;
            employeeDU.Text = string.Empty;
            employeePhone.Text = string.Empty;
            employeeLevel.Text = string.Empty;
            employeeManager.Text = string.Empty;
            employeeName.Text = string.Empty;
            employeeCompanyID.Text = string.Empty;
        }

    }
}