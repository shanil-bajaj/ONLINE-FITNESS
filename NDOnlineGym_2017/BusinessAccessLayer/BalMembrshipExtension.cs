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
   
    public class BalMembrshipExtension
    {

        #region ------------Public Properties -----------

        DataConnection obDataCon = new DataConnection();

        public string Contact1 { get; set; }
        public int Member_AutoID { get; set; }        
        public int Member_ID1 { get; set; }
        public int Course_Auto { get; set; }
        public int Extension_AutoID { get; set; } 
        public string ExtensionReason { get; set; }        
        public int ExtendDays { get; set; }

        public DateTime? ExtensionDate { get; set; }
        public DateTime? TodayDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? OldEndDate { get; set; }
        public DateTime? NewEndDate { get; set; }

        public DateTime? MStartDate { get; set; }
        public DateTime? MEndDate { get; set; }

        public string SearchByText { get; set; }
        public int Executive_ID { get; set; }       
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int LoginID { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }

        #endregion

        #region ---------- Search By Member ID -----------
        public DataTable Select_MemberID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Member_ID1", Member_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MembershipExtension", param);
        }
        #endregion

        #region ------------- Select By Contact Number ----------
        public DataTable Select_MemberContact()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Contact", Contact1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MembershipExtension", param);
        }
        #endregion

        #region ----------- Get Course Details By Member ID ---------
        public DataTable Select_CoursePackage()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", "CourseDetail");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@TodayDate", TodayDate);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MembershipExtension", param);
        }
        #endregion 
       
        #region ------------ Insert And Update -----------
        public int Insert()
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[1] = new SqlParameter("@Course_Auto", Course_Auto);
            param[2] = new SqlParameter("@StartDate", StartDate);
            param[3] = new SqlParameter("@OldEndDate",OldEndDate);
            param[4] = new SqlParameter("@LoginID", LoginID);
            param[5] = new SqlParameter("@ExtensionReason", ExtensionReason);
            param[6] = new SqlParameter("@NewEndDate", NewEndDate);
            param[7] = new SqlParameter("@ExtendDays", ExtendDays);
            param[8] = new SqlParameter("@TodayDate", TodayDate);
            param[9] = new SqlParameter("@ExtensionDate", ExtensionDate);
            param[10] = new SqlParameter("@Executive_ID", Executive_ID);
            param[11] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[12] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[13] = new SqlParameter("@Action", Action);
            param[14] = new SqlParameter("@Extension_AutoID", Extension_AutoID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MembershipExtension", param);
        }
        #endregion

        #region ----------- Get Gridview Details ---------
        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@SearchByText", SearchByText);
            param[3] = new SqlParameter("@Extension_AutoID", Extension_AutoID);
            param[4] = new SqlParameter("@MStartDate", MStartDate);
            param[5] = new SqlParameter("@MEndDate", MEndDate);
            param[6] = new SqlParameter("@Action", Action);
            param[7] = new SqlParameter("@Category", Category);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MembershipExtension", param);
        }
        #endregion

        #region ------------ Check Course Is Already Extendd Or Not
        public bool Check_AllReadyExtendByMemberAutoId()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Course_Auto", Course_Auto);
            param[4] = new SqlParameter("@Action", Action);

            int i = obDataCon.Int_StoredProcedure_Parameter("SP_MembershipExtension", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion


    }



}
