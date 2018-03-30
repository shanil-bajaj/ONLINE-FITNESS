using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class ReportMemberBirthday : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalReportMemberBirthday objBalReportMemberBirthday = new BalReportMemberBirthday();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignDate();
                Gridview();
            }
        }

        public void AssignDate()
        {
            try
            {
                try
                {
                    DateTime date;
                    if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    { }

                    DateTime dtFirst = Convert.ToDateTime(date);
                    DateTime dtLast;

                    //Setting Start Date Month
                    dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
                    txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

                    //Setting Last Date of Month
                    dtLast = dtFirst.AddMonths(1).AddDays(-1);
                    txtToDate.Text = dtLast.ToString("dd-MM-yyyy");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    ErrorHandiling.SendErrorToText(ex);

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
            Gridview();
            gvReportMemberBirthday.PageIndex = 1;
            ddlMonth.SelectedValue = "--Select--" ;
        }

        private void AssignID()
        {
            objBalReportMemberBirthday.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objBalReportMemberBirthday.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalReportMemberBirthday.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        public void Gridview()
        {
            //DateTime Fromdate;
            //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            //{ }
            //DateTime Todate;
            //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            //{ }


                DateTime StartDate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
                {
                    string StartDate1 = StartDate.ToString("dd-MM-yyyy");
                    objBalReportMemberBirthday.StartDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }

                DateTime EndDate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
                {
                    string EndDate1 = EndDate.ToString("dd-MM-yyyy");
                    objBalReportMemberBirthday.EndDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
            AssignID();

           
            //objBalReportMemberBirthday.StartDate = Fromdate;
            //objBalReportMemberBirthday.EndDate = Todate;

            
            ViewState["Data"] = objBalReportMemberBirthday.GetDetails_MemberBirthdayReport();
            dt = (DataTable)ViewState["Data"];
            lblCount.Text = "0";
            if (objBalReportMemberBirthday.StartDate > objBalReportMemberBirthday.EndDate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                gvReportMemberBirthday.DataSource = null;
                gvReportMemberBirthday.DataBind();
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();

                    gvReportMemberBirthday.DataSource = dt;
                    gvReportMemberBirthday.DataBind();
                }
                else
                {
                    gvReportMemberBirthday.DataSource = dt;
                    gvReportMemberBirthday.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }
            }

        }

        protected void gvReportMemberBirthday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReportMemberBirthday.PageIndex = e.NewPageIndex;
            Gridview();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string month;
            month = ddlMonth.SelectedValue;
            switch (month)
            {
                case "January": objBalReportMemberBirthday.BirthMonth = 1;
                                break;
                case "February": objBalReportMemberBirthday.BirthMonth = 2;
                                break;
                case "March": objBalReportMemberBirthday.BirthMonth = 3;
                                break;
                case "April": objBalReportMemberBirthday.BirthMonth = 4;
                                break;
                case "May": objBalReportMemberBirthday.BirthMonth = 5;
                                break;
                case "June": objBalReportMemberBirthday.BirthMonth = 6;
                                break;
                case "July": objBalReportMemberBirthday.BirthMonth = 7;
                                break;
                case "August": objBalReportMemberBirthday.BirthMonth = 8;
                                break;
                case "September": objBalReportMemberBirthday.BirthMonth = 9;
                                break;
                case "October": objBalReportMemberBirthday.BirthMonth = 10;
                                break;
                case "November": objBalReportMemberBirthday.BirthMonth = 11;
                                break;
                case "December": objBalReportMemberBirthday.BirthMonth = 12;
                                break;
            }

            AssignID();

            ViewState["Data"] = objBalReportMemberBirthday.GetDetailsOfMonth_MemberBirthdayReport();
            dt = (DataTable)ViewState["Data"];


            if (dt.Rows.Count > 0)
            {
                gvReportMemberBirthday.DataSource = dt;
                gvReportMemberBirthday.DataBind();
            }
            else
            {
                gvReportMemberBirthday.DataSource = dt;
                gvReportMemberBirthday.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
            }
            gvReportMemberBirthday.PageIndex = 1;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    DataTable dt2 = (DataTable)ViewState["Data"];
                    if (dt2.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MemberBirthdayReportData" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvReportMemberBirthday.AllowPaging = false;

                           // gvReportMemberBirthday.AllowPaging = false;
                            gvReportMemberBirthday.DataSource = dt2;
                            gvReportMemberBirthday.DataBind();
                            gvReportMemberBirthday.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvReportMemberBirthday.HeaderRow.Cells)
                            {
                                cell.BackColor = gvReportMemberBirthday.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvReportMemberBirthday.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            gvReportMemberBirthday.GridLines = GridLines.Both;
                            gvReportMemberBirthday.RenderControl(hw);

                            //style to format numbers to string

                            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            //Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('DataGridView Is Empty, Can Not Export !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('DataGridView Is Empty, Can Not Export !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            AssignDate();
            ddlMonth.SelectedValue = "--Select--";
            gvReportMemberBirthday.DataSource = null;
            gvReportMemberBirthday.DataBind();
            ViewState["Data"] = null;
        }
    }
}