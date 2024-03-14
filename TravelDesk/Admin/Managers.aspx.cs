using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Admin
{
    public partial class Managers : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void addBtn_Click(object sender, EventArgs e)
        {

            Random ranID = new Random();
            int random = ranID.Next(100000, 999999);

            string userID = "TD" + random + "ARV";

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
                        cmd.Parameters.AddWithValue("@role", "Approver");
                        cmd.Parameters.AddWithValue("@email", approverEmail.Text);
                        cmd.Parameters.AddWithValue("@DU", approverDU.Text);
                        cmd.Parameters.AddWithValue("@phone", approverPhone.Text);
                        cmd.Parameters.AddWithValue("@level", approverLevel.Text);
                        cmd.Parameters.AddWithValue("@manager", approverManager.Text);
                        cmd.Parameters.AddWithValue("@name", approverName.Text);
                        cmd.Parameters.AddWithValue("@companyID", approverCompanyID.Text);
                        cmd.Parameters.AddWithValue("@password", approverCompanyID.Text);


                        var ctr = cmd.ExecuteNonQuery();

                        if (ctr >= 1)
                        {
                            // JavaScript to display the success modal and populate employee email and company ID
                            string script = @"
                                    <script>
                                        // Display the success modal after a successful operation
                                        $('#successModal').modal('show');
                                        // Populate the employee email and company ID in the modal
                                        $('#employeeEmaildone').text('" + approverEmail.Text + @"');
                                        $('#employeeCompanyIDdone').text('" + approverCompanyID.Text + @"');
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
            approverEmail.Text = string.Empty;
            approverDU.Text = string.Empty;
            approverPhone.Text = string.Empty;
            approverLevel.Text = string.Empty;
            approverManager.Text = string.Empty;
            approverName.Text = string.Empty;
            approverCompanyID.Text = string.Empty;
        }



    }
}