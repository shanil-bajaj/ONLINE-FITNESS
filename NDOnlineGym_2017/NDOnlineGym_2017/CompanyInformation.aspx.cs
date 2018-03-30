using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessAccessLayer;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DataAccessLayer;
using System.Text.RegularExpressions;
using System.Globalization;


namespace NDOnlineGym_2017
{
    public partial class CompanyInformation : System.Web.UI.Page
    {
        BalCompanyInformation ObjCompInfo = new BalCompanyInformation();
        DataTable dataTable = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
           // txtTermsAndConditions.InnerText = "ghjhghjghjgfdfdgffgh";
            try
            {
                if (!IsPostBack)
                {
                   AssignCompanyID();
                   
                    fillForm();
                    //if (btnSave.Text == "Edit")
                    //{
                    //    btnClear.Visible = false;
                    //}
                   
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
            
        }


         private void AssignCompanyID()
          {
              ObjCompInfo.Action = "GetCompanyID1";
              dataTable = ObjCompInfo.GetDetails();
              if (dataTable.Rows.Count > 0)
              {
                  txtCompanyID.Text = dataTable.Rows[0]["Company_ID1"].ToString();
              }
          }
          


        private void fillForm()
        {
            try
            {
                ObjCompInfo.Action = "SELECT_COMPANY_INFO";

                dataTable = ObjCompInfo.GetDetails();
                if (dataTable.Rows.Count > 0)
                {                   
                    ReadMode();

                    txtCompanyID.Text = dataTable.Rows[0]["Company_ID1"].ToString();
                    txtCompanyName.Text = dataTable.Rows[0]["CompanyName"].ToString();
                    ddlStatus.SelectedValue = dataTable.Rows[0]["Status"].ToString();
                    txtGSTNo.Text = dataTable.Rows[0]["GSTNo"].ToString();

                    txtAddress1.Text = dataTable.Rows[0]["Address1"].ToString();
                    txtAddress2.Text = dataTable.Rows[0]["Address2"].ToString();
                    txtLocation.Text = dataTable.Rows[0]["Location"].ToString();
                    txtState.Text = dataTable.Rows[0]["State"].ToString();
                    txtCity.Text = dataTable.Rows[0]["City"].ToString();

                    txtContact1.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtContact2.Text = dataTable.Rows[0]["Contact2"].ToString();
                    txtLandLine.Text = dataTable.Rows[0]["LandLine"].ToString();
                    txtEmailID.Text = dataTable.Rows[0]["Email"].ToString();
                    txtWebsite.Text = dataTable.Rows[0]["Website"].ToString();
                    txtCollectionSMSNo.Text = dataTable.Rows[0]["CollectionSMS"].ToString();

                    txtTermsAndCondition.Text = dataTable.Rows[0]["TermsAndCondition"].ToString();
                    txtOwnername.Text = dataTable.Rows[0]["OwnerName"].ToString();
                    txtOwnerContact.Text = dataTable.Rows[0]["OwnerContact"].ToString();
                    txtOwnerEmailID.Text = dataTable.Rows[0]["OwnerEmail"].ToString();
                    txtContactPerson.Text = dataTable.Rows[0]["ContactPerson"].ToString();
                    txtContactPersonNo.Text = dataTable.Rows[0]["ContactPersonNo"].ToString();
                    txtEontactPersonEmail.Text = dataTable.Rows[0]["ContactPersonEmail"].ToString();
                    imgMember.ImageUrl = dataTable.Rows[0]["CompanyLogoPath"].ToString();
                    ViewState["ImageUrl"] = dataTable.Rows[0]["CompanyLogoPath"].ToString();
                    ViewState["Company_AutoID"] = dataTable.Rows[0]["Company_AutoID"].ToString();
                }
                else
                {
                    btnSave.Text = "Save";
                    WriteMode();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void ReadMode()
        {           
            //txtCompanyID.Enabled = false;
            txtCompanyName.Enabled = false;
            ddlStatus.Enabled = false;
            txtGSTNo.Enabled = false;

            txtAddress1.Enabled = false;
            txtAddress2.Enabled = false;
            txtState.Enabled = false;
            txtCity.Enabled = false;
            txtLocation.Enabled = false;

            txtContact1.Enabled = false;
            txtContact2.Enabled = false;
            txtLandLine.Enabled = false;
            txtEmailID.Enabled = false;
            txtWebsite.Enabled = false;
            txtCollectionSMSNo.Enabled = false;

            txtTermsAndCondition.Enabled = false;

            txtOwnername.Enabled = false;
            txtOwnerEmailID.Enabled = false;
            txtOwnerContact.Enabled = false;
            txtContactPerson.Enabled = false;
            txtContactPersonNo.Enabled = false;
            txtEontactPersonEmail.Enabled = false;

            fileLogo.Enabled = false;
            btnRemove.Enabled = false;

        }


        private void WriteMode()
        {           
            //txtCompanyID.Enabled = true;
            txtCompanyName.Enabled = true;
            ddlStatus.Enabled = true;
            txtGSTNo.Enabled = true;

            txtAddress1.Enabled = true;
            txtAddress2.Enabled = true;
            txtState.Enabled = true;
            txtCity.Enabled = true;
            txtLocation.Enabled = true;

            txtContact1.Enabled = true;
            txtContact2.Enabled = true;
            txtLandLine.Enabled = true;
            txtEmailID.Enabled = true;
            txtWebsite.Enabled = true;
            txtCollectionSMSNo.Enabled = true;

            txtTermsAndCondition.Enabled = true;

            txtOwnername.Enabled = true;
            txtOwnerEmailID.Enabled = true;
            txtOwnerContact.Enabled = true;
            txtContactPerson.Enabled = true;
            txtContactPersonNo.Enabled = true;
            txtEontactPersonEmail.Enabled = true;

            fileLogo.Enabled = true;
            btnRemove.Enabled = true;

        }


        private void SetImagePath()
        {
            if (fileLogo.HasFile)
            {
                serverfilrpath = ViewState["serverfilrpath1"].ToString();
                ObjCompInfo.CompanyLogoPath = serverfilrpath;
            }
            else
            {
                if (imgMember.ImageUrl == "")
                {
                    serverfilrpath = "";
                    ObjCompInfo.CompanyLogoPath = "";
                }
                else
                {
                    serverfilrpath = ViewState["ImageUrl"].ToString();
                    ObjCompInfo.CompanyLogoPath = serverfilrpath;
                }
            }
        }


        string serverfilrpath = string.Empty;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    ObjCompInfo.Action = "INSERT";
                    AddParameters();
                    ImageUplode();
                    SetImagePath();
                    int i = ObjCompInfo.Insert_CompanyInformation();
                    if (i > 0)
                    {
                        fillForm();
                        btnSave.Text = "Edit";
                        //btnClear.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);                                       
                    }                  
                }
                else if (btnSave.Text == "Update")
                {
                    ObjCompInfo.Action = "Update";
                    AddParameters();
                    ImageUplode();
                    SetImagePath();
                    ObjCompInfo.Company_AutoID = Convert.ToInt32(ViewState["Company_AutoID"]);

                    int res = ObjCompInfo.Insert_CompanyInformation();
                    if (res > 0)
                    {
                        fillForm();
                        ReadMode();
                        btnSave.Text = "Edit";
                        //btnClear.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);                        
                                               
                    }                    
                }
                else if (btnSave.Text == "Edit")
                {              
                    btnSave.Text="Update";
                    fillForm();
                    WriteMode();
                    //btnClear.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }



            /*
             try
            {
                AddParameters();
                ImageUplode();

                if (fileLogo.HasFile)
                {
                    serverfilrpath = ViewState["serverfilrpath1"].ToString();
                    ObjCompInfo.CompanyLogoPath = serverfilrpath;
                }
                else
                {
                    if (imgMember.ImageUrl == "")
                    {
                        serverfilrpath = "";
                        ObjCompInfo.CompanyLogoPath = "";
                    }
                    else
                    {
                        serverfilrpath = ViewState["ImageUrl"].ToString();
                        ObjCompInfo.CompanyLogoPath = serverfilrpath;
                    }
                }

                if (btnSave.Text == "Save")
                {
                    ObjCompInfo.Action = "INSERT";
                    int i = ObjCompInfo.Insert_CompanyInformation();
                    if (i > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        Clear_Record();
                        AssignCompanyID();
                    }
                    if (i == 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('User Name And Password Cannot Duplicate !!!','Error');", true);
                    }
                }
                else
                {
                    ObjCompInfo.Action = "Update";
                    ObjCompInfo.Company_AutoID = Convert.ToInt32(ViewState["Company_AutoID"]);
                    int res = ObjCompInfo.Insert_CompanyInformation();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        Clear_Record();
                        btnSave.Text = "Save";
                        AssignCompanyID();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Is Already Exists !!!','Error');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
            */

        }

        

        private void AddParameters()
        {
            ObjCompInfo.Company_ID1 = Convert.ToInt32(txtCompanyID.Text);
            ObjCompInfo.CompanyName = Regex.Replace(txtCompanyName.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");//txtCompanyName.Text;
            ObjCompInfo.Status = ddlStatus.SelectedValue;
            ObjCompInfo.GSTNo = Regex.Replace(txtGSTNo.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");

            ObjCompInfo.Address1 = Regex.Replace(txtAddress1.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.Address2 = Regex.Replace(txtAddress2.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.State = Regex.Replace(txtState.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.City = Regex.Replace(txtCity.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.Location = Regex.Replace(txtLocation.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");

            ObjCompInfo.Contact1 = Regex.Replace(txtContact1.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.Contact2 = Regex.Replace(txtContact2.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.Landline = Regex.Replace(txtLandLine.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.Email = Regex.Replace(txtEmailID.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.Website = Regex.Replace(txtWebsite.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.CollectionSMS = Regex.Replace(txtCollectionSMSNo.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");

            ObjCompInfo.TermsAndCondition = Regex.Replace(txtTermsAndCondition.Text, "^[ \t\r\n]+|[ \t\r\n]+$", ""); //HttpUtility.HtmlEncode(txtTermsAndCondition.Text);//txtTermsAndCondition.Text;            
            ObjCompInfo.OwnerName = Regex.Replace(txtOwnername.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.OwnerEmail = Regex.Replace(txtOwnerEmailID.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.OwnerContact = Regex.Replace(txtOwnerContact.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.ContactPerson = Regex.Replace(txtContactPerson.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.ContactPersonNo = Regex.Replace(txtContactPersonNo.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            ObjCompInfo.ContactPersonEmail = Regex.Replace(txtEontactPersonEmail.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
           
           // ObjCompInfo.Username = txtUsername.Text;
           // ObjCompInfo.Password = txtPassword.Text;

        }

        private void ImageUplode()
        {
            string newfileName = string.Empty;


            if ((fileLogo.PostedFile != null) && (fileLogo.PostedFile.ContentLength > 0))
            {
                Guid uid = Guid.NewGuid();
                string fn = System.IO.Path.GetFileName(fileLogo.PostedFile.FileName);
                DateTime dataTable = DateTime.Now;
                newfileName = txtCompanyName.Text.Trim() + "_" + dataTable.Year.ToString() + dataTable.Month.ToString() + dataTable.Day.ToString() + dataTable.Hour.ToString() + dataTable.Minute.ToString() + dataTable.Second.ToString();
                string fileName = Path.GetFileName(fileLogo.PostedFile.FileName);
                string primaryFileName = Path.GetFileNameWithoutExtension(fileName);
                string fileExtentionName = Path.GetExtension(fileName);
                string SaveLocation = Server.MapPath("/Logo/") + newfileName + fileExtentionName;
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
                        ViewState["serverfilrpath1"] = "/Logo/" + newfileName + fileExtentionName;
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Image size cannot be more then 1 MB !!!','Error');", true);
                        //    txtName.Focus();
                        //    return;
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invaild Format !!!','Error');", true);

                        return;
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandiling.SendErrorToText(ex);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

                    return;
                }
            }

        }

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

        private void Clear_Record()
        {
            //txtCompanyID.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            ddlStatus.SelectedIndex=0;
            txtGSTNo.Text = string.Empty;

            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtState.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtLocation.Text = string.Empty;

            txtContact1.Text = string.Empty;
            txtContact2.Text = string.Empty;
            txtLandLine.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtCollectionSMSNo.Text = string.Empty;

            txtTermsAndCondition.Text = string.Empty;            

            txtOwnername.Text = string.Empty;
            txtOwnerEmailID.Text = string.Empty;
            txtOwnerContact.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtContactPersonNo.Text = string.Empty;
            txtEontactPersonEmail.Text = string.Empty;
            //txtUsername.Text = string.Empty;
            //txtPassword.Text = string.Empty;

            imgMember.ImageUrl = "Icons/DefaultLogo.png";
            btnSave.Text = "Save";

            ViewState["serverfilrpath1"] = string.Empty;
            ViewState["ImageUrl"] = string.Empty;
            ViewState["Company_AutoID"] = string.Empty;
        }

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
            string img = ViewState["ImageUrl"].ToString();
            File.Delete(Server.MapPath(img));           
        }
       

        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    BindCompanyInfoGrid();
        //}

        /*
        private void BindCompanyInfoGrid()
        {
            try
            {
                //ObjCompInfo.Action = "SELECT_COMPANY_INFO";
                dataTable = ObjCompInfo.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    gvCompanyInfo.Visible = true;
                    gvCompanyInfo.DataSource = dataTable;
                    gvCompanyInfo.DataBind();
                }
                else
                {
                    gvCompanyInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ObjCompInfo.Company_AutoID = Convert.ToInt32(e.CommandArgument);
                ObjCompInfo.Action = "Delete";
                int i = ObjCompInfo.Insert_CompanyInformation();
                if (i > 0)
                {
                    BindCompanyInfoGrid();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);                
            }
        }

        
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ViewState["Company_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSave.Text = "Update";

                ObjCompInfo.Action = "SELECT_BY_ID_EDIT";
                ObjCompInfo.Company_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                
                dataTable = ObjCompInfo.GetDetails();
                if (dataTable.Rows.Count > 0)
                {
                    txtCompanyID.Text = dataTable.Rows[0]["Company_ID1"].ToString();
                    txtCompanyName.Text = dataTable.Rows[0]["CompanyName"].ToString();
                    ddlStatus.SelectedValue = dataTable.Rows[0]["Status"].ToString();
                    txtGSTNo.Text = dataTable.Rows[0]["GSTNo"].ToString();

                    txtAddress1.Text = dataTable.Rows[0]["Address1"].ToString();
                    txtAddress2.Text = dataTable.Rows[0]["Address2"].ToString();
                    txtLocation.Text = dataTable.Rows[0]["Location"].ToString();
                    txtState.Text = dataTable.Rows[0]["State"].ToString();
                    txtCity.Text = dataTable.Rows[0]["City"].ToString();

                    txtContact1.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtContact2.Text = dataTable.Rows[0]["Contact2"].ToString();
                    txtLandLine.Text = dataTable.Rows[0]["LandLine"].ToString();
                    txtEmailID.Text = dataTable.Rows[0]["Email"].ToString();
                    txtWebsite.Text = dataTable.Rows[0]["Website"].ToString();                    
                    txtCollectionSMSNo.Text = dataTable.Rows[0]["CollectionSMS"].ToString();
                    
                    txtTermsAndCondition.Text = dataTable.Rows[0]["TermsAndCondition"].ToString();
                    txtOwnername.Text = dataTable.Rows[0]["OwnerName"].ToString();
                    txtOwnerContact.Text = dataTable.Rows[0]["OwnerContact"].ToString();
                    txtOwnerEmailID.Text = dataTable.Rows[0]["OwnerEmail"].ToString();                                        
                    txtContactPerson.Text = dataTable.Rows[0]["ContactPerson"].ToString();
                    txtContactPersonNo.Text = dataTable.Rows[0]["ContactPersonNo"].ToString();
                    txtEontactPersonEmail.Text = dataTable.Rows[0]["ContactPersonEmail"].ToString();
                    imgMember.ImageUrl = dataTable.Rows[0]["CompanyLogoPath"].ToString();
                    ViewState["ImageUrl"] = dataTable.Rows[0]["CompanyLogoPath"].ToString();
                    txtUsername.Text = dataTable.Rows[0]["Username"].ToString();
                    txtPassword.Text = dataTable.Rows[0]["Password"].ToString();
                }
                Flag = 1;
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }*/


        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear_Record();
               // AssignCompanyID();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);                
            }
        }

        /*
        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCompanyInfo.PageIndex = 0;
            SearchIndexChanged();
            ddlSearch.Focus();
        }
        */

        /*
        private void SearchIndexChanged()
        {
            try
            {
                ObjCompInfo.Search_By = ddlSearch.SelectedItem.Text;

                if (ddlSearch.SelectedItem.Value == "--Select--")
                {
                    ObjCompInfo.Action = "SELECT_All_COMPANY_INFO";
                }
                else if (ddlSearch.SelectedItem.Value == "Company Name")
                {
                    ObjCompInfo.Action = "SELECT_By_CompanyName";                                        
                }
                else if (ddlSearch.SelectedItem.Value == "Owner Name")
                {
                    ObjCompInfo.Action = "SELECT_By_OwnerName";
                }
                else if (ddlSearch.SelectedItem.Value == "Owner Contact")
                {
                    ObjCompInfo.Action = "SELECT_By_OwnerContact";
                }
                else if (ddlSearch.SelectedItem.Value == "Contact 1")
                {
                    ObjCompInfo.Action = "SELECT_By_Contact1";
                }
                else if (ddlSearch.SelectedItem.Value == "Contact 2")
                {
                    ObjCompInfo.Action = "SELECT_By_Contact2";
                }
                else if (ddlSearch.SelectedItem.Value == "Contact Person No")
                {
                    ObjCompInfo.Action = "SELECT_By_ContactPersonNo";
                }
                else if (ddlSearch.SelectedItem.Value == "Contact Person")
                {
                    ObjCompInfo.Action = "SELECT_By_ContactPerson";
                }
                else if (ddlSearch.SelectedItem.Value == "City")
                {
                    ObjCompInfo.Action = "SELECT_By_City";
                }
                else if (ddlSearch.SelectedItem.Value == "State")
                {
                    ObjCompInfo.Action = "SELECT_By_State";
                }
                else if (ddlSearch.SelectedItem.Value == "Username")
                {
                    ObjCompInfo.Action = "SELECT_By_Username";
                }
                else if (ddlSearch.SelectedItem.Value == "Status")
                {
                    ObjCompInfo.Action = "SELECT_By_Status";
                }

            }
            catch (Exception ex)
            {                
                ErrorHandiling.SendErrorToText(ex);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchIndexChanged();
                ObjCompInfo.Search_By = txtSearch.Text.Trim();
                BindCompanyInfoGrid();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
            }
           
        }

        */

       


    }
}