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
using System;


namespace NDOnlineGym_2017
{
    public partial class Upgrade : System.Web.UI.Page
    {
        BalUpgrade objUpgrage = new BalUpgrade();
        BalPackage pack = new BalPackage();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();

        DataTable dataTable = new DataTable();
        DataTable dataTable1 = new DataTable();
        DataSet dataSet = new DataSet();
        static int Upgrade_AutoID;
        static int Member_AutoId = 0;
        static int Member_Id1 = 0;
        static int OldCourseAmotunt = 1;
        static int Quantity = 1;
        static string OldCourStartDate = "";
        DateTime CourseNewStartDate;
        DateTime CourseNewEndDate;

        int flag = 0;
        int res,res1,res2;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindDDLExecutive();
                    setExecutive();
                    BindPackageGridview();

                    if (Request.QueryString["UpgradeDetails"] != null)
                    {
                        //txtSearch.Enabled = false;
                        divUpgradeDetails.Visible = true;
                        divMemberUpgrade.Visible = false;
                        divsearch.Visible = true;
                        divFormDetails.Visible = false;
                        txtFromDate.Focus();
                        Assign_MonthDate();
                        SearchByDateFunction();
                    }
                    else
                    {                        
                        if (Request.QueryString["MemberID"] != null)
                        {

                            if (Request.QueryString["FNameMemDetails"] != null)
                            {
                                objUpgrage.Member_ID1 = Convert.ToInt32(Request.QueryString["MemberID"]);
                                objUpgrage.Member_ID1 = getMemIDByAutoID();
                            }
                            else
                            {
                                objUpgrage.Member_ID1 = Convert.ToInt32(Request.QueryString["MemberID"]);
                            }
                            
                            objUpgrage.Action = "SearchByMember_ID";
                            BindMemberDetails();
                        }

                        txtMemberID1.Focus();
                        AssignUpgradeDate();                        
                    }                    
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
                objUpgrage.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                objUpgrage.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objUpgrage.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region
        int member_Id;
        private int getMemIDByAutoID()
        {            
            AssignID();
            objUpgrage.Action = "GetMemIdByAutoId";
            dataSet = objUpgrage.GetDetails();
            if (dataSet.Tables[0].Columns.Count > 0)
            {
                member_Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Member_ID1"].ToString());
            }

            return member_Id;
        }
        #endregion

        #region ----------- Assign Upgrade Date -------------
        protected void AssignUpgradeDate()
        {
            try
            {
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local                   
                txtUpgradeDate.Text = localTime.ToString("dd-MM-yyyy");
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

        #region ---------- Meber ID Text Change Event -----------------
        protected void txtMemberID1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID1.Text != string.Empty)
                {
                    ClearFormOnMemberID();
                    objUpgrage.Member_ID1 = Convert.ToInt32(txtMemberID1.Text);
                    objUpgrage.Action = "SearchByMember_ID";
                    BindMemberDetails();
                    txtMemberID1.Focus();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void ClearFormOnMemberID()
        {
            try
            {
                            
                txtFirstName.Text = "";
                txtLastName.Text = "";
                ddlGender.SelectedIndex = 0;
                txtContact.Text = "";
                txtEmail.Text = "";
                txtTotal.Text = "0";
                txtPaid.Text = "0";
                txtBalance.Text = "0";
                lblTotalFeeDue.Text = "0";
                lblPaidFee.Text = "0";
                lblBalance.Text = "0";

                Member_AutoId = 0;
                Member_Id1 = 0;
                OldCourseAmotunt = 1;
                Quantity = 1;
                OldCourStartDate = "";

                ViewState["UpgradeDetails"] = null;
                ViewState["dataTable1"] = null;
                dataTable1 = (DataTable)ViewState["dataTable1"];

                ddlReceipt.Items.Clear();
                //ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                //ddlReceipt.SelectedIndex = 0;

                gvOCourse.Visible = false;
                gvOldCourse.DataSource = dataTable1;
                gvOldCourse.DataBind();

                divPakageAssign.Visible = false;
                GvPakageAssign.DataSource = dataTable1;
                GvPakageAssign.DataBind();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            
        }
        #endregion

        #region ---------- Meber Contact Text Change Event -----------------
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != string.Empty)
                {
                    ClearFormOnMemberContact();
                    objUpgrage.Contact1 = txtContact.Text;
                    objUpgrage.Action = "SearchByMember_Contact1";
                    BindMemberDetails();
                    txtContact.Focus();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void ClearFormOnMemberContact()
        {
            try
            {
                txtMemberID1.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                ddlGender.SelectedIndex = 0;
                txtEmail.Text = "";
                txtTotal.Text = "0";
                txtPaid.Text = "0";
                txtBalance.Text = "0";
                lblTotalFeeDue.Text = "0";
                lblPaidFee.Text = "0";
                lblBalance.Text = "0";

                Member_AutoId = 0;
                Member_Id1 = 0;
                OldCourseAmotunt = 1;
                Quantity = 1;
                OldCourStartDate = "";

                ViewState["UpgradeDetails"] = null;
                ViewState["dataTable1"] = null;
                dataTable1 = (DataTable)ViewState["dataTable1"];

                ddlReceipt.Items.Clear();
                //ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                //ddlReceipt.SelectedIndex = 0;

                gvOCourse.Visible = false;
                gvOldCourse.DataSource = dataTable1;
                gvOldCourse.DataBind();

                divPakageAssign.Visible = false;
                GvPakageAssign.DataSource = dataTable1;
                GvPakageAssign.DataBind();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
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
                dataSet = objUpgrage.GetDetails();
                dataSet.Tables[0].TableName = "MemberInfo";

                if (dataSet.Tables["MemberInfo"].Rows.Count != 0)
                {
                    Member_AutoId = Convert.ToInt32(dataSet.Tables["MemberInfo"].Rows[0]["Member_AutoID"].ToString());
                    Member_Id1 = Convert.ToInt32(dataSet.Tables["MemberInfo"].Rows[0]["Member_ID1"].ToString());

                    if (dataSet.Tables["MemberInfo"].Rows[0]["BlockStatus"].ToString() != "Block")
                    {
                        txtMemberID1.Text = dataSet.Tables["MemberInfo"].Rows[0]["Member_ID1"].ToString();
                        txtFirstName.Text = dataSet.Tables["MemberInfo"].Rows[0]["FName"].ToString();
                        txtLastName.Text = dataSet.Tables["MemberInfo"].Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataSet.Tables["MemberInfo"].Rows[0]["Gender"].ToString();
                        txtContact.Text = dataSet.Tables["MemberInfo"].Rows[0]["Contact1"].ToString();
                        txtEmail.Text = dataSet.Tables["MemberInfo"].Rows[0]["email"].ToString();

                        //txtNewFirstName.Focus();

                        objUpgrage.Member_ID1 = Member_Id1;
                        objUpgrage.Action = "CourseBy_MemberID";

                        dataTable = objUpgrage.SelectDetails();
                        if (dataTable.Rows.Count > 0)
                        {
                            ddlReceipt.DataSource = dataTable;
                            ddlReceipt.Items.Clear();
                            ddlReceipt.DataValueField = "ReceiptID";
                            ddlReceipt.DataTextField = "ReceiptID";
                            ddlReceipt.DataBind();
                            ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Course Not Assign !!!','Information');", true);
                        }

                    }
                    else
                    {
                        string url = "Termination.aspx?Member_ID=" + Member_Id1 + " &FNameUpgradePage=" + HttpUtility.UrlEncode("CourseUpgrade");
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
        protected void ddlReceipt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlReceipt.SelectedValue != "--Select--")
                {
                    gvOCourse.Visible = true;

                    AssignID();

                    objUpgrage.ReceiptID = Convert.ToInt32(ddlReceipt.SelectedItem.Value);
                    objUpgrage.Action = "CourseBy_ReceiptNo";
                    dataSet = objUpgrage.GetDetails();
                    dataSet.Tables[0].TableName = "CourseDetails";
                    dataSet.Tables[1].TableName = "PaymentDetails";

                    if (dataSet.Tables["PaymentDetails"].Rows.Count > 0)
                    {
                        txtTotal.Text = Convert.ToString(dataSet.Tables["PaymentDetails"].Rows[0]["TotalFeeDue"]);
                        txtPaid.Text = Convert.ToString(dataSet.Tables["PaymentDetails"].Rows[0]["PaidFee"]);
                        txtBalance.Text = Convert.ToString(dataSet.Tables["PaymentDetails"].Rows[0]["TotalBalance"]);
                    }

                    if (dataSet.Tables["CourseDetails"].Rows.Count > 0)
                    {
                        OldCourseAmotunt = Convert.ToInt32(dataSet.Tables["CourseDetails"].Rows[0]["Amount"]);
                        Quantity = Convert.ToInt32(dataSet.Tables["CourseDetails"].Rows[0]["Qty"]);

                        DateTime date = Convert.ToDateTime(dataSet.Tables["CourseDetails"].Rows[0]["StartDate"]);

                        OldCourStartDate = date.ToString("dd-MM-yyyy");

                        gvOldCourse.Visible = true;
                        gvOldCourse.DataSource = dataSet.Tables["CourseDetails"];
                        gvOldCourse.DataBind();
                    }

                }
                else
                {
                    txtTotal.Text = "0";
                    txtPaid.Text = "0";
                    txtBalance.Text = "0";
                    
                    gvOCourse.Visible = false;
                    gvOldCourse.Visible = false;
                    dataTable = null;
                    gvOldCourse.DataSource = dataTable;
                    gvOldCourse.DataBind();

                }
                ddlReceipt.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------- Clear All Form Details ---------------
        private void ClearAllForm()
        {
            try
            {
                btnUpgrade.Text = "Save";               
                txtMemberID1.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                ddlGender.SelectedIndex = 0;
                txtContact.Text = "";
                txtEmail.Text = "";
                txtTotal.Text = "0";
                txtPaid.Text = "0";
                txtBalance.Text = "0";
                lblTotalFeeDue.Text = "0";
                lblPaidFee.Text = "0";
                lblBalance.Text = "0";

                AssignUpgradeDate();

                Member_AutoId = 0;
                Member_Id1 = 0;
                OldCourseAmotunt = 1;
                Quantity = 1;
                OldCourStartDate = "";

                txtMemberID1.Enabled = true;
                txtContact.Enabled = true;
                ddlReceipt.Enabled = true;

                setExecutive();
                BindPackageGridview();

                ViewState["UpgradeDetails"] = null;
                ViewState["dataTable1"] = null;
                dataTable1 = (DataTable)ViewState["dataTable1"];

                ddlReceipt.Items.Clear();
                ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                ddlReceipt.SelectedIndex = 0;

                

                gvOCourse.Visible = false;
                gvOldCourse.DataSource = dataTable1;
                gvOldCourse.DataBind();               
               
                divPakageAssign.Visible = false;
                GvPakageAssign.DataSource = dataTable1;
                GvPakageAssign.DataBind();
                


            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            
        }
        #endregion

        #region ------------- Bind New Course Gridview ------------
        public void BindPackageGridview()
        {
            try
            {
                pack.Category = "Active";

                pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable.Clear();
                dataTable=pack.Get_Search();                

                if (dataTable.Rows.Count > 0)
                {
                    gvCourse.DataSource = dataTable;
                    gvCourse.DataBind();
                }
                else
                {
                    gvCourse.DataSource = dataTable;
                    gvCourse.DataBind();

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Couser Package Not Found !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------- Add Upgrade Cousre To Grid View ------------

        protected bool IsUpgradeContinue()
        {
            int upgradeDays=0;
            if (DateTime.TryParseExact(OldCourStartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewStartDate))
            {
            }

            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local   

            int DaysDiff = (localTime - CourseNewStartDate).Days;

            AssignID();
            objUpgrage.Action = "UpgradeDays";
            dataTable = objUpgrage.SelectDetails();

            if (dataTable.Rows.Count > 0)
            {               
                upgradeDays = Convert.ToInt32(dataTable.Rows[0]["Days"].ToString());                                  
            }
            else
            {                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Insert Days in Upgrade Master !!!','Error');", true);
            }

            if (upgradeDays >= DaysDiff)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpgradeContinue;
        public int k = 0;
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID1.Text != string.Empty || txtContact.Text != string.Empty)
                {
                    if (gvOldCourse.Rows.Count > 0)
                    {
                        string OldCourseEndDate = "";
                        foreach (GridViewRow row in gvOldCourse.Rows)
                        {
                            OldCourseEndDate = row.Cells[9].Text.Trim();
                        }
                        DateTime CourseEndDate;
                        if (DateTime.TryParseExact(OldCourseEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseEndDate))
                        {
                        }

                        DateTime utcTime = DateTime.UtcNow;
                        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local  

                        if (CourseEndDate.Date >= localTime.Date)
                        {
                            UpgradeContinue = IsUpgradeContinue();

                            if (UpgradeContinue == true)
                            {
                                if (GvPakageAssign.Rows.Count == 0)
                                {
                                    var btnPre = (Control)sender;
                                    GridViewRow row1 = (GridViewRow)btnPre.NamingContainer;

                                    int NewCourseAmt = Convert.ToInt32(row1.Cells[5].Text);

                                    if (OldCourseAmotunt <= NewCourseAmt)
                                    {
                                        flag = 0;
                                    }
                                    else
                                    {
                                        flag = 1;
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Higher Plan than previous plan !!!','Error');", true);
                                    }


                                    if (flag == 0)
                                    {
                                        divPakageAssign.Visible = true;
                                        DataRow dr = null;
                                        //dataTable1.Clear();
                                        //dataTable1.Columns.Add(new DataColumn("ID"));
                                        dataTable1.Columns.Add(new DataColumn("Pack_AutoID"));
                                        dataTable1.Columns.Add(new DataColumn("Package"));
                                        dataTable1.Columns.Add(new DataColumn("Duration"));
                                        dataTable1.Columns.Add(new DataColumn("Session"));
                                        dataTable1.Columns.Add(new DataColumn("Amount"));
                                        dataTable1.Columns.Add(new DataColumn("StartDate"));
                                        dataTable1.Columns.Add(new DataColumn("EndDate"));
                                        dataTable1.Columns.Add(new DataColumn("Quantity"));
                                        dataTable1.Columns.Add(new DataColumn("Total"));
                                        dataTable1.Columns.Add(new DataColumn("Discount"));
                                        dataTable1.Columns.Add(new DataColumn("FinalTotal"));

                                        if (ViewState["dataTable1"] != null)
                                        {
                                            dataTable1 = (DataTable)ViewState["dataTable1"];
                                        }

                                        bool exists = dataTable1.Select().ToList().Exists(row => row["Pack_AutoID"].ToString().ToUpper() == row1.Cells[1].Text);

                                        if (exists == false)
                                        {
                                            k = dataTable1.Rows.Count;
                                            dr = dataTable1.NewRow();
                                            //dr["ID"] = k;
                                            dr["Pack_AutoID"] = row1.Cells[1].Text;
                                            dr["Package"] = row1.Cells[2].Text;
                                            dr["Duration"] = row1.Cells[3].Text;
                                            dr["Session"] = row1.Cells[4].Text;
                                            dr["Amount"] = row1.Cells[5].Text;


                                            if (DateTime.TryParseExact(OldCourStartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewStartDate))
                                            {
                                            }

                                            int Days = Convert.ToInt32(row1.Cells[3].Text);

                                            string CEnd = CourseNewStartDate.AddDays(Days).ToString("dd-MM-yyyy");

                                            if (DateTime.TryParseExact(CEnd, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewEndDate))
                                            {
                                            }

                                            dr["StartDate"] = CourseNewStartDate.ToString("dd-MM-yyyy");
                                            dr["EndDate"] = CourseNewEndDate.ToString("dd-MM-yyyy");
                                            dr["Quantity"] = Quantity;
                                            dr["Total"] = Quantity * Convert.ToInt32(row1.Cells[5].Text);
                                            dr["Discount"] = 0;
                                            dr["FinalTotal"] = Quantity * Convert.ToInt32(row1.Cells[5].Text);

                                            int Total_Fess = Quantity * Convert.ToInt32(row1.Cells[5].Text);
                                            int Balance_Fess = Total_Fess - Convert.ToInt32(txtPaid.Text);
                                            lblTotalFeeDue.Text = Convert.ToString(Total_Fess);
                                            lblPaidFee.Text = txtPaid.Text;
                                            lblBalance.Text = Convert.ToString(Balance_Fess);

                                            dataTable1.Rows.InsertAt(dr, k);
                                            k++;
                                        }

                                        ViewState["dataTable1"] = dataTable1;
                                        GvPakageAssign.DataSource = dataTable1;
                                        GvPakageAssign.DataBind();
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('You Can Not Upgrade More Than One Course !!!','Error');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('You Can Not Upgrade This Course Because Upgrade Days Limit Over !!!','Error');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Another Course, Because You Can Not Upgrade Expired Course !!!','Error');", true);
                        }
                    }
                    else
                    {
                        ddlReceipt.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Old Course !!!','Error');", true);
                    }
                }
                else
                {
                    txtMemberID1.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Record !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
       
        #endregion

        #region --------- Remove Upgrade Cousre From Grid View ------------
        protected void GvPakageAssign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {               
                int index = Convert.ToInt32(e.RowIndex);
                dataTable1 = (DataTable)ViewState["dataTable1"];

                lblTotalFeeDue.Text = "0";
                lblPaidFee.Text = "0";
                lblBalance.Text = "0";

                dataTable1.Rows[index].Delete();
                dataTable1.Clear();

                ViewState["dataTable1"] = dataTable1;
               
                divPakageAssign.Visible = false;
                GvPakageAssign.DataSource = dataTable1;
                GvPakageAssign.DataBind();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ----------- Text Change Event Of Discount ------------
        protected void txtDisc_TextChanged(object sender, EventArgs e)
        {

            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
                dataTable1 = (DataTable)ViewState["dataTable1"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr;
                dr = dataTable1.NewRow();


                int duration;

                int s = currentRow.RowIndex;
                //dr["ID"] = s;// k;
                dr["Pack_AutoID"] = row.Cells[1].Text;
                dr["Package"] = row.Cells[2].Text;
                duration = Convert.ToInt32(row.Cells[3].Text);
                dr["Duration"] = row.Cells[3].Text;
                dr["Session"] = row.Cells[4].Text;


                TextBox txtsdate = (TextBox)currentRow.FindControl("txtsDate");
                dr["StartDate"] = txtsdate.Text;

                TextBox txtedate = (TextBox)currentRow.FindControl("txtEndate");
                dr["EndDate"] = txtedate.Text;

                TextBox txtamt = (TextBox)currentRow.FindControl("txtAmt");
                dr["Amount"] = txtamt.Text;

                TextBox Quantity = (TextBox)currentRow.FindControl("txtQuantity");
                dr["Quantity"] = Quantity.Text;

                TextBox Total = (TextBox)currentRow.FindControl("txtTotal");
                dr["Total"] = Total.Text;

                TextBox txtdisc = (TextBox)currentRow.FindControl("txtDisc");
                dr["Discount"] = txtdisc.Text;

                int finalTotal = Convert.ToInt32(Total.Text) - Convert.ToInt32(txtdisc.Text);
                dr["FinalTotal"] = finalTotal;


                dataTable1.Rows[s].Delete();
                dataTable1.Rows.InsertAt(dr, s);
                ViewState["dataTable1"] = dataTable1;
                GvPakageAssign.DataSource = dataTable1;
                GvPakageAssign.DataBind();

                int Balance_Fess = finalTotal - Convert.ToInt32(txtPaid.Text);
                lblTotalFeeDue.Text = Convert.ToString(finalTotal);
                lblPaidFee.Text = txtPaid.Text;
                lblBalance.Text = Convert.ToString(Balance_Fess);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region --------------- Upgrade Button -------------------
        protected void btnUpgrade_Click(object sender, EventArgs e)
        {
            try
            {
                UpgradeFunction();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        protected void UpgradeFunction()
        {
            try
            {
                if (txtMemberID1.Text != string.Empty || txtContact.Text != string.Empty)
                {
                    if (gvOldCourse.Rows.Count > 0)
                    {
                      if (GvPakageAssign.Rows.Count > 0)
                         {

                            AssignID();
                            objUpgrage.ReceiptID = Convert.ToInt32(ddlReceipt.SelectedValue);
                            objUpgrage.Action = "MemberAutoID_By_Receipt";
                            //dataTable = objUpgrage.SelectDetails();
                            dataSet = objUpgrage.SelectDetails1();

                            dataSet.Tables[0].TableName = "Member_Record";
                            dataSet.Tables[1].TableName = "Paid_Fee";              

                            AddParameter();

                            // Insert Upgrade Record               
                            if (dataSet.Tables["Member_Record"].Rows.Count > 0)                
                            {
                                foreach (DataRow dtRow in dataSet.Tables["Member_Record"].Rows)
                                {
                                    if (btnUpgrade.Text == "Save")
                                    {
                                        objUpgrage.Action = "INSERT";
                                    }
                                    else
                                    {
                                        objUpgrage.Action = "Update";
                                        objUpgrage.Upgrade_AutoID = Upgrade_AutoID;
                                    }

                                    string MemberAutoID = dtRow["Member_AutoID"].ToString();

                                    objUpgrage.Member_AutoID = Convert.ToInt32(MemberAutoID);

                                    res = objUpgrage.Insert_Update_Record();

                                }

                                if (res > 0)
                                {
                                    objUpgrage.Action = "UpdateCourse";
                                    res1 = objUpgrage.Insert_Update_Record();
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                                }

                                if (res1 > 0)
                                {
                                    if (dataSet.Tables["Paid_Fee"].Rows.Count > 0)
                                    {
                                        int receiptID,balReceiptID;
                                        objUpgrage.Action = "UpdateBalnce";
                                        int remeingBalance = Convert.ToInt32(lblTotalFeeDue.Text);
                                        foreach (DataRow row in dataSet.Tables["Paid_Fee"].Rows)
                                        {
                                             receiptID = Convert.ToInt32(row["ReceiptID"]);
                                            if (row["Bal_ReceiptID"].ToString() != string.Empty)
                                            {
                                                balReceiptID = Convert.ToInt32(row["Bal_ReceiptID"]);
                                                objUpgrage.Bal_Receipt = balReceiptID;
                                            }
                                            int paidFees = Convert.ToInt32(row["PaidFee"]);

                                            remeingBalance = remeingBalance - paidFees;
                                            objUpgrage.New_Balance = remeingBalance;
                                            objUpgrage.ReceiptID = receiptID;

                                            res2 = objUpgrage.Insert_Update_Record();
                                        }
                                    }

                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                                }


                                
                                if (res2 > 0)
                                {
                                    if (btnUpgrade.Text == "Save")
                                    {
                                        txtMemberID1.Focus();
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                                    }
                                    else
                                    {
                                        //txtSearch.Enabled = false;
                                        divUpgradeDetails.Visible = true;
                                        divMemberUpgrade.Visible = false;
                                        divsearch.Visible = true;
                                        divFormDetails.Visible = false;
                                        btnSearch.Focus();
                                        Assign_MonthDate();
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                                    }

                                    ClearAllForm();
                                                                      
                                }
                                else
                                {
                                    if (txtUpgradeDate.Text == "Save")
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Updated Failed !!!','Error');", true);
                                    }
                                }

                            }
                         }
                      else
                      {
                          ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Upgraded Course !!!','Error');", true);
                      }
                    }
                    else
                    {
                        ddlReceipt.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Old Course !!!','Error');", true);
                    }
                }
                else
                {
                    txtMemberID1.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Record !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void AddParameter()
        {
            try
            {

                objUpgrage.ReceiptID = Convert.ToInt32(ddlReceipt.SelectedValue);
                foreach (GridViewRow row in gvOldCourse.Rows)
                {
                    objUpgrage.Old_Pack_AutoID = Convert.ToInt32(row.Cells[0].Text.Trim());
                    objUpgrage.Old_Amount = Convert.ToInt32(row.Cells[4].Text.Trim());
                    objUpgrage.Qty = Convert.ToInt32(row.Cells[5].Text.Trim());
                    objUpgrage.Old_Discount = Convert.ToDouble(row.Cells[6].Text.Trim());
                    objUpgrage.Old_FinalTotal = Convert.ToDouble(row.Cells[7].Text.Trim());
                    objUpgrage.Old_PaidFee = Convert.ToDouble(txtPaid.Text.Trim());
                    objUpgrage.Old_Balance = Convert.ToDouble(txtBalance.Text.Trim());

                    string OldStDate = Convert.ToString(row.Cells[8].Text.Trim());
                    string OldEnDate = Convert.ToString(row.Cells[9].Text.Trim());

                    DateTime OldStartDate;
                    if (DateTime.TryParseExact(OldStDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out OldStartDate))
                    {
                        objUpgrage.Old_StartDate = OldStartDate;
                    }

                    DateTime OldEndDate;
                    if (DateTime.TryParseExact(OldEnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out OldEndDate))
                    {
                        objUpgrage.Old_EndDate = OldEndDate;
                    }

                }

                foreach (GridViewRow row in GvPakageAssign.Rows)
                {

                    objUpgrage.New_Pack_AutoID = Convert.ToInt32(row.Cells[1].Text.Trim());

                    TextBox txtStartDate = (TextBox)row.FindControl("txtsDate");
                    TextBox txtEndate = (TextBox)row.FindControl("txtEndate");
                    TextBox txtAmt = (TextBox)row.FindControl("txtAmt");
                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");

                    objUpgrage.New_Amount = Convert.ToInt32(txtAmt.Text.Trim());
                    objUpgrage.New_Discount = Convert.ToDouble(txtDisc.Text.Trim());
                    objUpgrage.New_Total = Convert.ToDouble(txtTotal.Text.Trim());
                    objUpgrage.New_FinalTotal = Convert.ToDouble(txtFinalTotal.Text.Trim());

                    objUpgrage.New_Balance = Convert.ToDouble(lblBalance.Text.Trim());

                    string NewStDate = Convert.ToString(txtStartDate.Text.Trim());
                    string NewEnDate = Convert.ToString(txtEndate.Text.Trim());

                    DateTime OldSDate;
                    if (DateTime.TryParseExact(NewStDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out OldSDate))
                    {
                        objUpgrage.New_StartDate = OldSDate;
                    }

                    DateTime OldEDate;
                    if (DateTime.TryParseExact(NewEnDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out OldEDate))
                    {
                        objUpgrage.New_EndDate = OldEDate;
                    }

                }

                DateTime UpgradeDate;
                if (DateTime.TryParseExact(txtUpgradeDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out UpgradeDate))
                {
                    objUpgrage.Upgrade_Date = UpgradeDate;
                }

                objUpgrage.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region ----------- Cancle Button Event -----------
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllForm();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

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
        }
        #endregion

        #region ------------ Search Button by Date -------------
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
                    objUpgrage.Action = "GetUpgradeDetails";
                    objUpgrage.Category = "Get_By_Date";

                    bindUpgradeDetailsGridView();
                    
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Bind Upgrade Details Gridview --------------
        private void bindUpgradeDetailsGridView()
        {
            try
            {
                dataTable = objUpgrage.SelectDetails();
                ViewState["UpgradeDetails"] = dataTable;
                lblCount.Text = Convert.ToString(dataTable.Rows.Count);

                if (dataTable.Rows.Count > 0)
                {
                    gvMemberUpgradeDetails.DataSource = dataTable;
                    gvMemberUpgradeDetails.DataBind();
                }
                else
                {
                    gvMemberUpgradeDetails.DataSource = dataTable;
                    gvMemberUpgradeDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {

                    gvMemberUpgradeDetails.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {

                    gvMemberUpgradeDetails.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {

                    gvMemberUpgradeDetails.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {

                    gvMemberUpgradeDetails.Columns[0].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {

                    gvMemberUpgradeDetails.Columns[0].Visible = false;
                }
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter From Date !!!','Error');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtToDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter To Date !!!','Error');", true);
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
                    objUpgrage.MStartDate = FromDate;
                    objUpgrage.MEndDate = ToDate;
                }
                else
                {
                    flag = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('From Date Should Not Be Greater Than To Date !!!','Error');", true);
                }
            }

            return flag;

        }
        #endregion

        #region ------------- Search Action For BindGridView -----------------
        private void SeacrhAction()
        {
            try
            {
                objUpgrage.Action = "GetUpgradeDetails";

                if (ddlcategory.SelectedValue.ToString() == "Member_ID")
                {
                    objUpgrage.Category = "Date_Member_ID";
                    objUpgrage.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "Member_Name")
                {
                    objUpgrage.Category = "Date_Member_Name";
                    objUpgrage.SearchByText = txtSearch.Text;
                }                
                else if (ddlcategory.SelectedValue.ToString() == "Member_Contact")
                {
                    objUpgrage.Category = "Date_Member_Contact";
                    objUpgrage.SearchByText = txtSearch.Text;
                }                
                else if (ddlcategory.SelectedValue.ToString() == "Executive")
                {
                    objUpgrage.Category = "Date_Executive";
                    objUpgrage.SearchByText = txtSearch.Text;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion     

        #region ------------- DDL Category Index Changed ---------
        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
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
                            bindUpgradeDetailsGridView();

                        }
                        else
                        {
                            txtSearch.Focus();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Enter Data On Search Field !!!','Error');", true);
                        }

                    }
                    else
                    {
                        ddlcategory.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Please Select Categry !!!','Error');", true);
                    }
                    
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
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

                    objUpgrage.Action = "GetUpgradeDetails";

                    if (ddlcategory.SelectedValue.ToString() == "Member_ID")
                    {
                        objUpgrage.Category = "Member_ID";
                        objUpgrage.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "Member_Name")
                    {
                        objUpgrage.Category = "Member_Name";
                        objUpgrage.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "Member_Contact")
                    {
                        objUpgrage.Category = "Member_Contact";
                        objUpgrage.SearchByText = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue.ToString() == "Executive")
                    {
                        objUpgrage.Category = "Executive";
                        objUpgrage.SearchByText = txtSearch.Text;
                    }

                    bindUpgradeDetailsGridView();
                    btnSearchCategory.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Select Category !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Edit Updrade Details --------------
        protected void btnEditUpgrade_Command(object sender, CommandEventArgs e)
        {
            try
            {               
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;                

                Upgrade_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                objUpgrage.Upgrade_AutoID = Upgrade_AutoID;

                AssignID();
                objUpgrage.Action = "EDIT";

                dataSet = objUpgrage.SelectDetails1();
                dataSet.Tables[0].TableName = "Member_UpgradeInfo";
                dataSet.Tables[1].TableName = "OldCourseGrid";
                dataSet.Tables[2].TableName = "UpgradeCourseGrid";   

                if (dataSet.Tables["Member_UpgradeInfo"].Rows.Count > 0)
                {
                    divUpgradeDetails.Visible = false;
                    divMemberUpgrade.Visible = true;
                    divsearch.Visible = false;
                    divFormDetails.Visible = true;

                    AssignUpgradeDate();
                    //bindDDLExecutive();
                    //setExecutive();
                    ddlExecutive.SelectedValue = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Executive"].ToString();
                    //BindPackageGridview();

                    Member_Id1 = Convert.ToInt32(dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Member_ID1"].ToString());

                    if (dataSet.Tables["Member_UpgradeInfo"].Rows[0]["BlockStatus"].ToString() != "Block")
                    {
                        ViewState["UpgradeDetails"] = dataSet;
                        btnUpgrade.Text = "Update";

                        txtMemberID1.Enabled = false;
                        txtContact.Enabled = false;

                        txtMemberID1.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Member_ID1"].ToString();
                        txtFirstName.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["FName"].ToString();
                        txtLastName.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Gender"].ToString();
                        txtContact.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Contact1"].ToString();
                        txtEmail.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["email"].ToString();

                        objUpgrage.Member_ID1 = Member_Id1;
                        objUpgrage.Action = "CourseBy_MemberID";

                        dataTable = objUpgrage.SelectDetails();
                        if (dataTable.Rows.Count > 0)
                        {
                            ddlReceipt.DataSource = dataTable;
                            ddlReceipt.Items.Clear();
                            ddlReceipt.DataValueField = "ReceiptID";
                            ddlReceipt.DataTextField = "ReceiptID";
                            ddlReceipt.DataBind();
                            ddlReceipt.Items.Insert(0, new ListItem("--Select--", "--Select--"));

                            ddlReceipt.SelectedValue = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["ReceiptID"].ToString();
                            ddlReceipt.Enabled = false;
                            txtTotal.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Old_FinalTotal"].ToString();
                            txtPaid.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Old_PaidFee"].ToString();
                            txtBalance.Text = dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Old_Balance"].ToString();

                            if (dataSet.Tables["OldCourseGrid"].Rows.Count > 0)
                            {
                                OldCourseAmotunt = Convert.ToInt32(dataSet.Tables["OldCourseGrid"].Rows[0]["Amount"]);
                                Quantity = Convert.ToInt32(dataSet.Tables["OldCourseGrid"].Rows[0]["Qty"]);

                                DateTime date = Convert.ToDateTime(dataSet.Tables["OldCourseGrid"].Rows[0]["StartDate"]);
                                OldCourStartDate = date.ToString("dd-MM-yyyy");

                                gvOCourse.Visible = true;
                                gvOldCourse.DataSource = dataSet.Tables["OldCourseGrid"];
                                gvOldCourse.DataBind();

                            }

                            if (dataSet.Tables["UpgradeCourseGrid"].Rows.Count > 0)
                            {
                                divPakageAssign.Visible = true;
                                dataTable1 = dataSet.Tables["UpgradeCourseGrid"];
                                ViewState["dataTable1"] = dataTable1;
                                GvPakageAssign.DataSource = dataTable1;
                                GvPakageAssign.DataBind();

                                int Total_Fess = Convert.ToInt32(dataTable1.Rows[0]["FinalTotal"].ToString());
                                int paid_Fees = Convert.ToInt32(dataSet.Tables["Member_UpgradeInfo"].Rows[0]["Old_PaidFee"].ToString());
                                int Balance_Fess = Total_Fess - paid_Fees;

                                lblTotalFeeDue.Text = Convert.ToString(Total_Fess);
                                lblPaidFee.Text = Convert.ToString(paid_Fees);
                                lblBalance.Text = Convert.ToString(Balance_Fess);
                            }
                           
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Course Not Assign !!!','Information');", true);
                        }                                                                                              

                    }
                    else
                    {
                        string url = "Termination.aspx?TransferMember_ID=" + HttpUtility.UrlEncode(Member_Id1.ToString());
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "text", "showConfirmation('" + url + "')", true);
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

        #region ---------- Upgrade Member Course Details Page Indexing --------------
        protected void gvMemberUpgradeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMemberUpgradeDetails.PageIndex = e.NewPageIndex;

                if (ViewState["UpgradeDetails"] != null)
                {
                    DataTable dataTable1 = (DataTable)ViewState["UpgradeDetails"];

                    gvMemberUpgradeDetails.DataSource = dataTable1;
                    gvMemberUpgradeDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ----------------- Export To Excle Record ----------------
        protected void btnExpord_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        protected void ExportToExcel()
        {
            try
            {
                
                if (ViewState["UpgradeDetails"] != null)
                {
                    dataTable1 = (DataTable)ViewState["UpgradeDetails"]; ;
                    if (dataTable1.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=UpgradeDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvMemberUpgradeDetails.Columns[0].Visible = false;
                            gvMemberUpgradeDetails.Columns[1].Visible = false;
                            gvMemberUpgradeDetails.AllowPaging = false;

                            gvMemberUpgradeDetails.DataSource = dataTable1;
                            gvMemberUpgradeDetails.DataBind();
                            gvMemberUpgradeDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMemberUpgradeDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMemberUpgradeDetails.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvMemberUpgradeDetails.Rows)
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


                            gvMemberUpgradeDetails.GridLines = GridLines.Both;
                            gvMemberUpgradeDetails.RenderControl(hw);

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

        #region ---------------- Refresh Button --------------
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ddlcategory.SelectedIndex = 0;
                txtSearch.Text = "";
                //txtSearch.Enabled = false;
                Assign_MonthDate();
                SearchByDateFunction();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- New Course Details Page Indexing --------------                
        protected void gvCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCourse.PageIndex = e.NewPageIndex;

                BindGridview_1();
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
                    pack.searchTxt = txtPackageSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "Duration")
                {
                    pack.Category = "Duration_Active";
                    pack.searchTxt = txtPackageSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Active")
                {
                    pack.Category = "Active";
                    pack.searchTxt = txtPackageSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Deactive")
                {
                    pack.Category = "Deactive";
                    pack.searchTxt = txtPackageSearch.Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    return;
                }
                pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable.Clear();
                dataTable = pack.Get_Search();
                if (dataTable.Rows.Count > 0)
                {
                    gvCourse.DataSource = dataTable;
                    gvCourse.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        #endregion

        protected void txtPackageSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindGridview_1();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

    }
}