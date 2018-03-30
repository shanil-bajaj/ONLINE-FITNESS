using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NDOnlineGym_2017
{
    public partial class AllFollowup : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalAllFollowup obBalAllFollowup = new BalAllFollowup();
        BalViewBalancePaymentFollowup obBalViewBalancePaymentFollowup = new BalViewBalancePaymentFollowup();
        BalEnquiry eng = new BalEnquiry();
        static int flag;
        int count = 0;


        //BalViewBalancePaymentFollowup obBalViewBalancePaymentFollowup = new BalViewBalancePaymentFollowup();
        BalCallRespondMaster obBalCallRespondMaster = new BalCallRespondMaster();
        BalFollowupTypeMaster obBalFollowupTypeMaster = new BalFollowupTypeMaster();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalFollowup objFollowup = new BalFollowup();
        DataTable dataTable = new DataTable();
        BalAddMember objMemberDetails = new BalAddMember();
        int res;
        static int MemberAutoID;
        static int flagpopup = 0;

        BalEnquiryFollowup objEnqFlw = new BalEnquiryFollowup();
        BalEnquiry objBalEnquiry = new BalEnquiry();
        static int Enq_ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (flagpopup == 1)
                Label33_ModalPopupExtender1.Show();
            if (flagpopup == 2)
                Label34_ModalPopupExtender1.Show();
            if (!IsPostBack)
            {
                AssignTodaysDate();
                if (Request.QueryString["FNAllFollowupDetailsBal"] != null)
                {
                    //obBalViewBalancePaymentFollowup.Category = "All";
                    //obBalViewBalancePaymentFollowup.Action = "Search";
                    //GridBindPayment();
                    //flag = 1;
                    PaymentFollowupBtnClick();
                }
                if (Request.QueryString["FNAllFollowupDetailsEnq"] != null)
                {
                    SearchByDateEnquiry();
                    flag = 1;
                }
            }
        }

        #region ------------ Assign All Date ------------------
        protected void AssignTodaysDate()
        {
            Labeldt.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Labeldt.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);


            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDateExistingFoll.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDateExistingFoll.Text = dtLast.ToString("dd-MM-yyyy");
                txtFromDatePayment.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDatePayment.Text = dtLast.ToString("dd-MM-yyyy");
                txtFromDateMemberEnd.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDateMemberEnd.Text = dtLast.ToString("dd-MM-yyyy");
                txtFromDateMemberFollowup.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDateMemberFollowup.Text = dtLast.ToString("dd-MM-yyyy");
                txtFromDateMeasurementFollowup.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDateMeasurementFollowup.Text = dtLast.ToString("dd-MM-yyyy");
                txtFromDateUpgrade.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDateUpgrade.Text = dtLast.ToString("dd-MM-yyyy");
                txtFromDateEnquiry.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDateEnquiry.Text = dtLast.ToString("dd-MM-yyyy"); 

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            }


            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalAllFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalAllFollowup.ToDate = Todate;


            if (DateTime.TryParseExact(txtFromDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            if (DateTime.TryParseExact(txtToDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;


            if (DateTime.TryParseExact(txtFromDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            eng.FromDate = Fromdate;
            if (DateTime.TryParseExact(txtToDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            eng.ToDate = Todate;

        }
        #endregion

        protected void btnPaymentFollowupNioti_Click(object sender, ImageClickEventArgs e)
        {
            PaymentFollowupBtnClick();
        }

        public void PaymentFollowupBtnClick()
        {
            divPaymentFollowup.Visible = true;
            divEnquiryFollowup.Visible = false;
            divMemberEndFollowup.Visible = false;
            divMembershipFollowup.Visible = false;
            divExistingFollowup.Visible = false;
            divUpgradeFollowup.Visible = false;
            divMeasurementFollowup.Visible = false;
            gvEnquiry.Visible = false;
            gvUpgradeDetails.Visible = false;
            gvMemberDetails.Visible = false;
            gvMeasurementDetails.Visible = false;
            //gvBalPayDetails.Visible = false;
            gvFollowupDetails.Visible = false;
            gvFollowupDetails.Visible = false;
            obBalViewBalancePaymentFollowup.Category = "All";
            obBalViewBalancePaymentFollowup.Action = "Search";
            GridBindPayment();
            flag = 1;
            txtFromDatePayment.Focus();
        }

        protected void btnEnquiryFollowupNioti_Click(object sender, ImageClickEventArgs e)
        {
            divPaymentFollowup.Visible = false;
            divEnquiryFollowup.Visible = true;
            divMemberEndFollowup.Visible = false;
            divMembershipFollowup.Visible = false;
            divExistingFollowup.Visible = false;
            divUpgradeFollowup.Visible = false;
            divMeasurementFollowup.Visible = false;
            //gvEnquiry.Visible = false;
            gvUpgradeDetails.Visible = false;
            gvMemberDetails.Visible = false;
            gvMemEndDetails.Visible = false;
            gvBalPayDetails.Visible = false;
            gvFollowupDetails.Visible = false;
            gvMeasurementDetails.Visible = false;
            SearchByDateEnquiry();
            //eng.Action = "SearchAll";
            //eng.DateCategory = "EnquiryDate";
            //bindEnquiry();
            flag = 1;
            txtFromDateEnquiry.Focus();
        }

        protected void btnMemberEndFollowupNioti_Click(object sender, ImageClickEventArgs e)
        {
            divPaymentFollowup.Visible = false;
            divEnquiryFollowup.Visible = false;
            divMemberEndFollowup.Visible = true;
            divMembershipFollowup.Visible = false;
            divUpgradeFollowup.Visible = false;
            divExistingFollowup.Visible = false;
            divMeasurementFollowup.Visible = false;
            gvEnquiry.Visible = false;
            gvUpgradeDetails.Visible = false;
            gvMeasurementDetails.Visible = false;
            gvMemberDetails.Visible = false;
            //gvMemEndDetails.Visible = false;
            gvBalPayDetails.Visible = false;
            gvFollowupDetails.Visible = false;
            bindDDLPackageMemberEnd();
            GridBindMemberEnd();
            flag = 3;
            txtFromDateMemberEnd.Focus();
        }

        protected void btnUpgradFollowupNioti_Click(object sender, ImageClickEventArgs e)
        {
            divPaymentFollowup.Visible = false;
            divEnquiryFollowup.Visible = false;
            divMemberEndFollowup.Visible = false;
            divMembershipFollowup.Visible = false;
            divExistingFollowup.Visible = false;
            divUpgradeFollowup.Visible = true;
            divMeasurementFollowup.Visible = false;
            gvEnquiry.Visible = false;
            //gvUpgradeDetails.Visible = false;
            gvMemberDetails.Visible = false;
            gvMemEndDetails.Visible = false;
            gvBalPayDetails.Visible = false;
            gvFollowupDetails.Visible = false;
            gvMeasurementDetails.Visible = false;

            bindDDLPackage();
            ddlPackageUpgrade.SelectedValue = "--Select--";
            obBalViewBalancePaymentFollowup.Action = "Search";
            obBalViewBalancePaymentFollowup.Category = "All";
            GridBindUpgrade();
            flag = 1;
            txtFromDateUpgrade.Focus();
        }

        protected void btnMembershipFollowupNioti_Click(object sender, ImageClickEventArgs e)
        {
            divPaymentFollowup.Visible = false;
            divEnquiryFollowup.Visible = false;
            divMemberEndFollowup.Visible = false;
            divMembershipFollowup.Visible = true;
            divExistingFollowup.Visible = false;
            divUpgradeFollowup.Visible = false;
            divMeasurementFollowup.Visible = false;
            gvEnquiry.Visible = false;
            gvUpgradeDetails.Visible = false;
            //gvMemberDetails.Visible = false;
            gvMemEndDetails.Visible = false;
            gvBalPayDetails.Visible = false;
            gvFollowupDetails.Visible = false;
            gvMeasurementDetails.Visible = false;
            GridBindMemberFollowup();
            flag = 3;
            txtFromDateMemberFollowup.Focus();
        }
        
        protected void btnMeasurementFollowupNioti_Click(object sender, ImageClickEventArgs e)
        {
            divPaymentFollowup.Visible = false;
            divEnquiryFollowup.Visible = false;
            divMemberEndFollowup.Visible = false;
            divMembershipFollowup.Visible = false;
            divMeasurementFollowup.Visible = true;
            divExistingFollowup.Visible = false;
            divUpgradeFollowup.Visible = false;

            gvEnquiry.Visible = false;
            gvUpgradeDetails.Visible = false;
            //gvMemberDetails.Visible = false;
            gvMemEndDetails.Visible = false;
            gvBalPayDetails.Visible = false;
            gvFollowupDetails.Visible = false;

            GridBindMeasurementFollowup();
            flag = 3;
        }

        protected void btnExistingFollowup_Click(object sender, ImageClickEventArgs e)
        {
            divPaymentFollowup.Visible = false;
            divEnquiryFollowup.Visible = false;
            divMemberEndFollowup.Visible = false;
            divMembershipFollowup.Visible = false;
            divExistingFollowup.Visible = true;
            divMeasurementFollowup.Visible = false;
            gvEnquiry.Visible = false;
            gvUpgradeDetails.Visible = false;
            gvMemberDetails.Visible = false;
            gvMemEndDetails.Visible = false;
            gvBalPayDetails.Visible = false;
            gvMeasurementDetails.Visible = false;
            //gvFollowupDetails.Visible = false;

            obBalAllFollowup.Action = "Search";
            obBalAllFollowup.DateCategory = "Followup Date";
            obBalAllFollowup.Category = "All";
            GridBindExistingFollowup();
            flag = 1;
            txtFromDateExistingFoll.Focus();
        }

        #region--------------------------------Existing Followup--------------------------------
        public void GridBindExistingFollowup()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                obBalAllFollowup.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDateExistingFoll.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                obBalAllFollowup.ToDate = Todate;

                if (Fromdate > Todate)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should be Less Than To Date !!!','Information');", true);
                    return;
                }
                obBalAllFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalAllFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                
                dt = obBalAllFollowup.SearchPaymentFollowup();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    gvFollowupDetails.Visible = true;
                    gvBalPayDetails.Visible = false;
                    gvMemEndDetails.Visible = false;
                    gvMemberDetails.Visible = false;
                    gvUpgradeDetails.Visible = false;
                    gvEnquiry.Visible = false;

                    gvFollowupDetails.DataSource = dt;
                    gvFollowupDetails.DataBind();
                    ViewState["ExistingFollDetails"] = dt;
                }
                else
                {
                    gvFollowupDetails.Visible = true;
                    gvBalPayDetails.Visible = false;
                    gvMemEndDetails.Visible = false;
                    gvMemberDetails.Visible = false;
                    gvUpgradeDetails.Visible = false;
                    gvEnquiry.Visible = false;

                    gvFollowupDetails.DataSource = null;
                    gvFollowupDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available !!!','Information');", true);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAllFollowup_Command(object sender, CommandEventArgs e)
        {

        }
        protected void gvFollowupDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //if (flag == 1)
            //{
            //    obBalViewBalancePaymentFollowup.Category = "All";
            //    obBalViewBalancePaymentFollowup.Action = "Search";
            //    gvBalPayDetails.PageIndex = e.NewPageIndex;
            //    GridBindPayment();
            //}
            //else if (flag == 2)
            //{
            //    obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            //    obBalViewBalancePaymentFollowup.Category = ddlCategoryPayment.SelectedValue;
            //    gvBalPayDetails.PageIndex = e.NewPageIndex;
            //    SearchByCategoryPayment();
            //}
            //else if (flag == 3)
            //{
            //    obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
            //    obBalViewBalancePaymentFollowup.Category = ddlCategoryPayment.SelectedValue;
            //    gvBalPayDetails.PageIndex = e.NewPageIndex;
            //    SearchByCategoryPayment();
            //}

        }

        public void SearchByCategoryExistingFollowup()
        {
            if (ddlCategoryExistingFollowup.SelectedValue == "Member ID")
            {
                obBalAllFollowup.SearchText = txtSearchExistingFollowup.Text;
            }
            if (ddlCategoryExistingFollowup.SelectedValue == "Name")
            {
                obBalAllFollowup.SearchText = txtSearchExistingFollowup.Text;
            }
            if (ddlCategoryExistingFollowup.SelectedValue == "Gender")
            {
                obBalAllFollowup.SearchText = txtSearchExistingFollowup.Text;
            }
            if (ddlCategoryExistingFollowup.SelectedValue == "Contact")
            {
                obBalAllFollowup.SearchText = txtSearchExistingFollowup.Text;
            }
            if (ddlCategoryExistingFollowup.SelectedValue == "Followup Type")
            {
                obBalAllFollowup.SearchText = txtSearchExistingFollowup.Text;
            }
            if (ddlCategoryExistingFollowup.SelectedValue == "Call Response")
            {
                obBalAllFollowup.SearchText = txtSearchExistingFollowup.Text;
            }
            if (ddlCategoryExistingFollowup.SelectedValue == "Rating")
            {
                obBalAllFollowup.SearchText = txtSearchExistingFollowup.Text;
            }
            GridBindExistingFollowup();
        }

        protected void txtSearchExistingFollowup_TextChanged(object sender, EventArgs e)
        {
            obBalAllFollowup.Action = "SearchByCategory";
            obBalAllFollowup.Category = ddlCategoryExistingFollowup.SelectedValue;
            obBalAllFollowup.DateCategory = "Followup Date";
            SearchByCategoryExistingFollowup();
            flag = 2;
        }

        protected void btnSearchExistingFollowup_Click(object sender, EventArgs e)
        {
            obBalAllFollowup.Category = "All";
            obBalAllFollowup.Action = "Search";
            obBalAllFollowup.DateCategory = "Followup Date";
            GridBindExistingFollowup();
            flag = 1;
        }

        protected void btnSearchByDateCategoryExistingFollowup_Click(object sender, EventArgs e)
        {
            if (ddlCategoryExistingFollowup.SelectedValue == "--Select--" && txtSearchExistingFollowup.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                ddlCategoryExistingFollowup.Focus();
                return;
            }
            else if (ddlCategoryExistingFollowup.SelectedValue != "--Select--" && txtSearchExistingFollowup.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                txtSearchExistingFollowup.Focus();
                return;
            }
            else
            {
                obBalAllFollowup.Action = "SearchByDateCategory";
                obBalAllFollowup.Category = ddlCategoryExistingFollowup.SelectedValue;
                if (ddlDateCategoryExistingFollowup.SelectedValue == "--Select--")
                    obBalAllFollowup.DateCategory = "Followup Date";
                else if (ddlDateCategoryExistingFollowup.SelectedValue == "Followup Date")
                    obBalAllFollowup.DateCategory = "Followup Date";
                else if (ddlDateCategoryExistingFollowup.SelectedValue == "Next Followup Date")
                    obBalAllFollowup.DateCategory = "Next Followup Date";
                SearchByCategoryExistingFollowup();
                flag = 3;
            }
        }

        protected void btnClearExistingFollowup_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString();

            txtSearchExistingFollowup.Text = "";
            AssignTodaysDate();
            ddlDateCategoryExistingFollowup.SelectedValue = "--Select--";
            ddlCategoryExistingFollowup.SelectedValue = "--Select--";
            txtFromDateExistingFoll.Focus();
        }

        protected void ddlDateCategoryExistingFollowup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateCategoryExistingFollowup.SelectedValue == "Payment Date")
                obBalAllFollowup.DateCategory = "Followup Date";
            else
                obBalAllFollowup.DateCategory = "Next Followup Date";

            obBalAllFollowup.Action = "Search";
            obBalAllFollowup.Category = "All";
            GridBindExistingFollowup();
                
        }
        
        protected void btnExportToExcelExistingFollowup_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
  
        protected void ExportToExcel()
        {
            try
            {
                if (ViewState["ExistingFollDetails"] != null)
                {
                    dt = (DataTable)ViewState["ExistingFollDetails"]; 
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
                            gvFollowupDetails.Columns[0].Visible = false;
                            gvFollowupDetails.Columns[1].Visible = false;
                            gvFollowupDetails.AllowPaging = false;
                            gvFollowupDetails.DataSource = dt;
                            gvFollowupDetails.DataBind();
                            gvFollowupDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvFollowupDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvFollowupDetails.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvFollowupDetails.Rows)
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
                            gvFollowupDetails.GridLines = GridLines.Both;
                            gvFollowupDetails.RenderControl(hw);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Data GridView is Empty,Can Not Export !!!.','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Data GridView is Empty,Can Not Export !!!.','Error');", true);
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

        #region----------------------------------Payment Followup---------------------------
       
        public void GridBindPayment()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDatePayment.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                obBalViewBalancePaymentFollowup.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDatePayment.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }

                if (Fromdate > Todate)
                {
                    gvBalPayDetails.DataSource = null;
                    gvBalPayDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should be Less Than To Date !!!','Information');", true);
                    return;
                }
                else
                {
                    obBalViewBalancePaymentFollowup.ToDate = Todate;
                    obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                    dt = obBalViewBalancePaymentFollowup.Search();
                    lblCount.Text = "0";
                    if (dt.Rows.Count > 0)
                    {

                        count = dt.Rows.Count;
                        lblCount.Text = count.ToString();

                        gvBalPayDetails.Visible = true;
                        gvBalPayDetails.DataSource = dt;
                        gvBalPayDetails.DataBind();
                        ViewState["PayFollDetails"] = dt;
                    }
                    else
                    {
                        ViewState["PayFollDetails"] = null;
                        gvBalPayDetails.Visible = false;
                        gvBalPayDetails.DataSource = dt;
                        gvBalPayDetails.DataBind();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Found !!!.','Information');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }

        }

        public void SearchByCategoryPayment()
        {
            
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            if (ddlCategoryPayment.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
            }
            if (ddlCategoryPayment.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
            }
            if (ddlCategoryPayment.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
            }
            if (ddlCategoryPayment.SelectedValue == "Gender")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
            }

            GridBindPayment();
        }

        protected void gvBalPayDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvBalPayDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Category = "All";
                obBalViewBalancePaymentFollowup.Action = "Search";
                gvBalPayDetails.PageIndex = e.NewPageIndex;
                GridBindPayment();
            }
            else if (flag == 2)
            {
                gvBalPayDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
                obBalViewBalancePaymentFollowup.Category = ddlCategoryPayment.SelectedValue;
                gvBalPayDetails.PageIndex = e.NewPageIndex;
                SearchByCategoryPayment();
            }
            else if (flag == 3)
            {
                gvBalPayDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                obBalViewBalancePaymentFollowup.Category = ddlCategoryPayment.SelectedValue;
                gvBalPayDetails.PageIndex = e.NewPageIndex;
                SearchByCategoryPayment();
            }

        }

        protected void btnSearchPayment_Click(object sender, EventArgs e)
        {
            gvBalPayDetails.PageIndex = 0;
            obBalViewBalancePaymentFollowup.Category = "All";
            obBalViewBalancePaymentFollowup.Action = "Search";
            GridBindPayment();
            flag = 1;
        }

        protected void btnSearchByDateCategoryPayment_Click(object sender, EventArgs e)
        {
            gvBalPayDetails.PageIndex = 0;
            if (ddlCategoryPayment.SelectedValue == "--Select--" && txtSearchPayment.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                ddlCategoryPayment.Focus();
                return;
            }
            else if (ddlCategoryPayment.SelectedValue != "--Select--" && txtSearchPayment.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                txtSearchPayment.Focus();
                return;
            }
            else
            {
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                obBalViewBalancePaymentFollowup.Category = ddlCategoryPayment.SelectedValue;
                SearchByCategoryPayment();
                flag = 3;
            }
        }

        public void ClearPayment()
        {
            AssignTodaysDate();
            ddlCategoryPayment.SelectedValue = "--Select--";
            txtSearchPayment.Text = "";
            txtFromDatePayment.Focus();
        }

        protected void btnClearPayment_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString();
            ClearPayment();
            gvBalPayDetails.DataSource = null;
            gvBalPayDetails.DataBind();
            //ddlCategoryPayment.Focus();
        }

        protected void btnBalPayPayment_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("BalancePayment.aspx?Member_ID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNAllFollowupDetailsBal=" + HttpUtility.UrlEncode("FNAllFollowupDetailsBal".ToString()));
        }

        string FNBalPayFollDetail = "FNBalPayFollDetail";
        protected void btnFollowupPayment_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNAllFollowupDetailsBal=" + HttpUtility.UrlEncode("FNAllFollowupDetailsBal".ToString()));
        }

        protected void btnNamePayment_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNAllFollowupDetails=" + HttpUtility.UrlEncode("FNAllFollowupDetails".ToString()));
        }

        protected void txtSearchPayment_TextChanged(object sender, EventArgs e)
        {
            gvBalPayDetails.PageIndex = 0;
            obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategoryPayment.SelectedValue;
            SearchByCategoryPayment();
            flag = 2;
            btnSearchByDateCategoryPayment.Focus();
        }

        protected void btnExportToExcelPayment_Click(object sender, EventArgs e)
        {
            ExportToExcelPayment();
        }
        protected void ExportToExcelPayment()
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
        #endregion

        #region-----------------------------------Member End Followup-------------------------------
        public void bindDDLPackageMemberEnd()
        {
            try
            {
                obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalViewBalancePaymentFollowup.BindDDLPackage();
                if (dt.Rows.Count > 0)
                {
                    ddlPackageMemberEnd.DataSource = dt;
                    ddlPackageMemberEnd.Items.Clear();
                    ddlPackageMemberEnd.DataValueField = "Pack_AutoID";
                    ddlPackageMemberEnd.DataTextField = "Package";
                    ddlPackageMemberEnd.DataBind();
                    ddlPackageMemberEnd.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlPackageMemberEnd.SelectedItem.Value = "--Select--";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Package !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GridBindMemberEnd()
        {
            try
            {
               
                SetDefaultParameter();
                //obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                //obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                if (ddlDateCategoryMemberEnd.SelectedValue == "--Select--")
                    obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                else if (ddlDateCategoryMemberEnd.SelectedValue == "End Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                else if (ddlDateCategoryMemberEnd.SelectedValue == "Next Followup Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

                if (ddlPackageMemberEnd.SelectedValue != "--Select--")
                    obBalViewBalancePaymentFollowup.Pack_AutoID = Convert.ToInt32(ddlPackageMemberEnd.SelectedValue);

                obBalViewBalancePaymentFollowup.Action = "Search";
                dt = obBalViewBalancePaymentFollowup.SearchMemEndFollowup();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    ViewState["MemberEndFollDetails"] = dt;
                    gvMemEndDetails.Visible = true;
                    gvMemEndDetails.DataSource = dt;
                    gvMemEndDetails.DataBind();
                }
                else
                {
                    gvMemEndDetails.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available !!!','Information');", true);
                    return;
                }
                flag = 3;
            }
            catch (Exception ex)
            {

            }

        }

        public void SetDefaultParameter()
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateMemberEnd.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateMemberEnd.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            if (Fromdate > Todate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should be Less Than Todate !!!.','Information');", true);
                return;
            }
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        public void SearchByDateCategoryMemberEnd()
        {
            
            if (ddlDateCategoryMemberEnd.SelectedValue == "--Select--" && ddlPackageMemberEnd.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                return;
            }
            else
            {
                SetDefaultParameter();
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                if (ddlDateCategoryMemberEnd.SelectedValue == "--Select--")
                    obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                else if (ddlDateCategoryMemberEnd.SelectedValue == "End Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "End Date";
                else if (ddlDateCategoryMemberEnd.SelectedValue == "Next Followup Date")
                    obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

                if (ddlPackageMemberEnd.SelectedValue != "--Select--")
                    obBalViewBalancePaymentFollowup.Pack_AutoID = Convert.ToInt32(ddlPackageMemberEnd.SelectedValue);

                flag = 1;
                dt = obBalViewBalancePaymentFollowup.SearchMemEndFollowup();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    ViewState["MemberEndFollDetails"] = dt;
                    gvMemEndDetails.Visible = true;
                    gvMemEndDetails.DataSource = dt;
                    gvMemEndDetails.DataBind();
                }
                else
                {
                    gvMemEndDetails.Visible = true;
                    gvMemEndDetails.DataSource = null;
                    gvMemEndDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Records Not Found !!!','Error');", true);
                    return;
                }
            }
        }

        public void SearchByCategoryMemberEnd()
        {
          
            SetDefaultParameter();
            //obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            //obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            if (ddlDateCategoryMemberEnd.SelectedValue == "--Select--")
                obBalViewBalancePaymentFollowup.DateCategory = "End Date";
            else if (ddlDateCategoryMemberEnd.SelectedValue == "End Date")
                obBalViewBalancePaymentFollowup.DateCategory = "End Date";
            else if (ddlDateCategoryMemberEnd.SelectedValue == "Next Followup Date")
                obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

            if (ddlPackageMemberEnd.SelectedValue != "--Select--")
                obBalViewBalancePaymentFollowup.Pack_AutoID = Convert.ToInt32(ddlPackageMemberEnd.SelectedValue);

            flag = 2;
            dt = obBalViewBalancePaymentFollowup.SearchMemEndFollowup();
            lblCount.Text = "0";
            if (dt.Rows.Count > 0)
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                ViewState["MemberEndFollDetails"] = dt;
                gvMemEndDetails.Visible = true;
                gvMemEndDetails.DataSource = dt;
                gvMemEndDetails.DataBind();
            }
            else
            {
                gvMemEndDetails.Visible = true;
                gvMemEndDetails.DataSource = null;
                gvMemEndDetails.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Records Not Found !!!','Error');", true);
                return;

            }
        }

        protected void gvMemEndDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ddlDateCategoryMemberEnd.SelectedValue == "--Select--")
                obBalViewBalancePaymentFollowup.DateCategory = "End Date";
            else if (ddlDateCategoryMemberEnd.SelectedValue == "End Date")
                obBalViewBalancePaymentFollowup.DateCategory = "End Date";
            else if (ddlDateCategoryMemberEnd.SelectedValue == "Next Followup Date")
                obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";
            SetDefaultParameter();
            if (flag == 1)
            {
                gvMemEndDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                gvMemEndDetails.PageIndex = e.NewPageIndex;
                SearchByDateCategoryMemberEnd();
                
            }
            else if (flag == 2)
            {
                gvMemEndDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
                gvMemEndDetails.PageIndex = e.NewPageIndex;
                SearchByCategoryMemberEnd();
                
            }
            else if(flag == 3)
            {
                gvMemEndDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Action = "Search";
                gvMemEndDetails.PageIndex = e.NewPageIndex;
                GridBindMemberEnd();
                
            }

        }

        protected void btnSearchMemberEnd_Click(object sender, EventArgs e)
        {
          
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateMemberEnd.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateMemberEnd.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            GridBindMemberEnd();
            btnSearchMemberEnd.Focus();
        }

        protected void btnSearchByDateCategoryMemberEnd_Click(object sender, EventArgs e)
        {
            gvMemEndDetails.PageIndex = 0;

            if (ddlDateCategoryMemberEnd.SelectedValue != "--Selct--" && ddlPackageMemberEnd.SelectedValue != "--Select--")
            {
                SearchByDateCategoryMemberEnd();
                btnSearchByDateCategoryMemberEnd.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                return;
            }

        }

        public void Clear()
        {
            AssignTodaysDate();
            ddlDateCategoryMemberEnd.SelectedValue = "--Select--";
            ddlPackageMemberEnd.SelectedValue = "--Select--";
        }

        protected void btnClearMemberEnd_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString(); 

            Clear();
            gvMemEndDetails.DataSource = null;
            gvMemEndDetails.DataBind();
            txtFromDateMemberEnd.Focus();
        }

        string FNMemEndFollDetail = "FNMemEndFollDetail";
        protected void btnFollowupMemberEnd_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Followup.aspx?MembershipEnd_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNMemEndFollDetail=" + HttpUtility.UrlEncode(FNMemEndFollDetail.ToString()));
        }

        protected void btnNameMemberEnd_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        protected void ddlDateCategoryMemberEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMemEndDetails.PageIndex = 0;
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateMemberEnd.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateMemberEnd.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            GridBindMemberEnd();
            ddlDateCategoryMemberEnd.Focus();
        }

        protected void ddlPackageMemberEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMemEndDetails.PageIndex = 0;
            SearchByDateCategoryMemberEnd();
            ddlPackageMemberEnd.Focus();
        }

        protected void btnExportToExcelMemberEnd_Click(object sender, EventArgs e)
        {
            ExportToExcelMemberEnd();
        }
        protected void ExportToExcelMemberEnd()
        {
            try
            {
                if (ViewState["MemberEndFollDetails"] != null)
                {
                    dt = (DataTable)ViewState["MemberEndFollDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MemberEndFollDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvMemEndDetails.Columns[0].Visible = false;
                            gvMemEndDetails.Columns[1].Visible = false;
                            gvMemEndDetails.AllowPaging = false;
                            gvMemEndDetails.DataSource = dt;
                            gvMemEndDetails.DataBind();
                            gvMemEndDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMemEndDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMemEndDetails.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvMemEndDetails.Rows)
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
                            gvMemEndDetails.GridLines = GridLines.Both;
                            gvMemEndDetails.RenderControl(hw);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Gridview is Empty Can Not Export !!!.','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Can Not Export !!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
               // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region-------------------------------Membership Followup----------------------------------
        public void GridBindMemberFollowup()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                obBalViewBalancePaymentFollowup.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                obBalViewBalancePaymentFollowup.ToDate = Todate;
                if (Fromdate > Todate)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Be Less than To Date !!!.','Information');", true);
                    return;
                }
                obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                obBalViewBalancePaymentFollowup.Category = "All";
                obBalViewBalancePaymentFollowup.Action = "Search";
                dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    ViewState["MembershipFollDetails"] = dt;    
                    gvMemberDetails.Visible = true;
                    gvMemberDetails.DataSource = dt;
                    gvMemberDetails.DataBind();
                }
                else
                {
                    gvMemberDetails.DataSource = null;
                    gvMemberDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Found !!!.','Information');", true);
                    return;
                }
                flag = 3;
            }
            catch (Exception ex)
            {

            }

        }

        public void SearchByDateCategoryMemberFollowup()
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategoryMemberFollowup.SelectedValue;
            //if (ddlDateCategoryMemberFollowup.SelectedValue == "--Select--")
            //    obBalViewBalancePaymentFollowup.DateCategory = "Followup Date";
            //else if (ddlDateCategoryMemberFollowup.SelectedValue == "Followup Date")
            //    obBalViewBalancePaymentFollowup.DateCategory = "Followup Date";
            //else if (ddlDateCategoryMemberFollowup.SelectedValue == "Next Followup Date")
            //    obBalViewBalancePaymentFollowup.DateCategory = "Next Followup Date";

            if (ddlCategoryMemberFollowup.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Gender")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            flag = 1;
            dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
            lblCount.Text = "0";
            if (dt.Rows.Count > 0)
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvMemberDetails.DataSource = dt;
                gvMemberDetails.DataBind();
                ViewState["MembershipFollDetails"] = dt;    
            }
            else
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvMemberDetails.DataSource = dt;
                gvMemberDetails.DataBind();
            }
        }

        public void SearchByCategoryMemberFollowup()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategoryMemberFollowup.SelectedValue;

            if (ddlCategoryMemberFollowup.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Gender")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            flag = 2;
            dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
            lblCount.Text = "0";
            if (dt.Rows.Count > 0)
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                
                gvMemberDetails.DataSource = dt;
                gvMemberDetails.DataBind();
                ViewState["MembershipFollDetails"] = dt;    
            }
            else
            {
                gvMemberDetails.DataSource = null;
                gvMemberDetails.DataBind();
            }
        }

        protected void gvMemberDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvMemberDetails.PageIndex = e.NewPageIndex;
                SearchByDateCategoryMemberFollowup();
            }
            else if (flag == 2)
            {
                gvMemberDetails.PageIndex = e.NewPageIndex;
                SearchByCategoryMemberFollowup();
            }
            else if (flag == 3)
            {
                gvMemberDetails.PageIndex = e.NewPageIndex;
                GridBindMemberFollowup();
            }

        }

        protected void btnSearchMemberFollowup_Click(object sender, EventArgs e)
        {
            gvMemberDetails.PageIndex = 0;
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            GridBindMemberFollowup();
        }

        protected void btnSearchByDateCategoryMemberFollowup_Click(object sender, EventArgs e)
        {
            gvMemberDetails.PageIndex = 0;
            if (ddlCategoryMemberFollowup.SelectedValue == "--Select--" && txtSearchMemberFollowup.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                ddlCategoryPayment.Focus();
                return;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue != "--Select--" && txtSearchMemberFollowup.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                txtSearchPayment.Focus();
                return;
            }
            else
            {
                SearchByDateCategoryMemberFollowup();
            }
        }

        public void ClearMemberFollowup()
        {
            AssignTodaysDate();
            ddlCategoryMemberFollowup.SelectedValue = "--Select--";
            txtSearchMemberFollowup.Text = "";
        }

        protected void btnClearMemberFollowup_Click(object sender, EventArgs e)
        {
            ClearMemberFollowup();
            gvMemberDetails.DataSource = null;
            gvMemberDetails.DataBind();
            txtFromDateMemberFollowup.Focus();
        }

        string FNMemberFollDetail = "FNMemberFollDetail";
        protected void btnFollowupMemberFollowup_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Followup.aspx?Other_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNMemberFollDetail=" + HttpUtility.UrlEncode(FNMemberFollDetail.ToString()));
        }

        protected void btnNameMemberFollowup_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        protected void txtSearchMemberFollowup_TextChanged(object sender, EventArgs e)
        {
            gvMemberDetails.PageIndex = 0;
            SearchByCategoryMemberFollowup();
            btnSearchMemberFollowup.Focus();
        }

        //protected void ddlDateCategoryMemberFollowup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DateTime Fromdate;
        //    if (DateTime.TryParseExact(txtFromDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
        //    { }
        //    obBalViewBalancePaymentFollowup.FromDate = Fromdate;
        //    DateTime Todate;
        //    if (DateTime.TryParseExact(txtToDateMemberFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
        //    { }
        //    obBalViewBalancePaymentFollowup.ToDate = Todate;
        //    GridBindMemberFollowup();
        //    ddlCategoryMemberFollowup.Focus();
        //}

        protected void btnExportToExcelMembership_Click(object sender, EventArgs e)
        {
            ExportToExcelMembership();
        }
        protected void ExportToExcelMembership()
        {
            try
            {
                if (ViewState["MembershipFollDetails"] != null)
                {
                    dt = (DataTable)ViewState["MembershipFollDetails"];
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MembershipFollDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvMemberDetails.Columns[0].Visible = false;
                            gvMemberDetails.Columns[1].Visible = false;
                            gvMemberDetails.AllowPaging = false;
                            gvMemberDetails.DataSource = dt;
                            gvMemberDetails.DataBind();
                            gvMemberDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMemberDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMemberDetails.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvMemberDetails.Rows)
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
                            gvMemberDetails.GridLines = GridLines.Both;
                            gvMemberDetails.RenderControl(hw);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Gridview is Empty Can Not Export !!!.','Error');", true);
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
        #endregion

        #region-------------------------------Measurement Followup----------------------------------
        public void GridBindMeasurementFollowup()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDateMeasurementFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                obBalViewBalancePaymentFollowup.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDateMeasurementFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                if (Fromdate > Todate)
                {                    
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                    return;
                }

                obBalViewBalancePaymentFollowup.ToDate = Todate;
                obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                obBalViewBalancePaymentFollowup.Category = "All";
                obBalViewBalancePaymentFollowup.Action = "Search";
                dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
                lblCount.Text = "0";

                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    ViewState["MeasurementFollDetails"] = dt;
                    gvMeasurementDetails.Visible = true;
                    gvMeasurementDetails.DataSource = dt;
                    gvMeasurementDetails.DataBind();
                }
                else
                {
                    gvMeasurementDetails.DataSource = null;
                    gvMeasurementDetails.DataBind();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SearchByDateCategoryMeasurementFollowup()
        {
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateMeasurementFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateMeasurementFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategoryMemberFollowup.SelectedValue;

            if (ddlCategoryMemberFollowup.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMemberFollowup.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMeasurementFollowup.SelectedValue == "Gender")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            flag = 1;
            dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
            lblCount.Text = "0";
            if (dt.Rows.Count > 0)
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvMeasurementDetails.DataSource = dt;
                gvMeasurementDetails.DataBind();
                ViewState["MeasurementFollDetails"] = dt;
            }
            else
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvMeasurementDetails.DataSource = null;
                gvMeasurementDetails.DataBind();
            }
        }

        public void SearchByCategoryMeasurementFollowup()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalViewBalancePaymentFollowup.Action = "SearchByCategory";
            obBalViewBalancePaymentFollowup.Category = ddlCategoryMeasurementFollowup.SelectedValue;
            if (ddlCategoryMeasurementFollowup.SelectedValue == "Member ID")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMeasurementFollowup.SelectedValue == "Name")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMeasurementFollowup.SelectedValue == "Contact")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            else if (ddlCategoryMeasurementFollowup.SelectedValue == "Gender")
            {
                obBalViewBalancePaymentFollowup.SearchText = txtSearchMemberFollowup.Text;
            }
            flag = 2;
            dt = obBalViewBalancePaymentFollowup.SearchMemshipFollowup();
            lblCount.Text = "0";
            if (dt.Rows.Count > 0)
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvMeasurementDetails.DataSource = dt;
                gvMeasurementDetails.DataBind();
                ViewState["MeasurementFollDetails"] = dt;
            }
            else
            {
                count = dt.Rows.Count;
                lblCount.Text = count.ToString();
                gvMeasurementDetails.DataSource = null;
                gvMeasurementDetails.DataBind();
            }
        }

        protected void gvMeasurementDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvMeasurementDetails.PageIndex = e.NewPageIndex;
                SearchByDateCategoryMeasurementFollowup();
            }
            else if (flag == 2)
            {
                gvMeasurementDetails.PageIndex = e.NewPageIndex;
                SearchByCategoryMeasurementFollowup();
            }
            else if (flag == 3)
            {
                gvMeasurementDetails.PageIndex = e.NewPageIndex;
                GridBindMeasurementFollowup();
            }

        }

        protected void btnSearchMeasurementFollowup_Click(object sender, EventArgs e)
        {
            gvMeasurementDetails.PageIndex = 0;
            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDateMeasurementFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            obBalViewBalancePaymentFollowup.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDateMeasurementFollowup.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            obBalViewBalancePaymentFollowup.ToDate = Todate;
            GridBindMeasurementFollowup();
        }

        protected void btnSearchByDateCategoryMeasurementFollowup_Click(object sender, EventArgs e)
        {
            gvMeasurementDetails.PageIndex = 0;
            SearchByDateCategoryMeasurementFollowup();
        }

        public void ClearMeasurementFollowup()
        {
            AssignTodaysDate();
            ddlCategoryMeasurementFollowup.SelectedValue = "--Select--";
            txtSearchMeasurementFollowup.Text = "";
        }

        protected void btnClearMeasurementFollowup_Click(object sender, EventArgs e)
        {
            ClearMeasurementFollowup();
            gvMeasurementDetails.DataSource = null;
            gvMeasurementDetails.DataBind();
            txtFromDateMeasurementFollowup.Focus();
        }

        string FNMeasurementFollDetail = "FNMeasurementFollDetail";
        protected void btnFollowupMeasurementFollowup_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Followup.aspx?Other_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNMeasurementFollDetail=" + HttpUtility.UrlEncode(FNMeasurementFollDetail.ToString()));
        }

        protected void btnNameMeasurementFollowup_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        protected void txtSearchMeasurementFollowup_TextChanged(object sender, EventArgs e)
        {
            gvMeasurementDetails.PageIndex = 0;
            SearchByCategoryMeasurementFollowup();
            btnSearchMeasurementFollowup.Focus();
        }

        protected void btnExportToExcelMeasurement_Click(object sender, EventArgs e)
        {
            ExportToExcelMeasurement();
        }
        protected void ExportToExcelMeasurement()
        {
            try
            {
                if (ViewState["MeasurementFollDetails"] != null)
                {
                    dt = (DataTable)ViewState["MeasurementFollDetails"];
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MeasurementFollDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvMeasurementDetails.Columns[0].Visible = false;
                            gvMeasurementDetails.Columns[1].Visible = false;
                            gvMeasurementDetails.AllowPaging = false;
                            gvMeasurementDetails.DataSource = dt;
                            gvMeasurementDetails.DataBind();
                            gvMeasurementDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMeasurementDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMeasurementDetails.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvMeasurementDetails.Rows)
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
                            gvMeasurementDetails.GridLines = GridLines.Both;
                            gvMeasurementDetails.RenderControl(hw);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Gridview is Empty Can Not Export !!!.','Error');", true);
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
        #endregion

        #region----------------------------------Upgrade Followup---------------------------
        public void bindDDLPackage()
        {
            try
            {
                obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalViewBalancePaymentFollowup.BindDDLPackage();
                if (dt.Rows.Count > 0)
                {
                    ddlPackageUpgrade.DataSource = dt;
                    ddlPackageUpgrade.Items.Clear();
                    ddlPackageUpgrade.DataValueField = "Pack_AutoID";
                    ddlPackageUpgrade.DataTextField = "Package";
                    ddlPackageUpgrade.DataBind();
                    ddlPackageUpgrade.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlPackageUpgrade.SelectedItem.Value = "--Select--";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Package !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GridBindUpgrade()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDateUpgrade.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                obBalViewBalancePaymentFollowup.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDateUpgrade.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                obBalViewBalancePaymentFollowup.ToDate = Todate;
                if (Fromdate > Todate)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Be Less than To Date !!!.','Information');", true);
                    return;
                }
                obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                dt = obBalViewBalancePaymentFollowup.SearchUpgrade();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    
                    gvUpgradeDetails.Visible = true;
                    gvUpgradeDetails.DataSource = dt;
                    gvUpgradeDetails.DataBind();
                    ViewState["UpgradeDetails"] = dt;
                }
                else
                {
                    gvUpgradeDetails.Visible = false; 
                    gvUpgradeDetails.DataSource = dt;
                    gvUpgradeDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Found !!!.','Information');", true);
                    return;
                }
            }
            catch (Exception ex)
            {

            }

        }

        //public void SearchByCategoryUpgrade()
        //{
        //    obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //    obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

        //    if (ddlCategoryPayment.SelectedValue == "Member ID")
        //    {
        //        obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
        //    }
        //    if (ddlCategoryPayment.SelectedValue == "Name")
        //    {
        //        obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
        //    }
        //    if (ddlCategoryPayment.SelectedValue == "Contact")
        //    {
        //        obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
        //    }
        //    if (ddlCategoryPayment.SelectedValue == "Gender")
        //    {
        //        obBalViewBalancePaymentFollowup.SearchText = txtSearchPayment.Text;
        //    }

        //    GridBindUpgrade();
        //}

        protected void gvUpgradeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvUpgradeDetails.PageIndex = 0;
                ddlPackageUpgrade.SelectedValue = "--Select--";
                obBalViewBalancePaymentFollowup.Action = "Search";
                obBalViewBalancePaymentFollowup.Category = "All";
                gvUpgradeDetails.PageIndex = e.NewPageIndex;
                GridBindUpgrade();
                flag = 1;
            }
            else if (flag == 2)
            {
                gvUpgradeDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                obBalViewBalancePaymentFollowup.Category = "Package ID";
                gvUpgradeDetails.PageIndex = e.NewPageIndex;
                GridBindUpgrade();
                flag = 2;
            }
            else if (flag == 3)
            {
                gvUpgradeDetails.PageIndex = 0;
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                obBalViewBalancePaymentFollowup.Category = "Package ID";
                obBalViewBalancePaymentFollowup.SearchText = ddlPackageUpgrade.SelectedValue;
                gvUpgradeDetails.PageIndex = e.NewPageIndex;
                GridBindUpgrade();
                flag = 3;
            }

        }

        protected void btnSearchUpgrade_Click(object sender, EventArgs e)
        {
            gvUpgradeDetails.PageIndex = 0;
            ddlPackageUpgrade.SelectedValue = "--Select--";
            obBalViewBalancePaymentFollowup.Action = "Search";
            obBalViewBalancePaymentFollowup.Category = "All";
            GridBindUpgrade();
            flag = 1;
        }

        protected void btnSearchByDateCategoryUpgrade_Click(object sender, EventArgs e)
        {
            gvUpgradeDetails.PageIndex = 0;
            if (ddlPackageUpgrade.SelectedValue == "--Select--" )
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                return;
            }
            else
            {
                obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
                obBalViewBalancePaymentFollowup.Category = "Package ID";
                obBalViewBalancePaymentFollowup.SearchText = ddlPackageUpgrade.SelectedValue;
                GridBindUpgrade();
                flag = 2;
            }
        }

        public void ClearUpgrade()
        {
            AssignTodaysDate();
            ddlCategoryPayment.SelectedValue = "--Select--";
            txtSearchPayment.Text = "";
        }

        protected void btnClearUpgrade_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString(); 

            ClearUpgrade();
            gvUpgradeDetails.DataSource = null;
            gvUpgradeDetails.DataBind();
            ddlCategoryPayment.Focus();
        }

        protected void btnBalPayUpgrade_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("BalancePayment.aspx?Member_ID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

       // string FNBalPayFollDetail = "FNBalPayFollDetail";
        protected void btnFollowupUpgrade_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + HttpUtility.UrlEncode(Member_AutoID.ToString()) + " &FNBalPayFollDetail=" + HttpUtility.UrlEncode("FNBalPayFollDetail"));
        }

        protected void btnNameUpgrade_Command(object sender, CommandEventArgs e)
        {
            int Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
        }

        protected void btnExportToExcelUpgrade_Click(object sender, EventArgs e)
        {
            ExportToExcelUpgrade();
        }
        protected void ExportToExcelUpgrade()
        {
            try
            {
                if (ViewState["UpgradeDetails"] != null)
                {
                    dt = (DataTable)ViewState["UpgradeDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=UpgradeDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvUpgradeDetails.Columns[0].Visible = false;
                            gvUpgradeDetails.Columns[1].Visible = false;
                            gvUpgradeDetails.AllowPaging = false;
                            gvUpgradeDetails.DataSource = dt;
                            gvUpgradeDetails.DataBind();
                            gvUpgradeDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvUpgradeDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvUpgradeDetails.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvUpgradeDetails.Rows)
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
                            gvUpgradeDetails.GridLines = GridLines.Both;
                            gvUpgradeDetails.RenderControl(hw);
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

        protected void ddlPackageUpgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvUpgradeDetails.PageIndex = 0;
            obBalViewBalancePaymentFollowup.Action = "SearchByDateCategory";
            obBalViewBalancePaymentFollowup.Category = "Package ID";
            obBalViewBalancePaymentFollowup.SearchText = ddlPackageUpgrade.SelectedValue;
            GridBindUpgrade();
            flag = 3;
        }
        
        #endregion

        #region-----------------------------Enquiry Followup----------------------------------

        #region ------------- Bind Method -------------
        private void AssignID()
        {
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
        }

        private void SearchEnquiry()
        {
            if (ddlSearchEnquiry.SelectedValue.ToString() == "--Select--")
            {
                //txtSearch.Enabled = false;
                eng.Category = "All";
            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "Enquiry ID")
            {
                eng.Category = "Enquiry ID";
                eng.searchTxt = txtSearchEnquiry.Text;

            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "First Name")
            {
                eng.Category = "First Name";
                eng.searchTxt = txtSearchEnquiry.Text;
            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "Last Name")
            {
                eng.Category = "Last Name";
                eng.searchTxt = txtSearchEnquiry.Text;
            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "Contact 1")
            {
                eng.Category = "Contact 1";
                eng.searchTxt = txtSearchEnquiry.Text;
            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "Enquiry Type")
            {
                eng.Category = "Enquiry Type";
                eng.searchTxt = txtSearchEnquiry.Text;
            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "Enquiry For")
            {
                eng.Category = "Enquiry For";
                eng.searchTxt = txtSearchEnquiry.Text;
            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "Rating")
            {
                eng.Category = "Rating";
                eng.searchTxt = txtSearchEnquiry.Text;
            }
            else if (ddlSearchEnquiry.SelectedValue.ToString() == "Source Of Enquiry")
            {
                eng.Category = "SourceOfEnquiry";
                eng.searchTxt = txtSearchEnquiry.Text;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                ddlSearchEnquiry.Focus();
                return;
            }
            bindEnquiry();
        }
        public void bindEnquiry()
        {
            try
            {
                gvEnquiry.Visible = true;
                AssignID();
                dt = eng.Get_Searchfollowup();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    count = dt.Rows.Count;
                    lblCount.Text = count.ToString();
                    
                    ViewState["EnquiryDetails"] = dt;
                    gvEnquiry.DataSource = dt;
                    gvEnquiry.DataBind();
                }
                else
                {
                    gvEnquiry.DataSource = dt;
                    gvEnquiry.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvEnquiry.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvEnquiry.Columns[1].Visible = false;
                }
                if (Request.QueryString["AddMember"] != null)
                {
                    gvEnquiry.Columns[0].Visible = false;
                    gvEnquiry.Columns[1].Visible = false;
                    gvEnquiry.Columns[2].Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ------------- Check From Date And To Date Validation
        int flag1 = 0;
        protected int chkFromDateNotLessToDateEnquiry()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDateEnquiry.Text == string.Empty)
            {
                flag1 = 1;
                txtFromDateEnquiry.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter From Date !!!','Error');", true);
            }
            else if (txtFromDateEnquiry.Text == string.Empty)
            {
                flag1 = 1;
                txtToDateEnquiry.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Enter To Date !!!','Error');", true);
            }
            else
            {

                if (DateTime.TryParseExact(txtFromDateEnquiry.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FromDate))
                {
                }

                if (DateTime.TryParseExact(txtToDateEnquiry.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ToDate))
                {
                }

                if (FromDate.Date <= ToDate.Date)
                {
                    flag1 = 0;
                    eng.FromDate = FromDate;
                    eng.ToDate = ToDate;
                }
                else
                {
                    flag1 = 1;
                    txtFromDateEnquiry.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('From Date Should Not Be Greater Than To Date !!!','Error');", true);
                }
            }

            return flag1;

        }
        #endregion

        public void SearchByDateEnquiry()
        {
            flag1 = chkFromDateNotLessToDateEnquiry();

            if (flag1 == 0)
            {
                eng.Action = "SearchAll";
                eng.DateCategory = "EnquiryDate";
                bindEnquiry();
            }
        }

        #region ---------- Search Button ----------
        protected void btnSearchEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                SearchByDateEnquiry();
                flag = 1;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        #endregion

        protected void txtSearchEnquiry_TextChanged(object sender, EventArgs e)
        {
            eng.Action = "SearchByCategory";
            SearchEnquiry();
            flag = 2;
            btnEnquiryDateEnquiry.Focus();
        }

        protected void btnEnquiryDateEnquiry_Click(object sender, EventArgs e)
        {
            SearchByDateEnquiry();
            flag = 1;
        }

        protected void btnFollowupDateEnquiry_Click(object sender, EventArgs e)
        {
            flag1 = chkFromDateNotLessToDateEnquiry();

            if (flag1 == 0)
            {
                eng.Action = "SearchAll";
                eng.DateCategory = "FollowupDate";
                bindEnquiry();
                flag = 3;
            }
        }

        protected void btnEnqDtWithCategoryEnquiry_Click(object sender, EventArgs e)
        {

            flag1 = chkFromDateNotLessToDateEnquiry();

            if (flag1 == 0)
            {
                if (ddlSearchEnquiry.SelectedValue != "--Select--")
                {
                    if (txtSearchEnquiry.Text != string.Empty)
                    {
                        // eng.ToDate = Todate;
                        eng.Action = "SearchByDateCategory";
                        eng.DateCategory = "EnquiryDateWithCategory";
                        SearchEnquiry();
                        flag = 4;
                    }
                    else
                    {
                        txtSearchEnquiry.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Data On Search Field !!!.','Error');", true);
                        return;
                    }
                }
                else
                {
                    ddlSearchEnquiry.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!.','Error');", true);
                    return;
                }

            }
        }

        protected void btnFollDtWithCategoryEnquiry_Click(object sender, EventArgs e)
        {
            flag1 = chkFromDateNotLessToDateEnquiry();

            if (flag1 == 0)
            {
                if (ddlSearchEnquiry.SelectedValue != "--Select--")
                {
                    if (txtSearchEnquiry.Text != string.Empty)
                    {
                        eng.Action = "SearchByDateCategory";
                        eng.DateCategory = "FollowupDateWithCategory";
                        SearchEnquiry();
                        flag = 5;
                    }
                    else
                    {
                        txtSearchEnquiry.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Data On Search Field !!!.','Error');", true);
                        return;
                    }
                }
                else
                {
                    ddlSearchEnquiry.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!.','Error');", true);
                    return;
                }

            }

        }

        protected void btnExportToExcelEnquiry_Click(object sender, EventArgs e)
        {
            ExportToExcelEnquiry();
        }
        protected void ExportToExcelEnquiry()
        {
            try
            {

                if (ViewState["EnquiryDetails"] != null)
                {
                    dt = (DataTable)ViewState["EnquiryDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=EnquiryDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvEnquiry.Columns[0].Visible = false;
                            gvEnquiry.Columns[1].Visible = false;
                            gvEnquiry.Columns[2].Visible = false;
                            gvEnquiry.Columns[3].Visible = false;
                            gvEnquiry.AllowPaging = false;

                            gvEnquiry.DataSource = dt;
                            gvEnquiry.DataBind();
                            gvEnquiry.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvEnquiry.HeaderRow.Cells)
                            {
                                cell.BackColor = gvEnquiry.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvEnquiry.Rows)
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

                                    foreach (Control control in controls)
                                    {
                                        switch (control.GetType().Name)
                                        {
                                            case "HyperLink":
                                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                                break;

                                            case "LinkButton":
                                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                                break;

                                        }
                                        cell.Controls.Remove(control);

                                    }
                                }
                            }


                            gvEnquiry.GridLines = GridLines.Both;
                            gvEnquiry.RenderControl(hw);

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

        protected void btnClearEnquiry_Click(object sender, EventArgs e)
        {
            gvEnquiry.DataSource = null;
            gvEnquiry.DataBind();
            ddlSearchEnquiry.SelectedValue = "--Select--";
            txtSearchEnquiry.Text = "";
            txtFromDateEnquiry.Focus();
            AssignTodaysDate();
        }

        public int Enq_id;
        protected void btnEditEnquiry_Command(object sender, CommandEventArgs e)
        {
            Enq_id = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("~/AddEnquiry.aspx?Enq_AutoID=" + Enq_id + " &FNAllFollowupDetailsEnq=" + HttpUtility.UrlEncode("FNAllFollowupDetailsEnq".ToString()));
        }

        #region --------- Delete Button -------------
        protected void btnDeleteEnquiry_Command(object sender, CommandEventArgs e)
        {
            try
            {
                eng.Enq_ID = Convert.ToInt32(e.CommandArgument);
                //eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                eng.Action = "Delete";
                int i = eng.Delete();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    bindEnquiry();
                    //ddlCompany.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
                return;
            }
        }
        #endregion
        int Enq_ID1;
        protected void btnFollowupEnquiry_Command(object sender, CommandEventArgs e)
        {
            Enq_ID1 = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("EnquiryFollowup.aspx?Enq_ID=" + Enq_ID1 + " &FNAllFollowupDetailsEnq=" + HttpUtility.UrlEncode("FNAllFollowupDetailsEnq".ToString()));
        }

        bool chkEnquiryIdIsMember = false;
        protected void btnAddMemberEnquiry_Command(object sender, CommandEventArgs e)
        {

            Enq_ID1 = Convert.ToInt32(e.CommandArgument);
            AssignID();
            eng.Enq_ID = Enq_ID1;
            chkEnquiryIdIsMember = eng.Check_EnquiryIdISMember();

            if (chkEnquiryIdIsMember == true)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('This Is Already Member  !!!','Error');", true);
                return;
            }
            else
            {
                Response.Redirect("AddMember.aspx?Enq_ID=" + Enq_ID +"&FNAllFollowupDetailsEnq=" + HttpUtility.UrlEncode("FNAllFollowupDetailsEnq".ToString()), false);
            }
        }

        protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnquiry.PageIndex = 0;
            if (flag == 1)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                SearchByDateEnquiry();
            }
            else if (flag == 2)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchByCategory";
                SearchEnquiry();
            }
            else if (flag == 3)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchAll";
                eng.DateCategory = "FollowupDate";
                bindEnquiry();
            }
            else if (flag == 4)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchByDateCategory";
                eng.DateCategory = "EnquiryDateWithCategory";
                SearchEnquiry();
            }
            else if (flag == 5)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchByDateCategory";
                eng.DateCategory = "FollowupDateWithCategory";
                SearchEnquiry();
            }
        }
        #endregion

        #region--------------------------Followup----------------------------


        public void BindGridByFollowupType()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (ddlFollowupType.SelectedValue != "--Select--")
                obBalViewBalancePaymentFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            else
                obBalViewBalancePaymentFollowup.FollowupType_AutoID = 0;
            // obBalViewBalancePaymentFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            //obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
            dt = obBalViewBalancePaymentFollowup.SelectFollDetails_By_MemAutoID();
            if (dt.Rows.Count > 0)
            {
                gvFollowupDetailspopup.DataSource = dt;
                gvFollowupDetailspopup.DataBind();
            }
            else
            {
                gvFollowupDetailspopup.DataSource = null;
                gvFollowupDetailspopup.DataBind();
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvFollowupDetailspopup.Columns[0].Visible = false;
                gvFollowupDetailspopup.Columns[1].Visible = false;
            }
        }

        public void bindDDLCallRespond()
        {
            try
            {
                obBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalCallRespondMaster.Select_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlCallPesponse.DataSource = dt;
                    ddlCallPesponse.DataValueField = "CallRespond_AutoID";
                    ddlCallPesponse.DataTextField = "Name";
                    ddlCallPesponse.DataBind();
                    ddlCallPesponse.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Call Respond Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AssignDateAndTime()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                txtNextFollowupTime.Text = localTime.ToString("HH:mm");
            }
        }

        public void bindDDLFollowupPayment()
        {
            try
            {
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_Payment();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetMemberDetails(int memberid)
        {
            txtMemberID.Enabled = false;
            txtContact.Enabled = false;
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Member_AutoID = Convert.ToInt32(memberid);
            dt = objMemberDetails.SelectByID_MemberInformation();
            if (dt.Rows.Count > 0)
            {
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                txtFirst.Text = dt.Rows[0]["FName"].ToString();
                txtLast.Text = dt.Rows[0]["LName"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                txtmail.Text = dt.Rows[0]["Email"].ToString();
            }
        }

        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dt.Rows.Count != 0)
                {
                    ddlExecutive.DataSource = dt;
                    ddlExecutive.Items.Clear();
                    ddlExecutive.DataValueField = "Staff_AutoID";
                    ddlExecutive.DataTextField = "Name";
                    ddlExecutive.DataBind();
                    //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        public void setExecutive()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dt = obBalStaffRegistration.GetExecutiveByID_ByBranch();
            if (dt.Rows.Count > 0)
            {
                ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutive.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
            }
            else
            {
                dt = obBalStaffRegistration.GetExecutiveByID_WithoutBranch();
                string staffid = dt.Rows[0]["Staff_AutoID"].ToString();
                string staffnm = dt.Rows[0]["Name"].ToString();
                ddlExecutive.Items.Insert(0, new ListItem(staffnm, staffid));
                ddlExecutive.SelectedItem.Text = staffnm;
                ddlExecutive.SelectedValue = staffid;
            }
        }

       
        private void BindFollowupTypeDDL()
        {
            try
            {
                AssignIDFoll();
                objFollowup.Action = "Select_FollowupType";
                dataTable = objFollowup.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    ddlFollowupType.DataSource = dataTable;
                    ddlFollowupType.Items.Clear();
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        public void bindDDLFollowupMembershipEnd()
        {
            try
            {
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_MembershipEnd();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void AssignIDFoll()
        {
            objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objFollowup.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }
        
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != string.Empty)
                {
                    AssignIDFoll();
                    objFollowup.Member_ID = Convert.ToInt32(txtMemberID.Text.Trim());
                    objFollowup.Action = "SearchByMemberID";

                    dataTable = objFollowup.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberIdNotFound();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    }
                    txtMemberID.Focus();
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        
        private void ClearFieldMemberIdNotFound()
        {
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtContact.Text = string.Empty;
            txtmail.Text = string.Empty;
            MemberAutoID = 0;
        }
        
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != string.Empty)
                {
                    AssignIDFoll();
                    objFollowup.Contact = txtContact.Text;
                    objFollowup.Action = "SearchByContact";

                    dataTable = objFollowup.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberContNotFound();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    }
                    txtContact.Focus();
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        private void ClearFieldMemberContNotFound()
        {
            txtMemberID.Text = string.Empty;
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtmail.Text = string.Empty;
            MemberAutoID = 0;
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtMemberID.Text == string.Empty || txtContact.Text == string.Empty || ddlFollowupType.SelectedValue == "--Select--" || ddlCallPesponse.SelectedValue == "--Select--" || ddlRating.SelectedValue == "--Select--"
                    || txtNextFollowupDate.Text == string.Empty || txtNextFollowupTime.Text == string.Empty || txtComment.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Enter All Fields','Error');", true);

                    if (txtMemberID.Text == "")
                    { txtMemberID.Style.Add("border", "1px solid red "); }

                    if (txtContact.Text == "")
                    { txtContact.Style.Add("border", "1px solid red "); }

                    if (ddlFollowupType.SelectedValue == "--Select--")
                    { ddlFollowupType.Style.Add("border", "1px solid red "); }

                    if (ddlCallPesponse.SelectedValue == "--Select--")
                    { ddlCallPesponse.Style.Add("border", "1px solid red "); }

                    if (ddlRating.SelectedValue == "--Select--")
                    { ddlRating.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupDate.Text == "")
                    { txtNextFollowupDate.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupTime.Text == "")
                    { txtNextFollowupTime.Style.Add("border", "1px solid red "); }

                    if (txtComment.Text == "")
                    { txtComment.Style.Add("border", "1px solid red "); }
                }
                else
                {
                    txtMemberID.Style.Add("border", "1px solid silver  ");
                    txtContact.Style.Add("border", "1px solid silver  ");
                    ddlFollowupType.Style.Add("border", "1px solid silver  ");
                    //txtExecutive.Style.Add("border", "1px solid silver  ");
                    ddlCallPesponse.Style.Add("border", "1px solid silver  ");
                    ddlRating.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
                    txtComment.Style.Add("border", "1px solid silver  ");
                    AssignIDFoll();
                    AddParameters();

                    if (btnSave.Text == "Save")
                    {
                        objFollowup.Action = "INSERT";
                        res = objFollowup.Insert_FollowupInformation();

                        if (res > 0)
                        {
                            ClearAllField();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                            Label33_ModalPopupExtender1.Hide();
                        }

                    }
                    else if (btnSave.Text == "Update")
                    {
                        objFollowup.Action = "Update";
                        objFollowup.Followup_AutoID = Convert.ToInt32(ViewState["Followup_AutoID"]);
                        res = objFollowup.Insert_FollowupInformation();
                        if (res > 0)
                        {
                            btnSave.Text = "Save";
                            txtMemberID.Enabled = true;
                            txtContact.Enabled = true;
                            ClearAllField();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void AddParameters()
        {
            //if (Request.QueryString["BalancePayment_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
            //else if (Request.QueryString["MembershipEnd_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
            //else if (Request.QueryString["Upgrade_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Upgrade_Member_AutoID"]);
            //else if (Request.QueryString["Measurement_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Measurement_Member_AutoID"]);
            //else if (Request.QueryString["Other_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
            //else
            objFollowup.Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            objFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            objFollowup.CallRespond_AutoID = ddlCallPesponse.SelectedValue;
            objFollowup.Rating = ddlRating.SelectedValue;


            DateTime NFDate;
            if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate))
            {
            }
            if (ddlRatingEnq.SelectedValue != "Not Interested")
                objFollowup.NextFollowupDate = NFDate;
            else
                objFollowup.NextFollowupDate = null;

            DateTime FDate;
            if (DateTime.TryParseExact(lblFollowupDateTime.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FDate))
            {
            }
            objFollowup.FollowupDate = FDate;

            objFollowup.NextFollowupTime = Convert.ToDateTime(txtNextFollowupTime.Text.ToString());
            objFollowup.FollowupTime = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt"));
            objFollowup.Comment = Regex.Replace(txtComment.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objFollowup.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

        }

        private void ClearAllField()
        {
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;
            ddlFollowupType.SelectedIndex = 0;
            ddlCallPesponse.SelectedIndex = 0;
            ddlRating.SelectedIndex = 0;
            AssignDateAndTime();
            txtComment.Text = string.Empty;
        }
       
        private void BindGridViewDetails()
        {
            AssignIDFoll();

            //dataTable.Clear();
            dataTable = objFollowup.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                gvFollowupDetailspopup.DataSource = dataTable;
                gvFollowupDetailspopup.DataBind();
            }
            else
            {
                gvFollowupDetailspopup.DataSource = dataTable;
                gvFollowupDetailspopup.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvFollowupDetailspopup.Columns[0].Visible = true;
                gvFollowupDetailspopup.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvFollowupDetailspopup.Columns[0].Visible = false;
                gvFollowupDetailspopup.Columns[1].Visible = false;
            }
        }
       
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignIDFoll();
                objFollowup.Followup_AutoID = Convert.ToInt32(e.CommandArgument);
                objFollowup.Action = "DeleteByFollowupAutoID";
                int i = objFollowup.Insert_FollowupInformation();
                if (i > 0)
                {
                    //BindGridViewDetails();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    if (Request.QueryString["FNBalPayFollDetail"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                    if (Request.QueryString["FNMemEndFollDetail"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                    if (Request.QueryString["FNameMemProfile"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                }
                else if (i == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
       
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignIDFoll();
                objFollowup.Followup_AutoID = Convert.ToInt32(e.CommandArgument);
                objFollowup.Action = "GetDetailsByFollowupAutoID";

                dataTable = objFollowup.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    btnSave.Text = "Update";
                    txtMemberID.Enabled = false;
                    txtContact.Enabled = false;
                    ViewState["Followup_AutoID"] = dataTable.Rows[0]["Followup_AutoID"].ToString();
                    txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                    txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                    txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                    ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                    txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    ddlFollowupType.SelectedValue = dataTable.Rows[0]["FollowupType_AutoID"].ToString();
                    ddlCallPesponse.SelectedValue = dataTable.Rows[0]["CallRespond_AutoID"].ToString();
                    ddlRating.SelectedValue = dataTable.Rows[0]["Rating"].ToString();
                    ddlExecutive.SelectedValue = dataTable.Rows[0]["Executive_ID"].ToString();
                    if (dataTable.Rows[0]["NextFollowupDate"].ToString() != "")
                    {
                        DateTime NFDate = Convert.ToDateTime(dataTable.Rows[0]["NextFollowupDate"].ToString());
                        DateTime NFDate1;
                        if (DateTime.TryParseExact(NFDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate1))
                        {
                            txtNextFollowupDate.Text = NFDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtNextFollowupDate.Text = "";

                    //string NextFollowupDate = Convert.ToDateTime(dataTable.Rows[0]["NextFollowupDate"]).ToString("dd-MM-yyyy");
                    //txtNextFollowupDate.Text = NextFollowupDate;
                    txtNextFollowupTime.Text = dataTable.Rows[0]["NextFollowupTime"].ToString();
                    DateTime todaydate;
                    if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy");
                    }
                    txtComment.Text = dataTable.Rows[0]["Comment"].ToString();
                }

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllField();
            txtMemberID.Style.Add("border", "1px solid silver  ");
            txtContact.Style.Add("border", "1px solid silver  ");
            ddlFollowupType.Style.Add("border", "1px solid silver  ");
            //txtExecutive.Style.Add("border", "1px solid silver  ");
            ddlCallPesponse.Style.Add("border", "1px solid silver  ");
            ddlRating.Style.Add("border", "1px solid silver  ");
            txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
            txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
            txtComment.Style.Add("border", "1px solid silver  ");
        }


        protected void gvFollowupDetailspopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFollowupDetailspopup.PageIndex = e.NewPageIndex;
            BindGridViewDetails();
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
        }

        protected void ddlRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRating.SelectedValue == "Not Interested")
                txtNextFollowupDate.Enabled = false;
            else
                txtNextFollowupDate.Enabled = true;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            flagpopup = 0;
            Label33_ModalPopupExtender1.Hide();
        }
        #endregion

        #region--------------------------Enquiry Followup----------------------------
        public void GetMemberDetailsByFollAutoIDEnq(int enqid)
        {
            objBalEnquiry.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalEnquiry.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalEnquiry.Enq_FollAutoID = Convert.ToInt32(enqid);
            dt = objBalEnquiry.GetMemberDetailsByFollAutoID();
            if (dt.Rows.Count > 0)
            {
                lblNameEnq.Text = dt.Rows[0]["FName"].ToString();
                lblContactEnq.Text = dt.Rows[0]["Contact1"].ToString();
                ViewState["enqautoid"] = dt.Rows[0]["Enq_AutoID"].ToString();

                if (dt.Rows[0]["DOB"].ToString() != "")
                {
                    DateTime dobdate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                    DateTime dobdate1;
                    if (DateTime.TryParseExact(dobdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dobdate1))
                    {
                        lbldOBEnq.Text = dobdate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    lbldOBEnq.Text = "";

                lblGenderEnq.Text = dt.Rows[0]["Gender"].ToString();
            }
        }

        public void GetMemberDetailsEnq(int enqid)
        {
            objBalEnquiry.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalEnquiry.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalEnquiry.Enq_ID = Convert.ToInt32(enqid);
            dt = objBalEnquiry.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                lblNameEnq.Text = dt.Rows[0]["FName"].ToString();
                lblContactEnq.Text = dt.Rows[0]["Contact1"].ToString();

                if (dt.Rows[0]["DOB"].ToString() != "")
                {
                    DateTime dobdate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                    DateTime dobdate1;
                    if (DateTime.TryParseExact(dobdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dobdate1))
                    {
                        lbldOBEnq.Text = dobdate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    lbldOBEnq.Text = "";

                lblGenderEnq.Text = dt.Rows[0]["Gender"].ToString();
            }
        }

        public void bindDDLCallRespondEnq()
        {
            try
            {
                obBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalCallRespondMaster.Select_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlCallResponseEnq.DataSource = dt;
                    ddlCallResponseEnq.DataValueField = "CallRespond_AutoID";
                    ddlCallResponseEnq.DataTextField = "Name";
                    ddlCallResponseEnq.DataBind();
                    ddlCallResponseEnq.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Call Respond Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLFollowupTypeEnq()
        {
            try
            {
                objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objEnqFlw.Select_FollowupTypeMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupTypeEnq.DataSource = dt;
                    ddlFollowupTypeEnq.DataValueField = "FollowupType_AutoID";
                    ddlFollowupTypeEnq.DataTextField = "Name";
                    ddlFollowupTypeEnq.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLExecutiveEnq()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dt.Rows.Count != 0)
                {
                    ddlExecutiveEnq.DataSource = dt;
                    ddlExecutiveEnq.Items.Clear();
                    ddlExecutiveEnq.DataValueField = "Staff_AutoID";
                    ddlExecutiveEnq.DataTextField = "Name";
                    ddlExecutiveEnq.DataBind();
                    //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlExecutiveEnq.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void setExecutiveEnq()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dt = obBalStaffRegistration.GetExecutiveByID_ByBranch();
            if (dt.Rows.Count > 0)
            {
                ddlExecutiveEnq.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutiveEnq.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
            }
            else
            {
                dt = obBalStaffRegistration.GetExecutiveByID_WithoutBranch();
                string staffid = dt.Rows[0]["Staff_AutoID"].ToString();
                string staffnm = dt.Rows[0]["Name"].ToString();
                ddlExecutiveEnq.Items.Insert(0, new ListItem(staffnm, staffid));
                ddlExecutiveEnq.SelectedItem.Text = staffnm;
                ddlExecutiveEnq.SelectedValue = staffid;
            }
        }

        public void AssignDateAndTimeEnq()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                lblFollwupDateEnq.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDateEnq.Text = todaydate.ToString("dd-MM-yyyy");

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                txtNextFollowupTimeEnq.Text = localTime.ToString("HH:mm");
            }
        }

        private void AssignIDEnq()
        {
            objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objEnqFlw.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        private void ClearAllFieldEnq()
        {
            AssignDateAndTime();
            chkExecutiveEnq.Checked = true;
            ddlExecutiveEnq.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutiveEnq.Enabled = false;
            ddlCallResponseEnq.SelectedValue = "--Select--";
            //ddlCallPesponse.SelectedItem.Text = "--Select--";
            ddlRatingEnq.SelectedIndex = 0;
            //txtNextFollowupTime.Text = string.Empty;
            txtCommentEnq.Text = string.Empty;
            bindDDLFollowupTypeEnq();

        }

        private void AddParametersEnq()
        {
            if (ViewState["EnqAutoID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
            }
            if (Request.QueryString["FNameViewEnqFoll"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["enqautoid"]);
            }
            else if (Request.QueryString["Data"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["Auto_ID"]);
            }
            else if (Request.QueryString["Enq_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
            }
            objEnqFlw.CallResponse_AutoID = Convert.ToInt32(ddlCallResponseEnq.SelectedValue);
            objEnqFlw.Rating = ddlRatingEnq.SelectedValue;
            if (ddlRatingEnq.SelectedValue != "Not Interested")
            {
                DateTime NFDate;
                if (DateTime.TryParseExact(txtNextFollowupDateEnq.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate))
                {
                    string NFDate1 = NFDate.ToString("dd-MM-yyyy");
                    objEnqFlw.NextFollowupDate = DateTime.ParseExact(NFDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
            }
            DateTime FDate;
            if (DateTime.TryParseExact(lblFollwupDateEnq.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FDate))
            {
                string FDate1 = FDate.ToString("dd-MM-yyyy");
                objEnqFlw.FollowupDate = DateTime.ParseExact(FDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            objEnqFlw.NextFollowupTime = Convert.ToDateTime(txtNextFollowupTimeEnq.Text.ToString());
            objEnqFlw.FollowupTime = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt"));
            objEnqFlw.Comment = Regex.Replace(txtCommentEnq.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objEnqFlw.Executive_ID = Convert.ToInt32(ddlExecutiveEnq.SelectedValue);
            objEnqFlw.FollowupType_AutoID = Convert.ToInt32(ddlFollowupTypeEnq.SelectedValue);
        }

        protected void btnSaveEnq_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCallResponseEnq.SelectedValue == "--Select--" || ddlRatingEnq.SelectedValue == "--Select--" || txtNextFollowupDateEnq.Text == string.Empty || txtNextFollowupTimeEnq.Text == string.Empty || txtCommentEnq.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Enter All Fields','Error');", true);

                    if (ddlCallResponseEnq.SelectedValue == "--Select--")
                    { ddlCallResponseEnq.Style.Add("border", "1px solid red "); }


                    if (ddlRatingEnq.SelectedValue == "--Select--")
                    { ddlRatingEnq.Style.Add("border", "1px solid red "); }


                    if (txtNextFollowupDateEnq.Text == "")
                    { txtNextFollowupDate.Style.Add("border", "1px solid red "); }

                    if (txtCommentEnq.Text == "")
                    { txtCommentEnq.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupTimeEnq.Text == "")
                    { txtNextFollowupTimeEnq.Style.Add("border", "1px solid red "); }
                }
                else
                {
                    ddlCallResponseEnq.Style.Add("border", "1px solid silver  ");
                    ddlRatingEnq.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupDateEnq.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupTimeEnq.Style.Add("border", "1px solid silver  ");
                    txtCommentEnq.Style.Add("border", "1px solid silver  ");

                    AssignIDEnq();
                    AddParametersEnq();
                    int res1;
                    if (btnSaveEnq.Text == "Save")
                    {
                        objEnqFlw.Action = "INSERT";
                        res1 = objEnqFlw.Insert_EnquiryFollowupInformation();

                        if (res1 > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                            ClearAllFieldEnq();
                            BindGridViewDetailsEnq();
                            flagpopup = 2;
                            Label34_ModalPopupExtender1.Hide();
                        }
                    }
                    else if (btnSaveEnq.Text == "Update")
                    {
                        objEnqFlw.Action = "Update";
                        objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(ViewState["EnqFollowup_AutoID"]);
                        res1 = objEnqFlw.Insert_EnquiryFollowupInformation();
                        if (res1 > 0)
                        {
                            btnSaveEnq.Text = "Save";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                            ClearAllFieldEnq();
                            BindGridViewDetailsEnq();

                        }
                    }
                    if (Request.QueryString["MenuEnquDetails"] != null)
                    {
                        Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                    }
                    if (Request.QueryString["MenuEnqFollowupDetails"] != null)
                    {
                        Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnqFollowupDetails=" + HttpUtility.UrlEncode("MenuEnqFollowupDetails".ToString()));
                    }
                    if (Request.QueryString["FNameSearchPage"] != null)
                    {
                        int EnqAutoId = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        Response.Redirect("SearchPage.aspx?Enq_AutoID=" + EnqAutoId + " &FNameSearchPage2=" + HttpUtility.UrlEncode("FNameSearchPage2".ToString()));
                    }
                    if (Request.QueryString["FNameViewEnqFoll"] != null)
                    {
                        Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void gridBindEnq()
        {
            dt = objEnqFlw.GetDetails();
            if (dt.Rows.Count > 0)
            {
                gvEnqFollowup.DataSource = dt;
                gvEnqFollowup.DataBind();
            }
            else
            {
                gvEnqFollowup.DataSource = dt;
                gvEnqFollowup.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvEnqFollowup.Columns[0].Visible = false;
                gvEnqFollowup.Columns[1].Visible = false;
            }
        }

        private void BindGridViewDetailsEnq()
        {
            if (ViewState["Auto_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["Auto_ID"].ToString());
            }
            else if (Request.QueryString["Enq_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
            }
            AssignIDEnq();
            objEnqFlw.Action = "BindDetailsByID";
            gridBindEnq();

        }

        protected void btnDeleteEnq_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignIDEnq();
                objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(e.CommandArgument);
                objEnqFlw.Action = "DeleteByEnqFollowupAutoID";
                int i = objEnqFlw.Insert_EnquiryFollowupInformation();
                if (i > 0)
                {
                    BindGridViewDetailsEnq();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    //BindGridViewDetailsEnq();
                }
                else if (i == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnEditEnq_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignIDEnq();
                objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(e.CommandArgument);
                objEnqFlw.Action = "GetDetailsByEnqFollowupAutoID";

                dt = objEnqFlw.GetDetails();
                if (dt.Rows.Count >= 0)
                {
                    btnSaveEnq.Text = "Update";

                    ViewState["EnqFollowup_AutoID"] = dt.Rows[0]["EnqFollowup_AutoID"].ToString();
                    ddlCallResponseEnq.SelectedItem.Value = dt.Rows[0]["CallRespond_AutoID"].ToString();
                    ddlCallResponseEnq.SelectedItem.Text = dt.Rows[0]["CallRespond"].ToString();
                    ddlRatingEnq.SelectedValue = dt.Rows[0]["Rating"].ToString();
                    string NextFollowupDate = Convert.ToDateTime(dt.Rows[0]["NextFollowupDate"]).ToString("dd-MM-yyyy");
                    txtNextFollowupDateEnq.Text = NextFollowupDate;
                    txtNextFollowupTimeEnq.Text = dt.Rows[0]["NextFollowupTime"].ToString();
                    txtCommentEnq.Text = dt.Rows[0]["Comment"].ToString();
                    ddlExecutiveEnq.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();
                    ddlFollowupTypeEnq.SelectedValue = dt.Rows[0]["FollowupType_AutoID"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearEnq_Click(object sender, EventArgs e)
        {
            ClearAllFieldEnq();
            btnSaveEnq.Text = "Save";
            ddlCallResponseEnq.Style.Add("border", "1px solid silver  ");
            ddlRatingEnq.Style.Add("border", "1px solid silver  ");
            txtNextFollowupDateEnq.Style.Add("border", "1px solid silver  ");
            txtNextFollowupTimeEnq.Style.Add("border", "1px solid silver  ");
            txtCommentEnq.Style.Add("border", "1px solid silver  ");
        }

        protected void gvEnqFollowup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnqFollowup.PageIndex = e.NewPageIndex;
            BindGridViewDetailsEnq();
            gvEnqFollowup.DataBind();
        }

        protected void chkExecutiveEnq_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutiveEnq.Checked == true)
                ddlExecutiveEnq.Enabled = false;
            else
                ddlExecutiveEnq.Enabled = true;
        }

        protected void ddlRatingEnq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRatingEnq.SelectedItem.Text == "Not Interested")
            {
                txtNextFollowupDateEnq.Enabled = false;
                txtNextFollowupDateEnq.Focus();
            }
            else
            {
                txtNextFollowupDateEnq.Enabled = true;
                txtNextFollowupDateEnq.Focus();

            }
        }

        protected void btnCancelEnq_Click(object sender, EventArgs e)
        {
            flagpopup = 0;
            Label34_ModalPopupExtender1.Hide();
        }
        #endregion

        #region -------------------grid select method-------------------------
        int MemberID1;
        int EnqID2;
        string BalancePayment_Member_AutoID = "";
        string Other_Member_AutoID = "";
        string MembershipEnd_Member_AutoID = "";
        string Enq_AutoID = "";
        protected void gvFollowupDetails_SelectedIndexChanged(object sender, EventArgs e)
        {


            GridViewRow row = gvFollowupDetails.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");
            Label txtfolltype = (Label)row.FindControl("txtFollowupType");
            if (txtfolltype.Text == "Enquiry")
            {
                flagpopup = 2;
                Label34_ModalPopupExtender1.Show();
                bindDDLFollowupTypeEnq();
                bindDDLExecutiveEnq();
                setExecutiveEnq();
                bindDDLCallRespondEnq();
                EnqID2 = Convert.ToInt32(lbl.Text);
                GetEnqAutoID_By_ID1(EnqID2);
            }
            else
            {
                flagpopup = 1;
                Label33_ModalPopupExtender1.Show();
                MemberID1 = Convert.ToInt32(lbl.Text);
                GetMemberAutoID_By_ID1(MemberID1);
                bindDDLCallRespond();
                BindFollowupTypeDDL();  // Bind Followup Type Drop Down List       
                bindDDLExecutive();    // Assign Executive Name
                setExecutive();
                AssignDateAndTime();
            }
            if (txtfolltype.Text == "Enquiry")
            {
                Enq_AutoID = ViewState["EnqAutoID"].ToString();
            }
            else if (txtfolltype.Text == "Other")
            {
                Other_Member_AutoID = ViewState["MemAutoID"].ToString();
            }
            else if (txtfolltype.Text == "Payment")
            {
                BalancePayment_Member_AutoID = ViewState["MemAutoID"].ToString();
            }
            if (Request.QueryString["Member_ID"] != null)
            {
                int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                GetMemberDetails(memberid);
            }

            if (BalancePayment_Member_AutoID != "")
            {
                bindDDLFollowupPayment();
                int Member_AutoID = Convert.ToInt32(BalancePayment_Member_AutoID);
                objFollowup.Member_AutoID = Member_AutoID;
                ddlFollowupType.Enabled = false;
                GetMemberDetails(Member_AutoID);
                obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(BalancePayment_Member_AutoID);
                BindGridByFollowupType();
            }
            if (Other_Member_AutoID != "")
            {

                BindFollowupTypeDDL();
                int Member_AutoID = Convert.ToInt32(Other_Member_AutoID);
                objFollowup.Member_AutoID = Member_AutoID;
                GetMemberDetails(Member_AutoID);
                obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Other_Member_AutoID);
                BindGridByFollowupType();
            }
            if (Enq_AutoID != "")
            {
                int enqid = Convert.ToInt32(ViewState["EnqAutoID"]);
                GetMemberDetailsEnq(enqid);
                lblFollwupDateEnq.Text = DateTime.Now.ToString("HH:MM tt");
                objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
                AssignDateAndTimeEnq();
                //EnquiryInfo();
                ddlCallResponseEnq.Focus();
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
                objEnqFlw.Action = "BindDetailsByID";
                GetMemberDetailsByFollAutoIDEnq(enqid);
                gridBindEnq();
            }
        }

        BalDashBoard dash = new BalDashBoard();
        public void GetMemberAutoID_By_ID1(int MemberID1)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dash.MemberId1 = Convert.ToInt32(MemberID1);
            dt = dash.GetMemberAutoID();
            if (dt.Rows.Count > 0)
            {
                ViewState["MemAutoID"] = dt.Rows[0]["Member_AutoID"];
            }
        }
        public void GetEnqAutoID_By_ID1(int EnqID1)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dash.Enq_ID1 = Convert.ToInt32(EnqID1);
            dt = dash.getEnqAutoID();
            if (dt.Rows.Count > 0)
            {
                ViewState["EnqAutoID"] = dt.Rows[0]["Enq_AutoID"];
            }
        }
        #endregion

        protected void gvBalPayDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvBalPayDetails.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");
            
                flagpopup = 1;
                Label33_ModalPopupExtender1.Show();
                MemberID1 = Convert.ToInt32(lbl.Text);
                GetMemberAutoID_By_ID1(MemberID1);
                bindDDLCallRespond();
                BindFollowupTypeDDL();  // Bind Followup Type Drop Down List       
                bindDDLExecutive();    // Assign Executive Name
                setExecutive();
                AssignDateAndTime();
                BalancePayment_Member_AutoID = ViewState["MemAutoID"].ToString();
                bindDDLFollowupPayment();
                int Member_AutoID = Convert.ToInt32(BalancePayment_Member_AutoID);
                objFollowup.Member_AutoID = Member_AutoID;
                ddlFollowupType.Enabled = false;
                GetMemberDetails(Member_AutoID);
                obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(BalancePayment_Member_AutoID);
                BindGridByFollowupType();
        }

        protected void gvMemEndDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvMemEndDetails.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");

            flagpopup = 1;
            Label33_ModalPopupExtender1.Show();
            MemberID1 = Convert.ToInt32(lbl.Text);
            GetMemberAutoID_By_ID1(MemberID1);
            bindDDLCallRespond();
            BindFollowupTypeDDL();  // Bind Followup Type Drop Down List       
            bindDDLExecutive();    // Assign Executive Name
            setExecutive();
            AssignDateAndTime();

            bindDDLFollowupMembershipEnd();
            int Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            objFollowup.Member_AutoID = Member_AutoID;
            ddlFollowupType.Enabled = false;
            GetMemberDetails(Member_AutoID);
            obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            BindGridByFollowupType();
            
        }

        protected void gvMemberDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvMemberDetails.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");

            flagpopup = 1;
            Label33_ModalPopupExtender1.Show();
            MemberID1 = Convert.ToInt32(lbl.Text);
            GetMemberAutoID_By_ID1(MemberID1);
            bindDDLCallRespond();
            BindFollowupTypeDDL();  // Bind Followup Type Drop Down List
            bindDDLExecutive();    // Assign Executive Name
            setExecutive();
            AssignDateAndTime();

            BindFollowupTypeDDL();
            int Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            objFollowup.Member_AutoID = Member_AutoID;
            GetMemberDetails(Member_AutoID);
            obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            BindGridByFollowupType();

        }

        protected void gvUpgradeDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvUpgradeDetails.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");
            flagpopup = 1;
            Label33_ModalPopupExtender1.Show();
            MemberID1 = Convert.ToInt32(lbl.Text);
            GetMemberAutoID_By_ID1(MemberID1);
            bindDDLCallRespond();
            BindFollowupTypeDDL();  // Bind Followup Type Drop Down List
            bindDDLExecutive();    // Assign Executive Name
            setExecutive();
            AssignDateAndTime();

            bindDDLFollowupUpgrade();
            int Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            objFollowup.Member_AutoID = Member_AutoID;
            GetMemberDetails(Member_AutoID);
            obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            BindGridByFollowupType();
        }

        protected void gvEnquiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvEnquiry.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");
            flagpopup = 2;
            Label34_ModalPopupExtender1.Show();
            bindDDLFollowupTypeEnq();
            bindDDLExecutiveEnq();
            setExecutiveEnq();
            bindDDLCallRespondEnq();
            EnqID2 = Convert.ToInt32(lbl.Text);
            GetEnqAutoID_By_ID1(EnqID2);

            int enqid = Convert.ToInt32(ViewState["EnqAutoID"]);
            GetMemberDetailsEnq(enqid);
            lblFollwupDateEnq.Text = DateTime.Now.ToString("HH:MM tt");
            objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
            AssignDateAndTimeEnq();
            //EnquiryInfo();
            ddlCallResponseEnq.Focus();
            objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
            objEnqFlw.Action = "BindDetailsByID";
            GetMemberDetailsByFollAutoIDEnq(enqid);
            gridBindEnq();
        }

        protected void gvMeasurementDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvMeasurementDetails.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");
            flagpopup = 1;
            Label33_ModalPopupExtender1.Show();
            MemberID1 = Convert.ToInt32(lbl.Text);
            GetMemberAutoID_By_ID1(MemberID1);
            bindDDLCallRespond();
            BindFollowupTypeDDL();  // Bind Followup Type Drop Down List
            bindDDLExecutive();    // Assign Executive Name
            setExecutive();
            AssignDateAndTime();

            bindDDLFollowupMeasurement();
            int Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            objFollowup.Member_AutoID = Member_AutoID;
            GetMemberDetails(Member_AutoID);
            obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            BindGridByFollowupType();
        }

        public void bindDDLFollowupUpgrade()
        {
            try
            {
                ddlFollowupType.Enabled = false;
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_Upgrade();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLFollowupMeasurement()
        {
            try
            {
                ddlFollowupType.Enabled = false;
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_Measurement();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}