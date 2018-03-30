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
   public class BalMemeberProfileInfo
    {
       DataConnection obDataCon = new DataConnection();
       public string Action { get; set; }
       public string Category { get; set; }
       public int Company_ID { get; set; }
       public int Branch_ID { get; set; }
       public int Login_AutoID { get; set; }
       public int Member_AutoID { get; set; }
       public int Member_ID1 { get; set; }
       public string Contact1 { get; set; }
       public string receiptID { get; set; }

       #region Select Memeber 
       public DataTable bindMemeber()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "Get_Member");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
         
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select Memeber
       public DataTable bindCallResponse()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "Get_CallResponse");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select MemeberDetails
       public DataTable bindMemeber1()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_MemberDetails");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select CourseDetails
       public DataTable CourseDetails()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_CourseDetails");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select AccountBalanceDetails
       public DataTable AccountBalanceDetails()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountBalanceDetails");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select AccountBalanceDetails1
       public DataTable AccountBalanceDetails1()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountBalanceDetails1");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select TotalFeeDetails
       public DataTable TotalFeeDetails()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "TotalFeeDetails");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion


       #region Select TotalFeeContact
       public DataTable TotalFeeContact()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "TotalFeeContact");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select AccountDetails
       public DataTable AccountDetails()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountDetails");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select AccountDetails1
       public DataTable AccountDetails1()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountDetails1");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select MemeberID
       public DataTable MemeberID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_MemberID");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select CourseID
       public DataTable CourseID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_CourseID");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion


       #region Select AccountBalanceID
       public DataTable AccountBalanceID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountBalanceID");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select ReceiptID
       public DataTable ReceiptID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "ReceiptID");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select ReceiptContact
       public DataTable ReceiptContact()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "ReceiptContact");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Contact1", Contact1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select Count
       public DataTable Count()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Count");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select MaxFees
       public DataSet MaxFees()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "MaxFees");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@ReceiptID", receiptID);
           return obDataCon.DataSet_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select TotalFee
       public DataTable TotalFeeID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "TotalFee");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select AccountID
       public DataTable AccountID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountID");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select MemeberContact
       public DataTable MemeberContact()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_MemberContact");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Contact1", Contact1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select CourseContact
       public DataTable CourseContact()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_CourseContact");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Contact1", Contact1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select AccountBalanceContact
       public DataTable AccountBalanceContact()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountBalanceContact");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Contact1", Contact1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region Select AccountContact
       public DataTable AccountContact()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "AccountContact");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Contact1", Contact1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region get Member Auto ID
       public DataTable Get_Member_Auto_ID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_Member_Auto_ID");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region get Member Auto ID ByMemberID1
       public DataTable Get_MemberAutoID_ByMemberID1()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_MemberAutoID_ByMemberID1");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_ID1", Member_ID1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region get Member Auto ID ByContact
       public DataTable Get_MemberAutoID_ByContact()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_MemberAutoID_ByContact1");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Contact1", Contact1);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region get Member ID
       public DataTable Get_Member_ID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "Get_Member_ID");
           param[1] = new SqlParameter("@Company_ID", Company_ID);
           param[2] = new SqlParameter("@Branch_ID", Branch_ID);
           param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion

       #region ---------------------Select All Data---------------------
       public DataTable Select_All()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@MemberID", Member_ID1);
           param[1] = new SqlParameter("@Branch_AutoID", Branch_ID);
           param[2] = new SqlParameter("@Company_AutoID", Company_ID);
           param[3] = new SqlParameter("@Action", "Select_All");
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberSearchFollowup", param);
       }
       #endregion

       #region ---------------------Select Member ID---------------------
       public DataTable MemberID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
           param[1] = new SqlParameter("@Branch_ID", Branch_ID);
           param[2] = new SqlParameter("@Company_ID", Company_ID);
           param[3] = new SqlParameter("@Action", "MemberID1");
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberInformationFetching", param);
       }
       #endregion
    }
}
