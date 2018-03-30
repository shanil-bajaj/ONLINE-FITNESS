using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;

namespace NDOnlineGym_2017
{
    public partial class Measurement : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalMeasurement objMsr = new BalMeasurement();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        static int Member_AtoId;
        static int MeasurementAutoId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //lblMemberAutoID.Visible = false;
                    txtMemberID.Focus();

                    if (Request.QueryString["MsrmntID"] != null)
                    {
                        MeasurementAutoId = Convert.ToInt32(Request.QueryString["MsrmntID"].ToString());
                        objMsr.Measurement_AutoId = MeasurementAutoId;
                        GetDataForEdit(MeasurementAutoId);
                    }
                    
                    BindProgrammerDDL();
                    bindDDLExecutive();
                    setExecutive();
                    //BindGridView();
                    AssignTodaysDate();                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        #region ------------ Assign Company and Branch ID ------------------
        private void AssignID()
        {
            objMsr.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMsr.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region ------------- Bind Executive DDL ----------------
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------------- Set Executive --------------------
        public void setExecutive()
        {
            try
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
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region -------------- Bing Programer DDL ------------------
        private void BindProgrammerDDL()
        {
            try
            {
                AssignID();
                objMsr.Action = "Select_Programmer";
                dt = objMsr.GetDetails();
                if (dt.Rows.Count >= 0)
                {
                    DdlProgrammer.DataSource = dt;
                    DdlProgrammer.Items.Clear();
                    DdlProgrammer.DataValueField = "Programmer_AutoID";
                    DdlProgrammer.DataTextField = "PName";
                    DdlProgrammer.DataBind();
                    DdlProgrammer.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion 
       
        #region -------------- text Member Id Changed -------------
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != string.Empty)
                {
                    AssignID();
                    objMsr.Member_Id = Convert.ToInt32(txtMemberID.Text.Trim());
                    objMsr.Action = "SearchByMemberID";
                    txtMemberID.Focus();
                    dt = objMsr.GetDetails();
                    if (dt.Rows.Count > 0)
                    {
                        //lblMemberAutoID.Text = dt.Rows[0]["Member_AutoID"].ToString();
                        Member_AtoId = Convert.ToInt32(dt.Rows[0]["Member_AutoID"].ToString());
                        txtFirst.Text = dt.Rows[0]["FName"].ToString();
                        txtLast.Text = dt.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                        txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                        txtmail.Text = dt.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        //ClearField();
                        Clear();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found. !!!','Information');", true);                        
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

        #region --------------- Search By Contact Text Change -------------------
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != string.Empty)
                {
                    AssignID();
                    objMsr.Contact = txtContact.Text;
                    objMsr.Action = "SearchByContact";
                    txtContact.Focus();
                    dt = objMsr.GetDetails();
                    if (dt.Rows.Count > 0)
                    {
                        //lblMemberAutoID.Text = dt.Rows[0]["Member_AutoID"].ToString();
                        Member_AtoId = Convert.ToInt32(dt.Rows[0]["Member_AutoID"].ToString());
                        txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                        txtFirst.Text = dt.Rows[0]["FName"].ToString();
                        txtLast.Text = dt.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                        txtmail.Text = dt.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        //ClearField();
                        Clear();                        
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found. !!!','Information');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion
       
        #region --------- Assign Today and Next measurment Date ----------
        protected void AssignTodaysDate()
        {
            try
            {
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                string NowDateTime = localTime.ToString("dd-MM-yyyy");
                txtDate.Text = NowDateTime;
                txtNxtFllwdte.Text = NowDateTime;
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion

        #region ------------- Check Measurement Date And Next FollowupDate Validation        
        protected int chkMeasurementDateNotNextFollowupDate()
        {
            int res=0;
            DateTime MesDate;
            DateTime NextFollDate;

            if (txtDate.Text == string.Empty)
            {
                res = 1;
                txtDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtNxtFllwdte.Text == string.Empty)
            {
                res = 1;
                txtNxtFllwdte.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter To Date !!!','Information');", true);
            }
            else
            {

                if (DateTime.TryParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out MesDate))
                {
                }

                if (DateTime.TryParseExact(txtNxtFllwdte.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NextFollDate))
                {
                }

                if (MesDate.Date <= NextFollDate.Date)
                {
                    res = 0;
                    objMsr.Date = MesDate;
                    objMsr.NextFollowupDate = NextFollDate;                   
                }
                else
                {
                    res = 1;
                    txtNxtFllwdte.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Measurement date should not be greater then Followup date !!!','Information');", true);
                }
            }

            return res;

        }
        #endregion

        #region ----------- Add Parameter -------------------
        public void AddParameter()
        {
            try
            {                              
                objMsr.Arms = txtArms.Text.Trim();
                objMsr.Weight = txtWaist.Text.Trim();
                objMsr.Height = txtHeight.Text.Trim();
                objMsr.Waist = txtWaist.Text.Trim();
                objMsr.Chest = txtChest.Text.Trim();
                objMsr.Thigh = txtThigh.Text.Trim();
                objMsr.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);               

                objMsr.Member_AutoID = Member_AtoId;
                objMsr.Age = txtMAge.Text.Trim();
                objMsr.Arms = txtArms.Text.Trim();
                objMsr.Bmass = txtBmass.Text.Trim();
                objMsr.Mmass = txtMmass.Text.Trim();
                objMsr.Neck = txtNeck.Text.Trim();
                objMsr.Shoulder = txtSholder.Text.Trim();
                objMsr.UpperAbdomen = txtUpperAbdomen.Text.Trim();
                objMsr.LowerAbdomen = txtLowerAbdomen.Text.Trim();
                objMsr.Vfat = txtVFat.Text.Trim();
                objMsr.Water = txtWater.Text.Trim();
                objMsr.UpperArms = txtUpperArms.Text.Trim();
                objMsr.Hips = txtHips.Text.Trim();
                objMsr.ChestExpanded = txtExpendedChest.Text.Trim();
                objMsr.Calf = txtCalf.Text.Trim();
                objMsr.ForArms = txtForArms.Text.Trim();
                objMsr.Fat = txtFat.Text.Trim();
                objMsr.DCI = txtDCI.Text.Trim();
                objMsr.BMI = txtBMI.Text.Trim();
                if (DdlProgrammer.SelectedValue != "--Select--")
                    objMsr.Programmer_AutoID = Convert.ToInt32(DdlProgrammer.SelectedValue.ToString());                
               
                AssignID();

                //if (Convert.ToInt32(ViewState["flag"]) != 1)
                //{
                //    objMsr.Action = "Insert";
                //}
                //else
                //{
                //    objMsr.Action = "Update";
                //    objMsr.Measurement_AutoId = Convert.ToInt32(ViewState["MsrmntID"]);
                //    ViewState["flag"] = 0;
                //}

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region -------------- Save And Update Button Event -------------------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text == string.Empty || txtContact.Text == string.Empty || DdlProgrammer.SelectedIndex == 0)
                {

                    if (txtMemberID.Text == string.Empty)
                    {
                        txtMemberID.Focus();
                        //txtMemberID.Style.Add("border", "1px solid red ");
                    }                   
                    else if (txtContact.Text == string.Empty)
                    {
                        txtContact.Focus();
                        //txtContact.Style.Add("border", "1px solid red ");
                    }
                    else if (DdlProgrammer.SelectedIndex == 0)
                    {
                        DdlProgrammer.Focus();
                        //DdlProgrammer.Style.Add("border", "1px solid red ");
                    }                    

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Required Field !!!','Error');", true);
                }
                else
                {
                    int flag1 = chkMeasurementDateNotNextFollowupDate();
                    if (flag1 == 0)
                    {
                        AddParameter();
                        if (btnSave.Text == "Save")
                        {
                            objMsr.Action = "Insert";

                            int k = objMsr.Insert_Update_Delete();
                            if (k > 0)
                            {
                                Clear();
                                txtMemberID.Focus();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Saved !!!','Error');", true);
                            }
                        }
                        else
                        {
                            objMsr.Action = "Update";                           
                            objMsr.Measurement_AutoId = MeasurementAutoId;

                            int k = objMsr.Insert_Update_Delete();
                            if (k > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                                Clear();                                
                                Response.Redirect("MeasurementDetails.aspx");                                
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ----------------- Clear Function ------------------
        private void Clear()
        {
            
            DdlProgrammer.SelectedValue = "--Select--";
            AssignTodaysDate();
            txtMemberID.Text = "";
            txtFirst.Text = "";
            txtLast.Text = "";
            txtContact.Text = "";
            txtmail.Text = "";
            txtArms.Text = "";
            txtWight.Text = "";
            txtWaist.Text = "";
            txtThigh.Text = "";
            txtChest.Text = "";
            txtHeight.Text = "";
            txtFat.Text = "";
            txtBmass.Text = "";
            txtBMI.Text = "";
            txtDCI.Text = "";
            txtMAge.Text = "";
            txtWater.Text = "";
            txtVFat.Text = "";
            txtNeck.Text = "";
            txtUpperArms.Text = "";
            txtForArms.Text = "";
            txtSholder.Text = "";
            txtHips.Text = "";
            txtExpendedChest.Text = "";
            txtUpperAbdomen.Text = "";
            txtLowerAbdomen.Text = "";
            txtCalf.Text = "";
            txtMmass.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;
            btnSave.Text = "Save";
            //txtMemberID.Style.Remove("border");
            //txtMemberID.Style.Add("border", "1px solid silver ");
            //txtContact.Style.Add("border", "1px solid silver ");
            //DdlProgrammer.Style.Add("border", "1px solid silver ");
            txtMemberID.Focus();
        }
        #endregion


        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
        }

        //public int MsrmntID;        
        private void GetDataForEdit(int MsrmntID)
        {
            try
            {
                dt.Clear();
                btnSave.Text = "Update";
                AssignID();
                dt = objMsr.Get_Edit();
                if (dt.Rows.Count > 0)
                {
                    txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                    txtFirst.Text = dt.Rows[0]["FName"].ToString();
                    txtLast.Text = dt.Rows[0]["LName"].ToString();
                    txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                    txtmail.Text = dt.Rows[0]["Email"].ToString();
                    ddlGender.SelectedItem.Text = dt.Rows[0]["Gender"].ToString();
                   
                    txtArms.Text = dt.Rows[0]["Arms"].ToString();
                    txtChest.Text = dt.Rows[0]["Chest"].ToString();
                    txtHeight.Text = dt.Rows[0]["Height"].ToString();
                    txtThigh.Text = dt.Rows[0]["Thigh"].ToString();
                    txtWaist.Text = dt.Rows[0]["Waist"].ToString();
                    txtWight.Text = dt.Rows[0]["Weight"].ToString();

                    txtFat.Text = dt.Rows[0]["Fat"].ToString();
                    txtMmass.Text = dt.Rows[0]["Mmass"].ToString();
                    txtBmass.Text = dt.Rows[0]["Bmass"].ToString();
                    txtBMI.Text = dt.Rows[0]["BMI"].ToString();
                    txtDCI.Text = dt.Rows[0]["DCI"].ToString();
                    txtMAge.Text = dt.Rows[0]["Age"].ToString();
                    txtWater.Text = dt.Rows[0]["Water"].ToString();
                    txtVFat.Text = dt.Rows[0]["Vfat"].ToString();
                    txtNeck.Text = dt.Rows[0]["Neck"].ToString();
                    txtUpperArms.Text = dt.Rows[0]["UpperArms"].ToString();
                    txtForArms.Text = dt.Rows[0]["ForArms"].ToString();
                    txtSholder.Text = dt.Rows[0]["Shoulder"].ToString();
                    txtHips.Text = dt.Rows[0]["Hips"].ToString();
                    txtExpendedChest.Text = dt.Rows[0]["ChestExpanded"].ToString();
                    txtUpperAbdomen.Text = dt.Rows[0]["UpperAbdomen"].ToString();
                    txtLowerAbdomen.Text = dt.Rows[0]["LowerAbdomen"].ToString();
                    txtCalf.Text = dt.Rows[0]["Calf"].ToString();
                    //ddlExecutive.SelectedValue=dt.Rows[0][""].ToString();
                    ddlExecutive.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();
                    DdlProgrammer.SelectedValue = dt.Rows[0]["Programmer_AutoID"].ToString();
                    if (dt.Rows[0]["Date"].ToString() != "")
                    {
                        DateTime Exp_Date = Convert.ToDateTime(dt.Rows[0]["Date"].ToString());
                        DateTime Exp_Date1;
                        if (DateTime.TryParseExact(Exp_Date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Exp_Date1))
                        {
                            txtDate.Text = Exp_Date1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtDate.Text = "";

                    if (dt.Rows[0]["NextFollowupDate"].ToString() != "")
                    {
                        DateTime Exp_Date = Convert.ToDateTime(dt.Rows[0]["NextFollowupDate"].ToString());
                        DateTime Exp_Date1;
                        if (DateTime.TryParseExact(Exp_Date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Exp_Date1))
                        {
                            txtNxtFllwdte.Text = Exp_Date1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtDate.Text = "";
                   
                    ddlExecutive.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();

                }
                ViewState["flag"] = 1;
                //txtExpenseID.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }


    }
}