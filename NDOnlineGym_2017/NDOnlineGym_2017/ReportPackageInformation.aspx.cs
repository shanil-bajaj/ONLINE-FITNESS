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
    public partial class PackageInformationReport : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        BalReportPackageInformation objBalReportPackageInformation = new BalReportPackageInformation();
        int count;
        protected void Page_Load(object sender, EventArgs e)
        {
            Gridview(); 
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Gridview();
            gvReportPackageInformation.PageIndex = 1;
        }

        private void AssignID()
        {
            objBalReportPackageInformation.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalReportPackageInformation.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalReportPackageInformation.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }


        public void Gridview()
        {

            AssignID();
            lblCount.Text = "0";
            ViewState["Data"] = objBalReportPackageInformation.GetDetails_PackageInformationReport();
            dt = (DataTable)ViewState["Data"];


            if (dt.Rows.Count > 0)
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvReportPackageInformation.DataSource = dt;
                gvReportPackageInformation.DataBind();
            }
            else
            {
                gvReportPackageInformation.DataSource = dt;
                gvReportPackageInformation.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ViewState["Data"]=null;
            gvReportPackageInformation.DataSource = null;
            gvReportPackageInformation.DataBind();
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

                        Response.AddHeader("content-disposition", "attachment;filename=PackageInformationReportData" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvReportPackageInformation.AllowPaging = false;

                            gvReportPackageInformation.AllowPaging = false;
                            this.Gridview();

                            gvReportPackageInformation.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvReportPackageInformation.HeaderRow.Cells)
                            {
                                cell.BackColor = gvReportPackageInformation.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvReportPackageInformation.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            gvReportPackageInformation.GridLines = GridLines.Both;
                            gvReportPackageInformation.RenderControl(hw);

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
                
            }
        }

        protected void gvReportPackageInformation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReportPackageInformation.PageIndex = e.NewPageIndex;
            Gridview();
        }
    }
}