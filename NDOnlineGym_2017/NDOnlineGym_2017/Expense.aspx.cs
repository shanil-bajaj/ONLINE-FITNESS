using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;
using System.Globalization;
using System.IO;
using System.Drawing;


namespace NDOnlineGym_2017
{
    public partial class Expense : System.Web.UI.Page
    {
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalExpense ObjExpense = new BalExpense();
        DataTable dt = new DataTable();
        static int ActionFlag;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtPayDetails.Text = "NA";
                    txtExpenseID.Focus();
                    Get_ExpanseId();
                    AssignTodaysDate();
                    AssignMonthDate();
                    bindDDLExecutive();
                    setExecutive();
                    Bind_ExpenseGrp();
                    Bind_PaymentType();

                    //if (Request.QueryString["MenuExpenseDetails"] != null)
                    //{
                    //    divsearch.Visible = true;
                    //    divFormDetails.Visible = false;
                    //}
                    //Search();
                    SearchByDate();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }            
        }       



        #region ---------- Assign Company ID and Branch ID -------------------
        private void AssignID()
        {
            ObjExpense.company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
            ObjExpense.Login_AutoId = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }
        #endregion

        #region ---------------- Bind Executive DDL --------------
        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dt.Rows.Count != 0)
                {
                    ddlExecutive.DataSource = dt;
                    ddlExecutive.Items.Clear();
                    ddlExecutive.DataValueField = "Staff_AutoID";
                    ddlExecutive.DataTextField = "Name";
                    ddlExecutive.DataBind();
                    //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------------- Set Executive DDL --------------
        public void setExecutive()
        {
            try
            {
                obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                dt = obBalStaffRegistration.GetExecutiveByID_ByBranch();
                if (dt.Rows.Count > 0)
                {
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                    ddlExecutive.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
                }
                else
                {
                    dt = obBalStaffRegistration.GetExecutiveByID_WithoutBranch();
                    string staffid = dt.Rows[0]["Staff_AutoID"].ToString();
                    string staffnm = dt.Rows[0]["Name"].ToString();
                    ddlExecutive.Items.Insert(0, new ListItem(staffnm, staffid));
                    ddlExecutive.SelectedItem.Text = staffnm;
                }
            }            
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }   

        }
        #endregion

        #region -------------- Assign Today Date ----------------
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtDate.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        #endregion

        #region ----------- Assign Month Start And End Date ---------------
        public void AssignMonthDate()
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
        #endregion

        #region ------------ Get Expanse Id --------------
        public void Get_ExpanseId()
        {
            try
            {
                dt.Clear();
                AssignID();
                dt = ObjExpense.Get_Expenseid();
                if (dt.Rows.Count > 0)
                {
                    txtExpenseID.Text = dt.Rows[0]["ExpenseId"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------------- Bind Expanse Group -------------------
        public void Bind_ExpenseGrp()
        {
            try
            {
                AssignID();
                dt = ObjExpense.Get_ExpenseGroup();
                if (dt.Rows.Count > 0)
                {
                    ddlExpenseGroup.DataSource = dt;
                    ddlExpenseGroup.Items.Clear();
                    ddlExpenseGroup.DataValueField = "Expgrp_AutoID";
                    ddlExpenseGroup.DataTextField = "Name";
                    ddlExpenseGroup.DataBind();
                    ddlExpenseGroup.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------------- Bind Payment Type ----------------
        public void Bind_PaymentType()
        {
            try
            {
                AssignID();
                dt = ObjExpense.Get_PaymentType();
                if (dt.Rows.Count > 0)
                {
                    ddlPaymentMode.DataSource = dt;
                    ddlPaymentMode.Items.Clear();
                    ddlPaymentMode.DataValueField = "PaymentMode_AutoID";
                    ddlPaymentMode.DataTextField = "Name";
                    ddlPaymentMode.DataBind();
                    ddlPaymentMode.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ----------------- Save Button Event -------------------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    AddParameter();
                    int Exits1 = Convert.ToInt32(ObjExpense.Exits());
                    if (Exits1 > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Expense ID already exist !!!','Error');", true);
                        Get_ExpanseId();                        
                        btnSave.Focus();
                        return;
                    }                  
                    else
                    {
                        int k = ObjExpense.Insert_Update_Delete();
                        if (k > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            Clear();
                            SearchByDate();
                            //Search();
                            //BindGv();
                        }
                    }
                }
                else
                {
                    AddParameter();
                    int k = ObjExpense.Insert_Update_Delete();
                    if (k > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        Clear();
                        //BindGv();
                        btnSave.Text = "Save";
                        //Search();
                        SearchByDate();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion      

        #region --------------------- Add Parameter -------------------------
        private void AddParameter()
        {
            ObjExpense.Ex_Id = Convert.ToInt32(txtExpenseID.Text);
           
            DateTime Exp_Date;
            if (DateTime.TryParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Exp_Date))
            {
                string Exp_Date1 = Exp_Date.ToString("dd-MM-yyyy");
                ObjExpense.Exp_Date = DateTime.ParseExact(Exp_Date1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            ObjExpense.name = txtName.Text;
            ObjExpense.note1 = txtNote1.Text;
            ObjExpense.note2 = txtNote2.Text;
            ObjExpense.amount =Convert.ToDouble(txtAmount.Text);
            ObjExpense.Pay_Details = txtPayDetails.Text;
            ObjExpense.Totalamount =Convert.ToDouble(txtTotalAmt.Text);
            if (txtTaxableAmt.Text != "")
            {
                ObjExpense.Taxamount = Convert.ToDouble(txtTaxableAmt.Text);
            }
            else
            {
                ObjExpense.Taxamount = Convert.ToDouble(0);
            }            

            ObjExpense.ExGrp_AutoId = Convert.ToInt32(ddlExpenseGroup.SelectedValue);
            ObjExpense.pay_AutoId =Convert.ToInt32(ddlPaymentMode.SelectedValue);           
            ObjExpense.Login_AutoId = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            AssignID();
            
            ObjExpense.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
            
            if (Convert.ToInt32(ViewState["flag"]) != 1)
            {
                ObjExpense.Action = "Insert";
            }
            else
            {
                ObjExpense.Action = "Update";
                ObjExpense.Ex_AutoId = Convert.ToInt32(ViewState["Expense_id"]);
                ViewState["flag"] = 0;
            }

        }
        #endregion

        private void Clear()
        {
            //txtExpenseID.Text = "";
            txtName.Text = "";
            txtNote1.Text = "";
            txtNote2.Text = "";
            txtPayDetails.Text = "NA";
            txtAmount.Text = "";
            txtTaxableAmt.Text = "";
            chkExecutive.Checked = true;            
            ddlExecutive.Enabled = false;
            ddlExpenseGroup.Text = "--Select--";
            ddlPaymentMode.Text = "--Select--";
            txtTotalAmt.Text = "";
            gvExpenseDetails.Visible = false;
            btnSave.Text = "Save";
            AssignTodaysDate();
            Get_ExpanseId();            
            setExecutive();           
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;
            ViewState["flag"] = 0;
            AssignMonthDate();
            txtSearch.Text = "";
            //lblCount.Text = "0";
        }

        public int Expense_id;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            Expense_id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["Expense_id"] = Convert.ToInt32(e.CommandArgument.ToString());
            GetDataForEdit(Expense_id);
            txtExpenseID.Focus();
        }

        //int Flag = 0;
        private void GetDataForEdit(int Expense_id)
        {
            try
            {
                btnSave.Text = "Update";
                AssignID();
                ObjExpense.Ex_AutoId = Convert.ToInt32(ViewState["Expense_id"]);
                dt.Clear();
                dt = ObjExpense.Get_Edit();
                if (dt.Rows.Count > 0)
                {
                    txtExpenseID.Text = dt.Rows[0]["Exp_ID1"].ToString();
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                    txtNote1.Text = dt.Rows[0]["Note1"].ToString();
                    txtNote2.Text = dt.Rows[0]["Note2"].ToString();
                    if (dt.Rows[0]["Exp_Date"].ToString() != "")
                    {
                        DateTime Exp_Date = Convert.ToDateTime(dt.Rows[0]["Exp_Date"].ToString());
                        DateTime Exp_Date1;
                        if (DateTime.TryParseExact(Exp_Date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Exp_Date1))
                        {
                            txtDate.Text = Exp_Date1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtDate.Text = "";

                    //txtDate.Text = dt.Rows[0]["Exp_Date"].ToString();
                    //txtDate.Text = Exp_Date.ToString("dd-MM-yyyy");
                    txtPayDetails.Text = dt.Rows[0]["Pay_Details"].ToString();
                    txtTotalAmt.Text = dt.Rows[0]["TotalAmount"].ToString();

                    txtTaxableAmt.Text = dt.Rows[0]["TaxableAmount"].ToString();
                    //ddlExecutive.SelectedValue = dt.Rows[0]["Login_AutoID"].ToString();
                    ddlExpenseGroup.SelectedValue = dt.Rows[0]["Expgrp_AutoID"].ToString();

                    ddlPaymentMode.SelectedValue = dt.Rows[0]["PaymentMode_AutoID"].ToString();
                    if (dt.Rows[0]["PayMode"].ToString() == "--Select--" || dt.Rows[0]["PayMode"].ToString() == "Cash")
                    {
                        txtPayDetails.Enabled = false;
                    }
                    else
                    {
                        txtPayDetails.Enabled = true;
                    }
                    ddlExecutive.SelectedValue= dt.Rows[0]["Executive_ID"].ToString();

                    //ViewState["ImageUrl"] = dt.Rows[0]["ImagePath"].ToString();

                }
                ViewState["flag"] = 1;
                txtExpenseID.Focus();
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
                ObjExpense.Ex_AutoId = Convert.ToInt32(e.CommandArgument);
                AssignID();
                ObjExpense.Action = "Delete";
                int i = ObjExpense.Delete();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    if (ActionFlag == 1)
                    {
                        SearchByDate();
                    }
                    else if (ActionFlag == 2)
                    {
                        SearchAction();
                    }
                    else if (ActionFlag == 3)
                    {
                        SearchActionWithDate();
                    }                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
                return;
            }
        }        

        #region ----------------- Bind GridView -----------------
        private void bindGridview()
        {
            AssignID();
            ViewState["ExpenseDetails"] = ObjExpense.Get_Search();
            dt = (DataTable)ViewState["ExpenseDetails"];
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvExpenseDetails.Visible = true;
                gvExpenseDetails.DataSource = dt;
                gvExpenseDetails.DataBind();
                lblTotal.Text = dt.Compute("Sum(TotalAmount)", "").ToString();
            }
            else
            {
                gvExpenseDetails.Visible = false;
                lblTotal.Text = "0";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!','Information');", true);
            }

            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvExpenseDetails.Columns[0].Visible = true;
                gvExpenseDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvExpenseDetails.Columns[0].Visible = true;
                gvExpenseDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvExpenseDetails.Columns[0].Visible = true;
                gvExpenseDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvExpenseDetails.Columns[0].Visible = true;
                gvExpenseDetails.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvExpenseDetails.Columns[0].Visible = false;
                gvExpenseDetails.Columns[1].Visible = false;
            }

        }
        #endregion

        #region ---------- Search Button ----------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            flag1 = chkFromDateNotLessToDate();
            if (flag1 == 0)
            {
                SearchByDate();
                btnSearch.Focus();
            }                                
        }

        public void SearchByDate()
        {
            
            flag1 = chkFromDateNotLessToDate();
            if (flag1 == 0)
            {
                ActionFlag = 1;
                ObjExpense.Action = "BindDetails";
                ObjExpense.Category = "Get_By_Date";
                bindGridview();
            }

        }
        #endregion

        #region ----------- Search On Text Change ----------------
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                SearchAction();
                txtSearch.Focus();              
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void SearchAction()
        {
            try
            {
                ActionFlag = 2;
                ObjExpense.Action = "BindDetails";
                if (ddlSearch.SelectedValue.ToString() == "Expense Id")
                {
                    ObjExpense.Category = "Expense_Id";
                    ObjExpense.searchTxt = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "Name")
                {
                    ObjExpense.Category = "Name";
                    ObjExpense.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Expgrp_Name")
                {
                    ObjExpense.Category = "ExpenseGrp";
                    ObjExpense.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "PaymentMode")
                {
                    ObjExpense.Category = "PaymentMode";
                    ObjExpense.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Executive")
                {
                    ObjExpense.Category = "Executive";
                    ObjExpense.searchTxt = txtSearch.Text;
                }         
                bindGridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        #endregion

        #region -------------- Search Button By Date and Category-------
        protected void btnSearchWiDate_Click(object sender, EventArgs e)
        {
            flag1 = chkFromDateNotLessToDate();

            if (flag1 == 0)
            {
                if (ddlSearch.SelectedValue != "--Select--")
                {
                    if (txtSearch.Text != string.Empty)
                    {                        
                        SearchActionWithDate();
                        btnSearchWiDate.Focus();
                    }
                    else
                    {
                        txtSearch.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                        return;
                    }
                }
                else
                {
                    ddlSearch.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!.','Information');", true);
                    return;
                }

            }           
        }

        private void SearchActionWithDate()
        {
            try
            {
                ActionFlag = 3;
                ObjExpense.Action = "SerachDateWithCategory";
                if (ddlSearch.SelectedValue.ToString() == "Expense Id")
                {
                    ObjExpense.Category = "Date_Expense_Id";
                    ObjExpense.searchTxt = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "Name")
                {
                    ObjExpense.Category = "Date_Name";
                    ObjExpense.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Expgrp_Name")
                {
                    ObjExpense.Category = "Date_ExpenseGrp";
                    ObjExpense.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "PaymentMode")
                {
                    ObjExpense.Category = "Date_PaymentMode";
                    ObjExpense.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Executive")
                {
                    ObjExpense.Category = "Date_Executive";
                    ObjExpense.searchTxt = txtSearch.Text;
                }
                bindGridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
           // Get_ExpanseId();
        }

        protected void txtTaxableAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtTaxableAmt.Text == string.Empty)
            {
                txtTaxableAmt.Text = "0";
            }
            if (txtAmount.Text != string.Empty)
            {
                if (Convert.ToDouble(txtAmount.Text) >= Convert.ToDouble(txtTaxableAmt.Text))
                {
                    txtTotalAmt.Text = (Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(txtTaxableAmt.Text)).ToString();
                    txtNote1.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Tax Amount Should Not Be Greater Than Taxable Amount!!!.','Error');", true);
                    txtTaxableAmt.Focus();
                }
            }
            else
            {
                txtAmount.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Enter Taxable Amount!!!.','Error');", true);
            }
        }
     
        protected void btnVouchaer_Command(object sender, CommandEventArgs e)
        {
            int Ex_AutoId = Convert.ToInt32(e.CommandArgument);            
            Response.Redirect("ExpenseVoucher.aspx?Ex_AutoId=" + Ex_AutoId);
            //string strPopup = "<script language='javascript' ID='script1'>"
            //  + "window.open('ExpenseVoucher.aspx?Ex_AutoId=" + Ex_AutoId
            //  + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=870,height=650')"
            //  + "</script>";
        
            //ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                        
        }

        #region --------------- Page Indexing --------------------------
        protected void gvExpenseDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpenseDetails.PageIndex = e.NewPageIndex;
            if (ActionFlag == 1)
            {
                SearchByDate();
            }
            else if (ActionFlag == 2)
            {
                SearchAction();
            }
            else if (ActionFlag == 3)
            {
                SearchActionWithDate();
            }                    
        }
        #endregion

        protected void txtAmount_TextChanged1(object sender, EventArgs e)
        {
            if (txtAmount.Text != string.Empty)
            {
                if (txtTaxableAmt.Text != string.Empty)
                {                    
                    txtTotalAmt.Text = (Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(txtTaxableAmt.Text)).ToString();                                      
                    txtTaxableAmt.Focus();                    
                }
                else
                {
                    txtTotalAmt.Text = txtAmount.Text;
                    txtTaxableAmt.Focus();
                }
            }           
        }
        bool chkExistingExpenseId = false;
        protected void txtExpenseID_TextChanged(object sender, EventArgs e)
        {
            AssignID();
            if (txtExpenseID.Text != string.Empty)
            {
                ObjExpense.Ex_Id = Convert.ToInt32(txtExpenseID.Text);

                chkExistingExpenseId = ObjExpense.chkExistingExpenseId();
                if (chkExistingExpenseId == true)
                {
                    Get_ExpanseId();
                    txtExpenseID.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enquiry Id Is Already Exists !!!','Error');", true);
                    return;
                }
                else
                {
                    txtDate.Focus();
                }
            }
            else
            {
                txtExpenseID.Focus();
            }
        }
        
        #region ------------- Check From Date And To Date Validation
        int flag1 = 0;
        protected int chkFromDateNotLessToDate()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                flag1 = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag1 = 1;
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
                    flag1 = 0;
                    ObjExpense.StartDate = FromDate;
                    ObjExpense.EndDate = ToDate;
                }
                else
                {
                    flag1 = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                }
            }

            return flag1;

        }
        #endregion

        #region -------- Export To Excle --------------
        protected void btnExpord_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToExcel()
        {
            try
            {

                if (ViewState["ExpenseDetails"] != null)
                {
                    dt = (DataTable)ViewState["ExpenseDetails"];
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=ExpenseDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvExpenseDetails.Columns[0].Visible = false;
                            gvExpenseDetails.Columns[1].Visible = false;
                            gvExpenseDetails.Columns[2].Visible = false;
                            gvExpenseDetails.AllowPaging = false;

                            gvExpenseDetails.DataSource = dt;
                            gvExpenseDetails.DataBind();
                            gvExpenseDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvExpenseDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvExpenseDetails.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvExpenseDetails.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                    List<Control> controls = new List<Control>();
                                    //Add controls to be removed to Generic List
                                    foreach (Control control in cell.Controls)
                                    {
                                        controls.Add(control);
                                    }

                                    foreach (Control control in controls)
                                    {
                                        switch (control.GetType().Name)
                                        {
                                            case "HyperLink":
                                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                                break;

                                            case "LinkButton":
                                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                                break;

                                        }
                                        cell.Controls.Remove(control);

                                    }
                                }
                            }


                            gvExpenseDetails.GridLines = GridLines.Both;
                            gvExpenseDetails.RenderControl(hw);

                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Data GridVIew Is Empty , Can Not Export !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Data GridVIew Is Empty , Can Not Export !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        #endregion

       


    }
 }