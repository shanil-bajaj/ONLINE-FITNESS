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
    public partial class ReportAllCollection : System.Web.UI.Page
    {

        BalReports objreport = new BalReports();
        DataTable dt = new DataTable();
        int Flag;
        static int flag;
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignDate();
                LabelDisable();
                objreport.Action = "CollectionReports";
                BindGridview(); 
            }
        }

        public void AssignDate()
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

        private void AssignID()
        {
            objreport.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objreport.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objreport.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        public void GetRecodByDate()
        {
            try
            {
                gvAllCollectionReport.Visible = true;

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

                //objreport.StartDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
                //objreport.EndDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
                AssignID();

               // dt = objreport.BindCollectionData();
               // ViewState["Data"] = dt;
               //// ViewState["Data"] = dt;
               // // ViewState["ViewActiveDeactive"] = dt;

                BindGridview();
                flag = 1;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void BindGridview()
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
                lblCount.Text = "0";

                if (objreport.StartDate > objreport.EndDate)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                    gvAllCollectionReport.DataSource = null;
                    gvAllCollectionReport.DataBind();
                }
                else
                {

                    DataSet ds = new DataSet();
                    LabelEnable();
                    ds = objreport.Collection_TotalSum();
                    ds.Tables[0].TableName = "TableGridData";
                    ds.Tables[1].TableName = "TableSumTotal";
                    ds.Tables[2].TableName = "TableSumPaid";
                    //ds.Tables[3].TableName = "TableSumBalance";
                    ds.Tables[3].TableName = "TableSumDiscount";
                    ds.Tables[4].TableName = "TableSumCash";
                    ds.Tables[5].TableName = "TableSumCard";
                    ds.Tables[6].TableName = "TableSumCheque";
                    ds.Tables[7].TableName = "TableSumOther";

                    if (ds.Tables.Count > 0)
                    {
                        LabelEnable();
                        gvAllCollectionReport.DataSource = ds.Tables["TableGridData"];
                        gvAllCollectionReport.DataBind();

                        ViewState["Data"] = ds.Tables["TableGridData"];

                        count = ds.Tables["TableGridData"].Rows.Count;
                        lblCount.Text = count.ToString();

                        lblTotalAmt.Text = ds.Tables["TableSumTotal"].Compute("sum(TotalFeeDue)", "").ToString();
                        lblTotalpaid.Text = ds.Tables["TableSumPaid"].Compute("sum(TotalPaid)", "").ToString();
                        double tot = Convert.ToDouble(lblTotalAmt.Text);
                        double paid = Convert.ToDouble(lblTotalpaid.Text);
                        double bal = tot - paid;

                        //int tot = Convert.ToInt32(lblTotalAmt.Text);
                        //int paid = Convert.ToInt32(lblTotalpaid.Text);
                        //int bal = tot - paid;
                        lblTotalBalance1.Text = bal.ToString();
                        lblTotalDisc.Text = ds.Tables["TableSumDiscount"].Compute("sum(Discount)", "").ToString();
                        lblToatalCash.Text = ds.Tables["TableSumCash"].Compute("sum(TotalPaid)", "").ToString();
                        lblToatalCard.Text = ds.Tables["TableSumCard"].Compute("sum(TotalPaid)", "").ToString();
                        lblToatalCheck.Text = ds.Tables["TableSumCheque"].Compute("sum(TotalPaid)", "").ToString();
                        lblToatalOther.Text = ds.Tables["TableSumOther"].Compute("sum(TotalPaid)", "").ToString();
                        gvAllCollectionReport.Style["width"] = "100%";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                        gvAllCollectionReport.DataSource = dt;
                        gvAllCollectionReport.DataBind();
                        LabelDisable();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            objreport.Action = "CollectionReports";
            BindGridview();
            flag = 1;
        }

        public void DDLSearch()
        {
            try
            {
                if (ddlSearchBy.SelectedValue.ToString() == "Member ID")
                {
                    objreport.Category = "ByMemberID";
                    objreport.searchTxt = txtSearch.Text;

                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Member Name")
                {
                    objreport.Category = "ByMemberName";
                    objreport.searchTxt = txtSearch.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Contact")
                {
                    objreport.Category = "ByMemberContact";
                    objreport.searchTxt = txtSearch.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Receipt No")
                {
                    objreport.Category = "ByReceiptNo";
                    objreport.searchTxt = txtSearch.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Payment Mode")
                {
                    objreport.Category = "ByPaymentMode";
                    objreport.searchTxt = txtSearch.Text;
                }
                else
                {
                    objreport.Category = "All";
                    txtSearch.Text = "";
                }

                BindGridview();
                flag = 2;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void LabelDisable()
        {
            lblAmtPaidByCard.Visible = false;
            lblAmtPaidByCash.Visible = false;
            //lblFees.Visible = false;
            lblDisc.Visible = false;
            lblTotalDisc.Visible = false;
            lblTotal.Visible = false;
            lblTotalAmt.Visible = false;
            lblGst.Visible = false;
            lblGstamt.Visible = false;
            lblFinalAmount.Visible = false;
            lblTotalpaid.Visible = false;
            lblToatalCash.Visible = false;
            lblToatalCard.Visible = false;
            lblAmtPaidByCheck.Visible = false;
            lblToatalCheck.Visible = false;
            lblAmtPaidByOther.Visible = false;
            lblToatalOther.Visible = false;
        }

        public void LabelEnable()
        {
            lblAmtPaidByCard.Visible = true;
            lblAmtPaidByCash.Visible = true;
            //lblFees.Visible = true;
            //lblFeepaid.Visible = true;
            lblDisc.Visible = true;
            lblTotalDisc.Visible = true;
            lblTotal.Visible = true;
            lblTotalAmt.Visible = true;
            lblGst.Visible = true;
            lblGstamt.Visible = true;
            lblFinalAmount.Visible = true;
            lblTotalpaid.Visible = true;
            lblToatalCash.Visible = true;
            lblToatalCard.Visible = true;
            lblAmtPaidByCheck.Visible = true;
            lblToatalCheck.Visible = true;
            lblAmtPaidByOther.Visible = true;
            lblToatalOther.Visible = true;
        }

        protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearchBy.SelectedValue == "All")
            {
                txtSearch.Enabled = false;
            }
            else
            {
                txtSearch.Enabled = true;
                txtSearch.Focus();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            objreport.Action = "SerachByCategory";
            DDLSearch();
            flag = 2;
        }

        protected void gvAllCollectionReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllCollectionReport.PageIndex = e.NewPageIndex;
            if (flag == 1)
            {
                objreport.Action = "CollectionReports";
                BindGridview();
            }
            else if (flag == 2)
            {
                objreport.Action = "SerachByCategory";
                DDLSearch();
            }
            else if (flag == 3)
            {
                objreport.Action = "SerachByDateWithCategory";
                DDLSearch();
            }
        }

        private void Amount_Sum(DataTable dt)
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    int Other = 0;
                    int Cash = 0;
                    int Cheque = 0;
                    int Card = 0;
                    int Total = 0;
                    DataTable dataTable = dt;
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["PaymentMode"].ToString() == "Cash")
                        {
                            Cash = Cash + Convert.ToInt32(dataTable.Rows[i]["PaidWithTax"]);
                        }
                        else if (dataTable.Rows[i]["PaymentMode"].ToString() == "Card")
                        {
                            Card = Card + Convert.ToInt32(dataTable.Rows[i]["PaidWithTax"]);
                        }
                        else if (dataTable.Rows[i]["PaymentMode"].ToString() == "Cheque")
                        {
                            Cheque = Cheque + Convert.ToInt32(dataTable.Rows[i]["PaidWithTax"]);
                        }
                        else
                        {
                            Other = Other + Convert.ToInt32(dataTable.Rows[i]["PaidWithTax"]);
                        }

                        Total = Total + Convert.ToInt32(dataTable.Rows[i]["PaidWithTax"]);
                    }

                    lblTotalpaid.Text = Total.ToString();
                    lblToatalCash.Text = Cash.ToString();
                    lblToatalCard.Text = Card.ToString();
                    lblToatalCheck.Text = Cheque.ToString();
                    lblToatalOther.Text = Other.ToString();
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

        protected void btnCancle_Click(object sender, EventArgs e)
        {
           // gvAllCollectionReport.Visible = false;
            ViewState["Data"] = null;
            ddlSearchBy.SelectedItem.Text = "--Select--";
            AssignDate();
            lblTotalAmt.Text = "0";
            lblToatalCash.Text = "0";
            lblToatalOther.Text = "0";
            lblToatalCard.Text = "0";
            lblToatalCheck.Text = "0";
            lblTotalDisc.Text = "0";
            lblGstamt.Text = "0";
            lblTotalpaid.Text = "0";
            lblTotalBalance.Text = "0";
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

                        Response.AddHeader("content-disposition", "attachment;filename=AllCollection" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvAllCollectionReport.AllowPaging = false;

                            gvAllCollectionReport.DataSource = dt2;
                            gvAllCollectionReport.DataBind();
                            gvAllCollectionReport.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvAllCollectionReport.HeaderRow.Cells)
                            {
                                cell.BackColor = gvAllCollectionReport.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvAllCollectionReport.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            gvAllCollectionReport.GridLines = GridLines.Both;
                            gvAllCollectionReport.RenderControl(hw);

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
                       // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('DataGridView Is Empty, Can Not Export !!!.','Information');", true);
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Can Not Export !!!.','Error');", true);
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
            /* Verifies that the control is rendered */
        }

        public void DDLSearchWithDate()
        {
            try
            {
                if (ddlSearchBy.SelectedValue.ToString() == "Member ID")
                {
                    objreport.Category = "ByMemberID";
                    objreport.searchTxt = txtSearch.Text;

                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Member Name")
                {
                    objreport.Category = "ByMemberName";
                    objreport.searchTxt = txtSearch.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Contact")
                {
                    objreport.Category = "ByMemberContact";
                    objreport.searchTxt = txtSearch.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Receipt No")
                {
                    objreport.Category = "ByReceiptNo";
                    objreport.searchTxt = txtSearch.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Payment Mode")
                {
                    objreport.Category = "ByPaymentMode";
                    objreport.searchTxt = txtSearch.Text;
                }
                else
                {
                    objreport.Category = "All";
                    txtSearch.Text = "";
                }

                AssignID();
                dt.Clear();
                dt = objreport.Collection_GetSearch();
                ViewState["Data"] = dt;
                BindGridview();
                flag = 2;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnSearchByDateAndCategory_Click(object sender, EventArgs e)
        {
            objreport.Action = "SerachByDateWithCategory";
            DDLSearch();
            flag = 3;
        }

 }

}