﻿using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Configuration;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalOccupationMaster
    {
        #region ------------Public Properties -----------

        public int Occupation_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }

        DataConnection obDataCon = new DataConnection();
        #endregion

        #region Check that Occupation Name is Exist or Not
        public bool Check_ExistingNameOccupationMaster()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Name", Name);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Action", "Chk_Existing");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_OccupationMaster", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Select All records from Occupation Master
        public DataTable Select_OccupationMaster()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_OccupationMaster", param);
        }
        #endregion

        #region Insert record in Occupation Master Table
        public int Insert_OccupationMaster()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Occupation_AutoID", Occupation_AutoID);
            param[1] = new SqlParameter("@Name", Name);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_OccupationMaster", param);
        }
        #endregion

        #region Select record from Occupation Master by ID
        public DataTable SelectByID_OccupationMaster()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Occupation_AutoID", Occupation_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_BY_ID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_OccupationMaster", param);
        }
        #endregion

    }
}
