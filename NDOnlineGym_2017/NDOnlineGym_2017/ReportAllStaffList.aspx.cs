using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Globalization;
using System.Drawing;
using System;
using DataAccessLayer;
namespace NDOnlineGym_2017
{
    public partial class ReportAllStaffList : System.Web.UI.Page
    {
        BalStaffRegistration StaffObj = new BalStaffRegistration();
        DataTable dataTable = new DataTable();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region Search_Click
        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {

                
                BindGrid();
              
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region Bind Grid View
        public void BindGrid()
        {
            try
            {

                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                dataTable = StaffObj.Select_All();
                ViewState["Data"] = dataTable;
                gvAllStaffList.Visible = true;

                lblCount.Text = "0";
                if (dataTable.Rows.Count > 0)
                {
                    count = dataTable.Rows.Count;
                    lblCount.Text = count.ToString();
                    gvAllStaffList.DataSource = dataTable;
                    gvAllStaffList.DataBind();
                }
                else
                {
                    gvAllStaffList.DataSource = dataTable;
                    gvAllStaffList.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region Paging
        protected void gvAllStaffList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllStaffList.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

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

                        Response.AddHeader("content-disposition", "attachment;filename=AllStaffListExport" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvAllStaffList.AllowPaging = false;

                            gvAllStaffList.DataSource = dt2;
                            gvAllStaffList.DataBind();
                            gvAllStaffList.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvAllStaffList.HeaderRow.Cells)
                            {
                                cell.BackColor = gvAllStaffList.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvAllStaffList.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            gvAllStaffList.GridLines = GridLines.Both;
                            gvAllStaffList.RenderControl(hw);

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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            gvAllStaffList.Visible = false;
            ViewState["Data"] = null;
        }

    }
       
}