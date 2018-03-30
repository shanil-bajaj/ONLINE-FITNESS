using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessAccessLayer;
using DataAccessLayer;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class ReportEnquiryToEnroll : System.Web.UI.Page
    {
        BalEnquiryToEnroll objEnqToEnroll = new BalEnquiryToEnroll();
        DataTable dt = new DataTable();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Focus();
                    AssignTodayDate();
                    SearchByDate();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void AssignTodayDate()
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

        public void AssignID()
        {
            objEnqToEnroll.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEnqToEnroll.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        public void SearchByDate()
        {
            //ViewState["ViewActiveDeactive"] = null;
            GvEnquiryToeEnrollReport.Visible = true;
            objEnqToEnroll.FromDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
            objEnqToEnroll.ToDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
            AssignID();

            if (objEnqToEnroll.FromDate > objEnqToEnroll.ToDate)
            {
                objEnqToEnroll.Action = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('From Date Should Not be Greater than To Date..!!!','Error');", true);
                //return;
            }
            else
            {
                objEnqToEnroll.Action = "Select_By_Date";
            }
            dt = objEnqToEnroll.Bind_GV();
            ViewState["Data"] = dt;
            // ViewState["ViewActiveDeactive"] = dt;

            BindGridview();
            //flag = 1;
        }

        public void BindGridview()
        {
            lblCount.Text = "0";
            try
            {
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();

                    GvEnquiryToeEnrollReport.DataSource = dt;
                    GvEnquiryToeEnrollReport.DataBind();
                    GvEnquiryToeEnrollReport.Style["width"] = "100%";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
                    GvEnquiryToeEnrollReport.DataSource = dt;
                    GvEnquiryToeEnrollReport.DataBind();
                    GvEnquiryToeEnrollReport.Style["width"] = "100%";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            GvEnquiryToeEnrollReport.Visible = false;
            AssignTodayDate();
            ViewState["Data"] = null;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchByDate();
        }

        protected void GvEnquiryToeEnrollReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvEnquiryToeEnrollReport.PageIndex = e.NewPageIndex;
            BindGridview();
        }

        public void ExportToExcel()
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

                        Response.AddHeader("content-disposition", "attachment;filename=AllStaffListExport" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GvEnquiryToeEnrollReport.AllowPaging = false;

                            GvEnquiryToeEnrollReport.DataSource = dt2;
                            GvEnquiryToeEnrollReport.DataBind();
                            GvEnquiryToeEnrollReport.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GvEnquiryToeEnrollReport.HeaderRow.Cells)
                            {
                                cell.BackColor = GvEnquiryToeEnrollReport.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GvEnquiryToeEnrollReport.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            GvEnquiryToeEnrollReport.GridLines = GridLines.Both;
                            GvEnquiryToeEnrollReport.RenderControl(hw);

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
            /* Verifies that the control is rendered */
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}