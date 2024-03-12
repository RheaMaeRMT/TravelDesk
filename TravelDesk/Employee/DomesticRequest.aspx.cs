using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace TravelDesk.Employee
{
    public partial class DomesticRequest : System.Web.UI.Page
    {
        // Retrieve the connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["TravelDeskDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Your Page_Load code, if any
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // Read form inputs
                    string location = locationtxtbx.Text;
                    string name = empNametxtbox.Text;
                    string designation = empDesignation.Text;
                    int level = int.Parse(empLeveltxtbox.Text);
                    string voip = empvoiptxtbox.Text;
                    string phone = empNumtxtbox.Text;
                    string projCode = empProjCode.Text;
                    string facility = empFacility.Text;
                    string destination = empDestination.Text;
                    string departure = empDeparture.Text;
                    string returndate = empReturn.Text;
                    string purpose = empPurpose.Text;
                    string remarks = empRemarks.Text;

                    // Create a parameterized SQL query to prevent SQL injection
                    string query = "INSERT INTO Requests(rLocation, rEmpName, rDesignation, rLevel, rVOIPExt, rMobilenum, rProjectCode, " +
                                   "rFacility, rDestination, rDateofDeparture, rDateofReturn, rPurpose, rRemarks)" +
                                   "VALUES(@location, @name, @designation, @level, @voip, @phone, @projCode, " +
                                   "@facility, @destination, @departure, @returndate, @purpose, @remarks)";

                    // Use 'using' statement to ensure proper resource disposal
                    using (var db = new SqlConnection(connectionString))
                    {
                        using (var cmd = new SqlCommand(query, db))
                        {
                            // Add parameters
                            cmd.Parameters.AddWithValue("@location", location);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@designation", designation);
                            cmd.Parameters.AddWithValue("@level", level);
                            cmd.Parameters.AddWithValue("@voip", voip);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.Parameters.AddWithValue("@projCode", projCode);
                            cmd.Parameters.AddWithValue("@facility", facility);
                            cmd.Parameters.AddWithValue("@destination", destination);
                            cmd.Parameters.AddWithValue("@departure", departure);
                            cmd.Parameters.AddWithValue("@returndate", returndate);
                            cmd.Parameters.AddWithValue("@purpose", purpose);
                            cmd.Parameters.AddWithValue("@remarks", remarks);

                            // Open the connection
                            db.Open();

                            // Execute the query
                            int ctr = cmd.ExecuteNonQuery();

                            // Check the result
                            if (ctr == 1)
                            {
                                Response.Write("<script>alert('Domestic Travel Request Submitted');</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('Something went wrong with your input data.');</script>");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    Console.WriteLine($"Error: {ex.Message}");
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }
            }
        }
    }
}
