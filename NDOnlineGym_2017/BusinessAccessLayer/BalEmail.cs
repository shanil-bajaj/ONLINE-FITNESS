using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Data.Common;
using System.Data.SqlTypes;
using DataAccessLayer;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
   public class BalEmail
    {
        #region ------------Public Properties -----------

       public int EmailLogin_AutoID { get; set; }
       public string EmailID { get; set; }
       public string Password { get; set; }
       public int Company_AutoID { get; set; }
       public int Branch_AutoID { get; set; }
       public string Status { get; set; }
       public int Email_AutoID { get; set; }
       public string Action { get; set; }
       public string EmailType { get; set; }
       public string EmailContent { get; set; }
       public string Header { get; set; }
       public string Footer { get; set; }
       public string category { get; set; }
       public string action { get; set; }
       public int EmailContent_AutoID { get; set; }
       public DateTime FromDate { get; set; }
       public DateTime ToDate { get; set; }

       DataConnection obDataCon = new DataConnection();
        #endregion



       public int Insert_EMAILLogin()
       {
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Action", "Insert");
           param[1] = new SqlParameter("@EmailID", EmailID);
           param[2] = new SqlParameter("@Password", Password);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[4] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public DataTable Select_DetailsemailLogin()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "SelectAll");
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           //param[1] = new SqlParameter("@Branch_ID", Branch_ID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public int Update_EmailLogin()
       {
           SqlParameter[] param = new SqlParameter[6];
           param[0] = new SqlParameter("@Action", "Update");
           param[1] = new SqlParameter("@EmailID", EmailID);
           param[2] = new SqlParameter("@EmailLogin_AutoID", EmailLogin_AutoID);
           param[3] = new SqlParameter("@Password", Password);
           param[4] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[5] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_EmailDetails", param);
       }
       public DataTable Select_DetailEmailStatusLogin()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "SelectStatus");
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           //param[1] = new SqlParameter("@Branch_ID", Branch_ID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public int Insert_EmailStatus()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "StatusInsert");
           param[1] = new SqlParameter("@Status", Status);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public int Update_EmailStatus()
       {
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Action", "StatusUpdate");
           param[1] = new SqlParameter("@Status", Status);
           param[2] = new SqlParameter("@Email_AutoID", Email_AutoID);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[4] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public DataSet All_Data()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "GetData");
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.DataSet_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public int Insert_Header()
       {
           SqlParameter[] param = new SqlParameter[6];
           param[0] = new SqlParameter("@Action", "Master_Insert");
           param[1] = new SqlParameter("@Header", Header);
           param[2] = new SqlParameter("@category", category);
           param[3] = new SqlParameter("@EmailType", EmailType);
           //param[4] = new SqlParameter("@EmailContent_AutoID", EmailContent_AutoID);
           param[4] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[5] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public int Insert_Footer()
       {
           SqlParameter[] param = new SqlParameter[6];
           param[0] = new SqlParameter("@Action", "Master_Insert");
           param[1] = new SqlParameter("@Footer", Footer);
           param[2] = new SqlParameter("@category", category);
           param[3] = new SqlParameter("@EmailType", EmailType);
           //param[4] = new SqlParameter("@EmailContent_AutoID", EmailContent_AutoID);
           param[4] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[5] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public DataTable All_MemberInfo()
       {
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Action", "All_MemberInfo");
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[3] = new SqlParameter("@FromDate", FromDate);
           param[4] = new SqlParameter("@ToDate", ToDate);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_EmailDetails", param);
       }

       public DataTable Get_companyEmailID()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Action", "Get_companyEmailID");
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_EmailDetails", param);
       }
    }
}
