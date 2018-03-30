<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="GroupSMS.aspx.cs" Inherits="NDOnlineGym_2017.GroupSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
            <div class="form-name-header">
                <h3>Send Group SMS
                    <div class="navigation">
                        <ul>
                            <li>File &nbsp; > &nbsp;</li>
                            <li>SMS  &nbsp; > &nbsp;</li>
                            <li>Group SMS</li>
                        </ul>
                    </div>
                    <h3></h3>
                    <h3></h3>
                </h3>
            </div>

            <div class="divForm">
                <div class="form-header">
                    <h4>&#10148; Send Group SMS</h4>
                </div>

                <div class="form-panel">
                    <table style="width: 100%;">
                        <tr>

                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnEnquiry" runat="server" Text="Enquiry" CssClass="form-btn" ValidationGroup="a" TabIndex="2" width="250px" 
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnEnquiry_Click"/>
                                </center>
                            </td>


                            <td class="cols">
                                <center>
                                   <asp:Button ID="btnEndBefore5Days" runat="server" Text="End Date Before 5 Days" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px" 
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnEndBefore5Days_Click" />
                                </center>
                            </td>


                        </tr>

                        <tr>

                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnRegistration" runat="server" Text="Registration" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnRegistration_Click" />
                                </center>
                            </td>


                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnEndBefore4Days" runat="server" Text="End Date Before 4 Days" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"  
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnEndBefore4Days_Click" />
                                </center>
                            </td>


                        </tr>

                        <tr>

                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnUpgrade" runat="server" Text="Upgrade" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" />
                                </center>
                            </td>


                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnEndBefore3Days" runat="server" Text="End Date Before 3 Days" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnEndBefore3Days_Click"  />
                                </center>
                            </td>


                        </tr>

                        <tr>

                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnBalReminder" runat="server" Text="Today's Balance Reminder" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnBalReminder_Click" />
                                </center>
                            </td>


                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnEndBefore2Days" runat="server" Text="End Date Before 2 Days" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnEndBefore2Days_Click" />
                                </center>
                            </td>


                        </tr>

                        <tr>

                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnEnquiryFllowup" runat="server" Text="Enquiry Followup" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnEnquiryFllowup_Click" />
                                </center>
                            </td>


                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnEndBefore1Days" runat="server" Text="End Date Before 1 Days" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnEndBefore1Days_Click" />
                                </center>
                            </td>


                        </tr>

                        <tr>

                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnTodayBirthday" runat="server" Text="Today's Birthday" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnTodayBirthday_Click" />
                                </center>
                            </td>


                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnTodayEndDate" runat="server" Text="Today's End Date" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px" 
                                       OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnTodayEndDate_Click" />
                                </center>
                            </td>


                        </tr>

                        <tr>

                            <td class="cols">
                                <center>
                                    <asp:Button ID="btnTodayAnniversary" runat="server" Text="Today's Anniversary" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                        OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btnTodayAnniversary_Click" />
                                </center>
                            </td>


                            <td class="cols">
                                <center>
                                    <asp:Button ID="btntodayBalPaid" runat="server" Text="Today's Balance Paid" CssClass="form-btn"  ValidationGroup="a" TabIndex="2" width="250px"
                                        OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" OnClick="btntodayBalPaid_Click" />
                                </center>
                            </td>


                        </tr>


                    </table>

                </div>

            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
