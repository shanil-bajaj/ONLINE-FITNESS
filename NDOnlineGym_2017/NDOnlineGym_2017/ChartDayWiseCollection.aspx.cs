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
    public partial class ChartDayWiseCollection : System.Web.UI.Page
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
            objChart.Brnach_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            if (!IsPostBack)
            {
                ddlYear.Focus();
                for (int i = 2013; i <= 2018; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                ddlYear.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Year.ToString()).Selected = true;  //set current year as selected           
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                for (int i = 1; i < 13; i++)
                {
                    ddlMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
                }
                ddlMonth.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Month.ToString()).Selected = true; // Set current month as selected
                load();
            }
        }

        public void load()
        {

            Label16.Text = "Collection Report  In " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text;
            int Ye = Convert.ToInt32(ddlYear.SelectedValue);
            month = Convert.ToInt32(ddlMonth.SelectedIndex + 1);
            DataSets ds = new DataSets();
            exp = 0;
            objChart.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Brnach_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            DataTable dt = objChart.BindCollectionDayWise();
            if(! (dt is DBNull))
            {
                for (int j = 1; j <= 31; j++)
                {
                    jan = 0;
                    jans = 0;
                    DataRow[] ServiceRow = dt.Select();
                    for (int i = 0; i < ServiceRow.Length; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(ServiceRow[i][0]);
                        if (D2.Month == month - 1 && D2.Year == Ye && D2.Day == j)
                        {
                            jan += Convert.ToDouble(ServiceRow[i][1]);
                        }
                    }
                    ds.Tables[0].Rows.Add(jan);
                    exp += jan;
                }
                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables[0].Select();
                this.Chart1.Series["Collection"].Points.Clear();
                for (int j = 0; j <= 30; j++)
                {
                    this.Chart1.Series["Collection"].Points.AddXY(j + 1, SeviceRow[j][0]);
                    // this.chart1.Series["Expense"]["PixelPointWidth"] = "15";
                    Chart1.ChartAreas[0].AxisX.IsMarginVisible = false;

                }
                lblTotalCollection.Text = exp.ToString();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            load();
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            load();
            ddlChart.Focus();
        }
    }
}