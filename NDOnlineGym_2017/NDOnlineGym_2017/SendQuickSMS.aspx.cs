using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class SendQuickSMS : System.Web.UI.Page
    {

        BalSendQuickSMS objBalSendQuickSMS = new BalSendQuickSMS();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Clear();                    
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        protected void Clear()
        {
            txtContactnum.Text = string.Empty;
            txtQuickSMS.Text = string.Empty;
            txtContactnum.Focus();
        }

        protected void btnQuickSMS_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                QuickSMSWithoutName();

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }        

        int res1 = 0;
        private void QuickSMSWithoutName()
        {

            if (txtContactnum.Text.Length == 0)
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Contact Number !!!','Error');", true);

            if (txtQuickSMS.Text.Length == 0)
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);


            if (txtContactnum.Text.Length != 0 && txtQuickSMS.Text.Length != 0)
            {
                res1 = SendSMSFun(txtContactnum.Text, txtQuickSMS.Text);
            }

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

        }

        private void AssignID()
        {
            objBalSendQuickSMS.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalSendQuickSMS.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        #region------------SMS Function--------------------

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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
       


    }
}