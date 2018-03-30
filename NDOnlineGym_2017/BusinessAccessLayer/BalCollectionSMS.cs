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
    public class BalCollectionSMS
    {
        #region ----------- Public property --------------

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TodayDate { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }

        DataConnection obDataCon = new DataConnection();

        #endregion 

        #region --------- Get Collection details ------------------
        public DataTable GetCollection()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@TodayDate", TodayDate);
            param[3] = new SqlParameter("@StartDate", StartDate);
            param[4] = new SqlParameter("@EndDate", EndDate);
            param[5] = new SqlParameter("@Action", Action);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CollectionSMS", param);
        }
        #endregion

        #region ---------------- Get Branch Details -------------- 
        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", Action);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CollectionSMS", param);
        }
        #endregion

        #region  --------------- Get login SMS Details -------------
        public DataSet GetSMSLoginDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", Action);

            return obDataCon.DataSet_StoredProcedure_Parameter("SP_CollectionSMS", param);
        }
        #endregion

    }
}
