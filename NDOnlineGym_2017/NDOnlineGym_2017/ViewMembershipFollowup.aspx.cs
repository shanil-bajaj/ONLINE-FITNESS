using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NDOnlineGym_2017
{
    public partial class ViewMembershipFollowup : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalViewBalancePaymentFollowup obBalViewBalancePaymentFollowup = new BalViewBalancePaymentFollowup();
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodaysDate();
                GridBind();
            }
        }

        #region ------------ Assign All Date ------------------
        protected void AssignTodaysDate()
        {
            Label1.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Label1.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);
            txtToDate.Text = dtLast.ToString("dd-MM-yyyy");

            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
        }
        #endregion

        #region-------------------------------Membership Followup----------------------------------
        public void GridBind()
        {
            try
            {
                obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                obBalViewBalancePaymentFollowup.Category = "All";
                obBalViewBalancePaymentFollowup.Action = "Search";
                if (ddlDateCategory.SelectedValue == "--Select--")
                    obBalViewBalancePaymentFollowup.DateCategory = "Reg Date";
                else if (ddlDateCategory.SelectedValue == "Followup Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "Followup Date";
                else if (ddlDateCategory.SelectedValue == "Next Followup Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";
                dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();

                if (ddlDateCategory.SelectedValue == "--Select--")
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvMemshipFoll.Visible = false;
                        gvMemberDetails.Visible = true;
                        gvMemberDetails.DataSource = dt;
                        gvMemberDetails.DataBind();
                    }
                    else
                    {
                        gvMemshipFoll.Visible = false;
                        gvMemberDetails.Visible = false;
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvMemberDetails.Visible = false;
                        gvMemshipFoll.Visible = true;
                        gvMemshipFoll.DataSource = dt;
                        gvMemshipFoll.DataBind();
                    }
                    else
                    {
                        gvMemberDetails.Visible = false;
                        gvMemshipFoll.Visible = false;
                    }
                }
                //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                //{
                //    gvMemshipFoll.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                //{
                //    gvMemshipFoll.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                //{
                //    gvMemshipFoll.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                //{
                //    gvMemshipFoll.Columns[1].Visible = false;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                //{
                //    gvMemshipFoll.Columns[1].Visible = false;
                //}
            }
            catch (Exception ex)
            {

            }

        }
        
        public void SearchByDateCategory()
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategory.SelectedValue;
            if (ddlDateCategory.SelectedValue == "--Select--")
                obBalViewBalancePaymentFollowup.DateCategory = "Followup Date";
            else if (ddlDateCategory.SelectedValue == "Followup Date")
                obBalViewBalancePaymentFollowup.DateCategory = "Followup Date";
            else if (ddlDateCategory.SelectedValue == "Next Followup Date")
                obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

            if (ddlCategory.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlCategory.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlCategory.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlCategory.SelectedValue == "Followup Type")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            flag = 1;
            dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
            if (dt.Rows.Count > 0)
            {
                gvMemberDetails.Visible = false;
                gvMemshipFoll.Visible = true;
                gvMemshipFoll.DataSource = dt;
                gvMemshipFoll.DataBind();
            }
            else
            {
                gvMemberDetails.Visible = false;
                gvMemshipFoll.Visible = true;
                gvMemshipFoll.DataSource = null;
                gvMemshipFoll.DataBind();
            }
        }

        public void SearchByCategory()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategory.SelectedValue;
            if (ddlDateCategory.SelectedValue == "--Select--")
                obBalViewBalancePaymentFollowup.DateCategory = "Followup Date";
            else if (ddlDateCategory.SelectedValue == "Followup Date")
                obBalViewBalancePaymentFollowup.DateCategory = "Followup Date";
            else if (ddlDateCategory.SelectedValue == "Next Followup Date")
                obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

            if (ddlCategory.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlCategory.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlCategory.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlCategory.SelectedValue == "Followup Type")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            flag = 2;
            dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
            if (dt.Rows.Count > 0)
            {
                gvMemberDetails.Visible = false;
                gvMemshipFoll.Visible = true;
                gvMemshipFoll.DataSource = dt;
                gvMemshipFoll.DataBind();
            }
            else
            {
                gvMemberDetails.Visible = false;
                gvMemshipFoll.Visible = true;
                gvMemshipFoll.DataSource = null;
                gvMemshipFoll.DataBind();
            }
        }

        protected void gvMemshipFoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvMemshipFoll.PageIndex = e.NewPageIndex;
                SearchByDateCategory();
            }
            else if (flag == 2)
            {
                gvMemshipFoll.PageIndex = e.NewPageIndex;
                SearchByCategory();
            }
            else
            {
                gvMemshipFoll.PageIndex = e.NewPageIndex;
                GridBind();
            }

        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            GridBind();
        }

        protected void btnSearchByDateCategory_Click(object sender, EventArgs e)
        {
            SearchByDateCategory();
        }

        public void Clear()
        {
            AssignTodaysDate();
            ddlCategory.SelectedValue = "--Select--";
            txtSearch.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            gvMemshipFoll.DataSource = null;
            gvMemshipFoll.DataBind();
            gvMemberDetails.DataSource = null;
            gvMemshipFoll.DataBind();
            txtFromDate.Focus();
        }

        string FNMemberFollDetail = "FNMemberFollDetail";
        protected void btnFollowup_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Followup.aspx?Other_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNMemberFollDetail=" + HttpUtility.UrlEncode(FNMemberFollDetail.ToString()));
        }

        protected void btnName_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchByCategory();
            btnSearch.Focus();
        }

        protected void ddlDateCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            GridBind();
            ddlCategory.Focus();
        }

        #endregion

        protected void btnExistingFollowup_Click(object sender, EventArgs e)
        {
            obBalViewBalancePaymentFollowup.FollowupType = "Other";
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = obBalViewBalancePaymentFollowup.SelectFollDetails_By_FollowupType();
            if (dt.Rows.Count > 0)
            {
                gvMemberDetails.Visible = false;
                gvMemshipFoll.Visible = true;
                gvMemshipFoll.DataSource = dt;
                gvMemshipFoll.DataBind();
            }
            else
            {
                gvMemberDetails.Visible = false;
                gvMemshipFoll.Visible = true;
                gvMemshipFoll.DataSource = null;
                gvMemshipFoll.DataBind();
            }
        }
    }
}