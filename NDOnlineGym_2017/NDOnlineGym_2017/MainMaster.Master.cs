using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.Reflection;
using System.Net;
using System.IO;
using System.Text;
using DataAccessLayer;
using System.Globalization;

namespace NDOnlineGym_2017
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();
        BalCollectionSMS ObjCollectionSms = new BalCollectionSMS();
        BalGroupSMS ObjGropsms = new BalGroupSMS();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        static int flag;
        int res;
        string Message = "";
        public int MID, EnqID;
        public string CourseName, PlanName, InstructorName;
        public DateTime CourseStartDate, CourseEndtDate, PayDate, NextPayDate, TodayDate,EnqDate;
        public string Executive, PaidAmt, PayMode, ChequeNo, RemFees, TotalFees, TotalPaidFees, TotalBalanceFees, RecpID;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    if (Request.Cookies["OnlineGym"] == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "MasterAdmin")
                        {
                            if (Request.QueryString["Flag"] == "1")
                            {
                                Session["Branch_ID"] = null;
                                liMemberSetting.Visible = false;
                                liUserSetting.Visible = false;
                                liReport.Visible = false;
                                //liAttendance.Visible = false;
                                liChart.Visible = false;
                                liDashboard.Visible = false;
                                liSendAllDataOnEmail.Visible = false;
                                liUser.Visible = false;
                                liStaffRegistration.Visible = false;
                                liUserRegistration.Visible = false;
                                liAssignDiet.Visible = false;
                                liChangePassword.Visible = false;
                                liPOSHead.Visible = false;
                                liPOS.Visible = false;
                                liMasterHead.Visible = false;
                                liMaster.Visible = false;
                                liStaffNotificationHome.Visible = false;
                                ulSMS.Visible = false;
                                txtSearch.Visible = false;
                                btnSearch.Visible = false;
                                ddlSearch.Visible = false;


                                liemailHeading.Visible = false;
                                liEmailLogin.Visible = false;
                                liemailTemp.Visible = false;
                                liemailstatus.Visible = false;
                                liMaster.Visible = false;
                                liEnquiryHead.Visible = false;
                                liMemberHead.Visible = false;
                                liCourseHead.Visible = false;
                                liPackageHead.Visible = false;
                                liExpenseHead.Visible = false;
                                liAttendance.Visible = false;
                                liQuickSMS.Visible = false;
                                liStaffRegistration.Visible = false;
                                liStaffDetails.Visible = false;
                                liUserRegistration.Visible = false;
                                liUserDetails.Visible = false;
                            }
                            dt = obBalBranchInformation.SELECT_CompanyDetails();
                            if (dt.Rows.Count > 0)
                            {
                                lblCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                                imgCompanyLogo.ImageUrl = dt.Rows[0]["CompanyLogoPath"].ToString();
                                lblName.Text = Request.Cookies["OnlineGym"]["Name"].ToString();
                            }
                        }

                        if (Request.QueryString["Flag"] != "1")
                        {
                            if (Request.QueryString["Branch_AutoID"] != null)
                                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.QueryString["Branch_AutoID"]);
                            else if (Request.Cookies["OnlineGym1"]["brIDHome"] != null)
                                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);

                            dt = obBalBranchInformation.SelectByID_BranchInformation();
                            if (dt.Rows.Count > 0)
                            {
                                lblCompanyName.Text = dt.Rows[0]["BranchName"].ToString();
                                imgCompanyLogo.ImageUrl = dt.Rows[0]["BranchLogoPath"].ToString();
                                lblName.Text = Request.Cookies["OnlineGym"]["Name"].ToString();
                            }
                            else
                            {
                                //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                            }
                        }
                        if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "MasterAdmin")
                        {
                            bind_PageMasterAdmin();
                        }
                        else if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "SuperAdmin")
                        {
                            bind_PageSuperAdmin();
                        }
                        else if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "Admin")
                        {
                            bind_PageAdmin();
                        }
                        else if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "Manager")
                        {
                            bind_PageManager();
                        }
                        else if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                        {
                            bind_PageUser();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                return;
            }
        }

        protected void bind_PageMasterAdmin()
        {
            
            liNewUpdateNotificationHome.Visible = true;
            liCompanyInformation.Visible = true;
            liBranchInformation.Visible = true;
            liSMSLogin.Visible = true;
            liCreatePackage.Visible = true;
            if (Request.QueryString["Flag"] == "1")
            {
                liStaffRegistration.Visible = false;
                liStaffNotificationHome.Visible = false;
            }
            else
            {
                liStaffNotificationHome.Visible = true;
                liStaffRegistration.Visible = true;
            }
            //liEmailLogin.Visible = true;
            //liMaster.Visible = true;
            //litermination.Visible = true;
        }
        protected void bind_PageSuperAdmin()
        {
            liStaffNotificationHome.Visible = true;
            liNewUpdateNotificationHome.Visible = false;
            liCompanyInformation.Visible = false;
            liBranchInformation.Visible = true;
            liSMSLogin.Visible = true;
            liCreatePackage.Visible = true;
            liStaffRegistration.Visible = true;
            liEmailLogin.Visible = true;
            liMaster.Visible = true;
            litermination.Visible = true;
        }
        protected void bind_PageAdmin()
        {
            liStaffNotificationHome.Visible = true;
            liNewUpdateNotificationHome.Visible = false;
            liCompanyInformation.Visible = false;
            liBranchInformation.Visible = true;
            liSMSLogin.Visible = true;
            liCreatePackage.Visible = true;
            liStaffRegistration.Visible = true;
            liEmailLogin.Visible = true;
            liMaster.Visible = true;
            litermination.Visible = true;
        }
        protected void bind_PageManager()
        {
            liStaffNotificationHome.Visible = true;
            liNewUpdateNotificationHome.Visible = false;
            liCompanyInformation.Visible = false;
            liBranchInformation.Visible = true;
            liSMSLogin.Visible = false;
            liCreatePackage.Visible = true;
            liStaffRegistration.Visible = true;
            liEmailLogin.Visible = false;
            liMaster.Visible = true;
            litermination.Visible = true;
        }
        protected void bind_PageUser()
        {
            licompsetthead.Visible = false;
            liStaffNotificationHome.Visible = true;
            liNewUpdateNotificationHome.Visible = false;
            liCompanyInformation.Visible = false;
            liBranchInformation.Visible = false;
            liSMSLogin.Visible = false;
            liCreatePackage.Visible = false;
            //liStaffRegistration.Visible = false;
            liEmailLogin.Visible = false; 
            liMaster.Visible = false;
            litermination.Visible = false;

            liReportBalancePayment.Visible = false;
            //liReportPaymentDetails.Visible = false;
            liReportAllFollowup.Visible = false;
            liReportActiveDeactive.Visible = false;
            liReportAllMemberList.Visible = false;
            liReportMemberBirthday.Visible = false;
            liReportAllCollection.Visible = false;
            //liReportCourseWiseCollection.Visible = false;
            liReportEnquiryToEnroll.Visible = false;
            liExecutivewiseEnquiryFollowup.Visible = false;
            liPackageInformation.Visible = false;
            liMasterHead.Visible = false;
            liChart.Visible = false;
        }

        protected void lnkBtnLogout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["OnlineGym"]["Authority"] == "Admin" || Request.Cookies["OnlineGym"]["Authority"] == "Manager" || Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                LogOutCollection();
            }
            Session["Branch_ID"] = null;
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        #region ----------- Assign Branch Id And Company I -----------
        private void AssignID()
        {
            ObjCollectionSms.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            ObjCollectionSms.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region -------- Log Out Collection SMS --------------
        protected void LogOutCollection()
        {
            AssignID();
            string Message = "";
            string Mobile = "";

            //string NowDateTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
            //DateTime date = DateTime.Now;

            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

            string NowDateTime = localTime.ToString("dd-MM-yyyy hh:mm:ss tt");

            DateTime firstDayOfMonth = new DateTime(localTime.Year, localTime.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            ObjCollectionSms.TodayDate = localTime;
            ObjCollectionSms.StartDate = firstDayOfMonth;
            ObjCollectionSms.EndDate = lastDayOfMonth;

            ObjCollectionSms.Action = "Collection_SMS_AT_Logout";

            dt = ObjCollectionSms.GetCollection();

            if (dt.Rows.Count > 0)
            {
                Mobile = dt.Rows[0]["Mobile_No"].ToString();
                string CompanyName = dt.Rows[0]["CompanyName"].ToString();
                string BranchName = dt.Rows[0]["BranchName"].ToString();
                string ActiveMember = dt.Rows[0]["Active"].ToString();
                string New_Add = dt.Rows[0]["NewAddMember"].ToString();
                string Enquiry = dt.Rows[0]["Enquiry"].ToString();
                string Month_Collection = dt.Rows[0]["Month_Collection"].ToString();
                string Today_Collection = dt.Rows[0]["Today_Collection"].ToString();
                string CH_Collection = dt.Rows[0]["CH_Collection"].ToString();
                string CQ_Collection = dt.Rows[0]["CQ_Collection"].ToString();
                string CD_Collection = dt.Rows[0]["CD_Collection"].ToString();
                string OD_Collection = dt.Rows[0]["OD_Collection"].ToString();
                string Amt_Expanse = dt.Rows[0]["Amt_Expanse"].ToString();

                Message = CompanyName + ", " + BranchName + "\n" + NowDateTime + "\nActive Member : " + ActiveMember + "\nTodays New Add Member : " + New_Add + "\nTodays Enquiry : " + Enquiry + "\nMonthly Total Collection : " + Month_Collection + "\nTodays Total Collection : " + Today_Collection + "\nTodays Cash: " + CH_Collection + "\nTodays Cheque  : " + CQ_Collection + "\nTodays Card : " + CD_Collection + "\nTodays Other : " + OD_Collection + " \nTodays Expanse" + Amt_Expanse;

            }

            int res = SendSMSFun(Mobile, Message);

        }
        #endregion

        #region------------SMS Function--------------------

        string suname = "", spass = "", senderid = "", routeid = "", status = "", totalSms = "";
        public int SendSMSFun(string Mobile, string Message)
        {
            int Val;
            try
            {             
                //if (ds.Tables["SMSInfo"].Rows.Count > 0)
                //{

                //}
                if(ds.Tables.Count == 0)
                {
                    AssignID();
                    ObjCollectionSms.Action = "SELECT_SMSLogin_INFO";
                    ds = ObjCollectionSms.GetSMSLoginDetails();
                    ds.Tables[0].TableName = "SMSInfo";
                }

                if (ds.Tables["SMSInfo"].Rows.Count > 0)
                {
                    suname = ds.Tables["SMSInfo"].Rows[0]["Username"].ToString();
                    spass = ds.Tables["SMSInfo"].Rows[0]["Password"].ToString();
                    senderid = ds.Tables["SMSInfo"].Rows[0]["Sender_ID"].ToString();
                    routeid = ds.Tables["SMSInfo"].Rows[0]["Route"].ToString();
                    status = ds.Tables["SMSInfo"].Rows[0]["Status"].ToString();
                }

                if (status == "ON")
                {
                    Val = 0;
                    string strUrl = "http://173.45.76.226:81/send.aspx?username=" + suname + "&pass=" + spass + "&route=" + routeid + "&senderid=" + senderid + "&numbers=" + Mobile + "&message=" + Server.UrlEncode(Message) + "";

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

        protected void lnkbtnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void RemoveQueryStringParams(string rname)
        {
            // reflect to readonly property
            PropertyInfo isReadOnly =
            typeof(System.Collections.Specialized.NameValueCollection)
            .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable
            isReadOnly.SetValue(this.Request.QueryString, false, null);
            // remove
            this.Request.QueryString.Remove(rname);
            // make collection readonly again
            isReadOnly.SetValue(this.Request.QueryString, true, null);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Search"] != null)
            {
                RemoveQueryStringParams("Search");
            }
            if (Request.QueryString["SearchBy"] != null)
            {
                RemoveQueryStringParams("SearchBy");
            }
            string SearchBy = ddlSearch.SelectedValue;
            Response.Redirect("SearchPage.aspx?Search="+txtSearch.Text.Trim()+"&SearchBy="+HttpUtility.UrlEncode(SearchBy.ToString()));
        }

        protected void btnSearchByCategory_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnMenuEnqDetails_Click(object sender, EventArgs e)
        {
            string menuEnquDet = "MenuEnqDetails";
            Response.Redirect("AddEnquiry.aspx?MenuEnquDetails=" + menuEnquDet);
        }

        protected void liBranchInformation_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Flag"] == "1")
                Response.Redirect("BranchInformation.aspx?Flag=" + 1);
            else
                Response.Redirect("BranchInformation.aspx");
        }

        protected void liCompanyInformation_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Flag"] == "1")
                Response.Redirect("CompanyInformation.aspx?Flag=" + 1);
            else
                Response.Redirect("CompanyInformation.aspx");
        }

        protected void liNewUpdateNotificationHome_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Flag"] == "1")
                Response.Redirect("NewUpdateNotificationHome.aspx?Flag=" + 1);
            else
                Response.Redirect("NewUpdateNotificationHome.aspx");
        }

        protected void lnkbtnMenuMemberDetails_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnMenuCourseDetails_Click(object sender, EventArgs e)
        {
            string MenuCourseDetails = "MenuCourseDetails";
            Response.Redirect("demoCourse.aspx?MenuCourseDetails=" + MenuCourseDetails);
        }

        protected void lnkbtnMenuExtensioneDetails_Click(object sender, EventArgs e)
        {
            string MenuExtensionDetails = "MenuExtensionDetails";
            Response.Redirect("MembershipExtension.aspx?MenuExtensionDetails=" + MenuExtensionDetails);
        }

        protected void lnkbtnMenuFreezingDetails_Click(object sender, EventArgs e)
        {
            string MenuFreezingDetails = "MenuFreezingDetails";
            Response.Redirect("MembershipFreezing.aspx?MenuFreezingDetails=" + MenuFreezingDetails);
        }

        protected void lnkbtnMenuBalancePaymentDetails_Click(object sender, EventArgs e)
        {
            string menuBalDet = "MenuBalanceDetails";
            Response.Redirect("BalancePayment.aspx?MenuBalanceDetails=" + menuBalDet);
        }

        protected void lnkbtnMenuUpgradeDetails_Click(object sender, EventArgs e)
        {
            string menuBalDet = "UpgradeDetails";
            Response.Redirect("Upgrade.aspx?UpgradeDetails=" + menuBalDet);
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnMenuAppointmentDetails_Click(object sender, EventArgs e)
        {

        }

        //protected void lnkbtnMenuPackageDetails_Click(object sender, EventArgs e)
        //{
        //    string MenuPackageDetails = "MenuPackageDetails";
        //    Response.Redirect("Packages.aspx?MenuPackageDetails=" + MenuPackageDetails);
        //}

        protected void lnkbtnMenuExpenseDetails_Click(object sender, EventArgs e)
        {
            string MenuExpenseDetails = "MenuExpenseDetails";
            Response.Redirect("Expense.aspx?MenuExpenseDetails=" + MenuExpenseDetails);
        }

        #region ---------------- Send All Todays SMS ----------------------
        protected void lnkBtntodaysSMS_Click(object sender, EventArgs e)
        {
            try
            {
                ObjGropsms.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                ObjGropsms.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                string Todate = localTime.ToString("dd-MM-yyyy");
                if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                {
                }

                ObjGropsms.TodayDate = TodayDate;

                ObjGropsms.Action = "Get_Record";

                 ds = ObjGropsms.GetAllDetails();

                 ds.Tables[0].TableName = "SMSInfo";
                 ds.Tables[1].TableName = "Template";
                 ds.Tables[2].TableName = "Enquiry";
                 ds.Tables[3].TableName = "Registration";
                 ds.Tables[4].TableName = "Birthday";
                 ds.Tables[5].TableName = "TodayAniversary";
                 ds.Tables[6].TableName = "EnquiryFollowup";
                 ds.Tables[7].TableName = "TodayBalanceRem";
                 ds.Tables[8].TableName = "TodayBalancePaid";
                 ds.Tables[9].TableName = "TodayEnddate";
                 ds.Tables[10].TableName = "TodayEnddate_1";
                 ds.Tables[11].TableName = "TodayEnddate_2";
                 ds.Tables[12].TableName = "TodayEnddate_3";
                 ds.Tables[13].TableName = "TodayEnddate_4";
                 ds.Tables[14].TableName = "TodayEnddate_5";                

                 if (ds.Tables["SMSInfo"].Rows.Count != 0)
                {
                    if (ds.Tables["SMSInfo"].Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        flag = 1;
                        SendAllSMS();
                    }
                    else
                    {
                        flag = 0;
                        SendAllSMS();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('SMS Login Details Not Found !!!','Information');", true);
                }


            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }


        }

        private void SendAllSMS()
        {
            if (ds.Tables["Template"].Rows.Count > 0)
            {                
                EnquirySMS();
                RegistrationSMS();
                TodaysBirthdaySMS();
                TodaysAnniversarySMS();
                TodaysEnquiryFllowupSMS();
                TodayBalanceRemainderSMS();
                TodayBalancePaidSMS();
                TodayEndDateSMS();
                OneDayBefore_EndDateSMS();
                TwoDayBefore_EndDateSMS();
                ThreeDayBefore_EndDateSMS();
                FourDayBefore_EndDateSMS();
                FiveDayBefore_EndDateSMS();


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }        

        private void EnquirySMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Enquiry"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Enquiry"].ToString();

                    if (ds.Tables["Enquiry"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["Enquiry"].Rows.Count; i++)
                        {
                            string Name = ds.Tables["Enquiry"].Rows[i]["Name"].ToString();
                            string Contact = ds.Tables["Enquiry"].Rows[i]["Contact1"].ToString();
                            EnqID = Convert.ToInt32(ds.Tables["Enquiry"].Rows[i]["Enq_ID1"].ToString());
                            EnqDate = Convert.ToDateTime(ds.Tables["Enquiry"].Rows[i]["EnqDate"]);

                            if (flag == 1)
                            {
                                string St = "";
                                string Gender = ds.Tables["Enquiry"].Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                            }
                            else
                            {
                                Message = GetMSG(TemplateMessage);
                            }

                            res = SendSMSFun(Contact, Message);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void RegistrationSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Renew"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Renew"].ToString();

                    if (ds.Tables["Registration"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["Registration"].Rows.Count; i++)
                        {
                            if (ds.Tables["Registration"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["Registration"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["Registration"].Rows[i]["Contact1"].ToString();
                                MID = Convert.ToInt32(ds.Tables["Registration"].Rows[i]["Member_ID1"]);
                                TotalFees = ds.Tables["Registration"].Rows[i]["Total_Fees"].ToString();
                                PaidAmt = ds.Tables["Registration"].Rows[i]["PaidFee"].ToString();
                                RemFees = ds.Tables["Registration"].Rows[i]["RemainingFee"].ToString();
                                PayDate = Convert.ToDateTime(ds.Tables["Registration"].Rows[i]["payDate"].ToString());

                                if (ds.Tables["Registration"].Rows[i]["NextPay_Date"].ToString() != string.Empty)
                                {
                                    NextPayDate = Convert.ToDateTime(ds.Tables["Registration"].Rows[i]["NextPay_Date"]);
                                }                              

                                PayMode = ds.Tables["Registration"].Rows[i]["PaymentMode"].ToString();
                                RecpID = ds.Tables["Registration"].Rows[i]["ReceiptID"].ToString();
                                ChequeNo = ds.Tables["Registration"].Rows[i]["ChequeNo"].ToString();

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["Registration"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }


        }

        private void TodaysBirthdaySMS()
        {
            try
            {

                if (ds.Tables["Template"].Rows[0]["birthday"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["birthday"].ToString();

                    if (ds.Tables["Birthday"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["Birthday"].Rows.Count; i++)
                        {
                            if (ds.Tables["Birthday"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["Birthday"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["Birthday"].Rows[i]["Contact1"].ToString();

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["Birthday"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + TemplateMessage;
                                }
                                else
                                {
                                    Message = TemplateMessage;
                                }
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void TodaysAnniversarySMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Aniversary"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Aniversary"].ToString();

                    if (ds.Tables["TodayAniversary"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayAniversary"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayAniversary"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayAniversary"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayAniversary"].Rows[i]["Contact1"].ToString();

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayAniversary"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + TemplateMessage;
                                }
                                else
                                {
                                    Message = TemplateMessage;
                                }
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TodaysEnquiryFllowupSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["EnquiryFollowup"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Aniversary"].ToString();

                    if (ds.Tables["EnquiryFollowup"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["EnquiryFollowup"].Rows.Count; i++)
                        {

                            string Name = ds.Tables["EnquiryFollowup"].Rows[i]["Name"].ToString();
                            string Contact = ds.Tables["EnquiryFollowup"].Rows[i]["Contact1"].ToString();

                            if (flag == 1)
                            {
                                string St = "";
                                string Gender = ds.Tables["EnquiryFollowup"].Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + TemplateMessage;
                            }
                            else
                            {
                                Message = TemplateMessage;
                            }
                            res = SendSMSFun(Contact, Message);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TodayBalanceRemainderSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Todaybalance"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Todaybalance"].ToString();

                    if (ds.Tables["TodayBalanceRem"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayBalanceRem"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayBalanceRem"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayBalanceRem"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayBalanceRem"].Rows[i]["Contact1"].ToString();
                                MID = Convert.ToInt32(ds.Tables["TodayBalanceRem"].Rows[i]["Member_ID1"]);
                                RemFees = ds.Tables["TodayBalanceRem"].Rows[i]["RemainingFee"].ToString();

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayBalanceRem"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage); //TemplateMessage;
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TodayBalancePaidSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["balancepaid"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["balancepaid"].ToString();

                    if (ds.Tables["TodayBalancePaid"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayBalancePaid"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayBalancePaid"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayBalancePaid"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayBalancePaid"].Rows[i]["Contact1"].ToString();
                                MID = Convert.ToInt32(ds.Tables["TodayBalancePaid"].Rows[i]["Member_ID1"]);
                                RecpID = ds.Tables["TodayBalancePaid"].Rows[i]["ReceiptID"].ToString();
                                PaidAmt = ds.Tables["TodayBalanceRem"].Rows[i]["PaidFees"].ToString();
                                RemFees = ds.Tables["TodayBalanceRem"].Rows[i]["RemainingFee"].ToString();

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayBalanceRem"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + TemplateMessage;
                                }
                                else
                                {
                                    Message = TemplateMessage;
                                }
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TodayEndDateSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Enddate"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Enddate"].ToString();

                    if (ds.Tables["TodayEnddate"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayEnddate"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayEnddate"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayEnddate"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayEnddate"].Rows[i]["Contact1"].ToString();
                                RecpID = ds.Tables["TodayEnddate"].Rows[i]["ReceiptID"].ToString();
                                CourseName = ds.Tables["TodayEnddate"].Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(ds.Tables["TodayEnddate"].Rows[i]["EndDate"].ToString());

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayEnddate"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
  
        }

        private void OneDayBefore_EndDateSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Enddate1"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Enddate1"].ToString();

                    if (ds.Tables["TodayEnddate_1"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayEnddate_1"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayEnddate_1"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayEnddate_1"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayEnddate_1"].Rows[i]["Contact1"].ToString();
                                RecpID = ds.Tables["TodayEnddate_1"].Rows[i]["ReceiptID"].ToString();
                                CourseName = ds.Tables["TodayEnddate_1"].Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(ds.Tables["TodayEnddate_1"].Rows[i]["EndDate"].ToString());

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayEnddate_1"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TwoDayBefore_EndDateSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Enddate2"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Enddate2"].ToString();

                    if (ds.Tables["TodayEnddate_2"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayEnddate_2"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayEnddate_2"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayEnddate_2"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayEnddate_2"].Rows[i]["Contact1"].ToString();
                                RecpID = ds.Tables["TodayEnddate_2"].Rows[i]["ReceiptID"].ToString();
                                CourseName = ds.Tables["TodayEnddate_2"].Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(ds.Tables["TodayEnddate_2"].Rows[i]["EndDate"].ToString());

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayEnddate_2"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ThreeDayBefore_EndDateSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Enddate3"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Enddate3"].ToString();

                    if (ds.Tables["TodayEnddate_3"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayEnddate_3"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayEnddate_3"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayEnddate_3"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayEnddate_3"].Rows[i]["Contact1"].ToString();
                                RecpID = ds.Tables["TodayEnddate_3"].Rows[i]["ReceiptID"].ToString();
                                CourseName = ds.Tables["TodayEnddate_3"].Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(ds.Tables["TodayEnddate_3"].Rows[i]["EndDate"].ToString());

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayEnddate_3"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void FourDayBefore_EndDateSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Enddate4"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Enddate4"].ToString();

                    if (ds.Tables["TodayEnddate_4"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayEnddate_4"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayEnddate_4"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayEnddate_4"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayEnddate_4"].Rows[i]["Contact1"].ToString();
                                RecpID = ds.Tables["TodayEnddate_4"].Rows[i]["ReceiptID"].ToString();
                                CourseName = ds.Tables["TodayEnddate_4"].Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(ds.Tables["TodayEnddate_4"].Rows[i]["EndDate"].ToString());

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayEnddate_4"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void FiveDayBefore_EndDateSMS()
        {
            try
            {
                if (ds.Tables["Template"].Rows[0]["Enddate5"].ToString() != string.Empty)
                {
                    string TemplateMessage = ds.Tables["Template"].Rows[0]["Enddate5"].ToString();

                    if (ds.Tables["TodayEnddate_5"].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables["TodayEnddate_5"].Rows.Count; i++)
                        {
                            if (ds.Tables["TodayEnddate_5"].Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = ds.Tables["TodayEnddate_5"].Rows[i]["Name"].ToString();
                                string Contact = ds.Tables["TodayEnddate_5"].Rows[i]["Contact1"].ToString();
                                RecpID = ds.Tables["TodayEnddate_5"].Rows[i]["ReceiptID"].ToString();
                                CourseName = ds.Tables["TodayEnddate_5"].Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(ds.Tables["TodayEnddate_5"].Rows[i]["EndDate"].ToString());

                                if (flag == 1)
                                {
                                    string St = "";
                                    string Gender = ds.Tables["TodayEnddate_5"].Rows[i]["Gender"].ToString();
                                    if (Gender == "Male")
                                    {
                                        St = "Sir";
                                    }
                                    if (Gender == "Female")
                                    {
                                        St = "Madam";
                                    }

                                    Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                }
                                else
                                {
                                    Message = GetMSG(TemplateMessage);
                                }
                                res = SendSMSFun(Contact, Message);

                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region ------------ Template Message --------------
        private string GetMSG(string TemplateMessage)
        {

            string message1 = string.Empty;
            StringBuilder sb = new StringBuilder(TemplateMessage);
            DateTime k = System.DateTime.Now;

            sb.Replace("#MID#", MID.ToString());
            sb.Replace("#REC#", RecpID);
            sb.Replace("#TFess#", TotalFees);
            sb.Replace("#PaidFees#", PaidAmt);
            sb.Replace("#RemBal#", RemFees);
            sb.Replace("#PayMode#", PayMode);
            sb.Replace("#PayDate#", PayDate.ToString("dd/MM/yyyy"));
            if (NextPayDate != null)
            {
                sb.Replace("#NextBalanceDate#", NextPayDate.ToString("dd/MM/yyyy"));
            }
            else
            {
                sb.Replace("#NextBalanceDate#", " ");
            }
            sb.Replace("#Package#", CourseName);
            sb.Replace("#CourseED#", CourseEndtDate.ToString("dd/MM/yyyy"));
            sb.Replace("#ENQID#", EnqID.ToString());
            sb.Replace("#ENQDate#", EnqDate.ToString("dd/MM/yyyy"));
            sb.Replace("#TODate#", k.ToString("dd/MM/yyyy"));
            sb.Replace("#ChequeNo#", ChequeNo);

            return message1 = sb.ToString();
        }
        #endregion

        protected void lnkbtnEnqFollowupDetails_Click(object sender, EventArgs e)
        {
            string MenuEnqFollowupDetails = "MenuEnqFollowupDetails";
            Response.Redirect("AddEnquiry.aspx?MenuEnqFollowupDetails=" + MenuEnqFollowupDetails);
        }

        protected void liStaffDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffRegistration.aspx?MenuStaffDetails=" + "MenuStaffDetails");
        }

        protected void liUserDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserRegistration.aspx?MenuUserDetails=" + "MenuUserDetails");
        }

    }
}