using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BalStaffRegistration
    {
        DataConnection obDataCon = new DataConnection();

        public Int32 Staff_AutoID { get; set; }
        public Int32 Staff_ID { get; set; }
        public Int32 Staff_ID1 { get; set; }
        public Int32 Comp_ID { get; set; }
        public Int32? Desig_ID { get; set; }
        public Int32? Dept_ID { get; set; }
        public Int32 Branch_ID { get; set; }
        public Int32 Executive_ID { get; set; }

        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Gender { get; set; }
        public DateTime Reg_date { get; set; }
        public DateTime? DOB { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Email { get; set; }
        public string Rights { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public string IDProofPath { get; set; }
        public float Salary { get; set; }
        public Int32? Shift { get; set; }
        public string CardNo { get; set; }
        public string ThumbImgPath { get; set; }
        public string Soft_Hand { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string con { get; set; }
        public string Action { get; set; }
        public string authority { get; set; }
        public string Category { get; set; }
        public string Named { get; set; }
        public string Namede { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string searchTxt { get; set; }


        #region Select All records from Branch Information
        public DataTable Select_All()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "SELECT");
            param[1] = new SqlParameter("@Company_ID", Comp_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        #region Get Designation Name
        public DataTable GetDesignationName()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Select_Designation");
            param[1] = new SqlParameter("@Company_ID", Comp_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        #region Get Shift Name
        public DataTable GetShift()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Select_Sift");
            param[1] = new SqlParameter("@Company_ID", Comp_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        #region Get Department Name
        public DataTable GetDepartmentName()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Select_Department");
            param[1] = new SqlParameter("@Company_ID", Comp_ID);
            param[2] = new SqlParameter("@Branch_ID", Branch_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        #region Select record from Branch Information For Autogenerared StaffID1
        public DataTable SearchCategory()
        {
            DataTable dt = new DataTable();
            SqlParameter[] Param = new SqlParameter[15];
            Param[0] = new SqlParameter("@FName", Fname);
            Param[1] = new SqlParameter("@LName", Lname);
            Param[2] = new SqlParameter("@Con", con);
            Param[3] = new SqlParameter("@Dept_ID", Dept_ID);
            Param[4] = new SqlParameter("@Company_ID", Comp_ID);
            Param[5] = new SqlParameter("@Username", Username);
            Param[6] = new SqlParameter("@Status", Status);
            Param[7] = new SqlParameter("@Rights", Rights);
            Param[8] = new SqlParameter("@Gender", Gender);
            Param[9] = new SqlParameter("@Action", Action);
            Param[10] = new SqlParameter("@Category", Category);
            Param[11] = new SqlParameter("@Staff_ID1", Staff_ID1);
            Param[12] = new SqlParameter("@Branch_ID", Branch_ID);
            Param[13] = new SqlParameter("@named", Named);
            Param[14] = new SqlParameter("@Namede", Namede);


            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", Param);

        }
        #endregion

        #region Check ID
        public int checkID()
        {
            int res;
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Staff_ID1", Staff_ID1);
            param[1] = new SqlParameter("@Staff_AutoID", Staff_AutoID);
            param[2] = new SqlParameter("@Company_ID", Comp_ID);
            param[3] = new SqlParameter("@Branch_ID", Branch_ID);
            param[4] = new SqlParameter("@Action", Action);
            res = obDataCon.Int_StoredProcedure_Parameter("SP_StaffResistration", param);
            return res;
        }
        #endregion

        #region insert record in database of staff registration
        public int Insert_StaffRegistration()
        {
            SqlParameter[] param = new SqlParameter[25];
            param[0]  = new SqlParameter("@FName", Fname);
            param[1]  = new SqlParameter("@LName", Lname);
            param[2]  = new SqlParameter("@RegDate", Reg_date);
            param[3]  = new SqlParameter("@DOB", DOB);
            param[4]  = new SqlParameter("@Contact1", Contact1);
            param[5]  = new SqlParameter("@Contact2", Contact2);
            param[6]  = new SqlParameter("@Address", Address);
            param[7]  = new SqlParameter("@Email", Email);
            param[8]  = new SqlParameter("@Dept_ID", Dept_ID);
            param[9]  = new SqlParameter("@Company_ID", Comp_ID);
            param[10] = new SqlParameter("@Branch_ID", Branch_ID);
            param[11] = new SqlParameter("@Status", Status);
            param[12] = new SqlParameter("@Rights", Rights);
            param[13] = new SqlParameter("@ImagePath", ImagePath);
            param[14] = new SqlParameter("@IDProofPath", IDProofPath);
            param[15] = new SqlParameter("@Salary", Salary);
            param[16] = new SqlParameter("@Shift", Shift);
            param[17] = new SqlParameter("@CardNo", CardNo);
            param[18] = new SqlParameter("@Thumb", ThumbImgPath);
            param[19] = new SqlParameter("@Desig_ID", Desig_ID);
            param[20] = new SqlParameter("@Gender", Gender);
            param[21] = new SqlParameter("@Staff_ID1", Staff_ID1);
            param[22] = new SqlParameter("@Staff_ID", Staff_ID);
            param[23] = new SqlParameter("@Executive_ID", Executive_ID);
            param[24] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_StaffResistration", param);

        }
        #endregion

        #region Delete
        public int Delete_Staff()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Staff_ID", Staff_ID);
            param[1] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        #region--Check Contact While Saving--
        public int Contactcheck()
        {
            int res;
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Contact1", Contact1);
            param[1] = new SqlParameter("@Action", Action);
            param[2] = new SqlParameter("@Company_ID", Comp_ID);
            param[3] = new SqlParameter("@Branch_ID", Branch_ID);
            res = obDataCon.Int_StoredProcedure_Parameter("SP_StaffResistration", param);
            return res;
        }
        #endregion


        #region--Check Contact While Updating--
        public int Contactcheck1()
        {
            int res;
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Contact1", Contact1);
            param[1] = new SqlParameter("@Action", Action);
            param[2] = new SqlParameter("@Staff_ID", Staff_AutoID);
            param[3] = new SqlParameter("@Company_ID", Comp_ID);
            param[4] = new SqlParameter("@Branch_ID", Branch_ID);
            res = obDataCon.Int_StoredProcedure_Parameter("SP_StaffResistration", param);
            return res;
        }
        #endregion


        #region Select record from User Information by ID
        public DataTable SelectByID_UserInformation()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Staff_Id", Staff_ID);
            param[1] = new SqlParameter("@Action", "SELECT_BY_ID");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        #region Select record from User Information by ID
        public DataTable SelectByIDNameContact_StaffInformation()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Staff_AutoID", Staff_AutoID);
            param[1] = new SqlParameter("@Staff_ID1", Staff_ID1);
            //param[1] = new SqlParameter("@Name", Name);
            param[2] = new SqlParameter("@Contact1", Contact1);
            param[3] = new SqlParameter("@Company_ID", Comp_ID);
            param[4] = new SqlParameter("@Branch_ID", Branch_ID);
            param[5] = new SqlParameter("@authority", authority);
            param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        #region Select record from Branch Information For Autogenerared StaffID1
        public DataTable Get_StaffID1()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Company_ID", Comp_ID);
            param[1] = new SqlParameter("@Branch_Id", Branch_ID);
            param[2] = new SqlParameter("@Action", "Get_StaffID1");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }
        #endregion

        public DataTable GetExecutiveByID_ByBranch()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Staff_Id", Staff_AutoID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@Action", "SELECT_Staff_by_Branch");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }

        public DataTable GetExecutiveByID_WithoutBranch()
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Staff_Id", Staff_AutoID);
            param[1] = new SqlParameter("@Action", "SELECT_Staff_withoutBranch");
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_StaffResistration", param);
        }

        #region Gridview Search
        public DataTable Get_Search()
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Company_ID", Comp_ID);
            param[1] = new SqlParameter("@Branch_ID", Branch_ID);
            param[2] = new SqlParameter("@searchTxt", searchTxt);
            param[3] = new SqlParameter("@Category", Category);
            param[4] = new SqlParameter("@FromDate", FromDate);
            param[5] = new SqlParameter("@ToDate", ToDate);
            param[6] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SearchStaff", param);
        }
        #endregion
    }
}
