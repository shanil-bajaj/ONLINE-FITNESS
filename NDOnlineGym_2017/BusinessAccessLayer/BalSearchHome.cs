using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Configuration;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalSearchHome
    {
        #region -----------------Public Properties -------------------

       public int Member_AutoID { get; set; }
       public int Member_ID1 { get; set; }
       public SqlDateTime RegDate { get; set; }
       public string FName { get; set; }
       public string LName { get; set; }
       public SqlDateTime DOB { get; set; }
       public string MariatalStatus { get; set; }
       public SqlDateTime AniversaryDate { get; set; }
       public string Gender { get; set; }
       public string Email { get; set; }
       public string Contact1 { get; set; }
       public string Contact2 { get; set; }
       public string WhatsAppNo { get; set; }
       public string con { get; set; }
       public string Address { get; set; }
       public string BloodGroup { get; set; }
       public string HealthDetails { get; set; }
       public string ImagePath { get; set; }
       public string IDProofName { get; set; }
       public string IDProofPath { get; set; }
       public string AccessCardNo { get; set; }
       public string Status { get; set; }
       public string SMSStatus { get; set; }
       public int Occupation_AutoID { get; set; }
       public int Enq_AutoID { get; set; }
       public int Staff_AutoID { get; set; }
       public int Company_AutoID { get; set; }
       public int Branch_AutoID { get; set; }
       public int Login_AutoID { get; set; }
       public string searchdata { get; set; }
       public string SearchData { get; set; }
       public string Category { get; set; }
       public string Action { get; set; }

       DataConnection obDataCon = new DataConnection();

       #endregion

        #region ---------------------Select All Data---------------------
        public DataTable Select_All()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SearchData", SearchData);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchHome", param);
        }
        #endregion

        #region ---------------------Select All Data---------------------

        public bool CheckExistsMemberID_Course()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Action", "ChkExists_MemberID_Course");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_SearchHome", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        //public DataTable CheckExistsMemberID_Course()
        //{
        //    SqlParameter[] param = new SqlParameter[4];
        //    param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
        //    param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
        //    param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
        //    param[3] = new SqlParameter("@Action", "ChkExists_MemberID_Course");
        //    return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchHome", param);
        //}
        #endregion

        #region ---------------------Select MemberID1 from Mem Auto ID---------------------
        public DataTable Select_MemID1()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SearchData", SearchData);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Action", "SelectMemID1");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchHome", param);
        }
        #endregion

        #region ---------------------Select EnqID1 from Enq Auto ID---------------------
        public DataTable Select_EnqID1()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SearchData", SearchData);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Action", "SelectEnqID1");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchHome", param);
        }
        #endregion
    }
}
