using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using System.Data;

namespace NDOnlineGym_2017
{
    public partial class PTExistingAttendance : System.Web.UI.Page
    {

        BalPTSearch cour = new BalPTSearch();
        BalPT pt = new BalPT();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodaysDate1();
                BindGrid_BYDate();
            }
        }
        protected void AssignTodaysDate1()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDate.Text = todaydate.ToString("dd-MM-yyyy");
                txtToDate.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        private void SeacrhAction1()
        {
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
            }
            else if (DropDownList3.SelectedValue.ToString() == "MemberID")
            {
                cour.Category = "MemberID";
                cour.searchTxt = TextBox1.Text;

            }
            else if (DropDownList3.SelectedValue.ToString() == "MemberName")
            {
                cour.Category = "MemberName";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "StaffID")
            {
                cour.Category = "StaffID";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "SatffName")
            {
                cour.Category = "SatffName";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "AlterInstructorName")
            {
                cour.Category = "AlterInstructorName";
                cour.searchTxt = TextBox1.Text;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                DropDownList3.Focus();
                return;
            }

        }
        private void SeacrhAction()
        {
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
            }
            else if (DropDownList3.SelectedValue.ToString() == "MemberID")
            {
                cour.Category = "MemberID";
                cour.searchTxt = TextBox1.Text;

            }
            else if (DropDownList3.SelectedValue.ToString() == "MemberName")
            {
                cour.Category = "MemberName";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "StaffID")
            {
                cour.Category = "StaffID";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "SatffName")
            {
                cour.Category = "SatffName";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "AlterInstructorName")
            {
                cour.Category = "AlterInstructorName";
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
                dt = cour.BindGV_Atten();
                if (dt.Rows.Count > 0)
                {
                    GVAttendance.DataSource = dt;
                    GVAttendance.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    GVAttendance.DataSource = dt;
                    GVAttendance.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }
                //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = false;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                //{
                //    GVAttendance.Columns[0].Visible = false;
                //    GVAttendance.Columns[1].Visible = false;
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
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
        private void AssignID()
        {
            cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
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
                cour.Action = "SearchByDate_Attendance";

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

                dt = cour.SearchByDate_Atten();
                if (dt.Rows.Count > 0)
                {
                    GVAttendance.DataSource = dt;
                    GVAttendance.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    GVAttendance.DataSource = dt;
                    GVAttendance.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }

                //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = false;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                //{
                //    GVAttendance.Columns[0].Visible = false;
                //    GVAttendance.Columns[1].Visible = false;
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void BindGrid_BYDate()
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


            //if (cour.StartDate > cour.EndDate)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
            //    AssignTodaysDate1();
            //}
            //else
            //{
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
           // }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid_BYDate(); 
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
                dt = cour.SearchByDateWithCategory_Atten();
                if (dt.Rows.Count > 0)
                {
                    GVAttendance.DataSource = dt;
                    GVAttendance.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    GVAttendance.DataSource = dt;
                    GVAttendance.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }
                //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = true;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                //{
                //    GVAttendance.Columns[0].Visible = true;
                //    GVAttendance.Columns[1].Visible = false;
                //}
                //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                //{
                //    GVAttendance.Columns[0].Visible = false;
                //    GVAttendance.Columns[1].Visible = false;
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        protected void btnSearchByDateWithCategory_Click(object sender, EventArgs e)
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


            //if (cour.StartDate > cour.EndDate)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
            //    AssignTodaysDate1();
            //}
            //else
            //{
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
            //}
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            AssignTodaysDate1();
            TextBox1.Text = "";
            DropDownList3.SelectedValue = "--Select--";
            //DropDownList3.SelectedItem.Text = "--Select--";
            GVAttendance.DataSource = null;
            GVAttendance.DataBind();

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

                        Response.AddHeader("content-disposition", "attachment;filename=PTAttenDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GVAttendance.Columns[0].Visible = false;                        
                            GVAttendance.AllowPaging = false;

                            GVAttendance.DataSource = dt2;
                            GVAttendance.DataBind();
                            GVAttendance.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GVAttendance.HeaderRow.Cells)
                            {
                                cell.BackColor = GVAttendance.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GVAttendance.Rows)
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


                            GVAttendance.GridLines = GridLines.Both;
                            GVAttendance.RenderControl(hw);

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

        protected void btnExportToExcel_Click(object sender, EventArgs e)
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
        //static int flag2 = 0;
        protected void GVAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {      
            GVAttendance.PageIndex = 0;
            if (flag2 == 2)
            {
                GVAttendance.PageIndex = e.NewPageIndex;
                SearchByDate();

            }
            else if (flag2 == 3)
            {
                GVAttendance.PageIndex = e.NewPageIndex;
                SearchBYCategory();
            }
            else
            {
                GVAttendance.PageIndex = e.NewPageIndex;
                BindSearch_gridview();
            }
        }
    }

}