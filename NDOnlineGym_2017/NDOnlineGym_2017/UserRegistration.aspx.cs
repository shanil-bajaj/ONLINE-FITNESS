using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Globalization;
using DataAccessLayer;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        BalUserResitration objUser = new BalUserResitration();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignMonthDate(); 
                if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                {
                    btnSave.Text = "Edit";
                    btnClear.Visible = false;
                    divformheader.Visible = false;
                    divformpanel.Visible = false;
                    int Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                    GetDataOnEdit(Login_AutoID);
                    Disable();
                }
                else
                    bindDDLStaffName();
                txtStaffID.Focus();

                if (Request.QueryString["MenuUserDetails"] != null)
                {
                    divUserRegistration.Visible = false;
                    divUserDetails.Visible = true;
                    divFormDetails.Visible = false;
                    divSearch.Visible = true;
                    txtFromDate.Focus();
                    serach();                    
                }
            }
        }

        public void Enable()
        {
            //txtStaffID.Enabled = true;
            //ddlStaffName.Enabled = true;
            //txtMobile.Enabled = true;
            //txtEmail.Enabled = true;
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            //ddlStatus.Enabled = true;
            //ddlAuthority.Enabled = true;
        }

        public void Disable()
        {
            txtStaffID.Enabled = false;
            ddlStaffName.Enabled = false;
            txtMobile.Enabled = false;
            txtEmail.Enabled = false;
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            ddlStatus.Enabled = false;
            ddlAuthority.Enabled = false;
        }

        public void bindDDLStaffName()
        {
            try
            {
                objUser.authority = Request.Cookies["OnlineGym"]["Authority"];
                objUser.Action = "BindDDL";
                objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objUser.Select_StaffForUserReg();
                if (dt.Rows.Count > 0)
                {
                    ddlStaffName.DataSource = dt;
                    ddlStaffName.Items.Clear();
                    ddlStaffName.DataValueField = "Staff_AutoID";
                    ddlStaffName.DataTextField = "Name";
                    ddlStaffName.DataBind();
                    ddlStaffName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlStaffName.SelectedValue="--Select--";
                }
                else
                {
                    ddlStaffName.DataSource = dt;
                    ddlStaffName.DataBind();
                    ddlStaffName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    //ddlStaffName.SelectedValue = "--Select--";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('First Insert Staff !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void AddParameters()
        { 
             DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                objUser.RegDate = todaydate.ToString("yyyy-MM-dd");
            }
            objUser.name = ddlStaffName.SelectedItem.Text;
            objUser.Staff_AutoID = Convert.ToInt32(ddlStaffName.SelectedItem.Value);
            objUser.mobile = txtMobile.Text;
            objUser.email = txtEmail.Text;
            // objUser.mobile = txtMobile.Text;
            objUser.userename = txtUsername.Text;
            objUser.password = txtPassword.Text;
            objUser.status = ddlStatus.SelectedItem.Value;
            objUser.authority = ddlAuthority.SelectedItem.Value;
            objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    AddParameters();
                    objUser.Action = "Exist";
                    int Exits1 = Convert.ToInt32(objUser.Exits());
                    if (Exits1 > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Username Already Exits !!!','Error');", true);
                        txtUsername.Focus();
                        return;
                    }
                    else
                    {
                        objUser.Action = "Insert";
                        int res = objUser.Insert_UserRegistration();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            bindDDLStaffName(); 
                            clear();
                            if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                            {
                                btnClear.Visible = false;
                            }
                            else
                                btnClear.Visible = true;
                            //BindGrid();
                            objUser.Action = "SearchByDate";
                            Bind();
                            btnSave.Focus();
                            
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    AddParameters();

                    objUser.Action = "ExistOnUpdate";
                    objUser.LogAutoId = Convert.ToInt32(ViewState["Login_AutoID"]);
                    int Exits1 = Convert.ToInt32(objUser.Exits());
                    if (Exits1 > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Username Already Exits !!!','Error');", true);
                        txtUsername.Focus();
                        return;
                    }
                    else
                    {
                        objUser.Action = "Update";
                        if (ViewState["Login_AutoID"] != null)
                            objUser.LogAutoId = Convert.ToInt32(ViewState["Login_AutoID"]);
                        else
                            objUser.LogAutoId = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                        int res = objUser.Insert_UserRegistration();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                            txtStaffID.Focus();
                            if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                            {
                                btnSave.Text = "Edit";
                                //clear();
                                Disable();
                            }
                            else
                            {
                                clear();
                                btnSave.Text = "Save";
                                // BindGrid();
                                objUser.Action = "SearchByDate";
                                Bind();
                            }
                            if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                            {
                                btnClear.Visible = false;
                            }
                            else
                                btnClear.Visible = true;
                            btnSave.Focus();
                            txtStaffID.Enabled = true;
                            ddlStaffName.Enabled = true;
                            txtMobile.Enabled = true;
                            bindDDLStaffName();
                            if (Request.QueryString["MenuUserDetails"] != null)
                            {
                                divFormDetails.Visible = false;
                                divSearch.Visible = true;
                                txtFromDate.Focus();
                                divUserRegistration.Visible = false;
                                divUserDetails.Visible = true;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Updated Failed !!!','Error');", true);
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        

        public void GetDataOnEdit( int Login_AutoID)
        {
            try
            {
                btnClear.Visible = false;
                txtStaffID.Enabled = false;
                txtEmail.Enabled = false;
                txtMobile.Enabled = false;
                ddlStaffName.Enabled = false;
                //bindDDLStaffName();
                objUser.LogAutoId = Convert.ToInt32(Login_AutoID);
                objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objUser.authority = Request.Cookies["OnlineGym"]["Authority"];
              
                objUser.Action = "Select_By_Id";
               
                dt = objUser.SelectByID_UserInformation();
                if (dt.Rows.Count > 0)
                {

                    if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                    {
                        //txtUsername.Enabled = true;
                        //txtPassword.Enabled = true;
                        ddlStatus.Enabled = true;
                        ddlAuthority.Enabled = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                    {
                        //txtUsername.Enabled = true;
                        //txtPassword.Enabled = true;
                        ddlStatus.Enabled = true;
                        ddlAuthority.Enabled = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                    {
                        //txtUsername.Enabled = true;
                        //txtPassword.Enabled = true;
                        ddlStatus.Enabled = true;
                        ddlAuthority.Enabled = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                    {
                        //txtUsername.Enabled = true;
                        //txtPassword.Enabled = true;
                        ddlStatus.Enabled = true;
                        ddlAuthority.Enabled = false;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                    {
                        //txtUsername.Enabled = false;
                        //txtPassword.Enabled = false;
                        ddlStatus.Enabled = false;
                        ddlAuthority.Enabled = false;
                    }
                    ddlStaffName.SelectedItem.Value = dt.Rows[0]["Staff_AutoID"].ToString();
                    ddlStaffName.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
                    txtStaffID.Text = dt.Rows[0]["Staff_ID1"].ToString();
                    txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                    txtUsername.Text = dt.Rows[0]["Username"].ToString();
                    txtPassword.Text = dt.Rows[0]["Password"].ToString();
                    ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    ddlAuthority.SelectedValue = dt.Rows[0]["Authority"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (Request.QueryString["MenuUserDetails"] != null)
                {
                    divUserRegistration.Visible = true;
                    divUserDetails.Visible = false;
                    divFormDetails.Visible = true;
                    divSearch.Visible = false;
                    txtFromDate.Focus();
                }
                ViewState["Login_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSave.Text = "Update";
                int Login_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                GetDataOnEdit(Login_AutoID);
                txtUsername.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void clear()
        {
            if (btnSave.Text == "Save")
                ddlStaffName.SelectedValue = "--Select--";
            else
            {
                ddlStaffName.SelectedItem.Value = "--Select--";
                ddlStaffName.SelectedItem.Text = "--Select--";
            }
            txtStaffID.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtPassword.Text="";
            txtSearch.Text="";
            txtUsername.Text = "";
            ddlStatus.SelectedValue = "--Select--";
            ddlAuthority.SelectedValue = "--Select--";
            btnSave.Text = "Save";
            ddlcategory.SelectedValue = "--Select--";
            txtStaffID.Focus();
            
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
           try
            {
                objUser.LogAutoId = Convert.ToInt32(e.CommandArgument);
                objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objUser.Action = "Delete";

                int i = objUser.Delete();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    //BindGrid();
                    objUser.Action = "SearchByDate";
                    Bind();
                    bindDDLStaffName();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Cannot Delete, Already Assigned !!!.','Information');", true);
                return;
            }
        
        }

        protected void txtStaffID_TextChanged(object sender, EventArgs e)
        {
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalStaffRegistration.Action = "SELECT_BY_StaffID";
            obBalStaffRegistration.Staff_ID1 = Convert.ToInt32(txtStaffID.Text);
            dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
            if (dt.Rows.Count > 0)
            {
                ddlStaffName.SelectedValue = dt.Rows[0]["Staff_AutoID"].ToString();
                ddlStaffName.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
                txtMobile.Text = dt.Rows[0]["Contact1"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtUsername.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record not Found !!!.','Information');", true);
                ddlStaffName.SelectedItem.Value = "--Select--";
                ddlStaffName.SelectedItem.Text = "--Select--";
                txtStaffID.Text = "";
                txtMobile.Text = "";
                txtEmail.Text = "";
                return;
            }
            txtUsername.Focus();
        }

        protected void ddlStaffName_SelectedIndexChanged(object sender, EventArgs e)
        {
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalStaffRegistration.Action = "SELECT_BY_Name";
            if (ddlStaffName.SelectedValue != "--Select--")
                obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(ddlStaffName.SelectedItem.Value);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Select Staff Name !!!.','Information');", true);
            dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
            if (dt.Rows.Count > 0)
            {
                txtStaffID.Text = dt.Rows[0]["Staff_ID1"].ToString();
                txtMobile.Text = dt.Rows[0]["Contact1"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtUsername.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record not Found !!!.','Information');", true);
                ddlStaffName.SelectedItem.Value = "--Select--";
                ddlStaffName.SelectedItem.Text = "--Select--";
                txtStaffID.Text = "";
                txtMobile.Text = "";
                txtEmail.Text = "";
                return;
            }

        }

        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalStaffRegistration.Action = "SELECT_BY_Mobile";
            obBalStaffRegistration.Contact1 = txtMobile.Text;
            dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
            if (dt.Rows.Count > 0)
            {
                txtStaffID.Text = dt.Rows[0]["Staff_ID1"].ToString();
                ddlStaffName.SelectedValue = dt.Rows[0]["Staff_AutoID"].ToString();
                ddlStaffName.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtUsername.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record not Found !!!.','Information');", true);
                ddlStaffName.SelectedItem.Value = "--Select--";
                ddlStaffName.SelectedItem.Text = "--Select--";
                txtStaffID.Text = "";
                txtMobile.Text = "";
                txtEmail.Text = "";
                return;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            gvUserReg.DataSource = null;
            gvUserReg.DataBind();
            txtStaffID.Focus();
            serach();

        }

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
            objUser.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            objUser.ToDate = Todate;
        }
        #endregion

        public void SearchByCategory()
        {
            try
            {
                if (txtSearch.Text != string.Empty)
                {
                    objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objUser.LogAutoId = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                    objUser.authority = Request.Cookies["OnlineGym"]["Authority"];

                    //if (ddlcategory.SelectedValue == "--Select--")
                    //{
                    //    objUser.Category = "All";
                    //}
                    //else 
                    if (ddlcategory.SelectedValue == "Name")
                    {
                        objUser.Category = "ByName";
                        objUser.searchTxt = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue == "Contact")
                    {
                        objUser.Category = "ByContact";
                        objUser.searchTxt = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue == "Status")
                    {
                        objUser.Category = "ByStaus";
                        objUser.searchTxt = txtSearch.Text;
                    }
                    else if (ddlcategory.SelectedValue == "Authority")
                    {
                        objUser.Category = "ByAuthority";
                        objUser.searchTxt = txtSearch.Text;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                        ddlcategory.Focus();
                        return;
                    }
                    Bind();
                    btnDateWithCategory.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        /// <summary>
        ///  current month record
        /// </summary>

        public void serach()
        {
            objUser.Action = "SearchByDate";
            objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objUser.authority = Request.Cookies["OnlineGym"]["Authority"];
            Bind();
            btnSearch.Focus();
            flag = 1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            serach();
        }

        private void Bind()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                objUser.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                objUser.ToDate = Todate;
                objUser.authority = Request.Cookies["OnlineGym"]["Authority"];
                objUser.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objUser.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);                
                dt.Clear();
                dt = objUser.Get_Search();
                lblCount.Text = dt.Rows.Count.ToString();
                ViewState["UserDetails"] = dt;
                if (dt.Rows.Count > 0)
                {                    
                    gvUserReg.DataSource = dt;
                    gvUserReg.DataBind();                    
                }
                else
                {
                    txtSearch.Text = "";
                    ddlcategory.SelectedValue = "--Select--";
                    gvUserReg.DataSource = null;
                    gvUserReg.DataBind();
                    gvUserReg.Style["width"] = "100%";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Find !!!.','Information');", true);
                    return;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvUserReg.Columns[0].Visible = true;
                    gvUserReg.Columns[1].Visible = true;
                    gvUserReg.Columns[5].Visible = true;
                    gvUserReg.Columns[6].Visible = true;

                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvUserReg.Columns[0].Visible = true;
                    gvUserReg.Columns[1].Visible = true;
                    gvUserReg.Columns[5].Visible = true;
                    gvUserReg.Columns[6].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvUserReg.Columns[0].Visible = true;
                    gvUserReg.Columns[1].Visible = true;
                    gvUserReg.Columns[5].Visible = true;
                    gvUserReg.Columns[6].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvUserReg.Columns[0].Visible = true;
                    gvUserReg.Columns[1].Visible = false;
                    gvUserReg.Columns[5].Visible = true;
                    gvUserReg.Columns[6].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvUserReg.Columns[0].Visible = false;
                    gvUserReg.Columns[1].Visible = false;
                    gvUserReg.Columns[5].Visible = false;
                    gvUserReg.Columns[6].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        static int flag;
        protected void gvUserReg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserReg.PageIndex = 0;

            if (flag == 1)
            {
                gvUserReg.PageIndex = e.NewPageIndex;
                objUser.Action = "SearchByDate";
                Bind();
            }
            else if (flag == 2)
            {
                gvUserReg.PageIndex = e.NewPageIndex;
                objUser.Action = "SearchByCategory";
                SearchByCategory();
            }
            else if (flag == 3)
            {
                gvUserReg.PageIndex = e.NewPageIndex;
                objUser.Action = "SearchByDateWithCategory";
                SearchByCategory();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            objUser.Action = "SearchByCategory";
            SearchByCategory();
            flag = 2;            
        }

        protected void btnDateWithCategory_Click(object sender, EventArgs e)
        {
            if (ddlcategory.SelectedValue == "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                return;
            }
            else if (ddlcategory.SelectedValue != "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                return;
            }
            else
            {
                objUser.Action = "SearchByDateWithCategory";
                SearchByCategory();
                flag = 3;
            }
        }
        protected void btnClear1_Click(object sender, EventArgs e)
        {
            lblCount.Text = "0";
            AssignMonthDate();
            ddlcategory.SelectedValue = "--Select--";
            txtSearch.Text = "";
            serach();  
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
                if (ViewState["UserDetails"] != null)
                {
                    dt = (DataTable)ViewState["UserDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=UserDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvUserReg.Columns[0].Visible = false;
                            gvUserReg.Columns[1].Visible = false;
                            gvUserReg.AllowPaging = false;

                            gvUserReg.DataSource = dt;
                            gvUserReg.DataBind();
                            gvUserReg.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvUserReg.HeaderRow.Cells)
                            {
                                cell.BackColor = gvUserReg.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvUserReg.Rows)
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
                            gvUserReg.GridLines = GridLines.Both;
                            gvUserReg.RenderControl(hw);
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