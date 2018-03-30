﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Configuration;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalPTTimeMaster
    {
        #region ------------Public Properties -----------

        public int Time_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public string Time { get; set; }
        public string Action { get; set; }

        DataConnection obDataCon = new DataConnection();
        #endregion

        #region Check that Department Name is Exist or Not
        public bool Check_ExistingTimePT_TimeMaster()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Time", Time);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Action", "Chk_Existing");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_PT_TimeMaster", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Select All records from Department Master
        public DataTable Select_PT_TimeMaster()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT_TimeMaster", param);
        }
        #endregion

        #region Insert record in Department Master Table
        public int Insert_PT_TimeMaster()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Time_AutoID", Time_AutoID);
            param[1] = new SqlParameter("@Time", Time);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_PT_TimeMaster", param);
        }
        #endregion

        #region Select record from Department Master by ID
        public DataTable SelectByID_PT_TimeMaster()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Time_AutoID", Time_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_BY_ID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_PT_TimeMaster", param);
        }
        #endregion


    }
}
