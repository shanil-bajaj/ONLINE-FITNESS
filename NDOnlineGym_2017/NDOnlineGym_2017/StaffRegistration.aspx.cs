using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Globalization;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class StaffRegistration : System.Web.UI.Page
    {
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalStaffRegistration StaffObj = new BalStaffRegistration();
        DataTable dataTable = new DataTable();
        DataTable dt = new DataTable();
        string newfileName = string.Empty;
        string serverfilrpath = string.Empty;
        static int branchid;
        static int flag;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    AssignMonthDate();
                    AssignTodaysDate();
                    BindDepartment();
                    BindDesignation();
                    BindShift();
                    Get_StaffID1();
                    //gvStaffReg.Visible = false;
                    bindDDLExecutive();
                    setExecutive();
                    txtStaffId1.Focus();
                    if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                    {
                        btnSave.Text = "Edit";
                        btnCancle.Visible = false;
                        divSearchHead.Visible = false;
                        divSearch.Visible = false;
                        divStaffDetails.Visible = false;
                        divStaffReg.Visible = true;
                        int Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
                        GetDataOnEdit(Staff_AutoID);
                        Disable();
                    }

                    if (Request.QueryString["MenuStaffDetails"] != null)
                    {
                        //divSearchHead.Visible = true;
                        divStaffDetails.Visible = true;
                        divFormDetails.Visible = false;
                        divStaffReg.Visible = false;
                        divSearch.Visible = true;
                        txtFromDate.Focus();
                        BindGridOnSearchButton();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                }
            }

        }
        #endregion

        #region ------------ Assign month Date ------------------
        protected void AssignMonthDate()
        {
            Label1.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Label1.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);
            txtToDate.Text = dtLast.ToString("dd-MM-yyyy");

            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            StaffObj.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            StaffObj.ToDate = Todate;
        }
        #endregion

        public void GetDataOnEdit(int Staff_AutoID)
        {
            try
            {
                //divSearchHead.Visible = false;
                divFormDetails.Visible = true;
                divStaffDetails.Visible = false;
                divStaffReg.Visible = true;
                divSearch.Visible = false;
                ViewState["Staff_ID"] = Staff_AutoID;
                StaffObj.Staff_ID = Convert.ToInt32(Staff_AutoID);
                dataTable = StaffObj.SelectByID_UserInformation();
                if (dataTable.Rows.Count > 0)
                {
                    StaffObj.Branch_ID = Convert.ToInt32(dataTable.Rows[0]["Branch_AutoID"].ToString());
                    StaffObj.Comp_ID = Convert.ToInt32(dataTable.Rows[0]["Company_AutoID"].ToString());
                    txtFirstName.Text = dataTable.Rows[0]["FName"].ToString();
                    txtLastName.Text = dataTable.Rows[0]["LName"].ToString();
                    txtContact1.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtContact2.Text = dataTable.Rows[0]["Contact2"].ToString();
                    ddlG.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                    if (dataTable.Rows[0]["RegDate"].ToString() != "")
                    {
                        DateTime Regdate = Convert.ToDateTime(dataTable.Rows[0]["RegDate"].ToString());
                        DateTime Regdate1;
                        if (DateTime.TryParseExact(Regdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Regdate1))
                        {
                            txtRegDate.Text = Regdate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtRegDate.Text = "";

                    txtEmail.Text = dataTable.Rows[0]["Email"].ToString();
                    txtIDProof.Text = dataTable.Rows[0]["IDProofPath"].ToString();

                    if (dataTable.Rows[0]["DOB"].ToString() != "")
                    {
                        DateTime DOBdate = Convert.ToDateTime(dataTable.Rows[0]["DOB"].ToString());
                        DateTime DOBdate1;
                        if (DateTime.TryParseExact(DOBdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOBdate1))
                        {
                            txtDOB.Text = DOBdate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtDOB.Text = "";

                    txtSalary.Text = dataTable.Rows[0]["Salary"].ToString();
                    txtAddress.Text = dataTable.Rows[0]["Address"].ToString();
                    if( dataTable.Rows[0]["Dept_AutoID"].ToString() == "")
                        ddlDepartment.SelectedValue = "--Select--";
                    else
                        ddlDepartment.SelectedValue = dataTable.Rows[0]["Dept_AutoID"].ToString();

                    if(dataTable.Rows[0]["Desig_AutoID"].ToString() == "")
                        ddlDesignation.SelectedValue ="--Select--";
                    else
                        ddlDesignation.SelectedValue = dataTable.Rows[0]["Desig_AutoID"].ToString();

                    txtStaffId1.Text = dataTable.Rows[0]["Staff_ID1"].ToString();
                    txtCardNumber.Text = dataTable.Rows[0]["CardNo"].ToString();
                    ddlStatus.Text = dataTable.Rows[0]["Status"].ToString();
                    if (dataTable.Rows[0]["Shift_AutoID"].ToString() == "")
                        ddlShift.SelectedValue = "--Select--";
                    else
                        ddlShift.SelectedValue = dataTable.Rows[0]["Shift_AutoID"].ToString();
                    
                    imgMember.ImageUrl = dataTable.Rows[0]["ImagePath"].ToString();
                    ViewState["imagepath"] = dataTable.Rows[0]["ImagePath"];
                    ddlExecutive.SelectedItem.Value = dataTable.Rows[0]["Executive_ID"].ToString();
                    ddlExecutive.SelectedItem.Text = dataTable.Rows[0]["Executive"].ToString();
                    txtFirstName.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void Enable()
        {
            try
            {
                txtSearch.Enabled = true;
                txtSalary.Enabled = true;
                txtRegDate.Enabled = true;
                txtLastName.Enabled = true;
                txtIDProof.Enabled = true;
                txtFirstName.Enabled = true;
                txtEmail.Enabled = true;
                txtDOB.Enabled = true;
                txtContact2.Enabled = true;
                txtContact1.Enabled = true;
                txtCardNumber.Enabled = true;
                txtAddress.Enabled = true;
                ddlcategory.Enabled = true;
                ddlG.Enabled = true;
                ddlStatus.Enabled = true;
                ddlShift.Enabled = true;
                //gvStaffReg.Visible = false;
                imgMember.Enabled = true;
                chkExecutive.Checked = true;
                //ddlExecutive.SelectedItem.Value = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutive.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void Disable()
        {
            try
            {
                txtSearch.Enabled = false;
                txtSalary.Enabled = false;
                txtRegDate.Enabled = false;
                txtLastName.Enabled = false;
                txtIDProof.Enabled = false;
                txtFirstName.Enabled = false;
                txtEmail.Enabled = false;
                txtDOB.Enabled = false;
                txtContact2.Enabled = false;
                txtContact1.Enabled = false;
                txtCardNumber.Enabled = false;
                txtAddress.Enabled = false;
                ddlcategory.Enabled = false;
                ddlG.Enabled = false;
                ddlStatus.Enabled = false;
                ddlShift.Enabled = false;
                //gvStaffReg.Visible = false;
                imgMember.Enabled = false;
                chkExecutive.Checked = true;
                ddlExecutive.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dt.Rows.Count != 0)
                {
                    ddlExecutive.DataSource = dt;
                    ddlExecutive.Items.Clear();
                    ddlExecutive.DataValueField = "Staff_AutoID";
                    ddlExecutive.DataTextField = "Name";
                    ddlExecutive.DataBind();
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void setExecutive()
        {
            try
            {
                obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
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
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #region Assign Date

        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtRegDate.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
#endregion


        #region Get StaffID1
        public void Get_StaffID1()
        {
            try
            {
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = StaffObj.Get_StaffID1();
                txtStaffId1.Text = dataTable.Rows[0]["Staff_ID1"].ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion


        #region Bind Designation
        private void BindDesignation()
        {
            try
            {
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = StaffObj.GetDesignationName();
                if (dataTable.Rows.Count > 0)
                {
                    ddlDesignation.DataSource = dataTable;
                    ddlDesignation.Items.Clear();
                    ddlDesignation.DataValueField = "Desig_AutoID";
                    ddlDesignation.DataTextField = "Name";
                    ddlDesignation.DataBind();
                    ddlDesignation.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion


        #region Clear Record
        public void ClearRecord()
        {
            try
            {
                flag = 0;
                txtSearch.Text = "";
                txtSalary.Text = "";
                txtRegDate.Text = "";
                txtLastName.Text = "";
                txtIDProof.Text = "";
                txtFirstName.Text = "";
                txtEmail.Text = "";
                txtDOB.Text = "";
                txtContact2.Text = "";
                txtContact1.Text = "";
                txtCardNumber.Text = "";
                txtAddress.Text = "";
                ddlcategory.SelectedIndex = 0;
                ddlG.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;
                ddlShift.SelectedItem.Value = "--Select--";
                AssignTodaysDate();
                BindDepartment();
                BindDesignation();
                BindShift();
                Get_StaffID1();
                btnSave.Text = "Save";
                //gvStaffReg.Visible = false;
                imgMember.ImageUrl = "";
                chkExecutive.Checked = true;
                ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutive.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion


        


        #region Bind Shift
        private void BindShift()
        {
            try
            {
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = StaffObj.GetShift();
                if (dataTable.Rows.Count > 0)
                {
                    ddlShift.DataSource = dataTable;
                    ddlShift.Items.Clear();
                    ddlShift.DataValueField = "Shift_AutoID";
                    ddlShift.DataTextField = "Name";
                    ddlShift.DataBind();
                    ddlShift.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region Bind Department
        private void BindDepartment()
        {
            try
            {
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = StaffObj.GetDepartmentName();
                if (dataTable.Rows.Count > 0)
                {
                    ddlDepartment.DataSource = dataTable;
                    ddlDepartment.Items.Clear();
                    ddlDepartment.DataValueField = "Dept_AutoID";
                    ddlDepartment.DataTextField = "Name";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion
        
        #region AddRecored
        public void AddRecord()
        {
            try
            {
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                StaffObj.Staff_ID1 = Convert.ToInt32(txtStaffId1.Text);
                StaffObj.Fname = txtFirstName.Text;
                StaffObj.Lname = txtLastName.Text;
                DateTime Regdate;
                if (DateTime.TryParseExact(txtRegDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Regdate))
                {
                    string Regdate1 = Regdate.ToString("dd-MM-yyyy");
                    StaffObj.Reg_date = DateTime.ParseExact(Regdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                if (txtDOB.Text != "")
                {
                    DateTime DOBdate;
                    if (DateTime.TryParseExact(txtDOB.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOBdate))
                    {
                        string DOBdate1 = DOBdate.ToString("dd-MM-yyyy");
                        StaffObj.DOB = DateTime.ParseExact(DOBdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                }
                else
                    StaffObj.DOB = null;
                StaffObj.Address = txtAddress.Text;
                if (txtSalary.Text != "")
                {
                    StaffObj.Salary = Convert.ToInt32(txtSalary.Text);
                }
                if (ddlShift.SelectedValue == "--Select--")
                {
                    StaffObj.Shift = null;
                }
                else
                {
                    StaffObj.Shift = Convert.ToInt32(ddlShift.SelectedValue);
                }
                StaffObj.CardNo = txtCardNumber.Text;
                StaffObj.Staff_ID1 = Convert.ToInt16(txtStaffId1.Text);
                if (ddlDepartment.SelectedItem.Value != "--Select--")
                {
                    StaffObj.Dept_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
                }
                else
                {
                    StaffObj.Dept_ID = null;
                }

                if (ddlDesignation.SelectedItem.Value != "--Select--")
                {
                    StaffObj.Desig_ID = Convert.ToInt32(ddlDesignation.SelectedValue);
                }
                else
                {
                    StaffObj.Desig_ID = null;
                }

                if (ddlStatus.Text != "--Select--")
                {
                    StaffObj.Status = ddlStatus.Text;
                }
                else
                {
                    StaffObj.Status = null;
                }
                StaffObj.Gender = ddlG.Text;
                StaffObj.Contact1 = txtContact1.Text;
                StaffObj.Contact2 = txtContact2.Text;
                StaffObj.IDProofPath = txtIDProof.Text;
                StaffObj.Email = txtEmail.Text;
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                StaffObj.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion


        #region Save_Button Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    if (txtStaffId1.Text != "")
                    {
                        StaffObj.Action = "CheckID";
                        int res = StaffObj.checkID();
                        if (res > 0)
                        {
                            Get_StaffID1();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Staff ID Already Assign !!!','Error');", true);
                            return;
                        }
                    }
                    AddRecord();
                    ImageUplode();
                    CheckID();
                    if (fileLogo.HasFile)
                    {
                        serverfilrpath = ViewState["serverfilrpath1"].ToString();
                        StaffObj.ImagePath = serverfilrpath;
                    }
                    else
                    {
                        if (ViewState["imagepath"] != null)
                        {
                            StaffObj.ImagePath = ViewState["imagepath"].ToString();
                        }
                    }
                    StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    StaffObj.Action = "Check_Contact_Save";
                    int r = StaffObj.Contactcheck();
                    if (r > 0)
                    {
                        txtContact1.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                        return;
                    }
                    else
                    {
                        StaffObj.Action = "Insert";
                        int res = StaffObj.Insert_StaffRegistration();
                        if (res > 0)
                        {

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            BindGrid();
                            if (Request.Cookies["OnlineGym"]["Authority"].ToString() != "User")
                            {
                                ClearRecord();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                            return;
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    if (txtStaffId1.Text != "")
                    {
                        StaffObj.Staff_AutoID = Convert.ToInt32(ViewState["Staff_ID"]);
                        StaffObj.Action = "CheckID_OnUpdate";
                        int res = StaffObj.checkID();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Staff ID Already Assign !!!','Error');", true);
                            return;
                        }
                    }
                    AddRecord();
                    ImageUplode();
                    if (fileLogo.HasFile)
                    {
                        serverfilrpath = ViewState["serverfilrpath1"].ToString();
                        StaffObj.ImagePath = serverfilrpath;
                    }
                    else
                    {
                        if (ViewState["imagepath"] != null)
                        {
                            StaffObj.ImagePath = ViewState["imagepath"].ToString();
                        }
                    }

                    StaffObj.Staff_ID = Convert.ToInt32(ViewState["Staff_ID"]);
                    StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    StaffObj.Staff_AutoID = Convert.ToInt32(ViewState["Staff_ID"]);

                    StaffObj.Action = "Check Contact";
                    int p = 0;
                    p = StaffObj.Contactcheck1();

                    if (p > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                        return;
                    }
                    else
                    {
                        StaffObj.Action = "Edit";
                        int res = StaffObj.Insert_StaffRegistration();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                            BindGrid();
                            if (Request.Cookies["OnlineGym"]["Authority"].ToString() != "User")
                                ClearRecord();
                            else
                            {
                                Disable();
                                btnSave.Text = "Edit";
                            }
                            if (Request.QueryString["MenuStaffDetails"] != null)
                            {
                                //divSearchHead.Visible = true;
                                divStaffDetails.Visible = true;
                                divStaffReg.Visible = false;
                                divFormDetails.Visible = false;
                                divSearch.Visible = true;
                                txtFromDate.Focus();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                            return;
                        }
                    }

                }
                else
                {
                    Enable();
                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion


        #region Image_Upload
        public void ImageUplode()
        {
            try
            {
                if ((fileLogo.PostedFile != null) && (fileLogo.PostedFile.ContentLength > 0))
                {
                    Guid uid = Guid.NewGuid();
                    string fn = System.IO.Path.GetFileName(fileLogo.PostedFile.FileName);
                    DateTime dt = DateTime.Now;
                    newfileName = txtFirstName.Text.Trim() + "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                    string fileName = Path.GetFileName(fileLogo.PostedFile.FileName);
                    string primaryFileName = Path.GetFileNameWithoutExtension(fileName);
                    string fileExtentionName = Path.GetExtension(fileName);
                    string SaveLocation = Server.MapPath("/Logo/") + newfileName + fileExtentionName;
                    try
                    {
                        string fileExtention = fileLogo.PostedFile.ContentType;
                        int fileLenght = fileLogo.PostedFile.ContentLength;
                        if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                        {
                            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fileLogo.PostedFile.InputStream);
                            System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);

                            objImage.Save(SaveLocation, ImageFormat.Jpeg);
                            ViewState["serverfilrpath1"] = "/Logo/" + newfileName + fileExtentionName;

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invaild Format !!!','Error');", true);
                            txtFirstName.Focus();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                        txtFirstName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
        
            var newWidth = (int)(85);
            var newHeight = (int)(75);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        #endregion


        #region Edit_Command
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int Staff_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["Staff_ID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSave.Text = "Update";
                GetDataOnEdit(Staff_AutoID);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion


        #region Delete_Command
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                if (id > 0)
                {
                    StaffObj.Action = "Delete";
                    StaffObj.Staff_ID = id;
                    int res = StaffObj.Delete_Staff();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully!!!','Success');", true);
                    BindGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Deleted!!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!!.','Error');", true);
            }
        }
        #endregion

        #region Clear
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearRecord();
        }
        #endregion

        #region ID CHECK
        public void CheckID()
        {
            try
            {
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                StaffObj.Staff_ID1 = Convert.ToInt32(txtStaffId1.Text);
                if (btnSave.Text == "Save")
                {
                    if (txtStaffId1.Text != "")
                    {
                        StaffObj.Action = "CheckID";
                        int res = StaffObj.checkID();
                        if (res > 0)
                        {
                            Get_StaffID1();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Staff ID Already Assign !!!','Error');", true);
                            return;
                        }
                    }
                }
                else
                {
                    if (txtStaffId1.Text != "")
                    {
                        StaffObj.Staff_AutoID = Convert.ToInt32(ViewState["Staff_ID"]);
                        StaffObj.Action = "CheckID_OnUpdate";
                        int res = StaffObj.checkID();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Staff ID Already Assign !!!','Error');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        # endregion

        #region Contact Check
        public void Contact_Check()
        {
            try
            {
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                StaffObj.Contact1 = txtContact1.Text;
                if (btnSave.Text == "Save")
                {
                    StaffObj.Action = "Check_Contact_Save";
                    int res = StaffObj.Contactcheck();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                        txtContact1.Focus();
                        return;
                    }
                    else
                    {
                        txtContact2.Focus();
                    }
                }
                else
                {
                    StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    StaffObj.Staff_AutoID = Convert.ToInt32(ViewState["Staff_ID"]);
                    StaffObj.Action = "Check Contact";
                    int re = 0;
                    re = StaffObj.Contactcheck1();

                    if (re > 0)
                    {
                        txtContact1.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                        return;
                    }
                    else
                    {
                        txtContact2.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        protected void txtContact1_TextChanged(object sender, EventArgs e)
        {
            Contact_Check();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            imgMember.ImageUrl = "";
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
            ddlExecutive.Focus();
        }

        #region StaffID_Change
        protected void txtStaffId1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                CheckID();
                if (txtStaffId1.Text == "")
                {
                    dataTable = StaffObj.Get_StaffID1();
                    txtStaffId1.Text = dataTable.Rows[0]["Staff_ID1"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region Bind Grid View
        public void BindGrid()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                StaffObj.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                StaffObj.ToDate = Todate;
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = StaffObj.Get_Search();
                lblCount.Text = Convert.ToString(dataTable.Rows.Count);
                if (dataTable.Rows.Count > 0)
                {
                    ViewState["StaffDetails"] = dataTable;
                    //gvStaffReg.Visible = true;
                    gvStaffReg.DataSource = dataTable;
                    gvStaffReg.DataBind();
                }
                else
                {
                    //gvStaffReg.Visible = true;
                    gvStaffReg.DataSource = dataTable;
                    gvStaffReg.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvStaffReg.Columns[0].Visible = true;
                    gvStaffReg.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvStaffReg.Columns[0].Visible = true;
                    gvStaffReg.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvStaffReg.Columns[0].Visible = true;
                    gvStaffReg.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvStaffReg.Columns[0].Visible = true;
                    gvStaffReg.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvStaffReg.Columns[0].Visible = false;
                    gvStaffReg.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        public void SearchByCategory()
        {
            try
            {
                StaffObj.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                StaffObj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);

                StaffObj.Action = "SearchBYCategory";
                if (ddlcategory.Text != "--Select--" && ddlcategory.Text != "")
                {
                    if (ddlcategory.Text == "StaffID")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = "Staff ID";
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "FirstName")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = "FirstName";
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "LastName")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = "LastName";
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "Contact")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = "Contact";
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "Status")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = "Status";
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "Rights")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = ddlcategory.Text;
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "Department")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = "Department";
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "Gender")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = ddlcategory.Text;
                        dataTable = StaffObj.SearchCategory();
                    }
                    else if (ddlcategory.Text == "Designation")
                    {
                        StaffObj.searchTxt = txtSearch.Text;
                        StaffObj.Category = "Designation";
                        dataTable = StaffObj.SearchCategory();
                    }
                    BindGrid();
                    //if (dataTable.Rows.Count > 0)
                    //{
                    //    gvStaffReg.Visible = true;
                    //    gvStaffReg.DataSource = dataTable;
                    //    gvStaffReg.DataBind();
                    //}
                    //else
                    //{
                    //    gvStaffReg.Visible = true;
                    //    gvStaffReg.DataSource = dataTable;
                    //    gvStaffReg.DataBind();
                    //}
                    //flag = 1;
                    //if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                    //{
                    //    gvStaffReg.Columns[0].Visible = true;
                    //    gvStaffReg.Columns[1].Visible = true;
                    //}
                    //else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                    //{
                    //    gvStaffReg.Columns[0].Visible = true;
                    //    gvStaffReg.Columns[1].Visible = true;
                    //}
                    //else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                    //{
                    //    gvStaffReg.Columns[0].Visible = true;
                    //    gvStaffReg.Columns[1].Visible = true;
                    //}
                    //else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                    //{
                    //    gvStaffReg.Columns[0].Visible = true;
                    //    gvStaffReg.Columns[1].Visible = false;
                    //}
                    //else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                    //{
                    //    gvStaffReg.Columns[0].Visible = false;
                    //    gvStaffReg.Columns[1].Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #region Search_Click
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime Fromdate;
                //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                //{ }
                //StaffObj.FromDate = Fromdate;
                //DateTime Todate;
                //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                //{ }
                //StaffObj.ToDate = Todate;
                //StaffObj.Action = "SearchByDate";
                //BindGrid();
                //flag = 1;
                //btnSearch.Focus();
                
                BindGridOnSearchButton();
            }
            catch (Exception ex)
            {

            }

        }
        protected void BindGridOnSearchButton()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                StaffObj.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                StaffObj.ToDate = Todate;
                StaffObj.Action = "SearchByDate";
                BindGrid();
                flag = 1;
                btnSearch.Focus();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Search On Text Change
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlcategory.Text != "--Select--" && ddlcategory.Text != "")
                {
                    StaffObj.Action = "SearchByCategory";
                    SearchByCategory();
                    flag = 2;
                    btnDateWithCategory.Focus();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Paging
        protected void gvStaffReg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                StaffObj.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                StaffObj.ToDate = Todate;
                if (flag == 1)
                {
                    gvStaffReg.PageIndex = e.NewPageIndex;
                    StaffObj.Action = "SearchByDate";
                    BindGrid();
                }
                else if (flag == 2)
                {
                    gvStaffReg.PageIndex = e.NewPageIndex;
                    StaffObj.Action = "SearchByCategory";
                    SearchByCategory();
                }
                else if (flag == 3)
                {
                    gvStaffReg.PageIndex = e.NewPageIndex;
                    StaffObj.Action = "SearchByDateWithCategory";
                    SearchByCategory();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        protected void btnDateWithCategory_Click(object sender, EventArgs e)
        {
            if (ddlcategory.SelectedValue == "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                ddlcategory.Focus();
                return;
            }
            else if (ddlcategory.SelectedValue != "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                txtSearch.Focus();
                return;
            }
            else if (ddlcategory.SelectedValue != "--Select--" && txtSearch.Text != "")
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                StaffObj.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                StaffObj.ToDate = Todate;
                StaffObj.Action = "SearchByDateWithCategory";
                //AssignMonthDate();
                SearchByCategory();
                flag = 3;
                btnDateWithCategory.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Search Category and Enter Search Text !!!.','Information');", true);
                ddlcategory.Focus();
                return;
            }
        }

        protected void btnClear1_Click(object sender, EventArgs e)
        {
            ddlcategory.SelectedValue = "--Select--";
            txtSearch.Text = "";
            AssignMonthDate();
            ViewState["StaffDetails"] = null;
            //gvStaffReg.DataSource = null;
            //gvStaffReg.DataBind();                       
            BindGridOnSearchButton();
        }

        #region ----------------- Export To Excle Record ----------------
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        protected void ExportToExcel()
        {
            try
            {
                if (ViewState["StaffDetails"] != null)
                {
                    dt = (DataTable)ViewState["StaffDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=StaffDetails_"+ DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvStaffReg.Columns[0].Visible = false;
                            gvStaffReg.Columns[1].Visible = false;
                            gvStaffReg.AllowPaging = false;
                            gvStaffReg.DataSource = dt;
                            gvStaffReg.DataBind();
                            gvStaffReg.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvStaffReg.HeaderRow.Cells)
                            {
                                cell.BackColor = gvStaffReg.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvStaffReg.Rows)
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
                            gvStaffReg.GridLines = GridLines.Both;
                            gvStaffReg.RenderControl(hw);
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
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        #endregion
    }
}