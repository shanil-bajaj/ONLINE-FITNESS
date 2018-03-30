using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Globalization;
using DataAccessLayer;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;


namespace NDOnlineGym_2017
{
    public partial class NewUpdateNotificationHome : System.Web.UI.Page
    {
        BalNewUpdateNotificationHome objNewUpdateNotificationHome = new BalNewUpdateNotificationHome();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        DataTable dt = new DataTable();
        static int flag;
        string newfileName = string.Empty;
        string serverfilrpath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtHeading.Focus();
                AssignTodaysDate();
            }
        }

        #region ------------ Assign All Date ------------------
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtFromDate.Text = todaydate.ToString("dd-MM-yyyy");
            }
        }
        #endregion

        public void AddParameters()
        {
            DateTime Todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todaydate))
            { }

            objNewUpdateNotificationHome.Heading = txtHeading.Text;
            objNewUpdateNotificationHome.Information = txtNotification.Text;
            objNewUpdateNotificationHome.Date = Todaydate;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AddParameters();
                ImageUplode();
                    if (ViewState["SUpdate_AutoID"] == null)
                    {
                        if (FileUploadeImg.HasFile)
                        {
                            serverfilrpath = ViewState["serverfilrpath1"].ToString();
                            objNewUpdateNotificationHome.Image = serverfilrpath;
                        }
                        else
                        {
                            objNewUpdateNotificationHome.Image = "";
                        }

                    }
                    else
                    {
                        if (FileUploadeImg.HasFile)
                        {
                            serverfilrpath = ViewState["serverfilrpath1"].ToString();
                            objNewUpdateNotificationHome.Image = serverfilrpath;
                        }
                        else
                        {
                            if (imgUpdate.ImageUrl == "")
                            {
                                serverfilrpath = "";
                                objNewUpdateNotificationHome.Image = "";
                            }
                            else
                            {
                                serverfilrpath = ViewState["ImageUrl"].ToString();
                                objNewUpdateNotificationHome.Image = serverfilrpath;
                            }
                        }
                    }
                
                if (btnSave.Text == "Save")
                {
                    
                    objNewUpdateNotificationHome.Action = "Insert";
                    int res = objNewUpdateNotificationHome.Insert_NewUpdateNotificationHome();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        Clear();
                        btnClear.Visible = true;
                        objNewUpdateNotificationHome.Action = "Search";
                        BindGrid();
                        txtHeading.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Saved Failed !!!','Error');", true);
                    }

                }
                else if (btnSave.Text == "Update")
                {
                    AddParameters();
                    objNewUpdateNotificationHome.Action = "Update";
                    objNewUpdateNotificationHome.SUpdate_AutoID = Convert.ToInt32(ViewState["SUpdate_AutoID"]);
                    int res = objNewUpdateNotificationHome.Insert_NewUpdateNotificationHome();
                    if (res > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        txtHeading.Focus();
                        if (Request.Cookies["OnlineGym"]["Authority"].ToString() == "User")
                        {
                            btnSave.Text = "Edit";
                            Clear();
                        }
                        else
                        {
                            btnSave.Text = "Save";
                            Clear();
                            BindGrid();
                        }
                        btnClear.Visible = true;
                        txtHeading.Focus();
                        objNewUpdateNotificationHome.Action = "Search";
                        BindGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Update Failed !!!','Error');", true);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void ImageUplode()
        {
            if ((FileUploadeImg.PostedFile != null) && (FileUploadeImg.PostedFile.ContentLength > 0))
            {
                Guid uid = Guid.NewGuid();
                string fn = System.IO.Path.GetFileName(FileUploadeImg.PostedFile.FileName);
                DateTime dt = DateTime.Now;
                newfileName = txtHeading.Text.Trim() + "_" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                string fileName = Path.GetFileName(FileUploadeImg.PostedFile.FileName);
                string primaryFileName = Path.GetFileNameWithoutExtension(fileName);
                string fileExtentionName = Path.GetExtension(fileName);
                string SaveLocation = Server.MapPath("/Logo/") + newfileName + fileExtentionName;
                try
                {
                    string fileExtention = FileUploadeImg.PostedFile.ContentType;
                    int fileLenght = FileUploadeImg.PostedFile.ContentLength;
                    if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                    {
                        System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUploadeImg.PostedFile.InputStream);
                        System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);
                        objImage.Save(SaveLocation, ImageFormat.Jpeg);
                        ViewState["serverfilrpath1"] = "/Logo/" + newfileName + fileExtentionName;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Invaild Format !!!','Error');", true);
                        txtHeading.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                    txtHeading.Focus();
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

        public void GetDataOnEdit(int SUpdate_AutoID)
        {
            try
            {
                //bindDDLStaffName();
                objNewUpdateNotificationHome.SUpdate_AutoID = Convert.ToInt32(SUpdate_AutoID);
                objNewUpdateNotificationHome.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"]);
                objNewUpdateNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                //objNewUpdateNotificationHome.authority = Request.Cookies["OnlineGym"]["Authority"];

                objNewUpdateNotificationHome.Action = "SELECT_BY_ID";

                dt = objNewUpdateNotificationHome.SelectByID_NewUpdateNotificationHome();
                if (dt.Rows.Count > 0)
                {
                    txtHeading.Text = dt.Rows[0]["Heading"].ToString();
                    txtNotification.Text = dt.Rows[0]["Information"].ToString();
                    imgUpdate.ImageUrl = dt.Rows[0]["Image"].ToString();
                    ViewState["ImageUrl"] = dt.Rows[0]["Image"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                int SUpdate_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["SUpdate_AutoID"] = SUpdate_AutoID;
                GetDataOnEdit(SUpdate_AutoID);
                txtHeading.Focus();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void Clear()
        {
            txtHeading.Text = "";
            txtNotification.Text = "";
            imgUpdate.ImageUrl = "";
            btnSave.Text = "Save";
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                DateTime Todaydate;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Todaydate))
                { }
                objNewUpdateNotificationHome.SUpdate_AutoID = Convert.ToInt32(e.CommandArgument);
                objNewUpdateNotificationHome.Date = Todaydate;
                objNewUpdateNotificationHome.Action = "Delete";
                int i = objNewUpdateNotificationHome.Insert_NewUpdateNotificationHome();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    objNewUpdateNotificationHome.Action = "Search";
                    flag = 2;
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Delete Failed !!!.','Error');", true);
                return;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (ddlSearch.SelectedValue != "--Select--")
            {
                if (txtSearch.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Enter Search Category !!!','Success');", true);
                    return;
                }
                else
                {
                    SearchByCategory();
                }
            }
            else if (txtSearch.Text != "")
            {
                if (ddlSearch.SelectedValue == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Search Category !!!','Success');", true);
                    return;
                }
                else
                {
                    SearchByCategory();
                }
            }
            else
            {
                objNewUpdateNotificationHome.Action = "Search";
                flag = 2;
            }
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            gvNotification.DataSource = null;
            gvNotification.DataBind();
            txtHeading.Focus();
        }

        // static int flag;
        protected void gvNotification_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvNotification.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            else if (flag == 2)
            {
                gvNotification.PageIndex = e.NewPageIndex;
                BindGrid();
            }
        }

        public void BindGrid()
        {
            try
            {
                objNewUpdateNotificationHome.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"]);
                objNewUpdateNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objNewUpdateNotificationHome.Select_DataAsPerSearchCriteriaBranch();
                if (dt.Rows.Count > 0)
                {
                    gvNotification.DataSource = dt;
                    gvNotification.DataBind();

                }
                else
                {
                    gvNotification.DataSource = null;
                    gvNotification.DataBind();
                    gvNotification.Style["width"] = "100%";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void SearchByCategory()
        {
            objNewUpdateNotificationHome.Action = "Search_By_Category";
            if (ddlSearch.SelectedValue == "Heading")
            {
                objNewUpdateNotificationHome.Category = "Heading";
                objNewUpdateNotificationHome.Heading = txtSearch.Text;
            }

            flag = 1;
        }
    }
}