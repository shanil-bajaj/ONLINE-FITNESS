using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace NDOnlineGym_2017
{
    public partial class PTAddTranning : System.Web.UI.Page
    {

        BalLoginForm ObjLogin = new BalLoginForm();
        BalPackage pack = new BalPackage();
        BalCourseReg cour = new BalCourseReg();
        BalAddMember objMemberDetails = new BalAddMember();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        DataTable dt = new DataTable();
        BalExpense ObjExpense = new BalExpense();
        BalBalancePayment objBalance = new BalBalancePayment();
        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();
        BalPT pt = new BalPT();

        BalEnquiry eng = new BalEnquiry();
        BalSendQuickSMS objBalSendQuickSMS = new BalSendQuickSMS();

        int memberid, Member_ID, member_Auto_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReceiptID();
                BindGridview_1();
                Bind_PaymentType();
                bindDDLExecutive();
                setExecutive();
                BindTrainerIncentiveWise();
                BindDays();
                BindTime();
                AssignTodaysDate1();
                txtReceiptid.Focus();
                txtReceiptid.TabIndex = 0;
                if (Request.QueryString["EditReceipt"] != null)
                {
                    int Receiptno = Convert.ToInt32(Request.QueryString["EditReceipt"].ToString());
                    GetDataForEdit(Receiptno);
                }
            }
        }
        DataTable DT_single = new DataTable();
        int Receipt;
       // string status;
        DataTable dt_package = new DataTable();
        public void GetDataForEdit(int Receiptno)
        {
            try
            {
                DataSet ds = new DataSet();
                btnSave.Text = "Update";
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.ReceiptID = Receiptno;
                txtReceiptid.Text = Receiptno.ToString(); //ViewState["EditReceipt"].ToString();
                DT_single = cour.Get_Edit_member();
                Receipt = Receiptno;
                ViewState["DT_single"] = DT_single;
                if (DT_single.Rows.Count > 0)
                {
                    int count = DT_single.Rows.Count;
                    if (count == 1)
                    {
                        status = "Single";
                    }

                    DT_single = cour.Get_Edit_member();
                    txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                    txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                    txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                    ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                    txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                    txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                    ////  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());

                    cour.ReceiptID = Receipt;
                    txtReceiptid.Text = Receipt.ToString();
                    DataTable dt_package = new DataTable();
                    dt_package = cour.Get_Edit_Assihnpackage();


                    lblpackAutoID.Text = dt_package.Rows[0]["Pack_AutoID"].ToString();
                    txtPackage.Text = dt_package.Rows[0]["Package"].ToString();
                    txtDuration.Text = dt_package.Rows[0]["Duration"].ToString();
                    txtSession.Text = dt_package.Rows[0]["Session"].ToString();

                    DateTime StartDate = Convert.ToDateTime(dt_package.Rows[0]["StartDate"].ToString());
                    DateTime StartDate1;
                    if (DateTime.TryParseExact(StartDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate1))
                    {
                        txtStartDate.Text = StartDate1.ToString("dd-MM-yyyy");

                    }

                    DateTime Endate = Convert.ToDateTime(dt_package.Rows[0]["EndDate"].ToString());
                    DateTime Endate1;
                    if (DateTime.TryParseExact(Endate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Endate1))
                    {
                        txtEndDate.Text = Endate1.ToString("dd-MM-yyyy");
                    }


                    // txtStartDate.Text = dt_package.Rows[0]["StartDate"].ToString();
                    // txtEndDate.Text = dt_package.Rows[0]["EndDate"].ToString();


                    txtQty.Text = dt_package.Rows[0]["Qty"].ToString();
                    txtAmount.Text = dt_package.Rows[0]["Amount"].ToString();
                    txtTotal.Text = dt_package.Rows[0]["Total"].ToString();
                    txtDic.Text = dt_package.Rows[0]["Discount"].ToString();
                    txtDicReason.Text = dt_package.Rows[0]["DiscReason"].ToString();
                    txtFinalTotal.Text = dt_package.Rows[0]["FinalTotal"].ToString();
                    lblTotalFee.Text = txtFinalTotal.Text;

                    ViewState["DT"] = dt_package;

                    DataTable dt_cash = cour.Get_Edit_Payment_PT();
                    ViewState["DT3"] = dt_cash;


                    int Bal_Auto = Convert.ToInt32(dt_cash.Rows[0]["Bal_Auto"].ToString());
                    txtTax.Text = dt_cash.Rows[0][0].ToString();
                    txtPaymode.Text = dt_cash.Rows[0]["PaymentMode"].ToString();
                    txtnumber.Text = dt_cash.Rows[0]["Cardno"].ToString();

                    DateTime date = Convert.ToDateTime(dt_package.Rows[0]["payDate"].ToString());
                    DateTime date1;
                    if (DateTime.TryParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date1))
                    {
                        txtdate.Text = date1.ToString("dd-MM-yyyy");

                    }


                    //txtdate.Text = dt_cash.Rows[0]["payDate"].ToString();

                    txtExpiryDate.Text = dt_cash.Rows[0]["CardExpirydate"].ToString();
                    if (txtExpiryDate.Text != "")
                    {

                        DateTime ExpiryDate = Convert.ToDateTime(dt_package.Rows[0]["CardExpirydate"].ToString());
                        DateTime ExpiryDate1;
                        if (DateTime.TryParseExact(Endate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ExpiryDate1))
                        {
                            txtEndDate.Text = ExpiryDate1.ToString("dd-MM-yyyy");
                        }

                    }
                    txtBankName.Text = dt_cash.Rows[0]["BankName"].ToString();
                    txtBranchname.Text = dt_cash.Rows[0]["BranchName"].ToString();
                    txtPaidAmount.Text = dt_cash.Rows[0]["Paid"].ToString();
                    txtTaxType.Text = dt_cash.Rows[0]["TaxType"].ToString();
                    txtTax.Text = dt_cash.Rows[0]["taxpec"].ToString();
                    txtrs.Text = dt_cash.Rows[0]["TaxValue"].ToString();
                    txtPaidWithTax.Text = dt_cash.Rows[0]["PaidWithTax"].ToString();

                    DataTable dt_cmnt = cour.Get_Edit_Cmnt();
                    if (dt_cmnt.Rows.Count > 0)
                    {
                        lblPaidFee.Text = dt_cmnt.Rows[0]["PaidFee"].ToString();
                        lblTotalFeeDue.Text = dt_cmnt.Rows[0]["TotalFeeDue"].ToString();
                        lblBalance.Text = dt_cmnt.Rows[0]["Balance"].ToString();
                        //txtNextFollowupDate.Text = dt_cmnt.Rows[0]["NextBalDate"].ToString();
                        txtComment.Text = dt_cmnt.Rows[0]["Comment"].ToString();

                        DateTime todaydate;
                        if (DateTime.TryParseExact(dt_cmnt.Rows[0]["NextBalDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                        {
                            txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
                        }
                    }

                    DataTable days = cour.Get_Edit_Days_Time();
                    ddlDays.SelectedValue = days.Rows[0]["Days_AutoID"].ToString();
                    ddlInstructor.SelectedValue = days.Rows[0]["Instructor_AutoID"].ToString();
                    ddlTime.SelectedValue = days.Rows[0]["Time_AutoID"].ToString();
                    //cour.ReceiptID = Receipt;
                    //txtReceiptid.Text = Receipt.ToString();
                    //DataTable dt_package = cour.Get_Edit_Assihnpackage();
                    //ViewState["DT"] = dt_package;
                    //GvPakageAssign.DataSource = dt_package;
                    //GvPakageAssign.DataBind();

                    //GvPakageAssign.Columns[0].Visible = false;

                    ////or simply use compute like below
                    //lblTotalFee.Text = dt_package.Compute("sum(FinalTotal)", "").ToString();

                    //DataTable dt_cash = cour.Get_Edit_Payment();
                    //ViewState["DT3"] = dt_cash;
                    //gvBalancePayment.DataSource = dt_cash;
                    //gvBalancePayment.DataBind();

                    //gvBalancePayment.Columns[0].Visible = false;

                    //DataTable dt_cmnt = cour.Get_Edit_Cmnt();
                    //if (dt_cmnt.Rows.Count > 0)
                    //{
                    //    lblPaidFee.Text = dt_cmnt.Rows[0]["PaidFee"].ToString();
                    //    lblTotalFeeDue.Text = dt_cmnt.Rows[0]["TotalFeeDue"].ToString();
                    //    lblBalance.Text = dt_cmnt.Rows[0]["Balance"].ToString();
                    //    // txtNextFollowupDate.Text = dt_cmnt.Rows[0]["NextBalDate"].ToString();
                    //    txtComment.Text = dt_cmnt.Rows[0]["Comment"].ToString();

                    //    DateTime todaydate;
                    //    if (DateTime.TryParseExact(dt_cmnt.Rows[0]["NextBalDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    //    {
                    //        txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
                    //    }
                    //}
                    ////For Help dt Empty
                    //dt_cash = cour.Get_Edit_Payment();
                    //ViewState["DT3"] = dt_cash;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void AllClear()
        {
            ReceiptID();
            BindGridview_1();
            Bind_PaymentType();
            bindDDLExecutive();
            setExecutive();
            BindTrainerIncentiveWise();
            BindDays();
            BindTime();
            AssignTodaysDate1();


            txtId1.Text = "";
            txtFirstName1.Text = "";
            txtLastName1.Text = "";
            ddlGender1.SelectedIndex = 0;
            txtContact1.Text = "";
            txtEmail1.Text = "";
            lblMemberAutoID.Text = "";

            lblpackAutoID.Text = "";
            txtPackage.Text = "";
            txtDuration.Text = "";
            txtSession.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";


            txtQty.Text = "";
            txtAmount.Text = "";
            txtTotal.Text = "0";
            txtDic.Text = "0";
            txtDicReason.Text = "";
            txtFinalTotal.Text = "0";
            lblTotalFee.Text = "0";

            lblPaidFee.Text = "0";
            lblTotalFee.Text = "0";
            lblTotalFeeDue.Text = "0";
            lblBalance.Text = "0";

            txtPaymode.Text = DropDownList1.SelectedItem.Text;
            txtnumber.Text = "";
            txtdate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
            txtExpiryDate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
            txtTaxType.Text = DropDownList2.SelectedValue.ToString();
            txtTax.Text = "0";
            txtrs.Text = "0";
            txtPaidWithTax.Text = "0";
            txtPaidAmount.Text = "0";


            txtTax.Text = "0";
            txtPaymode.Text = "";
            txtnumber.Text = "";
            txtdate.Text = "";
            txtExpiryDate.Text = "";
            txtBankName.Text = "";
            txtBranchname.Text = "";
            txtPaidAmount.Text = "0";
            txtTaxType.Text = "";
            txtTax.Text = "0";
            txtrs.Text = "0";
            txtPaidWithTax.Text = "";
            txtComment.Text = "";


        }
        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
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

        public void setExecutive()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
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
                ddlExecutive.SelectedValue = staffid;
            }
        }
        public void Bind_PaymentType()
        {
            try
            {
                ObjExpense.company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = ObjExpense.Get_PaymentType();
                if (dt.Rows.Count > 0)
                {
                    DropDownList1.DataSource = dt;
                    DropDownList1.Items.Clear();
                    DropDownList1.DataValueField = "PaymentMode_AutoID";
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void BindDays()
        {
            try
            {
                pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = pt.BindDays();
                if (dt.Rows.Count > 0)
                {
                    ddlDays.DataSource = dt;
                    ddlDays.Items.Clear();
                    ddlDays.DataValueField = "Days_AutoID";
                    ddlDays.DataTextField = "Days";
                    ddlDays.DataBind();
                    ddlDays.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void BindTime()
        {
            try
            {
                pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = pt.BindTime();
                if (dt.Rows.Count > 0)
                {
                    ddlTime.DataSource = dt;
                    ddlTime.Items.Clear();
                    ddlTime.DataValueField = "Time_AutoID";
                    ddlTime.DataTextField = "Time";
                    ddlTime.DataBind();
                    ddlTime.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void BindTime_AvailbleTime_Instructor(int InstruID)
        {
            try
            {
                pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                pt.TrainerID_Fk = InstruID;
                dt = pt.BindTime_AvaibleInstructor();
                if (dt.Rows.Count > 0)
                {
                    ddlTime.DataSource = dt;
                    ddlTime.Items.Clear();
                    ddlTime.DataValueField = "Time_AutoID";
                    ddlTime.DataTextField = "Time";
                    ddlTime.DataBind();
                    ddlTime.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void BindTrainerIncentiveWise()
        {
            try
            {
                pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = pt.BindStaff();
                if (dt.Rows.Count > 0)
                {
                    ddlInstructor.DataSource = dt;
                    ddlInstructor.Items.Clear();
                    ddlInstructor.DataValueField = "TrainerID_Fk";
                    ddlInstructor.DataTextField = "name";
                    ddlInstructor.DataBind();
                    ddlInstructor.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void BindGridview_1()
        {
            try
            {
                if (ddlSearch.SelectedValue.ToString() == "--Select--")
                {
                    pack.Category = "Select_Active";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Package")
                {
                    pack.Category = "Package_Active";
                    pack.searchTxt = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "Duration")
                {
                    pack.Category = "Duration_Active";
                    pack.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Active")
                {
                    pack.Category = "Active";
                    pack.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Deactive")
                {
                    pack.Category = "Deactive";
                    pack.searchTxt = txtSearch.Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    return;
                }
                pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt.Clear();
                dt = pack.Get_Search_PT();
                if (dt.Rows.Count > 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void ReceiptID()
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                txtReceiptid.Text = cour.Get_ReceiptID().ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void ReceiptID_Exists_orNot()
        {
            try
            {
                if (txtReceiptid.Text != "")
                {
                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                    int recei = cour.ReceiptIDExist_OR_Not();
                    if (recei > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Receipt ID Allready Exists !!!','Information');", true);
                        txtReceiptid.Text = "";
                        ReceiptID();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        protected void txtReceiptid_TextChanged(object sender, EventArgs e)
        {
            ReceiptID_Exists_orNot();
        }

        public void member_AutoID()
        {
            try
            {
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_ID1 = Convert.ToInt32(txtId1.Text);
                dt = objMember.Get_Member_Auto_ID();
                if (dt.Rows.Count > 0)
                {
                    member_Auto_ID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"]);
                    lblMemberAutoID.Text = dt.Rows[0]["Member_AutoID"].ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Invalid Member Id!!!','Information');", true);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BalanceID()
        {
            member_AutoID();
            try
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Member_ID1 = member_Auto_ID;
                objBalance.Member_AutoID = member_Auto_ID;
                int flg = objBalance.checkID_Exist_Not();
                if (flg > 0)
                {
                    dt = objBalance.BalanceID();
                    if (dt.Rows.Count > 0)
                    {
                        BindMember();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Invalid Member Id!!!','Information');", true);
                    txtId1.Text = "";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void BindMember()
        {
            txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
            txtFirstName1.Text = dt.Rows[0]["FName"].ToString();
            txtLastName1.Text = dt.Rows[0]["LName"].ToString();
            ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
            txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
            txtEmail1.Text = dt.Rows[0]["Email"].ToString();
            lblMemberAutoID.Text = dt.Rows[0]["Member_AutoID"].ToString();

        }
        protected void txtId1_TextChanged(object sender, EventArgs e)
        {
            objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalance.Member_ID1 = Convert.ToInt32(txtId1.Text);
            dt = objBalance.Check_MemberId_Exist();
            int i = Convert.ToInt32(dt.Rows[0]["memberID"].ToString());
            if (i > 0)
            {

                BalanceID();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Member ID Does Not Exists!','Information');", true);
            }
        }

        protected void txtContact1_TextChanged(object sender, EventArgs e)
        {
            if (txtContact1.Text != "")
            {
                MemeberContact();
            }
        }
        public void MemeberContact()
        {
            try
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Contact1 = txtContact1.Text;
                dt = objBalance.MemeberContact();
                if (dt.Rows.Count > 0)
                {
                    BindMember();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row1 = (GridViewRow)btnPre.NamingContainer;

                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Member_AutoID = Convert.ToInt32(lblMemberAutoID.Text);
                DataTable end = cour.GetMaxEndDate_ByMemberAutoID();
                if (end.Rows.Count > 0)
                {

                    int dur = Convert.ToInt32(end.Rows[0][1].ToString());
                    if (dur >= Convert.ToInt32(row1.Cells[3].Text))
                    {
                        lblpackAutoID.Text = row1.Cells[1].Text;
                        txtPackage.Text = row1.Cells[2].Text;
                        txtDuration.Text = row1.Cells[3].Text;
                        txtSession.Text = row1.Cells[4].Text;
                        txtStartDate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                        txtEndDate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");

                        int duration = Convert.ToInt32(row1.Cells[3].Text);
                        txtQty.Text = "1";
                        txtAmount.Text = row1.Cells[5].Text;
                        txtTotal.Text = row1.Cells[5].Text;
                        txtDic.Text = "0";
                        txtDicReason.Text = "";
                        txtFinalTotal.Text = row1.Cells[5].Text;
                        lblTotalFee.Text = row1.Cells[5].Text;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('PT Duration should be less than Course Duration !!!','Information');", true);
                    }
                }


                //lblpackAutoID.Text = row1.Cells[1].Text;
                //txtPackage.Text = row1.Cells[2].Text;
                //txtDuration.Text = row1.Cells[3].Text;
                //txtSession.Text = row1.Cells[4].Text;
                //txtStartDate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                //txtEndDate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");

                //int duration = Convert.ToInt32(row1.Cells[3].Text);
                //txtQty.Text = "1";
                //txtAmount.Text = row1.Cells[5].Text;
                //txtTotal.Text = row1.Cells[5].Text;
                //txtDic.Text = "0";
                //txtDicReason.Text = "";
                //txtFinalTotal.Text = row1.Cells[5].Text;
                //lblTotalFee.Text = row1.Cells[5].Text;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDuration.Text != "")
                {
                    DateTime sdate;
                    if (DateTime.TryParseExact(txtStartDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out sdate))
                    {
                    }
                    int duration = Convert.ToInt32(txtDuration.Text);
                    DateTime dtStartDate = sdate;
                    DateTime enddate = dtStartDate.AddDays(duration);
                    txtEndDate.Text = enddate.ToString("dd-MM-yyyy");

                    DateTime enddate1;
                    if (DateTime.TryParseExact(txtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out enddate1))
                    {
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        protected void txtDic_TextChanged(object sender, EventArgs e)
        {
            cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            cour.Pack_AutoID = Convert.ToInt32(lblpackAutoID.Text);
            int disc = cour.MaxDiscount();
            if (txtDic.Text == "")
            {
                txtDic.Text = "0";
            }
            if (txtDic.Text != "")
            {
                if (Convert.ToInt32(txtDic.Text) > disc)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Max Discount is Exceed The Limit !!!','Error');", true);
                    txtDic.Text = "0";
                    txtDic.Focus();
                }
                else
                {
                    int finalTotal = Convert.ToInt32(txtTotal.Text) - Convert.ToInt32(txtDic.Text);
                    txtFinalTotal.Text = Convert.ToString(finalTotal.ToString());
                    lblTotalFee.Text = txtFinalTotal.Text;
                }
            }
        }
        protected void AssignTodaysDate1()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        protected void addReceipt_Click(object sender, EventArgs e)
        {
            if (txtStartDate.Text != null && txtEndDate.Text != null)
            {

                if (DropDownList1.SelectedItem.Text != "--Select--")
                {
                    txtPaymode.Text = DropDownList1.SelectedItem.Text;
                    txtnumber.Text = "";
                    txtdate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                    txtExpiryDate.Text = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                    txtTaxType.Text = DropDownList2.SelectedValue.ToString();
                    txtTax.Text = "0";
                    txtrs.Text = "0";
                    txtPaidWithTax.Text = "0";
                    txtPaidAmount.Text = "0";

                    if (DropDownList2.SelectedValue == "Including")
                    {
                        cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                        cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                        DataTable tx = cour.Get_including_Tax();
                        if (tx.Rows.Count > 0)
                        {
                            txtTax.Text = tx.Rows[0][0].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Insert Tax In Master!!!','Error');", true);
                        }
                    }
                    else
                    {
                        //dr1["taxpec"] = "";
                    }

                    lblTotalFeeDue.Text = lblTotalFee.Text;
                    lblPaidFee.Text = "0";
                    lblBalance.Text = "0";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Payment Mode  !!!','Error');", true);
                }


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Start Date and End Date Should Not Be Blank  !!!','Error');", true);
            }
        }

        protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtPaidAmount.Text != "")
            {
                if ((Convert.ToInt32(txtPaidAmount.Text)) <= (Convert.ToInt32(lblTotalFee.Text)))
                {
                    if (txtTaxType.Text == "Excluding")
                    {
                        if (txtTax.Text != "")
                        {
                            txtrs.Text = Convert.ToString((Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100);
                            txtPaidWithTax.Text = Convert.ToString(Convert.ToInt32(txtPaidAmount.Text) + Convert.ToInt32(txtrs.Text));
                            lblPaidFee.Text = txtPaidWithTax.Text;
                            lblBalance.Text = Convert.ToString(Convert.ToInt32(lblTotalFeeDue.Text) - Convert.ToInt32(lblPaidFee.Text));
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        double a = 0, y = 0, x = 0, z = 0;
                        a = Convert.ToDouble(txtPaidAmount.Text);
                        y = Convert.ToDouble(txtTax.Text);

                        x = (100 * a) / (100 + y);
                        z = a - x;
                        txtrs.Text = z.ToString("#,0.00");
                        txtPaidWithTax.Text = txtPaidAmount.Text;
                        lblPaidFee.Text = txtPaidWithTax.Text;
                        lblBalance.Text = Convert.ToString(Convert.ToInt32(lblTotalFeeDue.Text) - Convert.ToInt32(lblPaidFee.Text));

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Paid Fees less than Total Fees !!!','Error');", true);
                    txtPaidAmount.Text = "0";
                }
            }
            else
            {
                txtPaidAmount.Text = "0";
            }
        }



        public int Save_BalancePayment()
        {
            try
            {
                //Convert.ToInt32(Lblmemeber_Auto.Text);
                cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                cour.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

                if (btnSave.Text == "Save")
                {
                    cour.Action = "Insert_BalancePayment";
                }
                else
                {
                    cour.Action = "Update_BalancePayment_PT";
                }
                cour.Status = "PT";

                cour.CourseMemberType = "Single";
                cour.Member_ID1 = Convert.ToInt32(txtId1.Text);

                cour.PaymentMode = txtPaymode.Text;

                DateTime txtDate1;
                if (DateTime.TryParseExact(txtdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out txtDate1))
                {
                    string txtDate11 = txtDate1.ToString("yyyy-MM-dd");
                    cour.payDate = Convert.ToDateTime(txtDate11);
                }

                if (txtPaymode.Text == "Cash")
                {

                    cour.Cardno = "";// txtNumber.Text;
                    cour.CardExpirydate = null;// Convert.ToDateTime(txtExpiryDate.Text);
                    cour.BankName = "";//txtBankName.Text;
                }
                else if (txtPaymode.Text == "NEFT")
                {
                    cour.CardExpirydate = null;

                }
                else if (txtPaymode.Text == "RTGS")
                {
                    cour.CardExpirydate = null;
                }
                else
                {
                    DateTime txtExpiryDate1;
                    if (DateTime.TryParseExact(txtExpiryDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out txtExpiryDate1))
                    {
                        string txtExpiryDate11 = txtExpiryDate1.ToString("yyyy-MM-dd");
                        cour.CardExpirydate = Convert.ToDateTime(txtExpiryDate11);
                    }
                    cour.Cardno = txtnumber.Text;
                    cour.BankName = txtBankName.Text;
                }
                cour.BranchName = txtBranchname.Text;
                if (txtPaidAmount.Text != "")
                {
                    cour.Paid = Convert.ToDouble(txtPaidAmount.Text);
                }
                else
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Paid Amount !!!','Error');", true);
                    cour.Paid = 0;
                }
                cour.TaxType = txtTaxType.Text;
                if (txtTax.Text != "")
                {
                    cour.taxpec = Convert.ToDouble(txtTax.Text);
                }
                else
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Paid Amount !!!','Error');", true);
                    cour.taxpec = 0;
                }
                if (txtrs.Text != "")
                {
                    cour.TaxValue = Convert.ToDouble(txtrs.Text);
                }
                else
                {
                    cour.TaxValue = 0;
                }
                if (txtPaidWithTax.Text != "")
                {
                    cour.PaidWithTax = Convert.ToDouble(txtPaidWithTax.Text);
                }
                else
                {
                    cour.PaidWithTax = 0;
                }

                Bal = cour.Insert_BalancePayment();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            return Bal;
        }

        public int Save_Balance_Details()
        {
            try
            {
                cour.Member_AutoID = 0;// Convert.ToInt32(lblMemberAutoID.Text);
                cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                cour.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

                if (btnSave.Text == "Save")
                {
                    cour.Action = "Insert_BalanceDetails";
                }
                else
                {
                    cour.Action = "Update_BalanceDetails_PT";
                }
                cour.CourseMemberType = "Single";
                cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                cour.Status = "PT";
                cour.PaidFee = Convert.ToDouble(lblPaidFee.Text);
                cour.TotalFeeDue = Convert.ToDouble(lblTotalFeeDue.Text);
                cour.Balance = Convert.ToDouble(lblBalance.Text);
                cour.Comment = txtComment.Text;
                // cour.NextBalDate = Convert.ToDateTime(txtNextFollowupDate.Text);
                if (lblBalance.Text != "0")
                {
                    DateTime todaydate;
                    if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                        //cour.NextBalDate = Convert.ToDateTime(todaydate1);
                        cour.NextBalDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                }
                else
                {
                    DateTime todaydate;
                    if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                        //cour.NextBalDate = Convert.ToDateTime(todaydate1);
                        cour.NextBalDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                }

                Bal_Details = cour.Insert_Balancedetails();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            return Bal_Details;
        }
        int Bal_Details = 0;
        protected void txtTax_TextChanged(object sender, EventArgs e)
        {
            if (txtTaxType.Text == "Excluding")
            {
                if (txtTax.Text != "")
                {
                    txtrs.Text = Convert.ToString((Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100);
                    txtPaidWithTax.Text = Convert.ToString(Convert.ToInt32(txtPaidAmount.Text) + Convert.ToInt32(txtrs.Text));
                    lblPaidFee.Text = txtPaidWithTax.Text;
                    lblBalance.Text = Convert.ToString(Convert.ToInt32(lblTotalFeeDue.Text) - Convert.ToInt32(lblPaidFee.Text));
                }
                else
                {

                }
            }
        }
        int course = 0;
        DateTime S_Date, E_Date;
        string Sdate, Edate;
        public int SaveAssign_Package()
        {
            try
            {
                // cour.Member_AutoID = Convert.ToInt32(lblMemberAutoID.Text);
                cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                if (btnSave.Text == "Save")
                {
                    cour.Action = "Insert_AssignCourse";
                }
                else
                {
                    cour.Action = "Update_AssignCourse";
                }
                cour.Status = "Active";
                cour.MemberType = "PT";

                if (txtContact1.Text != "")
                {
                    cour.Pack_AutoID = Convert.ToInt32(lblpackAutoID.Text);
                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);

                    if (DateTime.TryParseExact(txtStartDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                    {
                        Sdate = S_Date.ToString("yyyy-MM-dd");
                        cour.StartDate = Convert.ToDateTime(Sdate);
                    }
                    if (DateTime.TryParseExact(txtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                    {
                        Edate = E_Date.ToString("yyyy-MM-dd");
                        cour.EndDate = Convert.ToDateTime(Edate);
                    }

                    cour.Amount = Convert.ToDouble(txtAmount.Text);
                    cour.Qty = Convert.ToInt32(txtQty.Text);
                    cour.Total = Convert.ToInt32(txtTotal.Text);
                    cour.Discount = Convert.ToDouble(txtDic.Text);
                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);
                    cour.CourseMemberType = "Single";
                    cour.DiscReason = txtDicReason.Text;
                    cour.Staff_AutoID = Convert.ToInt32(ddlInstructor.SelectedValue.ToString());

                    course = cour.Insert_AssignPackage();
                    cour.Days_AutoID = Convert.ToInt32(ddlDays.SelectedValue.ToString());
                    cour.Time_AutoID = Convert.ToInt32(ddlTime.SelectedValue.ToString());
                    cour.Instructor_AutoID = Convert.ToInt32(ddlInstructor.SelectedValue.ToString());
                    course = cour.Insert_InstructorDetails();
                }
            }
            //}
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            return course;
        }


        #region------------SMS Function--------------------
        private void AssignID()
        {
            objBalSendQuickSMS.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalSendQuickSMS.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        string suname, spass, senderid, routeid, status;
        public int SendSMSFun(string Mobile, string Message)
        {
            int Val;
            try
            {
                AssignID();
                objBalSendQuickSMS.Action = "SELECT_SMSLogin_INFO";
                DataSet ds = new DataSet();

                ds = objBalSendQuickSMS.GetSMSLoginDetails();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    suname = ds.Tables[0].Rows[0]["Username"].ToString();
                    spass = ds.Tables[0].Rows[0]["Password"].ToString();
                    senderid = ds.Tables[0].Rows[0]["Sender_ID"].ToString();
                    routeid = ds.Tables[0].Rows[0]["Route"].ToString();
                    status = ds.Tables[0].Rows[0]["Status"].ToString();
                }

                if (status == "ON")
                {
                    Val = 0;
                    string strUrl = "http://173.45.76.226:81/send.aspx?username=" + suname + "&pass=" + spass + "&route=" + routeid + "&senderid=" + senderid + "&numbers=" + Mobile + "&message=" + Server.UrlEncode(Message) + "";
                    //System.Web.HttpUtility.UrlEncode;
                    WebRequest request = HttpWebRequest.Create(strUrl);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = (Stream)response.GetResponseStream();
                    StreamReader readStream = new StreamReader(s);
                    string dataString = readStream.ReadToEnd();
                    response.Close();
                    s.Close();
                    readStream.Close();
                }
                else
                {
                    Val = 2;
                }
            }
            catch (WebException ex)
            {
                Val = 1;
            }

            if (Val == 1)
                return 1;
            else if (Val == 2)
                return 2;

            else
                return 0;
        }

        #endregion

        string gender = "";
        string s = "";
        string newstring = "";
        private void SendSMSNew()
        {
            StringBuilder Message = new StringBuilder();

            eng.Action = "Get_RenewTemplate";
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            dt = eng.GetTemplate();
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0]["SMSWithName"].ToString() == "YES")
            //    {

            if (ddlGender1.SelectedValue == "Male")
            {
                gender = "Sir";
            }
            if (ddlGender1.SelectedValue == "Female")
            {
                gender = "Madam";
            }

            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime k = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local             

            //DateTime k = System.DateTime.Now;
            //pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            //pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            //pt.Member_AutoID = Convert.ToInt32(lblMemberAutoID.Text);
            //dt = pt.session();
            //if (dt.Rows.Count > 0)
            //{
            //    newstring = "Total Session =" + " " + dt.Rows[0]["TotalSession"].ToString() + "" + ", Complete Session =" + "" + dt.Rows[0]["CompleteSession"].ToString() + "" + " , Remaining Session = " + "" + dt.Rows[0]["RemSession"].ToString();
            //}


            newstring = " Package =" + txtPackage.Text + " " + ",Duration=" + txtDuration.Text + " " + ", Session=" + txtSession.Text + " " + " ,Start Date=" + txtStartDate.Text + " " + " ,End Date=" + txtEndDate.Text + " " + ",Discount=" + txtDic.Text + " " + ",Total Fees=" + lblTotalFeeDue.Text + " " + ", Total Paid=" + lblPaidFee.Text + " " + ",Rem Fees=" + lblBalance.Text;
            s = "Dear" + " " + txtFirstName1.Text + "" + txtLastName1.Text + " " + gender + " " + newstring;
            //   s = "Dear" + " " + cmbName.Text + " " + gender + " " + dr1[0][8].ToString();
            int res1 = SendSMSFun(txtContact1.Text, s);
            if (res1 == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Sending failed due to Internet Connection !!!','Error');", true);
                return;
            }
            else if (res1 == 2)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message not send! SMS Status is off !!!','Error');", true);
                return;
            }
            else
            {
             //   Clear();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Message Sent Successfully !!!','Success');", true);
            }
            //  SendSMSFun(txtContact1.Text, s);
            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Sms Send Successfully !!!','Success');", true);
            //    }
            //    else
            //    {
            //        // Message.Append(dt.Rows[0]["Enquiry"].ToString());
            //    }

            //}

            //string Mobile = txtContact1.Text;
            //SendSMSFun(Mobile, Message);           
        }

        int Bal = 0;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //if (txtFirstName1.Text != "")
                //{
                //    if (GvPakageAssign.Rows.Count > 0)
                //    {
                //        if (gvBalancePayment.Rows.Count > 0)
                //        {
                if (btnSave.Text == "Save")
                {
                    if (txtReceiptid.Text != "")
                    {
                        cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                        cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                        cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                        int recei = cour.ReceiptIDExist_OR_Not();
                        if (recei > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Receipt ID Allready Exists !!!','Error');", true);
                            txtReceiptid.Text = "";
                            ReceiptID();
                        }
                        else
                        {
                            //int k = save_submember();
                            //if (k >= 0)
                            //{
                            int cour1 = SaveAssign_Package();
                            if (cour1 > 0)
                            {
                                int balres = Save_BalancePayment();
                                if (balres > 0)
                                {
                                    int bal_Details = Save_Balance_Details();
                                    if (bal_Details > 0)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Save Successfully !!!','Success');", true);
                                        SendSMSNew();
                                        //AllClear();

                                        // ReceiptID();
                                        //BindSearch_gridview();
                                        BindGridview_1();

                                        int Receipt_No = Convert.ToInt32(txtReceiptid.Text);
                                        string strPopup = "<script language='javascript' ID='script1'>"
                                        + "window.open('Receipt.aspx?PT=" + Receipt_No
                                        + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
                                        + "</script>";
                                        ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                                        ReceiptID();

                                        if (Request.QueryString["FNameMemDetails"] != null)
                                        {
                                            int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                                            Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                                        }

                                        AllClear();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Contact Number !!!','Error');", true);
                                        txtContact1.Focus();
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Contact Number !!!','Error');", true);
                                txtContact1.Focus();
                            }
                            //}
                        }
                    }
                }
                else
                {
                    //if (DateTime.TryParseExact(txtStartDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                    //{
                    //    Sdate = S_Date.ToString("yyyy-MM-dd");
                    //}               
                    //if (DateTime.TryParseExact(txtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                    //{
                    //    Edate = E_Date.ToString("yyyy-MM-dd");
                    //}
                    //if (Sdate != null && Edate != null)
                    //{
                    int cour2 = SaveAssign_Package();
                    if (cour2 > 0)
                    {
                        int balres = Save_BalancePayment();
                        if (balres > 0)
                        {
                            int bal_Details = Save_Balance_Details();
                            if (bal_Details > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                                //AllClear();
                                //Divcourse.Visible = true;
                                //Div_paymode.Visible = true;
                                //btnSave.Text = "Save";
                                //txtReceiptid.Text = "";
                                //ReceiptID();
                                //dt1.Clear();
                                //dt3.Clear();
                                //ViewState["DT"] = null;
                                //ViewState["DT3"] = null;
                                //BindSearch_gridview();
                                //rbtnSingle.Checked = true;
                                //Single();
                                //divFormDetails.Visible = false;
                                //divsearch.Visible = true;
                                AllClear();
                                BindGridview_1();

                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Start Date and End Date Should Not Be Blank  !!!','Error');", true);
                    //}
                }
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Payment Mode  !!!','Error');", true);
                //        }
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Course !!!','Error');", true);
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Member Details !!!','Error');", true);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            AllClear();
        }

        protected void ddlTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        protected void ddlInstructor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInstructor.SelectedValue != "--Select--")
            {
                BindTime_AvailbleTime_Instructor(Convert.ToInt32(ddlInstructor.SelectedValue));
            }
        }
    }
}