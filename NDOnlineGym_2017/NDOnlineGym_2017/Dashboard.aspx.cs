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
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;


namespace NDOnlineGym_2017
{        

    public partial class Dashboard : System.Web.UI.Page
    {
        BalDashBoard dash = new BalDashBoard();
        DataTable dt = new DataTable();
        public int jan = 0;
        public int mem = 0;
        public int month;
        public int Ye;
        public double year;
        public int Enquiry;
        public int Branch_ID;
        BalChartBLL objChart = new BalChartBLL();
        BalStaffNotificationHome objStaffNotificationHome = new BalStaffNotificationHome();
        BalMemeberProfileInfo objMember = new BalMemeberProfileInfo();


        BalViewBalancePaymentFollowup obBalViewBalancePaymentFollowup = new BalViewBalancePaymentFollowup();
        BalCallRespondMaster obBalCallRespondMaster = new BalCallRespondMaster();
        BalFollowupTypeMaster obBalFollowupTypeMaster = new BalFollowupTypeMaster();
        BalStaffRegistration obBalStaffRegistration = new BalStaffRegistration();
        BalFollowup objFollowup = new BalFollowup();
        DataTable dataTable = new DataTable();
        BalAddMember objMemberDetails = new BalAddMember();
        int res;
        static int MemberAutoID;
        static  int flagpopup = 0;

        BalEnquiryFollowup objEnqFlw = new BalEnquiryFollowup();
        BalEnquiry objBalEnquiry = new BalEnquiry();
        static int Enq_ID;
        static int Company_AutoID;
        static int Branch_AutoID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

                if(flagpopup ==1)
                    Label33_ModalPopupExtender1.Show();
                if (flagpopup == 2)
                    Label34_ModalPopupExtender1.Show();
                DateTime todaydate;
                if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                { dash.date = todaydate; }
                if (!IsPostBack)
                {
                    if (Request.QueryString["Flag"] != "1")
                    {
                        BindStaffNotification();
                        BindChart();
                        bindMember();
                        bindMemberEnd();
                        BindCollection();
                        BindPresentMember();
                        Display_MemberBirth_Cnt();
                        Disply_Balance_Cnt();
                        MemberAnneversary();
                        StaffBirth();
                        Addmission();
                        enquir();
                        Active();
                        Deactive();
                        Enq_Flwp();
                        OtherFollupCount();
                        MemberEnd();
                        GridFollowup.Visible = false;
                        Display_PostDatedCheque_Cnt();
                    }
                }
                if (Request.QueryString["Flag"] == "1")
                {
                    if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
                    {
                        DivMain.Visible = false;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
                    {
                        DivMain.Visible = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
                    {
                        DivMain.Visible = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
                    {
                        DivMain.Visible = true;
                    }
                    else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
                    {
                        DivMain.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void GetMemberAutoID_By_ID1(int MemberID1)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dash.MemberId1 = Convert.ToInt32(MemberID1);
            dt = dash.GetMemberAutoID();
            if(dt.Rows.Count > 0)
            {
                ViewState["MemAutoID"] = dt.Rows[0]["Member_AutoID"];
            }
        }
  
        public void GetEnqAutoID_By_ID1(int EnqID1)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dash.Enq_ID1 = Convert.ToInt32(EnqID1);
            dt = dash.getEnqAutoID();
            if (dt.Rows.Count > 0)
            {
                ViewState["EnqAutoID"] = dt.Rows[0]["Enq_AutoID"];
            }
        }
  
        public void BindStaffNotification()
        {
            if (Session["Branch_ID"] != null)
                objStaffNotificationHome.Branch_AutoID = Convert.ToInt32(Session["Branch_ID"]);
            else
                objStaffNotificationHome.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objStaffNotificationHome.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = objStaffNotificationHome.Select_StaffNotificationHomePage();
            if (dt.Rows.Count > 0)
            {
                RepterDetails.DataSource = dt;
                RepterDetails.DataBind();
            }
        }

        public void bindMemberEnd()
        {
           int Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;

            DataSets ds = new DataSets();
            int Enquiryy = 0;
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            DataTable dt1 = objChart.BindMemberEndDayWise();
            if (dt1 != null)
            {
                DataRow[] ServiceRow2 = dt1.Select();
                for (int j = 1; j <= 31; j++)
                {
                    jan = 0;
                    
                    for (int m = 0; m < ServiceRow2.Length; m++)
                    {
                        if (ServiceRow2[m][0].ToString() != "")
                        {
                            DateTime D1 = Convert.ToDateTime(ServiceRow2[m][0]);
                            if (D1.Month == month && D1.Year == Ye && D1.Day == j)
                            {
                                jan += 1;
                            }
                        }
                    }

                    ds.Tables["MemberEnd"].Rows.Add(jan);

                    Enquiryy += jan;
                }
                ds.AcceptChanges();

                DataRow[] SeviceRow = ds.Tables["MemberEnd"].Select();

                //this.Chart1.Series["Enquiry"].Points.Clear();
                for (int j = 0; j <= 30; j++)
                {
                    this.ChartMemberEnd.Series[0].Points.AddXY(j + 1, SeviceRow[j][0]);
                    ChartMemberEnd.ChartAreas[0].AxisX.IsMarginVisible = false;

                }
                Label14.Text = Enquiryy.ToString();

            }

            ChartMemberEnd.Series[0].ChartType = SeriesChartType.Column;
        }

        public void BindCollection()
        {
             Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;
            DataSets ds = new DataSets();
            int exp = 0;
            objChart.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Brnach_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            DataTable dt = objChart.BindCollectionDayWise();
            if (!(dt is DBNull))
            {
                for (int j = 1; j <= 31; j++)
                {
                    jan = 0;
                    int jans = 0;
                    DataRow[] ServiceRow = dt.Select();
                    for (int i = 0; i < ServiceRow.Length; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(ServiceRow[i][0]);
                        if (D2.Month == month && D2.Year == Ye && D2.Day == j)
                        {
                            jan += Convert.ToInt32(ServiceRow[i][1]);
                            
                        }
                    }
                    ds.Tables[0].Rows.Add(jan);
                    exp += jan;
                }
                Label13.Text = exp.ToString();
                ds.AcceptChanges();
                DataRow[] SeviceRow = ds.Tables[0].Select();
                this.CollectionChart.Series["collection"].Points.Clear();
                for (int j = 0; j <= 30; j++)
                {
                    this.CollectionChart.Series["collection"].Points.AddXY(j + 1, SeviceRow[j][0]);
                    // this.chart1.Series["Expense"]["PixelPointWidth"] = "15";
                    CollectionChart.ChartAreas[0].AxisX.IsMarginVisible = false;

                }                        
            }
           
        }

        public void bindMember()
        {
            int Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;

            DataSets ds = new DataSets();
            int Enquiryy = 0;
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            DataTable dt1 = objChart.BindMemberDayWise();
            if (dt1 != null)
            {
                DataRow[] ServiceRow2 = dt1.Select();
                for (int j = 1; j <= 31; j++)
                {
                    jan = 0;

                    for (int m = 0; m < ServiceRow2.Length; m++)
                    {
                        if (ServiceRow2[m][0].ToString() != "")
                        {
                            DateTime D1 = Convert.ToDateTime(ServiceRow2[m][0]);
                            if (D1.Month == month && D1.Year == Ye && D1.Day == j)
                            {
                                jan += 1;
                             
                            }
                        }
                    }
                    Enquiryy += jan;
                    ds.Tables["Member"].Rows.Add(jan);                   
                }
                ds.AcceptChanges();
                
               
                DataRow[] SeviceRow = ds.Tables["Member"].Select();
                Label12.Text = Enquiryy.ToString();
                //this.Chart1.Series["Enquiry"].Points.Clear();
                for (int j = 0; j <= 30; j++)
                {
                    this.ChartMember.Series[0].Points.AddXY(j + 1, SeviceRow[j][0]);
                    ChartMember.ChartAreas[0].AxisX.IsMarginVisible = false;
                }         

            }

            //dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            //dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            //int addmis = dash.Addmission();
            //if (addmis > 0)
            //{
            //    Label12.Text = addmis.ToString();
            //}
            ChartMember.Series[0].ChartType = SeriesChartType.Column;        
               
        }

        public void BindChart()
        {
            Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;

            DataSets ds = new DataSets();
            int Enquiryy = 0;
            objChart.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objChart.Comp_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            DataTable dt1 = objChart.BindEnquiryDayWise();
            if (dt1 != null)
            {
                int cnt = Convert.ToInt32(dt1.Rows.Count);

                DataRow[] ServiceRow2 = dt1.Select();
                for (int j = 1; j <= 31; j++)
                {
                    jan = 0;
                    for (int m = 0; m < ServiceRow2.Length; m++)
                    {
                        if (ServiceRow2[m][0].ToString() != "")
                        {
                            DateTime D1 = Convert.ToDateTime(ServiceRow2[m][0]);
                            if (D1.Month == month && D1.Year == Ye && D1.Day == j)
                            {
                                jan += 1;

                            }
                        }
                    }
                    Enquiryy += jan;
                    Label11.Text = Enquiryy.ToString();
                    ds.Tables["Enquiry"].Rows.Add(jan);
                }
                ds.AcceptChanges();             
               
                DataRow[] SeviceRow = ds.Tables["Enquiry"].Select();
                               
                for (int j = 0; j <= 30; j++)
                {
                    this.Chart2.Series[0].Points.AddXY(j + 1, SeviceRow[j][0]);
                    Chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
                }
            
            }
            //dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            //dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            //int ENQ = dash.Enquiry();
            //if (ENQ > 0)
            //{
            //    Label11.Text = ENQ.ToString();
            //}
                Chart2.Series[0].ChartType = SeriesChartType.Column;         
               
        }

        public void BindPresentMember()
        {
            Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;
            DataSet1 ds = new DataSet1();
            int exp = 0;
            int Enquiryy = 0;
            objChart.Company_Id = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"].ToString());
            objChart.Brnach_Id = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"].ToString());
            DataTable dt = objChart.Bindpresentmember();
            if (!(dt is DBNull))
            {
                for (int j = 1; j <= 31; j++)
                {
                    jan = 0;
                    int jans = 0;
                    DataRow[] ServiceRow = dt.Select();
                    for (int i = 0; i < ServiceRow.Length; i++)
                    {
                        DateTime D2 = Convert.ToDateTime(ServiceRow[i][1]);
                        if (D2.Month == month && D2.Year == Ye && D2.Day == j)
                        {
                            jan += 1;

                        }
                    }                 
                    Enquiryy += jan;
                    Label15.Text = Enquiryy.ToString();
                   // ds.Tables[0].Rows.Add(jan);
                    ds.Tables["present"].Rows.Add(jan);
                   
                }
                ds.AcceptChanges();
            

                DataRow[] SeviceRow = ds.Tables["present"].Select();
             
                //this.Chart1.Series["Enquiry"].Points.Clear();
                for (int j = 0; j <= 30; j++)
                {
                    this.Chart1.Series[0].Points.AddXY(j + 1, SeviceRow[j][0]);
                    Chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                }         

            }

        }

        protected void Display_MemberBirth_Cnt()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int membirth = dash.Display_MemberBirth_Cnt();
            if (membirth > 0)
            {
                btnCntMemberBday.Text = membirth.ToString();
            }
            else { btnCntMemberBday.Text = "0"; }
        }

        protected void StaffBirth()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int staffbirth = dash.StaffBirth();
            if (staffbirth > 0)
            {
                btnCntStaffBday.Text = staffbirth.ToString();
            }
            else { btnCntStaffBday.Text = "0"; }
        }

        protected void MemberAnneversary()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int memAnn = dash.MemberAnneversary();
            if (memAnn > 0)
            {

                btnCntAnniversary.Text = memAnn.ToString();
            }
            else { btnCntAnniversary.Text = "0"; }
        }

        protected void enquir()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int ENQ = dash.Enquiry();
            if (ENQ > 0)
            {
                btnCntEnq.Text = ENQ.ToString();
            }
            else { btnCntEnq.Text = "0"; }
        }

        protected void Enq_Flwp()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            //int ENQ = dash.Enquiryflwp();
            //if (ENQ > 0)
            //{

            dt = dash.AllEnquiryflwp();
            if (dt.Rows.Count > 0)
            {
                btnCntEnqFolw.Text = dt.Rows.Count.ToString();
            }
            else { btnCntEnqFolw.Text = "0"; }
        }

        protected void Addmission()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int addmis = dash.Addmission();
            if (addmis > 0)
            {
                btnCntAddmission.Text = addmis.ToString();
            }
            else { btnCntAddmission.Text = "0"; }
        }

        protected void Active()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int Act = dash.Active();
            if (Act > 0)
            {
                btnCntActiveMem.Text = Act.ToString();
            }
            else { btnCntActiveMem.Text = "0"; }
        }

        protected void Deactive()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int DAct = dash.Deactive();
            if (DAct > 0)
            {

                btnCntDeactiveMem.Text = DAct.ToString();
            }
            else { btnCntDeactiveMem.Text = "0"; }
        }

        protected void MemberEnd()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.MemberEnd();
            if (dt.Rows.Count > 0)
            {
                btnCntMemberEnd.Text = dt.Rows.Count.ToString();
            }
            else { btnCntMemberEnd.Text = "0"; }
        }

        protected void Disply_Balance_Cnt()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int balanceCnt;
             dt   = dash.TodayBalancePayment();
            if (dt.Rows.Count > 0)
            {
                balanceCnt = dt.Rows.Count;
                btnCntPaymentDate.Text = balanceCnt.ToString();
            }
            else { btnCntPaymentDate.Text = "0"; }
        }

        protected void Display_PostDatedCheque_Cnt()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            int ExpDateCnt;
            dt = dash.TodayPostDatedCheque();
            if (dt.Rows.Count > 0)
            {
                ExpDateCnt = dt.Rows.Count;
                btnCntPDC.Text = ExpDateCnt.ToString();
            }
            else { btnCntPDC.Text = "0"; }
        }

        protected void OtherFollupCount()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.OtherFollowup();
            if (dt.Rows.Count > 0)
            {
                btnCntOtherFollowp.Text = dt.Rows.Count.ToString();
            }
            else { btnCntOtherFollowp.Text = "0"; }
        }



[System.Web.Services.WebMethod]
        public static GridView checkUserName()
        {
            GridView gv = new GridView();
            BalDashBoard dash = new BalDashBoard();
            string message = "";
            DataTable dt;
            dash.Branch_ID = Branch_AutoID; //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Company_AutoID;
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            { dash.date = todaydate; }

            dt = dash.AllMemberBirth();
            
            if (dt.Rows.Count > 0)
            {
                gv.DataSource = dt;
                gv.DataBind();
            }
            GridView gv1;
            if (dt.Rows.Count > 0)
            {
                 gv1 = gv;
            }
            else
            {
                 gv1 = gv;
            }
            //Return the result
            return gv1;

            //SqlDataAdapter SqlAda;
            //SqlConnection con = getConnection();
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "sproc";
            //cmd.Parameters.Add("@IDNO", SqlDbType.VarChar).Value = idno;
            //cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
            //cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = tel;
            //cmd.Connection = con;
            //try
            //{
            //    con.Open();
            //    gv.EmptyDataText = "No data.";

            //    SqlAda = new SqlDataAdapter(cmd);
            //    ds = new DataSet();

            //    SqlAda.Fill(ds);

            //    gv.DataSource = ds;
            //    gv.DataBind();
            //    message = "success";
            //}
            //catch (Exception ex)
            //{
            //    appObj.myMessageBox(ex.Message);
            //    message = "error";
            //}
            //finally
            //{
            //    con.Close();
            //    con.Dispose();
            //}
            //return message;
        }  

        protected void btnCntMemberBday_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.AllMemberBirth();
            lblnotfNm.InnerText = "Member BirthDay";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = dt;
                GVNotification.DataBind();
                GVNotification.Columns[0].Visible = true;

            }
            else
            {
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = null;
                GVNotification.DataBind();
              
            }
            bindAllCharts();
        }

        protected void btnCntStaffBday_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.ALLStaffBirth();
            lblnotfNm.InnerText = "Staff BirthDay";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = dt;
                GVNotification.DataBind();
                GVNotification.Columns[0].Visible = false;
                
            }
            else
            {
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = null;
                GVNotification.DataBind();
            }
            bindAllCharts();
        }

        protected void btnCntAnniversary_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.AllMemberAnneversary();
            lblnotfNm.InnerText = "Member Anniversary";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = dt;
                GVNotification.DataBind();
                GVNotification.Columns[0].Visible = true;
            }
            else
            {
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = null;
                GVNotification.DataBind();
            }
            bindAllCharts();
        }

        protected void btnCntEnq_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.AllEnquiry();
            lblnotfNm.InnerText = "Today's Enquiry";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GridFollowup.Visible = false;
                GVNotification.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                gvEnquiry.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                gvEnquiry.DataSource = dt;
                gvEnquiry.DataBind();
            }
            else
            {
                GVNotification.Visible = false;
                gvEnquiry.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                gvEnquiry.DataSource = null;
                gvEnquiry.DataBind();
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvEnquiry.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvEnquiry.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvEnquiry.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvEnquiry.Columns[0].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvEnquiry.Columns[0].Visible = false;
            }

            bindAllCharts();
        }

        protected void btnCntEnqFolw_Click(object sender, EventArgs e)
        {
            EnqFollowupClick();
        }

        public void EnqFollowupClick()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.AllEnquiryflwp();
            lblnotfNm.InnerText = "Enquiry Followup";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                gvmembershipEnd.Visible = false;
                gvEnquiry.Visible = false;
                GVNotification.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GridFollowup.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GridFollowup.DataSource = dt;
                GridFollowup.DataBind();
            }
            else
            {
                GVNotification.Visible = false;
                tblNotfication.Visible = false;
                divNotification.Visible = false;
            }
            bindAllCharts();
        }

        protected void btnCntAddmission_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.AllAddmission();
            lblnotfNm.InnerText = "Today's Admission";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                gvmembershipEnd.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = dt;
                GVNotification.DataBind();
                GVNotification.Columns[0].Visible = true;
            }
            else
            {
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = null;
                GVNotification.DataBind();
            }
            bindAllCharts();
        }

        protected void btnCntActiveMem_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.AllActive();
            lblnotfNm.InnerText = "Active Members";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                gvmembershipEnd.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = dt;
                GVNotification.DataBind();
                GVNotification.Columns[0].Visible = true;
            }
            else
            {
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = null;
                GVNotification.DataBind();
            }
            bindAllCharts();
        }

        protected void btnCntDeactiveMem_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.AllDeactive();
            lblnotfNm.InnerText = "Deactive Members";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                gvmembershipEnd.Visible = false;
                divNotification.Visible = true;
                GVNotification.DataSource = dt;
                GVNotification.DataBind();
                GVNotification.Columns[0].Visible = true;
            }
            else
            {
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = null;
                GVNotification.DataBind();
            }
            bindAllCharts();
        }

        protected void btnCntMemberEnd_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.MemberEnd();
            lblnotfNm.InnerText = "Membership End Date";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = dt;
                GVNotification.DataBind();
                GVNotification.Columns[0].Visible = true;
                gvmembershipEnd.Visible = false;
            }
            else
            {
                GVNotification.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GVNotification.DataSource = null;
                GVNotification.DataBind();
            }
            bindAllCharts();
        }

        protected void btnCntPaymentDate_Click(object sender, EventArgs e)
        {
            paymentClick();
        }

        public void paymentClick()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.TodayBalancePayment();
            lblnotfNm.InnerText = "Today's Payment";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GVNotification.Visible = false;
                gvEnquiry.Visible = false;
                gvEnquiry.Visible = false;
                GVNotification.Visible = false;
                GridFollowup.Visible = true;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GridFollowup.DataSource = dt;
                GridFollowup.DataBind();
                gvmembershipEnd.Visible = false;
            }
            else
            {
                GVNotification.Visible = false;
                tblNotfication.Visible = false;
                divNotification.Visible = false;
            }
            bindAllCharts();
        }

        protected void btnCntPDC_Click(object sender, EventArgs e)
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.TodayPostDatedCheque();
            lblnotfNm.InnerText = "Today's Cheque";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                gvmembershipEnd.Visible = false;
                GVNotification.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = true;
                tblNotfication.Visible = true;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                divNotification.Visible = true;
                gvEnquiry.DataSource = dt;
                gvEnquiry.DataBind();
            }
            else
            {
                GVNotification.Visible = false;
                gvEnquiry.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                gvEnquiry.DataSource = null;
                gvEnquiry.DataBind();
                
            }
            gvEnquiry.Columns[0].Visible = false;
            bindAllCharts();
        }

        protected void btnCntOtherFollowp_Click(object sender, EventArgs e)
        {
            OtherFollowupClick();
        }

        public void OtherFollowupClick()
        {
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);

            dt = dash.OtherFollowup();
            lblnotfNm.InnerText = "Other Followup";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GVNotification.Visible = false;
                gvEnquiry.Visible = false;
                GVNotification.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                gvmembershipEnd.Visible = false;
                GridFollowup.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                GridFollowup.DataSource = dt;
                GridFollowup.DataBind();

            }
            else
            {
                GVNotification.Visible = false;
                tblNotfication.Visible = false;
                divNotification.Visible = false;

            }
            bindAllCharts();
        }

        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            if (lblnotfNm.InnerText == "Enquiry Followup")
            {
                //Response.Redirect("EnquiryFollowup.aspx?Data=" + HttpUtility.UrlEncode(e.CommandArgument.ToString()));
                int EnqID1 = Convert.ToInt32((e.CommandArgument.ToString()));
                string strPopup = "<script language='javascript' ID='script1'>"
                + "window.open('EnquiryFollowup.aspx?Data=" + EnqID1
                + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=950,height=650')"
                + "</script>";
                ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
            else if (lblnotfNm.InnerText == "Other Followup")
            {
                int memautoid = 0;
                //Response.Redirect("Followup.aspx?FID=" + HttpUtility.UrlEncode(e.CommandArgument.ToString()));
                int ID = Convert.ToInt32((e.CommandArgument.ToString()));
                objMember.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objMember.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                objMember.Member_ID1 = Convert.ToInt32(e.CommandArgument.ToString());
                dt = objMember.Get_MemberAutoID_ByMemberID1();
                if (dt.Rows.Count > 0)
                {
                    memautoid = Convert.ToInt32(dt.Rows[0]["Member_AutoID"]);
                }
                string strPopup = "<script language='javascript' ID='script1'>"
                + "window.open('Followup.aspx?Other_Member_AutoID=" + memautoid
                + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=950,height=650')"
                + "</script>";
                ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
            else if (lblnotfNm.InnerText == "Today's Payment")
            {
                int MemberID1 = Convert.ToInt32(e.CommandArgument.ToString());
                GetMemberAutoID_By_ID1(MemberID1);
                int Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
                //Response.Redirect("Followup.aspx?BalancePayment_Member_AutoID=" + Member_AutoID);
                int ID = Convert.ToInt32((e.CommandArgument.ToString()));
                string strPopup = "<script language='javascript' ID='script1'>"
                + "window.open('Followup.aspx?BalancePayment_Member_AutoID=" + Member_AutoID
                + "','_blank', 'toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=950,height=650')"
                + "</script>";
                ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            }
        }
        
        protected void btnAdd_Command1(object sender, CommandEventArgs e)
        {
            dash.MemberId1=Convert.ToInt32(HttpUtility.UrlEncode(e.CommandArgument.ToString()));
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = dash.AutoID();
            if (dt.Rows.Count > 0)
            {
                int MemAutoid=Convert.ToInt32( dt.Rows[0]["Member_AutoID"]);
                Response.Redirect("MemberProfile.aspx?MemberId=" + MemAutoid.ToString());
            }

            //Response.Redirect("MemberProfile.aspx?MemID=" + HttpUtility.UrlEncode(e.CommandArgument.ToString()));
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            tblNotfication.Visible = false;
            bindAllCharts();
        }

        public void bindAllCharts()
        {
            BindChart();
            bindMember();
            bindMemberEnd();
            BindCollection();
            BindPresentMember();
        }

        protected void RepterDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string status = "";
            HiddenField hf = e.Item.FindControl("HiddenField1") as HiddenField;
            if (hf != null)
            {
                string val = hf.Value;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("imgStaff") as System.Web.UI.WebControls.Image;
                img.ImageUrl = val;
            }
        }

        string FNameDashboard = "Dashboard";
        protected void btnDetails_Command(object sender, CommandEventArgs e)
        {
            dash.Enq_ID1 = Convert.ToInt32(HttpUtility.UrlEncode(e.CommandArgument.ToString()));
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            dt = dash.getEnqAutoID();
            if (dt.Rows.Count > 0)
            {
                int EnqAuto_ID = Convert.ToInt32(dt.Rows[0]["Enq_AutoID"]);
                Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + EnqAuto_ID.ToString() + " &FNameDashboard=" + HttpUtility.UrlEncode(FNameDashboard.ToString()));
            }

            //Response.Redirect("MemberProfile.aspx?EnqAuto_ID=" + HttpUtility.UrlEncode(e.CommandArgument.ToString()));
        }

        protected void Chart2_Click(object sender, ImageMapEventArgs e)
        {
            int day;
            Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            day =  Convert.ToInt32(e.PostBackValue);           
            DateTime date2;
            string todayDate = day + "-" + month + "-" + Ye;
            DateTime date = Convert.ToDateTime(todayDate);
            if (DateTime.TryParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date2))
            {
                dash.date = date2;
            }
            dash.Action = "GetEnquiryInfo";
            dash.Category = "Information";
             dt = dash.GetInfo();
           
             lblnotfNm.InnerText = "Enquiry";
             if (dt.Rows.Count > 0)
             {
                 gvpresent.Visible = false;
                 gvmembershipEnd.Visible = false;
                 GridFollowup.Visible = false;
                 GVNotification.Visible = false;
                 gvChartInfo.Visible = false;
                 gvCollectionInfo.Visible = false;
                 gvEnquiry.Visible = true;
                 tblNotfication.Visible = true;
                 divNotification.Visible = true;
                 gvEnquiry.DataSource = dt;
                 gvEnquiry.DataBind();
               
             }
             else
             {

             }
             bindAllCharts();
        }

        protected void ChartMember_Click1(object sender, ImageMapEventArgs e)
        {
            int day;
            Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            day = Convert.ToInt32(e.PostBackValue);
          
            DateTime date2;
            string todayDate=day + "-" + month + "-" + Ye;
            DateTime date = Convert.ToDateTime(todayDate);
            if (DateTime.TryParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date2))
            {
                dash.date = date2;
            }

            dash.Action = "GetMemberInfo";
            dash.Category = "Information";
            dt = dash.GetInfo();
            lblnotfNm.InnerText = "Members";

            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                gvmembershipEnd.Visible = false;
                GridFollowup.Visible = false;
                gvEnquiry.Visible = false;
                gvChartInfo.Visible = true;
                GVNotification.Visible = false;
                gvCollectionInfo.Visible = false;
                gvChartInfo.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                gvChartInfo.DataSource = dt;
                gvChartInfo.DataBind();          
           }
            else
            {

            }
            bindAllCharts();
        }

        protected void CollectionChart_Click(object sender, ImageMapEventArgs e)
        {
            int day;
            Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            day = Convert.ToInt32(e.PostBackValue);
            DateTime date2;
            string todayDate = day + "-" + month + "-" + Ye;
            DateTime date = Convert.ToDateTime(todayDate);
            if (DateTime.TryParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date2))
            {
                dash.date = date2;
            }
            dash.Action = "GetCollectionInfo";
            dash.Category = "Information";
            dt = dash.GetInfo();

            lblnotfNm.InnerText = "Collection";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                gvmembershipEnd.Visible = false;
                gvEnquiry.Visible = false;
                GridFollowup.Visible = false;
                GVNotification.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;

                gvCollectionInfo.DataSource = dt;
                gvCollectionInfo.DataBind();

            }
            else
            {

            }
            bindAllCharts();
        }

        protected void ChartMemberEnd_Click(object sender, ImageMapEventArgs e)
        {
            int day;
            Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            day = Convert.ToInt32(e.PostBackValue);
            
            string todayDate = day + "-" + month + "-" + Ye;
            DateTime date3;

            DateTime date = Convert.ToDateTime(todayDate);

            if (DateTime.TryParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date3))
            {
                dash.date = date3;
            }
            dash.Action = "GetMemberEndInfo";
            dash.Category = "Information";
            dt = dash.GetInfo();

            lblnotfNm.InnerText = "Membeship End Date";
            if (dt.Rows.Count > 0)
            {
                gvpresent.Visible = false;
                GridFollowup.Visible = false;
                GVNotification.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                gvmembershipEnd.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                gvmembershipEnd.DataSource = dt;
                gvmembershipEnd.DataBind();

            }
            else
            {

            }
            bindAllCharts(); 
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            int day;
            Ye = DateTime.UtcNow.Year;
            month = DateTime.UtcNow.Month;
            dash.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dash.Comp_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            day = Convert.ToInt32(e.PostBackValue);

            string todayDate = day + "-" + month + "-" + Ye;
            DateTime date3;

            DateTime date = Convert.ToDateTime(todayDate);

            if (DateTime.TryParseExact(date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date3))
            {
                dash.date = date3;
            }
            dash.Action = "GetPresentMember";
            dash.Category = "Information";
            dt = dash.GetInfo();

            lblnotfNm.InnerText = "Present Member";
            if (dt.Rows.Count > 0)
            {
                GridFollowup.Visible = false;
                GVNotification.Visible = false;
                gvChartInfo.Visible = false;
                gvCollectionInfo.Visible = false;
                gvmembershipEnd.Visible = false;
                gvpresent.Visible = true;
                tblNotfication.Visible = true;
                divNotification.Visible = true;
                gvpresent.DataSource = dt;
                gvpresent.DataBind();

            }
            else
            {


            }
            bindAllCharts(); 
        }

        #region -------------------grid select method-------------------------
        int MemberID1;
        int EnqID2;
        string BalancePayment_Member_AutoID = "";
        string Other_Member_AutoID = "";
        string MembershipEnd_Member_AutoID = "";
        string Enq_AutoID = "";
        protected void GridFollowup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            GridViewRow row = GridFollowup.SelectedRow;
            Label lbl = (Label)row.FindControl("Label1");
            if (lblnotfNm.InnerText == "Enquiry Followup")
            {
                flagpopup = 2;
                Label34_ModalPopupExtender1.Show();
                bindDDLFollowupTypeEnq();
                bindDDLExecutiveEnq();
                setExecutiveEnq();
                bindDDLCallRespondEnq();
                EnqID2 = Convert.ToInt32(lbl.Text);
                GetEnqAutoID_By_ID1(EnqID2);
            }
            else
            {
                flagpopup = 1;
                Label33_ModalPopupExtender1.Show();
                MemberID1 = Convert.ToInt32(lbl.Text);
                GetMemberAutoID_By_ID1(MemberID1);
                bindDDLCallRespond();
                BindFollowupTypeDDL();  // Bind Followup Type Drop Down List       
                bindDDLExecutive();    // Assign Executive Name
                setExecutive();
                AssignDateAndTime();
            }
            
            
            
            
                if (lblnotfNm.InnerText == "Enquiry Followup")
                {
                    Enq_AutoID = ViewState["EnqAutoID"].ToString();
                }
                else if (lblnotfNm.InnerText == "Other Followup")
                {
                    Other_Member_AutoID = ViewState["MemAutoID"].ToString();
                }
                else if (lblnotfNm.InnerText == "Today's Payment")
                {
                    BalancePayment_Member_AutoID = ViewState["MemAutoID"].ToString();
                }
                if (Request.QueryString["Member_ID"] != null)
                {
                    int memberid = Convert.ToInt32(Request.QueryString["Member_ID"]);
                    GetMemberDetails(memberid);
                }

                if (BalancePayment_Member_AutoID != "")
                {
                    bindDDLFollowupPayment();
                    int Member_AutoID = Convert.ToInt32(BalancePayment_Member_AutoID);
                    objFollowup.Member_AutoID = Member_AutoID;
                    ddlFollowupType.Enabled = false;
                    GetMemberDetails(Member_AutoID);
                    obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(BalancePayment_Member_AutoID);
                        BindGridByFollowupType();
                }
                if (Other_Member_AutoID != "")
                {

                    BindFollowupTypeDDL();
                    int Member_AutoID = Convert.ToInt32(Other_Member_AutoID);
                    objFollowup.Member_AutoID = Member_AutoID;
                    GetMemberDetails(Member_AutoID);
                    obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Other_Member_AutoID);
                    BindGridByFollowupType();
                }
                if (Enq_AutoID != "")
                {
                    int enqid = Convert.ToInt32(ViewState["EnqAutoID"]);
                    GetMemberDetailsEnq(enqid);
                    lblFollwupDateEnq.Text = DateTime.Now.ToString("HH:MM tt");
                    objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                    objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                    objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
                    AssignDateAndTimeEnq();
                    //EnquiryInfo();
                    ddlCallResponseEnq.Focus();
                    objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
                    objEnqFlw.Action = "BindDetailsByID";
                    GetMemberDetailsByFollAutoIDEnq(enqid);
                    gridBindEnq();
                }
        }
        #endregion

        #region--------------------------Followup----------------------------


        public void BindGridByFollowupType()
        {
            obBalViewBalancePaymentFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            obBalViewBalancePaymentFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            if (ddlFollowupType.SelectedValue != "--Select--")
                obBalViewBalancePaymentFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            else
                obBalViewBalancePaymentFollowup.FollowupType_AutoID = 0;
            // obBalViewBalancePaymentFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            //obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
            dt = obBalViewBalancePaymentFollowup.SelectFollDetails_By_MemAutoID();
            if (dt.Rows.Count > 0)
            {
                gvFollowupDetails.DataSource = dt;
                gvFollowupDetails.DataBind();
            }
            else
            {
                gvFollowupDetails.DataSource = null;
                gvFollowupDetails.DataBind();
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvFollowupDetails.Columns[0].Visible = false;
                gvFollowupDetails.Columns[1].Visible = false;
            }
        }

        public void bindDDLCallRespond()
        {
            try
            {
                obBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalCallRespondMaster.Select_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlCallPesponse.DataSource = dt;
                    ddlCallPesponse.DataValueField = "CallRespond_AutoID";
                    ddlCallPesponse.DataTextField = "Name";
                    ddlCallPesponse.DataBind();
                    ddlCallPesponse.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Call Respond Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AssignDateAndTime()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDate.Text = todaydate.ToString("dd-MM-yyyy");

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                txtNextFollowupTime.Text = localTime.ToString("HH:mm");
            }
        }

        public void bindDDLFollowupPayment()
        {
            try
            {
                objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objFollowup.SELECT_FollowupType_Payment();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupType.DataSource = dt;
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetMemberDetails(int memberid)
        {
            txtMemberID.Enabled = false;
            txtContact.Enabled = false;
            objMemberDetails.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objMemberDetails.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objMemberDetails.Member_AutoID = Convert.ToInt32(memberid);
            dt = objMemberDetails.SelectByID_MemberInformation();
            if (dt.Rows.Count > 0)
            {
                txtMemberID.Text = dt.Rows[0]["Member_ID1"].ToString();
                txtFirst.Text = dt.Rows[0]["FName"].ToString();
                txtLast.Text = dt.Rows[0]["LName"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtContact.Text = dt.Rows[0]["Contact1"].ToString();
                txtmail.Text = dt.Rows[0]["Email"].ToString();
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

        #region ------Bind Followup Type --------
        private void BindFollowupTypeDDL()
        {
            try
            {
                AssignID();
                objFollowup.Action = "Select_FollowupType";
                dataTable = objFollowup.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    ddlFollowupType.DataSource = dataTable;
                    ddlFollowupType.Items.Clear();
                    ddlFollowupType.DataValueField = "FollowupType_AutoID";
                    ddlFollowupType.DataTextField = "Name";
                    ddlFollowupType.DataBind();
                    ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion

        #region -------- Assign Company and Branch ID ------------
        private void AssignID()
        {
            objFollowup.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objFollowup.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objFollowup.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }
        #endregion

        #region ------ Search Member Details By Member ID --------
        protected void txtMemberID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberID.Text != string.Empty)
                {
                    AssignID();
                    objFollowup.Member_ID = Convert.ToInt32(txtMemberID.Text.Trim());
                    objFollowup.Action = "SearchByMemberID";

                    dataTable = objFollowup.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberIdNotFound();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    }
                    txtMemberID.Focus();
                }
            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion

        #region  ------ Clear Fields When Search Member Details By Member ID --------
        private void ClearFieldMemberIdNotFound()
        {
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtContact.Text = string.Empty;
            txtmail.Text = string.Empty;
            MemberAutoID = 0;
        }
        #endregion

        #region ------ Search Member Details By Contact Number --------
        protected void txtContact_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContact.Text != string.Empty)
                {
                    AssignID();
                    objFollowup.Contact = txtContact.Text;
                    objFollowup.Action = "SearchByContact";

                    dataTable = objFollowup.GetDetails();
                    if (dataTable.Rows.Count > 0)
                    {
                        MemberAutoID = Convert.ToInt32(dataTable.Rows[0]["Member_AutoID"].ToString());
                        txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                        txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                        txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                        ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                        txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        ClearFieldMemberContNotFound();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record  Not Found !!!','Error');", true);
                    }
                    txtContact.Focus();
                }
            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);

            }
        }
        #endregion

        #region ------ Clear Fields When Search Member Details By Contact Number --------
        private void ClearFieldMemberContNotFound()
        {
            txtMemberID.Text = string.Empty;
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtmail.Text = string.Empty;
            MemberAutoID = 0;
        }
        #endregion

        #region --------- Save Button --------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtMemberID.Text == string.Empty || txtContact.Text == string.Empty || ddlFollowupType.SelectedValue == "--Select--" || ddlCallPesponse.SelectedValue == "--Select--" || ddlRating.SelectedValue == "--Select--"
                    || txtNextFollowupDate.Text == string.Empty || txtNextFollowupTime.Text == string.Empty || txtComment.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Enter All Fields','Error');", true);

                    if (txtMemberID.Text == "")
                    { txtMemberID.Style.Add("border", "1px solid red "); }

                    if (txtContact.Text == "")
                    { txtContact.Style.Add("border", "1px solid red "); }

                    if (ddlFollowupType.SelectedValue == "--Select--")
                    { ddlFollowupType.Style.Add("border", "1px solid red "); }

                    if (ddlCallPesponse.SelectedValue == "--Select--")
                    { ddlCallPesponse.Style.Add("border", "1px solid red "); }

                    if (ddlRating.SelectedValue == "--Select--")
                    { ddlRating.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupDate.Text == "")
                    { txtNextFollowupDate.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupTime.Text == "")
                    { txtNextFollowupTime.Style.Add("border", "1px solid red "); }

                    if (txtComment.Text == "")
                    { txtComment.Style.Add("border", "1px solid red "); }
                }
                else
                {
                    txtMemberID.Style.Add("border", "1px solid silver  ");
                    txtContact.Style.Add("border", "1px solid silver  ");
                    ddlFollowupType.Style.Add("border", "1px solid silver  ");
                    //txtExecutive.Style.Add("border", "1px solid silver  ");
                    ddlCallPesponse.Style.Add("border", "1px solid silver  ");
                    ddlRating.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
                    txtComment.Style.Add("border", "1px solid silver  ");
                    AssignID();
                    AddParameters();

                    if (btnSave.Text == "Save")
                    {
                        objFollowup.Action = "INSERT";
                        res = objFollowup.Insert_FollowupInformation();

                        if (res > 0)
                        {
                            ClearAllField();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                            flagpopup = 0;
                            Label33_ModalPopupExtender1.Hide();
                            if (lblnotfNm.InnerText == "Today's Payment")
                            {
                                Disply_Balance_Cnt();
                                paymentClick();
                            }
                            if (lblnotfNm.InnerText == "Other Followup")
                            {
                                OtherFollupCount();
                                OtherFollowupClick();
                            }
                        }
                        
                    }
                    else if (btnSave.Text == "Update")
                    {
                        objFollowup.Action = "Update";
                        objFollowup.Followup_AutoID = Convert.ToInt32(ViewState["Followup_AutoID"]);
                        res = objFollowup.Insert_FollowupInformation();
                        if (res > 0)
                        {
                            btnSave.Text = "Save";
                            txtMemberID.Enabled = true;
                            txtContact.Enabled = true;
                            ClearAllField();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                            flagpopup = 0;
                            Label33_ModalPopupExtender1.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        private void AddParameters()
        {
            //if (Request.QueryString["BalancePayment_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
            //else if (Request.QueryString["MembershipEnd_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
            //else if (Request.QueryString["Upgrade_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Upgrade_Member_AutoID"]);
            //else if (Request.QueryString["Measurement_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Measurement_Member_AutoID"]);
            //else if (Request.QueryString["Other_Member_AutoID"] != null)
            //    objFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
            //else
            objFollowup.Member_AutoID = Convert.ToInt32(ViewState["MemAutoID"]);
            objFollowup.FollowupType_AutoID = Convert.ToInt32(ddlFollowupType.SelectedValue);
            objFollowup.CallRespond_AutoID = ddlCallPesponse.SelectedValue;
            objFollowup.Rating = ddlRating.SelectedValue;


            DateTime NFDate;
            if (DateTime.TryParseExact(txtNextFollowupDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate))
            {
            }
            if (ddlRatingEnq.SelectedValue != "Not Interested")
                objFollowup.NextFollowupDate = NFDate;
            else
                objFollowup.NextFollowupDate = null;

            DateTime FDate;
            if (DateTime.TryParseExact(lblFollowupDateTime.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FDate))
            {
            }
            objFollowup.FollowupDate = FDate;

            objFollowup.NextFollowupTime = Convert.ToDateTime(txtNextFollowupTime.Text.ToString());
            objFollowup.FollowupTime = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt"));
            objFollowup.Comment = Regex.Replace(txtComment.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objFollowup.Executive_ID = Convert.ToInt32(ddlExecutive.SelectedValue);

        }

        private void ClearAllField()
        {
            chkExecutive.Checked = true;
            ddlExecutive.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutive.Enabled = false;
            ddlFollowupType.SelectedIndex = 0;
            ddlCallPesponse.SelectedIndex = 0;
            ddlRating.SelectedIndex = 0;
            AssignDateAndTime();
            txtComment.Text = string.Empty;
        }
        #endregion

        #region ------------ Bind GridView -----------------
        private void BindGridViewDetails()
        {
            AssignID();

            //dataTable.Clear();
            dataTable = objFollowup.GetDetails();
            if (dataTable.Rows.Count > 0)
            {
                gvFollowupDetails.DataSource = dataTable;
                gvFollowupDetails.DataBind();
            }
            else
            {
                gvFollowupDetails.DataSource = dataTable;
                gvFollowupDetails.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvFollowupDetails.Columns[0].Visible = true;
                gvFollowupDetails.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvFollowupDetails.Columns[0].Visible = false;
                gvFollowupDetails.Columns[1].Visible = false;
            }
        }
        #endregion

        #region --------- Delete By Followup ID --------
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();
                objFollowup.Followup_AutoID = Convert.ToInt32(e.CommandArgument);
                objFollowup.Action = "DeleteByFollowupAutoID";
                int i = objFollowup.Insert_FollowupInformation();
                if (i > 0)
                {
                    //BindGridViewDetails();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    if (Request.QueryString["FNBalPayFollDetail"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["BalancePayment_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                    if (Request.QueryString["FNMemEndFollDetail"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["MembershipEnd_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                    if (Request.QueryString["FNameMemProfile"] != null)
                    {
                        obBalViewBalancePaymentFollowup.Member_AutoID = Convert.ToInt32(Request.QueryString["Other_Member_AutoID"]);
                        BindGridByFollowupType();
                    }
                }
                else if (i == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }
        #endregion

        #region --------- Edit By Followup ID  --------
        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignID();
                objFollowup.Followup_AutoID = Convert.ToInt32(e.CommandArgument);
                objFollowup.Action = "GetDetailsByFollowupAutoID";

                dataTable = objFollowup.GetDetails();
                if (dataTable.Rows.Count >= 0)
                {
                    btnSave.Text = "Update";
                    txtMemberID.Enabled = false;
                    txtContact.Enabled = false;
                    ViewState["Followup_AutoID"] = dataTable.Rows[0]["Followup_AutoID"].ToString();
                    txtMemberID.Text = dataTable.Rows[0]["Member_ID1"].ToString();
                    txtFirst.Text = dataTable.Rows[0]["FName"].ToString();
                    txtLast.Text = dataTable.Rows[0]["LName"].ToString();
                    ddlGender.SelectedValue = dataTable.Rows[0]["Gender"].ToString();
                    txtContact.Text = dataTable.Rows[0]["Contact1"].ToString();
                    txtmail.Text = dataTable.Rows[0]["Email"].ToString();
                    ddlFollowupType.SelectedValue = dataTable.Rows[0]["FollowupType_AutoID"].ToString();
                    ddlCallPesponse.SelectedValue = dataTable.Rows[0]["CallRespond_AutoID"].ToString();
                    ddlRating.SelectedValue = dataTable.Rows[0]["Rating"].ToString();
                    ddlExecutive.SelectedValue = dataTable.Rows[0]["Executive_ID"].ToString();
                    if (dataTable.Rows[0]["NextFollowupDate"].ToString() != "")
                    {
                        DateTime NFDate = Convert.ToDateTime(dataTable.Rows[0]["NextFollowupDate"].ToString());
                        DateTime NFDate1;
                        if (DateTime.TryParseExact(NFDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate1))
                        {
                            txtNextFollowupDate.Text = NFDate1.ToString("dd-MM-yyyy");
                        }
                    }
                    else
                        txtNextFollowupDate.Text = "";

                    //string NextFollowupDate = Convert.ToDateTime(dataTable.Rows[0]["NextFollowupDate"]).ToString("dd-MM-yyyy");
                    //txtNextFollowupDate.Text = NextFollowupDate;
                    txtNextFollowupTime.Text = dataTable.Rows[0]["NextFollowupTime"].ToString();
                    DateTime todaydate;
                    if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
                    {
                        lblFollowupDateTime.Text = todaydate.ToString("dd-MM-yyyy");
                    }
                    txtComment.Text = dataTable.Rows[0]["Comment"].ToString();
                }

            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }

        }
        #endregion

        #region ------------ Clear Button ------------
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllField();
            txtMemberID.Style.Add("border", "1px solid silver  ");
            txtContact.Style.Add("border", "1px solid silver  ");
            ddlFollowupType.Style.Add("border", "1px solid silver  ");
            //txtExecutive.Style.Add("border", "1px solid silver  ");
            ddlCallPesponse.Style.Add("border", "1px solid silver  ");
            ddlRating.Style.Add("border", "1px solid silver  ");
            txtNextFollowupDate.Style.Add("border", "1px solid silver  ");
            txtNextFollowupTime.Style.Add("border", "1px solid silver  ");
            txtComment.Style.Add("border", "1px solid silver  ");
        }
        #endregion

        protected void gvFollowupDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFollowupDetails.PageIndex = e.NewPageIndex;
            BindGridViewDetails();
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked == true)
                ddlExecutive.Enabled = false;
            else
                ddlExecutive.Enabled = true;
        }

        protected void ddlRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRating.SelectedValue == "Not Interested")
                txtNextFollowupDate.Enabled = false;
            else
                txtNextFollowupDate.Enabled = true;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
             flagpopup = 0;
             Label33_ModalPopupExtender1.Hide();
        }
        #endregion

        #region--------------------------Enquiry Followup----------------------------
        public void GetMemberDetailsByFollAutoIDEnq(int enqid)
        {
            objBalEnquiry.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalEnquiry.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalEnquiry.Enq_FollAutoID = Convert.ToInt32(enqid);
            dt = objBalEnquiry.GetMemberDetailsByFollAutoID();
            if (dt.Rows.Count > 0)
            {
                lblNameEnq.Text = dt.Rows[0]["FName"].ToString();
                lblContactEnq.Text = dt.Rows[0]["Contact1"].ToString();
                ViewState["enqautoid"] = dt.Rows[0]["Enq_AutoID"].ToString();

                if (dt.Rows[0]["DOB"].ToString() != "")
                {
                    DateTime dobdate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                    DateTime dobdate1;
                    if (DateTime.TryParseExact(dobdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dobdate1))
                    {
                        lbldOBEnq.Text = dobdate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    lbldOBEnq.Text = "";

                lblGenderEnq.Text = dt.Rows[0]["Gender"].ToString();
            }
        }

        public void GetMemberDetailsEnq(int enqid)
        {
            objBalEnquiry.Company_ID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objBalEnquiry.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objBalEnquiry.Enq_ID = Convert.ToInt32(enqid);
            dt = objBalEnquiry.Get_Edit();
            if (dt.Rows.Count > 0)
            {
                lblNameEnq.Text = dt.Rows[0]["FName"].ToString();
                lblContactEnq.Text = dt.Rows[0]["Contact1"].ToString();

                if (dt.Rows[0]["DOB"].ToString() != "")
                {
                    DateTime dobdate = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                    DateTime dobdate1;
                    if (DateTime.TryParseExact(dobdate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dobdate1))
                    {
                        lbldOBEnq.Text = dobdate1.ToString("dd-MM-yyyy");
                    }
                }
                else
                    lbldOBEnq.Text = "";

                lblGenderEnq.Text = dt.Rows[0]["Gender"].ToString();
            }
        }

        public void bindDDLCallRespondEnq()
        {
            try
            {
                obBalCallRespondMaster.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                obBalCallRespondMaster.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = obBalCallRespondMaster.Select_CallRespondMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlCallResponseEnq.DataSource = dt;
                    ddlCallResponseEnq.DataValueField = "CallRespond_AutoID";
                    ddlCallResponseEnq.DataTextField = "Name";
                    ddlCallResponseEnq.DataBind();
                    ddlCallResponseEnq.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Call Respond Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLFollowupTypeEnq()
        {
            try
            {
                objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
                objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
                dt = objEnqFlw.Select_FollowupTypeMaster();
                if (dt.Rows.Count > 0)
                {
                    ddlFollowupTypeEnq.DataSource = dt;
                    ddlFollowupTypeEnq.DataValueField = "FollowupType_AutoID";
                    ddlFollowupTypeEnq.DataTextField = "Name";
                    ddlFollowupTypeEnq.DataBind();
                    //ddlFollowupType.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Followup Type Master !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void bindDDLExecutiveEnq()
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
                    ddlExecutiveEnq.DataSource = dt;
                    ddlExecutiveEnq.Items.Clear();
                    ddlExecutiveEnq.DataValueField = "Staff_AutoID";
                    ddlExecutiveEnq.DataTextField = "Name";
                    ddlExecutiveEnq.DataBind();
                    //ddlExecutive.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    ddlExecutiveEnq.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('First Insert Staff !!!','Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void setExecutiveEnq()
        {
            obBalStaffRegistration.Staff_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Staff_AutoID"]);
            obBalStaffRegistration.Branch_ID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            dt = obBalStaffRegistration.GetExecutiveByID_ByBranch();
            if (dt.Rows.Count > 0)
            {
                ddlExecutiveEnq.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
                ddlExecutiveEnq.SelectedItem.Text = dt.Rows[0]["Name"].ToString();
            }
            else
            {
                dt = obBalStaffRegistration.GetExecutiveByID_WithoutBranch();
                string staffid = dt.Rows[0]["Staff_AutoID"].ToString();
                string staffnm = dt.Rows[0]["Name"].ToString();
                ddlExecutiveEnq.Items.Insert(0, new ListItem(staffnm, staffid));
                ddlExecutiveEnq.SelectedItem.Text = staffnm;
                ddlExecutiveEnq.SelectedValue = staffid;
            }
        }

        public void AssignDateAndTimeEnq()
        {
            DateTime todaydate;
            if (DateTime.TryParseExact(DateTime.UtcNow.AddHours(5.5).ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out todaydate))
            {
                lblFollwupDateEnq.Text = todaydate.ToString("dd-MM-yyyy");
                txtNextFollowupDateEnq.Text = todaydate.ToString("dd-MM-yyyy");

                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
                txtNextFollowupTimeEnq.Text = localTime.ToString("HH:mm");
            }
        }

        private void AssignIDEnq()
        {
            objEnqFlw.Branch_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym1"]["brIDHome"]); //Convert.ToInt32(Session["Branch_ID"]);
            objEnqFlw.Company_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Company_ID"]);
            objEnqFlw.Login_AutoID = Convert.ToInt32(Request.Cookies["OnlineGym"]["Login_ID"]);
        }

        private void ClearAllFieldEnq()
        {
            AssignDateAndTime();
            chkExecutiveEnq.Checked = true;
            ddlExecutiveEnq.SelectedValue = Request.Cookies["OnlineGym"]["Staff_AutoID"];
            ddlExecutiveEnq.Enabled = false;
            ddlCallResponseEnq.SelectedValue = "--Select--";
            //ddlCallPesponse.SelectedItem.Text = "--Select--";
            ddlRatingEnq.SelectedIndex = 0;
            //txtNextFollowupTime.Text = string.Empty;
            txtCommentEnq.Text = string.Empty;
            bindDDLFollowupTypeEnq();

        }

        private void AddParametersEnq()
        {
            //if (Request.QueryString["FNameViewEnqFoll"] != null)
            //{
            //    objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["enqautoid"]);
            //}
            //else if (Request.QueryString["Data"] != null)
            //{
            //    objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["Auto_ID"]);
            //}
            //else if (Request.QueryString["Enq_ID"] != null)
            //{
            objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["EnqAutoID"]);
            //}
            objEnqFlw.CallResponse_AutoID = Convert.ToInt32(ddlCallResponseEnq.SelectedValue);
            objEnqFlw.Rating = ddlRatingEnq.SelectedValue;
            if (ddlRatingEnq.SelectedValue != "Not Interested")
            {
                DateTime NFDate;
                if (DateTime.TryParseExact(txtNextFollowupDateEnq.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NFDate))
                {
                    string NFDate1 = NFDate.ToString("dd-MM-yyyy");
                    objEnqFlw.NextFollowupDate = DateTime.ParseExact(NFDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                }
            }
            DateTime FDate;
            if (DateTime.TryParseExact(lblFollwupDateEnq.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out FDate))
            {
                string FDate1 = FDate.ToString("dd-MM-yyyy");
                objEnqFlw.FollowupDate = DateTime.ParseExact(FDate1, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            }

            objEnqFlw.NextFollowupTime = Convert.ToDateTime(txtNextFollowupTimeEnq.Text.ToString());
            objEnqFlw.FollowupTime = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt"));
            objEnqFlw.Comment = Regex.Replace(txtCommentEnq.Text, "^[ \t\r\n]+|[ \t\r\n]+$", "");
            objEnqFlw.Executive_ID = Convert.ToInt32(ddlExecutiveEnq.SelectedValue);
            objEnqFlw.FollowupType_AutoID = Convert.ToInt32(ddlFollowupTypeEnq.SelectedValue);
        }

        protected void btnSaveEnq_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCallResponseEnq.SelectedValue == "--Select--" || ddlRatingEnq.SelectedValue == "--Select--" || txtNextFollowupDateEnq.Text == string.Empty || txtNextFollowupTimeEnq.Text == string.Empty || txtCommentEnq.Text == string.Empty)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Msg", "toastr.error('Enter All Fields','Error');", true);

                    if (ddlCallResponseEnq.SelectedValue == "--Select--")
                    { ddlCallResponseEnq.Style.Add("border", "1px solid red "); }


                    if (ddlRatingEnq.SelectedValue == "--Select--")
                    { ddlRatingEnq.Style.Add("border", "1px solid red "); }


                    if (txtNextFollowupDateEnq.Text == "")
                    { txtNextFollowupDate.Style.Add("border", "1px solid red "); }

                    if (txtCommentEnq.Text == "")
                    { txtCommentEnq.Style.Add("border", "1px solid red "); }

                    if (txtNextFollowupTimeEnq.Text == "")
                    { txtNextFollowupTimeEnq.Style.Add("border", "1px solid red "); }
                }
                else
                {
                    ddlCallResponseEnq.Style.Add("border", "1px solid silver  ");
                    ddlRatingEnq.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupDateEnq.Style.Add("border", "1px solid silver  ");
                    txtNextFollowupTimeEnq.Style.Add("border", "1px solid silver  ");
                    txtCommentEnq.Style.Add("border", "1px solid silver  ");

                    AssignIDEnq();
                    AddParametersEnq();
                    int res1;
                    if (btnSaveEnq.Text == "Save")
                    {
                        objEnqFlw.Action = "INSERT";
                        res1 = objEnqFlw.Insert_EnquiryFollowupInformation();

                        if (res1 > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Saved Successfully','Success');", true);
                            ClearAllFieldEnq();
                            BindGridViewDetailsEnq();
                            flagpopup = 0;
                            Label34_ModalPopupExtender1.Hide();

                            Enq_Flwp();
                            EnqFollowupClick();
                        }
                    }
                    else if (btnSaveEnq.Text == "Update")
                    {
                        objEnqFlw.Action = "Update";
                        objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(ViewState["EnqFollowup_AutoID"]);
                        res1 = objEnqFlw.Insert_EnquiryFollowupInformation();
                        if (res1 > 0)
                        {
                            btnSaveEnq.Text = "Save";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Updated Successfully !!!','Success');", true);
                            ClearAllFieldEnq();
                            BindGridViewDetailsEnq();
                            flagpopup = 0;
                            Label34_ModalPopupExtender1.Hide();
                        }
                    }
                    //if (Request.QueryString["MenuEnquDetails"] != null)
                    //{
                    //    Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                    //}
                    //if (Request.QueryString["MenuEnqFollowupDetails"] != null)
                    //{
                    //    Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnqFollowupDetails=" + HttpUtility.UrlEncode("MenuEnqFollowupDetails".ToString()));
                    //}
                    //if (Request.QueryString["FNameSearchPage"] != null)
                    //{
                    //    int EnqAutoId = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("SearchPage.aspx?Enq_AutoID=" + EnqAutoId + " &FNameSearchPage2=" + HttpUtility.UrlEncode("FNameSearchPage2".ToString()));
                    //}
                    //if (Request.QueryString["FNameViewEnqFoll"] != null)
                    //{
                    //    Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
                    //    Response.Redirect("AddEnquiry.aspx?Enq_AutoID=" + Enq_ID + " &MenuEnquDetails=" + HttpUtility.UrlEncode("MenuEnquDetails".ToString()));
                    //}
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        public void gridBindEnq()
        {
            dt = objEnqFlw.GetDetails();
            if (dt.Rows.Count > 0)
            {
                gvEnqFollowup.DataSource = dt;
                gvEnqFollowup.DataBind();
            }
            else
            {
                gvEnqFollowup.DataSource = dt;
                gvEnqFollowup.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Record Not Found !!!.','Error');", true);
            }
            if (Request.Cookies["OnlineGym"]["Authority"] == "MasterAdmin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "SuperAdmin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Admin")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = true;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "Manager")
            {
                gvEnqFollowup.Columns[0].Visible = true;
                gvEnqFollowup.Columns[1].Visible = false;
            }
            else if (Request.Cookies["OnlineGym"]["Authority"] == "User")
            {
                gvEnqFollowup.Columns[0].Visible = false;
                gvEnqFollowup.Columns[1].Visible = false;
            }
        }

        private void BindGridViewDetailsEnq()
        {
            if (ViewState["Auto_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(ViewState["Auto_ID"].ToString());
            }
            else if (Request.QueryString["Enq_ID"] != null)
            {
                objEnqFlw.Enq_ID = Convert.ToInt32(Request.QueryString["Enq_ID"]);
            }
            AssignIDEnq();
            objEnqFlw.Action = "BindDetailsByID";
            gridBindEnq();

        }

        protected void btnDeleteEnq_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignIDEnq();
                objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(e.CommandArgument);
                objEnqFlw.Action = "DeleteByEnqFollowupAutoID";
                int i = objEnqFlw.Insert_EnquiryFollowupInformation();
                if (i > 0)
                {
                    BindGridViewDetailsEnq();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.success('Record Deleted Successfully !!!','Success');", true);
                    //BindGridViewDetailsEnq();
                }
                else if (i == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('Cannot Delete !!!','Error');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnEditEnq_Command(object sender, CommandEventArgs e)
        {
            try
            {
                AssignIDEnq();
                objEnqFlw.EnqFollowup_AutoID = Convert.ToInt32(e.CommandArgument);
                objEnqFlw.Action = "GetDetailsByEnqFollowupAutoID";

                dt = objEnqFlw.GetDetails();
                if (dt.Rows.Count >= 0)
                {
                    btnSaveEnq.Text = "Update";

                    ViewState["EnqFollowup_AutoID"] = dt.Rows[0]["EnqFollowup_AutoID"].ToString();
                    ddlCallResponseEnq.SelectedItem.Value = dt.Rows[0]["CallRespond_AutoID"].ToString();
                    ddlCallResponseEnq.SelectedItem.Text = dt.Rows[0]["CallRespond"].ToString();
                    ddlRatingEnq.SelectedValue = dt.Rows[0]["Rating"].ToString();
                    string NextFollowupDate = Convert.ToDateTime(dt.Rows[0]["NextFollowupDate"]).ToString("dd-MM-yyyy");
                    txtNextFollowupDateEnq.Text = NextFollowupDate;
                    txtNextFollowupTimeEnq.Text = dt.Rows[0]["NextFollowupTime"].ToString();
                    txtCommentEnq.Text = dt.Rows[0]["Comment"].ToString();
                    ddlExecutiveEnq.SelectedValue = dt.Rows[0]["Executive_ID"].ToString();
                    ddlFollowupTypeEnq.SelectedValue = dt.Rows[0]["FollowupType_AutoID"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Msg", "toastr.error('" + ex.Message.ToString() + "','Error');", true);
            }
        }

        protected void btnClearEnq_Click(object sender, EventArgs e)
        {
            ClearAllFieldEnq();
            btnSaveEnq.Text = "Save";
            ddlCallResponseEnq.Style.Add("border", "1px solid silver  ");
            ddlRatingEnq.Style.Add("border", "1px solid silver  ");
            txtNextFollowupDateEnq.Style.Add("border", "1px solid silver  ");
            txtNextFollowupTimeEnq.Style.Add("border", "1px solid silver  ");
            txtCommentEnq.Style.Add("border", "1px solid silver  ");
        }

        protected void gvEnqFollowup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnqFollowup.PageIndex = e.NewPageIndex;
            BindGridViewDetailsEnq();
            gvEnqFollowup.DataBind();
        }

        protected void chkExecutiveEnq_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutiveEnq.Checked == true)
                ddlExecutiveEnq.Enabled = false;
            else
                ddlExecutiveEnq.Enabled = true;
        }

        protected void ddlRatingEnq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRatingEnq.SelectedItem.Text == "Not Interested")
            {
                txtNextFollowupDateEnq.Enabled = false;
                txtNextFollowupDateEnq.Focus();
            }
            else
            {
                txtNextFollowupDateEnq.Enabled = true;
                txtNextFollowupDateEnq.Focus();

            }
        }

        protected void btnCancelEnq_Click(object sender, EventArgs e)
        {
            flagpopup = 0;
            Label34_ModalPopupExtender1.Hide();
        }
        #endregion

        
    }
}