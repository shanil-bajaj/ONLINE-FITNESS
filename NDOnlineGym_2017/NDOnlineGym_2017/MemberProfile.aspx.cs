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
using DataAccessLayer;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Reflection;

namespace NDOnlineGym_2017
{
    public partial class MemberProfile : System.Web.UI.Page
    {
        static int flagid;
        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();
        BalCallRespondMaster obBalCallRespondMaster = new BalCallRespondMaster();
        BalFollowupTypeMaster obBalFollowupTypeMaster = new BalFollowupTypeMaster();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dataTable = new DataTable();
        BalFollowup objFollowup = new BalFollowup();
        int res;
        static int MemberAutoID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
                bindDDLCallRespond();
                BindFollowupTypeDDL();  // Bind Followup Type Drop Down List     
                bindDDLExecutive();    // Assign Executive Name
                setExecutive();
                AssignDateAndTime();
                bindMemeber();
                txtMemberID.Focus();
                if (Request.QueryString["MemberId"] != null)
                {
                    objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objMember.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                    objMember.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberId"]);
                    MemberDetailsinfo();
                    CourseDetails();
                  //  AccountBalanceDetails();
                    AccountDetails();
                    GridBind();                
                    lnkbtnEdit.Focus();

                    RemoveQueryStringParams("MemberId");
                }
                else
                {
                    bindMemeber();
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    lnkbtnEdit.Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    lnkbtnEdit.Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    lnkbtnEdit.Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    lnkbtnEdit.Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    lnkbtnEdit.Visible = false;
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

        #region -------- Assign Company and Branch ID ------------
        private void AssignID()
        {
            objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objFollowup.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }
        #endregion

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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('First Insert Call Respond Master !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AssignDateAndTime()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                txtNextFollowupTime.Text = localTime.ToString("HH:mm");
            }
        }
        #region ------Bind Followup Type --------
        private void BindFollowupTypeDDL()
        {
            try
            {
                AssignID();
                objFollowup.Action = "Select_FollowupType";
                dataTable = objFollowup.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    ddlFollowupType.DataSource = dataTable;
                    ddlFollowupType.Items.Clear();
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion 

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
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('First Insert Staff !!!','Information');", true);
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

        public void GridBind()
        {
            try
            {
                objMember.Action = "Select_All";
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                if (Request.QueryString["MemberId"] != null)
                {
                    objMember.Member_ID1 = Convert.ToInt32(Request.QueryString["MemberId"]);
                }
                else
                {
                    Member_Auto_ID();
                    objMember.Member_ID1 = MemberAuto_ID;
                }
                dt = objMember.Select_All();
                if (dt.Rows.Count > 0)
                {
                    RepterDetails.DataSource = dt;
                    RepterDetails.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                return;
            }

        }

        public void bindMemeber()
        {
            try
            {
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                dt = objMember.bindMemeber();
                if (dt.Rows.Count > 0)
                {
                    ddlMemberName.DataSource = dt;
                    ddlMemberName.Items.Clear();
                    ddlMemberName.DataValueField = "Member_AutoID";
                    ddlMemberName.DataTextField = "Name";
                    ddlMemberName.DataBind();
                    ddlMemberName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlMemberName.SelectedItem.Value = "--Select--";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                return;
            }
        }

        protected void btnInformation_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            btnInformation.CssClass = "btn-information-section btn-information-section-selected";
            btnCourse.CssClass = "btn-information-section";
            btnAccountDetails.CssClass = "btn-information-section";
            btnIdProof.CssClass = "btn-information-section";
        }

        protected void btnCourse_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            btnInformation.CssClass = "btn-information-section";
            btnCourse.CssClass = "btn-information-section btn-information-section-selected";
            btnAccountDetails.CssClass = "btn-information-section";
            btnIdProof.CssClass = "btn-information-section";
        }

        protected void btnAccountDetails_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            btnInformation.CssClass = "btn-information-section";
            btnCourse.CssClass = "btn-information-section";
            btnAccountDetails.CssClass = "btn-information-section btn-information-section-selected";
            btnIdProof.CssClass = "btn-information-section";
        }

        protected void btnIdProof_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            btnInformation.CssClass = "btn-information-section";
            btnCourse.CssClass = "btn-information-section";
            btnAccountDetails.CssClass = "btn-information-section";
            btnIdProof.CssClass = "btn-information-section btn-information-section-selected";
        }
        string blockUn;
        public void AttachedMemberInformation()
        {
            txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
            txtContact.Text = dt.Rows[0]["Contact1"].ToString();
            ddlMemberName.SelectedValue = dt.Rows[0]["Member_AutoID"].ToString();
            ddlMemberName.SelectedItem.Text = dt.Rows[0]["Name"].ToString();

            lblMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
            lblFirstN.Text = dt.Rows[0]["FName"].ToString();
            lblLastN.Text = dt.Rows[0]["LName"].ToString();
            lblContact.Text = dt.Rows[0]["Contact1"].ToString();
            lblAddress.Text = dt.Rows[0]["Address"].ToString();
            lblEmailID.Text = dt.Rows[0]["Email"].ToString();
            lblWhatsapp.Text = dt.Rows[0]["WhatsAppNo"].ToString();
            lblGender.Text = dt.Rows[0]["Gender"].ToString();
            lblMAritalS.Text = dt.Rows[0]["MariatalStatus"].ToString();
            lblOccupation.Text = dt.Rows[0]["Occupation"].ToString();
            lblHealthDetails.Text = dt.Rows[0]["HealthDetails"].ToString();
            lblBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();
            lblStatus.Text = dt.Rows[0]["Status"].ToString();
            lblSMSstatus.Text = dt.Rows[0]["SMSStatus"].ToString();
            lblMemberName.Text = dt.Rows[0]["FName"].ToString();
            ImgMemberPhoto.ImageUrl = dt.Rows[0]["ImagePath"].ToString();
            lblIDProof.Text = dt.Rows[0]["IDProofName"].ToString();
            imgIDProof.ImageUrl = dt.Rows[0]["IDProofPath"].ToString();
            ViewState["imagepath"] = dt.Rows[0]["ImagePath"].ToString();
            ViewState["imageIDProof"] = dt.Rows[0]["IDProofPath"].ToString();
            lblBlackUn.Text = dt.Rows[0]["BlockStatus"].ToString();
            blockUn = dt.Rows[0]["BlockStatus"].ToString();

            string status=dt.Rows[0]["Status"].ToString();
            if (status == "Active")
            {
                lblMemberName.CssClass = "ClassActive";
            }
            else if (status == "Deactive")
            {
                lblMemberName.CssClass = "ClassDeactive";
            }


            if (dt.Rows[0]["DOB"].ToString() != "")
            {
                DateTime Dob = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                DateTime Dob1;
                if (DateTime.TryParseExact(Dob.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Dob1))
                {
                    lblDOB.Text = Dob1.ToString("dd-MM-yyyy");
                }
            }
            else
                lblDOB.Text = "";

            if (dt.Rows[0]["AniversaryDate"].ToString() != "")
            {
                DateTime anniversary = Convert.ToDateTime(dt.Rows[0]["AniversaryDate"].ToString());
                DateTime anniversary1;
                if (DateTime.TryParseExact(anniversary.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out anniversary1))
                {
                    lblanniversary.Text = anniversary1.ToString("dd-MM-yyyy");
                }
            }
            else
                lblanniversary.Text = "";

            if (dt.Rows[0]["RegDate"].ToString() != "")
            {
                DateTime Dob = Convert.ToDateTime(dt.Rows[0]["RegDate"].ToString());
                DateTime Dob1;
                if (DateTime.TryParseExact(Dob.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Dob1))
                {
                    lblRegDate.Text = Dob1.ToString("dd-MM-yyyy");
                }
            }
            else
                lblRegDate.Text = "";
        }

        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            RepterDetails.DataSource = null;
            RepterDetails.DataBind();
            Get_MemberAutoID_ByMemberID1();
            //ImageID();
            GridBind();
            ddlMemberName.Focus();
            
        }

        public void Get_MemberAutoID_ByMemberID1()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMember.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
            dt = objMember.Get_MemberAutoID_ByMemberID1();
            if (dt.Rows.Count > 0)
            {
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                ddlMemberName.SelectedValue = dt.Rows[0]["Member_AutoID"].ToString();
                txtContact.Text = dt.Rows[0]["Contact1"].ToString();

                MemberDetailsinfo();
                CourseDetails();
                //AccountBalanceDetails();
                AccountDetails();
                GridBind();
                lnkbtnEdit.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available!!!','Information');", true);
                return;
            }
        }

        protected void ddlMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepterDetails.DataSource = null;
            RepterDetails.DataBind();
            MemberDetailsinfo();
            CourseDetails();
            //AccountBalanceDetails();
            AccountDetails();
          // ImageDetails();
            GridBind();
            txtContact.Focus();
           
        }

        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            RepterDetails.DataSource = null;
            RepterDetails.DataBind();
            Get_MemberAutoID_ByContact();
            //ImageContact();
            GridBind();
            
        }

        public void Get_MemberAutoID_ByContact()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMember.Contact1 = txtContact.Text;
            dt = objMember.Get_MemberAutoID_ByContact();
            if (dt.Rows.Count > 0)
            {
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                ddlMemberName.SelectedValue = dt.Rows[0]["Member_AutoID"].ToString();
                txtContact.Text = dt.Rows[0]["Contact1"].ToString();

                MemberDetailsinfo();
                CourseDetails();
                //AccountBalanceDetails();
                AccountDetails();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available!!!','Information');", true);
                return;
            }
        }

        public void MemberDetailsinfo()
        {
            try
            {
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                if (Request.QueryString["MemberId"] != null)
                    objMember.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberId"]);
                else
                objMember.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
                dt = objMember.bindMemeber1();
                if (dt.Rows.Count > 0)
                {
                    AttachedMemberInformation();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Records Not Available !!!','Information');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                return;
            }
        }

        public void CourseDetails()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (Request.QueryString["MemberId"] != null)
                objMember.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberId"]);
            else
                objMember.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
            dt = objMember.CourseDetails();
            if (dt.Rows.Count > 0)
            {
                gvCourseDetails.DataSource = dt;
                gvCourseDetails.DataBind();
            }
            else
            {
                gvCourseDetails.DataSource = null;
                gvCourseDetails.DataBind();
            }

        }

        //public void AccountBalanceDetails()
        //{
        //    objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //    objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //    if (Request.QueryString["MemberId"] != null)
        //        objMember.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberId"]);
        //    else
        //        objMember.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
        //    dt = objMember.AccountBalanceDetails();
        //    if (dt.Rows.Count > 0)
        //    {
        //        BindBalance();
        //    }
        //    else
        //    {
        //        dt.Rows.Clear(); dt.Columns.Clear();
        //        dt = objMember.AccountBalanceDetails1();
        //        BindBalance();
        //    }
        //}

        int Bal=0, Bal1=0, total=0, total1=0, paid=0, paid1=0;

        public void AccountDetails()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (Request.QueryString["MemberId"] != null)
                objMember.Member_AutoID = Convert.ToInt32(Request.QueryString["MemberId"]);
            else
                objMember.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
            dt = objMember.AccountDetails();
            if (dt.Rows.Count > 0)
            {
                gvAccountDetails.DataSource = dt;
                gvAccountDetails.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                  Bal  = Convert.ToInt32( dt.Rows[i]["Bal"].ToString());
                  Bal1 = Bal1 + Bal;
                  total = Convert.ToInt32(  dt.Rows[i]["total"].ToString());
                  total1 = total1 + total;
                  paid =  Convert.ToInt32( dt.Rows[i]["paid"].ToString());
                  paid1 = paid1 + paid;
                }

                 txtBalanceFees.Text=Bal1.ToString();
                 txtToatlFees.Text=total1.ToString();
                 txtPaidFees.Text = paid1.ToString();


            }
            else
            {
                dt = objMember.AccountDetails1();
                gvAccountDetails.DataSource = dt;
                gvAccountDetails.DataBind();
            }
        }

        //public void ImageID()
        //{
        //    objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //    objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //    if (Request.QueryString["MemberId"] != null)
        //    {
        //        objMember.Member_ID1 = MemberID1;
        //    }
        //    else if (Request.QueryString["MemID"] != null)
        //    {
        //        objMember.Member_ID1 = MemberID1;
        //    }
        //    else
        //    {
        //        objMember.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
        //    }
        //    dt = objMember.MemeberID();
        //    if (dt.Rows.Count > 0)
        //    {
        //        ImgMemberPhoto.ImageUrl = dt.Rows[0]["IDProofPath"].ToString();
        //        lblMemberName.Text = dt.Rows[0]["FName"].ToString();
        //    }
        //}

        public void ImageDetails()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            objMember.Member_ID1 = Convert.ToInt32(txtMemberID.Text);

            dt = objMember.MemeberID();
            if (dt.Rows.Count > 0)
            {
                ImgMemberPhoto.ImageUrl = dt.Rows[0]["IDProofPath"].ToString();
                lblMemberName.Text = dt.Rows[0]["FName"].ToString();
            }
        }

        public void ImageContact()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMember.Contact1 = txtContact.Text;
            dt = objMember.MemeberContact();
            if (dt.Rows.Count > 0)
            {
                ImgMemberPhoto.ImageUrl = dt.Rows[0]["IDProofPath"].ToString();
                lblMemberName.Text = dt.Rows[0]["FName"].ToString();
            }
        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                int Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
                Response.Redirect("AddMember.aspx?MemberID=" + Member_AutoID);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Member First !!!','Information');", true);

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                Member_Auto_ID();
                Response.Redirect("CourseRegistration.aspx?CourseMemberID=" + MemberAuto_ID);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
        }

        public void BindBalance()
        {
            txtBalanceFees.Text = dt.Rows[0]["Bal"].ToString();
            txtToatlFees.Text = dt.Rows[0]["total"].ToString();
            txtPaidFees.Text = dt.Rows[0]["paid"].ToString();
        }
       
        protected void btnEdit_Click1(object sender, EventArgs e)
        {
            int Balance_ID = 0;
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                string flg = "MemInfo";
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
                dt = objMember.AccountDetails();
                if (dt.Rows.Count > 0)
                {
                    Balance_ID = Convert.ToInt32(dt.Rows[0]["ID_Auto"].ToString());
                }

                Response.Redirect("CourseRegistration.aspx?Balance_ID=" + Balance_ID);

            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record  Not Found !!!','Information');", true);
        }

        protected void gvCourseDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnPreview_Command(object sender, CommandEventArgs e)
        {
            int Receipt_No = Convert.ToInt32(e.CommandArgument.ToString());
            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('Receipt.aspx?Receipt_No=" + Receipt_No
            + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650')"
            + "</script>";
            ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }

        string FNameMemProfile = "FNameMemProfile";
        int MemberAuto_ID = 0;
        protected void lnkBtnFollowup_Click(object sender, EventArgs e)
        {
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                int Member_AutoID = Convert.ToInt32(ddlMemberName.SelectedValue);
                Response.Redirect("Followup.aspx?Other_Member_AutoID=" + Member_AutoID + " &FNameMemProfile=" + HttpUtility.UrlEncode(FNameMemProfile.ToString()));

            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Member First !!!','Information');", true);
        }

        

        public void Member_Auto_ID()
        {
            objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_ID1 = Convert.ToInt32(txtMemberID.Text);
                dt = objMember.Get_Member_Auto_ID();
                if (dt.Rows.Count > 0)
                {
                    MemberAuto_ID = Convert.ToInt32(dt.Rows[0]["Member_AutoID"]);
                }
        }

        protected void lnkRenew_Click(object sender, EventArgs e)
        {
            if(lblBlackUn.Text =="Block")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('You Can Not Assign Package To Block Member !!!','Information');", true);
            }
            else
            {
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                MemberAuto_ID = Convert.ToInt32(ddlMemberName.SelectedValue);
                Response.Redirect("demoCourse.aspx?Member_ID=" + MemberAuto_ID);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Member First !!!','Information');", true);
           }
        }
        protected void lnkBalance_Click(object sender, EventArgs e)
        {
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                MemberAuto_ID = Convert.ToInt32(ddlMemberName.SelectedValue);
                Response.Redirect("BalancePayment.aspx?Member_ID=" + MemberAuto_ID);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Member First !!!','Information');", true);
        }

        protected void lnkFreezing_Click(object sender, EventArgs e)
        {
            //int i = Convert.ToInt32(gvCourseDetails.SelectedRow.Cells[1]);
            //if (gvCourseDetails.SelectedRow.Cells[1].ToString() == null)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Course Not Assign To This Member !!!','Information');", true);
            //}
            //else
            //{
                if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
                {
                    MemberAuto_ID = Convert.ToInt32(ddlMemberName.SelectedValue);
                    Response.Redirect("MembershipFreezing.aspx?Member_ID=" + MemberAuto_ID);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Member First !!!','Information');", true);
            
        }

        protected void lnkappointment_Click(object sender, EventArgs e)
        {
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                MemberAuto_ID = Convert.ToInt32(ddlMemberName.SelectedValue);
                Response.Redirect("AppointmentCalender.aspx?MemberID=" + MemberAuto_ID);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Member First !!!','Information');", true);
        }

        protected void gvCourseDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCourseDetails.PageIndex = e.NewPageIndex;
            CourseDetails();
        }

        protected void gvAccountDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAccountDetails.PageIndex = e.NewPageIndex;
            AccountDetails();
        }

        protected void gvCourseDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void ClearAllField()
        {
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;
            txtMemberID.Text = string.Empty;
            txtContact.Text = string.Empty;           
            MemberAutoID = 0;
            ddlFollowupType.SelectedIndex = 0;
            //txtExecutive.Text = string.Empty;
            ddlCallPesponse.SelectedIndex = 0;
            ddlRating.SelectedIndex = 0;
            AssignDateAndTime();
            //txtNextFollowupDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");    // Assign Next Followup Date
            //lblFollowupDateTime.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");   // Assign Followup Date
            //txtNextFollowupTime.Text = string.Empty;
            txtComment.Text = string.Empty;
        }

        private void AddParameters()
        {
            if (Request.QueryString["BalancePayment_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
            else if (Request.QueryString["MembershipEnd_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
            else if (Request.QueryString["Upgrade_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Upgrade_Member_AutoID"]);
            else if (Request.QueryString["Measurement_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Measurement_Member_AutoID"]);
            else if (Request.QueryString["Other_Member_AutoID"] != null)
                objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
            else
                Member_Auto_ID();       
            objFollowup.Member_AutoID = MemberAuto_ID;
            objFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            objFollowup.CallRespond_AutoID = ddlCallPesponse.SelectedValue;
            objFollowup.Rating = ddlRating.SelectedValue;

            DateTime NFDate;
            if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate))
            {
            }
            if (ddlRating.SelectedValue != "Not Interested")
                objFollowup.NextFollowupDate = NFDate;
            else
                objFollowup.NextFollowupDate = null;

            DateTime FDate;
            if (DateTime.TryParseExact(lblFollowupDateTime.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FDate))
            {
            }
            objFollowup.FollowupDate = FDate;

            objFollowup.NextFollowupTime = Convert.ToDateTime(txtNextFollowupTime.Text.ToString());
            objFollowup.FollowupTime = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt"));
            objFollowup.Comment = Regex.Replace(txtComment.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objFollowup.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtMemberID.Text == string.Empty || txtContact.Text == string.Empty || ddlFollowupType.SelectedValue == "--Select--" || ddlCallPesponse.SelectedValue == "--Select--" || ddlRating.SelectedValue == "--Select--"
                    || txtNextFollowupDate.Text == string.Empty || txtNextFollowupTime.Text == string.Empty || txtComment.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Enter All Fields','Error');", true);

                    if (txtMemberID.Text == "")
                    { txtMemberID.Style.Add("border", "1px solid red "); }

                    if (txtContact.Text == "")
                    { txtContact.Style.Add("border", "1px solid red "); }

                    if (ddlFollowupType.SelectedValue == "--Select--")
                    { ddlFollowupType.Style.Add("border", "1px solid red "); }

                    //if (txtExecutive.Text == string.Empty)
                    //{ txtExecutive.Style.Add("border", "1px solid red "); }


                    if (ddlCallPesponse.SelectedValue == "--Select--")
                    { ddlCallPesponse.Style.Add("border", "1px solid red "); }


                    if (ddlRating.SelectedValue == "--Select--")
                    { ddlRating.Style.Add("border", "1px solid red "); }


                    if (txtNextFollowupDate.Text == "")
                    { txtNextFollowupDate.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupTime.Text == "")
                    { txtNextFollowupTime.Style.Add("border", "1px solid red "); }

                    if (txtComment.Text == "")
                    { txtComment.Style.Add("border", "1px solid red "); }
                }
                else
                {
                    txtMemberID.Style.Add("border", "1px solid silver  ");
                    txtContact.Style.Add("border", "1px solid silver  ");
                    ddlFollowupType.Style.Add("border", "1px solid silver  ");
                    //txtExecutive.Style.Add("border", "1px solid silver  ");
                    ddlCallPesponse.Style.Add("border", "1px solid silver  ");
                    ddlRating.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
                    txtComment.Style.Add("border", "1px solid silver  ");

                    AssignID();
                    AddParameters();

                    if (btnSave.Text == "Save")
                    {
                        objFollowup.Action = "INSERT";
                        res = objFollowup.Insert_FollowupInformation();

                        if (res > 0)
                        {
                            ClearAllField();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                            GridBind();
                        }
                        if (Request.QueryString["FNMemEndFollDetail"] != null)
                        {
                            Response.Redirect("ViewMemberEndFollowup.aspx");
                        }
                        if (Request.QueryString["FNBalPayFollDetail"] != null)
                        {
                            Response.Redirect("ViewBalancePaymentFollowup.aspx");
                        }
                        if (Request.QueryString["FNMemberFollDetail.aspx"] != null)
                        {
                            Response.Redirect("ViewMembershipFollowup.aspx");
                        }
                        if (Request.QueryString["FNameMemProfile"] != null)
                        {
                            int MemberAutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
                            Response.Redirect("MemberProfile.aspx?MemberId=" + MemberAutoID);
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        objFollowup.Action = "Update";
                        objFollowup.Followup_AutoID = Convert.ToInt32(ViewState["Followup_AutoID"]);

                        res = objFollowup.Insert_FollowupInformation();
                        if (res > 0)
                        {
                            btnSave.Text = "Save";
                            txtMemberID.Enabled = true;
                            txtContact.Enabled = true;
                            ClearAllField();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);

                        }
                    }

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
        }

        protected void lnkAttendance_Click(object sender, EventArgs e)
        {
            if (ddlMemberName.SelectedItem.Text != "--Select--" && txtMemberID.Text != "" && txtContact.Text != "")
            {
                MemberAuto_ID = Convert.ToInt32(ddlMemberName.SelectedValue);
                Response.Redirect("MemberNumericalAttendance.aspx?MemberID=" + MemberAuto_ID);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Member First !!!','Information');", true);
        }

       
    }
}
