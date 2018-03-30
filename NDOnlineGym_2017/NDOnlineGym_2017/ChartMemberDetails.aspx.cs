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
    public partial class ChartMemberDetails : System.Web.UI.Page
    {
        BalChartBLL objChart = new BalChartBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
             objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

            if (!IsPostBack)
            {
                if (Request.QueryString["duration"] != null)
                {
                    lblDuration.Text = Request.QueryString["duration"];
                }
                if (Request.QueryString["pakage"] != null)
                {
                    lblPackage.Text = Request.QueryString["pakage"];
                }
                BindGridView();
            }
        }

        protected void BindGridView()
        {
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

            objChart.Package = lblPackage.Text;
            objChart.Duration =Convert.ToInt32(lblDuration.Text);
            DataTable dt = objChart.BindMember();
            gvMember.DataSource = dt;
            gvMember.DataBind();

        }

        protected void gvMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMember.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}