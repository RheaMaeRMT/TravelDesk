using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk.Employee
{
    public partial class EmployeeRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewRequests_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href = 'ListofRequests.aspx'; </script>");

        }
    }
}