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
    public partial class POSAddProduct : System.Web.UI.Page
    {
        POSBalProduct objProduct = new POSBalProduct();
        System.Data.SqlClient.SqlTransaction trans;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                txtProductCode.Focus();
            }
        }

        public void clear()
        {
            txtProductCode.Text = "";
            txtCGSTNo.Text = "";
            txtGst.Text = "";
            txtIGSTNo.Text = "";
            txtProductName.Text = "";
            txtPurchaseRate.Text="";
            txtQuantity.Text = "";
            txtSearch.Text = "";
            txtSellingRate.Text = "";
            txtSGSTNo.Text = "";
            txtUOM.Text = "";
            txthsnCode.Text = "";
            
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
                    dt = objProduct.Exits();
                    if (dt.Rows.Count > 0)
                    {
                        Exits1 = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                    if (Exits1 > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Product Code AlReady Exits !!!','Error');", true);

                        btnSave.Focus();
                        return;
                    }
                    else
                    {
                        int k = objProduct.Insert_Update_Delete();
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
                    int k = objProduct.Insert_Update_Delete();
                    if (k > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Update Successfully !!!','Success');", true);
                        clear();
                        btnSave.Text = "Save";
                        txtProductCode.Focus();
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
                objProduct.ProductCode = txtProductCode.Text;
                objProduct.ProductName = txtProductName.Text;
                int i = 0;
                if (txtQuantity.Text == "")
                {
                    objProduct.Quantity = i;
                    objProduct.Stock = i;
                }
                else
                {
                    objProduct.Quantity = Convert.ToDouble(txtQuantity.Text.Trim());
                    objProduct.Stock = Convert.ToDouble(txtQuantity.Text.Trim());
                }
              
              
                objProduct.HSNCode = txthsnCode.Text.Trim();
                objProduct.GST = Convert.ToInt32(txtGst.Text.Trim());
                objProduct.CGST = Convert.ToDouble(txtCGSTNo.Text.Trim());
                objProduct.SGST = Convert.ToDouble(txtSGSTNo.Text.Trim());
                objProduct.IGST = Convert.ToInt32(txtIGSTNo.Text.Trim());
                objProduct.UOM = txtUOM.Text.Trim();
                objProduct.PurchaseRate = Convert.ToDouble(txtPurchaseRate.Text.Trim());
                objProduct.SellingRate = Convert.ToDouble(txtSellingRate.Text.Trim());           
                    
                objProduct.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                objProduct.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objProduct.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                if (Convert.ToInt32(ViewState["flag"]) != 1)
                {
                    objProduct.Action = "Insert";
                }
                else
                {
                    objProduct.Action = "Update";
                    objProduct.Auto_ID = Convert.ToInt32(ViewState["Product_id"]);
                    ViewState["flag"] = 0;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        public int Product_id;
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            Product_id = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["Product_id"] = Convert.ToInt32(e.CommandArgument.ToString());
            GetDataForEdit(Product_id);
        }

        int Flag = 0;
        public void GetDataForEdit(int AutoID)
        {
            btnSave.Text = "Update";
            objProduct.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objProduct.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objProduct.Auto_ID = Convert.ToInt32(ViewState["Product_id"]);
            dt.Clear();
            dt = objProduct.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                txtProductCode.Text = dt.Rows[0]["ProductCode"].ToString();
                txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                txtCGSTNo.Text = dt.Rows[0]["CGST"].ToString();
                txtGst.Text = dt.Rows[0]["GST"].ToString();
                txthsnCode.Text = dt.Rows[0]["HSNCode"].ToString();
                txtIGSTNo.Text = dt.Rows[0]["IGST"].ToString();
                txtPurchaseRate.Text = dt.Rows[0]["PurchaseRate"].ToString();
                txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
             
                txtSellingRate.Text = dt.Rows[0]["SellingRate"].ToString();
                txtSGSTNo.Text = dt.Rows[0]["SGST"].ToString();
                txtUOM.Text = dt.Rows[0]["UOM"].ToString();
            }
            ViewState["flag"] = 1;
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                objProduct.Auto_ID = Convert.ToInt32(e.CommandArgument);
                objProduct.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objProduct.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objProduct.Action = "Delete";
                int i = objProduct.Delete();
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
                    objProduct.Category = "All";

                }
                else if (ddlcategory.SelectedValue.ToString() == "ProductCode")
                {
                    objProduct.Category = "ProductCode";
                    objProduct.txtSearch = txtSearch.Text;

                }
                else if (ddlcategory.SelectedValue.ToString() == "ProductName")
                {
                    objProduct.Category = "ProductName";
                    objProduct.txtSearch = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "HSNCode")
                {
                    objProduct.Category = "HSNCode";
                    objProduct.txtSearch = txtSearch.Text;
                }
                else if (ddlcategory.SelectedValue.ToString() == "GST")
                {
                    objProduct.Category = "GST";
                    objProduct.txtSearch = txtSearch.Text;
                }

                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Please Select Category !!!','Error');", true);
                    txtProductCode.Focus();
                    return;
                }
                objProduct.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objProduct.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt.Clear();
                dt = objProduct.Get_Search();
                if (dt.Rows.Count > 0)
                {
                    gvProduct.DataSource = dt;
                    gvProduct.DataBind();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }

        protected void txtGst_TextChanged(object sender, EventArgs e)
        {
            double a, b,c=0;
            a = Convert.ToDouble(txtGst.Text);
            b = a / 2;
            txtCGSTNo.Text = b.ToString();
            txtSGSTNo.Text = b.ToString();
            txtIGSTNo.Text = c.ToString();
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcategory.SelectedValue == "All")
            {
                txtSearch.Text = "";
            }
        }


    }
}