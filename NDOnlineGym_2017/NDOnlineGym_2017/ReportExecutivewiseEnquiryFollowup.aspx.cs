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
    public partial class ReportExecutivewiseEnquiryFollowup : System.Web.UI.Page
    {
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalReportEnquiryFollowup obj=new BalReportEnquiryFollowup();
        // BalReportMemeberWiseInstructor obj = new BalReportMemeberWiseInstructor();
        DataTable dt=new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    AssignDate();
                    bindDDLExecutive();
                }
            }
            catch(Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                obBalStaffRegistration.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalStaffRegistration.SelectByIDNameContact_StaffInformation();
                if (dt.Rows.Count != 0)
                {
                    ddlExecutive.DataSource = dt;
                    ddlExecutive.Items.Clear();
                    ddlExecutive.DataValueField = "Staff_AutoID";
                    ddlExecutive.DataTextField = "Name";
                    ddlExecutive.DataBind();
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                    ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
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

        public void AssignID()
        {
            obj.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obj.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        public void SearchByDate()
        {
            try
            {
               // AssignDate();
                AssignID();

                GVEnquiryFollo.Visible = true;
                obj.FromDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
                obj.ToDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
                //AssignID();
                dt = obj.SearchByDate();
                ViewState["Data"] = dt;
                // ViewState["ViewActiveDeactive"] = dt;

                BindGridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        private void BindGridview()
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    GVEnquiryFollo.DataSource = dt;
                    GVEnquiryFollo.DataBind();
                }
                else
                {
                    GVEnquiryFollo.DataSource = dt;
                    GVEnquiryFollo.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchByDate();
        }


        public void SearchByExecutive()
        {
            try
            {
                AssignID();
                obj.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
                dt = obj.BindDataIntructoeWise();
                ViewState["Data"] = dt;
                BindGridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void SerachByNextFlwDate()
        {
            try
            {
                AssignID();
                obj.FromDate = Convert.ToDateTime(txtFromDate.Text, new CultureInfo("en-GB"));
                obj.ToDate = Convert.ToDateTime(txtToDate.Text, new CultureInfo("en-GB"));
                //AssignID();
                dt = obj.SearchByNextFlwDate();
                ViewState["Data"] = dt;
                // ViewState["ViewActiveDeactive"] = dt;

                BindGridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
     
        protected void ddlExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchByExecutive();
        }

        protected void btnExportToExcel_Click1(object sender, EventArgs e)
        {
            ExportToExcel();
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
                            GVEnquiryFollo.AllowPaging = false;

                            GVEnquiryFollo.DataSource = dt2;
                            GVEnquiryFollo.DataBind();
                            GVEnquiryFollo.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GVEnquiryFollo.HeaderRow.Cells)
                            {
                                cell.BackColor = GVEnquiryFollo.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GVEnquiryFollo.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            GVEnquiryFollo.GridLines = GridLines.Both;
                            GVEnquiryFollo.RenderControl(hw);

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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }                                   
}