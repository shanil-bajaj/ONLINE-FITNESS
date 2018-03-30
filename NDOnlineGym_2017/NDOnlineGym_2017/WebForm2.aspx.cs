using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NDOnlineGym_2017
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.BindDummyRow();
            }
        }

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();
            dummy.Columns.Add("Member_ID1");
            dummy.Columns.Add("FName");
            dummy.Columns.Add("LName");
            dummy.Columns.Add("Contact1");
            dummy.Rows.Add();
            gvCustomers.DataSource = dummy;
            gvCustomers.DataBind();
        }

        [WebMethod]
        public static string GetCustomers()
        {
            string query = "SELECT Member_ID1, FName, LName,Contact1 FROM MemberDetails where Branch_AutoID = 1";
            SqlCommand cmd = new SqlCommand(query);
            return GetData(cmd).GetXml();
        }

        private static DataSet GetData(SqlCommand cmd)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["NDOnlineGym"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        return ds;

                    }
                }
            }
        }
    }
}