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
    public partial class ChartCourseWise : System.Web.UI.Page
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
                ddlCourse.Focus();
                Bind_ddlCourse();
                if (Session["VAL"] != null)
                {
                    ddlCourse.SelectedItem.Text = Session["VAL"].ToString();
                }

               BindCorcewiseChart();
            }    
        }

        protected void Bind_ddlCourse()
        {
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            DataSet dataSet = objChart.GetCourseName2();

            ddlCourse.DataSource = dataSet;
            ddlCourse.Items.Clear();
            ddlCourse.DataValueField = "Package";
            ddlCourse.DataTextField = "Package";
            ddlCourse.DataBind();
                           
        }

        public void BindCorcewiseChart()
        {
            //    if (comboBox1.Text != "Select Year")
            //    {
            //if (Session["VAL"] == null)
            //{
                Label16.Text =  ddlCourse.SelectedItem.Text;
            //}
            string course = ddlCourse.SelectedValue.ToString();
            DataSets ds = new DataSets();
            int payment = 0;
            //  int ActiveMember = 0;
            //objChart.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
            //obBalCourse.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
            //objChart.Package_ID = int.Parse(ddlCourse.SelectedItem.Value);
            //obBalCourse.Package_ID = int.Parse(ddlCourse.SelectedItem.Value);
            //  DataSet dtCourse = obBalCourse.GetCourseNamePackId();

            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            objChart.Package = ddlCourse.SelectedItem.Text;
            
            DataTable dt = objChart.BindCoursePlanWise();
            if (dt.Rows.Count > 0)
            {
               
                ds.AcceptChanges();

                DataRow[] SeviceRow = ds.Tables["ExpenseDayWise"].Select();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow[] ServiceRow = dt.Select();

                    string plan = dt.Rows[i]["Duration"].ToString();
                    int cnt3 = Convert.ToInt32(dt.Rows[i]["Number"].ToString());
                    payment = payment + cnt3;

                    this.Chart1.Series["course"].Points.AddXY(plan, cnt3);
                }
                lblCoursewise.Text = payment.ToString();
            }
            else
            {
                this.Chart1.Series["course"].Points.AddXY(0, 0);
                lblCoursewise.Text = "0";
            }
            //obBalHomeNotifications.type = "Active";
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            int countEnq = objChart.BindActiveDeactiveByStatus().Rows.Count;
            lblActiveMember.Text = countEnq.ToString();

            if (ddlChart.Text == "Column")
            {
                Chart1.Series["course"].ChartType = SeriesChartType.Column;
                //  imgRS.Visible = true;
            }
            if (ddlChart.Text == "Pie")
            {
                Chart1.Series["course"].ChartType = SeriesChartType.Pie;
                Chart1.Series["course"].MarkerSize = 10;
                Chart1.Series["course"].MarkerStyle = MarkerStyle.Circle;
                //chart1.Series["Expense"].BorderWidth = 4;
                // imgRS.Visible = false;
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChart.SelectedItem.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please select chart type !!!.','Information');", true);
            }
            else
            BindCorcewiseChart();
        }

        protected void ddlChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCorcewiseChart();
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objChart.Package =ddlCourse.SelectedItem.Text;
            objChart.Duration = Convert.ToInt32(e.PostBackValue.ToString());
            DataTable dtCourse = objChart.GetplanName();
            if (dtCourse != null)
            {
                string pakage = ddlCourse.SelectedItem.Text;
                int Pack_AutoID = Convert.ToInt32(dtCourse.Rows[0]["Pack_AutoID"].ToString());
                int duration = Convert.ToInt32(e.PostBackValue.ToString());
                string strPopup = "<script language='javascript' ID='script1'>"
                + "window.open('ChartMemberDetails.aspx?duration=" + duration
                + "&Pack_AutoID= " + Pack_AutoID + "&pakage= " + pakage + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
                + "</script>";
                ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }

        }
    }
}