using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;


namespace BusinessAccessLayer
{
    public class BalCourtBooking
    {
        #region----Prperties-------

        DataConnection obDataCon = new DataConnection();
        DataTable dt = new DataTable();
        
        public int Slot_AutoID { get; set; }
        public int CourtMaster_AutoID { get; set; }
        public string  StartTime { get; set; }
        public string  EndTime { get; set; }
        public String Member_Price { get; set; }
        public String NonMember_price { get; set; }
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Member_AutoID { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Contact1 { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Action { get; set; }

        
        

        #endregion

        public int Insert_Update_Delete()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID",Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID",Company_AutoID);
            param[2] = new SqlParameter("@Name", Name);
            param[3] = new SqlParameter("@CourtMaster_AutoID",CourtMaster_AutoID);
            param[4] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_CourtBooking", param);
        }

        public DataTable BindGV()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "BindGvTSBMaster");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourtBooking", param);
        }

        public DataTable Get_Edit()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "BindGvTSBMaster");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourtBooking", param);
        }

        public int Insert_Update_DeleteTimeAndPrice()
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Branch_AutoID",Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID",Company_AutoID);
            param[2] = new SqlParameter("@StartTime",StartTime);
            param[3] = new SqlParameter("@EndTime",EndTime);
            param[4] = new SqlParameter("@Action",Action);
            param[5] = new SqlParameter("@Member_Price",Member_Price);
            param[6] = new SqlParameter("@NonMember_price",NonMember_price);
            param[7] = new SqlParameter("@CourtMaster_AutoID", CourtMaster_AutoID);
            param[8] = new SqlParameter("@Slot_AutoID",Slot_AutoID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_CourtBooking", param);
        }

        public DataTable Get_CourtAutoID()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "GetCourtID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourtBooking", param);
        }

        public DataTable GetByMemberID()
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[3] = new SqlParameter("@FName", FName);
            param[4] = new SqlParameter("@LName", LName);
            param[5] = new SqlParameter("@Contact1", Contact1);
            param[6] = new SqlParameter("@Email", Email);
            param[7] = new SqlParameter("@Gender", Gender);
            param[8] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Measurement", param);
        }

        public DataTable GetMemberAutoID()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "GetMemberAutoID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourtBooking", param);
        }
    }
}
