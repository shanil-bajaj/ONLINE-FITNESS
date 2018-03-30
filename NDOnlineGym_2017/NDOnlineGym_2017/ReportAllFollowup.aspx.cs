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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


namespace NDOnlineGym_2017
{
    public partial class ReportAllFollowup : System.Web.UI.Page
    {
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalFollowupTypeMaster obBalFollowupTypeMaster = new BalFollowupTypeMaster();
        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();
        BalFollowup objFollowup = new BalFollowup();
        DataTable dt = new DataTable();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignDate();
                PageLoadGrid(); 
            }
        }
        public void AssignDate()
        {
            try
            {
                DateTime date;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                { }

                DateTime dtFirst = Convert.ToDateTime(date);
                DateTime dtLast;

                //Setting Start Date Month
                dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
                txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

                //Setting Last Date of Month
                dtLast = dtFirst.AddMonths(1).AddDays(-1);
                txtToDate.Text = dtLast.ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);

            }

        }

        //public void BindDDLExecutive()
        //{
        //    try
        //    {
        //        obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
        //        obBalStaffRegistration.Action = "BindDDL";
        //        obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //        obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //        dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
        //        if (dt.Rows.Count != 0)
        //        {
        //            ddlSearchBy.DataSource = dt;
        //            ddlSearchBy.Items.Clear();
        //            ddlSearchBy.DataValueField = "Staff_AutoID";
        //            ddlSearchBy.DataTextField = "Name";
        //            ddlSearchBy.DataBind();
        //            //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //            txtSearch.Text = Request.Cookies["OnlineGym"]["Staff_AutoID"];
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }
        //}
        
        //private void BindDDLFollowupType()
        //{
        //    try
        //    {
        //        AssignID();
        //        objFollowup.Action = "Select_FollowupType";
        //        dt = objFollowup.GetDetails();
        //        if (dt.Rows.Count >= 0)
        //        {
        //            ddlSearchBy.DataSource = dt;
        //            ddlSearchBy.Items.Clear();
        //            ddlSearchBy.DataValueField = "FollowupType_AutoID";
        //            ddlSearchBy.DataTextField = "Name";
        //            ddlSearchBy.DataBind();
        //            ddlSearchBy.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandiling.SendErrorToText(ex);
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

        //    }
        //}

        //public void BindDDLMemberID()
        //{
        //    try
        //    {
        //        objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //        objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //        dt = objMember.bindMemeber();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlSearchBy.DataSource = dt;
        //            ddlSearchBy.Items.Clear();
        //            ddlSearchBy.DataValueField = "Member_AutoID";
        //            ddlSearchBy.DataTextField = "Member_ID1";
        //            ddlSearchBy.DataBind();
        //            ddlSearchBy.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //            ddlSearchBy.SelectedItem.Value = "--Select--";
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        return;
        //    }
        //}
           
        //public void BindDDLMemeberName()
        //{
        //    try
        //    {
        //        objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //        objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //        dt = objMember.bindMemeber();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlSearchBy.DataSource = dt;
        //            ddlSearchBy.Items.Clear();
        //            ddlSearchBy.DataValueField = "Member_AutoID";
        //            ddlSearchBy.DataTextField = "Name";
        //            ddlSearchBy.DataBind();
        //            ddlSearchBy.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //            ddlSearchBy.SelectedItem.Value = "--Select--";
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        return;
        //    }
        //}

        //public void BindDDLContact()
        //{
        //    try
        //    {
        //        objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //        objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //        dt = objMember.bindMemeber();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlSearchBy.DataSource = dt;
        //            ddlSearchBy.Items.Clear();
        //            ddlSearchBy.DataValueField = "Contact1";
        //            ddlSearchBy.DataTextField = "Contact1";
        //            ddlSearchBy.DataBind();
        //            ddlSearchBy.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //            ddlSearchBy.SelectedItem.Value = "--Select--";
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        return;
        //    }
        //}

        //public void BindDDLCallResponse()
        //{
        //    try
        //    {
        //        objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //        objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //        dt = objMember.bindCallResponse();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlSearchBy.DataSource = dt;
        //            ddlSearchBy.Items.Clear();
        //            ddlSearchBy.DataValueField = "CallRespond_AutoID";
        //            ddlSearchBy.DataTextField = "Name";
        //            ddlSearchBy.DataBind();
        //            ddlSearchBy.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //            ddlSearchBy.SelectedItem.Value = "--Select--";
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        return;
        //    }
        //}

        //protected void ddlSearchByCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSearchByCategory.SelectedValue == "All")
        //    {
        //        txtSearch.Text = "All";
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByExecutive")
        //    {
        //        BindDDLExecutive();
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByFollowupType")
        //    {
        //        BindDDLFollowupType();
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByMemberID")
        //    {
        //        BindDDLMemberID();
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByName")
        //    {
        //        BindDDLMemeberName();
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByContact")
        //    {
        //        BindDDLContact();
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByGender")
        //    {
        //        ddlSearchBy.SelectedItem.Text = "Male";
        //        ddlSearchBy.SelectedItem.Text = "Female";
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByRating")
        //    {
        //        ddlSearchBy.SelectedItem.Text = "Hot";
        //        ddlSearchBy.SelectedItem.Text = "Cold";
        //        ddlSearchBy.SelectedItem.Text = "Warm";
        //        ddlSearchBy.SelectedItem.Text = "Expected";
        //        ddlSearchBy.SelectedItem.Text = "Not Intrested";
        //    }
        //    else if (ddlSearchByCategory.SelectedValue == "SearchByCallResponse")
        //    {
        //        BindDDLCallResponse();
        //    }
            
        //}

        public void AssignID()
        {
            objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objFollowup.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        public void BindGrid()
        {
            AssignID();
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            {
                objFollowup.FromDate = Fromdate;
            }
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            {
                objFollowup.ToDate = Todate;
            }
            dt = objFollowup.Search1();
            if (dt.Rows.Count > 0)
            {
                gvAllFollowupReport.DataSource = dt;
                gvAllFollowupReport.DataBind();
            }
            else
            {
                gvAllFollowupReport.DataSource = null;
                gvAllFollowupReport.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Records not Available  !!!','Error');", true);
                return;
            }
        }

        public void SearchCategory()
        {
            if (ddlSearchByCategory.SelectedValue == "All")
            {
                objFollowup.Category = "All";
                objFollowup.SearchText = "All";
            }
            else if (ddlSearchByCategory.SelectedValue == "SearchByExecutive")
            {
                objFollowup.Category = "SearchByExecutive";
                objFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlSearchByCategory.SelectedValue == "SearchByFollowupType")
            {
                objFollowup.Category = "SearchByFollowupType";
                objFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlSearchByCategory.SelectedValue == "SearchByRating")
            {
                objFollowup.Category = "SearchByRating";
                objFollowup.SearchText = txtSearch.Text;
            }

            else if (ddlSearchByCategory.SelectedValue == "SearchByCallResponse")
            {
                objFollowup.Category = "SearchByCallResponse";
                objFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlSearchByCategory.SelectedValue == "SearchByName")
            {
                objFollowup.Category = "SearchByName";
                objFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlSearchByCategory.SelectedValue == "SearchByMemberID")
            {
                objFollowup.Category = "SearchByMemberID";
                objFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlSearchByCategory.SelectedValue == "SearchByContact")
            {
                objFollowup.Category = "SearchByContact";
                objFollowup.SearchText = txtSearch.Text;
            }
            else if (ddlSearchByCategory.SelectedValue == "SearchByGender")
            {
                objFollowup.Category = "SearchByGender";
                objFollowup.SearchText = txtSearch.Text;
            }
        }

        protected void btnSearchByFollowupDateCategory_Click(object sender, EventArgs e)
        {
            objFollowup.DateCategory = "FollowupDate";
            objFollowup.Action = "SearchByDateCategory";
            SearchCategory();
            BindGrid();
        }
       
        protected void btnSearchByNextFollDateCategory_Click(object sender, EventArgs e)
        {
            objFollowup.DateCategory = "NextFollowupDate";
            objFollowup.Action = "SearchByDateCategory";
            SearchCategory();
            BindGrid();
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            AssignDate();
            txtSearch.Text = "";
            ddlSearchByCategory.SelectedValue = "--Select--";
        }

        public void PageLoadGrid()
        {
            ddlSearchByCategory.SelectedValue = "--Select--";
            txtSearch.Text = "--Select--";
            objFollowup.DateCategory = "FollowupDate";
            objFollowup.Action = "Search";
            objFollowup.Category = "All";
            objFollowup.SearchText = "All";
            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageLoadGrid();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            objFollowup.Action = "SearchByCategory";
            SearchCategory();
            BindGrid(); 
        }
    }
}