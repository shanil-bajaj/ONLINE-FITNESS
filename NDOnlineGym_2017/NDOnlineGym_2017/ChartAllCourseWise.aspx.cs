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
    public partial class ChartAllCourseWise : System.Web.UI.Page
    {
        BalChartBLL objChart = new BalChartBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

            if (!IsPostBack)
            {
                BindCorcewiseChart();
            }
        }

        public void BindCorcewiseChart()
        {
            //    if (comboBox1.Text != "Select Year")
            //    {
           
            DataSets ds = new DataSets();
            int ActiveMember = 0;
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());


            DataSet dtCourse = objChart.GetCourseName();
            DataTable dt = objChart.BindActiveByCorse();
            if (dt != null)
            {
                DataRow[] ServiceRow = dt.Select();
                for (int j = 0; j < ServiceRow.Length; j++)
                {
                    for (int i = 0; i < dtCourse.Tables[0].Rows.Count; i++)
                    {
                        //int x = Convert.ToInt32(dtCourse.Tables[0].Rows[i]["Pack_AutoID"].ToString());
                        //int y = Convert.ToInt32(dt.Rows[j]["Pack_AutoID"].ToString());

                        //if (x == y)
                        //{
                            
                        //    ActiveMember += Convert.ToInt32(ServiceRow[j][0]);
                        //    ds.Tables["ExpenseDayWise"].Rows.Add(0, ActiveMember);
                        //}
                    }
                }

                ds.AcceptChanges();

                DataRow[] SeviceRow = ds.Tables["ExpenseDayWise"].Select();

                for (int j = 0; j < ServiceRow.Length; j++)
                {
                    for (int i = 0; i < dtCourse.Tables[0].Rows.Count; i++)
                    {
                        string x = dtCourse.Tables[0].Rows[i]["Package"].ToString();
                        string y = dt.Rows[j]["Package"].ToString();

                        if (x == y)
                        {
                            this.Chart1.Series["Allcourse"].Points.AddXY(dtCourse.Tables[0].Rows[i]["Package"].ToString(), ServiceRow[j][0]);

                        }

                    }
                }
                objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                int countEnq = objChart.BindActiveDeactiveByStatus().Rows.Count;
                lblActiveMember.Text = countEnq.ToString();
           
                //lblActiveMember.Text = ActiveMember.ToString();
                
            }
        }

        protected void Chart1_Load(object sender, EventArgs e)
        {

        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

            objChart.Package = e.PostBackValue;
            DataTable dtCourse2 = objChart.GetCourseName1();
            if (dtCourse2 != null)
            {
                if (dtCourse2.Rows.Count > 0)
                {
                    string y1 = dtCourse2.Rows[0]["Package"].ToString();
                    HttpContext.Current.Session["VAL"] = y1;
                    Response.Redirect("ChartCourseWise.aspx", false);
                }

            }        
        }
    }
}