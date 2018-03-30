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
    public class BalDesignationMaster
    {
        #region ------------Public Properties -----------

        public int Desig_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }

        DataConnection obDataCon = new DataConnection();
        #endregion

        #region Check that Designation Name is Exist or Not
        public bool Check_ExistingNameDesignationMaster()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Name", Name);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Action", "Chk_Existing");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_DesignationMaster", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Select All records from Designation Master
        public DataTable Select_DesignationMaster()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DesignationMaster", param);
        }
        #endregion

        #region Insert record in Designation Master Table
        public int Insert_DesignationMaster()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Desig_AutoID", Desig_AutoID);
            param[1] = new SqlParameter("@Name", Name);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_DesignationMaster", param);
        }
        #endregion

        #region Select record from Designation Master by ID
        public DataTable SelectByID_DesignationMaster()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Desig_AutoID", Desig_AutoID);
            param[1] = new SqlParameter("@Action", "SELECT_BY_ID");
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_DesignationMaster", param);
        }
        #endregion

    }
}