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
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class ReportBlockUnblock : System.Web.UI.Page
    {
        BalTermination objBalTerm = new BalTermination();
        DataTable dt = new DataTable();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddltype.Focus();
                    AssignID();
                    try
                    {
                        lblCount.Text = "0";
                        AssignID();
                        objBalTerm.Status = "All";
                        dt = objBalTerm.BindBlockUnblockReport();
                        ViewState["Data"] = dt;
                        if (dt.Rows.Count > 0)
                        {
                            count = dt.Rows.Count;
                            lblCount.Text = count.ToString();
                            GvBlockUnblockMember.DataSource = dt;
                            GvBlockUnblockMember.DataBind();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                            GvBlockUnblockMember.Visible = false;
                        }
                        GvBlockUnblockMember.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        ErrorHandiling.SendErrorToText(ex);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void AssignID()
        {
            objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
            objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        public void BindGridView()
        {
            try
            {
                lblCount.Text = "0";
                AssignID();
                objBalTerm.Status = ddltype.SelectedValue;
                dt = objBalTerm.BindBlockUnblockReport();
                ViewState["Data"] = dt;
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    GvBlockUnblockMember.DataSource = dt;
                    GvBlockUnblockMember.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                    GvBlockUnblockMember.Visible = false;
                }
                GvBlockUnblockMember.Visible = true;

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            GvBlockUnblockMember.Visible = false;
            ddltype.SelectedIndex = 0;
            ViewState["Data"] = null;
        }

        public void ExportToExcel()
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    DataTable dt2 = (DataTable)ViewState["Data"];
                    if(dt2.Rows.Count>0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=BlockUnblockExport" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GvBlockUnblockMember.AllowPaging = false;

                            GvBlockUnblockMember.DataSource = dt2;
                            GvBlockUnblockMember.DataBind();

                            GvBlockUnblockMember.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GvBlockUnblockMember.HeaderRow.Cells)
                            {
                                cell.BackColor = GvBlockUnblockMember.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GvBlockUnblockMember.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            GvBlockUnblockMember.GridLines = GridLines.Both;
                            GvBlockUnblockMember.RenderControl(hw);

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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void GvBlockUnblockMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBlockUnblockMember.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}