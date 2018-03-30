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
    public partial class ChartEnquiryToAddMember : System.Web.UI.Page
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
                BindChart_EnquiryToAddMember_column();
            }
        }
        public void BindChart_EnquiryToAddMember_column()
        {
            double jan = 0;
            double jans = 0;
            int month;
            double year;
            double Enquiry;
            double AddMember;
           
            Enquiry = 0;
            AddMember = 0;
          
            int Ye = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            DataSets ds = new DataSets();
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            //DataTable dt = objChart.BindEnquiryDayWise();
            //DataTable dt1 = objChart.BindAddMember();

            DataSet ds1 = objChart.BindEnquiryDayWise1();

            DataTable dt1 = objChart.BindAddMember();

            if (ds1 != null && dt1 != null)
            {
                //DataRow[] ServiceRow1 = dt.Select();
                DataRow[] ServiceRow1 = ds1.Tables[0].Select();

                DataRow[] ServiceRow2 = dt1.Select();
                for (month = 1; month < 13; month++)
                {
                    jan = 0;
                    jans = 0;
                    for (int i = 0; i < ServiceRow1.Length; i++)
                    {
                        if (ServiceRow1[i][0].ToString() != "")
                        {
                            DateTime D1 = Convert.ToDateTime(ServiceRow1[i][0]);
                            if (D1.Month == month && D1.Year == Ye)
                            {
                                jan += 1;
                            }
                        }
                    }
                    //for (int i = 0; i < ServiceRow2.Length; i++)
                    //{
                    //    if (ServiceRow2[i][0].ToString() != "")
                    //    {
                    //        DateTime D1 = Convert.ToDateTime(ServiceRow2[i][0]);
                    //        if (D1.Month == month && D1.Year == Ye)
                    //        {
                    //            jan += 1;
                    //        }
                    //    }
                    //}
                    for (int i = 0; i < ServiceRow2.Length; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(ServiceRow2[i][0]);
                        if (D2.Month == month && D2.Year == Ye)
                        {
                            jans += 1;
                        }
                    }
                    ds.Tables["EnquiryAddMember"].Rows.Add(jan, jans);
                    Enquiry += jan;
                    AddMember += jans;
                }
                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables["EnquiryAddMember"].Select();
                //this.Chart1.Series["Enquiry"].Points.Clear();
                this.Chart8.Series["Enquiry"].Points.AddXY("Jan", SeviceRow[0][0]);
                lblJanEnquiry.Text = (SeviceRow[0][0]).ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Feb", SeviceRow[1][0]);
                lblFebEnquiry.Text = SeviceRow[1][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Mar", SeviceRow[2][0]);
                lblMarEnquiry.Text = SeviceRow[2][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Apr", SeviceRow[3][0]);
                lblAprEnquiry.Text = SeviceRow[3][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("May", SeviceRow[4][0]);
                lblMayEnquiry.Text = SeviceRow[4][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Jun", SeviceRow[5][0]);
                lbljuneEnquiry.Text = SeviceRow[5][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("July", SeviceRow[6][0]);
                lblJulyEnquiry.Text = SeviceRow[6][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Aug", SeviceRow[7][0]);
                lblAugEnquiry.Text = SeviceRow[7][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Sep", SeviceRow[8][0]);
                lblSepEnquiry.Text = SeviceRow[8][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Oct", SeviceRow[9][0]);
                lblOctEnquiry.Text = SeviceRow[9][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Nov", SeviceRow[10][0]);
                lblNovEnquiry.Text = SeviceRow[10][0].ToString();

                this.Chart8.Series["Enquiry"].Points.AddXY("Dec", SeviceRow[11][0]);
                lblDecEnquiry.Text = SeviceRow[11][0].ToString();

                //this.Chart1.Series["AddMember"].Points.Clear();

                this.Chart8.Series["AddMember"].Points.AddXY("Jan", SeviceRow[0][1]);
                lblJanRegMember.Text = SeviceRow[0][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Feb", SeviceRow[1][1]);
                lblFebRegMember.Text = SeviceRow[1][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Mar", SeviceRow[2][1]);
                lblMarRegMember.Text = SeviceRow[2][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Apr", SeviceRow[3][1]);
                lblAprRegMember.Text = SeviceRow[3][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("May", SeviceRow[4][1]);
                lblMayRegMember.Text = SeviceRow[4][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Jun", SeviceRow[5][1]);
                lbljuneRegMember.Text = SeviceRow[5][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("July", SeviceRow[6][1]);
                lblJulyRegMember.Text = SeviceRow[6][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Aug", SeviceRow[7][1]);
                lblAugRegMember.Text = SeviceRow[7][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Sep", SeviceRow[8][1]);
                lblSepRegMember.Text = SeviceRow[8][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Oct", SeviceRow[9][1]);
                lblOctRegMember.Text = SeviceRow[9][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Nov", SeviceRow[10][1]);
                lblNovRegMember.Text = SeviceRow[10][1].ToString();

                this.Chart8.Series["AddMember"].Points.AddXY("Dec", SeviceRow[11][1]);
                lblDecRegMember.Text = SeviceRow[11][1].ToString();


                Chart8.Series["Enquiry"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
                Chart8.Series["AddMember"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;



                if (ddlChart.Text == "Column")
                {
                    Chart8.Series["Enquiry"].ChartType = SeriesChartType.Column;
                    Chart8.Series["AddMember"].ChartType = SeriesChartType.Column;
                    //imgRS.Visible = true;
                }
                if (ddlChart.Text == "Line")
                {
                    Chart8.Series["Enquiry"].ChartType = SeriesChartType.Line;
                    Chart8.Series["AddMember"].ChartType = SeriesChartType.Line;

                    Chart8.Series["Enquiry"].MarkerSize = 10;
                    Chart8.Series["Enquiry"].MarkerStyle = MarkerStyle.Circle;
                    Chart8.Series["Enquiry"].BorderWidth = 4;

                    Chart8.Series["AddMember"].MarkerSize = 10;
                    Chart8.Series["AddMember"].MarkerStyle = MarkerStyle.Circle;
                    Chart8.Series["AddMember"].BorderWidth = 4;
                    //imgRS.Visible = false;
                }
                lblTotEnquiry.Text = Enquiry.ToString();
                lblTotRegMember.Text = AddMember.ToString();

                Label23.Text = Enquiry.ToString();
                Label24.Text = AddMember.ToString();


                //if (ddlChart.Text == "Line")
                //{
                //    Chart8.Series["AddMember"].ChartType = SeriesChartType.Line;
                //    Chart8.Series["AddMember"].MarkerSize = 10;
                //    Chart8.Series["AddMember"].MarkerStyle = MarkerStyle.Circle;
                //    //chart1.Series["Expense"].BorderWidth = 4;
                //    //imgRS.Visible = false;
                //}
            }
            ds1.Tables.Clear();
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart_EnquiryToAddMember_column();
        }

        protected void Chart8_Load(object sender, EventArgs e)
        {

        }

       
    }
}