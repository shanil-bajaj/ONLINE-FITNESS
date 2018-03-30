using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;
using BusinessAccessLayer;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class AssignDietToMember : System.Web.UI.Page
    {
        BalDietPlan plan = new BalDietPlan();
        DataTable table = new DataTable();
        DataTable dt = new DataTable();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        static int Member_AutoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                bindDDLExecutive();
                setExecutive();
                AssignTodaysDate();
                //txttodte.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                //txtfrmdte.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                Avoidation();

                if (Request.QueryString["AssignDietDetails"] != null)
                {
                    AssignMonthDate();
                    divFormDetails.Visible = false;
                    divSearchField.Visible = true;
                    plan.Action = "Search";
                    GridBind();
                    formheadDetails.Visible = true;
                    formhead.Visible = false;
                    txtFromDate1.Focus();
                }
            }

            
        }

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
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

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

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
            ddlExecutive.Focus();
        }

        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txttodte.Text = todaydate.ToString("dd-MM-yyyy");
                txtfrmdte.Text = todaydate.ToString("dd-MM-yyyy");
                txtDietDate.Text = todaydate.ToString("dd-MM-yyyy");
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                //txtEnqTime.Text = localTime.ToString("HH:mm");
            }
        }
        protected void AssignMonthDate()
        {
            Labeldt.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Labeldt.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);


            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDate1.Text = dtFirst.ToString("dd-MM-yyyy");
                txtToDate1.Text = dtLast.ToString("dd-MM-yyyy");
            }

        }
        public void Avoidation()
        {
            try
            {
                plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                plan.Action = "Avoidation";
                table = plan.Select_MemberID();
                if (table.Rows.Count > 0)
                {
                    txtavoid.Text = table.Rows[0]["Name"].ToString();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetMember()
        {
            plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            table = plan.Select_Member();
            if (table.Rows.Count > 0)
            {
                TxtID.Attributes.Add("autocomplete", "on");
            }
        }

        public void GridBind()
        {
            try
            {
                plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                DateTime fromdate;
                DateTime todate;
                if (DateTime.TryParseExact(txtFromDate1.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromdate))
                {
                    plan.FromDate = fromdate;
                }
                if (DateTime.TryParseExact(txtToDate1.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todate))
                {
                    plan.ToDate = todate;
                }
                if (fromdate > todate)
                {                    
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should not be greater than To Date !!!','Information');", true);
                    return;
                }

                table = plan.SearchCategory();
                if (table.Rows.Count > 0)
                {
                    gvDiet.Visible = true;
                    gvDiet.DataSource = table;
                    ViewState["DietDetails"] = table;
                    gvDiet.DataBind();
                }
                else
                {
                    gvDiet.Visible = false;
                    gvDiet.DataSource = null;
                    gvDiet.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!','Information');", true);
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvDiet.Columns[0].Visible = true;
                    gvDiet.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvDiet.Columns[0].Visible = true;
                    gvDiet.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvDiet.Columns[0].Visible = true;
                    gvDiet.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvDiet.Columns[0].Visible = true;
                    gvDiet.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvDiet.Columns[0].Visible = false;
                    gvDiet.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
 
        protected void TxtID_TextChanged(object sender, EventArgs e)
        {
            try
            {

                plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                //plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                plan.Member_ID = Convert.ToInt32(TxtID.Text);
                plan.Action = "SelectBy_ID";
                table = plan.Select_MemberID();
                if (table.Rows.Count > 0)
                {
                    txtFirst.Text = table.Rows[0]["FName"].ToString();
                    txtLast.Text = table.Rows[0]["LName"].ToString();
                    ddlGender.SelectedItem.Value= table.Rows[0]["Gender"].ToString();
                    ddlGender.SelectedItem.Text = table.Rows[0]["Gender"].ToString();
                    txtContact.Text = table.Rows[0]["Contact1"].ToString();
                    txtmail.Text = table.Rows[0]["Email"].ToString();
                    txtPreHistory.Focus();

                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!','Error');", true);
                   chkExecutive.Checked = true;
                   ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                   ddlExecutive.Enabled = false;
                   TxtID.Text = "";
                   txtFirst.Text = "";
                   txtLast.Text = "";
                   txtmail.Text = "";
                   txtContact.Text = "";
                   ddlGender.SelectedItem.Text = "--Select--";
                   return;
                }
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error(ex,'Error');", true);
            }

        }

        public void clearRecord()
        {
            try
            {
                chkExecutive.Checked = true;
                ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutive.Enabled = false; 
                TxtID.Text = "";
                txtFirst.Text = "";
                txtLast.Text = "";
                txtmail.Text = "";
                txtContact.Text = "";
                ddlGender.SelectedItem.Text = "--Select--";
                txtDietitian.Text = "";
                txtPreHistory.Text = "";
                Avoidation();
                //txttodte.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                //txtfrmdte.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                AssignTodaysDate();
                txtSearch.Text = "";
                ddlSearch.SelectedValue = "--Select--";

                btnSave.Text = "Save";

                txtmeal1.Text = "";
                txtmeal2.Text = "";
                txtmeal3.Text = "";
                txtmeal4.Text = "";
                txtmeal5.Text = "";
                txtmeal6.Text = "";
                txttime1.Text = "";
                txttime2.Text = "";
                txttime3.Text = "";
                txttime4.Text = "";
                txttime5.Text = "";
                txttime6.Text = "";

                DropDownList2.SelectedIndex=0;
                DropDownList3.SelectedIndex=0;
                DropDownList4.SelectedIndex=0;
                DropDownList5.SelectedIndex=0;
                DropDownList6.SelectedIndex=0;
                DropDownList7.SelectedIndex=0;

                gvDiet.Visible = false;
            }
            catch(Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Something Exception Occured !!!','Error');", true);
            }
        }

        public void AddParameter()
        {
            Nullable<DateTime> dt = null;
            try
            {
                plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                plan.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                plan.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
                plan.Member_ID = Convert.ToInt32(TxtID.Text);

                plan.Action = "SelectBy_ID";
                table = plan.Select_MemberID();
                if (table.Rows.Count > 0)
                    plan.Member_AutoID = Convert.ToInt32(table.Rows[0]["Member_AutoID"]);
                plan.Diet_Date = DateTime.Now;

                plan.Meal1 = txtmeal1.Text;
               
                if (txttime1.Text != "")
                {
                    plan.Time1 = Convert.ToDateTime(txttime1.Text);
                }
                else
                {
                    plan.Time1 = dt;
                }
                if (DropDownList2.Text != "--Select--")
                {
                    plan.Meal1Sta = DropDownList2.Text;
                }    
                  
                plan.Meal2 = txtmeal2.Text;
                if (txttime2.Text != "")
                {
                    plan.Time2 = Convert.ToDateTime(txttime2.Text);
                }
                else
                {
                    plan.Time2 = dt;
                }
                if (DropDownList3.Text != "--Select--")
                {
                    plan.Meal2Sta = DropDownList3.Text;
                }   

                plan.Meal3 = txtmeal3.Text;
                if (txttime3.Text != "")
                {
                    plan.Time3 = Convert.ToDateTime(txttime3.Text);
                }
                else
                {
                    plan.Time3 = dt;
                }
                if (DropDownList4.Text != "--Select--")
                {
                    plan.Meal3Sta = DropDownList4.Text;
                }   
                  

                plan.Meal4 = txtmeal4.Text;
                if (txttime4.Text != "")
                {
                    plan.Time4 = Convert.ToDateTime(txttime4.Text);
                }
                else
                {
                    plan.Time4 = dt;
                }
                if (DropDownList5.Text != "--Select--")
                {
                    plan.Meal4Sta = DropDownList5.Text;
                }    
                  
                plan.Meal5 = txtmeal5.Text;
                if (txttime5.Text != "")
                {
                    plan.Time5 = Convert.ToDateTime(txttime5.Text);
                }
                else
                {
                    plan.Time5 = dt;
                }
                if (DropDownList6.Text != "--Select--")
                {
                    plan.Meal5Sta = DropDownList6.Text;
                }

                plan.Meal6 = txtmeal6.Text;
                if (txttime6.Text != "")
                {
                    plan.Time6 = Convert.ToDateTime(txttime6.Text);
                }
                else
                {
                    plan.Time6 = dt;
                }
                if (DropDownList7.Text != "--Select--")
                {
                    plan.Meal6Sta = DropDownList7.Text;
                }

                    DateTime TOdte;
                    if (DateTime.TryParseExact(txttodte.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TOdte))
                    {
                        string Regdate1 = TOdte.ToString("dd-MM-yyyy");
                        plan.ToDate = DateTime.ParseExact(Regdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }

                    DateTime FROdte;
                    if (DateTime.TryParseExact(txtfrmdte.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FROdte))
                    {
                        string Regdate1 = FROdte.ToString("dd-MM-yyyy");
                        plan.FromDate = DateTime.ParseExact(Regdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }

 
                plan.Avoid=txtavoid.Text;
                plan.Dietatian = txtDietitian.Text;
                plan.PreHistory = txtPreHistory.Text;

               
        
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                AddParameter();
                plan.Action = "InsertDiet";
                int res = plan.Insert_DietPlan();
                if (res > 0)
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                    clearRecord();

                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Saved !!!','Error');", true);
                }
            }
            else
            {
                AddParameter();
                plan.Action = "Update";
                plan.Diet_ID = Convert.ToInt16(ViewState["Diet_AutoID"]);
                int res = plan.Insert_DietPlan();
                if (res > 0)
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                    clearRecord();

                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Saved !!!','Error');", true);
                }

            }
        }
        public void Get_MemberAutoID_ByMemberID1()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMember.Member_ID1 = Convert.ToInt32(txtSearch.Text);
            dt = objMember.Get_MemberAutoID_ByMemberID1();
            if (dt.Rows.Count > 0)
            {
                Member_AutoID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"]);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available!!!','Information');", true);
                return;
            }
        }
        public void SearchByCategory()
        {
            if (ddlSearch.Text != "--Select--" && ddlSearch.Text != "")
            {
                if (ddlSearch.Text == "MemberID")
                {
                    Get_MemberAutoID_ByMemberID1();
                    plan.SearchText = Member_AutoID.ToString();
                    plan.Category = "MemberID";
                }
                if (ddlSearch.Text == "Name")
                {
                    plan.SearchText = txtSearch.Text;
                    plan.Category = "Name";
                }
                if (ddlSearch.Text == "Contact")
                {
                    plan.SearchText = txtSearch.Text;
                    plan.Category = "Contact";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            plan.Action = "Search";
            GridBind();
        }

        protected void btnDateWithCategory_Click(object sender, EventArgs e)
        {

            if (ddlSearch.SelectedValue == "--Select--" && txtSearch.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                ddlSearch.Focus();
                return;
            }
            else if (ddlSearch.SelectedValue != "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                txtSearch.Focus();
                return;
            }
            else
            {
                plan.Action = "SearchByDateWithCategory";
                SearchByCategory();
                GridBind();
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                if (id > 0)
                {
                    plan.Action = "Delete";
                    plan.Diet_ID = id;
                    int res = plan.Delete_Diet();
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully!!!','Success');", true);
                    GridBind();
                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Deleted!!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Something Error Occured While Deleting !!!.','Error');", true);
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ViewState["Diet_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                btnSave.Text = "Update";
                plan.Action = "Select_By_ID";
                plan.Diet_ID = Convert.ToInt32(e.CommandArgument.ToString());
                table = plan.SelectByID_Diet_Information();
                if (table.Rows.Count > 0)
                {
                    txttime1.Text = table.Rows[0]["Time1"].ToString();
                    txttime2.Text = table.Rows[0]["Time2"].ToString();
                    txttime3.Text = table.Rows[0]["Time3"].ToString();
                    txttime4.Text = table.Rows[0]["Time4"].ToString();
                    txttime5.Text = table.Rows[0]["Time5"].ToString();
                    txttime6.Text = table.Rows[0]["Time6"].ToString();

                    txtmeal1.Text = table.Rows[0]["Meal1"].ToString();
                    txtmeal2.Text = table.Rows[0]["Meal2"].ToString();
                    txtmeal3.Text = table.Rows[0]["Meal3"].ToString();
                    txtmeal4.Text = table.Rows[0]["Meal4"].ToString();
                    txtmeal5.Text = table.Rows[0]["Meal5"].ToString();
                    txtmeal6.Text = table.Rows[0]["Meal6"].ToString();

                    DropDownList2.SelectedValue = table.Rows[0]["Meal1Sta"].ToString();
                    DropDownList3.SelectedValue = table.Rows[0]["Meal2Sta"].ToString();
                    DropDownList4.SelectedValue = table.Rows[0]["Meal3Sta"].ToString();
                    DropDownList5.SelectedValue = table.Rows[0]["Meal4Sta"].ToString();
                    DropDownList6.SelectedValue = table.Rows[0]["Meal5Sta"].ToString();
                    DropDownList7.SelectedValue = table.Rows[0]["Meal6Sta"].ToString();

                    if (table.Rows[0]["ToDate"].ToString() != "")
                    {
                        DateTime Todte = Convert.ToDateTime(table.Rows[0]["ToDate"].ToString());
                        DateTime tde;
                        if (DateTime.TryParseExact(Todte.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tde))
                        {
                            txttodte.Text = tde.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txttodte.Text = "";


                    if (table.Rows[0]["FromDate"].ToString() != "")
                    {
                        DateTime Frodte = Convert.ToDateTime(table.Rows[0]["FromDate"].ToString());
                        DateTime fdt;
                        if (DateTime.TryParseExact(Frodte.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fdt))
                        {
                            txtfrmdte.Text = fdt.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtfrmdte.Text = "";


                    ddlExecutive.SelectedValue = table.Rows[0]["Executive_ID"].ToString();
                    txtPreHistory.Text = table.Rows[0]["PreHistory"].ToString();
                    txtavoid.Text = table.Rows[0]["Avoid"].ToString();
                    txtDietitian.Text = table.Rows[0]["Dietatian"].ToString();

                    plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    TxtID.Text = table.Rows[0]["Member_AutoID"].ToString();
                    plan.Member_ID = Convert.ToInt32(TxtID.Text);
                    
                    plan.Action = "Select By ID";
                    table = plan.Select_MemberID();
                    if (table.Rows.Count > 0)
                    {
                        txtFirst.Text = table.Rows[0]["FName"].ToString();
                        txtLast.Text = table.Rows[0]["LName"].ToString();
                        ddlGender.SelectedItem.Value = table.Rows[0]["Gender"].ToString();
                        ddlGender.SelectedItem.Text = table.Rows[0]["Gender"].ToString();
                        txtContact.Text = table.Rows[0]["Contact1"].ToString();
                        txtmail.Text = table.Rows[0]["Email"].ToString();
                        
                    }
                    else
                    {
                       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!','Error');", true);
                    }
                    

                }
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Something Error Occured While Update !!!.','Error');", true);

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearRecord();
           
        }

        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();
        //public void Get_MemberAutoID_ByMemberID1()
        //{
        //    objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //    objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //    objMember.Member_ID1 = Convert.ToInt32(txtSearch.Text);
        //    dt = objMember.Get_MemberAutoID_ByMemberID1();
        //    if (dt.Rows.Count > 0)
        //    {
        //        Member_AutoID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"]);
        //    }
        //}
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            plan.Action = "SearchByCategory";
            SearchByCategory();
            GridBind();
        }

        protected void txtContact_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                plan.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                plan.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                plan.Contact1 = txtContact.Text;
                plan.Action = "Select By Contact";
                table = plan.Select_MemberContact();
                if (table.Rows.Count > 0)
                {
                    txtFirst.Text = table.Rows[0]["FName"].ToString();
                    txtLast.Text = table.Rows[0]["LName"].ToString();
                    TxtID.Text = table.Rows[0]["Member_ID1"].ToString();
                    txtmail.Text=table.Rows[0]["Email"].ToString();
                    ddlGender.SelectedItem.Value = table.Rows[0]["Gender"].ToString();
                    ddlGender.SelectedItem.Text = table.Rows[0]["Gender"].ToString();
                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!','Error');", true);
                }
                txtPreHistory.Focus();
            }
            catch (Exception ex)
            {       
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error(ex,'Error');", true);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToExcel()
        {
            try
            {
                if (ViewState["DietDetails"] != null)
                {
                    dt = (DataTable)ViewState["DietDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=DietDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvDiet.Columns[0].Visible = false;
                            gvDiet.Columns[1].Visible = false;
                            gvDiet.AllowPaging = false;
                            gvDiet.DataSource = dt;
                            gvDiet.DataBind();
                            gvDiet.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvDiet.HeaderRow.Cells)
                            {
                                cell.BackColor = gvDiet.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvDiet.Rows)
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
                                }
                            }
                            gvDiet.GridLines = GridLines.Both;
                            gvDiet.RenderControl(hw);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Data GridView is Empty,Can Not Export !!!.','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Data GridView is Empty,Can Not Export !!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            AssignMonthDate();
            plan.Action = "Search";
            GridBind();
            ddlSearch.SelectedValue = "--Select--";
            txtSearch.Text = "--Select--";
        }
    }
}