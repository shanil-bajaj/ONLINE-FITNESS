using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class ViewHealthDetails : System.Web.UI.Page
    {
        BalViewHealthDetails objViewHealthDetails = new BalViewHealthDetails();
        DataTable dataTable = new DataTable();

        //static int Member_AutoID;
        static int Member_Id1;

        protected void Page_Load(object sender, EventArgs e)
        {
             try
            {            
                if (!IsPostBack)
                {
                    if (Request.QueryString["Member_Id"] != null)
                    {
                        Member_Id1 = Convert.ToInt32(Request.QueryString["Member_Id"]);
                        GetCompanyDetails();
                        GetExistingMemberHealthDetails();
                    }
                }
            }
             catch (Exception ex)
             {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                 ErrorHandiling.SendErrorToText(ex);
             }
        }
      

        #region --------- Assign Comp and Branch ID-------------------------
        private void AssignID()
        {
            try
            {
                objViewHealthDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                objViewHealthDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- GetCompanyDetails --------------
        protected void GetCompanyDetails()
        {
            AssignID();
            objViewHealthDetails.Action = "GetCompanyDetails";
            dataTable = objViewHealthDetails.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                lblCompanyName.Text = dataTable.Rows[0]["BranchName"].ToString();
                imgCompanyLogo.ImageUrl = dataTable.Rows[0]["BranchLogoPath"].ToString();
                lblAddress1.Text = dataTable.Rows[0]["Address1"].ToString();
                lblAddress2.Text = dataTable.Rows[0]["Address2"].ToString();
                lblEmail.Text = dataTable.Rows[0]["Email"].ToString();
            }
        }
        #endregion

        #region ---------- Get Member Health Details --------------
        private void GetExistingMemberHealthDetails()
        {
            try
            {
                AssignID();
                //objViewHealthDetails.Member_AutoID = Member_AutoID;
                objViewHealthDetails.Member_ID1 = Member_Id1;
                objViewHealthDetails.Action = "GetMemberHeailthDetails";
                dataTable = objViewHealthDetails.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    lblMemberId.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                    lblName.Text = dataTable.Rows[0]["Name"].ToString();
                    lblContact.Text = dataTable.Rows[0]["Contact1"].ToString();

                    lblDoctoreCare.Text = dataTable.Rows[0]["DoctorCareStatus"].ToString();
                    lblDoctorCareReason.Text = dataTable.Rows[0]["DoctorCareReason"].ToString();
                    lblPhysicalExamination.Text = dataTable.Rows[0]["PhysicalExamination"].ToString();
                    lblStressStaus.Text = dataTable.Rows[0]["ExerciseStressStatus"].ToString();
                    lblStressResult.Text = dataTable.Rows[0]["StressResult"].ToString();
                    lblMedication.Text = dataTable.Rows[0]["MedicationStatus"].ToString();
                    lblMedicationReason.Text = dataTable.Rows[0]["MedicationReason"].ToString();
                    lblHospitalized.Text = dataTable.Rows[0]["HospitalizedStatus"].ToString();
                    lblHospitalizedReason.Text = dataTable.Rows[0]["HospitalizedReason"].ToString();
                    lblSmoke.Text = dataTable.Rows[0]["SmokeStatus"].ToString();
                    lblPregnant.Text = dataTable.Rows[0]["PregnantStatus"].ToString();
                    lblAlcohol.Text = dataTable.Rows[0]["AlcoholStatus"].ToString();
                    lblStressLevel.Text = dataTable.Rows[0]["StressLevelStatus"].ToString();
                    lblModerately.Text = dataTable.Rows[0]["ModeratelyStatus"].ToString();
                    lblBPStatus.Text = dataTable.Rows[0]["BPStatus"].ToString();
                    lblCholestrol.Text = dataTable.Rows[0]["CholestrolStatus"].ToString();
                    lblHeartDisease.Text = dataTable.Rows[0]["HeartDiseaseStatus"].ToString();
                    lblRheumatiDisease.Text = dataTable.Rows[0]["RheumatiDiseaseStatus"].ToString();
                    lblChestPain.Text = dataTable.Rows[0]["ChestPainStatus"].ToString();
                    lblHeartBeat.Text = dataTable.Rows[0]["HeartBeatStatus"].ToString();
                    lblFaint.Text = dataTable.Rows[0]["FaintStatus"].ToString();
                    lblBreath.Text = dataTable.Rows[0]["BreathStatus"].ToString();
                    lblCramping.Text = dataTable.Rows[0]["CrampingStatus"].ToString();
                    lblEmphysema.Text = dataTable.Rows[0]["EmphysemaStatus"].ToString();
                    lblMetabolic.Text = dataTable.Rows[0]["MetabolicStatus"].ToString();
                    lblEpilepsy.Text = dataTable.Rows[0]["EpilepsyStatus"].ToString();
                    lblAsthma.Text = dataTable.Rows[0]["AsthmaStatus"].ToString();
                    lblPainStatus.Text = dataTable.Rows[0]["PainStatus"].ToString();
                    lblOtherPain.Text = dataTable.Rows[0]["OtherPainStatus"].ToString();
                    lblMusclePain.Text = dataTable.Rows[0]["MusclePainStatus"].ToString();

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