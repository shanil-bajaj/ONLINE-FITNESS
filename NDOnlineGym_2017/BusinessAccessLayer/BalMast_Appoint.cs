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
  public class BalMast_Appoint
    {
        public int Appoint_AutoID { get; set; }
        public int Appoint_ID { get; set; }
        public int branch_ID { get; set; }
        public int Comp_ID { get; set; }
        public int Session { get; set; }
        public string Appoint_Type { get; set; }
        public DateTime Time { get; set; }
        public int Ammount { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public string Action { get; set; }

        DataConnection obDataCon = new DataConnection();

        public int Insert_Appoint()
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Appoint_AutoID", Appoint_AutoID);
            param[1] = new SqlParameter("@Action", Action);
            param[2] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[3] = new SqlParameter("@Company_AutoID", Comp_ID);
            param[4] = new SqlParameter("@Session",Session);
            param[5] = new SqlParameter("@type", Appoint_Type);
            param[6] = new SqlParameter("@time", Time);
            param[7] = new SqlParameter("@color", Color);
            param[8] = new SqlParameter("@Status", Status);
            param[9] = new SqlParameter("@Ammount", Ammount);
            param[10]=new SqlParameter("@Appoint_ID",Appoint_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_AppointMaster", param);
        }


        public DataTable Select_AppointID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[2] = new SqlParameter("@Company_AutoID", Comp_ID);
            param[3] = new SqlParameter("@Appoint_AutoID", Appoint_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AppointMaster", param);
        }

        #region Get_AppointID
        public DataTable Get_AppointID()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "Get_AppointID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AppointMaster", param);
        }
        #endregion 

        public DataTable BindGrid()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Bind");
            param[1] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[2] = new SqlParameter("@Company_AutoID", Comp_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AppointMaster", param);
        }

        public int Delete_Appoint()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Appoint_AutoID", Appoint_AutoID);
            param[1] = new SqlParameter("@Action", Action);
            param[2] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[3] = new SqlParameter("@Company_AutoID", Comp_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_AppointMaster", param);
        }

      
     

    }
}
