using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace BusinessAccessLayer
{
    public class BalPT
    {
        DataConnection obDataCon = new DataConnection();
        /// <summary>
        /// ---------------Days Master--------------
        /// </summary>
        public int Days_AutoID { get; set; }
        public string Days { get; set; }
        /// <summary>
        /// ----------------------Time Master---------
        /// </summary>
        public int Time_AutoID { get; set; }
        public string Time { get; set; }
        /// <summary>
        /// ---------------Incentive Master--------------
        /// </summary>
        public int Trainer_AutoID { get; set; }
        public int TrainerID_Fk { get; set; }
        public float Incentive { get; set; }

        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Member_AutoID { get; set; }
        public int Member_ID1 { get; set; }
        public string Contact { get; set; }

        public int Att_AutoID { get; set; }
       // public int Member_AutoID { get; set; }
        public int Instructor_AutoID { get; set; }
        public DateTime AttenDate { get; set; }
        public string InTime { get; set; }  
        public string OutTime { get; set; }
        public string AltInstructorName { get; set; }
        public string Note { get; set; }
        public int SessionCnt { get; set; }


        public DataTable BindDays()
        {
            SqlParameter[] param=new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_DaysMaster");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }

        public DataTable BindTime()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_TimeMaster");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }

        public DataTable BindTime_AvaibleInstructor()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_TimeMaster_AvaibleInstructor");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@TrainerID_Fk", TrainerID_Fk);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }

        public DataTable BindStaff()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_Staff");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }
        public int checkID_Exist_Not()
        {
            int res;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "checkID_Exist_Not");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            res = obDataCon.Int_StoredProcedure_Parameter("SP_PT", param);
            return res;
        }
        public DataTable Bindmember()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Bindmember");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }

        public DataTable Bindmember_Contact()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Bindmember_Contact");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Contact", Contact);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }

        public DataTable BindInstructor()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "BindInstructor");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@TrainerID_Fk", TrainerID_Fk);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);  
        }

        public DataTable GetStaffAutoid()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "GetStaffid");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@TrainerID_Fk", TrainerID_Fk);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);  
        }

        public DataTable session()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "session");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }

        public int insert_Prsent()
        {
            int res=0;
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Action", "present");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@TrainerID_Fk", TrainerID_Fk);
            param[5] = new SqlParameter("@AttenDate", AttenDate);
            param[6] = new SqlParameter("@InTime", InTime);
            param[7] = new SqlParameter("@OutTime", OutTime);
            param[8] = new SqlParameter("@AltInstructorName", AltInstructorName);
            param[9] = new SqlParameter("@Note", Note);
            param[10] = new SqlParameter("@SessionCnt", SessionCnt);
            res = obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_PT", param);
            return res;
        }

        //----------------------- Instructor Report---------------------

        public DataTable insstructorInfo()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "insstructorInfo");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@TrainerID_Fk", TrainerID_Fk);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT", param);
        }
    }
}
