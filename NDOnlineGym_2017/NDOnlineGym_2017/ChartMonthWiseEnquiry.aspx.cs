using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using BusinessAccessLayer;
using System.Web.UI.DataVisualization.Charting;



namespace NDOnlineGym_2017
{
    public partial class ChartMonthWiseEnquiry : System.Web.UI.Page
    {
       
        BalChartBLL objChart = new BalChartBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlYear.Focus();
                //Fill Years
                for (int i = 2013; i <= 2018; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                ddlYear.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Year.ToString()).Selected = true;  //set current year as selected           
               BindChart_EnquiryMntWise_column();
            }
        }
      
        
        public void BindChart_EnquiryMntWise_column()
        {
            int jan = 0;
            int month;
            double year;
            int enquiry;
            //if (comboBox1.Text != "Select Year")
            //{
            Label3.Text = "Enquiry Report In Jan " + ddlYear.SelectedItem.Text + " To Dec " + ddlYear.SelectedItem.Text;
            int Ye = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            DataSets ds = new DataSets();
            enquiry = 0;
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            DataTable dt = objChart.BindEnquiryDayWise();
           
            DataTable dt1 = objChart.BindEnquiryDayWise();
            if (dt != null && dt1 != null)
            {
                DataRow[] ServiceRow2 = dt1.Select();
                for (month = 1; month < 13; month++)
                {
                    jan = 0;
                    DataRow[] ServiceRow = dt.Select();
                    //for (int i = 0; i < ServiceRow.Length; i++)
                    //{
                    //    DateTime D2 = Convert.ToDateTime(ServiceRow[i][0]);
                    //    if (D2.Month == month && D2.Year == Ye)
                    //    {
                    //        jan += 1;
                    //    }
                    //}
                    for (int j = 0; j < ServiceRow2.Length; j++)
                    {
                        if (ServiceRow2[j][0].ToString() != "")
                        {
                        DateTime D1 = Convert.ToDateTime(ServiceRow2[j][0]);
                        if (D1.Month == month && D1.Year == Ye)
                        {
                            jan += 1;
                        }
                         }
                    }
                    ds.Tables["Enquiry"].Rows.Add(jan);
                    enquiry += jan;
                }

                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables["Enquiry"].Select();
                // this.Chart1.Series["enquiry"].Points.Clear();
                this.Chart2.Series[0].Points.AddXY("Jan", SeviceRow[0][0]);
                //label2.Text = "JAN" + "   " + SeviceRow[0][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Feb", SeviceRow[1][0]);
                //label3.Text = "FEB" + "   " + SeviceRow[1][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Mar", SeviceRow[2][0]);
                //label4.Text = "MAR" + "   " + SeviceRow[2][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Apr", SeviceRow[3][0]);
                //label5.Text = "APR" + "   " + SeviceRow[3][0].ToString();
                this.Chart2.Series[0].Points.AddXY("May", SeviceRow[4][0]);
                //label6.Text = "MAY" + "   " + SeviceRow[4][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Jun", SeviceRow[5][0]);
                //label7.Text = "JUN" + "   " + SeviceRow[5][0].ToString();
                this.Chart2.Series[0].Points.AddXY("July", SeviceRow[6][0]);
                //label8.Text = "JUL" + "   " + SeviceRow[6][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Aug", SeviceRow[7][0]);
                //label11.Text = "AUG" + "   " + SeviceRow[7][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Sep", SeviceRow[8][0]);
                //label9.Text = "SEP" + "   " + SeviceRow[8][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Oct", SeviceRow[9][0]);
                // label10.Text = "OCT" + "   " + SeviceRow[9][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Nov", SeviceRow[10][0]);
                //label12.Text = "NOV" + "   " + SeviceRow[10][0].ToString();
                this.Chart2.Series[0].Points.AddXY("Dec", SeviceRow[11][0]);
                //label13.Text = "DEC" + "   " + SeviceRow[11][0].ToString();
                Label2.Text = enquiry.ToString();
                // groupBox1.Visible = true;

                Chart2.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;

                if (ddlChart.Text == "Column")
                {
                    Chart2.Series[0].ChartType = SeriesChartType.Column;
                  //  imgRS.Visible = true;
                }
                if (ddlChart.Text == "Pie")
                {
                    Chart2.Series[0].ChartType = SeriesChartType.Pie;
                    Chart2.Series[0].MarkerSize = 10;
                    Chart2.Series[0].MarkerStyle = MarkerStyle.Circle;
                    //chart1.Series["Expense"].BorderWidth = 4;
               //     imgRS.Visible = false;
                }
            }
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart_EnquiryMntWise_column();
            ddlChart.Focus();
        }

        }
    }
