using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class ExpenseVoucher : System.Web.UI.Page
    {
        BalExpense ObjExpense = new BalExpense();
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Ex_AutoId"] == null)
                {
                    Response.Redirect("Expense.aspx");
                }
                else
                {
                    ObjExpense.Ex_AutoId = Convert.ToInt32(Request.QueryString["Ex_AutoId"].ToString());
                    BindCompanyInfo();
                }
                //BindCompanyInfo();
            }


        }

        public void BindCompanyInfo()
        {
           // ObjExpense.Branch_Id = Convert.ToInt32(Session["Branch_ID"]);
            obBalBranchInformation.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            //ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
           // DataTable dt2 = ObjExpense.GetCompanyinfByBranchIdId();
            DataTable dt2 = obBalBranchInformation.SelectByID_BranchInformation();
            if (dt2.Rows.Count > 0)
            {
                lblCompanyName.Text = dt2.Rows[0]["BranchName"].ToString();

                lblAdd1.Text = dt2.Rows[0]["Address1"].ToString();
                lblAdd1.Text += ", " + dt2.Rows[0]["Address2"].ToString();
                lblAdd2.Text = dt2.Rows[0]["City"].ToString();
                lblAdd2.Text += ", " + dt2.Rows[0]["State"].ToString();
                lblAdd2.Text += "<br/> ";
                lblMobile.Text = dt2.Rows[0]["Contact1"].ToString();

            }

            ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            ObjExpense.company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
           // ObjExpense.Branch_Id = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
            ObjExpense.Ex_AutoId = Convert.ToInt32(Request.QueryString["Ex_AutoId"].ToString());
            dt = ObjExpense.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["Name"].ToString();
                lblExpNo.Text = dt.Rows[0]["Exp_ID1"].ToString();
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["Exp_Date"]).ToString("dd-MM-yyyy");
                lblNote1.Text = dt.Rows[0]["Note1"].ToString();
                lblNote2.Text = dt.Rows[0]["Note2"].ToString();
                lblAmount.Text = dt.Rows[0]["Amount"].ToString() + ".00";
                lblTotalAmount.Text = dt.Rows[0]["TotalAmount"].ToString() + ".00";
                lblTaxAmount.Text = dt.Rows[0]["TaxableAmount"].ToString() + ".00";
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Expense.aspx");
        }
    }
}