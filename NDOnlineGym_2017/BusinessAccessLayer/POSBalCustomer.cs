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
  public class POSBalCustomer
    {

        DataConnection obDataCon = new DataConnection();
        #region Customer property

        public int Auto_ID { get; set; }
        public int Customer_ID { get; set; } 
        public string Name { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }
        public string GSTNo { get; set; }
        public int Login_AutoID { get; set; }
        public int Company_ID { get; set; }
        public int Branch_ID { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
        public string searchTxt { get; set; }

        #endregion


        #region Get CustomerID
        public DataTable Get_CustomerID()
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Action", "Get_CutomerID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_POSCustomer", param);
        }
        #endregion

       #region  Enquiry Insert / Update / Delete
        public int Insert_Update_Delete()
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@Auto_ID", Auto_ID);
            param[1] = new SqlParameter("@Customer_ID", Customer_ID);
            param[2] = new SqlParameter("@Name", Name);
            param[3] = new SqlParameter("@Contact1", Contact1);
            param[4] = new SqlParameter("@Contact2", Contact2);
            param[5] = new SqlParameter("@Address", Address);
            param[6] = new SqlParameter("@State", State);
            param[7] = new SqlParameter("@Pincode", Pincode);
            param[8] = new SqlParameter("@GSTNo", GSTNo);
            param[9] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[10] = new SqlParameter("@Company_ID", Company_ID);
            param[11] = new SqlParameter("@Branch_ID", Branch_ID);
            param[12] = new SqlParameter("@Action", Action);
            param[13] = new SqlParameter("@Category", Category);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_POSCustomer", param);

        }
       #endregion

        #region Check Exits or nt
        public DataTable Exits()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Exits");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Customer_ID", Customer_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_POSCustomer", param);
        }
        #endregion 

        #region Gridview Search
        public DataTable Get_Search()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Action", "Searching");
            param[1] = new SqlParameter("@Category", Category);
            param[2] = new SqlParameter("@Company_ID", Company_ID);
            param[3] = new SqlParameter("@Branch_ID", Branch_ID);
            param[4] = new SqlParameter("@searchTxt", searchTxt);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_POSCustomer", param);
        }
        #endregion

        public int Delete()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Company_ID", Company_ID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Action", Action);
            param[3] = new SqlParameter("@Auto_ID", Auto_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_POSCustomer", param);
        }

        #region Get Record by Enq id
        public DataTable Get_Edit()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", "Edit");
            param[1] = new SqlParameter("@Company_ID", Company_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            param[3] = new SqlParameter("@Auto_ID", Auto_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_POSCustomer", param);
        }
        #endregion
    }
}   
