using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace NDOnlineGym_2017
{
    public partial class PTReportPersonalInstructorReport : System.Web.UI.Page
    {
        BalPT pt = new BalPT();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTrainerIncentiveWise();
                ddlInstructor.Focus();
            }
        }

        public void BindTrainerIncentiveWise()
        {
            try
            {
                pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = pt.BindStaff();
                if (dt.Rows.Count > 0)
                {
                    ddlInstructor.DataSource = dt;
                    ddlInstructor.Items.Clear();
                    ddlInstructor.DataValueField = "TrainerID_Fk";
                    ddlInstructor.DataTextField = "name";
                    ddlInstructor.DataBind();
                    ddlInstructor.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void ddlInstructor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlInstructor.SelectedValue != "--Select--")
            {
                pt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                pt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                pt.TrainerID_Fk = Convert.ToInt32(ddlInstructor.SelectedValue.ToString());
                dt = pt.insstructorInfo();
                if (dt.Rows.Count > 0)
                {
                    lblTotal.Text = Convert.ToString(dt.Rows.Count);
                    gvActiveDeactive.DataSource = dt;
                    gvActiveDeactive.DataBind();
                    ViewState["dt"] = dt;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Instructor !!!','Error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlInstructor.SelectedIndex = 0;
            gvActiveDeactive.DataSource = null;
            gvActiveDeactive.DataBind();
            ddlInstructor.Focus();

        }
        public void ExportToExcel1()
        {
            try
            {
                if (ViewState["dt"] != null)
                {
                    DataTable dt2 = (DataTable)ViewState["dt"];
                    if (dt2.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=InstructorReport" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            //gvActiveDeactive.Columns[0].Visible = false;
                            //gvActiveDeactive.Columns[1].Visible = false;
                            //gvActiveDeactive.Columns[2].Visible = false;
                            //gvActiveDeactive.Columns[3].Visible = false;
                            gvActiveDeactive.AllowPaging = false;

                            gvActiveDeactive.DataSource = dt2;
                            gvActiveDeactive.DataBind();
                            gvActiveDeactive.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvActiveDeactive.HeaderRow.Cells)
                            {
                                cell.BackColor = gvActiveDeactive.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvActiveDeactive.Rows)
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


                            gvActiveDeactive.GridLines = GridLines.Both;
                            gvActiveDeactive.RenderControl(hw);

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
            /*Verifies that the control is rendered */
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel1();
        }
    }
}