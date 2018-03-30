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
    public partial class ReportPayment : System.Web.UI.Page
    {
        BalReports objreport = new BalReports();
        DataTable dt = new DataTable();
        BalExpense ObjExpense = new BalExpense();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindExecutive();
                AssignDate();
                //Bind_PaymentType();
                LableDisabled();
                Gridview();
            }
        }
        //public void Bind_PaymentType()
        //{
        //    try
        //    {
        //        ObjExpense.company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
        //        ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
        //        dt = ObjExpense.Get_PaymentType();
        //        if (dt.Rows.Count > 0)
        //        {
        //            DropDownList1.DataSource = dt;
        //            DropDownList1.Items.Clear();
        //            DropDownList1.DataValueField = "PaymentMode_AutoID";
        //            DropDownList1.DataTextField = "Name";
        //            DropDownList1.DataBind();
        //            DropDownList1.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }
        //}
        public void AssignDate()
        {
            try
            {
                DateTime date;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                { }

                DateTime dtFirst = Convert.ToDateTime(date);
                DateTime dtLast = Convert.ToDateTime(date);

                //Setting Start Date Month
               // dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
                txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

                //Setting Last Date of Month
               // dtLast = dtFirst.AddMonths(1).AddDays(-1);
                txtToDate.Text = dtLast.ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);

            }
        }

        //public void BindExecutive()
        //{
        //    try
        //    {
        //        AssignID();
        //        dt = objreport.Get_ExecutiveName();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlExecutive.DataSource = dt;
        //            ddlExecutive.Items.Clear();
        //            ddlExecutive.DataValueField = "Staff_AutoID";
        //            ddlExecutive.DataTextField = "Executive";
        //            ddlExecutive.DataBind();
        //            ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }
        //}

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
                LableEnabaled();
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                AssignID();

                //objreport.Staff_AutoID = Convert.ToInt32(ddlExecutive.SelectedValue);
                //objreport.MemberType = ddltype.SelectedValue;

                //lblFromDateTime.Text = Fromdate.ToString("dd-MM-yyyy");
                //lblToDateTime.Text = Todate.ToString("dd-MM-yyyy");

                //if (ddlSearch.SelectedItem.Text == "--Select--")
                //{
                //    objreport.Category = "--Select--";
                //    lblFinalAmount.Text = "Total Paid";
                //}
                //else
                //{
                //    objreport.Executive = "Executive";
                //    objreport.ExecutiveID = Convert.ToInt32(ddlSearch.SelectedValue.ToString());
                //    lblFinalAmount.Text = "Total Paid";
                //}
                //if (ddltype.SelectedItem.Text == "--Select--")
                //{

                //    objreport.MemberType = "--Select--";
                //    lblFinalAmount.Text = "Total Paid";
                //}
                //else
                //{
                //    objreport.MemberType = ddltype.SelectedValue.ToString();
                //    lblFinalAmount.Text = "Total Paid";

                //}
                //if (DropDownList1.SelectedItem.Text == "--Select--")
                //{
                //    objreport.PaymentMode = "--Select--";
                //    lblFinalAmount.Text = "Total Paid";
                //}
                //else
                //{
                //    objreport.PaymentMode = DropDownList1.SelectedItem.Text;// "PaymentMode";
                //    objreport.PaymentModeID = 0;//Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                //    lblFinalAmount.Text = DropDownList1.SelectedItem.Text;
                //}

                if (ddlSearch.SelectedValue.ToString() == "--Select--")
                {
                    objreport.Category = "--Select--";
                    objreport.MemberType = ddltype.SelectedValue.ToString();
                    lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Member Id")
                {
                    objreport.Category = "MemberId";
                    objreport.MemberType = ddltype.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Member Name")
                {
                    objreport.Category = "MemberName";
                    objreport.MemberType = ddltype.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Contact")
                {
                    objreport.Category = "Contact";
                    objreport.MemberType = ddltype.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Payment Type")
                {
                    objreport.Category = "PaymentType";
                    objreport.MemberType = ddltype.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    lblFinalAmount.Text = "Total Paid";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Executive")
                {
                    objreport.Category = "Executive";
                    objreport.MemberType = ddltype.SelectedValue.ToString();
                    objreport.searchTxt = txtSearch.Text;
                    lblFinalAmount.Text = "Total Paid";
                }
                //else if (ddlSearch.SelectedValue.ToString() == "Executive")
                //{
                //    objreport.Category = "ByExecutive";
                //    objreport.searchTxt = txtSearch.Text;
                //}
                objreport.StartDate = Fromdate;
                objreport.EndDate = Todate;


                ViewState["Data"] = objreport.Report_PaymentReport();
                dt = (DataTable)ViewState["Data"];
                lblCount.Text = "0";
                //dt = objreport.Report_PaymentReport();
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();

                    GvPaymentReports.DataSource = dt;
                    GvPaymentReports.DataBind();
                    lblFinalAmount.Text = "Total Paid";
                    lblTotalpaid.Text = dt.Compute("sum(Paid)", "").ToString();
                }
                else
                {
                    GvPaymentReports.DataSource = dt;
                    GvPaymentReports.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                }
                Amount_Sum(dt);
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
            //clear();
        }

        protected void GvPaymentReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvPaymentReports.PageIndex = e.NewPageIndex;
            Gridview();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
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

                        Response.AddHeader("content-disposition", "attachment;filename=PaymentReportData" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GvPaymentReports.AllowPaging = false;
                            GvPaymentReports.DataSource = dt2;
                            GvPaymentReports.DataBind();

                            GvPaymentReports.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GvPaymentReports.HeaderRow.Cells)
                            {
                                cell.BackColor = GvPaymentReports.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GvPaymentReports.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            GvPaymentReports.GridLines = GridLines.Both;
                            GvPaymentReports.RenderControl(hw);

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
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Can Not Export !!!.','Error');", true);
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        public void LableDisabled()
        {
            lblFinalAmount.Visible = false;
            lblToatalCard.Visible = false;
            lblToatalCheck.Visible = false;
            lblToatalCash.Visible = false;
            lblToatalOther.Visible = false;
            lblAmtPaidByOther.Visible = false;
            lblAmtPaidByCheck.Visible = false;
            lblAmtPaidByCard.Visible = false;
            lblAmtPaidByCash.Visible = false;
            lblFinalAmount.Visible = false;
        }

        public void LableEnabaled()
        {
            lblFinalAmount.Visible = true;
            lblToatalCard.Visible = true;
            lblToatalCheck.Visible = true;
            lblToatalCash.Visible = true;
            lblToatalOther.Visible = true;
            lblAmtPaidByOther.Visible = true;
            lblAmtPaidByCheck.Visible = true;
            lblAmtPaidByCard.Visible = true;
            lblAmtPaidByCash.Visible = true;
            lblFinalAmount.Visible = true;
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
                    DataTable dataTable = dt;
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["PaymentMode"].ToString() == "Cash")
                        {
                            Cash = Cash + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        }
                        else if (dataTable.Rows[i]["PaymentMode"].ToString() == "Card")
                        {
                            Card = Card + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        }
                        else if (dataTable.Rows[i]["PaymentMode"].ToString() == "Cheque")
                        {
                            Cheque = Cheque + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        }
                        else
                        {
                            Other = Other + Convert.ToInt32(dataTable.Rows[i]["PaidFee"]);
                        }

                        //Total = Total + Convert.ToInt32(dataTable.Rows[i]["Paid_Fee"]);
                    }

                    //txtTotl.Text = Total.ToString();
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

        public void clear()
        {
            //BindExecutive();
            AssignDate();
            //Bind_PaymentType();
            ddltype.SelectedIndex = 0;
            GvPaymentReports.DataSource = null;
            GvPaymentReports.DataBind();
            lblFinalAmount.Text = "Total Paid";
            lblTotalpaid.Text = "0";
            lblToatalCash.Text = "0";
            lblToatalCheck.Text = "0";
            lblToatalCard.Text = "0";
            lblToatalOther.Text = "0";
            lblTotalpaid.Text = "0";
            //lblGstamt.Text = "0";
            ddlSearch.SelectedValue = "--Select--";
            txtSearch.Text = "";
            GvPaymentReports.PageIndex = 1;
            ViewState["Data"] = null;
        }


        protected void btnCancle_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearch.SelectedValue == "--Select--")
            {
                txtSearch.Enabled = false;
                txtSearch.Text = "";
            }
            else
            {
                txtSearch.Enabled = true;
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

    }
}