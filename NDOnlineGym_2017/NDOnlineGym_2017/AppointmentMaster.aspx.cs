using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;
using System.Globalization;
using BusinessAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class AppointmentMaster : System.Web.UI.Page
    {
        BalMast_Appoint appoint = new BalMast_Appoint();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAppointment_ID();

        }

        public void BindGrid()
        {
            try
            {
                appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = appoint.BindGrid();

                if (dt.Rows.Count > 0)
                {

                    gvPackage.Visible = true;
                    gvPackage.DataSource = dt;
                    gvPackage.DataBind();
                }
                else
                {
                    gvPackage.Visible = false;
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvPackage.Columns[0].Visible = true;
                    gvPackage.Columns[1].Visible = false;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvPackage.Columns[0].Visible = false;
                    gvPackage.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Clear()
        {
            try
            {
                txtAppointmentType.Text = "";
                txtSession.Text = "";
                txtStatus.Text = "";
                txtAppointmentType.Text = "";
                txtTimeFrom.Text = "";
                txtAmount.Text = "";
                GetAppointment_ID();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region GetAppointment_ID
        public void GetAppointment_ID()
        {
            try
            {
                dt = appoint.Get_AppointID();
                txtAppID.Text = dt.Rows[0]["Appoint_ID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void btnClear_Click(object sender, EventArgs e)
        {
          Clear();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
               
                    appoint.Appoint_ID = Convert.ToInt32(txtAppID.Text);
                 
                    appoint.Color = txtColor.Text;
                    if (txtAmount.Text != "")
                    {
                        appoint.Ammount = Convert.ToInt32(txtAmount.Text);
                    }
                    appoint.Status = txtStatus.Text;
                    appoint.Session = Convert.ToInt32(txtSession.Text);
                    appoint.Appoint_Type = txtAppointmentType.Text;
                    if (txtTimeFrom.Text != null)
                    {
                        appoint.Time = Convert.ToDateTime(txtTimeFrom.Text);
                    }

                    if (btnSave.Text == "Save")
                    {
                    appoint.Action = "Insert";
                    int res = appoint.Insert_Appoint();
                    if (res > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully !!!','Success');", true);
                        Clear();

                    }
                }
                else
                {
                    appoint.Appoint_AutoID = Convert.ToInt32(ViewState["Member_AutoID"].ToString());
                    appoint.Action = "Update";
                    int result = appoint.Insert_Appoint();
                    if (result > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                        Clear();
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {

                appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);//Convert.ToInt32(Session["Branch_ID"]);
                appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                if (btnSave.Text == "Save")
                {

                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    if (id > 0)
                    {
                        appoint.Action = "Delete";
                        appoint.Appoint_AutoID = id;
                        int res = appoint.Delete_Appoint();
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Deleted Successfully!!!','Success');", true);
                        BindGrid();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Not Deleted!!!.','Error');", true);
                    }
                }
               
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Something Error Occured While Deleting !!!.','Error');", true);
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
             try
            {
                ViewState["Member_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());
                appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                btnSave.Text = "Update";
                appoint.Action = "Select_By_ID";
                appoint.Appoint_AutoID = Convert.ToInt32(e.CommandArgument.ToString());
                dt = appoint.Select_AppointID();
                if (dt.Rows.Count > 0)
                {
                    txtAppID.Text = dt.Rows[0]["AppoinmentMaster_ID"].ToString();
                    txtAppointmentType.Text = dt.Rows[0]["Appoint_Type"].ToString();
                    txtSession.Text = dt.Rows[0]["Session"].ToString();
                    txtAmount.Text = dt.Rows[0]["Ammount"].ToString();
                    txtStatus.Text = dt.Rows[0]["Status"].ToString();
                    txtTimeFrom.Text = dt.Rows[0]["Time"].ToString();
                    txtColor.Text = dt.Rows[0]["color"].ToString();
                }
             }
                 catch(Exception ex)
             {
                     throw ex;
             }
            
        }
    }
}