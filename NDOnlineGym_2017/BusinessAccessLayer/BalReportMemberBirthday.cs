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
    public class BalReportMemberBirthday
    {    
        DataConnection obDataCon = new DataConnection();

        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public int BirthMonth { get; set; }
        public SqlDateTime StartDate { get; set; }
        public SqlDateTime EndDate { get; set; }

        public object GetDetails_MemberBirthdayReport()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@StartDate", StartDate);
            param[3] = new SqlParameter("@EndDate", EndDate);
            param[4] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[5] = new SqlParameter("@Action", "BirthdayBetweenDate");
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_MemberBirthdayReports", param);
        }

        public object GetDetailsOfMonth_MemberBirthdayReport()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@BirthMonth", BirthMonth);
            param[4] = new SqlParameter("@Action", "BirthdayOfMonth");
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_MemberBirthdayReports", param);
        }
           
    }
}
