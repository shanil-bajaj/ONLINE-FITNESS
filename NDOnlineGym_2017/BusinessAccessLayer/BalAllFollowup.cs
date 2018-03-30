using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalAllFollowup
    {
        DataConnection obDataCon = new DataConnection();
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Member_AutoID { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
        public string SearchText { get; set; }
        public string DateCategory { get; set; }
        public int Pack_AutoID { get; set; }
        public string FollowupType { get; set; }
        public int FollowupType_AutoID { get; set; }

        #region Select record for Balance Payment Followup Details
        public DataTable SearchPaymentFollowup()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@FromDate", FromDate);
            Param[1] = new SqlParameter("@ToDate", ToDate);
            Param[2] = new SqlParameter("@Category", Category);
            Param[3] = new SqlParameter("@SearchText", SearchText);
            Param[4] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[5] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[6] = new SqlParameter("@DateCategory", DateCategory);
            Param[7] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchExistingFollowup", Param);

        }
        #endregion
    }
}
