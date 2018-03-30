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
    public partial class demoCourse : System.Web.UI.Page
    {
        BalLoginForm ObjLogin = new BalLoginForm();
        BalPackage pack = new BalPackage();
        BalCourseReg cour = new BalCourseReg();
        BalAddMember objMemberDetails = new BalAddMember();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        DataTable dt = new DataTable();
        BalExpense ObjExpense = new BalExpense();
        BalBalancePayment objBalance = new BalBalancePayment();

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

                rbtnSingle.Checked = true;
                Single();
                Get_MemberID1();
                ReceiptID();
                // Get_SubMemberID1();
                //BindGridview();
                BindGridview_1();
                txtFirstName1.Focus();
                AssignTodaysDate1();
                NextfDate();
                //  GetData();
                Bind_PaymentType();
                divsearch.Visible = false;
                if (Request.QueryString["Member_ID"] != null)
                {
                    int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    GetMemberDetails(memberid);
                    rbtnCouple.Enabled = false;
                    rbtnGroup.Enabled = false;

                    txtFirstName1.Enabled = false;
                    txtLastName1.Enabled = false;
                    ddlGender1.Enabled = false;
                    txtContact1.Enabled = false;
                    txtEmail1.Enabled = false;
                }  
                bindDDLExecutive();
                setExecutive();

                if (btnSave.Text == "Update")
                {
                    chkExecutive.Enabled = false;
                }
                if (Request.QueryString["Contact1"] != null)
                {
                    string contact1 = Request.QueryString["Contact1"];
                    Chk_Contactno(contact1);
                    txtEmail1.Focus();
                    rbtnCouple.Enabled = false;
                    rbtnGroup.Enabled = false;

                    txtFirstName1.Enabled = false;
                    txtLastName1.Enabled = false;
                    ddlGender1.Enabled = false;
                    txtContact1.Enabled = false;
                    txtEmail1.Enabled = false;
                    RemoveQueryStringParams("Contact1");
                }
                addReceipt.Enabled = true;
                addReceipt.Visible = true;

                if (Request.QueryString["MenuCourseDetails"] != null)
                {
                    divCourseReg.Visible = false;
                    divsearch.Visible = true;
                    divFormDetails.Visible = false;
                    CourseDetails.Visible = true;
                    txtFromDate.Focus();
                    SerachByDate_Cur();
                }

                if (Request.QueryString["Member_ID_Block"] != null)
                {
                    int memberid = Convert.ToInt32(Request.QueryString["Member_ID_Block"]);
                    GetMemberDetails(memberid);
                    rbtnCouple.Enabled = false;
                    rbtnGroup.Enabled = false;

                    //txtFirstName1.Enabled = false;
                    //txtLastName1.Enabled = false;
                    //ddlGender1.Enabled = false;
                    //txtContact1.Enabled = false;
                    //txtEmail1.Enabled = false;
                }  
            }
        }

        protected void RemoveQueryStringParams(string rname)
        {
            // reflect to readonly property
            PropertyInfo isReadOnly =
            typeof(System.Collections.Specialized.NameValueCollection)
            .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable
            isReadOnly.SetValue(this.Request.QueryString, false, null);
            // remove
            this.Request.QueryString.Remove(rname);
            // make collection readonly again
            isReadOnly.SetValue(this.Request.QueryString, true, null);
        }
        #region Assign_Date
        protected void AssignTodaysDate1()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDate.Text = todaydate.ToString("dd-MM-yyyy");
                txtToDate.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDate_CalendarExtender.StartDate = todaydate;
                //txtDOB.Text = todaydate.ToString("dd-MM-yyyy");
                //txtAnniversary.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        #endregion
        public void Bind_PaymentType()
        {
            try
            {
                ObjExpense.company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = ObjExpense.Get_PaymentType();
                if (dt.Rows.Count > 0)
                {
                    ddlPayMode.DataSource = dt;
                    ddlPayMode.Items.Clear();
                    ddlPayMode.DataValueField = "PaymentMode_AutoID";
                    ddlPayMode.DataTextField = "Name";
                    ddlPayMode.DataBind();
                    ddlPayMode.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void GetMemberDetails(int memberid)
        {
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objMemberDetails.Member_AutoID = Convert.ToInt32(memberid);
            dt = objMemberDetails.SelectByID_MemberInformation();
            if (dt.Rows.Count > 0)
            {
                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                txtFirstName1.Text = dt.Rows[0]["FName"].ToString();
                txtLastName1.Text = dt.Rows[0]["LName"].ToString();
                ddlGender1.Text = dt.Rows[0]["Gender"].ToString();
                txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                txtEmail1.Text = dt.Rows[0]["Email"].ToString();
            }
        }
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
            }
        }
        public void NextfDate()
        {
            DateTime Regdate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Regdate))
            {
                txtNextFollowupDate.Text = Regdate.ToString("dd-MM-yyyy");

            }
        }
        public void BindGridview()
        {
            try
            {
                pack.Category = "Active";
                pack.searchTxt = "Active";
                pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt.Clear();
                dt = pack.Get_Search();
                if (dt.Rows.Count > 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void BindGridview_1()
        {
            try
            {
                if (ddlSearch.SelectedValue.ToString() == "--Select--")
                {
                    pack.Category = "Select_Active";
                }
                else if (ddlSearch.SelectedValue.ToString() == "Package")
                {
                    pack.Category = "Package_Active";
                    pack.searchTxt = txtSearch.Text;

                }
                else if (ddlSearch.SelectedValue.ToString() == "Duration")
                {
                    pack.Category = "Duration_Active";
                    pack.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Active")
                {
                    pack.Category = "Active";
                    pack.searchTxt = txtSearch.Text;
                }
                else if (ddlSearch.SelectedValue.ToString() == "Deactive")
                {
                    pack.Category = "Deactive";
                    pack.searchTxt = txtSearch.Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    return;
                }
                pack.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                pack.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt.Clear();
                dt = pack.Get_Search();
                if (dt.Rows.Count > 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        static int Quantity = 1;
        protected void rbtnSingle_CheckedChanged(object sender, EventArgs e)
        {
            Single();

        }
        public void Single()
        {
            tableInfo.Visible = true;
            row1.Visible = true;
            row2.Visible = false;
            row3.Visible = false;
            row4.Visible = false;
            row5.Visible = false;
            row6.Visible = false;
            row7.Visible = false;
            row8.Visible = false;
            row9.Visible = false;
            row10.Visible = false;
            Get_MemberID1();
            Quantity = 1;
            txtFirstName1.Focus();
            txtnumber.Text = "";
            txtnumber.Enabled = false;

        }

        protected void rbtnCouple_CheckedChanged(object sender, EventArgs e)
        {
            Couple();
        }
        public void Couple()
        {
            tableInfo.Visible = true;
            row1.Visible = true;
            row2.Visible = true;
            row3.Visible = false;
            row4.Visible = false;
            row5.Visible = false;
            row6.Visible = false;
            row7.Visible = false;
            row8.Visible = false;
            row9.Visible = false;
            row10.Visible = false;
            Get_MemberID1();
            Quantity = 2;
            txtFirstName1.Focus();
            txtnumber.Text = "";
            txtnumber.Enabled = false;

        }
        protected void rbtnGroup_CheckedChanged(object sender, EventArgs e)
        {
            txtnumber.Enabled = true;
        }

        public void Group()
        {
            tableInfo.Visible = true;
            int ch = Convert.ToInt32(txtnumber.Text.ToString());
            txtFirstName1.Focus();
            switch (ch)
            {
                case 1: row1.Visible = true;
                    row2.Visible = false;
                    row3.Visible = false;
                    row4.Visible = false;
                    row5.Visible = false;
                    row6.Visible = false;
                    row7.Visible = false;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    Quantity = 1;
                    Get_MemberID1();
                    break;

                case 2: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = false;
                    row4.Visible = false;
                    row5.Visible = false;
                    row6.Visible = false;
                    row7.Visible = false;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 2;
                    break;

                case 3: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = false;
                    row5.Visible = false;
                    row6.Visible = false;
                    row7.Visible = false;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 3;
                    break;

                case 4: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;
                    row5.Visible = false;
                    row6.Visible = false;
                    row7.Visible = false;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 4;
                    break;

                case 5: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;
                    row5.Visible = true;
                    row6.Visible = false;
                    row7.Visible = false;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 5;
                    break;

                case 6: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;
                    row5.Visible = true;
                    row6.Visible = true;
                    row7.Visible = false;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 6;
                    break;

                case 7: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;
                    row5.Visible = true;
                    row6.Visible = true;
                    row7.Visible = true;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 7;
                    break;

                case 8: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;
                    row5.Visible = true;
                    row6.Visible = true;
                    row7.Visible = true;
                    row8.Visible = true;
                    row9.Visible = false;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 8;
                    break;

                case 9: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;
                    row5.Visible = true;
                    row6.Visible = true;
                    row7.Visible = true;
                    row8.Visible = true;
                    row9.Visible = true;
                    row10.Visible = false;
                    Get_MemberID1();
                    Quantity = 9;
                    break;

                case 10: row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;
                    row5.Visible = true;
                    row6.Visible = true;
                    row7.Visible = true;
                    row8.Visible = true;
                    row9.Visible = true;
                    row10.Visible = true;
                    Get_MemberID1();
                    Quantity = 10;
                    break;
            }
        }
        protected void txtnumber_TextChanged(object sender, EventArgs e)
        {
            Group();

            try
            {
                DataRow dr = null;
                //dt.Clear();
                dt1.Columns.Add(new DataColumn("Course_Auto"));
                dt1.Columns.Add(new DataColumn("Pack_AutoID"));
                dt1.Columns.Add(new DataColumn("Package"));
                dt1.Columns.Add(new DataColumn("Duration"));
                dt1.Columns.Add(new DataColumn("Session"));
                dt1.Columns.Add(new DataColumn("Amount"));
                dt1.Columns.Add(new DataColumn("StartDate"));
                dt1.Columns.Add(new DataColumn("EndDate"));
                dt1.Columns.Add(new DataColumn("Qty"));
                dt1.Columns.Add(new DataColumn("Total"));
                dt1.Columns.Add(new DataColumn("Discount"));
                dt1.Columns.Add(new DataColumn("FinalTotal"));
                dt1.Columns.Add(new DataColumn("DiscReason"));
                dt1.Columns.Add(new DataColumn("Staff_AutoID"));

                DataTable dtgurup = new DataTable();
                if (ViewState["DT"] != null)
                {
                    dtgurup.Clear();             
                    dtgurup = (DataTable)ViewState["DT"];
                }
                if (dtgurup.Rows.Count > 0)
                {
                    lblTotalFee.Text = "0";
                    for (int i = 0; i < dtgurup.Rows.Count; i++)
                    {
                        k = dt1.Rows.Count;
                        dr = dt1.NewRow();
                        dr["Course_Auto"] = dtgurup.Rows[i]["Course_Auto"].ToString();
                        dr["Pack_AutoID"] = dtgurup.Rows[i]["Pack_AutoID"].ToString();
                        dr["Package"] = dtgurup.Rows[i]["Package"].ToString();
                        dr["Duration"] = dtgurup.Rows[i]["Duration"].ToString();
                        dr["Session"] = dtgurup.Rows[i]["Session"].ToString();
                        dr["Amount"] = dtgurup.Rows[i]["Amount"].ToString();

                        int duration = Convert.ToInt32(dtgurup.Rows[i]["Duration"].ToString());

                        dr["StartDate"] = dtgurup.Rows[i]["StartDate"].ToString();// DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                        dr["EndDate"] = dtgurup.Rows[i]["EndDate"].ToString();// DateTime.UtcNow.Date.AddDays(duration).AddHours(5.5).ToString("dd-MM-yyyy");
                        dr["Qty"] = txtnumber.Text;
                        dr["Total"] = Convert.ToInt32(txtnumber.Text) * Convert.ToInt32(dtgurup.Rows[i]["Amount"].ToString());
                        dr["Discount"] = dtgurup.Rows[i]["Discount"].ToString();
                        dr["FinalTotal"] = (Quantity * Convert.ToInt32(dtgurup.Rows[i]["Amount"].ToString())) - Convert.ToInt32(dtgurup.Rows[i]["Discount"].ToString());
                        dr["DiscReason"] = dtgurup.Rows[i]["DiscReason"].ToString();
                        dr["Staff_AutoID"] = dtgurup.Rows[i]["Staff_AutoID"].ToString();

                        lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) + (Quantity * Convert.ToInt32(dtgurup.Rows[i]["Amount"].ToString())));

                        dt1.Rows.InsertAt(dr, k);
                        k++;
                        // }
                    }
                    ViewState["DT"] = dt1;
                    GvPakageAssign.DataSource = dt1;
                    GvPakageAssign.DataBind();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }


        int flag;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public int k = 0;
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime todaydate;
                //if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                //{
                //}

                var btnPre = (Control)sender;
                GridViewRow row1 = (GridViewRow)btnPre.NamingContainer;

                DataRow dr = null;
                //dt.Clear();
                dt1.Columns.Add(new DataColumn("Course_Auto"));
                dt1.Columns.Add(new DataColumn("Pack_AutoID"));
                dt1.Columns.Add(new DataColumn("Package"));
                dt1.Columns.Add(new DataColumn("Duration"));
                dt1.Columns.Add(new DataColumn("Session"));
                dt1.Columns.Add(new DataColumn("Amount"));
                dt1.Columns.Add(new DataColumn("StartDate"));
                dt1.Columns.Add(new DataColumn("EndDate"));
                dt1.Columns.Add(new DataColumn("Qty"));
                dt1.Columns.Add(new DataColumn("Total"));
                dt1.Columns.Add(new DataColumn("Discount"));
                dt1.Columns.Add(new DataColumn("FinalTotal"));
                dt1.Columns.Add(new DataColumn("DiscReason"));
                dt1.Columns.Add(new DataColumn("Staff_AutoID"));


                if (ViewState["DT"] != null)
                {
                    dt1.Clear();
                    dt1 = (DataTable)ViewState["DT"];
                }

                bool exists = dt1.Select().ToList().Exists(row => row["Pack_AutoID"].ToString().ToUpper() == row1.Cells[1].Text);

                if (exists == false)
                {
                    k = dt1.Rows.Count;
                    dr = dt1.NewRow();
                    dr["Course_Auto"] = k;
                    dr["Pack_AutoID"] = row1.Cells[1].Text;
                    dr["Package"] = row1.Cells[2].Text;
                    dr["Duration"] = row1.Cells[3].Text;
                    dr["Session"] = row1.Cells[4].Text;
                    dr["Amount"] = row1.Cells[5].Text;

                    int duration = Convert.ToInt32(row1.Cells[3].Text);

                    dr["StartDate"] = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                    dr["EndDate"] = DateTime.UtcNow.Date.AddDays(duration).AddHours(5.5).ToString("dd-MM-yyyy");
                    dr["Qty"] = Quantity;
                    dr["Total"] = Quantity * Convert.ToInt32(row1.Cells[5].Text);
                    dr["Discount"] = '0';
                    dr["FinalTotal"] = Quantity * Convert.ToInt32(row1.Cells[5].Text);
                    dr["DiscReason"] = "";
                    dr["Staff_AutoID"] = "";

                    //Asspak_total += Convert.ToDouble(Quantity * Convert.ToInt32(row1.Cells[5].Text));
                    //lblTotalFee.Text = Asspak_total.ToString();
                    lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) + (Quantity * Convert.ToInt32(row1.Cells[5].Text)));

                    dt1.Rows.InsertAt(dr, k);
                    k++;
                }
                ViewState["DT"] = dt1;
                GvPakageAssign.DataSource = dt1;
                GvPakageAssign.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        //protected void btnDelete_Command(object sender, CommandEventArgs e)
        //{
        //    try
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        dt1 = (DataTable)ViewState["DT"];
        //        dt1.Rows.RemoveAt(index);
        //        ViewState["DT"] = dt1;
        //        GvPakageAssign.DataSource = dt1;
        //        GvPakageAssign.DataBind();

        //        //int k = Convert.ToInt32(e.CommandArgument);
        //        //dt1 = (DataTable)ViewState["DT"];
        //        //dt1.Rows.RemoveAt(k);
        //        //GvPakageAssign.DataSource = dt1;
        //        //GvPakageAssign.DataBind();
        //        //ViewState["DT"] = dt1;
        //    }
        //    catch (Exception ex)
        //    {

        //        return;
        //    }
        //}

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindGridview_1();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtsDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
                if (btnSave.Text == "Update" && kt == 1)
                {
                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                    // txtReceiptid.Text = Receipt.ToString();
                    txtReceiptid.Text = ViewState["member_autoid"].ToString();
                    dt_package = cour.Get_Edit_Assihnpackage();
                    //ViewState["DT"] = dt_package;
                    kt = 2;
                    dt1.Columns.Add(new DataColumn("Course_Auto"));
                    dt1.Columns.Add(new DataColumn("Pack_AutoID"));
                    dt1.Columns.Add(new DataColumn("Package"));
                    dt1.Columns.Add(new DataColumn("Duration"));
                    dt1.Columns.Add(new DataColumn("Session"));
                    dt1.Columns.Add(new DataColumn("Amount"));
                    dt1.Columns.Add(new DataColumn("StartDate"));
                    dt1.Columns.Add(new DataColumn("EndDate"));
                    dt1.Columns.Add(new DataColumn("Qty"));
                    dt1.Columns.Add(new DataColumn("Total"));
                    dt1.Columns.Add(new DataColumn("Discount"));
                    dt1.Columns.Add(new DataColumn("FinalTotal"));
                    dt1.Columns.Add(new DataColumn("DiscReason"));
                    dt1.Columns.Add(new DataColumn("Staff_AutoID"));
                    DataRow row3 = dt1.NewRow();
                    int k = 0;
                    foreach (DataRow dr1 in dt_package.Rows)
                    {
                        k += 1;
                        row3 = dt1.NewRow();
                        row3["Course_Auto"] = k;//dr1["Course_Auto"].ToString();
                        row3["Pack_AutoID"] = dr1["Pack_AutoID"].ToString();

                        row3["Package"] = dr1["Package"].ToString();
                        row3["Duration"] = dr1["Duration"].ToString();

                        row3["Session"] = dr1["Session"].ToString();
                        row3["Amount"] = dr1["Amount"].ToString();

                        DateTime Enqdate = Convert.ToDateTime(dr1["StartDate"].ToString());
                        DateTime Enqdate1;
                        if (DateTime.TryParseExact(Enqdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Enqdate1))
                        {
                            row3["StartDate"] = Enqdate1.ToString("dd-MM-yyyy");
                        }


                        DateTime EndDate = Convert.ToDateTime(dr1["StartDate"].ToString());
                        DateTime EndDate1;
                        if (DateTime.TryParseExact(Enqdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate1))
                        {
                            row3["EndDate"] = EndDate1.ToString("dd-MM-yyyy");
                        }
                        // row3["StartDate"] = dr1["StartDate"].ToString();
                        //row3["EndDate"] = dr1["EndDate"].ToString();

                        row3["Qty"] = dr1["Qty"].ToString();
                        row3["Total"] = dr1["Total"].ToString();

                        row3["Discount"] = dr1["Discount"].ToString();
                        row3["FinalTotal"] = dr1["FinalTotal"].ToString();

                        row3["DiscReason"] = dr1["DiscReason"].ToString();
                        row3["Staff_AutoID"] = dr1["Staff_AutoID"].ToString();

                        dt1.Rows.Add(row3);
                        ViewState["DT"] = dt1;
                    }

                    dt1 = (DataTable)ViewState["DT"];
                    var btnPre = (Control)sender;
                    GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                    DataRow dr;
                    dr = dt1.NewRow();
                    int duration;

                    int s = currentRow.RowIndex;
                    dr["Course_Auto"] = s;// k;
                    dr["Pack_AutoID"] = row.Cells[1].Text;
                    dr["Package"] = row.Cells[2].Text;
                    duration = Convert.ToInt32(row.Cells[3].Text);
                    dr["Duration"] = row.Cells[3].Text;
                    dr["Session"] = row.Cells[4].Text;

                    TextBox txtsdate = (TextBox)currentRow.FindControl("txtsDate");
                    dr["StartDate"] = txtsdate.Text;

                    TextBox txtedate = (TextBox)currentRow.FindControl("txtEndate");
                    dr["EndDate"] = txtedate.Text;

                    TextBox txtamt = (TextBox)currentRow.FindControl("txtAmt");
                    dr["Amount"] = txtamt.Text;

                    TextBox Quantity = (TextBox)currentRow.FindControl("txtQuantity");
                    dr["Qty"] = Quantity.Text;

                    TextBox Total = (TextBox)currentRow.FindControl("txtTotal");
                    dr["Total"] = Total.Text;

                    TextBox txtdisc = (TextBox)currentRow.FindControl("txtDisc");
                    dr["Discount"] = txtdisc.Text;

                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                    int disc = cour.MaxDiscount();
                    if (txtdisc.Text == "")
                    {
                        txtdisc.Text = "0";
                        dr["Discount"] = txtdisc.Text;
                        dr["FinalTotal"] = Total.Text;

                    }

                    if (txtdisc.Text == "0")
                    {
                        txtdisc.Text = "0";
                        dr["Discount"] = txtdisc.Text;
                        dr["FinalTotal"] = Total.Text;

                    }

                    if (Convert.ToInt32(txtdisc.Text) > disc)
                    {
                        //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Max Discount is Exceed The Limit !!!','Error');", true);
                        dr["Discount"] = "0";
                        txtdisc.Focus();
                        dr["FinalTotal"] = Total.Text;

                    }
                    else
                    {
                        int finalTotal = Convert.ToInt32(Total.Text) - Convert.ToInt32(txtdisc.Text);
                        dr["FinalTotal"] = finalTotal;
                        //lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) - Convert.ToDouble(txtdisc.Text));
                        //if (lblTotalFeeDue.Text != "0")
                        //{
                        //    lblTotalFeeDue.Text = lblTotalFee.Text;
                        //    lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                        //}

                    }

                    TextBox txtDiscreason = (TextBox)currentRow.FindControl("txtDiscreason");
                    dr["DiscReason"] = txtDiscreason.Text;

                    TextBox ddlInstru = (TextBox)currentRow.FindControl("ddlInstru");
                    dr["Staff_AutoID"] = ddlInstru.Text;

                    dt1.Rows[s].Delete();
                    dt1.Rows.InsertAt(dr, s);
                    ViewState["DT"] = dt1;
                    GvPakageAssign.DataSource = dt1;
                    GvPakageAssign.DataBind();

                    foreach (GridViewRow row4 in GvPakageAssign.Rows)
                    {
                        TextBox tb = (TextBox)row4.FindControl("txtFinalTotal");
                        string someVariableName = tb.Text; // get the value from TextBox
                        if (tb.Text != "")
                        {
                            c = c + Convert.ToDouble(tb.Text); //storing total qty into variable
                        }
                        lblTotalFee.Text = c.ToString();
                    }

                    if (lblTotalFeeDue.Text != "0")
                    {
                        lblTotalFeeDue.Text = lblTotalFee.Text;
                        lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                    }
                }
                else
                {

                    dt1 = (DataTable)ViewState["DT"];
                    var btnPre = (Control)sender;
                    GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                    DataRow dr;
                    dr = dt1.NewRow();
                    int duration;


                    int s = currentRow.RowIndex;
                    dr["Course_Auto"] = s;// k;
                    dr["Pack_AutoID"] = row.Cells[1].Text;
                    dr["Package"] = row.Cells[2].Text;
                    duration = Convert.ToInt32(row.Cells[3].Text);
                    dr["Duration"] = row.Cells[3].Text;
                    dr["Session"] = row.Cells[4].Text;

                    TextBox txtsdate = (TextBox)currentRow.FindControl("txtsDate");
                    DateTime sdate;
                    if (DateTime.TryParseExact(txtsdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out sdate))
                    {
                    }

                    DateTime dtStartDate = sdate;
                    DateTime enddate = dtStartDate.AddDays(duration);
                    dr["StartDate"] = txtsdate.Text;
                    TextBox txtedate = (TextBox)currentRow.FindControl("txtEndate");
                    txtedate.Text = enddate.ToString("dd-MM-yyyy");    //enddate.ToShortDateString();
                    DateTime enddate1;
                    if (DateTime.TryParseExact(txtedate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out enddate1))
                    {
                    }
                    //txtedate.Text = enddate1.ToShortDateString();
                    dr["EndDate"] = txtedate.Text;

                    TextBox txtamt = (TextBox)currentRow.FindControl("txtAmt");
                    dr["Amount"] = txtamt.Text;

                    TextBox Quantity = (TextBox)currentRow.FindControl("txtQuantity");
                    dr["Qty"] = Quantity.Text;

                    TextBox Total = (TextBox)currentRow.FindControl("txtTotal");
                    dr["Total"] = Total.Text;

                    TextBox txtdisc = (TextBox)currentRow.FindControl("txtDisc");
                    dr["Discount"] = txtdisc.Text;

                    TextBox FinalTotal = (TextBox)currentRow.FindControl("txtDisc");
                    //  dr["FinalTotal"] = FinalTotal.Text;

                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                    int disc = cour.MaxDiscount();
                    if (txtdisc.Text == "")
                    {
                        txtdisc.Text = "0";
                        dr["Discount"] = txtdisc.Text;
                        dr["FinalTotal"] = Total.Text;

                    }

                    if (txtdisc.Text == "0")
                    {
                        txtdisc.Text = "0";
                        dr["Discount"] = txtdisc.Text;
                        dr["FinalTotal"] = Total.Text;

                    }

                    if (Convert.ToInt32(txtdisc.Text) > disc)
                    {
                        //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Max Discount is Exceed The Limit !!!','Error');", true);
                        dr["Discount"] = "0";
                        txtdisc.Focus();
                        dr["FinalTotal"] = Total.Text;

                    }
                    else
                    {
                        int finalTotal = Convert.ToInt32(Total.Text) - Convert.ToInt32(txtdisc.Text);
                        dr["FinalTotal"] = finalTotal;
                        //lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) - Convert.ToDouble(txtdisc.Text));
                        //if (lblTotalFeeDue.Text != "0")
                        //{
                        //    lblTotalFeeDue.Text = lblTotalFee.Text;
                        //    lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                        //}
                    }


                    TextBox txtDiscreason = (TextBox)currentRow.FindControl("txtDiscreason");
                    dr["DiscReason"] = txtDiscreason.Text;

                    TextBox ddlInstru = (TextBox)currentRow.FindControl("ddlInstru");
                    dr["Staff_AutoID"] = ddlInstru.Text;

                    // dt1.Rows.Add(dr);
                    //dt1.Rows.InsertAt(dr, k);            
                    //k = k - 1;
                    dt1.Rows[s].Delete();
                    dt1.Rows.InsertAt(dr, s);
                    //k++;
                    ViewState["DT"] = dt1;
                    GvPakageAssign.DataSource = dt1;
                    GvPakageAssign.DataBind();

                    foreach (GridViewRow row3 in GvPakageAssign.Rows)
                    {
                        TextBox tb = (TextBox)row3.FindControl("txtFinalTotal");
                        string someVariableName = tb.Text; // get the value from TextBox
                        if (tb.Text != "")
                        {
                            c = c + Convert.ToDouble(tb.Text); //storing total qty into variable
                        }
                        lblTotalFee.Text = c.ToString();
                    }
                    if (lblTotalFeeDue.Text != "0")
                    {
                        lblTotalFeeDue.Text = lblTotalFee.Text;
                        lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        static double Asspak_total = 0;
        static int kt = 0;
        protected void txtDisc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
                if (btnSave.Text == "Update" && kt == 1)
                {
                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                    // txtReceiptid.Text = Receipt.ToString();
                    txtReceiptid.Text = ViewState["member_autoid"].ToString();
                    dt_package = cour.Get_Edit_Assihnpackage();
                    //ViewState["DT"] = dt_package;
                    kt = 2;
                    dt1.Columns.Add(new DataColumn("Course_Auto"));
                    dt1.Columns.Add(new DataColumn("Pack_AutoID"));
                    dt1.Columns.Add(new DataColumn("Package"));
                    dt1.Columns.Add(new DataColumn("Duration"));
                    dt1.Columns.Add(new DataColumn("Session"));
                    dt1.Columns.Add(new DataColumn("Amount"));
                    dt1.Columns.Add(new DataColumn("StartDate"));
                    dt1.Columns.Add(new DataColumn("EndDate"));
                    dt1.Columns.Add(new DataColumn("Qty"));
                    dt1.Columns.Add(new DataColumn("Total"));
                    dt1.Columns.Add(new DataColumn("Discount"));
                    dt1.Columns.Add(new DataColumn("FinalTotal"));
                    dt1.Columns.Add(new DataColumn("DiscReason"));
                    dt1.Columns.Add(new DataColumn("Staff_AutoID"));
                    DataRow row3 = dt1.NewRow();
                    int k = 0;
                    foreach (DataRow dr1 in dt_package.Rows)
                    {
                        k += 1;
                        row3 = dt1.NewRow();
                        row3["Course_Auto"] = k;// dr1["Course_Auto"].ToString();
                        row3["Pack_AutoID"] = dr1["Pack_AutoID"].ToString();

                        row3["Package"] = dr1["Package"].ToString();
                        row3["Duration"] = dr1["Duration"].ToString();

                        row3["Session"] = dr1["Session"].ToString();
                        row3["Amount"] = dr1["Amount"].ToString();

                        DateTime Enqdate = Convert.ToDateTime(dr1["StartDate"].ToString());
                        DateTime Enqdate1;
                        if (DateTime.TryParseExact(Enqdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Enqdate1))
                        {
                            row3["StartDate"] = Enqdate1.ToString("dd-MM-yyyy");
                        }


                        DateTime EndDate = Convert.ToDateTime(dr1["StartDate"].ToString());
                        DateTime EndDate1;
                        if (DateTime.TryParseExact(Enqdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate1))
                        {
                            row3["EndDate"] = EndDate1.ToString("dd-MM-yyyy");
                        }

                        //row3["StartDate"] = dr1["StartDate"].ToString();
                        //row3["EndDate"] = dr1["EndDate"].ToString();

                        row3["Qty"] = dr1["Qty"].ToString();
                        row3["Total"] = dr1["Total"].ToString();

                        row3["Discount"] = dr1["Discount"].ToString();
                        row3["FinalTotal"] = dr1["FinalTotal"].ToString();

                        row3["DiscReason"] = dr1["DiscReason"].ToString();
                        row3["Staff_AutoID"] = dr1["Staff_AutoID"].ToString();

                        dt1.Rows.Add(row3);
                        ViewState["DT"] = dt1;
                    }
                }

                dt1 = (DataTable)ViewState["DT"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr;
                dr = dt1.NewRow();
                int duration;

                int s = currentRow.RowIndex;
                dr["Course_Auto"] = s;// k;
                dr["Pack_AutoID"] = row.Cells[1].Text;
                dr["Package"] = row.Cells[2].Text;
                duration = Convert.ToInt32(row.Cells[3].Text);
                dr["Duration"] = row.Cells[3].Text;
                dr["Session"] = row.Cells[4].Text;

                TextBox txtsdate = (TextBox)currentRow.FindControl("txtsDate");
                dr["StartDate"] = txtsdate.Text;

                TextBox txtedate = (TextBox)currentRow.FindControl("txtEndate");
                dr["EndDate"] = txtedate.Text;

                TextBox txtamt = (TextBox)currentRow.FindControl("txtAmt");
                dr["Amount"] = txtamt.Text;

                TextBox Quantity = (TextBox)currentRow.FindControl("txtQuantity");
                dr["Qty"] = Quantity.Text;

                TextBox Total = (TextBox)currentRow.FindControl("txtTotal");
                dr["Total"] = Total.Text;

                TextBox txtdisc = (TextBox)currentRow.FindControl("txtDisc");

                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                int disc = cour.MaxDiscount();
                if (txtdisc.Text == "")
                {
                    txtdisc.Text = "0";
                    dr["Discount"] = txtdisc.Text;
                }

                if (txtdisc.Text != "")
                {
                    dr["Discount"] = txtdisc.Text;
                }

                //if (Convert.ToInt32(txtdisc.Text) > disc)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Max Discount is Exceed The Limit !!!','Error');", true);
                //    dr["Discount"] = "0";
                //    txtdisc.Focus();
                //    dr["FinalTotal"] = Total.Text;

                //}
                //else
                //{
                //    int finalTotal = Convert.ToInt32(Total.Text) - Convert.ToInt32(txtdisc.Text);
                //    dr["FinalTotal"] = finalTotal;
                //    //lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) - Convert.ToDouble(txtdisc.Text));
                //    //if (lblTotalFeeDue.Text != "0")
                //    //{
                //    //    lblTotalFeeDue.Text = lblTotalFee.Text;
                //    //    lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                //    //}

                //}

                if (rbtnSingle.Checked == true)
                {
                    if (Convert.ToInt32(txtdisc.Text) > disc)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Max Discount is Exceed The Limit !!!','Error');", true);
                        dr["Discount"] = "0";
                        txtdisc.Focus();
                        dr["FinalTotal"] = Total.Text;
                    }
                    else
                    {
                        int finalTotal = Convert.ToInt32(Total.Text) - Convert.ToInt32(txtdisc.Text);
                        dr["FinalTotal"] = finalTotal;
                    }
                }
                else if (rbtnCouple.Checked == true)
                {
                    if (Convert.ToInt32(txtdisc.Text) > (disc * 2))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Max Discount is Exceed The Limit !!!','Error');", true);
                        dr["Discount"] = "0";
                        txtdisc.Focus();
                        dr["FinalTotal"] = Total.Text;
                    }
                    else
                    {
                        int finalTotal = Convert.ToInt32(Total.Text) - Convert.ToInt32(txtdisc.Text);
                        dr["FinalTotal"] = finalTotal;
                    }
                }
                else if (rbtnGroup.Checked == true)
                {
                    if (txtnumber.Text != "")
                    {
                        if (Convert.ToInt32(txtdisc.Text) > (disc * Convert.ToInt32(txtnumber.Text)))
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Max Discount is Exceed The Limit !!!','Error');", true);
                            dr["Discount"] = "0";
                            txtdisc.Focus();
                            dr["FinalTotal"] = Total.Text;
                        }
                        else
                        {
                            int finalTotal = Convert.ToInt32(Total.Text) - Convert.ToInt32(txtdisc.Text);
                            dr["FinalTotal"] = finalTotal;
                        }
                    }
                }

                TextBox txtDiscreason = (TextBox)currentRow.FindControl("txtDiscreason");
                dr["DiscReason"] = txtDiscreason.Text;

                TextBox ddlInstru = (TextBox)currentRow.FindControl("ddlInstru");
                dr["Staff_AutoID"] = ddlInstru.Text;
                //Asspak_total -= Convert.ToDouble(txtdisc.Text);
                // lblTotalFee.Text = Asspak_total.ToString();

                // Asspak_total -= Convert.ToDouble(txtdisc.Text);


                dt1.Rows[s].Delete();
                dt1.Rows.InsertAt(dr, s);
                ViewState["DT"] = dt1;
                GvPakageAssign.DataSource = dt1;
                GvPakageAssign.DataBind();

                foreach (GridViewRow row3 in GvPakageAssign.Rows)
                {
                    TextBox tb = (TextBox)row3.FindControl("txtFinalTotal");
                    string someVariableName = tb.Text; // get the value from TextBox
                    if (tb.Text != "")
                    {
                        c = c + Convert.ToDouble(tb.Text); //storing total qty into variable
                    }
                    lblTotalFee.Text = c.ToString();
                }
                //lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) - Convert.ToDouble(txtdisc.Text));
                if (lblTotalFeeDue.Text != "0")
                {
                    lblTotalFeeDue.Text = lblTotalFee.Text;
                    lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                }

                int count = int.Parse(GvPakageAssign.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        //Delete Row From Package Assingn Gridview
        protected void GvPakageAssign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                dt1 = (DataTable)ViewState["DT"];

                double paid = Convert.ToDouble(dt1.Rows[index][11].ToString());
                lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) - paid);

                dt1.Rows[index].Delete();
                ViewState["DT"] = dt1;
                GvPakageAssign.DataSource = dt1;
                GvPakageAssign.DataBind();

                if (dt1.Rows.Count == 0)
                {
                    lblTotalFee.Text = "0";
                    gvBalancePayment.DataSource = null;
                    gvBalancePayment.DataBind();
                    lblBalance.Text = "0";
                    lblPaidFee.Text = "0";
                    lblTotalFeeDue.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        DataTable dt3 = new DataTable();
        static double TotalFeeDue = 0;
        protected void addReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GvPakageAssign.Rows)
                {


                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");


                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                    {
                        Sdate = S_Date.ToString("yyyy-MM-dd");
                    }

                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");


                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                    {
                        Edate = E_Date.ToString("yyyy-MM-dd");
                    }
                }

                if (Sdate != null && Edate != null)
                {
                    if (gvBalancePayment.Rows.Count > 0)
                    {
                        addReceipt.Enabled = false;
                        addReceipt.Visible = false;
                    }
                    else
                    {

                        DataRow dr1 = null;
                        //dt.Clear();
                        dt3.Columns.Add(new DataColumn("Bal_Auto"));
                        dt3.Columns.Add(new DataColumn("PaymentMode"));
                        dt3.Columns.Add(new DataColumn("Cardno"));
                        dt3.Columns.Add(new DataColumn("payDate"));
                        dt3.Columns.Add(new DataColumn("CardExpirydate"));
                        dt3.Columns.Add(new DataColumn("BankName"));
                        dt3.Columns.Add(new DataColumn("BranchName"));
                        dt3.Columns.Add(new DataColumn("Paid"));
                        dt3.Columns.Add(new DataColumn("TaxType"));
                        dt3.Columns.Add(new DataColumn("taxpec"));
                        dt3.Columns.Add(new DataColumn("TaxValue"));
                        dt3.Columns.Add(new DataColumn("PaidWithTax"));

                        if (ViewState["DT3"] != null)
                        {
                            dt3 = (DataTable)ViewState["DT3"];
                        }

                        k = dt3.Rows.Count;
                        dr1 = dt3.NewRow();
                        dr1["Bal_Auto"] = k;
                        dr1["PaymentMode"] = ddlPayMode.SelectedItem.Text;

                        if (ddlPayMode.SelectedItem.Text != "--Select--")
                        {
                            dr1["Cardno"] = "";
                            dr1["payDate"] = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                            dr1["CardExpirydate"] = DateTime.UtcNow.Date.AddHours(5.5).ToString("dd-MM-yyyy");
                            dr1["BankName"] = "";
                            dr1["BranchName"] = "";
                            dr1["Paid"] = "0";
                            dr1["TaxType"] = DropDownList2.SelectedValue.ToString();

                            if (DropDownList2.SelectedValue == "Including")
                            {
                                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                                DataTable tx = cour.Get_including_Tax();
                                if (tx.Rows.Count > 0)
                                {
                                    dr1["taxpec"] = tx.Rows[0][0].ToString();
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Insert Tax In Master!!!','Error');", true);
                                }
                            }
                            else
                            {
                                dr1["taxpec"] = "";
                            }
                            dr1["TaxValue"] = "";
                            dr1["PaidWithTax"] = "";

                            dt3.Rows.InsertAt(dr1, k);
                            k++;
                            //}
                            ViewState["DT3"] = dt3;
                            gvBalancePayment.DataSource = dt3;
                            gvBalancePayment.DataBind();


                            if (lblTotalFeeDue.Text == "0")
                            {
                                lblTotalFeeDue.Text = lblTotalFee.Text;
                                TotalFees_Due = Convert.ToDouble(lblTotalFee.Text);
                            }
                            if (lblBalance.Text == "0")
                            {
                                lblBalance.Text = lblTotalFee.Text;
                                Balance = Convert.ToDouble(lblBalance.Text);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Payment Mode  !!!','Error');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Start Date and End Date Should Not Be Blank  !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }


        }
        int p = 0;
        protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;

                if (btnSave.Text == "Update" && kt == 1 || kt == 2)
                {
                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    txtReceiptid.Text = ViewState["member_autoid"].ToString();
                    if (txtReceiptid.Text != "")
                        cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter ReceiptID !!!','Information');", true);
                    dt = cour.Get_Edit_MemberType();
                    if (dt.Rows.Count > 0)
                    {
                        cour.Status = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        cour.Status = dt.Rows[0][0].ToString();
                    }
                    dt_package = cour.Get_Edit_Payment();
                    //ViewState["DT"] = dt_package;
                    kt = 3;

                    dt3.Columns.Add(new DataColumn("Bal_Auto"));
                    dt3.Columns.Add(new DataColumn("PaymentMode"));
                    dt3.Columns.Add(new DataColumn("Cardno"));
                    dt3.Columns.Add(new DataColumn("payDate"));
                    dt3.Columns.Add(new DataColumn("CardExpirydate"));
                    dt3.Columns.Add(new DataColumn("BankName"));
                    dt3.Columns.Add(new DataColumn("BranchName"));
                    dt3.Columns.Add(new DataColumn("Paid"));
                    dt3.Columns.Add(new DataColumn("TaxType"));
                    dt3.Columns.Add(new DataColumn("taxpec"));
                    dt3.Columns.Add(new DataColumn("TaxValue"));
                    dt3.Columns.Add(new DataColumn("PaidWithTax"));

                    DataRow row3 = dt3.NewRow();
                    int j = 0;
                    foreach (DataRow dr11 in dt_package.Rows)
                    {
                        j += 1;
                        row3 = dt3.NewRow();
                        row3[0] = dr11["Bal_Auto"].ToString();
                        row3[1] = dr11["PaymentMode"].ToString();

                        row3[2] = dr11["Cardno"].ToString();
                        row3[3] = dr11["payDate"].ToString();

                        row3[4] = dr11["CardExpirydate"].ToString();
                        row3[5] = dr11["BankName"].ToString();

                        row3[6] = dr11["BranchName"].ToString();
                        row3[7] = dr11["Paid"].ToString();

                        row3[8] = dr11["TaxType"].ToString();
                        row3[9] = dr11["taxpec"].ToString();

                        row3[10] = dr11["TaxValue"].ToString();
                        row3[11] = dr11["PaidWithTax"].ToString();

                        dt3.Rows.Add(row3);
                        ViewState["DT3"] = dt3;
                    }
                }

                dt3 = (DataTable)ViewState["DT3"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr1;
                dr1 = dt3.NewRow();
                int duration;
                dr1 = dt3.NewRow();
                int s = currentRow.RowIndex;
                dr1["Bal_Auto"] = s;

                //ViewState["k"] = s.ToString();               
                //ViewState["k_hold"] = p.ToString(); 

                TextBox txtPaymentMode = (TextBox)currentRow.FindControl("txtPaymentMode");
                dr1["PaymentMode"] = txtPaymentMode.Text;

                TextBox txtNumber = (TextBox)currentRow.FindControl("txtNumber");
                dr1["Cardno"] = txtNumber.Text;

                TextBox txtDate = (TextBox)currentRow.FindControl("txtDate");
                dr1["payDate"] = txtDate.Text;

                TextBox txtExpiryDate = (TextBox)currentRow.FindControl("txtExpiryDate");
                dr1["CardExpirydate"] = txtExpiryDate.Text;

                TextBox txtBankName = (TextBox)currentRow.FindControl("txtBankName");
                dr1["BankName"] = txtBankName.Text;

                TextBox txtBranchName = (TextBox)currentRow.FindControl("txtBranchName");
                dr1["BranchName"] = txtBranchName.Text;

                TextBox txtPaidAmount = (TextBox)currentRow.FindControl("txtPaidAmount");
                if (txtPaidAmount.Text != "")
                {
                    dr1["Paid"] = txtPaidAmount.Text;
                }
                else
                {
                    dr1["Paid"] = "0";
                    txtPaidAmount.Text = "0";
                }

                TextBox ddlTax = (TextBox)currentRow.FindControl("ddlTax");
                dr1["TaxType"] = ddlTax.Text;//ViewState["DDlTax"];
                double Taxvalue;

                TextBox txtTax = (TextBox)currentRow.FindControl("txtTax");
                dr1["taxpec"] = txtTax.Text;

                if ((Convert.ToInt32(txtPaidAmount.Text)) <= (Convert.ToInt32(lblTotalFee.Text)))
                {
                    if (ddlTax.Text == "Excluding")
                    {
                        if (txtTax.Text != "")
                        {
                            Taxvalue = (Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100;
                            dr1["TaxValue"] = Taxvalue.ToString();
                            dr1["PaidWithTax"] = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                            PaidFees += Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                            lblPaidFee.Text = PaidFees.ToString();
                            double temp = Convert.ToDouble(lblTotalFeeDue.Text);
                            double duefees = temp + Taxvalue;
                        }
                        else
                        {

                        }


                        //    lblTotalFeeDue.Text = duefees.ToString();
                        //    lblBalance.Text = Convert.ToString(duefees - PaidFees);

                    }
                    else
                    {
                        double a = 0, y = 0, x = 0, z = 0;
                        a = Convert.ToDouble(txtPaidAmount.Text);
                        y = Convert.ToDouble(txtTax.Text);

                        x = (100 * a) / (100 + y);
                        z = a - x;
                        TextBox Txtvalue = (TextBox)currentRow.FindControl("Txtvalue");
                        dr1["TaxValue"] = z.ToString("#,0.00");
                        TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");
                        dr1["PaidWithTax"] = txtPaidAmount.Text;


                        //    lblPaidFee.Text = (Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(txtPaidAmount.Text)).ToString();
                        //    lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Paid Fees less than Total Fees !!!','Error');", true);
                    dr1["Paid"] = "0";
                }
                dt3.Rows[s].Delete();
                dt3.Rows.InsertAt(dr1, s);
                k++;
                ViewState["DT3"] = dt3;
                gvBalancePayment.DataSource = dt3;
                gvBalancePayment.DataBind();

                //for (int i = 0; i < (dt3.Rows.Count); i++)
                //{
                //    a = Convert.ToDouble(dt3.Rows[i][11].ToString());
                //    c = c + a; //storing total qty into variable 
                //    lblPaidFee.Text = c.ToString();
                //}
                // lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();



                foreach (GridViewRow row3 in gvBalancePayment.Rows)
                {
                    TextBox tb = (TextBox)row3.FindControl("txtTotalAmount");
                    string someVariableName = tb.Text; // get the value from TextBox
                    if (tb.Text != "")
                    {
                        c = c + Convert.ToDouble(tb.Text); //storing total qty into variable
                    }
                    lblPaidFee.Text = c.ToString();
                }
                lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        double k_paid = 0;
        static double FinalTotal = 0;
        static double PaidFees = 0;
        static double TotalFees_Due = 0;
        static double Balance = 0;
        double a = 0, b = 0, c = 0;
        protected void txtTax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;

                if (btnSave.Text == "Update" && kt == 1 || kt == 2)
                {
                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                    txtReceiptid.Text = ViewState["member_autoid"].ToString();
                    dt_package = cour.Get_Edit_Payment();
                    //ViewState["DT"] = dt_package;
                    kt = 3;

                    dt3.Columns.Add(new DataColumn("Bal_Auto"));
                    dt3.Columns.Add(new DataColumn("PaymentMode"));
                    dt3.Columns.Add(new DataColumn("Cardno"));
                    dt3.Columns.Add(new DataColumn("payDate"));
                    dt3.Columns.Add(new DataColumn("CardExpirydate"));
                    dt3.Columns.Add(new DataColumn("BankName"));
                    dt3.Columns.Add(new DataColumn("BranchName"));
                    dt3.Columns.Add(new DataColumn("Paid"));
                    dt3.Columns.Add(new DataColumn("TaxType"));
                    dt3.Columns.Add(new DataColumn("taxpec"));
                    dt3.Columns.Add(new DataColumn("TaxValue"));
                    dt3.Columns.Add(new DataColumn("PaidWithTax"));

                    DataRow row3 = dt3.NewRow();
                    int j = 0;
                    foreach (DataRow dr11 in dt_package.Rows)
                    {
                        j += 1;
                        row3 = dt3.NewRow();
                        row3[0] = dr11["Bal_Auto"].ToString();
                        row3[1] = dr11["PaymentMode"].ToString();

                        row3[2] = dr11["Cardno"].ToString();
                        row3[3] = dr11["payDate"].ToString();

                        row3[4] = dr11["CardExpirydate"].ToString();
                        row3[5] = dr11["BankName"].ToString();

                        row3[6] = dr11["BranchName"].ToString();
                        row3[7] = dr11["Paid"].ToString();

                        row3[8] = dr11["TaxType"].ToString();
                        row3[9] = dr11["taxpec"].ToString();

                        row3[10] = dr11["TaxValue"].ToString();
                        row3[11] = dr11["PaidWithTax"].ToString();

                        dt3.Rows.Add(row3);
                        ViewState["DT3"] = dt3;
                    }
                }

                dt3 = (DataTable)ViewState["DT3"];
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr1;
                dr1 = dt3.NewRow();
                int duration;
                dr1 = dt3.NewRow();

                int s = currentRow.RowIndex;
                //dr["ID"] = s;

                //dt3.Columns.Add(new DataColumn("Bal_Auto"));
                //dt3.Columns.Add(new DataColumn("PaymentMode"));
                //dt3.Columns.Add(new DataColumn("Cardno"));
                //dt3.Columns.Add(new DataColumn("payDate"));
                //dt3.Columns.Add(new DataColumn("CardExpirydate"));
                //dt3.Columns.Add(new DataColumn("BankName"));
                //dt3.Columns.Add(new DataColumn("BranchName"));
                //dt3.Columns.Add(new DataColumn("Paid"));
                //dt3.Columns.Add(new DataColumn("TaxType"));
                //dt3.Columns.Add(new DataColumn("taxpec"));
                //dt3.Columns.Add(new DataColumn("TaxValue"));
                //dt3.Columns.Add(new DataColumn("PaidWithTax"));


                dr1["Bal_Auto"] = s;

                TextBox txtPaymentMode = (TextBox)currentRow.FindControl("txtPaymentMode");
                dr1["PaymentMode"] = txtPaymentMode.Text;

                TextBox txtNumber = (TextBox)currentRow.FindControl("txtNumber");
                dr1["Cardno"] = txtNumber.Text;

                TextBox txtDate = (TextBox)currentRow.FindControl("txtDate");
                dr1["payDate"] = txtDate.Text;

                TextBox txtExpiryDate = (TextBox)currentRow.FindControl("txtExpiryDate");
                dr1["CardExpirydate"] = txtExpiryDate.Text;

                TextBox txtBankName = (TextBox)currentRow.FindControl("txtBankName");
                dr1["BankName"] = txtBankName.Text;

                TextBox txtBranchName = (TextBox)currentRow.FindControl("txtBranchName");
                dr1["BranchName"] = txtBranchName.Text;

                TextBox txtPaidAmount = (TextBox)currentRow.FindControl("txtPaidAmount");
                dr1["Paid"] = txtPaidAmount.Text;

                TextBox ddlTax = (TextBox)currentRow.FindControl("ddlTax");
                dr1["TaxType"] = ddlTax.Text;//ViewState["DDlTax"];
                double Taxvalue;

                TextBox txtTax = (TextBox)currentRow.FindControl("txtTax");
                dr1["taxpec"] = txtTax.Text;

                if (ddlTax.Text == "Excluding")
                {
                    Taxvalue = (Convert.ToDouble(txtPaidAmount.Text) * Convert.ToDouble(txtTax.Text)) / 100;
                    dr1["TaxValue"] = Taxvalue.ToString();
                    dr1["PaidWithTax"] = Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                    PaidFees += Convert.ToDouble(txtPaidAmount.Text) + Taxvalue;
                    //lblPaidFee.Text = PaidFees.ToString();
                    double temp = Convert.ToDouble(lblTotalFeeDue.Text);
                    double duefees = temp + Taxvalue;
                    lblTotalFeeDue.Text = duefees.ToString();
                    //lblBalance.Text = Convert.ToString(duefees - PaidFees);

                }
                else
                {
                    //TextBox Txtvalue = (TextBox)currentRow.FindControl("Txtvalue");
                    //dr1["Txtvalue"] = Txtvalue.Text;
                    //TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");
                    //dr1["txtTotalAmount"] = txtTotalAmount.Text;


                    double a = 0, y = 0, x = 0, z = 0;
                    a = Convert.ToDouble(txtPaidAmount.Text);
                    y = Convert.ToDouble(txtTax.Text);

                    x = (100 * a) / (100 + y);
                    z = a - x;
                    TextBox Txtvalue = (TextBox)currentRow.FindControl("Txtvalue");
                    dr1["TaxValue"] = z.ToString("#,0.00");
                    TextBox txtTotalAmount = (TextBox)currentRow.FindControl("txtTotalAmount");
                    dr1["PaidWithTax"] = txtPaidAmount.Text;
                    //lblPaidFee.Text = (Convert.ToDouble(lblPaidFee.Text) + Convert.ToDouble(txtPaidAmount.Text)).ToString();
                    //lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();

                }
                dt3.Rows[s].Delete();
                dt3.Rows.InsertAt(dr1, s);
                k++;
                //}
                ViewState["DT3"] = dt3;
                gvBalancePayment.DataSource = dt3;
                gvBalancePayment.DataBind();

                foreach (GridViewRow row3 in gvBalancePayment.Rows)
                {
                    TextBox tb = (TextBox)row3.FindControl("txtTotalAmount");
                    string someVariableName = tb.Text; // get the value from TextBox
                    c = c + Convert.ToDouble(tb.Text); //storing total qty into variable 
                    lblPaidFee.Text = c.ToString();
                }
                lblBalance.Text = (Convert.ToDouble(lblTotalFeeDue.Text) - Convert.ToDouble(lblPaidFee.Text)).ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void gvBalancePayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                dt3 = (DataTable)ViewState["DT3"];

                double paid = 0, paid_with_Tax = 0, bal = 0, k = 0;

                if (dt3.Rows[index][7].ToString() != "")
                {
                    paid = Convert.ToDouble(dt3.Rows[index][7].ToString());
                }
                if (dt3.Rows[index][11].ToString() != "")
                {
                    paid_with_Tax = Convert.ToDouble(dt3.Rows[index][11].ToString());
                }

                bal = Convert.ToDouble(paid_with_Tax - paid);
                k = Convert.ToDouble(lblTotalFeeDue.Text) - bal;
                lblTotalFeeDue.Text = k.ToString();
                lblPaidFee.Text = Convert.ToString(Convert.ToDouble(lblPaidFee.Text) - paid_with_Tax);
                lblBalance.Text = Convert.ToString(Convert.ToDouble(lblBalance.Text) + paid);

                dt3.Rows[index].Delete();
                ViewState["DT3"] = dt3;
                gvBalancePayment.DataSource = dt3;
                gvBalancePayment.DataBind();
                if (gvBalancePayment.Rows.Count > 0)
                {
                    addReceipt.Enabled = false;
                    addReceipt.Visible = false;
                }
                else
                {
                    addReceipt.Enabled = true;
                    addReceipt.Visible = true;
                }
                if (dt3.Rows.Count == 0)
                {
                    lblBalance.Text = "0";
                    lblPaidFee.Text = "0";
                    lblTotalFeeDue.Text = "0";
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            AllClear();

            rbtnSingle.Checked = true;
            Get_MemberID1();
            ReceiptID();
            ViewState["DT"] = null;
            ViewState["DT3"] = null;
            rbtnSingle.Checked = true;
            rbtnCouple.Checked = false;
            rbtnGroup.Checked = false;
            Single();
        }

        public void BindInstructor()
        {

            foreach (GridViewRow row in GvPakageAssign.Rows)
            {
                DataTable dtinstr = new DataTable();
                dtinstr = cour.Get_Instructor();
                DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                ddlInstru.DataSource = dtinstr;
                ddlInstru.DataTextField = "FullName";
                ddlInstru.DataValueField = "Staff_AutoID";
                ddlInstru.DataBind();

            }
        }
        string Receiptid;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtFirstName1.Text != "")
                {
                    if (GvPakageAssign.Rows.Count > 0)
                    {
                        if (gvBalancePayment.Rows.Count > 0)
                        {
                            if (btnSave.Text == "Save")
                            {
                                if (txtReceiptid.Text != "")
                                {
                                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                                    cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                                    int recei = cour.ReceiptIDExist_OR_Not();
                                    if (recei > 0)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Receipt ID Allready Exists !!!','Error');", true);
                                        txtReceiptid.Text = "";
                                        ReceiptID();
                                    }
                                    else
                                    {
                                        int k = save_submember();
                                        if (k >= 0)
                                        {
                                            int cour1 = SaveAssign_Package();
                                            if (cour1 > 0)
                                            {
                                                int balres = Save_BalancePayment();
                                                if (balres > 0)
                                                {
                                                    int bal_Details = Save_Balance_Details();
                                                    if (bal_Details > 0)
                                                    {
                                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Save Successfully !!!','Success');", true);
                                                        SendSMSNew();
                                                        AllClear();
                                                        // Get_MemberID1();
                                                        // ReceiptID();
                                                        BindSearch_gridview();
                                                        BindGridview_1();
                                                        rbtnSingle.Checked = true;
                                                        Single();
                                                        //bindDDLExecutive();
                                                        //setExecutive();

                                                        int Receipt_No = Convert.ToInt32(txtReceiptid.Text);
                                                        string strPopup = "<script language='javascript' ID='script1'>"
                                                        + "window.open('Receipt.aspx?Receipt_No=" + Receipt_No
                                                        + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
                                                        + "</script>";
                                                        ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                                                        Get_MemberID1();
                                                        ReceiptID();
                                                        ViewState["DT"] = null;
                                                        ViewState["DT3"] = null;
                                                        rbtnSingle.Checked = true;
                                                        rbtnCouple.Checked = false;
                                                        rbtnGroup.Checked = false;
                                                        Single();
                                                        if (Request.QueryString["FNameMemDetails"] != null)
                                                        {
                                                            int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                                                            Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                                                        }

                                                    }
                                                    else
                                                    {
                                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Contact Number !!!','Error');", true);
                                                        txtContact1.Focus();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Contact Number !!!','Error');", true);
                                                txtContact1.Focus();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {

                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                    }
                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                    }
                                }

                                if (Sdate != null && Edate != null)
                                {
                                    int cour2 = SaveAssign_Package();
                                    if (cour2 > 0)
                                    {
                                        int balres = Save_BalancePayment();
                                        if (balres > 0)
                                        {
                                            int bal_Details = Save_Balance_Details();
                                            if (bal_Details > 0)
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                                                AllClear();
                                                Divcourse.Visible = true;
                                                Div_paymode.Visible = true;
                                                btnSave.Text = "Save";
                                                txtReceiptid.Text = "";
                                                ReceiptID();
                                                dt1.Clear();
                                                dt3.Clear();
                                                ViewState["DT"] = null;
                                                ViewState["DT3"] = null;
                                                BindSearch_gridview();
                                                rbtnSingle.Checked = true;
                                                Single();
                                                divFormDetails.Visible = false;
                                                divsearch.Visible = true;

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Start Date and End Date Should Not Be Blank  !!!','Error');", true);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Payment Mode  !!!','Error');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Course !!!','Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Member Details !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        int course = 0;
        DateTime S_Date, E_Date;
        string Sdate, Edate;
        public int SaveAssign_Package()
        {
            try
            {
                //Convert.ToInt32(Lblmemeber_Auto.Text);
                cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                if (btnSave.Text == "Save")
                {
                    cour.Action = "Insert_AssignCourse";
                }
                else
                {
                    cour.Action = "Update_AssignCourse";
                }

                cour.Status = "Active";
                dt = cour.Get_Edit_MemberType();
                if (dt.Rows.Count > 0)
                {
                    cour.MemberType = dt.Rows[0][0].ToString();
                }
                else
                {
                    cour.MemberType = "New";
                }
                
                if (GvPakageAssign.Rows.Count > 0)
                {

                    if (rbtnSingle.Checked == true)
                    {
                        if (txtContact1.Text != "")
                        {
                            foreach (GridViewRow row in GvPakageAssign.Rows)
                            {
                                cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                cour.Member_ID1 = Convert.ToInt32(txtId1.Text);

                                TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                {
                                    Sdate = S_Date.ToString("yyyy-MM-dd");
                                    cour.StartDate = Convert.ToDateTime(Sdate);
                                }

                                TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                {
                                    Edate = E_Date.ToString("yyyy-MM-dd");
                                    cour.EndDate = Convert.ToDateTime(Edate);
                                }

                                TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                cour.Total = Convert.ToInt32(txtTotal.Text);

                                TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                cour.Discount = Convert.ToDouble(txtDisc.Text);

                                TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);
                                cour.CourseMemberType = "Single";

                                TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                cour.DiscReason = txtDiscreason.Text;

                                course = cour.Insert_AssignPackage();

                                //cour.Contact = txtContact1.Text;
                                //cnt1 = cour.ContactExist_OR_Not();
                                //if (cnt1 == 0)
                                //{
                                //    course = cour.Insert_AssignPackage();
                                //}
                                //else
                                //{
                                //   // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Enter Contact Number !!!','Error');", true);
                                //}
                            }
                        }
                    }

                    if (rbtnCouple.Checked == true)
                    {
                        cour.CourseMemberType = "Couple";
                        if (txtContact1.Text != "" && txtContact2.Text != "")
                        {
                            foreach (GridViewRow row in GvPakageAssign.Rows)
                            {
                                cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                cour.Member_ID1 = Convert.ToInt32(txtId1.Text);

                                TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                {
                                    Sdate = S_Date.ToString("yyyy-MM-dd");
                                    cour.StartDate = Convert.ToDateTime(Sdate);
                                }

                                TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                {
                                    Edate = E_Date.ToString("yyyy-MM-dd");
                                    cour.EndDate = Convert.ToDateTime(Edate);
                                }

                                TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                cour.Total = Convert.ToInt32(txtTotal.Text);

                                TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                cour.Discount = Convert.ToDouble(txtDisc.Text);

                                TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                cour.DiscReason = txtDiscreason.Text;
                                course = cour.Insert_AssignPackage();
                            }

                            foreach (GridViewRow row in GvPakageAssign.Rows)
                            {
                                cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                cour.Member_ID1 = Convert.ToInt32(txtId2.Text);

                                TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                {
                                    Sdate = S_Date.ToString("yyyy-MM-dd");
                                    cour.StartDate = Convert.ToDateTime(Sdate);
                                }

                                TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                {
                                    Edate = E_Date.ToString("yyyy-MM-dd");
                                    cour.EndDate = Convert.ToDateTime(Edate);
                                }

                                TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                cour.Total = Convert.ToInt32(txtTotal.Text);

                                TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                cour.Discount = Convert.ToDouble(txtDisc.Text);

                                TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);



                                TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                cour.DiscReason = txtDiscreason.Text;
                                course = cour.Insert_AssignPackage();
                            }
                        }

                    }

                    if (rbtnGroup.Checked == true)
                    {
                        cour.CourseMemberType = "Group";
                        if (txtnumber.Text == "1")
                        {
                            if (txtContact1.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);

                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);



                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "2")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);

                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);

                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "3")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }
                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "4")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "" && txtConatct4.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "5")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "" && txtConatct4.Text != "" && txtContact5.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "6")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "" && txtConatct4.Text != "" && txtContact5.Text != "" && txtContact6.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "7")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "" && txtConatct4.Text != "" && txtContact5.Text != "" && txtContact6.Text != "" && txtContact7.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "8")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "" && txtConatct4.Text != "" && txtContact5.Text != "" && txtContact6.Text != ""
                                && txtContact7.Text != "" && txtContact8.Text != "")
                            {

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId8.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "9")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "" && txtConatct4.Text != "" && txtContact5.Text != "" && txtContact6.Text != ""
                                && txtContact7.Text != "" && txtContact8.Text != "" && txtConatct9.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId8.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId9.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                        if (txtnumber.Text == "10")
                        {
                            if (txtContact1.Text != "" && txtContact2.Text != "" && txtContact3.Text != "" && txtConatct4.Text != "" && txtContact5.Text != "" && txtContact6.Text != ""
                               && txtContact7.Text != "" && txtContact8.Text != "" && txtConatct9.Text != "" && txtConatct10.Text != "")
                            {
                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId8.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId9.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }

                                foreach (GridViewRow row in GvPakageAssign.Rows)
                                {
                                    cour.Pack_AutoID = Convert.ToInt32(row.Cells[1].Text);
                                    cour.Member_ID1 = Convert.ToInt32(txtId10.Text);
                                    TextBox txtsDate = (TextBox)row.FindControl("txtsDate");
                                    // cour.StartDate = Convert.ToDateTime(txtsDate.Text);

                                    if (DateTime.TryParseExact(txtsDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out S_Date))
                                    {
                                        Sdate = S_Date.ToString("yyyy-MM-dd");
                                        cour.StartDate = Convert.ToDateTime(Sdate);
                                    }

                                    TextBox TxtEndDate = (TextBox)row.FindControl("txtEndate");
                                    // cour.EndDate = Convert.ToDateTime(TxtEndDate.Text);

                                    if (DateTime.TryParseExact(TxtEndDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out E_Date))
                                    {
                                        Edate = E_Date.ToString("yyyy-MM-dd");
                                        cour.EndDate = Convert.ToDateTime(Edate);
                                    }

                                    TextBox TxtAmount = (TextBox)row.FindControl("txtAmt");
                                    cour.Amount = Convert.ToDouble(TxtAmount.Text);

                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    cour.Qty = Convert.ToInt32(txtQuantity.Text);

                                    TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
                                    cour.Total = Convert.ToInt32(txtTotal.Text);

                                    TextBox txtDisc = (TextBox)row.FindControl("txtDisc");
                                    cour.Discount = Convert.ToDouble(txtDisc.Text);

                                    TextBox txtFinalTotal = (TextBox)row.FindControl("txtFinalTotal");
                                    cour.FinalTotal = Convert.ToDouble(txtFinalTotal.Text);

                                    //DropDownList ddlInstru = (DropDownList)row.FindControl("ddlInstru");
                                    //cour.Staff_AutoID = ;// Convert.ToInt32(ddlInstru.SelectedValue);

                                    TextBox txtDiscreason = (TextBox)row.FindControl("txtDiscreason");
                                    cour.DiscReason = txtDiscreason.Text;
                                    course = cour.Insert_AssignPackage();
                                }
                            }
                        }

                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            return course;
        }

        int Bal = 0;
        public int Save_BalancePayment()
        {
            try
            {
                //Convert.ToInt32(Lblmemeber_Auto.Text);
                cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                cour.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

                if (btnSave.Text == "Save")
                {
                    cour.Action = "Insert_BalancePayment";
                }
                else
                {
                    cour.Action = "Update_BalancePayment";
                }
                dt = cour.Get_Edit_MemberType();
                if (dt.Rows.Count > 0)
                {
                    cour.Status = dt.Rows[0][0].ToString();
                }
                else
                {
                    cour.Status = "New";
                }
               
                if (rbtnSingle.Checked == true)
                {
                    cour.CourseMemberType = "Single";
                }
                if (rbtnCouple.Checked == true)
                {
                    cour.CourseMemberType = "Couple";
                }
                if (rbtnGroup.Checked == true)
                {
                    cour.CourseMemberType = "Group";
                }
                cour.Member_ID1 = Convert.ToInt32(txtId1.Text);
                if (gvBalancePayment.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvBalancePayment.Rows)
                    {
                        TextBox txtPaymentMode = (TextBox)row.FindControl("txtPaymentMode");
                        cour.PaymentMode = txtPaymentMode.Text;


                        TextBox txtDate = (TextBox)row.FindControl("txtDate");
                        // cour.payDate = Convert.ToDateTime(txtDate.Text);

                        DateTime txtDate1;
                        if (DateTime.TryParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out txtDate1))
                        {
                            string txtDate11 = txtDate1.ToString("yyyy-MM-dd");
                            cour.payDate = Convert.ToDateTime(txtDate11);
                        }


                        if (txtPaymentMode.Text == "Cash")
                        {
                            TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                            cour.Cardno = "";// txtNumber.Text;

                            TextBox txtExpiryDate = (TextBox)row.FindControl("txtExpiryDate");

                            cour.CardExpirydate = null;// Convert.ToDateTime(txtExpiryDate.Text);
                            TextBox txtBankName = (TextBox)row.FindControl("txtBankName");
                            cour.BankName = "";//txtBankName.Text;
                        }
                        else if (txtPaymentMode.Text == "NEFT")
                        {
                            cour.CardExpirydate = null;

                        }
                        else if (txtPaymentMode.Text == "RTGS")
                        {
                            cour.CardExpirydate = null;
                        }
                        else
                        {
                            TextBox txtExpiryDate = (TextBox)row.FindControl("txtExpiryDate");
                            //cour.CardExpirydate = Convert.ToDateTime(txtExpiryDate.Text);

                            DateTime txtExpiryDate1;
                            if (DateTime.TryParseExact(txtExpiryDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out txtExpiryDate1))
                            {
                                string txtExpiryDate11 = txtExpiryDate1.ToString("yyyy-MM-dd");
                                cour.CardExpirydate = Convert.ToDateTime(txtExpiryDate11);
                            }

                            TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                            cour.Cardno = txtNumber.Text;

                            TextBox txtBankName = (TextBox)row.FindControl("txtBankName");
                            cour.BankName = txtBankName.Text;
                        }


                        TextBox txtBranchName = (TextBox)row.FindControl("txtBranchName");
                        cour.BranchName = txtBranchName.Text;

                        TextBox txtPaidAmount = (TextBox)row.FindControl("txtPaidAmount");
                        if (txtPaidAmount.Text != "")
                        {
                            cour.Paid = Convert.ToDouble(txtPaidAmount.Text);
                        }
                        else
                        {
                            // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Paid Amount !!!','Error');", true);
                            cour.Paid = 0;
                        }
                        TextBox ddlTax = (TextBox)row.FindControl("ddlTax");
                        cour.TaxType = ddlTax.Text;

                        TextBox txtTax = (TextBox)row.FindControl("txtTax");
                        if (txtTax.Text != "")
                        {
                            cour.taxpec = Convert.ToDouble(txtTax.Text);
                        }
                        else
                        {
                            // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Paid Amount !!!','Error');", true);
                            cour.taxpec = 0;
                        }

                        TextBox Txtvalue = (TextBox)row.FindControl("Txtvalue");

                        if (Txtvalue.Text != "")
                        {
                            cour.TaxValue = Convert.ToDouble(Txtvalue.Text);
                        }
                        else
                        {
                            cour.TaxValue = 0;
                        }

                        TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");

                        if (txtTotalAmount.Text != "")
                        {
                            cour.PaidWithTax = Convert.ToDouble(txtTotalAmount.Text);
                        }
                        else
                        {
                            cour.PaidWithTax = 0;
                        }

                        Bal = cour.Insert_BalancePayment();

                        //cour.Contact = txtContact1.Text;
                        //cnt1 = cour.ContactExist_OR_Not();
                        //if (cnt1 == 0)
                        //{
                        //    Bal = cour.Insert_BalancePayment();
                        //}
                        //else
                        //{
                        //    // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Enter Contact Number !!!','Error');", true);
                        //}

                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            return Bal;
        }

        public void AllClear()
        {
            try
            {
                btnSave.Text = "Save";
                //BindGridview();
                dt1.Clear();
                GVCourseDetails.DataSource = null;
                GVCourseDetails.DataBind();
                gvBalancePayment.DataSource = null;
                gvBalancePayment.DataBind();
                dt3.Clear();
                GvPakageAssign.DataSource = null;
                GvPakageAssign.DataBind();
                txtComment.Text = "";
                lblBalance.Text = "0";
                lblTotalFee.Text = "0";
                lblTotalFeeDue.Text = "0";

                txtId1.Text = "";
                txtLastName1.Text = "";
                txtFirstName1.Text = "";
                txtContact1.Text = "";
                txtEmail1.Text = "";
                ddlGender1.SelectedIndex = 0;

                txtId2.Text = "";
                txtLastName2.Text = "";
                txtFirstName2.Text = "";
                txtContact2.Text = "";
                txtEmail2.Text = "";
                ddlGender2.SelectedIndex = 0;

                txtId3.Text = "";
                txtLastName3.Text = "";
                txtFirstName3.Text = "";
                txtContact3.Text = "";
                txtEmail3.Text = "";
                ddlGende3.SelectedIndex = 0;

                txtId4.Text = "";
                txtLastName4.Text = "";
                txtFirstName4.Text = "";
                txtConatct4.Text = "";
                txtEmail4.Text = "";
                ddlGender4.SelectedIndex = 0;

                txtId5.Text = "";
                txtLastName5.Text = "";
                txtFirstName5.Text = "";
                txtContact5.Text = "";
                txtEmail5.Text = "";
                ddlGender5.SelectedIndex = 0;

                txtId6.Text = "";
                txtLastName6.Text = "";
                txtFirstName6.Text = "";
                txtContact6.Text = "";
                txtEmail6.Text = "";
                ddlGender6.SelectedIndex = 0;

                txtId7.Text = "";
                txtLastName7.Text = "";
                txtFirstName7.Text = "";
                txtContact7.Text = "";
                txtEmail7.Text = "";
                ddlGender7.SelectedIndex = 0;

                txtId7.Text = "";
                txtLastName7.Text = "";
                txtFirstName7.Text = "";
                txtContact7.Text = "";
                txtEmail7.Text = "";
                ddlGender7.SelectedIndex = 0;

                txtId8.Text = "";
                txtLastName8.Text = "";
                txtFirstName8.Text = "";
                txtContact8.Text = "";
                txtEmail8.Text = "";
                ddlGender8.SelectedIndex = 0;

                txtId9.Text = "";
                txtLastName9.Text = "";
                txtFirstName9.Text = "";
                txtConatct9.Text = "";
                txtEmail9.Text = "";
                ddlGender9.SelectedIndex = 0;

                txtId10.Text = "";
                txtLastName10.Text = "";
                txtFirstName10.Text = "";
                txtConatct10.Text = "";
                txtEmail10.Text = "";
                ddlGender10.SelectedIndex = 0;

                lblPaidFee.Text = "0";
                Bind_PaymentType();
                ddlPayMode.SelectedIndex = 0;
                DropDownList2.SelectedIndex = 0;
                Get_MemberID1();
                NextfDate();

                DropDownList3.SelectedValue = "--Select--";
                TextBox1.Text = "";
                TextBox1.Enabled = false;
                chkExecutive.Checked = true;
                ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutive.Enabled = false;

                rbtnCouple.Enabled = true;
                rbtnGroup.Enabled = true;

                txtFirstName1.Enabled = true;
                txtLastName1.Enabled = true;
                ddlGender1.Enabled = true;
                txtContact1.Enabled = true;
                txtEmail1.Enabled = true;
                RemoveQueryStringParams("Contact1");
                txtReceiptid.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        int Bal_Details = 0;
        public int Save_Balance_Details()
        {
            try
            {
                cour.Member_AutoID = 0;// Convert.ToInt32(Lblmemeber_Auto.Text);
                cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                cour.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

                if (btnSave.Text == "Save")
                {
                    cour.Action = "Insert_BalanceDetails";
                }
                else
                {
                    cour.Action = "Update_BalanceDetails";
                }

                if (rbtnSingle.Checked == true)
                {
                    cour.CourseMemberType = "Single";
                }
                if (rbtnCouple.Checked == true)
                {
                    cour.CourseMemberType = "Couple";
                }
                if (rbtnGroup.Checked == true)
                {
                    cour.CourseMemberType = "Group";
                }
                cour.Member_ID1 = Convert.ToInt32(txtId1.Text);

                dt = cour.Get_Edit_MemberType();
                if (dt.Rows.Count > 0)
                {
                    cour.Status = dt.Rows[0][0].ToString();
                }
                else
                {
                    cour.Status = "New";
                }
               // cour.Status = "New";
                cour.PaidFee = Convert.ToDouble(lblPaidFee.Text);
                cour.TotalFeeDue = Convert.ToDouble(lblTotalFeeDue.Text);
                cour.Balance = Convert.ToDouble(lblBalance.Text);
                cour.Comment = txtComment.Text;
                // cour.NextBalDate = Convert.ToDateTime(txtNextFollowupDate.Text);
                if (lblBalance.Text != "0")
                {
                    DateTime todaydate;
                    if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                        //cour.NextBalDate = Convert.ToDateTime(todaydate1);
                        cour.NextBalDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                }
                else
                {
                    DateTime todaydate;
                    if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        string todaydate1 = todaydate.ToString("dd-MM-yyyy");
                        //cour.NextBalDate = Convert.ToDateTime(todaydate1);
                        cour.NextBalDate = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                }

                Bal_Details = cour.Insert_Balancedetails();
                //cour.Contact = txtContact1.Text;
                //cnt1 = cour.ContactExist_OR_Not();
                //if (cnt1 == 0)
                //{
                //    Bal_Details = cour.Insert_Balancedetails();
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Enter Contact Number !!!','Error');", true);
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            return Bal_Details;
        }
        #region GetMember_ID
        public void ReceiptID()
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                txtReceiptid.Text = cour.Get_ReceiptID().ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }


        public void Get_MemberID1()
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = cour.Get_MemberID1();
                //txtReceiptid.Text = dt.Rows[0]["Member_ID1"].ToString();
                if (dt.Rows.Count > 0)
                {
                    if (rbtnSingle.Checked == true)
                    {
                        txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                    }
                    if (rbtnCouple.Checked == true)
                    {
                        txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                        int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                        txtId2.Text = Convert.ToString(id + 1);
                    }
                    if (rbtnGroup.Checked == true)
                    {
                        txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                        int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                        txtId2.Text = Convert.ToString(id + 1);
                        txtId3.Text = Convert.ToString(id + 2);
                        txtId4.Text = Convert.ToString(id + 3);
                        txtId5.Text = Convert.ToString(id + 4);
                        txtId6.Text = Convert.ToString(id + 5);
                        txtId7.Text = Convert.ToString(id + 6);
                        txtId8.Text = Convert.ToString(id + 7);
                        txtId9.Text = Convert.ToString(id + 8);
                        txtId10.Text = Convert.ToString(id + 9);
                    }

                }
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
                    //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void setExecutive()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            dt = obBalStaffRegistration.GetExecutiveByID_ByBranch();
            if (dt.Rows.Count > 0)
            {
                ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutive.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
            }
            else
            {
                dt = obBalStaffRegistration.GetExecutiveByID_WithoutBranch();
                string staffid = dt.Rows[0]["Staff_AutoID"].ToString();
                string staffnm = dt.Rows[0]["Name"].ToString();
                ddlExecutive.Items.Insert(0, new ListItem(staffnm, staffid));
                ddlExecutive.SelectedItem.Text = staffnm;
                ddlExecutive.SelectedValue = staffid;
            }
        }
        DataTable dt_sub = new DataTable();
        public void Get_SubMemberID1()
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                // dt = objMemberDetails.Get_MemberID1();
                dt_sub = objMemberDetails.Get_MemberID1();
                if (dt_sub.Rows.Count > 0)
                {
                    if (rbtnSingle.Checked == true)
                    {
                        txtId1.Text = dt_sub.Rows[0]["Member_ID1"].ToString();
                    }
                    if (rbtnCouple.Checked == true)
                    {
                        txtId1.Text = dt_sub.Rows[0]["Member_ID1"].ToString();
                        int id = Convert.ToInt32(dt_sub.Rows[0]["Member_ID1"].ToString());
                        txtId2.Text = Convert.ToString(id + 1);
                    }
                    if (rbtnGroup.Checked == true)
                    {
                        txtId1.Text = dt_sub.Rows[0]["Member_ID1"].ToString();
                        int id = Convert.ToInt32(dt_sub.Rows[0]["Member_ID1"].ToString());
                        txtId2.Text = Convert.ToString(id + 1);
                        txtId3.Text = Convert.ToString(id + 2);
                        txtId4.Text = Convert.ToString(id + 3);
                        txtId5.Text = Convert.ToString(id + 4);
                        txtId6.Text = Convert.ToString(id + 5);
                        txtId7.Text = Convert.ToString(id + 6);
                        txtId8.Text = Convert.ToString(id + 7);
                        txtId9.Text = Convert.ToString(id + 8);
                        txtId10.Text = Convert.ToString(id + 9);
                    }

                }
                // txtReceiptid.Text = dt_sub.Rows[0]["MemberSub_ID1"].ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        int res = 0;
        int cnt1;
        public int save_submember()
        {
            try
            {

                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                objMemberDetails.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
                //  objMemberDetails.ReceiptID = 0;//Convert.ToInt32(txtReceiptid.Text);              

                DateTime Regdate;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Regdate))
                {
                    string Regdate1 = Regdate.ToString("dd-MM-yyyy");
                    objMemberDetails.RegDate = DateTime.ParseExact(Regdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                objMemberDetails.Status = "Active";

                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMemberDetails.SMSStatus = "Yes";


                if (rbtnSingle.Checked == true)
                {
                    cour.Contact = txtContact1.Text;
                    cnt1 = cour.ContactExist_OR_Not();
                    if (cnt1 == 0)
                    {
                        if (txtContact1.Text != "")
                        {
                            objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                            objMemberDetails.FName = txtFirstName1.Text;
                            objMemberDetails.LName = txtLastName1.Text;
                            objMemberDetails.Gender = ddlGender1.Text;
                            objMemberDetails.Contact1 = txtContact1.Text;
                            objMemberDetails.Email = txtEmail1.Text;

                            objMemberDetails.MembershipStatus = "Group_Owner";

                            if (btnSave.Text == "Save")
                            {
                                objMemberDetails.Action = "Insert";
                            }
                            else
                            {
                                objMemberDetails.Action = "Update";
                            }
                            res = objMemberDetails.Insert_MemberDetails();
                        }
                    }
                }
                if (rbtnCouple.Checked == true)
                {
                    cour.Contact = txtContact1.Text;
                    cnt1 = cour.ContactExist_OR_Not();
                    if (cnt1 == 0)
                    {
                        if (txtContact1.Text != "")
                        {
                            objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                            objMemberDetails.FName = txtFirstName1.Text;
                            objMemberDetails.LName = txtLastName1.Text;
                            objMemberDetails.Gender = ddlGender1.Text;
                            objMemberDetails.Contact1 = txtContact1.Text;
                            objMemberDetails.Email = txtEmail1.Text;
                            objMemberDetails.MembershipStatus = "Group_Owner";

                            if (btnSave.Text == "Save")
                            {
                                objMemberDetails.Action = "Insert";
                            }
                            else
                            {
                                objMemberDetails.Action = "Update";
                            }
                            res = objMemberDetails.Insert_MemberDetails();
                        }
                    }
                    cour.Contact = txtContact2.Text;
                    cnt1 = cour.ContactExist_OR_Not();
                    if (cnt1 == 0)
                    {
                        if (txtContact2.Text != "")
                        {
                            objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                            objMemberDetails.FName = txtFirstName2.Text;
                            objMemberDetails.LName = txtLastName2.Text;
                            objMemberDetails.Gender = ddlGender2.Text;
                            objMemberDetails.Contact1 = txtContact2.Text;
                            objMemberDetails.Email = txtEmail2.Text;

                            objMemberDetails.MembershipStatus = "Member";

                            if (btnSave.Text == "Save")
                            {
                                objMemberDetails.Action = "Insert";
                            }
                            else
                            {
                                objMemberDetails.Action = "Update";
                            }
                            res = objMemberDetails.Insert_MemberDetails();
                        }
                    }
                }
                if (rbtnGroup.Checked == true)
                {


                    if (txtnumber.Text == "1")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;

                                objMemberDetails.MembershipStatus = "Group_Owner";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }
                    if (txtnumber.Text == "2")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;
                                objMemberDetails.MembershipStatus = "Group_Owner";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;

                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }

                    if (txtnumber.Text == "3")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;

                                objMemberDetails.MembershipStatus = "Group_Owner";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;

                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {

                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);

                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;
                                objMemberDetails.Email = txtEmail3.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }


                    if (txtnumber.Text == "4")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;

                                objMemberDetails.MembershipStatus = "Group_Owner";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;

                                objMemberDetails.MembershipStatus = "Member";


                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;

                                objMemberDetails.Email = txtEmail3.Text;
                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct4.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                objMemberDetails.FName = txtFirstName4.Text;
                                objMemberDetails.LName = txtLastName4.Text;
                                objMemberDetails.Gender = ddlGender4.Text;
                                objMemberDetails.Contact1 = txtConatct4.Text;
                                objMemberDetails.Email = txtEmail4.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }
                    if (txtnumber.Text == "5")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;

                                objMemberDetails.MembershipStatus = "Group_Owner";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {

                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;
                                objMemberDetails.Email = txtEmail3.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }


                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {

                            if (txtConatct4.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                objMemberDetails.FName = txtFirstName4.Text;
                                objMemberDetails.LName = txtLastName4.Text;
                                objMemberDetails.Gender = ddlGender4.Text;
                                objMemberDetails.Contact1 = txtConatct4.Text;
                                objMemberDetails.Email = txtEmail4.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact5.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                objMemberDetails.FName = txtFirstName5.Text;
                                objMemberDetails.LName = txtLastName5.Text;
                                objMemberDetails.Gender = ddlGender5.Text;
                                objMemberDetails.Contact1 = txtContact5.Text;
                                objMemberDetails.Email = txtEmail5.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }
                    if (txtnumber.Text == "6")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }


                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;
                                objMemberDetails.Email = txtEmail3.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                return res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct4.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                objMemberDetails.FName = txtFirstName4.Text;
                                objMemberDetails.LName = txtLastName4.Text;
                                objMemberDetails.Gender = ddlGender4.Text;
                                objMemberDetails.Contact1 = txtConatct4.Text;
                                objMemberDetails.Email = txtEmail4.Text;

                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact5.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                objMemberDetails.FName = txtFirstName5.Text;
                                objMemberDetails.LName = txtLastName5.Text;
                                objMemberDetails.Gender = ddlGender5.Text;
                                objMemberDetails.Contact1 = txtContact5.Text;
                                objMemberDetails.Email = txtEmail5.Text;

                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {

                            if (txtContact6.Text != "")
                            {

                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                objMemberDetails.FName = txtFirstName6.Text;
                                objMemberDetails.LName = txtLastName6.Text;
                                objMemberDetails.Gender = ddlGender6.Text;
                                objMemberDetails.Contact1 = txtContact6.Text;
                                objMemberDetails.Email = txtEmail6.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }

                    if (txtnumber.Text == "7")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;

                                objMemberDetails.MembershipStatus = "Group_Owner";


                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;

                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {

                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;
                                objMemberDetails.Email = txtEmail3.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct4.Text != "")
                            {

                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                objMemberDetails.FName = txtFirstName4.Text;
                                objMemberDetails.LName = txtLastName4.Text;
                                objMemberDetails.Gender = ddlGender4.Text;
                                objMemberDetails.Contact1 = txtConatct4.Text;
                                objMemberDetails.Email = txtEmail4.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }


                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact5.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                objMemberDetails.FName = txtFirstName5.Text;
                                objMemberDetails.LName = txtLastName5.Text;
                                objMemberDetails.Gender = ddlGender5.Text;
                                objMemberDetails.Contact1 = txtContact5.Text;
                                objMemberDetails.Email = txtEmail5.Text;

                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();

                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {

                            if (txtContact6.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                objMemberDetails.FName = txtFirstName6.Text;
                                objMemberDetails.LName = txtLastName6.Text;
                                objMemberDetails.Gender = ddlGender6.Text;
                                objMemberDetails.Contact1 = txtContact6.Text;
                                objMemberDetails.Email = txtEmail6.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact7.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                objMemberDetails.FName = txtFirstName7.Text;
                                objMemberDetails.LName = txtLastName7.Text;
                                objMemberDetails.Gender = ddlGender7.Text;
                                objMemberDetails.Contact1 = txtContact7.Text;
                                objMemberDetails.Email = txtEmail7.Text;

                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();

                            }
                        }
                    }
                    if (txtnumber.Text == "8")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {

                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;

                                objMemberDetails.MembershipStatus = "Group_Owner";


                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;

                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;
                                objMemberDetails.Email = txtEmail3.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct4.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                objMemberDetails.FName = txtFirstName4.Text;
                                objMemberDetails.LName = txtLastName4.Text;
                                objMemberDetails.Gender = ddlGender4.Text;
                                objMemberDetails.Contact1 = txtConatct4.Text;
                                objMemberDetails.Email = txtEmail4.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }


                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact5.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                objMemberDetails.FName = txtFirstName5.Text;
                                objMemberDetails.LName = txtLastName5.Text;
                                objMemberDetails.Gender = ddlGender5.Text;
                                objMemberDetails.Contact1 = txtContact5.Text;
                                objMemberDetails.Email = txtEmail5.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();

                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact6.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                objMemberDetails.FName = txtFirstName6.Text;
                                objMemberDetails.LName = txtLastName6.Text;
                                objMemberDetails.Gender = ddlGender6.Text;
                                objMemberDetails.Contact1 = txtContact6.Text;
                                objMemberDetails.Email = txtEmail6.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact7.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                objMemberDetails.FName = txtFirstName7.Text;
                                objMemberDetails.LName = txtLastName7.Text;
                                objMemberDetails.Gender = ddlGender7.Text;
                                objMemberDetails.Contact1 = txtContact7.Text;
                                objMemberDetails.Email = txtEmail7.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact8.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact8.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId8.Text);
                                objMemberDetails.FName = txtFirstName8.Text;
                                objMemberDetails.LName = txtLastName8.Text;
                                objMemberDetails.Gender = ddlGender8.Text;
                                objMemberDetails.Contact1 = txtContact8.Text;
                                objMemberDetails.Email = txtEmail8.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();

                            }
                        }
                    }
                    if (txtnumber.Text == "9")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {

                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;

                                objMemberDetails.MembershipStatus = "Group_Owner";


                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;


                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;
                                objMemberDetails.Email = txtEmail3.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct4.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                objMemberDetails.FName = txtFirstName4.Text;
                                objMemberDetails.LName = txtLastName4.Text;
                                objMemberDetails.Gender = ddlGender4.Text;
                                objMemberDetails.Contact1 = txtConatct4.Text;
                                objMemberDetails.Email = txtEmail4.Text;

                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact5.Text != "")
                            {

                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                objMemberDetails.FName = txtFirstName5.Text;
                                objMemberDetails.LName = txtLastName5.Text;
                                objMemberDetails.Gender = ddlGender5.Text;
                                objMemberDetails.Contact1 = txtContact5.Text;
                                objMemberDetails.Email = txtEmail5.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact6.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                objMemberDetails.FName = txtFirstName6.Text;
                                objMemberDetails.LName = txtLastName6.Text;
                                objMemberDetails.Gender = ddlGender6.Text;
                                objMemberDetails.Contact1 = txtContact6.Text;
                                objMemberDetails.Email = txtEmail6.Text;

                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact7.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                objMemberDetails.FName = txtFirstName7.Text;
                                objMemberDetails.LName = txtLastName7.Text;
                                objMemberDetails.Gender = ddlGender7.Text;
                                objMemberDetails.Contact1 = txtContact7.Text;
                                objMemberDetails.Email = txtEmail7.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact8.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact8.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId8.Text);
                                objMemberDetails.FName = txtFirstName8.Text;
                                objMemberDetails.LName = txtLastName8.Text;
                                objMemberDetails.Gender = ddlGender8.Text;
                                objMemberDetails.Contact1 = txtContact8.Text;
                                objMemberDetails.Email = txtEmail8.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct9.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct9.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId9.Text);
                                objMemberDetails.FName = txtFirstName9.Text;
                                objMemberDetails.LName = txtLastName9.Text;
                                objMemberDetails.Gender = ddlGender9.Text;
                                objMemberDetails.Contact1 = txtConatct9.Text;
                                objMemberDetails.Email = txtEmail9.Text;


                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }
                    if (txtnumber.Text == "10")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact1.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId1.Text);
                                objMemberDetails.FName = txtFirstName1.Text;
                                objMemberDetails.LName = txtLastName1.Text;
                                objMemberDetails.Gender = ddlGender1.Text;
                                objMemberDetails.Contact1 = txtContact1.Text;
                                objMemberDetails.Email = txtEmail1.Text;
                                objMemberDetails.MembershipStatus = "Group_Owner";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact2.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId2.Text);
                                objMemberDetails.FName = txtFirstName2.Text;
                                objMemberDetails.LName = txtLastName2.Text;
                                objMemberDetails.Gender = ddlGender2.Text;
                                objMemberDetails.Contact1 = txtContact2.Text;
                                objMemberDetails.Email = txtEmail2.Text;
                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact3.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId3.Text);
                                objMemberDetails.FName = txtFirstName3.Text;
                                objMemberDetails.LName = txtLastName3.Text;
                                objMemberDetails.Gender = ddlGende3.Text;
                                objMemberDetails.Contact1 = txtContact3.Text;
                                objMemberDetails.Email = txtEmail3.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct4.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId4.Text);
                                objMemberDetails.FName = txtFirstName4.Text;
                                objMemberDetails.LName = txtLastName4.Text;
                                objMemberDetails.Gender = ddlGender4.Text;
                                objMemberDetails.Contact1 = txtConatct4.Text;
                                objMemberDetails.Email = txtEmail4.Text;
                                objMemberDetails.MembershipStatus = "Member";

                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact5.Text != "")
                            {

                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId5.Text);
                                objMemberDetails.FName = txtFirstName5.Text;
                                objMemberDetails.LName = txtLastName5.Text;
                                objMemberDetails.Gender = ddlGender5.Text;
                                objMemberDetails.Contact1 = txtContact5.Text;
                                objMemberDetails.Email = txtEmail5.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact6.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId6.Text);
                                objMemberDetails.FName = txtFirstName6.Text;
                                objMemberDetails.LName = txtLastName6.Text;
                                objMemberDetails.Gender = ddlGender6.Text;
                                objMemberDetails.Contact1 = txtContact6.Text;
                                objMemberDetails.Email = txtEmail6.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact7.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId7.Text);
                                objMemberDetails.FName = txtFirstName7.Text;
                                objMemberDetails.LName = txtLastName7.Text;
                                objMemberDetails.Gender = ddlGender7.Text;
                                objMemberDetails.Contact1 = txtContact7.Text;
                                objMemberDetails.Email = txtEmail7.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtContact8.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtContact8.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId8.Text);
                                objMemberDetails.FName = txtFirstName8.Text;
                                objMemberDetails.LName = txtLastName8.Text;
                                objMemberDetails.Gender = ddlGender8.Text;
                                objMemberDetails.Contact1 = txtContact8.Text;
                                objMemberDetails.Email = txtEmail8.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }

                        cour.Contact = txtConatct9.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct9.Text != "")
                            {
                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId9.Text);
                                objMemberDetails.FName = txtFirstName9.Text;
                                objMemberDetails.LName = txtLastName9.Text;
                                objMemberDetails.Gender = ddlGender9.Text;
                                objMemberDetails.Contact1 = txtConatct9.Text;
                                objMemberDetails.Email = txtEmail9.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                        cour.Contact = txtConatct10.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 == 0)
                        {
                            if (txtConatct10.Text != "")
                            {

                                objMemberDetails.Member_ID1 = Convert.ToInt32(txtId10.Text);
                                objMemberDetails.FName = txtFirstName10.Text;
                                objMemberDetails.LName = txtLastName10.Text;
                                objMemberDetails.Gender = ddlGender10.Text;
                                objMemberDetails.Contact1 = txtConatct10.Text;
                                objMemberDetails.Email = txtEmail10.Text;
                                objMemberDetails.MembershipStatus = "Member";
                                if (btnSave.Text == "Save")
                                {
                                    objMemberDetails.Action = "Insert";
                                }
                                else
                                {
                                    objMemberDetails.Action = "Update";
                                }
                                res = objMemberDetails.Insert_MemberDetails();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
            return res;
        }
        #endregion
        protected int AddParameters()
        {
            //int res;
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            objMemberDetails.Member_ID1 = Convert.ToInt32(txtReceiptid.Text);
            DateTime Regdate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Regdate))
            {
                string Regdate1 = Regdate.ToString("dd-MM-yyyy");
                objMemberDetails.RegDate = DateTime.ParseExact(Regdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            objMemberDetails.FName = txtFirstName1.Text;
            objMemberDetails.LName = txtLastName1.Text;
            objMemberDetails.Gender = ddlGender1.Text;
            objMemberDetails.Contact1 = txtContact1.Text;
            objMemberDetails.Email = txtEmail1.Text;

            if (btnSave.Text == "Save")
            {
                objMemberDetails.Action = "Insert";
            }
            else
            {
                objMemberDetails.Action = "Update";
            }
            return res = objMemberDetails.Insert_MemberDetails();


        }
        private void SeacrhAction()
        {
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                TextBox1.Enabled = false;
                cour.Category = "--Select--";
            }
            else if (DropDownList3.SelectedValue.ToString() == "Member ID")
            {
                cour.Category = "Member ID";
                cour.searchTxt = TextBox1.Text;

            }
            else if (DropDownList3.SelectedValue.ToString() == "Receipt ID")
            {
                cour.Category = "Receipt ID";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "First Name")
            {
                cour.Category = "First Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Last Name")
            {
                cour.Category = "Last Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "ContactNo")
            {
                cour.Category = "ContactNo";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Course Name")
            {
                cour.Category = "Course Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Active")
            {
                cour.Category = "Active";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Deactive")
            {
                cour.Category = "Deactive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "New")
            {
                cour.Category = "New";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Renew")
            {
                cour.Category = "Renew";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Upgrade")
            {
                cour.Category = "Upgrade";
                cour.searchTxt = TextBox1.Text;
            }

            else if (DropDownList3.SelectedValue.ToString() == "Executive")
            {
                cour.Category = "Executive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "PaymentMode")
            {
                cour.Category = "PaymentMode";
                cour.searchTxt = TextBox1.Text;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                DropDownList3.Focus();
                return;
            }

        }

        private void SeacrhAction1()
        {
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                TextBox1.Enabled = false;
                cour.Category = "--Select--";
            }
            else if (DropDownList3.SelectedValue.ToString() == "Member ID")
            {
                cour.Category = "Member ID";
                cour.searchTxt = TextBox1.Text;

            }
            else if (DropDownList3.SelectedValue.ToString() == "Receipt ID")
            {
                cour.Category = "Receipt ID";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "First Name")
            {
                cour.Category = "First Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Last Name")
            {
                cour.Category = "Last Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "ContactNo")
            {
                cour.Category = "ContactNo";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Course Name")
            {
                cour.Category = "Course Name";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Active")
            {
                cour.Category = "Active";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Deactive")
            {
                cour.Category = "Deactive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "New")
            {
                cour.Category = "New";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Renew")
            {
                cour.Category = "Renew";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "Upgrade")
            {
                cour.Category = "Upgrade";
                cour.searchTxt = TextBox1.Text;
            }

            else if (DropDownList3.SelectedValue.ToString() == "Executive")
            {
                cour.Category = "Executive";
                cour.searchTxt = TextBox1.Text;
            }
            else if (DropDownList3.SelectedValue.ToString() == "PaymentMode")
            {
                cour.Category = "PaymentMode";
                cour.searchTxt = TextBox1.Text;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                DropDownList3.Focus();
                return;
            }

        }
        public void SearchBYCategory()
        {
            try
            {
                dt.Clear();
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                DateTime payDate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate))
                {
                    string payDate4 = payDate.ToString("dd-MM-yyyy");
                    cour.payDate = DateTime.ParseExact(payDate4, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }

                DateTime payDate2;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate2))
                {
                    string payDate3 = payDate2.ToString("dd-MM-yyyy");
                    cour.payDate1 = DateTime.ParseExact(payDate3, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }

                //DateTime payDate;
                //DateTime payDate1;
                //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate))
                //{
                //    cour.payDate = Convert.ToDateTime(payDate.ToString("dd-MM-yyyy"));

                //}

                //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate1))
                //{
                //    cour.payDate1 = Convert.ToDateTime(payDate1.ToString("dd-MM-yyyy"));

                //}
                SeacrhAction1();
                lblCount.Text = "0";
                dt = cour.SearchByDateWithCategory();
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString(); 
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    lblCount.Text = dt.Rows.Count.ToString(); 
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    GVCourseDetails.Columns[0].Visible = false;
                    GVCourseDetails.Columns[1].Visible = false;
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
                dt.Clear();
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //SeacrhAction();
                cour.Action = "SearchByDate";

                DateTime payDate;
                if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate))
                {
                    string payDate1 = payDate.ToString("dd-MM-yyyy");
                    cour.payDate = DateTime.ParseExact(payDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                DateTime payDate2;
                if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate2))
                {
                    string payDate3 = payDate2.ToString("dd-MM-yyyy");
                    cour.payDate1 = DateTime.ParseExact(payDate3, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
                lblCount.Text = "0";
                dt = cour.SearchByDate();
                if (dt.Rows.Count > 0)
                {
                   
                    lblCount.Text = dt.Rows.Count.ToString();
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    GVCourseDetails.Columns[0].Visible = false;
                    GVCourseDetails.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void BindSearch_gridview()
        {
            try
            {
                dt.Clear();
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                SeacrhAction();
                dt = cour.BindGV();
                lblCount.Text = "0";
                if (dt.Rows.Count > 0)
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                }
                else
                {
                    lblCount.Text = dt.Rows.Count.ToString();
                    GVCourseDetails.DataSource = dt;
                    GVCourseDetails.DataBind();
                    ViewState["dt"] = dt;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    GVCourseDetails.Columns[0].Visible = true;
                    GVCourseDetails.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    GVCourseDetails.Columns[0].Visible = false;
                    GVCourseDetails.Columns[1].Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
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

                        Response.AddHeader("content-disposition", "attachment;filename=CourseDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);

                            //To Export all pages
                            GVCourseDetails.Columns[0].Visible = false;
                            GVCourseDetails.Columns[1].Visible = false;
                            GVCourseDetails.Columns[2].Visible = false;
                            GVCourseDetails.Columns[3].Visible = false;
                            GVCourseDetails.AllowPaging = false;

                            GVCourseDetails.DataSource = dt2;
                            GVCourseDetails.DataBind();
                            GVCourseDetails.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in GVCourseDetails.HeaderRow.Cells)
                            {
                                cell.BackColor = GVCourseDetails.HeaderStyle.BackColor;
                            }

                            foreach (GridViewRow row in GVCourseDetails.Rows)
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


                            GVCourseDetails.GridLines = GridLines.Both;
                            GVCourseDetails.RenderControl(hw);

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


        int member_autoid;
        string MemberType;

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            divFormDetails.Visible = true;
            divsearch.Visible = false;
            member_autoid = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["member_autoid"] = Convert.ToInt32(e.CommandArgument.ToString());          
            GetDataForEdit(member_autoid);
            Divcourse.Visible = false;
            Div_paymode.Visible = false;
            kt = 1;

        }

        DataTable DT_single = new DataTable();
        int Receipt;
        string status;
        DataTable dt_package;
        public void GetDataForEdit(int member_autoid)
        {
            try
            {
                DataSet ds = new DataSet();
                btnSave.Text = "Update";
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.ReceiptID = member_autoid;
                txtReceiptid.Text = ViewState["member_autoid"].ToString();
                DT_single = cour.Get_Edit_member();
                Receipt = member_autoid;
                ViewState["DT_single"] = DT_single;
                if (DT_single.Rows.Count > 0)
                {
                    int count = DT_single.Rows.Count;
                    if (count == 1)
                    {
                        status = "Single";
                    }
                    if (count == 2)
                    {
                        status = "Couple";
                    }
                    if (count <= 10 && count > 2)
                    {
                        status = "Group";
                    }

                    if (status == "Single")
                    {
                        Single();
                        DT_single = cour.Get_Edit_member();
                        txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                        txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                        txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                        ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                        txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                        txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                        //   //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());
                        rbtnSingle.Checked = true;

                        cour.ReceiptID = Receipt;
                        txtReceiptid.Text = Receipt.ToString();
                        dt_package = cour.Get_Edit_Assihnpackage();

                        ViewState["DT"] = dt_package;
                        GvPakageAssign.DataSource = dt_package;
                        GvPakageAssign.DataBind();

                        GvPakageAssign.Columns[0].Visible = false;

                        //dt1.Clear();
                        //DataRow dr;                    
                        //dt1.Columns.Add(new DataColumn("Course_Auto"));
                        //dt1.Columns.Add(new DataColumn("Pack_AutoID"));
                        //dt1.Columns.Add(new DataColumn("Package"));
                        //dt1.Columns.Add(new DataColumn("Duration"));
                        //dt1.Columns.Add(new DataColumn("Session"));
                        //dt1.Columns.Add(new DataColumn("Amount"));
                        //dt1.Columns.Add(new DataColumn("StartDate"));
                        //dt1.Columns.Add(new DataColumn("EndDate"));
                        //dt1.Columns.Add(new DataColumn("Qty"));
                        //dt1.Columns.Add(new DataColumn("Total"));
                        //dt1.Columns.Add(new DataColumn("Discount"));
                        //dt1.Columns.Add(new DataColumn("FinalTotal"));
                        //dt1.Columns.Add(new DataColumn("DiscReason"));
                        //dt1.Columns.Add(new DataColumn("Staff_AutoID"));
                        //dr = dt1.NewRow();
                        //for (int i = 0; dt_package.Rows.Count > 0; i++)
                        //{
                        //    dr["Course_Auto"] = dt_package.Rows[i]["Course_Auto"].ToString();// k;
                        //    dr["Pack_AutoID"] = dt_package.Rows[i]["Pack_AutoID"].ToString(); ;
                        //    dr["Package"] = dt_package.Rows[i]["Package"].ToString();
                        //    //   duration = Convert.ToInt32(row.Cells[3].Text);
                        //    dr["Duration"] = dt_package.Rows[i]["Duration"].ToString();
                        //    dr["Session"] = dt_package.Rows[i]["Session"].ToString();
                        //    dr["StartDate"] = dt_package.Rows[i]["StartDate"].ToString();
                        //    dr["EndDate"] = dt_package.Rows[i]["EndDate"].ToString();
                        //    dr["Amount"] = dt_package.Rows[i]["Amount"].ToString();
                        //    dr["Qty"] = dt_package.Rows[i]["Qty"].ToString();
                        //    dr["Total"] = dt_package.Rows[i]["Total"].ToString();
                        //    dr["Discount"] = dt_package.Rows[i]["Discount"].ToString();
                        //    dr["FinalTotal"] = dt_package.Rows[i]["FinalTotal"].ToString();
                        //    dr["DiscReason"] = dt_package.Rows[i]["DiscReason"].ToString();
                        //    dr["Staff_AutoID"] = dt_package.Rows[i]["Staff_AutoID"].ToString();
                        //   // lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) - Convert.ToDouble(txtdisc.Text));
                        //    //  dt1.Rows[s].Delete();
                        //    dt1.Rows.InsertAt(dr, i);
                        //}
                        //    ViewState["DT"] = dt1;
                        //    GvPakageAssign.DataSource = dt1;
                        //    GvPakageAssign.DataBind();

                        //or simply use compute like below
                        lblTotalFee.Text = dt_package.Compute("sum(FinalTotal)", "").ToString();

                        //dt1 = dt_package.Copy();
                        //GvPakageAssign.DataSource = dt1;
                        //GvPakageAssign.DataBind();

                        dt = cour.Get_Edit_MemberType();
                        if (dt.Rows.Count > 0)
                        {
                            cour.Status = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            cour.Status = dt.Rows[0][0].ToString();
                        }
                     
                        DataTable dt_cash = cour.Get_Edit_Payment();
                        ViewState["DT3"] = dt_cash;
                        gvBalancePayment.DataSource = dt_cash;
                        gvBalancePayment.DataBind();

                        gvBalancePayment.Columns[0].Visible = false;

                        DataTable dt_cmnt = cour.Get_Edit_Cmnt();
                        if (dt_cmnt.Rows.Count > 0)
                        {
                            lblPaidFee.Text = dt_cmnt.Rows[0]["PaidFee"].ToString();
                            lblTotalFeeDue.Text = dt_cmnt.Rows[0]["TotalFeeDue"].ToString();
                            lblBalance.Text = dt_cmnt.Rows[0]["Balance"].ToString();
                            //txtNextFollowupDate.Text = dt_cmnt.Rows[0]["NextBalDate"].ToString();
                            txtComment.Text = dt_cmnt.Rows[0]["Comment"].ToString();

                            DateTime todaydate;
                            if (DateTime.TryParseExact(dt_cmnt.Rows[0]["NextBalDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                            {
                                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
                            }

                        }


                    }
                    if (status == "Couple")
                    {
                        Couple();
                        DT_single = cour.Get_Edit_member();

                        txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                        txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                        txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                        ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                        txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                        txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                        //  //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());

                        txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                        txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                        txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                        ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                        txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                        txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                        //   //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());

                        rbtnCouple.Checked = true;

                        cour.ReceiptID = Receipt;
                        txtReceiptid.Text = Receipt.ToString();
                        DataTable dt_package = cour.Get_Edit_Assihnpackage();
                        ViewState["DT"] = dt_package;
                        GvPakageAssign.DataSource = dt_package;
                        GvPakageAssign.DataBind();
                        GvPakageAssign.Columns[0].Visible = false;
                        //or simply use compute like below
                        lblTotalFee.Text = dt_package.Compute("sum(FinalTotal)", "").ToString();

                        DataTable dt_cash = cour.Get_Edit_Payment();
                        ViewState["DT3"] = dt_cash;
                        gvBalancePayment.DataSource = dt_cash;
                        gvBalancePayment.DataBind();

                        gvBalancePayment.Columns[0].Visible = false;

                        DataTable dt_cmnt = cour.Get_Edit_Cmnt();
                        if (dt_cmnt.Rows.Count > 0)
                        {
                            lblPaidFee.Text = dt_cmnt.Rows[0]["PaidFee"].ToString();
                            lblTotalFeeDue.Text = dt_cmnt.Rows[0]["TotalFeeDue"].ToString();
                            lblBalance.Text = dt_cmnt.Rows[0]["Balance"].ToString();
                            // txtNextFollowupDate.Text = dt_cmnt.Rows[0]["NextBalDate"].ToString();
                            txtComment.Text = dt_cmnt.Rows[0]["Comment"].ToString();

                            DateTime todaydate;
                            if (DateTime.TryParseExact(dt_cmnt.Rows[0]["NextBalDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                            {
                                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
                            }

                        }

                    }
                    if (status == "Group")
                    {
                        txtnumber.Enabled = true;
                        txtnumber.Text = Convert.ToString(count);
                        Group();
                        DT_single = cour.Get_Edit_member();

                        if (count == 1)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //   //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());
                        }

                        if (count == 2)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();
                        }


                        if (count == 3)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();
                        }


                        if (count == 4)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());

                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();

                            txtId4.Text = DT_single.Rows[3]["Member_ID1"].ToString();
                            txtFirstName4.Text = DT_single.Rows[3]["FName"].ToString();
                            txtLastName4.Text = DT_single.Rows[3]["LName"].ToString();
                            ddlGender4.Text = DT_single.Rows[3]["Gender"].ToString();
                            txtConatct4.Text = DT_single.Rows[3]["Contact1"].ToString();
                            txtEmail4.Text = DT_single.Rows[3]["Email"].ToString();
                        }


                        if (count == 5)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();

                            txtId4.Text = DT_single.Rows[3]["Member_ID1"].ToString();
                            txtFirstName4.Text = DT_single.Rows[3]["FName"].ToString();
                            txtLastName4.Text = DT_single.Rows[3]["LName"].ToString();
                            ddlGender4.Text = DT_single.Rows[3]["Gender"].ToString();
                            txtConatct4.Text = DT_single.Rows[3]["Contact1"].ToString();
                            txtEmail4.Text = DT_single.Rows[3]["Email"].ToString();

                            txtId5.Text = DT_single.Rows[4]["Member_ID1"].ToString();
                            txtFirstName5.Text = DT_single.Rows[4]["FName"].ToString();
                            txtLastName5.Text = DT_single.Rows[4]["LName"].ToString();
                            ddlGender5.Text = DT_single.Rows[4]["Gender"].ToString();
                            txtContact5.Text = DT_single.Rows[4]["Contact1"].ToString();
                            txtEmail5.Text = DT_single.Rows[4]["Email"].ToString();

                        }


                        if (count == 6)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();

                            txtId4.Text = DT_single.Rows[3]["Member_ID1"].ToString();
                            txtFirstName4.Text = DT_single.Rows[3]["FName"].ToString();
                            txtLastName4.Text = DT_single.Rows[3]["LName"].ToString();
                            ddlGender4.Text = DT_single.Rows[3]["Gender"].ToString();
                            txtConatct4.Text = DT_single.Rows[3]["Contact1"].ToString();
                            txtEmail4.Text = DT_single.Rows[3]["Email"].ToString();

                            txtId5.Text = DT_single.Rows[4]["Member_ID1"].ToString();
                            txtFirstName5.Text = DT_single.Rows[4]["FName"].ToString();
                            txtLastName5.Text = DT_single.Rows[4]["LName"].ToString();
                            ddlGender5.Text = DT_single.Rows[4]["Gender"].ToString();
                            txtContact5.Text = DT_single.Rows[4]["Contact1"].ToString();
                            txtEmail5.Text = DT_single.Rows[4]["Email"].ToString();

                            txtId6.Text = DT_single.Rows[5]["Member_ID1"].ToString();
                            txtFirstName6.Text = DT_single.Rows[5]["FName"].ToString();
                            txtLastName6.Text = DT_single.Rows[5]["LName"].ToString();
                            ddlGender6.Text = DT_single.Rows[5]["Gender"].ToString();
                            txtContact6.Text = DT_single.Rows[5]["Contact1"].ToString();
                            txtEmail6.Text = DT_single.Rows[5]["Email"].ToString();

                        }

                        if (count == 7)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();

                            txtId4.Text = DT_single.Rows[3]["Member_ID1"].ToString();
                            txtFirstName4.Text = DT_single.Rows[3]["FName"].ToString();
                            txtLastName4.Text = DT_single.Rows[3]["LName"].ToString();
                            ddlGender4.Text = DT_single.Rows[3]["Gender"].ToString();
                            txtConatct4.Text = DT_single.Rows[3]["Contact1"].ToString();
                            txtEmail4.Text = DT_single.Rows[3]["Email"].ToString();

                            txtId5.Text = DT_single.Rows[4]["Member_ID1"].ToString();
                            txtFirstName5.Text = DT_single.Rows[4]["FName"].ToString();
                            txtLastName5.Text = DT_single.Rows[4]["LName"].ToString();
                            ddlGender5.Text = DT_single.Rows[4]["Gender"].ToString();
                            txtContact5.Text = DT_single.Rows[4]["Contact1"].ToString();
                            txtEmail5.Text = DT_single.Rows[4]["Email"].ToString();

                            txtId6.Text = DT_single.Rows[5]["Member_ID1"].ToString();
                            txtFirstName6.Text = DT_single.Rows[5]["FName"].ToString();
                            txtLastName6.Text = DT_single.Rows[5]["LName"].ToString();
                            ddlGender6.Text = DT_single.Rows[5]["Gender"].ToString();
                            txtContact6.Text = DT_single.Rows[5]["Contact1"].ToString();
                            txtEmail6.Text = DT_single.Rows[5]["Email"].ToString();

                            txtId7.Text = DT_single.Rows[6]["Member_ID1"].ToString();
                            txtFirstName7.Text = DT_single.Rows[6]["FName"].ToString();
                            txtLastName7.Text = DT_single.Rows[6]["LName"].ToString();
                            ddlGender7.Text = DT_single.Rows[6]["Gender"].ToString();
                            txtContact7.Text = DT_single.Rows[6]["Contact1"].ToString();
                            txtEmail7.Text = DT_single.Rows[6]["Email"].ToString();

                        }

                        if (count == 8)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();

                            txtId4.Text = DT_single.Rows[3]["Member_ID1"].ToString();
                            txtFirstName4.Text = DT_single.Rows[3]["FName"].ToString();
                            txtLastName4.Text = DT_single.Rows[3]["LName"].ToString();
                            ddlGender4.Text = DT_single.Rows[3]["Gender"].ToString();
                            txtConatct4.Text = DT_single.Rows[3]["Contact1"].ToString();
                            txtEmail4.Text = DT_single.Rows[3]["Email"].ToString();

                            txtId5.Text = DT_single.Rows[4]["Member_ID1"].ToString();
                            txtFirstName5.Text = DT_single.Rows[4]["FName"].ToString();
                            txtLastName5.Text = DT_single.Rows[4]["LName"].ToString();
                            ddlGender5.Text = DT_single.Rows[4]["Gender"].ToString();
                            txtContact5.Text = DT_single.Rows[4]["Contact1"].ToString();
                            txtEmail5.Text = DT_single.Rows[4]["Email"].ToString();

                            txtId6.Text = DT_single.Rows[5]["Member_ID1"].ToString();
                            txtFirstName6.Text = DT_single.Rows[5]["FName"].ToString();
                            txtLastName6.Text = DT_single.Rows[5]["LName"].ToString();
                            ddlGender6.Text = DT_single.Rows[5]["Gender"].ToString();
                            txtContact6.Text = DT_single.Rows[5]["Contact1"].ToString();
                            txtEmail6.Text = DT_single.Rows[5]["Email"].ToString();

                            txtId7.Text = DT_single.Rows[6]["Member_ID1"].ToString();
                            txtFirstName7.Text = DT_single.Rows[6]["FName"].ToString();
                            txtLastName7.Text = DT_single.Rows[6]["LName"].ToString();
                            ddlGender7.Text = DT_single.Rows[6]["Gender"].ToString();
                            txtContact7.Text = DT_single.Rows[6]["Contact1"].ToString();
                            txtEmail7.Text = DT_single.Rows[6]["Email"].ToString();

                            txtId8.Text = DT_single.Rows[7]["Member_ID1"].ToString();
                            txtFirstName8.Text = DT_single.Rows[7]["FName"].ToString();
                            txtLastName8.Text = DT_single.Rows[7]["LName"].ToString();
                            ddlGender8.Text = DT_single.Rows[7]["Gender"].ToString();
                            txtContact8.Text = DT_single.Rows[7]["Contact1"].ToString();
                            txtEmail8.Text = DT_single.Rows[7]["Email"].ToString();

                        }

                        if (count == 9)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();

                            txtId4.Text = DT_single.Rows[3]["Member_ID1"].ToString();
                            txtFirstName4.Text = DT_single.Rows[3]["FName"].ToString();
                            txtLastName4.Text = DT_single.Rows[3]["LName"].ToString();
                            ddlGender4.Text = DT_single.Rows[3]["Gender"].ToString();
                            txtConatct4.Text = DT_single.Rows[3]["Contact1"].ToString();
                            txtEmail4.Text = DT_single.Rows[3]["Email"].ToString();

                            txtId5.Text = DT_single.Rows[4]["Member_ID1"].ToString();
                            txtFirstName5.Text = DT_single.Rows[4]["FName"].ToString();
                            txtLastName5.Text = DT_single.Rows[4]["LName"].ToString();
                            ddlGender5.Text = DT_single.Rows[4]["Gender"].ToString();
                            txtContact5.Text = DT_single.Rows[4]["Contact1"].ToString();
                            txtEmail5.Text = DT_single.Rows[4]["Email"].ToString();

                            txtId6.Text = DT_single.Rows[5]["Member_ID1"].ToString();
                            txtFirstName6.Text = DT_single.Rows[5]["FName"].ToString();
                            txtLastName6.Text = DT_single.Rows[5]["LName"].ToString();
                            ddlGender6.Text = DT_single.Rows[5]["Gender"].ToString();
                            txtContact6.Text = DT_single.Rows[5]["Contact1"].ToString();
                            txtEmail6.Text = DT_single.Rows[5]["Email"].ToString();

                            txtId7.Text = DT_single.Rows[6]["Member_ID1"].ToString();
                            txtFirstName7.Text = DT_single.Rows[6]["FName"].ToString();
                            txtLastName7.Text = DT_single.Rows[6]["LName"].ToString();
                            ddlGender7.Text = DT_single.Rows[6]["Gender"].ToString();
                            txtContact7.Text = DT_single.Rows[6]["Contact1"].ToString();
                            txtEmail7.Text = DT_single.Rows[6]["Email"].ToString();

                            txtId8.Text = DT_single.Rows[7]["Member_ID1"].ToString();
                            txtFirstName8.Text = DT_single.Rows[7]["FName"].ToString();
                            txtLastName8.Text = DT_single.Rows[7]["LName"].ToString();
                            ddlGender8.Text = DT_single.Rows[7]["Gender"].ToString();
                            txtContact8.Text = DT_single.Rows[7]["Contact1"].ToString();
                            txtEmail8.Text = DT_single.Rows[7]["Email"].ToString();

                            txtId9.Text = DT_single.Rows[8]["Member_ID1"].ToString();
                            txtFirstName9.Text = DT_single.Rows[8]["FName"].ToString();
                            txtLastName9.Text = DT_single.Rows[8]["LName"].ToString();
                            ddlGender9.Text = DT_single.Rows[8]["Gender"].ToString();
                            txtConatct9.Text = DT_single.Rows[8]["Contact1"].ToString();
                            txtEmail9.Text = DT_single.Rows[8]["Email"].ToString();

                        }
                        if (count == 10)
                        {
                            txtId1.Text = DT_single.Rows[0]["Member_ID1"].ToString();
                            txtFirstName1.Text = DT_single.Rows[0]["FName"].ToString();
                            txtLastName1.Text = DT_single.Rows[0]["LName"].ToString();
                            ddlGender1.Text = DT_single.Rows[0]["Gender"].ToString();
                            txtContact1.Text = DT_single.Rows[0]["Contact1"].ToString();
                            txtEmail1.Text = DT_single.Rows[0]["Email"].ToString();
                            //  Receipt = Convert.ToInt32(DT_single.Rows[0]["ReceiptID"].ToString());


                            txtId2.Text = DT_single.Rows[1]["Member_ID1"].ToString();
                            txtFirstName2.Text = DT_single.Rows[1]["FName"].ToString();
                            txtLastName2.Text = DT_single.Rows[1]["LName"].ToString();
                            ddlGender2.Text = DT_single.Rows[1]["Gender"].ToString();
                            txtContact2.Text = DT_single.Rows[1]["Contact1"].ToString();
                            txtEmail2.Text = DT_single.Rows[1]["Email"].ToString();

                            txtId3.Text = DT_single.Rows[2]["Member_ID1"].ToString();
                            txtFirstName3.Text = DT_single.Rows[2]["FName"].ToString();
                            txtLastName3.Text = DT_single.Rows[2]["LName"].ToString();
                            ddlGende3.Text = DT_single.Rows[2]["Gender"].ToString();
                            txtContact3.Text = DT_single.Rows[2]["Contact1"].ToString();
                            txtEmail3.Text = DT_single.Rows[2]["Email"].ToString();

                            txtId4.Text = DT_single.Rows[3]["Member_ID1"].ToString();
                            txtFirstName4.Text = DT_single.Rows[3]["FName"].ToString();
                            txtLastName4.Text = DT_single.Rows[3]["LName"].ToString();
                            ddlGender4.Text = DT_single.Rows[3]["Gender"].ToString();
                            txtConatct4.Text = DT_single.Rows[3]["Contact1"].ToString();
                            txtEmail4.Text = DT_single.Rows[3]["Email"].ToString();

                            txtId5.Text = DT_single.Rows[4]["Member_ID1"].ToString();
                            txtFirstName5.Text = DT_single.Rows[4]["FName"].ToString();
                            txtLastName5.Text = DT_single.Rows[4]["LName"].ToString();
                            ddlGender5.Text = DT_single.Rows[4]["Gender"].ToString();
                            txtContact5.Text = DT_single.Rows[4]["Contact1"].ToString();
                            txtEmail5.Text = DT_single.Rows[4]["Email"].ToString();

                            txtId6.Text = DT_single.Rows[5]["Member_ID1"].ToString();
                            txtFirstName6.Text = DT_single.Rows[5]["FName"].ToString();
                            txtLastName6.Text = DT_single.Rows[5]["LName"].ToString();
                            ddlGender6.Text = DT_single.Rows[5]["Gender"].ToString();
                            txtContact6.Text = DT_single.Rows[5]["Contact1"].ToString();
                            txtEmail6.Text = DT_single.Rows[5]["Email"].ToString();

                            txtId7.Text = DT_single.Rows[6]["Member_ID1"].ToString();
                            txtFirstName7.Text = DT_single.Rows[6]["FName"].ToString();
                            txtLastName7.Text = DT_single.Rows[6]["LName"].ToString();
                            ddlGender7.Text = DT_single.Rows[6]["Gender"].ToString();
                            txtContact7.Text = DT_single.Rows[6]["Contact1"].ToString();
                            txtEmail7.Text = DT_single.Rows[6]["Email"].ToString();

                            txtId8.Text = DT_single.Rows[7]["Member_ID1"].ToString();
                            txtFirstName8.Text = DT_single.Rows[7]["FName"].ToString();
                            txtLastName8.Text = DT_single.Rows[7]["LName"].ToString();
                            ddlGender8.Text = DT_single.Rows[7]["Gender"].ToString();
                            txtContact8.Text = DT_single.Rows[7]["Contact1"].ToString();
                            txtEmail8.Text = DT_single.Rows[7]["Email"].ToString();

                            txtId9.Text = DT_single.Rows[8]["Member_ID1"].ToString();
                            txtFirstName9.Text = DT_single.Rows[8]["FName"].ToString();
                            txtLastName9.Text = DT_single.Rows[8]["LName"].ToString();
                            ddlGender9.Text = DT_single.Rows[8]["Gender"].ToString();
                            txtConatct9.Text = DT_single.Rows[8]["Contact1"].ToString();
                            txtEmail9.Text = DT_single.Rows[8]["Email"].ToString();

                            txtId10.Text = DT_single.Rows[9]["Member_ID1"].ToString();
                            txtFirstName10.Text = DT_single.Rows[9]["FName"].ToString();
                            txtLastName10.Text = DT_single.Rows[9]["LName"].ToString();
                            ddlGender10.Text = DT_single.Rows[9]["Gender"].ToString();
                            txtConatct10.Text = DT_single.Rows[9]["Contact1"].ToString();
                            txtEmail10.Text = DT_single.Rows[9]["Email"].ToString();
                        }
                        cour.ReceiptID = Receipt;
                        txtReceiptid.Text = Receipt.ToString();
                        DataTable dt_package = cour.Get_Edit_Assihnpackage();
                        ViewState["DT"] = dt_package;
                        GvPakageAssign.DataSource = dt_package;
                        GvPakageAssign.DataBind();

                        GvPakageAssign.Columns[0].Visible = false;

                        //or simply use compute like below
                        lblTotalFee.Text = dt_package.Compute("sum(FinalTotal)", "").ToString();

                        DataTable dt_cash = cour.Get_Edit_Payment();
                        ViewState["DT3"] = dt_cash;
                        gvBalancePayment.DataSource = dt_cash;
                        gvBalancePayment.DataBind();

                        gvBalancePayment.Columns[0].Visible = false;

                        DataTable dt_cmnt = cour.Get_Edit_Cmnt();
                        if (dt_cmnt.Rows.Count > 0)
                        {
                            lblPaidFee.Text = dt_cmnt.Rows[0]["PaidFee"].ToString();
                            lblTotalFeeDue.Text = dt_cmnt.Rows[0]["TotalFeeDue"].ToString();
                            lblBalance.Text = dt_cmnt.Rows[0]["Balance"].ToString();
                            // txtNextFollowupDate.Text = dt_cmnt.Rows[0]["NextBalDate"].ToString();
                            txtComment.Text = dt_cmnt.Rows[0]["Comment"].ToString();

                            DateTime todaydate;
                            if (DateTime.TryParseExact(dt_cmnt.Rows[0]["NextBalDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                            {
                                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
                            }
                        }
                        //For Help dt Empty
                        dt_cash = cour.Get_Edit_Payment();
                        ViewState["DT3"] = dt_cash;
                        rbtnCouple.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public void GET_MemberID1_For_REFREnce()
        {
            cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = cour.Get_MemberID1();
            if (dt.Rows.Count > 0)
            {
                if (rbtnSingle.Checked == true)
                {
                    txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                }
                if (rbtnCouple.Checked == true)
                {

                    cour.Contact = txtContact1.Text;
                    cnt1 = cour.ContactExist_OR_Not();
                    if (cnt1 > 0)
                    {
                    }
                    else
                    {
                        dt = cour.Get_MemberID1();
                        if (dt.Rows.Count > 0)
                        {
                            txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                            int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                            txtId2.Text = Convert.ToString(id + 1);
                        }
                    }
                    cour.Contact = txtContact2.Text;
                    cnt1 = cour.ContactExist_OR_Not();
                    if (cnt1 > 0)
                    {
                    }
                    else
                    {
                        dt = cour.Get_MemberID1();
                        if (dt.Rows.Count > 0)
                        {
                            //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                            int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                            txtId2.Text = Convert.ToString(id + 1);
                        }

                    }
                }
                if (rbtnGroup.Checked == true)
                {
                    if (txtnumber.Text == "1")
                    {
                        txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                    }

                    if (txtnumber.Text == "2")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);


                            }

                        }
                    }

                    if (txtnumber.Text == "3")
                    {

                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                    }

                    if (txtnumber.Text == "4")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId4.Text = Convert.ToString(id + 3);
                            }
                        }
                    }

                    if (txtnumber.Text == "5")
                    {

                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId4.Text = Convert.ToString(id + 3);
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId5.Text = Convert.ToString(id + 4);
                            }
                        }
                    }

                    if (txtnumber.Text == "6")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId4.Text = Convert.ToString(id + 3);
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId5.Text = Convert.ToString(id + 4);
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId6.Text = Convert.ToString(id + 5);
                            }
                        }
                    }

                    if (txtnumber.Text == "7")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId4.Text = Convert.ToString(id + 3);
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId5.Text = Convert.ToString(id + 4);
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId6.Text = Convert.ToString(id + 5);
                            }
                        }

                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId7.Text = Convert.ToString(id + 6);
                            }
                        }
                    }

                    if (txtnumber.Text == "8")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId4.Text = Convert.ToString(id + 3);
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId5.Text = Convert.ToString(id + 4);
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId6.Text = Convert.ToString(id + 5);
                            }
                        }

                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId7.Text = Convert.ToString(id + 6);
                            }
                        }

                        cour.Contact = txtContact8.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId8.Text = Convert.ToString(id + 7);
                            }
                        }
                    }

                    if (txtnumber.Text == "9")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId4.Text = Convert.ToString(id + 3);
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId5.Text = Convert.ToString(id + 4);
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId6.Text = Convert.ToString(id + 5);
                            }
                        }

                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId7.Text = Convert.ToString(id + 6);
                            }
                        }

                        cour.Contact = txtContact8.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId8.Text = Convert.ToString(id + 7);
                            }
                        }

                        cour.Contact = txtConatct9.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId9.Text = Convert.ToString(id + 8);
                            }
                        }
                    }

                    if (txtnumber.Text == "10")
                    {
                        cour.Contact = txtContact1.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact2.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId2.Text = Convert.ToString(id + 1);
                            }
                        }

                        cour.Contact = txtContact3.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId3.Text = Convert.ToString(id + 2);
                            }
                        }

                        cour.Contact = txtConatct4.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId4.Text = Convert.ToString(id + 3);
                            }
                        }

                        cour.Contact = txtContact5.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId5.Text = Convert.ToString(id + 4);
                            }
                        }

                        cour.Contact = txtContact6.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId6.Text = Convert.ToString(id + 5);
                            }
                        }

                        cour.Contact = txtContact7.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId7.Text = Convert.ToString(id + 6);
                            }
                        }

                        cour.Contact = txtContact8.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId8.Text = Convert.ToString(id + 7);
                            }
                        }

                        cour.Contact = txtConatct9.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId9.Text = Convert.ToString(id + 8);
                            }
                        }

                        cour.Contact = txtConatct10.Text;
                        cnt1 = cour.ContactExist_OR_Not();
                        if (cnt1 > 0)
                        {
                        }
                        else
                        {
                            dt = cour.Get_MemberID1();
                            if (dt.Rows.Count > 0)
                            {
                                //txtId1.Text = dt.Rows[0]["Member_ID1"].ToString();
                                int id = Convert.ToInt32(dt.Rows[0]["Member_ID1"].ToString());
                                txtId10.Text = Convert.ToString(id + 9);
                            }
                        }
                    }
                }
            }
        }

        protected void txtContact1_TextChanged(object sender, EventArgs e)
        {
            if (txtContact1.Text != "")
            {
                Chk_Contactno(txtContact1.Text);
                txtEmail1.Focus();
            }
        }
        DataTable dt_cont = new DataTable();
        public void Chk_Contactno(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId1.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    if (dt_cont.Rows[0]["BlockStatus"].ToString() != "Block")
                    {
                        txtId1.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                        txtFirstName1.Text = dt_cont.Rows[0]["FName"].ToString();
                        txtLastName1.Text = dt_cont.Rows[0]["LName"].ToString();
                        ddlGender1.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                        txtContact1.Text = dt_cont.Rows[0]["Contact1"].ToString();
                        txtEmail1.Text = dt_cont.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        string url = "Termination.aspx?Member_ID=" + txtId1.Text + " &Course=" + HttpUtility.UrlEncode("Course");
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "text", "showConfirmation('" + url + "')", true);
                    }
                }
                else
                {
                    GET_MemberID1_For_REFREnce();
                    //txtLastName1.Text = "";
                    //txtFirstName1.Text = "";
                    //txtEmail1.Text = "";
                    //ddlGender1.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void Chk_Contactno_2(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId2.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName2.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName2.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender2.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtContact2.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail2.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    GET_MemberID1_For_REFREnce();
                    //txtFirstName2.Text = "";
                    //txtLastName2.Text = "";
                    //txtEmail2.Text = "";
                    //ddlGender2.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void Chk_Contactno_3(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId3.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName3.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName3.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGende3.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtContact3.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail3.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    GET_MemberID1_For_REFREnce();
                    //txtFirstName3.Text = "";
                    //txtLastName3.Text = "";
                    //txtEmail3.Text = "";
                    //ddlGende3.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtContact2_TextChanged(object sender, EventArgs e)
        {
            if (txtContact2.Text != "")
            {
                Chk_Contactno_2(txtContact2.Text);
                txtEmail2.Focus();
            }
        }

        protected void txtContact3_TextChanged(object sender, EventArgs e)
        {
            if (txtContact3.Text != "")
            {
                Chk_Contactno_3(txtContact3.Text);
                txtEmail3.Focus();
            }
        }

        protected void txtConatct4_TextChanged(object sender, EventArgs e)
        {
            if (txtConatct4.Text != "")
            {
                Chk_Contactno_4(txtConatct4.Text);
                txtEmail4.Focus();
            }
        }

        public void Chk_Contactno_4(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId4.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName4.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName4.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender4.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtConatct4.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail4.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    GET_MemberID1_For_REFREnce();
                    //txtFirstName4.Text = "";
                    //txtLastName4.Text = "";
                    //txtEmail4.Text = "";
                    //ddlGender4.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void Chk_Contactno_5(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId5.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName5.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName5.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender5.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtContact5.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail5.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    // GET_MemberID1_For_REFREnce();
                    //txtFirstName5.Text = "";
                    //txtLastName5.Text = "";
                    //txtEmail5.Text = "";
                    //ddlGender5.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtContact5_TextChanged(object sender, EventArgs e)
        {
            if (txtContact5.Text != "")
            {
                Chk_Contactno_5(txtContact5.Text);
                txtEmail5.Focus();
            }
        }

        public void Chk_Contactno_6(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId6.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName6.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName6.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender6.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtContact6.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail6.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    // GET_MemberID1_For_REFREnce();
                    //txtFirstName6.Text = "";
                    //txtLastName6.Text = "";
                    //txtEmail6.Text = "";
                    //ddlGender6.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtContact6_TextChanged(object sender, EventArgs e)
        {
            if (txtContact6.Text != "")
            {
                Chk_Contactno_6(txtContact6.Text);
                txtEmail6.Focus();
            }
        }

        public void Chk_Contactno_7(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId7.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName7.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName7.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender7.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtContact7.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail7.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    // GET_MemberID1_For_REFREnce();
                    //txtFirstName7.Text = "";
                    //txtLastName7.Text = "";
                    //txtEmail7.Text = "";
                    //ddlGender7.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtContact7_TextChanged(object sender, EventArgs e)
        {
            if (txtContact7.Text != "")
            {
                Chk_Contactno_7(txtContact7.Text);
                txtEmail7.Focus();
            }
        }

        public void Chk_Contactno_8(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId8.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName8.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName8.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender8.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtContact8.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail8.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    // GET_MemberID1_For_REFREnce();
                    //txtFirstName8.Text = "";
                    //txtLastName8.Text = "";
                    //txtEmail8.Text = "";
                    //ddlGender8.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtContact8_TextChanged(object sender, EventArgs e)
        {
            if (txtContact8.Text != "")
            {
                Chk_Contactno_8(txtContact8.Text);
                txtEmail8.Focus();
            }
        }

        public void Chk_Contactno_9(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId9.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName9.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName9.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender9.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtConatct9.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail9.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    GET_MemberID1_For_REFREnce();
                    //txtFirstName9.Text = "";
                    //txtLastName9.Text = "";
                    //txtEmail9.Text = "";
                    //ddlGender9.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtConatct9_TextChanged(object sender, EventArgs e)
        {
            if (txtConatct9.Text != "")
            {
                Chk_Contactno_9(txtConatct9.Text);
                txtEmail9.Focus();
            }
        }

        public void Chk_Contactno_10(string contct)
        {
            try
            {
                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.Contact = contct;
                dt_cont.Clear();
                dt_cont = cour.ChkContactno();
                if (dt_cont.Rows.Count > 0)
                {
                    txtId10.Text = dt_cont.Rows[0]["Member_ID1"].ToString();
                    txtFirstName10.Text = dt_cont.Rows[0]["FName"].ToString();
                    txtLastName10.Text = dt_cont.Rows[0]["LName"].ToString();
                    ddlGender10.SelectedValue = dt_cont.Rows[0]["Gender"].ToString();
                    txtConatct10.Text = dt_cont.Rows[0]["Contact1"].ToString();
                    txtEmail10.Text = dt_cont.Rows[0]["Email"].ToString();
                }
                else
                {
                    // GET_MemberID1_For_REFREnce();
                    //txtFirstName10.Text = "";
                    //txtLastName10.Text = "";
                    //txtEmail10.Text = "";
                    //ddlGender10.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtConatct10_TextChanged(object sender, EventArgs e)
        {
            if (txtConatct10.Text != "")
            {
                Chk_Contactno_10(txtConatct10.Text);
                txtEmail10.Focus();
            }
        }

        protected void btnpreview_Command(object sender, CommandEventArgs e)
        {
            int Receipt_No = Convert.ToInt32(e.CommandArgument.ToString());
            cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            cour.ReceiptID = Receipt_No;
            dt = cour.Get_Status();
            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0][0].ToString();
                if (status == "New")
                {
                    string strPopup = "<script language='javascript' ID='script1'>"
          + "window.open('Receipt.aspx?Receipt_No=" + Receipt_No
          + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
          + "</script>";
                    ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                }
                else
                {
                    // int Receipt_No = Convert.ToInt32(txtReceiptid.Text);
                    string strPopup = "<script language='javascript' ID='script1'>"
                    + "window.open('Receipt.aspx?PT=" + Receipt_No
                    + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
                    + "</script>";
                    ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                }
            }



        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
            if (DropDownList3.SelectedValue.ToString() == "--Select--")
            {
                //txtSearch.Style.Add("border", "1px solid silver ");
                TextBox1.Enabled = false;

            }
            else if (DropDownList3.SelectedValue.ToString() == "Active")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }
            else if (DropDownList3.SelectedValue.ToString() == "Deactive")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }

            else if (DropDownList3.SelectedValue.ToString() == "New")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }

            else if (DropDownList3.SelectedValue.ToString() == "Renew")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }

            else if (DropDownList3.SelectedValue.ToString() == "Upgrade")
            {
                TextBox1.Enabled = false;
                BindSearch_gridview();
            }
            else
            {
                TextBox1.Enabled = true;
            }
            DropDownList3.Focus();
        }


        protected void gvCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCourse.PageIndex = e.NewPageIndex;
            BindGridview_1();
        }
        public void ReceiptID_Exists_orNot()
        {
            try
            {
                if (txtReceiptid.Text != "")
                {
                    cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
                    int recei = cour.ReceiptIDExist_OR_Not();
                    if (recei > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Receipt ID Allready Exists !!!','Error');", true);
                        txtReceiptid.Text = "";
                        ReceiptID();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        protected void txtReceiptid_TextChanged(object sender, EventArgs e)
        {
            ReceiptID_Exists_orNot();
        }
        DataTable del = new DataTable();
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int REceipt = Convert.ToInt32(e.CommandArgument.ToString());

                cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                cour.ReceiptID = REceipt;
                del = cour.FetchRemBal_BYReceiptID();
                //if (del.Rows.Count > 0)
                //{
                //if (Convert.ToInt32(del.Rows[0][0].ToString()) > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Balance is Remaining !!!','Error');", true);
                //}
                //else
                //{
                int opp = cour.DeleteReceipt();
                //del = cour.FetchCourseid_BYReceiptID();
                //if (del.Rows.Count > 0)
                //{
                //    for (int i = 0; i < del.Rows.Count; i++)
                //    {
                //        cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                //        cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //        cour.Course_Auto = Convert.ToInt32(del.Rows[i][0].ToString());
                //        opp = cour.DeleteFreezing();
                //    }
                //}
                //if (del.Rows.Count > 0)
                //{
                //    for (int i = 0; i < del.Rows.Count; i++)
                //    {
                //        cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                //        cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //        cour.Course_Auto = Convert.ToInt32(del.Rows[i][0].ToString());
                //        opp = cour.DeleteExtension();
                //    }
                //}
                if (opp > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    SearchByDate();
                }
                // }

                // }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Allready Exists !!!','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        BalEnquiry eng = new BalEnquiry();
        string gender = "";
        string s = "";
        private void SendSMSNew()
        {
                   
            StringBuilder Message = new StringBuilder();

            eng.Action = "Get_RenewTemplate";
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            eng.Contact1 = txtContact1.Text;
            dt = eng.GetTemplate();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SMSStatus"].ToString() == "Yes")
                {

                    DateTime utcTime = DateTime.UtcNow;
                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime k = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local             

                    string abc = dt.Rows[0]["Renew"].ToString();
                    string newstring = string.Empty;
                    StringBuilder sb = new StringBuilder(abc);
                    sb.Replace("#MID#", txtId1.Text);
                    sb.Replace("#REC#", txtReceiptid.Text);
                    sb.Replace("#TFess#", lblTotalFeeDue.Text);
                    sb.Replace("#PaidFees#", lblPaidFee.Text);
                    sb.Replace("#RemBal#", lblBalance.Text);

                    foreach (GridViewRow row in gvBalancePayment.Rows)
                    {
                        TextBox txtDate = (TextBox)row.FindControl("txtDate");
                        DateTime payDate;
                        if (DateTime.TryParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out payDate))
                        {
                            sb.Replace("#PayDate#", payDate.ToString("dd/MM/yyyy"));
                        }

                        TextBox txtNumber = (TextBox)row.FindControl("txtNumber");
                        if (txtNumber.Text == string.Empty)
                        {
                            sb.Replace("#ChequeNo#", "");
                        }
                        else
                        {
                            sb.Replace("#ChequeNo#", txtNumber.Text);
                        }

                    }

                    sb.Replace("#TODate#", k.ToString("dd/MM/yyyy HH:mm tt"));
                    sb.Replace("#PayMode#", ddlPayMode.SelectedItem.Text);
                    if (txtNextFollowupDate.Text != null)
                    {
                        DateTime NextFollowupDate;
                        if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NextFollowupDate))
                        {
                            sb.Replace("#NextBalanceDate#", NextFollowupDate.ToString("dd/MM/yyyy"));
                        }

                    }
                    else
                    {
                        sb.Replace("#NextBalanceDate#", " ");
                    }

                    newstring = sb.ToString();

                    if (dt.Rows[0]["SMSWithName"].ToString() == "YES")
                    {

                        if (ddlGender1.SelectedValue == "Male")
                        {
                            gender = "Sir";
                        }
                        if (ddlGender1.SelectedValue == "Female")
                        {
                            gender = "Madam";
                        }
                        s = "Dear" + " " + txtFirstName1.Text + "" + txtLastName1.Text + " " + gender + " " + newstring;
                        SendSMSFun(txtContact1.Text, s);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Sms Send Successfully !!!','Success');", true);
                    }
                    else
                    {
                        s = newstring;
                        SendSMSFun(txtContact1.Text, s);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Sms Send Successfully !!!','Success');", true);
                    }

                }
            }        
        }

       string suname, spass, senderid, routeid, status1;
        public void SendSMSFun(string Mobile, string Message)
        {
            int Val;
            try
            {
                eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                eng.Action = "SELECT_SMSLogin_INFO";
                DataSet ds = new DataSet();

                ds = eng.GetSMSLoginDetails();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    suname = ds.Tables[0].Rows[0]["Username"].ToString();
                    spass = ds.Tables[0].Rows[0]["Password"].ToString();
                    senderid = ds.Tables[0].Rows[0]["Sender_ID"].ToString();
                    routeid = ds.Tables[0].Rows[0]["Route"].ToString();
                    status1 = ds.Tables[0].Rows[0]["Status"].ToString();
                }

                if (status1 == "ON")
                {
                    Val = 0;
                    string strUrl = "http://173.45.76.226:81/send.aspx?username=" + suname + "&pass=" + spass + "&route=" + routeid + "&senderid=" + senderid + "&numbers=" + Mobile + "&message=" + Server.UrlEncode(Message.ToString()) + "";
                    //System.Web.HttpUtility.UrlEncode;
                    WebRequest request = HttpWebRequest.Create(strUrl);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = (Stream)response.GetResponseStream();
                    StreamReader readStream = new StreamReader(s);
                    string dataString = readStream.ReadToEnd();
                    response.Close();
                    s.Close();
                    readStream.Close();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        protected void btnResend_Command(object sender, CommandEventArgs e)
        {

        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
            {
                ddlExecutive.Enabled = false;
                //filePhoto.Focus();
            }
            else
            {
                ddlExecutive.Enabled = true;
                // ddlExecutive.Focus();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindSearch_gridview();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string StartDate1, EndDate1;
            DateTime StartDate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
            {
                StartDate1 = StartDate.ToString("dd-MM-yyyy");
                cour.StartDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            DateTime EndDate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
            {
                EndDate1 = EndDate.ToString("dd-MM-yyyy");
                cour.EndDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }
            AssignID();


            if (cour.StartDate > cour.EndDate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                AssignTodaysDate1();
            }
            else
            {
                if (DropDownList3.SelectedValue == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                }
                if (TextBox1.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                }
                else if (DropDownList3.SelectedValue != "--Select--" && TextBox1.Text != "")
                {
                    try
                    {
                        SearchBYCategory();
                        flag2 = 3;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                        ErrorHandiling.SendErrorToText(ex);
                    }
                }
            }
        }

        protected void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel1();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void SerachByDate_Cur()
        {

            string StartDate1, EndDate1;
            DateTime StartDate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate))
            {
                StartDate1 = StartDate.ToString("dd-MM-yyyy");
                cour.StartDate = DateTime.ParseExact(StartDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            DateTime EndDate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
            {
                EndDate1 = EndDate.ToString("dd-MM-yyyy");
                cour.EndDate = DateTime.ParseExact(EndDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }
            AssignID();


            if (cour.StartDate > cour.EndDate)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('From Date Should Not Be Greater Than To Date !!!.','Information');", true);
                AssignTodaysDate1();
            }
            else
            {
                try
                {
                    SearchByDate();
                    // BindSearch_gridview();
                    flag2 = 2;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    ErrorHandiling.SendErrorToText(ex);
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SerachByDate_Cur();
        }
        private void AssignID()
        {
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
        }
        static int flag2 = 0;
        protected void GVCourseDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVCourseDetails.PageIndex = 0;
            if (flag2 == 2)
            {
                GVCourseDetails.PageIndex = e.NewPageIndex;
                SearchByDate();

            }
            else if (flag2 == 3)
            {
                GVCourseDetails.PageIndex = e.NewPageIndex;
                SearchBYCategory();
            }
            else
            {
                GVCourseDetails.PageIndex = e.NewPageIndex;
                BindSearch_gridview();
            }
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            //cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            //dt = cour.Instructor();
            //if (dt.Rows.Count > 0)
            //{
            //    ddlInstructor.DataSource = dt;
            //    ddlInstructor.Items.Clear();
            //    ddlInstructor.DataValueField = "ReceiptID";
            //    ddlInstructor.DataTextField = "ReceiptID";
            //    ddlInstructor.DataBind();
            //    ddlInstructor.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            //}

        }

        protected void GvPakageAssign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //Find the DropDownList in the Row
            //    DropDownList ddlInstructor = (e.Row.FindControl("ddlInstructor") as DropDownList);

            //    dt = cour.Instructor();
            //    if (dt.Rows.Count > 0)
            //    {
            //        ddlInstructor.DataSource = dt;
            //        ddlInstructor.Items.Clear();
            //        ddlInstructor.DataValueField = "Staff_AutoID";
            //        ddlInstructor.DataTextField = "Name";
            //        ddlInstructor.DataBind();
            //        ddlInstructor.Items.Insert(0, new ListItem("--Select--", "--Select--"));


            //        //Select the Country of Customer in DropDownList
            //        string country = (e.Row.FindControl("ddlInstructor") as Label).Text;
            //        ddlInstructor.Items.FindByValue(country).Selected = true;
            //    }

            //}
        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            AssignTodaysDate1();
            TextBox1.Text = "";
            DropDownList3.SelectedValue = "--Select--";
            //DropDownList3.SelectedItem.Text = "--Select--";
            //GVCourseDetails.DataSource = null;
            //GVCourseDetails.DataBind();
            SerachByDate_Cur();
        }
        //protected void BtnSelectDelete_Click(object sender, EventArgs e)
        //{
        //    int count = 0;
        //    SetData();
        //    GVCourseDetails.AllowPaging = false;
        //    GVCourseDetails.DataBind();
        //    ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
        //    count = arr.Count;
        //    for (int i = 0; i < GVCourseDetails.Rows.Count; i++)
        //    {
        //        if (arr.Contains(GVCourseDetails.DataKeys[i].Value))
        //        {
        //           // DeleteRecord(GVCourseDetails.DataKeys[i].Value.ToString());
        //            arr.Remove(GVCourseDetails.DataKeys[i].Value);
        //        }
        //    }
        //    ViewState["SelectedRecords"] = arr;
        //    // hfCount.Value = "0";
        //    GVCourseDetails.AllowPaging = true;
        //    BindGridview_1();
        //   // ShowMessage(count);
        //}


        //protected void btnDelete_Command(object sender, CommandEventArgs e)
        //{
        //    try
        //    {

        //        cour.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
        //        cour.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //        cour.ReceiptID = Convert.ToInt32(txtReceiptid.Text);
        //        int index = Convert.ToInt32(e.CommandArgument.ToString());
        //        cour.Bal_Auto = index;
        //        cour.Get_DeletePaymentType();
        //        DataTable dt_cash = cour.Get_Edit_Payment();
        //        ViewState["DT3"] = dt_cash;
        //        gvBalancePayment.DataSource = dt_cash;
        //        gvBalancePayment.DataBind();             

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }

        //}

        //protected void btnDelete_AssignPackage_Command(object sender, CommandEventArgs e)
        //{
        //    try
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument.ToString());
        //        dt1 = (DataTable)ViewState["DT"];

        //        double paid = Convert.ToDouble(dt1.Rows[index][11].ToString());
        //        lblTotalFee.Text = Convert.ToString(Convert.ToDouble(lblTotalFee.Text) - paid);

        //        dt1.Rows[index].Delete();
        //        ViewState["DT"] = dt1;
        //        GvPakageAssign.DataSource = dt1;
        //        GvPakageAssign.DataBind();

        //        if (dt1.Rows.Count == 0)
        //        {
        //            lblTotalFee.Text = "0";
        //            gvBalancePayment.DataSource = null;
        //            gvBalancePayment.DataBind();
        //            lblBalance.Text = "0";
        //            lblPaidFee.Text = "0";
        //            lblTotalFeeDue.Text = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }
        //}
    }
}