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
    public class BalSendSMS
    {
        #region Public Property

        DataConnection obDataCon = new DataConnection();

        public string MemberName { get; set; }
        public int Member_ID1 { get; set; }
        public int Branch_AutoID { get; set; }
        public string Contact1 { get; set; }
        public int Company_AutoID { get; set; }
        public string Status { get; set; }
        public string Rating { get; set; }
        public string Gender { get; set; }

        public string Action { get; set; }

        #endregion

        #region ------------- Get Member Details ------------
        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Member_ID1", Member_ID1);
            param[3] = new SqlParameter("@Status", Status);
            param[4] = new SqlParameter("@Rating", Rating);
            param[5] = new SqlParameter("@Gender", Gender);
            param[6] = new SqlParameter("@Contact1", Contact1);
            param[7] = new SqlParameter("@Action", Action);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SendSMS", param);
        }
        #endregion

        #region ------------- Check SMS Status Details ------------------
        public DataTable Get_SMSLogin_Datails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SendSMS", param);
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

    }
}
