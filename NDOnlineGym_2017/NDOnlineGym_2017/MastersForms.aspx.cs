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
    public partial class MastersForms : System.Web.UI.Page
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
        BalExpenseGroupMaster objBalExpenseGroupMaster = new BalExpenseGroupMaster();
        BalProgrammerMaster objBalProgrammerMaster = new BalProgrammerMaster();
        BalFollowupTypeMaster objBalFollowupTypeMaster = new BalFollowupTypeMaster();
        BalTaxMaster objBalTaxMaster = new BalTaxMaster();
        BalMemberUpgradeMaster objBalMemberUpgradeMaster = new BalMemberUpgradeMaster();
        BalCallRespondMaster objBalCallRespondMaster = new BalCallRespondMaster();
        BalShiftMaster objBalShiftMaster = new BalShiftMaster();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtDesignation.Focus();
                    BindDesignationMasterGrid();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

       

        protected void btnDesignation_Click(object sender, EventArgs e)
        {
            txtDesignation.Focus();
            MultiView1.ActiveViewIndex = 0;
            btnDesignation.CssClass = "btn-menu btn-menu-selected";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            ClearRecordDesig();
        }

        protected void btnDepartment_Click(object sender, EventArgs e)
        {
            txtDepartment.Focus();
            MultiView1.ActiveViewIndex = 1;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu btn-menu-selected";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            BindDepartmentMasterGrid();
            ClearRecordDept();
        }

        protected void btnEnquiryType_Click(object sender, EventArgs e)
        {
            txtEnquiryType.Focus();
            MultiView1.ActiveViewIndex = 2;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu btn-menu-selected";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindEnquiryTypeMasterGrid();
            ClearRecordEnqType();
        }

        protected void btnEnquiryFor_Click(object sender, EventArgs e)
        {
            txtEnquiryFor.Focus();
            MultiView1.ActiveViewIndex = 3;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu btn-menu-selected";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindEnquiryForMasterGrid();
            ClearRecordEnqFor();
        }

        protected void btnOcccupation_Click(object sender, EventArgs e)
        {
            txtOccupation.Focus();
            MultiView1.ActiveViewIndex = 4;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu btn-menu-selected";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindOccupationMasterGrid();
            ClearRecordOccupation();
        }

        protected void btnShift_Click(object sender, EventArgs e)
        {
            txtShift.Focus();
            MultiView1.ActiveViewIndex = 5;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu btn-menu-selected";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            BindShiftMasterGrid();
            ClearRecordShift();
        }

        protected void btnSourceOfEnquiry_Click(object sender, EventArgs e)
        {
            txtSourceOfEnquiry.Focus();
            MultiView1.ActiveViewIndex = 6;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu btn-menu-selected";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindSourceOfEnquiryMasterGrid();
            ClearRecordSourceOfEnq();
        }

        protected void btnMuscularGroup_Click(object sender, EventArgs e)
        {
            txtMuscularGroup.Focus();
            MultiView1.ActiveViewIndex = 7;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu btn-menu-selected";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindMuscularGroupMasterGrid();
            ClearRecordMuscularGroup();
        }

        protected void btnWorkoutName_Click(object sender, EventArgs e)
        {
            bindDDLMuscularGroup();
            txtWorkoutName.Focus();
            MultiView1.ActiveViewIndex = 8;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu btn-menu-selected";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindWorkoutNameMasterGrid();
            ClearRecordWorkoutName();
        }

        protected void btnDietAvoid_Click(object sender, EventArgs e)
        {
            txtDietAvoid.Focus();
            MultiView1.ActiveViewIndex = 9;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu btn-menu-selected";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";

            objBalDietAvoidMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objBalDietAvoidMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            GetDietAvoidMasterDetails();
            if (txtDietAvoid.Text == "")
                btnSaveDietAvoid.Text = "Save";
            else
            {
                btnSaveDietAvoid.Text = "Edit";
                btnSaveDietAvoid.Focus();
            }
        }

        protected void btnDietDeclaration_Click(object sender, EventArgs e)
        {
            txtDietDeclaration.Focus();
            MultiView1.ActiveViewIndex = 10;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu btn-menu-selected";
            btnExpenseGroup.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";

            objBalDietDeclarationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objBalDietDeclarationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            GetDietDeclarationMasterDetails();
            if (txtDietDeclaration.Text == "")
                btnSaveDietDeclaration.Text = "Save";
            else
            {
                btnSaveDietDeclaration.Text = "Edit";
                btnSaveDietDeclaration.Focus();
            }
        }

        protected void btnPaymentDetails_Click(object sender, EventArgs e)
        {
            txtPaymentDetail.Focus();
            MultiView1.ActiveViewIndex = 11;
            btnDesignation.CssClass = "btn-menu ";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu btn-menu-selected";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindPaymentDetailMasterGrid();
            ClearRecordPaymentDetail();
        }

        protected void btnExpenseGroup_Click(object sender, EventArgs e)
        {
            txtExpenseGroup.Focus();
            MultiView1.ActiveViewIndex = 12;
            btnDesignation.CssClass = "btn-menu";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu btn-menu-selected";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindExpenseGroupMasterGrid();
            ClearRecordExpenseGroup();
        }

        protected void btnProgrammer_Click(object sender, EventArgs e)
        {
            txtProgrammer.Focus();
            MultiView1.ActiveViewIndex = 13;
            btnDesignation.CssClass = "btn-menu";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu btn-menu-selected";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            BindProgrammerMasterGrid();
            ClearRecordProgrammer();
        }

        protected void btnFollowupType_Click(object sender, EventArgs e)
        {
            txtProgrammer.Focus();
            MultiView1.ActiveViewIndex = 14;
            btnDesignation.CssClass = "btn-menu";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu btn-menu-selected";
            btnCallRespond.CssClass = "btn-menu";
            BindFollowupTypeMasterGrid();
            ClearRecordFollowupType();
        }

        protected void btnTax_Click(object sender, EventArgs e)
        {
            txtTax.Focus();
            MultiView1.ActiveViewIndex = 15;
            btnDesignation.CssClass = "btn-menu";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu btn-menu-selected";
            btnCallRespond.CssClass = "btn-menu";

            objBalTaxMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objBalTaxMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            GetTaxMasterDetails();
            if (txtTax.Text == "")
                btnSaveTax1.Text = "Save";
            else
            {
                btnSaveTax1.Text = "Edit";
                btnSaveTax1.Focus();
            }
        }

        protected void btnCallRespond_Click(object sender, EventArgs e)
        {
            txtCallRespond.Focus();
            MultiView1.ActiveViewIndex = 16;
            btnDesignation.CssClass = "btn-menu";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu btn-menu-selected";
            BindCallRespondMasterGrid();
            ClearRecordCallRespond();
        }

        protected void btnUpgradeDays_Click(object sender, EventArgs e)
        {
            txtUpgradeDays.Focus();
            MultiView1.ActiveViewIndex = 17;
            btnDesignation.CssClass = "btn-menu";
            btnDepartment.CssClass = "btn-menu";
            btnEnquiryType.CssClass = "btn-menu";
            btnEnquiryFor.CssClass = "btn-menu";
            btnOcccupation.CssClass = "btn-menu";
            btnShift.CssClass = "btn-menu";
            btnSourceOfEnquiry.CssClass = "btn-menu";
            btnMuscularGroup.CssClass = "btn-menu";
            btnWorkoutName.CssClass = "btn-menu";
            btnDietAvoid.CssClass = "btn-menu";
            btnDietDeclaration.CssClass = "btn-menu";
            btnPaymentDetails.CssClass = "btn-menu";
            btnExpenseGroup.CssClass = "btn-menu";
            btnProgrammer.CssClass = "btn-menu";
            btnFollowupType.CssClass = "btn-menu";
            btnTax.CssClass = "btn-menu";
            btnCallRespond.CssClass = "btn-menu";
            btnUpgradeDays.CssClass = "btn-menu btn-menu-selected";

            objBalMemberUpgradeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objBalMemberUpgradeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            GetMemberUpgradeMasterDetails();
            if (txtUpgradeDays.Text == "")
                btnSavetxtUpgradeDays.Text = "Save";
            else
            {
                btnSavetxtUpgradeDays.Text = "Edit";
                btnSavetxtUpgradeDays.Focus();
            }
        }


        #region----------------------------Designation Master-----------------------------------

        public void BindDesignationMasterGrid()
        {
            try
            {
                objBalDesignationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvDesignation.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvDesignation.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvDesignation.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvDesignation.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvDesignation.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
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
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveDesignation.Text == "Save")
                {
                    bool chkExistingDesignationName = false;
                    chkExistingDesignationName = objBalDesignationMaster.Check_ExistingNameDesignationMaster();
                    if (chkExistingDesignationName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDesignation.Focus();
                        return;
                    }
                    else
                    {
                        objBalDesignationMaster.Action = "Insert";
                        int res = objBalDesignationMaster.Insert_DesignationMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordDesig();
                            BindDesignationMasterGrid();
                            txtDesignation.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDesig();
                        BindDesignationMasterGrid();
                        btnSaveDesignation.Text = "Save";
                        txtDesignation.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDesignation.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearDesignation_Click(object sender, EventArgs e)
        {
            ClearRecordDesig();
            btnClearDesignation.Focus();
        }

        protected void btnDeleteDesig_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDesignationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalDesignationMaster.Action = "Delete";
                objBalDesignationMaster.Desig_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalDesignationMaster.Insert_DesignationMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindDesignationMasterGrid();
                    txtDesignation.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalDesignationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Desig_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveDesignation.Text = "Update";
                objBalDesignationMaster.Desig_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalDesignationMaster.SelectByID_DesignationMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDesignation.Text = dt.Rows[0]["Name"].ToString();
                }
                txtDesignation.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Department Master-----------------------------------

        public void BindDepartmentMasterGrid()
        {
            try
            {
                objBalDepartmentMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

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
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvDepartment.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvDepartment.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvDepartment.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvDepartment.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvDepartment.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordDept()
        {
            txtDepartment.Text = "";
            txtDepartment.Focus();
            btnSaveDepartment.Text = "Save";
            txtDepartment.Focus();
        }

        protected void btnSaveDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                objBalDepartmentMaster.Name = txtDepartment.Text;
                objBalDepartmentMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveDepartment.Text == "Save")
                {
                    bool chkExistingDepartmentName = false;
                    chkExistingDepartmentName = objBalDepartmentMaster.Check_ExistingNameDepartmentMaster();
                    if (chkExistingDepartmentName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtDepartment.Focus();
                        return;
                    }
                    else
                    {
                        objBalDepartmentMaster.Action = "Insert";
                        int res = objBalDepartmentMaster.Insert_DepartmentMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordDept();
                            BindDepartmentMasterGrid();
                            txtDepartment.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordDept();
                        BindDepartmentMasterGrid();
                        btnSaveDepartment.Text = "Save";
                        txtDepartment.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtDepartment.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearDepartment_Click(object sender, EventArgs e)
        {
            ClearRecordDept();
            btnClearDepartment.Focus();
        }

        protected void btnDeleteDept_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalDepartmentMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                objBalDepartmentMaster.Action = "Delete";
                objBalDepartmentMaster.Dept_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalDepartmentMaster.Insert_DepartmentMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindDepartmentMasterGrid();
                    txtDepartment.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalDepartmentMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Dept_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveDepartment.Text = "Update";
                objBalDepartmentMaster.Dept_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalDepartmentMaster.SelectByID_DepartmentMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDepartment.Text = dt.Rows[0]["Name"].ToString();
                    txtDepartment.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Enquiry Type Master-----------------------------------

        public void BindEnquiryTypeMasterGrid()
        {
            try
            {
                objBalEnquiryTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvEnquiryType.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvEnquiryType.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvEnquiryType.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvEnquiryType.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvEnquiryType.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordEnqType()
        {
            txtEnquiryType.Text = "";
            txtEnquiryType.Focus();
            btnSaveEnquiryType.Text = "Save";
            txtEnquiryType.Focus();
        }

        protected void btnSaveEnquiryType_Click(object sender, EventArgs e)
        {
            try
            {
                objBalEnquiryTypeMaster.Name = txtEnquiryType.Text;
                objBalEnquiryTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveEnquiryType.Text == "Save")
                {
                    bool chkExistingEnquiryTypeName = false;
                    chkExistingEnquiryTypeName = objBalEnquiryTypeMaster.Check_ExistingNameEnquiryTypeMaster();
                    if (chkExistingEnquiryTypeName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtEnquiryType.Focus();
                        return;
                    }
                    else
                    {
                        objBalEnquiryTypeMaster.Action = "Insert";
                        int res = objBalEnquiryTypeMaster.Insert_EnquiryTypeMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordEnqType();
                            BindEnquiryTypeMasterGrid();
                            txtEnquiryType.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordEnqType();
                        BindEnquiryTypeMasterGrid();
                        btnSaveEnquiryType.Text = "Save";
                        txtEnquiryType.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtEnquiryType.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearEnquiryType_Click(object sender, EventArgs e)
        {
            ClearRecordEnqType();
            btnClearEnquiryType.Focus();
        }

        protected void btnDeleteEnquiryType_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalEnquiryTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalEnquiryTypeMaster.Action = "Delete";
                objBalEnquiryTypeMaster.EnqType_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalEnquiryTypeMaster.Insert_EnquiryTypeMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindEnquiryTypeMasterGrid();
                    txtEnquiryType.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalEnquiryTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["EnqType_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveEnquiryType.Text = "Update";
                objBalEnquiryTypeMaster.EnqType_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalEnquiryTypeMaster.SelectByID_EnquiryTypeMaster();
                if (dt.Rows.Count > 0)
                {
                    txtEnquiryType.Text = dt.Rows[0]["Name"].ToString(); 
                    txtEnquiryType.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Enquiry For Master-----------------------------------

        public void BindEnquiryForMasterGrid()
        {
            try
            {
                objBalEnquiryForMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvEnquiryFor.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvEnquiryFor.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvEnquiryFor.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvEnquiryFor.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvEnquiryFor.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordEnqFor()
        {
            txtEnquiryFor.Text = "";
            txtEnquiryFor.Focus();
            btnSaveEnquiryFor.Text = "Save";
            txtEnquiryFor.Focus();
        }

        protected void btnSaveEnquiryFor_Click(object sender, EventArgs e)
        {
            try
            {
                objBalEnquiryForMaster.Name = txtEnquiryFor.Text;
                objBalEnquiryForMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveEnquiryFor.Text == "Save")
                {
                    bool chkExistingEnquiryForName = false;
                    chkExistingEnquiryForName = objBalEnquiryForMaster.Check_ExistingNameEnquiryForMaster();
                    if (chkExistingEnquiryForName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtEnquiryFor.Focus();
                        return;
                    }
                    else
                    {
                        objBalEnquiryForMaster.Action = "Insert";
                        int res = objBalEnquiryForMaster.Insert_EnquiryForMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordEnqFor();
                            BindEnquiryForMasterGrid();
                            txtEnquiryFor.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordEnqFor();
                        BindEnquiryForMasterGrid();
                        btnSaveEnquiryFor.Text = "Save"; 
                        txtEnquiryFor.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtEnquiryFor.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearEnquiryFor_Click(object sender, EventArgs e)
        {
            ClearRecordEnqFor();
            btnClearEnquiryFor.Focus();
        }

        protected void btnDeleteEnquiryFor_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalEnquiryForMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalEnquiryForMaster.Action = "Delete";
                objBalEnquiryForMaster.EnqFor_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalEnquiryForMaster.Insert_EnquiryForMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindEnquiryForMasterGrid();
                    txtEnquiryFor.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalEnquiryForMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["EnqFor_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveEnquiryFor.Text = "Update";
                objBalEnquiryForMaster.EnqFor_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalEnquiryForMaster.SelectByID_EnquiryForMaster();
                if (dt.Rows.Count > 0)
                {
                    txtEnquiryFor.Text = dt.Rows[0]["Name"].ToString();
                    txtEnquiryFor.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Occupation Master-----------------------------------

        public void BindOccupationMasterGrid()
        {
            try
            {
                objBalOccupationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvOccupation.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvOccupation.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvOccupation.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvOccupation.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvOccupation.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordOccupation()
        {
            txtOccupation.Text = "";
            txtOccupation.Focus();
            btnSaveOccupation.Text = "Save";
            txtOccupation.Focus();
        }

        protected void btnSaveOccupation_Click(object sender, EventArgs e)
        {
            try
            {
                objBalOccupationMaster.Name = txtOccupation.Text;
                objBalOccupationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveOccupation.Text == "Save")
                {
                    bool chkExistingOccupationName = false;
                    chkExistingOccupationName = objBalOccupationMaster.Check_ExistingNameOccupationMaster();
                    if (chkExistingOccupationName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtOccupation.Focus();
                        return;
                    }
                    else
                    {
                        objBalOccupationMaster.Action = "Insert";
                        int res = objBalOccupationMaster.Insert_OccupationMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordOccupation();
                            BindOccupationMasterGrid();
                            txtOccupation.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordOccupation();
                        BindOccupationMasterGrid();
                        btnSaveOccupation.Text = "Save";
                        txtOccupation.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtOccupation.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearOccupation_Click(object sender, EventArgs e)
        {
            ClearRecordOccupation();
            btnClearOccupation.Focus();
        }

        protected void btnDeleteOccupation_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalOccupationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalOccupationMaster.Action = "Delete";
                objBalOccupationMaster.Occupation_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalOccupationMaster.Insert_OccupationMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindOccupationMasterGrid();
                    txtOccupation.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalOccupationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Occupation_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveOccupation.Text = "Update";
                objBalOccupationMaster.Occupation_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalOccupationMaster.SelectByID_OccupationMaster();
                if (dt.Rows.Count > 0)
                {
                    txtOccupation.Text = dt.Rows[0]["Name"].ToString(); 
                    txtOccupation.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Shift Master-----------------------------------

        public void BindShiftMasterGrid()
        {
            try
            {
                objBalShiftMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalShiftMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalShiftMaster.Select_ShiftMaster();

                if (dt.Rows.Count > 0)
                {
                    gvShift.Visible = true;
                    gvShift.DataSource = dt;
                    gvShift.DataBind();
                }
                else
                {
                    gvShift.Visible = false;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvShift.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvShift.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvShift.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvShift.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvShift.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordShift()
        {
            txtShift.Text = "";
            txtShift.Focus();
            btnSaveShift.Text = "Save";
        }

        protected void btnSaveShift_Click(object sender, EventArgs e)
        {
            try
            {
                objBalShiftMaster.Name = txtShift.Text;
                objBalShiftMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalShiftMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveShift.Text == "Save")
                {
                    bool chkExistingShiftName = false;
                    chkExistingShiftName = objBalShiftMaster.Check_ExistingNameShiftMaster();
                    if (chkExistingShiftName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtShift.Focus();
                        return;
                    }
                    else
                    {
                        objBalShiftMaster.Action = "Insert";
                        int res = objBalShiftMaster.Insert_ShiftMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordShift();
                            BindShiftMasterGrid();
                            txtShift.Focus();
                        }
                    }
                }
                else
                {
                    objBalShiftMaster.Action = "Update";
                    objBalShiftMaster.Shift_AutoID = Convert.ToInt32(ViewState["Shift_AutoID"]);
                    int res = objBalShiftMaster.Insert_ShiftMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordShift();
                        BindShiftMasterGrid();
                        btnSaveShift.Text = "Save";
                        txtShift.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtShift.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearShift_Click(object sender, EventArgs e)
        {
            ClearRecordShift();
            btnClearShift.Focus();
        }

        protected void btnDeleteShift_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalShiftMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalShiftMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalShiftMaster.Action = "Delete";
                objBalShiftMaster.Shift_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalShiftMaster.Insert_ShiftMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindShiftMasterGrid();
                    txtShift.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShift.PageIndex = e.NewPageIndex;
            BindShiftMasterGrid();
        }

        protected void btnEditShift_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalShiftMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalShiftMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Shift_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveShift.Text = "Update";
                objBalShiftMaster.Shift_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalShiftMaster.SelectByID_ShiftMaster();
                if (dt.Rows.Count > 0)
                {
                    txtShift.Text = dt.Rows[0]["Name"].ToString();
                }
                txtShift.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------SourceOfEnquiry Master-----------------------------------

        public void BindSourceOfEnquiryMasterGrid()
        {
            try
            {
                objBalSourceOfEnquiryMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvSourceOfEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvSourceOfEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvSourceOfEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvSourceOfEnquiry.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvSourceOfEnquiry.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordSourceOfEnq()
        {
            txtSourceOfEnquiry.Text = "";
            txtSourceOfEnquiry.Focus();
            btnSaveSourceOfEnquiry.Text = "Save";
            txtSourceOfEnquiry.Focus();
        }

        protected void btnSaveSourceOfEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                objBalSourceOfEnquiryMaster.Name = txtSourceOfEnquiry.Text;
                objBalSourceOfEnquiryMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveSourceOfEnquiry.Text == "Save")
                {
                    bool chkExistingSourceOfEnquiryName = false;
                    chkExistingSourceOfEnquiryName = objBalSourceOfEnquiryMaster.Check_ExistingNameSourceOfEnquiryMaster();
                    if (chkExistingSourceOfEnquiryName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtSourceOfEnquiry.Focus();
                        return;
                    }
                    else
                    {
                        objBalSourceOfEnquiryMaster.Action = "Insert";
                        int res = objBalSourceOfEnquiryMaster.Insert_SourceOfEnquiryMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordSourceOfEnq();
                            BindSourceOfEnquiryMasterGrid();
                            txtSourceOfEnquiry.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordSourceOfEnq();
                        BindSourceOfEnquiryMasterGrid();
                        btnSaveSourceOfEnquiry.Text = "Save";
                        txtSourceOfEnquiry.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtSourceOfEnquiry.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearSourceOfEnquiry_Click(object sender, EventArgs e)
        {
            ClearRecordSourceOfEnq();
            btnClearSourceOfEnquiry.Focus();
        }

        protected void btnDeleteSourceOfEnquiry_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalSourceOfEnquiryMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalSourceOfEnquiryMaster.Action = "Delete";
                objBalSourceOfEnquiryMaster.SourceOfEnq_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalSourceOfEnquiryMaster.Insert_SourceOfEnquiryMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindSourceOfEnquiryMasterGrid();
                    txtSourceOfEnquiry.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalSourceOfEnquiryMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["SourceOfEnq_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveSourceOfEnquiry.Text = "Update";
                objBalSourceOfEnquiryMaster.SourceOfEnq_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalSourceOfEnquiryMaster.SelectByID_SourceOfEnquiryMaster();
                if (dt.Rows.Count > 0)
                {
                    txtSourceOfEnquiry.Text = dt.Rows[0]["Name"].ToString();
                    txtSourceOfEnquiry.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------WorkoutName Master-----------------------------------

        public void bindDDLMuscularGroup()
        {
            try
            {
                objBalMuscularGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalMuscularGroupMaster.Select_MuscularGroupMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlMuscularGroup.DataSource = dt;
                    ddlMuscularGroup.Items.Clear();
                    ddlMuscularGroup.DataValueField = "MuscularGroup_AutoID";
                    ddlMuscularGroup.DataTextField = "Name";
                    ddlMuscularGroup.DataBind();
                    ddlMuscularGroup.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindWorkoutNameMasterGrid()
        {
            try
            {
                objBalWorkoutNameMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvWorkoutName.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvWorkoutName.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvWorkoutName.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvWorkoutName.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvWorkoutName.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordWorkoutName()
        {
            txtWorkoutName.Text = "";
            txtWorkoutName.Focus();
            ddlMuscularGroup.SelectedValue = "--Select--";
           // ddlMuscularGroup.SelectedItem.Text = "--Select--";
            btnSaveWorkoutName.Text = "Save";
            txtWorkoutName.Focus();
        }

        protected void btnSaveWorkoutName_Click(object sender, EventArgs e)
        {
            try
            {
                objBalWorkoutNameMaster.Name = txtWorkoutName.Text;
                objBalWorkoutNameMaster.MuscularGroup_AutoID = Convert.ToInt32(ddlMuscularGroup.SelectedItem.Value);
                objBalWorkoutNameMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveWorkoutName.Text == "Save")
                {
                    bool chkExistingWorkoutNameName = false;
                    chkExistingWorkoutNameName = objBalWorkoutNameMaster.Check_ExistingNameWorkoutNameMaster();
                    if (chkExistingWorkoutNameName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtWorkoutName.Focus();
                        return;
                    }
                    else
                    {
                        objBalWorkoutNameMaster.Action = "Insert";
                        int res = objBalWorkoutNameMaster.Insert_WorkoutNameMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordWorkoutName();
                            BindWorkoutNameMasterGrid();
                            txtWorkoutName.Focus();
                        }
                    }
                }
                else
                {
                    objBalWorkoutNameMaster.Action = "Update";
                    objBalWorkoutNameMaster.WorkoutName_AutoID = Convert.ToInt32(ViewState["Shift_AutoID"]);
                    int res = objBalWorkoutNameMaster.Insert_WorkoutNameMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordWorkoutName();
                        BindWorkoutNameMasterGrid();
                        btnSaveWorkoutName.Text = "Save";
                        txtWorkoutName.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtWorkoutName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearWorkoutName_Click(object sender, EventArgs e)
        {
            ClearRecordWorkoutName();
            btnClearWorkoutName.Focus();
        }

        protected void btnDeleteWorkoutName_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalWorkoutNameMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalWorkoutNameMaster.Action = "Delete";
                objBalWorkoutNameMaster.WorkoutName_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalWorkoutNameMaster.Insert_WorkoutNameMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindWorkoutNameMasterGrid();
                    txtWorkoutName.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalWorkoutNameMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Shift_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveWorkoutName.Text = "Update";
                objBalWorkoutNameMaster.WorkoutName_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalWorkoutNameMaster.SelectByID_WorkoutNameMaster();
                if (dt.Rows.Count > 0)
                {
                    txtWorkoutName.Text = dt.Rows[0]["Name"].ToString();
                    ddlMuscularGroup.SelectedValue = dt.Rows[0]["MuscularGroup_AutoID"].ToString();
                    ddlMuscularGroup.SelectedItem.Text = dt.Rows[0]["MuscularGroup"].ToString();
                    txtWorkoutName.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------MuscularGroup Master-----------------------------------

        public void BindMuscularGroupMasterGrid()
        {
            try
            {
                objBalMuscularGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvMuscularGroup.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvMuscularGroup.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvMuscularGroup.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvMuscularGroup.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvMuscularGroup.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordMuscularGroup()
        {
            txtMuscularGroup.Text = "";
            txtMuscularGroup.Focus();
            btnSaveMuscularGroup.Text = "Save";
            txtMuscularGroup.Focus();
        }

        protected void btnSaveMuscularGroup_Click(object sender, EventArgs e)
        {
            try
            {
                objBalMuscularGroupMaster.Name = txtMuscularGroup.Text;
                objBalMuscularGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveMuscularGroup.Text == "Save")
                {
                    bool chkExistingMuscularGroupName = false;
                    chkExistingMuscularGroupName = objBalMuscularGroupMaster.Check_ExistingNameMuscularGroupMaster();
                    if (chkExistingMuscularGroupName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtMuscularGroup.Focus();
                        return;
                    }
                    else
                    {
                        objBalMuscularGroupMaster.Action = "Insert";
                        int res = objBalMuscularGroupMaster.Insert_MuscularGroupMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordMuscularGroup();
                            BindMuscularGroupMasterGrid();
                            txtMuscularGroup.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordMuscularGroup();
                        BindMuscularGroupMasterGrid();
                        btnSaveMuscularGroup.Text = "Save";
                        txtMuscularGroup.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtMuscularGroup.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
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
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalMuscularGroupMaster.Action = "Delete";
                objBalMuscularGroupMaster.MuscularGroup_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalMuscularGroupMaster.Insert_MuscularGroupMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindMuscularGroupMasterGrid();
                    txtMuscularGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalMuscularGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["MuscularGroup_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveMuscularGroup.Text = "Update";
                objBalMuscularGroupMaster.MuscularGroup_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalMuscularGroupMaster.SelectByID_MuscularGroupMaster();
                if (dt.Rows.Count > 0)
                {
                    txtMuscularGroup.Text = dt.Rows[0]["Name"].ToString();
                    txtMuscularGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------DietAvoid Master-----------------------------------

        public void GetDietAvoidMasterDetails()
        {
            try
            {
                objBalDietAvoidMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietAvoidMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalDietAvoidMaster.Select_DietAvoidMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDietAvoid.Enabled = false;
                    txtDietAvoid.Text = dt.Rows[0]["Name"].ToString();
                    btnSaveDietAvoid.Text = "Edit";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        
        protected void btnSaveDietAvoid_Click(object sender, EventArgs e)
        {
            try
            {
                objBalDietAvoidMaster.Name = txtDietAvoid.Text;
                objBalDietAvoidMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietAvoidMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                if (btnSaveDietAvoid.Text == "Save")
                {
                    objBalDietAvoidMaster.Action = "Insert";
                    int res = objBalDietAvoidMaster.Insert_DietAvoidMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        txtDietAvoid.Enabled = false;
                        btnSaveDietAvoid.Text = "Edit";
                    }
                }
                else if (btnSaveDietAvoid.Text == "Update")
                {
                    objBalDietAvoidMaster.Action = "Insert";
                    int res = objBalDietAvoidMaster.Insert_DietAvoidMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        txtDietAvoid.Enabled = false;
                        btnSaveDietAvoid.Text = "Edit";
                        GetDietAvoidMasterDetails();
                    }
                }
                else
                {
                    txtDietAvoid.Enabled = true;
                    btnSaveDietAvoid.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------DietDeclaration Master-----------------------------------
        public void GetDietDeclarationMasterDetails()
        {
            try
            {
                objBalDietDeclarationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietDeclarationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalDietDeclarationMaster.Select_DietDeclarationMaster();
                if (dt.Rows.Count > 0)
                {
                    txtDietDeclaration.Enabled = false;
                    txtDietDeclaration.Text = dt.Rows[0]["Name"].ToString();
                    btnSaveDietDeclaration.Text = "Edit";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
       
        protected void btnSaveDietDeclaration_Click(object sender, EventArgs e)
        {

            try
            {
                objBalDietDeclarationMaster.Name = txtDietDeclaration.Text;
                objBalDietDeclarationMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalDietDeclarationMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                if (btnSaveDietDeclaration.Text == "Save")
                {
                    objBalDietDeclarationMaster.Action = "Insert";
                    int res = objBalDietDeclarationMaster.Insert_DietDeclarationMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        txtDietDeclaration.Enabled = false;
                        btnSaveDietDeclaration.Text = "Edit";
                    }
                }
                else if (btnSaveDietDeclaration.Text == "Update")
                {
                    objBalDietDeclarationMaster.Action = "Insert";
                    int res = objBalDietDeclarationMaster.Insert_DietDeclarationMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        txtDietDeclaration.Enabled = false;
                        btnSaveDietDeclaration.Text = "Edit";
                        GetDietDeclarationMasterDetails();
                    }
                }
                else
                {
                    txtDietDeclaration.Enabled = true;
                    txtDietDeclaration.Focus();
                    btnSaveDietDeclaration.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
            
        }

        #endregion

        #region----------------------------PaymentDetail Master-----------------------------------

        public void BindPaymentDetailMasterGrid()
        {
            try
            {
                objBalPaymentDetailMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
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
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvPaymentDetail.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvPaymentDetail.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvPaymentDetail.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvPaymentDetail.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvPaymentDetail.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordPaymentDetail()
        {
            txtPaymentDetail.Text = "";
            txtPaymentDetail.Focus();
            btnSavePaymentDetail.Text = "Save";
            txtPaymentDetail.Focus();
        }

        protected void btnSavePaymentDetail_Click(object sender, EventArgs e)
        {
            try
            {
                objBalPaymentDetailMaster.Name = txtPaymentDetail.Text;
                objBalPaymentDetailMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSavePaymentDetail.Text == "Save")
                {
                    bool chkExistingPaymentDetailName = false;
                    chkExistingPaymentDetailName = objBalPaymentDetailMaster.Check_ExistingNamePaymentDetailMaster();
                    if (chkExistingPaymentDetailName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtPaymentDetail.Focus();
                        return;
                    }
                    else
                    {
                        objBalPaymentDetailMaster.Action = "Insert";
                        int res = objBalPaymentDetailMaster.Insert_PaymentDetailMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordPaymentDetail();
                            BindPaymentDetailMasterGrid();
                            txtPaymentDetail.Focus();
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordPaymentDetail();
                        BindPaymentDetailMasterGrid();
                        btnSavePaymentDetail.Text = "Save";
                        txtPaymentDetail.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtPaymentDetail.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearPaymentDetail_Click(object sender, EventArgs e)
        {
            ClearRecordPaymentDetail();
            btnClearPaymentDetail.Focus();
        }

        protected void btnDeletePaymentDetail_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalPaymentDetailMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalPaymentDetailMaster.Action = "Delete";
                objBalPaymentDetailMaster.PaymentMode_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalPaymentDetailMaster.Insert_PaymentDetailMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindPaymentDetailMasterGrid();
                    txtPaymentDetail.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
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
                objBalPaymentDetailMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["PaymentMode_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSavePaymentDetail.Text = "Update";
                objBalPaymentDetailMaster.PaymentMode_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalPaymentDetailMaster.SelectByID_PaymentDetailMaster();
                if (dt.Rows.Count > 0)
                {
                    txtPaymentDetail.Text = dt.Rows[0]["Name"].ToString();
                    txtPaymentDetail.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------ExpenseGroup Master-----------------------------------

        public void BindExpenseGroupMasterGrid()
        {
            try
            {
                objBalExpenseGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalExpenseGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalExpenseGroupMaster.Select_ExpenseGroupMaster();

                if (dt.Rows.Count > 0)
                {
                    gvExpenseGroup.Visible = true;
                    gvExpenseGroup.DataSource = dt;
                    gvExpenseGroup.DataBind();
                }
                else
                {
                    gvExpenseGroup.Visible = false;
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvExpenseGroup.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvExpenseGroup.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvExpenseGroup.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvExpenseGroup.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvExpenseGroup.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordExpenseGroup()
        {
            txtExpenseGroup.Text = "";
            txtExpenseGroup.Focus();
            btnSaveExpenseGroup.Text = "Save";
            txtExpenseGroup.Focus();
        }

        protected void btnSaveExpenseGroup_Click(object sender, EventArgs e)
        {
            try
            {
                objBalExpenseGroupMaster.Name = txtExpenseGroup.Text;
                objBalExpenseGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalExpenseGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveExpenseGroup.Text == "Save")
                {
                    bool chkExistingExpenseGroupName = false;
                    chkExistingExpenseGroupName = objBalExpenseGroupMaster.Check_ExistingNameExpenseGroupMaster();
                    if (chkExistingExpenseGroupName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtExpenseGroup.Focus();
                        return;
                    }
                    else
                    {
                        objBalExpenseGroupMaster.Action = "Insert";
                        int res = objBalExpenseGroupMaster.Insert_ExpenseGroupMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordExpenseGroup();
                            BindExpenseGroupMasterGrid();
                            txtExpenseGroup.Focus();
                        }
                    }
                }
                else
                {
                    objBalExpenseGroupMaster.Action = "Update";
                    objBalExpenseGroupMaster.Expgrp_AutoID = Convert.ToInt32(ViewState["Expgrp_AutoID"]);
                    int res = objBalExpenseGroupMaster.Insert_ExpenseGroupMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordExpenseGroup();
                        BindExpenseGroupMasterGrid();
                        btnSaveExpenseGroup.Text = "Save";
                        txtExpenseGroup.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtExpenseGroup.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearExpenseGroup_Click(object sender, EventArgs e)
        {
            ClearRecordExpenseGroup();
            btnClearExpenseGroup.Focus();
        }

        protected void btnDeleteExpenseGroup_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalExpenseGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalExpenseGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalExpenseGroupMaster.Action = "Delete";
                objBalExpenseGroupMaster.Expgrp_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalExpenseGroupMaster.Insert_ExpenseGroupMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindExpenseGroupMasterGrid();
                    txtExpenseGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvExpenseGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpenseGroup.PageIndex = e.NewPageIndex;
            BindExpenseGroupMasterGrid();
        }

        protected void btnEditExpenseGroup_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalExpenseGroupMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalExpenseGroupMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Expgrp_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveExpenseGroup.Text = "Update";
                objBalExpenseGroupMaster.Expgrp_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalExpenseGroupMaster.SelectByID_ExpenseGroupMaster();
                if (dt.Rows.Count > 0)
                {
                    txtExpenseGroup.Text = dt.Rows[0]["Name"].ToString();
                    txtExpenseGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Programmer Master-----------------------------------

        public void BindProgrammerMasterGrid()
        {
            try
            {
                objBalProgrammerMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalProgrammerMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalProgrammerMaster.Select_ProgrammerMaster();

                if (dt.Rows.Count > 0)
                {
                    gvProgrammer.Visible = true;
                    gvProgrammer.DataSource = dt;
                    gvProgrammer.DataBind();
                }
                else
                {
                    gvProgrammer.Visible = false;
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvProgrammer.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvProgrammer.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvProgrammer.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvProgrammer.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvProgrammer.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordProgrammer()
        {
            txtProgrammer.Text = "";
            txtProgrammer.Focus();
            btnSaveProgrammer.Text = "Save";
            txtProgrammer.Focus();
        }

        protected void btnSaveProgrammer_Click(object sender, EventArgs e)
        {
            try
            {
                objBalProgrammerMaster.Name = txtProgrammer.Text;
                objBalProgrammerMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalProgrammerMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveProgrammer.Text == "Save")
                {
                    bool chkExistingProgrammerName = false;
                    chkExistingProgrammerName = objBalProgrammerMaster.Check_ExistingNameProgrammerMaster();
                    if (chkExistingProgrammerName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtProgrammer.Focus();
                        return;
                    }
                    else
                    {
                        objBalProgrammerMaster.Action = "Insert";
                        int res = objBalProgrammerMaster.Insert_ProgrammerMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordProgrammer();
                            BindProgrammerMasterGrid();
                            txtProgrammer.Focus();
                        }
                    }
                }
                else
                {
                    objBalProgrammerMaster.Action = "Update";
                    objBalProgrammerMaster.Programmer_AutoID = Convert.ToInt32(ViewState["Programmer_AutoID"]);
                    int res = objBalProgrammerMaster.Insert_ProgrammerMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordProgrammer();
                        BindProgrammerMasterGrid();
                        btnSaveProgrammer.Text = "Save";
                        txtProgrammer.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtProgrammer.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearProgrammer_Click(object sender, EventArgs e)
        {
            ClearRecordProgrammer();
            btnClearProgrammer.Focus();
        }

        protected void btnDeleteProgrammer_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalProgrammerMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalProgrammerMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalProgrammerMaster.Action = "Delete";
                objBalProgrammerMaster.Programmer_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalProgrammerMaster.Insert_ProgrammerMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindProgrammerMasterGrid();
                    txtProgrammer.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvProgrammer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProgrammer.PageIndex = e.NewPageIndex;
            BindProgrammerMasterGrid();
        }

        protected void btnEditProgrammer_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalProgrammerMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalProgrammerMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["Programmer_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveProgrammer.Text = "Update";
                objBalProgrammerMaster.Programmer_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalProgrammerMaster.SelectByID_ProgrammerMaster();
                if (dt.Rows.Count > 0)
                {
                    txtProgrammer.Text = dt.Rows[0]["Name"].ToString();
                }
                txtProgrammer.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------FollowupType Master-----------------------------------

        public void BindFollowupTypeMasterGrid()
        {
            try
            {
                objBalFollowupTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalFollowupTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalFollowupTypeMaster.Select_FollowupTypeMaster();

                if (dt.Rows.Count > 0)
                {
                    gvFollowupType.Visible = true;
                    gvFollowupType.DataSource = dt;
                    gvFollowupType.DataBind();
                }
                else
                {
                    gvFollowupType.Visible = false;
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvFollowupType.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvFollowupType.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvFollowupType.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvFollowupType.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvFollowupType.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordFollowupType()
        {
            txtFollowupType.Text = "";
            txtFollowupType.Focus();
            btnSaveFollowupType.Text = "Save";
            txtFollowupType.Focus();
        }

        protected void btnSaveFollowupType_Click(object sender, EventArgs e)
        {
            try
            {
                objBalFollowupTypeMaster.Name = txtFollowupType.Text;
                objBalFollowupTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalFollowupTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveFollowupType.Text == "Save")
                {
                    bool chkExistingFollowupTypeName = false;
                    chkExistingFollowupTypeName = objBalFollowupTypeMaster.Check_ExistingNameFollowupTypeMaster();
                    if (chkExistingFollowupTypeName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtFollowupType.Focus();
                        return;
                    }
                    else
                    {
                        objBalFollowupTypeMaster.Action = "Insert";
                        int res = objBalFollowupTypeMaster.Insert_FollowupTypeMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordFollowupType();
                            BindFollowupTypeMasterGrid();
                            txtFollowupType.Focus();
                        }
                    }
                }
                else
                {
                    objBalFollowupTypeMaster.Action = "Update";
                    objBalFollowupTypeMaster.FollowupType_AutoID = Convert.ToInt32(ViewState["FollowupType_AutoID"]);
                    int res = objBalFollowupTypeMaster.Insert_FollowupTypeMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordFollowupType();
                        BindFollowupTypeMasterGrid();
                        btnSaveFollowupType.Text = "Save";
                        txtFollowupType.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtFollowupType.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearFollowupType_Click(object sender, EventArgs e)
        {
            ClearRecordFollowupType();
            btnClearFollowupType.Focus();
        }

        protected void btnDeleteFollowupType_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalFollowupTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalFollowupTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalFollowupTypeMaster.Action = "Delete";
                objBalFollowupTypeMaster.FollowupType_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalFollowupTypeMaster.Insert_FollowupTypeMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindFollowupTypeMasterGrid();
                    txtFollowupType.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvFollowupType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFollowupType.PageIndex = e.NewPageIndex;
            BindFollowupTypeMasterGrid();
        }

        protected void btnEditFollowupType_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalFollowupTypeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalFollowupTypeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["FollowupType_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveFollowupType.Text = "Update";
                objBalFollowupTypeMaster.FollowupType_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalFollowupTypeMaster.SelectByID_FollowupTypeMaster();
                if (dt.Rows.Count > 0)
                {
                    txtFollowupType.Text = dt.Rows[0]["Name"].ToString();
                }
                txtFollowupType.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Tax Master-----------------------------------

        public void GetTaxMasterDetails()
        {
            try
            {
                objBalTaxMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalTaxMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalTaxMaster.Select_TaxMaster();
                if (dt.Rows.Count > 0)
                {
                    txtTax.Enabled = false;
                    txtTax.Text = dt.Rows[0]["Name"].ToString();
                    btnSaveTax1.Text = "Edit";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnSaveTax1_Click(object sender, EventArgs e)
        {
            try
            {
                objBalTaxMaster.Name = txtTax.Text;
                objBalTaxMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalTaxMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                if (btnSaveTax1.Text == "Save")
                {
                    objBalTaxMaster.Action = "Insert";
                    int res = objBalTaxMaster.Insert_TaxMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        txtTax.Enabled = false;
                        btnSaveTax1.Text = "Edit";
                    }
                }
                else if (btnSaveTax1.Text == "Update")
                {
                    objBalTaxMaster.Action = "Insert";
                    int res = objBalTaxMaster.Insert_TaxMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        txtTax.Enabled = false;
                        btnSaveTax1.Text = "Edit";
                        GetTaxMasterDetails();
                    }
                }
                else
                {
                    txtTax.Enabled = true;
                    btnSaveTax1.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------CallRespond Master-----------------------------------

        public void BindCallRespondMasterGrid()
        {
            try
            {
                objBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalCallRespondMaster.Select_CallRespondMaster();

                if (dt.Rows.Count > 0)
                {
                    gvCallRespond.Visible = true;
                    gvCallRespond.DataSource = dt;
                    gvCallRespond.DataBind();
                }
                else
                {
                    gvCallRespond.Visible = false;
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvCallRespond.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvCallRespond.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvCallRespond.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvCallRespond.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvCallRespond.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void ClearRecordCallRespond()
        {
            txtCallRespond.Text = "";
            txtCallRespond.Focus();
            btnSaveCallRespond.Text = "Save";
            txtCallRespond.Focus();
        }

        protected void btnSaveCallRespond_Click(object sender, EventArgs e)
        {
            try
            {
                objBalCallRespondMaster.Name = txtCallRespond.Text;
                objBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());

                if (btnSaveCallRespond.Text == "Save")
                {
                    bool chkExistingCallRespondName = false;
                    chkExistingCallRespondName = objBalCallRespondMaster.Check_ExistingNameCallRespondMaster();
                    if (chkExistingCallRespondName == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        txtCallRespond.Focus();
                        return;
                    }
                    else
                    {
                        objBalCallRespondMaster.Action = "Insert";
                        int res = objBalCallRespondMaster.Insert_CallRespondMaster();
                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearRecordCallRespond();
                            BindCallRespondMasterGrid();
                            txtCallRespond.Focus();
                        }
                    }
                }
                else
                {
                    objBalCallRespondMaster.Action = "Update";
                    objBalCallRespondMaster.CallRespond_AutoID = Convert.ToInt32(ViewState["CallRespond_AutoID"]);
                    int res = objBalCallRespondMaster.Insert_CallRespondMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        ClearRecordCallRespond();
                        BindCallRespondMasterGrid();
                        btnSaveCallRespond.Text = "Save";
                        txtCallRespond.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record is already exists !!!','Error');", true);
                        txtCallRespond.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearCallRespond_Click(object sender, EventArgs e)
        {
            ClearRecordCallRespond();
            btnClearCallRespond.Focus();
        }

        protected void btnDeleteCallRespond_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                objBalCallRespondMaster.Action = "Delete";
                objBalCallRespondMaster.CallRespond_AutoID = Convert.ToInt32(e.CommandArgument);
                int i = objBalCallRespondMaster.Insert_CallRespondMaster();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindCallRespondMasterGrid();
                    txtCallRespond.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!.','Error');", true);
                return;
            }
        }

        protected void gvCallRespond_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCallRespond.PageIndex = e.NewPageIndex;
            BindCallRespondMasterGrid();
        }

        protected void btnEditCallRespond_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                ViewState["CallRespond_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSaveCallRespond.Text = "Update";
                objBalCallRespondMaster.CallRespond_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objBalCallRespondMaster.SelectByID_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    txtCallRespond.Text = dt.Rows[0]["Name"].ToString();
                }
                txtCallRespond.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        #region----------------------------Upgrade Days Master-----------------------------------

        public void GetMemberUpgradeMasterDetails()
        {
            try
            {
                objBalMemberUpgradeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMemberUpgradeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                dt = objBalMemberUpgradeMaster.Select_MemberUpgradeMaster();
                if (dt.Rows.Count > 0)
                {
                    txtUpgradeDays.Enabled = false;
                    txtUpgradeDays.Text = dt.Rows[0]["Days"].ToString();
                    btnSaveTax1.Text = "Edit";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnSavetxtUpgradeDays_Click(object sender, EventArgs e)
        {
            try
            {
                objBalMemberUpgradeMaster.Days = txtUpgradeDays.Text;
                objBalMemberUpgradeMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                objBalMemberUpgradeMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
                if (btnSavetxtUpgradeDays.Text == "Save")
                {
                    objBalMemberUpgradeMaster.Action = "Insert";
                    int res = objBalMemberUpgradeMaster.Insert_MemberUpgradeMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        txtUpgradeDays.Enabled = false;
                        btnSavetxtUpgradeDays.Text = "Edit";
                    }
                }
                else if (btnSavetxtUpgradeDays.Text == "Update")
                {
                    objBalMemberUpgradeMaster.Action = "Insert";
                    int res = objBalMemberUpgradeMaster.Insert_MemberUpgradeMaster();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        txtUpgradeDays.Enabled = false;
                        btnSavetxtUpgradeDays.Text = "Edit";
                        GetMemberUpgradeMasterDetails();
                    }
                }
                else
                {
                    txtUpgradeDays.Enabled = true;
                    btnSavetxtUpgradeDays.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        #endregion

        
    }
}