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
   public class BalTermination
   {

       #region ------------Public Properties -----------

       public string Name { get; set; }
       public DateTime Date { get; set; }
       public string Reason { get; set; }
       public string Gender { get; set; }
       public string Status { get; set; }
       public int Member_Id { get; set; }
       public int Company_AutoID { get; set; }
       public int Branch_AutoID { get; set; }
       public string Category { get; set; }
       public string Action { get; set; }

       DataConnection obDataCon = new DataConnection();

       #endregion


       #region Get All Record of Termination Details
       public DataTable BindAllMembers()
       {
           SqlParameter[] param = new SqlParameter[3];
           param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Action", "SELECT_All");

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }
       #endregion

       #region Get record of Termination Details By Member ID
       public DataTable Bind_TerminationByID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[2] = new SqlParameter("@Member_Id", Member_Id);         
           param[3] = new SqlParameter("@Action", "SELECT_ByMemberID");

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }
       #endregion

       #region Get record of Termination Details By Member Name
       public DataTable Bind_TerminationByName()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Name", Name);
           param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[3] = new SqlParameter("@Action", "SELECT_ByMemberName");

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }
       #endregion

       #region Get record of Termination Details By Gender
       public DataTable Bind_TerminationByDDL()
       {
           SqlParameter[] param = new SqlParameter[5];
           param[0] = new SqlParameter("@Gender", Gender);
           param[1] = new SqlParameter("@Status", Status);
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[4] = new SqlParameter("@Action", Action);

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }
       #endregion

       #region Select record from User Information by ID
       public DataTable SelectByID_MemberInformation()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Member_AutoID", Member_Id);
           param[1] = new SqlParameter("@Action", "SELECT_BY_ID");
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }
       #endregion

         #region Insert record in Block Reason Table
       public int InsertBlockReason()
       {
           SqlParameter[] param = new SqlParameter[7];
           param[0] = new SqlParameter("@Action", "Insert_ReasonByID");
           param[1] = new SqlParameter("@Member_Id", Member_Id);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[4] = new SqlParameter("@Reason", Reason);
           param[5] = new SqlParameter("@Status", Status);
           param[6] = new SqlParameter("@Date", Date);

           return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }

         #endregion

       public DataTable BindGVByStatus()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Member_AutoID", Member_Id);
           param[1] = new SqlParameter("@Action", "SelectReasonBy_Id");
           param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }


       public DataTable BindBlockUnblockReport()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Action", "BlockUnblockReport");
           param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[3] = new SqlParameter("@Status", Status);
           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }

       public DataTable GetMember_AutoID()
       {
           SqlParameter[] param = new SqlParameter[4];
           param[0] = new SqlParameter("@Member_Id", Member_Id);           
           param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
           param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
           param[3] = new SqlParameter("@Action", "GetMember_AutoID");

           return obDataCon.DataTable_StoredProcedure_Parameter("SP_TerminationDetails", param);
       }
   }
}
