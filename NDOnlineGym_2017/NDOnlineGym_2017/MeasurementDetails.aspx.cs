using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class MeasurementDetails : System.Web.UI.Page
    {
        BalMeasurement objMsr = new BalMeasurement();
        DataTable dt = new DataTable();
        int flag = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignDate();
                txtFromDate.Focus();
                //BindGridView();
                SearchByDateFunction();
            }

        }

        #region -------------- Assign Id ------------------
        private void AssignID()
        {
            objMsr.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objMsr.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region --------------- Assign Month Start And End Date -----------------
        public void AssignDate()
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

        #region ------------- Check From Date And To Date Validation ---------------
        protected int chkFromDateNotLessToDate()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag = 1;
                txtToDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Enter To Date !!!','Information');", true);
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
                    flag = 0;
                    objMsr.StartDate = FromDate;
                    objMsr.EndDate = ToDate;
                }
                else
                {
                    flag = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                }
            }

            return flag;

        }
        #endregion
       
        /*
        #region ----------- Bind Grid View ------------
        public void BindGridView()
        {
            try
            {
                flag = chkFromDateNotLessToDate();

                if (flag == 0)
                {
                    AssignID();
                    objMsr.Action = "SelectGridViewData";
                    dt.Clear();

                    dt = objMsr.SelectGridData();
                    ViewState["Data"] = dt;
                    lblCount.Text = dt.Rows.Count.ToString();
                    if (dt.Rows.Count > 0)
                    {                        
                        gvMeasurement.DataSource = dt;
                        gvMeasurement.DataBind();
                    }
                    else
                    {
                        gvMeasurement.DataSource = dt;
                        gvMeasurement.DataBind();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                    }

                    if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                    {
                        gvMeasurement.Columns[0].Visible = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                    {
                        gvMeasurement.Columns[0].Visible = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                    {
                        gvMeasurement.Columns[0].Visible = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                    {
                        gvMeasurement.Columns[0].Visible = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                    {
                        gvMeasurement.Columns[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion
        */
       

        #region ------------ Edit Measurement Record --------------
        public int MsrmntID;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {            
            MsrmntID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("Measurement.aspx?MsrmntID=" + MsrmntID, false);

        }
        #endregion

        #region ------------ Delete Event ----------------
        protected void btnDelete_Command1(object sender, CommandEventArgs e)
        {
            try
            {
                objMsr.Measurement_AutoId = Convert.ToInt32(e.CommandArgument);
                AssignID();
                objMsr.Action = "Delete";
                int i = objMsr.Delete();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    // BindBranchInfoGrid();
                    //ddlCompany.Focus();
                    //BindGridView();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion 

        #region ------------ Search by Date -------------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //BindGridView();
            SearchByDateFunction(); 
        }

        protected void SearchByDateFunction()
        {
            try
            {
                flag = chkFromDateNotLessToDate();

                if (flag == 0)
                {
                    AssignID();
                    objMsr.Action = "SelectGridViewData"; 
                    BindGridViewDetails();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        #endregion

        #region ------------ Search By Date And Category----------- 
        protected void BtnSearchwtCategory_Click(object sender, EventArgs e)
        {
            flag = chkFromDateNotLessToDate();
            if (flag == 0)
            {                
                if (DDlSearch.SelectedValue.ToString() != "--Select--")
                {
                    if (txtSearch.Text != string.Empty)
                    {
                        AssignID();
                        SeacrhAction();
                        //Search();
                        BindGridViewDetails();

                    }
                    else
                    {
                        txtSearch.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Enter Data On Search Field !!!','Information');", true);
                    }

                }
                else
                {
                    DDlSearch.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Please Select Categry !!!','Information');", true);
                }

            }
           
        }       

        #endregion

        /*
        public void Search()
        {
            try
            {
                dt = objMsr.Get_Search();
                ViewState["Data"] = dt;
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    gvMeasurement.DataSource = dt;
                    gvMeasurement.DataBind();
                }
                else
                {
                    gvMeasurement.DataSource = dt;
                    gvMeasurement.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvMeasurement.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvMeasurement.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvMeasurement.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvMeasurement.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvMeasurement.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }       
        */

        #region ------------- Search Action For BindGridView -----------------
        private void SeacrhAction()
        {
            try
            {
                objMsr.Action = "SerachByCategory";

                if (DDlSearch.SelectedValue.ToString() == "Member Id")
                {
                    objMsr.Category = "ById";
                    objMsr.searchTxt = txtSearch.Text;
                }
                else if (DDlSearch.SelectedValue.ToString() == "Name")
                {
                    objMsr.Category = "ByName";
                    objMsr.searchTxt = txtSearch.Text;
                }
                else if (DDlSearch.SelectedValue.ToString() == "Contact")
                {
                    objMsr.Category = "ByContact";
                    objMsr.searchTxt = txtSearch.Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                    DDlSearch.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Bind Extension Details Gridview --------------
        private void BindGridViewDetails()
        {
            ViewState["Data"]  = objMsr.SelectGridData();
            dt = (DataTable)ViewState["Data"];

            lblCount.Text = Convert.ToString(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                gvMeasurement.DataSource = dt;
                gvMeasurement.DataBind();
            }
            else
            {
                gvMeasurement.DataSource = dt;
                gvMeasurement.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
            }

            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvMeasurement.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvMeasurement.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvMeasurement.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvMeasurement.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvMeasurement.Columns[0].Visible = false;
            }

        }
        #endregion


        protected void DDlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDlSearch.SelectedItem.Text == "--Select--")
            {
                txtSearch.Enabled = false;
                txtSearch.Text = "";
            }
            else
            {
                txtSearch.Enabled = true;
                txtSearch.Focus();
                txtSearch.Text = "";
            }
        }

        public void ExportToExcel()
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    dt = (DataTable)ViewState["Data"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MeasurementDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";

                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvMeasurement.Columns[0].Visible = false;
                            gvMeasurement.Columns[1].Visible = false;
                            gvMeasurement.AllowPaging = false;

                            gvMeasurement.DataSource = dt;
                            gvMeasurement.DataBind();
                            gvMeasurement.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMeasurement.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMeasurement.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvMeasurement.Rows)
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
                            gvMeasurement.GridLines = GridLines.Both;
                            gvMeasurement.RenderControl(hw);

                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('DataGridView Is Empty, Can Not Export !!!.','Information');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('DataGridView Is Empty, Can Not Export !!!.','Information');", true);
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
            /* Verifies that the control is rendered */
        }

        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        #region -------------- Page Indexing ---------------
        protected void gvMeasurement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMeasurement.PageIndex = e.NewPageIndex;
            //BindGridView();
        }
        #endregion

        #region ---------- Clear Button Event ---------
        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                DDlSearch.SelectedIndex=0;
                txtSearch.Text = "";
                txtFromDate.Focus();
                SearchByDateFunction();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------ Search On Text Change ----------------
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != string.Empty)
                {
                    AssignID();                    
                    objMsr.Action = "BindDetails";

                    if (DDlSearch.SelectedValue.ToString() == "Member Id")
                    {
                        objMsr.Category = "MemberID";
                        objMsr.searchTxt = txtSearch.Text;

                    }
                    else if (DDlSearch.SelectedValue.ToString() == "Name")
                    {
                        objMsr.Category = "MemberName";
                        objMsr.searchTxt = txtSearch.Text;
                    }
                    else if (DDlSearch.SelectedValue.ToString() == "Contact")
                    {
                        objMsr.Category = "Contact";
                        objMsr.searchTxt = txtSearch.Text;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                        DDlSearch.Focus();
                        return;
                    }

                    BindGridViewDetails();
                    txtSearch.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('Select Category !!!','Information');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion


    }
}