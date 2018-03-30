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
    public class BalSMSTemplate
    {
        #region Public Property

        DataConnection obDataCon = new DataConnection();

        public int SMSTemplate_AutoID { get; set; }
        public string Enddate5 { get; set; }
        public string Enddate4 { get; set; }
        public string Enddate3 { get; set; }
        public string Enddate2 { get; set; }
        public string Enddate1 { get; set; }
        public string Enddate { get; set; }
        public string Enquiry { get; set; }
        public string Renew { get; set; }
        public string Upgrade { get; set; }
        public string Todaybalance { get; set; }
        public string EnquiryFollowup { get; set; }
        public string balancepaid { get; set; }
        public string birthday { get; set; }
        public string birthday_Staff { get; set; }
        public string Aniversary { get; set; }
        public string AbesntMember { get; set; }
        public int Branch_AutoID { get; set; }
        public int Company_AutoID { get; set; }

        public string Action { get; set; }

        #endregion


        public DataTable GetDetails()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[1] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[2] = new SqlParameter("@Action", Action);
            return obDataCon.DataTable_StoredProcedure_Parameter("SP_SMSTemplate", param);
        }

       
        public int Insert_SMSTemplateInformation()
        {
             SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@Enddate5", Enddate5);
            param[1] = new SqlParameter("@Enddate4", Enddate4);
            param[2] = new SqlParameter("@Enddate3", Enddate3);
            param[3] = new SqlParameter("@Enddate2", Enddate2);
            param[4] = new SqlParameter("@Enddate1", Enddate1);
            param[5] = new SqlParameter("@Enddate", Enddate);
            param[6] = new SqlParameter("@Enquiry", Enquiry);
            param[7] = new SqlParameter("@Renew", Renew);
            param[8] = new SqlParameter("@Upgrade", Upgrade);
            param[9] = new SqlParameter("@Todaybalance", Todaybalance);
            param[10] = new SqlParameter("@EnquiryFollowup", EnquiryFollowup);
            param[11] = new SqlParameter("@balancepaid", balancepaid);
            param[12] = new SqlParameter("@birthday", birthday);
            param[13] = new SqlParameter("@birthday_Staff", birthday_Staff);
            param[14] = new SqlParameter("@Aniversary", Aniversary);
            param[15] = new SqlParameter("@AbesntMember", AbesntMember);
            param[16] = new SqlParameter("@Branch_AutoID", Branch_AutoID);
            param[17] = new SqlParameter("@Company_AutoID", Company_AutoID);
            param[18] = new SqlParameter("@SMSTemplate_AutoID", SMSTemplate_AutoID);
            param[19] = new SqlParameter("@Action", Action);
            return obDataCon.Insert_Update_Delete_StoredProcedure_Parameter("SP_SMSTemplate", param);
        }

        
    }
}

