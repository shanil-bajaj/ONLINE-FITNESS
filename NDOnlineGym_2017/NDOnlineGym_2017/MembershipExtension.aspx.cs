using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;
using System.Globalization;
using BusinessAccessLayer;
using DataAccessLayer;
using System.IO;

namespace NDOnlineGym_2017
{
    public partial class MembershipExtension : System.Web.UI.Page
    {
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();

        BalMembrshipExtension ObjExtend = new BalMembrshipExtension();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        static int MemberAutoID;
        static int Member_Id1 = 0;
        static int Extension_AutoID;
        string CourseSDate = "";
        string CourseEndDate = "";
        DateTime CourseStartDate;
        DateTime CourseOldEndDate;
        DateTime CourseNewEndDate;
        DateTime TodayDate;
        int flag = 0;

        int res;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // txtSearch.Enabled = false;                
                ViewState["DT"] = null;

                txtMemberId.Focus();

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                string NowDateTime = localTime.ToString("dd-MM-yyyy");

                txtExtensionDate.Text = NowDateTime;
                bindDDLExecutive();
                setExecutive();
                if (Request.QueryString["MenuExtensionDetails"] != null)
                {
                    divMemExtension.Visible = false;
                    divMemExtensionDetails.Visible = true;
                    divsearch.Visible = true;
                    divFormDetails.Visible = false;
                    Assign_MonthDate();
                    txtFromDate.Focus();
                    SearchByDateFunction();
                }

                // Redirect from Termination Form 
                if (Request.QueryString["MemberID"] != null)
                {
                    txtMemberId.Text = Request.QueryString["MemberID"].ToString();
                    BindByMemberID();
                }
            }
        }

        #region ----------- Assign Comp and Branch ID---------------
        private void AssignID()
        {
            ObjExtend.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            ObjExtend.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            ObjExtend.LoginID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }
        #endregion

        #region ----------- Assign Today date ---------------
        private void AssignTodaydate()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            string Todate = localTime.ToString("dd-MM-yyyy");

           // string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
            if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
            {
            }

           ObjExtend.TodayDate = TodayDate;
        }
        #endregion

        #region ------------ Assign Month Start And End Date --------------
        private void Assign_MonthDate()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

            DateTime firstDayOfMonth = new DateTime(localTime.Year, localTime.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            txtFromDate.Text = firstDayOfMonth.ToString("dd-MM-yyyy");
            txtToDate.Text = lastDayOfMonth.ToString("dd-MM-yyyy");

            //ObjTrans.MStartDate = firstDayOfMonth;
            //ObjTrans.MEndDate = lastDayOfMonth;
        }
        #endregion

        #region --------- Bind Executive DDL -----------------
        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dt.Rows.Count != 0)
                {
                    ddlExecutive.DataSource = dt;
                    ddlExecutive.Items.Clear();
                    ddlExecutive.DataValueField = "Staff_AutoID";
                    ddlExecutive.DataTextField = "Name";
                    ddlExecutive.DataBind();
                    //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff. !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region -------------- Set Executive ------------------
        public void setExecutive()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dt = obBalStaffRegistration.GetExecutiveByID_ByBranch();
            if (dt.Rows.Count > 0)
            {
                ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutive.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
            }
            else
            {
                dt = obBalStaffRegistration.GetExecutiveByID_WithoutBranch();
                string staffid = dt.Rows[0]["Staff_AutoID"].ToString();
                string staffnm = dt.Rows[0]["Name"].ToString();
                ddlExecutive.Items.Insert(0, new ListItem(staffnm, staffid));
                ddlExecutive.SelectedItem.Text = staffnm;
                ddlExecutive.SelectedValue = staffid;
            }
        }
        #endregion

        #region ------------ Search Record By Member Id -------------------
        protected void txtMemberId_TextChanged(object sender, EventArgs e)
        {
            try
            {

                BindByMemberID();               
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        private void BindByMemberID()
        {
            try
            {

                AssignID();
                ObjExtend.Member_ID1 = Convert.ToInt32(txtMemberId.Text);
                ObjExtend.Action = "Select By ID";
                dt = ObjExtend.Select_MemberID();
                if (dt.Rows.Count > 0)
                {
                    FillForm();
                    ViewState["DT"] = null;
                    txtContact1.Focus();

                    //   txtContact1.Focus();
                    //   MemberAutoID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"].ToString());
                    //   txtFirstName1.Text = dt.Rows[0]["FName"].ToString();
                    //   txtLastName1.Text = dt.Rows[0]["LName"].ToString();
                    //   ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
                    //   txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                    //   txtEmail1.Text = dt.Rows[0]["Email"].ToString();

                    ///*   string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //   if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    //   {
                    //   }

                    //   ObjExtend.TodayDate = TodayDate;
                    // */
                    //   AssignTodaydate();

                    //   ObjExtend.Member_AutoID = MemberAutoID;

                    //   dt = ObjExtend.Select_CoursePackage();
                    //   if (dt.Rows.Count > 0)
                    //   {
                    //       ViewState["PK"] = dt.Rows[0]["Package"].ToString();
                    //       gvExistingCourse.Visible = true;
                    //       gvExistingCourse.DataSource = dt;
                    //       gvExistingCourse.DataBind();
                    //       gvExistingCourse.Columns[0].Visible = true;

                    //       GvExtensionAssign.DataSource = null;
                    //       GvExtensionAssign.DataBind();
                    //       GvExtensionAssign.Visible = false;      
                    //   }
                    //   else
                    //   {
                    //       ClearCourseNotFound();
                    //       txtMemberId.Focus();
                    //       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Course Not Assign. !!!','Information');", true);
                    //   }

                }
                else
                {
                    ClearMemberNotFound();
                    txtMemberId.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found. !!!','Information');", true);
                }
            }            
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }

        }       
        #endregion

        #region ------------ Search Record By Member Contact Number-------------------
        protected void txtContact1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                ObjExtend.Contact1 = txtContact1.Text;
                ObjExtend.Action = "Select By Contact";
                dt = ObjExtend.Select_MemberContact();
                if (dt.Rows.Count > 0)
                {
                    ViewState["DT"] = null;
                    FillForm();                    
                    chkExecutive.Focus();
                    
                   // MemberAutoID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"].ToString());
                   // txtFirstName1.Text = dt.Rows[0]["FName"].ToString();
                   // txtLastName1.Text = dt.Rows[0]["LName"].ToString();
                   // ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
                   // txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                   // txtEmail1.Text = dt.Rows[0]["Email"].ToString();
                   // txtMemberId.Text = dt.Rows[0]["Member_ID1"].ToString();
                   // ObjExtend.Member_AutoID = MemberAutoID;
                                       
                   ///*
                   // string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                   // if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                   // {
                   // }

                   // ObjExtend.TodayDate = TodayDate;
                   // */

                   // AssignTodaydate();
                    
                   // dt = ObjExtend.Select_CoursePackage();
                   // if (dt.Rows.Count > 0)
                   // {
                   //     ViewState["PK"] = dt.Rows[0]["Package"].ToString();
                   //     gvExistingCourse.Visible = true;
                   //     gvExistingCourse.DataSource = dt;
                   //     gvExistingCourse.DataBind();
                   //     gvExistingCourse.Columns[0].Visible = true;

                   //     GvExtensionAssign.DataSource = null;
                   //     GvExtensionAssign.DataBind();
                   //     GvExtensionAssign.Visible = false;   

                   // }
                   // else
                   // {
                   //     ClearCourseNotFound();
                   //     txtContact1.Focus();
                   //     ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Course Not Assign. !!!','Information');", true);
                   // }
                }
                else
                {
                    ClearMemberNotFound();
                    txtContact1.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found. !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ----------- Fill Extension Form -------------
        protected void FillForm()
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    Member_Id1 = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());

                    if (dt.Rows[0]["BlockStatus"].ToString() != "Block")
                    {
                        MemberAutoID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"].ToString());
                        txtMemberId.Text = dt.Rows[0]["Member_ID1"].ToString();
                        txtFirstName1.Text = dt.Rows[0]["FName"].ToString();
                        txtLastName1.Text = dt.Rows[0]["LName"].ToString();
                        ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
                        txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                        txtEmail1.Text = dt.Rows[0]["Email"].ToString();

                        AssignTodaydate();

                        ObjExtend.Member_AutoID = MemberAutoID;

                        dt = ObjExtend.Select_CoursePackage();
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["PK"] = dt.Rows[0]["Package"].ToString();
                            gvExistingCourse.Visible = true;
                            gvExistingCourse.DataSource = dt;
                            gvExistingCourse.DataBind();
                            gvExistingCourse.Columns[0].Visible = true;

                            GvExtensionAssign.DataSource = null;
                            GvExtensionAssign.DataBind();
                            GvExtensionAssign.Visible = false;
                        }
                        else
                        {
                            ClearCourseNotFound();
                            txtMemberId.Focus();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Course Not Assign. !!!','Information');", true);
                        }
                    }
                    else
                    {
                        string url = "Termination.aspx?Member_ID=" + Member_Id1 + " &FNameExtensionPage=" + HttpUtility.UrlEncode("MembershipExtension");
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "text", "showConfirmation('" + url + "')", true);
                    }

                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ------------ Clear Function When Member Not Found -------------------
        private void ClearMemberNotFound()
        {
            MemberAutoID = 0;
            txtMemberId.Text = string.Empty;
            txtFirstName1.Text = "";
            txtLastName1.Text = "";
            ddlGender1.SelectedIndex = 0;
            txtContact1.Text = "";
            txtEmail1.Text = "";

            gvExistingCourse.Visible = false;
            gvExistingCourse.DataSource = null;
            gvExistingCourse.DataBind();

            GvExtensionAssign.Visible = false;
            GvExtensionAssign.DataSource = null;            
            GvExtensionAssign.DataBind();
                        
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;
        }
        #endregion

        #region ------------ Clear Function When Course Not Found -------------------
        private void ClearCourseNotFound()
        {
            gvExistingCourse.Visible = false;
            gvExistingCourse.DataSource = null;
            gvExistingCourse.DataBind();

            GvExtensionAssign.Visible = false;
            GvExtensionAssign.DataSource = null;
            GvExtensionAssign.DataBind();
        }
        #endregion
        
        #region ------------ Edit(Add) Button --------------
        bool chkAllReadyExtend = false;
        public static int k = 0;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int flag = 0;
                AssignID();
               
                /*
                string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                {
                }

                ObjExtend.TodayDate = TodayDate;
                */

                AssignTodaydate();

                ObjExtend.Member_AutoID = MemberAutoID;
                ObjExtend.Course_Auto = Convert.ToInt32(e.CommandArgument.ToString());
                ObjExtend.Action = "Check_ExtendByMemberAutoId";
                chkAllReadyExtend = ObjExtend.Check_AllReadyExtendByMemberAutoId();

                if (chkAllReadyExtend == true)
                {
                    flag = 1;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('This Course Is Already Extended. !!!','Information');", true);
                    return;
                }

                if (flag == 0)
                {
                    int Course_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                    var btnPre = (Control)sender;
                    GridViewRow row1 = (GridViewRow)btnPre.NamingContainer;

                    DataRow dr = null;
                    dt1.Columns.Add(new DataColumn("ID"));
                    dt1.Columns.Add(new DataColumn("Course_AutoID"));
                    dt1.Columns.Add(new DataColumn("ReceiptID"));
                    dt1.Columns.Add(new DataColumn("Package"));
                    dt1.Columns.Add(new DataColumn("Duration"));
                    dt1.Columns.Add(new DataColumn("Session"));
                    dt1.Columns.Add(new DataColumn("StartDate"));
                    dt1.Columns.Add(new DataColumn("EndDate"));
                    dt1.Columns.Add(new DataColumn("ExtendDays"));
                    dt1.Columns.Add(new DataColumn("NewEndDate"));
                    dt1.Columns.Add(new DataColumn("ExtensionReason"));

                    if (ViewState["DT"] != null)
                    {
                        dt1 = (DataTable)ViewState["DT"];
                    }

                    bool exists = dt1.Select().ToList().Exists(row => row["Course_AutoID"].ToString().ToUpper() == e.CommandArgument.ToString());

                    if (exists == false)
                    {
                        k = dt1.Rows.Count;
                        dr = dt1.NewRow();

                        dr["ID"] = k;
                        dr["Course_AutoID"] = Course_AutoID;
                        dr["ReceiptID"] = row1.Cells[1].Text;
                        dr["Package"] = row1.Cells[2].Text;
                        dr["Duration"] = row1.Cells[3].Text;
                        dr["Session"] = row1.Cells[4].Text;
                        dr["StartDate"] = row1.Cells[5].Text;
                        dr["EndDate"] = row1.Cells[6].Text;
                        dr["ExtendDays"] = "";
                        dr["NewEndDate"] = row1.Cells[6].Text;
                        dr["ExtensionReason"] = "";

                        dt1.Rows.InsertAt(dr, k);
                        k++;
                    }

                    ViewState["DT"] = dt1;
                    GvExtensionAssign.Visible = true;
                    GvExtensionAssign.DataSource = dt1;
                    GvExtensionAssign.DataBind();

                    TextBox txtsExtensionDay = (TextBox)GvExtensionAssign.Rows[GvExtensionAssign.Rows.Count - 1].FindControl("txtExtendDays");
                    txtsExtensionDay.Focus();
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region -------------- Remove Row From Gridview ---------------
        protected void GvExtensionAssign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                dt1 = (DataTable)ViewState["DT"];
                dt1.Rows[index].Delete();
                ViewState["DT"] = dt1;
                GvExtensionAssign.DataSource = dt1;
                GvExtensionAssign.DataBind();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
           
        }
        #endregion

        #region ------------ Days Text Change Event ---------------
        protected void txtsExtDay_TextChanged(object sender, EventArgs e)
        {
            try
            {                                              
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
                dt1 = (DataTable)ViewState["DT"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr;
                dr = dt1.NewRow();
                int s = currentRow.RowIndex;

                if (btnSave.Text == "Save")
                {
                    dr["ID"] = s;
                }

                dr["Course_AutoID"] = row.Cells[1].Text;
                dr["ReceiptID"] = row.Cells[2].Text;
                dr["Package"] = row.Cells[3].Text;
                dr["Duration"] = row.Cells[4].Text;
                dr["Session"] = row.Cells[5].Text;
                //dr["StartDate"] = row.Cells[6].Text.Trim();
                //dr["EndDate"] = row.Cells[7].Text.Trim();
                
                CourseSDate = row.Cells[6].Text.Trim();
                CourseEndDate = row.Cells[7].Text.Trim();

                if (DateTime.TryParseExact(CourseSDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                {
                }
                              
                
                if (DateTime.TryParseExact(CourseEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                {
                }

                if (btnSave.Text == "Save")
                {
                    dr["StartDate"] = CourseStartDate.ToString("dd-MM-yyyy");
                    dr["EndDate"] = CourseOldEndDate.ToString("dd-MM-yyyy");
                }
                else
                {
                    dr["StartDate"] = CourseStartDate;
                    dr["EndDate"] = CourseOldEndDate;
                }

                TextBox txtExtendDays = (TextBox)currentRow.FindControl("txtExtendDays");
                
                int ExtendDays = Convert.ToInt32(txtExtendDays.Text);
                dr["ExtendDays"] = ExtendDays.ToString();
                CourseNewEndDate = CourseOldEndDate.AddDays(ExtendDays);
                
                if (btnSave.Text == "Save")
                {                    
                    dr["NewEndDate"] = CourseNewEndDate.ToString("dd-MM-yyyy");
                }
                else
                {
                    dr["NewEndDate"] = CourseNewEndDate;
                }

                TextBox txtExtenReason = (TextBox)currentRow.FindControl("txtExtensionReason");
                string reason = txtExtenReason.Text.ToString();
                dr["ExtensionReason"] = reason;


                dt1.Rows[s].Delete();
                dt1.Rows.InsertAt(dr, s);
                ViewState["DT"] = dt1;
                GvExtensionAssign.DataSource = dt1;
                GvExtensionAssign.DataBind();

                txtExtenReason.Focus();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion      

        #region ---------- Clear Function --------------
        public void Clear()
        {
            ViewState["DT"] = null;
            MemberAutoID = 0;
            txtMemberId.Text = string.Empty;
            txtMemberId.Enabled = true;
            txtMemberId.Focus();
            txtFirstName1.Text = string.Empty;
            txtLastName1.Text = string.Empty;
            ddlGender1.SelectedIndex = 0;
            txtContact1.Text = string.Empty;
            txtContact1.Enabled = true;
            txtEmail1.Text = string.Empty;
            btnSave.Text = "Save";

            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            string NowDateTime = localTime.ToString("dd-MM-yyyy");

            txtExtensionDate.Text = NowDateTime;
            //txtExtensionDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;

            gvExistingCourse.Visible = false;
            gvExistingCourse.DataSource = null;            
            gvExistingCourse.DataBind();
            gvExistingCourse.Columns[0].Visible = true;

            GvExtensionAssign.Visible = false;
            GvExtensionAssign.DataSource = null;
            GvExtensionAssign.DataBind();
            GvExtensionAssign.Columns[0].Visible = true;

            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            //txtSearch.Enabled = false;

            gvMembershipExtension.DataSource = null;
            gvMembershipExtension.DataBind();
            gvMembershipExtension.Visible = false;

        }
        #endregion

        #region ------------ Clear Button ------
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion

        #region ------------ Save Button ----------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberId.Text != string.Empty || txtContact1.Text != string.Empty)
                {
                    AssignID();
                    //ObjExtend.Member_ID1 = Convert.ToInt32(txtMemberId.Text);                
                    ObjExtend.Member_AutoID = MemberAutoID;

                    if (btnSave.Text == "Save")
                    {
                        ObjExtend.Action = "Insert";
                        res = AddParameter();

                        if (res > 0)
                        {
                            Clear();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully. !!!','Success');", true);
                            if (Request.QueryString["FNameMemDetails"] != null)
                            {
                                int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                                Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                            }
                            else if (Request.QueryString["FNameSearchPage"] != null)
                            {
                                int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                                Response.Redirect("SearchPage.aspx?Member_AutoID=" + memberid + " &FNameSearchPage1=" + HttpUtility.UrlEncode("FNameSearchPage1".ToString()));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed. !!!','Error');", true);
                        }

                    }
                    else if (btnSave.Text == "Update")
                    {
                        ObjExtend.Action = "UPDATE";
                        ObjExtend.Extension_AutoID = Extension_AutoID;
                        res = AddParameter();

                        if (res > 0)
                        {
                            Clear();
                            divMemExtension.Visible = false;
                            divMemExtensionDetails.Visible = true;
                            divsearch.Visible = true;
                            divFormDetails.Visible = false;
                            SearchByDateFunction();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully. !!!','Success');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Updated Failed. !!!','Error');", true);
                        }
                    }

                }
                else
                {
                    txtMemberId.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter All Fields. !!!','Error');", true);
                }               

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ---------- Add Parameter ------------
        private int AddParameter()
        {
            int flag = 0;
            foreach (GridViewRow row in GvExtensionAssign.Rows)
            {
                TextBox txtExtendDays = (TextBox)row.FindControl("txtExtendDays");

                if (txtExtendDays.Text == string.Empty)
                {
                    flag = 1;
                    txtExtendDays.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Extend Days Should Not Be Empty. !!!','Error');", true);
                }
                else if (txtExtendDays.Text == "0")
                {
                    flag = 1;
                    txtExtendDays.Text = string.Empty;
                    txtExtendDays.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Extend Days Should Not Be Zero. !!!','Error');", true);
                }

                TextBox txtNewEndDate = (TextBox)row.FindControl("txtNextEndDate");
                                       
                if (DateTime.TryParseExact(txtNewEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewEndDate))
                {
                }                    
                
                AssignTodaydate();
                
                if(CourseNewEndDate.Date < TodayDate.Date)
                {
                    flag = 1;
                    txtExtendDays.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Next End Date Of Course Always Greaters Than Extended Date. !!!','Error');", true);
                }

            }

            if (flag != 1)
            {
                foreach (GridViewRow row in GvExtensionAssign.Rows)
                {
                    /*
                    string regDate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    if (DateTime.TryParseExact(regDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    ObjExtend.TodayDate = TodayDate;
                    */

                    AssignTodaydate();
                    string extendDate=txtExtensionDate.Text;

                    DateTime ExtendDate;
                    if (DateTime.TryParseExact(extendDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ExtendDate))
                    {
                    }

                    ObjExtend.ExtensionDate = ExtendDate;

                    ObjExtend.Course_Auto = Convert.ToInt32(row.Cells[1].Text);
                    //ObjExtend.StartDate = Convert.ToDateTime(row.Cells[6].Text);
                    //ObjExtend.OldEndDate = Convert.ToDateTime(row.Cells[7].Text);

                    CourseSDate = row.Cells[6].Text.Trim();
                    CourseEndDate = row.Cells[7].Text.Trim();

                    if (DateTime.TryParseExact(CourseSDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                    {
                    }


                    if (DateTime.TryParseExact(CourseEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                    {
                    }

                    ObjExtend.StartDate = CourseStartDate;
                    ObjExtend.OldEndDate = CourseOldEndDate;

                    TextBox txtNewEndDate = (TextBox)row.FindControl("txtNextEndDate");
                                       
                    if (DateTime.TryParseExact(txtNewEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewEndDate))
                    {
                    }                    
                    ObjExtend.NewEndDate = CourseNewEndDate;                                                                           
                    
                    TextBox txtExtendDays = (TextBox)row.FindControl("txtExtendDays");
                    ObjExtend.ExtendDays = Convert.ToInt32(txtExtendDays.Text);

                    TextBox txtExtensionReason = (TextBox)row.FindControl("txtExtensionReason");
                    ObjExtend.ExtensionReason = txtExtensionReason.Text;

                    ObjExtend.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

                    res = ObjExtend.Insert();
                    //int result = ObjExtend.UpdateDate();
                }
            }
            return res;
        }
        #endregion

        #region ------------ Check Executive Changed -------------------
        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
            {
                ddlExecutive.Enabled = false;
                chkExecutive.Focus();
            }
            else
            {
                ddlExecutive.Enabled = true;
                ddlExecutive.Focus();
            }         
        }
        #endregion

        //#region ----------- On Search DDL Index Changed -------------
        //protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtSearch.Text = string.Empty;
        //    if (ddlSearch.SelectedValue.ToString() == "--Select--")
        //    {
        //        txtSearch.Style.Add("border", "1px solid silver ");
        //        txtSearch.Enabled = false;

        //    }
        //    else
        //    {
        //        txtSearch.Enabled = true;
        //    }
        //    ddlSearch.Focus();
        //}
        //#endregion

        #region -------------- Search Button -------------
        protected void btnSearch_Click(object sender, EventArgs e)
        {            
            try
            {
                SearchByDateFunction(); 
                    //BindGridViewDetails();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void SearchByDateFunction()
        {
            try
            {
                flag = chkFromDateNotLessToDate();

                if (flag == 0)
                {
                    AssignID();
                    ObjExtend.Action = "BindDetails";
                    ObjExtend.Category = "Get_By_Date";

                    BindGridViewDetails();

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Bind Extension Details Gridview --------------
        private void BindGridViewDetails()
        {
            //SeacrhAction();
            //AssignID();

            ViewState["ExtensionDetails"] = ObjExtend.GetDetails();
            dt = (DataTable)ViewState["ExtensionDetails"];
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvMembershipExtension.Visible = true;
                gvMembershipExtension.DataSource = dt;
                gvMembershipExtension.DataBind();
            }
            else
            {
                gvMembershipExtension.DataSource = dt;
                gvMembershipExtension.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
            }


             if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvMembershipExtension.Columns[0].Visible = true;
                   
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvMembershipExtension.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvMembershipExtension.Columns[0].Visible = true;
                    
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvMembershipExtension.Columns[0].Visible = true;
                    
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvMembershipExtension.Columns[0].Visible = false;                    
                }

        }
        #endregion

        #region ------------- Search Action For BindGridView -----------------
        private void SeacrhAction()
        {
            try
            {
                ObjExtend.Action = "BindDetails";

                if (ddlSearch.SelectedValue.ToString() == "MemberID")
                {
                    ObjExtend.Category = "Date_MemberID";
                    ObjExtend.SearchByText = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "MFName")
                {
                    ObjExtend.Category = "Date_MemberFirstName";
                    ObjExtend.SearchByText = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "MLName")
                {
                    ObjExtend.Category = "Date_MemberLastName";
                    ObjExtend.SearchByText = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Contact")
                {
                    ObjExtend.Category = "Date_Contact";
                    ObjExtend.SearchByText = txtSearch.Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                    ddlSearch.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------- Check From Date And To Date Validation ---------------
        protected int chkFromDateNotLessToDate()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtToDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Enter To Date !!!','Information');", true);
            }
            else
            {

                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FromDate))
                {
                }

                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ToDate))
                {
                }

                if (FromDate.Date <= ToDate.Date)
                {
                    flag = 0;
                    ObjExtend.MStartDate = FromDate;
                    ObjExtend.MEndDate = ToDate;
                }
                else
                {
                    flag = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                }
            }

            return flag;

        }
        #endregion

        #region ------------ Search On Text Change ----------------
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != string.Empty)
                {

                    AssignID();

                    ObjExtend.Action = "BindDetails";

                    if (ddlSearch.SelectedValue.ToString() == "MemberID")
                    {
                        ObjExtend.Category = "MemberID";
                        ObjExtend.SearchByText = txtSearch.Text;

                    }
                    else if (ddlSearch.SelectedValue.ToString() == "MFName")
                    {
                        ObjExtend.Category = "MemberFirstName";
                        ObjExtend.SearchByText = txtSearch.Text;
                    }
                    else if (ddlSearch.SelectedValue.ToString() == "MLName")
                    {
                        ObjExtend.Category = "MemberLastName";
                        ObjExtend.SearchByText = txtSearch.Text;
                    }
                    else if (ddlSearch.SelectedValue.ToString() == "Contact")
                    {
                        ObjExtend.Category = "Contact";
                        ObjExtend.SearchByText = txtSearch.Text;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                        ddlSearch.Focus();
                        return;
                    }

                    BindGridViewDetails();
                    btnSearchCategory.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Select Category !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region -------------- Search Button By Date and Category-------
        protected void btnSearchCategory_Click(object sender, EventArgs e)
        {
            try
            {
                flag = chkFromDateNotLessToDate();
                if (flag == 0)
                {
                    if (ddlSearch.SelectedValue.ToString() != "--Select--")
                    {
                        if (txtSearch.Text != string.Empty)
                        {
                            AssignID();
                            SeacrhAction();
                            BindGridViewDetails();

                        }
                        else
                        {
                            txtSearch.Focus();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Enter Data On Search Field !!!','Information');", true);
                        }

                    }
                    else
                    {
                        ddlSearch.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Please Select Categry !!!','Information');", true);
                    }

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region ----------- Edit GridView Record Button -----------
        protected void btnExtensionEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;

                AssignTodaydate();

                //string CourseNewEndDate1 = row.Cells[12].Text.Trim();
                string CourseNewEndDate1 = row.Cells[GetColumnIndexByName(gvMembershipExtension, "Course New End Date")].Text.Trim();
                if (DateTime.TryParseExact(CourseNewEndDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewEndDate))
                {
                }

                if (CourseNewEndDate.Date > TodayDate.Date)
                {

                    Extension_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                    ObjExtend.Extension_AutoID = Extension_AutoID;

                    AssignID();
                    ObjExtend.Action = "EDIT";
                    dt = ObjExtend.GetDetails();

                    if (dt.Rows.Count > 0)
                    {
                        divMemExtension.Visible = true;
                        divMemExtensionDetails.Visible = false;
                        divsearch.Visible = false;
                        divFormDetails.Visible = true;

                        ViewState["DT"] = dt;                        
                        btnSave.Text = "Update";

                        txtMemberId.Text = dt.Rows[0]["Member_ID1"].ToString();
                        txtMemberId.Enabled = false;
                        txtFirstName1.Text = dt.Rows[0]["FName"].ToString();
                        txtLastName1.Text = dt.Rows[0]["LName"].ToString();
                        ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
                        txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                        txtContact1.Enabled = false;
                        txtEmail1.Text = dt.Rows[0]["Email"].ToString();

                        DateTime Extdate = Convert.ToDateTime(dt.Rows[0]["TodayDate"].ToString());
                        DateTime Extenddate;
                        if (DateTime.TryParseExact(Extdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Extenddate))
                        {
                            txtExtensionDate.Text = Extenddate.ToString("dd-MM-yyyy");
                        }                                               

                        gvExistingCourse.DataSource = dt;
                        gvExistingCourse.DataBind();
                        gvExistingCourse.Visible = true;
                        gvExistingCourse.Columns[0].Visible = false;

                        GvExtensionAssign.DataSource = dt;
                        GvExtensionAssign.DataBind();
                        GvExtensionAssign.Visible = true;
                        GvExtensionAssign.Columns[0].Visible = false;

                        TextBox txtExtendDay = (TextBox)GvExtensionAssign.Rows[GvExtensionAssign.Rows.Count - 1].FindControl("txtExtendDays");
                        txtExtendDay.Focus();                        
                    }
                }
                else
                {
                    //Can't Be Edit Because Course Is Expired
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Could Not Be Edit Because Of Course Expired. !!!','Information');", true);
                    
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region -------------- Delete Gridview Rocord ----------------
        protected void btnExtensionDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;                

                AssignTodaydate();

                CourseEndDate = row.Cells[GetColumnIndexByName(gvMembershipExtension, "Course End Date")].Text.Trim();

                //CourseEndDate = row.Cells[10].Text.Trim();                               

                if (DateTime.TryParseExact(CourseEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                {
                }


                //string  CourseNewEndDate1 = row.Cells[12].Text.Trim();
                string CourseNewEndDate1 = row.Cells[GetColumnIndexByName(gvMembershipExtension, "Course New End Date")].Text.Trim();
                if (DateTime.TryParseExact(CourseNewEndDate1, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewEndDate))
                {
                }

                if (CourseOldEndDate.Date < TodayDate.Date && CourseNewEndDate.Date > TodayDate.Date)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Could Not Be Deleted Because Of Extension Duration. !!!','Information');", true);
                }
                else
                {
                    int Course_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                    ObjExtend.Course_Auto = Course_AutoID;

                    AssignID();
                    ObjExtend.OldEndDate = CourseOldEndDate;
                    ObjExtend.NewEndDate = CourseNewEndDate;
                    ObjExtend.Action = "DELETE";

                    res = ObjExtend.Insert();

                    if (res > 0)
                    {
                        BindGridViewDetails();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully. !!!','Success');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Deleted Failed. !!!','Information');", true);
                    }

                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region -------------- Get Column index of GridView By Column Name --------------
        private int GetColumnIndexByName(GridView grid, string name)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText.ToLower().Trim() == name.ToLower().Trim())
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion

        #region -------- Export To Excle --------------
        protected void btnExpord_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToExcel()
        {
            try
            {
                
                if (ViewState["ExtensionDetails"] != null)
                {
                    dt = (DataTable)ViewState["ExtensionDetails"];
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=ExtensionDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvMembershipExtension.Columns[0].Visible = false;
                            gvMembershipExtension.Columns[1].Visible = false;
                            gvMembershipExtension.AllowPaging = false;

                            gvMembershipExtension.DataSource = dt;
                            gvMembershipExtension.DataBind();
                            gvMembershipExtension.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMembershipExtension.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMembershipExtension.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvMembershipExtension.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                    List<Control> controls = new List<Control>();
                                    //Add controls to be removed to Generic List
                                    foreach (Control control in cell.Controls)
                                    {
                                        controls.Add(control);
                                    }

                                    foreach (Control control in controls)
                                    {
                                        switch (control.GetType().Name)
                                        {
                                            case "HyperLink":
                                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                                break;

                                            case "LinkButton":
                                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                                break;

                                        }
                                        cell.Controls.Remove(control);

                                    }
                                }
                            }


                            gvMembershipExtension.GridLines = GridLines.Both;
                            gvMembershipExtension.RenderControl(hw);

                            //style to format numbers to string

                            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            //Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Data GridVIew Is Empty , Can Not Export !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Data GridVIew Is Empty , Can Not Export !!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        #endregion

        #region ---------------- Refresh Button --------------
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = "";
                //txtSearch.Enabled = false;
                Assign_MonthDate();
                dt = null;
                ViewState["ExtensionDetails"] = dt;
                gvMembershipExtension.DataSource = dt;
                gvMembershipExtension.DataBind();
                SearchByDateFunction();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- Membership Extension Details Page Indexing --------------
        protected void gvMembershipExtension_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMembershipExtension.PageIndex = e.NewPageIndex;

                if (ViewState["ExtensionDetails"] != null)
                {
                    DataTable dataTable1 = (DataTable)ViewState["ExtensionDetails"];

                    gvMembershipExtension.DataSource = dataTable1;
                    gvMembershipExtension.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion



    }
}