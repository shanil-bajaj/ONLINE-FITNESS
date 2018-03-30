using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace BusinessAccessLayer
{
    public class BalEnquiry
    {

        DataConnection obDataCon = new DataConnection();
        #region Enquiry property

        public SqlDateTime FromDate { get; set; }
        public SqlDateTime ToDate { get; set; }
        public string DateCategory { get; set; }
        public int Enq_FollAutoID { get; set; }
        public int Enq_ID { get; set; }
        public int Enq_ID1 { get; set; }
        public SqlDateTime EnqDate { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public SqlDateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string WhatsAppNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FollowupType { get; set; }
        public string Rating { get; set; }
        public string ReferenceDetails { get; set; }
        public int? CallRespond_AutoID { get; set; }
        public string Comment { get; set; }
        public SqlDateTime NextFollowupDate { get; set; }
        public DateTime? Time { get; set; }
        public string ImagePath { get; set; }
        public int? EnqType_ID { get; set; }
        public int? EnqFor_ID { get; set; }
        public int? SourceOfEnq_ID { get; set; }
        public int Login_AutoID { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Executive_ID { get; set; }
        public string Action { get; set; }
        public string Category { get; set;}
        public string searchTxt { get; set; }

        #endregion

        #region  Enquiry Insert / Update / Delete
        public int Insert_Update_Delete()
        {
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@Enq_ID1", Enq_ID1);
            param[1] = new SqlParameter("@EnqDate", EnqDate);
            param[2] = new SqlParameter("@FName", FName);
            param[3] = new SqlParameter("@LName", LName);
            param[4] = new SqlParameter("@DOB", DOB);
            param[5] = new SqlParameter("@Gender", Gender);
            param[6] = new SqlParameter("@Contact1", Contact1);
            param[7] = new SqlParameter("@Contact2", Contact2);
            param[8] = new SqlParameter("@WhatsAppNo", WhatsAppNo);
            param[9] = new SqlParameter("@Address", Address);
            param[10] = new SqlParameter("@FollowupType", FollowupType);
            param[11] = new SqlParameter("@Rating", Rating);
            param[12] = new SqlParameter("@ReferenceDetails", ReferenceDetails);
            param[13] = new SqlParameter("@CallRespond_AutoID", CallRespond_AutoID);
            param[14] = new SqlParameter("@Comment", Comment);
            param[15] = new SqlParameter("@NextFollowupDate", NextFollowupDate);
            param[16] = new SqlParameter("@Time", Time);
            param[17] = new SqlParameter("@ImagePath", ImagePath);
            param[18] = new SqlParameter("@EnqType_ID", EnqType_ID);
            param[19] = new SqlParameter("@EnqFor_ID", EnqFor_ID);
            param[20] = new SqlParameter("@SourceOfEnq_ID", SourceOfEnq_ID);
            param[21] = new SqlParameter("@Login_AutoID",Login_AutoID);
            param[22] = new SqlParameter("@Company_ID", Company_ID);
            param[23] = new SqlParameter("@Branch_ID", Branch_ID);
            param[24] = new SqlParameter("@Action", Action);
            param[25] = new SqlParameter("@Email", Email);
            param[26] = new SqlParameter("@Enq_ID", Enq_ID);
            param[27] = new SqlParameter("@Executive_ID", Executive_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion

        public int Delete()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_ID", Company_ID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@Enq_ID", Enq_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Enquiry", param);
        }

        #region Check Exits or not
        public DataTable Exits()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Exits");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Enq_ID1", Enq_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion 

        #region Check contact Exits or nt
        public DataTable ExitsContact()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Contact1", Contact1);
            param[4] = new SqlParameter("@Enq_ID1", Enq_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion 

        #region Get Enquiry Type
        public DataTable Get_EnquiryType()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_Enquiry_Type");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion 

        #region Get Enquiry For
        public DataTable Get_EnquiryFor()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_Enquiry_For");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion 

        #region Get SourceOfEnquiry
        public DataTable Get_SourceOfEnquiry()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_SourceOfEnquiry");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion 

        #region Get Enquiryid
        public DataTable Get_Enquiryid()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_ID", Company_ID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Action", "Get_EnquiryID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion

        #region Gridview Search
        public DataTable Get_Searchfollowup()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Company_ID", Company_ID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@searchTxt", searchTxt);
            param[3] = new SqlParameter("@Category", Category);
            param[4] = new SqlParameter("@DateCategory", DateCategory);
            param[5] = new SqlParameter("@FromDate", FromDate);
            param[6] = new SqlParameter("@ToDate", ToDate);
            param[7] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Search_EnquiryFollowup_Details", param);
        }
        #endregion

        #region Gridview Search
        public DataTable Get_Search()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Company_ID", Company_ID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@searchTxt", searchTxt); 
            param[3] = new SqlParameter("@Category", Category);
            param[4] = new SqlParameter("@DateCategory", DateCategory);
            param[5] = new SqlParameter("@FromDate", FromDate);
            param[6] = new SqlParameter("@ToDate", ToDate);
            param[7] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchEnquiry", param);
        }
        #endregion

        #region Get Record by Enq id
        public DataTable Get_Edit()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Edit");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Enq_ID", Enq_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion


        public bool Check_ExistingEnquiryId()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Exits");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Enq_ID1", Enq_ID1);

            int i = obDataCon.Int_StoredProcedure_Parameter("SP_Enquiry", param);
            if (i > 0)
                return true;
            else
                return false;

           // return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }


        public bool Check_EnquiryIdISMember()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "EnqIDIsMember");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Enq_ID", Enq_ID);

            int i = obDataCon.Int_StoredProcedure_Parameter("SP_Enquiry", param);
            if (i > 0)
                return true;
            else
                return false;
           
        }


        #region Get SMS Login Details
        public DataSet GetSMSLoginDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion

        #region Get Enquiry Template
        public DataTable GetTemplate()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_ID", Branch_ID);
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Contact1", Contact1);
            param[3] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion

        
        #region Get Enquiry Template
        public DataTable GetMemberDetailsByFollAutoID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "EditFollowup");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Enq_FollAutoID", Enq_FollAutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        }
        #endregion

    }
}
