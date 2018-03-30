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
    public partial class ChartAdmission : System.Web.UI.Page
    {
        BalChartBLL objChart = new BalChartBLL();

        public int jan = 0;
        public int month;
        public double year;
        public int enquiry;

        protected void Page_Load(object sender, EventArgs e)
        {
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            if (!IsPostBack)
            {
                ddlYear.Focus();
                //Fill Years
                for (int i = 2013; i <= 2018; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                ddlYear.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Year.ToString()).Selected = true;  //set current year as selected  
                BindChart();
            }
        }
             public void BindChart()
        {
           
            Chart1.Series["New"].ChartType = SeriesChartType.Column;

            Chart1.Series["New"].MarkerSize = 10;
           // Chart1.Series["New"].MarkerStyle = MarkerStyle.Circle;
            Chart1.Series["New"].BorderWidth = 4;
          //  Chart1.Series["New"].MarkerColor = System.Drawing.Color.YellowGreen;
            //if (comboBox1.Text != "Select Year")
            //{
          
            int Ye = Convert.ToInt32(ddlYear.SelectedValue);
            DataSets ds = new DataSets();
            enquiry = 0;
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            DataTable dt = objChart.BindMemberDayWise();
            if (dt != null)
            {
                //DataTable dt1 = objChart.BindAddMember();
                //DataRow[] ServiceRow2 = dt1.Select();
                for (month = 1; month < 13; month++)
                {
                    jan = 0;
                    DataRow[] ServiceRow = dt.Select();
                    for (int i = 0; i < ServiceRow.Length; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(ServiceRow[i][0]);
                        if (D2.Month == month && D2.Year == Ye)
                        {
                            jan += 1;
                        }
                    }
                   
                    ds.Tables["New"].Rows.Add(jan);
                    enquiry += jan;
                }


                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables["New"].Select();
                //this.Chart1.Series["New"].Points.Clear();
                this.Chart1.Series["New"].Points.AddXY("Jan", SeviceRow[0][0]);

                this.Chart1.Series["New"].Points.AddXY("Feb", SeviceRow[1][0]);

                this.Chart1.Series["New"].Points.AddXY("Mar", SeviceRow[2][0]);

                this.Chart1.Series["New"].Points.AddXY("Apr", SeviceRow[3][0]);

                this.Chart1.Series["New"].Points.AddXY("May", SeviceRow[4][0]);

                this.Chart1.Series["New"].Points.AddXY("Jun", SeviceRow[5][0]);

                this.Chart1.Series["New"].Points.AddXY("July", SeviceRow[6][0]);

                this.Chart1.Series["New"].Points.AddXY("Aug", SeviceRow[7][0]);

                this.Chart1.Series["New"].Points.AddXY("Sep", SeviceRow[8][0]);

                this.Chart1.Series["New"].Points.AddXY("Oct", SeviceRow[9][0]);

                this.Chart1.Series["New"].Points.AddXY("Nov", SeviceRow[10][0]);

                this.Chart1.Series["New"].Points.AddXY("Dec", SeviceRow[11][0]);

                lblNew.Text = enquiry.ToString();
                

            }
            if (ddlChart.Text == "Column")
            {
                Chart1.Series["New"].ChartType = SeriesChartType.Column;
                
            }
            if (ddlChart.Text == "Line")
            {
                Chart1.Series["New"].ChartType = SeriesChartType.Line;
                Chart1.Series["New"].MarkerSize = 10;
                Chart1.Series["New"].MarkerStyle = MarkerStyle.Circle;
                //chart1.Series["Expense"].BorderWidth = 4;
               

            }
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart();
        }
    }
}