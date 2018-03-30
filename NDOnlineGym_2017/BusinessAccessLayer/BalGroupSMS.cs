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
    public class BalGroupSMS
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
    
        #region ---------- Check SMS Status -------------
        public DataTable GetSMSLoginDatails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_GroupSMS", param);
        }
        #endregion

        #region ---------- Get Details ----------
        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@TodayDate", TodayDate);
            param[3] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_GroupSMS", param);
        }
        #endregion

        #region ------------ Get Template --------------
        public DataTable GetTemplate()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_GroupSMS", param);
        }
        #endregion

        #region ---------------- Get SMS Login Details -------------------
        public DataSet GetSMSLoginDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_SendSMS", param);
        }
        #endregion


        public DataSet GetAllDetails()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@TodayDate", TodayDate);
            param[3] = new SqlParameter("@Action", Action);

            return obDataCon.DataSet_StoredProcedure_Parameter("SP_GroupSMS", param);
        }

    }
}
