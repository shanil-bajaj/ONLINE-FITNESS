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
   public class POSBalProduct
    {
        DataConnection obDataCon = new DataConnection();
        #region Customer property

        public int Auto_ID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public string HSNCode { get; set; }
        public string UOM { get; set; }
        public double Stock { get; set; }
        public double PurchaseRate { get; set; }
        public double SellingRate { get; set; }
        public double GST { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double IGST { get; set; }
        public int Company_AutoID { get; set; }
        public int Branch_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
        public string txtSearch { get; set; }
        #endregion

        #region  Enquiry Insert / Update / Delete
        public int Insert_Update_Delete()
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@Auto_ID", Auto_ID);
            param[1] = new SqlParameter("@ProductCode", ProductCode);
            param[2] = new SqlParameter("@ProductName", ProductName);
            param[3] = new SqlParameter("@Quantity", Quantity);
            param[4] = new SqlParameter("@HSNCode", HSNCode);
            param[5] = new SqlParameter("@Stock", Stock);
            param[6] = new SqlParameter("@PurchaseRate", PurchaseRate);
            param[7] = new SqlParameter("@SellingRate", SellingRate);
            param[8] = new SqlParameter("@CGST", CGST);
            param[9] = new SqlParameter("@GST", GST);
            param[10] = new SqlParameter("@SGST", SGST);
            param[11] = new SqlParameter("@IGST", IGST);
            param[12] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[13] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[14] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[15] = new SqlParameter("@Action", Action);
            param[16] = new SqlParameter("@Category", Category);
            param[17] = new SqlParameter("@UOM", UOM);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_POSProduct", param);
        }
        #endregion

        #region Check Exits or nt
        public DataTable Exits()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Exits");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@ProductCode", ProductCode);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_POSProduct", param);
        }
        #endregion 

        public int Delete()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[1] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@Auto_ID", Auto_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_POSProduct", param);
        }

        #region Get Record by Enq id
        public DataTable Get_Edit()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Edit");
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[3] = new SqlParameter("@Auto_ID", Auto_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_POSProduct", param);
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
            param[4] = new SqlParameter("@txtSearch", txtSearch);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_POSProduct", param);
        }
        #endregion
    }
}
