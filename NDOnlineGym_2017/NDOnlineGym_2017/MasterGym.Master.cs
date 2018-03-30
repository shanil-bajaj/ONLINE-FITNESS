using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;

namespace NDOnlineGym_2017
{
    public partial class MasterGym : System.Web.UI.MasterPage
    {
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["OnlineGym"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                dt = obBalBranchInformation.SELECT_CompanyDetails();
                if (dt.Rows.Count > 0)
                {
                    lblCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                    imgCompanyLogo.ImageUrl = dt.Rows[0]["CompanyLogoPath"].ToString();
                    lblName.Text = Request.Cookies["OnlineGym"]["Name"].ToString();
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    lnkBtnDashboard.Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    lnkBtnDashboard.Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    lnkBtnDashboard.Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    lnkBtnDashboard.Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    lnkBtnDashboard.Visible = false;
                }
            }
        }

        protected void lnkBtnLogout_Click(object sender, EventArgs e)
        {
            Session["Branch_ID"] = null;
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void lnkBtnDashboard_Click(object sender, EventArgs e)
        {
            int flag = 1;
            Response.Redirect("Dashboard.aspx?Flag=" + HttpUtility.UrlEncode(flag.ToString()));
        }
    }
}