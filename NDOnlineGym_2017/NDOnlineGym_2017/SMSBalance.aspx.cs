using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class SMSBalance : System.Web.UI.Page
    {
        BalSMSLogin objSMS = new BalSMSLogin();
        DataTable dataTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
              string SMSBalance =BalanceSMS();

              if (SMSBalance == "SMS Details Empty")
              {
                  lblSMSBalance.ForeColor = System.Drawing.Color.Red;
                  lblSMSBalance.Text = "SMS Details Empty";
              }
              else if (SMSBalance != "")
              {
                  lblSMSBalance.ForeColor = System.Drawing.Color.Green;
                  lblSMSBalance.Text = SMSBalance;
              }
              else
              {
                  lblSMSBalance.ForeColor = System.Drawing.Color.Red;
                  lblSMSBalance.Text = "SMS Status Off";
              }
            
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        string suname = "", spass = "", senderid = "", routeid = "", status = "", totalSms = "";
        private string BalanceSMS()
        {
            objSMS.Action = "SELECT_SMSLogin_INFO";
            AssignID();
            dataTable = objSMS.GetDetails();
            
            try
            {
                if (dataTable.Rows.Count > 0)
                {

                    suname = dataTable.Rows[0]["Username"].ToString();
                    spass = dataTable.Rows[0]["Password"].ToString();
                    senderid = dataTable.Rows[0]["Sender_ID"].ToString();
                    routeid = dataTable.Rows[0]["Route"].ToString();

                    status = dataTable.Rows[0]["Status"].ToString();

                    if (status == "ON")
                    {
                        string strUrl = "http://173.45.76.226:81/balance.aspx?username=" + suname + "&pass=" + spass + "&route=" + routeid + "&senderid=" + senderid + "";
                        WebRequest request = HttpWebRequest.Create(strUrl);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream stream = (Stream)response.GetResponseStream();
                        StreamReader readStream = new StreamReader(stream);
                        string dataString = readStream.ReadLine();

                        if (dataString.Contains(":"))
                        {
                            string[] lines = Regex.Split(dataString, ":");
                            foreach (string line in lines)
                            {
                                totalSms = lines[1];
                            }
                        }
                        response.Close();
                        stream.Close();
                        readStream.Close();
                    }
                }
                else
                {
                    totalSms = "SMS Details Empty";
                }
                return totalSms;

            }
            catch (WebException ex)
            {
                ErrorHandiling.SendErrorToText(ex);               
                throw ex;
            }


        }

        private void AssignID()
        {
            objSMS.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objSMS.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);   
        }
    }
}