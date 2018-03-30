using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class ReportAllMemberList : System.Web.UI.Page
    {
        BalReportAllMemberList objRepAllMem = new BalReportAllMemberList();
        DataTable dataTable = new DataTable();
        static int flag = 0;
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    BindGridView(); 
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

       
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void BindGridView()
        {
            AssignID();
            objRepAllMem.Action = "SelectAllMemberList";

            dataTable = objRepAllMem.GetDetails();
            lblCount.Text = "0";
            if (dataTable.Rows.Count > 0)
            {
                count = dataTable.Rows.Count;
                lblCount.Text = count.ToString();
                flag = 1;
                gvReportAllMemberList.DataSource = dataTable;
                gvReportAllMemberList.DataBind();
            }
            else
            {
                flag = 0;
                gvReportAllMemberList.DataSource = dataTable;
                gvReportAllMemberList.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
            }
        }

        private void AssignID()
        {
            objRepAllMem.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objRepAllMem.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                flag = 0;
                gvReportAllMemberList.DataSource = null;
                gvReportAllMemberList.DataBind();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        protected void gvReportAllMemberList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvReportAllMemberList.PageIndex = e.NewPageIndex;
                BindGridView();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (flag == 1)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.AddHeader("content-disposition", "attachment;filename=AllMemberListExport" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);

                        //To Export all pages
                        gvReportAllMemberList.AllowPaging = false;

                        gvReportAllMemberList.AllowPaging = false;
                        this.BindGridView();

                        gvReportAllMemberList.HeaderRow.BackColor = Color.White;
                        foreach (TableCell cell in gvReportAllMemberList.HeaderRow.Cells)
                        {
                            cell.BackColor = gvReportAllMemberList.HeaderStyle.BackColor;
                        }

                        foreach (GridViewRow row in gvReportAllMemberList.Rows)
                        {
                            row.BackColor = Color.White;
                            foreach (TableCell cell in row.Cells)
                            {
                                cell.CssClass = "textmode";
                            }
                        }

                        gvReportAllMemberList.GridLines = GridLines.Both;
                        gvReportAllMemberList.RenderControl(hw);

                        //style to format numbers to string

                        //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                        //Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                        
                        flag = 0;
                    }
                }
                else
                {
                    flag = 0;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('DataGridView Is Empty, Can Not Export !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

      



    }
}