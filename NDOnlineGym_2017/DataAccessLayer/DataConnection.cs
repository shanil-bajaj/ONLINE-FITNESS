using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataConnection
    {
        static DataSet ds = new DataSet();
        static DataTable dt = new DataTable();
        //static SqlDataReader sdr;
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NDOnlineGym"].ToString());

        // Open Connection
        public static void OpenConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
        }
        // Close Connection
        public static void CloseConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
        }
        // Count Record Query using SqlDataAdapter In DataTable No Parameter
        public int Int_Query_NoParameter(string strQuery)
        {
            int Result;
            try
            {
                dt.Clear();
                SqlDataAdapter sda = new SqlDataAdapter(strQuery, con);
                sda.Fill(dt);
                Result = Convert.ToInt32(dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        // Count Record Query using SqlDataAdapter In DataTable with Parameter
        public int Int_Query_Parameter(string strQuery, SqlParameter[] paramList)
        {
            int Result;
            try
            {
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.Clear();
                if (null != paramList)
                {
                    foreach (SqlParameter objParameter in paramList)
                    {
                        cmd.Parameters.Add(objParameter);
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                dt.Columns.Clear();
                sda.Fill(dt);
                Result = Convert.ToInt32(dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        // Insert Update Delete Record Using Query ExecuteNonQuery with Parameter
        public int Insert_Update_Delete_Query_Parameter(string strQuery, SqlParameter[] paramList)
        {
            int Result;
            try
            {
                dt.Clear();
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.Clear();
                if (null != paramList)
                {
                    foreach (SqlParameter objParameter in paramList)
                    {
                        cmd.Parameters.Add(objParameter);
                    }
                }
                CloseConnection();
                OpenConnection();
                Result = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        // Select Record Query using SqlDataAdapter In DataTable No Parameter 
        public DataTable DataTable_Query_NoParameter(string Query)
        {
            try
            {
                dt.Rows.Clear(); dt.Columns.Clear();
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        // Select Record Query using SqlDataAdapter In DataTable with Parameter
        public DataTable DataTable_Query_Parameter(string strQuery, SqlParameter[] paramList)
        {
            try
            {
                dt.Rows.Clear(); dt.Columns.Clear();
                SqlCommand cmd = new SqlCommand(strQuery, con);
                cmd.Parameters.Clear();
                if (null != paramList)
                {
                    foreach (SqlParameter objParameter in paramList)
                    {
                        cmd.Parameters.Add(objParameter);
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        // Count Record StoredProcedure using SqlDataAdapter In DataTable with Parameter 
        public int Int_StoredProcedure_Parameter(string StrProcName, SqlParameter[] paramList)
        {
            int retVal = 0;
            try
            {
                dt.Rows.Clear(); dt.Columns.Clear();
                SqlCommand cmd = new SqlCommand(StrProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                if (null != paramList)
                {
                    foreach (SqlParameter objParameter in paramList)
                    {
                        cmd.Parameters.Add(objParameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    retVal = Convert.ToInt32(dt.Rows[0][0]);
                else
                    retVal = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        // Select Record using StoredProcedure SqlDataAdapter In DataTable with Parameter
        public DataTable DataTable_StoredProcedure_Parameter(string StrProcName, SqlParameter[] paramList)
        {
            try
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                SqlCommand cmd = new SqlCommand(StrProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                if (null != paramList)
                {
                    foreach (SqlParameter objParameter in paramList)
                    {
                        cmd.Parameters.Add(objParameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        // Insert, Update, Delete Record using StoredProcedure ExecuteNonQuery with Parameter
        public int Insert_Update_Delete_StoredProcedure_Parameter(string StrProcName, SqlParameter[] paramList)
        {
            int retval = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(StrProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                if (null != paramList)
                {
                    foreach (SqlParameter objParameter in paramList)
                    {
                        cmd.Parameters.Add(objParameter);
                    }
                }
                CloseConnection();
                OpenConnection();
                retval = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retval;
        }

        public DataSet DataSet_StoredProcedure_Parameter(string StrProcName, SqlParameter[] paramList)
        {
            try
            {
                ds.Tables.Clear();
                //dt.Columns.Clear();
                SqlCommand cmd = new SqlCommand(StrProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                if (null != paramList)
                {
                    foreach (SqlParameter objParameter in paramList)
                    {
                        cmd.Parameters.Add(objParameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
