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
    public partial class ChartMonthWiseCollection : System.Web.UI.Page
    {
        public double jan = 0;
        public double jans = 0;
        public int month;
        public double year;
        public double payment;

        BalChartBLL objChart = new BalChartBLL();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart();
            ddlChart.Focus();
        }

        public void BindChart()
        {
            //    if (comboBox1.Text != "Select Year")
            //    {
            Label1.Text = "Payment Collection Report  From Jan " + ddlYear.SelectedItem.Text + " To Dec " + ddlYear.SelectedItem.Text;
            int Ye = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            DSCollection ds = new DSCollection();
            payment = 0;
            objChart.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Brnach_Id = Convert.ToInt32(Session["Branch_ID"].ToString());
            DataTable dt = objChart.BindCollectionDayWise();
            if (dt != null)
            {
                for (month = 1; month < 13; month++)
                {
                    jan = 0;
                    jans = 0;
                    DataRow[] ServiceRow = dt.Select();
                    for (int i = 0; i < ServiceRow.Length; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(ServiceRow[i][0]);
                        if (D2.Month == month && D2.Year == Ye)
                        {
                            jan += Convert.ToDouble(ServiceRow[i][1]);
                        }
                    }
                    ds.Tables["CollectionMonthWise"].Rows.Add(jan, jans);
                    payment += jan;
                }
                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables["CollectionMonthWise"].Select();
                // this.chart1.Series["Payment"].Points.Clear();
                this.Chart1.Series["Payment"].Points.AddXY("Jan", SeviceRow[0][0]);
                // label2.Text = "JAN" + "  " + SeviceRow[0][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Feb", SeviceRow[1][0]);
                ///label3.Text = "FEB" + "  " + SeviceRow[1][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Mar", SeviceRow[2][0]);
                //label4.Text = "MAR" + "  " + SeviceRow[2][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Apr", SeviceRow[3][0]);
                //label5.Text = "APR" + "  " + SeviceRow[3][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("May", SeviceRow[4][0]);
                //label6.Text = "MAY" + "  " + SeviceRow[4][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Jun", SeviceRow[5][0]);
                //label7.Text = "JUN" + "  " + SeviceRow[5][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("July", SeviceRow[6][0]);
                // label8.Text = "JUL" + "  " + SeviceRow[6][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Aug", SeviceRow[7][0]);
                //label11.Text = "AUG" + "  " + SeviceRow[7][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Sep", SeviceRow[8][0]);
                //label9.Text = "SEP" + "  " + SeviceRow[8][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Oct", SeviceRow[9][0]);
                //label10.Text = "OCT" + "  " + SeviceRow[9][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Nov", SeviceRow[10][0]);
                //label12.Text = "NOV" + "  " + SeviceRow[10][0].ToString();
                this.Chart1.Series["Payment"].Points.AddXY("Dec", SeviceRow[11][0]);
                //label13.Text = "DEC" + "  " + SeviceRow[11][0].ToString();
                lblTotalCollection.Text = payment.ToString();
                //groupBox1.Visible = true;

                if (ddlChart.Text == "Column")
                {
                    Chart1.Series["Payment"].ChartType = SeriesChartType.Column;
                   // imgRS.Visible = true;
                }
                if (ddlChart.Text == "Pie")
                {
                    Chart1.Series["Payment"].ChartType = SeriesChartType.Pie;
                    Chart1.Series["Payment"].MarkerSize = 10;
                    Chart1.Series["Payment"].MarkerStyle = MarkerStyle.Circle;
                    //chart1.Series["Expense"].BorderWidth = 4;
                  //  imgRS.Visible = false;

                }

            }
        }
    }
}