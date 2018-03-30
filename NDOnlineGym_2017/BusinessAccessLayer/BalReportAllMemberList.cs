﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalReportAllMemberList
    {
        #region
        DataConnection obDataCon = new DataConnection();

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
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportAllMemberList", param);
        }

    }
}