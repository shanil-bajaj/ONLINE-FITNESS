using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
//using iTextSharp;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;

namespace NDOnlineGym_2017
{
    public partial class Receipt : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        BalBranchInformation obBalBranchInformation = new BalBranchInformation();
        BalCourseReceipt obBalCourseReceipt = new BalCourseReceipt();
        BalBalancePayment objBalance = new BalBalancePayment();
        int Receipt_No, Receipt_No1;
        string Status;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Receipt_No"] != null)
                {
                    Refer.InnerText = "Receipt No.";
                    ReceiptNo.Visible = false;
                    Receipt_No = Convert.ToInt32(Request.QueryString["Receipt_No"]);
                    lblReferReceiptNo.Text = Receipt_No.ToString();
                    AssignTodaysDate();
                    GetCompanyDetails();
                    CountReceiptMembers();
                    GetReceiptMembers();
                    GetReceiptCourseDetails();
                    GetReceiptPaymentDetails();
                    GetReceiptBalanceDetails();
                  //  GetTotalDiscountDetails();
                }

                if (Request.QueryString["PT"] != null)
                {
                    Refer.InnerText = "Receipt No.";
                    ReceiptNo.Visible = false;
                    Receipt_No = Convert.ToInt32(Request.QueryString["PT"]);
                    lblReferReceiptNo.Text = Receipt_No.ToString();
                    AssignTodaysDate();
                    GetCompanyDetails();
                    CountReceiptMembers();
                    GetReceiptMembers();
                    GetReceiptCourseDetails();
                    GetReceiptPaymentDetails_PT();
                   // GetReceiptPaymentDetails();
                    GetReceiptBalanceDetails_PT();
                   // GetReceiptBalanceDetails();
                    //  GetTotalDiscountDetails();
                }

                if (Request.QueryString["Receipt_Balance"] != null)
                {
                    ReceiptNo.Visible = true;
                    Receipt_No1 = Convert.ToInt32(Request.QueryString["Receipt_Balance"]);
                    Receipt_No = Convert.ToInt32(Request.QueryString["ID"]);
                    //Bal_ReceiptNo();
                    lblReferReceiptNo.Text = Receipt_No.ToString();
                    lblReceiptNo.Text = Receipt_No1.ToString();
                    AssignTodaysDate();
                    GetCompanyDetails();
                    CountReceiptMembers();
                    GetReceiptMembers();
                    GetReceiptCourseDetails();
                    GetReceiptPaymentDetails1();
                    GetReceiptBalanceDetails1();
                   // GetTotalDiscountDetails();
                }
                if (Request.QueryString["ID"] != null && Request.QueryString["Receipt_Balance"]==null)
                {

                    Refer.InnerText = "Receipt No.";
                    ReceiptNo.Visible = false;
                    Receipt_No = Convert.ToInt32(Request.QueryString["ID"]);
                    lblReferReceiptNo.Text = Receipt_No.ToString();
                    AssignTodaysDate();
                    GetCompanyDetails();
                    CountReceiptMembers();
                    GetReceiptMembers();
                    GetReceiptCourseDetails();
                    GetReceiptPaymentDetails();
                    GetReceiptBalanceDetails();

                }

            }

        }

        public void Bal_ReceiptNo()
        {
            objBalance.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalance.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalance.ReceiptID1 = Receipt_No1;
            dt = objBalance.get_ReceiptNo();
            {
                Receipt_No = Convert.ToInt32(dt.Rows[0]["ReceiptID"].ToString());

            }
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        if (Request.QueryString["Receipt_No"] != null)
        //        {
        //            Refer.InnerText = "Receipt No.";
        //            ReceiptNo.Visible = false;
        //            Receipt_No = Convert.ToInt32(Request.QueryString["Receipt_No"]);
        //            lblReferReceiptNo.Text = Receipt_No.ToString();
        //            AssignTodaysDate();
        //            GetCompanyDetails();
        //            CountReceiptMembers();
        //            GetReceiptMembers();
        //            GetReceiptCourseDetails();
        //            GetReceiptPaymentDetails();
        //            GetReceiptBalanceDetails();
        //        }
        //        if (Request.QueryString["Receipt_Balance"] != null)
        //        {
        //            Receipt_No1 = Convert.ToInt32(Request.QueryString["Receipt_Balance"]);
        //            Bal_ReceiptNo();
        //            lblReferReceiptNo.Text = Receipt_No.ToString();
        //            lblReceiptNo.Text = Receipt_No1.ToString();
        //            AssignTodaysDate();
        //            GetCompanyDetails();
        //            CountReceiptMembers();
        //            GetReceiptMembers();
        //            GetReceiptCourseDetails();
        //            GetReceiptPaymentDetails1();
        //            GetReceiptBalanceDetails1();
        //        }

        //    }

        //}

        protected void GetReceiptPaymentDetails_PT()
        {

            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetReceiptPaymentDetails_PT();

            if (dt.Rows.Count > 0)
            {
                DateTime paydate, nextpaydate;
                //string GST ;
                //int n = Convert.ToInt32(dt.Rows[0]["payDate"].ToString());
                //int n1 = n / 2;
                //GST = n1.ToString();

                if (dt.Rows[0]["payDate"].ToString() != "")
                {
                    paydate = Convert.ToDateTime(dt.Rows[0]["payDate"].ToString());
                    lblReceiptDate.Text = paydate.ToString("dd-MM-yyyy");
                }
                else
                {
                    lblReceiptDate.Text = "";
                }

                if (dt.Rows[0]["NextBalDate"].ToString() != "")
                {
                    nextpaydate = Convert.ToDateTime(dt.Rows[0]["NextBalDate"].ToString());
                    lblNextPayDate.Text = nextpaydate.ToString("dd-MM-yyyy");
                }
                else
                {
                    lblNextPayDate.Text = "";
                }
                lblGST.Text = dt.Rows[0]["taxpec"].ToString();
                lblCGST.Text = dt.Rows[0]["tax"].ToString();
                lblSGST.Text = dt.Rows[0]["tax"].ToString();
                //string paydate1 = dt.Rows[0]["payDate"].ToString();
                //if (DateTime.TryParseExact(paydate1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out paydate))
                //{
                //    lblReceiptDate.Text = paydate.ToString("dd-MM-yyyy");
                //}
                //string nextpaydate1 = dt.Rows[0]["NextBalDate"].ToString();
                //if (DateTime.TryParseExact(nextpaydate1, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out nextpaydate))
                //{
                //    lblNextPayDate.Text = nextpaydate.ToString("dd-MM-yyyy");
                //}


                RepeaterPaymentDetails.DataSource = dt;
                RepeaterPaymentDetails.DataBind();
            }

        }


        #region Assign_Date
        protected void AssignTodaysDate()
        {
            //DateTime todaydate;
            //if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            //{
            //    lblReceiptDate.Text = todaydate.ToString("dd-MM-yyyy");
              
            //}
        }
        #endregion

        #region GetCompanyDetails
        protected void GetCompanyDetails()
        {
            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = obBalCourseReceipt.SELECT_CompanyDetails();
            if (dt.Rows.Count > 0)
            {
                lblCompanyName.Text = dt.Rows[0]["BranchName"].ToString();
                imgCompanyLogo.ImageUrl = dt.Rows[0]["BranchLogoPath"].ToString();
                lblAddress1.Text = dt.Rows[0]["Address1"].ToString();
                lblAddress2.Text = dt.Rows[0]["Address2"].ToString();
                lblEmail.Text = dt.Rows[0]["Email"].ToString();
                lblGSTNo.Text = dt.Rows[0]["GSTNo"].ToString();
                lblNote1.Text = dt.Rows[0]["TermsAndCondition"].ToString();
                lblCompanyNamefooter.Text = dt.Rows[0]["BranchName"].ToString();
            }
        }
        #endregion

        #region CountReceiptMembers
        protected void CountReceiptMembers()
        {
            int cnt = 0;
            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.CountReceiptMembers();
            if (dt.Rows.Count > 0)
            {
                cnt = Convert.ToInt32(dt.Rows[0]["cnt"].ToString());
            }
            if (cnt == 1)
            {
                lblCourseCategory.Text = "Single";
            }
            else if (cnt == 2)
            {
                lblCourseCategory.Text = "Couple";
                group.Visible = true;
                getOwnerName();
            }
            else if (cnt > 2)
            {
                lblCourseCategory.Text = "Group";
                group.Visible = true;
                getOwnerName();
            }
        }

        protected void getOwnerName()
        {
            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetOwnerName();
            if (dt.Rows.Count > 0)
            {
                lblGroupOwner.Text = dt.Rows[0]["Name"].ToString();
            }
            
        }
        #endregion

        #region GetReceiptMembers
        protected void GetReceiptMembers()
        {

            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetReceiptMembers();
            if (dt.Rows.Count > 0)
            {
                RepterMemberDetails.DataSource = dt;
                RepterMemberDetails.DataBind();
            }
            
        }
        #endregion

        #region GetReceiptCourseDetails
        protected void GetReceiptCourseDetails()
        {

            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetReceiptCourseDetails();
            if (dt.Rows.Count > 0)
            {
                RepeaterCourseDetails.DataSource = dt;
                RepeaterCourseDetails.DataBind();
            }

            int sum = Convert.ToInt32(dt.Compute("SUM(Discount)", string.Empty));
            lblTotalDiscount.Text = sum.ToString();

        }
        #endregion

        #region GetReceiptPaymentDetails
        protected void GetReceiptPaymentDetails()
        {

            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetReceiptPaymentDetails();

            if (dt.Rows.Count > 0)
            {
                DateTime paydate, nextpaydate;
                //string GST ;
                //int n = Convert.ToInt32(dt.Rows[0]["payDate"].ToString());
                //int n1 = n / 2;
                //GST = n1.ToString();
                
                if (dt.Rows[0]["payDate"].ToString() != "")
                {
                    paydate = Convert.ToDateTime(dt.Rows[0]["payDate"].ToString());
                    lblReceiptDate.Text = paydate.ToString("dd-MM-yyyy");
                }
                else
                {
                    lblReceiptDate.Text = "";
                }

                if (dt.Rows[0]["NextBalDate"].ToString() != "")
                {
                    nextpaydate = Convert.ToDateTime(dt.Rows[0]["NextBalDate"].ToString());
                    lblNextPayDate.Text = nextpaydate.ToString("dd-MM-yyyy");
                }
                else
                {
                    lblNextPayDate.Text = "";
                }
                lblGST.Text = dt.Rows[0]["taxpec"].ToString();
                lblCGST.Text = dt.Rows[0]["tax"].ToString();
                lblSGST.Text = dt.Rows[0]["tax"].ToString();
                //string paydate1 = dt.Rows[0]["payDate"].ToString();
                //if (DateTime.TryParseExact(paydate1.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out paydate))
                //{
                //    lblReceiptDate.Text = paydate.ToString("dd-MM-yyyy");
                //}
                //string nextpaydate1 = dt.Rows[0]["NextBalDate"].ToString();
                //if (DateTime.TryParseExact(nextpaydate1, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out nextpaydate))
                //{
                //    lblNextPayDate.Text = nextpaydate.ToString("dd-MM-yyyy");
                //}
                    
               
                RepeaterPaymentDetails.DataSource = dt;
                RepeaterPaymentDetails.DataBind();
            }

        }
        protected void GetReceiptPaymentDetails1()
        {
            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            obBalCourseReceipt.ReferReceiptID = Receipt_No1;

            dt = obBalCourseReceipt.GetReceiptPaymentDetails1();
            if (dt.Rows.Count > 0)
            {
                DateTime paydate, nextpaydate;
                if (dt.Rows[0]["payDate"].ToString() != "")
                {
                    paydate = Convert.ToDateTime(dt.Rows[0]["payDate"].ToString());
                    lblReceiptDate.Text = paydate.ToString("dd-MM-yyyy");
                }
                else
                {
                    lblReceiptDate.Text = "";
                }

                if (dt.Rows[0]["NextBalDate"].ToString() != "")
                {
                    nextpaydate = Convert.ToDateTime(dt.Rows[0]["NextBalDate"].ToString());
                    lblNextPayDate.Text = nextpaydate.ToString("dd-MM-yyyy");
                }
                else
                {
                    lblNextPayDate.Text = "";
                }
                RepeaterPaymentDetails.DataSource = dt;
                RepeaterPaymentDetails.DataBind();
            }

        }
        #endregion

        protected void GetReceiptBalanceDetails_PT()
        {

            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetReceiptBalanceDetails_PT();
            if (dt.Rows.Count > 0)
            {
                lblTotalPaid.Text = dt.Rows[0]["PaidFee"].ToString();
                lblBalance.Text = dt.Rows[0]["Balance"].ToString();
                lblReceiptTotal.Text = dt.Rows[0]["TotalFeeDue"].ToString();
            }

        }
        #region GetReceiptBalanceDetails
        protected void GetReceiptBalanceDetails()
        {

            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetReceiptBalanceDetails();
            if (dt.Rows.Count > 0)
            {
                lblTotalPaid.Text = dt.Rows[0]["PaidFee"].ToString();
                lblBalance.Text = dt.Rows[0]["Balance"].ToString();
                lblReceiptTotal.Text = dt.Rows[0]["TotalFeeDue"].ToString();
            }

        }

        protected void GetReceiptBalanceDetails1()
        {

            obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
            obBalCourseReceipt.ReceiptID = Receipt_No;
            dt = obBalCourseReceipt.GetReceiptBalanceDetails1();
            if (dt.Rows.Count > 0)
            {
                lblTotalPaid.Text = dt.Rows[0]["PaidFee"].ToString();
                lblBalance.Text = dt.Rows[0]["Balance"].ToString();
                lblReceiptTotal.Text = dt.Rows[0]["TotalFeeDue"].ToString();
            }

        }
        #endregion

        protected void btnSendOnMail_Click(object sender, EventArgs e)
        {

        //    ////Create a pdf document.
        //    //PdfDocument doc = new PdfDocument();
        //    //String url = "http://www.london2012.com/news/articles/paralympic-torch-relay-route-revealed-1258473.html";
        //    //doc.LoadFromHTML(url, false, true, true);
        //    ////Save pdf file.
        //    //doc.SaveToFile("sample.pdf");
        //    //doc.Close();
        //    ////Launching the Pdf file.
        //    //System.Diagnostics.Process.Start("sample.pdf");



            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //pnlPerson.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();
     



            //try
            //{
            //    object TargetFile = FileName;
            //    string ModifiedFileName = string.Empty;
            //    string FinalFileName = string.Empty;

            //    // HtmlStream = HtmlStream.Replace("\r\n", "");

            //    GeneratePDF.HtmlToPdfBuilder builder = new GeneratePDF.HtmlToPdfBuilder(iTextSharp.text.PageSize.A4);

            //    GeneratePDF.HtmlPdfPage first = builder.AddPage();
            //    first.AppendHtml(pnlPerson);
            //    builder.ImportStylesheet(HttpContext.Current.Request.PhysicalApplicationPath + "CSS\\myxyz.css");
            //    byte[] file = builder.RenderPdf();
            //    File.WriteAllBytes(TargetFile.ToString(), file);

            //    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());
            //    ModifiedFileName = TargetFile.ToString();
            //    ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");

            //    iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, "", "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
            //    reader.Close();
            //    if (File.Exists(TargetFile.ToString()))
            //        File.Delete(TargetFile.ToString());
            //    FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
            //    File.Copy(ModifiedFileName, FinalFileName);
            //    if (File.Exists(ModifiedFileName))
            //        File.Delete(ModifiedFileName);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }

        //#region GetTotalDiscountDetails
        //protected void GetTotalDiscountDetails()
        //{

        //    obBalCourseReceipt.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
        //    obBalCourseReceipt.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
        //    obBalCourseReceipt.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        //    obBalCourseReceipt.ReceiptID = Receipt_No;
        //    dt = obBalCourseReceipt.GetTotalDiscountDetails();
        //    if (dt.Rows.Count > 0)
        //    {
        //        lblTotalDiscount.Text = dt.Rows[0]["TotalDiscount"].ToString();
        //    }

        
        //#endregion



    }
}