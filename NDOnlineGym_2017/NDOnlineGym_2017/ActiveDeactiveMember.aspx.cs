using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using BusinessAccessLayer;
using DataAccessLayer;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class ActiveDeactiveMember : System.Web.UI.Page
    {
        BalActiveDeactive objActDct = new BalActiveDeactive();
        BalLoginForm ObjLogin = new BalLoginForm();
        DataTable dt1 = new DataTable();
        DataSet ds = new DataSet();
        //int Flag;
        static int flag;
        int Total;
        int Active;

        protected void Page_Load(object sender, EventArgs e)
        {
          if (!IsPostBack)
            {
                DateTime TodayDate;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out TodayDate))
                { }
                ObjLogin.TodayDate = TodayDate;
                ObjLogin.UpdateStatusCourse();
                ObjLogin.UpdateStatusMember();
                AssignMonthDate();
                GetActiveMember();
                txtFromDate.Focus();
            }
        }

        #region ---------------- Assign Company and Branch ID ---------------
        public void AssignID()
        {
            objActDct.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objActDct.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region ----------------- Assign Month Date ------------------
        protected void AssignMonthDate()
        {
            try
            {
                DateTime date;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                { }

                DateTime dtFirst = Convert.ToDateTime(date);
                DateTime dtLast;

                //Setting Start Date Month
                dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
                txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

                //Setting Last Date of Month
                dtLast = dtFirst.AddMonths(1).AddDays(-1);
                txtToDate.Text = dtLast.ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------------- Get Active Deactive Member ------------------
        protected void GetActiveMember()
        {
            objActDct.Status = "Active";
            AssignID();
            objActDct.Action = "Select_Member_By_Status";
            dt1 = objActDct.Bind_gvActiveDeactive();            
            GridViewBind(dt1);
            lblTotal.Text = dt1.Rows.Count.ToString();
            lblActive.Text = dt1.Rows.Count.ToString();
            lblDeactive.Text = (Convert.ToInt32(lblTotal.Text) - Convert.ToInt32(lblActive.Text)).ToString();
            flag = 1;          
        }
        #endregion

        #region ----------------- Bind GridView --------------
        protected void GridViewBind(DataTable dt1)
        {
            try
            {
                ViewState["ViewActiveDeactive"] = dt1;
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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        protected void ClearRecord()
        {
            AssignMonthDate();
            txtMemberID.Text = "";
            txtMemberName.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }

        protected void btnName_Command(object sender, CommandEventArgs e)
        {
            int MemberId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MemberProfile.aspx?MemberId=" + MemberId);
        }

        protected void GVActiveDeactive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {           
            if (flag == 1)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                GetActiveMember();
            }
            else if (flag == 2)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                MemberId_txtchanded();
            }
            else if (flag == 3)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                Membername_TxtChanged();
            }
            else if (flag == 4)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                status_txtchanged();
            }
            else if (flag == 5)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                status_txtchanged();
            }
            else if (flag == 6)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                Gender_Txtchanged();
            }
            else if (flag == 7)
            {
                GVActiveDeactive.PageIndex = e.NewPageIndex;
                SearchByDate();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           // AssignTodaysDate();
            ClearRecord();
            GetActiveMember();
            GVActiveDeactive.PageIndex = 1;
        }

        #region ------------------ Member ID Text Change --------------
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            MemberId_txtchanded();
            txtMemberID.Focus();
            GVActiveDeactive.PageIndex = 0;
        }

        public void MemberId_txtchanded()
        {
            try
            {
                ddlGender.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;

                objActDct.Member_ID = Convert.ToInt32(txtMemberID.Text);
                AssignID();
                objActDct.Action = "SELECT_ByMemberID";
                dt1 = objActDct.Bind_gvActiveDeactive();

                GridViewBind(dt1);
                Total = dt1.Rows.Count;
                Active = CountActive(dt1);

                lblTotal.Text = Total.ToString(); // Total
                lblActive.Text = Active.ToString(); // Active
                lblDeactive.Text = (Total - Active).ToString(); // Deactive

                if (Total > 0)
                {
                    txtMemberName.Focus();
                }
                else
                {
                    txtMemberID.Focus();
                    txtMemberID.Text = "";
                }
                flag = 2;
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion


        private int CountActive(DataTable dt1)
        {
            var count = dt1.Rows.Count;
            int Active = 0;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string status = dt1.Rows[i]["Status"].ToString();
                    if (status == "Active")
                    {
                        Active = Active + 1;
                    }
                }
            }
            return Active;
        }

        #region -------------- Member Name Text Change -------------
        protected void txtMemberName_TextChanged(object sender, EventArgs e)
        {
            GVActiveDeactive.PageIndex = 0;
            Membername_TxtChanged();
            txtMemberName.Focus();

        }

        public void Membername_TxtChanged()
        {
            try
            {
                ddlGender.SelectedIndex = 0;
                ddlStatus.SelectedIndex = 0;
             
                objActDct.MemberName = txtMemberName.Text;
                AssignID();
                objActDct.Action = "SELECT_ByMemberName";
                dt1 = objActDct.Bind_gvActiveDeactive();

                GridViewBind(dt1);

                Total = dt1.Rows.Count;
                Active = CountActive(dt1);

                lblTotal.Text = Total.ToString(); // Total
                lblActive.Text = Active.ToString(); // Active
                lblDeactive.Text = (Total - Active).ToString(); // Deactive

                if (Total > 0)
                {
                    txtMemberName.Focus();
                }
                else
                {
                    txtMemberID.Focus();
                }
                flag = 3;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Status Change DDL ------------------
        protected void ddlStatus_TextChanged(object sender, EventArgs e)
        {
            GVActiveDeactive.PageIndex = 0;
            status_txtchanged();
            ddlStatus.Focus();
           
        }

        public void status_txtchanged()
        {
            try
            {
                //ViewState["ViewActiveDeactive"] = null;
                objActDct.Status = ddlStatus.SelectedItem.Text;
                AssignID();
                if (ddlGender.SelectedItem.Value != "0")
                {
                    objActDct.Gender = ddlGender.SelectedItem.Text;     //.SelectedValue;
                    objActDct.Action = "SearchByGengerAndStatus";
                    dt1 = objActDct.Bind_gvActiveDeactive();
                }
                else
                {
                    objActDct.Action = "Select_Member_By_Status";
                    dt1 = objActDct.Bind_gvActiveDeactive();
                }

                //ViewState["ViewActiveDeactive"] = dt1;
                //dt = dataTable;
                GridViewBind(dt1);

                int Total = dt1.Rows.Count;
                int Active = CountActive(dt1);

                lblTotal.Text = Total.ToString(); // Total
                lblActive.Text = Active.ToString(); // Active
                lblDeactive.Text = (Total - Active).ToString(); // Deactive
                flag = 4;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ----------------- Gender DDL Change ---------------
        protected void ddlGender_TextChanged(object sender, EventArgs e)
        {
            GVActiveDeactive.PageIndex = 0;
            Gender_Txtchanged();
            ddlGender.Focus();
           
        }

        public void Gender_Txtchanged()
        {
            try
            {
                objActDct.Gender = ddlGender.SelectedItem.Text;
                AssignID();
                if (ddlStatus.SelectedItem.Value != "0")
                {                   
                    objActDct.Status = ddlStatus.SelectedItem.Text;
                    objActDct.Action = "SearchByGengerAndStatus";
                    dt1= objActDct.Bind_gvActiveDeactive();
                }
                else
                {
                    objActDct.Action = "Select_Member_By_Gender";
                    dt1 = objActDct.Bind_gvActiveDeactive();
                }

                GridViewBind(dt1);

                int Total = dt1.Rows.Count;
                int Active = CountActive(dt1);

                lblTotal.Text = Total.ToString(); // Total
                lblActive.Text = Active.ToString(); // Active
                lblDeactive.Text = (Total - Active).ToString(); // Deactive
                flag = 6;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------------- Search by DAte Button
        protected void btnSerachByDate_Click(object sender, EventArgs e)
        {            
            GVActiveDeactive.PageIndex = 0;
            SearchByDate();              
        }

        public void SearchByDate()
        {
             flag1 = chkFromDateNotLessToDate();
             if (flag1 == 0)
             {
                 AssignID();
                 objActDct.Action = "Select_By_Date";

                 dt1 = objActDct.Bind_gvActiveDeactiveByDate();                 
                 GridViewBind(dt1);

                 int Total = dt1.Rows.Count;
                 int Active = CountActive(dt1);

                 lblTotal.Text = Total.ToString(); // Total
                 lblActive.Text = Active.ToString(); // Active
                 lblDeactive.Text = (Total - Active).ToString(); // Deactive

                 txtMemberID.Text = "";
                 txtMemberName.Text = "";
                 ddlGender.SelectedIndex = 0;
                 ddlStatus.SelectedIndex = 0;
                 flag = 7;

             }           
        }
        #endregion

        #region ------------- Check From Date And To Date Validation
        int flag1 = 0;
        protected int chkFromDateNotLessToDate()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                flag1 = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag1 = 1;
                txtToDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter To Date !!!','Information');", true);
            }
            else
            {

                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FromDate))
                {
                }

                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ToDate))
                {
                }

                if (FromDate.Date <= ToDate.Date)
                {
                    flag1 = 0;
                    objActDct.FromDate = FromDate;
                    objActDct.ToDate = ToDate;
                }
                else
                {
                    flag1 = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                }
            }

            return flag1;
        }
        #endregion

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

        #region ----------------- Export to Excle -------------
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToExcel()
        {
            try
            {
                if (flag == 1)
                {
                    GetActiveMember();
                }

                dt1 = (DataTable)ViewState["ViewActiveDeactive"];
                if (dt1.Rows.Count > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ActiveDeactiveMember_GridDataexport.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);

                        //To Export all pages
                        GVActiveDeactive.AllowPaging = false;
                        GVActiveDeactive.DataSource = dt1;//(DataTable)ViewState["ViewActiveDeactive"];
                        GVActiveDeactive.DataBind();

                        GVActiveDeactive.HeaderRow.BackColor = Color.White;
                        foreach (TableCell cell in GVActiveDeactive.HeaderRow.Cells)
                        {
                            cell.BackColor = GVActiveDeactive.HeaderStyle.BackColor;
                        }
                        foreach (GridViewRow row in GVActiveDeactive.Rows)
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
                        GVActiveDeactive.GridLines = GridLines.Both;
                        GVActiveDeactive.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('Data Gridview Is Empty,Cannot Export!!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
        #endregion

    }
}