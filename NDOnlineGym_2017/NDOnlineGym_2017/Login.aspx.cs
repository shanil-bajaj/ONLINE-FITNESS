using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using BusinessAccessLayer;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Mail;
using System.Globalization;
using DataAccessLayer;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
namespace NDOnlineGym_2017
{
    public partial class Login : System.Web.UI.Page
    {
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();
        //DataTable dt = new DataTable();
        BalSendQuickSMS objBalSendQuickSMS = new BalSendQuickSMS();
        BalLoginForm ObjLogin = new BalLoginForm();
        DataTable dt = new DataTable();
        public string auth;
        string Name, UserName, Password, Email, Mobile, To;
        public int STF_Id;
        int User_Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            if (!IsPostBack)
            {
                DateTime TodayDate;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                { }
                ObjLogin.TodayDate = TodayDate;
                ObjLogin.UpdateStatusCourse();
                ObjLogin.UpdateStatusMember();


                dt = obBalBranchInformation.SELECT_CompanyDetails();
                if (dt.Rows.Count > 0)
                {
                    //lblCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                    imgCompanyLogo.ImageUrl = dt.Rows[0]["CompanyLogoPath"].ToString();
                    //lblName.Text = Request.Cookies["OnlineGym"]["Name"].ToString();
                }
            }
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            divForgotPassword.Visible = true;
            divLogin.Visible = false;
            txtForgotUsername.Focus();
        }

        protected void btnLoginLink_Click(object sender, EventArgs e)
        {
            divForgotPassword.Visible = false;
            divLogin.Visible = true;

        }
        public static string username;
        public string status12;
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
              
                Session["Username"] = txtUsername.Text;
                username = txtUsername.Text;
                ObjLogin.Username = txtUsername.Text;
                ObjLogin.Password = txtPassword.Text;
               // ObjLogin.Status = status12;
               // ObjLogin.Status =(dt.Rows[0]["Status"]).ToString();
                bool chk_UserLogin = false;
                chk_UserLogin = ObjLogin.chk_UserLogin();

                if (chk_UserLogin == true)
                {
                    dt.Rows.Clear(); dt.Columns.Clear();
                    dt = ObjLogin.Bind_UserLogin();
                    if (dt.Rows.Count > 0)
                    {
                        Response.Cookies["OnlineGym"]["Name"] = dt.Rows[0]["Name"].ToString();
                        Response.Cookies["OnlineGym"]["Login_ID"] = dt.Rows[0]["Login_AutoID"].ToString();
                        Response.Cookies["OnlineGym"]["Staff_AutoID"] = dt.Rows[0]["Staff_AutoID"].ToString();
                        //Response.Cookies["OnlineGym"]["Staff_ID"] = dt.Rows[0]["Staff_AutoID"].ToString();
                        Response.Cookies["OnlineGym"]["Branch_ID"] = dt.Rows[0]["Branch_AutoID"].ToString();
                        Session["Branch_ID"] = dt.Rows[0]["Branch_AutoID"].ToString();
                        //Session["Branch_ID"] = dt.Rows[0]["Branch_AutoID"].ToString();
                        Response.Cookies["OnlineGym"]["Authority"] = dt.Rows[0]["Authority"].ToString();
                        Response.Cookies["OnlineGym"]["Company_ID"] = dt.Rows[0]["Company_AutoID"].ToString();
                        Response.Cookies["OnlineGym"].Expires = DateTime.Now.AddDays(1);//DateTime.Now.AddMinutes(10);

                        DateTime LoginDate;
                        if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out LoginDate))
                        { }
                        ObjLogin.Date = LoginDate;

                        DateTime LoginTime;
                        if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("hh:mm:ss"), "hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out LoginTime))
                        { }
                        ObjLogin.Time = LoginTime;

                        //int Login_AutoID_fk;
                       // ObjLogin.Login_AutoID_fk = Login_AutoID_fk;
                        ObjLogin.Login_AutoID_fk = Convert.ToInt32(dt.Rows[0]["Login_AutoID"]);
                        //ObjLogin 
                        //ObjLogin.Authority = dt.Rows[0]["Authority"].ToString();
                        //ObjLogin.Staff_AutoID = Convert.ToInt32(dt.Rows[0]["Staff_AutoID"]);
                        //ObjLogin.Company_AutoID =Convert.ToInt32(dt.Rows[0]["Company_AutoID"]);
                       // ObjLogin.Branch_AutoID = Convert.ToInt32(dt.Rows[0]["Branch_AutoID"]);
                         int i = ObjLogin.Insert_LoginDetails();
                         ObjLogin.Status = dt.Rows[0]["Status"].ToString();

                         status12 = dt.Rows[0]["Status"].ToString();
                       
                         if (status12 == "Active")
                         {
                             //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                             //{
                             Response.Redirect("~/Home.aspx", false);
                             //}
                             //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                             //{
                             //    Response.Redirect("~/Home.aspx", false);
                             //}
                             //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                             //{
                             //    Response.Redirect("~/Home.aspx", false);
                             //}
                         }
                         else
                         {
                              ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Login Failed Because User is Deactive !!!','Error');", true);
                              //btnForgotPassword.Enabled = false;
                              txtUsername.Text = "";
                              txtUsername.Focus();
                         }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Wrong Username Or Password !!!','Error');", true);
                        clear();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Wrong Username Or Password !!!','Error');", true);
                    clear();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //if (status12 == "Active")
                //{
                    ObjLogin.Username = txtForgotUsername.Text;
                    ObjLogin.Email = txtEmail.Text;
                    dt = ObjLogin.getUserDetailsByEmail();
                    if (dt.Rows.Count > 0)
                    {
                        Name = dt.Rows[0]["Name"].ToString();
                        UserName = dt.Rows[0]["Username"].ToString();
                        User_Id = Convert.ToInt32(dt.Rows[0]["Login_AutoID"].ToString());
                        Password = dt.Rows[0]["Password"].ToString();
                        To = dt.Rows[0]["Email"].ToString();
                        Mobile = dt.Rows[0]["Mobile"].ToString();

                        SendEmail();
                        SendPassword_By_SMS();
                        clear();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Email id is not valid.','Error');", true);
                    }
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Cannot Recover Password, Because User is Deactive !!!','Error');", true);

                //    clear();
                //}
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        
        private void SendEmail()
        {
            MailMessage mailObj = new MailMessage();

            string Sub = " Your ND Fitness+ Password";
            string Body = "Dear  " + Name + " <br />  ND Fitness+ User, <br />This is a system generated email.Please do not reply.<br/><br/> Your account details is:<br/>Username : " + UserName + "<br/>Password : " + Password + "<br/><br/> Please ignore this email if you did not request for password. <br/><br/>Please feel free to contact us 9156184755 or email navkardreamsoft@gmail.com<br/><br/>Thanks <br/> Team ND Fitness+";
            mailObj.From = new MailAddress("gymfitnessplus27@gmail.com");

            if (To == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Email ID Does Not Exist','Error');", true);
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
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Password Has Been Sent !!!','Success');", true);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Email Failed','Error');", true);
                }
            }
        }

        string suname1, spass1, senderid1, routeid1, status1, Mobile1, Message1;
        public int SendSMS(string Mobile, string Message)
        {
            int Val;
            try
            {
                //obBalSendSMSForm.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
                //dt = ObjLogin.GetData();
                //if (dt.Rows.Count > 0)
                //{
                //    suname1 = dt.Rows[0]["Username"].ToString();
                //    spass1 = dt.Rows[0]["Password"].ToString();
                //    senderid1 = dt.Rows[0]["SenderId"].ToString();
                //    routeid1 = dt.Rows[0]["Route"].ToString();
                //    status1 = dt.Rows[0]["Status"].ToString();

                suname1 = "aaa1";
                spass1 = "Navkaraaa1";
                senderid1 = "Gymmmm";
                routeid1 = "trans1";
                status1 = "ON";
                //}
                //objBalSendQuickSMS.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"]);
                //objBalSendQuickSMS.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //objBalSendQuickSMS.Action = "SELECT_SMSLogin_INFO";
                //DataSet ds = new DataSet();

                //ds = objBalSendQuickSMS.GetSMSLoginDetails();

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    suname1 = ds.Tables[0].Rows[0]["Username"].ToString();
                //    spass1 = ds.Tables[0].Rows[0]["Password"].ToString();
                //    senderid1 = ds.Tables[0].Rows[0]["Sender_ID"].ToString();
                //    routeid1 = ds.Tables[0].Rows[0]["Route"].ToString();
                //    status1 = ds.Tables[0].Rows[0]["Status"].ToString();
                //}

                Val = 0;
                string strUrl = "http://173.45.76.226:81/send.aspx?username=" + suname1 + "&pass=" + spass1 + "&route=" + routeid1 + "&senderid=" + senderid1 + "&numbers=" + Mobile + "&message=" + Message + "";

                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();

            }
            catch (WebException ex)
            {
                Val = 1;
            }

            if (Val == 1)
                return 1;
            else if (Val == 2)
                return 2;
            else return 0;
        }

        public void SendPassword_By_SMS()
        {
            //obBalSendSMSForm.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);

            Name = dt.Rows[0]["Name"].ToString();
            UserName = dt.Rows[0]["Username"].ToString();
            User_Id = Convert.ToInt32(dt.Rows[0]["Login_AutoID"].ToString());
            Password = dt.Rows[0]["Password"].ToString();
            To = dt.Rows[0]["Email"].ToString();
            Mobile = dt.Rows[0]["Mobile"].ToString();

            //Pass = dt.Rows[0]["Password"].ToString();
            //user_Id = Convert.ToInt32(dt.Rows[0]["Login_ID"].ToString());
            //Username = dt.Rows[0]["Username"].ToString();
            //to = dt.Rows[0]["Email"].ToString();
            //Mobile = dt.Rows[0]["Mobile"].ToString();

            string Message = "Hi " + Name + ",\nyour Login details \n\n Username : " + UserName + "\n Password : " + Password + "\n\n Thanks \n ND Fitness+ ";


            int res1 = SendSMS(Mobile, Message);

            if (res1 == 1)
            {
                //Error_Validation_Page("Message Sending failed due to Internet Connection !!!");
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Message Sending Failed Due To Internet Connection','Failed');", true);
                return;
            }
            else if (res1 == 2)
            {
                //Error_Validation_Page("Message not send! SMS Status is off !!!");
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.warning('Message not send! SMS Status is off !!!','Warning');", true);
                return;
            }
            else
            {
                //Successfull_Validation_Page("Message Sent Successfully !!!");
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Message Has Been Sent','Success');", true);

            }

        }

        public void clear()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtForgotUsername.Text = "";
            txtMobile.Enabled = true;
            txtEmail.Enabled = true;
        }

       

        private void AssignID()
        {

            ObjLogin.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
            ObjLogin.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        protected void txtForgotUsername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjLogin.Username = Convert.ToString(txtForgotUsername.Text.Trim());
                // ObjLogin.Action = "SearchByUsername";
                dt = ObjLogin.LoginDetails();
                if (dt.Rows.Count > 0)
                {
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                    txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                    txtEmail.Enabled = false;
                    txtMobile.Enabled = false;
                    btnSend.Focus();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    clear();
                    txtForgotUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            //if (status12 == "Active")
            //{
            //    //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            //    //{
            //    Response.Redirect("~/Home.aspx", false);
            //    //}
            //    //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            //    //{
            //    //    Response.Redirect("~/Home.aspx", false);
            //    //}
            //    //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            //    //{
            //    //    Response.Redirect("~/Home.aspx", false);
            //    //}
            //}
            //else
            //{
            //    //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Login Failed Because User is Deactive !!!','Error');", true);
            //    btnForgotPassword.Enabled = false;
            //    //txtUsername.Text = "";
            //    //txtUsername.Focus();
            //}
        }
    }
}