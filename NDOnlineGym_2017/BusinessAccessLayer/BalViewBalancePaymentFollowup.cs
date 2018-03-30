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
    public class BalViewBalancePaymentFollowup
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
        public DataTable Search()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@FromDate", FromDate);
            Param[1] = new SqlParameter("@ToDate", ToDate);
            Param[2] = new SqlParameter("@Category", Category);
            Param[3] = new SqlParameter("@SearchText", SearchText);
            Param[4] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[5] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BalPayFollowupDetails", Param);

        }
        #endregion

        #region Select record for Enquiry Followup Details
        public DataTable SearchEnqFollowup()
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
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_EnqFollowupDetails", Param);

        }
        #endregion

        #region Bind Package Dropdown
        public DataTable BindDDLPackage()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[3];
            Param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[2] = new SqlParameter("@Action", "SelectPackage");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemEndFollowupDetails", Param);
        }
        #endregion

        #region Select record for Member End Followup Details
        public DataTable SearchMemEndFollowup()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[8];
            Param[0] = new SqlParameter("@FromDate", FromDate);
            Param[1] = new SqlParameter("@ToDate", ToDate);
            Param[2] = new SqlParameter("@SearchText", SearchText);
            Param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[4] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[5] = new SqlParameter("@DateCategory", DateCategory);
            Param[6] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            Param[7] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemEndFollowupDetails", Param);

        }
        #endregion

        #region Select record for Membership Followup Details
        public DataTable SearchMemshipFollowup()
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
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MembershipFollowupDetails", Param);
        }
        #endregion

        #region Select record for Membership Followup Details
        public DataTable SelectFollDetails_By_MemAutoID()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[5];
            Param[0] = new SqlParameter("@FollowupType_AutoID", FollowupType_AutoID);
            Param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            Param[4] = new SqlParameter("@Action", "SelectFollDetails_By_MemAutoID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BalPayFollowupDetails", Param);
        }
        #endregion

        #region Select record for Membership Followup Details
        public DataTable SelectFollDetails_By_FollowupType()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[6];
            Param[0] = new SqlParameter("@FollowupType", FollowupType);
            Param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[3] = new SqlParameter("@FromDate", FromDate);
            Param[4] = new SqlParameter("@ToDate", ToDate);
            Param[5] = new SqlParameter("@Action", "SelectFollDetails_By_FollowupType");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BalPayFollowupDetails", Param);
        }
        #endregion

        #region Select record for Balance Payment Followup Details
        public DataTable SearchUpgrade()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[7];
            Param[0] = new SqlParameter("@FromDate", FromDate);
            Param[1] = new SqlParameter("@ToDate", ToDate);
            Param[2] = new SqlParameter("@Category", Category);
            Param[3] = new SqlParameter("@SearchText", SearchText);
            Param[4] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            Param[5] = new SqlParameter("@Company_AutoID", Company_AutoID);
            Param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_UpgradeFollowupDetails", Param);

        }
        #endregion
    }
}
