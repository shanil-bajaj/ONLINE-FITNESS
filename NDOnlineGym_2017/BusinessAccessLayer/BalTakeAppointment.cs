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

    public class BalTakeAppointment
    {
        DataConnection obDataCon = new DataConnection();

        public int TAppointment_AutoID { get; set; }
        public int Member_AutoID { get; set; }
        public int AppoinmentMaster_AutoID { get; set; }
        public int TAppointment_ID { get; set; }
        public int Pression { get; set; }
        public int Staff_AutoID { get; set; }
        public int Appoint_AutoID { get; set; }
        public int Executive_ID { get; set; }
        public int branch_ID { get; set; }
        public int Comp_ID { get; set; }
        public string Action { get; set; }
        public  DateTime Time{ get; set; }
        public int Ammount { get; set; }
        public string Appoint_Type { get; set; }
        public string Contact1 { get; set; }
        public string Programmer_Name { get; set; }
        public int LoginID { get; set; }
        public DateTime Apdate { get; set; }

        public int Insert()
        {
            SqlParameter[] param=new SqlParameter[12];
            param[0] = new SqlParameter("@Member_AutoID", Member_AutoID);
            param[1] = new SqlParameter("@Company_AutoID",branch_ID);
            param[2] = new SqlParameter("@Branch_AutoID", Comp_ID);
            param[3] = new SqlParameter("@ProgrammerName", Programmer_Name);
            param[4] = new SqlParameter("@Login_AutoID", LoginID);
            param[5] = new SqlParameter("@Presession", Pression);
            param[6] = new SqlParameter("@Action", Action);
            param[7] = new SqlParameter("@Ammount", Ammount);
            param[8] = new SqlParameter("@Time", Time);
            param[9] = new SqlParameter("@Appoint_type", Appoint_Type);
            param[10] = new SqlParameter("@ApDate", Apdate);
            param[11] = new SqlParameter("@Executive_ID", Executive_ID);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_TakeAppoint", param);
        }

        public DataTable BindGrid()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Bind");
            param[1] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[2] = new SqlParameter("@Company_AutoID", Comp_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_AppointMaster", param);
        }

        public DataTable BindStaff()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Action", "Select_Staff");
            param[1] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[2] = new SqlParameter("@Company_AutoID", Comp_ID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_TakeAppoint", param);
        }

        public DataTable Select_MemberID()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[2] = new SqlParameter("@Company_AutoID", Comp_ID);
            param[3] = new SqlParameter("@Member_AutoID", Member_AutoID);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_TakeAppoint", param);
        }

        public DataTable Select_MemberContact()
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Action", Action);
            param[1] = new SqlParameter("@Branch_AutoID", branch_ID);
            param[2] = new SqlParameter("@Company_AutoID", Comp_ID);
            param[3] = new SqlParameter("@Contact", Contact1);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_TakeAppoint", param);
        }
    }
}

