using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalFollowup
    {
        #region ------------Public Properties -----------

        DataConnection obDataCon = new DataConnection();

        public string SearchText { get; set; }
        public string DateCategory { get; set; }
        public int Member_ID { get; set; }
        public int Executive_ID { get; set; }
        public string Contact { get; set; }
        public string CallRespond_AutoID { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }

        public DateTime? FollowupTime { get; set; }
        public DateTime? NextFollowupTime { get; set; }
        public DateTime? FollowupDate { get; set; }
        public DateTime? NextFollowupDate { get; set; }

        public int Followup_AutoID { get; set; }
        public int FollowupType_AutoID { get; set; }
        public int Member_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }       
        public int Login_AutoID { get; set; }

        public string SearchByText { get; set; }

        public string Category { get; set; }
        public string Action { get; set; }

        public string Search { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        #endregion

        public DataTable Search1()
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
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_ReportAllFollowup", Param);

        }

        #region ----------- Get Details ---------------
        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@SearchByText", SearchByText);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@Followup_AutoID", Followup_AutoID);
            param[5] = new SqlParameter("@Member_ID", Member_ID);
            param[6] = new SqlParameter("@Contact", Contact);
            param[7] = new SqlParameter("@Category", Category);
            param[8] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Followup", param);
        }
        #endregion

        #region ------------ Insert, Update, Delete -----------------
        public int Insert_FollowupInformation()
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@FollowupDate", FollowupDate);
            param[1] = new SqlParameter("@FollowupTime", FollowupTime);
            param[2] = new SqlParameter("@CallRespond_AutoID", CallRespond_AutoID);
            param[3] = new SqlParameter("@Comment", Comment);
            param[4] = new SqlParameter("@Rating", Rating);
            param[5] = new SqlParameter("@NextFollowupDate", NextFollowupDate);
            param[6] = new SqlParameter("@NextFollowupTime", NextFollowupTime);
            param[7] = new SqlParameter("@FollowupType_AutoID", FollowupType_AutoID);
            param[8] = new SqlParameter("@Member_AutoID", Member_AutoID);            
            param[9] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[10] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[11] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[12] = new SqlParameter("@Followup_AutoID", Followup_AutoID);
            param[13] = new SqlParameter("@Executive_ID", Executive_ID);
            param[14] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Followup", param);
        }
        #endregion

        //#region Select All records from FollowupType Master
        //public DataTable Select_FollowupTypeEnquiry()
        //{
        //    SqlParameter[] param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
        //    param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
        //    param[2] = new SqlParameter("@Action", "SELECT_FollowupType_Enquiry");
        //    return obDataCon.DataTable_StoredProcedure_Parameter("SP_Followup", param);
        //}
        //#endregion

        #region Select All records from FollowupType MembershipEnd
        public DataTable SELECT_FollowupType_MembershipEnd()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT_FollowupType_MembershipEnd");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Followup", param);
        }
        #endregion

        #region Select All records from FollowupType Upgrade
        public DataTable SELECT_FollowupType_Upgrade()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT_FollowupType_Upgrade");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Followup", param);
        }
        #endregion

        #region Select All records from FollowupType Payment
        public DataTable SELECT_FollowupType_Payment()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT_FollowupType_Payment");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Followup", param);
        }
        #endregion

        #region Select All records from FollowupType Measurement
        public DataTable SELECT_FollowupType_Measurement()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT_FollowupType_Measurement");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Followup", param);
        }
        #endregion

        #region Select All records from Followup
        public DataTable SELECT_Followup()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@FromDate", FromDate);
            param[3] = new SqlParameter("@ToDate", ToDate);
            param[4] = new SqlParameter("@Category", Category);
            param[5] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AllFollowupReport", param);
        }
        #endregion

        #region Select All records from Followup SearchByFollowup
        public DataTable SearchByFollowup()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@FromDate", FromDate);
            param[3] = new SqlParameter("@ToDate", ToDate);
            param[4] = new SqlParameter("@Search", Search);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AllFollowupReport", param);
        }
        #endregion

        #region Select All records from Followup SearchByNextFollowup
        public DataTable SearchByNextFollowup()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@FromDate", FromDate);
            param[3] = new SqlParameter("@ToDate", ToDate);
            param[4] = new SqlParameter("@Search", Search);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AllFollowupReport", param);
        }
        #endregion
        
    }
}
