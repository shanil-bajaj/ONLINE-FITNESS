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
    public partial class ReportMembershipEndDate : System.Web.UI.Page
    {
        BalActiveDeactive objActDct = new BalActiveDeactive();
        DataTable dt1 = new DataTable();
        int Flag;
        static int flag;
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssignDate();
                SearchData();
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            objActDct.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objActDct.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            //objActDct.FromDate = Convert.ToDateTime(txtFromDate.Text);
            //objActDct.ToDate = Convert.ToDateTime(txtToDate.Text);

            DateTime FromDate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FromDate))
            {
                string FromDate1 = FromDate.ToString("dd-MM-yyyy");
                objActDct.FromDate = DateTime.ParseExact(FromDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            DateTime ToDate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ToDate))
            {
                string ToDate1 = ToDate.ToString("dd-MM-yyyy");
                objActDct.ToDate = DateTime.ParseExact(ToDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            if (objActDct.FromDate > objActDct.ToDate)
            {
                objActDct.Action = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('From Date Should Not be Greater than To Date..!!!','Error');", true);
                //return;
            }
            else
            {
                objActDct.Action = "Select_By_End_Date";
            }
            dt1 = objActDct.Bind_gvActiveDeactiveByDate();
            lblCount.Text = "0";
            if (dt1.Rows.Count > 0)
            {
                count = dt1.Rows.Count;
                lblCount.Text = count.ToString();
                gvDesignation.DataSource = dt1;
                gvDesignation.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('Records Not Available..!!!','Error');", true);
            }
           
            flag = 1;
            ViewState["Data"] = dt1;

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

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            AssignDate();
           // SearchData();
            ViewState["Data"] = null;
            gvDesignation.DataSource = null;
            gvDesignation.DataBind();
        }

        protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvDesignation.PageIndex = e.NewPageIndex;
                SearchData();
            }
        }

        public void ExportToExcel()
        {
            try
            {
                if (ViewState["Data"] != null)
                {
                    DataTable dt4 = (DataTable)ViewState["Data"];
                    if (dt4.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=MembershipEndDate" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            gvDesignation.AllowPaging = false;

                            gvDesignation.DataSource = dt4;
                            gvDesignation.DataBind();
                            gvDesignation.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvDesignation.HeaderRow.Cells)
                            {
                                cell.BackColor = gvDesignation.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in gvDesignation.Rows)
                            {
                                row.BackColor = Color.White;
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.CssClass = "textmode";
                                }
                            }

                            gvDesignation.GridLines = GridLines.Both;
                            gvDesignation.RenderControl(hw);

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