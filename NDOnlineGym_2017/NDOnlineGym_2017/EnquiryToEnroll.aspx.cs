using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessAccessLayer;
using DataAccessLayer;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class EnquiryToEnroll : System.Web.UI.Page
    {
        BalEnquiryToEnroll objEnqToEnroll = new BalEnquiryToEnroll();
        DataTable dt = new DataTable();
        int Flag;
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignTodayDate();
                txtSearchBy.Enabled = false;
                txtFromDate.Focus();
                SerachByJoiningDate();
            }
        }

        public void AssignTodayDate()
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

        public void AssignID()
        {
            objEnqToEnroll.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEnqToEnroll.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        public void SearchByDate()
        {
            //ViewState["ViewActiveDeactive"] = null;
            objEnqToEnroll.FromDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
            objEnqToEnroll.ToDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
            AssignID();

            if (objEnqToEnroll.FromDate > objEnqToEnroll.ToDate)
            {
                objEnqToEnroll.Action = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('From Date Should Not be Greater than To Date..!!!','Error');", true);
                //return;
            }
            else
            {
                objEnqToEnroll.Action = "Select_By_Date";
            }
            dt = objEnqToEnroll.Bind_GV();
           // ViewState["ViewActiveDeactive"] = dt;

            GridViewBind();
            flag = 1;
        }
        
        private void GridViewBind()
        {
            try
            {
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    gvEnquiryToEnroll.DataSource = dt;
                    gvEnquiryToEnroll.DataBind();
                    gvEnquiryToEnroll.Style["width"] = "100%";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
                    gvEnquiryToEnroll.DataSource = dt;
                    gvEnquiryToEnroll.DataBind();
                    gvEnquiryToEnroll.Style["width"] = "100%";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + "Record Not Found" + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchByDate();
        }

        public void Search()
        {
            objEnqToEnroll.Action = "SearchByCategory";
            if (ddlSearchBy.SelectedValue.ToString() == "--Select--")
            {
                objEnqToEnroll.Category = "All";
            }
            else if (ddlSearchBy.SelectedValue.ToString() == "Member Name")
            {
                objEnqToEnroll.Category = "ByName";
                objEnqToEnroll.searchTxt = txtSearchBy.Text;
            }
            else if (ddlSearchBy.SelectedValue.ToString() == "Member Contact")
            {
                objEnqToEnroll.Category = "ByContact";
                objEnqToEnroll.searchTxt = txtSearchBy.Text;
            }
            AssignID(); 
            dt = objEnqToEnroll.GetsearchBy();
      
            //AssignTodayDate();
            GridViewBind();
            flag = 2;
        }

        protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearchBy.SelectedItem.Text == "--Select--")
            {
                txtSearchBy.Enabled = false;
            }
            else
            {
                txtSearchBy.Enabled = true;
                txtSearchBy.Focus();
                txtSearchBy.Text = "";
            }
        }

        protected void txtSearchBy_TextChanged(object sender, EventArgs e)
        {
            Search();
            btnSearchWtCategory.Focus();
        }

        public void SerachByJoiningDate()
        {
            objEnqToEnroll.FromDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
            objEnqToEnroll.ToDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
            AssignID();

            if (objEnqToEnroll.FromDate > objEnqToEnroll.ToDate)
            {
                objEnqToEnroll.Action = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('From Date Should Not be Greater than To Date..!!!','Error');", true);
                //return;
            }
            else
            {
                objEnqToEnroll.Action = "Select_By_JoiningDate";
            }
            dt = objEnqToEnroll.Bind_GVWithJoiningDate();
            // ViewState["ViewActiveDeactive"] = dt;

            GridViewBind();
            flag = 3;
        }

        protected void btnJoiningDate_Click(object sender, EventArgs e)
        {
            SerachByJoiningDate();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            AssignTodayDate();
            ddlSearchBy.Text = "--Select--";
            txtSearchBy.Text = "";
            SerachByJoiningDate();
            txtSearchBy.Enabled = false;
            txtFromDate.Focus();
            gvEnquiryToEnroll.PageIndex = 0;
        }

        public void SearchWithDateWtCategory()
        {
            objEnqToEnroll.FromDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
            objEnqToEnroll.ToDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
            AssignID();

            if (objEnqToEnroll.FromDate > objEnqToEnroll.ToDate)
            {
                objEnqToEnroll.Action = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('From Date Should Not be Greater than To Date..!!!','Error');", true);
                //return;
            }
            else
            {
                Search();
                objEnqToEnroll.Action = "Select_By_DateWtCategory";
               
            }
           
            // ViewState["ViewActiveDeactive"] = dt;
           
            dt = objEnqToEnroll.Bind_GVWtDateAndCategory();
            GridViewBind();
            flag = 4;
        }

        protected void btnSearchWtCategory_Click(object sender, EventArgs e)
        {
            if (ddlSearchBy.SelectedValue == "--Select--" && txtSearchBy.Text != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                ddlSearchBy.Focus();
                return;
            }
            else if (ddlSearchBy.SelectedValue != "--Select--" && txtSearchBy.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                txtSearchBy.Focus();
                return;
            }
            else
            {
                SearchWithDateWtCategory();
            }
        }

        protected void gvEnquiryToEnroll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvEnquiryToEnroll.PageIndex = e.NewPageIndex;
                 SearchByDate();
            }
            else if(flag==2)
            {
                gvEnquiryToEnroll.PageIndex = e.NewPageIndex;
                Search();
            }
            else if (flag == 3)
            {
                gvEnquiryToEnroll.PageIndex = e.NewPageIndex;
                SerachByJoiningDate();
            }
            else if (flag==4)
            {
                gvEnquiryToEnroll.PageIndex = e.NewPageIndex;
                SearchWithDateWtCategory();
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToExcel()
        {
            try
            {
                if (ViewState["DietDetails"] != null)
                {
                    dt = (DataTable)ViewState["DietDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=EnqToEnrollDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            //To Export all pages
                            gvEnquiryToEnroll.AllowPaging = false;
                            gvEnquiryToEnroll.DataSource = dt;
                            gvEnquiryToEnroll.DataBind();
                            gvEnquiryToEnroll.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvEnquiryToEnroll.HeaderRow.Cells)
                            {
                                cell.BackColor = gvEnquiryToEnroll.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvEnquiryToEnroll.Rows)
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
                                }
                            }
                            gvEnquiryToEnroll.GridLines = GridLines.Both;
                            gvEnquiryToEnroll.RenderControl(hw);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Data GridView is Empty,Can Not Export !!!.','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Data GridView is Empty,Can Not Export !!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
    }
}