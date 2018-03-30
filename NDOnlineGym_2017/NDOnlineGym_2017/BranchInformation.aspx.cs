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

namespace NDOnlineGym_2017
{
    public partial class BranchInformation : System.Web.UI.Page
    {
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();
        DataTable dt = new DataTable();
        string newfileName = string.Empty;
        string serverfilrpath = string.Empty;
        int Branch_AutoID;
        static int flag;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlCompany.Focus();
                    Get_BranchID1();
                    bindDDLCompanyName();
                   
                    if (Request.QueryString["Branch_AutoID"] != null)
                    {
                        Branch_AutoID = Convert.ToInt32(Request.QueryString["Branch_AutoID"].ToString());
                        GetDataForEdit(Branch_AutoID);
                    }
                    else if (Session["Branch_ID"] != null)
                    {
                        Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"].ToString());
                        GetDataForEdit(Branch_AutoID);
                        btnSave.Text = "Edit";
                        //btnClear.Visible = false;
                        Disabled();
                    }
                    else
                    {
                        btnSave.Text = "Save";
                        Enabled();
                    }
                }
            }
            catch (Exception ex)
            {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }



        public void Disabled()
        {
            txtBranchID.Enabled = false;
            txtBranchName.Enabled = false;
            txtContact1.Enabled = false;
            txtContact2.Enabled = false;
            txtLandLine.Enabled = false;
            txtEmailID.Enabled = false;
            txtWebsite.Enabled = false;
            txtAddress1.Enabled = false;
            txtAddress2.Enabled = false;
            txtLocation.Enabled = false;
            txtState.Enabled = false;
            txtCity.Enabled = false;
            txtCollectionSMSNo.Enabled = false;
            txtGSTNo.Enabled = false;
            txtTermsAndConditions.Enabled = false;
            txtOwnername.Enabled = false;
            txtOwnerContact.Enabled = false;
            txtOwnerEmailID.Enabled = false;
            txtState.Enabled = false;
            ddlCompany.Enabled = false;
            txtContactPerson.Enabled = false;
            txtContactPersonNo.Enabled = false;
            txtContactPersonEmail.Enabled = false;
            FileUploadeLogo.Enabled = false;
            ddlStatus.Enabled = false;
        }

        public void Enabled()
        {
            ddlStatus.Enabled = true;
            txtBranchID.Enabled = true;
            txtBranchName.Enabled = true;
            txtContact1.Enabled = true;
            txtContact2.Enabled = true;
            txtLandLine.Enabled = true;
            txtEmailID.Enabled = true;
            txtWebsite.Enabled = true;
            txtAddress1.Enabled = true;
            txtAddress2.Enabled = true;
            txtLocation.Enabled = true;
            txtState.Enabled = true;
            txtCity.Enabled = true;
            txtCollectionSMSNo.Enabled = true;
            txtGSTNo.Enabled = true;
            txtTermsAndConditions.Enabled = true;
            txtOwnername.Enabled = true;
            txtOwnerContact.Enabled = true;
            txtOwnerEmailID.Enabled = true;
            txtState.Enabled = true;
            ddlCompany.Enabled = true;
            txtContactPerson.Enabled = true;
            txtContactPersonNo.Enabled = true;
            txtContactPersonEmail.Enabled = true;
            FileUploadeLogo.Enabled = true;
        }
        
        public void Get_BranchID1()
        {
            try
            {
                dt = obBalBranchInformation.Get_BranchID1();
                txtBranchID.Text = dt.Rows[0]["Branch_ID1"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLCompanyName()
        {
            try
            {
                dt = obBalBranchInformation.SELECT_CompanyDetails();
                if (dt.Rows.Count > 0)
                {
                    ddlCompany.DataSource = dt;
                    ddlCompany.Items.Clear();
                    ddlCompany.DataValueField = "Company_AutoID";
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataBind();
                    ddlCompany.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearRecord()
        {

            txtBranchID.Text = "";
            txtBranchName.Text = "";
            txtContact1.Text = "";
            txtContact2.Text = "";
            txtLandLine.Text = "";
            txtEmailID.Text = "";
            txtWebsite.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtLocation.Text = "";
            txtState.Text = "";
            txtCity.Text = "";
            txtCollectionSMSNo.Text = "";
            txtGSTNo.Text = "";
            txtTermsAndConditions.Text = "";
            txtOwnername.Text = "";
            txtOwnerContact.Text = "";
            txtOwnerEmailID.Text = "";
            ddlStatus.SelectedValue = "--Select--";
            ddlCompany.SelectedValue = "--Select--";
            txtContactPerson.Text = "";
            txtContactPersonNo.Text = "";
            txtContactPersonEmail.Text = "";
            btnSave.Text = "Save";
            Get_BranchID1();
        }

        public void ImageUplode()
        {
            if ((FileUploadeLogo.PostedFile != null) && (FileUploadeLogo.PostedFile.ContentLength > 0))
            {
                Guid uid = Guid.NewGuid();
                string fn = System.IO.Path.GetFileName(FileUploadeLogo.PostedFile.FileName);
                DateTime dt = DateTime.Now;
                newfileName = txtBranchName.Text.Trim() + "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                string fileName = Path.GetFileName(FileUploadeLogo.PostedFile.FileName);
                string primaryFileName = Path.GetFileNameWithoutExtension(fileName);
                string fileExtentionName = Path.GetExtension(fileName);
                string SaveLocation = Server.MapPath("/Logo/") + newfileName + fileExtentionName;
                try
                {
                    string fileExtention = FileUploadeLogo.PostedFile.ContentType;
                    int fileLenght = FileUploadeLogo.PostedFile.ContentLength;
                    if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                    {
                        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUploadeLogo.PostedFile.InputStream);
                        System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);
                        objImage.Save(SaveLocation, ImageFormat.Jpeg);
                        ViewState["serverfilrpath1"] = "/Logo/" + newfileName + fileExtentionName;
                    }
                    else
                    {
                         ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invaild Format !!!','Error');", true);
                        txtBranchName.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    txtBranchName.Focus();
                    return;
                }
            }
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(85);
            var newHeight = (int)(75);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public void AddParameters()
        {
            try
            {
                obBalBranchInformation.Branch_ID1 = Convert.ToInt32(txtBranchID.Text);
                obBalBranchInformation.BranchName = txtBranchName.Text;
                obBalBranchInformation.Contact1 = txtContact1.Text;
                obBalBranchInformation.Contact2 = txtContact2.Text;
                obBalBranchInformation.Landline = txtLandLine.Text;
                obBalBranchInformation.Email = txtEmailID.Text;
                obBalBranchInformation.Website = txtWebsite.Text;
                obBalBranchInformation.Address1 = txtAddress1.Text;
                obBalBranchInformation.Address2 = txtAddress2.Text;
                obBalBranchInformation.Location = txtLocation.Text;
                obBalBranchInformation.State = txtState.Text;
                obBalBranchInformation.City = txtCity.Text;
                obBalBranchInformation.CollectionSMS = txtCollectionSMSNo.Text;
                obBalBranchInformation.GSTNo = txtGSTNo.Text;
                obBalBranchInformation.TermsAndCondition = txtTermsAndConditions.Text;
                obBalBranchInformation.OwnerName = txtOwnername.Text;
                obBalBranchInformation.OwnerContact = txtOwnerContact.Text;
                obBalBranchInformation.OwnerEmail = txtOwnerEmailID.Text;
                obBalBranchInformation.Status = ddlStatus.SelectedValue;
                if (ddlCompany.SelectedValue != "--Select--")
                    obBalBranchInformation.Company_AutoID = Convert.ToInt32(ddlCompany.SelectedValue);
                else
                {
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Company Name First !!!','Error');", true);
                    ddlCompany.Focus();
                    return;
                }
                obBalBranchInformation.ContactPerson = txtContactPerson.Text;
                obBalBranchInformation.ContactPersonNo = txtContactPersonNo.Text;
                obBalBranchInformation.ContactPersonEmail = txtContactPersonEmail.Text;
                
            }
            catch (Exception ex)
            {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                    AddParameters();
                    ImageUplode();
                    if (Request.QueryString["Flag"] != "1")
                    {
                        if (Request.Cookies["OnlineGym1"]["brIDHome"] == null)
                        {
                            if (FileUploadeLogo.HasFile)
                            {
                                serverfilrpath = ViewState["serverfilrpath1"].ToString();
                                obBalBranchInformation.BranchLogoPath = serverfilrpath;
                            }
                            else
                            {
                                obBalBranchInformation.BranchLogoPath = "";
                            }

                        }
                        else
                        {
                            if (FileUploadeLogo.HasFile)
                            {
                                serverfilrpath = ViewState["serverfilrpath1"].ToString();
                                obBalBranchInformation.BranchLogoPath = serverfilrpath;
                            }
                            else
                            {
                                if (imgBranch.ImageUrl == "")
                                {
                                    serverfilrpath = "";
                                    obBalBranchInformation.BranchLogoPath = "";
                                }
                                else
                                {
                                    serverfilrpath = ViewState["ImageUrl"].ToString();
                                    obBalBranchInformation.BranchLogoPath = serverfilrpath;
                                }
                            }
                        }
                    }
                    else
                    {
                        
                            if (FileUploadeLogo.HasFile)
                            {
                                serverfilrpath = ViewState["serverfilrpath1"].ToString();
                                obBalBranchInformation.BranchLogoPath = serverfilrpath;
                            }
                            else
                            {
                                if (imgBranch.ImageUrl == "")
                                {
                                    serverfilrpath = "";
                                    obBalBranchInformation.BranchLogoPath = "";
                                }
                                else
                                {
                                    serverfilrpath = ViewState["ImageUrl"].ToString();
                                    obBalBranchInformation.BranchLogoPath = serverfilrpath;
                                }
                            }
                        }


                        if (btnSave.Text == "Save")
                        {
                            obBalBranchInformation.Action = "Insert";
                            bool chkExistingBranchName = false;
                            chkExistingBranchName = obBalBranchInformation.Check_ExistingNameBranchInformation();
                            if (chkExistingBranchName == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Branch Name is already exists !!!','Error');", true);
                                txtCity.Focus();
                                return;
                            }
                            else
                            {
                                int res = obBalBranchInformation.Insert_BranchInformation();
                                if (res > 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                                    if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "MasterAdmin")
                                    {
                                        ClearRecord();
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Branch ID Is Already Exists !!!','Error');", true);
                                    txtCity.Focus();
                                    return;
                                }
                            }
                        }
                        else if (btnSave.Text == "Update")
                        {
                            if (Request.QueryString["Branch_AutoID"] != null)
                            {
                                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.QueryString["Branch_AutoID"]);
                            }
                            else if (Request.Cookies["OnlineGym1"]["brIDHome"] != null)
                            {
                                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                            }
                            bool chkExistingBranchName = false;
                            chkExistingBranchName = obBalBranchInformation.Check_ExistingNameBranchNameUpdate();
                            if (chkExistingBranchName == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Branch Name is already exists !!!','Error');", true);
                                txtCity.Focus();
                                return;
                            }
                            else
                            {
                                obBalBranchInformation.Action = "Update";
                                int res = obBalBranchInformation.Insert_BranchInformation();
                                if (res > 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                                    if (FileUploadeLogo.HasFile)
                                    {
                                        imgBranch.ImageUrl = ViewState["serverfilrpath1"].ToString();
                                    }
                                    else
                                    {
                                        imgBranch.ImageUrl = ViewState["ImageUrl"].ToString();
                                    }
                                    btnSave.Text = "Edit";
                                    //btnClear.Visible = false;
                                    Disabled();
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Branch ID Is Already Exists !!!','Error');", true);
                                    txtCity.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Enabled();
                            btnSave.Text = "Update";
                            //btnClear.Visible = false;
                        }
                    
            }
            catch (Exception ex)
            {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearRecord();
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                obBalBranchInformation.Branch_AutoID = Convert.ToInt32(e.CommandArgument);
                obBalBranchInformation.Action = "Delete";
                int i = obBalBranchInformation.Insert_BranchInformation();//DeleteBranchInformation();
                if (i > 0)
                {
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    ddlCompany.Focus();
                }
            }
            catch (Exception ex)
            {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
                return;
            }
        }

        
        public void GetDataForEdit(int Branch_AutoID)
        {
            if (ViewState["Branch_AutoID"] != null)
            {
                btnSave.Text = "Edit";
                //btnClear.Visible = false;
            }
            else
            {
                btnSave.Text = "Update";
               // btnClear.Visible = false;
            }
            obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Branch_AutoID);
            dt = obBalBranchInformation.SelectByID_BranchInformation();
            if (dt.Rows.Count > 0)
            {
                txtBranchID.Text = dt.Rows[0]["Branch_ID1"].ToString();
                txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                txtContact2.Text = dt.Rows[0]["Contact2"].ToString();
                txtLandLine.Text = dt.Rows[0]["LandLine"].ToString();
                txtEmailID.Text = dt.Rows[0]["Email"].ToString();
                txtWebsite.Text = dt.Rows[0]["Website"].ToString();
                txtAddress1.Text = dt.Rows[0]["Address1"].ToString();
                txtAddress2.Text = dt.Rows[0]["Address2"].ToString();
                txtLocation.Text = dt.Rows[0]["Location"].ToString();
                txtState.Text = dt.Rows[0]["State"].ToString();
                txtCity.Text = dt.Rows[0]["City"].ToString();
                txtCollectionSMSNo.Text = dt.Rows[0]["CollectionSMS"].ToString();
                txtGSTNo.Text = dt.Rows[0]["GSTNo"].ToString();
                txtTermsAndConditions.Text = dt.Rows[0]["TermsAndCondition"].ToString();
                txtOwnername.Text = dt.Rows[0]["OwnerName"].ToString();
                txtOwnerContact.Text = dt.Rows[0]["OwnerContact"].ToString();
                txtOwnerEmailID.Text = dt.Rows[0]["OwnerEmail"].ToString();
                ddlStatus.SelectedItem.Value = dt.Rows[0]["Status"].ToString();
                ddlStatus.SelectedItem.Text = dt.Rows[0]["Status"].ToString();
                ddlCompany.SelectedValue = dt.Rows[0]["Company_AutoID"].ToString();
                txtContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
                txtContactPersonNo.Text = dt.Rows[0]["ContactPersonNo"].ToString();
                txtContactPersonEmail.Text = dt.Rows[0]["ContactPersonEmail"].ToString();
                imgBranch.ImageUrl = dt.Rows[0]["BranchLogoPath"].ToString();
                ViewState["ImageUrl"] = dt.Rows[0]["BranchLogoPath"].ToString();
                
            }
            flag = 1;
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                Branch_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["Branch_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                GetDataForEdit(Branch_AutoID);
                if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "Admin")
                {
                    btnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            imgBranch.ImageUrl = "";

            if (ViewState["ImageUrl"] != null)
            {
                string img = ViewState["ImageUrl"].ToString();
                File.Delete(Server.MapPath(img));
            }
            else
            {
                ViewState["imagepath"] = "";
            }

        }

    

        protected void txtBranchName_TextChanged(object sender, EventArgs e)
        {
            //obBalBranchInformation.BranchName = txtBranchName.Text;
            //bool chkExistingBranchName = false;
            //chkExistingBranchName = obBalBranchInformation.Check_ExistingNameBranchInformation();
            //if (chkExistingBranchName == true)
            //{
            //     ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Branch Name is already exists !!!','Error');", true);
            //     txtBranchName.Focus();
            //    return;
            //}
        }
    }
}