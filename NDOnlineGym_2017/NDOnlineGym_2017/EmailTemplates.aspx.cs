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
    public partial class EmailTemplates : System.Web.UI.Page
    {
        string head, foot;
        static string Type = "";
        BalEmail objEmail = new BalEmail();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmailHeader.Focus();
                Bind_Data();
            }
        }


        void Bind_Data()
        {
            objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            DataSet data = objEmail.All_Data();
            try{
            if (data.Tables[0].Rows.Count > 0)
            {
                txtEmailFooter.Enabled = false;
                txtEmailHeader.Enabled = false;
                btnEmailHeader.Text = "Edit";
                btnEmailFooter.Text = "Edit";
                objEmail.EmailContent_AutoID = Convert.ToInt16(data.Tables[0].Rows[0]["EmailContent_AutoID"].ToString());
                string Type = data.Tables[0].Rows[0]["EmailType"].ToString();
                ViewState["Data"] = Type;
                if (Type == "Header")
                {
                    txtEmailHeader.Text = data.Tables[0].Rows[0]["EmailContent"].ToString();
                    txtEmailFooter.Text = data.Tables[0].Rows[1]["EmailContent"].ToString();
                }
                else
                {
                    txtEmailFooter.Text = data.Tables[0].Rows[0]["EmailContent"].ToString();
                    txtEmailHeader.Text = data.Tables[0].Rows[1]["EmailContent"].ToString();
                }

            }
            }
             catch (Exception ex)
            {    
               {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
             }

        }
        void Insert_HeaderData()
        {
            try
            {
                // DataTable dt = new DataTable();
                // objMaster.action="Master_Insert";
                if (btnEmailHeader.Text == "Save")
                {
                    objEmail.Header = txtEmailHeader.Text.Trim();
                    objEmail.EmailType = "Header";
                    objEmail.category = "Insert_H_Content";
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    int nw = objEmail.Insert_Header();
                    if (nw > 0)
                    {

                        txtEmailHeader.Enabled = false;
                        btnEmailHeader.Text = "Edit";
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Header Saved Successfully !!!','Success');", true);
                        txtEmailFooter.Focus();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Not Saved!!!','Error');", true);
                    }
                }
                else if (txtEmailHeader.Enabled == true)
                {
                    head = txtEmailHeader.Text.Trim();
                    Type = "Header";
                    Edit_Data();
                }
                //else if (txtEmailFooter.Enabled == true)
                //{
                //    foot = txtEmailFooter.Text.Trim();
                //    Type="Footer";
                //    Edit_Data();
                //}

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        void Edit_Data()
        {
            try
            {            
                                

                if (Type.ToString() == "Header")
                {
                    objEmail.category = "Update_H_Content";
                    objEmail.Header = head;
                    objEmail.EmailType = Type.ToString();
                    //objMaster.Type = "Footer";
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    int chk = objEmail.Insert_Header();
                    if (chk > 0)
                    {
                        txtEmailHeader.Enabled = false;
                        btnEmailHeader.Text = "Edit";
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Header Updated Successfully !!!','Success');", true);
                        btnEmailFooter.Focus();
                       
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Not Update','Error');", true);
                    }
                }
                else
                {
                    objEmail.category = "Update_F_Content";
                    objEmail.EmailType = Type.ToString();
                    objEmail.Footer = foot;
                    //objMaster.Type = "Footer";
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    int chk = objEmail.Insert_Footer();
                    if (chk > 0)
                    {
                        txtEmailFooter.Enabled = false;
                        btnEmailFooter.Text = "Edit";
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Footer Updated Successfully !!!','Success');", true);
                       
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Not Update','Error');", true);                      
                    }
                }
            }

            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
              
            }


        }

        void Insert_FooterData()
        {
            try
            {
                //  DataTable dt = new DataTable();
                objEmail.action = "Master_Insert";
                if (btnEmailFooter.Text == "Save")
                {
                    objEmail.Footer = txtEmailFooter.Text.Trim();
                    objEmail.EmailType = "Footer";
                    objEmail.category = "Insert_F_Content";
                    objEmail.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEmail.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    int chk = objEmail.Insert_Footer();
                    if (chk > 0)
                    {
                        txtEmailFooter.Enabled = false;
                        btnEmailFooter.Text = "Edit";
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Footer Saved Successfully !!!','Success');", true);
                       
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Not Save','Error');", true);   
                    }


                }
                else if (txtEmailFooter.Enabled == true)
                {
                    foot = txtEmailFooter.Text.Trim();
                    Type = "Footer";
                    Edit_Data();
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        protected void btnEmailHeader_Click(object sender, EventArgs e)
        {
            if (btnEmailHeader.Text == "Edit")
            {
                
                txtEmailHeader.Focus();
                txtEmailHeader.Enabled = true;
                btnEmailHeader.Text = "Update";

            }
            else
            {
                if (txtEmailHeader.Text == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Please Write Something In Header Box','Error');", true); 
                }
                else
                Insert_HeaderData();

            }
        }

        protected void btnEmailFooter_Click(object sender, EventArgs e)
        {
            if (btnEmailFooter.Text == "Edit")
            {
              
                txtEmailFooter.Focus();
                txtEmailFooter.Enabled = true;
                btnEmailFooter.Text = "Update";
            }
            else
            {
                if (txtEmailFooter.Text == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Please Write Something In Footer Box','Error');", true);
                }
                else
                Insert_FooterData();
            }
        }
    }
}