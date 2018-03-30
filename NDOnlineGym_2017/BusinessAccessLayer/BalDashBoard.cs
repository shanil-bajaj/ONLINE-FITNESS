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
   public class BalDashBoard
    {
       DataConnection obDataCon = new DataConnection();
        public int Branch_ID { get; set; }
        public int Comp_ID { get; set; }
        public string Action { get; set; }
        public DateTime date { get; set; }
        public int MemberId1 { get; set; }
        public int Enq_ID1 { get; set; }
        public string Duration { get; set; }
        public string Category { get; set; }

        public DataTable AutoID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "AutoID");
            param[3] = new SqlParameter("@MemberID1", MemberId1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
        }

        public DataTable getEnqAutoID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "getEnqAutoID");
            param[3] = new SqlParameter("@Enq_ID1", Enq_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
        }

        public DataTable GetMemberAutoID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@MemberID1", MemberId1);
            param[3] = new SqlParameter("@Action", "GetMemberAutoID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
        }

        public int Display_MemberBirth_Cnt()
        {
            SqlParameter[] param=new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Member_BirthDay");
            param[3] = new SqlParameter("@date",date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }


        public DataTable TodayBalancePayment()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "BalancePayment");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param); ;
        }

        public DataTable TodayPostDatedCheque()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "PostDatedCheque");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param); ;
        }

        public int StaffBirth()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Staff_BirthDay");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }

        public DataTable ALLStaffBirth()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "ALLStaff_BirthDay");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);

        }


        public int Active()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Active");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }


        public int Deactive()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Deactive");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }


        public DataTable AllMemberBirth()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "All_Member_BirthDay");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
             
        }

        public DataTable OtherFollowup()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "OtherFollowup");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
        }

        public DataTable MemberEnd()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "MembershipEndDate");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
        }

        public int MemberAnneversary()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Member_Aniversary");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }

        public DataTable AllMemberAnneversary()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "AllMember_Aniversary");
            param[3] = new SqlParameter("@date", date);
            return  obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
           
        }

        public int Addmission()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Admission");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }

        public int Collection()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Collection");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }

        public DataTable AllAddmission()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "AllAdmission");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);

        }

        public int Enquiryflwp()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Count_EnquiryFlwp");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }

        public DataTable AllEnquiryflwp()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "AllCount_EnquiryFlwp");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);     
        }

        public int Enquiry()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "Count_Enquiry");
            param[3] = new SqlParameter("@date", date);
            int res = obDataCon.Int_StoredProcedure_Parameter("SP_Notification", param);
            return res;
        }

        public DataTable AllEnquiry()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "ALLCount_Enquiry");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);
        }

        public DataTable AllActive()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "ALLActive");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);

        }

        public DataTable AllDeactive()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@Action", "ALLDeactive");
            param[3] = new SqlParameter("@date", date);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);

        }

        public DataTable GetInfo()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_ID);
            param[1] = new SqlParameter("@Comp_AutoID", Comp_ID);
            param[2] = new SqlParameter("@date", date);
            param[3] = new SqlParameter("@Action", Action);
            param[4] = new SqlParameter("@Category", Category);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Notification", param);

        }


    }
}
