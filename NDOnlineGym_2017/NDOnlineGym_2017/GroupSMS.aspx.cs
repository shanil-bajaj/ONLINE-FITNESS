using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class GroupSMS : System.Web.UI.Page
    {
        BalGroupSMS ObjGropsms = new BalGroupSMS();
        DataTable dataTable = new DataTable();
        int res;
        string Message = "";
        DateTime TodayDate;
        public int MID, EnqID;
        public string CourseName, PlanName, InstructorName;
        public DateTime CourseStartDate, CourseEndtDate, PayDate, NextPayDate, EnqDate;
        public string Executive, PaidAmt, PayMode, ChequeNo, RemFees, TotalFees, TotalPaidFees, TotalBalanceFees, RecpID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }


        #region ----------- Assign Branch Id And Company I -----------
        private void AssignID()
        {
            ObjGropsms.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            ObjGropsms.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region ----------------- Assign Today Date ------------------
        private void AssignTodayDate()
        {
            try
            {

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                string Todate = localTime.ToString("dd-MM-yyyy");

                if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                {
                }

                ObjGropsms.TodayDate = TodayDate;
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region --------------- Display Message -------------------
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

        #region----------------- SMS Function-------------------------

        string suname, spass, senderid, routeid, status;
        public int SendSMSFun(string Mobile, string Message)
        {
            int Val;
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";
                DataSet ds = new DataSet();

                ds = ObjGropsms.GetSMSLoginDetails();

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
            //sb.Replace("#NextBalanceDate#", NextPayDate.ToString("dd/MM/yyyy"));
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

        #region -------- Send Todays Enquiry SMS --------------
        protected void btnEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        EnquirySMSWithName();
                    }
                    else
                    {
                        EnquirySMSWithoutName();
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

        private void EnquirySMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enquiry"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enquiry"].ToString();

                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayEnquiry";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Enqiry Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string Name = dataTable.Rows[i]["Name"].ToString();
                            string Contact = dataTable.Rows[i]["Contact1"].ToString();
                            EnqID = Convert.ToInt32(dataTable.Rows[i]["Enq_ID1"]);
                            EnqDate = Convert.ToDateTime(dataTable.Rows[i]["EnqDate"]);

                            string St = "";
                            string Gender = dataTable.Rows[i]["Gender"].ToString();
                            if (Gender == "Male")
                            {
                                St = "Sir";
                            }
                            if (Gender == "Female")
                            {
                                St = "Madam";
                            }

                            Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                            res = SendSMSFun(Contact, Message);

                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Enquiry Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        private void EnquirySMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enquiry"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enquiry"].ToString();
                                       
                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayEnquiry";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Enquiry Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string Contact = dataTable.Rows[i]["Contact1"].ToString();
                            EnqID = Convert.ToInt32(dataTable.Rows[i]["Enq_ID1"]);
                            EnqDate = Convert.ToDateTime(dataTable.Rows[i]["EnqDate"]);

                            Message = GetMSG(TemplateMessage);
                            res = SendSMSFun(Contact, Message);

                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Enquiry Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }      

        #endregion

        #region----------------- Send Registration SMS -----------------
        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        RegSMSWithName();
                    }
                    else
                    {
                        RegSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        private void RegSMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Renew"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Renew"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayRegistration";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Registeration Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                MID = Convert.ToInt32(dataTable.Rows[i]["Member_ID1"]);
                                TotalFees = dataTable.Rows[i]["Total_Fees"].ToString();
                                PaidAmt = dataTable.Rows[i]["PaidFee"].ToString();
                                RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                PayDate = Convert.ToDateTime(dataTable.Rows[i]["payDate"].ToString());
                                NextPayDate = Convert.ToDateTime(dataTable.Rows[i]["NextPay_Date"]);
                                PayMode = dataTable.Rows[i]["PaymentMode"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                ChequeNo = dataTable.Rows[i]["ChequeNo"].ToString();
                            
                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }                           
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Registration Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void RegSMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Renew"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Renew"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;

                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayRegistration";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Registeration Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                MID = Convert.ToInt32(dataTable.Rows[i]["Member_ID1"]);
                                TotalFees = dataTable.Rows[i]["Total_Fees"].ToString();
                                PaidAmt = dataTable.Rows[i]["PaidFee"].ToString();
                                RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                NextPayDate = Convert.ToDateTime(dataTable.Rows[i]["NextPay_Date"]);
                                PayMode = dataTable.Rows[i]["PaymentMode"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                ChequeNo = dataTable.Rows[i]["ChequeNo"].ToString();
                          
                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }

                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Registration Record Not Found !!!','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }       

        #endregion       

        #region ------------------- Todays Birthday SMS -----------------
        protected void btnTodayBirthday_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodaysBirthdaySMSWithName();
                    }
                    else
                    {
                        TodaysBirthdaySMSWithoutName();
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

        private void TodaysBirthdaySMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["birthday"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["birthday"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayBirthday";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Birthday Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();

                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + TemplateMessage;

                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Birthday Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        private void TodaysBirthdaySMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["birthday"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["birthday"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayBirthday";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Birthday Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();

                                Message = TemplateMessage;

                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Birthday Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }
        #endregion

        #region ----------- Todays Anniversary SMS -----------------
        protected void btnTodayAnniversary_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodaysAnniversarySMSWithName();
                    }
                    else
                    {
                        TodaysAnniversarySMSWithoutName();
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

        private void TodaysAnniversarySMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Aniversary"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Aniversary"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayAniversary";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Aniversary Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();


                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + TemplateMessage;

                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Aniversary Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        private void TodaysAnniversarySMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Aniversary"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Aniversary"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayAniversary";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Aniversary Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();

                                Message = TemplateMessage;

                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Aniversary Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        #endregion

        #region --------------- Todays Enquiry Followup --------------
        protected void btnEnquiryFllowup_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodaysEnquiryFllowupSMSWithName();
                    }
                    else
                    {
                        TodaysEnquiryFllowupSMSWithoutName();
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

        private void TodaysEnquiryFllowupSMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["EnquiryFollowup"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["EnquiryFollowup"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayEnquiryFollowup";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Enquiry Followup Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string Name = dataTable.Rows[i]["Name"].ToString();
                            string Contact = dataTable.Rows[i]["Contact1"].ToString();


                            string St = "";
                            string Gender = dataTable.Rows[i]["Gender"].ToString();
                            if (Gender == "Male")
                            {
                                St = "Sir";
                            }
                            if (Gender == "Female")
                            {
                                St = "Madam";
                            }

                            Message = "Dear " + Name + " " + St + ", " + TemplateMessage;

                            res = SendSMSFun(Contact, Message);

                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Enquiry Followup Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        private void TodaysEnquiryFllowupSMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["EnquiryFollowup"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["EnquiryFollowup"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayEnquiryFollowup";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Enquiry Followup Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string Contact = dataTable.Rows[i]["Contact1"].ToString();

                            Message = TemplateMessage;

                            res = SendSMSFun(Contact, Message);

                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Enquiry Followup Record Not Found !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        #endregion

        #region ------------------- Todays Balance Remainder --------------------
        protected void btnBalReminder_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                       BalanceRemainderSMSWithName();
                    }
                    else
                    {
                        BalanceRemainderSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void BalanceRemainderSMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Todaybalance"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Todaybalance"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayBalanceRem";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Registeration Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                MID = Convert.ToInt32(dataTable.Rows[i]["Member_ID1"]);
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();                           
                                RemFees = dataTable.Rows[i]["RemainingFee"].ToString();                            
                            
                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }                         
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Balance Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        private void BalanceRemainderSMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Todaybalance"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Todaybalance"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayBalanceRem";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Registeration Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                MID = Convert.ToInt32(dataTable.Rows[i]["Member_ID1"]);
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                                          
                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Balance Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        #endregion

        #region ------------------- Todays Balance Paid --------------------
        protected void btntodayBalPaid_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        BalancePaidSMSWithName();
                    }
                    else
                    {
                        BalancePaidSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void BalancePaidSMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["balancepaid"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["balancepaid"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayBalancePaid";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Registeration Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                              
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                MID = Convert.ToInt32(dataTable.Rows[i]["Member_ID1"]);
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                PaidAmt = dataTable.Rows[i]["PaidFees"].ToString();
                                RemFees = dataTable.Rows[i]["RemainingFee"].ToString();

                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }                 
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Balance Paid Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void BalancePaidSMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["balancepaid"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["balancepaid"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayBalancePaid";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Registeration Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                MID = Convert.ToInt32(dataTable.Rows[i]["Member_ID1"]);
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                PaidAmt = dataTable.Rows[i]["PaidFees"].ToString();
                                RemFees = dataTable.Rows[i]["RemainingFee"].ToString();                                

                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Balance Paid Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        #endregion

        #region ------------------ Today End Date SMS ------------------
        protected void btnTodayEndDate_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodayEndDateSMSWithName();
                    }
                    else
                    {
                        TodayEndDateSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void TodayEndDateSMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayEnddate";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime( dataTable.Rows[i]["EndDate"].ToString());


                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }                      
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void TodayEndDateSMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate"].ToString();

                    //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //{
                    //}

                    //ObjGropsms.TodayDate = TodayDate;
                    AssignTodayDate();
                    ObjGropsms.Action = "Get_TodayEnddate";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {                                
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Todays Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        #endregion

        #region ------------------ Before 1 day End Date SMS ------------------
        protected void btnEndBefore1Days_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodayEndDate_1_SMSWithName();
                    }
                    else
                    {
                        TodayEndDate_1_SMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void TodayEndDate_1_SMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate1"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate1"].ToString();

                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+1).ToString("dd-MM-yyyy");                   
                    
                    //string Todate = DateTime.Now.Date.AddDays(+1).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_1";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }   
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 1 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void TodayEndDate_1_SMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate1"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate1"].ToString();

                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+1).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+1).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_1";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }                     
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 1 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        #endregion

        #region ------------------ Before 2 day End Date SMS ------------------
        protected void btnEndBefore2Days_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodayEndDate_2_SMSWithName();
                    }
                    else
                    {
                        TodayEndDate_2_SMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void TodayEndDate_2_SMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate2"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate2"].ToString();

                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+2).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+2).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_2";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 2 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void TodayEndDate_2_SMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate2"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate2"].ToString();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+2).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+2).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_2";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 2 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        #endregion

        #region ------------------ Before 3 day End Date SMS ------------------
        protected void btnEndBefore3Days_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodayEndDate_3_SMSWithName();
                    }
                    else
                    {
                        TodayEndDate_3_SMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void TodayEndDate_3_SMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate3"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate3"].ToString();

                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                    string Todate = localTime.AddDays(+3).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+3).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_3";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 3 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void TodayEndDate_3_SMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate3"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate3"].ToString();

                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+3).ToString("dd-MM-yyyy"); 
                    //string Todate = DateTime.Now.Date.AddDays(+3).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_3";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 3 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        #endregion

        #region ------------------ Before 4 day End Date SMS ------------------
        protected void btnEndBefore4Days_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodayEndDate_4_SMSWithName();
                    }
                    else
                    {
                        TodayEndDate_4_SMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void TodayEndDate_4_SMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate4"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate4"].ToString();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+4).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+4).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_4";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 4 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void TodayEndDate_4_SMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate4"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate4"].ToString();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+4).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+4).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_4";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());
                               
                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 4 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        #endregion

        #region ------------------ Before 5 day End Date SMS ------------------
        protected void btnEndBefore5Days_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjGropsms.Action = "SELECT_SMSLogin_INFO";

                dataTable = ObjGropsms.GetSMSLoginDatails();

                if (dataTable.Rows.Count != 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        TodayEndDate_5_SMSWithName();
                    }
                    else
                    {
                        TodayEndDate_5_SMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('SMS Login Details Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void TodayEndDate_5_SMSWithName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate5"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate5"].ToString();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+5).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+5).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_5";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string Name = dataTable.Rows[i]["Name"].ToString();
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());

                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                Message = "Dear " + Name + " " + St + ", " + GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 5 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }

        }

        private void TodayEndDate_5_SMSWithoutName()
        {
            AssignID();

            ObjGropsms.Action = "Get_Template";
            dataTable = ObjGropsms.GetTemplate(); // Get Template Message

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["Enddate5"].ToString() != string.Empty)
                {
                    string TemplateMessage = dataTable.Rows[0]["Enddate5"].ToString();
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                    string Todate = localTime.AddDays(+5).ToString("dd-MM-yyyy"); 

                    //string Todate = DateTime.Now.Date.AddDays(+5).ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjGropsms.TodayDate = TodayDate;
                    ObjGropsms.Action = "Get_TodayEnddate_5";

                    dataTable = ObjGropsms.GetDetails(); // Get Today Course End Date Details
                    if (dataTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {                                
                                string Contact = dataTable.Rows[i]["Contact1"].ToString();
                                //RemFees = dataTable.Rows[i]["RemainingFee"].ToString();
                                RecpID = dataTable.Rows[i]["ReceiptID"].ToString();
                                CourseName = dataTable.Rows[i]["Package"].ToString();
                                CourseEndtDate = Convert.ToDateTime(dataTable.Rows[i]["EndDate"].ToString());
                                
                                Message = GetMSG(TemplateMessage);
                                res = SendSMSFun(Contact, Message);
                            }
                        }

                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Before 5 Day Course End Date Record Not Found !!.','Information');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Template Is Empty !!!','Error');", true);
            }
        }

        #endregion


    }
}