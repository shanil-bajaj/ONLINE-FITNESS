using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Configuration;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalPTTrainerIncentiveMaster
    {
        #region ------------Public Properties -----------

        public int Trainer_AutoID { get; set; }
        public int TrainerID_Fk { get; set; }
        public string Incentive { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public string Action { get; set; }

        DataConnection obDataCon = new DataConnection();
        #endregion

        #region Check that Department Name is Exist or Not
        public bool Check_ExistingPTTrainerIncentiveMaster()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Incentive", Incentive);
            param[1] = new SqlParameter("@TrainerID_Fk", TrainerID_Fk);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@Action", "Chk_Existing");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_PT_TrainerIncentiveMaster", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Select All records from Department Master
        public DataTable Select_PTTrainerIncentiveMaster()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT_TrainerIncentiveMaster", param);
        }
        #endregion

        #region Insert record in Department Master Table
        public int Insert_PTTrainerIncentiveMaster()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Trainer_AutoID", Trainer_AutoID);
            param[1] = new SqlParameter("@TrainerID_Fk", TrainerID_Fk);
            param[2] = new SqlParameter("@Incentive", Incentive);
            param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[4] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[5] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_PT_TrainerIncentiveMaster", param);
        }
        #endregion

        #region Select record from Department Master by ID
        public DataTable SelectByID_PTTrainerIncentiveMaster()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Trainer_AutoID", Trainer_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_BY_ID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT_TrainerIncentiveMaster", param);
        }
        #endregion

        #region Select record from Department Master by ID
        public DataTable SelectInstructor()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SelectInstructor");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT_TrainerIncentiveMaster", param);
        }
        #endregion
    }
}
