using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;

namespace NDOnlineGym_2017
{
    public partial class PTMasterForm : System.Web.UI.Page
    {
        BalPTDaysMaster objBalPTDaysMaster = new BalPTDaysMaster();
        BalPTTimeMaster objBalPTTimeMaster = new BalPTTimeMaster();
        BalPTTrainerIncentiveMaster objBalPTTrainerIncentiveMaster = new BalPTTrainerIncentiveMaster();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnDays_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            btnDays.CssClass = "btn-menu btn-menu-selected";
            btnTime.CssClass = "btn-menu";
            btnIncentive.CssClass = "btn-menu";
            BindPT_DaysMasterGrid();
        }

        protected void btnTime_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            btnDays.CssClass = "btn-menu";
            btnTime.CssClass = "btn-menu btn-menu-selected";
            btnIncentive.CssClass = "btn-menu";
            BindPT_TimeMasterGrid();
        }

        protected void btnIncentive_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            btnDays.CssClass = "btn-menu";
            btnTime.CssClass = "btn-menu";
            btnIncentive.CssClass = "btn-menu btn-menu-selected";
            bindInstructor();
            BindPT_TrainerIncentiveMaster();
        }


        #region----------------------------Days Master-----------------------------------

        public void BindPT_DaysMasterGrid()
        {
            try
            {
                objBalPTDaysMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTDaysMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                dt = objBalPTDaysMaster.Select_PT_DaysMaster();

                if (dt.Rows.Count > 0)
                {
                    gvDays.Visible = true;
                    gvDays.DataSource = dt;
                    gvDays.DataBind();
                }
                else
                {
                    gvDays.Visible = false;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvDays.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvDays.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvDays.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvDays.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvDays.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordDays()
        {
            txtDays.Text = "";
            txtDays.Focus();
            btnSaveDays.Text = "Save";
            txtDays.Focus();
        }

        protected void btnSaveDays_Click(object sender, EventArgs e)
        {
            try
            {
                objBalPTDaysMaster.Days = txtDays.Text;
                objBalPTDaysMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTDaysMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveDays.Text == "Save")
                {
                    bool chkExistingPTDays = false;
                    chkExistingPTDays = objBalPTDaysMaster.Check_ExistingDaysPT_DaysMaster();
                    if (chkExistingPTDays == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDays.Focus();
                        return;
                    }
                    else
                    {
                        objBalPTDaysMaster.Action = "Insert";
                        int res = objBalPTDaysMaster.Insert_PT_DaysMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordDays();
                            BindPT_DaysMasterGrid();
                            txtDays.Focus();
                        }
                    }
                }
                else
                {
                    objBalPTDaysMaster.Action = "Update";
                    objBalPTDaysMaster.Days_AutoID = Convert.ToInt32(ViewState["Days_AutoID"]);
                    int res = objBalPTDaysMaster.Insert_PT_DaysMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDays();
                        BindPT_DaysMasterGrid();
                        btnSaveDays.Text = "Save";
                        txtDays.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDays.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearDays_Click(object sender, EventArgs e)
        {
            ClearRecordDays();
            btnClearDays.Focus();
        }

        protected void btnDeleteDays_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPTDaysMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTDaysMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                objBalPTDaysMaster.Action = "Delete";
                objBalPTDaysMaster.Days_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalPTDaysMaster.Insert_PT_DaysMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindPT_DaysMasterGrid();
                    txtDays.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvDays_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDays.PageIndex = e.NewPageIndex;
            BindPT_DaysMasterGrid();
        }

        protected void btnEditDays_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPTDaysMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTDaysMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Days_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveDays.Text = "Update";
                objBalPTDaysMaster.Days_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalPTDaysMaster.SelectByID_PT_DaysMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDays.Text = dt.Rows[0]["Days"].ToString();
                    txtDays.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Time Master-----------------------------------

        public void BindPT_TimeMasterGrid()
        {
            try
            {
                objBalPTTimeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTimeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                dt = objBalPTTimeMaster.Select_PT_TimeMaster();

                if (dt.Rows.Count > 0)
                {
                    gvTime.Visible = true;
                    gvTime.DataSource = dt;
                    gvTime.DataBind();
                }
                else
                {
                    gvTime.Visible = false;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvTime.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvTime.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvTime.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvTime.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvTime.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordTime()
        {
            txtTime.Text = "";
            txtTime.Focus();
            btnSaveTime.Text = "Save";
            txtTime.Focus();
        }

        protected void btnSaveTime_Click(object sender, EventArgs e)
        {
            try
            {
                objBalPTTimeMaster.Time = txtTime.Text;
                objBalPTTimeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTimeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveTime.Text == "Save")
                {
                    bool chkExistingPTTime = false;
                    chkExistingPTTime = objBalPTTimeMaster.Check_ExistingTimePT_TimeMaster();
                    if (chkExistingPTTime == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtTime.Focus();
                        return;
                    }
                    else
                    {
                        objBalPTTimeMaster.Action = "Insert";
                        int res = objBalPTTimeMaster.Insert_PT_TimeMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordTime();
                            BindPT_TimeMasterGrid();
                            txtTime.Focus();
                        }
                    }
                }
                else
                {
                    objBalPTTimeMaster.Action = "Update";
                    objBalPTTimeMaster.Time_AutoID = Convert.ToInt32(ViewState["Time_AutoID"]);
                    int res = objBalPTTimeMaster.Insert_PT_TimeMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordTime();
                        BindPT_TimeMasterGrid();
                        btnSaveTime.Text = "Save";
                        txtTime.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtTime.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearTime_Click(object sender, EventArgs e)
        {
            ClearRecordTime();
            btnClearTime.Focus();
        }

        protected void btnDeleteTime_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPTTimeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTimeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                objBalPTTimeMaster.Action = "Delete";
                objBalPTTimeMaster.Time_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalPTTimeMaster.Insert_PT_TimeMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindPT_TimeMasterGrid();
                    txtTime.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvTime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTime.PageIndex = e.NewPageIndex;
            BindPT_TimeMasterGrid();
        }

        protected void btnEditTime_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPTTimeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTimeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Time_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveTime.Text = "Update";
                objBalPTTimeMaster.Time_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalPTTimeMaster.SelectByID_PT_TimeMaster();
                if (dt.Rows.Count > 0)
                {
                    txtTime.Text = dt.Rows[0]["Time"].ToString();
                    txtTime.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Incentive Master-----------------------------------

        public void bindInstructor()
        {
            try
            {
                objBalPTTrainerIncentiveMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalPTTrainerIncentiveMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objBalPTTrainerIncentiveMaster.SelectInstructor();
                if (dt.Rows.Count > 0)
                {
                    ddlTname.DataSource = dt;
                    ddlTname.DataValueField = "Staff_AutoID";
                    ddlTname.DataTextField = "Name";
                    ddlTname.DataBind();
                    ddlTname.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Instructor !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindPT_TrainerIncentiveMaster()
        {
            try
            {
                objBalPTTrainerIncentiveMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTrainerIncentiveMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                dt = objBalPTTrainerIncentiveMaster.Select_PTTrainerIncentiveMaster();

                if (dt.Rows.Count > 0)
                {
                    gvIncentive.Visible = true;
                    gvIncentive.DataSource = dt;
                    gvIncentive.DataBind();
                }
                else
                {
                    gvIncentive.Visible = false;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvIncentive.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvIncentive.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvIncentive.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvIncentive.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvIncentive.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordIncentive()
        {
            txtIncentive.Text = "";
            ddlTname.Focus(); 
            ddlTname.SelectedValue = "--Select--";
            btnSaveIncentive.Text = "Save";
        }

        protected void btnSaveIncentive_Click(object sender, EventArgs e)
        {
            try
            {
                objBalPTTrainerIncentiveMaster.Incentive = txtIncentive.Text;
                objBalPTTrainerIncentiveMaster.TrainerID_Fk = Convert.ToInt32(ddlTname.SelectedValue);
                objBalPTTrainerIncentiveMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTrainerIncentiveMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveIncentive.Text == "Save")
                {
                    bool chkExistingPTDays = false;
                    chkExistingPTDays = objBalPTTrainerIncentiveMaster.Check_ExistingPTTrainerIncentiveMaster();
                    if (chkExistingPTDays == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDays.Focus();
                        return;
                    }
                    else
                    {
                        objBalPTTrainerIncentiveMaster.Action = "Insert";
                        int res = objBalPTTrainerIncentiveMaster.Insert_PTTrainerIncentiveMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordIncentive();
                            BindPT_TrainerIncentiveMaster();
                            txtIncentive.Focus();
                        }
                    }
                }
                else
                {
                    objBalPTTrainerIncentiveMaster.Action = "Update";
                    objBalPTTrainerIncentiveMaster.Trainer_AutoID = Convert.ToInt32(ViewState["Trainer_AutoID"]);
                    int res = objBalPTTrainerIncentiveMaster.Insert_PTTrainerIncentiveMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordIncentive();
                        BindPT_TrainerIncentiveMaster();
                        btnSaveIncentive.Text = "Save";
                        txtDays.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDays.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearIncentive_Click(object sender, EventArgs e)
        {
            ClearRecordIncentive();
            btnClearDays.Focus();
        }

        protected void btnDeleteIncentive_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPTTrainerIncentiveMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTrainerIncentiveMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                objBalPTTrainerIncentiveMaster.Action = "Delete";
                objBalPTTrainerIncentiveMaster.Trainer_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalPTTrainerIncentiveMaster.Insert_PTTrainerIncentiveMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindPT_TrainerIncentiveMaster();
                    txtDays.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvIncentive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIncentive.PageIndex = e.NewPageIndex;
            BindPT_TrainerIncentiveMaster();
        }

        protected void btnEditIncentive_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPTTrainerIncentiveMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPTTrainerIncentiveMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Trainer_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveIncentive.Text = "Update";
                objBalPTTrainerIncentiveMaster.Trainer_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalPTTrainerIncentiveMaster.SelectByID_PTTrainerIncentiveMaster();
                if (dt.Rows.Count > 0)
                {
                    txtIncentive.Text = dt.Rows[0]["Incentive"].ToString();
                    ddlTname.SelectedValue = dt.Rows[0]["TrainerID_Fk"].ToString();
                    ddlTname.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

    }
}