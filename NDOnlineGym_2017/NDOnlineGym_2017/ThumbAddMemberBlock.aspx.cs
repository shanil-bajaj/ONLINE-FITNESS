using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using NDOnlineGym_2017.ServiceReference1;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Configuration;
using DataAccessLayer;
using BusinessAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class ThumbAddMemberBlock : System.Web.UI.Page
    {
        BalActiveDeactive objActDct = new BalActiveDeactive();
        ServiceReference1.WebServiceSoapClient k = new ServiceReference1.WebServiceSoapClient();
        //SqlConnection con = new SqlConnection("Data Source=192.168.2.5; Initial Catalog=SmartOffice; User ID=admin ; Password=admin;");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ThumbGym"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    GetApiKey();
                    GetSerialKey();
                    GetActiveMember();
                    // string str = k.AddEmployee(ViewState["Apikey"].ToString(), txtMemberID_AddMember.Text, txtMemberName_AddMember.Text, txtCardNumber_AddMember.Text, txtSerialno_AddMember.Text);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                }

            }
        }
        DataTable dt1 = new DataTable();
        public void AssignID()
        {
            objActDct.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objActDct.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        protected void btnName_Command(object sender, CommandEventArgs e)
        {
            int MemberId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MemberProfile.aspx?MemberId=" + MemberId);
        }
        int Flag;
        static int flag;
        protected void GridViewBind(DataTable dt1)
        {
            try
            {

                if (dt1.Rows.Count > 0)
                {
                    GVActiveDeactive.DataSource = dt1;
                    GVActiveDeactive.DataBind();
                    GVActiveDeactive.Style["width"] = "100%";
                }
                else
                {
                    GVActiveDeactive.DataSource = dt1;
                    GVActiveDeactive.DataBind();
                    GVActiveDeactive.Style["width"] = "100%";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        protected void GetActiveMember()
        {
            //ViewState["ViewActiveDeactive"] = null;
            objActDct.Status = "All";
            AssignID();
            objActDct.Action = "Select_Member_By_Status";
            dt1 = objActDct.Bind_gvActiveDeactive();   //Bind_gvActiveByDate();

            GridViewBind(dt1);
            flag = 1;
        }
        public void GetSerialKey()
        {

            SqlDataAdapter da = new SqlDataAdapter("select SerialNumber from Devices where DeviceFName='essl'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ViewState["SerialNumber"] = dt.Rows[0][0].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('please Generate SerialNumber key from Attendance software !!!.','Information');", true);
            }

        }
        public void GetApiKey()
        {

            SqlDataAdapter da = new SqlDataAdapter("select APIKey from MasterSettings", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ViewState["Apikey"] = dt.Rows[0][0].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('please Generate Api key from Attendance software !!!.','Information');", true);
            }

        }
        protected void btnblock_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != "" && txtSerialNumber_BlockMember.Text != "" && txtIsBlock.Text != "")
                {
                    string str = k.BlockUnblockUser(ViewState["Apikey"].ToString(), txtMemberID.Text, txtSerialNumber_BlockMember.Text, Convert.ToBoolean(txtIsBlock.Text));
                    if (str == "Success")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Member Block  Successfully !!!','Success');", true);
                        txtMemberID.Text = "";
                        txtIsBlock.Text = "";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Error !!!','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('please Enter Information!!!.','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        int GetColumnIndexByName(GridViewRow row, string SearchColumnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(SearchColumnName))
                    {
                        break;
                    }
                }
                columnIndex++;
            }
            return columnIndex;
        }
        protected void GVActiveDeactive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = GetColumnIndexByName(e.Row, "status");
                if (e.Row.Cells[index].Text == "Active")
                {
                    e.Row.Cells[index].BackColor = System.Drawing.Color.LightGreen;
                }
                else if (e.Row.Cells[index].Text == "Deactive")
                {
                    e.Row.Cells[index].BackColor = System.Drawing.Color.LightCoral;    //FromName("#ff8080"); ;
                }
                else
                {
                    e.Row.Cells[index].BackColor = System.Drawing.Color.White;
                }
            }

        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
            {
                try
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        //string card = "";
                        //string str = k.AddEmployee(ViewState["Apikey"].ToString(),
                        //row.Cells[1].Text,
                        //row.Cells[2].Text,
                        //card,
                        //ViewState["SerialNumber"].ToString());
                        //if (str == "Success")
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Member Add Successfully !!!','Success');", true);
                        //    GridView1.DataSource = null;
                        //    GridView1.DataBind();

                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Error !!!','Error');", true);
                        //}
                        bool staus ;
                        if (row.Cells[7].Text == "Active")
                        {
                            staus = true;
                        }
                        else
                        {
                            staus = true;
                        }
                        string str = k.BlockUnblockUser(ViewState["Apikey"].ToString(), row.Cells[1].Text, ViewState["SerialNumber"].ToString(), staus);
                        if (str == "Success")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Member Block  Successfully !!!','Success');", true);
                            txtMemberID.Text = "";
                            txtIsBlock.Text = "";
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Error !!!','Error');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Add Member !!!','Error');", true);
            }

        }
        protected void GVActiveDeactive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            if (flag == 1)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                GetActiveMember();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                dt1 = (DataTable)ViewState["DT"];

                dt1.Rows[index].Delete();
                ViewState["DT"] = dt1;
                GridView1.DataSource = dt1;
                GridView1.DataBind();

                if (dt1.Rows.Count == 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        int k1;
        //DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        // public int k = 0;
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                var btnPre = (Control)sender;
                GridViewRow row1 = (GridViewRow)btnPre.NamingContainer;

                DataRow dr = null;
                //dt.Clear();
                dt1.Columns.Add(new DataColumn("Member_ID1"));
                dt1.Columns.Add(new DataColumn("Name"));
                dt1.Columns.Add(new DataColumn("Gender"));
                dt1.Columns.Add(new DataColumn("Contact1"));
                dt1.Columns.Add(new DataColumn("EndDate"));
                dt1.Columns.Add(new DataColumn("Balance"));
                dt1.Columns.Add(new DataColumn("status"));

                if (ViewState["DT"] != null)
                {
                    dt1.Clear();
                    dt1 = (DataTable)ViewState["DT"];
                }

                bool exists = dt1.Select().ToList().Exists(row => row["Member_ID1"].ToString().ToUpper() == row1.Cells[1].Text);

                if (exists == false)
                {
                    k1 = dt1.Rows.Count;
                    dr = dt1.NewRow();
                    dr["Member_ID1"] = row1.Cells[1].Text; ;
                    //dr["Name"] = row1.Cells[2].Text;
                    LinkButton lnkbtn = (LinkButton)row1.FindControl("btnName");
                    dr["Name"] = lnkbtn.Text;
                    dr["Gender"] = row1.Cells[3].Text;
                    dr["Contact1"] = row1.Cells[4].Text;
                    dr["EndDate"] = row1.Cells[5].Text;
                    dr["Balance"] = row1.Cells[6].Text;
                    dr["status"] = row1.Cells[7].Text;


                    dt1.Rows.InsertAt(dr, k1);
                    k1++;
                }
                ViewState["DT"] = dt1;
                GridView1.DataSource = dt1;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
    }
}