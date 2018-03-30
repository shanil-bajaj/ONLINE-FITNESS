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
    public partial class PTAttendance : System.Web.UI.Page
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
                txtMemberID.Focus();
            }
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

                    if (ddlGender.SelectedValue == "Male")
                    {
                        gender = "Sir";
                    }
                    if (ddlGender.SelectedValue == "Female")
                    {
                        gender = "Madam";
                    }

                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime k = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local             

                    //DateTime k = System.DateTime.Now;
                    pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                    pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    pt.Member_AutoID = Convert.ToInt32(lblMemberAutoID.Text);
                    dt = pt.session();
                    if (dt.Rows.Count > 0)
                    {
                        newstring = "Total Session =" + " " + dt.Rows[0]["TotalSession"].ToString() + "" + ", Complete Session =" + "" + dt.Rows[0]["CompleteSession"].ToString() + "" + " , Remaining Session = " + "" + dt.Rows[0]["RemSession"].ToString();
                    }
                   
                    s = "Dear" + " " + txtFirst.Text + "" + txtLast.Text + " " + gender + " " + newstring;
                    //   s = "Dear" + " " + cmbName.Text + " " + gender + " " + dr1[0][8].ToString();
                  int  res1 = SendSMSFun(txtContact.Text, s);
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
                        Clear();
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


        public void member_AutoID()
        {
            try
            {
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
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
                pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                pt.Member_AutoID = member_Auto_ID;
                pt.Member_AutoID = member_Auto_ID;
                int flg = pt.checkID_Exist_Not();
                if (flg > 0)
                {
                    dt = pt.Bindmember();
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
                    txtMemberID.Text = "";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void BindMember()
        {
            txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
            txtFirst.Text = dt.Rows[0]["FName"].ToString();
            txtLast.Text = dt.Rows[0]["LName"].ToString();
            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            txtContact.Text = dt.Rows[0]["Contact1"].ToString();
            txtmail.Text = dt.Rows[0]["Email"].ToString();
            lblMemberAutoID.Text = dt.Rows[0]["Member_AutoID"].ToString();

        }

        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            if (txtMemberID.Text != "")
            {
                BalanceID();
                txtInstructorID.Focus();
            }
        }

        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != "")
                {
                    pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    pt.Contact = txtContact.Text;
                    dt = pt.Bindmember_Contact();
                    if (dt.Rows.Count > 0)
                    {
                        BindMember();
                        txtInstructorID.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Invalid Conatct No!!!','Information');", true);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void txtInstructorID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtInstructorID.Text != "")
                {
                    pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    pt.TrainerID_Fk = Convert.ToInt32(txtInstructorID.Text);
                    dt = pt.GetStaffAutoid();
                    pt.TrainerID_Fk = Convert.ToInt32(dt.Rows[0][0].ToString());
                    dt = pt.BindInstructor();
                    if (dt.Rows.Count > 0)
                    {
                        lblinstructor.Text = dt.Rows[0][0].ToString();
                        txtInstructorName.Text = dt.Rows[0]["Name"].ToString();
                        txtAlternativeName.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Invalid Insrtuctor ID!!!','Information');", true);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertData()
        {
            pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            pt.TrainerID_Fk = Convert.ToInt32(lblinstructor.Text);
            pt.Member_AutoID = Convert.ToInt32(lblMemberAutoID.Text);

            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                pt.AttenDate = todaydate;
            }
            //pt.AttenDate="";
            pt.InTime = DateTime.UtcNow.AddHours(5.5).ToString("h:mm:ss tt"); //DateTime.Now.ToString("h:mm:ss tt");
            pt.OutTime = "";
            pt.SessionCnt = 1;
            pt.AltInstructorName = txtAlternativeName.Text;
            pt.Note = txtNote.Text;

        }
        public void Clear()
        {
            txtMemberID.Text = "";
            txtFirst.Text = "";
            txtLast.Text = "";
            txtContact.Text = "";
            txtmail.Text = "";
            ddlGender.SelectedIndex = 0;
            txtInstructorID.Text = "";
            txtInstructorName.Text = "";
            txtAlternativeName.Text = "";
            txtNote.Text = "";

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            txtMemberID.Focus();
        }
        protected void btnPresent_Click(object sender, EventArgs e)
        {

            if (txtMemberID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Invalid Member ID!!!','Information');", true);
            }

            if (txtInstructorID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Invalid Insrtuctor ID!!!','Information');", true);
            }
            if (txtMemberID.Text != "" && txtInstructorID.Text != "")
            {
                InsertData();
                int k = pt.insert_Prsent();
                if (k > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Welcome " + txtFirst.Text + " ','Success');", true);
                    SendSMSNew();
                    Clear();
                    txtMemberID.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Enter Value!!!','Information');", true);
            }
        }
    }
}