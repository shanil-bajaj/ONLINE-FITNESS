using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace BusinessAccessLayer
{
    public class BalReports
    {
        DataConnection obDataCon = new DataConnection();

        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public string MemberType { get; set; }
        public string MemberName { get; set; }
        public SqlDateTime StartDate { get; set; }
        public SqlDateTime EndDate { get; set; }
        public int Staff_AutoID { get; set; }

        public int ExecutiveID { get; set; }
        public int PaymentModeID { get; set; }

        public string searchTxt { get; set; }
        public string Category { get; set; }
        public string Executive { get; set; }
        public string PaymentMode { get; set; }

        public string Action { get; set; }
        public int Member_AutoID { get; set; }

        public string Action_REf { get; set; }
        /// <summary>
        /// Payment Report
        /// </summary>
        /// <returns></returns>
        public object Report_PaymentReport()
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@StartDate", StartDate);
            param[3] = new SqlParameter("@EndDate", EndDate);
            param[4] = new SqlParameter("@Executive", Executive);
            param[5] = new SqlParameter("@MemberType", MemberType);
            param[6] = new SqlParameter("@Action", "PaymentReports");
            param[7] = new SqlParameter("@PaymentMode", PaymentMode);
            param[8] = new SqlParameter("@ExecutiveID", ExecutiveID);
            param[9] = new SqlParameter("@PaymentModeID", PaymentModeID);
            param[10] = new SqlParameter("@Category", Category);
            param[11] = new SqlParameter("@searchTxt", searchTxt);
            //param[12] = new SqlParameter("@Executive", Executive);
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_Reports", param);
        }

        public DataTable Get_ExecutiveName()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "Bind_Executive");
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_Reports", param);
        }
        /// <summary>
        /// Balance Report
        /// </summary>
        /// <returns></returns>
        public DataTable MemberName_BalNotNull()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "MemberName_BalNotNull");
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_BalanceReport", param);
        }

        public DataTable Report_Balance()
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);          
            param[2] = new SqlParameter("@Action", "BalanceReports");
            param[3] = new SqlParameter("@Action_REf", Action_REf);
            param[4] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[5] = new SqlParameter("@StartDate", StartDate);
            param[6] = new SqlParameter("@EndDate", EndDate);
            param[7] = new SqlParameter("@Category", Category);
            param[8] = new SqlParameter("@MemberName", MemberName);
            param[9] = new SqlParameter("@searchTxt", searchTxt);
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_BalanceReport", param);
        }

        public DataTable BindCollectionData()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", "CollectionReports");
            param[3] = new SqlParameter("@StartDate", StartDate);
            param[4] = new SqlParameter("@EndDate", EndDate);
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_Reports", param);
        }

        public DataTable Collection_GetSearch()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@StartDate", StartDate);
            param[4] = new SqlParameter("@EndDate", EndDate);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@searchTxt", searchTxt);
           // param[6] = new SqlParameter("@searchTxt", searchTxt);
            return obDataCon.DataTable_StoredProcedure_Parameter("Sp_Reports", param);
        }
    
        public DataSet Collection_TotalSum()
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@StartDate", StartDate);
            param[4] = new SqlParameter("@EndDate", EndDate);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@searchTxt", searchTxt);
            param[7] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[8] = new SqlParameter("@Action_REf", Action_REf);
            
            // param[6] = new SqlParameter("@searchTxt", searchTxt);
            return obDataCon.DataSet_StoredProcedure_Parameter("SP_CollectionReport", param);
        }

        public DataSet Report_Balance_TotalSum()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            //param[2] = new SqlParameter("@Action", "BalanceReports");
            param[3] = new SqlParameter("@Action_REf", Action_REf);
            param[4] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[5] = new SqlParameter("@StartDate", StartDate);
            param[6] = new SqlParameter("@EndDate", EndDate);

           return obDataCon.DataSet_StoredProcedure_Parameter("Sp_Reports", param);
        }
    }
}
