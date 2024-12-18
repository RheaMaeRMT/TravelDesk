﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelDesk
{
    public partial class SiteMaster : MasterPage
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DB_TravelDesk"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session["userID"] == null && Session["userName"] == null)
            {
                Response.Write("<script>window.location.href = '../LoginPage.aspx'; </script>");

            }
            else if (Session["userID"] != null && (Session["userName"] != null))
            {
                string userName = (string)Session["userName"];
                string role = (string)Session["userRole"];
                
                if (role == "Admin")
                {
                    lblUserName.Text = userName;

                }
                else
                {
                    Response.Write("<script>window.location.href = '../LoginPage.aspx'; </script>");


                }

            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Write("<script>window.location.href = '../LoginPage.aspx'; </script>");

        }
    }
}