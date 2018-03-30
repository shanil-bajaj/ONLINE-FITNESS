using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NDOnlineGym_2017
{
    public partial class receiptemail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //string CompanyName, Address1, Address2, city, state, phone, email, note1, note2, note3, note4, newstring;
        //public void SendEmail()
        //{
        //    try
        //    {
        //        BALCourseRegistrationForm objBALCourseRegistrationForm = new BALCourseRegistrationForm();
        //        obBalMemInfo.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
        //        objBALCourseRegistrationForm.Branch_ID = Convert.ToInt32(Request.Cookies["GymSoftware"]["Branch_ID"]);
        //        obBalMemInfo.Member_ID = Convert.ToInt32(txtMemberId.Text);
        //        objBALCourseRegistrationForm.Member_ID = Convert.ToInt32(txtMemberId.Text);
        //        string MemEmail;
        //        dt = obBalMemInfo.Select_By_Member_ID_Member_Details();
        //        if (dt.Rows.Count > 0)
        //        {
        //            MemEmail = dt.Rows[0]["Email"].ToString();
        //            dt = obBalMemInfo.Select_EmailStatus();
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (dt.Rows[0]["Status"].ToString() == "ON")
        //                {
        //                    DataSet dataSet = objBALCourseRegistrationForm.GetCompanyinfByBranchIdId();
        //                    if (dataSet.Tables[0].Rows.Count > 0)
        //                    {
        //                        CompanyName = dataSet.Tables[0].Rows[0]["CompanyName"].ToString();
        //                        Address1 = dataSet.Tables[0].Rows[0]["Address1"].ToString();
        //                        Address2 = dataSet.Tables[0].Rows[0]["Address2"].ToString();
        //                        city = dataSet.Tables[0].Rows[0]["City"].ToString();
        //                        state = dataSet.Tables[0].Rows[0]["State"].ToString();
        //                        phone = dataSet.Tables[0].Rows[0]["Phone"].ToString();
        //                        email = dataSet.Tables[0].Rows[0]["Email"].ToString();
        //                        note1 = dataSet.Tables[0].Rows[0]["Note1"].ToString();
        //                        note2 = dataSet.Tables[0].Rows[0]["Note2"].ToString();
        //                        note3 = dataSet.Tables[0].Rows[0]["Note3"].ToString();
        //                        note4 = dataSet.Tables[0].Rows[0]["Note4"].ToString();
        //                    }

        //                    dt = obBalMemInfo.Select_By_Header();

        //                    string EmailContentHeader = dt.Rows[0]["EmailContent"].ToString();
        //                    string newstring = string.Empty;
        //                    StringBuilder sb = new StringBuilder(EmailContentHeader);
        //                    sb.Replace("name#Mname", ddlMemberName.SelectedItem.Text);
        //                    newstring = sb.ToString();
        //                    if (MemEmail != "")
        //                    {
        //                        dt = obBalMemInfo.Select_By_EmailLogin();
        //                        MailMessage msg = new MailMessage();
        //                        MailAddress fromaddress = new MailAddress(dt.Rows[0]["EmailID"].ToString());
        //                        msg.From = fromaddress;
        //                        msg.To.Add(MemEmail);
        //                        msg.Subject = "Course Receipt";












                                //string message = string.Empty;
                                //message = newstring;// " <b>Hi " + cmbName.Text + "</b></br></br> </br></br> </br></br> " + gm.Rows[0]["EmailContent"].ToString() + "</br></br>";
                                //message += "<div  style='width:100%;'><div style='display: table;height: 100%;margin: 0 auto;'><div style='display: table-cell; vertical-align: middle;'><div><div id='dvContainerPrint' style=' width:100%;border:1px solid black;padding:10px; '>";
                                //message += "<div id='dvContainer' runat='server' visible='true'><div style='width: 725px;'><div><div style='display: table;height: 100%;margin: 0 auto;color:black' id='receipt'> ";
                                //message += "<div style='display: table-cell;vertical-align: middle;color:black'><div style='float: left;margin:5px;text-align:center; '><ul style='width: 20%;padding: 0px 0px;text-align: left;float: left;list-style: none; margin: 0px;'><li style='margin: 10px 0px 0px 0px;'></li></ul>";
                                //message += "<ul  style='width: 55%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin: 0px; margin: 10px 0px 0px 0px;'><li style='text-align: center; width: 400px; font-size: x-large; font-weight: bold;'>" + lblCompanyName.Text + "</li>";
                                //message += "<li style='width: 55%;padding: 0px 0px;text-align: left; float: left;list-style: none; margin: 0px; text-align: center; width: 400px;'>" + lblAddress1.Text + "</li>";
                                //message += "<li style='width: 55%;padding: 0px 0px;text-align: left; float: left;list-style: none; margin: 0px; text-align: center; width: 400px;'>" + lblAddress2.Text + "</li>";
                                //message += "<li style='width: 55%;padding: 0px 0px;text-align: left; float: left;list-style: none; margin: 0px; text-align: center; width: 400px;'>" + lblEmail.Text + "</li></ul>";
                                //message += "<ul style='width: 25%;padding: 0px 0px;text-align: left;float: right;list-style: none; margin: 0px;'>";
                                //message += "<li style='width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;'><span><strong>Receipt Date :</strong></span>" + lblReceiptDate.Text + "</li>";
                                //message += "<li style='width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;'><span><strong>GST No. :</strong></span>" + lblGSTNo.Text + "</li>";
                                //message += "<li style='width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;'><span><strong>Refer Receipt No. :</strong></span>" + lblReferReceiptNo.Text + "</li>";
                                //message += "<li style='width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;'><span><strong>Receipt No. :</strong></span>" + lblReceiptNo.Text + "</li></ul></div><table><tr>";
                                //message += "<td style='margin-right:20px'><div style='width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;'> ";
                                //message += "<span><strong>Category :</strong></span>" + lblCourseCategory.Text + "</div></td>";
                                //message += "<td style='margin-right:20px'><div style='width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;'> ";
                                //message += "<span><strong>Admin : </strong></span>" + lblGroupOwner.Text + "</div></td></tr></table>";


                                ////***********************************************************
                                //message += "<span><strong>Admin : </strong></span>" + lblGroupOwner.Text + "</div></td></tr></table>";

                                ////***********************************************************
                                 










        //                        message += "<td style='width:400px; padding-left:15px'> <b>Receipt No  :  </b>" + ViewState["BalPayid"] + "<br/><b>Pay Date  :  </b>" + txtPaymentDate.Text + "<br/></td></tr>";
        //                        message += "<tr><td colspan='2'><table border='1'><tr><th style='width:316px'><b>Course Name  :  </b>" + "</th><th style='width:195px'><b>Plan Name :  </b>" + "</th> <th style='width:135px'><b>Reg. Fees :  </b>" + "</th> <th style='width:114px'><b>Course Fees :  </b>" + "</th></tr>";
        //                        message += "<tr><td style='width:316px'>" + ddlCourseName.SelectedItem.Text + "</td><td style='width:195px'>" + ddlPlanName.SelectedItem.Text + "</td> <td style='width:135px'>" + txtRegFees.Text + "</td> <td style='width:114px'>" + txtCourseFees.Text + "</td> </tr></table></td></tr>";
        //                        message += "<tr><td style='width:400px;padding-left:15px'> <b>Start Date :  </b>" + txtStartDate.Text + "<br/><b>End Date :  </b>" + txtEndDate.Text + "<br/><b>Pay. Mode :  </b>" + ddlPaymentMode.SelectedItem.Text + "&nbsp;&nbsp;&nbsp;" + txtPaymentDetails.Text + "<br/><b>Paid Amt. In Words :  </b>" + ViewState["Amtinword"] + "<br/><b>Balance :  </b>" + txtBalance.Text + "</td>";
        //                        message += "<td style='width:400px;padding-left:15px'> <b>Discount :  </b>" + txtDiscount.Text + "<br/><b>Tax :  </b>" + txtTax.Text + "&nbsp;&nbsp;&nbsp" + txtRS.Text + "<br/><b>Total :  </b>" + txtTotalFees.Text + "<br/><b>Paid Fees :  </b>" + txtPaidFees.Text + "<br/><b>Followup Date :  </b>" + txtNextPaymentDate.Text + "</td></tr>";
        //                        message += "<tr><td style=' border-top:1px solid black; padding-left:15px' colspan='2'><b>Terms And Condition's :  </b><br/>" + note1 + "<br/>" + note2 + "<br/>" + note3 + "<br/>" + note4 + "</td></tr>";
        //                        message += " <tr><td><tr><td><b>Member Sign :  </b></td>";
        //                        message += " <td><tr><td><b>Authorised Sign :  </b>" + CompanyName + "</td></tr></td></tr></table>";

        //                        dt = obBalMemInfo.Select_By_Footer();
        //                        string EmailContentFooter = dt.Rows[0]["EmailContent"].ToString();
        //                        message += dt.Rows[0]["EmailContent"].ToString();
        //                        msg.Body = message;

        //                        msg.IsBodyHtml = true;
        //                        SmtpClient Client = new SmtpClient();
        //                        Client.Host = "relay-hosting.secureserver.net";   //-- Donot change.
        //                        Client.Port = 25; //--- Donot change
        //                        Client.EnableSsl = false;//--- Donot change
        //                        Client.UseDefaultCredentials = true;
        //                        dt = obBalMemInfo.Select_By_EmailLogin();
        //                        NetworkCredential credentials = new NetworkCredential(dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Password"].ToString());
        //                        Client.Credentials = credentials;
        //                        try
        //                        {
        //                            Client.Send(msg);
        //                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.success('Email Has Been Sent','Success');", true);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Email Failed','Failed');", true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Error_Validation_Page("Email Status is Off");
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Error_Validation_Page("Email Not Exists");
        //                return;
        //            }
        //            //Client.UseDefaultCredentials = false;
        //            //dt = obBalMemInfo.Select_By_EmailLogin();
        //            //Client.Credentials = new System.Net.NetworkCredential(dt.Rows[0]["EmailID"].ToString(), dt.Rows[0]["Password"].ToString());
        //            //// Client.Credentials = new System.Net.NetworkCredential("navkardreams.it@gmail.com", "9970583111@");
        //            //Client.EnableSsl = true;
        //            //Client.Send(msg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Error_Validation_Page(ex.Message.ToString());
        //    }
        //}
    }
}