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
    public class BalFreezingMember
    {

        #region ------------Public Properties -----------
        
        public string SearchByText { get; set; }

        public int Member_AutoID { get; set; }
        public int Executive_ID { get; set; }
        public int Member_ID1 { get; set; }
        public int Freezing_AutoID { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }


        public int Course_AutoID { get; set; }
        public int FreezingDays { get; set; }
        public string FreezingReason { get; set; } 

        public DateTime? TodayDate { get; set; }
        public DateTime? FreezingStartDate { get; set; }
        public DateTime? FreezingEndDate { get; set; }
        public DateTime? CourseStartDate { get; set; }
        public DateTime? CourseOldEndDate { get; set; }
        public DateTime? CourseNewEndDate { get; set; }
        public DateTime? FreezingDate { get; set; }

        public DateTime? MStartDate { get; set; }
        public DateTime? MEndDate { get; set; }

        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }

        DataConnection obDataCon = new DataConnection();

        #endregion

        #region Get Memeber Details By Member Id And Contact Number

        public DataTable GetMemeberDetails()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_ID1", Member_ID1);
            param[4] = new SqlParameter("@Contact1", Contact1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberFreezing", param);
        }

        #endregion

        #region Select Course By Member Auto Id

        public DataTable Select_CoursePackage()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", "CourseMemeberAutoID");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@TodayDate", TodayDate);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberFreezing", param);
        }

        #endregion


        public DataTable GetExecutive()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "ExecutiveName");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberFreezing", param);
        }

        public int Insert_FreezingInformation()
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@Action", Action);            
            param[1] = new SqlParameter("@FreezingDate", FreezingDate);
            param[2] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[3] = new SqlParameter("@Course_AutoID", Course_AutoID);
            param[4] = new SqlParameter("@CourseStartDate", CourseStartDate);
            param[5] = new SqlParameter("@CourseOldEndDate", CourseOldEndDate);
            param[6] = new SqlParameter("@CourseNewEndDate", CourseNewEndDate);
            param[7] = new SqlParameter("@FreezingDays", FreezingDays);
            param[8] = new SqlParameter("@FreezingReason", FreezingReason);
            param[9] = new SqlParameter("@FreezingStartDate", FreezingStartDate);
            param[10] = new SqlParameter("@FreezingEndDate", FreezingEndDate);
            param[11] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[12] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[13] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[14] = new SqlParameter("@Executive_ID", Executive_ID);
            param[15] = new SqlParameter("@TodayDate", TodayDate);
            param[16] = new SqlParameter("@Freezing_AutoID", Freezing_AutoID);

            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemberFreezing", param);
        }

        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[8];                                 
            param[0] = new SqlParameter("@Freezing_AutoID", Freezing_AutoID);
            param[1] = new SqlParameter("@MStartDate", MStartDate);
            param[2] = new SqlParameter("@MEndDate", MEndDate);
            param[3] = new SqlParameter("@SearchByText", SearchByText);
            param[4] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[5] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[6] = new SqlParameter("@Category", Category);
            param[7] = new SqlParameter("@Action", Action);


            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberFreezing", param);
        }

        public DataSet GetFreezingDetailsByCourseID()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Course_AutoID", Course_AutoID);
            param[4] = new SqlParameter("@Freezing_AutoID", Freezing_AutoID);

            return obDataCon.DataSet_StoredProcedure_Parameter("SP_MemberFreezing", param);
        }


        public bool Check_AllReadyFreezedByMemberAutoId()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@TodayDate", TodayDate);
            param[4] = new SqlParameter("@Course_AutoID", Course_AutoID);
            param[5] = new SqlParameter("@Action", Action);

            int i = obDataCon.Int_StoredProcedure_Parameter("SP_MemberFreezing", param);
            if (i > 0)
                return true;
            else
                return false;
        }


        public DataTable GetMember_ID1_ByMemberAutoID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "GetMember_ID1_ByMemberAutoID");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberFreezing", param);
        }


        
    }
}
