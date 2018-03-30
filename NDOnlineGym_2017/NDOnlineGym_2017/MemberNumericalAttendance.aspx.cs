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
    public partial class MemberNumericalAttendance : System.Web.UI.Page
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
                //if (flag == 1)
                //    flag = 0;
                AssignTodaysDate();
                bindDDLMemberName();
                BindPageLoadGrid();
                if (Request.QueryString["Member_ID"] != null)
                {
                    Get_MemberAutoID_ByMemberID1();
                }
                txtMemberID.Focus();
            }
        }

        #region ------------ Assign All Date ------------------
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                objBalMemberNumericAttendance.AttndanceDate = todaydate;
                
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                txtTime.Text = localTime.ToString("HH:mm");
                objBalMemberNumericAttendance.AttndanceTime = Convert.ToDateTime(txtTime.Text);
            }
        }
        #endregion

        public void bindDDLMemberName()
        {
            try
            {
                
                objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
               
                dt = objBalMemberNumericAttendance.SELECT_AllMembers();
                if (dt.Rows.Count > 0)
                {
                    ddlMemberName.DataSource = dt;
                    ddlMemberName.Items.Clear();
                    ddlMemberName.DataValueField = "Member_AutoID";
                    ddlMemberName.DataTextField = "Name";
                    ddlMemberName.DataBind();
                    ddlMemberName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlMemberName.SelectedItem.Value = "--Select--";
                }
                else
                {
                    ddlMemberName.DataSource = null;
                    ddlMemberName.DataBind();
                    ddlMemberName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    //ddlMemberName.SelectedValue = "--Select--";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Found !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            
            if (mid.ToString() != txtMemberID.Text)
            {
                flag = 0;
            }
            if (flag == 0)
            {
                Get_MemberAutoID_ByMemberID1();
                present();
                flag = 1;
                //mid = Convert.ToInt32(txtMemberID.Text);
            }


            flag1 = 1;
            txtMemberID.Text = "";
            ddlMemberName.SelectedValue = "--Select--";
            gvCourseDetails.DataSource = null;
            gvCourseDetails.DataBind();
        }

        public void Get_MemberAutoID_ByMemberID1()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (Request.QueryString["Member_ID"] != null)
                objMember.Member_ID1 = Convert.ToInt32(Request.QueryString["Member_ID"]);
            else
            {
                if(txtMemberID.Text != "")
                objMember.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
            }
            dt = objMember.Get_MemberAutoID_ByMemberID1();
            if (dt.Rows.Count > 0)
            {
                ddlMemberName.SelectedValue = dt.Rows[0]["Member_AutoID"].ToString();
                ImgMember.ImageUrl = dt.Rows[0]["ImagePath"].ToString();
                objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); 
                objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                if(ddlMemberName.SelectedValue != "--Select--")
                objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
                dt = objBalMemberNumericAttendance.SELECT_CourseDetails();
                if (dt.Rows.Count > 0)
                {
                    gvCourseDetails.DataSource = dt;
                    gvCourseDetails.DataBind();
                }
                else
                {
                    gvCourseDetails.DataSource = null;
                    gvCourseDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Found !!!','Information');", true);
                    txtMemberID.Text = "";
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Found !!!','Information');", true);
                txtMemberID.Text = "";
                return;
            }
            
        }

        public void Get_MemberDetails_ByMemberAutoID()
        {
            objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
            dt = objBalMemberNumericAttendance.SELECT_Member_ByAutoID();
            if (dt.Rows.Count > 0)
            {
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                ImgMember.ImageUrl = dt.Rows[0]["ImagePath"].ToString();
                dt = objBalMemberNumericAttendance.SELECT_CourseDetails();
                if (dt.Rows.Count > 0)
                {
                    gvCourseDetails.DataSource = dt;
                    gvCourseDetails.DataBind();
                }
                else
                {
                    gvCourseDetails.DataSource = null;
                    gvCourseDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.warning('Member is Deactive !!!','Warning');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Found !!!','Information');", true);
                ddlMemberName.SelectedValue = "--Select--";
                return;
            }
        }

        protected void ddlMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPresent.Focus();
            Get_MemberDetails_ByMemberAutoID();
            //present();
            
        }

        public void present()
        {
            AssignTodaysDate();
            string MemberName;
            objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalMemberNumericAttendance.Executive_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            if (ddlMemberName.SelectedValue != "--Select--")
                objBalMemberNumericAttendance.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg1", "toastr.warning('" + Result + " ','Warning');", true);
                objBalMemberNumericAttendance.Action = "INSERT";
                int res = objBalMemberNumericAttendance.Insert_attendance();
                if (res > 0)
                {
                    ds = objBalMemberNumericAttendance.SELECT_AttendanceDetails();
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Welcome " + MemberName + " ','Success');", true);
                    if (Request.QueryString["FNameMemDetails"] != null)
                    {
                        int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                        Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                    }
                }
            }
        }

        protected void btnPresent_Click(object sender, EventArgs e)
        {
           
            Get_MemberAutoID_ByMemberID1();
            present();
            txtMemberID.Text = "";
            ddlMemberName.SelectedValue = "--Select--";
            gvCourseDetails.DataSource = null;
            gvCourseDetails.DataBind();
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
            ds = objBalMemberNumericAttendance.SELECT_AttendanceDetails();
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

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                objBalMemberNumericAttendance.TodayDate = todaydate;
            }
            objBalMemberNumericAttendance.Attendance_AutoID = Convert.ToInt32(e.CommandArgument);
            objBalMemberNumericAttendance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objBalMemberNumericAttendance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            int res = objBalMemberNumericAttendance.Delete_attendance();
            if (res > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Error');", true);

                ds = objBalMemberNumericAttendance.SELECT_AttendanceDetails();
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
            Label memautoid = (Label)e.Row.FindControl("lblmemautoid");
                if (memautoid != null)
                {
                    mem = memautoid.Text;
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