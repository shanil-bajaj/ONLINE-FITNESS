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
    public partial class ReportActiveDeactive : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalReportActiveDeactiveMember objBalReportActiveDeactiveMember = new BalReportActiveDeactiveMember();
        int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Gridview();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "--Select--";
            gvActiveDeactive.DataSource = null;
            gvActiveDeactive.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Gridview();
            gvActiveDeactive.PageIndex = 1;
            
        }

        private void AssignID()
        {
            objBalReportActiveDeactiveMember.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalReportActiveDeactiveMember.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalReportActiveDeactiveMember.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        public void Gridview()
        {
            AssignID();
            objBalReportActiveDeactiveMember.Status = ddlStatus.SelectedValue;
            ViewState["Data"] = objBalReportActiveDeactiveMember.GetDetails_ActiveDeactiveMemberReport();
            dt = (DataTable)ViewState["Data"];

            lblCount.Text = "0";
            if (dt.Rows.Count > 0)
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvActiveDeactive.DataSource = dt;
                gvActiveDeactive.DataBind();
                lblTotal.Text = dt.Compute("Count(Member_ID1)", "").ToString();
            }
            else
            {
                gvActiveDeactive.DataSource = dt;
                gvActiveDeactive.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
            }

        }

        protected void gvActiveDeactive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActiveDeactive.PageIndex = e.NewPageIndex;
            Gridview();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.AddHeader("content-disposition", "attachment;filename=ActiveDeactiveMemberReportData" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);

                        //To Export all pages
                        gvActiveDeactive.AllowPaging = false;

                        gvActiveDeactive.AllowPaging = false;
                        this.Gridview();

                        gvActiveDeactive.HeaderRow.BackColor = Color.White;
                        foreach (TableCell cell in gvActiveDeactive.HeaderRow.Cells)
                        {
                            cell.BackColor = gvActiveDeactive.HeaderStyle.BackColor;
                        }

                        foreach (GridViewRow row in gvActiveDeactive.Rows)
                        {
                            row.BackColor = Color.White;
                            foreach (TableCell cell in row.Cells)
                            {
                                cell.CssClass = "textmode";
                            }
                        }

                        gvActiveDeactive.GridLines = GridLines.Both;
                        gvActiveDeactive.RenderControl(hw);

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
            catch (Exception ex)
            {

            }

        }

       

       
    }
}