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
    public partial class NewDomesticRequest : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

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