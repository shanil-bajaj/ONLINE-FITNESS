using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using BusinessAccessLayer;
using System.Data;
using System.Net;
using System.IO;

namespace NDOnlineGym_2017
{
    public partial class SendSMSNew : System.Web.UI.Page
    {
        BalSendSMS objSendSMS = new BalSendSMS();
        DataTable dataTable = new DataTable();

        int res1 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ddlName.Focus();
                    BindDDL_MemberName();
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        protected void btnIndividual_Click(object sender, EventArgs e)
        {
            ddlName.Focus();
            MultiView1.ActiveViewIndex = 0;
            btnIndividual.CssClass = "btn-information-section btn-information-section-selected";
            btnGroup.CssClass = "btn-information-section ";
            btnEnquiry.CssClass = "btn-information-section";
            btnBalance1.CssClass = "btn-information-section";
            btnGender1.CssClass = "btn-information-section";
        }

        protected void btnGroup_Click(object sender, EventArgs e)
        {
            ddlGroupStatus.Focus();
            MultiView1.ActiveViewIndex = 1;
            btnIndividual.CssClass = "btn-information-section ";
            btnGroup.CssClass = "btn-information-section btn-information-section-selected ";
            btnEnquiry.CssClass = "btn-information-section";
            btnBalance1.CssClass = "btn-information-section";
            btnGender1.CssClass = "btn-information-section";

        }

        protected void btnEnquiry_Click(object sender, EventArgs e)
        {
            ddlEnquiry.Focus();
            MultiView1.ActiveViewIndex = 2;
            btnIndividual.CssClass = "btn-information-section ";
            btnGroup.CssClass = "btn-information-section ";
            btnEnquiry.CssClass = "btn-information-section btn-information-section-selected";
            btnBalance1.CssClass = "btn-information-section";
            btnGender1.CssClass = "btn-information-section";
        }

        protected void btnBalance_Click1(object sender, EventArgs e)
        {
            txtContentBalance.Focus();
            MultiView1.ActiveViewIndex = 3;
            btnIndividual.CssClass = "btn-information-section ";
            btnGroup.CssClass = "btn-information-section ";
            btnEnquiry.CssClass = "btn-information-section";
            btnBalance1.CssClass = "btn-information-section btn-information-section-selected";
            btnGender1.CssClass = "btn-information-section";
        }

        protected void btnGender_Click1(object sender, EventArgs e)
        {
            ddlGender.Focus();
            MultiView1.ActiveViewIndex = 4;
            btnIndividual.CssClass = "btn-information-section ";
            btnGroup.CssClass = "btn-information-section ";
            btnEnquiry.CssClass = "btn-information-section";
            btnBalance1.CssClass = "btn-information-section";
            btnGender1.CssClass = "btn-information-section btn-information-section-selected";
        }


        private void AssignID()
        {
            objSendSMS.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objSendSMS.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        }

        private void BindDDL_MemberName()
        {
            try
            {
                AssignID();
                objSendSMS.Action = "Select_Member";
                dataTable = objSendSMS.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    ddlName.DataSource = dataTable;
                    ddlName.Items.Clear();
                    ddlName.DataValueField = "Member_ID1";
                    ddlName.DataTextField = "MemberName";
                    ddlName.DataBind();
                    ddlName.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_BY_Member_ID1";
                objSendSMS.Member_ID1 = Convert.ToInt32(ddlName.SelectedValue);

                dataTable = objSendSMS.GetDetails();
                if (dataTable.Rows.Count > 0)
                {
                    txtContactnum.Text = dataTable.Rows[0]["Contact1"].ToString();
                    ddlName.Focus();
                }
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                //}
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        protected void txtContactnum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_BY_Contact1";
                objSendSMS.Contact1 = txtContactnum.Text;

                dataTable = objSendSMS.GetDetails();
                if (dataTable.Rows.Count > 0)
                {
                    ddlName.SelectedValue = dataTable.Rows[0]["Member_ID1"].ToString();
                    txtContactnum.Focus();
                }
                else
                {
                    ddlName.SelectedIndex = 0;
                    txtContactnum.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }

        private void DisplayMessage()
        {
            if (res1 == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message Sending failed due to Internet Connection !!!','Error');", true);
                return;
            }
            else if (res1 == 2)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Message not send! SMS Status is off !!!','Error');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Message Sent Successfully !!!','Success');", true);
            }
        }



        #region------------SMS Function--------------------

        string suname, spass, senderid, routeid, status;
        public int SendSMSFun(string Mobile, string Message)
        {
            int Val;
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_SMSLogin_INFO";
                DataSet ds = new DataSet();
                //ds.Tables.Add(objSendSMS.GetDetails()); //objSendSMS.GetDetails();

                ds = objSendSMS.GetSMSLoginDetails();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    suname = ds.Tables[0].Rows[0]["Username"].ToString();
                    spass = ds.Tables[0].Rows[0]["Password"].ToString();
                    senderid = ds.Tables[0].Rows[0]["Sender_ID"].ToString();
                    routeid = ds.Tables[0].Rows[0]["Route"].ToString();
                    status = ds.Tables[0].Rows[0]["Status"].ToString();
                }

                if (status == "ON")
                {
                    Val = 0;
                    string strUrl = "http://173.45.76.226:81/send.aspx?username=" + suname + "&pass=" + spass + "&route=" + routeid + "&senderid=" + senderid + "&numbers=" + Mobile + "&message=" + Server.UrlEncode(Message) + "";
                    //System.Web.HttpUtility.UrlEncode;
                    WebRequest request = HttpWebRequest.Create(strUrl);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = (Stream)response.GetResponseStream();
                    StreamReader readStream = new StreamReader(s);
                    string dataString = readStream.ReadToEnd();
                    response.Close();
                    s.Close();
                    readStream.Close();
                }
                else
                {
                    Val = 2;
                }
            }
            catch (WebException ex)
            {
                Val = 1;
            }

            if (Val == 1)
                return 1;
            else if (Val == 2)
                return 2;

            else
                return 0;
        }

        #endregion


        #region-----------Individual SMS----------------------

        protected void btnSendIndividual_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_SMSLogin_INFO";
                dataTable = objSendSMS.Get_SMSLogin_Datails();
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        individualSMSWithName();
                    }
                    else
                    {
                        indiSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('SMS Login Information Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void individualSMSWithName()
        {

            if (txtContactnum.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Mobile No !!!','Error');", true);
            }

            if (txtIndividual.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);
            }

            if (txtContactnum.Text.Length != 0 && txtIndividual.Text.Length != 0)
            {
                string MemName = ddlName.SelectedItem.Text;

                //objSendSMS.MemberName = MemName; 
                objSendSMS.Action = "Get_Member_Gender_SMSStatus_by_Id";
                objSendSMS.Member_ID1 = Convert.ToInt32(ddlName.SelectedValue);
                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["SMSStatus"].ToString() == "Yes")
                    {
                        string St = "";
                        string Gender = dataTable.Rows[0]["Gender"].ToString();
                        if (Gender == "Male")
                        {
                            St = "Sir";
                        }
                        if (Gender == "Female")
                        {
                            St = "Madam";
                        }

                        string Message = "Dear " + MemName + " " + St + ",\n" + txtIndividual.Text;
                        string Mobile = txtContactnum.Text;
                        res1 = SendSMSFun(Mobile, Message);

                        ddlName.Focus();
                        txtIndividual.Text = string.Empty;
                        txtContactnum.Text = string.Empty;
                        ddlName.SelectedIndex = 0;

                        DisplayMessage();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member SMS Status is off !!!','Error');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member SMS Status is off !!!','Error');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
                return;
            }

        }

        private void indiSMSWithoutName()
        {
            // AssignID();
            if (txtContactnum.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Mobile No !!!','Error');", true);
            }

            if (txtIndividual.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);
            }


            if (txtContactnum.Text.Length != 0 && txtIndividual.Text.Length != 0)
            {
                string MemName = ddlName.SelectedItem.Text;

                objSendSMS.Action = "Get_Member_SMSStatus_by_Id";
                objSendSMS.Member_ID1 = Convert.ToInt32(ddlName.SelectedValue);
                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["SMSStatus"].ToString() == "Yes")
                    {
                        res1 = SendSMSFun(txtContactnum.Text, txtIndividual.Text);
                        ddlName.Focus();
                        txtIndividual.Text = string.Empty;
                        txtContactnum.Text = string.Empty;
                        ddlName.SelectedIndex = 0;
                        DisplayMessage();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member SMS Status is off !!!','Error');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Member SMS Status is off !!!','Error');", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }


        }

        #endregion


        #region ------------Group SMS--------------------

        protected void btnSendGropuSMS_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_SMSLogin_INFO";
                dataTable = objSendSMS.Get_SMSLogin_Datails();
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        GroupSMSWithName();
                    }
                    else
                    {
                        GroupSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('SMS Login Information Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void GroupSMSWithName()
        {
            AssignID();

            if (ddlGroupStatus.SelectedValue == "--Select--")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Group Status !!!','Error');", true);

            if (txtGroupSMS.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);


            if (ddlGroupStatus.SelectedValue != "--Select--" && txtGroupSMS.Text.Length != 0)
            {
                objSendSMS.Status = ddlGroupStatus.SelectedValue;
                objSendSMS.Action = "SELECT_By_Status";

                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string MemName = dataTable.Rows[i]["MemberName"].ToString();
                        objSendSMS.MemberName = MemName;

                        if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                        {
                            string St = "";
                            string Gender = dataTable.Rows[i]["Gender"].ToString();
                            if (Gender == "Male")
                            {
                                St = "Sir";
                            }
                            if (Gender == "Female")
                            {
                                St = "Madam";
                            }

                            MNumber = dataTable.Rows[i]["Contact1"].ToString();
                            string Message = "Dear " + MemName + " " + St + ",\n" + txtGroupSMS.Text;
                            res1 = SendSMSFun(MNumber, Message);

                        }

                    }
                    ddlGroupStatus.Focus();
                    ddlGroupStatus.SelectedIndex = 0;
                    txtGroupSMS.Text = "";
                    DisplayMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Any Member Present !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }
        }

        private void GroupSMSWithoutName()
        {
            AssignID();

            if (ddlGroupStatus.SelectedValue == "--Select--")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Group Status !!!','Error');", true);

            if (txtGroupSMS.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);


            if (ddlGroupStatus.SelectedValue != "--Select--" && txtGroupSMS.Text.Length != 0)
            {
                objSendSMS.Status = ddlGroupStatus.SelectedValue;
                objSendSMS.Action = "SELECT_By_Status";

                dataTable = objSendSMS.GetDetails();
                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    int k1 = 0;
                    int Count150 = 1;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                        {
                            if (i < dataTable.Rows.Count - 1)
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                                MNumber += ",";
                            }
                            else
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                            }
                            k1++;
                            if (k1 == 150)
                            {
                                if (Count150 != 1)
                                {
                                    Count150++;
                                }
                                res1 = SendSMSFun(MNumber, txtGroupSMS.Text);
                                k1 = 0;
                                MNumber = string.Empty;
                            }
                        }

                    }

                    if (dataTable.Rows.Count / Count150 != 150)
                    {
                        res1 = SendSMSFun(MNumber, txtGroupSMS.Text);
                    }
                    ddlGroupStatus.Focus();
                    ddlGroupStatus.SelectedIndex = 0;
                    txtGroupSMS.Text = "";
                    DisplayMessage();


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Any Member Present !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }
        }

        #endregion


        #region-----------Enquiry SMS-----------------

        protected void btnSendEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_SMSLogin_INFO";
                dataTable = objSendSMS.Get_SMSLogin_Datails();
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        EnquirySMSWithName();
                    }
                    else
                    {
                        EnquirySMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('SMS Login Information Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void EnquirySMSWithName()
        {

            if (ddlEnquiry.SelectedValue == "--Select--")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Enquiry !!!','Error');", true);

            if (txtEnquiry.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);

            if (ddlEnquiry.SelectedValue != "--Select--" && txtEnquiry.Text.Length != 0)
            {
                objSendSMS.Rating = ddlEnquiry.SelectedValue;
                objSendSMS.Action = "SELECT_By_EnquiryRating";

                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string MemName = dataTable.Rows[i]["MemberName"].ToString();
                        objSendSMS.MemberName = MemName;

                        //if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                        //{
                            string St = "";
                            string Gender = dataTable.Rows[i]["Gender"].ToString();
                            if (Gender == "Male")
                            {
                                St = "Sir";
                            }
                            if (Gender == "Female")
                            {
                                St = "Madam";
                            }

                            MNumber = dataTable.Rows[i]["Contact1"].ToString();
                            string Message = "Dear " + MemName + " " + St + ",\n" + txtEnquiry.Text;
                            res1 = SendSMSFun(MNumber, Message);
                        //}

                    }

                    ddlEnquiry.Focus();
                    ddlEnquiry.SelectedIndex = 0;
                    txtEnquiry.Text = "";

                    DisplayMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Any Member Present !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }
        }

        private void EnquirySMSWithoutName()
        {
            if (ddlEnquiry.SelectedValue == "--Select--")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Enquiry !!!','Error');", true);

            if (txtEnquiry.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);

            if (ddlEnquiry.SelectedValue != "--Select--" && txtEnquiry.Text.Length != 0)
            {
                objSendSMS.Rating = ddlEnquiry.SelectedValue;
                objSendSMS.Action = "SELECT_By_EnquiryRating";
                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    int k1 = 0;
                    int Count150 = 1;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        //if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                        //{

                            if (i < dataTable.Rows.Count - 1)
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                                MNumber += ",";
                            }
                            else
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                            }
                            k1++;

                            if (k1 == 150)
                            {
                                if (Count150 != 1)
                                {
                                    Count150++;
                                }
                                res1 = SendSMSFun(MNumber, txtEnquiry.Text);
                                k1 = 0;
                                MNumber = string.Empty;
                            }

                        //}
                    }

                    if (dataTable.Rows.Count / Count150 != 150)
                    {
                        res1 = SendSMSFun(MNumber, txtEnquiry.Text);
                    }

                    ddlEnquiry.Focus();
                    ddlGroupStatus.SelectedIndex = 0;
                    txtGroupSMS.Text = "";

                    DisplayMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Any Member Present !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }

        }

        #endregion


        #region---------------Balance SMS-----------------

        protected void btnBalance_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_SMSLogin_INFO";
                dataTable = objSendSMS.Get_SMSLogin_Datails();
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        BalanceSMSWithName();
                    }
                    else
                    {
                        BalanceSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('SMS Login Information Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }       

        private void BalanceSMSWithName()
        {            
            if (txtContentBalance.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);
            
            if (txtContentBalance.Text.Length != 0)
            {
                AssignID();               
                objSendSMS.Action = "Select_By_RemBalancePayment";
                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string MemName = dataTable.Rows[i]["MemberName"].ToString();
                        objSendSMS.MemberName = MemName;

                        if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                        {
                            string St = "";
                            string Gender = dataTable.Rows[i]["Gender"].ToString();
                            if (Gender == "Male")
                            {
                                St = "Sir";
                            }
                            if (Gender == "Female")
                            {
                                St = "Madam";
                            }

                            string Member_ID = Convert.ToString(dataTable.Rows[i]["Member_ID1"]);
                            string Balance = Convert.ToString(dataTable.Rows[i]["Balance"]);

                            MNumber = dataTable.Rows[i]["Contact1"].ToString();
                            string Message = "Dear " + MemName + " " + St + ",\nMember Id : "+ Member_ID +",\nRemaining Balance :" + Balance +",\n" + txtContentBalance.Text;
                            res1 = SendSMSFun(MNumber, Message);

                        }

                    }

                    txtContentBalance.Text = "";
                    DisplayMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Member Balance Remaining !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }

        }

        private void BalanceSMSWithoutName()
        {

            if (txtContentBalance.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);

            if (txtContentBalance.Text.Length != 0)
            {
                AssignID();
                objSendSMS.Action = "Select_By_RemBalancePayment";
                dataTable = objSendSMS.GetDetails();
                
                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    int k1 = 0;
                    int Count150 = 1;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                        {
                            if (i < dataTable.Rows.Count - 1)
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                                MNumber += ",";
                            }
                            else
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                            }
                            k1++;
                            if (k1 == 150)
                            {
                                if (Count150 != 1)
                                {
                                    Count150++;
                                }
                                res1 = SendSMSFun(MNumber, txtContentBalance.Text);
                                k1 = 0;
                                MNumber = string.Empty;
                            }
                        }

                    }

                    if (dataTable.Rows.Count / Count150 != 150)
                    {
                        res1 = SendSMSFun(MNumber, txtContentBalance.Text);
                    }

                    txtContentBalance.Text = "";
                    DisplayMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Member Balance Remaining !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }

        }


        #endregion


        #region--------------- SMS By Gender-----------------

        protected void btnGender_Click(object sender, EventArgs e)
        {
            try
            {
                AssignID();
                objSendSMS.Action = "SELECT_SMSLogin_INFO";
                dataTable = objSendSMS.Get_SMSLogin_Datails();
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["SMSWithName"].ToString() == "YES")
                    {
                        GenderSMSWithName();
                    }
                    else
                    {
                        GenderSMSWithoutName();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('SMS Login Information Not Found !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorHandiling.SendErrorToText(ex);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void GenderSMSWithName()
        {

            if (ddlGender.SelectedValue == "--Select--")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Gender !!!','Error');", true);

            if (txtGender.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);

            if (ddlGender.SelectedValue != "--Select--" && txtGender.Text.Length != 0)
            {
                objSendSMS.Gender = ddlGender.SelectedValue;
                objSendSMS.Action = "SELECT_By_Gender";

                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        string MemName = dataTable.Rows[i]["MemberName"].ToString();
                        objSendSMS.MemberName = MemName;

                        if (dataTable.Rows.Count > 0)
                        {
                            if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                            {
                                string St = "";
                                string Gender = dataTable.Rows[i]["Gender"].ToString();
                                if (Gender == "Male")
                                {
                                    St = "Sir";
                                }
                                if (Gender == "Female")
                                {
                                    St = "Madam";
                                }

                                MNumber = dataTable.Rows[i]["Contact1"].ToString();
                                string Message = "Dear " + MemName + " " + St + ",\n" + txtGender.Text;
                                res1 = SendSMSFun(MNumber, Message);

                            }
                        }
                    }
                    ddlGender.Focus();
                    ddlGender.SelectedIndex = 0;
                    txtGender.Text = string.Empty;

                    DisplayMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Any Member Present !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }
        }

        private void GenderSMSWithoutName()
        {
            if (ddlGender.SelectedValue == "--Select--")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Select Gender !!!','Error');", true);

            if (txtGender.Text.Length == 0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Enter Message !!!','Error');", true);

            if (ddlGender.SelectedValue != "--Select--" && txtGender.Text.Length != 0)
            {
                objSendSMS.Gender = ddlGender.SelectedValue;
                objSendSMS.Action = "SELECT_By_Gender";

                dataTable = objSendSMS.GetDetails();

                if (dataTable.Rows.Count > 0)
                {
                    string MNumber = "";
                    int k1 = 0;
                    int Count150 = 1;

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i]["SMSStatus"].ToString() == "Yes")
                        {

                            if (i < dataTable.Rows.Count - 1)
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                                MNumber += ",";
                            }
                            else
                            {
                                MNumber += dataTable.Rows[i]["Contact1"].ToString();
                            }
                            k1++;
                            if (k1 == 150)
                            {
                                if (Count150 != 1)
                                {
                                    Count150++;
                                }
                                res1 = SendSMSFun(MNumber, txtGroupSMS.Text);
                                k1 = 0;
                                MNumber = string.Empty;
                            }

                        }
                    }

                    if (dataTable.Rows.Count / Count150 != 150)
                    {
                        res1 = SendSMSFun(MNumber, txtGender.Text);
                    }

                    ddlGender.Focus();
                    ddlGender.SelectedIndex = 0;
                    txtGender.Text = "";

                    DisplayMessage();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('No Any Member Present !!!','Error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Fill All Fields !!!','Error');", true);
            }
        }

        #endregion

        protected void btnClearIndi_Click(object sender, EventArgs e)
        {
            ddlName.Focus();
            ddlName.SelectedIndex = 0;
            txtContactnum.Text = string.Empty;
            txtIndividual.Text = string.Empty;
        }

        protected void btnClearGroup_Click(object sender, EventArgs e)
        {
            ddlGroupStatus.Focus();
            ddlGroupStatus.SelectedIndex = 0;
            txtGroupSMS.Text = string.Empty;
        }

        protected void btnClearEnquiry_Click(object sender, EventArgs e)
        {
            ddlEnquiry.Focus();
            ddlEnquiry.SelectedIndex = 0;
            txtEnquiry.Text = string.Empty;
        }

        protected void btnClearBalance_Click(object sender, EventArgs e)
        {
            txtContentBalance.Focus();
            txtContentBalance.Text = string.Empty;
        }

        protected void btnClearGender_Click(object sender, EventArgs e)
        {
            ddlGender.Focus();
            ddlGender.SelectedIndex = 0;
            txtGender.Text = string.Empty;
        }

       
    }
}