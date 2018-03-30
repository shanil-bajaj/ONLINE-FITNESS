<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="NDOnlineGym_2017.SearchPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .gv-mid-list {
            list-style: none !important;
            padding: 0px 10px !important;
            font-size: 13px !important;
            margin-top: 5px;
        }

            .gv-mid-list li {
                list-style: none !important;
                margin: 0px !important;
                padding: 0px !important;
                font-size: 13px !important;
            }

        .Serach-navigation {
            float: left;
            padding: 0px;
            margin: 0px;
            list-style: none;
        }

            .Serach-navigation li {
                float: left;
            }

        .navigation-link {
            color: orangered;
            font-size: 13px;
            padding: 0px 7px;
            border-right: 1px solid orangered;
            float: left;
        }

            .navigation-link:hover {
                color: darkred;
            }

        .serach-img {
            width: 80px;
            height: 80px;
        }

            .serach-img:hover {
                margin-top: -5px;
                width: 85px;
                height: 85px;
                margin-left: -5px;
            }

        .gv-right-list {
            list-style: none !important;
            margin-top: 0px !important;
            padding: 0px 0px !important;
        }

            .gv-right-list li {
                list-style: none !important;
                margin-bottom: 3px !important;
                padding: 0px !important;
                font-size: 13px !important;
            }

                .gv-right-list li span {
                    padding-right: 5px !important;
                }

        .ActiveStatus {
            background-color: rgb(46, 128, 53);
            font-weight: bold;
            color: white;
            padding: 10px 30px;
        }

        .DeactiveStatus {
            background-color: rgb(202, 47, 71);
            font-weight: bold;
            color: white;
            padding: 10px 25px;
        }

        .typeMember {
            color: rgb(77, 37, 31);
            font-weight: bold;
            font-size: 14px;
        }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-name-header">
            <h3>Search Page
                 <%--<div class="navigation" >
                    <ul>
                        <li>Member Setting &nbsp; > &nbsp;</li>
                        <li>Member &nbsp; > &nbsp;</li>
                        <li>Search Page</li>
                    </ul>
                 </div>--%>
            </h3>

       
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <div class="sc">
            <div id="divNotFoundSearch" runat="server">
                <center><table style="margin-top:30px;">
                    <tr>
                        <td style="padding:10px;">
                            <a href="AddEnquiry.aspx"><asp:Image ID="Image1" runat="server" CssClass="serach-img" ImageUrl="../Images/images (1).jpg" ToolTip="Add Enquiry" />
                        </td>
                         <td style="padding:10px;">
                            <a href="AddMember.aspx"><asp:Image ID="Image2"  runat="server" CssClass="serach-img"  ImageUrl="../Images/download (2).jpg" ToolTip="Add Member" />
                        </td>
                         <td style="padding:10px;">
                            <a href="demoCourse.aspx"><asp:Image ID="Image3"  runat="server" CssClass="serach-img"  ImageUrl="../Images/images (9).jpg" ToolTip="Add Course" />
                        </td>
                         <td style="padding:10px;">
                            <a href=""><asp:Image ID="Image4"  runat="server" CssClass="serach-img"  ImageUrl="../Images/images (5).jpg"  ToolTip="POS" />
                        </td>
                         <td style="padding:10px;">
                            <a href=""><asp:Image ID="Image5"  runat="server" CssClass="serach-img"  ImageUrl="../Images/images (6).jpg" ToolTip="Appointment" />
                        </td>
                         <td style="padding:10px;">
                            <a href="BalancePayment.aspx"><asp:Image ID="Image6"  runat="server" CssClass="serach-img" ImageUrl="../Images/download (2).png" ToolTip="Balance Payment" />
                        </td>
                    </tr>
                </table></center>
            </div>

            

            <asp:Repeater ID="RepterDetails" runat="server" OnItemDataBound="RepterDetails_ItemDataBound" >
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <div id="divEnquiryFoundSearch" style="padding: 5px 10px; margin-bottom: 5px; border-bottom: 1px solid silver;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 12%;">
                                    <asp:HiddenField Value='<%# Eval("ImagePath") %>' ID="HiddenField1" runat="server" />
                            <asp:Image ID="imgMember" runat="server" Style="width: 90px; height: 90px; border-radius: 5px;" />
                                </td>
                                <td style="width: 33%;">
                                     <asp:LinkButton ID="lbtnMemberProfile" runat="server" style="background:none;border:none;text-decoration:none" CommandArgument='<%# Eval("ID") %>'
                                         OnCommand="lbtnMemberProfile_Command">
                                            <span style="font-size: 18px; font-weight: bold;color:gray;border:none;text-decoration:none"><%# Eval("Name") %></span>
                                     </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnEnquiry" runat="server" style="background:none;border:none;text-decoration:none" >
                                            <span style="font-size: 18px; font-weight: bold;color:gray;border:none;text-decoration:none"><%# Eval("Name") %></span>
                                     </asp:LinkButton>
                                    <ul class="gv-mid-list">

                                        <li style="margin-bottom: 3px !important;">
                                            <span class="icon" style="background-image: url('Icon/Call.png'); background-repeat: no-repeat; background-size: 100% 100%; padding: 3px 8px 3px 8px; margin-right: 10px;"></span>
                                            <span><%# Eval("Contact1") %></span>
                                        </li>
                                        <li style="margin-bottom: 5px !important;">
                                            <span class="icon" style="background-image: url('Icon/MailBox.png'); background-repeat: no-repeat; background-size: 100% 100%; padding: 2px 7px 2px 7px; margin-right: 10px;"></span>
                                            <span><%# Eval("Email") %></span>
                                        </li>
                                        <li style="margin-bottom: 5px !important;">
                                            <span class="icon" style="background-image: url('Icon/Address.png'); background-repeat: no-repeat; background-size: 100% 100%; padding: 2px 7px 2px 7px; margin-right: 10px;"></span>
                                            <span><%# Eval("Address") %></span>

                                        </li>
                                    </ul>

                                </td>
                                <td style="width: 25%;">
                                    <ul class="gv-right-list">
                                        <li>
                                            <asp:HiddenField Value='<%# Eval("ID1") %>' ID="HiddenField3" runat="server" />
                                            <strong style="font-size: 13px;">ID :</strong>
                                            <%--<span style=""><%# Eval("ID") %></span>--%>
                                            <asp:Label ID="lblID" style="font-weight:bold; font-size:15px" runat="server" Text='<%# Eval("ID1") %>' ></asp:Label>
                                        </li>
                                        <li>
                                            <span><strong>DOB :</strong></span>
                                            <span><%# Eval("DOB", "{0:dd-MM-yyyy}") %></span>
                                        </li>
                                        

                                        <li>
                                            <span><strong>Type :</strong></span>
                                            <asp:HiddenField Value='<%# Eval("Type") %>' ID="HiddenField2" runat="server" />
                                           <%-- <span id="type" runat="server" style=""><strong><%# Eval("Type") %></strong></span>--%>
                                            <asp:Label ID="lbltype" style="font-weight:bold; font-size:15px" runat="server" Text='<%# Eval("Type") %>' ></asp:Label>
                                        </li>

                                    </ul>
                                </td>
                                <td style="width: 8%" visible="false">
                                    <%--<div id="div2" runat="server" class="ActiveStatus">
                                <span>Dective</span>
                            </div>--%>
                                </td>
                                <td style="width: 10%" visible="false">
                                    <%-- <div id="div1" runat="server" class="ActiveStatus">Active</div>--%>
                                </td>
                            </tr>
                        </table>
                        <table style="margin-left: 120px;">
                            <tr>
                                <td style="">
                                    <ul class="Serach-navigation" style="">
                                    <asp:LinkButton ID="lnkBtnEditEnquiry" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnEditEnquiry_Command" runat="server" CssClass="navigation-link" Style="border-left: 1px solid orangered;">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnEditMember" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnEditMember_Command" runat="server" CssClass="navigation-link" >Edit</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnAddMember" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnAddMember_Command" runat="server" CssClass="navigation-link">Add Member</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnAssignPackage" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnAssignPackage_Command" runat="server" CssClass="navigation-link">Assign Package</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnEnquiryFollowup" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnEnquiryFollowup_Command" runat="server" CssClass="navigation-link">Followup</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnMemberFollowup" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnMemberFollowup_Command" runat="server" CssClass="navigation-link">Followup</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnBalance" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnBalance_Command" runat="server" CssClass="navigation-link">Balance</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnUpgrade" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnUpgrade_Command" runat="server" CssClass="navigation-link">Upgrade</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnTransfer" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnTransfer_Command" runat="server" CssClass="navigation-link">Transfer</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnFreezing" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnFreezing_Command" runat="server" CssClass="navigation-link">Freezing</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnAppointment" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnAppointment_Command" runat="server" CssClass="navigation-link">Appointment</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnAttendance" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnAttendance_Command" runat="server" CssClass="navigation-link">Attendance</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnPT" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnPT_Command" runat="server" CssClass="navigation-link">P.T</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnPOS" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnPOS_Command" runat="server" CssClass="navigation-link">POS</asp:LinkButton>

                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
                  </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
