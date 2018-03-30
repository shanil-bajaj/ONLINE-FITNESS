using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;
using System.Globalization;
using BusinessAccessLayer;

namespace NDOnlineGym_2017
{
    public partial class TakeAppointment : System.Web.UI.Page
    {
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalTakeAppointment appoint = new BalTakeAppointment();
        DataTable dt = new DataTable();
        BalAddMember objMemberDetails = new BalAddMember();

        protected void Page_Load(object sender, EventArgs e)
        {
                BindMaster();
            
            if (!IsPostBack)
            {
            bindStaff();
            bindDDLExecutive();
            setExecutive();
            if (Request.QueryString["Member_ID"] != null)
            {
                int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                GetMemberDetails(memberid);
            }
            }
        }

        public void GetMemberDetails(int memberid)
        {
            appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            appoint.Member_AutoID = Convert.ToInt32(memberid);
            appoint.Action = "Select By ID";
            dt = appoint.Select_MemberID();
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                //ddlExecutive.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();
            }
        }

        public void bindDDLExecutive()
        {
            try
            {
                obBalStaffRegistration.authority = Request.Cookies["OnlineGym"]["Authority"];
                obBalStaffRegistration.Action = "BindDDL";
                obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
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
                //ErrorHandiling.SendErrorToText(ex);
            }
        }

        public void setExecutive()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
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

       
        #region Bind Master
        public void BindMaster()
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Bind Staff
        public void bindStaff()
        {
            try
            {
                appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = appoint.BindStaff();
                if (dt.Rows.Count > 0)
                {
                    ddlProgrammerName.DataSource = dt;
                    ddlProgrammerName.Items.Clear();
                    ddlProgrammerName.DataValueField = "Staff_AutoID";
                    ddlProgrammerName.DataTextField = "Name";
                    ddlProgrammerName.DataBind();
                    ddlProgrammerName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Clear Records
        public void Clear()
        {
            txtContact.Text="";
            txtID.Text="";
            txtName.Text="";
            txtPreSession.Text="";
            GridView1.Visible=false;
            bindStaff();
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false; 


        }
       #endregion

        #region Bind Member BY ID
        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                appoint.Member_AutoID = Convert.ToInt32(txtID.Text);
                appoint.Action = "Select By ID";
                dt = appoint.Select_MemberID();
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                    ddlExecutive.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error(ex,'Error');", true);

            }
        }
        #endregion

        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public static int K = 0;
        int res;

          #region ADD TO NEXT GRID_VIEW
          protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                GridView1.Visible = true;
                var btnPre = (Control)sender;
                GridViewRow row = (GridViewRow)btnPre.NamingContainer;
                DataRow dr = null;
                //dt.Clear();
                if (ViewState["DT"] == null)
                {
                    dt1.Columns.Add(new DataColumn("ID"));
                    dt1.Columns.Add(new DataColumn("Appoint_Type"));
                    dt1.Columns.Add(new DataColumn("Session"));
                    dt1.Columns.Add(new DataColumn("Ammount"));
                    dt1.Columns.Add(new DataColumn("AppointDate"));
                    dt1.Columns.Add(new DataColumn("Time"));
                    dt1.Columns.Add(new DataColumn("ProgrammerName"));
                }
                if (ViewState["DT"] != null)
                {
                    dt1 = (DataTable)ViewState["DT"];
                }
                dr = dt1.NewRow();
                dr["ID"] = K;
                dr["Appoint_Type"] = row.Cells[2].Text;
                dr["Session"] = row.Cells[3].Text;
                dr["Ammount"] = row.Cells[4].Text;

                //DateTime ApDate;
                //if (DateTime.TryParseExact(DateTime.Now.Date.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ApDate))
                //{
                //    string DOBdate1 = ApDate.ToString("dd-MM-yyyy");
                //    dr["AppointDate"] = DateTime.ParseExact(DOBdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                //}


                dr["AppointDate"] = DateTime.Now.Date.ToString("dd-MM-yyyy");
                dr["Time"] = row.Cells[5].Text;
                dr["ProgrammerName"] = ddlProgrammerName.SelectedItem.Text;
                dt1.Rows.InsertAt(dr, K);
                K++;
                ViewState["DT"] = dt1;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }

            catch (Exception ex)
            {
                
                throw ex;
            }

        }
               #endregion

          #region Cut From GridView
          protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridView1.Visible = true;
                dt1 = (DataTable)ViewState["DT"];
                ViewState["DT"] = dt1;
                dt1.Rows[index].Delete();
                GridView1.DataSource = dt1;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                return;
            }
        }

          #endregion

          #region Contact Wise Search

          protected void txtContact_TextChanged(object sender, EventArgs e)
          {
              try
              {


                  appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                  appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                  appoint.Contact1 = txtContact.Text;
                  appoint.Action = "Select By Contact";
                  dt = appoint.Select_MemberContact();
                  if (dt.Rows.Count > 0)
                  {
                      txtName.Text = dt.Rows[0]["Name"].ToString();
                      txtID.Text = dt.Rows[0]["Member_ID1"].ToString();
                  }
                  else
                  {
                      ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Record Not Found !!!','Error');", true);
                  }
              }
              catch (Exception ex)
              {
                  ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error(ex,'Error');", true);

              }
          }
#endregion


          #region  Save Grid

          protected void btnSave_Click(object sender, EventArgs e)
          {
              if (GridView1.Visible != false)
              {
                  try
                  {
                      GridView1.Visible = true;
                      appoint.Member_AutoID = Convert.ToInt32(txtID.Text);
                      appoint.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);
                      appoint.branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                      appoint.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                      appoint.LoginID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                      appoint.Action = "Insert";

                      foreach (GridViewRow row in GridView1.Rows)
                      {

                          appoint.Pression = Convert.ToInt32(row.Cells[3].Text);

                          appoint.Ammount = Convert.ToInt32(row.Cells[4].Text);

                          appoint.Appoint_Type = row.Cells[2].Text;

                          appoint.Programmer_Name = row.Cells[7].Text;

                          string apd = row.Cells[6].Text;

                          //DateTime Apdate;
                          //if (DateTime.TryParseExact(apd, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Apdate))
                          //{
                          //    string DOBdate1 = Apdate.ToString("dd-MM-yyyy");
                          //    appoint.Time = DateTime.ParseExact(DOBdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                          //}


                          //TextBox txtsdate = (TextBox)currentRow.FindControl("txtsDate");
                          //DateTime sdate;
                          //if (DateTime.TryParseExact(txtsdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out sdate))
                          //{
                          //    string DOBdate1 = sdate.ToString("dd-MM-yyyy");
                          //    appoint.Apdate = DateTime.ParseExact(DOBdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                          //}


                          TextBox txtedate = (TextBox)row.FindControl("txtsDate");
                          appoint.Apdate =Convert.ToDateTime( txtedate.Text);

                         appoint.Time = Convert.ToDateTime(row.Cells[6].Text);

                      //   appoint.Apdate = Convert.ToDateTime(row.Cells[5].Text);
                          res = appoint.Insert();

                      }

                      if (res > 0)
                      {
                          Clear();
                          ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                          if (Request.QueryString["FNameMemDetails"] != null)
                          {
                              int memberid = Convert.ToInt32(Request.QueryString["MemberID"]);
                              Response.Redirect("MemberDetails.aspx?Member_AutoID=" + memberid + " &FNameMemDetails=" + HttpUtility.UrlEncode("FNameMemDetails".ToString()));
                          }
                      }
                  }
                  catch(Exception ex)
                  {
                      throw ex;
                  }
              }
          }

          #endregion

          protected void txtsDate_TextChanged(object sender, EventArgs e)
          {
              GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
              dt1 = (DataTable)ViewState["DT"];
              var btnPre = (Control)sender;
              GridViewRow row = (GridViewRow)btnPre.NamingContainer;
              DataRow dr;
              dr = dt1.NewRow();

              int s = currentRow.RowIndex;

              dr["ID"] = s;
              dr["Appoint_Type"] = row.Cells[2].Text;
              dr["Session"] = row.Cells[3].Text;
              dr["Ammount"] = row.Cells[4].Text;
              dr["Time"] = row.Cells[6].Text;
              dr["ProgrammerName"] = ddlProgrammerName.SelectedItem.Text;
            
              TextBox txtsdate = (TextBox)currentRow.FindControl("txtsDate");
              DateTime sdate;


              if (DateTime.TryParseExact(txtsdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out sdate))
              {
                 // string DOBdate1 = sdate.ToString("dd-MM-yyyy");
                  dr["AppointDate"] = sdate.ToString("dd-MM-yyyy");
                     // DateTime.ParseExact(DOBdate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
              }


          //    dr["AppointDate"] = sdate.ToString();
            

              dt1.Rows[s].Delete();
              dt1.Rows.InsertAt(dr, s);
              ViewState["DT"] = dt1;
              GridView1.DataSource = dt1;
              GridView1.DataBind();




          }
          protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
          {
              if (chkExecutive.Checked == true)
                  ddlExecutive.Enabled = false;
              else
                  ddlExecutive.Enabled = true;
          }
    }

    }
