<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MemberProfile.aspx.cs" Inherits="NDOnlineGym_2017.MemberProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script>
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgMemberPhoto.ClientID%>').prop('src', e.target.result)
                 };
                 reader.readAsDataURL(input.files[0]);
                 }
             }
    </script>
    <script>
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgIDProof.ClientID%>').prop('src', e.target.result)
                 };
                 reader.readAsDataURL(input.files[0]);
                 }
             }
    </script>

    <script>

        function RestrictSpaceSpecial(e) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    </script>
    <style>
        .divTimeLine {
            padding: 0px 5px 5px 5px;
        }

        .timeLineProfile {
            height: 180px;
            width: 100%;
        }

        .ClientName {
        }

        .ClassActive {
            background: rgb(8, 187, 88);
            font-size: 20px;
            color: white;
            margin: 50px 0px 0px 40px;
            padding: 5px 10px;
            z-index: 1000;
            position: absolute;
        }

        .ClassDeactive {
            background: rgb(221, 49, 70);
            font-size: 20px;
            color: white;
            margin: 50px 0px 0px 40px;
            padding: 5px 10px;
            z-index: 1000;
            position: absolute;
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

        .divMemberInformation {
            padding: 5px;
            width: 1000px;
            margin-top: 40px;
        }

        .divHeader {
            width: 100%;
            height: 25px;
        }

        .btn-information-section {
            padding: 7px 10px;
            border: none;
            width: 25%;
            float: left;
            font-weight: bold;
            font-size: 14px;
            border-bottom: 1px solid black;
            background-color: silver;
        }

        .btn-information-section-selected {
            border-bottom: none;
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-top: 1px solid black;
            background-color: rgb(128, 128, 128);
        }

        .divContent {
            width: 100%;
            height: 300px;
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
        }

        .Information {
            width: 100%;
            font-size: 14px;
            padding-top: 30px;
            padding-left: 20px;
            padding-bottom: 30px;
        }

            .Information td {
                padding: 5px 10px;
            }

        .divFollowups {
            margin-top: 20px;
            padding: 10px;
        }

        input:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .d:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .GridView {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView a /** FOR THE PAGING ICONS  **/ {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView a:focus {
                    color: orangered;
                }

                .GridView a:hover {
                    color: orangered;
                }

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 1021px">
                <div class="divForm" style="margin-bottom: 10px; background-color: silver">
                    <div class="form-panel">

                        <table style="width: 100%;">
                            <tr>
                                <th style="width: 33%; text-align: center; font-size: 14px; color: black">Member ID </th>
                                <th style="width: 33%; text-align: center; font-size: 14px; color: black">Member Name</th>
                                <th style="width: 33%; text-align: center; font-size: 14px; color: black">Contact</th>
                            </tr>
                            <tr>
                                <td style="width: 33%; text-align: center">
                                    <asp:TextBox ID="txtMemberID" runat="server" OnTextChanged="txtMemberID_TextChanged" AutoPostBack="true" TabIndex="1" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                </td>
                                <td style="width: 33%; text-align: center">
                                    <asp:DropDownList ID="ddlMemberName" runat="server" Style="width: 180px" CssClass="d" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlMemberName_SelectedIndexChanged">
                                        <%--//  <asp:ListItem Value="--Select--">--Select--</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 33%; text-align: center">
                                    <asp:TextBox ID="txtContact" runat="server" OnTextChanged="txtContact_TextChanged" AutoPostBack="true" TabIndex="3" MaxLength="11" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="divForm">
                    <div class="form-panel">
                        <div class="divTimeLine">
                            <div class="timeLineProfile">
                                <asp:Image ID="Image1" CssClass="timeLineProfile" runat="server" ImageUrl="../Icon/Time-For-Exercise-Facebook-Cover1.png" Style="border-radius: 5px;" />


                            </div>
                            <asp:Label ID="lblMemberName" runat="server" CssClass="ClientName"></asp:Label>
                            <div style="height: 150px; width: 150px; border-radius: 5px; margin-top: -60px; margin-left: 25px;">
                                <asp:Image ID="ImgMemberPhoto" runat="server" ImageUrl="Icons/IdproofLogo.png"
                                    Style="background-size: 100%; height: 100px; width: 120px; border: 5px solid silver;"
                                    class="fileupload-preview thumbnail" />
                            </div>


                        </div>

                        <table style="margin-left: 280px; margin-top: -80px">
                            <tr>
                                <td style="">
                                    <ul class="Serach-navigation" style="">
                                        <%-- <li><a class="navigation-link" style="border-left:1px solid orangered;">Edit</a></li>
                                <li><a class="navigation-link" >Renew</a></li>
                                 <li><a class="navigation-link">Balance</a></li>
                                <li><a class="navigation-link">Upgrade</a></li>
                                <li><a class="navigation-link">Transfer</a></li>
                                <li><a class="navigation-link">Freezing</a></li>
                                <li><a class="navigation-link" >Follow up</a></li>
                                <li><a class="navigation-link">Appointment</a></li>
                                <li><a class="navigation-link">Attendance</a></li>
                                <li><a class="navigation-link">P.T</a></li>
                                <li><a class="navigation-link">POS</a></li>--%>

                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="navigation-link" Style="border-left: 1px solid orangered; height: 16px;" OnClick="lnkbtnEdit_Click">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnFollowup" runat="server" CssClass="navigation-link" OnClick="lnkBtnFollowup_Click">Followup</asp:LinkButton>
                                        <asp:LinkButton ID="lnkRenew" runat="server" CssClass="navigation-link" OnClick="lnkRenew_Click">Assign Package</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBalance" runat="server" CssClass="navigation-link" OnClick="lnkBalance_Click">Balance</asp:LinkButton>
                                        <asp:LinkButton ID="lnkUpgrade" runat="server" CssClass="navigation-link">Upgrade</asp:LinkButton>
                                        <asp:LinkButton ID="lnkTransfer" runat="server" CssClass="navigation-link">Transfer</asp:LinkButton>
                                        <asp:LinkButton ID="lnkFreezing" runat="server" CssClass="navigation-link" OnClick="lnkFreezing_Click">Freezing</asp:LinkButton>
                                        <asp:LinkButton ID="lnkappointment" runat="server" CssClass="navigation-link" OnClick="lnkappointment_Click">Appointment</asp:LinkButton>
                                        <asp:LinkButton ID="lnkAttendance" runat="server" CssClass="navigation-link" OnClick="lnkAttendance_Click">Attendance</asp:LinkButton>
                                        <asp:LinkButton ID="lnkPT" runat="server" CssClass="navigation-link">P.T</asp:LinkButton>
                                        <asp:LinkButton ID="lnkPOS" runat="server" CssClass="navigation-link">POS</asp:LinkButton>
                                    </ul>
                                </td>
                            </tr>
                        </table>

                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <td>
                                    <div class="divMemberInformation">
                                        <div class="divHeader">
                                            <asp:Button ID="btnInformation" runat="server" Text="Information" CssClass="btn-information-section btn-information-section-selected" OnClick="btnInformation_Click" />
                                            <asp:Button ID="btnCourse" runat="server" Text="Course" CssClass="btn-information-section" OnClick="btnCourse_Click" />
                                            <asp:Button ID="btnAccountDetails" runat="server" Text="Account Details" CssClass="btn-information-section" OnClick="btnAccountDetails_Click" />
                                            <asp:Button ID="btnIdProof" runat="server" Text="Id Proof" CssClass="btn-information-section" OnClick="btnIdProof_Click" />
                                        </div>
                                        <div class="divContent">
                                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                                <asp:View ID="View1" runat="server">
                                                    <div style="padding: 20px">
                                                        <fieldset>
                                                            <legend>Personal Information</legend>
                                                            <table style="" class="Information">
                                                                <tr>
                                                                    <td style="width: 33%">
                                                                        <strong style="">ID &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblMemberID" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>First Name &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblFirstN" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Last Name &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblLastN" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 33%">
                                                                        <strong style="">Contact &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblContact" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>DOB &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblDOB" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Reg Date &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblRegDate" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 33%">
                                                                        <strong style="">Address &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Email ID &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblEmailID" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>WhatsApp No &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblWhatsapp" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 33%">
                                                                        <strong style="">Gender &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Marital status &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblMAritalS" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Anniversary &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblanniversary" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 33%">
                                                                        <strong style="">Occupation &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblOccupation" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Health Details &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblHealthDetails" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Blood Group &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblBloodGroup" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 33%">
                                                                        <strong style="">Status &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>SMS Status &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblSMSstatus" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 33%">
                                                                        <strong>Block/Unblock &nbsp; : &nbsp;</strong>
                                                                        <asp:Label ID="lblBlackUn" runat="server"></asp:Label>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="View2" runat="server">
                                                    <div style="padding: 20px">
                                                        <fieldset>
                                                            <legend>Course</legend>
                                                            <div style="width: 900px; height: 220px; overflow-x: auto; overflow-y: auto;">

                                                                <asp:GridView ID="gvCourseDetails" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                                                    DataKeyNames="ReceiptID" EmptyDataText="No record found." Width="1100px" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView"
                                                                    AllowPaging="True" TabIndex="27" OnRowDataBound="gvCourseDetails_RowDataBound" OnPageIndexChanging="gvCourseDetails_PageIndexChanging" OnSelectedIndexChanged="gvCourseDetails_SelectedIndexChanged">

                                                                    <Columns>

                                                                        <asp:TemplateField ControlStyle-Width="5px" HeaderText="Preview">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnPreview" runat="server" CausesValidation="false" Text="Preview"
                                                                                    CommandArgument='<%#Bind("ReceiptID")%>' TabIndex="28" OnCommand="btnPreview_Command" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:BoundField HeaderText="Receipt ID " DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                                                            <HeaderTemplate>
                                                                                <b>Payment Date</b>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%#Eval("payDate","{0:dd-MM-yyyy}")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--  <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="100px" ControlStyle-Width="100px" >
                         <ItemTemplate> 
                             <%# Eval("payDate","{0:dd-MM-yyyy}") %>
                         </ItemTemplate>
                     </asp:TemplateField>--%>
                                                                        <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ItemTemplate>
                                                                                <%# Eval("StartDate","{0:dd-MM-yyyy}") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="End Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ItemTemplate>
                                                                                <%# Eval("EndDate","{0:dd-MM-yyyy}") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Course Member Type" DataField="CourseMemberType" HeaderStyle-HorizontalAlign="left" />

                                                                        <asp:BoundField HeaderText="No. of Member" DataField="Qty" HeaderStyle-HorizontalAlign="left" />

                                                                        <asp:BoundField HeaderText="Course Fees" DataField="Amount" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Discount" DataField="Discount" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Total Fees" DataField="FinalTotal" HeaderStyle-HorizontalAlign="left" />

                                                                        <asp:BoundField HeaderText="MemberType" DataField="MemberType" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />

                                                                    </Columns>

                                                                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                                                    <RowStyle Height="20px" />
                                                                    <AlternatingRowStyle Height="20px" BackColor="White" />

                                                                </asp:GridView>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="View3" runat="server">
                                                    <div style="padding: 20px">
                                                        <fieldset>
                                                            <legend>Account Details</legend>
                                                            <table style="width: 100%; margin-top: 5px">
                                                                <tr>
                                                                    <th style="width: 33%; text-align: center">Total Fees</th>
                                                                    <th style="width: 33%; text-align: center">Paid Fees</th>
                                                                    <th style="width: 33%; text-align: center">Balance Fees</th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 33%; text-align: center">
                                                                        <asp:TextBox ID="txtToatlFees" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 33%; text-align: center">
                                                                        <asp:TextBox ID="txtPaidFees" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 33%; text-align: center">
                                                                        <asp:TextBox ID="txtBalanceFees" runat="server" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="width: 900px; height: 180px; overflow-x: auto; overflow-y: auto; margin-top: 10px">
                                                                <asp:GridView ID="gvAccountDetails" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                                                    DataKeyNames="Bal_Auto" EmptyDataText="No record found." Width="900px" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView"
                                                                    AllowPaging="True" TabIndex="27" OnPageIndexChanging="gvAccountDetails_PageIndexChanging">
                                                                    <Columns>
                                                                        <%-- <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit" >
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" 
                                CommandArgument='<%#Eval("ID_Auto")%>' TabIndex="28" OnClick="btnEdit_Click1"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                                        <asp:BoundField HeaderText="Receipt ID " DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                                                            <HeaderTemplate>
                                                                                <b>Payment Date</b>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%#Eval("payDate","{0:dd-MM-yyyy}")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Total Fees" DataField="TotalFeeDue" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Paid Fees" DataField="Paid" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Balance Fees" DataField="Balance" HeaderStyle-HorizontalAlign="left" />
                                                                        <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />

                                                                        <asp:TemplateField HeaderText="Next Payment Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                                                            <ItemTemplate>
                                                                                <%# Eval("NextBalDate","{0:dd-MM-yyyy}") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>

                                                                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                                                    <RowStyle Height="20px" />
                                                                    <AlternatingRowStyle Height="20px" BackColor="White" />


                                                                </asp:GridView>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="View4" runat="server">
                                                    <div style="padding: 20px">
                                                        <fieldset>
                                                            <legend>ID Proof</legend>

                                                            <div id="IDProof" class="tab-pane fade">
                                                                <ul style="list-style: none; padding: 0; margin: 0;">
                                                                    <li class="row-full-ulImgButton" style="padding: 20px;">
                                                                        <asp:Image ID="imgIDProof" runat="server" AlternateText="ID Proof" ImageUrl="Icons/IdproofLogo.png" />
                                                                    </li>
                                                                </ul>
                                                                <span><strong>ID Proof :</strong></span>
                                                                <asp:Label ID="lblIDProof" runat="server"></asp:Label>
                                                            </div>

                                                        </fieldset>

                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <%--Followup section--%>

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
                                <%-- <td>
                                <asp:DropDownList ID="ddlExecutive" runat="server" Style="width: 180px; padding: 3px 5px;" CssClass="ddl" TabIndex="8">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>

                            </td>--%>
                                <td>
                                    <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" AutoPostBack="true" />
                                    <asp:DropDownList ID="ddlExecutive" runat="server" TabIndex="18" Enabled="false" Style="width: 150px; padding: 3px">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCallPesponse" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="9">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Busy">Busy</asp:ListItem>
                                        <asp:ListItem Value="CallNotReceived">Call Not Received</asp:ListItem>
                                        <asp:ListItem Value="NotReachable">Not Reachable</asp:ListItem>
                                        <asp:ListItem Value="WrongNumber">Wrong Number</asp:ListItem>
                                        <asp:ListItem Value="NotInService">Not In Service</asp:ListItem>
                                        <asp:ListItem Value="Divert">Divert</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRating" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="10">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                        <asp:ListItem Value="Cold">Cold</asp:ListItem>
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
                                    <asp:Label ID="lblFollowupDateTime" runat="server" Text="" Style="color: red; font-size: 13px"></asp:Label>
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
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a"  TabIndex="15" OnClick="btnSave_Click"  />
                 <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" ValidationGroup="a" TabIndex="16" OnClick="btnClear_Click" />
             </center>
                    </div>

                    <%--End Followup section--%>
                </div>
                <div class="divForm">
                    <div class="form-header">
                        <h4>&#10148;Follow up </h4>
                    </div>
                    <asp:Repeater ID="RepterDetails" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="divFollowups">
                                <div style="width: 100%; padding: 5px 10px; background-color: gray; color: white; font-size: 20px">
                                    <span id="lblFollowupDate" runat="server"><%# Eval("FollowupDate", "{0:ddd , dd-MM-yyyy}") %>&nbsp;&nbsp<span><%# Eval("Name") %></span></span><%-- Mon 23 sept 2017--%>
                                </div>
                                <table style="width: 100%; padding: 10px; border-left: 1px solid gray; border-right: 1px solid gray; border-top: 1px solid gray; border-bottom: none;">
                                    <tr>
                                        <th style="width: 20%">Call Response</th>
                                        <th style="width: 20%">Rating</th>
                                        <th style="width: 20%">Executive</th>
                                        <th style="width: 20%">Next Followup Date & Time</th>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%"><span><%# Eval("CallResponse") %></span></td>
                                        <td style="width: 20%"><span><%# Eval("Rating") %></span></td>
                                        <td style="width: 20%"><span><%# Eval("staff") %></span></td>
                                        <td style="width: 20%"><span><%# Eval("NextFollowupDate", "{0:dd-MM-yyyy}") %></span>&nbsp;<span><%# Eval("NextFollowupTime") %></span></td>
                                    </tr>

                                </table>
                                <table style="width: 100%; padding: 10px; border-left: 1px solid gray; border-right: 1px solid gray; border-bottom: 1px solid gray; border-top: none;">
                                    <tr>
                                        <td>
                                            <strong>Comment</strong> : <span><%# Eval("Comment") %></span>
                                        </td>

                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvCourseDetails" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
