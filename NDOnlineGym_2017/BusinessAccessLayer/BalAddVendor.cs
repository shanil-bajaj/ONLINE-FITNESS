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
    public class BalAddVendor
    {
        #region
        DataConnection obDataCon = new DataConnection();

        public int Vendor_AutoID { get; set; }
        public int Vendor_ID { get; set; }      
        public string Name { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string GSTNo { get; set; }

        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }
        public int Login_AutoID { get; set; }
        

        public string SearchByText { get; set; }

        public string Category { get; set; }
        public string Action { get; set; }

        #endregion


        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[3] = new SqlParameter("@SearchByText", SearchByText);
            param[4] = new SqlParameter("@Vendor_AutoID", Vendor_AutoID);
            param[5] = new SqlParameter("@Contact1", Contact1);
            param[6] = new SqlParameter("@Category", Category);
            param[7] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AddVendor", param);
        }

        public int Insert_VendorInformation()
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@Vendor_AutoID", Vendor_AutoID);
            param[1] = new SqlParameter("@Vendor_ID", Vendor_ID);
            param[2] = new SqlParameter("@Name", Name);
            param[3] = new SqlParameter("@Contact1", Contact1);
            param[4] = new SqlParameter("@Contact2", Contact2);
            param[5] = new SqlParameter("@Address", Address);
            param[6] = new SqlParameter("@State", State);
            param[7] = new SqlParameter("@Pincode", Pincode);
            param[8] = new SqlParameter("@GSTNo", GSTNo);
            param[9] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[10] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[11] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[12] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_AddVendor", param);
        }

        public bool Check_ExistingVendorId()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Vendor_ID", Vendor_ID);
            param[1] = new SqlParameter("@Login_AutoID", Login_AutoID);
            param[2] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[3] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[4] = new SqlParameter("@Action", Action);

            int i = obDataCon.Int_StoredProcedure_Parameter("SP_AddVendor", param);
            if (i > 0)
                return true;
            else
                return false;

        }

    }
}
