using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccessLayer;

namespace BusinessAccessLayer
{
   public class BalDietPlan
    {
        public int Executive_ID { get; set; }
        public int Diet_ID { get; set; }
        public int Comp_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Login_ID { get; set; }
        public int Member_ID { get; set; }
        public int Member_AutoID { get; set; }
        public string Contact1 { get; set; }
        public string SearchText { get; set; }
        public DateTime Diet_Date { get; set; }
        public string Meal1 { get; set; }        
        public DateTime? Time1 { get; set; }
        public string Meal1Sta { get; set; }
        public string Meal2 { get; set; }
        public DateTime? Time2 { get; set; }
        public string Meal2Sta { get; set; }
        public string Meal3 { get; set; }
        public DateTime? Time3 { get; set; }
        public string Meal3Sta { get; set; }
        public string Meal4 { get; set; }
        public DateTime? Time4 { get; set; }
        public string Meal4Sta { get; set; }
        public string Meal5 { get; set; }
        public DateTime? Time5 { get; set; }
        public string Meal5Sta { get; set; }
        public string Meal6 { get; set; }
        public DateTime? Time6 { get; set; }
        public string Meal6Sta { get; set; }
        public string status { get; set; }
        public string Dietatian { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public string Avoid { get; set; }
        public string Action { get; set; }
        public string PreHistory { get; set; }
        public string Category { get; set; }

        DataConnection obDataCon = new DataConnection();

        public DataTable Select_MemberID()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action",Action);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Comp_ID", Comp_ID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@Member_ID1", Member_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DietPlan", param);
        }


        public DataTable Select_MemberContact()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Comp_ID", Comp_ID);
            param[3] = new SqlParameter("@Contact1", Contact1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DietPlan", param);
        }

        public DataTable BindGrid()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Comp_ID", Comp_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DietPlan", param);

        }

        public DataTable Select_Member()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Select all");
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Comp_ID", Comp_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DietPlan", param);
        }

        public int Delete_Diet()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Diet_ID", Diet_ID);
            param[1] = new SqlParameter("@Action", Action);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Comp_ID", Comp_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_DietPlan", param);
        }

        #region Select record from  by ID
        public DataTable SelectByID_Diet_Information()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Diet_ID", Diet_ID);
            param[1] = new SqlParameter("@Action", Action);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Comp_ID", Comp_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DietPlan", param);
        }
        #endregion

        public int Insert_DietPlan()
        {
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@Comp_ID", Comp_ID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Login_ID", Login_ID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@date", Diet_Date);
            param[5] = new SqlParameter("@Meal1", Meal1);
            param[6] = new SqlParameter("@Meal2", Meal2);
            param[7] = new SqlParameter("@Meal3", Meal3);
            param[8] = new SqlParameter("@Meal4", Meal4);
            param[9] = new SqlParameter("@Meal5", Meal5);
            param[10] = new SqlParameter("@Meal6", Meal6);
            param[11] = new SqlParameter("@Time1", Time1);
            param[12] = new SqlParameter("@Time2", Time2);
            param[13] = new SqlParameter("@Time3", Time3);
            param[14] = new SqlParameter("@Time4", Time4);
            param[15] = new SqlParameter("@Time5", Time5);
            param[16] = new SqlParameter("@Time6", Time6);
            param[17] = new SqlParameter("@Meal1Sta", Meal1Sta);
            param[18] = new SqlParameter("@Meal2Sta", Meal2Sta);
            param[19] = new SqlParameter("@Meal3Sta", Meal3Sta);
            param[20] = new SqlParameter("@Meal4Sta", Meal4Sta);
            param[21] = new SqlParameter("@Meal5Sta", Meal5Sta);
            param[22] = new SqlParameter("@Meal6Sta", Meal6Sta);
            param[23] = new SqlParameter("@Dietatian", Dietatian);
            param[24] = new SqlParameter("@ToDate", ToDate);
            param[25] = new SqlParameter("@FromDate", FromDate);
            param[26] = new SqlParameter("@Avoid", Avoid);
            param[27] = new SqlParameter("@Action", Action);
            param[28] = new SqlParameter("@PreHistory", PreHistory);
            param[29] = new SqlParameter("@Diet_ID", Diet_ID);
            param[30] = new SqlParameter("@Executive_ID", Executive_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_DietPlan", param);
        }

          #region Gridview Search
        public DataTable SearchCategory()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@SearchText", SearchText);
            param[3] = new SqlParameter("@FromDate", FromDate);
            param[4] = new SqlParameter("@ToDate", ToDate);
            param[5] = new SqlParameter("@Branch_ID", Branch_ID);
            param[6] = new SqlParameter("@Comp_ID", Comp_ID);
            // param[3] = new SqlParameter("@Diet_ID", Diet_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DietPlan", param);
        }
         #endregion

    }
}
