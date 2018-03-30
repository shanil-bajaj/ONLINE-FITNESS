using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataAccessLayer;

namespace BusinessAccessLayer
{
  public class BalChartBLL
    {
     
     
      DataConnection objdal = new DataConnection();
      public int Branch_AutoID { get; set; }
      public int Comp_AutoID { get; set; }
      public int Package_ID { get; set; }
      public int Month { get; set; }
      public int Year { get; set; }
      public int Month1 { get; set; }
      public int Year1 { get; set; }
      public int Brnach_Id { get; set; }
      public int Company_Id { get; set; }
      public string Package { get; set; }
      public int Duration { get; set; }


      public DataTable BindEnquiryDayWise()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
          param[1] = new SqlParameter("@Comp_AutoID", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "Select_Enquiry");
          return objdal.DataTable_StoredProcedure_Parameter("SP_EnquiryCharts", param);
      }

      public DataSet BindEnquiryDayWise1()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
          param[1] = new SqlParameter("@Comp_AutoID", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "Select_Enquiry");
          return objdal.DataSet_StoredProcedure_Parameter("SP_EnquiryCharts", param);
      }

      public DataTable BindAddMember()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "BindAddMember");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindMemberDayWise()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
          param[1] = new SqlParameter("@Comp_AutoID", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "Select_Member");
          return objdal.DataTable_StoredProcedure_Parameter("SP_EnquiryCharts", param);
      }

      public DataTable BindMemberEndDayWise()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
          param[1] = new SqlParameter("@Comp_AutoID", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "Select_MemberEnd");
          return objdal.DataTable_StoredProcedure_Parameter("SP_EnquiryCharts", param);
      }

      public DataTable BindExpenseDayWise()
      {
          //return objdal.BindWoSPData("usp_BindChartExpenseDayWise");
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Brnach_Id);
          param[1] = new SqlParameter("@Company_Id", Company_Id);
          param[2] = new SqlParameter("@Action", "ExpenseDayWise");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindCollectionDayWise()
      {
          //return objdal.BindWoSPData("usp_BindChartExpenseDayWise");
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Brnach_Id);
          param[1] = new SqlParameter("@Company_Id", Company_Id);
          param[2] = new SqlParameter("@Action", "CollectionDayWise");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataSet BindCollectionDayWise1()
      {
          //return objdal.BindWoSPData("usp_BindChartExpenseDayWise");
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Brnach_Id);
          param[1] = new SqlParameter("@Company_Id", Company_Id);
          param[2] = new SqlParameter("@Action", "CollectionDayWise");
          return objdal.DataSet_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataSet GetCourseName()
      {        
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "BindCourse");
          return objdal.DataSet_StoredProcedure_Parameter("SP_Charts", param);
      }
      public DataSet GetCourseName2()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "BindCourse2");
          return objdal.DataSet_StoredProcedure_Parameter("SP_Charts", param);
      }
      public DataTable GetCourseName1()
      {
          SqlParameter[] param = new SqlParameter[4];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Package", Package);
          param[3] = new SqlParameter("@Action", "BindCourse1");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindCoursePlanWise()
      {
          SqlParameter[] param = new SqlParameter[5];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Package", Package);
          param[3] = new SqlParameter("@Status", "Active");
          param[4] = new SqlParameter("@Action", "BindCoursePlanWise");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindActiveDeactiveByStatus()
      {
          SqlParameter[] param = new SqlParameter[4];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Status", "Active");
          param[3] = new SqlParameter("@Action", "BindActiveDeactiveByStatus");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataSet BindDate()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "GetDate");
          return objdal.DataSet_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindEnquiryFollowupStaffWise()
      {
          SqlParameter[] param = new SqlParameter[5];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);    
          param[2] = new SqlParameter("@Month", Month1);
          param[3] = new SqlParameter("@Year", Year1);
          param[4] = new SqlParameter("@Action", "CountFollowup");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataSet BindDate1()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "GetDate1");
          return objdal.DataSet_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindEnquiryStaffWise()
      {
          SqlParameter[] param = new SqlParameter[5];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Month", Month);
          param[3] = new SqlParameter("@Year", Year);
          param[4] = new SqlParameter("@Action", "CountEnquiry");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindActiveByCorse()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "BindCourseActiveMember");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }
      public DataTable GetplanName()
      {
          SqlParameter[] param = new SqlParameter[5];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Package", Package);
          param[3] = new SqlParameter("@Duration", Duration);
          param[4] = new SqlParameter("@Action", "GetplanName");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      //public DataSet BindMemeberInfo()
      //{
      //    SqlParameter[] param = new SqlParameter[5];
      //    param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
      //    param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
      //    param[2] = new SqlParameter("@Package", Package);
      //    param[3] = new SqlParameter("@Duration", Duration);
      //    param[4] = new SqlParameter("@Action", "BindMemeberInfo");
      //    return objdal.DataSet_StoredProcedure_Parameter("SP_Charts", param);
      //}
      public DataTable BindMember()
      {
          SqlParameter[] param = new SqlParameter[5];
          param[0] = new SqlParameter("@Branch_ID",Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id",Comp_AutoID);
          param[2] = new SqlParameter("@Package",Package);
          param[3] = new SqlParameter("@Duration",Duration);
          param[4] = new SqlParameter("@Action", "BindMember");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }
      public DataSet GetSourceName()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "BindSourceEnquiry");
          return objdal.DataSet_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable BindEnquiry()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Branch_AutoID);
          param[1] = new SqlParameter("@Company_Id", Comp_AutoID);
          param[2] = new SqlParameter("@Action", "BindEnquiry");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

      public DataTable Bindpresentmember()
      {
          SqlParameter[] param = new SqlParameter[3];
          param[0] = new SqlParameter("@Branch_ID", Brnach_Id);
          param[1] = new SqlParameter("@Company_Id", Company_Id);
          param[2] = new SqlParameter("@Action", "Bindpresentmember");
          return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
      }

    }
}
