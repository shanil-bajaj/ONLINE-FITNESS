using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using BusinessAccessLayer;
using DataAccessLayer;


namespace NDOnlineGym_2017
{
    public partial class SMSTemplate : System.Web.UI.Page
    {
        BalSMSTemplate objSMSTemplate = new BalSMSTemplate();
        DataTable dataTable = new DataTable();
        int res;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtMemberBirthdat.Focus();
                    fillForm();
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
            AssignID();
            objSMSTemplate.Action = "SELECT_SMSTemplate";
            dataTable = objSMSTemplate.GetDetails();

            if (dataTable.Rows.Count > 0)
            {
                ReadMode();
                txtMemberBirthdat.Text = dataTable.Rows[0]["birthday"].ToString();
                txtStaffBirthday.Text = dataTable.Rows[0]["birthday_Staff"].ToString();
                txtMemberAnniversary.Text = dataTable.Rows[0]["Aniversary"].ToString();
                txtEnquiry.Text = dataTable.Rows[0]["Enquiry"].ToString();
                txtEnquiryFollowup.Text = dataTable.Rows[0]["EnquiryFollowup"].ToString();
                txtRegistration.Text = dataTable.Rows[0]["Renew"].ToString();
                txtUpgrade.Text = dataTable.Rows[0]["Upgrade"].ToString();
                txtTodaysBalance.Text = dataTable.Rows[0]["Todaybalance"].ToString();
                txtBalance.Text = dataTable.Rows[0]["balancepaid"].ToString();
                txtEndBefore.Text = dataTable.Rows[0]["Enddate5"].ToString();
                txtEndBefore4.Text = dataTable.Rows[0]["Enddate4"].ToString();
                txtEndBefore3.Text = dataTable.Rows[0]["Enddate3"].ToString();
                txtEndBefore2.Text = dataTable.Rows[0]["Enddate2"].ToString();
                txtEndBefore1.Text = dataTable.Rows[0]["Enddate1"].ToString();
                txtTodayEndDate.Text = dataTable.Rows[0]["Enddate"].ToString();
                txtAbsentMember.Text = dataTable.Rows[0]["AbesntMember"].ToString();

                ViewState["SMSTemplate_AutoID"] = dataTable.Rows[0]["SMSTemplate_AutoID"].ToString();
            }
            else
            {
                btnSave.Text = "Save";
                WriteMode();
            }
        }

        private void AssignID()
        {
            objSMSTemplate.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objSMSTemplate.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        private void ReadMode()
        {
            txtMemberBirthdat.Enabled = false;
            txtStaffBirthday.Enabled = false;
            txtMemberAnniversary.Enabled = false;
            txtEnquiry.Enabled = false;
            txtEnquiryFollowup.Enabled = false;
            txtRegistration.Enabled = false;
            txtUpgrade.Enabled = false;
            txtTodaysBalance.Enabled = false;
            txtBalance.Enabled = false;
            txtEndBefore.Enabled = false;
            txtEndBefore4.Enabled = false;
            txtEndBefore3.Enabled = false;
            txtEndBefore2.Enabled = false;
            txtEndBefore1.Enabled = false;
            txtTodayEndDate.Enabled = false;
            txtAbsentMember.Enabled = false;

        }

        private void WriteMode()
        {
            txtMemberBirthdat.Enabled = true;
            txtStaffBirthday.Enabled = true;
            txtMemberAnniversary.Enabled = true;
            txtEnquiry.Enabled = true;
            txtEnquiryFollowup.Enabled = true;
            txtRegistration.Enabled = true;
            txtUpgrade.Enabled = true;
            txtTodaysBalance.Enabled = true;
            txtBalance.Enabled = true;
            txtEndBefore.Enabled = true;
            txtEndBefore4.Enabled = true;
            txtEndBefore3.Enabled = true;
            txtEndBefore2.Enabled = true;
            txtEndBefore1.Enabled = true;
            txtTodayEndDate.Enabled = true;
            txtAbsentMember.Enabled = true;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    objSMSTemplate.Action = "INSERT";
                    AssignID();
                    AddParameters();
                    res = objSMSTemplate.Insert_SMSTemplateInformation();
                    if (res > 0)
                    {
                        fillForm();
                        btnSave.Text = "Edit";                        
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    objSMSTemplate.Action = "Update";
                    AddParameters();
                    objSMSTemplate.SMSTemplate_AutoID = Convert.ToInt32(ViewState["SMSTemplate_AutoID"]);

                    res = objSMSTemplate.Insert_SMSTemplateInformation();
                    if (res > 0)
                    {
                        fillForm();
                        ReadMode();
                        btnSave.Text = "Edit";                        
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                      

                    }
                }
                else if (btnSave.Text == "Edit")
                {
                    btnSave.Text = "Update";
                    txtMemberBirthdat.Focus();
                    fillForm();
                    WriteMode();                    
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
           objSMSTemplate.birthday= Regex.Replace(txtMemberBirthdat.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.birthday_Staff = Regex.Replace(txtStaffBirthday.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Aniversary = Regex.Replace(txtMemberAnniversary.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Enquiry = Regex.Replace(txtEnquiry.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.EnquiryFollowup = Regex.Replace(txtEnquiryFollowup.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Renew = Regex.Replace(txtRegistration.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Upgrade = Regex.Replace(txtUpgrade.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Todaybalance = Regex.Replace(txtTodaysBalance.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.balancepaid = Regex.Replace(txtBalance.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Enddate5 = Regex.Replace(txtEndBefore.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Enddate4 = Regex.Replace(txtEndBefore4.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Enddate3 = Regex.Replace(txtEndBefore3.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Enddate2 = Regex.Replace(txtEndBefore2.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Enddate1 = Regex.Replace(txtEndBefore1.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.Enddate = Regex.Replace(txtTodayEndDate.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           objSMSTemplate.AbesntMember = Regex.Replace(txtAbsentMember.Text, "^[ \t\r\n]+|[ \t\r\n]+$", ""); 
        }



    }
}