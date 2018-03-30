using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Globalization;
using System.IO;
using System.Drawing;

namespace NDOnlineGym_2017
{
    public partial class CourtBookingForm : System.Web.UI.Page
    {
        BalCourtBooking Objcourt = new BalCourtBooking();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLCourtTypeMaster();
            }
        }

        #region-------------AssignID---------------
        private void AssignID()
        {
            Objcourt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            Objcourt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }
        #endregion

        #region -----------Bind Court Master Type--------------
        public void DDLCourtTypeMaster()
        {
            try
            {
                AssignID();
                dt = Objcourt.Get_Edit();
                if (dt.Rows.Count > 0)
                {
                    ddlCourtType.DataSource = dt;
                    ddlCourtType.Items.Clear();
                    ddlCourtType.DataValueField = "CourtMaster_AutoID";
                    ddlCourtType.DataTextField = "Name";
                    ddlCourtType.DataBind();
                    ddlCourtType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
                ErrorHandiling.SendErrorToText(ex);
            }
        }
        #endregion

        public void Save()
        {

        }

        protected void txtId1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSingle.Checked == true)
                {
                    if (txtId1.Text != string.Empty)
                    {
                        AssignID();
                       
                        dt = Objcourt.GetMemberAutoID();
                        if(dt.Rows.Count>0)
                        {
                           lblID.Text = dt.Rows[0]["Member_AutoID"].ToString();
                        }
                        Objcourt.Action = "SearchByMemberID";
                        dt = Objcourt.GetByMemberID();
                        if (dt.Rows.Count > 0)
                        {
                        //    lblID.Text = dt.Rows[0]["Member_AutoID"].ToString();
                        //    txtFirstName1.Text = dt.Rows[0]["FName"].ToString();
                        //    txtLastName1.Text = dt.Rows[0]["LName"].ToString();
                        //    ddlGender1.SelectedValue = dt.Rows[0]["Gender"].ToString();
                        //    txtContact1.Text = dt.Rows[0]["Contact1"].ToString();
                        //    txtEmail1.Text = dt.Rows[0]["Email"].ToString();
                        }
                        else
                        {
                            //ClearField();
                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
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

       
    }
}