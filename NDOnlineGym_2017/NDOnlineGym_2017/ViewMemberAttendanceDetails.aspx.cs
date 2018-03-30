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
    public partial class ViewMemberAttendanceDetails : System.Web.UI.Page
    {
        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();
        DataTable dt = new DataTable();
        BalMemberNumericAttendance objBalMemberNumericAttendance = new BalMemberNumericAttendance();
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodaysDate();
                bindDDLMemberName();
                GridBindPageLoad();
                if (Request.QueryString["MemberId"] != null)
                {
                    objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberId"]);
                    dt = objBalMemberNumericAttendance.SELECT_Member_ByAutoID();
                    if (dt.Rows.Count > 0)
                    {
                        txtID.Text = dt.Rows[0]["Member_ID1"].ToString();
                    }
                    objBalMemberNumericAttendance.Action = "SearchByCategory";
                    Get_MemberAutoID_ByMemberID1();
                    
                    GridBind();
                    flag = 4;

                }
            }
        }

        public void bindDDLMemberName()
        {
            try
            {

                objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                dt = objBalMemberNumericAttendance.SELECT_AllMembers();
                if (dt.Rows.Count > 0)
                {
                    ddlName.DataSource = dt;
                    ddlName.Items.Clear();
                    ddlName.DataValueField = "Member_AutoID";
                    ddlName.DataTextField = "Name";
                    ddlName.DataBind();
                    ddlName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlName.SelectedItem.Value = "--Select--";
                }
                else
                {
                    ddlName.DataSource = dt;
                    ddlName.DataBind();
                    ddlName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    //ddlName.SelectedValue = "--Select--";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Records Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region ------------ Assign All Date ------------------
        protected void AssignTodaysDate()
        { DateTime todaydate;
        if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
        { }
            Label1.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = todaydate;
            DateTime dtLast = todaydate;

            txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");
            txtToDate.Text = dtLast.ToString("dd-MM-yyyy");

            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            objBalMemberNumericAttendance.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            objBalMemberNumericAttendance.ToDate = Todate;
        }
        #endregion

        #region BIND GRID
        public void GridBindPageLoad()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                objBalMemberNumericAttendance.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                objBalMemberNumericAttendance.ToDate = Todate;
                objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objBalMemberNumericAttendance.Search_AttendanceDetails();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    gvAttendanceDetails.Visible = true;
                    gvAttendanceDetails.DataSource = dt;
                    gvAttendanceDetails.DataBind();
                }
                else
                {
                    gvAttendanceDetails.Visible = false;
                }
                flag = 1;
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region BIND GRID
        public void GridBind()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                objBalMemberNumericAttendance.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                objBalMemberNumericAttendance.ToDate = Todate;
                objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objBalMemberNumericAttendance.Search_AttendanceDetails();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    gvAttendanceDetails.Visible = true;
                    gvAttendanceDetails.DataSource = dt;
                    gvAttendanceDetails.DataBind();
                }
                else
                {
                    gvAttendanceDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region Page Indexing
        protected void gvMemshipFoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvAttendanceDetails.PageIndex = e.NewPageIndex;
                GridBindPageLoad();
            }
            else if (flag == 2)
            {
                gvAttendanceDetails.PageIndex = e.NewPageIndex;
                objBalMemberNumericAttendance.Category = "All";
                objBalMemberNumericAttendance.Action = "Search";
                GridBind();
            }
            else if (flag == 3)
            {
                gvAttendanceDetails.PageIndex = e.NewPageIndex;
                objBalMemberNumericAttendance.Action = "SearchByDateCategory";
                if (ddlName.SelectedValue != "--Select--" && txtID.Text != "")
                {
                    objBalMemberNumericAttendance.Category = "Name";
                }
                if (ddlAttendanceStatus.SelectedValue != "All")
                {
                    objBalMemberNumericAttendance.Category = "AttendanceStatus";
                }
                if (ddlName.SelectedValue != "--Select--" && txtID.Text != "" && ddlAttendanceStatus.SelectedValue != "All")
                {
                    objBalMemberNumericAttendance.Category = "NameAndAttstatus";
                }
                GridBind();
            }
            else if (flag == 4)
            {
                gvAttendanceDetails.PageIndex = e.NewPageIndex;
                objBalMemberNumericAttendance.Action = "SearchByCategory";
                objBalMemberNumericAttendance.Category = "Name";
                GridBind();
            }
            else if (flag == 5)
            {
                gvAttendanceDetails.PageIndex = e.NewPageIndex;
                objBalMemberNumericAttendance.Category = "AttendanceStatus";
                objBalMemberNumericAttendance.Action = "SearchByCategory";
                GridBind();
            }
        }
        #endregion
       
        /// <summary>
        /// Search current month record
        /// </summary>

        public void Search()
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            objBalMemberNumericAttendance.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            objBalMemberNumericAttendance.ToDate = Todate;
            objBalMemberNumericAttendance.Category = "All";
            objBalMemberNumericAttendance.Action = "Search";
            GridBind();
            flag = 2;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearchByDateCategory_Click(object sender, EventArgs e)
        {
            objBalMemberNumericAttendance.Action = "SearchByDateCategory";
            
            if (ddlName.SelectedValue != "--Select--" && txtID.Text != "")
            {
                objBalMemberNumericAttendance.Category = "Name";
            }
            if (ddlAttendanceStatus.SelectedValue != "All")
            {
                objBalMemberNumericAttendance.Category = "AttendanceStatus";
            }
            if (ddlName.SelectedValue != "--Select--" && txtID.Text != "" && ddlAttendanceStatus.SelectedValue != "All")
            {
                objBalMemberNumericAttendance.Category = "NameAndAttstatus";
            }
            flag = 3;
        }

        public void Clear()
        {
            AssignTodaysDate();
            ddlName.SelectedValue = "--Select--";
            ddlAttendanceStatus.SelectedValue = "All";
            txtID.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            gvAttendanceDetails.DataSource = null;
            gvAttendanceDetails.DataBind();
            txtFromDate.Focus();
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            objBalMemberNumericAttendance.Action = "SearchByCategory";
            Get_MemberAutoID_ByMemberID1();
            if (ddlName.SelectedValue != "--Select--")
            {
                objBalMemberNumericAttendance.Category = "Name";
                objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlName.SelectedValue);
            }
            GridBind();
            flag = 4;
        }

        public void Get_MemberAutoID_ByMemberID1()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if(txtID.Text != "")
                objMember.Member_ID1 = Convert.ToInt32(txtID.Text);
            dt = objMember.Get_MemberAutoID_ByMemberID1();
            if (dt.Rows.Count > 0)
            {
                ddlName.SelectedValue = dt.Rows[0]["Member_AutoID"].ToString();
                objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlName.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Records Not Available!!!','Error');", true);
                txtID.Text = "";
                return;
            }

        }

        public void Get_MemberDetails_ByMemberAutoID()
        {
            objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if(ddlName.SelectedValue != "--Select--")
                objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlName.SelectedValue);
            dt = objBalMemberNumericAttendance.SELECT_Member_ByAutoID();
            if (dt.Rows.Count > 0)
            {
                txtID.Text = dt.Rows[0]["Member_ID1"].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Records Not Available!!!','Error');", true);
                ddlName.SelectedValue = "--Select--";
                return;
            }
        }

        protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBalMemberNumericAttendance.Action = "SearchByCategory";
            Get_MemberDetails_ByMemberAutoID();
            if (ddlName.SelectedValue != "--Select--")
            {
                objBalMemberNumericAttendance.Category = "Name";
                objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlName.SelectedValue);
            }
            GridBind();
            flag = 4;
        }

        protected void ddlAttendanceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBalMemberNumericAttendance.Action = "SearchByCategory";
            if (ddlAttendanceStatus.SelectedValue != "All")
            {
                objBalMemberNumericAttendance.Category = "AttendanceStatus";
                objBalMemberNumericAttendance.AttndanceStatus = ddlAttendanceStatus.SelectedValue;
            }
            GridBind();
            flag = 5;
        }
    }
}