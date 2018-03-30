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

namespace NDOnlineGym_2017
{
    public partial class Termination : System.Web.UI.Page
    {
        BalTermination objBalTerm = new BalTermination();
        DataTable dataTable = new DataTable();
        DataTable dt = new DataTable();
        DataSet ds1 = new DataSet();
        int Flag;
        static int flag;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Query String From Membership Transfer
                //if (Request.QueryString["TransferMember_ID"] != null)
                
                if (Request.QueryString["Member_ID"] != null)
                {
                    objBalTerm.Member_Id = Convert.ToInt32(Request.QueryString["Member_ID"]);                    
                    objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
                    objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    dataTable = objBalTerm.GetMember_AutoID();

                    if (dataTable.Rows.Count > 0)
                    {
                        int Member_AutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        ViewState["Member_AutoID"] = Member_AutoID;
                        EditFunction();
                    }
                }
                else
                {
                    txtMemberID.Focus();
                    ClearRecord();
                    GetAllMembers();
                    AssignTodaysDate();
                    //BindGVBlockUnblockStatus();
                }
                
            }
        }
        protected void AssignTodaysDate()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                txtDate.Text = todaydate.ToString("dd-MM-yyyy");              
            }
        }
        protected void GetAllMembers()
        {
            try
            {
               // gvTerminationDetails.PageIndex = 0;
                ViewState["ViewBlockUnblock"] = null;

                objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
                objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = objBalTerm.BindAllMembers();

                ViewState["ViewBlockUnblock"] = dataTable;
                dt = dataTable;
                GridViewBind(dataTable);

                int count = dt.Rows.Count;
                int UnBlock = CountUnBlock(dataTable);

                lblTotal.Text = count.ToString();//gvTermination.Rows.Count.ToString(); //total
                lblUnblock.Text = UnBlock.ToString(); //Unblock
                lblBlock.Text = (Convert.ToInt32(lblTotal.Text) - UnBlock).ToString(); //Block
                flag = 1;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void GridViewBind(DataTable dataTable1)
        {
            try
            {
                
                if (dataTable1.Rows.Count > 0)
                {
                    gvTerminationDetails.DataSource = dataTable1;
                    gvTerminationDetails.DataBind();
                    gvTerminationDetails.Style["width"] = "100%";
                }
                else
                {
                    gvTerminationDetails.DataSource = dataTable1;
                    gvTerminationDetails.DataBind();
                    gvTerminationDetails.Style["width"] = "100%";
                }
                if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                {
                    gvTerminationDetails.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                {
                    gvTerminationDetails.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                {
                    gvTerminationDetails.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                {
                    gvTerminationDetails.Columns[0].Visible = true;
                }
                else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                {
                    gvTerminationDetails.Columns[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        private int CountUnBlock(DataTable dataTable)
        {
            var count = dataTable.Rows.Count;
            int UnBlock = 0;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string status = dataTable.Rows[i]["BlockStatus"].ToString();
                    if (status == "UnBlock")
                    {
                        UnBlock = UnBlock + 1;
                    }
                }
            }
            return UnBlock;
        }

        protected void ClearRecord()
        {
            txtMemberID.Text = "";
            txtMemberName.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
        protected void ClearRecordForMemberID()
        {           
            txtMemberName.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }

        public void memberId_TxtChanged()
        {
            try
            {
                ViewState["ViewBlockUnblock"] = null;
                ClearRecordForMemberID();
                objBalTerm.Member_Id = Convert.ToInt32(txtMemberID.Text);
                objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
                objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = objBalTerm.Bind_TerminationByID();

                ViewState["ViewBlockUnblock"] = dataTable;
                dt = dataTable;
                GridViewBind(dataTable);

                int count = dt.Rows.Count;
                int UnBlock = CountUnBlock(dataTable);

                lblTotal.Text = count.ToString();//gvTermination.Rows.Count.ToString(); //total
                lblUnblock.Text = UnBlock.ToString(); //Unblock
                lblBlock.Text = (Convert.ToInt32(lblTotal.Text) - UnBlock).ToString(); //Block

                if (count > 0)
                {
                    txtMemberID.Focus();
                }
                else
                {
                    txtMemberID.Focus();
                    txtMemberID.Text = "";
                }
                flag = 2;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            memberId_TxtChanged();
        }

        protected void ClearRecordForMemberName()
        {
            txtMemberID.Text = "";
            ddlGender.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }

        public void memberName_txtChanged()
        {
            try
            {
                ViewState["ViewBlockUnblock"] = null;

                ClearRecordForMemberName();
                objBalTerm.Name = txtMemberName.Text;
                objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
                objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dataTable = objBalTerm.Bind_TerminationByName();

                ViewState["ViewBlockUnblock"] = dataTable;
                dt = dataTable;
                GridViewBind(dataTable);

                int count = dt.Rows.Count;
                int UnBlock = CountUnBlock(dataTable);

                lblTotal.Text = count.ToString();//gvTermination.Rows.Count.ToString(); //total
                lblUnblock.Text = UnBlock.ToString(); //Unblock
                lblBlock.Text = (Convert.ToInt32(lblTotal.Text) - UnBlock).ToString(); //Block

                if (count > 0)
                {
                    txtMemberName.Focus();
                }
                else
                {
                    txtMemberName.Focus();
                    txtMemberName.Text = "";
                }

                flag = 3;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        protected void txtMemberName_TextChanged(object sender, EventArgs e)
        {
            memberName_txtChanged();
        }

        protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvTerminationDetails.PageIndex = 0;
            getRecordByGender();
            ddlGender.Focus();
        }

        protected void ClearAllRecordForDll()
        {
            txtMemberID.Text = "";           
            
            txtMemberName.Text = "";
        }

        protected void getRecordByGender()
        {
            try
            {

                ViewState["ViewBlockUnblock"] = null;
                ClearAllRecordForDll();
                objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
                objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalTerm.Gender = ddlGender.SelectedItem.Text;
                objBalTerm.Action = "SearchByGender";

                if (ddlStatus.SelectedValue != "--Select--")
                {
                    if (ddlStatus.SelectedItem.Text != "All")
                    {
                        objBalTerm.Action = "SearchByGenAndSts";
                        objBalTerm.Status = ddlStatus.SelectedItem.Text;
                    }
                }

                //dataTable=objBalTerm.Bind_TerminationByGender();
                dataTable = objBalTerm.Bind_TerminationByDDL();

                ViewState["ViewBlockUnblock"] = dataTable;
                dt = dataTable;
                GridViewBind(dataTable);

                int count = dt.Rows.Count;
                int UnBlock = CountUnBlock(dataTable);

                lblTotal.Text = count.ToString();//gvTermination.Rows.Count.ToString(); //total
                lblUnblock.Text = UnBlock.ToString(); //Unblock
                lblBlock.Text = (Convert.ToInt32(lblTotal.Text) - UnBlock).ToString(); //Block
                flag = 4;

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }


        public void ddlStatusChanged()
        {
            try
            {
                //gvTerminationDetails.PageIndex = 0;
                ViewState["ViewBlockUnblock"] = null;
                ClearAllRecordForDll();
                objBalTerm.Action = "SearchByStatus";

                if (ddlGender.SelectedValue != "--Select--")
                {
                    objBalTerm.Action = "SearchByGenAndSts";
                    objBalTerm.Gender = ddlGender.SelectedItem.Text;
                }
                objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Request.Cookies["OnlineGym"]["Branch_ID"]);
                objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                var Status = ddlStatus.SelectedItem.Text;
                if (Status == "Block" || Status == "UnBlock")
                {
                    objBalTerm.Status = Status;
                    dataTable = objBalTerm.Bind_TerminationByDDL();
                }

                else if (Status == "All")
                {
                    if (ddlGender.SelectedItem.Value != "--Select--")
                    {
                        getRecordByGender();
                    }
                    else
                    {
                        GetAllMembers();
                    }
                }
                flag = 5;

                ViewState["ViewBlockUnblock"] = dataTable;
                dt = dataTable;
                GridViewBind(dataTable);

                int count = dt.Rows.Count;
                int UnBlock = CountUnBlock(dataTable);

                lblTotal.Text = count.ToString();//gvTermination.Rows.Count.ToString(); //total
                lblUnblock.Text = UnBlock.ToString(); //Unblock
                lblBlock.Text = (Convert.ToInt32(lblTotal.Text) - UnBlock).ToString(); //Block
                ddlStatus.Focus();

               
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStatusChanged();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtMemberID.Text = "";
            txtMemberName.Text = "";
            ddlGender.SelectedValue = "--Select--";
            ddlStatus.SelectedValue = "--Select--";
            GetAllMembers();
            gvTerminationDetails.PageIndex = 0;

        }       

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            ViewState["Member_AutoID"] = Convert.ToInt32(e.CommandArgument.ToString());                        
            EditFunction();
        }

       
        public void EditFunction()
        {
            AssignTodaysDate();
            div1.Visible = true;
            div2.Visible = true;

            //objBalTerm.Member_Id = Convert.ToInt32(e.CommandArgument.ToString());            

            objBalTerm.Member_Id = Convert.ToInt32(ViewState["Member_AutoID"]);
            objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            
            dt = objBalTerm.SelectByID_MemberInformation();

            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["FName"].ToString();
            }
            gvTerminationDetails.Visible = false;
            DivTermination.Visible = false;
            BindGVBlockUnblockStatus();
        }

        protected void btnSave_Command(object sender, CommandEventArgs e)
        {
            objBalTerm.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]);
            objBalTerm.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            DateTime Date;
            if (DateTime.TryParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out Date))
            {
                string Date1 = Date.ToString("dd-MM-yyyy");
                objBalTerm.Date = DateTime.ParseExact(Date1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            objBalTerm.Member_Id = Convert.ToInt32(ViewState["Member_AutoID"]);
           // objBalTerm.Date = Convert.ToDateTime(txtDate.Text);
            objBalTerm.Reason = txtReason.Text;           

            dt = objBalTerm.SelectByID_MemberInformation();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BlockStatus"].ToString() == "UnBlock")
                {
                    objBalTerm.Status = "Block";
                }
                else
                {
                    objBalTerm.Status = "UnBlock";
                }
            }

            int res = objBalTerm.InsertBlockReason();
            if(res > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Status Changed !!!','Success');", true);
                flag = 6;
                BindGVBlockUnblockStatus();
                lblName.Text = "";
                txtReason.Text = "";
                //AssignTodaysDate();
                //GetAllMembers();

                if (Request.QueryString["FNameTransferPage"] != null)
                {
                    int MemberID1 = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    string url = "MembershipTransfer.aspx?MemberID=" +MemberID1;
                    Response.Redirect(url);  
                }
                else if(Request.QueryString["FNameUpgradePage"] != null)
                {
                    int MemberID1 = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    string url = "Upgrade.aspx?MemberID=" + MemberID1;
                    Response.Redirect(url);  
                }
                else if (Request.QueryString["FNameFreezingPage"] != null)
                {
                    int MemberID1 = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    string url = "MembershipFreezing.aspx?MemberID=" + MemberID1;
                    Response.Redirect(url);  
                }
                else if (Request.QueryString["FNameExtensionPage"] != null)
                {
                    int MemberID1 = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    string url = "MembershipExtension.aspx?MemberID=" + MemberID1;
                    Response.Redirect(url);
                }
                else if (Request.QueryString["Course"] != null)
                {
                    int MemberID1 = Convert.ToInt32(ViewState["Member_AutoID"]); //Convert.ToInt32(Request.QueryString["Member_ID"]);
                    string url = "demoCourse.aspx?Member_ID_Block=" + MemberID1;
                    Response.Redirect(url);
                }
                
            }
            else
            {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('Status Not Changed !!!','Error');", true);
            }

        }

        public void BindGVBlockUnblockStatus()
        {
            try
            {
               
                dt = objBalTerm.BindGVByStatus();
                //objBalTerm.Action = "SelectReasonBy_Id";
                if (dt.Rows.Count > 0)
                {
                    GVBlockUnblockStatus.DataSource = dt;
                    GVBlockUnblockStatus.DataBind();
                    GVBlockUnblockStatus.Style["width"] = "100%";
                }
                else
                {
                    GVBlockUnblockStatus.DataSource = dt;
                    GVBlockUnblockStatus.DataBind();
                    GVBlockUnblockStatus.Style["width"] = "100%";
                }
                flag = 6;
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnClear1_Command(object sender, CommandEventArgs e)
        {
            txtReason.Text = "";
            AssignTodaysDate();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Termination.aspx");
            DivTermination.Visible = false;
            gvTerminationDetails.Visible = false;
        }

        protected void gvTerminationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = 0;
                LinkButton uList = (LinkButton)e.Row.Cells[index].FindControl("btnEdit");
                string test = uList.Text;

                if (test == "UnBlock")
                {
                    e.Row.Cells[index].BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    e.Row.Cells[index].BackColor = System.Drawing.Color.FromName("#ff8080"); ;
                }

            }
        }

        protected void gvTerminationDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 1)
            {
                gvTerminationDetails.PageIndex = e.NewPageIndex;
                GetAllMembers();
            }
           else if(flag==2)
            {
                gvTerminationDetails.PageIndex = e.NewPageIndex;
                memberId_TxtChanged();
            }
            else if (flag == 3)
            {
                gvTerminationDetails.PageIndex = e.NewPageIndex;
                memberName_txtChanged();

            }
            else if (flag==4)
            {
                gvTerminationDetails.PageIndex = e.NewPageIndex;
                getRecordByGender();
            }
            else if (flag == 5)
            {
                gvTerminationDetails.PageIndex = e.NewPageIndex;
                ddlStatusChanged();
            }
           
        }

        protected void GVBlockUnblockStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (flag == 6)
            {
                GVBlockUnblockStatus.PageIndex = e.NewPageIndex;
                BindGVBlockUnblockStatus();
            }
        }


    }
}