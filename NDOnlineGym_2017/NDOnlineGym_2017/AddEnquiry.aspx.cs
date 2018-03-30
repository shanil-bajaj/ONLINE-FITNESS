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
using System.Net;
using System.Text;
using System.Net.Mail;
using WhatsAppApi;
using System.Web.Services;

namespace NDOnlineGym_2017
{
    public partial class AddEnquiry : System.Web.UI.Page
    {
        BalCallRespondMaster obBalCallRespondMaster = new BalCallRespondMaster();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalEnquiry eng = new BalEnquiry();
        System.Data.SqlClient.SqlTransaction trans;
        DataTable dt = new DataTable();
        DateTime todaydate;
        int Enq_ID = 0;
        static int Flag = 0; 
         static int Company_AutoID;
         static int Branch_AutoID;
         static int EnqID;
         static string btntext;

        protected void Page_Load(object sender, EventArgs e)
        {
                Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); 
                Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                if (btnSave.Text == "Update")
                    EnqID = Convert.ToInt32(txtEnquiryId.Text);
                btntext = btnSave.Text;
                if (!IsPostBack)
                {
                AssignMonthDate();
                txtEnquiryId.Focus();
                //txtSearch.Enabled = false;
                Get_Enquiryid();
                AssignTodaysDate();
                bindDDLExecutive();
                setExecutive();
                bindDDLCallRespond();                               
                Bind_EnquiryType();
                Bind_EnquiryFor();
                Bind_ddlSourceOfEnquiry();
                                                
                if (Request.QueryString["Enq_AutoID"] != null)
                {
                    int AutoID = Convert.ToInt32(Request.QueryString["Enq_AutoID"]);
                    ViewState["Enq_id"] = AutoID;
                    GetDataForEdit(AutoID);
                }

                if (Request.QueryString["AddMember"] != null)
                {
                    divFormDetails.Visible = false;
                    divsearch.Visible = true;
                    bind();
                }

                if (Request.QueryString["MenuEnquDetails"] != null)
                {
                    divsearch.Visible = true;
                    divFormDetails.Visible = false;
                    divEnquiryDetails.Visible = true;
                    divAddEnq.Visible = false;
                    txtFromDate.Focus();

                    SearchByDate();
                    Flag = 1;
                }

                if (Request.QueryString["MenuEnqFollowupDetails"] != null)
                {
                    divAddEnq.Visible = false;
                    divEnqFoll.Visible = true;
                    divsearch.Visible = true;
                    divFormDetails.Visible = false;
                    txtFromDate.Focus();
                }

            }
        }

        #region ---------- Assign Company ID and Branch ID -------------------
      

        private void AssignID()
        {
             
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
        }
        #endregion

        #region --------- Get Enquiry ID -----------
        private void Get_Enquiryid()
        {
            dt.Clear();
            AssignID();
            dt = eng.Get_Enquiryid();
            if (dt.Rows.Count > 0)
            {
                txtEnquiryId.Text = dt.Rows[0]["Enquiryid"].ToString();
            }
        }
        #endregion

        #region ------------ Assign All Date ------------------
        protected void AssignTodaysDate()
        {

            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtEnqDate.Text = todaydate.ToString("dd-MM-yyyy");
                //txtDOB.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

                txtEnqTime.Text = localTime.ToString("HH:mm");
            }
        }
        #endregion

        #region ---------------- Bind Call Response DDL --------------
        public void bindDDLCallRespond()
        {
            try
            {                
                obBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                obBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalCallRespondMaster.Select_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlCallResponde.DataSource = dt;
                    //ddlExecutive.Items.Clear();
                    ddlCallResponde.DataValueField = "CallRespond_AutoID";
                    ddlCallResponde.DataTextField = "Name";
                    ddlCallResponde.DataBind();
                    ddlCallResponde.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Call Respond Master !!!','Error');", true);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ---------------- Bind Executive DDL --------------
        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                //obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
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
        #endregion

        #region ---------------- Set Executive DDL --------------
        public void setExecutive()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            //obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
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
        #endregion       
                
        #region ----------- Bind Enquiry Type DDL --------------
        public void Bind_EnquiryType()
        {
            try
            {
                AssignID();
                dt = eng.Get_EnquiryType();
                if (dt.Rows.Count > 0)
                {
                    ddlEnquiryType.DataSource = dt;
                    ddlEnquiryType.Items.Clear();
                    ddlEnquiryType.DataValueField = "EnqType_AutoID";
                    ddlEnquiryType.DataTextField = "Name";
                    ddlEnquiryType.DataBind();
                    ddlEnquiryType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ----------- Bind Enquiry For DDL --------------
        public void Bind_EnquiryFor()
        {
            try
            {
                eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = eng.Get_EnquiryFor();
                if (dt.Rows.Count > 0)
                {
                    ddlEnquiryFor.DataSource = dt;
                    ddlEnquiryFor.Items.Clear();
                    ddlEnquiryFor.DataValueField = "EnqFor_AutoID";
                    ddlEnquiryFor.DataTextField = "Name";
                    ddlEnquiryFor.DataBind();
                    ddlEnquiryFor.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region --------- Bind Source OF Enquiry DDL --------------
        public void Bind_ddlSourceOfEnquiry()
        {
            try
            {
                eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
                eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                dt = eng.Get_SourceOfEnquiry();
                if (dt.Rows.Count > 0)
                {
                    ddlSourceOfEnquiry.DataSource = dt;
                    ddlSourceOfEnquiry.Items.Clear();
                    ddlSourceOfEnquiry.DataValueField = "SourceOfEnq_AutoID";
                    ddlSourceOfEnquiry.DataTextField = "Name";
                    ddlSourceOfEnquiry.DataBind();
                    ddlSourceOfEnquiry.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            //var newWidth = (int)(image.Width * ratio);
            //var newHeight = (int)(image.Height * ratio);

            var newWidth = (int)(75);
            var newHeight = (int)(75);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        string newfileName = string.Empty;
        string serverfilrpath = string.Empty;

        #region -------- Image Upload -------------
        public void ImageUplode()
        {
            //if ((fileLogo.PostedFile != null) && (fileLogo.PostedFile.ContentLength > 0))
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
                    if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png" || fileExtention == "image/x-icon")
                    {
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

        private void SetImagePath()
        {
            if (fileLogo.HasFile)
            {
                serverfilrpath = ViewState["serverfilrpath1"].ToString();
                eng.ImagePath = serverfilrpath;
            }
            else
            {
                if (imgMember.ImageUrl == "")
                {
                    serverfilrpath = "";
                    eng.ImagePath = "";
                }
                else
                {
                    serverfilrpath = ViewState["ImageUrl"].ToString();
                    eng.ImagePath = serverfilrpath;
                }
            }
        }
        #endregion

        #region ------------ Clear Function ---------------
        public void ClearFun()
        {
            
            txtEnquiryId.Focus();
            txtEnquiryId.Enabled = true;
            txtDOB.Text = string.Empty;
            chkExecutive.Checked = true;
            ddlExecutive.Enabled = false;
            txtNextFollowupDate.Enabled = true;

            AssignTodaysDate();
            Get_Enquiryid();

            txtFirstName.Text = txtLastName.Text = txtContact1.Text = "";
            txtContact2.Text = txtWhatsAppNo.Text = txtEmail.Text = txtAddress.Text = "";
            txtReferennceDetails.Text = txtComment.Text = "";
            ddlGender.SelectedValue = "--Select--";
            ddlRating.SelectedValue = "--Select--";
            ddlCallResponde.SelectedValue = "--Select--";
            ddlEnquiryType.SelectedValue = "--Select--";
            ddlEnquiryFor.SelectedValue = "--Select--";
            ddlSourceOfEnquiry.SelectedValue = "--Select--";            
            ddlExecutive.SelectedValue =Request.Cookies["OnlineGym"]["Staff_AutoID"];

            imgMember.ImageUrl = "";                        
            
        }
        #endregion

        public void AddParameter()
        {
            try
            {
                eng.Enq_ID1 = Convert.ToInt32(txtEnquiryId.Text);

                DateTime EnqDate;
                if (DateTime.TryParseExact(txtEnqDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out EnqDate))
                {
                    string EnqDate1 = EnqDate.ToString("dd-MM-yyyy");
                    eng.EnqDate = DateTime.ParseExact(EnqDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }

                if (txtEnqTime.Text != "")
                {

                    eng.Time = Convert.ToDateTime(txtEnqTime.Text);//System.DateTime.Now.ToUniversalTime();
                }
                else
                {
                    eng.Time = null;
                }

                eng.FName = txtFirstName.Text.Trim();
                eng.LName = txtLastName.Text.Trim();

                if (txtDOB.Text != string.Empty)
                {
                    DateTime DOB;
                    if (DateTime.TryParseExact(txtDOB.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOB))
                    {
                        string DOB1 = DOB.ToString("dd-MM-yyyy");
                        eng.DOB = DateTime.ParseExact(DOB1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                }
                //else
                //{
                //    eng.DOB = null;
                //}
                
                eng.Gender = ddlGender.SelectedValue.ToString();
                eng.Contact1 = txtContact1.Text.Trim();

                eng.Contact2 = txtContact2.Text.Trim();
                eng.WhatsAppNo = txtWhatsAppNo.Text.Trim();//txtEmail

                eng.Email = txtEmail.Text.Trim();

                //if (txtEmail.Text != string.Empty)
                //{
                //    bool valid = IsValidEmail(txtEmail.Text);

                //    if (valid == true)
                //    {
                //        eng.Email = txtEmail.Text.Trim();
                //    }
                //    else
                //    {
                //        txtEmail.Focus();
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invalid Email Id !!!','Error');", true);

                //    }

                //}  

                
                eng.Address = txtAddress.Text.Trim();
                eng.FollowupType = "Enquiry";
                eng.Rating = ddlRating.SelectedValue.ToString();

                eng.ReferenceDetails = txtReferennceDetails.Text.Trim();

                if (ddlCallResponde.SelectedValue != "--Select--")
                    eng.CallRespond_AutoID = Convert.ToInt32(ddlCallResponde.SelectedValue);
                else
                    eng.CallRespond_AutoID =null;
                eng.Comment = txtComment.Text.Trim();

                if (ddlRating.SelectedValue != "Not Interested")
                {
                    DateTime NextFollowupDate;
                    if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NextFollowupDate))
                    {
                        string NextFollowupDate1 = NextFollowupDate.ToString("dd-MM-yyyy");

                        eng.NextFollowupDate = DateTime.ParseExact(NextFollowupDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    }
                }

                //ImageUplode();

                //if (fileLogo.HasFile)
                //{
                //    serverfilrpath = ViewState["serverfilrpath1"].ToString();
                //    eng.ImagePath = serverfilrpath;
                //}                
                //else
                //{
                //    if (imgEquipment.ImageUrl == "")
                //    {
                //        serverfilrpath = "";
                //        eng.ImagePath = "";
                //    }
                //    else
                //    {
                //        serverfilrpath = ViewState["serverfilrpath1"].ToString();
                //        eng.ImagePath = serverfilrpath;
                //    }
                //}



                if (ddlEnquiryType.SelectedValue != "--Select--")
                    eng.EnqType_ID = Convert.ToInt32(ddlEnquiryType.SelectedValue.ToString());
                else
                    eng.EnqType_ID = null;

                if(ddlEnquiryFor.SelectedValue != "--Select--")
                    eng.EnqFor_ID = Convert.ToInt32(ddlEnquiryFor.SelectedValue.ToString());
                else
                    eng.EnqFor_ID = null;

                if(ddlSourceOfEnquiry.SelectedValue != "--Select--")
                    eng.SourceOfEnq_ID = Convert.ToInt32(ddlSourceOfEnquiry.SelectedValue.ToString());
                else
                    eng.SourceOfEnq_ID = null;

                eng.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

                eng.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);                
                eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);                
                eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                if (Convert.ToInt32(ViewState["flag"]) != 1)
                {
                    eng.Action = "Insert";
                }
                else
                {
                    eng.Action = "Update";
                    eng.Enq_ID = Convert.ToInt32(ViewState["Enq_id"]);
                    ViewState["flag"] = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        #region --------- Clear Button -----------
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ClearFun();
            
            txtEnquiryId.Enabled = true;
            btnSave.Text = "Save";            
            ddlSearch.SelectedValue = "--Select--";
            txtSearch.Text = "";
            //txtSearch.Enabled = false;
            gvEnquiry.DataSource = null;
            gvEnquiry.DataBind();
            gvEnquiry.Visible = false;
        }
        #endregion

        #region --------- Save and Update Button -----------

        int Exits1=0;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    dt.Clear();
                                       
                    AddParameter();
                    ImageUplode();
                    SetImagePath();
                    dt = eng.Exits();
                    if (dt.Rows.Count > 0)
                    {
                        Exits1 = Convert.ToInt32(dt.Rows[0][0].ToString());                        
                    }
                    if (Exits1 > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enquiry ID AlReady Exits !!!','Error');", true);
                        Get_Enquiryid();
                        btnSave.Focus();
                        return;
                    }

                    eng.Action = "ExitsContact";
                    dt = eng.ExitsContact();
                    if (dt.Rows.Count > 0)
                    {
                        Exits1 = Convert.ToInt32(dt.Rows[0][0].ToString());                        
                    }

                    if (Exits1 > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Number AlReady Exits !!!','Error');", true);
                        Get_Enquiryid();
                        txtContact1.Focus();
                        return;
                    }
                   
                    if (txtEmail.Text != string.Empty)
                    {
                        bool valid = IsValidEmail(txtEmail.Text);

                        if (valid == false)
                        {                         
                            txtEmail.Focus();
                            Exits1 = 1;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invalid Email Id !!!','Error');", true);
                            return;
                        }

                    }

                    Check_NextFollowupDateIsGraterTodayDate();

                    if(Exits1 == 0)
                    {
                        eng.Action = "Insert";
                        int k = eng.Insert_Update_Delete();
                        if (k > 0)
                        {
                            SendSMSNew();  
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            ClearFun();
                        }
                    }
                }
                else
                {
                    AddParameter();
                    ImageUplode();
                    SetImagePath();

                    eng.Action = "ExitsContact_Update";
                    dt = eng.ExitsContact();
                    if (dt.Rows.Count > 0)
                    {
                        Exits1 = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }

                    if (Exits1 > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Number AlReady Exits !!!','Error');", true);
                        Get_Enquiryid();
                        txtContact1.Focus();
                        return;
                    }


                    if (txtEmail.Text != string.Empty)
                    {
                        bool valid = IsValidEmail(txtEmail.Text);

                        if (valid == false)
                        {                         
                            txtEmail.Focus();
                            Exits1 = 1;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invalid Email Id !!!','Error');", true);
                            return;
                        }

                    }

                    Check_NextFollowupDateIsGraterTodayDate();

                    if (Exits1 == 0)
                    {
                        eng.Action = "Update";
                        int k = eng.Insert_Update_Delete();
                        if (k > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Update Successfully !!!','Success');", true);
                            if (Request.QueryString["FNameSearchPage"] != null)
                                Response.Redirect("SearchPage.aspx?Search=" + txtSearch.Text);
                            if (Request.QueryString["FNameDashboard"] != null)
                                Response.Redirect("Dashboard.aspx");
                            if (Request.QueryString["FNAllFollowupDetailsEnq"] != null)
                            {
                                string Pagename = "FNAllFollowupDetailsEnq";
                                Response.Redirect("AllFollowup.aspx?FNAllFollowupDetailsEnq=" + Pagename);
                            }
                            if (Request.QueryString["FNameViewEnqFoll"] != null)
                            {
                                Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                                Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                            }
                            if (Request.QueryString["MenuEnquDetails"] != null)
                            {
                                Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                                Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                            }
                            if (Request.QueryString["MenuEnqFollowupDetails"] != null)
                            {
                                Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                                Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnqFollowupDetails=" + HttpUtility.UrlEncode("MenuEnqFollowupDetails".ToString()));
                            }
                            btnSave.Text = "Save";
                            ClearFun();                            
                            bind();

                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

       #region--------- Send SMS ------------
        private void SendSMSNew()
        {
            StringBuilder Message = new StringBuilder();

            eng.Action = "Get_EnquiryTemplate";
            dt = eng.GetTemplate();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SMSWithName"].ToString() == "YES")
                {
                    Message.Append("Dear ");
                    Message.Append(txtFirstName.Text + " " + txtLastName.Text);

                    if (ddlGender.SelectedValue == "Male")
                    {
                        Message.Append(" Sir, ");
                    }
                    if (ddlGender.SelectedValue == "Female")
                    {
                        Message.Append(" Madam, ");
                    }

                }

                StringBuilder sb = new StringBuilder(dt.Rows[0]["Enquiry"].ToString());
                sb.Replace("#ENQID#", txtEnquiryId.Text);
                sb.Replace("#ENQDate#", txtEnqDate.Text);
                    
                Message.Append(sb.ToString());
                //Message.Append(dt.Rows[0]["Enquiry"].ToString());

                //}
                //else
                //{
                //    Message.Append(dt.Rows[0]["Enquiry"].ToString());
                //}

            }
            else
            {
                Message.Append("Thanks for enquiry.");
            }

            string Mobile = txtContact1.Text;            
            SendSMSFun(Mobile, Message);

            if (txtEmail.Text != string.Empty)
            {
                bool valid= IsValidEmail(txtEmail.Text);

                if (valid == true)
                {
                    SendEmail(txtEmail.Text.ToString(), Message);
                }

            }  
        }        

        string suname, spass, senderid, routeid, status;
        public void SendSMSFun(string Mobile, StringBuilder Message)
        {
            int Val;
            try
            {
                AssignID();
                eng.Action = "SELECT_SMSLogin_INFO";
                DataSet ds = new DataSet();

                ds = eng.GetSMSLoginDetails();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    suname = ds.Tables[0].Rows[0]["Username"].ToString();
                    spass = ds.Tables[0].Rows[0]["Password"].ToString();
                    senderid = ds.Tables[0].Rows[0]["Sender_ID"].ToString();
                    routeid = ds.Tables[0].Rows[0]["Route"].ToString();
                    status = ds.Tables[0].Rows[0]["Status"].ToString();
                }

                if (status == "ON")
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
            catch (WebException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

           
        }


        private void SendEmail(string To, StringBuilder Message)
        {
            MailMessage mailObj = new MailMessage();

            string Sub = " Enquiry Details ";
            string Body = "<br />This is a system generated email.Please do not reply.<br/> <br /> " + Message + 
                          "<br/><br/> <br/><br/>Please feel free to contact us 9156184755 or email navkardreamsoft@gmail.com <br/><br/>Thanks <br/> Team ND Fitness+";
            mailObj.From = new MailAddress("gymfitnessplus27@gmail.com");

            if (To == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Email ID Does Not Exist','Error');", true);
            }
            else
            {
                mailObj.To.Add(To);
                mailObj.Subject = Sub;
                mailObj.Body = Body;
                mailObj.IsBodyHtml = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                SmtpClient SMTPServer = new SmtpClient();
                //SMTPServer.Host = "smtp.gmail.com";
                //SMTPServer.Port = 587;
                //SMTPServer.EnableSsl = true;
                SMTPServer.Host = "relay-hosting.secureserver.net";   //-- Donot change.
                SMTPServer.Port = 25; //--- Donot change
                SMTPServer.EnableSsl = false;//--- Donot change
                SMTPServer.UseDefaultCredentials = true;
                NetworkCredential credentials = new NetworkCredential("gymfitnessplus27@gmail.com", "fitnessplus123");
                SMTPServer.Credentials = credentials;
                try
                {
                    SMTPServer.Send(mailObj);
                    //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Password Has Been Sent !!!','Success');", true);
                }
                catch (Exception ex)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Email Failed','Error');", true);
                }
            }
        }

        #endregion

       #region----------- Check Valid Email Address -------------
       bool IsValidEmail(string email)
       {
           try
           {
               var addr = new System.Net.Mail.MailAddress(email);
               return true;
           }
           catch
           {
               return false;
           }
       }
       #endregion

       #region ----------- Check Emaild IS Valid -------------
       protected void txtEmail_TextChanged(object sender, EventArgs e)
       {
           if (txtEmail.Text != string.Empty)
           {
               bool valid = IsValidEmail(txtEmail.Text);

               if (valid == false)
               {
                   txtEmail.Focus();                   
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invalid Email Id !!!','Error');", true);
                   return;
               }
           }
       }
       #endregion

       #region ------- Search Dropdown ------------
       protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlSearch.SelectedValue.ToString() == "--Select--")
            //{
            //    txtSearch.Enabled = false;
            //}
            //else
            //{
            //    txtSearch.Enabled = true;
            //}

            //txtSearch.Text = string.Empty;
            //if (ddlSearch.SelectedValue.ToString() == "--Select--")
            //{
            //    //txtSearch.Style.Add("border", "1px solid silver ");
            //    txtSearch.Enabled = false;

            //}
            //else
            //{
            //    txtSearch.Enabled = true;
            //}
            ddlSearch.Focus();

        }
        #endregion

        #region ---------- Edit Button ---------------

        public int Enq_id;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            Enq_id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["Enq_id"] = Convert.ToInt32(e.CommandArgument.ToString());
            if (Request.QueryString["AddMember"] != null)
            {
                divFormDetails.Visible = true;
            }
            if (Request.QueryString["MenuEnquDetails"] != null)
            {
                divAddEnq.Visible = true;
                divEnquiryDetails.Visible = false;
                divsearch.Visible = false;
                divFormDetails.Visible = true;
            }
            if (Request.QueryString["MenuEnqFollowupDetails"] != null)
            {
                divsearch.Visible = false;
                divFormDetails.Visible = true;
            }
            GetDataForEdit(Enq_id);
            txtEnqTime.Focus();
        }

        //int Flag = 0;
        public void GetDataForEdit(int AutoID)
        {
            txtEnquiryId.Enabled = false;
            btnSave.Text = "Update";
            AssignID();                        
            eng.Enq_ID = Convert.ToInt32(AutoID);

            dt.Clear();
            dt = eng.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                txtEnquiryId.Text = dt.Rows[0]["Enq_ID1"].ToString();

                if (dt.Rows[0]["EnqDate"].ToString() != "")
                {
                    DateTime Enqdate = Convert.ToDateTime(dt.Rows[0]["EnqDate"].ToString());
                    DateTime Enqdate1;
                    if (DateTime.TryParseExact(Enqdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Enqdate1))
                    {
                        txtEnqDate.Text = Enqdate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    txtEnqDate.Text = "";
                
                txtFirstName.Text = dt.Rows[0]["FName"].ToString();
                txtLastName.Text = dt.Rows[0]["LName"].ToString();

                if (dt.Rows[0]["DOB"].ToString() != "")
                {
                    DateTime DOBDate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                    DateTime DOBDate1;
                    if (DateTime.TryParseExact(DOBDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DOBDate1))
                    {
                        txtDOB.Text = DOBDate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    txtDOB.Text = "";
                
                ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                txtContact2.Text = dt.Rows[0]["Contact2"].ToString();
                txtWhatsAppNo.Text = dt.Rows[0]["WhatsAppNo"].ToString();
                txtEnqTime.Text = dt.Rows[0]["Time"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                ddlRating.SelectedValue = dt.Rows[0]["Rating"].ToString();
                txtReferennceDetails.Text = dt.Rows[0]["ReferenceDetails"].ToString();
                txtComment.Text = dt.Rows[0]["Comment"].ToString();
                ddlExecutive.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();

                if (dt.Rows[0]["CallRespond_AutoID"].ToString() != string.Empty)                
                    ddlCallResponde.SelectedValue = dt.Rows[0]["CallRespond_AutoID"].ToString();                                

                if (dt.Rows[0]["NextFollowupDate"].ToString() != "")
                {
                    DateTime NxtFollDate = Convert.ToDateTime(dt.Rows[0]["NextFollowupDate"].ToString());
                    DateTime NxtFollDate1;
                    if (DateTime.TryParseExact(NxtFollDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NxtFollDate1))
                    {
                        txtNextFollowupDate.Text = NxtFollDate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    txtNextFollowupDate.Text = "";

                if(dt.Rows[0]["Rating"].ToString() == "Not Interested")
                {
                    txtNextFollowupDate.Enabled = false;
                    txtNextFollowupDate.Text = string.Empty;
                }                

                if (dt.Rows[0]["EnqType_AutoID"].ToString() != string.Empty)
                    ddlEnquiryType.SelectedValue = dt.Rows[0]["EnqType_AutoID"].ToString();

                if (dt.Rows[0]["EnqFor_AutoID"].ToString() != string.Empty)
                    ddlEnquiryFor.SelectedValue = dt.Rows[0]["EnqFor_AutoID"].ToString();               

                if (dt.Rows[0]["SourceOfEnq_AutoID"].ToString() != string.Empty)
                    ddlSourceOfEnquiry.SelectedValue = dt.Rows[0]["SourceOfEnq_AutoID"].ToString();

                imgMember.ImageUrl = dt.Rows[0]["ImagePath"].ToString();
                ViewState["ImageUrl"] = dt.Rows[0]["ImagePath"].ToString();

            }
            ViewState["flag"] = 1;
        }

        #endregion

        #region --------- Delete Button -------------
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                eng.Enq_ID = Convert.ToInt32(e.CommandArgument);
                //eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                eng.Action = "Delete";
                int i = eng.Delete();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    bind();
                    //ddlCompany.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
                return;
            }
        }
        #endregion

        #region ----------- Check Enquiry Id IS Allready Exist --------
        bool chkExistingEnquiryId = false;
        protected void txtEnquiryId_TextChanged(object sender, EventArgs e)
        {      

           // dt.Clear();
            //eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (txtEnquiryId.Text != string.Empty)
            {
                eng.Enq_ID1 = Convert.ToInt32(txtEnquiryId.Text);

                chkExistingEnquiryId = eng.Check_ExistingEnquiryId();
                if (chkExistingEnquiryId == true)
                {
                    Get_Enquiryid();
                    txtEnquiryId.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enquiry Id Is Already Exists !!!','Error');", true);
                    return;
                }
                else
                {
                    txtEnqDate.Focus();
                }
            }
            else
            {
                txtEnquiryId.Focus();
            }

           // dt = eng.Exits();

            

            //if (dt.Rows.Count > 0)
            //{
            //    Exits1 = Convert.ToInt32(dt.Rows[0][0].ToString());
            //}
            //if (Exits1 > 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enquiry ID AlReady Exits !!!','Error');", true);
            //    Get_Enquiryid();
            //    btnSave.Focus();
            //    return;
            //}
        }
        #endregion

        #region ---------- Check Contact IS Allready Exist-------
                
        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string checkUserName(string IDVal)
        //{
        //    BalEnquiry eng = new BalEnquiry();
        //    DataTable dt = new DataTable();
        //    eng.Contact1 = IDVal;
        //    eng.Company_ID = Company_AutoID;
        //    eng.Branch_ID = Branch_AutoID;
        //    eng.Action = "ExitsContact";
        //    dt = eng.ExitsContact();


        //   string result = string.Empty;
        //    if (dt.Rows.Count > 0)
        //    {
        //        result = "ID is available, you can use it";
        //    }
        //    else
        //    {
        //        result = "ID already in use";
        //    }
        //    //Return the result
        //    return result;
        //}

       


       
       [System.Web.Services.WebMethod]
        public static string CheckEmail(string contact)
        {
        //    //if (btntext == "Save")
        //    //{
        //    //    eng.Action = "ExitsContact";
        //    //}
        //    //else
        //    //{
        //    //    eng.Enq_ID1 = Convert.ToInt32(EnqID);
        //    //    eng.Action = "ExitsContact_Update";
        //    //}




            BalEnquiry eng = new BalEnquiry();
            DataTable dt = new DataTable();
            eng.Contact1 = contact;
            eng.Company_ID = Company_AutoID;
            eng.Branch_ID = Branch_AutoID;
            //eng.Action = "ExitsContact";

            if (btntext == "Save")
            {
                eng.Action = "ExitsContact";
            }
            else
            {
                eng.Enq_ID1 = Convert.ToInt32(EnqID);
                eng.Action = "ExitsContact_Update";
            }
            dt = eng.ExitsContact();


            string retval = "";
            if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                retval = "true";
            }
            else
            {
                retval = "false";
            }
            //Return the result
            return retval;
        }
   


        protected void txtContact1_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();           
            AssignID();
            if (btnSave.Text == "Save")
            {
                eng.Action = "ExitsContact";
            }
            else
            {
                eng.Enq_ID1 = Convert.ToInt32(txtEnquiryId.Text);
                eng.Action = "ExitsContact_Update";
            }
            eng.Contact1 = txtContact1.Text;
            dt = eng.ExitsContact();
            if (dt.Rows.Count > 0)
            {
                Exits1 = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            if (Exits1 > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Contact Number Already Exits !!!','Error');", true);
                //Get_Enquiryid();
                txtContact1.Focus();
                //btnSave.Focus();
                return;
            }
            else
            {
                txtContact2.Focus();
            }
        }
        #endregion

        #region Check NextFollowup Date is Not Grater Than Enquiry Date
        
        DateTime date, tdate, bday, fdate;
        protected void txtNettFollowupDate_TextChanged(object sender, EventArgs e)
        {
            Check_NextFollowupDateIsGraterTodayDate();
            txtNextFollowupDate.Focus();
        }

        protected void Check_NextFollowupDateIsGraterTodayDate()
        {
           
            if (txtNextFollowupDate.Text != "")
            {
                if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fdate) && DateTime.TryParseExact(txtEnqDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    if (btnSave.Text == "Save")
                    {
                        if (fdate < date)
                        {
                            Exits1 = 1;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('From Date should not be Greater Than To Date !!!','Error');", true);
                            txtNextFollowupDate.Focus();
                            return;
                        }
                        else
                        {
                            string fdate1 = fdate.ToString("yyyy-MM-dd");
                            eng.NextFollowupDate = Convert.ToDateTime(fdate1);
                        }
                    }
                    else
                    {
                        string date1 = fdate.ToString("yyyy-MM-dd");
                        eng.NextFollowupDate = Convert.ToDateTime(date1);
                    }
                }
            }
        }
        #endregion


        protected void btnFollowup_Command(object sender, CommandEventArgs e)
        {
            if (Request.QueryString["MenuEnquDetails"] != null)
            {
                Enq_ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("EnquiryFollowup.aspx?Enq_ID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
            }
            if (Request.QueryString["MenuEnqFollowupDetails"] != null)
            {
                Enq_ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("EnquiryFollowup.aspx?Enq_ID=" + Enq_ID + " &MenuEnqFollowupDetails=" + HttpUtility.UrlEncode("MenuEnqFollowupDetails".ToString()));
            }   
        }

        bool chkEnquiryIdIsMember = false;
        protected void btnAddMember_Command(object sender, CommandEventArgs e)
        {

            Enq_ID = Convert.ToInt32(e.CommandArgument);
            AssignID();
            eng.Enq_ID = Enq_ID;           
            chkEnquiryIdIsMember = eng.Check_EnquiryIdISMember();

            if (chkEnquiryIdIsMember == true)      
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('This Is Already Member  !!!','Error');", true);
                return;
            }
            else
            {
                Response.Redirect("AddMember.aspx?Enq_ID=" + Enq_ID, true);               
            }

            
        }

        protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnquiry.PageIndex = 0;
            if (Flag == 1)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                SearchByDate();
            }
            else if (Flag == 2)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchByCategory";
                Search();
            }
            else if (Flag == 3)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchAll";
                eng.DateCategory = "FollowupDate";
                bind();
            }
            else if (Flag == 4)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchByDateCategory";
                eng.DateCategory = "EnquiryDateWithCategory";
                Search();
            }
            else if (Flag == 5)
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
                eng.Action = "SearchByDateCategory";
                eng.DateCategory = "FollowupDateWithCategory";
                Search();
            }
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
            {
                ddlExecutive.Enabled = false;
                fileLogo.Focus();
            }
            else
            {
                ddlExecutive.Enabled = true;
                ddlExecutive.Focus();
            }
            
        }

        #region ------------ Rating DDL Change ---------------
        protected void ddlRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRating.SelectedValue == "Not Interested")
                {
                    txtNextFollowupDate.Enabled = false;
                    txtNextFollowupDate.Text = string.Empty;
                }
                else
                {
                    txtNextFollowupDate.Enabled = true;

                    if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                    }

                    txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");
                }
                ddlRating.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region ------------ Assign All Date ------------------
        protected void AssignMonthDate()
        {
            Label1.Text = DateTime.Today.ToShortDateString();

            DateTime dtFirst = Convert.ToDateTime(Label1.Text);
            DateTime dtLast;

            //Setting Start Date Month
            dtFirst = new DateTime(dtFirst.Year, dtFirst.Month, 1);
            txtFromDate.Text = dtFirst.ToString("dd-MM-yyyy");

            //Setting Last Date of Month
            dtLast = dtFirst.AddMonths(1).AddDays(-1);
            txtToDate.Text = dtLast.ToString("dd-MM-yyyy");

            DateTime Fromdate;
            if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            { }
            eng.FromDate = Fromdate;
            DateTime Todate;
            if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            { }
            eng.ToDate = Todate;
        }
        #endregion

        #region ------------ Assign Action On Search
        private void Search()
        {
            //if (ddlSearch.SelectedValue.ToString() == "--Select--")
            //{
            //    //txtSearch.Enabled = false;
            //    eng.Category = "All";
            //}
            //else
            if (ddlSearch.SelectedValue.ToString() == "Enquiry ID")
            {
                eng.Category = "Enquiry ID";
                eng.searchTxt = txtSearch.Text;

            }
            else if (ddlSearch.SelectedValue.ToString() == "First Name")
            {
                eng.Category = "First Name";
                eng.searchTxt = txtSearch.Text;
            }
            else if (ddlSearch.SelectedValue.ToString() == "Last Name")
            {
                eng.Category = "Last Name";
                eng.searchTxt = txtSearch.Text;
            }
            else if (ddlSearch.SelectedValue.ToString() == "Contact 1")
            {
                eng.Category = "Contact 1";
                eng.searchTxt = txtSearch.Text;
            }
            else if (ddlSearch.SelectedValue.ToString() == "Enquiry Type")
            {
                eng.Category = "Enquiry Type";
                eng.searchTxt = txtSearch.Text;
            }
            else if (ddlSearch.SelectedValue.ToString() == "Enquiry For")
            {
                eng.Category = "Enquiry For";
                eng.searchTxt = txtSearch.Text;
            }
            else if (ddlSearch.SelectedValue.ToString() == "Rating")
            {
                eng.Category = "Rating";
                eng.searchTxt = txtSearch.Text;
            }
            else if (ddlSearch.SelectedValue.ToString() == "Source Of Enquiry")
            {
                eng.Category = "SourceOfEnquiry";
                eng.searchTxt = txtSearch.Text;
            }
            else if (ddlSearch.SelectedValue.ToString() == "Executive Name")
            {
                eng.Category = "ExecutiveName";
                eng.searchTxt = txtSearch.Text;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!','Information');", true);
                ddlSearch.Focus();
                return;
            }

            bind();
        }
        #endregion

        #region ------------- Bind Method -------------

        public void bind()
        {
            try
            {
                //eng.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                //eng.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //dt.Clear();
                gvEnquiry.Visible = true;
                AssignID();
                dt = eng.Get_Search();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                if (dt.Rows.Count > 0)
                {

                    ViewState["EnquiryDetails"] = dt;                    
                    gvEnquiry.DataSource = dt;
                    gvEnquiry.DataBind();
                }
                else
                {
                    gvEnquiry.DataSource = dt;
                    gvEnquiry.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Record Not Found !!!.','Information');", true);
                }

                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvEnquiry.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvEnquiry.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvEnquiry.Columns[1].Visible = false;
                }
                if (Request.QueryString["AddMember"] != null)
                {
                    gvEnquiry.Columns[0].Visible = false;
                    gvEnquiry.Columns[1].Visible = false;
                    gvEnquiry.Columns[2].Visible = false;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        #region ------------- Check From Date And To Date Validation
        int flag1 = 0;
        protected int chkFromDateNotLessToDate()
        {
            DateTime FromDate;
            DateTime ToDate;

            if (txtFromDate.Text == string.Empty)
            {
                flag1 = 1;
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter From Date !!!','Information');", true);
            }
            else if (txtFromDate.Text == string.Empty)
            {
                flag1 = 1;
                txtToDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter To Date !!!','Information');", true);
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
                    flag1 = 0;
                    eng.FromDate = FromDate;
                    eng.ToDate = ToDate;
                }
                else
                {
                    flag1 = 1;
                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Error", "toastr.info('From Date Should Not Be Greater Than To Date !!!','Information');", true);
                }
            }

            return flag1;

        }
        #endregion


        public void SearchByDate()
        {
            flag1 = chkFromDateNotLessToDate();

            if (flag1 == 0)
            {
                eng.Action = "SearchAll";
                eng.DateCategory = "EnquiryDate";
                bind();
            }
                   
            //DateTime Fromdate;
            //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            //{ }
            //eng.FromDate = Fromdate;
            //DateTime Todate;
            //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            //{ }
            //eng.ToDate = Todate;
            //if (Fromdate > Todate)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('From Date Should Be Less than To Date !!!','Error');", true);
            //    return;
            //}
            //else
            //{
            //    eng.Action = "SearchAll";
            //    eng.DateCategory = "EnquiryDate";
            //    if (txtSearch.Enabled == true && txtSearch.Text == string.Empty)
            //    {
            //        txtSearch.Focus();
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter All Fields !!!','Error');", true);
            //    }
            //    else
            //    {
            //        txtSearch.Style.Add("border", "1px solid silver ");
            //        //Search();
            //        bind();
            //    }
            //}
        }

        #region ---------- Search Button ----------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime Fromdate;
                //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
                //{ }
                //eng.FromDate = Fromdate;
                //DateTime Todate;
                //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
                //{ }
                //if (Fromdate > Todate)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('From Date Should Be Less than To Date !!!','Error');", true);
                //    return;
                //}
                //else
                //{
                //    eng.ToDate = Todate;
                    SearchByDate();
                    Flag = 1;
                    
                //}
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }

        #endregion

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            eng.Action = "SearchByCategory";
            Search();
            Flag = 2;
            btnEnquiryDate.Focus();
        }

        protected void btnEnquiryDate_Click(object sender, EventArgs e)
        {
            //DateTime Fromdate;
            //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            //{ }
            //eng.FromDate = Fromdate;
            //DateTime Todate;
            //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            //{ }
            //eng.ToDate = Todate;
            //if (Fromdate > Todate)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('From Date Should Be Less than To Date !!!','Error');", true);
            //    return;
            //}
            //else
            //{
                SearchByDate();
                Flag = 1;
            //}
        }

        protected void btnFollowupDate_Click(object sender, EventArgs e)
        {
            //DateTime Fromdate;
            //if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            //{ }
            //eng.FromDate = Fromdate;
            //DateTime Todate;
            //if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            //{ }
            //eng.ToDate = Todate;
            //if (Fromdate > Todate)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('From Date Should Be Less than To Date !!!','Error');", true);
            //    return;
            //}
            //else

            flag1 = chkFromDateNotLessToDate();

            if (flag1 == 0)           
            {
                eng.Action = "SearchAll";
                eng.DateCategory = "FollowupDate";
                bind();
                Flag = 3;
            }
        }

        protected void btnEnqDtWithCategory_Click(object sender, EventArgs e)
        {

            flag1 = chkFromDateNotLessToDate();

            if (flag1 == 0)
            {
                if (ddlSearch.SelectedValue != "--Select--")
                {
                    if (txtSearch.Text != string.Empty)
                    {
                        // eng.ToDate = Todate;
                        eng.Action = "SearchByDateCategory";
                        eng.DateCategory = "EnquiryDateWithCategory";
                        Search();
                        Flag = 4;
                    }
                    else
                    {
                        txtSearch.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                    return;
                    }
                }
                else
                {
                    ddlSearch.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Please Select Category !!!.','Information');", true);
                    return;
                }
               
            }


            //if (ddlSearch.SelectedValue == "--Select--" && txtSearch.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
            //    return;
            //}
            //else if (ddlSearch.SelectedValue != "--Select--" && txtSearch.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
            //    return;
            //}
            //else
            //{
            //    DateTime Fromdate;
            //    if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            //    { }
            //    eng.FromDate = Fromdate;
            //    DateTime Todate;
            //    if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            //    { }
            //    if (Fromdate > Todate)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('From Date Should Be Less than To Date !!!','Error');", true);
            //        return;
            //    }
            //    else
            //    {
            //        eng.ToDate = Todate;
            //        eng.Action = "SearchByDateCategory";
            //        eng.DateCategory = "EnquiryDateWithCategory";
            //        Search();
            //        Flag = 4;
            //    }
            //}
        }

        protected void btnFollDtWithCategory_Click(object sender, EventArgs e)
        {
            flag1 = chkFromDateNotLessToDate();

            if (flag1 == 0)
            {
                if (ddlSearch.SelectedValue != "--Select--")
                {
                    if (txtSearch.Text != string.Empty)
                    {
                        eng.Action = "SearchByDateCategory";
                        eng.DateCategory = "FollowupDateWithCategory";
                        Search();
                        Flag = 5;
                    }
                    else
                    {
                        txtSearch.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Information", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
                    return;
                    }
                }
                else
                {
                    ddlSearch.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
                    return;
                }
               
            }

            //if (ddlSearch.SelectedValue == "--Select--" && txtSearch.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Please Select Category !!!.','Information');", true);
            //    return;
            //}
            //else if (ddlSearch.SelectedValue != "--Select--" && txtSearch.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Enter Data On Search Field !!!.','Information');", true);
            //    return;
            //}
            //else
            //{
            //    DateTime Fromdate;
            //    if (DateTime.TryParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Fromdate))
            //    { }
            //    eng.FromDate = Fromdate;
            //    DateTime Todate;
            //    if (DateTime.TryParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todate))
            //    { }
            //    eng.ToDate = Todate;
            //    if (Fromdate > Todate)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('From Date Should Be Less than To Date !!!','Error');", true);
            //        return;
            //    }
            //    else
            //    {
            //        eng.Action = "SearchByDateCategory";
            //        eng.DateCategory = "FollowupDateWithCategory";
            //        Search();
            //        Flag = 5;
            //    }
            //}
        }
      

        protected void gvEnqFoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        string FNameViewEnqFoll = "FNameViewEnqFoll";
        protected void btnEnqFollowup_Command(object sender, CommandEventArgs e)
        {
            int Enq_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("EnquiryFollowup.aspx?Enq_ID=" + HttpUtility.UrlEncode(Enq_AutoID.ToString()) + " &FNameViewEnqFoll=" + HttpUtility.UrlEncode(FNameViewEnqFoll.ToString()));
        }

        protected void btnName_Command(object sender, CommandEventArgs e)
        {
            int Enq_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + HttpUtility.UrlEncode(Enq_AutoID.ToString()) + " &FNameViewEnqFoll=" + HttpUtility.UrlEncode(FNameViewEnqFoll.ToString()));
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

            gvEnquiry.DataSource = null;
            gvEnquiry.DataBind();
            AssignMonthDate();
            ddlSearch.SelectedValue = "--Select--";
            txtSearch.Text = "";
            txtFromDate.Focus();
            SearchByDate();
            Flag = 1;

        }

        #region ----------------- Export To Excle Record ----------------
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        //protected void ExportToExcel()
        //{
        //    try
        //    {

        //        if (ViewState["EnquiryDetails"] != null)
        //        {
        //            dt = (DataTable)ViewState["EnquiryDetails"]; ;
        //            if (dt.Rows.Count > 0)
        //            {
        //                Response.Clear();
        //                Response.Buffer = true;
        //                Response.ClearContent();
        //                Response.ClearHeaders();

        //                Response.AddHeader("content-disposition", "attachment;filename=EnquiryDetails" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
        //                Response.Charset = "";
        //                Response.ContentType = "application/vnd.ms-excel";
        //                using (StringWriter sw = new StringWriter())
        //                {
        //                    HtmlTextWriter hw = new HtmlTextWriter(sw);

        //                    //To Export all pages
        //                    gvEnquiry.Columns[0].Visible = false;
        //                    gvEnquiry.Columns[1].Visible = false;
        //                    gvEnquiry.Columns[2].Visible = false;
        //                    gvEnquiry.Columns[3].Visible = false;
        //                    gvEnquiry.AllowPaging = false;

        //                    gvEnquiry.DataSource = dt;
        //                    gvEnquiry.DataBind();
        //                    gvEnquiry.HeaderRow.BackColor = Color.White;
        //                    foreach (TableCell cell in gvEnquiry.HeaderRow.Cells)
        //                    {
        //                        cell.BackColor = gvEnquiry.HeaderStyle.BackColor;
        //                    }

        //                    foreach (GridViewRow row in gvEnquiry.Rows)
        //                    {
        //                        row.BackColor = Color.White;
        //                        foreach (TableCell cell in row.Cells)
        //                        {
        //                            cell.CssClass = "textmode";
        //                            List<Control> controls = new List<Control>();
        //                            //Add controls to be removed to Generic List
        //                            foreach (Control control in cell.Controls)
        //                            {
        //                                controls.Add(control);
        //                            }

        //                            foreach (Control control in controls)
        //                            {
        //                                switch (control.GetType().Name)
        //                                {
        //                                    case "HyperLink":
        //                                        cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
        //                                        break;

        //                                    case "LinkButton":
        //                                        cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
        //                                        break;

        //                                }
        //                                cell.Controls.Remove(control);

        //                            }
        //                        }
        //                    }


        //                    gvEnquiry.GridLines = GridLines.Both;
        //                    gvEnquiry.RenderControl(hw);

        //                    //style to format numbers to string

        //                    //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //                    //Response.Write(style);
        //                    Response.Output.Write(sw.ToString());
        //                    Response.Flush();
        //                    Response.End();

        //                }
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Data GridVIew Is Empty , Can Not Export !!!.','Information');", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.info('Data GridVIew Is Empty , Can Not Export !!!.','Information');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
        //        ErrorHandiling.SendErrorToText(ex);
        //    }

        //}



        protected void ExportToExcel()
        {
            try
            {
                if (ViewState["EnquiryDetails"] != null)
                {
                    dt = (DataTable)ViewState["EnquiryDetails"]; ;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.AddHeader("content-disposition", "attachment;filename=EnqDetails_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xls");
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.ms-excel";
                        using (StringWriter sw = new StringWriter())
                        {
                            HtmlTextWriter hw = new HtmlTextWriter(sw);
                            gvEnquiry.Columns[0].Visible = false;
                            gvEnquiry.Columns[1].Visible = false;
                            gvEnquiry.Columns[2].Visible = false;
                            gvEnquiry.Columns[3].Visible = false;
                            gvEnquiry.AllowPaging = false;
                            gvEnquiry.DataSource = dt;
                            gvEnquiry.DataBind();
                            gvEnquiry.HeaderRow.BackColor = Color.White;
                            foreach (TableCell cell in gvEnquiry.HeaderRow.Cells)
                            {
                                cell.BackColor = gvEnquiry.HeaderStyle.BackColor;
                            }
                            foreach (GridViewRow row in gvEnquiry.Rows)
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
                            gvEnquiry.GridLines = GridLines.Both;
                            gvEnquiry.RenderControl(hw);
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



        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        #endregion

        #region --------- Remove Images --------------
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveLogo();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void RemoveLogo()
        {
            imgMember.ImageUrl = "";

            if (ViewState["ImageUrl"] != null)
            {
                string img = ViewState["ImageUrl"].ToString();
                File.Delete(Server.MapPath(img));
            }
            else
            {
                ViewState["ImageUrl"] = "";
            }

        }

        #endregion


    }
}