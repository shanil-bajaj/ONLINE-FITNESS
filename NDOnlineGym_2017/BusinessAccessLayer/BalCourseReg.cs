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
    public class BalCourseReg
    {


        DataConnection obDataCon = new DataConnection();
        /// <summary>
        /// Add Member Property
        /// </summary>
        public int Member_AutoID { get; set; }
        public int Member_ID1 { get; set; }

        /// <summary>
        /// Add Member Sub Table Property
        /// </summary>
        public int MemberSub_Auto { get; set; }
        public int MemberSub_ID1 { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Login_AutoID { get; set; }

        /// <summary>
        /// Balance Payment Property
        /// </summary>      
        public int Bal_Auto { get; set; }
        public string PaymentMode { get; set; }
        public string Cardno { get; set; }
        
        public DateTime? CardExpirydate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public double Paid { get; set; }
        public string TaxType { get; set; }
        public double taxpec { get; set; }
        public double TaxValue { get; set; }
        public double PaidWithTax { get; set; }
        public int ReceiptID { get; set; }

        /// <summary>
        /// Balance Details
        /// </summary>     
        public int ID_Auto { get; set; }
        public double PaidFee { get; set; }
        public double TotalFeeDue { get; set; }
        public double Balance { get; set; }
        public string Comment { get; set; }
        public DateTime? NextBalDate { get; set; }
        public string Action { get; set; }

        /// <summary>
        /// Assign Package
        /// </summary>
        public int Pack_AutoID { get; set; }
        public SqlDateTime StartDate { get; set; }
        public SqlDateTime EndDate { get; set; }
        public double Amount { get; set; }
        public int Qty { get; set; }
        public int Total { get; set; }
        public double Discount { get; set; }
        public double FinalTotal { get; set; }

        public string MemberType { get; set; }
        public int? Staff_AutoID { get; set; }
        public string DiscReason { get; set; }

        public string CourseMemberType { get; set; }
        public string MembershipStatus { get; set; }
        public string Category { get; set; }
        public string searchTxt { get; set; }

        public int Course_Auto { get; set; }
        public int Executive_ID { get; set; }

        public DateTime payDate { get; set; }
        public DateTime payDate1 { get; set; }


        public int Get_MaxMemberID()
        {
            int res;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Member_ID1", Member_ID1);
            param[3] = new SqlParameter("@Action", Action);
            res = obDataCon.Int_StoredProcedure_Parameter("Course_Submember_Save", param);
            return res;
        }

        public int Get_ReceiptID()
        {
            int res;
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_ReceiptID");
            res = obDataCon.Int_StoredProcedure_Parameter("Course_Submember_Save", param);
            return res;
        }

        #region Select record from SubMember Details For Autogenerared MemberID1

        public DataTable Get_SubMemberID1()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_SubMemberID1");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        #endregion 

        public DataTable Get_MemberID1()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_MemberID1");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public DataTable Get_Instructor()
        {

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Instructor");
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        #region Insert record in Member Sub Table
        public int Insert_Member_Sub()
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@MemberSub_ID1", MemberSub_ID1);
            param[5] = new SqlParameter("@FName", FName);
            param[6] = new SqlParameter("@LName", LName);
            param[7] = new SqlParameter("@Gender", Gender);
            param[8] = new SqlParameter("@Contact", Contact);
            param[9] = new SqlParameter("@Email", Email);
            param[10] = new SqlParameter("@Status", Status);
            param[11] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[12] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        #endregion

        #region Insert Assign Package
        public int Insert_AssignPackage()
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            param[4] = new SqlParameter("@StartDate", StartDate);
            param[5] = new SqlParameter("@EndDate", EndDate);
            param[6] = new SqlParameter("@Amount", Amount);
            param[7] = new SqlParameter("@Qty", Qty);
            param[8] = new SqlParameter("@Total", Total);
            param[9] = new SqlParameter("@Discount", Discount);
            param[10] = new SqlParameter("@Status", Status);
            param[11] = new SqlParameter("@FinalTotal", FinalTotal);
            param[12] = new SqlParameter("@Login_AutoID", Login_AutoID);
            //param[13] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[13] = new SqlParameter("@ReceiptID", ReceiptID);
            param[14] = new SqlParameter("@MemberType", MemberType);
            param[15] = new SqlParameter("@Staff_AutoID", Staff_AutoID);
            param[16] = new SqlParameter("@DiscReason", DiscReason);
            param[17] = new SqlParameter("@Member_ID1", Member_ID1);
            param[18] = new SqlParameter("@CourseMemberType", CourseMemberType);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        #endregion
        public int Days_AutoID { get; set; }
        public int Time_AutoID { get; set; }
        public int Instructor_AutoID { get; set; }
        public int Insert_InstructorDetails()
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Action", "PT_Save");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);     
            param[5] = new SqlParameter("@Member_ID1", Member_ID1);
            param[6] = new SqlParameter("@Days_AutoID", Days_AutoID);
            param[7] = new SqlParameter("@Time_AutoID", Time_AutoID);
            param[8] = new SqlParameter("@Instructor_AutoID", Instructor_AutoID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        #region Insert BalancePayment
        public int Insert_BalancePayment()
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@PaymentMode", PaymentMode);
            param[4] = new SqlParameter("@Cardno", Cardno);
            param[5] = new SqlParameter("@payDate", payDate);
            param[6] = new SqlParameter("@CardExpirydate", CardExpirydate);
            param[7] = new SqlParameter("@BankName", BankName);
            param[8] = new SqlParameter("@BranchName", BranchName);
            param[9] = new SqlParameter("@Paid", Paid);
            param[10] = new SqlParameter("@Status", Status);
            param[11] = new SqlParameter("@TaxType", TaxType);
            param[12] = new SqlParameter("@taxpec", taxpec);
            param[13] = new SqlParameter("@TaxValue", TaxValue);
            param[14] = new SqlParameter("@PaidWithTax", PaidWithTax);
            param[15] = new SqlParameter("@Login_AutoID", Login_AutoID);
            //param[16] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[16] = new SqlParameter("@ReceiptID", ReceiptID);
            param[17] = new SqlParameter("@CourseMemberType", CourseMemberType);
            param[18] = new SqlParameter("@Member_ID1", Member_ID1);
            param[19] = new SqlParameter("@Executive_ID", Executive_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        #endregion

        #region Insert Balance details
        public int Insert_Balancedetails()
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Login_AutoID", Login_AutoID);
            //param[4] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);
            param[5] = new SqlParameter("@Status", Status);
            param[6] = new SqlParameter("@PaidFee", PaidFee);
            param[7] = new SqlParameter("@TotalFeeDue", TotalFeeDue);
            param[8] = new SqlParameter("@Balance", Balance);
            param[9] = new SqlParameter("@Comment", Comment);
            param[10] = new SqlParameter("@NextBalDate", NextBalDate);
            param[11] = new SqlParameter("@CourseMemberType", CourseMemberType);
            param[12] = new SqlParameter("@Member_ID1", Member_ID1);
            param[13] = new SqlParameter("@Executive_ID", Executive_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        #endregion


       
        public DataTable Get_Edit_member()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Edit_member");
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        public DataTable Get_Edit_Assihnpackage()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Edit_Assihnpackage");
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        public DataTable Get_Edit_Payment()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Edit_Payment");
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);
            param[5] = new SqlParameter("@Status", Status);
            
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        public DataTable Get_Edit_MemberType()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Edit_MemberType");
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public DataTable Get_Edit_Payment_PT()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Edit_Payment_PT");
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        public DataTable Get_Edit_Cmnt()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Edit_Cmnt");
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public DataTable Get_Edit_Days_Time()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Get_Edit_Days_Time");
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public DataTable ChkContactno()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "ChkContactNo");
            param[3] = new SqlParameter("@Contact", Contact);         
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public int ContactExist_OR_Not()
        {
            int res;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "ContactExist_OR_Not");
            param[3] = new SqlParameter("@Contact", Contact);    
            res = obDataCon.Int_StoredProcedure_Parameter("Course_Submember_Save", param);
            return res;
        }


        public int Get_DeletePaymentType()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action","Get_DeletePaymentType");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@Bal_Auto", Bal_Auto);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public DataTable Get_including_Tax()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Get_including_Tax");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
        public int ReceiptIDExist_OR_Not()
        {
            int res;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "ReceiptIDExist_OR_Not");
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            res = obDataCon.Int_StoredProcedure_Parameter("Course_Submember_Save", param);
            return res;
        }
        public int MaxDiscount()
        {
            int res;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "MaxDiscount");
            param[3] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            res = obDataCon.Int_StoredProcedure_Parameter("Course_Submember_Save", param);
            return res;
        }
        public int DeleteReceipt()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "DeleteReceipt");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);    
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public DataTable FetchCourseid_BYReceiptID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "FetchCourseid_byReceiptID");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
           
        }

        public DataTable FetchRemBal_BYReceiptID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "FetchRemBal_BYReceiptID");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);

        }

        public DataTable Get_Status()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Get_Status");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);

        }

        public int DeleteFreezing()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "DeleteFreezing");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Course_Auto", Course_Auto);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public int DeleteExtension()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "DeleteExtension");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Course_Auto", Course_Auto);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        //------------------------------------ Seraching    Crietria ------------------------


        public DataTable BindGV()
        {

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "Bindview");
            param[3] = new SqlParameter("@Category", Category);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);


        }

        public DataTable SearchByDate()
        {

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@payDate", payDate);
            param[4] = new SqlParameter("@payDate1", payDate1);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);


        }

        public DataTable SearchByDateWithCategory()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "SearchByDateWithCategory");
            param[3] = new SqlParameter("@Category", Category);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            param[5] = new SqlParameter("@payDate", payDate);
            param[6] = new SqlParameter("@payDate1", payDate1);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }

        public DataTable GetMaxEndDate_ByMemberAutoID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", "GetMaxEndDate");
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("Course_Submember_Save", param);
        }
    }
}
