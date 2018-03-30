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
    public partial class ReportBalancePayment : System.Web.UI.Page
    {
        BalReports objreport = new BalReports();
        DataTable dt = new DataTable();
        int Flag;
        int count = 0;
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignDate();
                Bind_MemberName();
                lblbal.Text = "0";
                txtFromDate.Focus();
                //DataTable table = GetTable1();
                //DataTable table2 = GetTable2();
                //DataTable table3 = mergeTable(table, table2);
                Gridview(); 
            }
        }

        //private DataTable mergeTable(DataTable table, DataTable table2)
        //{
        //    table2.Columns.Add("Split", typeof(int));
        //    table2.Columns.Add("D_id", typeof(int));
        //    int split_column_index = table2.Columns["Split"].Ordinal;
        //    int D_id_column_index = table2.Columns["D_id"].Ordinal;

        //    for (int i = 0; i < table.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < table.Columns.Count; j++)
        //        {
        //            if (table.Columns[j].ToString() == "Split")
        //            {
        //                table2.Rows[i][split_column_index] = table.Rows[i][j].ToString();
        //            }
        //            if (table.Columns[j].ToString() == "D_id")
        //            {
        //                table2.Rows[i][D_id_column_index] = table.Rows[i][j].ToString();
        //            }
        //        }
        //    }

        //    return table2;

        //}

        //private DataTable GetTable1()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("C#1", typeof(int));
        //    table.Columns.Add("C#2", typeof(int));
        //    table.Columns.Add("C#3", typeof(int));
        //    table.Columns.Add("C#4", typeof(int));
        //    table.Columns.Add("C#5", typeof(int));
        //    table.Columns.Add("Split", typeof(int));
        //    table.Columns.Add("D_id", typeof(int));
        //    table.Rows.Add(1, 2, 3, 4, 5, 6, 7);
        //    table.Rows.Add(2, 3, 4, 5, 6, 7, 8);
        //    table.Rows.Add(3, 4, 5, 6, 7, 8, 9);
        //    return table;
        //}
        //private DataTable GetTable2()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("C#1", typeof(int));
        //    table.Columns.Add("C#2", typeof(int));

        //    table.Rows.Add(1, 2);
        //    table.Rows.Add(2, 3);
        //    table.Rows.Add(3, 4);
        //    return table;
        //}

        public void Bind_MemberName()
        {
            try
            {
                objreport.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objreport.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = objreport.MemberName_BalNotNull();
                if (dt.Rows.Count > 0)
                {
                    ddlMemberName.DataSource = dt;
                    ddlMemberName.Items.Clear();
                    ddlMemberName.DataValueField = "Member_AutoID";
                    ddlMemberName.DataTextField = "Name";
                    ddlMemberName.DataBind();
                    ddlMemberName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
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
        private void AssignID()
        {
            objreport.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objreport.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objreport.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        public void Gridview()
        {
            try
            {
                DateTime StartDate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
                {
                    string StartDate1 = StartDate.ToString("dd-MM-yyyy");
                    objreport.StartDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }

                DateTime EndDate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
                {
                    string EndDate1 = EndDate.ToString("dd-MM-yyyy");
                    objreport.EndDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                AssignID();
                objreport.MemberName = (ddlMemberName.SelectedItem.ToString());
               // objreport.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue.ToString());

                if (ddlSearch.SelectedValue.ToString() == "--Select--")
                {
                    objreport.Category = "--Select--";
                    //objreport.MemberType = ddlSearch.SelectedValue.ToString();
                    //lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Member Id")
                {
                    objreport.Category = "MemberId";
                    // objreport.MemberType = ddlSearch.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    //lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Member Name")
                {
                    objreport.Category = "MemberName";
                    //objreport.MemberType = ddlSearch.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    // lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Contact")
                {
                    objreport.Category = "Contact";
                    // objreport.MemberType = ddlSearch.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    //lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Payment Type")
                {
                    objreport.Category = "PaymentType";
                    // objreport.MemberType = ddlSearch.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    // lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Executive")
                {
                    objreport.Category = "Executive";
                    // objreport.MemberType = ddlSearch.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    //lblFinalAmount.Text = "Total Paid";
                }

                ViewState["Data"] = objreport.Report_Balance();
                dt = (DataTable)ViewState["Data"];
                lblCount.Text = "0";
                //dt = objreport.Report_PaymentReport();
                if (objreport.StartDate > objreport.EndDate)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                    GvBalanceReports.DataSource = null;
                    GvBalanceReports.DataBind();
                }
                else
                {
                    if(dt.Rows.Count >0)
                    {
                        count = dt.Rows.Count;
                        lblCount.Text = count.ToString();

                        GvBalanceReports.DataSource = dt;
                        GvBalanceReports.DataBind();
                        //lblFinalAmount.Text = "Total Paid";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                        GvBalanceReports.DataSource = dt;
                        GvBalanceReports.DataBind();
                        //LabelDisable();
                    }
                    Amount_Sum(dt);
                    //    DataSet ds = new DataSet();
                    //    //LabelEnable();
                    //    ds = objreport.Report_Balance_TotalSum();
                    //    ViewState["Data"] = objreport.Report_Balance_TotalSum();
                    //    ds = (DataSet)ViewState["Data"];
                    //    ds.Tables[0].TableName = "TableGridData";
                    //    ds.Tables[1].TableName = "TableSumAmt";
                    //    ViewState["Data"] = objreport.Report_Balance_TotalSum();
                    //    ds = (DataSet)ViewState["Data"];
                    //    objreport.Action = "BalanceReports";
                    //    ds = objreport.Report_Balance_TotalSum();
                    //    ds.Tables[0].TableName = "TableGridData";
                    //    ds.Tables[1].TableName = "TableSumTotal";
                    //    ds.Tables[2].TableName = "TableSumPaid";
                    //    //ds.Tables[2].TableName = "TableSumDiscount";

                    //    if (ds.Tables.Count > 0)
                    //    {
                    //        //LabelEnable();
                    //        GvBalanceReports.DataSource = ds.Tables["TableGridData"];
                    //        GvBalanceReports.DataBind();
                    //        //lblTotalAmt.Text = ds.Tables["TableSumTotal"].Compute("sum(TotalFeeDue)", "").ToString();
                    //        //lblTotalpaid.Text = ds.Tables["TableSumPaid"].Compute("sum(TotalPaid)", "").ToString();
                    //        lblTotal.Text = ds.Tables["TableSumTotal"].Compute("sum(TotalFeeDue)", "").ToString();
                    //        lblTotalpaid.Text = ds.Tables["TableSumPaid"].Compute("sum(TotalPaid)", "").ToString();
                    //        int tot = Convert.ToInt32(lblTotal.Text);
                    //        int paid = Convert.ToInt32(lblTotalpaid.Text);
                    //        int bal = tot - paid;
                    //        lblbal.Text = bal.ToString();
                    //        //lblTotalDisc.Text = ds.Tables["TableSumDiscount"].Compute("sum(Discount)", "").ToString();
                    //       // lblbal.Text = ds.Tables["TableSumAmt"].Compute("sum(Balance)", "").ToString();
                    //        GvBalanceReports.Style["width"] = "100%";

                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                    //        GvBalanceReports.DataSource = dt;
                    //        GvBalanceReports.DataBind();
                    //        //LabelDisable();
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        //private void BindGridview()
        //{
        //    try
        //    {
        //        DateTime StartDate;
        //        if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
        //        {
        //            string StartDate1 = StartDate.ToString("dd-MM-yyyy");
        //            objreport.StartDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
        //        }

        //        DateTime EndDate;
        //        if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
        //        {
        //            string EndDate1 = EndDate.ToString("dd-MM-yyyy");
        //            objreport.EndDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
        //        }
        //        AssignID();


        //        if (objreport.StartDate > objreport.EndDate)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
        //            GvBalanceReports.DataSource = null;
        //            GvBalanceReports.DataBind();
        //        }
        //        else
        //        {

        //            DataSet ds = new DataSet();
        //            //LabelEnable();
        //            ds = objreport.Collection_TotalSum();
        //            ds.Tables[0].TableName = "TableGridData";
        //            ds.Tables[1].TableName = "TableSumAmt";
        //            ds.Tables[2].TableName = "TableSumDiscount";

        //            if (ds.Tables.Count > 0)
        //            {
        //                //LabelEnable();
        //                GvBalanceReports.DataSource = ds.Tables["TableGridData"];
        //                GvBalanceReports.DataBind();
        //                lblTotal.Text = ds.Tables["TableSumAmt"].Compute("sum(TotalFeeDue)", "").ToString();
        //                lblTotalpaid.Text = ds.Tables["TableSumAmt"].Compute("sum(TotalPaid)", "").ToString();
        //                //lblTotalDisc.Text = ds.Tables["TableSumDiscount"].Compute("sum(Discount)", "").ToString();
        //                lblbal.Text = ds.Tables["TableSumAmt"].Compute("sum(Balance)", "").ToString();
        //                GvBalanceReports.Style["width"] = "100%";
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
        //                GvBalanceReports.DataSource = dt;
        //                GvBalanceReports.DataBind();
        //                //LabelDisable();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }
        //}


        protected void btnSearch_Click(object sender, EventArgs e)
        {
          
            Gridview();
            flag = 1;
            lblTotal.Visible = false;
            lblTotalpaid.Visible = false;
        }

        private void Amount_Sum(DataTable dt)
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    //int Other = 0;
                    //int Cash = 0;
                    //int Cheque = 0;
                    //int Card = 0;
                    int bal = 0;
                    DataTable dataTable = dt;
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        //if (dataTable.Rows[i]["PaymentMode"].ToString() == "Cash")
                        //{
                            bal = bal + Convert.ToInt32(dataTable.Rows[i]["Balance"]);
                       // }
                        //if (dataTable.Rows[i]["PaymentMode"].ToString() == "Cash")
                        //{
                        //    Cash = Cash + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        //}
                        //else if (dataTable.Rows[i]["PaymentMode"].ToString() == "Card")
                        //{
                        //    Card = Card + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        //}
                        //else if (dataTable.Rows[i]["PaymentMode"].ToString() == "Cheque")
                        //{
                        //    Cheque = Cheque + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        //}
                        //else
                        //{
                        //    Other = Other + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        //}

                        //Total = Total + Convert.ToInt32(dataTable.Rows[i]["Paid_Fee"]);
                    }

                    //txtTotl.Text = Total.ToString();
                    //lblToatalCash.Text = Cash.ToString();
                    //lblToatalCard.Text = Card.ToString();
                    //lblToatalCheck.Text = Cheque.ToString();
                    //lblToatalOther.Text = Other.ToString();
                    lblbal.Text = bal.ToString();
                }

                else
                {
                    //Error_Validation_Page("Records Not Found!!!");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        public void ExportToExcel()
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    DataTable dt2 = (DataTable)ViewState["Data"];
                    //if (ds2.Rows.Count > 0)
                    //if (ds2.Tables[0].Rows[0].Count > 0)
                    if (dt2 != null)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=BalanceReportData" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GvBalanceReports.AllowPaging = false;

                            GvBalanceReports.AllowPaging = false;
                            this.Gridview();

                            GvBalanceReports.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GvBalanceReports.HeaderRow.Cells)
                            {
                                cell.BackColor = GvBalanceReports.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GvBalanceReports.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            GvBalanceReports.GridLines = GridLines.Both;
                            GvBalanceReports.RenderControl(hw);

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

        protected void GvBalanceReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                GvBalanceReports.PageIndex = e.NewPageIndex;
                Gridview();
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            AssignDate();
            Bind_MemberName();
            GvBalanceReports.DataSource = null;
            GvBalanceReports.DataBind();
            //lblTotal.Text = "0";
            //lblTotalpaid.Text = "0";
            lblbal.Text = "0";
            GvBalanceReports.PageIndex = 0;
            ddlMemberName.SelectedValue = "--Select--";
            ddlSearch.SelectedValue = "--Select--";
            txtSearch.Text = "";
            ViewState["Data"] = null;
        }
    }
}