using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;
using System.Data.SqlClient;
namespace BusinessAccessLayer
{
    public  class BalLoginForm
    {
        DataConnection obDataCon = new DataConnection();

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string Authority { get; set; }

        public int Staff_AutoID { get; set; }

        public string Rights { get; set; }

        public string ForgetPaaUsername { get; set; }

        public string Email { get; set; }

        public string Contact1 { get; set; }

        public int Company_AutoID { get; set; }

        public int Branch_AutoID { get; set; }

        public int Login_AutoID_fk { get; set; }

        public int Login_AutoID { get; set; }

        public object Mobile { get; set; }

        public  object NewPassword { get; set; }

        public string Status { get; set; }

        public DateTime TodayDate { get; set; }

        public string Status1 { get; set; }

        public string Action { get; set; }


        public bool chk_UserLogin()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Username", Username);
            param[1] = new SqlParameter("@Password", Password);
            //param[2] = new SqlParameter("@Status", Status);
            param[2] = new SqlParameter("@Action", "CHECK");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_LoginDetails", param);
            if (i > 0)
            { return true; }
            else
            { return false; }
        }

        public DataTable Bind_UserLogin()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Username", Username);
            param[1] = new SqlParameter("@Password", Password);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            //param[3] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Action", "CHECK_SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_LoginDetails", param);
        }

        public int Insert_LoginDetails()
        {
            SqlParameter[] param = new SqlParameter[4];
            //param[0] = new SqlParameter("@Username", Username);
           // param[1] = new SqlParameter("@Password", Password);
            param[0] = new SqlParameter("@Date", Date);
            param[1] = new SqlParameter("@Time", Time);
            param[2] = new SqlParameter("@Login_AutoID_fk", Login_AutoID_fk);
           // param[4] = new SqlParameter("@Staff_AutoID", Staff_AutoID);
           // param[4] = new SqlParameter("@Company_AutoID", Company_AutoID);
            //param[5] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            //param[6] = new SqlParameter("@Authority", Authority);
            param[3] = new SqlParameter("@Action", "Insert_LoginData");
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_LoginDetails", param);
        }

        public DataTable getUserDetailsByEmail()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Email", Email);
            param[1] = new SqlParameter("@Mobile", Mobile);
            param[2] = new SqlParameter("@Username", Username);
            param[3] = new SqlParameter("@Action", "GET_DETAILS_BY_EMAIL");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_LoginDetails", param);
        }

 
        public bool ChangePassword()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[1] = new SqlParameter("@NewPassword", NewPassword);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[4] = new SqlParameter("@Action", "ChangePassword");
            int i = obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_LoginDetails", param);
            if (i > 0)
            { return true; }
            else
            { return false; }
        }

        public int UpdateStatusUser()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TodayDate", TodayDate);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_ChangeStatus_User", param);
        }




        public DataTable LoginDetails()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Username", Username);
            //param[1] = new SqlParameter("@Password", Password);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            //param[3] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Action", "SearchByUsername");
            param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_LoginDetails", param);
        }

        public int UpdateStatusCourse()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TodayDate", TodayDate);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("ChangeStatusCourse_ActiveDeactive", param);
        }

        public int UpdateStatusMember()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TodayDate", TodayDate);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("ChangeStatusMember_ActiveDeactive", param);
        }

        public DataTable BindLogDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TodayDate", TodayDate);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_GetLogDetails", param);
        }
    }

}
