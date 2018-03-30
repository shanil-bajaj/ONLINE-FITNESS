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
    public partial class BalancePayment : System.Web.UI.Page
    {
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalBalancePayment objBalance = new BalBalancePayment();
        DataTable dt = new DataTable();
        DataTable table = new DataTable();
        BalAddMember objMemberDetails = new BalAddMember();
        BalEnquiry eng = new BalEnquiry();
        System.Data.SqlClient.SqlTransaction trans;
        BalCourseReg cour = new BalCourseReg();
        DataSet ds4 = new DataSet();
        int memberid ,Member_ID,member_Auto_ID;
        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();
        static double FinalTotal = 0;
        static double PaidFees = 0;
        static double TotalFees_Due = 0;
        double paidAmount;
        DataTable dt_package;
        static int kt = 0;
        static double paidEdit;
        static double paidEdit1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodaysDate();
                bindDDLExecutive();
                setExecutive();
                TxtID.Focus();
                ReceiptID();
                PaymentMode();
                AssignMonthDate();

                if (Request.QueryString["Member_ID"] != null )
                {
                    memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    GetMemberDetails(memberid);

                    Member_Auto_ID();
                    objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objBalance.Member_ID1 = Member_ID;
                    dt = objBalance.ReceiptID();
                    if (dt.Rows.Count > 0)
                    {
                        ddlReceipt.DataSource = dt;
                        ddlReceipt.Items.Clear();
                        ddlReceipt.DataValueField = "ReceiptID";
                        ddlReceipt.DataTextField = "ReceiptID";
                        ddlReceipt.DataBind();
                        ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    }

                    TxtID.Enabled = false;
                    txtFirst.Enabled = false;
                    txtLast.Enabled = false;
                    txtContact.Enabled = false;
                    ddlGender1.Enabled = false;
                    txtmail.Enabled = false;
                }
                TxtID.Focus();
                addReceipt.Enabled = true;
                addReceipt.Visible = true;
                if (Request.QueryString["MenuBalanceDetails"] != null)
                {
                    TxtID.Enabled = false;
                    txtFirst.Enabled = false;
                    txtLast.Enabled = false;
                    txtContact.Enabled = false;
                    ddlGender1.Enabled = false;
                    txtmail.Enabled = false;
                    divsearch.Visible = true;
                    divFormDetails.Visible = false;
                    txtFromDate.Focus();
                    Serach();
                    
                }

                if (Request.QueryString["BalancePayment_Member_AutoID"] != null)
                {
                    TxtID.Enabled = false;
                    txtFirst.Enabled = false;
                    txtLast.Enabled = false;
                    txtContact.Enabled = false;
                    ddlGender1.Enabled = false;
                    txtmail.Enabled = false;
                    divsearch.Visible = true;
                    divFormDetails.Visible = false;
                    txtFromDate.Focus();

                }             
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
                        gvBalanceDetails.DataSource = dt;
                        gvBalanceDetails.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Invalid Member Id!!!','Information');", true);
                    TxtID.Text = "";
                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }                    

        }

     
        public void PaymentMode()
        {
            objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);            
            dt = objBalance.Get_PaymentMode();
            if (dt.Rows.Count > 0)
            {
                ddlPaymentMode.DataSource = dt;
                ddlPaymentMode.Items.Clear();
                ddlPaymentMode.DataValueField = "PaymentMode_AutoID";
                ddlPaymentMode.DataTextField = "Name";
                ddlPaymentMode.DataBind();
               // ddlPaymentMode.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            }
        }
        
        public void Member_Auto_ID()
        {            
                memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_AutoID = memberid;
                dt = objMember.Get_Member_ID();
                if (dt.Rows.Count > 0)
                {
                    Member_ID = Convert.ToInt32(dt.Rows[0]["Member_ID1"]);
                }
            
        }
        public void Member_Auto_ID1()
        {
            memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMember.Member_AutoID = memberid;
            dt = objMember.Get_Member_ID();
            if (dt.Rows.Count > 0)
            {
                Member_ID = Convert.ToInt32(dt.Rows[0]["Member_ID1"]);
            }

        }

        public void GetMemberDetails(int memberid)
        {
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Member_AutoID = Convert.ToInt32(memberid);
            dt = objMemberDetails.SelectByID_MemberInformation();
            if (dt.Rows.Count > 0)
            {
                TxtID.Text = dt.Rows[0]["Member_ID1"].ToString();
                txtFirst.Text = dt.Rows[0]["FName"].ToString();
                txtLast.Text = dt.Rows[0]["LName"].ToString();
                ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                txtmail.Text = dt.Rows[0]["Email"].ToString();
                lblMemberAutoID.Text =memberid.ToString();
            }
        }

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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('First Insert Staff !!!','Information');", true);
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
                ddlExecutive.SelectedValue = staffid;
            }
        }

        #region Assign_Date
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");

            }
        }
        #endregion   
                      
        public void member_AutoID()
        {
            try
            {
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_ID1 = Convert.ToInt32(TxtID.Text);
                dt = objMember.Get_Member_Auto_ID();
                if (dt.Rows.Count > 0)
                {
                    member_Auto_ID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"]);
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

        public void BindMember()
        {
            TxtID.Text = dt.Rows[0]["Member_ID1"].ToString();
            txtFirst.Text = dt.Rows[0]["FName"].ToString();
            txtLast.Text = dt.Rows[0]["LName"].ToString();
            ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
            txtContact.Text = dt.Rows[0]["Contact1"].ToString();
            txtmail.Text = dt.Rows[0]["Email"].ToString();
            //lblBalance.Text = dt.Rows[0]["Balance"].ToString();
            //lblPaidFee.Text = dt.Rows[0]["PaidFee"].ToString();
            //lblTotalFeeDue.Text = dt.Rows[0]["TotalFeeDue"].ToString();
            //// ddlGender12.SelectedItem.Value = dt.Rows[0]["ReceiptID"].ToString();
            //txtBalance.Text = dt.Rows[0]["Balance"].ToString();
            //txtPaid.Text = dt.Rows[0]["PaidFee"].ToString();
            //txtTotal.Text = dt.Rows[0]["TotalFeeDue"].ToString();
            lblMemberAutoID.Text = dt.Rows[0]["Member_AutoID"].ToString();
           // lblBalAuto.Text = dt.Rows[0]["Bal_Auto"].ToString();
            //txtExecutiveName.Text = dt.Rows[0]["Name"].ToString();
            //ddlExecutive.SelectedItem.Value = dt.Rows[0]["staffName"].ToString();
        }


        protected void TxtID_TextChanged(object sender, EventArgs e)
        {
            objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalance.Member_ID1 = Convert.ToInt32(TxtID.Text);
            dt = objBalance.Check_MemberId_Exist();
            int i = Convert.ToInt32(dt.Rows[0]["memberID"].ToString());
             if (i == 0)
             {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Member ID Does Not Exists!','Information');", true);
                 AllClear();
                 TxtID.Focus();
                 ReceiptID();
                 ViewState["DT3"] = null;
                 gvBalanceDetails.DataSource = null;
                 gvBalanceDetails.DataBind();
                 dt.Clear();
             }
             else
             {
                 BalanceID();
                 receiptNumberID();
                 txtBalance.Text = "0";
                 txtPaid.Text = "0";
                 txtTotal.Text = "0";
                 lblPaidFee.Text = "0";
                 lblBalance.Text = "0";
                 lblTotalFeeDue.Text = "0";
             }
        }

        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            MemeberContact();
            ReceiptContact();
            txtmail.Focus();
        }

        public void MemeberContact()
        {
            try
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Contact1 = txtContact.Text;
                dt = objBalance.MemeberContact();
                if (dt.Rows.Count > 0)
                {
                    BindMember();
                    gvBalanceDetails.DataSource = dt;
                    gvBalanceDetails.DataBind();
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

        public void receiptNumberID()
        {
            try
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Member_ID1 = Convert.ToInt32(TxtID.Text);
                dt = objBalance.ReceiptID();
                if (dt.Rows.Count > 0)
                {
                    ddlReceipt.DataSource = dt;
                    ddlReceipt.Items.Clear();
                    ddlReceipt.DataValueField = "ReceiptID";
                    ddlReceipt.DataTextField = "ReceiptID";
                    ddlReceipt.DataBind();
                    ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));
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
        
        public void ReceiptContact()
        {
            try
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Contact1 = txtContact.Text;
                dt = objBalance.ReceiptContact();
                if (dt.Rows.Count > 0)
                {
                    ddlReceipt.DataSource = dt;
                    ddlReceipt.Items.Clear();
                    ddlReceipt.DataValueField = "ReceiptID";
                    ddlReceipt.DataTextField = "ReceiptID";
                    ddlReceipt.DataBind();
                    ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));
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

        protected void ddlReceipt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReceipt.Text != "--Select--")
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.ReceiptID1 = Convert.ToInt32(ddlReceipt.SelectedItem.Value);                
                dt = objBalance.Fees();
                if (dt.Rows.Count > 0)
                {
                    lblBalance.Text = dt.Rows[0]["Balance"].ToString();                                  
                    txtBalance.Text = dt.Rows[0]["Balance"].ToString();                  
                }
                 dt = objBalance.Fees1();
                 if (dt.Rows.Count > 0)
                 {
                     lblTotalFeeDue.Text = dt.Rows[0]["TotalFeeDue"].ToString();
                     txtTotal.Text = dt.Rows[0]["TotalFeeDue"].ToString();
                 }
                 dt = objBalance.Fees2();
                 if (dt.Rows.Count > 0)
                 {
                     lblPaidFee.Text = dt.Rows[0]["PaidFee"].ToString();
                     txtPaid.Text = dt.Rows[0]["PaidFee"].ToString();
                 }
                 //dt = objBalance.Bind_ReceiptWise();
                 //if (dt.Rows.Count>0)
                 //{
                 //    gvBalanceDetails.DataSource = dt;
                 //    gvBalanceDetails.DataBind();
                 //}
            }
            else
            {
                lblBalance.Text = "0";
                lblPaidFee.Text = "0";
                lblTotalFeeDue.Text = "0";
                txtBalance.Text = "0";
                txtPaid.Text = "0";
                txtTotal.Text = "0";
            }
            txtTotal.Focus();
        }

        DataTable dt3 = new DataTable();
        static double TotalFeeDue = 0;
        int flag;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public int k = 0;
        double a = 0, b = 0, c = 0;

        protected void addReceipt_Click(object sender, EventArgs e)
        {
            lblTempPaid.Text = "";
            try
            {
                if (gvBalancePayment.Rows.Count > 0)
                {
                    addReceipt.Enabled = false;
                    addReceipt.Visible = false;
                }
                else
                {
                    dt3.Clear();
                    //GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
                    //var btnPre = (Control)sender;
                    //GridViewRow row2 = (GridViewRow)btnPre.NamingContainer;
                    DataRow dr1 = null;
                    //dt.Clear();
                    dt3.Columns.Add(new DataColumn("Bal_ID"));
                    dt3.Columns.Add(new DataColumn("PaymentMode"));
                    dt3.Columns.Add(new DataColumn("Number"));
                    dt3.Columns.Add(new DataColumn("Date"));
                    dt3.Columns.Add(new DataColumn("CardExpiryDate"));
                    dt3.Columns.Add(new DataColumn("BankName"));
                    dt3.Columns.Add(new DataColumn("BranchName"));
                    dt3.Columns.Add(new DataColumn("PaidAmount"));
                    dt3.Columns.Add(new DataColumn("ddlTax"));
                    dt3.Columns.Add(new DataColumn("txtTax"));
                    dt3.Columns.Add(new DataColumn("Txtvalue"));
                    dt3.Columns.Add(new DataColumn("txtTotalAmount"));
                    dt3.Columns.Add(new DataColumn("txtTempPaid"));

                    if (ViewState["DT3"] != null)
                    {
                        dt3 = (DataTable)ViewState["DT3"];
                    }

                    // bool exists = dt3.Select().ToList().Exists(row => row["Bal_ID"].ToString().ToUpper() == row2.Cells[1].Text);

                    //if (exists == false)
                    //{
                    k = dt3.Rows.Count;
                    dr1 = dt3.NewRow();
                    dr1["Bal_ID"] = k;
                    dr1["PaymentMode"] = ddlPaymentMode.SelectedItem.Text;
                    dr1["Number"] = "";
                    dr1["Date"] = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                    dr1["CardExpiryDate"] = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                    dr1["BankName"] = "";
                    dr1["BranchName"] = "";
                    dr1["PaidAmount"] = "";
                    dr1["ddlTax"] = ddlGSTType.SelectedValue.ToString();

                    if (ddlGSTType.SelectedValue == "Including")
                    {
                        cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                        cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                        DataTable tx = cour.Get_including_Tax();
                        dr1["txtTax"] = tx.Rows[0][0].ToString();
                    }
                    else
                    {
                        dr1["txtTax"] = "";
                    }

                    dr1["Txtvalue"] = "";
                    dr1["txtTotalAmount"] = "";
                    dr1["txtTempPaid"] = "";

                    dt3.Rows.InsertAt(dr1, k);
                    k++;
                    //}
                    ViewState["DT3"] = dt3;
                    gvBalancePayment.DataSource = dt3;
                    gvBalancePayment.DataBind();
                    gvBalancePayment.Focus();

                    //if (lblTotalFeeDue.Text == "0")
                    //{
                    //    lblTotalFeeDue.Text = lblTotalFee.Text;
                    //    TotalFees_Due = Convert.ToDouble(lblTotalFee.Text);
                    //}
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void tempPaid()
        {
            if (lblTempPaid.Text != "")
            {
              
                lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(lblTempPaid.Text)).ToString();
                lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(lblTempPaid.Text)).ToString();
            }
        }

       
        public void PaidEditFetch()
        {
            objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalance.Bal_ReceiptID = Convert.ToInt32(txtReceiptid.Text);
            dt_package = objBalance.Get_Edit_Payment();
     
            paidEdit = Convert.ToInt32(dt_package.Rows[0]["PaidAmount"]);
            paidEdit1 = Convert.ToInt32(dt_package.Rows[0]["PaidAmount"]);
        }

        protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;

            if (btnSave.Text == "Update")
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Bal_ReceiptID = Convert.ToInt32(txtReceiptid.Text);              
                objBalance.Member_ID1 = Convert.ToInt32(ViewState["member_autoid"].ToString());
                dt_package = objBalance.Get_Edit_Payment();              
              

                dt3.Columns.Add(new DataColumn("Bal_ID"));
                dt3.Columns.Add(new DataColumn("PaymentMode"));
                dt3.Columns.Add(new DataColumn("Number"));
                dt3.Columns.Add(new DataColumn("Date"));
                dt3.Columns.Add(new DataColumn("CardExpiryDate"));
                dt3.Columns.Add(new DataColumn("BankName"));
                dt3.Columns.Add(new DataColumn("BranchName"));
                dt3.Columns.Add(new DataColumn("PaidAmount"));
                dt3.Columns.Add(new DataColumn("ddlTax"));
                dt3.Columns.Add(new DataColumn("txtTax"));
                dt3.Columns.Add(new DataColumn("Txtvalue"));
                dt3.Columns.Add(new DataColumn("txtTotalAmount"));

                DataRow row3 = dt3.NewRow();
                int j = 0;
                foreach (DataRow dr11 in dt_package.Rows)
                {
                    j += 1;
                    row3 = dt3.NewRow();
                    row3[0] = dr11["Bal_ID"].ToString();
                    row3[1] = dr11["PaymentMode"].ToString();
                    row3[2] = dr11["Number"].ToString();
                    row3[3] = dr11["Date"].ToString();
                    row3[4] = dr11["CardExpiryDate"].ToString();
                    row3[5] = dr11["BankName"].ToString();
                    row3[6] = dr11["BranchName"].ToString();
                    row3[7] = dr11["PaidAmount"].ToString();
                    row3[8] = dr11["ddlTax"].ToString();
                    row3[9] = dr11["txtTax"].ToString();
                    row3[10] = dr11["Txtvalue"].ToString();
                    row3[11] = dr11["txtTotalAmount"].ToString();

                    dt3.Rows.Add(row3);
                    ViewState["DT3"] = dt3;
                }
            }


                dt3 = (DataTable)ViewState["DT3"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr1;
                dr1 = dt3.NewRow();
                int duration;
                dr1 = dt3.NewRow();
                int s = currentRow.RowIndex;
                //dr["ID"] = s;
                dr1["Bal_ID"] = s;

                TextBox txtPaymentMode = (TextBox)currentRow.FindControl("txtPaymentMode");
                dr1["PaymentMode"] = txtPaymentMode.Text;

                TextBox txtNumber = (TextBox)currentRow.FindControl("txtNumber");
                dr1["Number"] = txtNumber.Text;

                TextBox txtDate = (TextBox)currentRow.FindControl("txtDate");
                dr1["Date"] = txtDate.Text;

                TextBox txtExpiryDate = (TextBox)currentRow.FindControl("txtExpiryDate");
                dr1["CardExpiryDate"] = txtExpiryDate.Text;

                TextBox txtBankName = (TextBox)currentRow.FindControl("txtBankName");
                dr1["BankName"] = txtBankName.Text;

                TextBox txtBranchName = (TextBox)currentRow.FindControl("txtBranchName");
                dr1["BranchName"] = txtBranchName.Text;

                TextBox txtPaidAmount = (TextBox)currentRow.FindControl("txtPaidAmount");
                dr1["PaidAmount"] = txtPaidAmount.Text;

                TextBox ddlTax = (TextBox)currentRow.FindControl("ddlTax");
                dr1["ddlTax"] = ddlTax.Text;//ViewState["DDlTax"];
                double Taxvalue;

                TextBox txtTax = (TextBox)currentRow.FindControl("txtTax");
                dr1["txtTax"] = txtTax.Text;



                double a = 0, y = 0, x = 0, z = 0;
                int zero = 0;

                if (btnSave.Text == "Update")
                {

                    a = Convert.ToDouble(txtPaidAmount.Text);
                    y = Convert.ToDouble(txtBalance.Text);


                    if (paidEdit1 >= a)
                    {
                        if (paidEdit != zero)
                        {
                            lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(paidEdit)).ToString();
                            lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(paidEdit)).ToString();
                        }

                        if (ddlTax.Text == "Excluding")
                        {

                            if (lblTempPaid.Text.ToString() != "")
                            {

                                lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(lblTempPaid.Text)).ToString();
                                lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(lblTempPaid.Text)).ToString();
                            }
                            Taxvalue = (Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100;
                            dr1["Txtvalue"] = Taxvalue.ToString();
                            dr1["txtTotalAmount"] = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                            PaidFees = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                            lblPaidFee.Text = (Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(PaidFees)).ToString();
                            double temp = Convert.ToDouble(lblTotalFeeDue.Text);
                            double duefees = temp + Taxvalue;
                            lblTotalFeeDue.Text = duefees.ToString();
                            lblBalance.Text = Convert.ToString(Convert.ToDouble(duefees) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                        }
                        else
                        {

                            if (lblTempPaid.Text.ToString() != "")
                            {
                                lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(lblTempPaid.Text)).ToString();
                                lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(lblTempPaid.Text)).ToString();
                            }

                            //tempPaid();
                            double a1 = 0, y1 = 0, x1 = 0, z1 = 0;
                            //  paidAmount = Convert.ToDouble(txtPaidAmount.Text);

                            a1 = a;
                            y1 = Convert.ToDouble(txtTax.Text);

                            x1 = (100 * a1) / (100 + y1);
                            z1 = a1 - x1;
                            TextBox Txtvalue = (TextBox)currentRow.FindControl("Txtvalue");
                            dr1["Txtvalue"] = z1.ToString("#,0.00");
                            TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");
                            dr1["txtTotalAmount"] = txtPaidAmount.Text;
                            lblPaidFee.Text = (Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(txtPaidAmount.Text)).ToString();
                            lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                        }
                        dt3.Rows[s].Delete();
                        dt3.Rows.InsertAt(dr1, s);
                        k++;
                        //}
                        ViewState["DT3"] = dt3;
                        gvBalancePayment.DataSource = dt3;
                        gvBalancePayment.DataBind();
                        paidEdit = zero;
                        if (txtPaidAmount.Text != "")
                        {
                            foreach (GridViewRow row4 in gvBalancePayment.Rows)
                            {
                                lblTempPaid.Text = txtPaidAmount.Text;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Paid Amount Should Not Be Greater Than Previous Paid Amount ','Information');", true);
                    }


                }


                else
                {
                    a = Convert.ToDouble(txtPaidAmount.Text);
                    y = Convert.ToDouble(txtBalance.Text);

                    if (a > y)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Paid Amount Should Not Be Greater Than Balance Fees ','Information');", true);
                        txtPaidAmount.Focus();
                        txtPaidAmount.Text = "";
                    }
                    else
                    {

                        if (ddlTax.Text == "Excluding")
                        {
                            if (lblTempPaid.Text.ToString() != "")
                            {

                                lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(lblTempPaid.Text)).ToString();
                                lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(lblTempPaid.Text)).ToString();
                            }


                            Taxvalue = (Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100;
                            dr1["Txtvalue"] = Taxvalue.ToString();
                            dr1["txtTotalAmount"] = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                            PaidFees = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                            lblPaidFee.Text = (Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(PaidFees)).ToString();
                            double temp = Convert.ToDouble(lblTotalFeeDue.Text);
                            double duefees = temp + Taxvalue;
                            lblTotalFeeDue.Text = duefees.ToString();
                            lblBalance.Text = Convert.ToString(Convert.ToDouble(duefees) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                        }
                        else
                        {
                            if (lblTempPaid.Text.ToString() != "")
                            {
                                lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(lblTempPaid.Text)).ToString();
                                lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(lblTempPaid.Text)).ToString();
                            }

                            //tempPaid();
                            double a1 = 0, y1 = 0, x1 = 0, z1 = 0;
                            //  paidAmount = Convert.ToDouble(txtPaidAmount.Text);

                            a1 = a;
                            y1 = Convert.ToDouble(txtTax.Text);

                            x1 = (100 * a1) / (100 + y1);
                            z1 = a1 - x1;
                            TextBox Txtvalue = (TextBox)currentRow.FindControl("Txtvalue");
                            dr1["Txtvalue"] = z1.ToString("#,0.00");
                            TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");
                            dr1["txtTotalAmount"] = txtPaidAmount.Text;
                            lblPaidFee.Text = (Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(txtPaidAmount.Text)).ToString();
                            lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                        }
                    }
                    dt3.Rows[s].Delete();
                    dt3.Rows.InsertAt(dr1, s);
                    k++;
                    //}
                    ViewState["DT3"] = dt3;
                    gvBalancePayment.DataSource = dt3;
                    gvBalancePayment.DataBind();
                    if (txtPaidAmount.Text != "")
                    {
                        foreach (GridViewRow row4 in gvBalancePayment.Rows)
                        {
                            lblTempPaid.Text = txtPaidAmount.Text;
                        }
                    }
                }
                   
                  
            
        }
     
        
        protected void txtTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;

                dt3 = (DataTable)ViewState["DT3"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr1;
                dr1 = dt3.NewRow();
                int duration;
                dr1 = dt3.NewRow();

                int s = currentRow.RowIndex;
                //dr["ID"] = s;
                dr1["Bal_ID"] = s;

                TextBox txtPaymentMode = (TextBox)currentRow.FindControl("txtPaymentMode");
                dr1["PaymentMode"] = txtPaymentMode.Text;

                TextBox txtNumber = (TextBox)currentRow.FindControl("txtNumber");
                dr1["Number"] = txtNumber.Text;

                TextBox txtDate = (TextBox)currentRow.FindControl("txtDate");
                dr1["Date"] = txtDate.Text;

                TextBox txtExpiryDate = (TextBox)currentRow.FindControl("txtExpiryDate");
                dr1["CardExpiryDate"] = txtExpiryDate.Text;

                TextBox txtBankName = (TextBox)currentRow.FindControl("txtBankName");
                dr1["BankName"] = txtBankName.Text;

                TextBox txtBranchName = (TextBox)currentRow.FindControl("txtBranchName");
                dr1["BranchName"] = txtBranchName.Text;

                TextBox txtPaidAmount = (TextBox)currentRow.FindControl("txtPaidAmount");
                dr1["PaidAmount"] = txtPaidAmount.Text;

                TextBox ddlTax = (TextBox)currentRow.FindControl("ddlTax");
                dr1["ddlTax"] = ddlTax.Text;//ViewState["DDlTax"];
                double Taxvalue;

                TextBox txtTax = (TextBox)currentRow.FindControl("txtTax");
                dr1["txtTax"] = txtTax.Text;

                TextBox txtTempPaid = (TextBox)currentRow.FindControl("txtTempPaid");
                dr1["txtTempPaid"] = txtTempPaid.Text;


                if (ddlTax.Text == "Excluding")
                {
                    if (lblTempPaid.Text != "")
                    {

                        lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(lblTempPaid.Text)).ToString();
                        lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(lblTempPaid.Text)).ToString();
                    }
                    Taxvalue = (Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100;
                    dr1["Txtvalue"] = Taxvalue.ToString();
                    dr1["txtTotalAmount"] = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                    PaidFees = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                    lblPaidFee.Text =(Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(PaidFees)).ToString();
                    double temp = Convert.ToDouble(lblTotalFeeDue.Text);
                    double duefees = temp + Taxvalue;

                    lblTotalFeeDue.Text = duefees.ToString();
                    lblBalance.Text = Convert.ToString(Convert.ToDouble(duefees) -Convert.ToDouble(lblPaidFee.Text)).ToString();
                }
                else
                {
                    if (lblTempPaid.Text != "")
                    {

                        lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - Convert.ToDouble(lblTempPaid.Text)).ToString();
                        lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + Convert.ToDouble(lblTempPaid.Text)).ToString();
                    }
                    double a = 0, y = 0, x = 0, z = 0;
                  
                    a = Convert.ToDouble(txtPaidAmount.Text);
                    y = Convert.ToDouble(txtTax.Text);
                    x = (100 * a) / (100 + y);
                    z = a - x;
                    TextBox Txtvalue = (TextBox)currentRow.FindControl("Txtvalue");
                    dr1["Txtvalue"] = z.ToString("#,0.00");
                    TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");
                    dr1["txtTotalAmount"] = txtPaidAmount.Text;

                    lblPaidFee.Text = (Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(txtPaidAmount.Text)).ToString();
                    lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                }
                dt3.Rows[s].Delete();
                dt3.Rows.InsertAt(dr1, s);
                k++;
                //}
                ViewState["DT3"] = dt3;
                gvBalancePayment.DataSource = dt3;
                gvBalancePayment.DataBind();
                lblTempPaid.Text = txtPaidAmount.Text;
                txtTempPaid.Text = lblTempPaid.Text;
                if (txtPaidAmount.Text != "")
                {
                foreach (GridViewRow row3 in gvBalancePayment.Rows)
                {
                    lblTempPaid.Text = txtPaidAmount.Text;
                }
                }
             
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvBalancePayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            double paid =0, paid_with_Tax= 0, gstValue=0;
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                dt3 = (DataTable)ViewState["DT3"];
                if (dt3.Rows[index][7].ToString() != "")
                {
                     paid = Convert.ToDouble(dt3.Rows[index][7].ToString());
                }
                if (dt3.Rows[index][11].ToString() != "")
                {
                    paid_with_Tax = Convert.ToDouble(dt3.Rows[index][11].ToString());
                }

                //var textBoxObj = (TextBox)row.Cells[4].Controls[0];
                //TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");

                lblBalance.Text = (Convert.ToInt32(lblBalance.Text) +Convert.ToInt32( paid_with_Tax)).ToString();
                lblPaidFee.Text = (Convert.ToInt32(lblPaidFee.Text) - Convert.ToInt32(paid_with_Tax)).ToString();

                string gstType = dt3.Rows[index][8].ToString();
                
                if (dt3.Rows[index][11].ToString() != "")
                {
                     gstValue = Convert.ToDouble(dt3.Rows[index][10].ToString());
                }
                if (gstType == "Excluding")
                {
                    lblTotalFeeDue.Text = (Convert.ToInt32(lblTotalFeeDue.Text) - Convert.ToInt32(gstValue)).ToString();
                    lblBalance.Text = (Convert.ToInt32(lblBalance.Text) - Convert.ToInt32(gstValue)).ToString();
                }
              

                dt3.Rows[index].Delete();
                ViewState["DT3"] = dt3;
                gvBalancePayment.DataSource = dt3;
                gvBalancePayment.DataBind();

                //if (dt3.Rows.Count == 0)
                //{
                //    lblBalance.Text = "0";
                //    lblPaidFee.Text = "0";
                //    lblTotalFeeDue.Text = "0";
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        int Bal_Details = 0;
        public int Save_Balance_Details()
        {
            try
            {

                objBalance.Member_AutoID = Convert.ToInt32(lblMemberAutoID.Text);
                objBalance.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
                objBalance.ReceiptID1 = Convert.ToInt32(ddlReceipt.SelectedItem.Value);
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                if (btnSave.Text == "Save")
                {
                    objBalance.Action = "Insert_BalanceDetails";
                }
                else
                {
                    objBalance.Action = "Update_BalanceDetails";
                }
                objBalance.Status = "Balance";
                if (gvBalancePayment.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvBalancePayment.Rows)
                    {
                        TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                        if (txtPaidAmount.Text != "")
                        {
                            c = c + Convert.ToDouble(txtPaidAmount.Text); //storing total qty into variable
                        }                                           
                    }
                    objBalance.PaidFee = c;// Convert.ToDouble(txtPaidAmount.Text);
                }
                int i = 0;
                objBalance.TotalFeeDue = Convert.ToDouble(i);
                objBalance.Balance = Convert.ToDouble(lblBalance.Text);
                //if (lblBalance.Text != "0")
                //{
                    DateTime todaydate;
                    if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                       
                        objBalance.NextBalDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                //}
                //else
                //{
                //    objBalance.NextBalDate = null;
                //}
               


                objBalance.Member_ID1 = Convert.ToInt32(TxtID.Text);
                objBalance.Bal_ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                Bal_Details = objBalance.Insert_Balancedetails();
            }
            catch (Exception ex)
            {

            }
            return Bal_Details;
        }

        int Bal = 0;
        public int Save_BalancePayment()
        {
            try
            {
                objBalance.Member_AutoID = Convert.ToInt32(lblMemberAutoID.Text);
                objBalance.ReceiptID1 = Convert.ToInt32(ddlReceipt.SelectedItem.Value);
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                objBalance.Member_ID1 = Convert.ToInt32(TxtID.Text);
                objBalance.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
                if (btnSave.Text == "Save")
                {
                    objBalance.Action = "Insert_BalancePayment";
                }
                else
                {
                    objBalance.Action = "Update_BalancePayment";
                }
                objBalance.Status = "Balance";
                objBalance.Bal_ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                if (gvBalancePayment.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvBalancePayment.Rows)
                    {
                        TextBox txtPaymentMode = (TextBox)row.FindControl("txtPaymentMode");
                        objBalance.PaymentMode = txtPaymentMode.Text;

                        TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                        objBalance.Cardno = txtNumber.Text;

                        TextBox txtDate = (TextBox)row.FindControl("txtDate");
                      ///  objBalance.payDate = Convert.ToDateTime(txtDate.Text);                      

                        DateTime txtDate1;
                        if (DateTime.TryParseExact(txtDate.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out txtDate1))
                        {
                            string txtDate11 = txtDate1.ToString("yyyy-MM-dd");
                            objBalance.payDate = Convert.ToDateTime(txtDate11);
                        }


                        TextBox txtExpiryDate = (TextBox)row.FindControl("txtExpiryDate");
                        DateTime ExpiryDate;
                        if (DateTime.TryParseExact(txtExpiryDate.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ExpiryDate))
                        {
                            string txtDate11 = ExpiryDate.ToString("yyyy-MM-dd");
                            objBalance.CardExpirydate = Convert.ToDateTime(txtDate11);
                        }

                        TextBox txtBankName = (TextBox)row.FindControl("txtBankName");
                        objBalance.BankName = txtBankName.Text;

                        TextBox txtBranchName = (TextBox)row.FindControl("txtBranchName");
                        objBalance.BranchName = txtBranchName.Text;

                        TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                        objBalance.Paid = Convert.ToDouble(txtPaidAmount.Text);

                        TextBox ddlTax = (TextBox)row.FindControl("ddlTax");
                        objBalance.TaxType = ddlTax.Text;

                        TextBox txtTax = (TextBox)row.FindControl("txtTax");
                        objBalance.taxpec = Convert.ToDouble(txtTax.Text);

                        TextBox Txtvalue = (TextBox)row.FindControl("Txtvalue");
                        objBalance.TaxValue = Convert.ToDouble(Txtvalue.Text);

                        TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");
                        objBalance.PaidWithTax = Convert.ToDouble(txtTotalAmount.Text);

                        Bal = objBalance.Insert_BalancePayment();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
            return Bal;
        }

        public void AllClear()
        {
            try
            {
                ddlExecutive.Enabled = false;
                chkExecutive.Checked = true;
                ViewState["DT3"] = null;
                lblTempPaid.Text = "";
                //BindGridview();
                gvBalancePayment.DataSource = null;
                gvBalancePayment.DataBind();
                dt3.Clear();
                txtComment.Text = "";
                lblBalance.Text = "0";              
                lblTotalFeeDue.Text = "0";
                txtBalance.Text = "0";
                txtPaid.Text = "0";
                txtTotal.Text = "0";
                ddlGender1.SelectedIndex = 0;
                ddlReceipt.Items.Clear();
                ddlReceipt.Text = "--Select--";
                txtFirst.Text = "";
                txtLast.Text = "";
                TxtID.Text = "";
                txtContact.Text = "";
                txtmail.Text = "";
                lblPaidFee.Text = "0";
                AssignTodaysDate();
                ddlPaymentMode.SelectedItem.Text = "Cash";
                ddlGSTType.SelectedItem.Text = "Including";
                ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Login_ID"];
                
                gvBalanceDetails.DataSource = null;
                gvBalanceDetails.DataBind();
                dt.Clear();
                TxtID.Focus();
                txtBalance.Text = "0";
                txtPaid.Text = "0";
                txtTotal.Text = "0";
                lblPaidFee.Text = "0";
                lblBalance.Text = "0";
                lblTotalFeeDue.Text = "0";
                txtReceiptid.Focus();
                ddlcategory.SelectedItem.Text = "--Select--"; ;
                txtSearch.Text = "";
                TxtID.Enabled = true;
                txtFirst.Enabled = true;
                txtLast.Enabled = true;
                txtContact.Enabled = true;
                ddlGender1.Enabled = true;
                txtmail.Enabled = true;
            }
            catch (Exception ex)
            {
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            objBalance.ReceiptID1 = Convert.ToInt32(txtReceiptid.Text);
            dt = objBalance.Check_ReceiptId();
            int i = Convert.ToInt32(dt.Rows[0]["checkR"].ToString());
            if (i > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Receipt ID Already Exists!','Information');", true);
            }
            else
            {
                if (btnSave.Text == "Save")
                {
                    if (TxtID.Text != "")
                    {
                        int balres = Save_BalancePayment();
                        if (balres > 0)
                        {
                            int bal_Details = Save_Balance_Details();
                            if (bal_Details > 0)
                            {

                                int ReceiptId = 0;
                                string ReferID;

                                ReferID = ddlReceipt.SelectedValue;
                                ReceiptId = Convert.ToInt32(txtReceiptid.Text);

                                SendSMSNew();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                                AllClear();
                                TxtID.Focus();
                                ReceiptID();
                                ViewState["DT3"] = null;
                                gvBalanceDetails.DataSource = null;
                                gvBalanceDetails.DataBind();
                                dt.Clear();
                                if (Request.QueryString["FNAllFollowupDetailsBal"] != null)
                                {
                                    string Pagename = "FNAllFollowupDetailsBal";
                                    Response.Redirect("AllFollowup.aspx?FNAllFollowupDetailsBal=" + Pagename);
                                }
                                else if (Request.QueryString["FNameMemDetails"] != null)
                                {
                                    int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                                    Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                                }
                                else if (Request.QueryString["Member_ID"] != null)
                                {
                                    int Member_ID1 = Convert.ToInt32(lblMemberAutoID.Text);
                                    Response.Redirect("MemberProfile.aspx?MemberId=" + Member_ID1);
                                }
                                    

                                    string strPopup = "<script language='javascript' ID='script1'>"
                               + "window.open('Receipt.aspx?Receipt_Balance=" + ReceiptId
                               + "&ID= " + ReferID + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
                               + "</script>";
                                    ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                               
                            }
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Saved  !!!','Information');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter All mandetory field  !!!','Error');", true);
                        return;
                    }
                }
                else
                {
                   int balres = Save_BalancePayment();
                   if (balres > 0)
                   {
                       int bal_Details = Save_Balance_Details();
                       if (bal_Details > 0)
                       {
                           ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                           //AllClear();
                           //btnSave.Text = "Save";
                           //ReceiptID();
                           //Div_paymode.Visible = true;
                           //gvBalanceDetails.DataSource = null;
                           //gvBalanceDetails.DataBind();
                           divsearch.Visible = true;
                           divFormDetails.Visible = false;
                           txtFromDate.Focus();
                           gvBalanceDetails.DataSource = null;
                           gvBalanceDetails.DataBind();
                       }
                   }
                   else
                   {
                       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Saved  !!!','Information');", true);
                       return;
                   }
                }
            }
        }
        #region ---------- Assign Company ID and Branch ID -------------------
        private void AssignID()
        {
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
        }
        #endregion

        #region --------- Send SMS ------------
        private void SendSMSNew()
        {
            StringBuilder Message = new StringBuilder();

            //objBalance.Action = "Get_BalancePaymentInfo";
            objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = objBalance.Fees();
                if (dt.Rows[0]["SMSWithName"].ToString() == "YES")
                {
                    Message.Append("Dear ");
                    Message.Append(txtFirst.Text + " " + txtLast.Text);
                    Message.Append(" , " + "Member ID = ");
                    Message.Append(TxtID.Text);
                    Message.Append(" , " + "Receipt ID = ");
                    Message.Append(ddlReceipt.Text);
                    dt = objBalance.Fees1();
                    if (dt.Rows.Count > 0)
                    {
                        Message.Append(" , " + "Total Fees = ");
                        Message.Append(dt.Rows[0]["TotalFeeDue"].ToString());
                    }

                    if (gvBalancePayment.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvBalancePayment.Rows)
                        {
                            TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                            objBalance.Paid = Convert.ToDouble(txtPaidAmount.Text);
                            Message.Append(" , " + "Current Paid Fees = ");
                            Message.Append(txtPaidAmount.Text);
                        }
                    }
                    dt = objBalance.Fees2();
                    if (dt.Rows.Count > 0)
                    {
                        Message.Append(" , " + "Total Paid Fees = ");
                        Message.Append(dt.Rows[0]["PaidFee"].ToString());
                    }
                    dt = objBalance.Fees();
                    if (dt.Rows.Count > 0)
                    {
                        Message.Append(" , " + "Balance = ");
                        Message.Append(dt.Rows[0]["Balance"].ToString());
                    }
                  
                }
                else
                {
                    Message.Append(" , " + "Member ID = ");
                    Message.Append(TxtID.Text);
                    Message.Append(" , " + "Receipt ID = ");
                    Message.Append(ddlReceipt.Text);
                    dt = objBalance.Fees1();
                    if (dt.Rows.Count > 0)
                    {
                        Message.Append(" , " + "Total Fees = ");
                        Message.Append(dt.Rows[0]["TotalFeeDue"].ToString());
                    }

                    if (gvBalancePayment.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvBalancePayment.Rows)
                        {
                            TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                            objBalance.Paid = Convert.ToDouble(txtPaidAmount.Text);
                            Message.Append(" , " + "Current Paid Fees = ");
                            Message.Append(txtPaidAmount.Text);
                        }
                    }
                    dt = objBalance.Fees2();
                    if (dt.Rows.Count > 0)
                    {
                        Message.Append(" , " + "Total Paid Fees = ");
                        Message.Append(dt.Rows[0]["PaidFee"].ToString());
                    }
                    dt = objBalance.Fees();
                    if (dt.Rows.Count > 0)
                    {
                        Message.Append(" , " + "Balance = ");
                        Message.Append(dt.Rows[0]["Balance"].ToString());
                    }
                }
            

            string Mobile = txtContact.Text;
            SendSMSFun(Mobile, Message);

            //string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
 

          //if (Regex.IsMatch(txtmail.Text , pattern))
          //{   
          //  if (txtmail.Text != string.Empty)
          //  {
          //      SendEmail(txtmail.Text.ToString(), Message);
          //  } 
          //}
        }

      
        
        #endregion

        private void SendEmail(string To, StringBuilder Message)
        {
            MailMessage mailObj = new MailMessage();

            string Sub = " Balance Payment Details ";
            string Body = "<br />This is a system generated email.Please do not reply.<br/> <br /> " + Message +
                          "<br/><br/> <br/><br/>Please feel free to contact us 9156184755 or email navkardreamsoft@gmail.com <br/><br/>Thanks <br/> Team ND Fitness+";
            mailObj.From = new MailAddress("gymfitnessplus27@gmail.com");

            if (To == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Email ID Does Not Exist','Information');", true);
            }
            else
            {
                mailObj.To.Add(To);
                mailObj.Subject = Sub;
                mailObj.Body = Body;
                mailObj.IsBodyHtml = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                SmtpClient SMTPServer = new SmtpClient();
                //SMTPServer.Host = "smtp.gmail.com";
                //SMTPServer.Port = 587;
                //SMTPServer.EnableSsl = true;
                SMTPServer.Host = "relay-hosting.secureserver.net";   //-- Donot change.
                SMTPServer.Port = 25; //--- Donot change
                SMTPServer.EnableSsl = false;//--- Donot change
                SMTPServer.UseDefaultCredentials = true;
                NetworkCredential credentials = new NetworkCredential("gymfitnessplus27@gmail.com", "fitnessplus123");
                SMTPServer.Credentials = credentials;
                try
                {
                    SMTPServer.Send(mailObj);
                    //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Password Has Been Sent !!!','Success');", true);
                }
                catch (Exception ex)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Email Failed','Error');", true);
                }
}
        }

        string suname, spass, senderid, routeid, status;
        public void SendSMSFun(string Mobile, StringBuilder Message)
        {
            int Val;
            try
            {
                AssignID();
                eng.Action = "SELECT_SMSLogin_INFO";
                DataSet ds = new DataSet();

                ds = eng.GetSMSLoginDetails();

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
                    string strUrl = "http://173.45.76.226:81/send.aspx?username=" + suname + "&pass=" + spass + "&route=" + routeid + "&senderid=" + senderid + "&numbers=" + Mobile + "&message=" + Server.UrlEncode(Message.ToString()) + "";
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

            }
            catch (WebException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }


        protected void btnCancle_Click(object sender, EventArgs e)
        {
            AllClear();
            btnSave.Text = "Save";
            ReceiptID();
            Div_paymode.Visible = true;
            gvBalanceDetails.DataSource = null;
            gvBalanceDetails.DataBind();
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;

            ddlExecutive.Focus();
        }

       

        //protected void btnpreview_Command(object sender, CommandEventArgs e)
        //{
        //    int ID=0;
        //    int Receipt_Balance = Convert.ToInt32(e.CommandArgument.ToString());
           
        //    string strPopup = "<script language='javascript' ID='script1'>"
        //    + "window.open('Receipt.aspx?Receipt_Balance=" + Receipt_Balance
        //    + "&ID= " + ID + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
        //    + "</script>";
        //    ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        //}

        protected void gvBalanceDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBalanceDetails.PageIndex = e.NewPageIndex;
            if (TxtID.Text == "")
            {
                BalancaWithoutID();
            }
            else
            {
                BalanceID();
            }
            //MemeberContact();
        }

        protected void btnpreview_Command1(object sender, CommandEventArgs e)
        {
            int BalancePayment_Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + BalancePayment_Member_AutoID);
        }

        protected void gvBalancePayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBalancePayment.PageIndex = e.NewPageIndex;
            BalanceID();
        }

        protected void txtReceiptid_TextChanged(object sender, EventArgs e)
        {
            ReceiptID_Exists_orNot();
            TxtID.Focus();
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
        public void ReceiptID()
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                txtReceiptid.Text = cour.Get_ReceiptID().ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnResend_Command(object sender, CommandEventArgs e)
        {

        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            int i = 0;
            if (e.CommandArgument.ToString() =="")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('You can not delete course receipt !','Information');", true);
            }
            else
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.Bal_ReceiptID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalance.DeleteRecord();
                ReceiptID();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                if (TxtID.Text != "")
                {
                    BalanceID();
                }
                else
                {
                    BalancaWithoutID();
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
          
        }

        public void BalancaWithoutID()
        {
            try
            {
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                lblCount.Text = "0";
                dt = objBalance.BalanceWithoutID();
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    gvBalanceDetails.DataSource = dt;
                    gvBalanceDetails.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
          // txtSearch.Text = "";
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dateWithCategory();
        }
        public void SearchData()
        {
            objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalance.searchddl = ddlcategory.SelectedValue;
            objBalance.Action = "Searching";
            if (ddlcategory.SelectedValue == "ReceiptNumber")
            {
                objBalance.searchddl = txtSearch.Text;
                objBalance.Category = "ReceiptNumber";
                //flag = 1;
            }
            else if (ddlcategory.SelectedValue == "MemberId")
            {
                objBalance.searchddl = txtSearch.Text;
                objBalance.Category = "MemberId";
                //flag = 2;
            }
            else if (ddlcategory.SelectedValue == "FirstName")
            {
                objBalance.searchddl = txtSearch.Text;
                objBalance.Category = "FirstName";
                // flag = 3;
            }
            else if (ddlcategory.SelectedValue == "LastName")
            {
                objBalance.searchddl = txtSearch.Text;
                objBalance.Category = "LastName";
                //flag = 4;
            }
            else if (ddlcategory.SelectedValue == "Contact")
            {
                objBalance.searchddl = txtSearch.Text;
                objBalance.Category = "Contact";
                //flag = 5;
            }

            lblCount.Text = "0";
            dt = objBalance.SearchDDL();
            if (dt.Rows.Count > 0)
            {
                lblCount.Text = dt.Rows.Count.ToString();
                gvBalanceDetails.DataSource = dt;
                gvBalanceDetails.DataBind();
            }
        }

        protected void gvBalanceDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnpreview_Click1(object sender, EventArgs e)
        {
            var btnPre = (Control)sender;
            GridViewRow row = (GridViewRow)btnPre.NamingContainer;
            string i = "&nbsp;";
            int ReceiptID=0, ReferID=0;
            string j=row.Cells[6].Text.Trim();

            ReferID = Convert.ToInt32(row.Cells[5].Text);             
            if (ReferID != 0 && j == i)
            {
                ReferID = Convert.ToInt32(row.Cells[5].Text);
                string strPopup = "<script language='javascript' ID='script1'>"
           + "window.open('Receipt.aspx?ID= " + ReferID + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
           + "</script>";
                ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
           
            }
            else
            {
                ReferID = Convert.ToInt32(row.Cells[5].Text);
                ReceiptID = Convert.ToInt32(row.Cells[6].Text);
              
                string strPopup = "<script language='javascript' ID='script1'>"
           + "window.open('Receipt.aspx?Receipt_Balance=" + ReceiptID
           + "&ID= " + ReferID + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
           + "</script>";
                ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }

        }
        #region ------------ Assign All Date ------------------
        protected void AssignMonthDate()
        {
            Label1.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Label1.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);
            txtToDate.Text = dtLast.ToString("dd-MM-yyyy");

            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
          //  eng.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
         //eng.ToDate = Todate;
        }
        #endregion

        /// <summary>
        ///  Search Button method code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Serach()
        {
            string StartDate1, EndDate1;
            DateTime StartDate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
            {
                StartDate1 = StartDate.ToString("dd-MM-yyyy");
                objBalance.FromDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            DateTime EndDate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
            {
                EndDate1 = EndDate.ToString("dd-MM-yyyy");
                objBalance.ToDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }
            AssignID();


            if (objBalance.FromDate > objBalance.ToDate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                AssignMonthDate();
            }
            else
            {
                if (txtSearch.Text != "")
                {
                    SearchData();
                }
                else
                {
                    BalancaWithoutID();
                    ddlcategory.SelectedValue = "--Select--";
                    txtSearch.Text = "";
                    btnSearch.Focus();
                }
            }
        }
        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            Serach();
        }

        protected void btnReresh_Click(object sender, EventArgs e)
        {
            AssignMonthDate();
            txtSearch.Text = "";
            ddlcategory.SelectedValue = "--Select--";
            dt.Clear();
            lblCount.Text = "0";
            if (dt.Rows.Count > 0)
            {
                lblCount.Text = dt.Rows.Count.ToString();
            }
            gvBalanceDetails.DataSource= dt;
            gvBalanceDetails.DataBind();            
        }

        protected void btnDtWithCategory_Click(object sender, EventArgs e)
        {
            dateWithCategory();
        }

        public void dateWithCategory()
        {
            if (ddlcategory.SelectedValue == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
            }
            if (txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
            }
            else if (ddlcategory.SelectedValue != "--Select--" && txtSearch.Text != "")
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                objBalance.FromDate = Fromdate;

                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                objBalance.ToDate = Todate;
                objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalance.searchddl = ddlcategory.SelectedValue;
                objBalance.Action = "SearchWithCategory";
                if (ddlcategory.SelectedValue == "ReceiptNumber")
                {
                    objBalance.searchddl = txtSearch.Text;
                    objBalance.Category = "ReceiptNumber";
                    //flag = 1;
                }
                else if (ddlcategory.SelectedValue == "MemberId")
                {
                    objBalance.searchddl = txtSearch.Text;
                    objBalance.Category = "MemberId";
                    //flag = 2;
                }
                else if (ddlcategory.SelectedValue == "FirstName")
                {
                    objBalance.searchddl = txtSearch.Text;
                    objBalance.Category = "FirstName";
                    // flag = 3;
                }
                else if (ddlcategory.SelectedValue == "LastName")
                {
                    objBalance.searchddl = txtSearch.Text;
                    objBalance.Category = "LastName";
                    //flag = 4;
                }
                else if (ddlcategory.SelectedValue == "Contact")
                {
                    objBalance.searchddl = txtSearch.Text;
                    objBalance.Category = "Contact";
                    //flag = 5;
                }
                lblCount.Text = "0";
                dt = objBalance.SearchWithCategory();
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    gvBalanceDetails.DataSource = dt;
                    gvBalanceDetails.DataBind();

                }

            }
        }

        int Member_AutoId;
        string bal_rec_ID;

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
           
            var btnPre = (Control)sender;
            GridViewRow row = (GridViewRow)btnPre.NamingContainer;
            string i = "&nbsp;";
            bal_rec_ID = row.Cells[6].Text.ToString();
            if (bal_rec_ID.ToString() == i)
         {
             ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('You can not Edit course receipt !','Information');", true);
         }
            else
         {
           objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
           objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
           objBalance.Member_ID1 = Convert.ToInt32(e.CommandArgument.ToString());
           ViewState["member_autoid"] = Convert.ToInt32(e.CommandArgument.ToString());
           lblMemberAutoID.Text = e.CommandArgument.ToString();
             dt = objBalance.BalanceID();
                    if (dt.Rows.Count > 0)
                    {
                        TxtID.Text = dt.Rows[0]["Member_ID1"].ToString();
                        txtFirst.Text = dt.Rows[0]["FName"].ToString();
                        txtLast.Text = dt.Rows[0]["LName"].ToString();
                        ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
                        txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                        txtmail.Text = dt.Rows[0]["Email"].ToString();
                        gvBalanceDetails.DataSource = dt;
                        gvBalanceDetails.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
                    }
                    ddlReceipt.SelectedItem.Value = row.Cells[5].Text.ToString();
                    ddlReceipt.SelectedItem.Text = row.Cells[5].Text.ToString();
                   // ddlReceipt.SelectedValue = row.Cells[5].Text.ToString();
                    ddlReceipt.DataValueField = row.Cells[5].Text.ToString();
                    ddlReceipt.DataTextField=row.Cells[5].Text.ToString();
                    txtReceiptid.Text = row.Cells[6].Text.ToString();
                    objBalance.ReceiptID1 = Convert.ToInt32(ddlReceipt.SelectedItem.Value);     
                    dt = objBalance.Fees();
                    if (dt.Rows.Count > 0)
                    {
                        lblBalance.Text = dt.Rows[0]["Balance"].ToString();
                        txtBalance.Text = dt.Rows[0]["Balance"].ToString();
                    }
                    dt = objBalance.Fees1();
                    if (dt.Rows.Count > 0)
                    {
                        lblTotalFeeDue.Text = dt.Rows[0]["TotalFeeDue"].ToString();
                        txtTotal.Text = dt.Rows[0]["TotalFeeDue"].ToString();
                    }
                    dt = objBalance.Fees2();
                    if (dt.Rows.Count > 0)
                    {
                        lblPaidFee.Text = dt.Rows[0]["PaidFee"].ToString();
                        txtPaid.Text = dt.Rows[0]["PaidFee"].ToString();
                    }

                    dt.Clear();
                    objBalance.Bal_ReceiptID =Convert.ToInt32(row.Cells[6].Text);
                    dt = objBalance.Get_Edit_Payment();
                    gvBalancePayment.Columns[0].Visible = false;
                    gvBalancePayment.DataSource = dt;
                    gvBalancePayment.DataBind();      
            
                    divFormDetails.Visible = true;
                    divsearch.Visible = false;
                    divGridView.Visible = true;
                    btnSave.Text = "Update";
                    Div_paymode.Visible = false;
                    PaidEditFetch();
         }
         
        }
        public void getDataForEdit(int Member_AutoId)
        {
         


        }


    }
}