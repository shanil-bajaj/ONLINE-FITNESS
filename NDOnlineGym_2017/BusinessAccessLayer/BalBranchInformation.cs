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
    public class BalBranchInformation
    {
        #region ------------Public Properties -----------

        public int Branch_AutoID { get; set; }
        public int Branch_ID1 { get; set; }
        public string BranchName { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CollectionSMS { get; set; }
        public string GSTNo { get; set; }
        public string TermsAndCondition { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public string OwnerEmail { get; set; }
        public string BranchLogoPath { get; set; }
        public string Status { get; set; }
        public int Company_AutoID { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }
        public string ContactPersonEmail { get; set; }
        //public string Username { get; set; }
        public string CompanyName { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
        public DateTime ExpiryDate { get; set; }

        DataConnection obDataCon = new DataConnection();

        #endregion

        #region Check that Branch Name is Exist or Not
        public bool Check_ExistingNameBranchInformation()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BranchName", BranchName);
            param[1] = new SqlParameter("@Action", "Chk_Existing");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_BranchInformation", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Check that Branch Name is Exist or Not while updating
        public bool Check_ExistingNameBranchNameUpdate()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BranchName", BranchName);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Chk_ExistingUpdate");
            int i = obDataCon.Int_StoredProcedure_Parameter("SP_BranchInformation", param);
            if (i > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region Select All records from Branch Information
        public DataTable Select_BranchInformation()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "SELECT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BranchInformation", param);
        }
        #endregion

        #region Select All records from Branch Information
        public DataTable SelectAll_BranchInformation()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "SELECT_All");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BranchInformation", param);
        }
        #endregion

        #region Insert record in Branch Information Table
        public int Insert_BranchInformation()
        {
            SqlParameter[] param = new SqlParameter[26];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Branch_ID1", Branch_ID1);
            param[2] = new SqlParameter("@BranchName", BranchName);
            param[3] = new SqlParameter("@Contact1", Contact1);
            param[4] = new SqlParameter("@Contact2", Contact2);
            param[5] = new SqlParameter("@Landline", Landline);
            param[6] = new SqlParameter("@Email", Email);
            param[7] = new SqlParameter("@Website", Website);
            param[8] = new SqlParameter("@Address1", Address1);
            param[9] = new SqlParameter("@Address2", Address2);
            param[10] = new SqlParameter("@Location", Location);
            param[11] = new SqlParameter("@State", State);
            param[12] = new SqlParameter("@City", City);
            param[13] = new SqlParameter("@CollectionSMS", CollectionSMS);
            param[14] = new SqlParameter("@GSTNo", GSTNo);
            param[15] = new SqlParameter("@TermsAndCondition", TermsAndCondition);
            param[16] = new SqlParameter("@OwnerName", OwnerName);
            param[17] = new SqlParameter("@OwnerContact", OwnerContact);
            param[18] = new SqlParameter("@OwnerEmail", OwnerEmail);
            param[19] = new SqlParameter("@BranchLogoPath", BranchLogoPath);
            param[20] = new SqlParameter("@Status", Status);
            param[21] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[22] = new SqlParameter("@ContactPerson", ContactPerson);
            param[23] = new SqlParameter("@ContactPersonNo", ContactPersonNo);
            param[24] = new SqlParameter("@ContactPersonEmail", ContactPersonEmail);
            param[25] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_BranchInformation", param);
        }
        #endregion

       

        #region Select record from Branch Information by ID
        public DataTable SelectByID_BranchInformation()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Action", "SELECT_BY_ID_EDIT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BranchInformation", param);
        }
        #endregion 

        #region Select record from Branch Information by ID
        public DataTable SELECT_CompanyDetails()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "SELECT_CompanyDetails");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BranchInformation", param);
        }
        #endregion

        #region Select record from Branch Information For Autogenerared BranchID1
        public DataTable Get_BranchID1()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "Get_BranchID1");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_BranchInformation", param);
        }
        #endregion 

        #region Get Data Accorfing to Search Criteria
        public DataTable Select_DataAsPerSearchCriteriaBranch()
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@Branch_ID1", Branch_ID1);
            param[1] = new SqlParameter("@BranchName", BranchName);
            param[2] = new SqlParameter("@Contact1", Contact1);
            param[3] = new SqlParameter("@Contact2", Contact2);
            param[4] = new SqlParameter("@State", State);
            param[5] = new SqlParameter("@City", City);
            param[6] = new SqlParameter("@OwnerName", OwnerName);
            param[7] = new SqlParameter("@OwnerContact", OwnerContact);
            param[8] = new SqlParameter("@Status", Status);
            param[9] = new SqlParameter("@CompanyName", CompanyName);
            param[10] = new SqlParameter("@ContactPerson", ContactPerson);
            param[11] = new SqlParameter("@ContactPersonNo", ContactPersonNo);
            param[12] = new SqlParameter("@Category", Category);
            param[13] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[14] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[15] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchBranchInformation", param);
        }
        #endregion

        #region Get Data Accorfing to Search Criteria
        public DataTable Select_DataAsPerSearchCriteriaBranchAdmin()
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@Branch_ID1", Branch_ID1);
            param[1] = new SqlParameter("@BranchName", BranchName);
            param[2] = new SqlParameter("@Contact1", Contact1);
            param[3] = new SqlParameter("@Contact2", Contact2);
            param[4] = new SqlParameter("@State", State);
            param[5] = new SqlParameter("@City", City);
            param[6] = new SqlParameter("@OwnerName", OwnerName);
            param[7] = new SqlParameter("@OwnerContact", OwnerContact);
            param[8] = new SqlParameter("@Status", Status);
            param[9] = new SqlParameter("@CompanyName", CompanyName);
            param[10] = new SqlParameter("@ContactPerson", ContactPerson);
            param[11] = new SqlParameter("@ContactPersonNo", ContactPersonNo);
            param[12] = new SqlParameter("@Category", Category);
            param[13] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[14] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[15] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchBranchInformationAdmin", param);
        }
        #endregion

        #region Select All records of Master Admin Information
        public DataTable SP_Select_BranchForHomePage()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SelectData_MAdmin_Admin", param);
        }
        #endregion

    }
}
