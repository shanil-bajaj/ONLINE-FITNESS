using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BusinessAccessLayer;
using DataAccessLayer;
using System.Data;
using System.Data.SqlTypes;

namespace BusinessAccessLayer
{
    public class BalExpense
    {
        DataConnection obDataCon = new DataConnection();  
        // Expanse Proprties
        #region
        public int Ex_AutoId { get; set; }
        public int Executive_ID { get; set; }
        public int Ex_Id { get; set; }
         public string name { get; set; }
         //public DateTime Ex_Date { get; set; }
         public double amount { get; set; }
         public double Totalamount { get; set; }
         public double Taxamount { get; set; }
         public string note1 { get; set; }
         public string note2 { get; set; }
         public int ExGrp_AutoId { get; set; }
         public int pay_AutoId { get; set; }
         public string Pay_Details { get; set; }
         public int company_Id { get; set; }
         public int Branch_Id { get; set; }
         public int Login_AutoId { get; set; }
        // public DateTime ExpDate { get; set; }
         public SqlDateTime Exp_Date { get; set; }
         public string Category { get; set; }
         public string searchTxt { get; set; }
         public string Action { get; set; }
         public DateTime? StartDate { get; set; }
         public DateTime? EndDate { get; set; }
        #endregion

         
         public int Insert_Update_Delete()
         {
             SqlParameter[] param = new SqlParameter[17];
             param[0] = new SqlParameter("@Ex_Id", Ex_Id);
             param[1] = new SqlParameter("Exp_Date", Exp_Date);
             param[2] = new SqlParameter("@name", name);
             param[3] = new SqlParameter("@amount", amount);
             param[4] = new SqlParameter("@Totalamount", Totalamount);
             param[5] = new SqlParameter("@Taxamount", Taxamount);
             param[6] = new SqlParameter("@note1", note1);
             param[7] = new SqlParameter("@note2", note2);
             param[8] = new SqlParameter("@company_Id", company_Id);
             param[9] = new SqlParameter("@Branch_Id", Branch_Id);
             param[10] = new SqlParameter("@Pay_Details", Pay_Details);
             param[11] = new SqlParameter("@Login_AutoId", Login_AutoId);
             param[12] = new SqlParameter("@ExGrp_AutoId", ExGrp_AutoId);
             param[13] = new SqlParameter("@pay_AutoId", pay_AutoId);
             param[14] = new SqlParameter("@Ex_AutoId", Ex_AutoId);
             param[15] = new SqlParameter("@Executive_ID", Executive_ID);
             param[16] = new SqlParameter("@Action", Action);
             return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Expense", param);
         }
        
         public int Exits()
         {
             SqlParameter[] param = new SqlParameter[4];
             param[0] = new SqlParameter("@Action", "Exits");
             param[1] = new SqlParameter("@company_Id", company_Id);
             param[2] = new SqlParameter("@Branch_Id", Branch_Id);
             param[3] = new SqlParameter("@Ex_Id", Ex_Id);
             return obDataCon.Int_StoredProcedure_Parameter("SP_Expense", param);

         }

         public DataTable Get_ExpenseGroup()
         {
             SqlParameter[] param = new SqlParameter[3];
             param[0] = new SqlParameter("@Action", "Bind_ExpenseGroup");
             param[1] = new SqlParameter("@company_Id", company_Id);
             param[2] = new SqlParameter("@Branch_Id", Branch_Id);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable Get_ExecutiveName()
         {
             SqlParameter[] param = new SqlParameter[3];
             param[0] = new SqlParameter("@Action", "Bind_Executive");
             param[1] = new SqlParameter("@company_Id", company_Id);
             param[2] = new SqlParameter("@Branch_Id", Branch_Id);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable Get_PaymentType()
         {
             SqlParameter[] param = new SqlParameter[3];
             param[0] = new SqlParameter("@Action", "Bind_PaymentType");
             param[1] = new SqlParameter("@company_Id", company_Id);
             param[2] = new SqlParameter("@Branch_Id", Branch_Id);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable Get_Expenseid()
         {

             SqlParameter[] param = new SqlParameter[3];
             param[0] = new SqlParameter("@Action", "Get_ExpenseID");
             param[1] = new SqlParameter("@company_Id", company_Id);
             param[2] = new SqlParameter("@Branch_Id", Branch_Id);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable Get_Edit()
         {
             SqlParameter[] param = new SqlParameter[4];
             param[0] = new SqlParameter("@Action", "SelectByExID");
             param[1] = new SqlParameter("@company_Id", company_Id);
             param[2] = new SqlParameter("@Branch_Id", Branch_Id);
             param[3] = new SqlParameter("@Ex_AutoId", Ex_AutoId);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public int Delete()
         {
             SqlParameter[] param = new SqlParameter[4];
             param[0] = new SqlParameter("@company_Id", company_Id);
             param[1] = new SqlParameter("@Branch_Id", Branch_Id);
             param[2] = new SqlParameter("@Action", Action);
             param[3] = new SqlParameter("@Ex_AutoId", Ex_AutoId);
             return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable Select_AllForReport()
         {
             SqlParameter[] param = new SqlParameter[5];
             param[0] = new SqlParameter("@company_Id", company_Id);
             param[1] = new SqlParameter("@Branch_Id", Branch_Id);
             param[2] = new SqlParameter("@StartDate", StartDate);
             param[3] = new SqlParameter("@EndDate", EndDate);
             param[4] = new SqlParameter("@Action", "SelectAllForReoprt");

             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         } 

         public DataTable Select_All()
         {
             SqlParameter[] param = new SqlParameter[3];
             param[0] = new SqlParameter("@company_Id", company_Id);
             param[1] = new SqlParameter("@Branch_Id", Branch_Id);
             param[2] = new SqlParameter("@Action", "Select");

             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable Get_Search()
         {
             SqlParameter[] param = new SqlParameter[7];
             param[0] = new SqlParameter("@Action",Action);
             param[1] = new SqlParameter("@Category", Category);
             param[2] = new SqlParameter("@company_Id", company_Id);
             param[3] = new SqlParameter("@Branch_Id", Branch_Id);
             param[4] = new SqlParameter("@searchTxt", searchTxt);
             param[5] = new SqlParameter("@StartDate", StartDate);
             param[6] = new SqlParameter("@EndDate", EndDate);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable GetCompanyinfByBranchIdId()
         {
             SqlParameter[] param = new SqlParameter[2];
             param[0] = new SqlParameter("@Action", "SelectcomapnyData");
             param[1] = new SqlParameter("@Branch_Id", Branch_Id);
             //param[2] = new SqlParameter("@Ex_AutoId", Ex_AutoId);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         public DataTable GetExpenseInformation()
         {
             SqlParameter[] param = new SqlParameter[2];
            // param[0] = new SqlParameter("@Branch_ID", Branch_ID);
             param[0] = new SqlParameter("@Action", "SelectExistingExpenase");
             param[1] = new SqlParameter("@Ex_AutoId", Ex_AutoId);
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }

         //public DataSet GetCompanyinfByBranchIdId()
         //{
         //    Database defaultDB = Connection.GetDefaultDBConnection();


         //    string strSqlCommand = "SP_GetCompanyinfByBranchIdId";
         //    DbCommand dbCommand = defaultDB.GetStoredProcCommand(strSqlCommand);

         //    defaultDB.AddInParameter(dbCommand, "@Branch_ID", DbType.Int32, this.Branch_ID);

         //    return defaultDB.ExecuteDataSet(dbCommand);
         //}

         public bool chkExistingExpenseId()
         {
             SqlParameter[] param = new SqlParameter[4];
             param[0] = new SqlParameter("@Action", "Exits");
             param[1] = new SqlParameter("@company_Id", company_Id);
             param[2] = new SqlParameter("@Branch_Id", Branch_Id);
             param[3] = new SqlParameter("@Ex_Id", Ex_Id);

             int i = obDataCon.Int_StoredProcedure_Parameter("SP_Expense", param);
             if (i > 0)
                 return true;
             else
                 return false;
         }

         public DataTable Report_SelectByExpGrp()
         {
             SqlParameter[] param = new SqlParameter[6];
             param[0] = new SqlParameter("@company_Id", company_Id);
             param[1] = new SqlParameter("@Branch_Id", Branch_Id);
             param[2] = new SqlParameter("@StartDate", StartDate);
             param[3] = new SqlParameter("@EndDate", EndDate);
             param[4] = new SqlParameter("@ExGrp_AutoId", ExGrp_AutoId);
             param[5] = new SqlParameter("@Action", "SelectReportByExpGrp");
             return obDataCon.DataTable_StoredProcedure_Parameter("SP_Expense", param);
         }
    }
}
