using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;
//using System.Drawing;
//using System.IO;
//using System.Drawing.Imaging;

namespace NDOnlineGym_2017
{
    public partial class Home : System.Web.UI.Page
    {
        BalLoginForm obBalLoginForm = new BalLoginForm();
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        BalStaffNotificationHome objStaffNotificationHome = new BalStaffNotificationHome();
        BalNewUpdateNotificationHome objNewUpdateNotificationHome = new BalNewUpdateNotificationHome();
        int flag = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindDDLBranchName();
                BindLogDetails();
                BindUpdateNotification();
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    BindBranchInfo();
                    divLogDet.Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    BindBranchInfo();
                    divLogDet.Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    BindBranchInfo();
                    divLogDet.Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    BindBranchInfo();
                    divLogDet.Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    BindBranchInfo();
                    divLogDet.Visible = false;
                }
               
            }
        }

        public void BindBranchInfo()
        {
            try
            {
                obBalBranchInformation.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"].ToString());
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    //gvBranchInfo.Columns[0].Visible = false;
                    obBalBranchInformation.Action = "SELECT_MasterAdmin";
                    dt = obBalBranchInformation.SP_Select_BranchForHomePage();
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    obBalBranchInformation.Action = "SELECT_SuperAdmin";
                    dt = obBalBranchInformation.SP_Select_BranchForHomePage();
                   // gvBranchInfo.Columns[0].Visible = false;
                    gvBranchInfo.Columns[1].Visible = false;
                    gvBranchInfo.Columns[8].Visible = false;
                    gvBranchInfo.Columns[9].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    obBalBranchInformation.Action = "SELECT_Admin";
                    dt = obBalBranchInformation.SP_Select_BranchForHomePage();
                    //gvBranchInfo.Columns[0].Visible = false;
                    gvBranchInfo.Columns[1].Visible = false;
                    gvBranchInfo.Columns[8].Visible = false;
                    gvBranchInfo.Columns[9].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    obBalBranchInformation.Action = "SELECT_Manager";
                    dt = obBalBranchInformation.SP_Select_BranchForHomePage();
                    //gvBranchInfo.Columns[0].Visible = false;
                    gvBranchInfo.Columns[1].Visible = false;
                    gvBranchInfo.Columns[8].Visible = false;
                    gvBranchInfo.Columns[9].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    obBalBranchInformation.Action = "SELECT_User";
                    dt = obBalBranchInformation.SP_Select_BranchForHomePage();
                    gvBranchInfo.Columns[0].Visible = false;
                    gvBranchInfo.Columns[1].Visible = false;
                    gvBranchInfo.Columns[8].Visible = false;
                    gvBranchInfo.Columns[9].Visible = false;
                }
                if (dt.Rows.Count > 0)
                {
                    gvBranchInfo.Visible = true;
                    gvBranchInfo.DataSource = dt;
                    gvBranchInfo.DataBind();
                }
                else
                {
                    gvBranchInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        //public void BindStaffNotification()
        //{
        //    objStaffNotificationHome.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"]);
        //    objStaffNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //    dt = objStaffNotificationHome.Select_StaffNotificationHomePage();
        //    if (dt.Rows.Count > 0)
        //    {
        //        RepterDetails.DataSource = dt;
        //        RepterDetails.DataBind();
        //    }
        //}

        public void bindDDLBranchName()
        {
            try
            {
                dt = obBalBranchInformation.SelectAll_BranchInformation();
                if (dt.Rows.Count > 0)
                {
                    ddlBranch.DataSource = dt;
                    ddlBranch.Items.Clear();
                    ddlBranch.DataValueField = "Branch_AutoID";
                    ddlBranch.DataTextField = "BranchName";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindLogDetails()
        {
            if (flag == 1)
                obBalLoginForm.Action = "SelectByBranch";
            else
                obBalLoginForm.Action = "SelectAll";

            DateTime TodayDate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
            { }
            obBalLoginForm.TodayDate = TodayDate;
            dt = obBalLoginForm.BindLogDetails();
            if (dt.Rows.Count > 0)
            {
                gvLogDetails.DataSource = dt;
                gvLogDetails.DataBind();
            }
            else
            {
                gvLogDetails.DataSource = null;
                gvLogDetails.DataBind();
            }
        }


        public void BindUpdateNotification()
        {
            ds = objNewUpdateNotificationHome.Select_UpdateNotificationHome();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }

        protected void btnName_Command(object sender, CommandEventArgs e)
        {
            //try
            //{
                int Branch_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Cookies["OnlineGym1"]["brIDHome"] = Branch_AutoID.ToString();
                Session["Branch_ID"] = Branch_AutoID.ToString();
                Response.Redirect("Dashboard.aspx?Branch_AutoID=" + HttpUtility.UrlEncode(Branch_AutoID.ToString()));
            //}
            //catch (Exception ex)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            //}
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(e.CommandArgument);
                obBalBranchInformation.Action = "Delete";
                int i = obBalBranchInformation.Insert_BranchInformation();//DeleteBranchInformation();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindBranchInfo();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
                return;
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int Branch_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("BranchInformation.aspx?Branch_AutoID=" + HttpUtility.UrlEncode(Branch_AutoID.ToString()));
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void gvBranchInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBranchInfo.PageIndex = e.NewPageIndex;
            BindBranchInfo();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField hf1 = e.Item.FindControl("HiddenField2") as HiddenField;
            if (hf1 != null)
            {
                string val = hf1.Value;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("ImgUpdate") as System.Web.UI.WebControls.Image;
                
                img.ImageUrl = val;
            }
                HtmlGenericControl ImageSection = (HtmlGenericControl)e.Item.FindControl("ImageSection");
                if (ImageSection != null)
                {
                    ImageSection.Attributes["class"] = "image-section-odd";
                }
                HtmlGenericControl UpdateSection = (HtmlGenericControl)e.Item.FindControl("UpdateSection");
                if (UpdateSection != null)
                {
                    UpdateSection.Attributes["class"] = "update-section-odd";
                }
                HtmlGenericControl divHeading = (HtmlGenericControl)e.Item.FindControl("divHeading");
                if (divHeading != null)
                {
                    divHeading.Attributes["class"] = "heading-section-odd";
                }
                HtmlGenericControl divDescription = (HtmlGenericControl)e.Item.FindControl("divDescription");
                if (divDescription != null)
                {
                    divDescription.Attributes["class"] = "description-section-odd";
                }
                HtmlGenericControl divdate = (HtmlGenericControl)e.Item.FindControl("divdate");
                if (divdate != null)
                {
                    divdate.Attributes["class"] = "date-section-odd";
                }

            

        }

        protected void gvLogDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLogDetails.PageIndex = e.NewPageIndex;
            BindLogDetails();
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = 1;
            obBalLoginForm.Branch_AutoID = Convert.ToInt32(ddlBranch.SelectedValue);
            BindLogDetails();
        }
    }
}