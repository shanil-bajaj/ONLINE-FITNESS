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
    public class BalNewUpdateNotificationHome
    {
        DataConnection obDataCon = new DataConnection();

        public int SUpdate_AutoID { get; set; }
        public string Heading { get; set; }
        public string Image { get; set; }
        public string Information { get; set; }
        public DateTime Date { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }

        #region Select All records from NewUpdateNotificationHome
        public DataTable Select_NewUpdateNotificationHome()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_NewUpdateNotificationHome", param);
        }
        #endregion

        #region Insert record in NewUpdateNotificationHome Table
        public int Insert_NewUpdateNotificationHome()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@SUpdate_AutoID", SUpdate_AutoID);
            param[1] = new SqlParameter("@Heading", Heading);
            param[2] = new SqlParameter("@Image", Image);
            param[3] = new SqlParameter("@Information", Information);
            param[4] = new SqlParameter("@Date", Date);
            param[5] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_NewUpdateNotificationHome", param);
        }
        #endregion

        #region Select record from NewUpdateNotificationHome by ID
        public DataTable SelectByID_NewUpdateNotificationHome()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SUpdate_AutoID", SUpdate_AutoID);
            param[1] = new SqlParameter("@Action", "SELECT_BY_ID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_NewUpdateNotificationHome", param);
        }
        #endregion

        #region Get Data Accorfing to Search Criteria
        public DataTable Select_DataAsPerSearchCriteriaBranch()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Heading", Heading);
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_NewUpdateNotificationHome", param);
        }
        #endregion

        #region Select All records from NewUpdateNotificationHome
        public DataSet Select_UpdateNotificationHome()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "SELECTUpdateHome");
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_NewUpdateNotificationHome", param);
        }
        #endregion
    }
}
