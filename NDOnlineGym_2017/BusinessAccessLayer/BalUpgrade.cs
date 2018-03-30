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
    public class BalUpgrade
    {
        #region ------------Public Properties -----------

        DataConnection obDataCon = new DataConnection();

        // Member Details Property
        public int Upgrade_AutoID { get; set; }        
        public int Member_ID1 { get; set; }
        public int Member_AutoID { get; set; }                       
        public string Contact1 { get; set; }
        
        // Course Details Property
        public int Upgrade_ReceiptID { get; set; }
        public int ReceiptID { get; set; }
        public string ReceiptStstus { get; set; }

        public int Old_Pack_AutoID { get; set; }
        public DateTime? Old_StartDate { get; set; }
        public DateTime? Old_EndDate { get; set; }
        public int Old_Amount { get; set; }
        public int Qty { get; set; }
        //public double Old_Total { get; set; }
        public double Old_Discount { get; set; }
        public double Old_PaidFee { get; set; }
        public double Old_FinalTotal { get; set; }
        public double Old_Balance { get; set; }


        public int New_Pack_AutoID { get; set; }
        public DateTime? New_StartDate { get; set; }
        public DateTime? New_EndDate { get; set; }
        public int New_Amount { get; set; }
        public double New_Total { get; set; }
        public double New_Discount { get; set; }
        public double New_Balance { get; set; }
        public double New_FinalTotal { get; set; }              

        public DateTime? Upgrade_Date { get; set; }        
        public DateTime? MStartDate { get; set; }
        public DateTime? MEndDate { get; set; }

        public string MemberType { get; set; }
        public string SearchByText { get; set; }
        public int Executive_ID { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public string Category { get; set; }
        public int Bal_Receipt { get; set; }
        public string Action { get; set; }

        #endregion

        public DataSet GetDetails()
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Member_ID1", Member_ID1);
            param[3] = new SqlParameter("@Contact1", Contact1);
            param[4] = new SqlParameter("@ReceiptID", ReceiptID);
            param[5] = new SqlParameter("@Action", Action);
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_Upgrade", param);
        }

        public DataTable SelectDetails()
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Member_ID1", Member_ID1);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@MStartDate", MStartDate);
            param[5] = new SqlParameter("@MEndDate", MEndDate);
            param[6] = new SqlParameter("@Contact1", Contact1);
            param[7] = new SqlParameter("@Upgrade_AutoID", Upgrade_AutoID);
            param[8] = new SqlParameter("@SearchByText", SearchByText);
            param[9] = new SqlParameter("@Category", Category);
            param[10] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Upgrade", param);
        }

        public DataSet SelectDetails1()
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Member_ID1", Member_ID1);
            param[3] = new SqlParameter("@ReceiptID", ReceiptID);
            param[4] = new SqlParameter("@MStartDate", MStartDate);
            param[5] = new SqlParameter("@MEndDate", MEndDate);
            param[6] = new SqlParameter("@Contact1", Contact1);
            param[7] = new SqlParameter("@Upgrade_AutoID", Upgrade_AutoID);
            param[8] = new SqlParameter("@SearchByText", SearchByText);
            param[9] = new SqlParameter("@Category", Category);
            param[10] = new SqlParameter("@Action", Action);
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_Upgrade", param);
        }


        public int Insert_Update_Record()
        {

            SqlParameter[] param = new SqlParameter[26];

            param[0] = new SqlParameter("@ReceiptID",ReceiptID);
            param[1] = new SqlParameter("@Old_Pack_AutoID ",Old_Pack_AutoID);
            param[2] = new SqlParameter("@Old_Amount",Old_Amount);
            param[3] = new SqlParameter("@Qty",Qty);
            param[4] = new SqlParameter("@Old_Discount",Old_Discount);
            param[5] = new SqlParameter("@Old_FinalTotal", Old_FinalTotal);
            param[6] = new SqlParameter("@Old_PaidFee",Old_PaidFee);
            param[7] = new SqlParameter("@Old_Balance",Old_Balance);
            param[8] = new SqlParameter("@Old_StartDate",Old_StartDate);
            param[9] = new SqlParameter("@Old_EndDate", Old_EndDate);
            param[10] = new SqlParameter("@New_Pack_AutoID", New_Pack_AutoID);
            param[11] = new SqlParameter("@New_Amount",New_Amount);
            param[12] = new SqlParameter("@New_Discount",New_Discount);
            param[13] = new SqlParameter("@New_Total",New_Total);
            param[14] = new SqlParameter("@New_FinalTotal",New_FinalTotal);
            param[15] = new SqlParameter("@New_Balance",New_Balance);
            param[16] = new SqlParameter("@New_StartDate", New_StartDate);
            param[17] = new SqlParameter("@New_EndDate", New_EndDate);
            param[18] = new SqlParameter("@Upgrade_Date",Upgrade_Date);
            param[19] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[20] = new SqlParameter("@Executive_ID", Executive_ID);
            param[21] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[22] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[23] = new SqlParameter("@Bal_Receipt", Bal_Receipt);
            param[24] = new SqlParameter("@Upgrade_AutoID", Upgrade_AutoID);
            param[25] = new SqlParameter("@Action", Action);

            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Upgrade", param);
        }





    }
}
