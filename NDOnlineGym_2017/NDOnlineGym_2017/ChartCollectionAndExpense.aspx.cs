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
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;

namespace NDOnlineGym_2017
{
    public partial class ChartCollectionAndExpense : System.Web.UI.Page
    {
        BalChartBLL objChart = new BalChartBLL();

        public double jan = 0;
        public double jans = 0;
        public int month;
        public double year;
        public double payment;
        public double exp;
        protected void Page_Load(object sender, EventArgs e)
        {
            objChart.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Brnach_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            if (!IsPostBack)
            {
                ddlYear.Focus();
                for (int i = 2013; i <= 2018; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                ddlYear.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Year.ToString()).Selected = true;  //set current year as selected 
                Collection_And_Expense_column();
            }
        }
        public void Collection_And_Expense_column()
        {
            double jan = 0;
            double jans = 0;
            int month;
            double year;
            double Collection;
            double Expense;

            Collection = 0;
            Expense = 0;
            //Label26.Text = "Collection And Expense Report  In " + ddlYear.SelectedItem.Text + "";
            int Ye = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            DataSets ds = new DataSets();
            objChart.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Brnach_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            DataSet ds1 = objChart.BindCollectionDayWise1();
            DataTable dt1 = objChart.BindExpenseDayWise();

            if (ds1 != null && dt1 != null)
            {
                DataRow[] ServiceRow1 = ds1.Tables[0].Select();


                DataRow[] ServiceRow2 = dt1.Select();
                for (month = 1; month < 13; month++)
                {
                    jan = 0;
                    jans = 0;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        if (ServiceRow1[i][0].ToString() != "")
                        {
                            DateTime D1 = Convert.ToDateTime(ServiceRow1[i][0]);
                            if (D1.Month == month && D1.Year == Ye)
                            {
                                jan += Convert.ToDouble(ds1.Tables[0].Rows[i]["PaidWithTax"].ToString());//Paid_Fees
                            }
                        }
                    }
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (ServiceRow2[i][0].ToString() != "")
                        {
                            DateTime D1 = Convert.ToDateTime(ServiceRow2[i][0]);
                            if (D1.Month == month && D1.Year == Ye)
                            {
                                jans += Convert.ToInt32(dt1.Rows[i]["TotalAmount"].ToString());//Amount
                            }
                        }
                    }
                    //for (int i = 0; i < ServiceRow2.Length; i++)
                    //{
                    //    DateTime D2 = Convert.ToDateTime(ServiceRow2[i][0]);
                    //    if (D2.Month == month && D2.Year == Ye)
                    //    {
                    //        jans += 1;
                    //    }
                    //}
                    ds.Tables["ExpenseDayWise"].Rows.Add(jan, jans);
                    Collection += jan;
                    Expense += jans;
                }
                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables["ExpenseDayWise"].Select();
                //this.Chart1.Series["Enquiry"].Points.Clear();
                this.Chart8.Series["Collection"].Points.AddXY("Jan", SeviceRow[0][0]);
                lblJanCollection.Text = (SeviceRow[0][0]).ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Feb", SeviceRow[1][0]);
                lblFebColl.Text = SeviceRow[1][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Mar", SeviceRow[2][0]);
                lblMarColl.Text = SeviceRow[2][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Apr", SeviceRow[3][0]);
                lblAprColl.Text = SeviceRow[3][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("May", SeviceRow[4][0]);
                lblMayColl.Text = SeviceRow[4][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Jun", SeviceRow[5][0]);
                lbljuneColl.Text = SeviceRow[5][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("July", SeviceRow[6][0]);
                lblJulyColl.Text = SeviceRow[6][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Aug", SeviceRow[7][0]);
                lblAugColl.Text = SeviceRow[7][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Sep", SeviceRow[8][0]);
                lblSepColl.Text = SeviceRow[8][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Oct", SeviceRow[9][0]);
                lblOctColl.Text = SeviceRow[9][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Nov", SeviceRow[10][0]);
                lblNovColl.Text = SeviceRow[10][0].ToString();

                this.Chart8.Series["Collection"].Points.AddXY("Dec", SeviceRow[11][0]);
                lblDecColl.Text = SeviceRow[11][0].ToString();

                //this.Chart1.Series["AddMember"].Points.Clear();
                this.Chart8.Series["Expense"].Points.AddXY("Jan", SeviceRow[0][1]);
                lblJanExpanse.Text = SeviceRow[0][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Feb", SeviceRow[1][1]);
                lblFebExp.Text = SeviceRow[1][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Mar", SeviceRow[2][1]);
                lblMarExp.Text = SeviceRow[2][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Apr", SeviceRow[3][1]);
                lblAprExp.Text = SeviceRow[3][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("May", SeviceRow[4][1]);
                lblMayExp.Text = SeviceRow[4][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Jun", SeviceRow[5][1]);
                lbljuneExp.Text = SeviceRow[5][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("July", SeviceRow[6][1]);
                lblJulyExp.Text = SeviceRow[6][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Aug", SeviceRow[7][1]);
                lblAugExp.Text = SeviceRow[7][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Sep", SeviceRow[8][1]);
                lblSepExp.Text = SeviceRow[8][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Oct", SeviceRow[9][1]);
                lblOctExp.Text = SeviceRow[9][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Nov", SeviceRow[10][1]);
                lblNovExp.Text = SeviceRow[10][1].ToString();

                this.Chart8.Series["Expense"].Points.AddXY("Dec", SeviceRow[11][1]);
                lblDecExp.Text = SeviceRow[11][1].ToString();


                Chart8.Series["Collection"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
                Chart8.Series["Expense"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;

                lblTotColl.Text = Collection.ToString("0.00");
                lblTotExp.Text = Expense.ToString("0.00");

                lblTotalCollection.Text = Collection.ToString("0.00");
                lblTotalExpense.Text = Expense.ToString("0.00");

                if (ddlChart.Text == "Column")
                {
                    Chart8.Series["Collection"].ChartType = SeriesChartType.Column;
                    //  imgRS.Visible = true;
                }
                if (ddlChart.Text == "Line")
                {
                    Chart8.Series["Collection"].ChartType = SeriesChartType.Line;
                    Chart8.Series["Collection"].MarkerSize = 10;
                    Chart8.Series["Collection"].MarkerStyle = MarkerStyle.Circle;

                    Chart8.Series["Expense"].ChartType = SeriesChartType.Line;
                    Chart8.Series["Expense"].MarkerSize = 10;
                    Chart8.Series["Expense"].MarkerStyle = MarkerStyle.Circle;
                    //chart1.Series["Expense"].BorderWidth = 4;
                    //   imgRS.Visible = false;
                }
            }
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            Collection_And_Expense_column();
        }
    }
}