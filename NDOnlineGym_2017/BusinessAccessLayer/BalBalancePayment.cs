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
   public class BalBalancePayment
    {
        #region ------------Public Properties -----------

        public int Member_AutoID { get; set; }
        public int Member_ID1 { get; set; }
        public SqlDateTime RegDate { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Executive_ID { get; set; }
        public int Login_ID { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }
        public int ReceiptID1 { get; set; }
        public string PaymentMode { get; set; }
        public string Cardno { get; set; }
        public DateTime payDate { get; set; }
        public DateTime? CardExpirydate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public double Paid { get; set; }
        public string TaxType { get; set; }
        public double taxpec { get; set; }
        public double TaxValue { get; set; }
        public double PaidWithTax { get; set; }       
        public string Status { get; set; }
        public int Bal_Auto { get; set; }
        public double PaidFee { get; set; }
        public double Balance { get; set; }
        public int Bal_ReceiptID { get; set; }
        public string searchddl { get; set; }
       
      
        public double TotalFeeDue { get; set; }
        public SqlDateTime? NextBalDate { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        DataConnection obDataCon = new DataConnection();

        #endregion

        #region Insert record in Balance Details Table
        public int Insert_BalancePayment()
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@Action", Action);           
            param[1] = new SqlParameter("@PaymentMode", PaymentMode);
            param[2] = new SqlParameter("@Cardno", Cardno);
            param[3] = new SqlParameter("@payDate", payDate);
            param[4] = new SqlParameter("@CardExpirydate", CardExpirydate);
            param[5] = new SqlParameter("@BankName", BankName);
            param[6] = new SqlParameter("@BranchName", BranchName);
            param[7] = new SqlParameter("@Paid", Paid);
            param[8] = new SqlParameter("@TaxType", TaxType);
            param[9] = new SqlParameter("@taxpec", taxpec);
            param[10] = new SqlParameter("@TaxValue", TaxValue);
            param[11] = new SqlParameter("@PaidWithTax", PaidWithTax);
            param[12] = new SqlParameter("@ReceiptID", ReceiptID1);
            param[13] = new SqlParameter("@Status", Status);
            param[14] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[15] = new SqlParameter("@Company_ID", Company_AutoID);
            param[16] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[17] = new SqlParameter("@Login_AutoID", Login_ID);
            param[18] = new SqlParameter("@Executive_ID", Executive_ID);
            param[19] = new SqlParameter("@Member_ID1", Member_ID1);
            param[20] = new SqlParameter("@Bal_ReceiptID", Bal_ReceiptID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Insert Balance details
        public int Insert_Balancedetails()
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Login_AutoID", Login_ID);
            param[4] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[5] = new SqlParameter("@ReceiptID", ReceiptID1);
            param[6] = new SqlParameter("@Status", Status);
            param[7] = new SqlParameter("@PaidFee", PaidFee);
            param[8] = new SqlParameter("@TotalFeeDue", TotalFeeDue);
            param[9] = new SqlParameter("@Balance", Balance);
            param[10] = new SqlParameter("@NextBalDate", NextBalDate);
            param[11] = new SqlParameter("@Executive_ID", Executive_ID);
            param[12] = new SqlParameter("@Member_ID1", Member_ID1);
            param[13] = new SqlParameter("@Bal_ReceiptID", Bal_ReceiptID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion


        #region Select BalanceID
        public DataTable BalanceID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_BalanceID");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_ID1", Member_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select BalanceID1
        public DataTable BalanceID1()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_BalanceID1");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_ID1", Member_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Check Existing Receipt id
        public DataTable Check_ReceiptId()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Check_ReceiptId");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select BalanceWithoutID
        public DataTable BalanceWithoutID()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "BalanceWithoutID");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);           
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Searching
        public DataTable SearchDDL()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Company_ID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[4] = new SqlParameter("@searchddl ", searchddl);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Search with Category
        public DataTable SearchWithCategory()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Company_ID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[4] = new SqlParameter("@searchddl ", searchddl);
            param[5] = new SqlParameter("@FromDate", FromDate);
            param[6] = new SqlParameter("@ToDate", ToDate);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select CourseMemeberContact
        public DataTable MemeberContact()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_BalanceContact");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Contact1", Contact1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select ReceiptID
        public DataTable ReceiptID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_ReceiptID");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_ID1", Member_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Check_MemberId_Exist
        public DataTable Check_MemberId_Exist()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Check_MemberId_Exist");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_ID1", Member_ID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Get Payment Mode
        public DataTable Get_PaymentMode()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "PaymentMode");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);       
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Balance Details
        public DataTable Balancedetails()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", "Balancedetails");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@FromDate", FromDate);
            param[4] = new SqlParameter("@ToDate", ToDate);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion


        #region Select ReceiptContact
        public DataTable ReceiptContact()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_ReceiptContact");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Contact1", Contact1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion
        #region Select Fees
        public DataTable Fees()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_Fees");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Fees1
        public DataTable Fees1()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_Fees1");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Fees2
        public DataTable Fees2()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_Fees2");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select Bind_ReceiptWise
        public DataTable Bind_ReceiptWise()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Bind_ReceiptWise");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region SMS
        public DataTable SMS()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "SMS");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select get_ReceiptNo
        public DataTable get_ReceiptNo()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "get_ReceiptNo");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        #region Select DeleteRecord
        public DataTable DeleteRecord()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "DeleteRecord");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Bal_ReceiptID", Bal_ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }
        #endregion

        public int checkID_Exist_Not()
        {
            int res;         
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "checkID_Exist_Not");
            param[1] = new SqlParameter("@Company_ID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            res = obDataCon.Int_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
            return res;
        }

        public DataTable Get_Edit_Payment()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_ID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_ID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Edit_Payment");           
            param[3] = new SqlParameter("@Bal_ReceiptID", Bal_ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_MemberBalancePayment", param);
        }

    }
}
