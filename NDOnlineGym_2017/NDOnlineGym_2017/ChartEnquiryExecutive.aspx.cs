﻿using System;
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
    public partial class ChartEnquiryExecutive : System.Web.UI.Page
    {
        BalChartBLL objChart = new BalChartBLL();
        public int Enquiry = 0;
        public double jans = 0;
        public int month;
        public double year;
        public double payment;
        public int Branch_ID;

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
                DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
                for (int i = 1; i < 13; i++)
                {
                    ddlMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
                }
                ddlMonth.Items.FindByValue(DateTime.UtcNow.AddHours(5.5).Month.ToString()).Selected = true; // Set current month as selected
                //BindChart();
            }
        }
        public void BindChart()
        {
            //Label16.Text = "Enquiry Followup  In " + ddlMonth.SelectedItem.Text + " " + ddlYear.Text;
            ////int Ye = Convert.ToInt32(DateTime.Now.Year);
            int Ye = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedIndex + 1);
            objChart.Month = month;
            objChart.Year = Ye;
            DataSets ds = new DataSets();
            payment = 0;
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

            DataSet dataSet = objChart.BindDate1();
            DataTable dt = objChart.BindEnquiryStaffWise();

            if (dt != null)
            {

                Enquiry = 0;


                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["EnqDate"].ToString());
                        int x = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Staff_AutoID"].ToString());
                        int y = Convert.ToInt32(dt.Rows[j]["Executive_ID"].ToString());
                        if (D2.Month == month && D2.Year == Ye && x == y)
                        {
                            Enquiry += Convert.ToInt32(dt.Rows[j]["CountEnquiry"]);
                            ds.Tables["ExpenseDayWise"].Rows.Add(0, Enquiry);
                            break;
                        }
                    }
                }



                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables["ExpenseDayWise"].Select();
                this.Chart1.Series["Enquiry"].Points.Clear();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["EnqDate"].ToString());
                        int x = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Staff_AutoID"].ToString());
                        int y = Convert.ToInt32(dt.Rows[j]["Executive_ID"].ToString());
                        if (D2.Month == month && D2.Year == Ye && x == y)
                        {
                            this.Chart1.Series["Enquiry"].Points.AddXY(dataSet.Tables[0].Rows[i]["Name"].ToString(), dt.Rows[j]["CountEnquiry"]);
                            break;
                        }
                    }
                }
                lblTotalExpense.Text = Enquiry.ToString();
            }

            if (ddlChart.Text == "Column")
            {
                Chart1.Series["Enquiry"].ChartType = SeriesChartType.Column;
                //  imgRS.Visible = true;
            }
            if (ddlChart.Text == "Pie")
            {
                Chart1.Series["Enquiry"].ChartType = SeriesChartType.Pie;
                Chart1.Series["Enquiry"].MarkerSize = 10;
                Chart1.Series["Enquiry"].MarkerStyle = MarkerStyle.Circle;
                //chart1.Series["Expense"].BorderWidth = 4;
                // imgRS.Visible = false;
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChart.SelectedValue = "-1";
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChart.SelectedValue = "-1";
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChart();
        }
    }
}