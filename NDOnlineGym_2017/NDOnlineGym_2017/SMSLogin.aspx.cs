using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class SMSLogin : System.Web.UI.Page
    {
        BalSMSLogin objSMS = new BalSMSLogin();
        DataTable dataTable = new DataTable();
        int res;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //AssignID();

                    fillForm();

                    if (btnSave.Text == "Edit")
                    {
                        btnClear.Visible = false;
                    }

                    txtUsername.Focus();
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        private void fillForm()
        {
            objSMS.Action = "SELECT_SMSLogin_INFO";
            AssignID();
            dataTable = objSMS.GetDetails();

            if (dataTable.Rows.Count > 0)
            {
                ReadMode();
                txtUsername.Text = dataTable.Rows[0]["Username"].ToString();
                txtPassword.Text = dataTable.Rows[0]["Password"].ToString();
                txtSenderID.Text = dataTable.Rows[0]["Sender_ID"].ToString();
                txtRoute.Text = dataTable.Rows[0]["Route"].ToString();
                ddlStatus.SelectedValue = dataTable.Rows[0]["Status"].ToString();
                ddlSMSStatus.SelectedValue = dataTable.Rows[0]["AutoStatus"].ToString();
                ddlSMSWithName.SelectedValue = dataTable.Rows[0]["SMSWithName"].ToString();

                ViewState["SMS_AutoID"] = dataTable.Rows[0]["SMS_AutoID"].ToString();
            }
            else
            {
                btnSave.Text = "Save";
                ddlStatus.SelectedIndex = 1;
                WriteMode();
            }
        }

        private void ReadMode()
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtSenderID.Enabled = false;
            txtRoute.Enabled = false;
            ddlStatus.Enabled = false;
            ddlSMSStatus.Enabled = false;
            ddlSMSWithName.Enabled = false;
        }

        private void WriteMode()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtSenderID.Enabled = true;
            txtRoute.Enabled = true;
            ddlStatus.Enabled = true;
            ddlSMSStatus.Enabled = true;
            ddlSMSWithName.Enabled = true;
        }


        private void AssignID()
        {
            objSMS.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); // Convert.ToInt32(Session["Branch_ID"]);
            objSMS.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    objSMS.Action = "INSERT";
                    AssignID();
                    AddParameters();
                    res = objSMS.Insert_CompanyInformation();
                    if (res > 0)
                    {
                        fillForm();
                        btnSave.Text = "Edit";
                        btnClear.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    objSMS.Action = "Update";                    
                    AddParameters();
                    objSMS.SMS_AutoID = Convert.ToInt32(ViewState["SMS_AutoID"]);

                    res = objSMS.Insert_CompanyInformation();
                    if (res > 0)
                    {
                        fillForm();
                        ReadMode();                        
                        btnSave.Text = "Edit";
                        btnClear.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);

                    }
                }
                else if (btnSave.Text == "Edit")
                {
                    btnSave.Text = "Update";
                    fillForm();
                    WriteMode();
                    txtUsername.Focus();
                    btnClear.Visible = true;
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
            objSMS.Username = Regex.Replace(txtUsername.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");  //txtUsername.Text;
            objSMS.Password = Regex.Replace(txtPassword.Text, "^[ \t\r\n]+|[ \t\r\n]+$", ""); // txtPassword.Text;
            objSMS.Sender_ID = Regex.Replace(txtSenderID.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");   //txtSenderID.Text;
            objSMS.Route = Regex.Replace(txtRoute.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");   //txtRoute.Text;
            objSMS.Status =ddlStatus.SelectedValue;
            objSMS.AutoStatus = ddlSMSStatus.SelectedValue;
            objSMS.SMSWithName= ddlSMSWithName.SelectedValue;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearRecord();

        }

        private void ClearRecord()
        {
            txtUsername.Focus();
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtSenderID.Text = string.Empty;
            txtRoute.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlSMSStatus.SelectedIndex = 0;
            ddlSMSWithName.SelectedIndex = 0;
        }



    }
}