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
   public class BalAddMember
   {
       #region ------------Public Properties -----------

       public int Member_AutoID { get; set; }
       public int Executive_ID { get; set; }
       public int Member_ID1 { get; set; }
       public SqlDateTime RegDate { get; set; }
       public string FName { get; set; }
       public string LName { get; set; }
       public SqlDateTime? DOB { get; set; }
       public string MariatalStatus { get; set; }
       public SqlDateTime? AniversaryDate { get; set; }
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
       public int? Occupation_AutoID { get; set; }
       public int Enq_AutoID { get; set; }
       public int Staff_AutoID { get; set; }
       public int Company_AutoID { get; set; }
       public int Branch_AutoID { get; set; }
       public int Login_ID { get; set; }
       public int? ReceiptID { get; set; }
       public string Category { get; set; }
       public string Action { get; set; }
       public string MembershipStatus { get; set; }
       public string searchTxt { get; set; }
       public DateTime FromDate { get; set; }
       public DateTime ToDate { get; set; }
     
       DataConnection obDataCon = new DataConnection();

       #endregion


       #region Insert record in Member Details Table
       public int Insert_MemberDetails()
       {
           SqlParameter[] param = new SqlParameter[29];
           param[0] = new SqlParameter("@Action", Action);
           param[1] = new SqlParameter("@Member_ID1", Member_ID1);
           param[2] = new SqlParameter("@RegDate", RegDate);
           param[3] = new SqlParameter("@FName", FName);
           param[4] = new SqlParameter("@LName", LName);
           param[5] = new SqlParameter("@DOB", DOB);
           param[6] = new SqlParameter("@MariatalStatus", MariatalStatus);
           param[7] = new SqlParameter("@AniversaryDate", AniversaryDate);
           param[8] = new SqlParameter("@Gender", Gender);
           param[9] = new SqlParameter("@Contact1", Contact1);
           param[10] = new SqlParameter("@Contact2", Contact2);
           param[11] = new SqlParameter("@WhatsAppNo", WhatsAppNo);
           param[12] = new SqlParameter("@Address", Address);
           param[13] = new SqlParameter("@BloodGroup", BloodGroup);
           param[14] = new SqlParameter("@HealthDetails", HealthDetails);
           param[15] = new SqlParameter("@ImagePath", ImagePath);
           param[16] = new SqlParameter("@IDProofName", IDProofName);
           param[17] = new SqlParameter("@IDProofPath", IDProofPath);
           param[18] = new SqlParameter("@AccessCardNo", AccessCardNo);
           param[19] = new SqlParameter("@Status", Status);
           param[20] = new SqlParameter("@SMSStatus", SMSStatus);
           param[21] = new SqlParameter("@Occupation_AutoID", Occupation_AutoID);
           param[22] = new SqlParameter("@Enq_AutoID", Enq_AutoID);
           param[23] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[24] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[25] = new SqlParameter("@Email", Email);
           param[26] = new SqlParameter("@Member_AutoID",Member_AutoID);
           //param[27] = new SqlParameter("@ReceiptID", ReceiptID);
           param[27] = new SqlParameter("@Executive_ID", Executive_ID);
           param[28] = new SqlParameter("@MembershipStatus", MembershipStatus);
           //param[26] = new SqlParameter("@Password", Password);
         
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion

       #region--Check Contact While Saving--
       public int Contactcheck()
       {
           int res;
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Contact1", Contact1);
           param[1] = new SqlParameter("@Action", Action);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[4] = new SqlParameter("@Member_AutoID", Member_AutoID);
           res = obDataCon.Int_StoredProcedure_Parameter("SP_MemberDetails", param);
           return res;
       }
       #endregion


       #region--Check Contact While Updating--
       public int Contactcheck1()
       {
           int res;
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Contact1", Contact1);
           param[1] = new SqlParameter("@Action", Action);
           param[2] = new SqlParameter("@Member_AutoID", Member_AutoID);
           res = obDataCon.Int_StoredProcedure_Parameter("SP_MemberDetails", param);
           return res;
       }
       #endregion

       #region--Check ID While Saving--
       public int checkID()
       {
           int res;
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Member_ID1", Member_ID1);
           param[1] = new SqlParameter("@Action", Action);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[4] = new SqlParameter("@Member_AutoID", Member_AutoID);
           res = obDataCon.Int_StoredProcedure_Parameter("SP_MemberDetails", param);
           return res;
       }
       #endregion


       #region Gridview Search
       public DataTable SearchCategory()
       {
           SqlParameter[] param = new SqlParameter[10];
           param[0] = new SqlParameter("@Action", Action);
           param[1] = new SqlParameter("@Category", Category);
           param[2] = new SqlParameter("@Con", con);
           param[3] = new SqlParameter("@FName", FName);
           param[4] = new SqlParameter("@LName", LName);
           param[5] = new SqlParameter("@Gender", Gender);
           param[6] = new SqlParameter("@Member_ID1", Member_ID1);
           param[7] = new SqlParameter("@Status", Status);
           param[8] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[9] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion

       #region Select record from Member Details For Autogenerared MemberID1
       public DataTable Get_MemberID1()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[2] = new SqlParameter("@Action", "Get_MemberID1");
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion 

       #region Select record from Member Details by ID
       public DataTable SELECT_Occupation()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "SELECT_Occupation");
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion

       #region ----Select All----
       public DataTable Select_All()
       {

           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "All");
           param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion

       #region-----Delete Record-----
       public int Delete_Staff()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
           param[1] = new SqlParameter("@Action", "Delete");
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion

       #region Select record from User Information by ID
       public DataTable SelectByID_MemberInformation()
       {
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
           param[1] = new SqlParameter("@Action", "SELECT_BY_ID");
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[4] = new SqlParameter("@Login_AutoID",Login_ID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion


       #region Select record from Enquiry by ID
       public DataTable SelectByID_EnquiryInformation()
       {
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Enq_AutoID", Enq_AutoID);
           param[1] = new SqlParameter("@Action", "SELECT_BY_Enq_AutoID");
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[4] = new SqlParameter("@Login_AutoID", Login_ID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion

       #region Get Contact of max mem id
       public DataTable GetContactMaxID()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[2] = new SqlParameter("@Action", "GetContactMaxID");
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberDetails", param);
       }
       #endregion

       #region Gridview Search
       public DataTable Get_Search()
       {
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[2] = new SqlParameter("@searchTxt", searchTxt);
           param[3] = new SqlParameter("@Category", Category);
           //param[4] = new SqlParameter("@FromDate", FromDate);
           //param[5] = new SqlParameter("@ToDate", ToDate);
           param[4] = new SqlParameter("@Action", Action);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchMember", param);
       }
       #endregion
       
   }
}
