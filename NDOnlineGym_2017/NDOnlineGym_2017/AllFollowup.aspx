<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AllFollowup.aspx.cs" Inherits="NDOnlineGym_2017.AllFollowup" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .divForm1
        {
            width: 96%;
            background-color: whitesmoke;
            padding-bottom: 10px;
        }

        .btnFollowupNioti
        {
            width: 80px;
            height: 80px;
        }

            .btnFollowupNioti:hover
            {
                margin-top: -5px;
                width: 85px;
                height: 85px;
                margin-left: -5px;
            }

        .divIcon
        {
            width: 120px;
            height: 120px;
            padding: 10px;
            margin-right: 10px;
        }

        .GridView
        {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

        .GridView1
        {
            width: max-content;
        }

        .modalBackground
        {
            background-color: gray;
        }

        .HideGridColumn
        {
            display: none;
        }

        .GridView a /** FOR THE PAGING ICONS  **/
        {
            background-color: Transparent;
            padding: 5px 5px 5px 5px;
            color: black;
            text-decoration: none;
            font-weight: bold;
        }

            .GridView a:focus
            {
                color: orangered;
            }

            .GridView a:hover
            {
                color: orangered;
            }

        .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
        { /*color: #fff;*/
            padding: 5px 5px 5px 5px;
        }

        .sc
        {
            width: 1021px;
        }

        @media screen and (min-width: 1400px)
        {
            .sc
            {
                width: 1100px;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
                <center>
        <h2>Followup</h2>
        <table style="margin-top:20px;">
            <tr>
                <td class="divIcon" ><asp:Label ID="Labeldt" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:ImageButton ID="btnPaymentFollowupNioti" CssClass="btnFollowupNioti" runat="server" ImageUrl="../NotificationIcons/images (3).jpg" OnClick="btnPaymentFollowupNioti_Click" />
                    <div style="text-align:center"><h4>Payment</h4></div>
                </td>
                <td class="divIcon" >
                    <asp:ImageButton ID="btnEnquiryFollowupNioti" runat="server"  CssClass="btnFollowupNioti" ImageUrl="../NotificationIcons/images (2).jpg" OnClick="btnEnquiryFollowupNioti_Click" />
                    <div style="text-align:center"><h4>Enquiry</h4></div>
                </td>
                <td class="divIcon" >
                    <asp:ImageButton ID="btnMemberEndFollowupNioti" runat="server"  CssClass="btnFollowupNioti" ImageUrl="../NotificationIcons/download.png" OnClick="btnMemberEndFollowupNioti_Click" />
                    <div style="text-align:center"><h4>Member End</h4></div>
                </td>
                <td class="divIcon" >
                    <asp:ImageButton ID="btnUpgradFollowupNioti" runat="server"  CssClass="btnFollowupNioti" ImageUrl="../NotificationIcons/images (7).jpg" OnClick="btnUpgradFollowupNioti_Click" />
                    <div style="text-align:center"><h4>Upgrade</h4></div>
                </td>
                <td class="divIcon" >
                    <asp:ImageButton ID="btnMembershipFollowupNioti" runat="server"  CssClass="btnFollowupNioti" ImageUrl="../NotificationIcons/download (1).png" OnClick="btnMembershipFollowupNioti_Click" />
                    <div style="text-align:center"><h4>Membership</h4></div>
                </td>
                <td class="divIcon" >
                    <asp:ImageButton ID="btnMeasurementFollowupNoti" runat="server"  CssClass="btnFollowupNioti" ImageUrl="../NotificationIcons/Measurement.jpg" OnClick="btnMeasurementFollowupNioti_Click" />
                    <div style="text-align:center"><h4>Measurement</h4></div>
                </td>
                <td class="divIcon" >
                    <asp:ImageButton ID="btnExistingFollowup" runat="server"  CssClass="btnFollowupNioti" ImageUrl="../NotificationIcons/images (6).png" OnClick="btnExistingFollowup_Click"/>
                    <div style="text-align:center"><h4>Done Followup</h4></div>
                </td>
             </tr>
         </table>
    </center>

                <%----------------------------------------------------------Payment Followup---------------------------------------------------------------%>
                <div class="divForm" id="divPaymentFollowup" runat="server" style="margin-bottom: 20px;" visible="false">
                    <div class="form-header">
                        <h4>&#10148; Payment Followup  </h4>
                    </div>
                    <div class="divForm">
                        <div class="form-panel">
                            <table style="margin-left: 120px;">

                                <tr>
                                    <td>
                                        <table style="margin-top: 25px">
                                            <tr>
                                                <th>From Date</th>
                                                <th>To Date</th>
                                                 <th></th>
                                               <th>Category</th>
                                                <th>Search By</th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>

                                                <td>
                                                    <asp:TextBox ID="txtFromDatePayment" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtFromDatePayment_CalendarExtender" runat="server"
                                                        BehaviorID="txtFromDatePayment_CalendarExtender" TargetControlID="txtFromDatePayment" Format="dd-MM-yyyy" />
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtToDatePayment" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtToDatePayment_CalendarExtender" runat="server"
                                                        BehaviorID="txtToDatePayment_CalendarExtender" TargetControlID="txtToDatePayment" Format="dd-MM-yyyy" />
                                                </td>
                                                <td>
                                                <asp:Button ID="btnSearchPayment" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearchPayment_Click" UseSubmitBehavior="false" TabIndex="5" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCategoryPayment" runat="server" CssClass="ddl" TabIndex="2">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                                        <asp:ListItem Value="Name">Name</asp:ListItem>
                                                        <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                                        <asp:ListItem Value="Gender">Gender</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtSearchPayment" runat="server" CssClass="txt" TabIndex="4" OnTextChanged="txtSearchPayment_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <center class="btn-section">
                                     
                                    
                                    <asp:Button ID="btnSearchByDateCategoryPayment" runat="server" Text="Date With Category" OnClick="btnSearchByDateCategoryPayment_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClearPayment" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClearPayment_Click" />
                                    <asp:Button ID="btnExportToExcelPayment" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="38" OnClick="btnExportToExcelPayment_Click" />
                                 </center>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <%----------------------------------------------------------End Payment Followup---------------------------------------------------------------%>

                <%----------------------------------------------------------Enquiry Followup---------------------------------------------------------------%>
                <div class="divForm" id="divEnquiryFollowup" runat="server" style="margin-bottom: 20px;" visible="false">
                    <div class="form-header">
                        <h4>&#10148; Enquiry Followup  </h4>
                    </div>
                    <div class="form-panel">
                        <center>
                <table>
                    <tr>
                        <th>From Date</th>
                        <th>To Date</th>
                        <th></th>
                        <th>Category</th>
                        <th>Search by</th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDateEnquiry" runat="server" CssClass="txt" TabIndex="28"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDateEnquiry_Calnderfromdate" runat="server" BehaviorID="txtFromDateEnquiry_CalendarExtender" TargetControlID="txtFromDateEnquiry" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:TextBox ID="txtToDateEnquiry" runat="server" CssClass="txt" TabIndex="29"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="txtToDateEnquiry_Calnderfromdate" runat="server" BehaviorID="txtToDateEnquiry_CalendarExtender" TargetControlID="txtToDateEnquiry" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:Button ID="btnSearchEnquiry" runat="server" CssClass="form-btn" OnClick="btnSearchEnquiry_Click" TabIndex="30" Text="Search" />
                       </td>
                        <td>
                            <asp:DropDownList ID="ddlSearchEnquiry" runat="server" CssClass="ddl" TabIndex="31" >
                                <%--<asp:ListItem Value="All">All</asp:ListItem>--%>
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                <asp:ListItem Value="Enquiry ID">Enquiry ID</asp:ListItem>
                                <asp:ListItem Value="First Name">First Name</asp:ListItem>
                                <asp:ListItem Value="Last Name">Last Name</asp:ListItem>
                                <asp:ListItem Value="Contact 1">Contact</asp:ListItem>
                                <asp:ListItem Value="Enquiry Type">Enquiry Type</asp:ListItem>
                                <asp:ListItem Value="Enquiry For">Enquiry For</asp:ListItem>
                                <asp:ListItem Value="Source Of Enquiry">Source Of Enquiry</asp:ListItem>
                                <asp:ListItem Value="Rating">Rating</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchEnquiry" runat="server" CssClass="txt" Enabled="true" TabIndex="32" OnTextChanged="txtSearchEnquiry_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                       
                    </tr>

                </table>
              </center>

                        <center style="margin-top: 20px;">
                    <asp:Button ID="btnEnquiryDateEnquiry" runat="server" CssClass="form-btn"  TabIndex="33" Text="Enquiry Date" OnClick="btnEnquiryDateEnquiry_Click" />
                    <asp:Button ID="btnFollowupDateEnquiry" runat="server" CssClass="form-btn"  TabIndex="34" Text="Followup Date" OnClick="btnFollowupDateEnquiry_Click" />
                    <asp:Button ID="btnEnqDtWithCategoryEnquiry" runat="server" CssClass="form-btn"  TabIndex="35" Text="Enq Date With Category" OnClick="btnEnqDtWithCategoryEnquiry_Click" />
                    <asp:Button ID="btnFollDtWithCategoryEnquiry" runat="server" CssClass="form-btn"  TabIndex="36" Text="Followup Date With Category" OnClick="btnFollDtWithCategoryEnquiry_Click" />
                    <asp:Button ID="btnClearEnquiry" runat="server" Text="Clear" CssClass="form-btn" TabIndex="38" OnClick="btnClearEnquiry_Click" />
                    <asp:Button ID="btnExportToExcelEnquiry" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="39" OnClick="btnExportToExcelEnquiry_Click" />
                        </center>
                    </div>
                </div>
                <%----------------------------------------------------------End Enquiry Followup---------------------------------------------------------------%>

                <%----------------------------------------------------------Member End Followup---------------------------------------------------------------%>
                <div class="divForm" id="divMemberEndFollowup" runat="server" style="margin-bottom: 20px;" visible="false">
                    <div class="form-header">
                        <h4>&#10148; Member End Followup  </h4>
                    </div>
                    <div class="divForm">
                        <div class="form-panel">
                            <table style="margin-left: 150px;">

                                <tr>
                                    <td>
                                        <table style="margin-top: 25px">
                                            <tr>
                                                <th>From Date</th>
                                                <th>To Date</th>
                                                <th></th>
                                                <th>Date Category</th>
                                                <th>Package</th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <asp:Label ID="Label2" runat="server" Text="Label" Visible="false"></asp:Label>

                                                <td>
                                                    <asp:TextBox ID="txtFromDateMemberEnd" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtFromDateMemberEnd_CalendarExtender" runat="server"
                                                        BehaviorID="txtFromDateMemberEnd_CalendarExtender" TargetControlID="txtFromDateMemberEnd" Format="dd-MM-yyyy" />
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtToDateMemberEnd" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtToDateMemberEnd_CalendarExtender" runat="server"
                                                        BehaviorID="txtToDateMemberEnd_CalendarExtender" TargetControlID="txtToDateMemberEnd" Format="dd-MM-yyyy" />
                                                </td>
                                                 <td>
                                                <asp:Button ID="btnSearchMemberEnd" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearchMemberEnd_Click" UseSubmitBehavior="false" TabIndex="5" />
                                                      </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDateCategoryMemberEnd" runat="server" CssClass="ddl" TabIndex="3" OnSelectedIndexChanged="ddlDateCategoryMemberEnd_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="End Date">End Date</asp:ListItem>
                                                        <asp:ListItem Value="Next Followup Date">Next Followup Date</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="ddlPackageMemberEnd" runat="server" CssClass="ddl" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlPackageMemberEnd_SelectedIndexChanged">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>



                            </table>
                            <center class="btn-section">
                                     
                                    <asp:Button ID="btnSearchByDateCategoryMemberEnd" runat="server" Text="Date With Category" OnClick="btnSearchByDateCategoryMemberEnd_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClearMemberEnd" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClearMemberEnd_Click" />
                                    <asp:Button ID="btnExportToExcelMemberEnd" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="38" OnClick="btnExportToExcelMemberEnd_Click" />

                                 </center>

                        </div>
                    </div>
                </div>
                <%----------------------------------------------------------End Member End Followup---------------------------------------------------------------%>

                <%----------------------------------------------------------Membership Followup---------------------------------------------------------------%>
                <div class="divForm" id="divMembershipFollowup" runat="server" style="margin-bottom: 20px;" visible="false">

                    <div class="form-header">
                        <h4>&#10148; Membership Followup  </h4>
                    </div>
                    <div class="divForm">
                        <div class="form-panel">
                            <table style="width: 100%;">

                                <tr>
                                    <td>
                                        <table style="margin-top: 25px">
                                            <tr>
                                                <th>From Date</th>
                                                <th>To Date</th>
                                                <th></th>
                                                <th> Category</th>
                                                <th>Search By</th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>

                                                <td>
                                                    <asp:TextBox ID="txtFromDateMemberFollowup" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtFromDateMemberFollowup_CalendarExtender" runat="server"
                                                        BehaviorID="txtFromDateMemberFollowup_CalendarExtender" TargetControlID="txtFromDateMemberFollowup" Format="dd-MM-yyyy" />
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtToDateMemberFollowup" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtToDateMemberFollowup_CalendarExtender" runat="server"
                                                        BehaviorID="txtToDateMemberFollowup_CalendarExtender" TargetControlID="txtToDateMemberFollowup" Format="dd-MM-yyyy" />
                                                </td>
                                                 <td>
                                                <asp:Button ID="btnSearchMemberFollowup" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearchMemberFollowup_Click" UseSubmitBehavior="false" TabIndex="6" />
                                                 </td>
                                                     <td>
                                                    <asp:DropDownList ID="ddlCategoryMemberFollowup" runat="server" CssClass="ddl" TabIndex="4">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                                        <asp:ListItem Value="Name">Name</asp:ListItem>
                                                        <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                                        <asp:ListItem Value="Gender">Gender</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtSearchMemberFollowup" runat="server" CssClass="txt" TabIndex="5" OnTextChanged="txtSearchMemberFollowup_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <center class="btn-section">
                                    <asp:Button ID="btnSearchByDateCategoryMemberFollowup" runat="server" Text="Date With Category" OnClick="btnSearchByDateCategoryMemberFollowup_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClearMemberFollowup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClearMemberFollowup_Click" />
                                    <asp:Button ID="btnExportToExcelMembership" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="8" OnClick="btnExportToExcelMembership_Click" />
                                 </center>
                                    </td>
                                </tr>
                            </table>


                        </div>
                    </div>
                </div>
                <%----------------------------------------------------------End Membership Followup---------------------------------------------------------------%>

                <%----------------------------------------------------------Measurement Followup---------------------------------------------------------------%>
                <div class="divForm" id="divMeasurementFollowup" runat="server" style="margin-bottom: 20px;" visible="false">

                    <div class="form-header">
                        <h4>&#10148; Measurement Followup  </h4>
                    </div>
                    <div class="divForm">
                        <div class="form-panel">
                            <table style="width: 100%;">

                                <tr>
                                    <td>
                                        <table style="margin-top: 25px">
                                            <tr>
                                                <th>From Date</th>
                                                <th>To Date</th>
                                                <%--<th>Date Category</th>--%>
                                                <th>Category</th>
                                                <th>Search By</th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>

                                                <td>
                                                    <asp:TextBox ID="txtFromDateMeasurementFollowup" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                        BehaviorID="txtFromDateMeasurementFollowup_CalendarExtender" TargetControlID="txtFromDateMeasurementFollowup" Format="dd-MM-yyyy" />
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtToDateMeasurementFollowup" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        BehaviorID="txtToDateMeasurementFollowup_CalendarExtender" TargetControlID="txtToDateMeasurementFollowup" Format="dd-MM-yyyy" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearchMeasurementFollowup" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearchMeasurementFollowup_Click" UseSubmitBehavior="false" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCategoryMeasurementFollowup" runat="server" CssClass="ddl" TabIndex="4">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                                        <asp:ListItem Value="Name">Name</asp:ListItem>
                                                        <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                                        <asp:ListItem Value="Gender">Gender</asp:ListItem>

                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtSearchMeasurementFollowup" runat="server" CssClass="txt" TabIndex="5" OnTextChanged="txtSearchMeasurementFollowup_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <center class="btn-section">
                                    
                                    <asp:Button ID="btnSearchByDateCategoryMeasurementFollowup" runat="server" Text="Date With Category" OnClick="btnSearchByDateCategoryMeasurementFollowup_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClearMeasurementFollowup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClearMeasurementFollowup_Click" />
                                    <asp:Button ID="btnExportToExcelMeasurement" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="8" OnClick="btnExportToExcelMeasurement_Click" />

                                        </center>
                                    </td>
                                </tr>
                            </table>


                        </div>
                    </div>
                </div>
                <%----------------------------------------------------------End Measurement Followup---------------------------------------------------------------%>

                <%----------------------------------------------------------Existing Followup---------------------------------------------------------------%>
                <div class="divForm" id="divExistingFollowup" runat="server" style="margin-bottom: 20px;" visible="false">
                    <div class="form-header">
                        <h4>&#10148; Existing Followup  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="margin-left: 20px;">

                            <tr>
                                <td>
                                    <table style="margin-top: 25px">
                                        <tr>
                                            <th>From Date</th>
                                            <th>To Date</th>
                                            <th>Category</th>
                                            <th>Search By</th>
                                            <th>Date Category</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>

                                            <td>
                                                <asp:TextBox ID="txtFromDateExistingFoll" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtFromDateExistingFoll_CalendarExtender" runat="server"
                                                    BehaviorID="txtFromDateExistingFoll_CalendarExtender" TargetControlID="txtFromDateExistingFoll" Format="dd-MM-yyyy" />
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtToDateExistingFoll" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtToDateExistingFoll_CalendarExtender" runat="server"
                                                    BehaviorID="txtToDateExistingFoll_CalendarExtender" TargetControlID="txtToDateExistingFoll" Format="dd-MM-yyyy" />
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="ddlCategoryExistingFollowup" runat="server" CssClass="ddl" TabIndex="2">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                                    <asp:ListItem Value="Name">Name</asp:ListItem>
                                                    <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                                    <asp:ListItem Value="Gender">Gender</asp:ListItem>
                                                    <asp:ListItem Value="Followup Type">Followup Type</asp:ListItem>
                                                    <asp:ListItem Value="Call Response">Call Response</asp:ListItem>
                                                    <asp:ListItem Value="Rating">Rating</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtSearchExistingFollowup" runat="server" CssClass="txt" TabIndex="4" OnTextChanged="txtSearchExistingFollowup_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="ddlDateCategoryExistingFollowup" runat="server" CssClass="ddl" TabIndex="2" OnSelectedIndexChanged="ddlDateCategoryExistingFollowup_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Followup Date">Followup Date</asp:ListItem>
                                                    <asp:ListItem Value="Next Followup Date">Next Followup Date</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <center class="btn-section">
                                    <asp:Button ID="btnSearchExistingFollowup" runat="server" Text="Search" CssClass="form-btn"  UseSubmitBehavior="false" TabIndex="5" OnClick="btnSearchExistingFollowup_Click" />
                                    <asp:Button ID="btnSearchByDateCategoryExistingFollowup" runat="server" Text="Date With Category"  CssClass="form-btn" ValidationGroup="Category" OnClick="btnSearchByDateCategoryExistingFollowup_Click" TabIndex="6" />
                                    <asp:Button ID="btnClearExistingFollowup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClearExistingFollowup_Click" />
                                    <asp:Button ID="btnExportToExcelExistingFollowup" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="7" OnClick="btnExportToExcelExistingFollowup_Click" />
                                 </center>
                                </td>
                            </tr>
                        </table>


                    </div>
                </div>
                <%----------------------------------------------------------End Existing Followup---------------------------------------------------------------%>

                <%----------------------------------------------------------Upgrade Followup---------------------------------------------------------------%>
                <div class="divForm" id="divUpgradeFollowup" runat="server" style="margin-bottom: 20px;" visible="false">
                    <div class="form-header">
                        <h4>&#10148; Upgrade Followup  </h4>
                    </div>
                    <div class="divForm">
                        <div class="form-panel">
                            <table style="margin-left: 120px;">

                                <tr>
                                    <td>
                                        <table style="margin-top: 25px">
                                            <tr>
                                                <th>From Date</th>
                                                <th>To Date</th>
                                                <th>Package</th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>

                                                <td>
                                                    <asp:TextBox ID="txtFromDateUpgrade" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtFromDateUpgrade_CalendarExtender" runat="server"
                                                        BehaviorID="txtFromDateUpgrade_CalendarExtender" TargetControlID="txtFromDateUpgrade" Format="dd-MM-yyyy" />
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtToDateUpgrade" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtToDateUpgrade_CalendarExtender" runat="server"
                                                        BehaviorID="txtToDateUpgrade_CalendarExtender" TargetControlID="txtToDateUpgrade" Format="dd-MM-yyyy" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearchUpgrade" runat="server" Text="Search" OnClick="btnSearchUpgrade_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPackageUpgrade" runat="server" CssClass="ddl" TabIndex="2" OnSelectedIndexChanged="ddlPackageUpgrade_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <center class="btn-section">
                                    
                                    <asp:Button ID="btnSearchByDateCategoryUpgrade" runat="server" Text="Date With Category" OnClick="btnSearchByDateCategoryUpgrade_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClearUpgrade" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClearUpgrade_Click" />
                                    <asp:Button ID="btnExportToExcelUpgrade" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="38" OnClick="btnExportToExcelUpgrade_Click" />
                                 </center>
                                    </td>
                                </tr>
                            </table>


                        </div>
                    </div>
                </div>
                <%----------------------------------------------------------End Payment Followup---------------------------------------------------------------%>



                <%----------------------------------------------------------Followup Popup ---------------------------------------------------------------%>

                <asp:Label runat="server" Text="" ID="Label33"></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="Label33_ModalPopupExtender1" runat="server"
                    DynamicServicePath="" Enabled="True" BackgroundCssClass="modalBackground"
                    TargetControlID="Label33" PopupControlID="Panel1">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Label runat="server" Text="" ID="Label34"></asp:Label>
                <ajaxToolkit:ModalPopupExtender ID="Label34_ModalPopupExtender1" runat="server"
                    DynamicServicePath="" Enabled="True" BackgroundCssClass="modalBackground"
                    TargetControlID="Label34" PopupControlID="Panel2">
                </ajaxToolkit:ModalPopupExtender>

                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="1000px">
                    <div style="background-color: white">
                        <div class="form-name-header">
                            <h3>Add Followup</h3>
                        </div>

                        <div class="divForm">
                            <div class="form-header" id="formheader1">
                                <h4>&#10148;Member Details </h4>
                            </div>
                            <div class="form-panel" id="formpanel1">
                                <table style="height: 80px;">
                                    <tr>
                                        <th><span class="error">*</span>ID</th>
                                        <th><span class="error">*</span>Contact</th>
                                        <th>First Name</th>
                                        <th>Last Name</th>
                                        <th>Gender</th>
                                        <th>Email Id</th>
                                    </tr>
                                    <tr id="row1" runat="server">
                                        <td>
                                            <asp:TextBox ID="txtMemberID" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="1" AutoPostBack="true"
                                                onkeypress="return RestrictSpaceSpecial(event);" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtContact" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="2" AutoPostBack="true"
                                                onkeypress="return RestrictSpaceSpecial(event);" AutoCompleteType="Disabled"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFirst" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="3" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLast" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="4" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="5" Enabled="false">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmail" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="6" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="form-header">
                                <h4>&#10148;Add Followup </h4>
                            </div>
                            <div class="form-panel">
                                <table style="height: 80px;">
                                    <tr>
                                        <th><span class="error">*</span>Followup Type</th>
                                        <th><span class="error">*</span>Executive</th>
                                        <th><span class="error">*</span>Call Response</th>
                                        <th><span class="error">*</span>Rating</th>
                                        <th><span class="error">*</span>Next Followup Date</th>
                                        <th><span class="error">*</span>Next Followup Time</th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlFollowupType" runat="server" Style="width: 160px; padding: 3px 5px;" CssClass="ddl" TabIndex="7">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            </asp:DropDownList>

                                        </td>

                                        <td>
                                            <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" />
                                            <asp:DropDownList ID="ddlExecutive" runat="server" TabIndex="18" Enabled="false" Style="width: 150px; padding: 3px">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            </asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCallPesponse" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="9">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>

                                            </asp:DropDownList>

                                        </td>
                                        <td>

                                            <asp:DropDownList ID="ddlRating" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="10" OnSelectedIndexChanged="ddlRating_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                                <asp:ListItem Value="Cold">Cold</asp:ListItem>
                                                <asp:ListItem Value="Warm">Warm</asp:ListItem>
                                                <asp:ListItem Value="Expected">Expected</asp:ListItem>
                                                <asp:ListItem Value="Not Interested">Not Interested</asp:ListItem>
                                            </asp:DropDownList>


                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNextFollowupDate" runat="server" Style="width: 160px; padding: 3px 5px;" TabIndex="11"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtNextFollowupDate_CalendarExtender1" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtNextFollowupDate" Format="dd-MM-yyyy" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNextFollowupTime" runat="server" Style="width: 150px; height: 25px; padding: 3px 5px;" TabIndex="12" TextMode="Time"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                                <table>
                                    <tr>
                                        <td style="padding-top: 10px;">
                                            <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">Followup Date :</span>
                                            <asp:Label ID="lblFollowupDateTime" runat="server" Text="" Style="font-size: 13px"></asp:Label>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="padding: 10px;">
                                            <span class="error">*</span><span style="font-weight: bold; font-size: 12px; margin-left: 5px; margin-right: 45px;">Comment</span>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtComment" runat="server" CssClass="text" TabIndex="14" TextMode="MultiLine" Width="400px" Rows="4" Style="resize: none"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <center class="btn-section">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" OnClick="btnSave_Click" TabIndex="15"
                                                    OnClientClick="this.disabled = true;" UseSubmitBehavior="false" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" ValidationGroup="a" TabIndex="16" OnClick="btnClear_Click" />
                                                <asp:Button ID="btnCancle" runat="server" Text="Close" CssClass="form-btn" ValidationGroup="a" TabIndex="16" OnClick="btnCancle_Click" />
                                                </center>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="divForm">
                                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 10px;">
                                        <asp:GridView ID="gvFollowupDetailspopup" runat="server" AutoGenerateColumns="false" DataKeyNames="Followup_AutoID" CssClass="GridView" PagerStyle-CssClass="pager"
                                            AllowPaging="True" GridLines="None" OnPageIndexChanging="gvFollowupDetailspopup_PageIndexChanging" PageSize="20" Width="1000px">
                                            <Columns>
                                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("Followup_AutoID")%>'
                                                            OnCommand="btnEdit_Command" TabIndex="20" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="20"
                                                            CommandArgument='<%#Eval("Followup_AutoID")%>' OnCommand="btnDelete_Command"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Followup Type" DataField="FollowupType" HeaderStyle-HorizontalAlign="left" />
                                                <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                                <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                                    <HeaderTemplate>
                                                        <b>Followup Date</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Call Response" DataField="CallResponse" HeaderStyle-HorizontalAlign="left" />
                                                <asp:BoundField HeaderText="Rating" DataField="Rating" HeaderStyle-HorizontalAlign="left" />
                                                <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                                    <HeaderTemplate>
                                                        <b>Nxt Followup Date</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Nxt Followup Time" DataField="NextFollowupTime" HeaderStyle-HorizontalAlign="left" />
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Width="1000px">
                    <div style="background-color: white">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="sc">
                                    <div class="form-name-header">
                                        <h3>Enquiry Followup</h3>
                                    </div>
                                    <%--End Other Details--%>
                                    <div id="divEnquiryFollowupSection" runat="server">
                                        <div class="divForm1">
                                            <div class="form-header">
                                                <h4>&#10148;Add Followup </h4>
                                            </div>
                                            <div class="form-panel">
                                                <table>
                                                    <tr>
                                                        <td style="padding-top: 10px;">
                                                            <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">Name :</span>
                                                            <asp:Label ID="lblNameEnq" runat="server" Style="font-size: 13px"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px; padding-left: 20px;">
                                                            <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">Contact :</span>
                                                            <asp:Label ID="lblContactEnq" runat="server" Style="font-size: 13px"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px; padding-left: 20px;">
                                                            <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">Gender :</span>
                                                            <asp:Label ID="lblGenderEnq" runat="server" Style="font-size: 13px"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px; padding-left: 20px;">
                                                            <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">DOB :</span>
                                                            <asp:Label ID="lbldOBEnq" runat="server" Style="font-size: 13px"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px;">
                                                            <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">Followup Date :</span>
                                                            <asp:Label ID="lblFollwupDateEnq" runat="server" Text="" Style="font-size: 13px"></asp:Label>

                                                        </td>

                                                    </tr>
                                                </table>
                                                <table style="height: 80px;">
                                                    <tr>
                                                        <th><span class="error">*</span>Followup Type</th>
                                                        <th><span class="error">*</span>Executive</th>
                                                        <th><span class="error">*</span>Call Response</th>
                                                        <th><span class="error">*</span>Rating</th>
                                                        <th><span class="error">*</span>Next Followup Date</th>
                                                        <th><span class="error"></span>Next Followup Time</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFollowupTypeEnq" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" Enabled="false" TabIndex="1">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>

                                                        <td>
                                                            <asp:CheckBox ID="chkExecutiveEnq" runat="server" Checked="true" OnCheckedChanged="chkExecutiveEnq_CheckedChanged" AutoPostBack="true" />
                                                            <asp:DropDownList ID="ddlExecutiveEnq" runat="server" TabIndex="2" Enabled="false" Style="width: 150px; padding: 3px">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCallResponseEnq" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="3">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>

                                                            </asp:DropDownList>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRatingEnq" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" OnSelectedIndexChanged="ddlRatingEnq_SelectedIndexChanged" AutoPostBack="true" TabIndex="4">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                                                <asp:ListItem Value="Cold">Cold</asp:ListItem>
                                                                <asp:ListItem Value="Warm">Warm</asp:ListItem>
                                                                <asp:ListItem Value="Expected">Expected</asp:ListItem>
                                                                <asp:ListItem Value="Not Interested">Not Interested</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNextFollowupDateEnq" runat="server" Style="width: 160px; padding: 3px 5px;" TabIndex="5"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtNextFollowupDateEnq" Format="dd-MM-yyyy" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNextFollowupTimeEnq" runat="server" Style="width: 150px; height: 25px; padding: 3px 5px;" TabIndex="6" TextMode="Time"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                </table>

                                                <table>
                                                    <tr>
                                                        <td style="padding: 10px;">
                                                            <span class="error">*</span><span style="font-weight: bold; font-size: 12px; margin-left: 5px; margin-right: 70px;">Comment</span>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCommentEnq" runat="server" CssClass="text" TabIndex="5" TextMode="MultiLine" Width="400px" Rows="7" Style="resize: none"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <center class="btn-section">
                                                <asp:Button ID="btnSaveEnq" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" OnClick="btnSaveEnq_Click"
                                                    OnClientClick="this.disabled = true;" UseSubmitBehavior="false" TabIndex="8" />
                                                <asp:Button ID="btnCancelEnq" runat="server" Text="Close" CssClass="form-btn" ValidationGroup="a" TabIndex="7" OnClick="btnCancelEnq_Click" />
                                                <%--<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="form-btn" ValidationGroup="a"  TabIndex="8" OnClick="btnBack_Click" />--%>
                                                </center>
                                            </div>
                                        </div>
                                        <div style="width: 1000px; height: 200px; overflow-x: scroll; margin-top: 10px;">
                                            <asp:GridView ID="gvEnqFollowup" runat="server" AutoGenerateColumns="false" Width="1000px"
                                                DataKeyNames="EnqFollowup_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" OnPageIndexChanging="gvEnqFollowup_PageIndexChanging"
                                                AllowPaging="True" PageSize="20">
                                                <Columns>
                                                    <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEditEnq" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEditEnq_Command"
                                                                CommandArgument='<%#Eval("EnqFollowup_AutoID")%>' TabIndex="9" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDeleteEnq" runat="server" CausesValidation="false" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="9"
                                                                CommandArgument='<%#Eval("EnqFollowup_AutoID")%>' OnCommand="btnDeleteEnq_Command"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                                        <HeaderTemplate>
                                                            <b>Followup Date</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                                        <HeaderTemplate>
                                                            <b>NextFollowup Date</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField HeaderText="NF Time" DataField="NextFollowupTime" HeaderStyle-HorizontalAlign="left" />
                                                    <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />
                                                    <asp:BoundField HeaderText="Rating" DataField="Rating" HeaderStyle-HorizontalAlign="left" />
                                                    <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
                                                </Columns>

                                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                                <RowStyle Height="20px" />
                                                <AlternatingRowStyle Height="20px" BackColor="White" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>

                <%----------------------------------------------------------End Followup Popup---------------------------------------------------------------%>
                <div class="divForm" style="margin-top: 5px">
                    <div class="form-panel">
                        <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                            <div style="margin: 10px 0px 10px 10px">
                                <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                            </div>
                            <asp:GridView ID="gvFollowupDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="ID1"
                                Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView GridView1" GridLines="None" CellPadding="5"
                                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvFollowupDetails_PageIndexChanging"
                                OnSelectedIndexChanged="gvFollowupDetails_SelectedIndexChanged">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Followup" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAllFollowup" runat="server" CausesValidation="false" Text="Followup"
                                                CommandArgument='<%#Eval("ID")%>' TabIndex="32" OnCommand="btnAllFollowup_Command" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                  <%--  <asp:CommandField ShowSelectButton="true" HeaderText="Followup"
                                        SelectText="<img src='NotificationIcons/arrow_top_right-128.png' alt='Followup'/>" />--%>
                                     <asp:CommandField ShowSelectButton="true" ControlStyle-Width="20px" ItemStyle-Width="20px"
                                        SelectImageUrl="~/NotificationIcons/arrow_top_right-128.png" SelectText="Followup" ButtonType="Image"/>

                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAllFollowup" runat="server" CausesValidation="false" OnCommand="btnAllFollowup_Command" CommandArgument='<%#Eval("ID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/arrow_top_right-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Followup" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("ID1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("ID1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPayFollName" runat="server" CausesValidation="false"
                                                CommandArgument='<%#Eval("AutoID")%>' Text='<%#Eval("Name")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comment" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtComment" runat="server" Text='<%#Eval("Comment")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comment")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rating" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRating" runat="server" Text='<%#Eval("Rating") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRating" runat="server" Text='<%#Eval("Rating") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Call Response" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtCallResponse" runat="server" Text='<%#Eval("CallResponse") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblCallResponse" runat="server" Text='<%#Eval("CallResponse") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FollowupType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtFollowupType" runat="server" Text='<%#Eval("FollowupType") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblFollowupType" runat="server" Text='<%#Eval("FollowupType") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Followup Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtFollowupDate" runat="server" Text='<%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblFollowupDate" runat="server" Text='<%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NextFollowupDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNextFollowupDate" runat="server" Text='<%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNextFollowupDate" runat="server" Text='<%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NextFollowupTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNextFollowupTime" runat="server" Text='<%#Eval("NextFollowupTime")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNextFollowupTime" runat="server" Text='<%#Eval("NextFollowupTime")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Executive" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtExecutive" runat="server" Text='<%#Eval("Executive") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblExecutive" runat="server" Text='<%#Eval("Executive") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>

                            <asp:GridView ID="gvBalPayDetails" runat="server" AutoGenerateColumns="false" Width="1900px"
                                DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView GridView1" GridLines="None" CellPadding="3"
                                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvBalPayDetails_PageIndexChanging" OnSelectedIndexChanged="gvBalPayDetails_SelectedIndexChanged">
                                <Columns>
                                    <%--<asp:CommandField ShowSelectButton="true" ControlStyle-Width="40px" ItemStyle-Width="40px"
                                        SelectText="<img src='NotificationIcons/arrow_top_right-128.png' alt='Followup'/>" />--%>
                                     <asp:CommandField ShowSelectButton="true" ControlStyle-Width="20px" ItemStyle-Width="20px"
                                        SelectImageUrl="~/NotificationIcons/arrow_top_right-128.png" SelectText="Followup" ButtonType="Image"/>
                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Member_ID1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnBalPayPayment" runat="server" CausesValidation="false" OnCommand="btnBalPayPayment_Command" CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/images (3).jpg'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Balance Payment" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFollowupPayment" runat="server" CausesValidation="false" OnCommand="btnFollowupPayment_Command" CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/arrow_top_right-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Followup" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipt ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Member ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("MemberID1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnNamePayment" runat="server" CausesValidation="false"
                                                CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnNamePayment_Command" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Gender" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Package" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPackage" runat="server" Text='<%#Eval("Package") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("Package") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%-- <asp:TemplateField HeaderText="Pay Mode" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPaymentMode" runat="server" Text='<%#Eval("PaymentMode") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblPaymentMode" runat="server" Text='<%#Eval("PaymentMode") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Total" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtTotal" runat="server" Text='<%#Eval("Total") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PaidFee" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPaidFee" runat="server" Text='<%#Eval("PaidFee") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPaidFee" runat="server" Text='<%#Eval("PaidFee") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pay Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPayDate" runat="server" Text='<%#Eval("PayDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPayDate" runat="server" Text='<%#Eval("PayDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Pay Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNextBalDate" runat="server" Text='<%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNextBalDate" runat="server" Text='<%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" ControlStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtStatus" runat="server" Text='<%#Eval("Status") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>

                            <asp:GridView ID="gvMemEndDetails" runat="server" AutoGenerateColumns="false" Width="1000px"
                                DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvMemEndDetails_PageIndexChanging" OnSelectedIndexChanged="gvMemEndDetails_SelectedIndexChanged">
                                <Columns>
                                    <%--<asp:CommandField ShowSelectButton="true" ControlStyle-Width="40px" ItemStyle-Width="40px"
                                        SelectText="<img src='NotificationIcons/arrow_top_right-128.png' alt='Followup'/>" />--%>

                                     <asp:CommandField ShowSelectButton="true" ControlStyle-Width="20px" ItemStyle-Width="20px"
                                        SelectImageUrl="~/NotificationIcons/arrow_top_right-128.png" SelectText="Followup" ButtonType="Image"/>

                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Member_ID1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFollowupMemberEnd" runat="server" CausesValidation="false" OnCommand="btnFollowupMemberEnd_Command" CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/arrow_top_right-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Followup" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipt ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Member ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("MemberID1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnNameMemberEnd" runat="server" CausesValidation="false"
                                                CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnNameMemberEnd_Command" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Gender" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Package" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPackage" runat="server" Text='<%#Eval("Package") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("Package") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Start Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Total" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtTotal" runat="server" Text='<%#Eval("Total") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PaidFee" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPaidFee" runat="server" Text='<%#Eval("PaidFee") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPaidFee" runat="server" Text='<%#Eval("PaidFee") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pay Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPayDate" runat="server" Text='<%#Eval("PayDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPayDate" runat="server" Text='<%#Eval("PayDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Pay Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNextBalDate" runat="server" Text='<%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNextBalDate" runat="server" Text='<%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" ControlStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtStatus" runat="server" Text='<%#Eval("Status") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>

                            <asp:GridView ID="gvMemberDetails" runat="server" AutoGenerateColumns="false" Width="1000px"
                                DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvMemberDetails_PageIndexChanging" OnSelectedIndexChanged="gvMemberDetails_SelectedIndexChanged">
                                <Columns>
                                   <%-- <asp:CommandField ShowSelectButton="true" ControlStyle-Width="40px" ItemStyle-Width="40px"
                                        SelectText="<img src='NotificationIcons/arrow_top_right-128.png' alt='Followup'/>" />--%>
                                     <asp:CommandField ShowSelectButton="true" ControlStyle-Width="20px" ItemStyle-Width="20px"
                                        SelectImageUrl="~/NotificationIcons/arrow_top_right-128.png" SelectText="Followup" ButtonType="Image"/>

                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Member_ID1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFollowupMemberFollowup" runat="server" CausesValidation="false" OnCommand="btnFollowupMemberFollowup_Command" CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/arrow_top_right-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Followup" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reg Date" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRegDate" runat="server" Text='<%#Eval("RegDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRegDate" runat="server" Text='<%#Eval("RegDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Member ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("MemberID1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnNameMemberFollowup" runat="server" CausesValidation="false"
                                                CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnNameMemberFollowup_Command" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtEmail" runat="server" Text='<%#Eval("Email") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>

                            <asp:GridView ID="gvUpgradeDetails" runat="server" AutoGenerateColumns="false" Width="1000px"
                                DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvUpgradeDetails_PageIndexChanging" OnSelectedIndexChanged="gvUpgradeDetails_SelectedIndexChanged">
                                <Columns>
                                    <%--<asp:CommandField ShowSelectButton="true" ControlStyle-Width="40px" ItemStyle-Width="40px"
                                        SelectText="<img src='NotificationIcons/arrow_top_right-128.png' alt='Followup'/>" />--%>

                                     <asp:CommandField ShowSelectButton="true" ControlStyle-Width="20px" ItemStyle-Width="20px"
                                        SelectImageUrl="~/NotificationIcons/arrow_top_right-128.png" SelectText="Followup" ButtonType="Image"/>
                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Member_ID1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFollowupUpgrade" runat="server" CausesValidation="false" OnCommand="btnFollowupUpgrade_Command" CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/arrow_top_right-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Followup" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Member ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("MemberID1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnNameUpgrade" runat="server" CausesValidation="false"
                                                CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnNameUpgrade_Command" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Package" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPackage" runat="server" Text='<%#Eval("Package")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("Package")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtTotal" runat="server" Text='<%#Eval("Total") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPaid" runat="server" Text='<%#Eval("Paid") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPaid" runat="server" Text='<%#Eval("Paid") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtExecutive" runat="server" Text='<%#Eval("Executive") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblExecutive" runat="server" Text='<%#Eval("Executive") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>

                            <asp:GridView ID="gvEnquiry" runat="server" AutoGenerateColumns="false" DataKeyNames="Branch_AutoID" EmptyDataText="No record found."
                                Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView GridView1" GridLines="None" CellPadding="3" AllowPaging="True" PageSize="20"
                                OnPageIndexChanging="gvEnquiry_PageIndexChanging" Width="1900px" OnSelectedIndexChanged="gvEnquiry_SelectedIndexChanged">

                                <Columns>
                                   <%-- <asp:CommandField ShowSelectButton="true" ControlStyle-Width="40px" ItemStyle-Width="40px"
                                        SelectText="Followup"/>--%>
                                     <asp:CommandField ShowSelectButton="true" ControlStyle-Width="20px" ItemStyle-Width="20px"
                                        SelectImageUrl="~/NotificationIcons/arrow_top_right-128.png" SelectText="Followup" ButtonType="Image"/>

                                    <%--<asp:CommandField ShowSelectButton="true" SelectImageUrl="~/Images/icon.png" ButtonType="Image" SelectText="Select"  />--%>

                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Enq_ID1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditEnquiry" runat="server" CausesValidation="false" OnCommand="btnEditEnquiry_Command" CommandArgument='<%#Eval("Enq_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/edit.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDeleteEnquiry" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="40"
                                                CommandArgument='<%#Eval("Enq_AutoID")%>' OnCommand="btnDeleteEnquiry_Command"
                                                Style="background-image: url('../NotificationIcons/f-cross_256-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFollowupEnquiry" runat="server" CausesValidation="false" OnCommand="btnFollowupEnquiry_Command" CommandArgument='<%#Eval("Enq_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/arrow_top_right-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Followup" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAddMemberEnquiry" runat="server" CausesValidation="false" OnCommand="btnAddMemberEnquiry_Command" CommandArgument='<%#Eval("Enq_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/Add.png'); background-size: 100% 100%; padding-left: 10px; padding-top: 0px; padding-bottom: 2px;" ToolTip="Add Member" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField HeaderText="Enq.ID" DataField="Enq_ID1" HeaderStyle-HorizontalAlign="left" />
                                    <asp:TemplateField ItemStyle-Width="70px" ControlStyle-Width="70px">
                                        <HeaderTemplate>
                                            <b>Enquiry.Date</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("EnqDate","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="First Name" DataField="FName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Last Name" DataField="LName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Contact 1" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />

                                    <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Rating" DataField="Rating" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Contact 2" DataField="Contact2" HeaderStyle-HorizontalAlign="left" />
                                    <%--<asp:BoundField HeaderText="DOB" DataField="DOB" HeaderStyle-HorizontalAlign="left" />--%>
                                    <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <b>DateOfBirth</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("DOB","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="WhatsApp No" DataField="WhatsAppNo" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="left" />

                                    <asp:BoundField HeaderText="Enquiry Type" DataField="Enquiry_Type" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Enquiry For" DataField="Enquiry_For" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Source Of Enquiry" DataField="Source_Enquiry" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Reference Details" DataField="ReferenceDetails" HeaderStyle-HorizontalAlign="left" />
                                    <%--<asp:BoundField HeaderText="CallRespond" DataField="CallRespond" HeaderStyle-HorizontalAlign="left" />--%>

                                    <%--<asp:BoundField HeaderText="NextFollowupDate" DataField="NextFollowupDate" HeaderStyle-HorizontalAlign="left" />--%>
                                    <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <b>NextFollowupDate</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Time" DataField="Time1" HeaderStyle-HorizontalAlign="left" />
                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>

                            <asp:GridView ID="gvMeasurementDetails" runat="server" AutoGenerateColumns="false" Width="1000px"
                                DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvMeasurementDetails_PageIndexChanging" OnSelectedIndexChanged="gvMeasurementDetails_SelectedIndexChanged">
                                <Columns>
                                    <%--<asp:CommandField ShowSelectButton="true" ControlStyle-Width="40px" ItemStyle-Width="40px"
                                        SelectText="<img src='NotificationIcons/arrow_top_right-128.png' alt='Followup'/>" />--%>
                                     <asp:CommandField ShowSelectButton="true" ControlStyle-Width="20px" ItemStyle-Width="20px"
                                        SelectImageUrl="~/NotificationIcons/arrow_top_right-128.png" SelectText="Followup" ButtonType="Image"/>

                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Member_ID1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFollowupMeasurementFollowup" runat="server" CausesValidation="false" OnCommand="btnFollowupMeasurementFollowup_Command" CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="40"
                                                Style="background-image: url('../NotificationIcons/arrow_top_right-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Followup" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reg Date" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRegDate" runat="server" Text='<%#Eval("RegDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRegDate" runat="server" Text='<%#Eval("RegDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Member ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("MemberID1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnNameMemberFollowup" runat="server" CausesValidation="false"
                                                CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnNameMemberFollowup_Command" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtEmail" runat="server" Text='<%#Eval("Email") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcelExistingFollowup" />
            <asp:PostBackTrigger ControlID="btnExportToExcelPayment" />
            <asp:PostBackTrigger ControlID="btnExportToExcelMeasurement" />
            <asp:PostBackTrigger ControlID="btnExportToExcelMembership" />
            <asp:PostBackTrigger ControlID="btnExportToExcelMemberEnd" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
