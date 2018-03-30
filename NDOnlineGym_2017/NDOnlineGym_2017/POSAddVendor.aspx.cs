using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using DataAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class AddVendor : System.Web.UI.Page
    {
        BalAddVendor objVandor = new BalAddVendor();
        DataTable dataTable = new DataTable();
        int result = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AssignVendorID();

                    if (ddlcategory.SelectedValue.ToString() == "--Select--")
                    {                     
                        txtSearch.Enabled = false;
                    }

                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }


        #region ----------- Assign Max Vendor ID -------------
        private void AssignVendorID()
        {
            AssignID();
            objVandor.Action = "GetVendorID1";

            dataTable = objVandor.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                txtVendorID.Text = dataTable.Rows[0]["Vendor_ID"].ToString();
            }
        }
        #endregion

        #region ----------- Save Button -----------------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    objVandor.Action = "INSERT";
                    objVandor.Vendor_ID = Convert.ToInt32(txtVendorID.Text);
                    objVandor.Name = txtName.Text;
                    objVandor.Contact1 = txtContact1.Text;
                    objVandor.Contact2 = txtContact2.Text;
                    objVandor.Address = txtAddress.Text;
                    objVandor.State = txtState.Text;
                    objVandor.Pincode = txtPincode.Text;
                    objVandor.GSTNo = txtGSTNo.Text;
                    AssignID();

                    result = objVandor.Insert_VendorInformation();
                    if (result > 0)
                    {
                        ClearField();
                       // ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                    }
                    else if (result < 0)
                    {
                        txtVendorID.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Vendor Id Is Already Exists !!!','Error');", true);
                        return;
                    }
                }
                else if (btnSave.Text == "Update")
                {

                    objVandor.Action = "Update";
                    AddParameters();
                    AssignID();
                    objVandor.Vendor_AutoID = Convert.ToInt32(ViewState["Vendor_AutoID"]);

                    int res = objVandor.Insert_VendorInformation();
                    if (res > 0)
                    {                                         
                       // btnSave.Text = "Save"; 
                        ClearField();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion

        #region ------------- Add Parameter ---------------
        private void AddParameters()
        {
            objVandor.Vendor_ID = Convert.ToInt32(txtVendorID.Text);
            objVandor.Name = Regex.Replace(txtName.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objVandor.Contact1 = Regex.Replace(txtContact1.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objVandor.Contact2 = Regex.Replace(txtContact2.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objVandor.Address = Regex.Replace(txtAddress.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objVandor.State = Regex.Replace(txtState.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objVandor.Pincode = Regex.Replace(txtPincode.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objVandor.GSTNo = Regex.Replace(txtGSTNo.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
        }
        #endregion

        #region ---------- Assign Branch ID and Company ID and Login ID --------------
        private void AssignID()
        {
            objVandor.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objVandor.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objVandor.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }
        #endregion

        #region ----------- Clear Field Function ----------
        private void ClearField()
        {
            AssignVendorID();
            txtName.Text = string.Empty;
            txtContact1.Text = string.Empty;
            txtContact2.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPincode.Text = string.Empty;
            txtGSTNo.Text = string.Empty;
            btnSave.Text = "Save";
          
        }
        #endregion 

        #region ------------ Clear Button --------------
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearField();
        }
        #endregion

        #region ---------- Search Button ------------
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridViewDetails();
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ------------ Bind Gridview ---------
        private void BindGridViewDetails()
        {
            SeacrhAction();
            AssignID();

            //dataTable.Clear();
            dataTable = objVandor.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                gvVender.DataSource = dataTable;
                gvVender.DataBind();
            }
            else
            {
                gvVender.DataSource = dataTable;
                gvVender.DataBind();
            }
        }
        #endregion

        #region ----------- Search Action -------------
        private void SeacrhAction()
        {
            try
            {
                objVandor.Action = "BindGridViewDetails";

                if (ddlcategory.SelectedValue.ToString() == "--Select--")
                {
                    objVandor.Category = "SearchAll";
                    txtSearch.Enabled = false;
                }
                else if (ddlcategory.SelectedValue.ToString() == "Vendor_ID")
                {
                    objVandor.Category = "SearchByVendorID";
                    objVandor.SearchByText = txtSearch.Text;

                }
                else if (ddlcategory.SelectedValue.ToString() == "Name")
                {
                    objVandor.Category = "SearchByMemberName";
                    objVandor.SearchByText = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "GSTNo")
                {
                    objVandor.Category = "SearchByGSTNo";
                    objVandor.SearchByText = txtSearch.Text;
                }                
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    ddlcategory.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }

        }
        #endregion

        #region ----------- Category DDL ----------- 
        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcategory.SelectedValue.ToString() == "--Select--")
            {
                txtSearch.Enabled = false;
            }
            else
            {
                txtSearch.Enabled = true;
                //txtSearch.Focus();
            }
            txtSearch.Text = string.Empty;
            ddlcategory.Focus();

        }
        #endregion

        #region ------------ Delete Button ---------------
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();
                objVandor.Vendor_AutoID = Convert.ToInt32(e.CommandArgument);
                objVandor.Action = "DeleteByVendor_AutoID";
                result = objVandor.Insert_VendorInformation();
                if (result > 0)
                {
                    AssignVendorID();
                    BindGridViewDetails();
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
        #endregion

        #region -------------- Edit Button ----------
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                ViewState["Vendor_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                btnSave.Text = "Update";
                txtVendorID.Enabled = false;
                objVandor.Action = "SELECT_BY_VandorID_EDIT";
                objVandor.Vendor_AutoID = Convert.ToInt32(ViewState["Vendor_AutoID"]);

                dataTable = objVandor.GetDetails();
                if (dataTable.Rows.Count > 0)
                {
                    txtVendorID.Text = dataTable.Rows[0]["Vendor_ID"].ToString();
                    txtName.Text = dataTable.Rows[0]["Name"].ToString();
                    txtContact1.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtContact2.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtAddress.Text = dataTable.Rows[0]["Address"].ToString();
                    txtState.Text = dataTable.Rows[0]["State"].ToString();                    
                    txtPincode.Text = dataTable.Rows[0]["Pincode"].ToString();
                    txtGSTNo.Text = dataTable.Rows[0]["GSTNo"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region ------------ Vendor ID Text Change Event -----------
        bool chkExistingVendorId = false;
        protected void txtVendorID_TextChanged(object sender, EventArgs e)
        {
            CheckVendorIdExist();
        }
        #endregion

        #region ----------- Check Vendor ID Is Exist Or Not ----------
        protected void CheckVendorIdExist()
        {
            try
            {
                if (txtVendorID.Text != string.Empty)
                {
                    AssignID();
                    objVandor.Action = "CheckVendorIDExist";
                    objVandor.Vendor_ID = Convert.ToInt32(txtVendorID.Text);
                    chkExistingVendorId = objVandor.Check_ExistingVendorId();

                    if (chkExistingVendorId == true)
                    {
                        txtVendorID.Focus();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Vendor Id Is Already Exists !!!','Error');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region ------------- Vendor Grid View Page Indexing ---------
        protected void gvVender_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVender.PageIndex = e.NewPageIndex;
            BindGridViewDetails();
        }
        #endregion


    }
}