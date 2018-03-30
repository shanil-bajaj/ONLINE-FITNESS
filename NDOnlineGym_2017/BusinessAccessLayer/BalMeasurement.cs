using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
    public  class BalMeasurement
    {
        DataConnection obDataCon = new DataConnection();
        #region                    -----------Measurement Properties----------

        public string Action { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Member_Id { get; set; }
        public int Member_AutoID { get; set; }
        public int Measurement_AutoId { get; set; }
        public string SearchByText { get; set; }
        public string Contact { get; set; }
        
        public string Arms { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Chest { get; set; }
        public string Waist { get; set; }
        public string Thigh { get; set; }
        public DateTime? Date { get; set; }
        public int? Programmer_AutoID { get; set; }
        public int Executive_ID { get; set; }
        public string Fat { get; set; }
        public string Bmass { get; set; }
        public string Mmass { get; set; }
        public string BMI { get; set; }
        public string DCI { get; set; }
        public string Age { get; set; }
        public string Water { get; set; }
        public string Vfat { get; set; }
        public string Neck { get; set; }
        public string UpperArms { get; set; }
        public string ForArms { get; set; }
        public string Shoulder { get; set; }
        public string Hips { get; set; }
        public string ChestExpanded { get; set; }
        public string UpperAbdomen { get; set; }
        public string LowerAbdomen { get; set; }
        public string Calf { get; set; }
        public DateTime? NextFollowupDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Category { get; set; }
        public string searchTxt { get; set; }
        #endregion


        #region----------Bind Programmer------------

        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@SearchByText", SearchByText);
            param[3] = new SqlParameter("@Member_Id", Member_Id);
            param[4] = new SqlParameter("@Contact", Contact);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@Action",Action);
            param[7] = new SqlParameter("@Programmer_AutoID", Programmer_AutoID);
            //param[8] = new SqlParameter("@StartDate", StartDate);
            //param[9] = new SqlParameter("@EndDate", EndDate);
            //param[8] = new SqlParameter("@Arms", Arms);
            //param[9] = new SqlParameter("@Weight", Weight);
            //param[10] = new SqlParameter("@Height", Height);
            //param[11] = new SqlParameter("@Waist", Waist);
            //param[12] = new SqlParameter("@Thigh", Thigh);
            //param[13] = new SqlParameter("@Chest", Chest);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Measurement", param);
        }

        public int Insert_Update_Delete()
        {
            SqlParameter[] param = new SqlParameter[36];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@SearchByText", SearchByText);
            param[3] = new SqlParameter("@Member_Id", Member_Id);
            param[4] = new SqlParameter("@Contact", Contact);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@Action", Action);
            param[7] = new SqlParameter("@Programmer_AutoID", Programmer_AutoID);
            param[8] = new SqlParameter("@Arms", Arms);
            param[9] = new SqlParameter("@Weight", Weight);
            param[10] = new SqlParameter("@Height", Height);
            param[11] = new SqlParameter("@Waist", Waist);
            param[12] = new SqlParameter("@Thigh", Thigh);
            param[13] = new SqlParameter("@Chest", Chest);
            param[14] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[15] = new SqlParameter("@Date", Date);
            param[16] = new SqlParameter("@NextFollowupDate", NextFollowupDate);
            param[17] = new SqlParameter("@Executive_ID", Executive_ID);
            param[18] = new SqlParameter("@Measurement_AutoId", Measurement_AutoId);
            param[19] = new SqlParameter("@Fat", Fat);
            param[20] = new SqlParameter("@Bmass", Bmass);
            param[21] = new SqlParameter("@BMI", BMI);
            param[22] = new SqlParameter("@DCI", DCI);
            param[23] = new SqlParameter("@Age", Age);
            param[24] = new SqlParameter("@Water", Water);
            param[25] = new SqlParameter("@Vfat", Vfat);
            param[26] = new SqlParameter("@Neck", Neck);
            param[27] = new SqlParameter("@UpperArms", UpperArms);
            param[28] = new SqlParameter("@ForArms", ForArms);
            param[29] = new SqlParameter("@Shoulder", Shoulder);
            param[30] = new SqlParameter("@Hips", Hips);
            param[31] = new SqlParameter("@ChestExpanded", ChestExpanded);
            param[32] = new SqlParameter("@UpperAbdomen", UpperAbdomen);
            param[33] = new SqlParameter("@Calf", Calf);
            param[34] = new SqlParameter("@Mmass", Mmass);
            param[35] = new SqlParameter("@LowerAbdomen", LowerAbdomen);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Measurement", param);
        }

        #endregion   
    
        public int Exits()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Exits");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            return obDataCon.Int_StoredProcedure_Parameter("SP_Measurement", param);
        }

        public int Delete()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@Measurement_AutoId", Measurement_AutoId);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Measurement", param);
        }


        public DataTable Get_Edit()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "SelectByExID");
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Measurement_AutoId", Measurement_AutoId);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Measurement", param);
        }

        public DataTable Get_Search()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Action",Action);
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            param[5] = new SqlParameter("@StartDate", StartDate);
            param[6] = new SqlParameter("@EndDate", EndDate);
            //param[7] = new SqlParameter("@StartDate", StartDate);
            //param[8] = new SqlParameter("@EndDate", EndDate);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Measurement", param);
        }

        public DataTable SelectGridData()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@StartDate", StartDate);
            param[3] = new SqlParameter("@EndDate", EndDate);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            param[5] = new SqlParameter("@Category", Category);
            param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Measurement", param);
        }

        public DataTable GetMemeberID()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Measurement_AutoId", Measurement_AutoId);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            //param[3] = new SqlParameter("@EndDate", EndDate);
            param[4] = new SqlParameter("@Action", "GetMemID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Measurement", param);
        }
    }
}
