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
    public class BalCompanyInformation
    {
        #region Public Property

        DataConnection obDalCon = new DataConnection();

        public int Company_AutoID { get; set; }
        public int Company_ID1 {get;set;}
        public string CompanyName {get;set;}
        public string Contact1 {get;set;}
        public string Contact2 {get;set;}
        public string Landline {get;set;}
        public string Email {get;set;}
        public string Website {get;set;}
        public string Address1 {get;set;}
        public string Address2 {get;set;}
        public string Location {get;set;}
        public string State {get;set;}
        public string City {get;set;}
        public string CollectionSMS {get;set;}
        public string GSTNo {get;set;}
        public string TermsAndCondition {get;set;}
        public string OwnerName {get;set;}
        public string OwnerContact {get;set;}
        public string OwnerEmail {get;set;}
        public string CompanyLogoPath {get;set;}
        public string Status {get;set;}
        public string ContactPerson {get;set;}
        public string ContactPersonNo {get;set;}
        public string ContactPersonEmail {get;set;}
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime ExpiryDate { get; set; }

        public string Action { get; set; }
        public string Search_By { get; set; }

        #endregion




        //public bool chk_BranchName()
        //{
            
        //}

        public int Insert_CompanyInformation()
        {
            SqlParameter[] param = new SqlParameter[27];
            param[0] = new SqlParameter("@Company_ID1", Company_ID1);
            param[1] = new SqlParameter("@CompanyName", CompanyName);
            param[2] = new SqlParameter("@Status",Status);
            param[3] = new SqlParameter("@GSTNo",GSTNo);
            param[4] = new SqlParameter("@Address1",Address1);
            param[5] = new SqlParameter("@Address2",Address2);
            param[6] = new SqlParameter("@State",State);
            param[7] = new SqlParameter("@City",City);
            param[8] = new SqlParameter("@Location",Location);
            param[9] = new SqlParameter("@Contact1",Contact1);
            param[10] = new SqlParameter("@Contact2",Contact2);
            param[11] = new SqlParameter("@Landline",Landline);
            param[12] = new SqlParameter("@Email",Email);
            param[13] = new SqlParameter("@Website",Website);
            param[14] = new SqlParameter("@CollectionSMS",CollectionSMS);
            param[15] = new SqlParameter("@TermsAndCondition",TermsAndCondition);
            param[16] = new SqlParameter("@CompanyLogoPath", CompanyLogoPath);
            param[17] = new SqlParameter("@OwnerName",OwnerName);
            param[18] = new SqlParameter("@OwnerEmail",OwnerEmail);
            param[19] = new SqlParameter("@OwnerContact",OwnerContact);
            param[20] = new SqlParameter("@ContactPerson",ContactPerson);
            param[21] = new SqlParameter("@ContactPersonNo",ContactPersonNo);
            param[22] = new SqlParameter("@ContactPersonEmail",ContactPersonEmail);
            param[23] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[24] = new SqlParameter("@Username", Username);
            param[25] = new SqlParameter("@Password", Password);
            param[26] = new SqlParameter("@Action", Action);
            return obDalCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_CompanyInformation", param);
        }



        //public DataTable GetDetails()
        //{
        //    SqlParameter[] param = new SqlParameter[3];
        //    param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
        //    param[1] = new SqlParameter("@Search_By", Search_By);
        //    param[2] = new SqlParameter("@Action", Action);
        //    return obDalCon.DataTable_StoredProcedure_Parameter("SP_CompanyInformation", param);
        //}

        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Action", Action);
            return obDalCon.DataTable_StoredProcedure_Parameter("SP_CompanyInformation", param);
        }
    }
}
