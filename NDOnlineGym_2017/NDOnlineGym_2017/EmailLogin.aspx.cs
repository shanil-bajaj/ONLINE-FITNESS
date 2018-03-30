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
    public partial class EmailLogin : System.Web.UI.Page
    {
        BalEmail objEmail = new BalEmail();
        DataTable dt = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUsername1.Focus();
                GetData();
                //txtPassword.Attributes["value"] = txtPassword.Text;
            }
        }
        protected void AddEmailLogin()
        {
            //objBal_MasterEmail.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
            try
            {
                objEmail.EmailID = txtUsername1.Text;
                if (txtPassword1.Text == "")
                    objEmail.Password = ViewState["Password"].ToString();
                else
                    objEmail.Password = txtPassword1.Text;
                objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        private void EnabledFalse()
        {
            txtUsername1.Enabled = false;
            txtPassword1.Enabled = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnSave.Text == "Save")
                {
                    AddEmailLogin();
                    int res = objEmail.Insert_EMAILLogin();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Email Saved Successfully !!!','Success');", true);
                        EnabledFalse();
                        txtPassword1.Attributes["value"] = txtPassword1.Text;
                        txtPassword1.TextMode = TextBoxMode.Password;
                    }
                }
             else   if (btnSave.Text == "Edit")
                {

                    btnSave.Text = "Update";
                    txtUsername1.Enabled = true;
                    txtPassword1.Enabled = true;
                    txtUsername1.Focus();

                }
                else if (btnSave.Text == "Update")
                {
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    dt = objEmail.Select_DetailsemailLogin();
                    if (dt.Rows.Count > 0)
                        objEmail.EmailLogin_AutoID = Convert.ToInt32(dt.Rows[0]["EmailLogin_AutoID"].ToString());
                    else
                    {
                        EnabledFalse();
                        btnSave.Text = "Edit";
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Email Updated Successfully !!!','Success');", true);
                        return;
                    }
                    AddEmailLogin();
                 
                    int res = objEmail.Update_EmailLogin();
                    if (res >= 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Email Updated Successfully !!!','Success');", true);
                        btnSave.Text = "Edit";

                        EnabledFalse();
                    }
                    txtPassword1.Attributes["value"] = txtPassword1.Text;
                    txtPassword1.TextMode = TextBoxMode.Password;
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
        public void GetData()
        {
            // objBal_MasterEmail.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
            objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = objEmail.Select_DetailsemailLogin();
            if (dt.Rows.Count > 0)
            {
                btnSave.Text = "Edit";
                EnabledFalse();
              
                txtUsername1.Text = dt.Rows[0]["EmailID"].ToString();
                txtPassword1.Text = dt.Rows[0]["Password"].ToString();
                txtPassword1.Attributes["value"] = txtPassword1.Text;
                txtPassword1.TextMode = TextBoxMode.Password;
                ViewState["ID"] = dt.Rows[0]["EmailLogin_AutoID"].ToString();
                ViewState["Password"] = dt.Rows[0]["Password"].ToString();
            }
            else
            {
                btnSave.Text = "Save";
            }
        }

      }
}