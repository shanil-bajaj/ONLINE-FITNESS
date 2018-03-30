using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace BusinessAccessLayer
{
    public class BalCharts
    {
        DataConnection objdal = new DataConnection();

        public int Brnach_Id { get; set; }
        public int Company_Id { get; set; }

        public DataTable BindExpenseDayWise()
        {
            //return objdal.BindWoSPData("usp_BindChartExpenseDayWise");
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Branch_ID", Brnach_Id);
            param[1] = new SqlParameter("@Company_Id", Company_Id);
            param[2] = new SqlParameter("@Action","ExpenseDayWise");
            return objdal.DataTable_StoredProcedure_Parameter("SP_Charts", param);
        }
    }
}
