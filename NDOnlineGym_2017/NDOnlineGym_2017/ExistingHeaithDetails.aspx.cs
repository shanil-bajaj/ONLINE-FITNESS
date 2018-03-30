using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class ExistingHeaithDetails : System.Web.UI.Page
    {
        BalExistingHealthDetails ObjExistHealthDetails = new BalExistingHealthDetails();
        DataTable dataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                    
                    pageLoadRecord();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        

        #region --------- Assign Comp and Branch ID-------------------------
        private void AssignID()
        {
            try
            {
                ObjExistHealthDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                ObjExistHealthDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Bind Record On PageLoad -----------
        protected void pageLoadRecord()
        {
            try
            {
                txtMemberID.Focus();
                BindMemberList();
                ObjExistHealthDetails.Action = "BindExistingHealthDetails";
                BindExistingHealthDetails();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Bind Member DDL ----------
        private void BindMemberList()
        {
            try
            {
                AssignID();
                ObjExistHealthDetails.Action = "BindMemberList";
                dataTable = ObjExistHealthDetails.GetDetails();

                if (dataTable.Rows.Count != 0)
                {
                    ddlMemberName.DataSource = dataTable;
                    ddlMemberName.Items.Clear();
                    ddlMemberName.DataValueField = "Member_AutoID";
                    ddlMemberName.DataTextField = "Name";
                    ddlMemberName.DataBind();
                    ddlMemberName.Items.Insert(0, new ListItem("--Select--", "--Select--"));                    
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- Bind Existing HealthDetails Gridview ---------------
        private void BindExistingHealthDetails()
        {
            try
            {
                AssignID();
                //BindGridView();
                ViewState["ExeHealthDetail"] = ObjExistHealthDetails.GetDetails();
                dataTable = (DataTable)ViewState["ExeHealthDetail"];
                lblCount.Text = Convert.ToString(dataTable.Rows.Count);
                if (dataTable.Rows.Count != 0)
                {
                    gvExistingHealthDetails.DataSource = dataTable;
                    gvExistingHealthDetails.DataBind();
                }
                else
                {
                    gvExistingHealthDetails.DataSource = dataTable;
                    gvExistingHealthDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
                }                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Get Details By Member ID ---------------
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjExistHealthDetails.SearchByText = txtMemberID.Text;
                ObjExistHealthDetails.Action = "GetByMemberID";
                BindGridView();
                txtMemberID.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Get Details By Member Contact ---------------
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjExistHealthDetails.SearchByText = txtContact.Text;
                ObjExistHealthDetails.Action = "GetByContact";
                BindGridView();
                ddlMemberName.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Get Details By Member Name ---------------
        protected void ddlMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjExistHealthDetails.SearchByText = ddlMemberName.SelectedValue;
                ObjExistHealthDetails.Action = "GetByName";
                BindGridView();
                txtContact.Focus();                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Bind Gridview ----------------
        protected void BindGridView()
        {
            try
           {
                ViewState["ExeHealthDetail"]=ObjExistHealthDetails.GetDetails();
                dataTable = (DataTable)ViewState["ExeHealthDetail"];
                lblCount.Text = Convert.ToString(dataTable.Rows.Count);
                if (dataTable.Rows.Count != 0)
                {
                    txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                    ddlMemberName.SelectedValue = dataTable.Rows[0]["Member_AutoID1"].ToString();
                    txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                    gvExistingHealthDetails.DataSource = dataTable;
                    gvExistingHealthDetails.DataBind();
                }
                else
                {
                    txtMemberID.Text = "";
                    ddlMemberName.SelectedIndex =0;
                    txtContact.Text = "";
                    gvExistingHealthDetails.DataSource = dataTable;
                    gvExistingHealthDetails.DataBind();                    
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Refresh ---------------
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMemberID.Text = "";
            ddlMemberName.SelectedIndex = -1;
            txtContact.Text = "";
            pageLoadRecord();            
        }
        #endregion
       
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;

                int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());

                LinkButton btn = (LinkButton)row.FindControl("btnStatus");
                string Status = btn.Text;

                if (Status == "Completed")
                {
                    string url = "HealthDetails.aspx?HealthDetailsMember_ID=" + HttpUtility.UrlEncode(Member_AutoID.ToString());
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "text", "showConfirmation('" + url + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Present !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;

                int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());

                LinkButton btn = (LinkButton)row.FindControl("btnStatus");
                string Status = btn.Text;

                if (Status == "Completed")
                {
                    AssignID();
                    ObjExistHealthDetails.Member_AutoID = Member_AutoID;
                    ObjExistHealthDetails.Action = "DELETE";

                    int res = ObjExistHealthDetails.Insert_Update_Delete_Details();

                    if (res > 0)
                    {
                        pageLoadRecord();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Deletedd Failed !!!','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Can Not Delete Because Record Not Present !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnStatus_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;

                LinkButton btn = (LinkButton)row.FindControl("btnStatus");
                string Status = btn.Text;
   
                int Member_AutoID  = Convert.ToInt32(e.CommandArgument.ToString());

                if (Status == "Completed")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Can Not Insert Record  Present !!!','Information');", true);

                }
                else
                {
                    string url = "HealthDetails.aspx?HealthDetailsMember_ID=" + HttpUtility.UrlEncode(Member_AutoID.ToString());
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "text", "showConfirmation1('" + url + "')", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void gvExistingHealthDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvExistingHealthDetails.PageIndex = e.NewPageIndex;

                ObjExistHealthDetails.Action = "BindExistingHealthDetails";
                BindExistingHealthDetails();

                //dataTable = (DataTable)ViewState["ExeHealthDetail"];

                //if (dataTable.Rows.Count != 0)
                //{
                //    gvExistingHealthDetails.DataSource = dataTable;
                //    gvExistingHealthDetails.DataBind();
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        protected void btnPreview_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;

                LinkButton btn = (LinkButton)row.FindControl("btnStatus");
                string Status = btn.Text;
   
                int Member_Id1 = Convert.ToInt32(e.CommandArgument.ToString());

                if (Status == "Completed")
                {
                    string strPopup = "<script language='javascript' ID='script1'>"
                    + "window.open('ViewHealthDetails.aspx?Member_Id=" + Member_Id1
                    + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
                    + "</script>";
                    ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Present !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }


    }
}