using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace BusinessAccessLayer
{
    public class BalReportPackageInformation
    {
        DataConnection obDataCon = new DataConnection();

        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public string Action { get; set; }

        public object GetDetails_PackageInformationReport()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@Action", "GetPackageDetails");
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_PackageInformationReport", param);
        }
    }
}
