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
    public class BalPackage
    {
        DataConnection obDataCon = new DataConnection();
        #region Enquiry property

        public int Pack_AutoID { get; set; }
        public string Package { get; set; }
        public string Duration { get; set; }
        public int Session { get; set; }
        public Double Amount { get; set; }
        public Double Discount { get; set; }
        public string Status { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

        //public string FromTime { get; set; }
        //public string ToTime { get; set; }
        public string Type { get; set; }

        public string Action { get; set; }
        public string Category { get; set; }
        public string searchTxt { get; set; }

        #endregion

        #region  Enquiry Insert / Update 
        public int Insert_Update()
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@Package", Package);
            param[1] = new SqlParameter("@Duration", Duration);
            param[2] = new SqlParameter("@Session", Session);
            param[3] = new SqlParameter("@Amount", Amount);
            param[4] = new SqlParameter("@Discount", Discount);
            param[5] = new SqlParameter("@Status", Status);
            param[6] = new SqlParameter("@FromTime", FromTime);
            param[7] = new SqlParameter("@ToTime", ToTime);
            param[8] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[9] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[10] =new SqlParameter("@Action", Action);
            param[11] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            param[12] = new SqlParameter("@Type", Type);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Package", param);
        }
        #endregion

        #region Gridview Search
        public DataTable Get_Search()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", "Searching");
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Package", param);
        }
         public DataTable Get_Search_PT()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", "Searching_PT");
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Package", param);
        }
        
        #region Get Record by Enq id
        public DataTable Get_Edit()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Edit");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_Package", param);
        }
        #endregion
        public int Delete()
        {          
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Delete");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_Package", param);
        }

        public int ChkPack_ID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "ChkPack_ID");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Pack_AutoID", Pack_AutoID);
            int res;
            res = obDataCon.Int_StoredProcedure_Parameter("SP_Package", param);
            return res;
        }

        //#region Check Exits or nt
        //public DataTable Exits()
        //{
        //    SqlParameter[] param = new SqlParameter[4];
        //    param[0] = new SqlParameter("@Action", "Exits");
        //    param[1] = new SqlParameter("@Company_ID", Company_ID);
        //    param[2] = new SqlParameter("@Branch_ID", Branch_ID);
        //    param[3] = new SqlParameter("@Enq_ID", Enq_ID);
        //    return obDataCon.DataTable_StoredProcedure_Parameter("SP_Enquiry", param);
        //}
        #endregion 


    }
}
