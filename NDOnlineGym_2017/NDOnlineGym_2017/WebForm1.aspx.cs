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
    public partial class WebForm1 : System.Web.UI.Page
    {
        BalReports objreport = new BalReports();
        DataTable dt = new DataTable();
        int Flag;
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Text = "20-11-2017";
            DateTime today;
            if (DateTime.TryParseExact(TextBox1.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out today))
            {
                string EnqDate1 = today.ToString("dd-MM-yyyy");
                DateTime birthday = DateTime.ParseExact(EnqDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                GetAge(birthday);
            }

           
        }

        private string GetAge(DateTime birthday)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(birthday).Ticks).Year - 1;
            DateTime dtPastYearDate = birthday.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (dtPastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (dtPastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(dtPastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(dtPastYearDate).Hours;
            int Minutes = Now.Subtract(dtPastYearDate).Minutes;
            int Seconds = Now.Subtract(dtPastYearDate).Seconds;
            TextBox3.Text = String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
                               Years, Months, Days, Hours, Seconds);
            return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
                                Years, Months, Days, Hours, Seconds);
           

        }

        public void getAllCollection()
        {
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
        //            gvAllCollectionReport.DataSource = null;
        //            gvAllCollectionReport.DataBind();
        //        }
        //        else
        //        {

        //            DataSet ds = new DataSet();
        //            LabelEnable();
        //            ds = objreport.Collection_TotalSum();
        //            ds.Tables[0].TableName = "TableGridData";
        //            ds.Tables[1].TableName = "TableSumTotal";
        //            ds.Tables[2].TableName = "TableSumPaid";
        //            ds.Tables[3].TableName = "TableSumBalance";
        //            ds.Tables[4].TableName = "TableSumDiscount";
        //            ds.Tables[5].TableName = "TableSumCash";
        //            ds.Tables[6].TableName = "TableSumCard";
        //            ds.Tables[7].TableName = "TableSumCheque";

        //            if (ds.Tables.Count > 0)
        //            {
        //                LabelEnable();
        //                gvAllCollectionReport.DataSource = ds.Tables["TableGridData"];
        //                gvAllCollectionReport.DataBind();
        //                lblTotalAmt.Text = ds.Tables["TableSumTotal"].Compute("sum(TotalFeeDue)", "").ToString();
        //                lblTotalpaid.Text = ds.Tables["TableSumPaid"].Compute("sum(TotalPaid)", "").ToString();
        //                lblTotalDisc.Text = ds.Tables["TableSumDiscount"].Compute("sum(Discount)", "").ToString();
        //                lblTotalBalance1.Text = ds.Tables["TableSumBalance"].Compute("sum(Balance)", "").ToString();
        //                lblToatalCash.Text = ds.Tables["TableSumCash"].Compute("sum(TotalPaid)", "").ToString();
        //                lblToatalCard.Text = ds.Tables["TableSumCard"].Compute("sum(TotalPaid)", "").ToString();
        //                lblToatalCheck.Text = ds.Tables["TableSumCheque"].Compute("sum(TotalPaid)", "").ToString();
        //                gvAllCollectionReport.Style["width"] = "100%";
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
        //                gvAllCollectionReport.DataSource = dt;
        //                gvAllCollectionReport.DataBind();
        //                LabelDisable();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }
        //}
    }
}