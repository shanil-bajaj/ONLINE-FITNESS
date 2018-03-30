using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using DataAccessLayer;
using System.Text;

namespace NDOnlineGym_2017
{
    public partial class ExistingBalancePayment : System.Web.UI.Page
    {
        BalBalancePayment objBalance = new BalBalancePayment();
        DataTable dt = new DataTable();
        BalAddMember objMemberDetails = new BalAddMember();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodaysDate();
                SearchData();
            }
        }
        #region Assign_Date
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDate.Text = todaydate.ToString("dd-MM-yyyy");
                txtToDate.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        #endregion

        protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            if (ddlSearchBy.Text == "--Select--")
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                DateTime todaydate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                {
                    string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                    objBalance.FromDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                DateTime todaydate2;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate2))
                {
                    string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                    objBalance.ToDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                dt = objBalance.Balancedetails();

                if (dt.Rows.Count > 0)
                {
                    gvExistingBalancePayment.DataSource = dt;
                    gvExistingBalancePayment.DataBind();
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvExistingBalancePayment.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvExistingBalancePayment.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvExistingBalancePayment.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvExistingBalancePayment.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvExistingBalancePayment.Columns[0].Visible = false;
                }
            }
            else
            {

            }


        }

        protected void btnfollowup_Command(object sender, CommandEventArgs e)
        {

        }

        protected void btnPreview_Command(object sender, CommandEventArgs e)
        {

        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {

        }
    }
}