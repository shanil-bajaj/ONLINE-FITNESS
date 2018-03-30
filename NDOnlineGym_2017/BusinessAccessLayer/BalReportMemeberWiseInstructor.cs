using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;
using DataAccessLayer;
using System.Data.SqlClient;
namespace BusinessAccessLayer
{
    public class BalReportMemeberWiseInstructor
    {
        DataConnection obDataCon = new DataConnection();
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Executive_ID { get; set; }

        public DataTable BindDataByDate()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ToDate", ToDate);
            param[1] = new SqlParameter("@FromDate", FromDate);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[4] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[5] = new SqlParameter("@Action", "DateWiseMember");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportMemberWiseInstructorAllc", param);
        }

        public DataTable BindDataIntructoeWise()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Executive_ID", Executive_ID);
            param[3] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[4] = new SqlParameter("@Action", "ExecutiveWiseMember");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportMemberWiseInstructorAllc", param);
        }

        public DataTable BindExecutive()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Executive_ID", Executive_ID);
            param[3] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[4] = new SqlParameter("@Action", "BindDDL");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportMemberWiseInstructorAllc", param);
        }
    }
}
