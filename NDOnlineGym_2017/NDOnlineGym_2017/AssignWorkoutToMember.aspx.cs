using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class AssignWorkoutToMember : System.Web.UI.Page
    {
        BalAssignWorkoutToMember objWorkout = new BalAssignWorkoutToMember();
        DataTable dataTable = new DataTable();
        int res;
        static int MemberAutoID;
        int flag = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["WorkoutDetails"] != null)
                    {
                        divWorkout.Visible = false;
                        divWorkoutDetails.Visible = true;
                        divsearch.Visible = true;
                        divFormDetails.Visible = false;
                        Assign_MonthDate();
                        txtFromDate.Focus();
                        SearchByDateFunction(); 
                    }
                    else
                    {
                        txtMemberID.Focus();
                        txtSearchDetails.Enabled = false;
                        txtAssignDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        txtfrmdte.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        txttodte.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        BindProgrammerDDL();
                        BindWorkoutDetailsGridView();
                        
                    }                                      
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

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

        #region --------- Bind Programer List ------------
        private void BindProgrammerDDL()
        {
            try
            {
                AssignID();
                objWorkout.Action = "Select_Programmer";
                dataTable = objWorkout.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    ddlProgrammer.DataSource = dataTable;
                    ddlProgrammer.Items.Clear();
                    ddlProgrammer.DataValueField = "Programmer_AutoID";
                    ddlProgrammer.DataTextField = "PName";
                    ddlProgrammer.DataBind();
                    ddlProgrammer.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion

        #region ---------- Bind Workout Group Details ------------
        private void BindWorkoutDetailsGridView()
        {
            try
            {
                AssignID();
                if (ddlSearch.SelectedValue == "All")
                {
                    //txtSearch.Enabled = false;
                    objWorkout.Action = "Select_WorkoutDetailsAll";                    
                }
                else if (ddlSearch.SelectedValue == "MuscularGroup")
                {
                    objWorkout.Action = "SearchByMuscularGroup";
                }
                else if (ddlSearch.SelectedValue == "WorkoutName")
                {
                    objWorkout.Action = "SearchByWorkoutName";
                }       

                dataTable = objWorkout.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    gvWorkoutDetails.DataSource = dataTable;
                    gvWorkoutDetails.DataBind();
                }                                     
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }       
           
        }
        #endregion

        #region --------- Assign Comp and Branch ID-------------------------
        private void AssignID()
        {
            objWorkout.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objWorkout.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region ----------- Assign Date -------------
        private void AssignDate()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

            txtAssignDate.Text = localTime.ToString("dd-MM-yyyy");
            txtfrmdte.Text = localTime.Date.ToString("dd-MM-yyyy");
            txttodte.Text = localTime.ToString("dd-MM-yyyy");
        }
        #endregion


        //protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtSearch.Text = string.Empty;
        //    gvWorkoutDetails.PageIndex = 0;
        //    if (ddlSearch.SelectedValue == "All")
        //    {                
        //        //txtSearch.Enabled = false;
        //        //objWorkout.Action = "Select_WorkoutDetailsAll";                    
        //        BindWorkoutDetailsGridView();
        //    }
        //    else if (ddlSearch.SelectedValue == "MuscularGroup" || ddlSearch.SelectedValue == "WorkoutName")
        //    {
        //        txtSearch.Enabled = true;
        //    }
        //    ddlSearch.Focus();
        //}

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gvWorkoutDetails.PageIndex = 0;               
                if (txtSearch.Text != string.Empty)
                {
                    objWorkout.SearchByText = Regex.Replace(txtSearch.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");//txtSearch.Text;
                    
                    //if (ddlSearch.SelectedValue == "MuscularGroup")
                    //{
                    //    objWorkout.Action = "SearchByMuscularGroup";    
                    //}
                    //else if (ddlSearch.SelectedValue == "WorkoutName")
                    //{
                    //    objWorkout.Action = "SearchByWorkoutName";                          
                    //}                    
                    
                    BindWorkoutDetailsGridView();
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }       
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearField();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }   
        }

        private void ClearFieldMemberIdNotFound()
        {
            //txtMemberID.Text = string.Empty;
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtContact.Text = string.Empty;
            txtmail.Text = string.Empty;
        }
        private void ClearFieldMemberContNotFound()
        {
            txtMemberID.Text = string.Empty;
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            //txtContact.Text = string.Empty;
            txtmail.Text = string.Empty;
        }

        private void ClearField()
        {
            MemberAutoID = 0;
            txtMemberID.Text = string.Empty;
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtContact.Text = string.Empty;
            txtmail.Text = string.Empty;
            ddlProgrammer.SelectedIndex = 0;
            AssignDate();
            ddlWorkoutDay.SelectedIndex=0;            
            txtSearch.Text = string.Empty;            
            ViewState["DT"] = null;

            GvWorkoutAssign.DataSource = null;
            GvWorkoutAssign.DataBind();

            gvWorkoutRecord.DataSource = null;
            gvWorkoutRecord.DataBind();

            ddlSearch.SelectedIndex = 0;
            BindWorkoutDetailsGridView();
        }       

        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != string.Empty)
                {
                    AssignID();
                    objWorkout.Member_ID = Convert.ToInt32(txtMemberID.Text.Trim());
                    objWorkout.Action = "SearchByMemberID";

                    dataTable = objWorkout.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberIdNotFound();                        
                       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
                    }
                    txtMemberID.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }   

        }
       
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != string.Empty)
                {
                    AssignID();
                    objWorkout.Contact = txtContact.Text;
                    objWorkout.Action = "SearchByContact";

                    dataTable = objWorkout.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberContNotFound();                        
                       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
                    }
                    txtContact.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }   
        }

        public int k = 0;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {                
                int WorkoutName_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                var btnPre = (Control)sender;
                GridViewRow row1 = (GridViewRow)btnPre.NamingContainer;

                DataRow dr = null;
                //dt.Clear();
                dataTable.Columns.Add(new DataColumn("ID"));
                dataTable.Columns.Add(new DataColumn("WorkoutName_AutoID"));
                dataTable.Columns.Add(new DataColumn("MGroup"));
                dataTable.Columns.Add(new DataColumn("Workout"));
                dataTable.Columns.Add(new DataColumn("Sets"));
                dataTable.Columns.Add(new DataColumn("Resp"));

                if (ViewState["DT"] != null)
                {
                    dataTable = (DataTable)ViewState["DT"];                   
                }                

                bool exists = dataTable.Select().ToList().Exists(row => row["WorkoutName_AutoID"].ToString().ToUpper() == e.CommandArgument.ToString());

                if (exists == false)
                {
                    k = dataTable.Rows.Count;                    
                    dr = dataTable.NewRow();
                    dr["ID"] = k;
                    dr["WorkoutName_AutoID"] = row1.Cells[1].Text;
                    dr["MGroup"] = row1.Cells[2].Text;
                    dr["Workout"] = row1.Cells[3].Text;
                    dr["Sets"] = string.Empty;
                    dr["Resp"] = string.Empty;
                    // dataTable.Rows.Add(dr);

                    dataTable.Rows.InsertAt(dr, k);
                    k++;

                    ViewState["DT"] = dataTable;
                    
                    GvWorkoutAssign.DataSource = dataTable;
                    GvWorkoutAssign.DataBind();


                    TextBox txtSets = (TextBox)GvWorkoutAssign.Rows[GvWorkoutAssign.Rows.Count - 1].FindControl("txtSets");
                    txtSets.Focus();
                    
                }                               
                
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }   

        }

        protected void GvWorkoutAssign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);

                dataTable = (DataTable)ViewState["DT"];

                dataTable.Rows[index].Delete();

                ViewState["DT"] = dataTable;

                GvWorkoutAssign.DataSource = dataTable;
                GvWorkoutAssign.DataBind();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }   
        }

        protected void txtSets_TextChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;//.Parent.Parent;

                dataTable = (DataTable)ViewState["DT"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr;
                dr = dataTable.NewRow();

                int s = currentRow.RowIndex;

                dr["ID"] = s;
                dr["WorkoutName_AutoID"] = row.Cells[1].Text;
                dr["MGroup"] = row.Cells[2].Text;
                dr["Workout"] = row.Cells[3].Text;


                TextBox txtSets = (TextBox)currentRow.FindControl("txtSets");
                dr["Sets"] = txtSets.Text;

                TextBox txtResp = (TextBox)currentRow.FindControl("txtResp");
                dr["Resp"] = txtResp.Text;

                dataTable.Rows[s].Delete();
                dataTable.Rows.InsertAt(dr, s);
                ViewState["DT"] = dataTable;
                GvWorkoutAssign.DataSource = dataTable;
                GvWorkoutAssign.DataBind();

                txtResp.Focus();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }   
        }

        protected void txtResp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;//.Parent.Parent;

                dataTable = (DataTable)ViewState["DT"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr;
                dr = dataTable.NewRow();

                int s = currentRow.RowIndex;

                dr["ID"] = s;
                dr["WorkoutName_AutoID"] = row.Cells[1].Text;
                dr["MGroup"] = row.Cells[2].Text;
                dr["Workout"] = row.Cells[3].Text;


                TextBox txtSets = (TextBox)currentRow.FindControl("txtSets");
                dr["Sets"] = txtSets.Text;

                TextBox txtResp = (TextBox)currentRow.FindControl("txtResp");
                dr["Resp"] = txtResp.Text;

                dataTable.Rows[s].Delete();
                dataTable.Rows.InsertAt(dr, s);
                ViewState["DT"] = dataTable;
                GvWorkoutAssign.DataSource = dataTable;
                GvWorkoutAssign.DataBind();

                TextBox txtResp1 = (TextBox)GvWorkoutAssign.Rows[GvWorkoutAssign.Rows.Count - 1].FindControl("txtResp");
                txtResp1.Focus();               
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }   
        }

        //public int res = 0;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text == string.Empty || txtContact.Text == string.Empty || ddlProgrammer.SelectedValue == "--Select--" || ddlWorkoutDay.SelectedValue == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter All Fields !!!','Error');", true);
                    if (txtMemberID.Text == string.Empty)
                    {
                        txtMemberID.Style.Add("border", "1px solid red ");
                    }
                    else
                    {
                        txtMemberID.Style.Add("border", "1px solid silver ");
                    }

                    if (txtContact.Text == string.Empty)
                    {
                        txtContact.Style.Add("border", "1px solid red ");
                    }
                    else
                    {
                        txtContact.Style.Add("border", "1px solid silver ");
                    }

                    if (ddlProgrammer.SelectedValue == "--Select--")
                    {
                        ddlProgrammer.Style.Add("border", "1px solid red ");
                    }
                    else
                    {
                        ddlProgrammer.Style.Add("border", "1px solid silver ");
                    }

                    if (ddlWorkoutDay.SelectedValue == "--Select--")
                    {
                        ddlWorkoutDay.Style.Add("border", "1px solid red ");
                    }
                    else
                    {
                        ddlWorkoutDay.Style.Add("border", "1px solid silver ");
                    }
                }
                else
                {
                    if (btnSave.Text == "Save")
                    {

                        objWorkout.Action = "INSERT";
                        AssignID();
                        objWorkout.Member_AutoID = MemberAutoID;//Convert.ToInt32(txtMemberID.Text);
                        objWorkout.Programmer_AutoID = Convert.ToInt32(ddlProgrammer.SelectedValue);


                        DateTime AssignDate;
                        if (DateTime.TryParseExact(txtAssignDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out AssignDate))
                        {
                        }
                        objWorkout.AssignDate = AssignDate;

                        DateTime FromDate;
                        if (DateTime.TryParseExact(txtfrmdte.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FromDate))
                        {
                        }
                        objWorkout.FromDate = FromDate;

                        DateTime ToDate;
                        if (DateTime.TryParseExact(txttodte.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ToDate))
                        {
                        }
                        objWorkout.ToDate = ToDate;

                        objWorkout.WorkDay = ddlWorkoutDay.SelectedValue;

                        foreach (GridViewRow row in GvWorkoutAssign.Rows)
                        {
                            objWorkout.WorkoutName_AutoID = Convert.ToInt32(row.Cells[1].Text);

                            TextBox txtsdate = (TextBox)row.FindControl("txtSets");
                            objWorkout.Sets = txtsdate.Text;

                            TextBox txtedate = (TextBox)row.FindControl("txtResp");
                            objWorkout.Reps = txtedate.Text;

                            res = objWorkout.Insert_WorkoutInformation();
                        }

                        if (res > 0)
                        {
                            ClearField();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        }
                        else if (res < 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                            return;
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

        #region ---------- Search Button by Date ----------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchByDateFunction();               
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
                    objWorkout.Action = "BindDetails";
                    objWorkout.Category = "Get_By_Date";
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

        #region ------------- Check From Date And To Date Validation
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
                    objWorkout.FromDate = FromDate;
                    objWorkout.ToDate = ToDate;
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

        #region ----------- Bind Workout Details Gridview -------------
        protected void BindGridViewDetails()
        {
            //SeacrhAction();
            //AssignID();

            dataTable = objWorkout.GetDetails(); ;
            ViewState["Workout"] = dataTable;

            lblCount.Text = Convert.ToString(dataTable.Rows.Count);
            //dataTable = objWorkout.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                gvWorkoutDetails.Visible = true;

                gvWorkoutRecord.DataSource = dataTable;
                gvWorkoutRecord.DataBind();
                //gvWorkoutRecord.Focus();
            }
            else
            {
                gvWorkoutRecord.DataSource = dataTable;
                gvWorkoutRecord.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!','Information');", true);
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvWorkoutRecord.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvWorkoutRecord.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvWorkoutRecord.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvWorkoutRecord.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvWorkoutRecord.Columns[0].Visible = false;
            }
        }
        #endregion

        #region ------------- DDL Category Index Changed ---------
        //protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    txtSearchDetails.Text = string.Empty;
        //    if (ddlSearchBy.SelectedValue.ToString() == "--Select--")
        //    {
        //        txtSearchDetails.Style.Add("border", "1px solid silver ");
        //        txtSearchDetails.Enabled = false;

        //    }
        //    else
        //    {
        //        txtSearchDetails.Enabled = true;
        //    }
        //    ddlSearchBy.Focus();
        //}
        #endregion

        #region ------------ Search On Text Change ----------------
        protected void txtSearchDetails_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchDetails.Text != string.Empty)
                {
                    AssignID();
                    objWorkout.Action = "BindDetails";

                    if (ddlSearchBy.SelectedValue.ToString() == "MemberID")
                    {
                        objWorkout.Category = "MemberID";
                        objWorkout.SearchByText = txtSearchDetails.Text;

                    }
                    else if (ddlSearchBy.SelectedValue.ToString() == "MemberName")
                    {
                        objWorkout.Category = "MemberName";
                        objWorkout.SearchByText = txtSearchDetails.Text;
                    }
                    else if (ddlSearchBy.SelectedValue.ToString() == "Daywise")
                    {
                        objWorkout.Category = "Daywise";
                        objWorkout.SearchByText = txtSearchDetails.Text;
                    }
                    else if (ddlSearchBy.SelectedValue.ToString() == "Contact")
                    {
                        objWorkout.Category = "Contact";
                        objWorkout.SearchByText = txtSearchDetails.Text;
                    }
                    else if (ddlSearchBy.SelectedValue.ToString() == "MuscularGroupType")
                    {
                        objWorkout.Category = "MuscularGroupType";
                        objWorkout.SearchByText = txtSearchDetails.Text;
                    }
                    else if (ddlSearchBy.SelectedValue.ToString() == "WorkoutType")
                    {
                        objWorkout.Category = "WorkoutType";
                        objWorkout.SearchByText = txtSearchDetails.Text;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                        ddlSearchBy.Focus();
                        return;
                    }

                    BindGridViewDetails();
                    txtSearchDetails.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!','Information');", true);
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
        protected void btnSearchDatewithCategory_Click(object sender, EventArgs e)
        {
            try
            {
                flag = chkFromDateNotLessToDate();

                if (flag == 0)
                {
                    if (ddlSearchBy.SelectedValue.ToString() != "--Select--")
                    {
                        if (txtSearchDetails.Text != string.Empty)
                        {
                            AssignID();
                            SeacrhAction();
                            BindGridViewDetails();
                        }
                        else
                        {
                            txtSearchDetails.Focus();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter Data On Search Field !!!','Information');", true);
                        }

                    }
                    else
                    {
                        ddlSearchBy.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!','Information');", true);
                    }

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
                objWorkout.Action = "BindDetails";
                
                if (ddlSearchBy.SelectedValue.ToString() == "MemberID")
                {
                    objWorkout.Category = "Date_MemberID";
                    objWorkout.SearchByText = txtSearchDetails.Text;

                }
                else if (ddlSearchBy.SelectedValue.ToString() == "MemberName")
                {
                    objWorkout.Category = "Date_MemberName";
                    objWorkout.SearchByText = txtSearchDetails.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Daywise")
                {
                    objWorkout.Category = "Date_Daywise";
                    objWorkout.SearchByText = txtSearchDetails.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "Contact")
                {
                    objWorkout.Category = "Date_Contact";
                    objWorkout.SearchByText = txtSearchDetails.Text;
                }
                else if (ddlSearchBy.SelectedValue.ToString() == "MuscularGroupType")
                {
                    objWorkout.Category = "Date_MuscularGroupType";
                    objWorkout.SearchByText = txtSearchDetails.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "WorkoutType")
                {
                    objWorkout.Category = "Date_WorkoutType";
                    objWorkout.SearchByText = txtSearchDetails.Text;
                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    ddlSearchBy.Focus();
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


        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();
                objWorkout.Workout_AutoID = Convert.ToInt32(e.CommandArgument);
                objWorkout.Action = "DeleteByWorkoutAutoID";
                int i = objWorkout.Insert_WorkoutInformation();
                if (i > 0)
                {
                    BindGridViewDetails();
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Cannot Delete, Already Assigned !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Cannot Delete, Already Assigned !!!','Information');", true);
            }
        }

        protected void gvWorkoutDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorkoutDetails.PageIndex = e.NewPageIndex;
            BindWorkoutDetailsGridView();
        }

        protected void gvWorkoutRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorkoutRecord.PageIndex = e.NewPageIndex;
            //BindGridViewDetails();

            dataTable =(DataTable) ViewState["Workout"];

            //dataTable = objWorkout.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                gvWorkoutDetails.Visible = true;
                gvWorkoutRecord.DataSource = dataTable;
                gvWorkoutRecord.DataBind();
            }

        }

        #region ----------- Expot To Excle Record ------------------
        protected void btnExpord_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        protected void ExportToExcel()
        {
            try
            {
                if (ViewState["Workout"] != null)
                {

                    dataTable = (DataTable)ViewState["Workout"]; ;
                    if (dataTable.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=WorkoutDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvWorkoutRecord.Columns[0].Visible = false;                           
                            gvWorkoutRecord.AllowPaging = false;

                            gvWorkoutRecord.DataSource = dataTable;
                            gvWorkoutRecord.DataBind();
                            gvWorkoutRecord.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvWorkoutRecord.HeaderRow.Cells)
                            {
                                cell.BackColor = gvWorkoutRecord.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvWorkoutRecord.Rows)
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


                            gvWorkoutRecord.GridLines = GridLines.Both;
                            gvWorkoutRecord.RenderControl(hw);

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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Can Not Export Because Records Are Not Available !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Can Not Export Because Records Are Not Available !!!.','Information');", true);
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
            /* Verifies that the control is rendered */
        }

        #endregion

        #region ------------ Clear WorkOut Grid Details ---------------
        protected void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSearchBy.SelectedIndex = 0;
                txtSearchDetails.Text = "";
                Assign_MonthDate();
                SearchByDateFunction(); 
                //dataTable = null;
                //ViewState["Workout"] = dataTable;

                //gvWorkoutDetails.Visible = false;
                //gvWorkoutRecord.DataSource = dataTable;
                //gvWorkoutRecord.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion


        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    LinkButton lnkbtn = sender as LinkButton;
            
        //    //getting particular row linkbutton
        //    GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            
        //    //getting userid of particular row
        //    int index = Convert.ToInt32(GvWorkoutAssign.DataKeys[gvrow.RowIndex].Value.ToString());

        //    dataTable = (DataTable)ViewState["DT"];

        //    dataTable.Rows[index].Delete();
        //}

        //protected void btnDelete_Command(object sender, CommandEventArgs e)
        //{
        //    try
        //    {
        //        int k1 = Convert.ToInt32(e.CommandArgument);
        //        dataTable = (DataTable)ViewState["DT"];
        //        dataTable.Rows.RemoveAt(k1);                
        //        GvPakageAssign.DataSource = dataTable;
        //        GvPakageAssign.DataBind();
        //        ViewState["DT"] = dataTable;

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandiling.SendErrorToText(ex);
        //       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //    }
        //}        

               
    }
}