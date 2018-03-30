using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using DataAccessLayer;
using System.Globalization;

namespace NDOnlineGym_2017
{
    public partial class POSMaster : System.Web.UI.MasterPage
    {
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetBranchDetails();
            }
        }

        public void GetBranchDetails()
        {
            if (Request.QueryString["Branch_AutoID"] != null)
                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.QueryString["Branch_AutoID"]);
            else if (Request.Cookies["OnlineGym1"]["brIDHome"] != null)
                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);

            dt = obBalBranchInformation.SelectByID_BranchInformation();
            if (dt.Rows.Count > 0)
            {
                lblCompanyName.Text = dt.Rows[0]["BranchName"].ToString();
                imgCompanyLogo.ImageUrl = dt.Rows[0]["BranchLogoPath"].ToString();
                lblName.Text = Request.Cookies["OnlineGym"]["Name"].ToString();
            }
        }
    }
}