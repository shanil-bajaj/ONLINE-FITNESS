using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using BusinessAccessLayer;
using System.Web.UI.DataVisualization.Charting;

namespace NDOnlineGym_2017
{
    public partial class ChartDayWiseEnquiry : System.Web.UI.Page
    {
        public int jan = 0;
        public int month;
        public double year;
        public int Enquiry;
        public int Branch_ID;
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
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                for (int i = 1; i < 13; i++)
                {
                    ddlMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
                }
                ddlMonth.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Month.ToString()).Selected = true; // Set current month as selected
                BindChart();
            }
            } 




       
         public void BindChart()
      {
        Label16.Text = "Enquiry Report  In " + "" + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text;
        int Ye = Convert.ToInt32(ddlYear.SelectedValue.ToString());
        month = Convert.ToInt32(ddlMonth.SelectedIndex );

        DataSets ds = new DataSets();
        Enquiry = 0;
        objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        objChart.Comp_AutoID= Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        DataTable dt1 = objChart.BindEnquiryDayWise();
          
        //DataTable dt1 = objChart.BindAddMember();
        if (dt1 != null)
        {
            DataRow[] ServiceRow2 = dt1.Select();
            for (int j = 1; j <= 31; j++)
            {
                jan = 0;
                //DataRow[] ServiceRow = dt.Select();
                //for (int i = 0; i < ServiceRow.Length; i++)
                //{
                //    DateTime D2 = Convert.ToDateTime(ServiceRow[i][0]);
                //    if (D2.Month == month && D2.Year == Ye && D2.Day == j)
                //    {
                //        jan += 1;
                //    }
                //}
                for (int m = 0; m < ServiceRow2.Length; m++)
                {
                    if (ServiceRow2[m ][0].ToString() != "")
                    {
                    DateTime D1 = Convert.ToDateTime(ServiceRow2[m][0]);
                    if (D1.Month == month && D1.Year == Ye && D1.Day == j)
                    {
                        jan += 1;
                    }
                    }
                }

               ds.Tables["Enquiry"].Rows.Add(jan);
                Enquiry += jan;
            }
            ds.AcceptChanges();
            DataRow[] SeviceRow = ds.Tables["Enquiry"].Select();
            //this.Chart1.Series["Enquiry"].Points.Clear();
            for (int j = 0; j <= 30; j++)
            {
                
                this.Chart2.Series[0].Points.AddXY(j + 1, SeviceRow[j][0]);
                Chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            }
            Label15.Text = Enquiry.ToString();

        }


        if (ddlChart.Text == "Column")
        {
            Chart2.Series[0].ChartType = SeriesChartType.Column;
            imgRS.Visible = true;
        }
        if (ddlChart.Text == "Pie")
        {
            Chart2.Series[0].ChartType = SeriesChartType.Pie;
            Chart2.Series[0].MarkerSize = 10;
            Chart2.Series[0].MarkerStyle = MarkerStyle.Circle;
            //chart1.Series["Expense"].BorderWidth = 4;
        }
    }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlChart.Text!="--Select--")
            {
            BindChart();
            ddlChart.Focus();
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BindChart();
        }


      }
    }
