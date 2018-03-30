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
    public class BalMembershipTransfer
    {
        #region ------------Public Properties -----------

        DataConnection obDataCon = new DataConnection();
        
        // Member Details Property
        public int Transfer_AutoID { get; set; }
        public int OldMember_AutoID { get; set; }
        public int OldMember_ID1 { get; set; }
        public int NewMember_ID1 { get; set; }
        public int NewMember_AutoID{ get; set; }
        //public string OldMember_FName { get; set; }
        //public string OldMember_LName { get; set; }
        public string NewMember_FName { get; set; }
        public string NewMember_LName { get; set; }
        public string OldContact1 { get; set; }
        public string Contact1 { get; set; }
        public string Gender { get; set; }
        

        // Course Details Property
        public int TransReceiptID { get; set; }
        public string ReceiptStstus { get; set; }
        public int ReceiptID { get; set; }
        //public double Old_Total_Fees { get; set; }
        //public double Old_Paid_Fees { get; set; }
        public double Balance_Fees { get; set; }
        public double Transfer_Fees { get; set; }

        public string PaymentMode { get; set; }
        public string Cardno { get; set; }
        public string Comment { get; set; }
        public DateTime? NextBalDate { get; set; }
        public DateTime? CardExpirydate { get; set; }
        public DateTime? payDate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public double Paid { get; set; }
        public string TaxType { get; set; }
        public double taxpec { get; set; }
        public double TaxValue { get; set; }
        public double PaidWithTax { get; set; }
                       
        public DateTime? TodayDate { get; set; }
        public DateTime? Transfer_Date { get; set; }
        public DateTime? DOB { get; set; }

        public DateTime? MStartDate { get; set; }
        public DateTime? MEndDate { get; set; }
        
        public string SearchByText { get; set; }
        public int Executive_ID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }

        #endregion


        public DataSet GetDetails()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@OldMember_ID1", OldMember_ID1);
            param[3] = new SqlParameter("@OldContact1", OldContact1);
            param[4] = new SqlParameter("@Action", Action);
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_MembershipTransfer", param);
        }

        public DataTable SelectDetails()
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@OldMember_ID1", OldMember_ID1);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);            
            param[4] = new SqlParameter("MStartDate", MStartDate);
            param[5] = new SqlParameter("@MEndDate", MEndDate);
            param[6] = new SqlParameter("@Contact1", Contact1);
            param[7] = new SqlParameter("@SearchByText", SearchByText);
            param[8] = new SqlParameter("@Category", Category);
            param[9] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MembershipTransfer", param);
        }

        public int Insert_Update_Record()
        {
            SqlParameter[] param = new SqlParameter[33];

            param[0] = new SqlParameter("@OldMember_AutoID",OldMember_AutoID );
            param[1] = new SqlParameter("@NewMember_FName", NewMember_FName);
            param[2] = new SqlParameter("@NewMember_LName", NewMember_LName);
            param[3] = new SqlParameter("@Contact1", Contact1);
            param[4] = new SqlParameter("@Gender", Gender);
            param[5] = new SqlParameter("@DOB", DOB);
            param[6] = new SqlParameter("@Executive_ID", Executive_ID);
            param[7] = new SqlParameter("@ReceiptID", ReceiptID);
            param[8] = new SqlParameter("@Transfer_Date", Transfer_Date);
            param[9] = new SqlParameter("@Transfer_Fees", Transfer_Fees);
            param[10] = new SqlParameter("@PaymentMode", PaymentMode);
            param[11] = new SqlParameter("@payDate",payDate);
            param[12] = new SqlParameter("@Cardno",Cardno);
            param[13] = new SqlParameter("@CardExpirydate",CardExpirydate);
            param[14] = new SqlParameter("@BankName",BankName);
            param[15] = new SqlParameter("@BranchName",BranchName);
            param[16] = new SqlParameter("@Paid",Paid);
            param[17] = new SqlParameter("@TaxType", TaxType);
            param[18] = new SqlParameter("@taxpec",taxpec);
            param[19] = new SqlParameter("@TaxValue",TaxValue);
            param[20] = new SqlParameter("@PaidWithTax",PaidWithTax);
            param[21] = new SqlParameter("@Comment", Comment);
            param[22] = new SqlParameter("@NextBalDate", NextBalDate);
            param[23] = new SqlParameter("@Balance_Fees", Balance_Fees);
            param[24] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[25] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[26] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[27] = new SqlParameter("@TransReceiptID", TransReceiptID);
            param[28] = new SqlParameter("@ReceiptStstus", ReceiptStstus);
            param[29] = new SqlParameter("@NewMember_AutoID", NewMember_AutoID);
            param[30] = new SqlParameter("@NewMember_ID1", NewMember_ID1);
            param[31] = new SqlParameter("@Transfer_AutoID", Transfer_AutoID);
            param[32] = new SqlParameter("@Action", Action);

            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MembershipTransfer", param);
        }
       
        public bool TransReceiptIDExist_OR_Not()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@TransReceiptID", TransReceiptID);            
            param[3] = new SqlParameter("@Action", Action);

            int i = obDataCon.Int_StoredProcedure_Parameter("SP_MembershipTransfer", param);
            if (i > 0)
                return true;
            else
                return false;
            
        }


    }

}
