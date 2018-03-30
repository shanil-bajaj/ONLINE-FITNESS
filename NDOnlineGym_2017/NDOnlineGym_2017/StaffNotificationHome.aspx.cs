using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Globalization;
using DataAccessLayer;
using System.Data.SqlClient;

namespace NDOnlineGym_2017
{
    public partial class StaffNotificationHome : System.Web.UI.Page
    {
        BalStaffNotificationHome objStaffNotificationHome = new BalStaffNotificationHome();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        DataTable dt = new DataTable();
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDDLStaffName(); 
                ddlStaffName.Focus();
            }
        }

        public void bindDDLStaffName()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dt.Rows.Count > 0)
                {
                    ddlStaffName.DataSource = dt;
                    ddlStaffName.Items.Clear();
                    ddlStaffName.DataValueField = "Staff_AutoID";
                    ddlStaffName.DataTextField = "Name";
                    ddlStaffName.DataBind();
                    ddlStaffName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddParameters()
        {
            DateTime Todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todaydate))
            {}

            objStaffNotificationHome.StaffName = ddlStaffName.SelectedItem.Text;
            objStaffNotificationHome.Staff_AutoID = Convert.ToInt32(ddlStaffName.SelectedValue);
            objStaffNotificationHome.Notification = txtNotification.Text;
            objStaffNotificationHome.Date = Todaydate;
            objStaffNotificationHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objStaffNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    AddParameters();

                    objStaffNotificationHome.Action = "Insert";
                    int res = objStaffNotificationHome.Insert_StaffNotificationHome();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            Clear();
                            btnClear.Visible = true;
                            ddlStaffName.Focus();
                            objStaffNotificationHome.Action = "Search";
                            BindGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                        }
                   
                }
                else if (btnSave.Text == "Update")
                {
                    AddParameters();
                    objStaffNotificationHome.Action = "Update";
                    objStaffNotificationHome.Notification_AutoID = Convert.ToInt32(ViewState["Notification_AutoID"]);
                    int res = objStaffNotificationHome.Insert_StaffNotificationHome();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ddlStaffName.Focus();
                        if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                        {
                            btnSave.Text = "Edit";
                            Clear();
                        }
                        else
                        {
                            btnSave.Text = "Save";
                            Clear();
                            BindGrid();
                        }
                        btnClear.Visible = true;
                        ddlStaffName.Focus();
                        objStaffNotificationHome.Action = "Search";
                        BindGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Update Failed !!!','Error');", true);
                        return;
                    }
                }
               
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void GetDataOnEdit(int Notification_AutoID)
        {
            try
            {
                //bindDDLStaffName();
                objStaffNotificationHome.Notification_AutoID = Convert.ToInt32(Notification_AutoID);
                objStaffNotificationHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objStaffNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //objStaffNotificationHome.authority = Request.Cookies["OnlineGym"]["Authority"];

                objStaffNotificationHome.Action = "SELECT_BY_ID";

                dt = objStaffNotificationHome.SelectByID_StaffNotificationHome();
                if (dt.Rows.Count > 0)
                {
                    ddlStaffName.SelectedValue = dt.Rows[0]["Staff_AutoID"].ToString();
                   // ddlStaffName.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
                    txtNotification.Text = dt.Rows[0]["Notification"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                int Notification_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["Notification_AutoID"] = Notification_AutoID;
                GetDataOnEdit(Notification_AutoID);
                ddlStaffName.Focus();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void Clear()
        {
            ddlStaffName.SelectedValue = "--Select--";
            txtNotification.Text = "";
            btnSave.Text = "Save";
            ddlSearch.SelectedValue = "--Select--";
            txtSearch.Text = "";
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                DateTime Todaydate;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todaydate))
                { }
                objStaffNotificationHome.Date = Todaydate;
                objStaffNotificationHome.Notification_AutoID = Convert.ToInt32(e.CommandArgument);
                objStaffNotificationHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objStaffNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objStaffNotificationHome.Action = "Delete";
                int i = objStaffNotificationHome.Insert_StaffNotificationHome();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    objStaffNotificationHome.Action = "Search";
                    flag = 2;
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Delete Failed !!!.','Error');", true);
                return;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            if (ddlSearch.SelectedValue != "--Select--")
            {
                if (txtSearch.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Search Category !!!','Success');", true);
                    return;
                }
                else
                {
                    SearchByCategory();
                }
            }
            else if (txtSearch.Text != "")
            {
                if (ddlSearch.SelectedValue == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Search Category !!!','Success');", true);
                    return;
                }
                else
                {
                    SearchByCategory();
                }
            }
            else
            {
                objStaffNotificationHome.Action = "Search";
                flag = 2;
            }
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            gvNotification.DataSource = null;
            gvNotification.DataBind();
            ddlStaffName.Focus();
        }

        // static int flag;
        protected void gvNotification_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvNotification.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            else if (flag == 2)
            {
                gvNotification.PageIndex = e.NewPageIndex;
                BindGrid();
            }
        }

        public void BindGrid()
        {
            try
            {
                objStaffNotificationHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objStaffNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objStaffNotificationHome.Select_DataAsPerSearchCriteriaBranch();
                if (dt.Rows.Count > 0)
                {
                    gvNotification.DataSource = dt;
                    gvNotification.DataBind();

                }
                else
                {
                    gvNotification.DataSource = null;
                    gvNotification.DataBind();
                    gvNotification.Style["width"] = "100%";
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvNotification.Columns[0].Visible = true;
                    gvNotification.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvNotification.Columns[0].Visible = true;
                    gvNotification.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvNotification.Columns[0].Visible = true;
                    gvNotification.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvNotification.Columns[0].Visible = true;
                    gvNotification.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvNotification.Columns[0].Visible = false;
                    gvNotification.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void SearchByCategory()
        {
            objStaffNotificationHome.Action = "Search_By_Category";
            if (ddlSearch.SelectedValue == "Staff Name")
            {
                objStaffNotificationHome.Category = "Staff Name";
                objStaffNotificationHome.StaffName = txtSearch.Text;
            }
           
            flag = 1;
        }

    }
}