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


namespace NDOnlineGym_2017
{
    public partial class AddMember : System.Web.UI.Page
    {
        DataTable table=new DataTable();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();

        BalAddMember objMemberDetails = new BalAddMember();
        DataTable dt = new DataTable();
        string newfileName = string.Empty;
        string serverfilrpath = string.Empty;
        string serverfilrpath1 = string.Empty;

        int Member_ID, EnqId;
        static int flag;
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                bindDDLExecutive();
                setExecutive();
                AssignTodaysDate();
                txtMemberID.Focus();
                if (btnSave.Text == "Save")
                {
                    Get_MemberID1();
                }
                bindDDLOccupation();

                if (Request.QueryString["MemberID"] != null)
                {
                    Member_ID = Convert.ToInt32(Request.QueryString["MemberID"]);
                    BindMemberData();
                    ViewState["MemberID"] = Member_ID;
                }

                if (Request.QueryString["Enq_ID"] != null)
                {
                    EnqId = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    ViewState["EnqId"] = EnqId;
                    BindEnquiryData();                    
                }

            }

            if (!this.IsPostBack)
            {
                if (Request.InputStream.Length > 0)
                {
                    using (StreamReader reader = new StreamReader(Request.InputStream))
                    {
                        string hexString = Server.UrlEncode(reader.ReadToEnd());
                        string imageName = txtMemberID.Text.Trim() + "MemberID_" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
                        string imagePath = string.Format("~/MemberPhoto/{0}.png", imageName);
                       // string imagePath = string.Format("~/MemberPhoto/{0}.png");
                        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                        Session["CapturedImage"] = ResolveUrl(imagePath);
                        //ViewState["imagekT"] = imagePath.ToString();
                        Session["KT"] = imagePath.ToString();

                    }
                }
            }
        }

        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            string url = HttpContext.Current.Session["CapturedImage"].ToString();
            HttpContext.Current.Session["CapturedImage"] = null;
            return url;
        }

        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
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
               // ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void setExecutive()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
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

        private void BindEnquiryData()
        {
           try
           {
               objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
               objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
               objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
               objMemberDetails.Enq_AutoID = Convert.ToInt32(ViewState["EnqId"]);
               table = objMemberDetails.SelectByID_EnquiryInformation();
               {                   
                   txtFirstName.Text = table.Rows[0]["FName"].ToString();
                   txtLastName.Text = table.Rows[0]["LName"].ToString();
                   txtContact1.Text = table.Rows[0]["Contact1"].ToString();
                   txtContact2.Text = table.Rows[0]["Contact2"].ToString();
                   txtWatsapp.Text = table.Rows[0]["WhatsAppNo"].ToString();
                   txtEmailID.Text = table.Rows[0]["Email"].ToString();
                   imgMember.ImageUrl = table.Rows[0]["ImagePath"].ToString();
                   if(ViewState["EnqId"] != null)
                        imgIDProof.ImageUrl = "";
                   else
                       imgIDProof.ImageUrl = table.Rows[0]["IDProofPath"].ToString();

                   if (table.Rows[0]["DOB"].ToString() != "")
                   {
                       DateTime DOBDate = Convert.ToDateTime(table.Rows[0]["DOB"].ToString());
                       DateTime DOBDate1;
                       if (DateTime.TryParseExact(DOBDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOBDate1))
                       {
                           txtDOB.Text = DOBDate1.ToString("dd-MM-yyyy");
                       }
                   }
                   else
                       txtDOB.Text = "";

                   txtAddress.Text = table.Rows[0]["Address"].ToString();
                   ddlGender.SelectedValue = table.Rows[0]["Gender"].ToString();
                   imgMember.ImageUrl = table.Rows[0]["ImagePath"].ToString();
                   imgIDProof.ImageUrl = table.Rows[0]["IDProofPath"].ToString();
                   ViewState["imagepath"] = table.Rows[0]["ImagePath"].ToString();
                   ViewState["IDProofPath"] = table.Rows[0]["IDProofPath"].ToString();

               }

           }
           catch (Exception ex)
           {
               ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
           }
        }
        #endregion

        #region Bind MemberDate
        public void BindMemberData()
        {
            try
            {
                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                btnSave.Text = "Update";
                objMemberDetails.Member_AutoID = Member_ID;
                //  objMemberDetails.Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                table = objMemberDetails.SelectByID_MemberInformation();
                if (table.Rows.Count > 0)
                {
                    txtMemberID.Text = table.Rows[0]["Member_ID1"].ToString();
                    txtFirstName.Text = table.Rows[0]["FName"].ToString();
                    txtLastName.Text = table.Rows[0]["LName"].ToString();
                    txtContact1.Text = table.Rows[0]["Contact1"].ToString();
                    txtContact2.Text = table.Rows[0]["Contact2"].ToString();

                    if (table.Rows[0]["AniversaryDate"].ToString() != "")
                    {
                        DateTime AnnniDate = Convert.ToDateTime(table.Rows[0]["AniversaryDate"].ToString());
                        DateTime AnnniDate1;
                        if (DateTime.TryParseExact(AnnniDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out AnnniDate1))
                        {
                            txtAnniversary.Text = AnnniDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtAnniversary.Text = "";
                    //txtAnniversary.Text = table.Rows[0]["AniversaryDate"].ToString();
                    txtWatsapp.Text = table.Rows[0]["WhatsAppNo"].ToString();


                    if (table.Rows[0]["RegDate"].ToString() != "")
                    {
                        DateTime RegDate = Convert.ToDateTime(table.Rows[0]["RegDate"].ToString());
                        DateTime RegDate1;
                        if (DateTime.TryParseExact(RegDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out RegDate1))
                        {
                            txtRegDate.Text = RegDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtRegDate.Text = "";
                    txtEmailID.Text = table.Rows[0]["Email"].ToString();

                    if (table.Rows[0]["DOB"].ToString() != "")
                    {
                        DateTime DOBDate = Convert.ToDateTime(table.Rows[0]["DOB"].ToString());
                        DateTime DOBDate1;
                        if (DateTime.TryParseExact(DOBDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOBDate1))
                        {
                            txtDOB.Text = DOBDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtDOB.Text = "";

                    txtAddress.Text = table.Rows[0]["Address"].ToString();
                    ddlMaritalStatus.SelectedValue = table.Rows[0]["MariatalStatus"].ToString();
                    ddlBloodGroup.SelectedValue = table.Rows[0]["BloodGroup"].ToString();
                    ddlSMSStatus.Text = table.Rows[0]["SMSStatus"].ToString();
                    txtCardNumber.Text = table.Rows[0]["AccessCardNo"].ToString();
                    ddlGender.SelectedValue = table.Rows[0]["Gender"].ToString();
                    ddlStatus.SelectedValue = table.Rows[0]["Status"].ToString();
                    txtHealthDetails.Text = table.Rows[0]["HealthDetails"].ToString();
                    txtIDProof.Text = table.Rows[0]["IDProofName"].ToString();
                    imgMember.ImageUrl = table.Rows[0]["ImagePath"].ToString();
                    ViewState["imagepath"] = table.Rows[0]["ImagePath"].ToString();
                    ddlOccupation.SelectedItem.Value = table.Rows[0]["Occupation_AutoID"].ToString();
                    imgIDProof.ImageUrl = table.Rows[0]["IDProofPath"].ToString();
                    ViewState["IDProofPath"] = table.Rows[0]["IDProofPath"].ToString();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion


        #region BIND GRID
        public void GridBind()
        {
            try
            {
                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                table = objMemberDetails.Select_All();

                if (table.Rows.Count > 0)
                {

                    gvMember.Visible = true;
                    gvMember.DataSource = table;
                    gvMember.DataBind();
                }
                else
                {
                    gvMember.Visible = false;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvMember.Columns[0].Visible = true;
                    gvMember.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvMember.Columns[0].Visible = true;
                    gvMember.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvMember.Columns[0].Visible = true;
                    gvMember.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvMember.Columns[0].Visible = true;
                    gvMember.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvMember.Columns[0].Visible = false;
                    gvMember.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion


        #region GetMember_ID
        public void Get_MemberID1()
        {
            try
            {
                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objMemberDetails.Get_MemberID1();
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Bind Occupation
        public void bindDDLOccupation()
        {
            try
            {
                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objMemberDetails.SELECT_Occupation();
                if (dt.Rows.Count > 0)
                {
                    ddlOccupation.DataSource = dt;
                    ddlOccupation.Items.Clear();
                    ddlOccupation.DataValueField = "Occupation_AutoID";
                    ddlOccupation.DataTextField = "Name";
                    ddlOccupation.DataBind();
                    ddlOccupation.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                   //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Clear_Record
        public void ClearRecord()
        {
            ViewState["EnqId"] = null;
            txtMemberID.Text = "";
            txtFirstName.Text = "";
            txtContact1.Text = "";
            txtContact2.Text = "";
            txtLastName.Text = "";
            txtEmailID.Text = "";
            ddlGender.SelectedValue = "--Select--";
            //txtRegDate.Text ="";
            txtDOB.Text = "";
            txtAnniversary.Text = "";
            ddlMaritalStatus.SelectedValue = "--Select--";
            ddlBloodGroup.SelectedValue = "--Select--";
            txtWatsapp.Text = "";
            txtEmailID.Text = "";
            txtSearch.Text = "";
            ddlcategory.SelectedIndex = 0;
            txtAddress.Text = "";
            ddlOccupation.SelectedIndex = 0;
            txtHealthDetails.Text = "";
            txtIDProof.Text = "";
            ddlSMSStatus.SelectedValue = "Yes";
            txtCardNumber.Text = "";          
            btnSave.Text = "Save";
            imgMember.ImageUrl = "";
            imgIDProof.ImageUrl = "";
            AssignTodaysDate();
            Get_MemberID1();
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;

        }
        #endregion


        #region Assign_Date
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtRegDate.Text = todaydate.ToString("dd-MM-yyyy");
                //txtDOB.Text = todaydate.ToString("dd-MM-yyyy");
                //txtAnniversary.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        #endregion


        #region Save_Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
            AddParameters();
            ImageUplode();
            IDProofUplode();
            if (fileLogo.HasFile)
            {
                serverfilrpath1 = ViewState["serverfilrpath1"].ToString();
                objMemberDetails.ImagePath = serverfilrpath1;
            }
            else
            {
                
                if (ViewState["imagepath"] != null)
                {
                    objMemberDetails.ImagePath = ViewState["imagepath"].ToString();
                }
                else
                {
                    if (Session["KT"] != null)
                    {
                        objMemberDetails.ImagePath = Session["KT"].ToString();
                    }
                    //objMemberDetails.ImagePath = Session["KT"].ToString();// +txtFirstName.Text.Trim() + "_" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss"); 
                }

                if (Session["KT"] != null)
                {
                     objMemberDetails.ImagePath = Session["KT"].ToString();
                }
            }

            if (fileIDProof.HasFile)
            {
                if (txtIDProof.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter IDProof Name !!!','Error');", true);
                    return;
                    fileIDProof.Focus();
                }
                else
                {
                    serverfilrpath = ViewState["serverfilrpath"].ToString();
                    objMemberDetails.IDProofPath = serverfilrpath;
                }
            }
            else
            {
                if (imgIDProof.ImageUrl != "")
                {
                    if (txtIDProof.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter IDProof Name !!!','Error');", true);
                        return;
                    }
                    else
                        objMemberDetails.IDProofPath = ViewState["IDProofPath"].ToString();
                }
            }
            if (btnSave.Text == "Save")
            {
                if (txtMemberID.Text != "")
                {
                    objMemberDetails.Action = "CheckID";
                    int res1 = objMemberDetails.checkID();
                    if (res1 > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member ID Already Assign !!!','Error');", true);
                        Get_MemberID1();
                        return;
                    }
                }
                objMemberDetails.Action = "Check_Contact_Save";
                int res2 = objMemberDetails.Contactcheck();
                if (res2 > 0)
                {
                    txtContact1.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                    return;
                }
                else
                {
                    txtContact2.Focus();
                }


                objMemberDetails.Action = "Insert_Member";
                int res = objMemberDetails.Insert_MemberDetails();
                if (res > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                    dt = objMemberDetails.GetContactMaxID();
                    string contact = dt.Rows[0]["Contact1"].ToString();
                    Response.Redirect("~/demoCourse.aspx?Contact1=" + contact);
                    ClearRecord();
                    Session["KT"] = null;
                    if (Request.QueryString["FNAllFollowupDetailsEnq"] != null)
                    {
                        string Pagename = "FNAllFollowupDetailsEnq";
                        Response.Redirect("AllFollowup.aspx?FNAllFollowupDetailsEnq=" + Pagename);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Saved  !!!','Error');", true);
                    return;
                }
            }
            else
            {
                if (txtMemberID.Text != "")
                {
                    objMemberDetails.Action = "CheckID_OnUpdate";
                    if (Request.QueryString["MemberID"] != null)
                        objMemberDetails.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberID"]);
                    else
                        objMemberDetails.Member_AutoID = Convert.ToInt32(ViewState["Member_AutoID"]);
                    int res = objMemberDetails.checkID();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member ID Already Assign !!!','Error');", true);
                        Get_MemberID1();
                        return;
                    }
                }
                if (Request.QueryString["MemberID"] != null)
                    objMemberDetails.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberID"]);
                else
                    objMemberDetails.Member_AutoID = Convert.ToInt32(ViewState["Member_AutoID"]);
                objMemberDetails.Action = "Check_Contact_Update";
                int res3=objMemberDetails.Contactcheck();
                if (res3 > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                    return;
                }
                else
                {
                    txtContact2.Focus();
                }

                objMemberDetails.Action = "Edit";
                int re = objMemberDetails.Insert_MemberDetails();
                if (re > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                    
                    if (Request.QueryString["FNameMemDetails"] != null)
                    {
                        int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                        Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                    }
                    else if (Request.QueryString["FNameSearchPage"] != null)
                    {
                        int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                        Response.Redirect("SearchPage.aspx?Member_AutoID=" + memberid + " &FNameSearchPage1=" + HttpUtility.UrlEncode("FNameSearchPage1".ToString()));
                    }
                    else if (Request.QueryString["MemberID"] != null)
                    {
                        int Member_AutoID = Convert.ToInt32(Request.QueryString["MemberID"]);
                        Response.Redirect("MemberProfile.aspx?MemberId=" + HttpUtility.UrlEncode(Member_AutoID.ToString()));
                    }
                    
                    ClearRecord();
                    GridBind();
                    Get_MemberID1();
                    Session["KT"] = null;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Updated !!!','Error');", true);
                }
            }
        }

#endregion


        #region Add_Parameter
        protected void AddParameters()
        {
            objMemberDetails.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedItem.Value);
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            objMemberDetails.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
            DateTime Regdate;
            if (DateTime.TryParseExact(txtRegDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Regdate))
            {
                string Regdate1 = Regdate.ToString("dd-MM-yyyy");
                objMemberDetails.RegDate = DateTime.ParseExact(Regdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }
            if (txtAnniversary.Text != "")
            {
                DateTime AnniDate;
                if (DateTime.TryParseExact(txtAnniversary.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out AnniDate))
                {
                    string AnniDate1 = AnniDate.ToString("dd-MM-yyyy");
                    objMemberDetails.AniversaryDate = DateTime.ParseExact(AnniDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
            }
            else
                objMemberDetails.AniversaryDate = null;

            if (txtDOB.Text != "")
            {
                DateTime DOBDate;
                if (DateTime.TryParseExact(txtDOB.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOBDate))
                {
                    string DOBDate1 = DOBDate.ToString("dd-MM-yyyy");
                    objMemberDetails.DOB = DateTime.ParseExact(DOBDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
            }
            else
                objMemberDetails.DOB = null;

            objMemberDetails.FName = txtFirstName.Text;
            objMemberDetails.LName = txtLastName.Text;
            if (ddlMaritalStatus.SelectedValue != "--Select--")
            {
                objMemberDetails.MariatalStatus = ddlMaritalStatus.SelectedValue;
            }
            else objMemberDetails.MariatalStatus = null;

            if (ddlGender.SelectedValue != "--Select--")
            {
                objMemberDetails.Gender = ddlGender.SelectedValue;
            }
            else objMemberDetails.Gender = null;

            if (ddlBloodGroup.SelectedValue != "--Select--")
            {
                objMemberDetails.BloodGroup = ddlBloodGroup.SelectedValue;
            }
            else objMemberDetails.BloodGroup = null;

            objMemberDetails.Contact1 = txtContact1.Text;
            objMemberDetails.Contact2 = txtContact2.Text;
            objMemberDetails.WhatsAppNo = txtWatsapp.Text;
            objMemberDetails.Email = txtEmailID.Text;
            objMemberDetails.Address = txtAddress.Text;
           
            objMemberDetails.HealthDetails = txtHealthDetails.Text;
            objMemberDetails.AccessCardNo = txtCardNumber.Text;
            objMemberDetails.Status = ddlStatus.Text;
            objMemberDetails.IDProofName = txtIDProof.Text;
            objMemberDetails.AccessCardNo = txtCardNumber.Text;
            objMemberDetails.SMSStatus = ddlSMSStatus.Text;
           // objMemberDetails.MembershipStatus = "Group_Owner";
            objMemberDetails.MembershipStatus = "Member";
            if (ddlOccupation.Text != "--Select--")
            {
                objMemberDetails.Occupation_AutoID = Convert.ToInt32(ddlOccupation.SelectedValue);
            }
            else
                objMemberDetails.Occupation_AutoID = null;

            if (Convert.ToInt32(ViewState["EnqId"]) != 0)
            {
                objMemberDetails.Enq_AutoID = Convert.ToInt32(ViewState["EnqId"]);
            }
            else
            {
                objMemberDetails.Enq_AutoID = 0;
            }

        }
        #endregion


        #region Image_Upload
        public void ImageUplode()
        {
            if ((fileLogo.PostedFile != null) && (fileLogo.PostedFile.ContentLength > 0))
            {
                Guid uid = Guid.NewGuid();
                string fn = System.IO.Path.GetFileName(fileLogo.PostedFile.FileName);
                DateTime dt = DateTime.Now;
                newfileName = txtFirstName.Text.Trim() + "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                string fileName = Path.GetFileName(fileLogo.PostedFile.FileName);
                string primaryFileName = Path.GetFileNameWithoutExtension(fileName);
                string fileExtentionName = Path.GetExtension(fileName);
                string SaveLocation = Server.MapPath("/MemberPhoto/") + newfileName + fileExtentionName;
                try
                {
                    string fileExtention = fileLogo.PostedFile.ContentType;
                    int fileLenght = fileLogo.PostedFile.ContentLength;
                    if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                    {
                        //if (fileLenght <= 1048576)
                        //{
                        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fileLogo.PostedFile.InputStream);
                        System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);
                        // Saving image in jpeg format
                        objImage.Save(SaveLocation, ImageFormat.Jpeg);
                        ViewState["serverfilrpath1"] = "/MemberPhoto/" + newfileName + fileExtentionName;
                    }
                    else
                    {
                       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invaild Format !!!','Error');", true);
                        txtFirstName.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    txtFirstName.Focus();
                    return;
                }
            }
        }


        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            //var newWidth = (int)(image.Width * ratio);
            //var newHeight = (int)(image.Height * ratio);

            var newWidth = (int)(85);
            var newHeight = (int)(75);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public void IDProofUplode()
        {
            if ((fileIDProof.PostedFile != null) && (fileIDProof.PostedFile.ContentLength > 0))
            {
                Guid uid = Guid.NewGuid();
                string fn = System.IO.Path.GetFileName(fileIDProof.PostedFile.FileName);
                DateTime dt = DateTime.Now;
                newfileName = txtIDProof.Text.Trim() + "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                string fileName = Path.GetFileName(fileIDProof.PostedFile.FileName);
                string primaryFileName = Path.GetFileNameWithoutExtension(fileName);
                string fileExtentionName = Path.GetExtension(fileName);
                string SaveLocation = Server.MapPath("/IDProofImage/") + newfileName + fileExtentionName;
                try
                {
                    string fileExtention = fileIDProof.PostedFile.ContentType;
                    int fileLenght = fileIDProof.PostedFile.ContentLength;
                    if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                    {
                        //if (fileLenght <= 1048576)
                        //{
                        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fileIDProof.PostedFile.InputStream);
                        System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);
                        // Saving image in jpeg format
                        objImage.Save(SaveLocation, ImageFormat.Jpeg);
                        ViewState["serverfilrpath"] = "/IDProofImage/" + newfileName + fileExtentionName;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invaild Format !!!','Error');", true);
                        txtFirstName.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    txtFirstName.Focus();
                    return;
                }
            }
        }

        #endregion


        #region Edit_Command
        protected void btnEdit_Command1(object sender, CommandEventArgs e)
        {
            try
            {
                ViewState["Member_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMemberDetails.Login_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                btnSave.Text = "Update";
                objMemberDetails.Member_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                table = objMemberDetails.SelectByID_MemberInformation();
                if (table.Rows.Count > 0)
                {
                    
                    txtMemberID.Text = table.Rows[0]["Member_ID1"].ToString();
                    txtFirstName.Text = table.Rows[0]["FName"].ToString();
                    txtLastName.Text = table.Rows[0]["LName"].ToString();
                    txtContact1.Text = table.Rows[0]["Contact1"].ToString();
                    txtContact2.Text = table.Rows[0]["Contact2"].ToString();

                    if (table.Rows[0]["AniversaryDate"].ToString() != "")
                    {
                        DateTime AnnniDate = Convert.ToDateTime(table.Rows[0]["AniversaryDate"].ToString());
                        DateTime AnnniDate1;
                        if (DateTime.TryParseExact(AnnniDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out AnnniDate1))
                        {
                            txtAnniversary.Text = AnnniDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtAnniversary.Text = "";

                    //txtAnniversary.Text = table.Rows[0]["AniversaryDate"].ToString();
                    txtWatsapp.Text = table.Rows[0]["WhatsAppNo"].ToString();


                    if (table.Rows[0]["RegDate"].ToString() != "")
                    {
                        DateTime RegDate = Convert.ToDateTime(table.Rows[0]["RegDate"].ToString());
                        DateTime RegDate1;
                        if (DateTime.TryParseExact(RegDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out RegDate1))
                        {
                            txtRegDate.Text = RegDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtRegDate.Text = "";
                    txtEmailID.Text = table.Rows[0]["Email"].ToString();

                    if (table.Rows[0]["DOB"].ToString() != "")
                    {
                        DateTime DOBDate = Convert.ToDateTime(table.Rows[0]["DOB"].ToString());
                        DateTime DOBDate1;
                        if (DateTime.TryParseExact(DOBDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOBDate1))
                        {
                            txtDOB.Text = DOBDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtDOB.Text = "";

                    txtAddress.Text = table.Rows[0]["Address"].ToString();
                    if (table.Rows[0]["MariatalStatus"].ToString() != "")
                        ddlMaritalStatus.SelectedValue = table.Rows[0]["MariatalStatus"].ToString();
                    else
                        ddlMaritalStatus.SelectedValue = "--Select--";

                    if (table.Rows[0]["BloodGroup"].ToString() != "")
                        ddlBloodGroup.SelectedValue = table.Rows[0]["BloodGroup"].ToString();
                    else
                        ddlBloodGroup.SelectedValue = "--Select--";

                    //ddlBloodGroup.SelectedValue = table.Rows[0]["BloodGroup"].ToString();
                    ddlSMSStatus.SelectedValue = table.Rows[0]["SMSStatus"].ToString();
                    txtCardNumber.Text = table.Rows[0]["AccessCardNo"].ToString();
                    ddlGender.SelectedValue = table.Rows[0]["Gender"].ToString();
                    ddlStatus.SelectedValue = table.Rows[0]["Status"].ToString();
                    txtHealthDetails.Text = table.Rows[0]["HealthDetails"].ToString();
                    txtIDProof.Text = table.Rows[0]["IDProofName"].ToString();
                    imgMember.ImageUrl = table.Rows[0]["ImagePath"].ToString();
                    ViewState["imagepath"] = table.Rows[0]["ImagePath"].ToString();
                    imgIDProof.ImageUrl = table.Rows[0]["IDProofPath"].ToString();
                    ViewState["IDProofPath"] = table.Rows[0]["IDProofPath"].ToString();
                    ddlOccupation.SelectedValue = table.Rows[0]["Occupation_AutoID"].ToString();
                    ddlExecutive.SelectedValue = table.Rows[0]["Executive_ID"].ToString();
                    //ddlOccupation.SelectedItem.Value = table.Rows[0]["Occupation_AutoID"].ToString();
                    txtFirstName.Focus();
                }
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        #endregion


        #region Delete_Command
        protected void btnDelete_Command1(object sender, CommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                if (id > 0)
                {
                    objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                    objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objMemberDetails.Action = "Delete";
                    objMemberDetails.Member_AutoID = id;
                    int res = objMemberDetails.Delete_Staff();
                    if (res > 0)
                    {
                       ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully!!!','Success');", true);
                    }
                        GridBind();
                }
                else
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Deleted!!!.','Error');", true);
                }
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Can Not Be Deleted Because It Is Already Assigned !!!.','Error');", true);
            }
        }
#endregion

        public void SearchByCategory()
        {
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Action = "Searching";
            if (ddlcategory.SelectedValue == "First Name")
            {
                objMemberDetails.FName = txtSearch.Text;
                objMemberDetails.Category = "First Name";
                //flag = 1;
            }
            if (ddlcategory.SelectedValue == "Last Name")
            {
                objMemberDetails.LName = txtSearch.Text;
                objMemberDetails.Category = "Last Name";
                //flag = 2;
            }
            if (ddlcategory.SelectedValue == "Contact")
            {
                objMemberDetails.con = txtSearch.Text;
                objMemberDetails.Category = ddlcategory.Text;
               // flag = 3;
            }
            if (ddlcategory.SelectedValue == "Status")
            {
                objMemberDetails.Status = txtSearch.Text;
                objMemberDetails.Category = ddlcategory.Text;
                //flag = 4;
            }
            if (ddlcategory.SelectedValue == "Member_ID")
            {
                objMemberDetails.Member_ID1 = Convert.ToInt32(txtSearch.Text);
                objMemberDetails.Category = "Member_ID";
                //flag = 5;
            }
            if (ddlcategory.SelectedValue == "Gender")
            {
                objMemberDetails.Gender = txtSearch.Text;
                objMemberDetails.Category = ddlcategory.Text;
                //flag = 6;
            }
            flag = 1;
            table = objMemberDetails.SearchCategory();
            if (table.Rows.Count > 0)
            {
                gvMember.Visible = true;
                gvMember.DataSource = table;
                gvMember.DataBind();
            }
            else
            {
                gvMember.Visible = true;
                gvMember.DataSource = null;
                gvMember.DataBind();
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvMember.Columns[0].Visible = true;
                gvMember.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvMember.Columns[0].Visible = true;
                gvMember.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvMember.Columns[0].Visible = true;
                gvMember.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvMember.Columns[0].Visible = true;
                gvMember.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvMember.Columns[0].Visible = false;
                gvMember.Columns[1].Visible = false;
            }
        }

        #region Search_Record
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                objMemberDetails.Action = "SearchBYCategory";
                if (ddlcategory.Text != "--Select--" && ddlcategory.Text != "")
                {
                    SearchByCategory();
                }
                else
                {
                    GridBind();
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region Image_Remove
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            imgMember.ImageUrl = "";
            if (ViewState["imagepath"] != null)
            {
                string img = ViewState["imagepath"].ToString();
                File.Delete(Server.MapPath(img));
            }
            else
            {
                ViewState["imagepath"] = null;
            }
        }

        #endregion

        #region Image_Remove
        protected void btnRemove1_Click(object sender, EventArgs e)
        {
            //imgIDProof.ImageUrl = "";

            imgIDProof.ImageUrl = "";

            if (ViewState["IDProofPath"] != null)
            {
                string img = ViewState["IDProofPath"].ToString();
                File.Delete(Server.MapPath(img));
            }
            else
            {
                ViewState["IDProofPath"] = null;
            }

        }
        #endregion


        #region Page Indexing
        protected void gvMember_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvMember.PageIndex = e.NewPageIndex;
                SearchByCategory();
            }
            else
            {
                gvMember.PageIndex = e.NewPageIndex;
                GridBind();
            }

        }
#endregion


        #region Clear_Button
        protected void btnClear_Click(object sender, EventArgs e)
        {
            gvMember.Visible = false;
            ClearRecord();

        }
        #endregion


        #region Check ID

        public void CheckID()
        {
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
            if (btnSave.Text == "Save")
            {
                if (txtMemberID.Text != "")
                {
                    objMemberDetails.Action = "CheckID";
                    int res = objMemberDetails.checkID();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member ID Already Assign !!!','Error');", true);
                        Get_MemberID1();
                        return;
                    }
                }
            }
            else
            {
                if (txtMemberID.Text != "")
                {
                    objMemberDetails.Action = "CheckID_OnUpdate";
                    if(Request.QueryString["MemberID"] != null)
                        objMemberDetails.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberID"]);
                    else
                        objMemberDetails.Member_AutoID = Convert.ToInt32(ViewState["Member_AutoID"]);
                    int res = objMemberDetails.checkID();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member ID Already Assign !!!','Error');", true);
                        Get_MemberID1();
                        return;
                    }
                }
            }
        }

        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            CheckID();

            if (txtMemberID.Text == "")
            {
                objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objMemberDetails.Get_MemberID1();
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
            }
        }

        #endregion


        #region Contact Check
        public void Contact_Check()
        {

            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
             objMemberDetails.Contact1 = txtContact1.Text;
            if (btnSave.Text == "Save")
            {
                objMemberDetails.Action = "Check_Contact_Save";
                int res = objMemberDetails.Contactcheck();
                if (res > 0)
                {
                    txtContact1.Focus();
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                    return;
                }
                else
                {
                    txtContact2.Focus();
                }
            }
            else
            {
                if (Request.QueryString["MemberID"] != null)
                    objMemberDetails.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberID"]);
                else
                    objMemberDetails.Member_AutoID = Convert.ToInt32(ViewState["Member_AutoID"]);
                objMemberDetails.Action = "Check_Contact_Update";
                int res=objMemberDetails.Contactcheck();
                if (res > 0)
                {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Already Assign !!!','Error');", true);
                    return;
                }
                else
                {
                    txtContact2.Focus();
                }

            }

    }


        protected void txtContact1_TextChanged(object sender, EventArgs e)
        {
           Contact_Check();
        }
        #endregion

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
            ddlExecutive.Focus();
        }

        protected void btnAddFromEnquiry_Click(object sender, EventArgs e)
        {
            string page = "AddMember";
            Response.Redirect("AddEnquiry.aspx?AddMember=" + page);
        }

        protected void ddlMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaritalStatus.SelectedValue == "Married")
            {
                txtAnniversary.Enabled = true;
                txtAnniversary.Focus();
            }
            else
            {
                txtAnniversary.Enabled = false;
                ddlBloodGroup.Focus();
            }
        }

    }
    }
