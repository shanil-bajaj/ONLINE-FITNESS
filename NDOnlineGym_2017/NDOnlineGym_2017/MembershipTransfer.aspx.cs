using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class MembershipTransfer : System.Web.UI.Page
    {

        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalMembershipTransfer ObjTrans = new BalMembershipTransfer();
        BalExpense ObjExpense = new BalExpense();
        DataTable dataTable = new DataTable();
        DataTable dataTable1 = new DataTable();
        DataSet dataSet = new DataSet();
        int OldMember_ID = 0;
        static int OldMember_AutoID;
        static int NewMember_AutoId;
        static int NewMember_Id1;
        int flag = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["TransferDetails"] != null)
                    {
                        //txtSearch.Enabled = false;
                        divsearch.Visible = true;
                        divFormDetails.Visible = false;
                        divMembershipTransfer.Visible = false;
                        divMembershipTransferDetails.Visible = true;
                        btnSearch.Focus();
                        Assign_MonthDate();
                        SearchByDateFunction();
                    }
                    else
                    {
                        txtOldMemberId.Focus();
                        paymentdiv.Visible = false;
                        //txtSearch.Enabled = false;
                        //txtTransferDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        AssignTransferDate();
                        getTransferReceiptID();
                        bindDDLExecutive();
                        setExecutive();
                        Bind_PaymentType();
                        
                        // From Termination 
                        if (Request.QueryString["MemberID"] != null)
                        {
                            if (Request.QueryString["FNameMemDetails"] != null)
                            {
                                ObjTrans.OldMember_ID1 = Convert.ToInt32(Request.QueryString["MemberID"]);
                                OldMember_ID = getMemIDByAutoID(); 
                            }
                            else
                            {
                                OldMember_ID = Convert.ToInt32(Request.QueryString["MemberID"]);
                            }

                            ObjTrans.OldMember_ID1 = OldMember_ID;
                            ObjTrans.Action = "SearchByMember_ID";
                            BindMemberDetails();
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private int getMemIDByAutoID()
        {
            AssignID();
            ObjTrans.Action = "GetMemIdByAutoId";
            dataSet = ObjTrans.GetDetails();
            if (dataSet.Tables[0].Columns.Count > 0)
            {
                OldMember_ID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Member_ID1"].ToString());
            }

            return OldMember_ID;           
        }

        #region ------------ Assign Month Start And End Date --------------
        private void Assign_MonthDate()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            
            DateTime firstDayOfMonth = new DateTime(localTime.Year, localTime.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            txtFromDate.Text = firstDayOfMonth.ToString("dd-MM-yyyy");
            txtToDate.Text = lastDayOfMonth.ToString("dd-MM-yyyy");

            //ObjTrans.MStartDate = firstDayOfMonth;
            //ObjTrans.MEndDate = lastDayOfMonth;
        }
        #endregion

        #region ----------- Assign Upgrade Date -------------
        protected void AssignTransferDate()
        {
            try
            {
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local                   
                txtTransferDate.Text = localTime.ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------- Assign Comp and Branch ID-------------------------
        private void AssignID()
        {
            try
            {
                ObjTrans.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                ObjTrans.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                ObjTrans.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ----------- Get Transfer Receipt ID ------------
        private void getTransferReceiptID()
        {
            try
            {
                AssignID();
                ObjTrans.Action = "Get_ReceiptID";
                dataTable= ObjTrans.SelectDetails();
                if(dataTable.Rows.Count > 0)
                {
                    txtTransReceiptid.Text = dataTable.Rows[0]["TransReceiptID"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------- Bind Executive DDL -------------------------------
        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                dataTable = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dataTable.Rows.Count != 0)
                {
                    ddlExecutive.DataSource = dataTable;
                    ddlExecutive.Items.Clear();
                    ddlExecutive.DataValueField = "Staff_AutoID";
                    ddlExecutive.DataTextField = "Name";
                    ddlExecutive.DataBind();
                    //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff. !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- Set Executive -----------------------------------
        public void setExecutive()
        {
            try
            {
                obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                dataTable = obBalStaffRegistration.GetExecutiveByID_ByBranch();
                if (dataTable.Rows.Count > 0)
                {
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                    ddlExecutive.SelectedItem.Text = dataTable.Rows[0]["Name"].ToString();
                }
                else
                {
                    dataTable = obBalStaffRegistration.GetExecutiveByID_WithoutBranch();
                    string staffid = dataTable.Rows[0]["Staff_AutoID"].ToString();
                    string staffnm = dataTable.Rows[0]["Name"].ToString();
                    ddlExecutive.Items.Insert(0, new ListItem(staffnm, staffid));
                    ddlExecutive.SelectedItem.Text = staffnm;
                    ddlExecutive.SelectedValue = staffid;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region ------------ Bind Payment Type ----------------------------
        public void Bind_PaymentType()
        {
            try
            {
                ObjExpense.company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dataTable = ObjExpense.Get_PaymentType();

                if (dataTable.Rows.Count > 0)
                {
                    ddlPaymentType.DataSource = dataTable;
                    ddlPaymentType.Items.Clear();
                    ddlPaymentType.DataValueField = "PaymentMode_AutoID";
                    ddlPaymentType.DataTextField = "Name";
                    ddlPaymentType.DataBind();
                    ddlPaymentType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Payment Type. !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Meber ID Text Change Event --------------------
        protected void txtOldMemberId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtOldMemberId.Text != string.Empty)
                {                    
                    ObjTrans.OldMember_ID1 = Convert.ToInt32(txtOldMemberId.Text);
                    ObjTrans.Action = "SearchByMember_ID";
                    BindMemberDetails();
                    txtOldMemberId.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- Meber Contact Text Change Event -----------------
        protected void txtOldContactNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtOldContactNo.Text != string.Empty)
                {
                    ObjTrans.OldContact1 = txtOldContactNo.Text;
                    ObjTrans.Action = "SearchByMember_Contact1";
                    BindMemberDetails();
                    txtOldContactNo.Focus();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------------- Get Member Details ----------------
        private void BindMemberDetails()
        {
            try
            {
                AssignID();
                dataSet = ObjTrans.GetDetails();
                dataSet.Tables[0].TableName = "OldMemberInfo";

                if (dataSet.Tables["OldMemberInfo"].Rows.Count != 0)
                {
                    OldMember_AutoID = Convert.ToInt32(dataSet.Tables["OldMemberInfo"].Rows[0]["Member_AutoID"].ToString());
                    OldMember_ID = Convert.ToInt32(dataSet.Tables["OldMemberInfo"].Rows[0]["Member_ID1"].ToString());

                    if (dataSet.Tables["OldMemberInfo"].Rows[0]["BlockStatus"].ToString() != "Block")
                    {
                        txtOldMemberId.Text = dataSet.Tables["OldMemberInfo"].Rows[0]["Member_ID1"].ToString();
                        txtOldFirstName.Text = dataSet.Tables["OldMemberInfo"].Rows[0]["OldFName"].ToString();
                        txtOldLastName.Text = dataSet.Tables["OldMemberInfo"].Rows[0]["OldLName"].ToString();
                        txtOldContactNo.Text = dataSet.Tables["OldMemberInfo"].Rows[0]["OldContact1"].ToString();

                        //txtNewFirstName.Focus();

                        ObjTrans.OldMember_ID1 = OldMember_ID;
                        ObjTrans.Action = "CourseBy_MemberID";

                        dataTable = ObjTrans.SelectDetails();
                        if (dataTable.Rows.Count > 0)
                        {
                            ddlReceiptNo.DataSource = dataTable;
                            ddlReceiptNo.Items.Clear();
                            ddlReceiptNo.DataValueField = "ReceiptID";
                            ddlReceiptNo.DataTextField = "ReceiptID";
                            ddlReceiptNo.DataBind();
                            ddlReceiptNo.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Course Not Assign !!!','Information');", true);
                        }

                    }
                    else
                    {                        
                        string url = "Termination.aspx?Member_ID=" + OldMember_ID + " &FNameTransferPage=" + HttpUtility.UrlEncode("MemberTransfer");
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "text", "showConfirmation('" + url + "')", true);
                    }
                }
                else
                {
                    ClearAllForm();
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

        #region ---------- Clear Form Function ------------
        private void ClearAllForm()
        {
            txtOldMemberId.Text = string.Empty;
            txtOldMemberId.Focus();
            txtOldFirstName.Text = string.Empty;
            txtOldLastName.Text = string.Empty;
            txtOldContactNo.Text = string.Empty;

            txtNewFirstName.Text = string.Empty;
            txtNewLastName.Text = string.Empty;
            txtNewContact1.Text = string.Empty;
            ddlNewGender.SelectedIndex = 0;
            txtDOB.Text = string.Empty;
            setExecutive();

            ddlReceiptNo.Items.Clear();
            ddlReceiptNo.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            ddlReceiptNo.SelectedIndex = 0;

            txtTransferFees.Text = "0";
           // txtTransferDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            AssignTransferDate();
            gvCourseDetails.DataSource = null;
            gvCourseDetails.DataBind();
            gvCourseDetails.Visible = false;
            
            addPayment.Visible = true;
            ddlPaymentType.Enabled = true;
            paymentdiv.Visible = false;
            lblTotalFeeDue.Text = "0";
            lblPaidFee.Text = "0";
            lblBalance.Text = "0";
            
            ViewState["dataTable1"] = null;
            gvTransferPayment.DataSource = null;
            gvTransferPayment.DataBind();

            ddlTaxType.SelectedIndex = 0;

            ViewState["TransDetails"] = null;
            OldMember_ID = 0;
            OldMember_AutoID=0;
            NewMember_AutoId=0;
            NewMember_Id1=0;
            flag = 0;

        }
        #endregion

        #region ----------- Check New Contact Number Exist Or Not
        protected void txtNewContact1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtNewContact1.Text != "")
                {
                    Chk_ContactNoExist_Not(txtNewContact1.Text);
                    ddlNewGender.Focus();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        private void Chk_ContactNoExist_Not(string contact1)
        {
            try
            {
                AssignID();
                ObjTrans.Action = "Chk_ExistContct1";
                ObjTrans.Contact1 = contact1;

                dataTable = ObjTrans.SelectDetails();

                if (dataTable.Rows.Count > 0)
                {
                    NewMember_Id1 = Convert.ToInt32(dataTable.Rows[0]["Member_ID1"].ToString());
                    NewMember_AutoId = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                    txtNewFirstName.Text = dataTable.Rows[0]["FName"].ToString();
                    txtNewLastName.Text = dataTable.Rows[0]["LName"].ToString();
                    ddlNewGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();

                    string dob = dataTable.Rows[0]["DOB"].ToString();
                    if (dob != string.Empty)
                    {
                        DateTime DOB;
                        if (DateTime.TryParseExact(dob, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOB))
                        {
                        }

                        txtDOB.Text = DOB.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        txtDOB.Text = "";
                    }

                }
                else
                {
                    NewMember_Id1 = 0;
                    NewMember_AutoId = 0;

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        #endregion

        #region --------- Executive Check box CheckedChanged -------------------
        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkExecutive.Checked == true)
                {
                    ddlExecutive.Enabled = false;
                    chkExecutive.Focus();
                }
                else
                {
                    ddlExecutive.Enabled = true;
                    ddlExecutive.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Receipt Change DDL ---------------
        protected void ddlReceiptNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlReceiptNo.SelectedValue != "--Select--")
                {
                    AssignID();

                    ObjTrans.ReceiptID = Convert.ToInt32(ddlReceiptNo.SelectedItem.Value);
                    ObjTrans.Action = "CourseBy_ReceiptNo";
                    dataTable = ObjTrans.SelectDetails();

                    if (dataTable.Rows.Count > 0)
                    {
                        gvCourseDetails.Visible = true;
                        gvCourseDetails.DataSource = dataTable;
                        gvCourseDetails.DataBind();
                    }

                }
                ddlReceiptNo.Focus();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region -------------- Add Payment Mode Button ---------------
        public int k = 0;
        protected void addPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentType.SelectedValue != "--Select--")
                {
                    addPayment.Visible = false;
                    lblPaidFee.Text = "0";
                    lblBalance.Text = "0";
                    //txtTransferFees.Enabled = false;
                    ddlPaymentType.Enabled = false;
                    paymentdiv.Visible = true;
                    lblTotalFeeDue.Text = txtTransferFees.Text;
                    txtNextPaymentDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");

                    DataRow dr1 = null;
                    //dt.Clear();
                    dataTable1.Columns.Add(new DataColumn("Bal_Auto"));
                    dataTable1.Columns.Add(new DataColumn("PaymentMode"));
                    dataTable1.Columns.Add(new DataColumn("Cardno"));
                    dataTable1.Columns.Add(new DataColumn("payDate"));
                    dataTable1.Columns.Add(new DataColumn("CardExpirydate"));
                    dataTable1.Columns.Add(new DataColumn("BankName"));
                    dataTable1.Columns.Add(new DataColumn("BranchName"));
                    dataTable1.Columns.Add(new DataColumn("Paid"));
                    dataTable1.Columns.Add(new DataColumn("TaxType"));
                    dataTable1.Columns.Add(new DataColumn("taxpec"));
                    dataTable1.Columns.Add(new DataColumn("TaxValue"));
                    dataTable1.Columns.Add(new DataColumn("PaidWithTax"));


                    k = dataTable1.Rows.Count;
                    dr1 = dataTable1.NewRow();

                    dr1["Bal_Auto"] = k;
                    dr1["PaymentMode"] = ddlPaymentType.SelectedItem.Text;
                    dr1["Cardno"] = "";
                    dr1["payDate"] = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                    dr1["CardExpirydate"] = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                    dr1["BankName"] = "";
                    dr1["BranchName"] = "";
                    dr1["Paid"] = "0";
                    dr1["TaxType"] = ddlTaxType.SelectedValue.ToString();

                    if (ddlTaxType.SelectedValue == "Including")
                    {
                        AssignID();
                        ObjTrans.Action = "Get_including_Tax";
                        dataTable = ObjTrans.SelectDetails(); ;
                        if (dataTable.Rows.Count > 0)
                        {
                            dr1["taxpec"] = dataTable.Rows[0]["TaxPercent"].ToString();

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Insert Tax In Master !!!','Error');", true);
                        }
                    }
                    else
                    {
                        dr1["taxpec"] = "";
                    }


                    dr1["TaxValue"] = "";
                    dr1["PaidWithTax"] = "";

                    dataTable1.Rows.InsertAt(dr1, k);
                    k++;

                    ViewState["dataTable1"] = dataTable1;
                    gvTransferPayment.DataSource = dataTable1;
                    gvTransferPayment.DataBind();

                    TextBox txtNumber = (TextBox)gvTransferPayment.Rows[gvTransferPayment.Rows.Count - 1].FindControl("txtNumber");
                    txtNumber.Focus();

                    TextBox txtTaxPer = (TextBox)gvTransferPayment.Rows[gvTransferPayment.Rows.Count - 1].FindControl("txtTax");
                    if (ddlTaxType.SelectedValue == "Including")
                    {
                        txtTaxPer.Enabled = false;
                    }

                }
                else
                {
                    ddlPaymentType.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.error('Select Payment Type !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region ---------------- Remove Transfer Payment Grid ---------------
        protected void gvTransferPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);

                addPayment.Visible = true;
                //txtTransferFees.Enabled = true;
                ddlPaymentType.Enabled = true;
                paymentdiv.Visible = false;
                lblTotalFeeDue.Text = "0";
                lblPaidFee.Text = "0";
                lblBalance.Text = "0";

                dataTable1 = (DataTable)ViewState["dataTable1"];                

                dataTable1.Rows[index].Delete();

                ViewState["dataTable1"] = dataTable1;

                gvTransferPayment.DataSource = dataTable1;
                gvTransferPayment.DataBind();
                        

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region -------------- Paid Amount TextChange ------------
        double PaidFees = 0;
        protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
                dataTable1 = (DataTable)ViewState["dataTable1"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr1;
                dr1 = dataTable1.NewRow();

                TextBox txtPaymentMode = (TextBox)currentRow.FindControl("txtPaymentMode");
                TextBox txtNumber = (TextBox)currentRow.FindControl("txtNumber");
                TextBox txtDate = (TextBox)currentRow.FindControl("txtDate");
                TextBox txtExpiryDate = (TextBox)currentRow.FindControl("txtExpiryDate");
                TextBox txtBankName = (TextBox)currentRow.FindControl("txtBankName");
                TextBox txtBranchName = (TextBox)currentRow.FindControl("txtBranchName");
                TextBox txtPaidAmount = (TextBox)currentRow.FindControl("txtPaidAmount");
                TextBox txtTaxType = (TextBox)currentRow.FindControl("txtTaxType");
                TextBox txtTax = (TextBox)currentRow.FindControl("txtTax");
                TextBox Txtvalue = (TextBox)currentRow.FindControl("Txtvalue");
                TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");

                double lblTransFees = Convert.ToDouble(lblTotalFeeDue.Text);
                //lblPaidFee.Text = txtPaidAmount.Text;
                //int lblPaidFees = Convert.ToInt32(lblPaidFee.Text);
                //lblBalance.Text =Convert.ToString(lblTransFees - lblPaidFees);

                int TotalTransFess = Convert.ToInt32(txtTransferFees.Text);

                DateTime PayDate;
                if (DateTime.TryParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out PayDate))
                {
                }

                DateTime ExpiryDate;
                if (DateTime.TryParseExact(txtExpiryDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ExpiryDate))
                {
                }

                int s = currentRow.RowIndex;

                if (Convert.ToInt32(txtPaidAmount.Text) <= TotalTransFess)
                {
                    dr1["Bal_Auto"] = s;

                    if (txtPaymentMode.Text != string.Empty)
                    {
                        dr1["PaymentMode"] = txtPaymentMode.Text;
                    }
                    else
                    {
                        dr1["PaymentMode"] = "";
                    }

                    if (txtNumber.Text != string.Empty)
                    {
                        dr1["Cardno"] = txtNumber.Text;
                    }
                    else
                    {
                        dr1["Cardno"] = "";
                    }

                    if (PayDate.ToString("dd-MM-yyyy") != string.Empty)
                    {
                        dr1["payDate"] = PayDate.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        dr1["payDate"] = "";
                    }

                    if (ExpiryDate.ToString("dd-MM-yyyy") != string.Empty)
                    {
                        dr1["CardExpirydate"] = ExpiryDate.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        dr1["CardExpirydate"] = "";
                    }

                    if (txtBankName.Text != string.Empty)
                    {
                        dr1["BankName"] = txtBankName.Text;
                    }
                    else
                    {
                        dr1["BankName"] = "";
                    }

                    if (txtBranchName.Text != string.Empty)
                    {
                        dr1["BranchName"] = txtBranchName.Text;
                    }
                    else
                    {
                        dr1["BranchName"] = "";
                    }

                    if (txtPaidAmount.Text != string.Empty)
                    {
                        dr1["Paid"] = txtPaidAmount.Text;
                    }
                    else
                    {
                        dr1["Paid"] = "";
                    }

                    if (txtTaxType.Text != string.Empty)
                    {
                        dr1["TaxType"] = txtTaxType.Text;
                    }
                    else
                    {
                        dr1["TaxType"] = "";
                    }

                    if (txtTax.Text != string.Empty)
                    {
                        dr1["taxpec"] = txtTax.Text;
                    }
                    else
                    {
                        dr1["taxpec"] = "";
                    }

                    double Taxvalue;

                    if (ddlTaxType.Text == "Excluding")
                    {
                        if (txtTax.Text != "")
                        {
                            Taxvalue = (Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100;
                            dr1["TaxValue"] = Taxvalue.ToString();
                            dr1["PaidWithTax"] = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                            PaidFees += Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;

                            double temp = Convert.ToDouble(lblTotalFeeDue.Text);
                            double duefees = temp + Taxvalue;
                            lblPaidFee.Text = Convert.ToString(PaidFees);
                            lblBalance.Text = Convert.ToString(duefees - PaidFees);
                        }
                        else
                        {

                        }


                        //    lblTotalFeeDue.Text = duefees.ToString();


                    }
                    else
                    {
                        txtTax.Enabled = false;
                        double taxPercent = 0, x = 0;
                        PaidFees = Convert.ToDouble(txtPaidAmount.Text);
                        taxPercent = Convert.ToDouble(txtTax.Text);

                        x = (100 * PaidFees) / (100 + taxPercent);
                        Taxvalue = PaidFees - x;
                        dr1["TaxValue"] = Taxvalue.ToString("#,0.00");
                        dr1["PaidWithTax"] = txtPaidAmount.Text;

                        lblPaidFee.Text = Convert.ToString(PaidFees);
                        lblBalance.Text = Convert.ToString(lblTransFees - PaidFees);


                    }


                    //if (txtTotalAmount.Text != string.Empty)
                    //{
                    //    dr1["PaidWithTax"] = txtTotalAmount.Text;
                    //}
                    //else
                    //{
                    //    dr1["PaidWithTax"] = "";
                    //}

                    dataTable1.Rows[s].Delete();
                    dataTable1.Rows.InsertAt(dr1, s);
                    ViewState["dataTable1"] = dataTable1;
                    gvTransferPayment.DataSource = dataTable1;
                    gvTransferPayment.DataBind();

                    TextBox txtTaxPer = (TextBox)gvTransferPayment.Rows[gvTransferPayment.Rows.Count - 1].FindControl("txtTax");
                    if (ddlTaxType.SelectedValue == "Including")
                    {
                        txtTaxPer.Enabled = false;
                    }

                }
                else
                {
                    txtPaidAmount.Text = "0";
                    txtPaidAmount.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error(Paid Fees less than Total Fees !!!','Error');", true);
                }


               
                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion 

        #region ------------ Save Button Event ---------------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flag = 0;

                if (txtOldMemberId.Text ==string.Empty)
                {
                    flag = 1;
                    txtOldMemberId.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter Member Id !!!','Error');", true);
                }
                else if (txtOldContactNo.Text == string.Empty)
                {
                    flag = 1;
                    txtOldContactNo.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter Member Contact !!!','Error');", true);
                }
                else if (txtNewFirstName.Text == string.Empty)
                {
                    flag = 1;
                    txtNewFirstName.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter New Member Name !!!','Error');", true);
                }
                else if (txtNewContact1.Text == string.Empty)
                {
                    flag = 1;
                    txtNewContact1.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter Member Contact !!!','Error');", true);
                }
                else if (ddlNewGender.SelectedValue == "--Select--")
                {
                    flag = 1;
                    ddlNewGender.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Select Gender !!!','Error');", true);
                }
                else if (ddlReceiptNo.SelectedValue == "--Select--")
                {
                    flag = 1;
                    ddlReceiptNo.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Select Course Receipt !!!','Error');", true);
                }
                else if (txtTransferDate.Text == string.Empty)
                {
                    flag = 1;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter Transfer Date !!!','Error');", true);
                }
                else if (txtTransferFees.Text != string.Empty)
                {
                    if (Convert.ToInt32(txtTransferFees.Text) > 0)
                    {

                        if (ddlPaymentType.SelectedValue != "--Select--")
                        {
                            double paidAmt=0.0;
                            if (gvTransferPayment.Rows.Count > 0)
                            {
                                foreach (GridViewRow row in gvTransferPayment.Rows)
                                {
                                    TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");

                                    if (txtPaidAmount.Text != "")
                                    {
                                        paidAmt = Convert.ToDouble(txtPaidAmount.Text);
                                    }
                                }

                                if (paidAmt < Convert.ToDouble(txtTransferFees.Text))
                                {
                                    flag = 1;
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Please Pay All Transfer Fees !!!','Error');", true);
                                }
                                else if(paidAmt > Convert.ToDouble(txtTransferFees.Text))
                                {
                                    flag = 1;
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('You Can Not Pay Over Transfer Fees !!!','Error');", true);
                                }

                            }
                            else
                            {
                                flag = 1;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Please Pay All Transfer Fees !!!','Error');", true);
                            }
                        }
                        else
                        {
                            flag = 1;
                            ddlPaymentType.Focus();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Please Select Payment Mode !!!','Error');", true);
                        }
                    }

                }
                else
                {
                    DateTime todayDate = DateTime.UtcNow;
                    int count = 0;
                    if (gvCourseDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvCourseDetails.Rows)
                        {
                            string CourseEndDate = row.Cells[5].Text.Trim();

                            DateTime courseEndDate;
                            if (DateTime.TryParseExact(CourseEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out courseEndDate))
                            {
                            }

                            if (courseEndDate.Date > todayDate.Date)
                            {
                                flag = 0;
                                count = 1;
                                break;
                            }

                        }

                        if (count == 0)
                        {
                            flag = 1;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Cant Not Transfer Because All Course Expired !!!','Error');", true);
                        }

                    }

                }
                //else
                if(flag == 0 )
                {
                    AssignID();

                    if (btnSave.Text == "Save")
                    {
                        AddParameter();
                        ObjTrans.Action = "INSERT";
                        int res= ObjTrans.Insert_Update_Record();

                        if (res > 0)
                        {
                            ClearAllForm();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);

                            if (Request.QueryString["FNameMemDetails"] != null)
                            {
                                int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                                Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                        }


                       
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

        #region --------------- Add Parameter ---------------
        private void AddParameter()
        {
            try
            {
                ObjTrans.OldMember_AutoID = OldMember_AutoID;
                ObjTrans.NewMember_FName = txtNewFirstName.Text;
                ObjTrans.NewMember_LName = txtNewLastName.Text;
                ObjTrans.Contact1 = txtNewContact1.Text;
                ObjTrans.Gender = ddlNewGender.SelectedValue;

                if (NewMember_AutoId > 0)
                {
                    ObjTrans.NewMember_AutoID = NewMember_AutoId;
                }

                if (NewMember_Id1 > 0)
                {
                    ObjTrans.NewMember_ID1 = NewMember_Id1;
                }

                if (txtDOB.Text != string.Empty)
                {
                    DateTime DOB;
                    if (DateTime.TryParseExact(txtDOB.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOB))
                    {
                    }
                    ObjTrans.DOB = DOB;
                }
                
                ObjTrans.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
                ObjTrans.ReceiptID = Convert.ToInt32(ddlReceiptNo.SelectedValue);
                ObjTrans.ReceiptStstus="Transfer";
                
                if (txtTransReceiptid.Text != string.Empty)
                {
                    ObjTrans.TransReceiptID = Convert.ToInt32(txtTransReceiptid.Text);
                }
                else
                {
                    getTransferReceiptID();
                }

                if (txtTransferDate.Text != string.Empty)
                {
                    DateTime TransferDate;
                    if (DateTime.TryParseExact(txtTransferDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TransferDate))
                    {
                    }
                    ObjTrans.Transfer_Date = TransferDate;
                }

                if (txtTransferFees.Text != string.Empty)
                {
                    ObjTrans.Transfer_Fees = Convert.ToDouble(txtTransferFees.Text);
                }
                else
                {
                    ObjTrans.Transfer_Fees = Convert.ToDouble(0);
                }
                //ObjTrans.PaymentMode = ddlPaymentType.SelectedValue;


                if (gvTransferPayment.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvTransferPayment.Rows)
                    {
                        TextBox txtPaymentMode = (TextBox)row.FindControl("txtPaymentMode");
                        ObjTrans.PaymentMode = txtPaymentMode.Text;

                        TextBox txtDate = (TextBox)row.FindControl("txtDate");

                        DateTime txtDate1;
                        if (DateTime.TryParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out txtDate1))
                        {
                            ObjTrans.payDate = txtDate1;
                        }


                        TextBox txtBankName = (TextBox)row.FindControl("txtBankName");
                        TextBox txtBranchName = (TextBox)row.FindControl("txtBranchName");
                        TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                        TextBox txtTaxType = (TextBox)row.FindControl("txtTaxType");
                        TextBox txtTax = (TextBox)row.FindControl("txtTax");
                        TextBox Txtvalue = (TextBox)row.FindControl("Txtvalue");
                        TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");


                        if (txtPaymentMode.Text == "Cash")
                        {
                            ObjTrans.Cardno = "";
                            ObjTrans.CardExpirydate = null;
                            ObjTrans.BankName = "";
                            ObjTrans.BranchName = "";
                        }
                        else if (txtPaymentMode.Text == "NEFT" || txtPaymentMode.Text == "RTGS")
                        {
                            ObjTrans.CardExpirydate = null;
                        }
                        else
                        {
                            TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                            ObjTrans.Cardno = txtNumber.Text;

                            TextBox txtExpiryDate = (TextBox)row.FindControl("txtExpiryDate");
                            DateTime txtExpiryDate1;
                            if (DateTime.TryParseExact(txtExpiryDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out txtExpiryDate1))
                            {

                                ObjTrans.CardExpirydate = txtExpiryDate1;
                            }

                            ObjTrans.BankName = txtBankName.Text;
                            ObjTrans.BranchName = txtBranchName.Text;

                        }

                        if (txtPaidAmount.Text != "")
                        {
                            ObjTrans.Paid = Convert.ToDouble(txtPaidAmount.Text);
                        }
                        else
                        {
                            ObjTrans.Paid = 0;
                        }

                        ObjTrans.TaxType = txtTaxType.Text;

                        if (txtTax.Text != "")
                        {
                            ObjTrans.taxpec = Convert.ToDouble(txtTax.Text);
                        }
                        else
                        {
                            ObjTrans.taxpec = 0;
                        }

                        if (Txtvalue.Text != "")
                        {
                            ObjTrans.TaxValue = Convert.ToDouble(Txtvalue.Text);
                        }
                        else
                        {
                            ObjTrans.TaxValue = 0;
                        }

                        if (txtTotalAmount.Text != "")
                        {
                            ObjTrans.PaidWithTax = Convert.ToDouble(txtTotalAmount.Text);
                        }
                        else
                        {
                            ObjTrans.PaidWithTax = 0;
                        }

                    }

                    ObjTrans.Comment = txtComment.Text;

                    DateTime NextPayDate;
                    if (DateTime.TryParseExact(txtNextPaymentDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NextPayDate))
                    {

                        ObjTrans.NextBalDate = NextPayDate;
                    }

                    ObjTrans.Balance_Fees = Convert.ToDouble(lblBalance.Text);

                }
               

                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ----------- Transfer Receipt Id Text Change ---------------
        protected void txtTransReceiptid_TextChanged(object sender, EventArgs e)
        {
            chkTransReceiptID_Exists_Or_Not();
        }
        #endregion

        #region ----------- Transfer Receipt Exist Or Not ---------------
        bool chkExistingTransReceiptId = false;
        private void chkTransReceiptID_Exists_Or_Not()
        {
            try
            {
                if (txtTransReceiptid.Text != "")
                {
                    AssignID();
                    ObjTrans.Action = "Chk_ReceiptID_Exist";
                    ObjTrans.TransReceiptID = Convert.ToInt32(txtTransReceiptid.Text);

                    chkExistingTransReceiptId = ObjTrans.TransReceiptIDExist_OR_Not();

                    if (chkExistingTransReceiptId ==true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Receipt ID Allready Exists !!!','Error');", true);
                        txtTransReceiptid.Text = "";
                        getTransferReceiptID();
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

        #region ---------- Clear Button Event -------------
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllForm();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------- Check From Date And To Date Validation
        protected int chkFromDateNotLessToDate()
        {           
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
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
                    flag = 0;
                    ObjTrans.MStartDate = FromDate;
                    ObjTrans.MEndDate = ToDate;
                }
                else
                {
                    flag = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                }
            }

            return flag;

        }
        #endregion

        #region ---------- Search Button by Date ----------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchByDateFunction();                               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }


        protected void SearchByDateFunction()
        {
            try
            {
                flag = chkFromDateNotLessToDate();

                if (flag == 0)
                {
                    AssignID();
                    ObjTrans.Action = "GetTransferDetails";
                    ObjTrans.Category = "Get_By_Date";
                    bindTransDetailsGridView();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion
                       
        #region -------------- Search Button By Date and Category-------
        protected void btnSearchCategory_Click(object sender, EventArgs e)
        {
            try
            {
                flag = chkFromDateNotLessToDate();

                if (flag == 0)
                {
                    if (ddlcategory.SelectedValue.ToString() != "--Select--")
                    {
                        if (txtSearch.Text != string.Empty)
                        {                        
                            AssignID();
                            SeacrhAction();
                            bindTransDetailsGridView();
                        }
                        else
                        {
                            txtSearch.Focus();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter Data On Search Field !!!','Information');", true);
                        }

                    }
                    else
                    {
                        ddlcategory.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Categry !!!','Information');", true);
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

        #region ------------ Search On Text Change ----------------
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
             try
            {
                if (txtSearch.Text != string.Empty)
                {
                    AssignID();
                    ObjTrans.Action = "GetTransferDetails";

                    if (ddlcategory.SelectedValue.ToString() == "New_Member_ID")
                    {
                        ObjTrans.Category = "New_Member_ID";
                        ObjTrans.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "Old_Member_ID")
                    {
                        ObjTrans.Category = "Old_Member_ID";
                        ObjTrans.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "New_Member_Name")
                    {
                        ObjTrans.Category = "New_Member_Name";
                        ObjTrans.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "Old_Member_Name")
                    {
                        ObjTrans.Category = "Old_Member_Name";
                        ObjTrans.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "New_Member_Contact")
                    {
                        ObjTrans.Category = "New_Member_Contact";
                        ObjTrans.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "Old_Member_Contact")
                    {
                        ObjTrans.Category = "Old_Member_Contact";
                        ObjTrans.SearchByText = txtSearch.Text;
                    }                    
                    else if (ddlcategory.SelectedValue.ToString() == "Executive")
                    {
                        ObjTrans.Category = "Executive";
                        ObjTrans.SearchByText = txtSearch.Text;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!','Information');", true);
                        ddlcategory.Focus();
                        return;
                    }

                    bindTransDetailsGridView();
                    btnSearchCategory.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!','Information');", true);
                }

            }
             catch (Exception ex)
             {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                 ErrorHandiling.SendErrorToText(ex);
             }

        }
        #endregion

        #region ----------- Bind Transfer Details Gridview -------------

        private void bindTransDetailsGridView()
        {
            dataTable = ObjTrans.SelectDetails();
            ViewState["TransDetails"] = dataTable;
            lblCount.Text = Convert.ToString(dataTable.Rows.Count);
            if (dataTable.Rows.Count > 0)
            {
                gvMemberTransferDetails.DataSource = dataTable;
                gvMemberTransferDetails.DataBind();
            }
            else
            {
                gvMemberTransferDetails.DataSource = dataTable;
                gvMemberTransferDetails.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
            }

            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                
                gvMemberTransferDetails.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                
                gvMemberTransferDetails.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                
                gvMemberTransferDetails.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                
                gvMemberTransferDetails.Columns[0].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                
                gvMemberTransferDetails.Columns[0].Visible = false;
            }


        }
        #endregion

        #region ------------- DDL Category Index Changed ---------
       /* protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (ddlcategory.SelectedValue.ToString() == "--Select--")
                //{
                //    txtSearch.Enabled = false;
                //}
                //else
                //{
                //    txtSearch.Enabled = true;                   
                //}
                ddlcategory.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }*/
        #endregion

        #region ------------- Search Action For BindGridView -----------------
        private void SeacrhAction()
        {
            try
            {
                ObjTrans.Action = "GetTransferDetails";

                if (ddlcategory.SelectedValue.ToString() == "New_Member_ID")
                {
                    ObjTrans.Category = "Date_New_Member_ID";
                    ObjTrans.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "Old_Member_ID")
                {
                    ObjTrans.Category = "Date_Old_Member_ID";
                    ObjTrans.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "Old_Member_Name")
                {
                    ObjTrans.Category = "Date_Old_Member_Name";
                    ObjTrans.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "New_Member_Name")
                {
                    ObjTrans.Category = "Date_New_Member_Name";
                    ObjTrans.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "Old_Member_Contact")
                {
                    ObjTrans.Category = "Date_Old_Member_Contact";
                    ObjTrans.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "New_Member_Contact")
                {
                    ObjTrans.Category = "Date_New_Member_Contact";
                    ObjTrans.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "Executive")
                {
                    ObjTrans.Category = "Date_Executive";
                    ObjTrans.SearchByText = txtSearch.Text;
                }   

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion               

        #region --------- Delecte Button ----------------
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int TransferAutoID = Convert.ToInt32(e.CommandArgument.ToString());
                AssignID();
                ObjTrans.Transfer_AutoID = TransferAutoID;
                ObjTrans.Action = "DELETE";

                int res = ObjTrans.Insert_Update_Record();

                if (res > 0)
                {
                    ClearAllForm();
                    SearchByDateFunction();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Deleted Failed !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- Transfer Member Details Page Indexing -------------- 
        protected void gvMemberTransferDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMemberTransferDetails.PageIndex = e.NewPageIndex;

                if (ViewState["TransDetails"] != null)
                {
                    DataTable dt = (DataTable)ViewState["TransDetails"];

                    gvMemberTransferDetails.DataSource = dt;
                    gvMemberTransferDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region ----------- Expot To Excle Record ------------------
        protected void btnExpord_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        protected void ExportToExcel()
        {
            try
            {
                if (ViewState["TransDetails"] != null)
                {

                    dataTable1 = (DataTable)ViewState["TransDetails"]; ;
                    if (dataTable1.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=TransferDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvMemberTransferDetails.Columns[0].Visible = false;
                            gvMemberTransferDetails.Columns[1].Visible = false;
                            gvMemberTransferDetails.AllowPaging = false;

                            gvMemberTransferDetails.DataSource = dataTable1;
                            gvMemberTransferDetails.DataBind();
                            gvMemberTransferDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMemberTransferDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMemberTransferDetails.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvMemberTransferDetails.Rows)
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


                            gvMemberTransferDetails.GridLines = GridLines.Both;
                            gvMemberTransferDetails.RenderControl(hw);

                            //style to format numbers to string

                            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            //Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Can Not Export Because Records Are Not Available !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Can Not Export Because Records Are Not Available !!!.','Information');", true);
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
            /* Verifies that the control is rendered */
        }
        #endregion

        #region ---------------- Refresh Button --------------
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ddlcategory.SelectedIndex = 0;
                txtSearch.Text = "";
                //txtSearch.Enabled = false;
                Assign_MonthDate();
                dataTable = null;
                ViewState["TransDetails"] = dataTable;
                gvMemberTransferDetails.DataSource = dataTable;
                gvMemberTransferDetails.DataBind();
                SearchByDateFunction();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion





    }
}