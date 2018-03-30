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
    public class BalUserResitration
    {
        DataConnection obDataCon = new DataConnection();

        public string name { get;set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string userename { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public string authority { get; set; }
        public string RegDate { get; set; }
        public string Action { get; set; }
        public int LogAutoId { get; set; }
        public int Staff_AutoID { get; set; }
        public int Branch_Id { get; set; }
        public int Company_Id { get; set; }
        public string Category { get; set; }
        public string searchTxt { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int Insert_UserRegistration()
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@name", name);
            param[1] = new SqlParameter("@mobile", mobile);
            param[2] = new SqlParameter("@email", email);
            param[3] = new SqlParameter("@userename", userename);
            param[4] = new SqlParameter("@password", password);
            param[5] = new SqlParameter("@status", status);
            param[6] = new SqlParameter("@authority", authority);
            param[7] = new SqlParameter("@LogAutoId", LogAutoId);
            param[8] = new SqlParameter("@Branch_Id", Branch_Id);
            param[9] = new SqlParameter("@Company_Id", Company_Id);
            param[10] = new SqlParameter("@Staff_AutoID", Staff_AutoID);
            param[11] = new SqlParameter("@RegDate", RegDate);
            param[12] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_UserRegistration", param);
        }

        public DataTable Select_All()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@authority", authority);
            param[1] = new SqlParameter("@LogAutoId", LogAutoId);
            param[2] = new SqlParameter("@Branch_Id", Branch_Id);
            param[3] = new SqlParameter("@Company_Id", Company_Id);
            param[4] = new SqlParameter("@Action", "Get_Username");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_UserRegistration", param);
        }



        public DataTable SelectByID_UserInformation()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_Id", Branch_Id);
            param[1] = new SqlParameter("@Company_Id", Company_Id);
            param[2] = new SqlParameter("@LogAutoId", LogAutoId);
            param[3] = new SqlParameter("@authority", authority);
            param[4] = new SqlParameter("@Action", "Select_By_Id");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_UserRegistration", param);
        }

        public int Delete()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_Id", Company_Id);
            param[1] = new SqlParameter("@Branch_Id", Branch_Id);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@LogAutoId", LogAutoId);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_UserRegistration", param);
        }

        public DataTable Get_Search()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Company_ID", Company_Id);
            param[3] = new SqlParameter("@Branch_ID", Branch_Id);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            param[5] = new SqlParameter("@authority", authority);
            param[6] = new SqlParameter("@FromDate", FromDate);
            param[7] = new SqlParameter("@ToDate", ToDate);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchUser", param);
        }

        public int Exits()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_ID", Company_Id);
            param[2] = new SqlParameter("@Branch_ID", Branch_Id);
            param[3] = new SqlParameter("@LogAutoId", LogAutoId);
            param[4] = new SqlParameter("@userename", userename);
            return obDataCon.Int_StoredProcedure_Parameter("SP_UserRegistration", param);
        }

        public DataTable Select_StaffForUserReg()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_Id", Branch_Id);
            param[1] = new SqlParameter("@Company_Id", Company_Id);
            param[2] = new SqlParameter("@Action", "Select_StaffForUserReg");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_UserRegistration", param);
        }
    }
}

