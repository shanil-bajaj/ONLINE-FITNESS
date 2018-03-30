using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;

namespace NDOnlineGym_2017
{
    public partial class AllMasters : System.Web.UI.Page
    {
        BalDesignationMaster objBalDesignationMaster = new BalDesignationMaster();
        BalDepartmentMaster objBalDepartmentMaster = new BalDepartmentMaster();
        BalEnquiryTypeMaster objBalEnquiryTypeMaster = new BalEnquiryTypeMaster();
        BalEnquiryForMaster objBalEnquiryForMaster = new BalEnquiryForMaster();
        BalOccupationMaster objBalOccupationMaster = new BalOccupationMaster();
        BalSourceOfEnquiryMaster objBalSourceOfEnquiryMaster = new BalSourceOfEnquiryMaster();
        BalWorkoutNameMaster objBalWorkoutNameMaster = new BalWorkoutNameMaster();
        BalMuscularGroupMaster objBalMuscularGroupMaster = new BalMuscularGroupMaster();
        BalDietAvoidMaster objBalDietAvoidMaster = new BalDietAvoidMaster();
        BalDietDeclarationMaster objBalDietDeclarationMaster = new BalDietDeclarationMaster();
        BalPaymentDetailsMaster objBalPaymentDetailMaster = new BalPaymentDetailsMaster();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtDesignation.Focus();
                    BindDesignationMasterGrid();
                    //BindDepartmentMasterGrid();
                    //BindEnquiryTypeMasterGrid();
                    //BindEnquiryForMasterGrid();
                    //BindOccupationMasterGrid();
                    //BindSourceOfEnquiryMasterGrid();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #region----------------------------Designation Master-----------------------------------

        public void BindDesignationMasterGrid()
        {
            try
            {
                objBalDesignationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalDesignationMaster.Select_DesignationMaster();

                if (dt.Rows.Count > 0)
                {
                    gvDesignation.Visible = true;
                    gvDesignation.DataSource = dt;
                    gvDesignation.DataBind();
                }
                else
                {
                    gvDesignation.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordDesig()
        {
            txtDesignation.Text = "";
            txtDesignation.Focus();
            btnSaveDesignation.Text = "Save";
        }

        protected void btnSaveDesignation_Click(object sender, EventArgs e)
        {
            try
            {
                objBalDesignationMaster.Name = txtDesignation.Text;
                objBalDesignationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveDesignation.Text == "Save")
                {
                    bool chkExistingDesignationName = false;
                    chkExistingDesignationName = objBalDesignationMaster.Check_ExistingNameDesignationMaster();
                    if (chkExistingDesignationName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDesignation.Focus();
                        return;
                    }
                    else
                    {
                        objBalDesignationMaster.Action = "Insert";
                        int res = objBalDesignationMaster.Insert_DesignationMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordDesig();
                            BindDesignationMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalDesignationMaster.Action = "Update";
                    objBalDesignationMaster.Desig_AutoID = Convert.ToInt32(ViewState["Desig_AutoID"]);
                    int res = objBalDesignationMaster.Insert_DesignationMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDesig();
                        BindDesignationMasterGrid();
                        btnSaveDesignation.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDesignation.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearDesignation_Click(object sender, EventArgs e)
        {
            ClearRecordDesig();
        }

        protected void btnDeleteDesig_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDesignationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalDesignationMaster.Action = "Delete";
                objBalDesignationMaster.Desig_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalDesignationMaster.Insert_DesignationMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindDesignationMasterGrid();
                    txtDesignation.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDesignation.PageIndex = e.NewPageIndex;
            BindDesignationMasterGrid();
        }

        protected void btnEditDesig_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDesignationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Desig_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveDesignation.Text = "Update";
                objBalDesignationMaster.Desig_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalDesignationMaster.SelectByID_DesignationMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDesignation.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Department Master-----------------------------------

        public void BindDepartmentMasterGrid()
        {
            try
            {
                objBalDepartmentMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                dt = objBalDepartmentMaster.Select_DepartmentMaster();

                if (dt.Rows.Count > 0)
                {
                    gvDepartment.Visible = true;
                    gvDepartment.DataSource = dt;
                    gvDepartment.DataBind();
                }
                else
                {
                    gvDepartment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordDept()
        {
            txtDepartment.Text = "";
            txtDepartment.Focus();
            btnSaveDepartment.Text = "Save";
        }

        protected void btnSaveDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                objBalDepartmentMaster.Name = txtDepartment.Text;
                objBalDepartmentMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveDepartment.Text == "Save")
                {
                    bool chkExistingDepartmentName = false;
                    chkExistingDepartmentName = objBalDepartmentMaster.Check_ExistingNameDepartmentMaster();
                    if (chkExistingDepartmentName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDepartment.Focus();
                        return;
                    }
                    else
                    {
                        objBalDepartmentMaster.Action = "Insert";
                        int res = objBalDepartmentMaster.Insert_DepartmentMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordDept();
                            BindDepartmentMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalDepartmentMaster.Action = "Update";
                    objBalDepartmentMaster.Dept_AutoID = Convert.ToInt32(ViewState["Dept_AutoID"]);
                    int res = objBalDepartmentMaster.Insert_DepartmentMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDept();
                        BindDepartmentMasterGrid();
                        btnSaveDepartment.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDepartment.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearDepartment_Click(object sender, EventArgs e)
        {
            ClearRecordDept();
        }

        protected void btnDeleteDept_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDepartmentMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                objBalDepartmentMaster.Action = "Delete";
                objBalDepartmentMaster.Dept_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalDepartmentMaster.Insert_DepartmentMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindDepartmentMasterGrid();
                    txtDepartment.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepartment.PageIndex = e.NewPageIndex;
            BindDepartmentMasterGrid();
        }

        protected void btnEditDept_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDepartmentMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Dept_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveDepartment.Text = "Update";
                objBalDepartmentMaster.Dept_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalDepartmentMaster.SelectByID_DepartmentMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDepartment.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Enquiry Type Master-----------------------------------

        public void BindEnquiryTypeMasterGrid()
        {
            try
            {
                objBalEnquiryTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalEnquiryTypeMaster.Select_EnquiryTypeMaster();

                if (dt.Rows.Count > 0)
                {
                    gvEnquiryType.Visible = true;
                    gvEnquiryType.DataSource = dt;
                    gvEnquiryType.DataBind();
                }
                else
                {
                    gvEnquiryType.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordEnqType()
        {
            txtEnquiryType.Text = "";
            txtEnquiryType.Focus();
            btnSaveEnquiryType.Text = "Save";
        }

        protected void btnSaveEnquiryType_Click(object sender, EventArgs e)
        {
            try
            {
                objBalEnquiryTypeMaster.Name = txtEnquiryType.Text;
                objBalEnquiryTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveEnquiryType.Text == "Save")
                {
                    bool chkExistingEnquiryTypeName = false;
                    chkExistingEnquiryTypeName = objBalEnquiryTypeMaster.Check_ExistingNameEnquiryTypeMaster();
                    if (chkExistingEnquiryTypeName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtEnquiryType.Focus();
                        return;
                    }
                    else
                    {
                        objBalEnquiryTypeMaster.Action = "Insert";
                        int res = objBalEnquiryTypeMaster.Insert_EnquiryTypeMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordEnqType();
                            BindEnquiryTypeMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalEnquiryTypeMaster.Action = "Update";
                    objBalEnquiryTypeMaster.EnqType_AutoID = Convert.ToInt32(ViewState["EnqType_AutoID"]);
                    int res = objBalEnquiryTypeMaster.Insert_EnquiryTypeMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordEnqType();
                        BindEnquiryTypeMasterGrid();
                        btnSaveEnquiryType.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtEnquiryType.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearEnquiryType_Click(object sender, EventArgs e)
        {
            ClearRecordEnqType();
        }

        protected void btnDeleteEnquiryType_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalEnquiryTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalEnquiryTypeMaster.Action = "Delete";
                objBalEnquiryTypeMaster.EnqType_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalEnquiryTypeMaster.Insert_EnquiryTypeMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindEnquiryTypeMasterGrid();
                    txtEnquiryType.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvEnquiryType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnquiryType.PageIndex = e.NewPageIndex;
            BindEnquiryTypeMasterGrid();
        }

        protected void btnEditEnquiryType_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalEnquiryTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["EnqType_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveEnquiryType.Text = "Update";
                objBalEnquiryTypeMaster.EnqType_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalEnquiryTypeMaster.SelectByID_EnquiryTypeMaster();
                if (dt.Rows.Count > 0)
                {
                    txtEnquiryType.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Enquiry For Master-----------------------------------

        public void BindEnquiryForMasterGrid()
        {
            try
            {
                objBalEnquiryForMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalEnquiryForMaster.Select_EnquiryForMaster();

                if (dt.Rows.Count > 0)
                {
                    gvEnquiryFor.Visible = true;
                    gvEnquiryFor.DataSource = dt;
                    gvEnquiryFor.DataBind();
                }
                else
                {
                    gvEnquiryFor.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordEnqFor()
        {
            txtEnquiryFor.Text = "";
            txtEnquiryFor.Focus();
            btnSaveEnquiryFor.Text = "Save";
        }

        protected void btnSaveEnquiryFor_Click(object sender, EventArgs e)
        {
            try
            {
                objBalEnquiryForMaster.Name = txtEnquiryFor.Text;
                objBalEnquiryForMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveEnquiryFor.Text == "Save")
                {
                    bool chkExistingEnquiryForName = false;
                    chkExistingEnquiryForName = objBalEnquiryForMaster.Check_ExistingNameEnquiryForMaster();
                    if (chkExistingEnquiryForName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtEnquiryFor.Focus();
                        return;
                    }
                    else
                    {
                        objBalEnquiryForMaster.Action = "Insert";
                        int res = objBalEnquiryForMaster.Insert_EnquiryForMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordEnqFor();
                            BindEnquiryForMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalEnquiryForMaster.Action = "Update";
                    objBalEnquiryForMaster.EnqFor_AutoID = Convert.ToInt32(ViewState["EnqFor_AutoID"]);
                    int res = objBalEnquiryForMaster.Insert_EnquiryForMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordEnqFor();
                        BindEnquiryForMasterGrid();
                        btnSaveEnquiryFor.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtEnquiryFor.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearEnquiryFor_Click(object sender, EventArgs e)
        {
            ClearRecordEnqFor();
        }

        protected void btnDeleteEnquiryFor_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalEnquiryForMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalEnquiryForMaster.Action = "Delete";
                objBalEnquiryForMaster.EnqFor_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalEnquiryForMaster.Insert_EnquiryForMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindEnquiryForMasterGrid();
                    txtEnquiryFor.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvEnquiryFor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnquiryFor.PageIndex = e.NewPageIndex;
            BindEnquiryForMasterGrid();
        }

        protected void btnEditEnquiryFor_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalEnquiryForMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["EnqFor_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveEnquiryFor.Text = "Update";
                objBalEnquiryForMaster.EnqFor_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalEnquiryForMaster.SelectByID_EnquiryForMaster();
                if (dt.Rows.Count > 0)
                {
                    txtEnquiryFor.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Occupation Master-----------------------------------

        public void BindOccupationMasterGrid()
        {
            try
            {
                objBalOccupationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalOccupationMaster.Select_OccupationMaster();

                if (dt.Rows.Count > 0)
                {
                    gvOccupation.Visible = true;
                    gvOccupation.DataSource = dt;
                    gvOccupation.DataBind();
                }
                else
                {
                    gvOccupation.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordOccupation()
        {
            txtOccupation.Text = "";
            txtOccupation.Focus();
            btnSaveOccupation.Text = "Save";
        }

        protected void btnSaveOccupation_Click(object sender, EventArgs e)
        {
            try
            {
                objBalOccupationMaster.Name = txtOccupation.Text;
                objBalOccupationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveOccupation.Text == "Save")
                {
                    bool chkExistingOccupationName = false;
                    chkExistingOccupationName = objBalOccupationMaster.Check_ExistingNameOccupationMaster();
                    if (chkExistingOccupationName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtOccupation.Focus();
                        return;
                    }
                    else
                    {
                        objBalOccupationMaster.Action = "Insert";
                        int res = objBalOccupationMaster.Insert_OccupationMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordOccupation();
                            BindOccupationMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalOccupationMaster.Action = "Update";
                    objBalOccupationMaster.Occupation_AutoID = Convert.ToInt32(ViewState["Occupation_AutoID"]);
                    int res = objBalOccupationMaster.Insert_OccupationMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDesig();
                        BindOccupationMasterGrid();
                        btnSaveOccupation.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtOccupation.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearOccupation_Click(object sender, EventArgs e)
        {
            ClearRecordDesig();
        }

        protected void btnDeleteOccupation_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalOccupationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalOccupationMaster.Action = "Delete";
                objBalOccupationMaster.Occupation_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalOccupationMaster.Insert_OccupationMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindOccupationMasterGrid();
                    txtOccupation.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvOccupation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOccupation.PageIndex = e.NewPageIndex;
            BindOccupationMasterGrid();
        }

        protected void btnEditOccupation_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalOccupationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Occupation_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveOccupation.Text = "Update";
                objBalOccupationMaster.Occupation_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalOccupationMaster.SelectByID_OccupationMaster();
                if (dt.Rows.Count > 0)
                {
                    txtOccupation.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------SourceOfEnquiry Master-----------------------------------

        public void BindSourceOfEnquiryMasterGrid()
        {
            try
            {
                objBalSourceOfEnquiryMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalSourceOfEnquiryMaster.Select_SourceOfEnquiryMaster();

                if (dt.Rows.Count > 0)
                {
                    gvSourceOfEnquiry.Visible = true;
                    gvSourceOfEnquiry.DataSource = dt;
                    gvSourceOfEnquiry.DataBind();
                }
                else
                {
                    gvSourceOfEnquiry.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordSourceOfEnq()
        {
            txtSourceOfEnquiry.Text = "";
            txtSourceOfEnquiry.Focus();
            btnSaveSourceOfEnquiry.Text = "Save";
        }

        protected void btnSaveSourceOfEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                objBalSourceOfEnquiryMaster.Name = txtSourceOfEnquiry.Text;
                objBalSourceOfEnquiryMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveSourceOfEnquiry.Text == "Save")
                {
                    bool chkExistingSourceOfEnquiryName = false;
                    chkExistingSourceOfEnquiryName = objBalSourceOfEnquiryMaster.Check_ExistingNameSourceOfEnquiryMaster();
                    if (chkExistingSourceOfEnquiryName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtSourceOfEnquiry.Focus();
                        return;
                    }
                    else
                    {
                        objBalSourceOfEnquiryMaster.Action = "Insert";
                        int res = objBalSourceOfEnquiryMaster.Insert_SourceOfEnquiryMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordSourceOfEnq();
                            BindSourceOfEnquiryMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalSourceOfEnquiryMaster.Action = "Update";
                    objBalSourceOfEnquiryMaster.SourceOfEnq_AutoID = Convert.ToInt32(ViewState["SourceOfEnq_AutoID"]);
                    int res = objBalSourceOfEnquiryMaster.Insert_SourceOfEnquiryMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordSourceOfEnq();
                        BindSourceOfEnquiryMasterGrid();
                        btnSaveSourceOfEnquiry.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtSourceOfEnquiry.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearSourceOfEnquiry_Click(object sender, EventArgs e)
        {
            ClearRecordSourceOfEnq();
        }

        protected void btnDeleteSourceOfEnquiry_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalSourceOfEnquiryMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalSourceOfEnquiryMaster.Action = "Delete";
                objBalSourceOfEnquiryMaster.SourceOfEnq_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalSourceOfEnquiryMaster.Insert_SourceOfEnquiryMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindSourceOfEnquiryMasterGrid();
                    txtSourceOfEnquiry.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvSourceOfEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSourceOfEnquiry.PageIndex = e.NewPageIndex;
            BindSourceOfEnquiryMasterGrid();
        }

        protected void btnEditSourceOfEnquiry_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalSourceOfEnquiryMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["SourceOfEnq_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveSourceOfEnquiry.Text = "Update";
                objBalSourceOfEnquiryMaster.SourceOfEnq_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalSourceOfEnquiryMaster.SelectByID_SourceOfEnquiryMaster();
                if (dt.Rows.Count > 0)
                {
                    txtSourceOfEnquiry.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------WorkoutName Master-----------------------------------

        public void BindWorkoutNameMasterGrid()
        {
            try
            {
                objBalWorkoutNameMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalWorkoutNameMaster.Select_WorkoutNameMaster();

                if (dt.Rows.Count > 0)
                {
                    gvWorkoutName.Visible = true;
                    gvWorkoutName.DataSource = dt;
                    gvWorkoutName.DataBind();
                }
                else
                {
                    gvWorkoutName.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordWorkoutName()
        {
            txtWorkoutName.Text = "";
            txtWorkoutName.Focus();
            btnSaveWorkoutName.Text = "Save";
        }

        protected void btnSaveWorkoutName_Click(object sender, EventArgs e)
        {
            try
            {
                objBalWorkoutNameMaster.Name = txtWorkoutName.Text;
                objBalWorkoutNameMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveWorkoutName.Text == "Save")
                {
                    bool chkExistingWorkoutNameName = false;
                    chkExistingWorkoutNameName = objBalWorkoutNameMaster.Check_ExistingNameWorkoutNameMaster();
                    if (chkExistingWorkoutNameName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtWorkoutName.Focus();
                        return;
                    }
                    else
                    {
                        objBalWorkoutNameMaster.Action = "Insert";
                        int res = objBalWorkoutNameMaster.Insert_WorkoutNameMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordWorkoutName();
                            BindWorkoutNameMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalWorkoutNameMaster.Action = "Update";
                    objBalWorkoutNameMaster.WorkoutName_AutoID = Convert.ToInt32(ViewState["Desig_AutoID"]);
                    int res = objBalWorkoutNameMaster.Insert_WorkoutNameMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordWorkoutName();
                        BindWorkoutNameMasterGrid();
                        btnSaveWorkoutName.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtWorkoutName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearWorkoutName_Click(object sender, EventArgs e)
        {
            ClearRecordWorkoutName();
        }

        protected void btnDeleteWorkoutName_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalWorkoutNameMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalWorkoutNameMaster.Action = "Delete";
                objBalWorkoutNameMaster.WorkoutName_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalWorkoutNameMaster.Insert_WorkoutNameMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindWorkoutNameMasterGrid();
                    txtWorkoutName.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvWorkoutName_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorkoutName.PageIndex = e.NewPageIndex;
            BindWorkoutNameMasterGrid();
        }

        protected void btnEditWorkoutName_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalWorkoutNameMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Desig_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveWorkoutName.Text = "Update";
                objBalWorkoutNameMaster.WorkoutName_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalWorkoutNameMaster.SelectByID_WorkoutNameMaster();
                if (dt.Rows.Count > 0)
                {
                    txtWorkoutName.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------MuscularGroup Master-----------------------------------

        public void BindMuscularGroupMasterGrid()
        {
            try
            {
                objBalMuscularGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalMuscularGroupMaster.Select_MuscularGroupMaster();

                if (dt.Rows.Count > 0)
                {
                    gvMuscularGroup.Visible = true;
                    gvMuscularGroup.DataSource = dt;
                    gvMuscularGroup.DataBind();
                }
                else
                {
                    gvMuscularGroup.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordMuscularGroup()
        {
            txtMuscularGroup.Text = "";
            txtMuscularGroup.Focus();
            btnSaveMuscularGroup.Text = "Save";
        }

        protected void btnSaveMuscularGroup_Click(object sender, EventArgs e)
        {
            try
            {
                objBalMuscularGroupMaster.Name = txtMuscularGroup.Text;
                objBalMuscularGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveMuscularGroup.Text == "Save")
                {
                    bool chkExistingMuscularGroupName = false;
                    chkExistingMuscularGroupName = objBalMuscularGroupMaster.Check_ExistingNameMuscularGroupMaster();
                    if (chkExistingMuscularGroupName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtMuscularGroup.Focus();
                        return;
                    }
                    else
                    {
                        objBalMuscularGroupMaster.Action = "Insert";
                        int res = objBalMuscularGroupMaster.Insert_MuscularGroupMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordMuscularGroup();
                            BindMuscularGroupMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalMuscularGroupMaster.Action = "Update";
                    objBalMuscularGroupMaster.MuscularGroup_AutoID = Convert.ToInt32(ViewState["MuscularGroup_AutoID"]);
                    int res = objBalMuscularGroupMaster.Insert_MuscularGroupMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordMuscularGroup();
                        BindMuscularGroupMasterGrid();
                        btnSaveMuscularGroup.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtMuscularGroup.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearMuscularGroup_Click(object sender, EventArgs e)
        {
            ClearRecordMuscularGroup();
        }

        protected void btnDeleteMuscularGroup_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalMuscularGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalMuscularGroupMaster.Action = "Delete";
                objBalMuscularGroupMaster.MuscularGroup_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalMuscularGroupMaster.Insert_MuscularGroupMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindMuscularGroupMasterGrid();
                    txtMuscularGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvMuscularGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMuscularGroup.PageIndex = e.NewPageIndex;
            BindMuscularGroupMasterGrid();
        }

        protected void btnEditMuscularGroup_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalMuscularGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["MuscularGroup_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveMuscularGroup.Text = "Update";
                objBalMuscularGroupMaster.MuscularGroup_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalMuscularGroupMaster.SelectByID_MuscularGroupMaster();
                if (dt.Rows.Count > 0)
                {
                    txtMuscularGroup.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------DietAvoid Master-----------------------------------

        public void BindDietAvoidMasterGrid()
        {
            try
            {
                objBalDietAvoidMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietAvoidMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalDietAvoidMaster.Select_DietAvoidMaster();

                if (dt.Rows.Count > 0)
                {
                    gvDietAvoid.Visible = true;
                    gvDietAvoid.DataSource = dt;
                    gvDietAvoid.DataBind();
                }
                else
                {
                    gvDietAvoid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordDietAvoid()
        {
            txtDietAvoid.Text = "";
            txtDietAvoid.Focus();
            btnSaveDietAvoid.Text = "Save";
        }

        protected void btnSaveDietAvoid_Click(object sender, EventArgs e)
        {
            try
            {
                objBalDietAvoidMaster.Name = txtDietAvoid.Text;
                objBalDietAvoidMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietAvoidMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveDietAvoid.Text == "Save")
                {
                    bool chkExistingDietAvoidName = false;
                    chkExistingDietAvoidName = objBalDietAvoidMaster.Check_ExistingNameDietAvoidMaster();
                    if (chkExistingDietAvoidName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDietAvoid.Focus();
                        return;
                    }
                    else
                    {
                        objBalDietAvoidMaster.Action = "Insert";
                        int res = objBalDietAvoidMaster.Insert_DietAvoidMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordDietAvoid();
                            BindDietAvoidMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalDietAvoidMaster.Action = "Update";
                    objBalDietAvoidMaster.DietAvoid_AutoID = Convert.ToInt32(ViewState["DietAvoid_AutoID"]);
                    int res = objBalDietAvoidMaster.Insert_DietAvoidMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDietAvoid();
                        BindDietAvoidMasterGrid();
                        btnSaveDietAvoid.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDietAvoid.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearDietAvoid_Click(object sender, EventArgs e)
        {
            ClearRecordDietAvoid();
        }

        protected void btnDeleteDietAvoid_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDietAvoidMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietAvoidMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalDietAvoidMaster.Action = "Delete";
                objBalDietAvoidMaster.DietAvoid_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalDietAvoidMaster.Insert_DietAvoidMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindDietAvoidMasterGrid();
                    txtDietAvoid.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvDietAvoid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDietAvoid.PageIndex = e.NewPageIndex;
            BindDietAvoidMasterGrid();
        }

        protected void btnEditDietAvoid_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDietAvoidMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietAvoidMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["DietAvoid_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveDietAvoid.Text = "Update";
                objBalDietAvoidMaster.DietAvoid_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalDietAvoidMaster.SelectByID_DietAvoidMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDietAvoid.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------DietDeclaration Master-----------------------------------

        public void BindDietDeclarationMasterGrid()
        {
            try
            {
                objBalDietDeclarationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietDeclarationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalDietDeclarationMaster.Select_DietDeclarationMaster();

                if (dt.Rows.Count > 0)
                {
                    gvDietDeclaration.Visible = true;
                    gvDietDeclaration.DataSource = dt;
                    gvDietDeclaration.DataBind();
                }
                else
                {
                    gvDietDeclaration.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordDietDeclaration()
        {
            txtDietDeclaration.Text = "";
            txtDietDeclaration.Focus();
            btnSaveDietDeclaration.Text = "Save";
        }

        protected void btnSaveDietDeclaration_Click(object sender, EventArgs e)
        {
            try
            {
                objBalDietDeclarationMaster.Name = txtDietDeclaration.Text;
                objBalDietDeclarationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietDeclarationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveDietDeclaration.Text == "Save")
                {
                    bool chkExistingDietDeclarationName = false;
                    chkExistingDietDeclarationName = objBalDietDeclarationMaster.Check_ExistingNameDietDeclarationMaster();
                    if (chkExistingDietDeclarationName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDietDeclaration.Focus();
                        return;
                    }
                    else
                    {
                        objBalDietDeclarationMaster.Action = "Insert";
                        int res = objBalDietDeclarationMaster.Insert_DietDeclarationMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordDietDeclaration();
                            BindDietDeclarationMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalDietDeclarationMaster.Action = "Update";
                    objBalDietDeclarationMaster.DietDeclaration_AutoID = Convert.ToInt32(ViewState["DietDeclaration_AutoID"]);
                    int res = objBalDietDeclarationMaster.Insert_DietDeclarationMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDietDeclaration();
                        BindDietDeclarationMasterGrid();
                        btnSaveDietDeclaration.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDietDeclaration.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearDietDeclaration_Click(object sender, EventArgs e)
        {
            ClearRecordDietDeclaration();
        }

        protected void btnDeleteDietDeclaration_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDietDeclarationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietDeclarationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalDietDeclarationMaster.Action = "Delete";
                objBalDietDeclarationMaster.DietDeclaration_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalDietDeclarationMaster.Insert_DietDeclarationMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindDietDeclarationMasterGrid();
                    txtDietDeclaration.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvDietDeclaration_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDietDeclaration.PageIndex = e.NewPageIndex;
            BindDietDeclarationMasterGrid();
        }

        protected void btnEditDietDeclaration_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDietDeclarationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietDeclarationMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["DietDeclaration_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveDietDeclaration.Text = "Update";
                objBalDietDeclarationMaster.DietDeclaration_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalDietDeclarationMaster.SelectByID_DietDeclarationMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDietDeclaration.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------PaymentDetail Master-----------------------------------

        public void BindPaymentDetailMasterGrid()
        {
            try
            {
                objBalPaymentDetailMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalPaymentDetailMaster.Select_PaymentDetailMaster();

                if (dt.Rows.Count > 0)
                {
                    gvPaymentDetail.Visible = true;
                    gvPaymentDetail.DataSource = dt;
                    gvPaymentDetail.DataBind();
                }
                else
                {
                    gvPaymentDetail.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordPaymentDetail()
        {
            txtPaymentDetail.Text = "";
            txtPaymentDetail.Focus();
            btnSavePaymentDetail.Text = "Save";
        }

        protected void btnSavePaymentDetail_Click(object sender, EventArgs e)
        {
            try
            {
                objBalPaymentDetailMaster.Name = txtPaymentDetail.Text;
                objBalPaymentDetailMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSavePaymentDetail.Text == "Save")
                {
                    bool chkExistingPaymentDetailName = false;
                    chkExistingPaymentDetailName = objBalPaymentDetailMaster.Check_ExistingNamePaymentDetailMaster();
                    if (chkExistingPaymentDetailName == true)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtPaymentDetail.Focus();
                        return;
                    }
                    else
                    {
                        objBalPaymentDetailMaster.Action = "Insert";
                        int res = objBalPaymentDetailMaster.Insert_PaymentDetailMaster();
                        if (res > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordPaymentDetail();
                            BindPaymentDetailMasterGrid();
                        }
                    }
                }
                else
                {
                    objBalPaymentDetailMaster.Action = "Update";
                    objBalPaymentDetailMaster.PaymentMode_AutoID = Convert.ToInt32(ViewState["PaymentMode_AutoID"]);
                    int res = objBalPaymentDetailMaster.Insert_PaymentDetailMaster();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordPaymentDetail();
                        BindPaymentDetailMasterGrid();
                        btnSavePaymentDetail.Text = "Save";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtPaymentDetail.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearPaymentDetail_Click(object sender, EventArgs e)
        {
            ClearRecordPaymentDetail();
        }

        protected void btnDeletePaymentDetail_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPaymentDetailMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalPaymentDetailMaster.Action = "Delete";
                objBalPaymentDetailMaster.PaymentMode_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalPaymentDetailMaster.Insert_PaymentDetailMaster();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindPaymentDetailMasterGrid();
                    txtPaymentDetail.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvPaymentDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPaymentDetail.PageIndex = e.NewPageIndex;
            BindPaymentDetailMasterGrid();
        }

        protected void btnEditPaymentDetail_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPaymentDetailMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["PaymentMode_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSavePaymentDetail.Text = "Update";
                objBalPaymentDetailMaster.PaymentMode_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalPaymentDetailMaster.SelectByID_PaymentDetailMaster();
                if (dt.Rows.Count > 0)
                {
                    txtPaymentDetail.Text = dt.Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion
    }
}