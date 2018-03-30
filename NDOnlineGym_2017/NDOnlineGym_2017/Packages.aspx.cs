using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace NDOnlineGym_2017
{
    public partial class Packages : System.Web.UI.Page
    {
        BalPackage pack = new BalPackage();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPackageName.Focus();
                txtDisc.Text = "0";
                txtAmount.Text = "0";
                BindGridview();
                //if (Request.QueryString["MenuPackageDetails"] != null)
                //{
                //    divsearch.Visible = true;
                //    divFormDetails.Visible = false;
                //}
                //else
                //{
                //    divsearch.Visible = false;
                //    divFormDetails.Visible = true;
                //}
            }
        }
        public void save()
        {
            pack.Package = txtPackageName.Text.Trim();
            pack.Duration = txtDuration.Text.Trim();
            pack.Session = Convert.ToInt32(txtSession.Text);
            pack.Amount = Convert.ToDouble(txtAmount.Text);
            pack.Discount = Convert.ToDouble(txtDisc.Text);
            pack.Status = ddlStatus.SelectedValue.ToString();
            pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            pack.Type = ddtype.SelectedValue.ToString();
            if (txtTimeFrom.Text == "")
            {

            }
            else
            {
               pack.FromTime = Convert.ToDateTime(txtTimeFrom.Text.ToString());
                //pack.FromTime = txtTimeFrom.Text.ToString();
            
            if (txtTimeTo.Text == "")
            {
            }
            else
            {
                pack.ToTime = Convert.ToDateTime(txtTimeTo.Text.ToString());
               // pack.ToTime = txtTimeTo.Text.ToString();
            }

            if (Convert.ToInt32(ViewState["flag"]) != 1)
            {
                pack.Action = "Insert";
            }
            else
            {
                pack.Action = "Update";
                pack.Pack_AutoID = Convert.ToInt32(ViewState["pack_id"]);
                ViewState["flag"] = 0;
            }
        }
            }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPackageName.Text == "")
            {
                txtPackageName.Style.Add("border", "1px solid red ");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Package Name !!!','Error');", true);
            }
            else if (txtDuration.Text == "")
            {
                txtDuration.Style.Add("border", "1px solid red ");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Duration!!!','Error');", true);
            }
            else if (txtSession.Text == "")
            {
                txtSession.Style.Add("border", "1px solid red ");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Session !!!','Error');", true);
            }
            else if (txtAmount.Text == "")
            {
                txtAmount.Style.Add("border", "1px solid red ");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Amount !!!','Error');", true);
            }
            else if (ddlStatus.SelectedValue == "--Select--")
            {
                ddlStatus.Style.Add("border", "1px solid red ");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Status !!!','Error');", true);
            }
            else if (ddtype.SelectedValue == "--Select--")
            {
                ddlStatus.Style.Add("border", "1px solid red ");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Type !!!','Error');", true);
            }
            else
            {
                if (btnSave.Text == "Save")
                {
                    save();
                    pack.Action = "Insert";
                    int res = pack.Insert_Update();
                    if (res > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        Clear();
                        BindGridview();
                    }
                }
                else
                {
                    save();
                    pack.Action = "Update";
                    pack.Pack_AutoID = Convert.ToInt32(ViewState["pack_id"]);
                    int res = pack.Insert_Update();
                    if (res > 0)
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully!!!','Success');", true);
                        Clear();
                        btnSave.Text = "Save";
                        BindGridview();
                        //divsearch.Visible = true;
                        //divFormDetails.Visible = false;
                    }
                }

            }
        }
        public int pack_id;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            pack_id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["pack_id"] = Convert.ToInt32(e.CommandArgument.ToString());
            GetDataForEdit(pack_id);

        }
        int Flag = 0;
        public void GetDataForEdit(int pack_id)
        {
            btnSave.Text = "Update";
            pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]); 
            pack.Pack_AutoID = Convert.ToInt32(ViewState["pack_id"]);
            dt.Clear();
            dt = pack.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                txtPackageName.Text = dt.Rows[0]["Package"].ToString();
                txtDuration.Text = dt.Rows[0]["Duration"].ToString();
                txtSession.Text = dt.Rows[0]["Session"].ToString();
                txtTimeFrom.Text = dt.Rows[0]["FromTime"].ToString();
                txtTimeTo.Text = dt.Rows[0]["ToTime"].ToString();
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtDisc.Text = dt.Rows[0]["Discount"].ToString();
                ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                ddtype.SelectedValue = dt.Rows[0]["Type"].ToString();
            }
            ViewState["flag"] = 1;
            //divsearch.Visible = false;
            //divFormDetails.Visible = true;
        }

        public void Clear()
        {
            txtPackageName.Text = "";
            txtDuration.Text = "";
            txtSession.Text = "";
            txtAmount.Text = txtDisc.Text = "0";
          //  txtTimeFrom.Text = txtTimeTo.Text = TxtType.Text = "";
            ddtype.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtPackageName.Focus();
            lblCount.Text = "0";
            gvPackage.DataSource = null;
            gvPackage.DataBind();
            ddlSearch.SelectedValue = "--Select--";
            txtSearch.Text = "";
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                pack.Pack_AutoID = Convert.ToInt32(e.CommandArgument);
                pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]); 
                int ex = pack.ChkPack_ID();
                if (ex > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!!.','Error');", true);
                }
                else
                {
                    int i = pack.Delete();
                    if (i > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully!!!','Success');", true);
                        Clear();
                        BindGridview();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Deleted!!!.','Error');", true);

            }
        }
        public void BindGridview()
        {
            try
            {
                if (ddlSearch.SelectedValue.ToString() == "--Select--")
                {
                    pack.Category = "Select";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Package")
                {
                    pack.Category = "Package";
                    pack.searchTxt = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "Duration")
                {
                    pack.Category = "Duration";
                    pack.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Active")
                {
                    pack.Category = "Active";
                    pack.searchTxt = "Active";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Deactive")
                {
                    pack.Category = "Deactive";
                    pack.searchTxt = "Deactive";
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category!!!.','Error');", true);
                }
                pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                dt.Clear();
                dt = pack.Get_Search();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    gvPackage.DataSource = dt;
                    gvPackage.DataBind();
                    ViewState["dt"] = dt;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvPackage.Columns[0].Visible = false;
                    gvPackage.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true); ;
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {          
            BindGridview();
            gvPackage.Focus();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            txtPackageName.Focus();
        }

        protected void gvPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPackage.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        protected void txtSession_TextChanged(object sender, EventArgs e)
        {
            //if (txtDuration.Text != "")
            //{
            //    if (Convert.ToInt32(txtSession.Text) > Convert.ToInt32(txtDuration.Text))
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Session not more than Duration!!!.','Error');", true);
            //        txtSession.Text = "0";
            //        txtSession.Focus();
            //        txtSession.TabIndex = 3;
            //    }
            //    else
            //    {
            //        txtSession.Focus();
            //        txtSession.TabIndex = 3;
            //    }
            //}
        }
        protected void txtDisc_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtDisc.Text) > Convert.ToInt32(txtAmount.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Discount less than Amount !!!.','Error');", true);
                txtDisc.Text = "0";
                txtDisc.Focus();
                txtDisc.TabIndex = 7;
            }
            else
            {
                ddlStatus.Focus();
                ddlStatus.TabIndex = 8;
            }
            //if (txtDisc.Text == "")
            //{
            //    txtDisc.Text = "0";
            //    txtDisc.Focus();
            //    txtDisc.TabIndex = 7;
            //}
        }

        protected void BtnClear_11_Click(object sender, EventArgs e)
        {
            Clear();
            ddlStatus.Focus();
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            if (ddlSearch.SelectedValue.ToString() == "--Select--")
            {
                //txtSearch.Style.Add("border", "1px solid silver ");
                txtSearch.Enabled = false;

            }
            else if (ddlSearch.SelectedValue.ToString() == "Active")
            {
                txtSearch.Enabled = false;
                BindGridview();
            }
            else if (ddlSearch.SelectedValue.ToString() == "Deactive")
            {
                txtSearch.Enabled = false;
                BindGridview();
            }

            else if (ddlSearch.SelectedValue.ToString() == "Package")
            {
                txtSearch.Enabled = true;
                //BindGridview();
            }

            else if (ddlSearch.SelectedValue.ToString() == "Duration")
            {
                txtSearch.Enabled = true;
               // BindGridview();
            }
            ddlSearch.TabIndex = 12;
            ddlSearch.Focus();
        }

        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel1();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void ExportToExcel1()
        {
            try
            {
                if (ViewState["dt"] != null)
                {
                    DataTable dt2 = (DataTable)ViewState["dt"];
                    if (dt2.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=PackageDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                             gvPackage.Columns[0].Visible = false;
                             gvPackage.Columns[1].Visible = false;
                             gvPackage.AllowPaging = false;

                             gvPackage.DataSource = dt2;
                             gvPackage.DataBind();
                             gvPackage.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in  gvPackage.HeaderRow.Cells)
                            {
                                cell.BackColor =  gvPackage.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in  gvPackage.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                    List<Control> controls = new List<Control>();
                                    //Add controls to be removed to Generic List
                                    foreach (Control control in cell.Controls)
                                    {
                                        controls.Add(control);
                                    }

                                    foreach (Control control in controls)
                                    {
                                        switch (control.GetType().Name)
                                        {
                                            case "HyperLink":
                                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                                break;

                                            case "LinkButton":
                                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                                break;

                                        }
                                        cell.Controls.Remove(control);

                                    }
                                }
                            }


                             gvPackage.GridLines = GridLines.Both;
                             gvPackage.RenderControl(hw);

                            //style to format numbers to string

                            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Can Not Export !!!.','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Can Not Export !!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        //protected void txtAmount_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtAmount.Text != "")
        //    {
        //        txtDisc.Text = txtAmount.Text;
        //        txtAmount.Focus();
        //        txtAmount.TabIndex = 6;
        //    }
        //    else
        //    {
        //        txtAmount.Text = "0";
        //        txtDisc.Text = "0";
        //        txtAmount.Focus();
        //        txtAmount.TabIndex = 6;
        //    }

        //}
    }
}