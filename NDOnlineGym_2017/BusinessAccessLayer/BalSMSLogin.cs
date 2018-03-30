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
    public class BalSMSLogin
    {
        #region

        DataConnection obDataCon = new DataConnection();

        public int SMS_AutoID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Sender_ID { get; set; }
        public string Route { get; set; }
        public string Status { get; set; }
        public string AutoStatus { get; set; }
        public string SMSWithName { get; set; }
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }

        public string Action { get; set; }

        #endregion       
      
        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SMSLogin", param);
        }


        public int Insert_CompanyInformation()
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Username", Username);
            param[1] = new SqlParameter("@Password", Password);
            param[2] = new SqlParameter("@Sender_ID", Sender_ID);
            param[3] = new SqlParameter("@Route", Route);
            param[4] = new SqlParameter("@Status", Status);
            param[5] = new SqlParameter("@AutoStatus", AutoStatus);
            param[6] = new SqlParameter("@SMSWithName", SMSWithName);
            param[7] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[8] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[9] = new SqlParameter("@SMS_AutoID", SMS_AutoID);
            param[10] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_SMSLogin", param);
        }
    }
}
