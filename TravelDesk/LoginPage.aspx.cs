using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk
{
    public partial class LoginPage : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {


            

        }

        protected void signinBtn_Click(object sender, EventArgs e)
        {

            //credential from user input
            string email = txtEmail.Text;
            string pass = txtPassword.Text;

            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT COUNT (*) FROM users WHERE userEmail = @Email AND userPassword = @Password";
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", pass);

                        // Execute the query to get the count of matching users
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // User credentials exist in the database
                            // Retrieve user information
                            cmd.CommandText = "SELECT userID, userRole, userName, userComID FROM users WHERE userEmail = @Email AND userPassword = @Password";
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string userID = reader["userID"].ToString();
                                    string userRole = reader["userRole"].ToString();
                                    string userName = reader["userName"].ToString();
                                    string userComID = reader["userComID"].ToString();

                                    // Set session variables
                                    Session["userID"] = userID;
                                    Session["userRole"] = userRole;
                                    Session["userName"] = userName;
                                    Session["userComID"] = userComID;

                                    // Redirect to appropriate dashboard based on user role
                                    if (userRole == "Approver")
                                    {
                                        Response.Write("<script>alert ('Login Successfull! Redirecting to Approver Dashboard...'); window.location.href = '/Approver/ApproverDashboard.aspx'; </script>");
                                    }
                                    else if (userRole == "Employee")
                                    {
                                        Response.Write("<script>alert ('Login Successfull! Redirecting to Employee Dashboard'); window.location.href = '/Employee/EmployeeDashboard.aspx'; </script>");
                                    } else if (userRole == "Admin")
                                    {
                                        Response.Write("<script>alert ('Login Successfull! Redirecting to Admin Dashboard'); window.location.href = '/Admin/AdminDashboard.aspx'; </script>");
                                    }
                                }
                            }
                        }
                        else
                        {
                            // No matching user found
                            //AdminAccountVerify();
                            Response.Write("<script>alert ('Account not Found! Please double check your credentials'); window.location.href = 'LoginPage.aspx'; </script>");

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

        //private void AdminAccountVerify()
        //{
        //    //default credentials for ADMIN
        //    string defaultEmail = "defaultEmail@gmail.com";
        //    string defaultPass = "defaultPasswordAdmin";
        //    //credential from user input
        //    string email = txtEmail.Text;
        //    string pass = txtPassword.Text;


        //    if (defaultEmail == email && defaultPass == pass)
        //    {
        //        Session["email"] = defaultEmail;
        //        Session["role"] = "Admin";

        //        string userEmail = (string)Session["email"];
        //        string userRole = (string)Session["role"];
        //        string userID = GenerateUserID();
        //        string userName = "Miss Cheryll";
        //        string companyID = "SampleID";

        //        using (var db = new SqlConnection(connectionString))
        //        {
        //            db.Open();
        //            using (var cmd = db.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = "INSERT INTO users (userID, userRole, userEmail, userName,userComID, userPassword)"
        //                                    + "VALUES("
        //                                    + "@ID,"
        //                                    + "@role,"
        //                                    + "@email,"
        //                                    + "@name,"
        //                                    + "@companyID,"
        //                                    + "@password)";

        //                cmd.Parameters.AddWithValue("@ID", userID);
        //                cmd.Parameters.AddWithValue("@role", userRole);
        //                cmd.Parameters.AddWithValue("@email", userEmail);
        //                cmd.Parameters.AddWithValue("@name", userName);
        //                cmd.Parameters.AddWithValue("@companyID", companyID);
        //                cmd.Parameters.AddWithValue("@password", userID);


        //                var ctr = cmd.ExecuteNonQuery();

        //                if (ctr >= 1)
        //                {
        //                    Response.Write("<script>alert ('Login Successfull!'); window.location.href = '/Admin/AdminDashboard.aspx'; </script>");

        //                }
        //            }
        //        }


        //    }

        //}
        //private string GenerateUserID()
        //{
        //    Random ranID = new Random();
        //    int random = ranID.Next(100000, 999999);
        //    return "TD" + random + "ADM";

        //}
    }
}