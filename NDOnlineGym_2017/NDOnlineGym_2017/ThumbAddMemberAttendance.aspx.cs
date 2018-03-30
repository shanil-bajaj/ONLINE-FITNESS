using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using DataAccessLayer;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace NDOnlineGym_2017
{
    public partial class ThumbAddMemberAttendance : System.Web.UI.Page
    {

        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();

        BalMemberNumericAttendance objBalMemberNumericAttendance = new BalMemberNumericAttendance();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        static int flag = 0;
        static int flag1 = 0;
        static int mid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
          
                AssignTodaysDate();      
                BindPageLoadGrid();
                
            }
        }

        public void BindPageLoadGrid()
        {
            objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                objBalMemberNumericAttendance.TodayDate = todaydate;
            }
            ds = objBalMemberNumericAttendance.SELECT_AttendanceDetails_ForBiomatric();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAttendanceDetails.DataSource = ds.Tables[0];
                gvAttendanceDetails.DataBind();
            }
            else
            {
                gvAttendanceDetails.DataSource = null;
                gvAttendanceDetails.DataBind();
            }
        }
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                objBalMemberNumericAttendance.AttndanceDate = todaydate;

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

               // txtTime.Text = localTime.ToString("HH:mm");
              //  objBalMemberNumericAttendance.AttndanceTime = Convert.ToDateTime(txtTime.Text);
            }
        }

        string mem;
        protected void gvAttendanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            AssignTodaysDate();
            string MemberName;
            objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalMemberNumericAttendance.Executive_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);

            //if (gvAttendanceDetails.Rows.Count > 0)
            //{
            Label memID1 = (Label)e.Row.FindControl("txtMember_ID1");
            if (memID1 != null)
            {
               // mem = memID1.Text;

                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_ID1 = Convert.ToInt32(memID1.Text);
                dt = objMember.Get_MemberAutoID_ByMemberID1();
                if (dt.Rows.Count > 0)
                {
                    mem = dt.Rows[0]["Member_AutoID"].ToString();

                }

            }
            objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(mem);
            //}
            //else
            //{
            //    if (ddlMemberName.SelectedValue != "--Select--")
            //    objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
            //}

            //if (ddlMemberName.SelectedValue != "--Select--")
            //    objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(mem);
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                objBalMemberNumericAttendance.TodayDate = todaydate;
            }
            dt = objBalMemberNumericAttendance.SELECT_PresentCondition();
            if (dt.Rows.Count > 0)
            {

                MemberName = dt.Rows[0]["FName"].ToString();
                if (dt.Rows[0]["CourseEndDate"].ToString() != "")
                {
                    DateTime CourseEndDate = Convert.ToDateTime(dt.Rows[0]["CourseEndDate"].ToString());
                    DateTime CourseEndDate1;
                    if (DateTime.TryParseExact(CourseEndDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseEndDate1))
                    {
                        objBalMemberNumericAttendance.CourseEndDate = CourseEndDate1;
                    }
                }
                if (dt.Rows[0]["NextPaymentDate"].ToString() != "")
                {
                    DateTime NextPaymentDate = Convert.ToDateTime(dt.Rows[0]["NextPaymentDate"].ToString());
                    DateTime NextPaymentDate1;
                    if (DateTime.TryParseExact(NextPaymentDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NextPaymentDate1))
                    {
                        objBalMemberNumericAttendance.NextPaymentDate = NextPaymentDate1;
                    }
                }
                objBalMemberNumericAttendance.AttndanceStatus = dt.Rows[0]["AttndanceStatus"].ToString();
                objBalMemberNumericAttendance.Balance = Convert.ToInt32(dt.Rows[0]["Balance"].ToString());
                string Result = dt.Rows[0]["Result"].ToString();
                if (Result != "")
                {

                    if (Result == "Your balance is remaining")
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (Result == "Your balance is remaining and Balance Payment Date is Over")
                    {
                        e.Row.BackColor = System.Drawing.Color.LimeGreen;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (Result == "Your balance is remaining and Membership Expired")
                    {
                        e.Row.BackColor = System.Drawing.Color.Firebrick;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (Result == "Your balance is remaining Membership Expired and Balance Pay date is over")
                    {
                        e.Row.BackColor = System.Drawing.Color.BurlyWood;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (Result == "Your Membership is Expired")
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                        e.Row.ForeColor = System.Drawing.Color.Black;
                    }
                    //else if (Result == "Your balance is remaining")
                    //    e.Row.BackColor = System.Drawing.Color.BlanchedAlmond;
                    //else if (Result == "Your balance is remaining")
                    //    e.Row.BackColor = System.Drawing.Color.BlanchedAlmond;


                }
            }
        }
    }
}