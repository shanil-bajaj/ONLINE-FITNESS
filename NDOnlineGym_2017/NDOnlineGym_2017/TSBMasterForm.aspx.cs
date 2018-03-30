using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;

namespace NDOnlineGym_2017
{
    public partial class TSBMasterForm : System.Web.UI.Page
    {
        BalCourtBooking objcourt = new BalCourtBooking();
        DataTable dt = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
 
            }
        }

        private void AssignID()
        {
            objcourt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objcourt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

       
        protected void btnDays_Click(object sender, EventArgs e)
        {
           // MultiView1.ActiveViewIndex = 0;
            btnDays.CssClass = "btn-menu btn-menu-selected";
            //btnTime.CssClass = "btn-menu";
            //btnIncentive.CssClass = "btn-menu";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objcourt.Name = txtName.Text;
                if (btnSave.Text == "Save")
                {
                    objcourt.Action = "InsertCourtBookingMaster";
                    int k = objcourt.Insert_Update_Delete();
                    if (k > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        Clear();
                        BindGridView();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Not Saved !!!','Success');", true);
                    }
                }
                else
                {
                    //Save();
                    objcourt.Action = "UpdateCourtBookingMaster";
                    int k = objcourt.Insert_Update_Delete();
                    if (k > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        Clear();
                        BindGridView();
                        btnSave.Text = "Save";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void BindGridView()
        {
            dt = objcourt.BindGV();
            if (dt.Rows.Count > 0)
            {
                GvTSBMaster.DataSource = dt;
                GvTSBMaster.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!','Information');", true);
            }
        }

        private void Clear()
        {
            txtName.Text = "";
            btnSave.Text = "Save";
        }

      
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objcourt.CourtMaster_AutoID = Convert.ToInt32(e.CommandArgument);
                AssignID();
                objcourt.Action = "DeleteTSBMaster";
                int i = objcourt.Insert_Update_Delete();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    // BindBranchInfoGrid();
                    //ddlCompany.Focus();
                    BindGridView();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public int ID;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objcourt.Action = "EditTSBMaster"; 
                ID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["ID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSave.Text = "Update";
                AssignID();
                txtName.Focus();
                dt = objcourt.Get_Edit();
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

       
        //protected void btnTime_Click(object sender, EventArgs e)
        //{
        //    MultiView1.ActiveViewIndex = 1;
        //    btnDays.CssClass = "btn-menu";
        //    btnTime.CssClass = "btn-menu btn-menu-selected";
        //    btnIncentive.CssClass = "btn-menu";

        //}

        //protected void btnIncentive_Click(object sender, EventArgs e)
        //{
        //    MultiView1.ActiveViewIndex = 2;
        //    btnDays.CssClass = "btn-menu";
        //    btnTime.CssClass = "btn-menu";
        //    btnIncentive.CssClass = "btn-menu btn-menu-selected";
        //}
    }
}