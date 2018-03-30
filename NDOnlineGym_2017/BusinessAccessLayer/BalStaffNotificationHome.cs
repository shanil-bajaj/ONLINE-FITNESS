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
    public class BalStaffNotificationHome
    {
        DataConnection obDataCon = new DataConnection();
        public int Notification_AutoID { get; set; }
        public string Notification { get; set; }
        public string StaffName { get; set; }
        public DateTime Date { get; set; }
        public int Staff_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
        #region Select All records from StaffNotificationHome
        public DataTable Select_StaffNotificationHome()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffNotificationHome", param);
        }
        #endregion

        #region Insert record in StaffNotificationHome Table
        public int Insert_StaffNotificationHome()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Notification_AutoID", Notification_AutoID);
            param[1] = new SqlParameter("@Notification", Notification);
            param[2] = new SqlParameter("@StaffName", StaffName);
            param[3] = new SqlParameter("@Date", Date);
            param[4] = new SqlParameter("@Staff_AutoID", Staff_AutoID);
            param[5] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[6] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[7] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_StaffNotificationHome", param);
        }
        #endregion

        #region Select record from StaffNotificationHome by ID
        public DataTable SelectByID_StaffNotificationHome()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Notification_AutoID", Notification_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_BY_ID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffNotificationHome", param);
        }
        #endregion

        #region Get Data Accorfing to Search Criteria
        public DataTable Select_DataAsPerSearchCriteriaBranch()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@StaffName", StaffName);
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[4] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffNotificationHome", param);
        }
        #endregion

        #region Select All records from StaffNotificationHome
        public DataTable Select_StaffNotificationHomePage()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Action", "SELECTStaffNotificationHome");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffNotificationHome", param);
        }
        #endregion
    }
}
