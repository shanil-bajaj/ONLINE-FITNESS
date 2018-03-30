using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NDOnlineGym_2017
{
    public partial class ViewBalancePaymentFollowup : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalViewBalancePaymentFollowup obBalViewBalancePaymentFollowup = new BalViewBalancePaymentFollowup();
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodaysDate();
                GridBind();
                txtFromDate.Focus();
            }
        }

        #region---------------------------Payment Followup---------------------------
        #region ------------ Assign All Date ------------------
        protected void AssignTodaysDate()
        {
            //DateTime todaydate;
            //if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            //{
            //    txtFromDate.Text = todaydate.ToString("dd-MM-yyyy");
            //    txtToDate.Text = todaydate.ToString("dd-MM-yyyy");
              
            //    DateTime utcTime = DateTime.UtcNow;
            //    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            //    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            //}


            Label1.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Label1.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);
            txtToDate.Text = dtLast.ToString("dd-MM-yyyy");

            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
        }
        #endregion

        #region BIND GRID
        public void GridBind()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                obBalViewBalancePaymentFollowup.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                obBalViewBalancePaymentFollowup.ToDate = Todate;
                obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                
                dt = obBalViewBalancePaymentFollowup.Search();

                if (dt.Rows.Count > 0)
                {
                    gvPaymentFoll.Visible = false;
                    gvBalPayDetails.Visible = true;
                    gvBalPayDetails.DataSource = dt;
                    gvBalPayDetails.DataBind();
                    ViewState["PayFollDetails"] = dt;
                }
                else
                {
                    gvBalPayDetails.Visible = false;
                }
           }
            catch (Exception ex)
            {

            }

        }
        #endregion

        public void SearchByCategory()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            
            if (ddlCategory.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            if (ddlCategory.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            if (ddlCategory.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            if (ddlCategory.SelectedValue == "Gender")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearch.Text;
            }
            
            GridBind();
        }

        #region Page Indexing
        protected void gvBalPayDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                obBalViewBalancePaymentFollowup.Category = "All";
                obBalViewBalancePaymentFollowup.Action = "Search";
                gvBalPayDetails.PageIndex = e.NewPageIndex;
                GridBind();
            }
            else if (flag == 2)
            {
                obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
                obBalViewBalancePaymentFollowup.Category = ddlCategory.SelectedValue;
                gvBalPayDetails.PageIndex = e.NewPageIndex;
                SearchByCategory();
            }
            else if (flag == 3)
            {
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                obBalViewBalancePaymentFollowup.Category = ddlCategory.SelectedValue;
                gvBalPayDetails.PageIndex = e.NewPageIndex;
                SearchByCategory();
            }

        }
        #endregion


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            obBalViewBalancePaymentFollowup.Category = "All";
            obBalViewBalancePaymentFollowup.Action = "Search";
            GridBind();
            flag = 1;
        }

        protected void btnSearchByDateCategory_Click(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                return;
            }
            else if (ddlCategory.SelectedValue != "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                return;
            }
            else
            {
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                obBalViewBalancePaymentFollowup.Category = ddlCategory.SelectedValue;
                SearchByCategory();
                flag = 3;
            }
        }

        public void Clear()
        {
            AssignTodaysDate();
            ddlCategory.SelectedValue = "--Select--";
            txtSearch.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            gvBalPayDetails.DataSource = null;
            gvBalPayDetails.DataBind();
            gvPaymentFoll.DataSource = null;
            gvPaymentFoll.DataBind();
            ddlCategory.Focus();
        }

        protected void btnBalPay_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("BalancePayment.aspx?Member_ID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        string FNBalPayFollDetail = "FNBalPayFollDetail";
        protected void btnFollowup_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            //Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
            Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNBalPayFollDetail=" + HttpUtility.UrlEncode(FNBalPayFollDetail.ToString()));
        }

        protected void btnName_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategory.SelectedValue;
            SearchByCategory();
            flag = 2;
        }

        #endregion

        protected void btnExistingFollowup_Click(object sender, EventArgs e)
        {
            ExistingPayFoll();
        }

        public void ExistingPayFoll()
        {
            obBalViewBalancePaymentFollowup.FollowupType = "Payment";
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = obBalViewBalancePaymentFollowup.SelectFollDetails_By_FollowupType();
            if (dt.Rows.Count > 0)
            {
                gvBalPayDetails.Visible = false;
                gvPaymentFoll.Visible = true;
                gvPaymentFoll.DataSource = dt;
                gvPaymentFoll.DataBind();
            }
            else
            {
                gvBalPayDetails.Visible = false;
                gvPaymentFoll.Visible = true;
                gvPaymentFoll.DataSource = null;
                gvPaymentFoll.DataBind();
            }
        }

        protected void gvPaymentFoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBalPayDetails.PageIndex = e.NewPageIndex;
            ExistingPayFoll();
        }

        protected void btnPaymentFollowup_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            //Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
            Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNBalPayFollDetail=" + HttpUtility.UrlEncode(FNBalPayFollDetail.ToString()));
        }

        protected void btnPayFollName_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        #region ----------------- Export To Excle Record ----------------

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        protected void ExportToExcel()
        {
            try
            {

                if (ViewState["PayFollDetails"] != null)
                {
                    dt = (DataTable)ViewState["PayFollDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=PayFollDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvBalPayDetails.Columns[0].Visible = false;
                            gvBalPayDetails.Columns[1].Visible = false;
                            gvBalPayDetails.AllowPaging = false;
                            gvBalPayDetails.DataSource = dt;
                            gvBalPayDetails.DataBind();
                            gvBalPayDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvBalPayDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvBalPayDetails.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvBalPayDetails.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                    List<Control> controls = new List<Control>();
                                    //Add controls to be removed to Generic List
                                    foreach (Control control in cell.Controls)
                                    {
                                        controls.Add(control);
                                    }
                                }
                            }
                            gvBalPayDetails.GridLines = GridLines.Both;
                            gvBalPayDetails.RenderControl(hw);
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
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        #endregion
    }
}