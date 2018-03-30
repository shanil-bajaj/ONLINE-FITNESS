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

namespace NDOnlineGym_2017
{
    public partial class AddCustomer : System.Web.UI.Page
    {
        POSBalCustomer objCustomer=new POSBalCustomer();
        System.Data.SqlClient.SqlTransaction trans;
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Get_CustomerID();
                txtName.Focus();
            }
        }

        private void Get_CustomerID()
        {
            dt.Clear();
            dt = objCustomer.Get_CustomerID();
            if (dt.Rows.Count > 0)
            {
                txtCustomerID.Text = dt.Rows[0]["Customer_ID"].ToString();
            }
        }

        public void clear()
        {
            txtAddress.Text = "";
            txtContact1.Text = "";
            txtContact2.Text = "";
            txtGSTNo.Text = "";
            txtName.Text = "";
            txtPincode.Text = "";
            txtSearch.Text = "";
            txtState.Text = "";
            Get_CustomerID();
        }
        int Exits1;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    Save();
                    dt.Clear();
                    dt = objCustomer.Exits();
                    if (dt.Rows.Count > 0)
                    {
                        Exits1 = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                    if (Exits1 > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Customer ID AlReady Exits !!!','Error');", true);
                        Get_CustomerID();
                        btnSave.Focus();
                        return;
                    }
                    else
                    {
                        int k = objCustomer.Insert_Update_Delete();
                        if (k > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                            clear();
                        }
                    }
                }
                else
                {
                    Save();
                    int k = objCustomer.Insert_Update_Delete();
                    if (k > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Update Successfully !!!','Success');", true);
                        clear();
                        btnSave.Text = "Save";
                        txtName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void Save()
        {
            try
            {
                objCustomer.Customer_ID = Convert.ToInt32(txtCustomerID.Text);
                objCustomer.Name = txtName.Text.Trim();
                objCustomer.Contact1 = txtContact1.Text.Trim();
                objCustomer.Contact2 = txtContact2.Text.Trim();
                objCustomer.Address = txtAddress.Text.Trim();
                objCustomer.State = txtState.Text.Trim();
                int i = 0;
                if (txtPincode.Text == "")
                {
                    objCustomer.Pincode = i;
                }
                else
                {
                    objCustomer.Pincode = Convert.ToInt32(txtPincode.Text.Trim());
                }
                objCustomer.GSTNo = txtGSTNo.Text.Trim();
                objCustomer.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                objCustomer.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objCustomer.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                if (Convert.ToInt32(ViewState["flag"]) != 1)
                {
                    objCustomer.Action = "Insert";
                }
                else
                {
                    objCustomer.Action = "Update";
                    objCustomer.Auto_ID = Convert.ToInt32(ViewState["Customer_id"]);
                    ViewState["flag"] = 0;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bind();
        }
        public void bind()
        {
            try
            {
                if (ddlcategory.SelectedValue.ToString() == "All")
                {
                    objCustomer.Category = "All";
                   
                }
                else if (ddlcategory.SelectedValue.ToString() == "Customer_ID")
                {
                    objCustomer.Category = "Customer ID";
                    objCustomer.searchTxt = txtSearch.Text;

                }
                else if (ddlcategory.SelectedValue.ToString() == "Name")
                {
                    objCustomer.Category = "Name";
                    objCustomer.searchTxt = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "Contact 1")
                {
                    objCustomer.Category = "Contact 1";
                    objCustomer.searchTxt = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "GST No")
                {
                    objCustomer.Category = "GST No";
                    objCustomer.searchTxt = txtSearch.Text;
                }
               
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    txtName.Focus();
                    return;
                }
                objCustomer.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objCustomer.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt.Clear();
                dt = objCustomer.Get_Search();
                if (dt.Rows.Count > 0)
                {
                    gvCustomer.DataSource = dt;
                    gvCustomer.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objCustomer.Auto_ID  = Convert.ToInt32(e.CommandArgument);
                objCustomer.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objCustomer.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objCustomer.Action = "Delete";
                int i = objCustomer.Delete();
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    // BindBranchInfoGrid();
                    //ddlCompany.Focus();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Cannot Delete, Already Assigned !!!.','Error');", true);
                return;
            }
        }
        public int Customer_id;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            Customer_id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["Customer_id"] = Convert.ToInt32(e.CommandArgument.ToString());
            GetDataForEdit(Customer_id);
        }

        int Flag = 0;
        public void GetDataForEdit(int AutoID)
        {
            btnSave.Text = "Update";
            objCustomer.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objCustomer.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objCustomer.Auto_ID = Convert.ToInt32(ViewState["Customer_id"]);
            dt.Clear();
            dt = objCustomer.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                txtCustomerID.Text = dt.Rows[0]["Customer_ID"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtPincode.Text = dt.Rows[0]["Pincode"].ToString();
                txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                txtContact2.Text = dt.Rows[0]["Contact2"].ToString();
                txtState.Text = dt.Rows[0]["State"].ToString();
                txtGSTNo.Text = dt.Rows[0]["GSTNo"].ToString();
               
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
         

            }
            ViewState["flag"] = 1;
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                txtSearch.Text = "";
           
        }

    }
}