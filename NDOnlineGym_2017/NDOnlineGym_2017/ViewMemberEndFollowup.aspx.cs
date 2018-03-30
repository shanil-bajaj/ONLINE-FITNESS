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
    public partial class ViewMemberEndFollowup : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalViewBalancePaymentFollowup obBalViewBalancePaymentFollowup = new BalViewBalancePaymentFollowup();
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodaysDate();
                bindDDLPackage();
                GridBind();
            }
        }

        #region ---------- Assign Company ID and Branch ID -------------------
        private void AssignID()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

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

        #region ---------------------- Bind Package DropDown List-----------------
        public void bindDDLPackage()
        {
            try
            {
                AssignID();
                dt = obBalViewBalancePaymentFollowup.BindDDLPackage();
                if (dt.Rows.Count > 0)
                {
                    ddlPackage.DataSource = dt;
                    ddlPackage.Items.Clear();
                    ddlPackage.DataValueField = "Pack_AutoID";
                    ddlPackage.DataTextField = "Package";
                    ddlPackage.DataBind();
                    ddlPackage.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlPackage.SelectedItem.Value = "--Select--";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Package !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void GridBind()
        {
            try
            {
                AssignID();

                if (ddlDateCategory.SelectedValue == "--Select--")
                    obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                else if (ddlDateCategory.SelectedValue == "End Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                else if (ddlDateCategory.SelectedValue == "Next Followup Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

                if(ddlPackage.SelectedValue != "--Select--")
                obBalViewBalancePaymentFollowup.Pack_AutoID = Convert.ToInt32(ddlPackage.SelectedValue);

                obBalViewBalancePaymentFollowup.Action = "Search";
                dt = obBalViewBalancePaymentFollowup.SearchMemEndFollowup();

                lblCount.Text = Convert.ToString(dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {
                    gvMemEndDetails.Visible = true;
                    gvMemEndDetails.DataSource = dt;
                    gvMemEndDetails.DataBind();
                }
                else
                {
                    gvMemEndDetails.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available !!!','Information');", true);
                    return;
                }
                //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                //{
                //    gvMemEndDetails.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                //{
                //    gvMemEndDetails.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                //{
                //    gvMemEndDetails.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                //{
                //    gvMemEndDetails.Columns[1].Visible = false;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                //{
                //    gvMemEndDetails.Columns[1].Visible = false;
                //}
            }
            catch (Exception ex)
            {

            }

        }

        #region ------------ Search by Date and Category --------------
        public void SearchByDateCategory()
        {
            if (ddlDateCategory.SelectedValue == "--Select--" && ddlPackage.Text == "")
            {
                if (ddlDateCategory.SelectedValue != "--Select--")
                    ddlDateCategory.Focus();
                else if (ddlPackage.SelectedValue != "--Select--")
                    ddlPackage.Focus();

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                return;
            }
            else
            {
                //DateTime Fromdate;
                //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                //{ }
                //obBalViewBalancePaymentFollowup.FromDate = Fromdate;
                //DateTime Todate;
                //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                //{ }
                //obBalViewBalancePaymentFollowup.ToDate = Todate;

                chkdate = chkFromDateNotLessToDate();

                if (chkdate == 0)
                {
                    AssignID();
                    obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                    if (ddlDateCategory.SelectedValue == "--Select--")
                        obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                    else if (ddlDateCategory.SelectedValue == "End Date")
                        obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                    else if (ddlDateCategory.SelectedValue == "Next Followup Date")
                        obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

                    if (ddlPackage.SelectedValue != "--Select--")
                        obBalViewBalancePaymentFollowup.Pack_AutoID = Convert.ToInt32(ddlPackage.SelectedValue);

                    flag = 1;
                    dt = obBalViewBalancePaymentFollowup.SearchMemEndFollowup();
                    lblCount.Text = Convert.ToString(dt.Rows.Count);
                    if (dt.Rows.Count > 0)
                    {
                        gvMemEndDetails.Visible = true;
                        gvMemEndDetails.DataSource = dt;
                        gvMemEndDetails.DataBind();
                    }
                    else
                    {
                        gvMemEndDetails.Visible = true;
                        gvMemEndDetails.DataSource = null;
                        gvMemEndDetails.DataBind();
                    }
                }
            }
        }
        #endregion

        #region ---------------- Search by Category --------------
        public void SearchByCategory()
        {
            AssignID();
            obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            if (ddlDateCategory.SelectedValue == "--Select--")
                obBalViewBalancePaymentFollowup.DateCategory = "End Date";
            else if (ddlDateCategory.SelectedValue == "End Date")
                obBalViewBalancePaymentFollowup.DateCategory = "End Date";
            else if (ddlDateCategory.SelectedValue == "Next Followup Date")
                obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

            if (ddlPackage.SelectedValue != "--Select--")
            obBalViewBalancePaymentFollowup.Pack_AutoID = Convert.ToInt32(ddlPackage.SelectedValue);

            flag = 2;
            dt = obBalViewBalancePaymentFollowup.SearchMemEndFollowup();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvMemEndDetails.Visible = true;
                gvMemEndDetails.DataSource = dt;
                gvMemEndDetails.DataBind();
            }
            else
            {
                gvMemEndDetails.Visible = true;
                gvMemEndDetails.DataSource = null;
                gvMemEndDetails.DataBind();
            }
        }
        #endregion

        protected void gvMemEndDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            chkdate = chkFromDateNotLessToDate();

            if (chkdate == 0)
            {
                if (flag == 1)
                {
                    gvMemEndDetails.PageIndex = e.NewPageIndex;
                    SearchByDateCategory();
                }
                else if (flag == 2)
                {
                    gvMemEndDetails.PageIndex = e.NewPageIndex;
                    SearchByCategory();
                }
                else
                {
                    gvMemEndDetails.PageIndex = e.NewPageIndex;
                    GridBind();
                }
            }
        }

        #region ------------- Search by Date Event -----------------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            chkdate = chkFromDateNotLessToDate();

            if (chkdate == 0)
            {
                GridBind();
                btnSearch.Focus();
            }
        }
        #endregion

        #region ----------------- Search by Date and Category  Event ----------------
        protected void btnSearchByDateCategory_Click(object sender, EventArgs e)
        {            
            if (ddlDateCategory.SelectedValue != "--Select--" && ddlPackage.SelectedValue != "--Select--")
            {
                SearchByDateCategory();
                btnSearchByDateCategory.Focus();
            }
            else
            {
                if (ddlDateCategory.SelectedValue == "--Select--")
                    ddlDateCategory.Focus();
                else if (ddlPackage.SelectedValue == "--Select--")
                    ddlPackage.Focus();

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                return;
            }
           
        }

        #endregion
        
        #region -------------------- Clear Button Event --------------------
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            //gvMemberEndFoll.DataSource = null;
            //gvMemberEndFoll.DataBind();
            //gvMemEndDetails.DataSource = null;
            //gvMemEndDetails.DataBind();

            GridBind();
            txtFromDate.Focus();
        }

        public void Clear()
        {
            AssignTodaysDate();
            ddlDateCategory.SelectedValue = "--Select--";
            ddlPackage.SelectedValue = "--Select--";
        }
        #endregion
        
        #region

        //string FNMemEndFollDetail = "FNMemEndFollDetail";
        //protected void btnFollowup_Command(object sender, CommandEventArgs e)
        //{
        //    int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
        //    Response.Redirect("Followup.aspx?MembershipEnd_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNMemEndFollDetail=" + HttpUtility.UrlEncode(FNMemEndFollDetail.ToString()));
        //}


        //protected void btnName_Command(object sender, CommandEventArgs e)
        //{
        //    int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
        //    Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        //}

        //protected void btnExistingFollowup_Click(object sender, EventArgs e)
        //{
        //    MembershipEndFoll();
        //}

        //protected void btnMemEndName_Command(object sender, CommandEventArgs e)
        //{
        //    int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
        //    Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        //}

        //protected void btnMemEndFollowup_Command(object sender, CommandEventArgs e)
        //{
        //    int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
        //    Response.Redirect("Followup.aspx?MembershipEnd_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNMemEndFollDetail=" + HttpUtility.UrlEncode(FNMemEndFollDetail.ToString()));
        //}
        #endregion

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
            ddlDateCategory.Focus();
        }

        protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchByDateCategory();
            ddlPackage.Focus();
        }
       
        public void MembershipEndFoll()
        {
            obBalViewBalancePaymentFollowup.FollowupType = "Membership End";
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;

            AssignID();
            dt = obBalViewBalancePaymentFollowup.SelectFollDetails_By_FollowupType();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvMemEndDetails.Visible = false;
                gvMemberEndFoll.Visible = true;
                gvMemberEndFoll.DataSource = dt;
                gvMemberEndFoll.DataBind();
            }
            else
            {
                gvMemEndDetails.Visible = false;
                gvMemberEndFoll.Visible = true;
                gvMemberEndFoll.DataSource = null;
                gvMemberEndFoll.DataBind();
            }
        }

        protected void gvMemberEndFoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            chkdate = chkFromDateNotLessToDate();
            if (chkdate == 0)
            {
                gvMemEndDetails.PageIndex = e.NewPageIndex;
                MembershipEndFoll();
            }
        }       

        #region ------------- Check From Date And To Date Validation
        int chkdate = 0;
        protected int chkFromDateNotLessToDate()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                chkdate = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                chkdate = 1;
                txtToDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter To Date !!!','Information');", true);
            }
            else
            {

                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FromDate))
                {
                }

                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ToDate))
                {
                }

                if (FromDate.Date <= ToDate.Date)
                {
                    chkdate = 0;
                    obBalViewBalancePaymentFollowup.FromDate = FromDate;
                    obBalViewBalancePaymentFollowup.ToDate = ToDate;
                }
                else
                {
                    chkdate = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                }
            }

            return chkdate;

        }
        #endregion

    }
}