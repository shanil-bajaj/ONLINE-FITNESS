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
    public class BalAssignWorkoutToMember
    {
        #region
        DataConnection obDataCon = new DataConnection();

        public int Workout_AutoID { get; set; }
        public int Workout_ID1 { get; set; }
        public int Member_AutoID { get; set; }
        public int Member_ID { get; set; }
        public int WorkoutName_AutoID { get; set; }        
        public int Programmer_AutoID { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string WorkDay { get; set; }
        public string Sets { get; set; }
        public string Reps { get; set; }
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }

        public string Contact { get; set; }        
        public string SearchByText { get; set; }

        public string Category { get; set; }
        public string Action { get; set; }

        #endregion


        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@SearchByText", SearchByText);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@Member_ID", Member_ID);
            param[5] = new SqlParameter("@Contact", Contact);
            param[6] = new SqlParameter("@FromDate", FromDate);
            param[7] = new SqlParameter("@ToDate", ToDate);
            param[8] = new SqlParameter("@Category", Category);
            param[9] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AddWorkout", param);
        }

        public int Insert_WorkoutInformation()
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@Workout_AutoID", Workout_AutoID);
            param[1] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[2] = new SqlParameter("@WorkoutName_AutoID", WorkoutName_AutoID);                             
            param[3] = new SqlParameter("@Programmer_AutoID", Programmer_AutoID);
            param[4] = new SqlParameter("@AssignDate", AssignDate);
            param[5] = new SqlParameter("@FromDate", FromDate);
            param[6] = new SqlParameter("@ToDate", ToDate);
            param[7] = new SqlParameter("@WorkDay", WorkDay);
            param[8] = new SqlParameter("@Sets", Sets);
            param[9] = new SqlParameter("@Reps", Reps);
            param[10] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[11] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[12] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_AddWorkout", param);
        }

        //public int Exits()
        //{
        //    SqlParameter[] param = new SqlParameter[5];
        //    //param[0] = new SqlParameter("@WorkoutPlan_AutoID", WorkoutPlan_AutoID);
        //    //param[1] = new SqlParameter("@Exercise_AutoID", Exercise_AutoID);
        //    param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
        //    param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
        //    param[4] = new SqlParameter("@Action", Action);

        //    return obDataCon.Int_StoredProcedure_Parameter("SP_UserRegistration", param);
        //}
    }
}
