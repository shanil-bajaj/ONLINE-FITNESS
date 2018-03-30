using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;


namespace BusinessAccessLayer
{
    public class BalActiveDeactive
    {
        DataConnection obDataCon = new DataConnection(); 
        DataTable dt = new DataTable();

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public int Branch_ID { get; set; }
        public int Company_ID { get; set; }
        public string Action { get; set; }
        public string MemberName { get; set; }
        public int Member_ID { get; set; }
        public string Gender { get; set; }

       
        public DataTable Bind_gvActiveDeactive()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Status", Status);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Company_ID", Company_ID);
            param[3] = new SqlParameter("@Member_ID", Member_ID);
            param[4] = new SqlParameter("@MemberName", MemberName);
            param[5] = new SqlParameter("@Gender", Gender);
           param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ActiveDeactive", param);
        }


        public DataTable Bind_gvActiveDeactiveByDate()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ToDate", ToDate);
            param[1] = new SqlParameter("@FromDate", FromDate);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Company_ID", Company_ID);
            param[4] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ActiveDeactive", param);
        }
    }
}
