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


namespace NDOnlineGym_2017
{
    public partial class MembershipFreezing : System.Web.UI.Page
    {
        BalFreezingMember objFreezing = new BalFreezingMember();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();

        DataTable dataTable = new DataTable();
        DataTable dt = new DataTable();
        DataSet dataSet = new DataSet();
        int res;
        static int MemberAutoID;
        static int Member_Id1 = 0;
        static int Freezing_AutoID;
        string CourseSD = "";
        string CourseED = "";
        int flag = 0;

        DateTime TodayDate;


       

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                    string NowDateTime = localTime.ToString("dd-MM-yyyy");

                    ViewState["DT"] = null;
                    txtMemberId.Focus();                   
                    txtFreezedDate.Text = NowDateTime;
                    bindDDLExecutive();
                    setExecutive();

                    if (Request.QueryString["Member_ID"] != null)
                    {
                        int MemAutoID = Convert.ToInt32(Request.QueryString["Member_ID"]);
                        GetMember_ID1_ByMemberAutoID(MemAutoID);
                    }

                    // Redirect from Termination Form 
                    if (Request.QueryString["MemberID"] != null)
                    {
                        txtMemberId.Text = Request.QueryString["MemberID"].ToString();
                        CourseMemeberID();
                    }

                    if (Request.QueryString["MenuFreezingDetails"] != null)
                    {
                        divMemFrezzing.Visible = false;
                        divMemFrezzingDetails.Visible = true;
                        divsearch.Visible = true;
                        divFormDetails.Visible = false;
                        Assign_MonthDate();
                        txtFromDate.Focus();
                        SearchByDateFunction(); 
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void GetMember_ID1_ByMemberAutoID(int MemAutoID)
        {
            AssignID();
            objFreezing.Member_AutoID = MemAutoID;
            dt = objFreezing.GetMember_ID1_ByMemberAutoID();
            if (dt.Rows.Count > 0)
            {
                ViewState["Member_ID1"] = Convert.ToInt32(dt.Rows[0]["Member_ID1"]);
                CourseMemeberID();
            }
        }        

        #region ----------- Assign Branch Id And Company ID -----------
        private void AssignID()
        {
            objFreezing.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objFreezing.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region ----------- Assign Today date ---------------
        private void AssignTodaydate()
        {
            string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");
            if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
            {
            }

            objFreezing.TodayDate = TodayDate;
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
        }
        #endregion

        #region ----------- Bind Executive DDL ------------
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Set Executive ----------
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

        #region ------------ Search Record By Member Id Exist In Course -------------------
        protected void txtMemberId_TextChanged(object sender, EventArgs e)
        {
            ViewState["DT"] = null;
            CourseMemeberID();

        }

        public void CourseMemeberID()
        {
            try
            {               
                AssignID();

                if (Request.QueryString["Member_ID"] != null)
                    objFreezing.Member_ID1 = Convert.ToInt32(ViewState["Member_ID1"]);
                else
                objFreezing.Member_ID1 = Convert.ToInt32(txtMemberId.Text);

                objFreezing.Action = "GetDetailsByMemberAutoID";
                dataTable = objFreezing.GetMemeberDetails();
                if (dataTable.Rows.Count > 0)
                {
                    ClearCourseNotFound();
                    BindMemberCourse();
                    txtContact1.Focus();
                }
                else
                {
                    ClearMemberNotFound();
                    txtMemberId.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
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

            GvFreezingAssign.Visible = false;
            GvFreezingAssign.DataSource = null;
            GvFreezingAssign.DataBind();

            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;
        }
        #endregion

        #region ------------ Search Record By Contatct1 Exist In Course -------------------
        protected void txtContact1_TextChanged(object sender, EventArgs e)
        {
            ViewState["DT"] = null;
            CourseMemeberContact();
        }
        public void CourseMemeberContact()
        {
            try
            {                
                AssignID();
                objFreezing.Contact1 = txtContact1.Text;
                objFreezing.Action = "GetDetailsByMemberCon1";
                dataTable = objFreezing.GetMemeberDetails();
                if (dataTable.Rows.Count > 0)
                {
                    ClearCourseNotFound();
                    BindMemberCourse();
                    chkExecutive.Focus();
                }
                else
                {
                    ClearMemberNotFound();
                    txtContact1.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ------------ Bind Course Member ----------------
        public void BindMemberCourse()
        {
            //ViewState["DT"] = null;

            if (dataTable.Rows.Count > 0)
            {
                Member_Id1 = Convert.ToInt32(dataTable.Rows[0]["Member_ID1"].ToString());

                if (dataTable.Rows[0]["BlockStatus"].ToString() != "Block")
                {
                    MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                    txtMemberId.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                    txtFirstName1.Text = dataTable.Rows[0]["FName"].ToString();
                    txtLastName1.Text = dataTable.Rows[0]["LName"].ToString();
                    ddlGender1.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                    txtContact1.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtEmail1.Text = dataTable.Rows[0]["Email"].ToString();

                    AssignID();
                    AssignTodaydate();
                    objFreezing.TodayDate = TodayDate;
                    objFreezing.Member_AutoID = MemberAutoID;

                    dt = objFreezing.Select_CoursePackage();
                    if (dt.Rows.Count > 0)
                    {

                        gvExistingCourse.DataSource = dt;//dataTable;
                        gvExistingCourse.DataBind();
                        gvExistingCourse.Visible = true;
                        gvExistingCourse.Columns[0].Visible = true;

                        //GvFreezingAssign.DataSource = null;
                        //GvFreezingAssign.DataBind();
                        //GvFreezingAssign.Visible = false;                
                    }
                    else
                    {
                        ClearCourseNotFound();
                        txtMemberId.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Course Not Assign !!!','Error');", true);
                    }

                }
                else
                {
                    string url = "Termination.aspx?Member_ID=" + Member_Id1 + " &FNameFreezingPage=" + HttpUtility.UrlEncode("MemberFreezing");
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "text", "showConfirmation('" + url + "')", true);
                }


            }
                      
        }
        #endregion

        #region ------------ Clear Function When Course Not Found -------------------
        private void ClearCourseNotFound()
        {
            gvExistingCourse.Visible = false;
            gvExistingCourse.DataSource = null;
            gvExistingCourse.DataBind();

            GvFreezingAssign.Visible = false;
            GvFreezingAssign.DataSource = null;
            GvFreezingAssign.DataBind();
        }
        #endregion

        #region ------------ Edit(Add) Button --------------
        bool chkAllReadyFreezed = false;
        public static int k = 0;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {

                int flag = 0;
                
                AssignID();
                objFreezing.Course_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                objFreezing.Member_AutoID = MemberAutoID;
                objFreezing.Action = "Check_FreezedByMemberAutoId";

                //string Todate = DateTime.Now.Date.ToString("dd-MM-yyyy");   
             
                //if (DateTime.TryParseExact(Todate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                //{
                //}

                AssignTodaydate();
                objFreezing.TodayDate = TodayDate;

                chkAllReadyFreezed = objFreezing.Check_AllReadyFreezedByMemberAutoId();


                if (chkAllReadyFreezed == true)
                {
                    flag = 1;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('This Course Is Already Freezed !!!','Information');", true);
                    return;
                }

                if (flag == 0)
                {
                    int Course_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                    var btnPre = (Control)sender;
                    GridViewRow row1 = (GridViewRow)btnPre.NamingContainer;
                    string CourseED = row1.Cells[5].Text.Trim();
                    DateTime CourseOldEndDate;
                    if (DateTime.TryParseExact(CourseED, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                    {
                    }

                    string FSDate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    //DateTime TodayDate;
                    if (DateTime.TryParseExact(FSDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                    {
                    }

                    // Check Course End Date Is Not Less Than Today Date
                    if (CourseOldEndDate.Date > TodayDate.Date)
                    {
                        DataRow dr = null;
                        //dt.Clear();
                        dataTable.Columns.Add(new DataColumn("ID"));
                        dataTable.Columns.Add(new DataColumn("Course_AutoID"));
                        dataTable.Columns.Add(new DataColumn("Package"));
                        dataTable.Columns.Add(new DataColumn("Duration"));
                        dataTable.Columns.Add(new DataColumn("Session"));
                        dataTable.Columns.Add(new DataColumn("StartDate"));
                        dataTable.Columns.Add(new DataColumn("EndDate"));
                        dataTable.Columns.Add(new DataColumn("FreezingStartDate"));
                        dataTable.Columns.Add(new DataColumn("FreezingDays"));
                        dataTable.Columns.Add(new DataColumn("FreezingEndDate"));
                        dataTable.Columns.Add(new DataColumn("CourseNewEndDate"));
                        dataTable.Columns.Add(new DataColumn("FreezingReason"));
                        //dataTable.Columns.Add(new DataColumn("StaffName"));

                        if (ViewState["DT"] != null)
                        {
                            dataTable = (DataTable)ViewState["DT"];
                        }

                        bool exists = dataTable.Select().ToList().Exists(row => row["Course_AutoID"].ToString().ToUpper() == e.CommandArgument.ToString());

                        if (exists == false)
                        {

                            k = dataTable.Rows.Count;
                            dr = dataTable.NewRow();
                            dr["ID"] = k;
                            dr["Course_AutoID"] = Course_AutoID;//row1.Cells[0].Text;
                            dr["Package"] = row1.Cells[1].Text;
                            dr["Duration"] = row1.Cells[2].Text;
                            dr["Session"] = row1.Cells[3].Text;
                            dr["StartDate"] = row1.Cells[4].Text;
                            dr["EndDate"] = row1.Cells[5].Text;
                            dr["FreezingStartDate"] = FSDate;
                            dr["FreezingDays"] = "";
                            dr["FreezingEndDate"] = FSDate;

                            string CEnddate = row1.Cells[5].Text.Trim();
                            dr["CourseNewEndDate"] = CEnddate;

                            dr["FreezingReason"] = "";

                            //dr["StaffName"] = Request.Cookies["OnlineGym"]["Login_ID"];

                            dataTable.Rows.InsertAt(dr, k);
                            k++;

                        }

                        ViewState["DT"] = dataTable;
                        GvFreezingAssign.DataSource = dataTable;
                        GvFreezingAssign.DataBind();
                        GvFreezingAssign.Visible = true;
                        GvFreezingAssign.Columns[0].Visible = true;

                        TextBox txtFreezingDate = (TextBox)GvFreezingAssign.Rows[GvFreezingAssign.Rows.Count - 1].FindControl("txtFreezingDate");
                        txtFreezingDate.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Can't Be Freez Because Course Is Expired !!!','Error');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region -------------- Remove Row From Gridview ---------------
        protected void GvFreezingAssign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);

                dataTable = (DataTable)ViewState["DT"];

                dataTable.Rows[index].Delete();

                ViewState["DT"] = dataTable;

                GvFreezingAssign.DataSource = dataTable;
                GvFreezingAssign.DataBind();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ------------ Days Text Change Event ---------------
        protected void txtExtends_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int flag = 0;
                DateTime CourseOldEndDate;
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;//.Parent.Parent;
                dataTable = (DataTable)ViewState["DT"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr;
                dr = dataTable.NewRow();

                // Get Freezing Start Date 
                TextBox txtFSdate = (TextBox)currentRow.FindControl("txtFreezingDate");
                DateTime FSdate;
                if (DateTime.TryParseExact(txtFSdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FSdate))
                {
                }

                TextBox txtExtends = (TextBox)currentRow.FindControl("txtExtends");

                if (txtExtends.Text != string.Empty && txtExtends.Text !="0")
                {
                    int FreezingDays = Convert.ToInt32(txtExtends.Text);

                    DateTime FStartDate = FSdate;
                    DateTime FEndDate = FStartDate.AddDays(FreezingDays - 1);

                    AssignID();
                    objFreezing.Course_AutoID = Convert.ToInt32(row.Cells[1].Text);

                    // Get Previous Frezzing Record
                    if (btnSave.Text == "Save")
                    {
                        objFreezing.Action = "Get_FreezDetailsByCourseID";
                    }
                    else
                    {
                        objFreezing.Action = "Get_FreezDetailsByFreezing_AutoID";
                        objFreezing.Freezing_AutoID = Freezing_AutoID;
                    }
                    dataSet = objFreezing.GetFreezingDetailsByCourseID();

                    CourseSD = row.Cells[5].Text.Trim();
                    DateTime CourseStartDate;
                    if (DateTime.TryParseExact(CourseSD, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                    {
                    }

                    CourseED = row.Cells[6].Text.Trim();
                    if (DateTime.TryParseExact(CourseED, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                    {
                    }

                    if (FSdate.Date < CourseStartDate.Date)
                    {
                        flag = 1;
                        txtFSdate.Focus();
                        txtExtends.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Freezing Start Date Should Not Be Less Than Course Start  Date !!!','Error');", true);

                    }
                    else if (FSdate.Date > CourseOldEndDate.Date)
                    {
                        flag = 1;
                        txtFSdate.Focus();
                        txtExtends.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Freezing Start Date Should Not Be Greater Than Course End Date !!!','Error');", true);

                    }
                    else if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {

                            DateTime FrezzingStartdate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FreezingStartDate"].ToString());
                            DateTime FrezzingEnddate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FreezingEndDate"].ToString());

                            DateTime FreezStartDate;
                            if (DateTime.TryParseExact(FrezzingStartdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezStartDate))
                            {
                            }

                            DateTime FreezEndDate;
                            if (DateTime.TryParseExact(FrezzingStartdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezEndDate))
                            {
                            }

                            if ((FreezStartDate.Date < FSdate.Date) && (FrezzingEnddate.Date > FSdate.Date))
                            {
                                flag = 1;
                                txtFSdate.Focus();
                                txtExtends.Text = string.Empty;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Course Is Allready Freezed !!!','Error');", true);

                            }
                            else if ((FreezStartDate.Date < FEndDate.Date) && (FreezEndDate.Date > FEndDate.Date))
                            {
                                flag = 1;
                                txtFSdate.Focus();
                                txtExtends.Text = string.Empty;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Course Is Allready Freezed !!!','Error');", true);
                            }
                            else if ((FreezStartDate.Date > FSdate.Date) && (FreezEndDate.Date < FEndDate.Date))
                            {
                                flag = 1;
                                txtFSdate.Focus();
                                txtExtends.Text = string.Empty;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Course Is Allready Freezed !!!','Error');", true);
                            }

                        }
                    }

                    if (flag != 1)
                    {
                        int s = currentRow.RowIndex;

                        if (btnSave.Text == "Save")
                        {
                            dr["ID"] = s;
                        }

                        dr["Course_AutoID"] = row.Cells[1].Text;
                        dr["Package"] = row.Cells[2].Text;
                        dr["Duration"] = row.Cells[3].Text;
                        dr["Session"] = row.Cells[4].Text;

                        if (btnSave.Text == "Save")
                        {
                            // Assign Course Start Date

                            /*CourseSD = row.Cells[5].Text.Trim();
                            DateTime CourseStartDate;
                            if (DateTime.TryParseExact(CourseSD, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                            {
                            }*/
                            dr["StartDate"] = CourseStartDate.ToString("dd-MM-yyyy");


                            // Assign Course Old End Date

                            /*CourseED = row.Cells[6].Text.Trim();
                            //DateTime CourseOldEndDate;
                            if (DateTime.TryParseExact(CourseED, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                            {
                            }*/
                            dr["EndDate"] = CourseOldEndDate.ToString("dd-MM-yyyy");
                            //dr["EndDate"] = row.Cells[6].Text.Trim();
                        }
                        else
                        {
                            // Assign Course Start Date
                            /* CourseSD = row.Cells[5].Text.Trim();
                             DateTime CourseStartDate;
                             if (DateTime.TryParseExact(CourseSD, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                             {
                             }*/
                            dr["StartDate"] = CourseStartDate;


                            // Assign Course Old End Date
                            /* CourseED = row.Cells[6].Text.Trim();
                    
                             if (DateTime.TryParseExact(CourseED, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                             {
                             }*/
                            dr["EndDate"] = CourseOldEndDate;
                        }


                        /*TextBox txtExtends = (TextBox)currentRow.FindControl("txtExtends");
                        int FreezingDays = Convert.ToInt32(txtExtends.Text);*/
                        dr["FreezingDays"] = FreezingDays;

                        /*TextBox txtFSdate = (TextBox)currentRow.FindControl("txtFreezingDate");
                        DateTime FSdate;
                        if (DateTime.TryParseExact(txtFSdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FSdate))
                        {
                        }*/

                        if (btnSave.Text == "Save")
                        {

                            dr["FreezingStartDate"] = FSdate.ToString("dd-MM-yyyy");

                            /*DateTime FStartDate = FSdate;
                            DateTime FEndDate = FStartDate.AddDays(FreezingDays - 1);
                            */
                            //dr["FreezingEndDate"] = FEndDate.ToString("dd-MM-yyyy");


                            dr["FreezingEndDate"] = FEndDate.ToString("dd-MM-yyyy");

                            /*TextBox txtCEDate = (TextBox)currentRow.FindControl("txtNextEndDate");
                            DateTime CourseNewEndDate;
                            if (DateTime.TryParseExact(txtCEDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewEndDate))
                            {
                            }

                            DateTime CourseEndDate = CourseNewEndDate.AddDays(FreezingDays - 1);
                            dr["CourseNewEndDate"] = CourseEndDate.ToString("dd-MM-yyyy");*/

                            DateTime CourseEndDate = CourseOldEndDate.AddDays(FreezingDays);
                            dr["CourseNewEndDate"] = CourseEndDate.ToString("dd-MM-yyyy");
                        }
                        else
                        {
                            dr["FreezingStartDate"] = FSdate;

                            /* DateTime FStartDate = FSdate;
                             DateTime FEndDate = FStartDate.AddDays(FreezingDays - 1);
                             */

                            dr["FreezingEndDate"] = FEndDate; //.ToString("dd-MM-yyyy");

                            DateTime CourseEndDate = CourseOldEndDate.AddDays(FreezingDays);
                            dr["CourseNewEndDate"] = CourseEndDate; //.ToString("dd-MM-yyyy");
                        }

                        TextBox txtFreezingReason = (TextBox)currentRow.FindControl("txtFreezingReason");
                        dr["FreezingReason"] = txtFreezingReason.Text;

                        dataTable.Rows[s].Delete();
                        dataTable.Rows.InsertAt(dr, s);
                        ViewState["DT"] = dataTable;
                        GvFreezingAssign.DataSource = dataTable;
                        GvFreezingAssign.DataBind();

                        txtFreezingReason.Focus();
                    }
                }
                else
                {
                    if(txtExtends.Text == string.Empty )
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Extend Days Should Not Be Empty !!!','Error');", true);
                    }
                    else if (txtExtends.Text == "0")
                    {
                        txtExtends.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Extend Days Should Not Be Zero !!!','Error');", true);
                    }
                    txtExtends.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ---------- Row Data Bound Event --------------
        /*  protected void GvFreezingAssign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlExecutive = (e.Row.FindControl("ddlExecutive") as DropDownList);
                AssignID();
                dataTable = objFreezing.GetExecutive();

                ddlExecutive.DataSource = dataTable;
                ddlExecutive.DataTextField = "Name";
                ddlExecutive.DataValueField = "Staff_AutoID";
                ddlExecutive.DataBind();

                //Add Default Item in the DropDownList
                ddlExecutive.Items.Insert(0, new ListItem("Please select"));

                //Select the Country of Customer in DropDownList
                string Executive = (e.Row.FindControl("lblExecutive") as Label).Text;
                ddlExecutive.Items.FindByValue(Executive).Selected = true;
            }
        }*/
        #endregion

        #region ---------- Save Button --------------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberId.Text == string.Empty || txtContact1.Text == string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter All Fields !!!','Error');", true);
                    if (txtMemberId.Text == string.Empty)
                    {
                        txtMemberId.Style.Add("border", "1px solid red ");
                    }
                    else
                    {
                        txtMemberId.Style.Add("border", "1px solid silver ");
                    }

                    if (txtContact1.Text == string.Empty)
                    {
                        txtContact1.Style.Add("border", "1px solid red ");
                    }
                    else
                    {
                        txtContact1.Style.Add("border", "1px solid silver ");
                    }
                }
                else
                {
                    if (btnSave.Text == "Save")
                    {
                        objFreezing.Action = "INSERT";
                        AssignID();

                        objFreezing.Member_AutoID = MemberAutoID;

                        res = AddParameter();

                        if (res > 0)
                        {
                            Clear();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            if (Request.QueryString["FNameMemDetails"] != null)
                            {
                                int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                                Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                            }
                            else if (Request.QueryString["Member_ID"] != null)
                            {
                                int Member_AutoID = Convert.ToInt32(Request.QueryString["Member_ID"]);
                                Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
                            }
                            else if (Request.QueryString["FNameSearchPage"] != null)
                            {
                                int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                                Response.Redirect("SearchPage.aspx?Member_AutoID=" + memberid + " &FNameSearchPage1=" + HttpUtility.UrlEncode("FNameSearchPage1".ToString()));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                        }

                    }
                    else if (btnSave.Text == "Update")
                    {

                        objFreezing.Action = "UPDATE";
                        objFreezing.Freezing_AutoID = Freezing_AutoID;
                        AssignID();

                        res = AddParameter();

                        if (res > 0)
                        {

                            Clear();

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Updated Failed !!!','Error');", true);
                        }
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

        #region -------------- Add Parameter --------------
        protected int AddParameter()
        {
            int flag = 0;
            foreach (GridViewRow row in GvFreezingAssign.Rows)
            {
                TextBox txtFreezingDays = (TextBox)row.FindControl("txtExtends");
               
                if (txtFreezingDays.Text == string.Empty)
                {
                    flag = 1;
                    txtFreezingDays.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Extend Days Should Not Be Empty !!!','Error');", true);
                }
                else if (txtFreezingDays.Text == "0")
                {
                    flag = 1;
                    txtFreezingDays.Text = string.Empty;
                    txtFreezingDays.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Extend Days Should Not Be Zero !!!','Error');", true);
                }                

            }

            if (flag != 1)
            {

                foreach (GridViewRow row in GvFreezingAssign.Rows)
                {

                    TextBox txtFreezingStartDate = (TextBox)row.FindControl("txtFreezingDate");
                    DateTime FreezingStartDate;
                    if (DateTime.TryParseExact(txtFreezingStartDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezingStartDate))
                    {
                    }

                    CourseSD = row.Cells[5].Text.Trim();
                    DateTime CourseStartDate;
                    if (DateTime.TryParseExact(CourseSD, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                    {
                    }

                    CourseED = row.Cells[6].Text.Trim();
                    DateTime CourseOldEndDate;
                    if (DateTime.TryParseExact(CourseED, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                    {
                    }

                    TextBox txtFreezingDays = (TextBox)row.FindControl("txtExtends");

                    if (FreezingStartDate.Date < CourseStartDate.Date)
                    {
                        txtFreezingStartDate.Focus();
                        txtFreezingDays.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Freezing Start Date Should Not Be Greater Than Course End Date !!!','Error');", true);
                    }
                    else if (FreezingStartDate.Date > CourseOldEndDate.Date)
                    {
                        txtFreezingStartDate.Focus();
                        txtFreezingDays.Text = string.Empty;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Freezing Start Date Should Not Be Greater Than Course End Date !!!','Error');", true);
                    }
                    else
                    {
                        //string regDate = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        //DateTime TodayDate;
                        //if (DateTime.TryParseExact(regDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                        //{
                        //}

                        //objFreezing.TodayDate = TodayDate;


                        string FreezDate = txtFreezedDate.Text;
                        DateTime FreDate;
                        if (DateTime.TryParseExact(FreezDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreDate))
                        {
                        }

                        objFreezing.FreezingDate = FreDate;


                        //Assign Course ID                            
                        objFreezing.Course_AutoID = Convert.ToInt32(row.Cells[1].Text);

                        /*CourseSD = row.Cells[5].Text.Trim();
                        DateTime CourseStartDate;
                        if (DateTime.TryParseExact(CourseSD, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                        {
                        }*/
                        objFreezing.CourseStartDate = CourseStartDate;

                        /* CourseED = row.Cells[6].Text.Trim();
                         DateTime CourseOldEndDate;
                         if (DateTime.TryParseExact(CourseED, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseOldEndDate))
                         {
                         }*/
                        objFreezing.CourseOldEndDate = CourseOldEndDate;

                        //Assign Freezing Days
                        /*TextBox txtFreezingDays = (TextBox)row.FindControl("txtExtends");*/
                        objFreezing.FreezingDays = Convert.ToInt32(txtFreezingDays.Text);

                        // Assign Frezzing Start Date
                        /*TextBox txtFreezingStartDate = (TextBox)row.FindControl("txtFreezingDate");
                        DateTime FreezingStartDate;
                        if (DateTime.TryParseExact(txtFreezingStartDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezingStartDate))
                        {
                        }*/
                        objFreezing.FreezingStartDate = FreezingStartDate;

                        // Assign Frezzing End Date
                        TextBox txtFreezingEndDate = (TextBox)row.FindControl("txtFreezingEndDate");
                        DateTime FreezingEndDate;
                        if (DateTime.TryParseExact(txtFreezingEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezingEndDate))
                        {
                        }
                        objFreezing.FreezingEndDate = FreezingEndDate;


                        // Assign Course New End Date
                        TextBox txtCourseNewEndDate = (TextBox)row.FindControl("txtNextEndDate");
                        DateTime CourseNewEndDate;
                        if (DateTime.TryParseExact(txtCourseNewEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseNewEndDate))
                        {
                        }
                        objFreezing.CourseNewEndDate = CourseNewEndDate;

                        // Assign Freezing Reason
                        TextBox txtFreezingReason = (TextBox)row.FindControl("txtFreezingReason");
                        objFreezing.FreezingReason = txtFreezingReason.Text;

                        //Assign Login Id
                        objFreezing.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);

                        // Assign Executive                           
                        objFreezing.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

                        res = objFreezing.Insert_FreezingInformation();
                    }

                }
            }

            return res;
        }
        #endregion

        #region ---------- Clear Button --------------
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion

        #region ---------- Clear Function --------------
        protected void Clear()
        {
            ViewState["DT"] = null;
            txtMemberId.Text = string.Empty;
            txtMemberId.Enabled = true;
            txtMemberId.Focus();
            txtFirstName1.Text = string.Empty;
            txtLastName1.Text = string.Empty;
            ddlGender1.SelectedIndex = 0;
            txtContact1.Text = string.Empty;
            txtContact1.Enabled = true;
            txtEmail1.Text = string.Empty;

            txtFreezedDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            gvExistingCourse.Visible = false;
            gvExistingCourse.DataSource = null;
            gvExistingCourse.DataBind();
            gvExistingCourse.Columns[0].Visible = true;

            GvFreezingAssign.Visible = false;
            GvFreezingAssign.DataSource = null;
            GvFreezingAssign.DataBind();
            GvFreezingAssign.Columns[0].Visible = true;

            btnSave.Text = "Save";
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;

            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            txtSearch.Enabled = false;

            gvMemberFreezdetails.DataSource = null;
            gvMemberFreezdetails.DataBind();
            gvMemberFreezdetails.Visible = false;


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
                    objFreezing.Action = "BindDetails";
                    objFreezing.Category = "Get_By_Date";

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

        #region ------------- Check From Date And To Date Validation ------------------
        protected int chkFromDateNotLessToDate()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtToDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter To Date !!!','Information');", true);
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
                    objFreezing.MStartDate = FromDate;
                    objFreezing.MEndDate = ToDate;
                }
                else
                {
                    flag = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
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
                    objFreezing.Action = "BindDetails";

                    if (ddlSearch.SelectedValue.ToString() == "MemberID")
                    {
                        objFreezing.Category = "MemberID";
                        objFreezing.SearchByText = txtSearch.Text;

                    }
                    else if (ddlSearch.SelectedValue.ToString() == "Contact")
                    {
                        objFreezing.Category = "Contact";
                        objFreezing.SearchByText = txtSearch.Text;
                    }
                    else if (ddlSearch.SelectedValue.ToString() == "MFName")
                    {
                        objFreezing.Category = "MemberFirstName";
                        objFreezing.SearchByText = txtSearch.Text;
                    }
                    else if (ddlSearch.SelectedValue.ToString() == "MLName")
                    {
                        objFreezing.Category = "MemberLastName";
                        objFreezing.SearchByText = txtSearch.Text;
                    }                   
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!','Information');", true);
                        ddlSearch.Focus();
                        return;
                    }

                    BindGridViewDetails();
                    btnSearchCategory.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Select Category !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion
               
        #region ------------- Search Action For BindGridView -----------------
        private void SeacrhAction()
        {
            try
            {
                objFreezing.Action = "BindDetails";

                if (ddlSearch.SelectedValue.ToString() == "MemberID")
                {
                    objFreezing.Category = "Date_MemberID";
                    objFreezing.SearchByText = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "MFName")
                {
                    objFreezing.Category = "Date_MemberFirstName";
                    objFreezing.SearchByText = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "MLName")
                {
                    objFreezing.Category = "Date_MemberLastName";
                    objFreezing.SearchByText = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Contact")
                {
                    objFreezing.Category = "Date_Contact";
                    objFreezing.SearchByText = txtSearch.Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!','Information');", true);
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
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter Data On Search Field !!!','Information');", true);
                        }

                    }
                    else
                    {
                        ddlSearch.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Categry !!!','Information');", true);
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

        #region -------------- Bind Membership Freezing Details GridView --------------------
        private void BindGridViewDetails()
        {
            //SeacrhAction();
            //AssignID();

            ViewState["FreezingDetails"] = objFreezing.GetDetails();
            dataTable = (DataTable)ViewState["FreezingDetails"];
            lblCount.Text = Convert.ToString(dataTable.Rows.Count);

            if (dataTable.Rows.Count > 0)
            {
                gvMemberFreezdetails.Visible = true;
                gvMemberFreezdetails.DataSource = dataTable;
                gvMemberFreezdetails.DataBind();
            }
            else
            {
                gvMemberFreezdetails.DataSource = dataTable;
                gvMemberFreezdetails.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
            }

            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvMemberFreezdetails.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvMemberFreezdetails.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvMemberFreezdetails.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvMemberFreezdetails.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvMemberFreezdetails.Columns[0].Visible = false;
            }
        }
        #endregion

        #region ---------- Membership Freezing Details Page Indexing --------------
        protected void gvMemberFreezdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMemberFreezdetails.PageIndex = e.NewPageIndex;

                if (ViewState["FreezingDetails"] != null)
                {
                    DataTable dataTable1 = (DataTable)ViewState["FreezingDetails"];

                    gvMemberFreezdetails.DataSource = dataTable1;
                    gvMemberFreezdetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        protected void btnFreezEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {

                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;

                AssignTodaydate();
                
                string FreezingEndDate = row.Cells[13].Text.Trim();
                DateTime FreezEndDate;
                if (DateTime.TryParseExact(FreezingEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezEndDate))
                {
                }

                if (FreezEndDate.Date >= TodayDate.Date)
                {
                    Freezing_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                    objFreezing.Freezing_AutoID = Freezing_AutoID;

                    AssignID();
                    objFreezing.Action = "EDIT";
                    dataTable = objFreezing.GetDetails();

                    if (dataTable.Rows.Count > 0)
                    {
                        ViewState["DT"] = dataTable;
                        btnSave.Text = "Update";


                        DateTime Fredate = Convert.ToDateTime(dataTable.Rows[0]["TodayDate"].ToString());
                        DateTime Frezzdate;
                        if (DateTime.TryParseExact(Fredate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Frezzdate))
                        {
                            txtFreezedDate.Text = Frezzdate.ToString("dd-MM-yyyy");
                        }
                        
                        txtMemberId.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                        txtMemberId.Enabled = false;
                        txtFirstName1.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLastName1.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender1.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtContact1.Text = dataTable.Rows[0]["Contact1"].ToString();
                        txtContact1.Enabled = false;
                        txtEmail1.Text = dataTable.Rows[0]["Email"].ToString();


                        gvExistingCourse.DataSource = dataTable;
                        gvExistingCourse.DataBind();
                        gvExistingCourse.Visible = true;
                        gvExistingCourse.Columns[0].Visible = false;

                        GvFreezingAssign.DataSource = dataTable;
                        GvFreezingAssign.DataBind();
                        GvFreezingAssign.Visible = true;
                        GvFreezingAssign.Columns[0].Visible = false;

                        TextBox txtFreezingDate = (TextBox)GvFreezingAssign.Rows[GvFreezingAssign.Rows.Count - 1].FindControl("txtFreezingDate");
                        txtFreezingDate.Focus();
                    }


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Could Not Be Edit Because Freezing End Date Less Than Today Date. !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        protected void txtFreezingDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;

                TextBox txtFSdate = (TextBox)currentRow.FindControl("txtFreezingDate");
                DateTime FSdate;
                if (DateTime.TryParseExact(txtFSdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FSdate))
                {
                }

                CourseSD = row.Cells[5].Text.Trim();
                DateTime CourseStartDate;
                if (DateTime.TryParseExact(CourseSD, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseStartDate))
                {
                }

                CourseED = row.Cells[6].Text.Trim();
                DateTime CourseEndDate;
                if (DateTime.TryParseExact(CourseED, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out CourseEndDate))
                {
                }

                if (FSdate.Date < CourseStartDate.Date)
                {
                    txtFSdate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Freezing Start Date Should Not Be Less Than Course Start  Date !!!','Error');", true);
                }
                else if (FSdate.Date > CourseEndDate.Date)
                {
                    txtFSdate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Freezing Start Date Should Not Be Greater Than Course End Date !!!','Error');", true);
                }
                else
                {
                    // Set Focus On Freezing Days
                    TextBox txtFreezingDays = (TextBox)row.FindControl("txtExtends");
                    txtFreezingDays.Text = string.Empty;
                    txtFreezingDays.Focus();
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #region -------------- Delete Gridview Rocord ----------------
        protected void btnFreezingDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                AssignID();
                AssignTodaydate();
                Freezing_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                objFreezing.Freezing_AutoID = Freezing_AutoID;

                string FreezStartDate = row.Cells[13].Text.Trim();
                DateTime FreezingStartDate;
                if (DateTime.TryParseExact(FreezStartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezingStartDate))
                {
                }
                objFreezing.FreezingStartDate = FreezingStartDate;

                string FreezEndDate = row.Cells[14].Text.Trim();
                DateTime FreezingEndDate;
                if (DateTime.TryParseExact(FreezEndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FreezingEndDate))
                {
                }
                objFreezing.FreezingEndDate = FreezingEndDate;


                if (FreezingStartDate.Date < TodayDate.Date && FreezingEndDate.Date > TodayDate.Date)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Could Not Be Deleted Because Of Freezing Duration !!!','Information');", true);
                }
                else if (FreezingStartDate.Date > TodayDate.Date)
                {
                    objFreezing.Action = "DELETE";

                    res = objFreezing.Insert_FreezingInformation();
                    
                    if (res > 0)
                    {
                        BindGridViewDetails();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    }

                }
                else if (FreezingEndDate.Date < TodayDate.Date)
                {
                    objFreezing.Action = "DELETE";

                    res = objFreezing.Insert_FreezingInformation();

                    if (res > 0)
                    {
                        BindGridViewDetails();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
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

        protected void gvExistingCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExistingCourse.PageIndex = e.NewPageIndex;
            BindMemberCourse();
        }

        #region ---------------- Refresh Button --------------
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = "";
                Assign_MonthDate();
                SearchByDateFunction();                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
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

                if (ViewState["FreezingDetails"] != null)
                {
                    dt = (DataTable)ViewState["FreezingDetails"];
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=FreezingDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvMemberFreezdetails.Columns[0].Visible = false;
                            gvMemberFreezdetails.Columns[1].Visible = false;
                            gvMemberFreezdetails.AllowPaging = false;

                            gvMemberFreezdetails.DataSource = dt;
                            gvMemberFreezdetails.DataBind();
                            gvMemberFreezdetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMemberFreezdetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMemberFreezdetails.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvMemberFreezdetails.Rows)
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


                            gvMemberFreezdetails.GridLines = GridLines.Both;
                            gvMemberFreezdetails.RenderControl(hw);

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
       
        


    }
}