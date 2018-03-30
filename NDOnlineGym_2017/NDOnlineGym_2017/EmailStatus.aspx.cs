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

namespace NDOnlineGym_2017
{
    public partial class EmailStatus : System.Web.UI.Page
    {
        BalEmail objEmail = new BalEmail();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStatus.Focus();
                GetData();
                // ddlStatus.Attributes["value"] = ddlStatus.Text;
               
            }

        }
        public void GetData()
        {
            objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = objEmail.Select_DetailEmailStatusLogin();
            if (dt.Rows.Count > 0)
            {
                btnSave.Text = "Edit";
                EnabledFalse();
                ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                ViewState["ID"] = dt.Rows[0]["Email_AutoID"].ToString();
            }
            else
            {
                btnSave.Text = "Save";
            }
        }
        private void EnabledFalse()
        {
            ddlStatus.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objEmail.Status = ddlStatus.SelectedValue;
                    int res = objEmail.Insert_EmailStatus();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Status Saved Successfully !!!','Success');", true);
                        EnabledFalse();
                    }
                }
               else if (btnSave.Text == "Edit")
                {
                    btnSave.Text = "Update";
                    ddlStatus.Enabled = true;
                    ddlStatus.Focus();
                }
              
                else if (btnSave.Text == "Update")
                {
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    // objBal_MasterEmail.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
                    dt = objEmail.Select_DetailEmailStatusLogin();
                    if (dt.Rows.Count > 0)
                        objEmail.Email_AutoID = Convert.ToInt32(dt.Rows[0]["Email_AutoID"].ToString());
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Status Updated Successfully !!!','Success');", true);
                        return;
                    }
                    objEmail.Status = ddlStatus.SelectedValue;
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    int res = objEmail.Update_EmailStatus();
                    if (res >= 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Status Updated Successfully !!!','Success');", true);
                        btnSave.Text = "Edit";
                        EnabledFalse();
                    }
                    //ddlStatus.Attributes["value"] = ddlStatus.Text;
                    //txtPassword.TextMode = TextBoxMode.Password;
                }
                 
               
               if (btnSave.Text == "Save")
                {
                    btnSave.Text = "Edit";
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }


    }
}