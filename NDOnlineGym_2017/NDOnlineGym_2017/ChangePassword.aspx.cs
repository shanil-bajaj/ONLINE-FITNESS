using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace NDOnlineGym_2017
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        //BalUserResitration objUser = new BalUserResitration();
        BalLoginForm ObjLogin = new BalLoginForm();
        BalUserResitration objUser = new BalUserResitration();

        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            bindusername();
            txtOldPassword.Focus();
        }

        public void bindusername()
        {
            objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objUser.LogAutoId = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            objUser.authority = Request.Cookies["OnlineGym"]["Authority"].ToString();
            dt = objUser.Select_All();
            if (dt.Rows.Count > 0)
            {
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ObjLogin.Username = txtUsername.Text;
                ObjLogin.Password = txtOldPassword.Text;
                ObjLogin.NewPassword = txtNewPassword.Text;
                //objUser.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_AutoID"]);

                bool chk_User = ObjLogin.chk_UserLogin();
                if (chk_User == true)
                {
                    //ObjLogin.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
                    ObjLogin.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    ObjLogin.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                    dt = ObjLogin.Bind_UserLogin();
                    if (dt.Rows.Count > 0)
                        ObjLogin.Login_AutoID = Convert.ToInt32(dt.Rows[0]["Login_AutoID"]);

                    bool chk_Update = ObjLogin.ChangePassword();
                    if (chk_Update == true)
                    {
                        if (txtNewPassword.Text == txtConfirmPassword.Text)
                        {
                           
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Password Updated Successfully','Success');", true);
                            ClearRecord();
                        }
                        else
                        {
                            txtConfirmPassword.Focus();
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Re-Enter Password Here','Error');", true);
                            ClearRecord();
                            return;

                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Invalid Password','Error');", true);
                    ClearRecord();
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void ClearRecord()
        {
            //txtUsername.Text = "";
            txtOldPassword.Text="";
            txtNewPassword.Text="";
            txtConfirmPassword.Text = "";
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ClearRecord();
        }

        protected void txtOldPassword_TextChanged(object sender, EventArgs e)
        {

            ObjLogin.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_AutoID"]);
            ObjLogin.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            ObjLogin.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            ObjLogin.Username = txtUsername.Text;
            ObjLogin.Password = txtOldPassword.Text;

           // ObjLogin.Action = "CHECK";

            bool chk_User = ObjLogin.chk_UserLogin();
            if (chk_User == true)
            {
                //ObjLogin.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
                //ObjLogin.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"]);
                //dt = ObjLogin.Bind_UserLogin();
                //if (dt.Rows.Count > 0)
                //    ObjLogin.Login_AutoID = Convert.ToInt32(dt.Rows[0]["Login_AutoID"]);
                txtNewPassword.Focus();
            }
            else
            {
                txtOldPassword.Focus();
                txtOldPassword.Text = "";
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Invalid Password','Error');", true);

                //ClearRecord();
            }

        }


    }
}