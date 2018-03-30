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
    public class BalEnquiryFollowup
    {
        DataConnection obDataCon = new DataConnection();
        public int Enq_ID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int CallResponse_AutoID { get; set; }
        public int FollowupType_AutoID { get; set; }
        public string Contact { get; set; }

        public int Login_AutoID { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
        public DateTime? NextFollowupTime { get; set; }

        public string Category { get; set; }
        public DateTime? FollowupDate { get; set; }
        public DateTime? NextFollowupDate { get; set; }
        public DateTime? FollowupTime{ get; set; }
        public int EnqFollowup_AutoID { get; set; }
        public string Action { get; set; }
        //public string Action { get; set; }
        public string SearchByText { get; set; }
        public int Executive_ID { get; set; }

        public DataTable EnryInfoById()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Enq_ID", Enq_ID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_BY_Enq_ID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryFollowup", param);
        }

        public DataTable EnryInfoById1()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Enq_ID", Enq_ID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Action", "SELECT_BY_Enq_ID1");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryFollowup", param);
        }

        public int Insert_EnquiryFollowupInformation()
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@FollowupDate", FollowupDate);
            param[1] = new SqlParameter("@FollowupTime", FollowupTime);
            param[2] = new SqlParameter("@CallResponse_AutoID", CallResponse_AutoID);
            param[3] = new SqlParameter("@Comment", Comment);
            param[4] = new SqlParameter("@Rating", Rating);
            param[5] = new SqlParameter("@NextFollowupDate", NextFollowupDate);
            param[6] = new SqlParameter("@NextFollowupTime", NextFollowupTime);
           // para8[7] = new SqlParameter("@FollowupType_AutoID", FollowupType_AutoID);
            param[7] = new SqlParameter("@Enq_ID", Enq_ID);
            param[8] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[9] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[10] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[11] = new SqlParameter("@EnqFollowup_AutoID", EnqFollowup_AutoID);
            param[12] = new SqlParameter("@Executive_ID", Executive_ID);
            param[13] = new SqlParameter("@FollowupType_AutoID", FollowupType_AutoID);
            param[14] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_EnquiryFollowup", param);
        }

        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@SearchByText", SearchByText);
            //param[3] = new SqlParameter("@Enq_ID", Enq_ID);
            param[3] = new SqlParameter("@EnqFollowup_AutoID", EnqFollowup_AutoID);
            param[4] = new SqlParameter("@Enq_ID", Enq_ID);
            param[5] = new SqlParameter("@Contact", Contact);
            param[6] = new SqlParameter("@Category", Category);
            param[7] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryFollowup", param);
        }

        #region Select All records from FollowupType Master
        public DataTable Select_FollowupTypeMaster()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT_FollowupType");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnquiryFollowup", param);
        }
        #endregion
    }
}
