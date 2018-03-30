using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Web.Services;

using System.Configuration;
using System.Data.SqlClient;

namespace NDOnlineGym_2017
{
    public partial class MemberDetails : System.Web.UI.Page
    {
        DataTable table = new DataTable();
        BalLoginForm ObjLogin = new BalLoginForm();
        BalAddMember objMemberDetails = new BalAddMember();
        DataTable dt = new DataTable();
        string newfileName = string.Empty;
        string serverfilrpath = string.Empty;
        int Flag, Member_AutoID;
            //static int flagpageload = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlcategory.Focus();
                DateTime TodayDate;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                { 
                }
                ObjLogin.TodayDate = TodayDate;
                ObjLogin.UpdateStatusCourse();
                ObjLogin.UpdateStatusMember();
                objMemberDetails.Action = "SearchByDate";
                GridBind();
                AssignMonthDate();
                if (Request.QueryString["MemID"] != null)
                {

                    objMemberDetails.Member_ID1 = Convert.ToInt32(Request.QueryString["MemID"]);
                    objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objMemberDetails.Category = "Member_ID";
                    table = objMemberDetails.SearchCategory();
                    if (table.Rows.Count > 0)
                    {
                        RepterDetails.DataSource = table;
                        RepterDetails.DataBind();
                    }
                }
                if (Request.QueryString["FNameMemDetails"] != null)
                {
                    objMemberDetails.Action = "SearchByDate";
                    GridBind();
                }
                txtFromDate.Focus();
                ViewState["flagpageload"] = 0;
            }
            if (ViewState["flagpageload"].ToString() == "1")
            {
                objMemberDetails.Action = "SearchByCategory";
                SearchCategory();
            }
            else if (ViewState["flagpageload"].ToString() == "0")
            {
                objMemberDetails.Action = "SearchByDate";
                GridBind();
            }
        }

        [WebMethod]
        public static string GetCustomers(int pageIndex)
        {
            return GetCustomersData(pageIndex).GetXml();
        }

        public static DataSet GetCustomersData(int pageIndex)
        {
            string query = "[GetMembersPageWise]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", 10);
            cmd.Parameters.Add("@PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            return GetData(cmd);
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
                        sda.Fill(ds, "MemberDetails");
                        DataTable dt = new DataTable("PageCount");
                        dt.Columns.Add("PageCount");
                        dt.Rows.Add();
                        dt.Rows[0][0] = cmd.Parameters["@PageCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }
                }
            }
        }

        public void searchMember()
        {
            objMemberDetails.Action = "SearchByCategory";
            objMemberDetails.searchTxt = Request.QueryString["Member_AutoID"];
            objMemberDetails.Category = "MemberAutoID";
            GridBind();
        }

        #region ------------ Assign All Date ------------------
        protected void AssignMonthDate()
        {
            Label1.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Label1.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);
            txtToDate.Text = dtLast.ToString("dd-MM-yyyy");

            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            objMemberDetails.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            objMemberDetails.ToDate = Todate;
        }
        #endregion

        #region BIND GRID
        public void GridBind()
        {
            try
            {
                DateTime Fromdate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                { }
                objMemberDetails.FromDate = Fromdate;
                DateTime Todate;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                { }
                objMemberDetails.ToDate = Todate;
                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                table = objMemberDetails.Get_Search();

                if (table.Rows.Count > 0)
                {
                    ViewState["MemberDetails"] = table;
                    RepterDetails.DataSource = table;
                    RepterDetails.DataBind();
                }
                else
                {
                    RepterDetails.DataSource = null;
                    RepterDetails.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available !!!.','Information');", true);
                    return;
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        int i = 0;
        protected void RepterDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string status = "";
            HiddenField hf = e.Item.FindControl("HiddenField1") as HiddenField;
            if (hf != null)
            {
                string val = hf.Value;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("imgMember") as System.Web.UI.WebControls.Image;
                img.ImageUrl = val;
            }

            //HiddenField hf2 = e.Item.FindControl("HiddenField2") as HiddenField;
            //if (hf != null)
            //{
            //    string st = hf2.Value;
            //}
            Control control;
            control = e.Item.FindControl("lblStatus");

            Label lblStatus = e.Item.FindControl("lblStatus") as Label;
            if (lblStatus != null)
            {
                i = Convert.ToInt32(e.Item.ItemIndex + "");
                status = table.Rows[i]["Status"].ToString();
            }
            if (status.ToString() == "Active")
            {
                lblStatus.CssClass = "ActiveStatus";
            }
            else if(status.ToString() == "Deactive")
            {
                lblStatus.CssClass = "DeactiveStatus";
            }

            HiddenField hf3 = e.Item.FindControl("HiddenField3") as HiddenField;
            string MembershipStatus = "";
            Control control1;
            control1 = e.Item.FindControl("lblMembershipStatus");

            Label lblMembershipStatus = e.Item.FindControl("lblMembershipStatus") as Label;
            if (lblMembershipStatus != null)
            {
                i = Convert.ToInt32(e.Item.ItemIndex + "");
                MembershipStatus = table.Rows[i]["MembershipStatus"].ToString();
            }
            if (MembershipStatus.ToString() == "Group_Owner")
            {
                    control = e.Item.FindControl("lnkBtnEdit");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkbtnDelete");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnRenew");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnBalance");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnUpgrade");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnTransfer");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnFreezing");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAppointment");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAttendance");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnPT");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnPOS");
                if (control != null)
                    control.Visible = true;

            }
            else if (MembershipStatus.ToString() == "Member")
            {
                control = e.Item.FindControl("lnkBtnEdit");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkbtnDelete");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnRenew");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnBalance");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnUpgrade");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnTransfer");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnFreezing");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnAppointment");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAttendance");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnPT");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnPOS");
                if (control != null)
                    control.Visible = false;
            }

        }

        //#region Search On Text
        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //        objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //        objMemberDetails.Action = "SearchBYCategory";
        //        if (ddlcategory.Text != "--Select--" && ddlcategory.Text != "")
        //        {
        //            if (ddlcategory.Text == "FirstName")
        //            {
        //                objMemberDetails.FName = txtSearch.Text;
        //                objMemberDetails.Category = "First Name";
        //            }
        //            if (ddlcategory.Text == "LastName")
        //            {
        //                objMemberDetails.LName = txtSearch.Text;
        //                objMemberDetails.Category = "Last Name";
        //            }
        //            if (ddlcategory.Text == "Contact")
        //            {
        //                objMemberDetails.con = txtSearch.Text;
        //                objMemberDetails.Category = ddlcategory.Text;
        //            }
        //            if (ddlcategory.Text == "Status")
        //            {
        //                objMemberDetails.Status = txtSearch.Text;
        //                objMemberDetails.Category = ddlcategory.Text;
        //            }
        //            if (ddlcategory.Text == "Member_ID")
        //            {
        //                objMemberDetails.Member_ID1 = Convert.ToInt32(txtSearch.Text);
        //                objMemberDetails.Category = ddlcategory.Text;
        //            }
        //            if (ddlcategory.Text == "Gender")
        //            {
        //                objMemberDetails.Gender = txtSearch.Text;
        //                objMemberDetails.Category = ddlcategory.Text;
        //            }
        //            table = objMemberDetails.SearchCategory();
        //            if (table.Rows.Count > 0)
        //            {
        //                RepterDetails.DataSource = table;
        //                RepterDetails.DataBind();
        //            }

        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //# endregion

        #region Search_Record
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                objMemberDetails.Action = "SearchByDate";
                GridBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                return;
            }

        }
        #endregion

        string FNameMemDetails = "MemberDetails";
        protected void lbtnMemberProfile_Command(object sender, CommandEventArgs e)
        {
            int MemberId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MemberProfile.aspx?MemberId=" + MemberId + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnEdit_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/AddMember.aspx?MemberID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnAssignPackage_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/demoCourse.aspx?Member_ID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnMemberFollowup_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/Followup.aspx?Member_ID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnBalance_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/BalancePayment.aspx?Member_ID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnUpgrade_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/Upgrade.aspx?MemberID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnTransfer_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MembershipTransfer.aspx?MemberID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnFreezing_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MembershipFreezing.aspx?Member_ID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnAppointment_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/TakeAppointment.aspx?Member_ID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnAttendance_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MemberNumericalAttendance.aspx?Member_ID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode(FNameMemDetails.ToString()));
        }

        protected void lnkBtnPT_Command(object sender, CommandEventArgs e)
        {

        }

        protected void lnkBtnPOS_Command(object sender, CommandEventArgs e)
        {

        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            objMemberDetails.Action = "SearchByCategory";
            SearchCategory();
            btnDateWithCategory.Focus();
            ViewState["flagpageload"] = 1;
        }

        protected void btnDateWithCategory_Click(object sender, EventArgs e)
        {
            if (ddlcategory.SelectedValue == "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                return;
            }
            else if (ddlcategory.SelectedValue != "--Select--" && txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                return;
            }
            else
            {
                objMemberDetails.Action = "SearchByDateWithCategory";
                SearchCategory();
            }

        }

        #region ------------ Assign Action On Search
        private void SearchCategory()
        {
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (ddlcategory.Text != "--Select--" && ddlcategory.Text != "")
            {
                if (ddlcategory.Text == "Name")
                {
                    objMemberDetails.searchTxt = txtSearch.Text;
                    objMemberDetails.Category = "Name";
                }
                else if (ddlcategory.Text == "Contact")
                {
                    objMemberDetails.searchTxt = txtSearch.Text;
                    objMemberDetails.Category = ddlcategory.Text;
                }
                else if (ddlcategory.Text == "Status")
                {
                    objMemberDetails.searchTxt = txtSearch.Text;
                    objMemberDetails.Category = ddlcategory.Text;
                }
                else if (ddlcategory.Text == "Member ID")
                {
                    objMemberDetails.searchTxt = txtSearch.Text;
                    objMemberDetails.Category = ddlcategory.Text;
                }
                else if (ddlcategory.Text == "Gender")
                {
                    objMemberDetails.searchTxt = txtSearch.Text;
                    objMemberDetails.Category = ddlcategory.Text;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                ddlcategory.Focus();
                return;
            }
           GridBind();
        }
        #endregion

        protected void lnkbtnDelete_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Action = "Delete";
            objMemberDetails.Member_AutoID = memberid;
            int res = objMemberDetails.Delete_Staff();
            if (res > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully!!!','Success');", true);
                objMemberDetails.Action = "SearchByDate";
                GridBind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            objMemberDetails.Action = "SearchByDate";
            GridBind();
            AssignMonthDate();
            txtSearch.Text = "";
            ddlcategory.SelectedValue = "--Select--";
        }

        #region ----------------- Export To Excle Record ----------------
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        protected void ExportToExcel()
        {
            try
            {

                if (ViewState["MemberDetails"] != null)
                {
                    dt = (DataTable)ViewState["MemberDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MemberDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pagesgv

                            gvMemberDetails.AllowPaging = false;

                            gvMemberDetails.DataSource = dt;
                            gvMemberDetails.DataBind();
                            gvMemberDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMemberDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMemberDetails.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvMemberDetails.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                    List<Control> controls = new List<Control>();
                                    //Add controls to be removed to Generic List
                                    foreach (Control control in cell.Controls)
                                    {
                                        controls.Add(control);
                                    }

                                    foreach (Control control in controls)
                                    {
                                        switch (control.GetType().Name)
                                        {
                                            case "HyperLink":
                                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                                break;

                                            case "LinkButton":
                                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                                break;

                                        }
                                        cell.Controls.Remove(control);

                                    }
                                }
                            }


                            gvMemberDetails.GridLines = GridLines.Both;
                            gvMemberDetails.RenderControl(hw);

                            //style to format numbers to string

                            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            //Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Can Not Export !!!.','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Can Not Export !!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        #endregion
    }
}