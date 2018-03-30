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
    public class BalCourseReceipt
    {
        #region ------------Public Properties -----------

        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public int ReceiptID { get; set; }
        public int ReferReceiptID { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
      

        DataConnection obDataCon = new DataConnection();

        #endregion

        #region Select record from Branch Information by ID
        public DataTable SELECT_CompanyDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "SELECT_CompanyDetails");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
        #endregion


        #region Count Members from  Receipt No
        public DataTable CountReceiptMembers()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID",Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID",Company_AutoID );
            param[2] = new SqlParameter("@Login_AutoID",Login_AutoID );
            param[3] = new SqlParameter("@ReceiptID",ReceiptID );
            param[4] = new SqlParameter("@Action","GetMemberCountReceiptWise" );
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
      

         public DataTable GetOwnerName()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID",Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID",Company_AutoID );
            param[2] = new SqlParameter("@Login_AutoID",Login_AutoID );
            param[3] = new SqlParameter("@ReceiptID",ReceiptID );
            param[4] = new SqlParameter("@Action", "GetOwnerName");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
        #endregion

        #region get Member Details from  Receipt No
        public DataTable GetReceiptMembers()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@Action", "GetMemberDetailsReceiptWise");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
        #endregion

        #region get Course Details from  Receipt No
        public DataTable GetReceiptCourseDetails()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@Action", "GetReceiptCourseDetails");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
        #endregion

        #region get Payment Details from  Receipt No
        public DataTable GetReceiptPaymentDetails()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@Action", "GetReceiptPaymentDetails");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }

        public DataTable GetReceiptPaymentDetails1()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@ReferReceiptID", ReferReceiptID);
            param[5] = new SqlParameter("@Action", "GetReceiptPaymentDetails1");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }

        public DataTable GetReceiptPaymentDetails_PT()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@ReferReceiptID", ReferReceiptID);
            param[5] = new SqlParameter("@Action", "PT_Bal");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
        #endregion

        #region get Balance Details from  Receipt No
        public DataTable GetReceiptBalanceDetails()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@Action", "GetReceiptBalanceDetails");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }

        public DataTable GetReceiptBalanceDetails_PT()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@Action", "PT");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
        public DataTable GetReceiptBalanceDetails1()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@Action", "GetReceiptBalanceDetails1");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        }
        #endregion


        //#region get Total Discount from  Receipt No
        //public DataTable GetTotalDiscountDetails()
        //{
        //    SqlParameter[] param = new SqlParameter[5];
        //    param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
        //    param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
        //    param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
        //    param[3] = new SqlParameter("@ReceiptID", ReceiptID);
        //    param[4] = new SqlParameter("@Action", "GetReceiptTotalDiscountDetails");
        //    return obDataCon.DataTable_StoredProcedure_Parameter("SP_CourseReceipt", param);
        //}
        //#endregion
    }
}
