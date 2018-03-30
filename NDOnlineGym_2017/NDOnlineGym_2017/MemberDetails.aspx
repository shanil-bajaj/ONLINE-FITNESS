<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MemberDetails.aspx.cs" Inherits="NDOnlineGym_2017.MemberDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script type="text/javascript">

        function deletefun() {
            return confirm('Are you sure you want to delete?')
        }

    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<%--<script type="text/javascript">
    var pageIndex = 1;
    var pageCount;
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            GetRecords();
        }
    });
    function GetRecords() {
        pageIndex++;
        if (pageIndex == 2 || pageIndex <= pageCount) {
            $("#loader").show();
            $.ajax({
                type: "POST",
                url: "MemberDetails.aspx/GetCustomers",
                data: '{pageIndex: ' + pageIndex + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
    }
    function OnSuccess(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        pageCount = parseInt(xml.find("PageCount").eq(0).find("PageCount").text());
        var customers = xml.find("MemberDetails");
        customers.each(function () {
            var customer = $(this);
            var table = $("#dvCustomers table").eq(0).clone(true);
            $(".name", table).html(customer.find("ContactName").text());
            $(".city", table).html(customer.find("City").text());
            $(".postal", table).html(customer.find("PostalCode").text());
            $(".country", table).html(customer.find("Country").text());
            $(".phone", table).html(customer.find("Phone").text());
            $(".fax", table).html(customer.find("Fax").text());
            $("#dvCustomers").append(table).append("<br />");
        });
        $("#loader").hide();
    }
</script>--%>

    <%--<script type="text/javascript">
        $(function () {
            $('#container').masonry({ // options 
                itemSelector: '.item', columnWidth: 240,
                isAnimated: true,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
        });

        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                alert("end of the page");
            }
        });
</script>--%>
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

        .sc {
            width: 1021px;
        }

        @media screen and (min-width: 1400px) {
            .sc {
                width: 1100px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sc">
        <div class="divForm">

            <div class="form-panel">
                <div class="form-name-header">
                    <h3>Member Details
                 <div class="navigation">
                     <ul>
                         <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                         <li>Member &nbsp; > &nbsp;</li>
                         <li>Member Details</li>
                     </ul>
                 </div>
                    </h3>
                </div>
                <table>
                    <tr>
                        <th><%--From Date--%></th>
                        <th><%--To Date--%></th>
                        <th></th>
                        <th>Category</th>
                        <th>Search by</th>
                        <th></th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1" Style="width: 140px" Visible="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2" Style="width: 140px" Visible="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" OnClick="btnSearch_Click" Text="Search" TabIndex="3" Visible="false"/>

                        </td>

                        <td>
                            <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="5">
                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                <asp:ListItem Value="Name">Name</asp:ListItem>
                                <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                <asp:ListItem Value="Gender">Gender</asp:ListItem>
                                <asp:ListItem Value="Status">Status</asp:ListItem>
                            </asp:DropDownList>

                        </td>

                        <td>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="True" TabIndex="7" OnTextChanged="txtSearch_TextChanged" 
                                AutoPostBack="true" Style="width: 150px"></asp:TextBox>
                        </td>


                        <td>
                            <asp:Button ID="btnDateWithCategory" runat="server" CssClass="form-btn" TabIndex="8" Text="Date with category" Visible="false" OnClick="btnDateWithCategory_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnClear" runat="server" CssClass="form-btn" OnClick="btnClear_Click" Text="Clear" TabIndex="4" />
                            <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="38" OnClick="btnExportToExcel_Click" />
                        </td>
                    </tr>

                </table>
                <asp:GridView ID="gvMemberDetails" runat="server"></asp:GridView>
            </div>



        </div>
        
            <asp:Repeater ID="RepterDetails" runat="server" OnItemDataBound="RepterDetails_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                   
                    <div id="container">
                    <div id="divMemberFoundSearch" style="padding: 5px 10px; margin-bottom: 5px; border-bottom: 1px solid silver;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 12%;">
                                    <asp:HiddenField Value='<%# Eval("ImagePath") %>' ID="HiddenField1" runat="server" />
                                    <asp:Image ID="imgMember" runat="server" Style="width: 90px; height: 90px; border-radius: 5px;" />
                                    <%--<img alt="" src="/MemberPhoto/<%#Eval("ImagePath") %>" style="width:90px;height:90px;border-radius:5px;"/>--%>
                                    <%--                            <img alt="" src="Thumbnail.ashx?imgdata=<%# DataBinder.Eval(Container.DataItem, "ImagePath") %>" style="width:90px;height:90px;border-radius:5px;"/>--%>
                            
                                </td>
                                <td style="width: 33%;">
                                    <%--<asp:Label ID="lblMemberName" style="font-size:18px;font-weight:bold;" runat="server" <%# Eval("FName")%> ></asp:Label>--%>
                                    <%-- <span style="font-size: 18px; font-weight: bold;"><%# Eval("FName")%>&nbsp;<%# Eval("LName")%></span><ul class="gv-mid-list">--%>
                                    <asp:LinkButton ID="lbtnMemberProfile" runat="server" Style="background: none; border: none; text-decoration: none" CommandArgument='<%# Eval("ID") %>'
                                        OnCommand="lbtnMemberProfile_Command">
                                            <span style="font-size: 18px; font-weight: bold;color:gray;border:none;text-decoration:none"><%# Eval("Name") %></span>
                                    </asp:LinkButton>

                                    <ul style="list-style: none; padding: 0px; margin: 0px;">
                                        <li style="margin-bottom: 3px !important;">
                                            <span class="icon" style="background-image: url('Icon/Call.png'); background-repeat: no-repeat; background-size: 100% 100%; padding: 3px 8px 3px 8px; margin-right: 10px;"></span>
                                            <span><%# Eval("Contact1")%></span>
                                        </li>
                                        <li style="margin-bottom: 5px !important;">
                                            <span class="icon" style="background-image: url('Icon/MailBox.png'); background-repeat: no-repeat; background-size: 100% 100%; padding: 2px 7px 2px 7px; margin-right: 10px;"></span>
                                            <span><%# Eval("Email")%></span>
                                        </li>
                                        <li style="margin-bottom: 5px !important;">
                                            <span class="icon" style="background-image: url('Icon/Address.png'); background-repeat: no-repeat; background-size: 100% 100%; padding: 2px 7px 2px 7px; margin-right: 10px;"></span>
                                            <span><%# Eval("Address") %></span>

                                        </li>
                                        <%-- <li style="margin-left:25px;">
                                    <span><%# Eval("City") %>, <%# Eval("State") %></span>
                                </li>--%>
                                    </ul>

                                </td>
                                <td style="width: 25%;">
                                    <ul class="gv-right-list">
                                        <li>
                                            <strong style="font-size: 13px;">ID :</strong>
                                            <span style=""><%# Eval("Member_ID1") %></span>
                                        </li>
                                        <li>
                                            <span><strong>DOB :</strong></span>
                                            <span><%# Eval("DOB", "{0:dd-MM-yyyy}") %></span>
                                        </li>
                                        <%--   <li>
                                    <span><strong>Status :</strong></span>
                                    <asp:Label ID="Label1" runat="server" Text="Member" CssClass="ActiveStatus"></asp:Label>
                                </li>
                                        --%>
                                        <li>
                                            <strong style="font-size: 13px;">Registration Date :</strong>
                                            <span style=""><%# Eval("RegDate", "{0:dd-MM-yyyy}") %></span>
                                        </li>

                                        <li>
                                            <strong style="font-size: 13px;">Gender :</strong>
                                            <span style=""><%# Eval("Gender") %></span>
                                        </li>
                                        <%--<td style="width: 8%">--%>
                                        <li>
                                            <%--<span><%# Eval("Status")%></span>--%>
                                            <asp:HiddenField Value='<%# Eval("MembershipStatus") %>' ID="HiddenField3" runat="server" />
                                            <asp:Label ID="lblMembershipStatus" runat="server" Text='<%# Eval("MembershipStatus") %>' Visible="false"></asp:Label>
                                        </li>
                                </td>
                                </ul>
                        </td>
                        <td style="width: 8%">
                            <div id="divActiveDeactive" runat="server">
                                <%--<span><%# Eval("Status")%></span>--%>
                                <asp:HiddenField Value='<%# Eval("Status") %>' ID="HiddenField2" runat="server" />
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </div>
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
                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnEdit_Command" CssClass="navigation-link" Style="border-left: 1px solid orangered;">Edit</asp:LinkButton>
                                        <%-- <asp:LinkButton ID="lnkBtnRenew" runat="server" CommandArgument='<%# Eval("ID") %>' CssClass="navigation-link">Renew</asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkBtnAssignPackage" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnAssignPackage_Command" CssClass="navigation-link">Assign Package</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnMemberFollowup" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnMemberFollowup_Command" CssClass="navigation-link">Followup</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnBalance" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnBalance_Command" CssClass="navigation-link">Balance</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnUpgrade" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnUpgrade_Command" CssClass="navigation-link">Upgrade</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnTransfer" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnTransfer_Command" CssClass="navigation-link">Transfer</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnFreezing" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnFreezing_Command" CssClass="navigation-link">Freezing</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnAppointment" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnAppointment_Command" CssClass="navigation-link">Appointment</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnAttendance" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnAttendance_Command" CssClass="navigation-link">Attendance</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnPT" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnPT_Command" CssClass="navigation-link">P.T</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnPOS" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkBtnPOS_Command" CssClass="navigation-link">POS</asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete?')" OnCommand="lnkbtnDelete_Command" CssClass="navigation-link">Delete</asp:LinkButton>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                        </div>
                        
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
    </div>
    <img id="loader" alt=""  src="Images/loading.gif" style="display: none" />
</asp:Content>
