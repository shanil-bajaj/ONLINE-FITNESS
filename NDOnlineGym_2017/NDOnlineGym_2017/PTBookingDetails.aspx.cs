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
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace NDOnlineGym_2017
{
    public partial class PTBookingDetails : System.Web.UI.Page
    {

        BalLoginForm ObjLogin = new BalLoginForm();
        BalPackage pack = new BalPackage();
        BalPTSearch cour = new BalPTSearch();
        BalAddMember objMemberDetails = new BalAddMember();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        DataTable dt = new DataTable();
        BalExpense ObjExpense = new BalExpense();
        BalBalancePayment objBalance = new BalBalancePayment();
        BalPT pt = new BalPT();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                AssignTodaysDate1();
            }
        }
        protected void AssignTodaysDate1()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDate.Text = todaydate.ToString("dd-MM-yyyy");
                txtToDate.Text = todaydate.ToString("dd-MM-yyyy");
                //txtDOB.Text = todaydate.ToString("dd-MM-yyyy");
                //txtAnniversary.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        private void SeacrhAction()
        {
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                TextBox1.Enabled = false;
                cour.Category = "--Select--";
            }
            else if (DropDownList3.SelectedValue.ToString() == "Member ID")
            {
                cour.Category = "Member ID";
                cour.searchTxt = TextBox1.Text;

            }
            else if (DropDownList3.SelectedValue.ToString() == "Receipt ID")
            {
                cour.Category = "Receipt ID";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "First Name")
            {
                cour.Category = "First Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Last Name")
            {
                cour.Category = "Last Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "ContactNo")
            {
                cour.Category = "ContactNo";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Course Name")
            {
                cour.Category = "Course Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Active")
            {
                cour.Category = "Active";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Deactive")
            {
                cour.Category = "Deactive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "New")
            {
                cour.Category = "New";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Renew")
            {
                cour.Category = "Renew";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Upgrade")
            {
                cour.Category = "Upgrade";
                cour.searchTxt = TextBox1.Text;
            }

            else if (DropDownList3.SelectedValue.ToString() == "Executive")
            {
                cour.Category = "Executive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "PaymentMode")
            {
                cour.Category = "PaymentMode";
                cour.searchTxt = TextBox1.Text;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                DropDownList3.Focus();
                return;
            }
        }

        public void BindSearch_gridview()
        {
            try
            {
                dt.Clear();
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                SeacrhAction();
                dt = cour.BindGV();
                if (dt.Rows.Count > 0)
                {
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    GVCourseDetails.Columns[0].Visible = false;
                    GVCourseDetails.Columns[1].Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        protected void btnclear_Click(object sender, EventArgs e)
        {
            AssignTodaysDate1();
            TextBox1.Text = "";
            DropDownList3.SelectedValue = "--Select--";
            //DropDownList3.SelectedItem.Text = "--Select--";
            GVCourseDetails.DataSource = null;
            GVCourseDetails.DataBind();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindSearch_gridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        BalEnquiry eng = new BalEnquiry();
        string gender = "";
        string s = "";
        private void AssignID()
        {
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
        }
        static int flag2 = 0;
        public void SearchByDate()
        {
            try
            {
                dt.Clear();
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //SeacrhAction();
                cour.Action = "SearchByDate";

                DateTime payDate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate))
                {
                    string payDate1 = payDate.ToString("dd-MM-yyyy");
                    cour.payDate = DateTime.ParseExact(payDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                DateTime payDate2;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate2))
                {
                    string payDate3 = payDate2.ToString("dd-MM-yyyy");
                    cour.payDate1 = DateTime.ParseExact(payDate3, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }

                dt = cour.SearchByDate();
                if (dt.Rows.Count > 0)
                {
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    GVCourseDetails.Columns[0].Visible = false;
                    GVCourseDetails.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string StartDate1, EndDate1;
            DateTime StartDate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
            {
                StartDate1 = StartDate.ToString("dd-MM-yyyy");
                cour.StartDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            DateTime EndDate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
            {
                EndDate1 = EndDate.ToString("dd-MM-yyyy");
                cour.EndDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }
            AssignID();


            if (cour.StartDate > cour.EndDate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                AssignTodaysDate1();
            }
            else
            {
                try
                {
                    SearchByDate();
                    // BindSearch_gridview();
                    flag2 = 2;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    ErrorHandiling.SendErrorToText(ex);
                }
            }
        }
        private void SeacrhAction1()
        {
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                TextBox1.Enabled = false;
                cour.Category = "--Select--";
            }
            else if (DropDownList3.SelectedValue.ToString() == "Member ID")
            {
                cour.Category = "Member ID";
                cour.searchTxt = TextBox1.Text;

            }
            else if (DropDownList3.SelectedValue.ToString() == "Receipt ID")
            {
                cour.Category = "Receipt ID";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "First Name")
            {
                cour.Category = "First Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Last Name")
            {
                cour.Category = "Last Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "ContactNo")
            {
                cour.Category = "ContactNo";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Course Name")
            {
                cour.Category = "Course Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Active")
            {
                cour.Category = "Active";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Deactive")
            {
                cour.Category = "Deactive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "New")
            {
                cour.Category = "New";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Renew")
            {
                cour.Category = "Renew";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Upgrade")
            {
                cour.Category = "Upgrade";
                cour.searchTxt = TextBox1.Text;
            }

            else if (DropDownList3.SelectedValue.ToString() == "Executive")
            {
                cour.Category = "Executive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "PaymentMode")
            {
                cour.Category = "PaymentMode";
                cour.searchTxt = TextBox1.Text;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                DropDownList3.Focus();
                return;
            }

        }
        public void SearchBYCategory()
        {
            try
            {
                dt.Clear();
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                DateTime payDate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate))
                {
                    string payDate4 = payDate.ToString("dd-MM-yyyy");
                    cour.payDate = DateTime.ParseExact(payDate4, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }

                DateTime payDate2;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate2))
                {
                    string payDate3 = payDate2.ToString("dd-MM-yyyy");
                    cour.payDate1 = DateTime.ParseExact(payDate3, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }


                SeacrhAction1();
                dt = cour.SearchByDateWithCategory();
                if (dt.Rows.Count > 0)
                {
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    GVCourseDetails.Columns[0].Visible = false;
                    GVCourseDetails.Columns[1].Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        protected void BtnsearchWithDate_Click(object sender, EventArgs e)
        {
            string StartDate1, EndDate1;
            DateTime StartDate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
            {
                StartDate1 = StartDate.ToString("dd-MM-yyyy");
                cour.StartDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            DateTime EndDate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
            {
                EndDate1 = EndDate.ToString("dd-MM-yyyy");
                cour.EndDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }
            AssignID();


            if (cour.StartDate > cour.EndDate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                AssignTodaysDate1();
            }
            else
            {
                if (DropDownList3.SelectedValue == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                }
                if (TextBox1.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                }
                else if (DropDownList3.SelectedValue != "--Select--" && TextBox1.Text != "")
                {
                    try
                    {
                        SearchBYCategory();
                        flag2 = 3;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                        ErrorHandiling.SendErrorToText(ex);
                    }
                }
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                //txtSearch.Style.Add("border", "1px solid silver ");
                TextBox1.Enabled = false;

            }
            else if (DropDownList3.SelectedValue.ToString() == "Active")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }
            else if (DropDownList3.SelectedValue.ToString() == "Deactive")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }

            else if (DropDownList3.SelectedValue.ToString() == "New")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }

            else if (DropDownList3.SelectedValue.ToString() == "Renew")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }

            else if (DropDownList3.SelectedValue.ToString() == "Upgrade")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }
            else
            {
                TextBox1.Enabled = true;
            }
            DropDownList3.Focus();
        }

        protected void btnpreview_Command(object sender, CommandEventArgs e)
        {
            int Receipt_No = Convert.ToInt32(e.CommandArgument);
            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('Receipt.aspx?PT=" + Receipt_No
            + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
            + "</script>";
            ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            int Receipt_No = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("PTAddTranning.aspx?EditReceipt=" +Receipt_No); 
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

                        Response.AddHeader("content-disposition", "attachment;filename=PTDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GVCourseDetails.Columns[0].Visible = false;
                            GVCourseDetails.Columns[1].Visible = false;
                            GVCourseDetails.Columns[2].Visible = false;
                            GVCourseDetails.Columns[3].Visible = false;
                            GVCourseDetails.AllowPaging = false;

                            GVCourseDetails.DataSource = dt2;
                            GVCourseDetails.DataBind();
                            GVCourseDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GVCourseDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = GVCourseDetails.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GVCourseDetails.Rows)
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


                            GVCourseDetails.GridLines = GridLines.Both;
                            GVCourseDetails.RenderControl(hw);

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
        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel1();
        }
    }
}