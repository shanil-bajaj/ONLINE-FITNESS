using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Drawing.Imaging;
//using System.Globalization;

namespace NDOnlineGym_2017
{
    public partial class SearchPage : System.Web.UI.Page
    {
        BalSearchHome objBalSearchHome = new BalSearchHome();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["FNameSearchPage2"] != null)
                {
                    GetEnqID1_FromAutoID();
                    GridBind();
                }
                else if (Request.QueryString["FNameSearchPage1"] != null)
                {
                    GetMemID1_FromAutoID();
                    GridBind();
                }
                else
                {
                    GridBind();
                }
            }
        }

        public void GetMemID1_FromAutoID()
        { 
             objBalSearchHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
             objBalSearchHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
             objBalSearchHome.SearchData = Request.QueryString["Member_AutoID"].ToString();
             dt = objBalSearchHome.Select_MemID1();
             if (dt.Rows.Count > 0)
             {
                 ViewState["Member_ID1"] = dt.Rows[0]["Member_ID1"];
             }
        }

        public void GetEnqID1_FromAutoID()
        {
            objBalSearchHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalSearchHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalSearchHome.SearchData = Request.QueryString["Enq_AutoID"].ToString();
            dt = objBalSearchHome.Select_EnqID1();
            if (dt.Rows.Count > 0)
            {
                ViewState["Enq_ID1"] = dt.Rows[0]["Enq_ID1"];
            }
        }

        #region BIND GRID
        public void GridBind()
        {
            try
            {
                if (Request.QueryString["FNameSearchPage2"] != null)
                {
                    objBalSearchHome.SearchData = ViewState["Enq_ID1"].ToString();
                    objBalSearchHome.Action = "ID";
                }
                else if (Request.QueryString["FNameSearchPage1"] != null)
                {
                    objBalSearchHome.SearchData = ViewState["Member_ID1"].ToString();
                    objBalSearchHome.Action = "ID";
                }
                else
                {
                    objBalSearchHome.SearchData = Request.QueryString["Search"].ToString();
                    objBalSearchHome.Action = Request.QueryString["SearchBy"].ToString();
                }
               // objBalSearchHome.SearchBy = Request.QueryString["Search"].ToString();
                
                objBalSearchHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objBalSearchHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objBalSearchHome.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
                dt = objBalSearchHome.Select_All();

                if (dt.Rows.Count > 0)
                {
                    divNotFoundSearch.Visible = false;
                    RepterDetails.DataSource = dt;
                    RepterDetails.DataBind();
                }
                else
                {
                    divNotFoundSearch.Visible = true;
                }
            }
            catch (Exception ex)
            {
               
            }

        }
        #endregion

        int i = 0;

        protected void RepterDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Control control;
            string type1 = "";
            int id=0;
            HiddenField hf = e.Item.FindControl("HiddenField1") as HiddenField;
            if (hf != null)
            {
                string val = hf.Value;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("imgMember") as System.Web.UI.WebControls.Image;
                img.ImageUrl = val;
            }


            Control control;
            control = e.Item.FindControl("lbltype");

            Label lbltype = e.Item.FindControl("lbltype") as Label;
            if (lbltype != null)
            {
                i = Convert.ToInt32(e.Item.ItemIndex + "");
                type1 = dt.Rows[i]["Type"].ToString();
            }


            if (type1.ToString() == "Enquiry")
            {
                control = e.Item.FindControl("lnkBtnEditEnquiry");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAssignPackage");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lbtnMemberProfile");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lbtnEnquiry");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnEditMember");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnAddMember");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnEnquiryFollowup");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnMemberFollowup");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnBalance");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnUpgrade");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnTransfer");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnFreezing");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnAppointment");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnAttendance");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnPT");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnPOS");
                if (control != null)
                    control.Visible = false;

            }
            else if (type1.ToString() == "Member")
            {
                control = e.Item.FindControl("lbtnMemberProfile");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAssignPackage");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lbtnEnquiry");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnEditEnquiry");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnEditMember");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAddMember");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnEnquiryFollowup");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnMemberFollowup");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnBalance");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnUpgrade");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnTransfer");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnFreezing");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnAppointment");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAttendance");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnPT");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnPOS");
                if (control != null)
                    control.Visible = true;
            }

            else if (type1.ToString() == "Course")
            {
                control = e.Item.FindControl("lbtnMemberProfile");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAssignPackage");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lbtnEnquiry");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnEditEnquiry");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnEditMember");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAddMember");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnEnquiryFollowup");
                if (control != null)
                    control.Visible = false;

                control = e.Item.FindControl("lnkBtnMemberFollowup");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnBalance");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnUpgrade");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnTransfer");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnFreezing");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAppointment");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnAttendance");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnPT");
                if (control != null)
                    control.Visible = true;

                control = e.Item.FindControl("lnkBtnPOS");
                if (control != null)
                    control.Visible = true;
            }

        }

        string FNameSearchPage = "SearchPage";
        protected void lnkBtnEditEnquiry_Command(object sender, CommandEventArgs e)
        {
            string FNameSearchPage = "SearchPage";
            int enqid = Convert.ToInt32(e.CommandArgument);
            //Response.Redirect("~/AddEnquiry.aspx?Enq_AutoID=" + enqid + "&FNameSearchPage =" + "a");
            Response.Redirect("~/AddEnquiry.aspx?Enq_AutoID=" + enqid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnEditMember_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/AddMember.aspx?MemberID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnAddMember_Command(object sender, CommandEventArgs e)
        {
            int enqid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/AddMember.aspx?Enq_ID=" + enqid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnEnquiryFollowup_Command(object sender, CommandEventArgs e)
        {
            int enqid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/EnquiryFollowup.aspx?Enq_ID=" + enqid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnMemberFollowup_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/Followup.aspx?Member_ID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnBalance_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/BalancePayment.aspx?Member_ID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnUpgrade_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/Upgrade.aspx?MemberID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnTransfer_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MembershipTransfer.aspx?MemberID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnFreezing_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MembershipFreezing.aspx?Member_ID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnAppointment_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/TakeAppointment.aspx?Member_ID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnAttendance_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MemberNumericalAttendance.aspx?Member_ID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnPT_Command(object sender, CommandEventArgs e)
        {

        }

        protected void lnkBtnPOS_Command(object sender, CommandEventArgs e)
        {

        }

        protected void lbtnMemberProfile_Command(object sender, CommandEventArgs e)
        {
            int MemberId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/MemberProfile.aspx?MemberId=" + MemberId + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

        protected void lnkBtnAssignPackage_Command(object sender, CommandEventArgs e)
        {
            int memberid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/demoCourse.aspx?Member_ID=" + memberid + " &FNameSearchPage=" + HttpUtility.UrlEncode(FNameSearchPage.ToString()));
        }

       
    }
}