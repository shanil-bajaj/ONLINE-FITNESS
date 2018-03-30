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
    public class BalMemberNumericAttendance
    {
        #region ------------Public Properties -----------


        public int Attendance_AutoID { get; set; }
        public int Member_AutoID { get; set; }
        public DateTime AttndanceDate { get; set; }
        public DateTime TodayDate { get; set; }
        public DateTime AttndanceTime { get; set; }
        public string AttndanceStatus { get; set; }
        public DateTime CourseEndDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public int Balance { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Executive_ID { get; set; }
        public string Action { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Category { get; set; }

        DataConnection obDataCon = new DataConnection();

        #endregion

        #region Select All records from Branch Information
        public DataTable SELECT_AllMembers()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT_AllMembers");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }
        #endregion

        #region Select All records from Branch Information
        public DataTable SELECT_Member_ByAutoID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_Member_ByAutoID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }
        #endregion

        #region Select All records from Branch Information
        public DataTable SELECT_PresentCondition()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[3] = new SqlParameter("@TodayDate", TodayDate);
            param[4] = new SqlParameter("@Action", "AttendanceConditions");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }
        #endregion

        #region Select All records from Branch Information
        public DataSet SELECT_AttendanceDetails()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@TodayDate", TodayDate);
            param[3] = new SqlParameter("@Action", "SELECT_AttendanceDetails");
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }
        #endregion

        public DataSet SELECT_AttendanceDetails_ForBiomatric()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@TodayDate", TodayDate);
            param[3] = new SqlParameter("@Action", "SELECT_AttendanceDetails_ForBiomatric");
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }

        #region Select All records from Branch Information
        public DataTable SELECT_CourseDetails()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_CourseDetails");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }
        #endregion

        #region Insert record in Branch Information Table
        public int Insert_attendance()
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[1] = new SqlParameter("@AttndanceDate", AttndanceDate);
            param[2] = new SqlParameter("@AttndanceTime", AttndanceTime);
            param[3] = new SqlParameter("@AttndanceStatus", AttndanceStatus);
            param[4] = new SqlParameter("@CourseEndDate", CourseEndDate);
            param[5] = new SqlParameter("@NextPaymentDate", NextPaymentDate);
            param[6] = new SqlParameter("@Balance", Balance);
            param[7] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[8] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[9] = new SqlParameter("@Executive_ID", Executive_ID);
            param[10] = new SqlParameter("@Attendance_AutoID", Attendance_AutoID);
            param[11] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }
        #endregion

        #region Insert record in Branch Information Table
        public int Delete_attendance()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Attendance_AutoID", Attendance_AutoID);
            param[3] = new SqlParameter("@Action", "Delete");
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemNumericAttendance", param);
        }
        #endregion

        #region Select record for Membership Followup Details
        public DataTable Search_AttendanceDetails()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@AttndanceStatus", AttndanceStatus);
            Param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            Param[4] = new SqlParameter("@FromDate", FromDate);
            Param[5] = new SqlParameter("@ToDate", ToDate);
            Param[6] = new SqlParameter("@Category", Category);
            Param[7] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchMemberAttendance", Param);
        }
        #endregion
    }
}
