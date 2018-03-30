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
using System.Text.RegularExpressions;

namespace NDOnlineGym_2017
{
    public partial class EnquiryFollowup : System.Web.UI.Page
    {
        BalCallRespondMaster obBalCallRespondMaster = new BalCallRespondMaster();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalEnquiryFollowup objEnqFlw = new BalEnquiryFollowup();
        BalEnquiry objBalEnquiry = new BalEnquiry();
        DataTable dt = new DataTable();
        static int Enq_ID;
        int res;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindDDLFollowupType();
                bindDDLExecutive();
                setExecutive();
                bindDDLCallRespond();
                if (Request.QueryString["Enq_ID"] != null)
                {
                    int enqid = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    GetMemberDetails(enqid);
                    lblFollwupDate.Text = DateTime.Now.ToString("HH:MM tt");
                    objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    AssignDateAndTime();
                    //EnquiryInfo();
                    ddlCallPesponse.Focus();
                    
                    if (Request.QueryString["FNameViewEnqFoll"] != null)
                    {
                        objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        objEnqFlw.Action = "BindDetailsByID";
                        GetMemberDetailsByFollAutoID(enqid);
                        gridBind();
                    }
                    else if (Request.QueryString["MenuEnquDetails"] != null)
                    {
                        objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        objEnqFlw.Action = "BindDetailsByID";
                        gridBind();
                    }
                    else if (Request.QueryString["MenuEnqDetailsFoll"] != null)
                    {
                        objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        objEnqFlw.Action = "BindDetailsByID";
                        
                        gridBind();
                    }
                    else if (Request.QueryString["MenuEnqFollowupDetails"] != null)
                    {
                        objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        objEnqFlw.Action = "BindDetailsByID";
                        gridBind();
                    }
                    else if (Request.QueryString["FNameSearchPage"] != null)
                    {
                        objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                        objEnqFlw.Action = "BindDetailsByID";
                        gridBind();
                    }
                    else
                        BindGridViewDetails();

                    
                }
                if (Request.QueryString["Data"] != null)
                {
                    lblFollwupDate.Text = DateTime.Now.ToString("HH:MM tt");
                    objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Data"]);
                    dt = objEnqFlw.EnryInfoById1();
                    if (dt.Rows.Count > 0)
                    {
                        objEnqFlw.Enq_ID = Convert.ToInt32(dt.Rows[0]["Enq_AutoID"].ToString());
                        int enqid = Convert.ToInt32(dt.Rows[0]["Enq_AutoID"].ToString());
                        ViewState["Auto_ID"] = dt.Rows[0]["Enq_AutoID"].ToString();
                        GetMemberDetails(enqid);
                        //EnquiryInfo();
                        BindGridViewDetails();
                        AssignDateAndTime();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    }
                }
               
                
                //if (Request.QueryString["Enq_ID"] !=null)
                //{
                
                //    lblFollwupDate.Text = DateTime.Now.ToString("HH:MM tt");
                //    objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                //    objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //    objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                //    AssignDateAndTime();
                //    EnquiryInfo();
                //    ddlCallPesponse.Focus();
                //    BindGridViewDetails();
                //}
            }
        }

        public void GetMemberDetailsByFollAutoID(int enqid)
        {
            objBalEnquiry.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalEnquiry.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalEnquiry.Enq_FollAutoID = Convert.ToInt32(enqid);
            dt = objBalEnquiry.GetMemberDetailsByFollAutoID();
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["FName"].ToString();
                lblContact.Text = dt.Rows[0]["Contact1"].ToString();
                ViewState["enqautoid"] = dt.Rows[0]["Enq_AutoID"].ToString();

                if (dt.Rows[0]["DOB"].ToString() != "")
                {
                    DateTime dobdate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                    DateTime dobdate1;
                    if (DateTime.TryParseExact(dobdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dobdate1))
                    {
                        lbldOB.Text = dobdate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    lbldOB.Text = "";

                lblGender.Text = dt.Rows[0]["Gender"].ToString();
            }
        }

        public void GetMemberDetails(int enqid)
        {
            objBalEnquiry.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalEnquiry.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalEnquiry.Enq_ID = Convert.ToInt32(enqid);
            dt = objBalEnquiry.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["FName"].ToString();
                lblContact.Text = dt.Rows[0]["Contact1"].ToString();

                if (dt.Rows[0]["DOB"].ToString() != "")
                {
                    DateTime dobdate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                    DateTime dobdate1;
                    if (DateTime.TryParseExact(dobdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dobdate1))
                    {
                        lbldOB.Text = dobdate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    lbldOB.Text = "";

                lblGender.Text = dt.Rows[0]["Gender"].ToString();
            }
        }

        public void bindDDLCallRespond()
        {
            try
            {
                obBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalCallRespondMaster.Select_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlCallPesponse.DataSource = dt;
                    ddlCallPesponse.DataValueField = "CallRespond_AutoID";
                    ddlCallPesponse.DataTextField = "Name";
                    ddlCallPesponse.DataBind();
                    ddlCallPesponse.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Call Respond Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLFollowupType()
        {
            try
            {
                objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objEnqFlw.Select_FollowupTypeMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
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
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
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
         
        public void AssignDateAndTime()
        { 
             DateTime todaydate;
             if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
             {
                 lblFollwupDate.Text = todaydate.ToString("dd-MM-yyyy");
                 txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
                 
                 DateTime utcTime = DateTime.UtcNow;
                 TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                 DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                 txtNextFollowupTime.Text = localTime.ToString("HH:mm");
             }
        }
        //private void EnquiryInfo()
        //{
        //    try
        //    {
        //        dt = objEnqFlw.EnryInfoById();
        //        if (dt.Rows.Count > 0)
        //        {
        //            lblName.Text = dt.Rows[0]["FName"].ToString();
        //            lblContact.Text = dt.Rows[0]["Contact1"].ToString();

        //            if (dt.Rows[0]["DOB"].ToString() != "")
        //            {
        //                DateTime dobdate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
        //                DateTime dobdate1;
        //                if (DateTime.TryParseExact(dobdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dobdate1))
        //                {
        //                    lbldOB.Text = dobdate1.ToString("dd-MM-yyyy");
        //                }
        //            }
        //            else
        //                lbldOB.Text = "";
        //            lblGender.Text = dt.Rows[0]["Gender"].ToString();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);        
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandiling.SendErrorToText(ex);
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //    }
        //}


        private void AssignID()
        {
            objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objEnqFlw.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        
        private void ClearAllField()
        {
            AssignDateAndTime();
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false; 
            ddlCallPesponse.SelectedValue = "--Select--";
            //ddlCallPesponse.SelectedItem.Text = "--Select--";
            ddlRating.SelectedIndex = 0;
            //txtNextFollowupTime.Text = string.Empty;
            txtComment.Text = string.Empty;
            bindDDLFollowupType();
           
        }

        private void AddParameters()
        {
            if (Request.QueryString["FNameViewEnqFoll"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["enqautoid"]);
            }
            else if (Request.QueryString["Data"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["Auto_ID"]);
            }
            else if (Request.QueryString["Enq_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
            }
            objEnqFlw.CallResponse_AutoID = Convert.ToInt32(ddlCallPesponse.SelectedValue);
            objEnqFlw.Rating = ddlRating.SelectedValue;
            if (ddlRating.SelectedValue != "Not Interested")
            {
                DateTime NFDate;
                if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate))
                {
                    string NFDate1 = NFDate.ToString("dd-MM-yyyy");
                    objEnqFlw.NextFollowupDate = DateTime.ParseExact(NFDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
            }
            DateTime FDate;
            if (DateTime.TryParseExact(lblFollwupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FDate))
            {
                string FDate1 = FDate.ToString("dd-MM-yyyy");
                objEnqFlw.FollowupDate = DateTime.ParseExact(FDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            objEnqFlw.NextFollowupTime = Convert.ToDateTime(txtNextFollowupTime.Text.ToString());
            objEnqFlw.FollowupTime = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt"));
            objEnqFlw.Comment = Regex.Replace(txtComment.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objEnqFlw.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
            objEnqFlw.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCallPesponse.SelectedValue == "--Select--" || ddlRating.SelectedValue == "--Select--" || txtNextFollowupDate.Text == string.Empty || txtNextFollowupTime.Text == string.Empty || txtComment.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Enter All Fields','Error');", true);


                    if (ddlCallPesponse.SelectedValue == "--Select--")
                    { ddlCallPesponse.Style.Add("border", "1px solid red "); }


                    if (ddlRating.SelectedValue == "--Select--")
                    { ddlRating.Style.Add("border", "1px solid red "); }


                    if (txtNextFollowupDate.Text == "")
                    { txtNextFollowupDate.Style.Add("border", "1px solid red "); }
                   
                    if (txtComment.Text == "")
                    { txtComment.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupTime.Text == "")
                    { txtNextFollowupTime.Style.Add("border", "1px solid red "); }
                }
                else
                {
                    ddlCallPesponse.Style.Add("border", "1px solid silver  ");
                    ddlRating.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
                    txtComment.Style.Add("border", "1px solid silver  ");

                    AssignID();
                    AddParameters();

                    if (btnSave.Text == "Save")
                    {
                        objEnqFlw.Action = "INSERT";
                        res = objEnqFlw.Insert_EnquiryFollowupInformation();

                        if (res > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                            ClearAllField();
                            BindGridViewDetails();
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        objEnqFlw.Action = "Update";
                        objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(ViewState["EnqFollowup_AutoID"]);
                        res = objEnqFlw.Insert_EnquiryFollowupInformation();
                        if (res > 0)
                        {
                            btnSave.Text = "Save";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                            ClearAllField();
                            BindGridViewDetails();

                        }
                    }
                    //if (Request.QueryString["FNAllFollowupDetailsEnq"] != null)
                    //{
                    //    string Pagename = "FNAllFollowupDetailsEnq";
                    //    Response.Redirect("AllFollowup.aspx?FNAllFollowupDetailsEnq=" + Pagename);
                    //}
                    //if (Request.QueryString["MenuEnquDetails"] != null)
                    //{
                    //    Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                    //}
                    //if (Request.QueryString["MenuEnqFollowupDetails"] != null)
                    //{
                    //    Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnqFollowupDetails=" + HttpUtility.UrlEncode("MenuEnqFollowupDetails".ToString()));
                    //}
                    //if (Request.QueryString["FNameSearchPage"] != null)
                    //{
                    //    int EnqAutoId = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("SearchPage.aspx?Enq_AutoID=" + EnqAutoId + " &FNameSearchPage2=" + HttpUtility.UrlEncode("FNameSearchPage2".ToString()));
                    //}
                    //if (Request.QueryString["FNameViewEnqFoll"] != null)
                    //{
                    //    Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                    //}
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void gridBind()
        {
            dt = objEnqFlw.GetDetails();
            if (dt.Rows.Count > 0)
            {
                gvEnqFollowup.DataSource = dt;
                gvEnqFollowup.DataBind();
            }
            else
            {
                gvEnqFollowup.DataSource = dt;
                gvEnqFollowup.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvEnqFollowup.Columns[0].Visible = false;
                gvEnqFollowup.Columns[1].Visible = false;
            }
        }

        private void BindGridViewDetails()
        {
            if (ViewState["Auto_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["Auto_ID"].ToString());
            }
            else if (Request.QueryString["Enq_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
            }
            AssignID();
            objEnqFlw.Action = "BindDetailsByID";
            gridBind();

        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();
                objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(e.CommandArgument);
                objEnqFlw.Action = "DeleteByEnqFollowupAutoID";
                int i = objEnqFlw.Insert_EnquiryFollowupInformation();
                if (i > 0)
                {
                    BindGridViewDetails();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    BindGridViewDetails();
                }
                else if (i == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();
                objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(e.CommandArgument);
                objEnqFlw.Action = "GetDetailsByEnqFollowupAutoID";

                dt = objEnqFlw.GetDetails();
                if (dt.Rows.Count >= 0)
                {
                    btnSave.Text = "Update";

                    ViewState["EnqFollowup_AutoID"] = dt.Rows[0]["EnqFollowup_AutoID"].ToString();
                    ddlCallPesponse.SelectedItem.Value = dt.Rows[0]["CallRespond_AutoID"].ToString();
                    ddlCallPesponse.SelectedItem.Text = dt.Rows[0]["CallRespond"].ToString();
                    ddlRating.SelectedValue = dt.Rows[0]["Rating"].ToString();
                    string NextFollowupDate = Convert.ToDateTime(dt.Rows[0]["NextFollowupDate"]).ToString("dd-MM-yyyy");
                    txtNextFollowupDate.Text = NextFollowupDate;
                    txtNextFollowupTime.Text = dt.Rows[0]["NextFollowupTime"].ToString();
                    txtComment.Text = dt.Rows[0]["Comment"].ToString();
                    ddlExecutive.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();
                    ddlFollowupType.SelectedValue = dt.Rows[0]["FollowupType_AutoID"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllField();
            btnSave.Text = "Save";
            ddlCallPesponse.Style.Add("border", "1px solid silver  ");
            ddlRating.Style.Add("border", "1px solid silver  ");
            txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
            txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
            txtComment.Style.Add("border", "1px solid silver  ");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEnquiry.aspx",false);
        }

        protected void gvEnqFollowup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnqFollowup.PageIndex = e.NewPageIndex;
            BindGridViewDetails();
            gvEnqFollowup.DataBind();
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
        }

        protected void ddlRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRating.SelectedItem.Text == "Not Interested")
            {
                txtNextFollowupDate.Enabled = false;
                txtNextFollowupDate.Focus();
            }
            else
            {
                txtNextFollowupDate.Enabled = true;
                txtNextFollowupDate.Focus();

            }
        }
    }
}