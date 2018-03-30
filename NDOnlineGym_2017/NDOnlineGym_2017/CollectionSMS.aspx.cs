using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class CollectionSMS : System.Web.UI.Page
    {
        BalCollectionSMS ObjCollectionSms=new BalCollectionSMS();
        DataTable dataTable = new DataTable();
        int res;
        string Message = ""; 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtCollectionSMS.Text = string.Empty;
                    txtCollectionSMS.Focus();
                    BindDetails();
                }
                
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #region ------------ Bind Details -------------------
        private void BindDetails()
        {
            AssignID();
            ObjCollectionSms.Action = "Get_BranchMobileNo";

            dataTable = ObjCollectionSms.GetDetails();

            if (dataTable.Rows.Count > 0)
            {
                txtCollectionSMS.Text = dataTable.Rows[0]["CollectionSMS"].ToString();
            }

        }
        #endregion

        #region ----------- Assign Branch Id And Company I -----------
        private void AssignID()
        {
            ObjCollectionSms.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); 
            ObjCollectionSms.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region -----------Send Daily Collection Button -------------
                      
        protected void btnSendDailyCollection_Click(object sender, EventArgs e)
        {
            try
            {
                TodaysCollection();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void TodaysCollection()
        {
            string Mobile = txtCollectionSMS.Text;

            if (Mobile != string.Empty)
            {

                AssignID();

                //string NowDateTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                //DateTime date = DateTime.Now;                
                //DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                //DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                string NowDateTime = localTime.ToString("dd-MM-yyyy hh:mm:ss tt");

                DateTime firstDayOfMonth = new DateTime(localTime.Year, localTime.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                ObjCollectionSms.TodayDate = localTime;
                ObjCollectionSms.StartDate = firstDayOfMonth;
                ObjCollectionSms.EndDate = lastDayOfMonth;

                ObjCollectionSms.Action = "Today_Collection";

                dataTable = ObjCollectionSms.GetCollection();

                if (dataTable.Rows.Count > 0)
                {
                    string CompanyName = dataTable.Rows[0]["CompanyName"].ToString();
                    string BranchName = dataTable.Rows[0]["BranchName"].ToString();
                    string ActiveMember = dataTable.Rows[0]["Active"].ToString();
                    string New_Add = dataTable.Rows[0]["NewAddMember"].ToString();
                    string Enquiry = dataTable.Rows[0]["Enquiry"].ToString();
                    string Month_Collection = dataTable.Rows[0]["Month_Collection"].ToString();
                    string Today_Collection = dataTable.Rows[0]["Today_Collection"].ToString();
                    string CH_Collection = dataTable.Rows[0]["CH_Collection"].ToString();
                    string CQ_Collection = dataTable.Rows[0]["CQ_Collection"].ToString();
                    string CD_Collection = dataTable.Rows[0]["CD_Collection"].ToString();
                    string OD_Collection = dataTable.Rows[0]["OD_Collection"].ToString();

                    Message = CompanyName + ", " + BranchName + "\n" + NowDateTime + "\nActive Member : " + ActiveMember + "\nTodays New Add Member : " + New_Add + "\nTodays Enquiry : " + Enquiry + "\nTM Collection : " + Month_Collection + "\nTodays Total Collection : " + Today_Collection + "\nTodays Cash: " + CH_Collection + "\nTodays Cheque  : " + CQ_Collection + "\nTodays Card : " + CD_Collection + "\nTodays Other : " + OD_Collection;

                }

                res = SendSMSFun(Mobile, Message);


                DisplayMessage();
            }
            else
            {
                txtCollectionSMS.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Mobile Number !!!','Error');", true);
            }

        }

        #endregion                

        #region ------------- Send Monthly Collection Button ----------------

        protected void btnSendMonthlyCollection_Click(object sender, EventArgs e)
        {
            try
            {
                string Mobile = txtCollectionSMS.Text;

                if (Mobile != string.Empty)
                {

                    AssignID();

                    string NowDateTime = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                    DateTime date=DateTime.Now;
                    DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    
                    ObjCollectionSms.StartDate = firstDayOfMonth;
                    ObjCollectionSms.EndDate = lastDayOfMonth;
                    ObjCollectionSms.Action = "Month_Collection";

                    dataTable = ObjCollectionSms.GetCollection();

                    if (dataTable.Rows.Count > 0)
                    {
                        string CompanyName = dataTable.Rows[0]["CompanyName"].ToString();
                        string BranchName = dataTable.Rows[0]["BranchName"].ToString();
                        string ActiveMember = dataTable.Rows[0]["Active"].ToString();
                        string New_Add = dataTable.Rows[0]["NewAddMember"].ToString();
                        string Enquiry = dataTable.Rows[0]["Enquiry"].ToString();
                        string Month_Collection = dataTable.Rows[0]["Month_Collection"].ToString();
                        //string Today_Collection = dataTable.Rows[0]["Today_Collection"].ToString();
                        string CH_Collection = dataTable.Rows[0]["CH_Collection"].ToString();
                        string CQ_Collection = dataTable.Rows[0]["CQ_Collection"].ToString();
                        string CD_Collection = dataTable.Rows[0]["CD_Collection"].ToString();
                        string OD_Collection = dataTable.Rows[0]["OD_Collection"].ToString();

                        Message = CompanyName + ", " + BranchName + "\n" + NowDateTime + "\nTM Active Member : " + ActiveMember + "\nTM New Add Member : " + New_Add + "\nTM Enquiry : " + Enquiry + "\n TM Collection : " + Month_Collection + "\nTM Cash : " + CH_Collection + "\nTM Cheque  : " + CQ_Collection + "\nTM Card : " + CD_Collection + "\nTM Other : " + OD_Collection;

                    }

                    res = SendSMSFun(Mobile, Message);


                    DisplayMessage();
                }
                else
                {
                    txtCollectionSMS.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Mobile Number !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region------------SMS Function--------------------

        string suname = "", spass = "", senderid = "", routeid = "", status = "", totalSms = "";
        public int SendSMSFun(string Mobile, string Message)
        {
            int Val;
            try
            {
                AssignID();
                ObjCollectionSms.Action = "SELECT_SMSLogin_INFO";
                DataSet ds = new DataSet();

                ds = ObjCollectionSms.GetSMSLoginDetails();

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

        #region ------------ Display Success And Error Message -----------
        private void DisplayMessage()
        {
            if (res == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Sending failed due to Internet Connection !!!','Error');", true);
                return;
            }
            else if (res == 2)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message not send! SMS Status is off !!!','Error');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Message Sent Successfully !!!','Success');", true);
            }
        }
        #endregion

       

    }
}