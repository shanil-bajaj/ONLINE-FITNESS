using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalReportEnquiryFollowup
    {
        DataConnection obDataCon = new DataConnection();
        DataTable dt = new DataTable();

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int Branch_ID { get; set; }
        public int Company_ID { get; set; }

        public string Category { get; set; }
        public string Action { get; set; }
        public int Executive_ID { get; set; }

        public DataTable SearchByDate()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@ToDate", ToDate);
            param[3] = new SqlParameter("@FromDate", FromDate);
            param[4] = new SqlParameter("@Action", "SearchByDate");

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportEnqFollowup", param);
        }

        public DataTable BindDataIntructoeWise()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_ID",Branch_ID);
            param[1] = new SqlParameter("@Company_ID",Company_ID);
            //param[2] = new SqlParameter("@ToDate", ToDate);
            //param[3] = new SqlParameter("@FromDate", FromDate);
            //param[3] = new SqlParameter("@FromDate", FromDate);
            param[2] = new SqlParameter("@Executive_ID",Executive_ID);
            param[3] = new SqlParameter("@Action", "SearchByExecutive");

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportEnqFollowup", param);
        }

        public DataTable SearchByNextFlwDate()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@ToDate", ToDate);
            param[3] = new SqlParameter("@FromDate", FromDate);
            param[4] = new SqlParameter("@Action", "SearchByNextFlwDate");

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportEnqFollowup", param);
        }
    }
}
