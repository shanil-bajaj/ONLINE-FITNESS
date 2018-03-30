using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class ReportMemberwiseInstructionAllocation : System.Web.UI.Page
    {
      
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalReportMemeberWiseInstructor obj = new BalReportMemeberWiseInstructor();
        DataTable dt = new DataTable();
        int Flag;
        static int flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AssignDate();
                    bindDDLExecutive();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void AssignID()
        {
            obj.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            obj.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obj.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

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

        public void bindDDLExecutive()
        {
            try
            {
                 AssignID();
                dt = obj.BindExecutive();
                if (dt.Rows.Count > 0)
                {
                    ddlExecutive.DataSource = dt;
                    ddlExecutive.Items.Clear();
                    ddlExecutive.DataValueField = "Staff_AutoID";
                    ddlExecutive.DataTextField = "Name";
                    ddlExecutive.DataBind();
                    ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }


        public void SearchByDate()
        {
            try
            {
                obj.FromDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
                obj.ToDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
                AssignID();
                dt = obj.BindDataByDate();
                ViewState["Data"] = dt;
                BindGridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            flag = 1;
        }

        public void BindGridview()
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    gvMemberAllocation.DataSource = dt;
                    gvMemberAllocation.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('Record Not Found','Error');", true);
                    gvMemberAllocation.DataSource = dt;
                    gvMemberAllocation.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchByDate();
        }

        protected void gvMemberAllocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvMemberAllocation.PageIndex = e.NewPageIndex;
                SearchByDate();
            }
            else if (flag == 2)
            {
                gvMemberAllocation.PageIndex = e.NewPageIndex;
                SearchByInreuctorWise();
            }
       }

        public void SearchByInreuctorWise()
        {
            try
            {
                AssignID();
                obj.Executive_ID =Convert.ToInt32(ddlExecutive.SelectedValue);
                dt = obj.BindDataIntructoeWise();
                ViewState["Data"] = dt;
                BindGridview();
                flag = 2;
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            
        }

        protected void ddlExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchByInreuctorWise();
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            gvMemberAllocation.Visible = false;
            AssignDate();
            ddlExecutive.Text = "--Select--";
            ViewState["Data"] = null;
        }

        public void ExportToExcel()
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    DataTable dt2 = (DataTable)ViewState["Data"];
                    if (dt2.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MemberWiseInstructorData" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvMemberAllocation.AllowPaging = false;

                            gvMemberAllocation.DataSource = dt2;
                            gvMemberAllocation.DataBind();
                            gvMemberAllocation.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvMemberAllocation.HeaderRow.Cells)
                            {
                                cell.BackColor = gvMemberAllocation.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvMemberAllocation.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            gvMemberAllocation.GridLines = GridLines.Both;
                            gvMemberAllocation.RenderControl(hw);

                            //style to format numbers to string

                            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            Response.Write(style);
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
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }
}