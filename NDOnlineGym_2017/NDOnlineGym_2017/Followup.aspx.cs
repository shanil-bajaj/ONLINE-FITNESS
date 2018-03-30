using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class Followup : System.Web.UI.Page
    {
        BalViewBalancePaymentFollowup obBalViewBalancePaymentFollowup = new BalViewBalancePaymentFollowup();
        BalCallRespondMaster obBalCallRespondMaster = new BalCallRespondMaster();
        BalFollowupTypeMaster obBalFollowupTypeMaster = new BalFollowupTypeMaster();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalFollowup objFollowup = new BalFollowup();
        DataTable dataTable = new DataTable();
        DataTable dt = new DataTable();
        BalAddMember objMemberDetails = new BalAddMember();
        int res;
        static int MemberAutoID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDDLCallRespond();
                BindFollowupTypeDDL();  // Bind Followup Type Drop Down List       
                txtSearch.Enabled = false;
                bindDDLExecutive();    // Assign Executive Name
                setExecutive();
                AssignDateAndTime();

            
                if (Request.QueryString["Member_ID"] != null)
                {
                    int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    GetMemberDetails(memberid);
                }

                if (Request.QueryString["BalancePayment_Member_AutoID"] != null)
                {
                    bindDDLFollowupPayment();
                    int Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
                    objFollowup.Member_AutoID = Member_AutoID;
                    ddlFollowupType.Enabled = false;
                    GetMemberDetails(Member_AutoID);
                    //if (Request.QueryString["FNBalPayFollDetail"] != null)
                    //{
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
                        BindGridByFollowupType();
                    //}
                }

                if (Request.QueryString["MembershipEnd_Member_AutoID"] != null)
                {
                    bindDDLFollowupMembershipEnd();
                    int Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
                    objFollowup.Member_AutoID = Member_AutoID;
                    ddlFollowupType.Enabled = false;
                    GetMemberDetails(Member_AutoID);
                    if (Request.QueryString["FNMemEndFollDetail"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                }

                if (Request.QueryString["Upgrade_Member_AutoID"] != null)
                {
                    bindDDLFollowupUpgrade();
                    int Member_AutoID = Convert.ToInt32(Request.QueryString["Upgrade_Member_AutoID"]);
                    objFollowup.Member_AutoID = Member_AutoID;
                    ddlFollowupType.Enabled = false;
                    GetMemberDetails(Member_AutoID);
                }

                if (Request.QueryString["Measurement_Member_AutoID"] != null)
                {
                    bindDDLFollowupMeasurement();
                    int Member_AutoID = Convert.ToInt32(Request.QueryString["Measurement_Member_AutoID"]);
                    objFollowup.Member_AutoID = Member_AutoID;
                    ddlFollowupType.Enabled = false;
                    GetMemberDetails(Member_AutoID);
                }

                if (Request.QueryString["Other_Member_AutoID"] != null)
                {

                    BindFollowupTypeDDL();
                    int Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
                    objFollowup.Member_AutoID = Member_AutoID;
                    GetMemberDetails(Member_AutoID);
                    //if (Request.QueryString["FNameMemProfile"] != null)
                    //{
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
                        BindGridByFollowupType();
                    //}
                }
                if (Request.QueryString["FID"] != null)
                {
                    if (!IsPostBack)
                    {
                        AssignID();
                        objFollowup.Followup_AutoID = Convert.ToInt32(Request.QueryString["FID"]);
                        objFollowup.Action = "GetDetailsByFollowupAutoID";
                        dataTable = objFollowup.GetDetails();
                        if (dataTable.Rows.Count > 0)
                        {
                            btnSave.Text = "Update";
                            txtMemberID.Enabled = false;
                            txtContact.Enabled = false;
                            ViewState["Followup_AutoID"] = dataTable.Rows[0]["Followup_AutoID"].ToString();
                            txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                            txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                            txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                            ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                            txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                            txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                            ddlFollowupType.SelectedIndex = Convert.ToInt32(dataTable.Rows[0]["FollowupType_AutoID"].ToString());
                            ddlCallPesponse.SelectedValue = dataTable.Rows[0]["CallResponse"].ToString();
                            ddlRating.SelectedValue = dataTable.Rows[0]["Rating"].ToString();

                            if (dataTable.Rows[0]["NextFollowupDate"].ToString() != "")
                            {
                                DateTime NFDate = Convert.ToDateTime(dataTable.Rows[0]["NextFollowupDate"].ToString());
                                DateTime NFDate1;
                                if (DateTime.TryParseExact(NFDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate1))
                                {
                                    txtNextFollowupDate.Text = NFDate1.ToString("dd-MM-yyyy");
                                }
                            }
                            else
                                txtNextFollowupDate.Text = "";
                            txtNextFollowupTime.Text = dataTable.Rows[0]["NextFollowupTime"].ToString();
                            DateTime todaydate;
                            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                            {
                                lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy");
                            }
                            txtComment.Text = dataTable.Rows[0]["Comment"].ToString();
                        }
                    }
                }
            }
        }

        public void BindGridByFollowupType()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (ddlFollowupType.SelectedValue != "--Select--")
                obBalViewBalancePaymentFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            else
                obBalViewBalancePaymentFollowup.FollowupType_AutoID = 0;
           // obBalViewBalancePaymentFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            //obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
            divSearch.Visible = false;
            btnClear.Visible = false;
            dt = obBalViewBalancePaymentFollowup.SelectFollDetails_By_MemAutoID();
            if (dt.Rows.Count > 0)
            {
                gvFollowupDetails.DataSource = dt;
                gvFollowupDetails.DataBind();
            }
            else
            {
                gvFollowupDetails.DataSource = null;
                gvFollowupDetails.DataBind();
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvFollowupDetails.Columns[0].Visible = false;
                gvFollowupDetails.Columns[1].Visible = false;
            }
        }

        public void bindDDLCallRespond()
        {
            try
            {
                obBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalCallRespondMaster.Select_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlCallPesponse.DataSource = dt;
                    ddlCallPesponse.DataValueField = "CallRespond_AutoID";
                    ddlCallPesponse.DataTextField = "Name";
                    ddlCallPesponse.DataBind();
                    ddlCallPesponse.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Call Respond Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AssignDateAndTime()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                txtNextFollowupTime.Text = localTime.ToString("HH:mm");
            }
        }

        public void bindDDLFollowupPayment()
        {
            try
            {
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_Payment();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLFollowupMembershipEnd()
        {
            try
            {
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_MembershipEnd();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLFollowupUpgrade()
        {
            try
            {
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_Upgrade();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLFollowupMeasurement()
        {
            try
            {
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_Measurement();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetMemberDetails(int memberid)
        {
            txtMemberID.Enabled = false;
            txtContact.Enabled = false;
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Member_AutoID = Convert.ToInt32(memberid);
            dt = objMemberDetails.SelectByID_MemberInformation();
            if (dt.Rows.Count > 0)
            {
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                txtFirst.Text = dt.Rows[0]["FName"].ToString();
                txtLast.Text = dt.Rows[0]["LName"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                txtmail.Text = dt.Rows[0]["Email"].ToString();
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

        #region ------Bind Followup Type --------
        private void BindFollowupTypeDDL()
        {
            try
            {
                AssignID();
                objFollowup.Action = "Select_FollowupType";
                dataTable = objFollowup.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    ddlFollowupType.DataSource = dataTable;
                    ddlFollowupType.Items.Clear();
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion       

        #region -------- Assign Company and Branch ID ------------
        private void AssignID()
        {
            objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objFollowup.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }
        #endregion

        #region ------ Search Member Details By Member ID --------
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != string.Empty)
                {
                    AssignID();
                    objFollowup.Member_ID = Convert.ToInt32(txtMemberID.Text.Trim());
                    objFollowup.Action = "SearchByMemberID";

                    dataTable = objFollowup.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberIdNotFound();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    }
                    txtMemberID.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }   
        }
        #endregion

        #region  ------ Clear Fields When Search Member Details By Member ID --------
        private void ClearFieldMemberIdNotFound()
        {
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtContact.Text = string.Empty;
            txtmail.Text = string.Empty;
            MemberAutoID = 0;
        }
        #endregion

        #region ------ Search Member Details By Contact Number --------
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != string.Empty)
                {
                    AssignID();
                    objFollowup.Contact = txtContact.Text;
                    objFollowup.Action = "SearchByContact";

                    dataTable = objFollowup.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberContNotFound();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    }
                    txtContact.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }   
        }
        #endregion

        #region ------ Clear Fields When Search Member Details By Contact Number --------
        private void ClearFieldMemberContNotFound()
        {
            txtMemberID.Text = string.Empty;
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;            
            txtmail.Text = string.Empty;
            MemberAutoID = 0;
        }
        #endregion

        #region --------- Save Button --------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtMemberID.Text == string.Empty || txtContact.Text == string.Empty || ddlFollowupType.SelectedValue == "--Select--" || ddlCallPesponse.SelectedValue == "--Select--" || ddlRating.SelectedValue == "--Select--"
                    || txtNextFollowupDate.Text == string.Empty || txtNextFollowupTime.Text == string.Empty || txtComment.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Enter All Fields','Error');", true);

                    if (txtMemberID.Text == "")
                    { txtMemberID.Style.Add("border", "1px solid red "); }

                    if (txtContact.Text == "")
                    { txtContact.Style.Add("border", "1px solid red "); }

                    if (ddlFollowupType.SelectedValue == "--Select--")
                    { ddlFollowupType.Style.Add("border", "1px solid red "); }
                 
                    if (ddlCallPesponse.SelectedValue == "--Select--")
                    { ddlCallPesponse.Style.Add("border", "1px solid red "); }

                    if (ddlRating.SelectedValue == "--Select--")
                    { ddlRating.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupDate.Text == "")
                    { txtNextFollowupDate.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupTime.Text == "")
                    { txtNextFollowupTime.Style.Add("border", "1px solid red "); }

                    if (txtComment.Text == "")
                    { txtComment.Style.Add("border", "1px solid red "); }
                }
                else
                {
                    txtMemberID.Style.Add("border", "1px solid silver  ");
                    txtContact.Style.Add("border", "1px solid silver  ");
                    ddlFollowupType.Style.Add("border", "1px solid silver  ");
                    //txtExecutive.Style.Add("border", "1px solid silver  ");
                    ddlCallPesponse.Style.Add("border", "1px solid silver  ");
                    ddlRating.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
                    txtComment.Style.Add("border", "1px solid silver  ");                    
                    AssignID();
                    AddParameters();

                    if (btnSave.Text == "Save")
                    {
                        objFollowup.Action = "INSERT";                                               
                        res = objFollowup.Insert_FollowupInformation();

                        if (res > 0)
                        {
                            //if (Request.QueryString["BalancePayment_Member_AutoID"] != null)
                            //{
                            //    int memberid = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
                            //    //Response.Redirect("BalancePayment.aspx?BalancePayment_Member_AutoID=" + memberid);                                
                            //}
                            //else
                            //{
                                ClearAllField();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                            //}
                        }
                        if (Request.QueryString["FNAllFollowupDetailsBal"] != null)
                        {
                            string Pagename = "FNAllFollowupDetailsBal";
                            Response.Redirect("AllFollowup.aspx?FNAllFollowupDetailsBal=" + Pagename);
                        }
                        else if (Request.QueryString["FNameSearchPage"] != null)
                        {
                            int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                            Response.Redirect("SearchPage.aspx?Member_AutoID=" + memberid + " &FNameSearchPage1=" + HttpUtility.UrlEncode("FNameSearchPage1".ToString()));
                        }
                        else if (Request.QueryString["FNMemEndFollDetail"] != null)
                        {
                            Response.Redirect("ViewMemberEndFollowup.aspx");
                        }
                        else if (Request.QueryString["FNBalPayFollDetail"] != null)
                        {
                            Response.Redirect("ViewBalancePaymentFollowup.aspx");
                        }
                        else if (Request.QueryString["FNMemberFollDetail.aspx"] != null)
                        {
                            Response.Redirect("ViewMembershipFollowup.aspx");
                        }
                        else if (Request.QueryString["FNameMemProfile"] != null)
                        {
                            int MemberAutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
                            Response.Redirect("MemberProfile.aspx?MemberId=" + MemberAutoID);
                        }
                        else if (Request.QueryString["FNameMemDetails"] != null)
                        {
                            int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                            Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        objFollowup.Action = "Update";                       
                        objFollowup.Followup_AutoID = Convert.ToInt32(ViewState["Followup_AutoID"]);
                        res = objFollowup.Insert_FollowupInformation();
                        if (res > 0)
                        {                           
                                btnSave.Text = "Save";
                                txtMemberID.Enabled = true;
                                txtContact.Enabled = true;
                                ClearAllField();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            } 
        }

        private void AddParameters()
        {
            if (Request.QueryString["BalancePayment_Member_AutoID"] != null)
                    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
            else if (Request.QueryString["MembershipEnd_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
            else if (Request.QueryString["Upgrade_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Upgrade_Member_AutoID"]);
            else if (Request.QueryString["Measurement_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Measurement_Member_AutoID"]);
            else if (Request.QueryString["Other_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
            else
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Member_ID"]);
            objFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            objFollowup.CallRespond_AutoID = ddlCallPesponse.SelectedValue;
            objFollowup.Rating = ddlRating.SelectedValue;


            DateTime NFDate;
            if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate))
            {
            }
            if (ddlRating.SelectedValue != "Not Interested")
                objFollowup.NextFollowupDate = NFDate;
            else
                objFollowup.NextFollowupDate = null;

            DateTime FDate;
            if (DateTime.TryParseExact(lblFollowupDateTime.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FDate))
            {
            }
            objFollowup.FollowupDate = FDate;

            objFollowup.NextFollowupTime = Convert.ToDateTime(txtNextFollowupTime.Text.ToString());
            objFollowup.FollowupTime = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt"));
            objFollowup.Comment = Regex.Replace(txtComment.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objFollowup.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
            
        }

        private void ClearAllField()
        {
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false; 

            //txtMemberID.Text = string.Empty;
            //txtContact.Text = string.Empty;
            //txtFirst.Text = string.Empty;
            //txtLast.Text = string.Empty;
            //ddlGender.SelectedIndex = 0;           
            //txtmail.Text = string.Empty;
            //MemberAutoID = 0;

            ddlFollowupType.SelectedIndex = 0;
            //txtExecutive.Text = string.Empty;
            ddlCallPesponse.SelectedIndex = 0;
            ddlRating.SelectedIndex = 0;
            AssignDateAndTime();
            //txtNextFollowupDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");    // Assign Next Followup Date
            //lblFollowupDateTime.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");   // Assign Followup Date
            //txtNextFollowupTime.Text = string.Empty;
            txtComment.Text = string.Empty;
        }
        #endregion

        #region ------------- Search DDL Index Changed ---------------
        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                if (ddlSearch.SelectedValue.ToString() == "All")
                {
                    txtSearch.Style.Add("border", "1px solid silver ");
                    txtSearch.Enabled = false;

                }
                else
                {
                    txtSearch.Enabled = true;
                }
                ddlSearch.Focus();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region ----------- Search Button ------------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Enabled == true && txtSearch.Text == string.Empty)
                {
                    txtSearch.Style.Add("border", "1px solid red ");
                    txtSearch.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter All Fields !!!','Error');", true);
                }
                else
                {
                    txtSearch.Style.Add("border", "1px solid silver ");
                    BindGridViewDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ------------ Bind GridView -----------------
        private void BindGridViewDetails()
        {
            SeacrhAction();
            AssignID();

            //dataTable.Clear();
            dataTable = objFollowup.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                gvFollowupDetails.DataSource = dataTable;
                gvFollowupDetails.DataBind();
            }
            else
            {
                gvFollowupDetails.DataSource = dataTable;
                gvFollowupDetails.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvFollowupDetails.Columns[0].Visible = false;
                gvFollowupDetails.Columns[1].Visible = false;
            }
        }
        #endregion

        #region ------------ Assign Search Action -------------
        private void SeacrhAction()
        {
            try
            {
                objFollowup.Action = "BindDetails";

                if (ddlSearch.SelectedValue.ToString() == "All")
                {
                    objFollowup.Category = "All";
                    txtSearch.Enabled = false;
                }
                else if (ddlSearch.SelectedValue.ToString() == "MemberID")
                {
                    objFollowup.Category = "MemberID";
                    objFollowup.SearchByText = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "MemberName")
                {
                    objFollowup.Category = "MemberName";
                    objFollowup.SearchByText = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "FollowupType")
                {
                    objFollowup.Category = "FollowupType";
                    objFollowup.SearchByText = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Contact")
                {
                    objFollowup.Category = "Contact";
                    objFollowup.SearchByText = txtSearch.Text;
                }               
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    ddlSearch.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------- Delete By Followup ID --------
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();
                objFollowup.Followup_AutoID = Convert.ToInt32(e.CommandArgument);
                objFollowup.Action = "DeleteByFollowupAutoID";
                int i = objFollowup.Insert_FollowupInformation();
                if (i > 0)
                {
                    //BindGridViewDetails();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    if (Request.QueryString["FNBalPayFollDetail"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                    if (Request.QueryString["FNMemEndFollDetail"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                    if (Request.QueryString["FNameMemProfile"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                }
                else if(i == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region --------- Edit By Followup ID  --------
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();                
                objFollowup.Followup_AutoID = Convert.ToInt32(e.CommandArgument);
                objFollowup.Action = "GetDetailsByFollowupAutoID";

                dataTable = objFollowup.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    btnSave.Text = "Update";
                    txtMemberID.Enabled = false;
                    txtContact.Enabled = false;
                    ViewState["Followup_AutoID"] = dataTable.Rows[0]["Followup_AutoID"].ToString();
                    txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                    txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                    txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                    ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                    txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    ddlFollowupType.SelectedValue = dataTable.Rows[0]["FollowupType_AutoID"].ToString();
                    ddlCallPesponse.SelectedValue = dataTable.Rows[0]["CallRespond_AutoID"].ToString();
                    ddlRating.SelectedValue = dataTable.Rows[0]["Rating"].ToString();
                    ddlExecutive.SelectedValue = dataTable.Rows[0]["Executive_ID"].ToString();
                    if (dataTable.Rows[0]["NextFollowupDate"].ToString() != "")
                    {
                        DateTime NFDate = Convert.ToDateTime(dataTable.Rows[0]["NextFollowupDate"].ToString());
                        DateTime NFDate1;
                        if (DateTime.TryParseExact(NFDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate1))
                        {
                            txtNextFollowupDate.Text = NFDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtNextFollowupDate.Text = "";

                    //string NextFollowupDate = Convert.ToDateTime(dataTable.Rows[0]["NextFollowupDate"]).ToString("dd-MM-yyyy");
                    //txtNextFollowupDate.Text = NextFollowupDate;
                    txtNextFollowupTime.Text = dataTable.Rows[0]["NextFollowupTime"].ToString();
                    DateTime todaydate;
                    if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy"); 
                    }
                    txtComment.Text = dataTable.Rows[0]["Comment"].ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region ------------ Clear Button ------------
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllField();
            txtMemberID.Style.Add("border", "1px solid silver  ");
            txtContact.Style.Add("border", "1px solid silver  ");
            ddlFollowupType.Style.Add("border", "1px solid silver  ");
            //txtExecutive.Style.Add("border", "1px solid silver  ");
            ddlCallPesponse.Style.Add("border", "1px solid silver  ");
            ddlRating.Style.Add("border", "1px solid silver  ");
            txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
            txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
            txtComment.Style.Add("border", "1px solid silver  ");
        }
        #endregion

        protected void gvFollowupDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFollowupDetails.PageIndex = e.NewPageIndex;
            BindGridViewDetails();
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
        }

        protected void ddlRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlRating.SelectedValue == "Not Interested")
                txtNextFollowupDate.Enabled=false;
            else
                txtNextFollowupDate.Enabled=true;
        }


    }
}