using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAccessLayer;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{    
    public class BalEnquiryToEnroll
    {
        DataConnection obDataCon = new DataConnection();
        DataTable dt = new DataTable();

        public int Branch_ID { get; set; }
        public int Company_ID { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Action { get; set; }
        public string Category { get; set; }
        public string searchTxt { get; set; }

        public DataTable Bind_GV()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@ToDate", ToDate);
            param[3] = new SqlParameter("@FromDate", FromDate);
            param[4] = new SqlParameter("@Action", Action);
            //param[5] = new SqlParameter("@Category", Category);
            //param[6] = new SqlParameter("@searchTxt", searchTxt);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryToEnroll", param);
        }

        public DataTable GetsearchBy()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            //param[2] = new SqlParameter("@ToDate", ToDate);
            //param[3] = new SqlParameter("@FromDate", FromDate);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@Category", Category);
            param[4] = new SqlParameter("@searchTxt", searchTxt);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryToEnroll", param);
        }


        public DataTable Bind_GVWithJoiningDate()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@ToDate", ToDate);
            param[3] = new SqlParameter("@FromDate", FromDate);
            param[4] = new SqlParameter("@Action", Action);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryToEnroll", param);
        }

        public DataTable Bind_GVWtDateAndCategory()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@ToDate", ToDate);
            param[3] = new SqlParameter("@FromDate", FromDate);
            param[4] = new SqlParameter("@Action", Action);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@searchTxt", searchTxt);

            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryToEnroll", param);
        }
    }
}
