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
    public partial class HealthDetails : System.Web.UI.Page
    {
        BalHealthDetails ObjHealth = new BalHealthDetails();
        DataTable dataTable = new DataTable();
        static int Member_AutoID;
        int flag = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["HealthDetailsMember_ID"] != null)
                    {                        
                        ObjHealth.Member_AutoID = Convert.ToInt32(Request.QueryString["HealthDetailsMember_ID"]);
                        ObjHealth.Member_ID1 = getMemIDByAutoID();

                        ObjHealth.Action = "SearchByMember_ID";
                        BindMemberDetails();                                   
                    }
                    txtMemberID.Focus();
                }                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        #region --------------- Get Member id By Auto ID --------------
        int member_Id;
        private int getMemIDByAutoID()
        {
            AssignID();
            ObjHealth.Action = "GetMemIdByAutoId";
            dataTable = ObjHealth.GetDetails();

            if (dataTable.Rows.Count > 0)
            {
                member_Id = Convert.ToInt32(dataTable.Rows[0]["Member_ID1"].ToString());
            }

            return member_Id;
        }
        #endregion

        #region --------- Assign Comp and Branch ID-------------------------
        private void AssignID()
        {
            try
            {
                //ObjHealth.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                //ObjHealth.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //ObjHealth.Executive_ID = Convert.ToInt32(Response.Cookies["OnlineGym"]["Staff_AutoID"]);

                ObjHealth.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                ObjHealth.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                ObjHealth.Executive_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ---------- Meber ID Text Change Event -----------------
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != string.Empty)
                {
                    //ClearFormOnMemberID();
                    ObjHealth.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
                    ObjHealth.Action = "SearchByMember_ID";                                                             
                    BindMemberDetails();
                    txtMemberID.Focus();                    
                    
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }


        //private void ClearFormOnMemberID()
        //{
            
        //}        
        #endregion

        #region ---------- Meber Contact Text Change Event -----------------
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != string.Empty)
                {
                   // ClearFormOnMemberContact();
                    ObjHealth.Contact1 = txtContact.Text;
                    ObjHealth.Action = "SearchByMember_Contact1";
                    BindMemberDetails();
                    txtContact.Focus();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
       
        #endregion

        #region --------------- Get Member Details ----------------
        private void BindMemberDetails()
        {
            try
            {
                AssignID();
                dataTable = ObjHealth.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    Member_AutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                    txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                    txtMemberName.Text = dataTable.Rows[0]["Name"].ToString();
                    txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();

                    chk_ExistingRecord();
                }
                else
                {
                    ClearAllForm();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Record Not Found !!!','Information');", true);
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region ---------- Check Existing Details -----------
        private void chk_ExistingRecord()
        {
            try
            {
                ObjHealth.Member_AutoID = Member_AutoID;
                ObjHealth.Action = "GetHealthDetails";

                dataTable = ObjHealth.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    btnSave.Text = "Update";
                    FillDetails();
                }
                else
                {
                    btnSave.Text = "Save";
                    frmWriteMode();                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
                   
        #endregion

        #region ---------- Fill Details when Record Exist ----------------
        private void FillDetails()
        {
            try
            {
                string DoctorCareStatus = dataTable.Rows[0]["DoctorCareStatus"].ToString();
                string DoctorCareReason = dataTable.Rows[0]["DoctorCareReason"].ToString();
                string PhysicalExamination = dataTable.Rows[0]["PhysicalExamination"].ToString();
                string ExerciseStressStatus = dataTable.Rows[0]["ExerciseStressStatus"].ToString();
                string StressResult = dataTable.Rows[0]["StressResult"].ToString();
                string MedicationStatus = dataTable.Rows[0]["MedicationStatus"].ToString();
                string MedicationReason = dataTable.Rows[0]["MedicationReason"].ToString();
                string HospitalizedStatus = dataTable.Rows[0]["HospitalizedStatus"].ToString();
                string HospitalizedReason = dataTable.Rows[0]["HospitalizedReason"].ToString();
                string SmokeStatus = dataTable.Rows[0]["SmokeStatus"].ToString();
                string PregnantStatus = dataTable.Rows[0]["PregnantStatus"].ToString();
                string AlcoholStatus = dataTable.Rows[0]["AlcoholStatus"].ToString();
                string StressLevelStatus = dataTable.Rows[0]["StressLevelStatus"].ToString();
                string ModeratelyStatus = dataTable.Rows[0]["ModeratelyStatus"].ToString();
                string BPStatus = dataTable.Rows[0]["BPStatus"].ToString();
                string CholestrolStatus = dataTable.Rows[0]["CholestrolStatus"].ToString();
                string HeartDiseaseStatus = dataTable.Rows[0]["HeartDiseaseStatus"].ToString();
                string RheumatiDiseaseStatus = dataTable.Rows[0]["RheumatiDiseaseStatus"].ToString();
                string ChestPainStatus = dataTable.Rows[0]["ChestPainStatus"].ToString();
                string HeartBeatStatus = dataTable.Rows[0]["HeartBeatStatus"].ToString();
                string FaintStatus = dataTable.Rows[0]["FaintStatus"].ToString();
                string BreathStatus = dataTable.Rows[0]["BreathStatus"].ToString();
                string CrampingStatus = dataTable.Rows[0]["CrampingStatus"].ToString();
                string EmphysemaStatus = dataTable.Rows[0]["EmphysemaStatus"].ToString();
                string MetabolicStatus = dataTable.Rows[0]["MetabolicStatus"].ToString();
                string EpilepsyStatus = dataTable.Rows[0]["EpilepsyStatus"].ToString();
                string AsthmaStatus = dataTable.Rows[0]["AsthmaStatus"].ToString();
                string PainStatus = dataTable.Rows[0]["PainStatus"].ToString();
                string OtherPainStatus = dataTable.Rows[0]["OtherPainStatus"].ToString();
                string MusclePainStatus = dataTable.Rows[0]["MusclePainStatus"].ToString();

                if (DoctorCareStatus == "Yes")
                {
                    rbtnDoctorCareYes.Checked = true;
                    txtDoctorCareReason.Enabled = true;
                    if (DoctorCareReason != string.Empty)
                    {                       
                        txtDoctorCareReason.Text = DoctorCareReason;
                    }
                    
                }
                else if(DoctorCareStatus=="No")
                {
                    rbtnDoctorCareNo.Checked = true;
                }

                if (PhysicalExamination != string.Empty)
                {
                    txtPhysicalExamination.Text = PhysicalExamination;
                }

                if (ExerciseStressStatus == "Yes")
                {
                    rbtnStressYes.Checked = true;
                }
                else if (ExerciseStressStatus == "No")
                {
                    rbtnStressNo.Checked = true;
                }

                if (StressResult == "Yes")
                {
                    rbtnStressResultYes.Checked = true;
                }
                else if (StressResult == "No")
                {
                    rbtnStressResultNo.Checked = true;
                }

                if (MedicationStatus == "Yes")
                {
                    rbtnMedicationYes.Checked = true;
                    txtMedicationReason.Enabled = true;

                    if (MedicationReason != string.Empty)
                    {                        
                        txtMedicationReason.Text = MedicationReason;
                    }
                }
                else if (MedicationStatus == "No")
                {
                    rbtnMedicationNo.Checked = true;
                }

                if (HospitalizedStatus == "Yes")
                {
                    rbtnHospitalizedYes.Checked = true;
                    txtHospitalizedReason.Enabled = true;

                    if (HospitalizedReason != string.Empty)
                    {                        
                        txtHospitalizedReason.Text = HospitalizedReason;
                    }
                }
                else if (HospitalizedStatus == "No")
                {
                    rbtnHospitalizedNo.Checked = true;
                }

                if (SmokeStatus == "Yes")
                {
                    rbtnSmokeYes.Checked = true;
                }
                else if (SmokeStatus == "No")
                {
                    rbtnSmokeNo.Checked = true;
                }

                if (PregnantStatus == "Yes")
                {
                    rbtnPregnantYes.Checked = true;
                }
                else if (PregnantStatus == "No")
                {
                    rbtnPregnantNo.Checked = true;
                }

                if (AlcoholStatus == "Yes")
                {
                    rbtnAlcoholYes.Checked = true;
                }
                else if (AlcoholStatus == "No")
                {
                    rbtnAlcoholNo.Checked = true;
                }

                if (StressLevelStatus == "Yes")
                {
                    rbtnStressLevelYes.Checked = true;
                }
                else if (StressLevelStatus == "No")
                {
                    rbtnStressLevelNo.Checked = true;
                }

                if (ModeratelyStatus == "Yes")
                {
                    rbtnModeratelyYes.Checked = true;
                }
                else if (ModeratelyStatus == "No")
                {
                    rbtnModeratelyNo.Checked = true;
                }

                if (BPStatus == "Yes")
                {
                    rbtnBPStatusYes.Checked = true;
                }
                else if (BPStatus == "No")
                {
                    rbtnBPStatusNo.Checked = true;
                }

                if (CholestrolStatus == "Yes")
                {
                    rbtnCholestrolYes.Checked = true;
                }
                else if (CholestrolStatus == "No")
                {
                    rbtnCholestrolNo.Checked = true;
                }

                if (HeartDiseaseStatus == "Yes")
                {
                    rbtnHeartDiseaseYes.Checked = true;
                }
                else if (HeartDiseaseStatus == "No")
                {
                    rbtnHeartDiseaseNo.Checked = true;
                }

                if (RheumatiDiseaseStatus == "Yes")
                {
                    rbtnRheumatiDiseaseYes.Checked = true;
                }
                else if (RheumatiDiseaseStatus == "No")
                {
                    rbtnRheumatiDiseaseNo.Checked = true;
                }

                if (ChestPainStatus == "Yes")
                {
                    rbtnChestPainYes.Checked = true;
                }
                else if (ChestPainStatus == "No")
                {
                    rbtnChestPainNo.Checked = true;
                }

                if (HeartBeatStatus == "Yes")
                {
                    btnHeartBeatYes.Checked = true;
                }
                else if (HeartBeatStatus == "No")
                {
                    btnHeartBeatNo.Checked = true;
                }

                if (FaintStatus == "Yes")
                {
                    rbtnFaintYes.Checked = true;
                }
                else if (FaintStatus == "No")
                {
                    rbtnFaintNo.Checked = true;
                }

                if (BreathStatus == "Yes")
                {
                    rbtnBreathYes.Checked = true;
                }
                else if (BreathStatus == "No")
                {
                    rbtnBreathNo.Checked = true;
                }

                if (CrampingStatus == "Yes")
                {
                    rbtnCrampingYes.Checked = true;
                }
                else if (CrampingStatus == "No")
                {
                    rbtnCrampingNo.Checked = true;
                }

                if (EmphysemaStatus == "Yes")
                {
                    rbtnEmphysemaYes.Checked = true;
                }
                else if (EmphysemaStatus == "No")
                {
                    rbtnEmphysemaNo.Checked = true;
                }

                if (MetabolicStatus == "Yes")
                {
                    rbtnMetabolicYes.Checked = true;
                }
                else if (MetabolicStatus == "No")
                {
                    rbtnMetabolicNo.Checked = true;
                }

                if (EpilepsyStatus == "Yes")
                {
                    rbtnEpilepsyYes.Checked = true;
                }
                else if (EpilepsyStatus == "No")
                {
                    rbtnEpilepsyNo.Checked = true;
                }

                if (AsthmaStatus == "Yes")
                {
                    rbtnAsthmaYes.Checked = true;
                }
                else if (AsthmaStatus == "No")
                {
                    rbtnAsthmaNo.Checked = true;
                }

                if (PainStatus == "Yes")
                {
                    rbtnPainStatusYes.Checked = true;
                }
                else if (PainStatus == "No")
                {
                    rbtnPainStatusNo.Checked = true;
                }

                if (OtherPainStatus == "Yes")
                {
                    rbtnOtherPainYes.Checked = true;
                }
                else if (OtherPainStatus == "No")
                {
                    rbtnOtherPainNo.Checked = true;
                }

                if (MusclePainStatus == "Yes")
                {
                    btnMusclePainYes.Checked = true;
                }
                else if (MusclePainStatus == "No")
                {
                    btnMusclePainNo.Checked = true;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
         
        }
        #endregion

        #region ---------- Form Write Mode --------------
        private void frmWriteMode()
        {
            try
            {
                //btnSave.Text = "Save";
                //txtMemberID.Text = "";
                //txtMemberName.Text = "";
                //txtContact.Text = "";

                rbtnDoctorCareYes.Checked = false;
                txtDoctorCareReason.Text = "";
                txtDoctorCareReason.Enabled = false;
                rbtnDoctorCareNo.Checked = false;
                txtPhysicalExamination.Text = "";
                rbtnStressYes.Checked = false;
                rbtnStressNo.Checked = false;
                rbtnStressResultYes.Checked = false;
                rbtnStressResultNo.Checked = false;
                rbtnMedicationYes.Checked = false;
                txtMedicationReason.Text = "";
                txtMedicationReason.Enabled = false;
                rbtnMedicationNo.Checked = false;
                rbtnHospitalizedYes.Checked = false;
                txtHospitalizedReason.Text = "";
                txtHospitalizedReason.Enabled = false;
                rbtnHospitalizedNo.Checked = false;
                rbtnSmokeYes.Checked = false;
                rbtnSmokeNo.Checked = false;
                rbtnPregnantYes.Checked = false;
                rbtnPregnantNo.Checked = false;
                rbtnAlcoholYes.Checked = false;
                rbtnAlcoholNo.Checked = false;
                rbtnStressLevelYes.Checked = false;
                rbtnStressLevelNo.Checked = false;
                rbtnModeratelyYes.Checked = false;
                rbtnModeratelyNo.Checked = false;
                rbtnBPStatusYes.Checked = false;
                rbtnBPStatusNo.Checked = false;
                rbtnCholestrolYes.Checked = false;
                rbtnCholestrolNo.Checked = false;
                rbtnHeartDiseaseYes.Checked = false;
                rbtnHeartDiseaseNo.Checked = false;
                rbtnRheumatiDiseaseYes.Checked = false;
                rbtnRheumatiDiseaseNo.Checked = false;
                rbtnChestPainYes.Checked = false;
                rbtnChestPainNo.Checked = false;
                btnHeartBeatYes.Checked = false;
                btnHeartBeatNo.Checked = false;
                rbtnFaintYes.Checked = false;
                rbtnFaintNo.Checked = false;
                rbtnBreathYes.Checked = false;
                rbtnBreathNo.Checked = false;
                rbtnCrampingYes.Checked = false;
                rbtnCrampingNo.Checked = false;
                rbtnEmphysemaYes.Checked = false;
                rbtnEmphysemaNo.Checked = false;
                rbtnMetabolicYes.Checked = false;
                rbtnMetabolicNo.Checked = false;
                rbtnEpilepsyYes.Checked = false;
                rbtnEpilepsyNo.Checked = false;
                rbtnAsthmaYes.Checked = false;
                rbtnAsthmaNo.Checked = false;
                rbtnPainStatusYes.Checked = false;
                rbtnPainStatusNo.Checked = false;
                rbtnOtherPainYes.Checked = false;
                rbtnOtherPainNo.Checked = false;
                btnMusclePainYes.Checked = false;
                btnMusclePainNo.Checked = false;            
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------- Add Parameter -------------------
        private void addParameter()
        {
            ObjHealth.Member_AutoID = Member_AutoID;
            if (rbtnDoctorCareYes.Checked)
            {
               ObjHealth.DoctorCareStatus = "Yes";

               if (txtDoctorCareReason.Text != string.Empty)
               {
                   ObjHealth.DoctorCareReason = txtDoctorCareReason.Text;
               }
               else
               {
                   ObjHealth.DoctorCareReason = "";
               }
            }
            else if (rbtnDoctorCareNo.Checked)
            {
                ObjHealth.DoctorCareStatus = "No";
            }

            if (txtPhysicalExamination.Text != string.Empty)
            {
                ObjHealth.PhysicalExamination = txtPhysicalExamination.Text;
            }
            else
            {
                ObjHealth.PhysicalExamination = "";
            }

            if (rbtnStressYes.Checked)
            {
                ObjHealth.ExerciseStressStatus = "Yes";
            }
            else if (rbtnStressNo.Checked)
            {
                ObjHealth.ExerciseStressStatus = "No";
            }

            if (rbtnStressResultYes.Checked)
            {
                ObjHealth.StressResult = "Yes";
            }
            else if (rbtnStressResultNo.Checked)
            {
                ObjHealth.StressResult = "No";
            }

            if (rbtnMedicationYes.Checked)
            {
                ObjHealth.MedicationStatus = "Yes";

                if (txtMedicationReason.Text != string.Empty)
                {
                    ObjHealth.MedicationReason = txtMedicationReason.Text;
                }
                else
                {
                    ObjHealth.MedicationReason = "";
                }
            }
            else if (rbtnMedicationNo.Checked)
            {
                ObjHealth.MedicationStatus = "No";
            }

            if (rbtnHospitalizedYes.Checked)
            {
                ObjHealth.HospitalizedStatus = "Yes";

                if (txtHospitalizedReason.Text != string.Empty)
                {
                    ObjHealth.HospitalizedReason = txtHospitalizedReason.Text;
                }
                else
                {
                    ObjHealth.HospitalizedReason = "";
                }
            }
            else if (rbtnHospitalizedNo.Checked)
            {
                ObjHealth.HospitalizedStatus = "No";
            }

            if (rbtnSmokeYes.Checked)
            {
                ObjHealth.SmokeStatus = "Yes";
            }
            else if (rbtnSmokeNo.Checked)
            {
                ObjHealth.SmokeStatus = "No";
            }

            if (rbtnPregnantYes.Checked)
            {
                ObjHealth.PregnantStatus = "Yes";
            }
            else if (rbtnPregnantNo.Checked)
            {
                ObjHealth.PregnantStatus = "No";
            }

            if (rbtnAlcoholYes.Checked)
            {
                ObjHealth.AlcoholStatus = "Yes";
            }
            else if (rbtnAlcoholNo.Checked)
            {
                ObjHealth.AlcoholStatus = "No";
            }

            if (rbtnStressLevelYes.Checked)
            {
                ObjHealth.StressLevelStatus = "Yes";
            }
            else if (rbtnStressLevelNo.Checked)
            {
                ObjHealth.StressLevelStatus = "No";
            }

            if (rbtnModeratelyYes.Checked)
            {
                ObjHealth.ModeratelyStatus = "Yes";
            }
            else if (rbtnModeratelyNo.Checked)
            {
                ObjHealth.ModeratelyStatus = "No";
            }

            if (rbtnBPStatusYes.Checked)
            {
                ObjHealth.BPStatus = "Yes";
            }
            else if (rbtnBPStatusNo.Checked)
            {
                ObjHealth.BPStatus = "No";
            }

            if (rbtnCholestrolYes.Checked)
            {
                ObjHealth.CholestrolStatus = "Yes";
            }
            else if (rbtnCholestrolNo.Checked)
            {
                ObjHealth.CholestrolStatus = "No";
            }

            if (rbtnHeartDiseaseYes.Checked)
            {
                ObjHealth.HeartDiseaseStatus = "Yes";
            }
            else if (rbtnHeartDiseaseNo.Checked)
            {
                ObjHealth.HeartDiseaseStatus = "No";
            }

            if (rbtnRheumatiDiseaseYes.Checked)
            {
                ObjHealth.RheumatiDiseaseStatus = "Yes";
            }
            else if (rbtnRheumatiDiseaseNo.Checked)
            {
                ObjHealth.RheumatiDiseaseStatus = "No";
            }

            if (rbtnChestPainYes.Checked)
            {
                ObjHealth.ChestPainStatus = "Yes";
            }
            else if (rbtnChestPainNo.Checked)
            {
                ObjHealth.ChestPainStatus = "No";
            }

            if (btnHeartBeatYes.Checked)
            {
                ObjHealth.HeartBeatStatus = "Yes";
            }
            else if (btnHeartBeatNo.Checked)
            {
                ObjHealth.HeartBeatStatus = "No";
            }

            if (rbtnFaintYes.Checked)
            {
                ObjHealth.FaintStatus = "Yes";
            }
            else if (rbtnFaintNo.Checked)
            {
                ObjHealth.FaintStatus = "No";
            }

            if (rbtnBreathYes.Checked)
            {
                ObjHealth.BreathStatus = "Yes";
            }
            else if (rbtnBreathNo.Checked)
            {
                ObjHealth.BreathStatus = "No";
            }

            if (rbtnCrampingYes.Checked)
            {
                ObjHealth.CrampingStatus = "Yes";
            }
            else if (rbtnCrampingNo.Checked)
            {
                ObjHealth.CrampingStatus = "No";
            }

            if (rbtnEmphysemaYes.Checked)
            {
                ObjHealth.EmphysemaStatus = "Yes";
            }
            else if (rbtnEmphysemaNo.Checked)
            {
                ObjHealth.EmphysemaStatus = "No";
            }

            if (rbtnMetabolicYes.Checked)
            {
                ObjHealth.MetabolicStatus = "Yes";
            }
            else if (rbtnMetabolicNo.Checked)
            {
                ObjHealth.MetabolicStatus = "No";
            }

            if (rbtnEpilepsyYes.Checked)
            {
                ObjHealth.EpilepsyStatus = "Yes";
            }
            else if (rbtnEpilepsyNo.Checked)
            {
                ObjHealth.EpilepsyStatus = "No";
            }

            if (rbtnAsthmaYes.Checked)
            {
                ObjHealth.AsthmaStatus = "Yes";
            }
            else if (rbtnAsthmaNo.Checked)
            {
                ObjHealth.AsthmaStatus = "No";
            }

            if (rbtnPainStatusYes.Checked)
            {
                ObjHealth.PainStatus = "Yes";
            }
            else if (rbtnPainStatusNo.Checked)
            {
                ObjHealth.PainStatus = "No";
            }

            if (rbtnOtherPainYes.Checked)
            {
                ObjHealth.OtherPainStatus = "Yes";
            }
            else if (rbtnOtherPainNo.Checked)
            {
                ObjHealth.OtherPainStatus = "No";
            }

            if (btnMusclePainYes.Checked)
            {
                ObjHealth.MusclePainStatus = "Yes";
            }
            else if (btnMusclePainNo.Checked)
            {
                ObjHealth.MusclePainStatus = "No";
            }
           


        }  
        #endregion

        #region ------------- Clear All Form Details ---------------
        private void ClearAllForm()
        {
            btnSave.Text = "Save";
            txtMemberID.Text = "";
            txtMemberID.Focus();
            txtMemberName.Text = "";
            txtContact.Text = "";

            rbtnDoctorCareYes.Checked = false;                
            txtDoctorCareReason.Text = "";
            txtDoctorCareReason.Enabled = false;
            rbtnDoctorCareNo.Checked = false;            
            txtPhysicalExamination.Text = "";
            rbtnStressYes.Checked = false;           
            rbtnStressNo.Checked = false;
            rbtnStressResultYes.Checked = false;
            rbtnStressResultNo.Checked = false;
            rbtnMedicationYes.Checked = false;               
            txtMedicationReason.Text = "";
            txtMedicationReason.Enabled = false;
            rbtnMedicationNo.Checked = false;
            rbtnHospitalizedYes.Checked = false;
            txtHospitalizedReason.Text = "";
            txtHospitalizedReason.Enabled = false;
            rbtnHospitalizedNo.Checked = false;
            rbtnSmokeYes.Checked = false;
            rbtnSmokeNo.Checked = false;
            rbtnPregnantYes.Checked = false;
            rbtnPregnantNo.Checked = false;
            rbtnAlcoholYes.Checked = false;
            rbtnAlcoholNo.Checked = false;
            rbtnStressLevelYes.Checked = false;
            rbtnStressLevelNo.Checked = false;
            rbtnModeratelyYes.Checked = false;
            rbtnModeratelyNo.Checked = false;
            rbtnBPStatusYes.Checked = false;
            rbtnBPStatusNo.Checked = false;
            rbtnCholestrolYes.Checked = false;
            rbtnCholestrolNo.Checked = false;
            rbtnHeartDiseaseYes.Checked = false;
            rbtnHeartDiseaseNo.Checked = false;
            rbtnRheumatiDiseaseYes.Checked = false;
            rbtnRheumatiDiseaseNo.Checked = false;
            rbtnChestPainYes.Checked = false;
            rbtnChestPainNo.Checked = false;
            btnHeartBeatYes.Checked = false;
            btnHeartBeatNo.Checked = false;
            rbtnFaintYes.Checked = false;
            rbtnFaintNo.Checked = false;
            rbtnBreathYes.Checked = false;
            rbtnBreathNo.Checked = false;
            rbtnCrampingYes.Checked = false;
            rbtnCrampingNo.Checked = false;
            rbtnEmphysemaYes.Checked = false;
            rbtnEmphysemaNo.Checked = false;            
            rbtnMetabolicYes.Checked = false;           
            rbtnMetabolicNo.Checked = false;            
            rbtnEpilepsyYes.Checked = false;           
            rbtnEpilepsyNo.Checked = false;           
            rbtnAsthmaYes.Checked = false;            
            rbtnAsthmaNo.Checked = false;           
            rbtnPainStatusYes.Checked = false;            
            rbtnPainStatusNo.Checked = false;            
            rbtnOtherPainYes.Checked = false;            
            rbtnOtherPainNo.Checked = false;            
            btnMusclePainYes.Checked = false;            
            btnMusclePainNo.Checked = false;            

        }

        #endregion

        #region ------------------ Save Button -----------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != string.Empty)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                    txtMemberID.Focus();
                    ClearAllForm();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.error('Please Fill Member Record !!!','Error');", true);
                }

                if (flag == 1)
                {
                    AssignID();
                    addParameter();

                    if (btnSave.Text == "Save")
                    {
                        ObjHealth.Action = "INSERT";

                        int res = ObjHealth.Insert_Update_Delete_Details();

                        if (res > 0)
                        {
                            txtMemberID.Focus();
                            ClearAllForm();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);

                            if (Request.QueryString["HealthDetailsMember_ID"] != null)
                            {
                                string url = "ExistingHeaithDetails.aspx";
                                Response.Redirect(url);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Saved Failed !!!','Information');", true);
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        ObjHealth.Action = "UPDATE";

                        int res = ObjHealth.Insert_Update_Delete_Details();

                        if (res > 0)
                        {
                            txtMemberID.Focus();
                            ClearAllForm();                           
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);

                            if (Request.QueryString["HealthDetailsMember_ID"] != null)
                            {
                                string url = "ExistingHeaithDetails.aspx";
                                Response.Redirect(url);
                            }
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Clear Button Event -----------
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllForm();
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