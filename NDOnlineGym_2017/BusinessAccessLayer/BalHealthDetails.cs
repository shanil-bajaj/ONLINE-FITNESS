using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalHealthDetails
    {

        #region ------------Public Properties -----------

        DataConnection obDataCon = new DataConnection();

        // Member Details Property
        public int Health_AutoID { get; set; }
        public int Member_ID1 { get; set; }
        public int Member_AutoID { get; set; }
        public string Contact1 { get; set; }

        public string DoctorCareStatus { get; set; }
        public string DoctorCareReason { get; set; }
        public string PhysicalExamination { get; set; }
        public string ExerciseStressStatus { get; set; }
        public string StressResult { get; set; }
        public string MedicationStatus { get; set; }
        public string MedicationReason { get; set; }
        public string HospitalizedStatus { get; set; }
        public string HospitalizedReason { get; set; }
        public string SmokeStatus { get; set; }
        public string PregnantStatus { get; set; }
        public string AlcoholStatus { get; set; }
        public string StressLevelStatus { get; set; }
        public string ModeratelyStatus { get; set; }
        public string BPStatus { get; set; }
        public string CholestrolStatus { get; set; }
        public string HeartDiseaseStatus { get; set; }
        public string RheumatiDiseaseStatus { get; set; }
        public string ChestPainStatus { get; set; }
        public string HeartBeatStatus { get; set; }
        public string FaintStatus { get; set; }
        public string BreathStatus { get; set; }
        public string CrampingStatus { get; set; }
        public string EmphysemaStatus { get; set; }
        public string MetabolicStatus { get; set; }
        public string EpilepsyStatus { get; set; }
        public string AsthmaStatus { get; set; }
        public string PainStatus { get; set; }
        public string OtherPainStatus { get; set; }
        public string MusclePainStatus { get; set; }

        public int Executive_ID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public string Action { get; set; }

        #endregion

        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[6];            
            param[0] = new SqlParameter("@Member_ID1", Member_ID1);
            param[1] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[2] = new SqlParameter("@Contact1", Contact1);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[5] = new SqlParameter("@Action", Action);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberHealthDetails", param);
        }

        public int Insert_Update_Delete_Details()
        {
            SqlParameter[] param = new SqlParameter[37];
            param[0] = new SqlParameter("@Health_AutoID",Health_AutoID );
            param[1] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[2] = new SqlParameter("@DoctorCareStatus",DoctorCareStatus );
            param[3] = new SqlParameter("@DoctorCareReason", DoctorCareReason);
            param[4] = new SqlParameter("@PhysicalExamination", PhysicalExamination);
            param[5] = new SqlParameter("@ExerciseStressStatus",ExerciseStressStatus );
            param[6] = new SqlParameter("@StressResult", StressResult);
            param[7] = new SqlParameter("@MedicationStatus", MedicationStatus);
            param[8] = new SqlParameter("@MedicationReason", MedicationReason);
            param[9] = new SqlParameter("@HospitalizedStatus", HospitalizedStatus);
            param[10] = new SqlParameter("@HospitalizedReason",HospitalizedReason );
            param[11] = new SqlParameter("@SmokeStatus", SmokeStatus);
            param[12] = new SqlParameter("@PregnantStatus",PregnantStatus );
            param[13] = new SqlParameter("@AlcoholStatus", AlcoholStatus);
            param[14] = new SqlParameter("@StressLevelStatus",StressLevelStatus );
            param[15] = new SqlParameter("@ModeratelyStatus", ModeratelyStatus);
            param[16] = new SqlParameter("@BPStatus",BPStatus );
            param[17] = new SqlParameter("@CholestrolStatus", CholestrolStatus);
            param[18] = new SqlParameter("@HeartDiseaseStatus",HeartDiseaseStatus );
            param[19] = new SqlParameter("@RheumatiDiseaseStatus",RheumatiDiseaseStatus );
            param[20] = new SqlParameter("@ChestPainStatus",ChestPainStatus );
            param[21] = new SqlParameter("@HeartBeatStatus", HeartBeatStatus);
            param[22] = new SqlParameter("@FaintStatus",FaintStatus );
            param[23] = new SqlParameter("@BreathStatus",BreathStatus );
            param[24] = new SqlParameter("@CrampingStatus",CrampingStatus );
            param[25] = new SqlParameter("@EmphysemaStatus",EmphysemaStatus );
            param[26] = new SqlParameter("@MetabolicStatus",MetabolicStatus );
            param[27] = new SqlParameter("@EpilepsyStatus",EpilepsyStatus );
            param[28] = new SqlParameter("@AsthmaStatus",AsthmaStatus );
            param[29] = new SqlParameter("@PainStatus",PainStatus );
            param[30] = new SqlParameter("@OtherPainStatus",OtherPainStatus );
            param[31] = new SqlParameter("@MusclePainStatus",MusclePainStatus );
            param[32] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[33] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[34] = new SqlParameter("@Member_ID1", Member_ID1);
            param[35] = new SqlParameter("@Executive_ID", Executive_ID);  
            param[36] = new SqlParameter("@Action", Action);

            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemberHealthDetails", param);
        }
    }
}
