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
    public partial class ChartMonthWiseExpense : System.Web.UI.Page
    {

        public double jan = 0;
        public double jans = 0;
        public int month;
        public double year;
        public double expense;

        BalCharts objChart = new BalCharts();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlYear.Focus();
                for (int i = 2013; i <= 2018; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                ddlYear.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Year.ToString()).Selected = true;
                BindChart();
            }
        }

        public void BindChart()
        {
            Label1.Text = "Expense Report  From jan " + ddlYear.SelectedItem.Text + " To Dec " + ddlYear.SelectedItem.Text;
            int ye = Convert.ToInt32(ddlYear.SelectedValue);
            DataSet1 ds = new DataSet1();
            expense = 0;
            objChart.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Brnach_Id = Convert.ToInt32(Session["Branch_ID"].ToString());

            DataTable dt = objChart.BindExpenseDayWise();
            if (dt != null)
            {
                for (month = 1; month < 13; month++)
                {
                    jan = 0;
                    jans = 0;
                    DataRow[] ServiceRow = dt.Select();
                    for (int i = 0; i < ServiceRow.Length; i++)
                    {
                        DateTime D1 = Convert.ToDateTime(ServiceRow[i][0]);
                        if (D1.Month == month && D1.Year == ye)
                        {
                            jan += Convert.ToDouble(ServiceRow[i][1]);
                        }
                    }
                    ds.Tables["ExpenseDayWise"].Rows.Add(jan, jans);
                    expense += jan;

                }
                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables["ExpenseDayWise"].Select();
                // this.Chart1.Series["Expense"].Points.Clear();
                this.Chart1.Series["Expense"].Points.AddXY("Jan", SeviceRow[0][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Feb", SeviceRow[1][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Mar", SeviceRow[2][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Apr", SeviceRow[3][0]);

                this.Chart1.Series["Expense"].Points.AddXY("May", SeviceRow[4][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Jun", SeviceRow[5][0]);

                this.Chart1.Series["Expense"].Points.AddXY("July", SeviceRow[6][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Aug", SeviceRow[7][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Sep", SeviceRow[8][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Oct", SeviceRow[9][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Nov", SeviceRow[10][0]);

                this.Chart1.Series["Expense"].Points.AddXY("Dec", SeviceRow[11][0]);

                lblTotalExpense.Text = expense.ToString();
                //groupBox1.Visible = true;

                if (ddlChart.Text == "Column")
                {
                    Chart1.Series[0].ChartType = SeriesChartType.Column;
                    //imgRS.Visible = true;
                }
                if (ddlChart.Text == "Pie")
                {
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.Series[0].MarkerSize = 10;
                    Chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
                    //chart1.Series["Expense"].BorderWidth = 4;
                    //imgRS.Visible = false;

                }
            }
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart();
            ddlChart.Focus();
        }
    }
}