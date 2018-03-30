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
using System.IO;
using ClosedXML.Excel;
using System.Configuration;
using System.Data.SqlClient;

namespace NDOnlineGym_2017
{
    public partial class SendDataOnEmail : System.Web.UI.Page
    {
        BalEmail objEmail = new BalEmail();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AssignTodaysDate();
                BindData();
                txtFromDate.Focus();
            }
        }

        #region Assign_Date
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDate.Text = todaydate.ToString("dd-MM-yyyy");
                txtToDate.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        public DataTable BindData()
        {
            try
            {
                objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                DateTime todaydate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                {
                    string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                    objEmail.FromDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                DateTime todaydate2;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate2))
                {
                    string todaydate3 = todaydate2.ToString("dd-MM-yyyy");
                    objEmail.ToDate = DateTime.ParseExact(todaydate3, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                //DateTime fromdate;
                //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromdate))
                //{
                //    objEmail.FromDate = fromdate;
                //}

                //DateTime todate;
                //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todate))
                //{
                //    objEmail.ToDate = todate;
                //}
              
               
                dt = objEmail.All_MemberInfo();
                gvSendMail.DataSource = dt;
                gvSendMail.DataBind();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvSendMail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSendMail.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            if (gvSendMail.Rows.Count == 0)
            {
                
            }
            else
            {               
                
                ExportExcel();
            }
        }
        public void ExportExcel()
        {
            //Get the GridView Data from database.
            DataTable dt = BindData();// GetData();

            //Set DataTable Name which will be the name of Excel Sheet.
            dt.TableName = "GridView_Data";

            //Create a New Workbook.
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add the DataTable as Excel Worksheet.
                wb.Worksheets.Add(dt);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //Save the Excel Workbook to MemoryStream.
                    wb.SaveAs(memoryStream);

                    //Convert MemoryStream to Byte array.
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    //Send Email with Excel attachment.
                    DataTable k = new DataTable();
                    k = objEmail.Get_companyEmailID();

                    if (k.Rows.Count > 0)
                    {
                        string Email = k.Rows[0]["Email"].ToString();
                       
                        if (Email != "")
                        {
                            
                            MailMessage message = new MailMessage();
                            SmtpClient smtpClient = new SmtpClient();
                            string msg = string.Empty;
                            try
                            {
                                MailAddress fromAddress = new MailAddress("ndsoft.help@gmail.com", Email);
                                message.From = fromAddress;
                                message.To.Add(Email);
                                message.Subject = "GridView Exported Excel";
                                message.IsBodyHtml = true;
                                message.Body = "GridView Exported Excel Attachment";
                                //Add Byte array as Attachment.
                                message.Attachments.Add(new Attachment(new MemoryStream(bytes), "GridView.xlsx"));
                                // message.IsBodyHtml = true;
                                smtpClient.Host = "relay-hosting.secureserver.net";   //-- Donot change.
                                smtpClient.Port = 25; //--- Donot change
                                smtpClient.EnableSsl = false;//--- Donot change
                                //smtpClient.Host = "smtp.gmail.com";
                                //smtpClient.Port = 587;
                                //smtpClient.EnableSsl = true;
                                smtpClient.UseDefaultCredentials = true;
                                smtpClient.Credentials = new System.Net.NetworkCredential("ndsoft.help@gmail.com", "ndhelpteam");

                                smtpClient.Send(message);

                                //lblConfirmationMessage.Text = "Your email successfully sent.";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Send Successfully !!!','Success');", true);
                            }
                            catch (Exception ex)
                            {
                                msg = ex.Message;
                                //Error_Validation_Page("Email Id Does not Exist..");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Send  !!!','Error');", true);
                        }
                    }
                }
            }

        }
    }
}