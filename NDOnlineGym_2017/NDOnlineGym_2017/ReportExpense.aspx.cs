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
    public partial class ReportExpense : System.Web.UI.Page
    {
         BalExpense ObjExpense = new BalExpense();
         DataTable dt = new DataTable();
         int Flag;
         static int flag;
         int count = 0;
         protected void Page_Load(object sender, EventArgs e)
         {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Focus();
                    AssignDate();
                    Bind_ExpenseGrp();
                    ddlExpenseGroup.Text = "--Select--";
                    Search();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
         }

        public void AssignID()
        {
            ObjExpense.company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
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

        public void Bind_ExpenseGrp()
        {
            try
            {
                AssignID();
                dt = ObjExpense.Get_ExpenseGroup();
                
                if (dt.Rows.Count > 0)
                {
                    ddlExpenseGroup.DataSource = dt;
                    ddlExpenseGroup.Items.Clear();
                    ddlExpenseGroup.DataValueField = "Expgrp_AutoID";
                    ddlExpenseGroup.DataTextField = "Name";
                    ddlExpenseGroup.DataBind();
                    ddlExpenseGroup.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void BindGridview()
        {
            try
            {
                lblCount.Text = "0";
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            
               ObjExpense.StartDate = Fromdate;
               ObjExpense.EndDate = Todate;
               if (ObjExpense.StartDate > ObjExpense.EndDate)
               {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.info('From Date Should Not be Greater than To Date..!!!','Information');", true);   
               }
               else
               {
                  if (dt.Rows.Count > 0)
                  {
                      count = dt.Rows.Count;
                      lblCount.Text = count.ToString();
                      GvExpenseReport1.DataSource = dt;
                      GvExpenseReport1.DataBind();
                  }
                  else
                  {
                      GvExpenseReport1.DataSource = dt;
                      GvExpenseReport1.DataBind();
                      ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                  }
               }
             }
             catch (Exception ex)
             {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                 ErrorHandiling.SendErrorToText(ex);
             }
         }

        public void Search()
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }

            ObjExpense.StartDate = Fromdate;
            ObjExpense.EndDate = Todate;
           
            //dt = ObjExpense.Select_AllForReport();
            //ddlExpenseGroup. = Fromdate;
            //ddlExpenseGroup.EndDate = Todate;
            AssignID();
            dt = ObjExpense.Select_AllForReport();
            ViewState["Data"] = dt;

            flag = 1;
            BindGridview();
            GvExpenseReport1.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            AssignDate();
            ddlExpenseGroup.Text = "--Select--";
            GvExpenseReport1.Visible = false;
            ViewState["Data"] = null;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        public void ddlExpenseGrpSearch()
        {
            ObjExpense.ExGrp_AutoId = Convert.ToInt32(ddlExpenseGroup.SelectedValue);
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }

            ObjExpense.StartDate = Fromdate;
            ObjExpense.EndDate = Todate;
            AssignID();
            dt = ObjExpense.Report_SelectByExpGrp();
            ViewState["Data"] = dt;
            flag = 2;
            BindGridview();
            GvExpenseReport1.Visible = true;
        }

        protected void ddlExpenseGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlExpenseGrpSearch();
        }

        protected void GvExpenseReport1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                GvExpenseReport1.PageIndex = e.NewPageIndex;
                Search();
            }
            else if (flag == 2)
            {
                GvExpenseReport1.PageIndex = e.NewPageIndex;
                ddlExpenseGrpSearch();
            }
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

                        Response.AddHeader("content-disposition", "attachment;filename=ExpenseData" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GvExpenseReport1.AllowPaging = false;
                            // this.BindGridview();
                            GvExpenseReport1.DataSource = dt2;
                            GvExpenseReport1.DataBind();
                            GvExpenseReport1.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GvExpenseReport1.HeaderRow.Cells)
                            {
                                cell.BackColor = GvExpenseReport1.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GvExpenseReport1.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            GvExpenseReport1.GridLines = GridLines.Both;
                            GvExpenseReport1.RenderControl(hw);

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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }
     
    }
}