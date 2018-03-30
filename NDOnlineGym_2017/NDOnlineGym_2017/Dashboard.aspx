<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="NDOnlineGym_2017.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>

    <script type="text/javascript">

        function openPopup() {

            window.open("Followup.aspx", "_blank", "WIDTH=1080,HEIGHT=790,scrollbars=no, menubar=no,resizable=yes,directories=no,location=no");

        }
        //<![CDATA[    
        //
    </script>


    <script language="JavaScript" type="text/javascript">
        function CloseAndRefresh() {

            opener.location.reload(true);
            self.close();
        }
    </script>



    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .hideGridColumn{
        display:none;
        }
        .modalBackground {
            background-color: gray;
        }

        .outer-div {
            width: 135px;
            height: 70px;
            border: 1px solid gray;
        }

            .outer-div:hover {
                width: 140px;
                height: 75px;
                margin: -3px -3px;
                border-radius: 2px;
                border: 2px solid orangered;
            }

                .outer-div:hover .btnCnt {
                    color: orangered;
                }

                .outer-div:hover .div-lable {
                    color: orangered;
                }

        .inner-div-bar {
            width: 5px;
            height: 69px;
            background-color: rgb(29, 76, 81);
            float: left;
        }

        .inner-div {
            width: 125px;
            height: 69px;
            padding: 10px;
        }

        .div-lable {
            font-size: 11px;
            font-weight: bold;
            margin-top: -30px;
            margin-left: 35px;
        }

        .btnCnt {
            border: none;
            background-color: white;
            margin-top: 7px;
            font-size: 19px;
        }

        .divNotification {
            width: 100%;
            height: 300px;
            border: 1px solid silver;
            margin-bottom: 10px;
            padding: 5px;
            box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.30);
        }

        .btnCount {
            color: white;
            font-size: 40px;
            background: none;
            border: none;
            margin-top: -10px;
            text-decoration: none;
        }

        .lblNotification {
            color: white;
            font-size: 12px;
        }

        .divLable {
            width: 131px;
            height: 25px;
            float: left;
            margin-top: 20px;
        }

        .divImage {
            width: 25px;
            height: 35px;
            float: left;
            margin-top: 10px;
            margin-left: 5px;
            margin-right: 5px;
        }

        .notification {
            width: 168px;
            height: 90px;
            float: left;
        }

        #div1 {
            border: 1px solid rgb(39, 169, 227);
            background-color: rgb(39, 169, 227);
        }

        #div2 {
            border: 1px solid rgb(40, 183, 121);
            background-color: rgb(40, 183, 121);
        }

        #div3 {
            border: 1px solid rgb(255, 184, 72);
            background-color: rgb(255, 184, 72);
        }

        #div4 {
            border: 1px solid rgb(218, 84, 46);
            background-color: rgb(218, 84, 46);
        }

        #div5 {
            border: 1px solid rgb(34, 85, 164);
            background-color: rgb(34, 85, 164);
        }

        #div6 {
            border: 1px solid rgb(208, 99, 52);
            background-color: rgb(208, 99, 52);
        }

        #div7 {
            border: 1px solid rgb(49, 169, 169);
            background-color: rgb(49, 169, 169);
        }

        #div8 {
            border: 1px solid rgb(104, 150, 112);
            background-color: rgb(104, 150, 112);
        }

        #div9 {
            border: 1px solid rgb(48, 101, 141);
            background-color: rgb(48, 101, 141);
        }

        #div10 {
            border: 1px solid rgb(247, 77, 77);
            background-color: rgb(247, 77, 77);
        }

        #div11 {
            border: 1px solid rgb(170, 181, 33);
            background-color: rgb(170, 181, 33);
        }

        #div12 {
            border: 1px solid rgb(107, 65, 116);
            background-color: rgb(107, 65, 116);
        }

            #div1:hover, #div2:hover, #div3:hover, #div4:hover, #div5:hover, #div6:hover,
            #div7:hover, #div8:hover, #div9:hover, #div10:hover, #div11:hover, #div12:hover {
                background-color: rgb(46, 54, 63);
                border: 1px solid rgb(46, 54, 63);
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

        .btn-Close {
            float: right;
            font-size: 20px;
            background-color: whitesmoke;
            border: none;
            margin-top: -5px;
            height: 20px;
        }

            .btn-Close:hover {
                color: red;
                cursor: pointer;
            }

                    .HideGridColumn
        {
            display: none;
        }






        .bubble {
            background-color: #F2F2F2;
            border-radius: 5px;
            box-shadow: 0 0 6px #B2B2B2;
            display: inline-block;
            padding: 10px 18px;
            position: relative;
            vertical-align: top;
            width: 380px;
            height: auto;
            margin-top: 10px;
            margin-left: 20px;
        }

            .bubble::before {
                background-color: #F2F2F2;
                content: "\00a0";
                display: block;
                height: 16px;
                position: absolute;
                top: 11px;
                transform: rotate( 29deg ) skew( -35deg );
                -moz-transform: rotate( 29deg ) skew( -35deg );
                -ms-transform: rotate( 29deg ) skew( -35deg );
                -o-transform: rotate( 29deg ) skew( -35deg );
                -webkit-transform: rotate( 29deg ) skew( -35deg );
                width: 20px;
            }

        .divMsgEven {
            width: 15% !important;
            padding: 5px;
            float: left;
        }

        .divMsgOdd {
            width: 15% !important;
            padding: 5px;
            float: right;
        }

        .chat-even {
            float: left;
            margin: 5px 45px 5px 20px;
        }

            .chat-even::before {
                box-shadow: -2px 2px 2px 0 rgba( 178, 178, 178, .4 );
                left: -9px;
            }

        .chat-odd {
            float: right;
            margin: 5px 20px 5px 45px;
        }

            .chat-odd::before {
                box-shadow: 2px -2px 2px 0 rgba( 178, 178, 178, .4 );
                right: -9px;
            }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $(document).keydown(function (e) {
                // ESCAPE key pressed
                if (e.keyCode == 27) {
                    //$("#tblNotfication").hide();
                    $(".tblNotfication").hide();
                    // window.close();
                }
            });

        });

    </script>



        <script type="text/javascript">

            function checkIDAvailability() {
                $.ajax({
                    type: "POST",
                    url: "Dashboard.aspx?Branch_AutoID=1/checkUserName",
                    data: "{gv: '" + $("#<%=GVNotification.ClientID %>") + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onSuccess,
                failure: function (AjaxResponse) {
                    document.getElementById("Label2").innerHTML = "Dfgdfg";
                }
            });
            function onSuccess(AjaxResponse) {

                var Con = document.getElementById('<%= GVNotification.ClientID %>');
                toastr.error('Contact already exists !!!', 'Error');
                Con.focus();
                //document.getElementById("Label2").innerHTML = AjaxResponse.d;
            }
            }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div id="DivMain" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <table style="width: 100%">
                    <tr>
                        <td>

                            <table style="padding: 3px; width: 100%">
                                <tr>
                                    <td>
                                        <div id="div1" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/353_-_Cake-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label runat="server" CssClass="lblNotification"> Member Birthday</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
<%--                                                <center><asp:LinkButton ID="btnCntMemberBday" runat="server" CssClass="btnCount" Text="10" OnClick="checkIDAvailability()" /></center>--%>
                                                <center><asp:LinkButton ID="btnCntMemberBday" runat="server" CssClass="btnCount" Text="10" OnClick="btnCntMemberBday_Click" /></center>
                                            </div>

                                        </div>
                                    </td>
                                    <td>
                                        <div id="div2" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image1" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/birthday_cake-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label2" runat="server" CssClass="lblNotification"> Staff Birthday</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntStaffBday" runat="server" CssClass="btnCount" OnClick="btnCntStaffBday_Click" Text="5" /></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div3" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image2" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/33-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label1" runat="server" CssClass="lblNotification">Anniversary</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntAnniversary" runat="server" CssClass="btnCount" OnClick="btnCntAnniversary_Click" Text="5" /></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div4" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image3" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/Noun_Project_20Icon_10px_grid-20-2-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label3" runat="server" CssClass="lblNotification">Today's Enquiry</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntEnq" runat="server" CssClass="btnCount" OnClick="btnCntEnq_Click" Text="5" /></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div5" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image4" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/99-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label4" runat="server" CssClass="lblNotification">Enquiry Followup</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntEnqFolw" runat="server" CssClass="btnCount" OnClick="btnCntEnqFolw_Click" Text="5" /></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div6" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image5" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/add-user-2-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label runat="server" CssClass="lblNotification">Today's Admission</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntAddmission" runat="server" CssClass="btnCount" OnClick="btnCntAddmission_Click" Text="10" /></center>
                                            </div>

                                        </div>
                                    </td>
                                </tr>

                                <tr>

                                    <td>
                                        <div id="div7" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image6" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/user_active-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label6" runat="server" CssClass="lblNotification">Active Member</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntActiveMem" runat="server" CssClass="btnCount" OnClick="btnCntActiveMem_Click" Text="5" /></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div8" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image7" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/General_Office_36-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label7" runat="server" CssClass="lblNotification">Deactive Member</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <%--                                       <center><asp:Button ID="Button66" runat="server" CssClass="btnCount" OnClick="Button6_Click Text="5" /></center>--%>
                                                <center><asp:LinkButton ID="btnCntDeactiveMem" runat="server" CssClass="btnCount" OnClick="btnCntDeactiveMem_Click" /></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div9" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image8" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/date-time-calendar-stopwatch-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label8" runat="server" CssClass="lblNotification">M.ship End Date</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntMemberEnd" runat="server" CssClass="btnCount" Text="5" OnClick="btnCntMemberEnd_Click" /></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div10" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image9" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/credit-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label9" runat="server" CssClass="lblNotification">Payment Date</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntPaymentDate" runat="server" CssClass="btnCount" Text="5" onclick="btnCntPaymentDate_Click"/></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div11" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image10" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/1-15-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label5" runat="server" CssClass="lblNotification">Post Dated Cheque</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntPDC" runat="server" CssClass="btnCount" Text="5" onclick="btnCntPDC_Click"/></center>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="div12" style="" class="notification">
                                            <div class="divImage">
                                                <asp:Image ID="Image11" runat="server" Style="width: 35px; height: 35px;" ImageUrl="../NotificationIcons/Elevator-128.png" />
                                            </div>
                                            <div class="divLable">
                                                <center><asp:Label ID="Label10" runat="server" CssClass="lblNotification">Other Followup</asp:Label></center>
                                            </div>
                                            <div style="width: 168px">
                                                <center><asp:LinkButton ID="btnCntOtherFollowp" runat="server" CssClass="btnCount" OnClick="btnCntOtherFollowp_Click" Text="5" /></center>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <table id="tblNotfication" class="tblNotfication" style="width: 100%;" runat="server" visible="false">
                    <tr>
                        <td>

                            <div runat="server" style="width: 100%; height: 25px; border: 1px solid silver; margin-top: 10px; margin-right: 10px; padding: 5px; box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.25); background-color: whitesmoke; color: black; font-weight: bold;">
                                <span id="lblnotfNm" runat="server" style="float: left">Notification Details</span>
                                <asp:Button ID="btnClose" runat="server" CssClass="btn-Close" Text="&times;" OnClick="btnClose_Click"></asp:Button>
                            </div>
                            <div id="divNotification" runat="server" class="divNotification" visible="false">

                                <div style="width: 1020px; height: 290px; overflow-y: scroll; overflow-x: scroll; border: 1px solid silver;">
                                    <asp:GridView ID="GVNotification" runat="server" Width="1000px"
                                        DataKeyNames="ID" Font-Size="13px" PagerStyle-CssClass="pager" EmptyDataText="No Record Found" CssClass="GridView" GridLines="None" CellPadding="5"
                                        AllowPaging="false">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Member Information" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="false" Text="Information" OnCommand="btnAdd_Command1"
                                                        CommandArgument='<%#Eval("ID")%>' TabIndex="5" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="WhiteSmoke" />
                                    </asp:GridView>

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
                                    <asp:GridView ID="GridFollowup" runat="server" Width="1000px"
                                        DataKeyNames="ID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView"
                                        Visible="false" EmptyDataText="No Record Found." GridLines="None" CellPadding="5"
                                        OnSelectedIndexChanged="GridFollowup_SelectedIndexChanged">

                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" HeaderText="Followup" SelectText="Followup"   />
                                            <asp:TemplateField HeaderText="Followup" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="false" Width="100px" 
                                                        Text="Followup" OnCommand="btnAdd_Command" CommandArgument='<%#Eval("ID")%>' TabIndex="5" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID" HeaderStyle-CssClass="HideGridColumn" ItemStyle-CssClass="HideGridColumn">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="WhiteSmoke" />
                                    </asp:GridView>
                                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="1000px">
                                        <div style="background-color:white">
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
                                                        <asp:GridView ID="gvFollowupDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Followup_AutoID" CssClass="GridView" PagerStyle-CssClass="pager"
                                                            AllowPaging="True" GridLines="None" OnPageIndexChanging="gvFollowupDetails_PageIndexChanging" PageSize="20" Width="1000px">
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
                                        <div style="background-color:white">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="sc">
                                                    <div class="form-name-header">
                                                        <h3>Enquiry Followup</h3>
                                                    </div>
                                                    <%--End Other Details--%>
                                                    <div id="divEnquiryFollowupSection" runat="server">
                                                        <div class="divForm">
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
                                                        <div style="width: 1000px; height: 200px; overflow-x: scroll; overflow-y: scroll; margin-top: 10px;">
                                                            <asp:GridView ID="gvEnqFollowup" runat="server" AutoGenerateColumns="false" Width="1000px"
                                                                DataKeyNames="EnqFollowup_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" OnPageIndexChanging="gvEnqFollowup_PageIndexChanging"
                                                                AllowPaging="True" PageSize="20">
                                                                <Columns>
                                                                    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
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
                                                                    </asp:TemplateField>
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
                                                                    <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="100px" />
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

                                    <asp:GridView ID="gvEnquiry" runat="server" Width="1000px"
                                        DataKeyNames="ID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" Visible="false"
                                        EmptyDataText="No Record Found." GridLines="None" CellPadding="5">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDetails" runat="server" CausesValidation="false" Width="100px" Text="Edit"
                                                        OnCommand="btnDetails_Command" CommandArgument='<%#Eval("ID")%>' TabIndex="5" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="WhiteSmoke" />
                                    </asp:GridView>

                                    <asp:GridView ID="gvChartInfo" runat="server" Width="1000px"
                                        DataKeyNames="ID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" Visible="false"
                                        EmptyDataText="No Record Found." GridLines="None" CellPadding="5">

                                        <Columns>
                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="WhiteSmoke" />
                                    </asp:GridView>
                                    <asp:GridView ID="gvCollectionInfo" runat="server" Width="1000px"
                                        DataKeyNames="ID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" Visible="false"
                                        EmptyDataText="No Record Found." GridLines="None" CellPadding="5">

                                        <Columns>
                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="WhiteSmoke" />
                                    </asp:GridView>
                                    <asp:GridView ID="gvmembershipEnd" runat="server" Width="1000px"
                                        DataKeyNames="ID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" Visible="false"
                                        EmptyDataText="No Record Found." GridLines="None" CellPadding="5">

                                        <Columns>
                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="WhiteSmoke" />
                                    </asp:GridView>
                                    <asp:GridView ID="gvpresent" runat="server" Width="1000px"
                                        DataKeyNames="ID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" Visible="false"
                                        EmptyDataText="No Record Found." GridLines="None" CellPadding="5">

                                        <Columns>
                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="WhiteSmoke" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </td>
                    </tr>
                </table>


                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">
                            <div runat="server" style="width: 98%; height: 25px; border: 1px solid silver; margin-top: 10px; margin-right: 10px; padding: 5px; box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.25); background-color: rgb(116, 168, 125); color: white; font-weight: bold;">
                                Enquiry
                            </div>
                            <div runat="server" class="divNotification" style="width: 98%; height: 250px">
                                <span style="float: right; padding-right: 30px">Total Enquiry :
                                    <asp:Label ID="Label11" runat="server"></asp:Label></span>
                                <asp:Chart ID="Chart2" runat="server" Width="500px" Height="220px" OnClick="Chart2_Click">
                                    <Series>
                                        <asp:Series Name="enquiry" Legend="Legend1" MarkerColor="255, 192, 192" ToolTip="#VALX, #VAL" PostBackValue="#VALX" IsXValueIndexed="True"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisY IntervalAutoMode="VariableCount" Title="Enquiries">
                                                <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                            </AxisY>
                                            <AxisX IntervalAutoMode="VariableCount" Title="Days">
                                                <MajorGrid Interval="Auto" LineColor="White" LineDashStyle="Dash" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend Name="Legend1">
                                        </asp:Legend>
                                    </Legends>
                                </asp:Chart>
                            </div>
                        </td>
                        <td style="width: 50%">
                            <div id="Div13" runat="server" style="width: 98%; height: 25px; border: 1px solid silver; margin-top: 10px; margin-right: 10px; padding: 5px; box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.25); background-color: rgb(250, 147, 117); color: white; font-weight: bold;">
                                Members
                            </div>
                            <div id="Div14" runat="server" class="divNotification" style="width: 98%; height: 250px">
                                <span style="float: right; padding-right: 30px">Total Member :
                                    <asp:Label ID="Label12" runat="server"></asp:Label></span>
                                <asp:Chart ID="ChartMember" runat="server" Width="500px" Height="220px" OnClick="ChartMember_Click1">
                                    <Series>
                                        <asp:Series Name="Member" Legend="Legend1" MarkerColor="255, 192, 192" ToolTip="#VALX, #VAL" PostBackValue="#VALX" IsXValueIndexed="True"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisY IntervalAutoMode="VariableCount" Title="Members">
                                                <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                            </AxisY>
                                            <AxisX IntervalAutoMode="VariableCount" Title="Days">
                                                <MajorGrid Interval="Auto" LineColor="White" LineDashStyle="Dash" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend Name="Legend1">
                                        </asp:Legend>
                                    </Legends>
                                </asp:Chart>

                            </div>
                        </td>

                    </tr>
                </table>

                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">
                            <div id="Div15" runat="server" style="width: 98%; height: 25px; border: 1px solid silver; margin-top: 10px; margin-right: 10px; padding: 5px; box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.25); background-color: rgb(247, 77, 77); color: white; font-weight: bold;">
                                Collection
                            </div>
                            <div id="Div16" runat="server" class="divNotification" style="width: 98%; height: 250px">
                                <span style="float: right; padding-right: 30px">Collection :
                                    <asp:Label ID="Label13" runat="server"></asp:Label></span>
                                <asp:Chart ID="CollectionChart" runat="server" Width="500px" Height="220px" OnClick="CollectionChart_Click">
                                    <Series>
                                        <asp:Series Name="collection" Legend="Legend1" MarkerColor="255, 192, 192" ToolTip="#VALX, #VAL" PostBackValue="#VALX" IsXValueIndexed="True"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisY IntervalAutoMode="VariableCount" Title="Collection">
                                                <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                            </AxisY>
                                            <AxisX IntervalAutoMode="VariableCount" Title="Days">
                                                <MajorGrid Interval="Auto" LineColor="White" LineDashStyle="Dash" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend Name="Legend1">
                                        </asp:Legend>
                                    </Legends>
                                </asp:Chart>

                            </div>
                        </td>
                        <td style="width: 50%">
                            <div id="Div17" runat="server" style="width: 98%; height: 25px; border: 1px solid silver; margin-top: 10px; margin-right: 10px; padding: 5px; box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.25); background-color: rgb(97, 139, 206); color: white; font-weight: bold;">
                                Membership End Date
                            </div>
                            <div id="Div18" runat="server" class="divNotification" style="width: 98%; height: 250px">

                                <span style="float: right; padding-right: 30px">Total Membership End :
                                    <asp:Label ID="Label14" runat="server"></asp:Label></span>
                                <asp:Chart ID="ChartMemberEnd" runat="server" Width="500px" Height="220px" OnClick="ChartMemberEnd_Click">
                                    <Series>
                                        <asp:Series Name="MemberEnd" Legend="Legend1" MarkerColor="255, 192, 192" ToolTip="#VALX, #VAL" PostBackValue="#VALX" IsXValueIndexed="True"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisY IntervalAutoMode="VariableCount" Title="Membership End Date">
                                                <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                            </AxisY>
                                            <AxisX IntervalAutoMode="VariableCount" Title="Days">
                                                <MajorGrid Interval="Auto" LineColor="White" LineDashStyle="Dash" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend Name="Legend1">
                                        </asp:Legend>
                                    </Legends>
                                </asp:Chart>

                            </div>
                        </td>

                    </tr>
                </table>


                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%">
                            <div id="Div20" runat="server" style="width: 98%; height: 25px; border: 1px solid silver; margin-top: 10px; margin-right: 10px; padding: 5px; box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.25); background-color: rgb(255, 184, 72); color: white; font-weight: bold;">
                                Present Members
                            </div>
                            <div id="Div21" runat="server" class="divNotification" style="width: 98%; height: 400px">

                                <span style="float: right; padding-right: 30px">Today's Present Member :
                                    <asp:Label ID="Label15" runat="server"></asp:Label></span>
                                <asp:Chart ID="Chart1" runat="server" Width="500px" Height="220px" OnClick="Chart1_Click">
                                    <Series>
                                        <asp:Series Name="present" Legend="Legend1" MarkerColor="255, 192, 192" ToolTip="#VALX, #VAL" PostBackValue="#VALX" IsXValueIndexed="True"></asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisY IntervalAutoMode="VariableCount" Title="Members">
                                                <MajorGrid LineColor="White" LineDashStyle="Dash" />
                                            </AxisY>
                                            <AxisX IntervalAutoMode="VariableCount" Title="Days">
                                                <MajorGrid Interval="Auto" LineColor="White" LineDashStyle="Dash" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend Name="Legend1">
                                        </asp:Legend>
                                    </Legends>
                                </asp:Chart>

                            </div>
                        </td>
                        <td style="width: 50%">
                            <%--Staff Notification--%>
                            <div id="Div19" runat="server" style="width: 98%; height: 25px; border: 1px solid silver; margin-top: 10px; margin-right: 10px; padding: 5px; box-shadow: -2px 2px 10px 4px rgba(0, 0, 0, 0.25); background-color: rgb(208, 217, 87); color: white; font-weight: bold;">
                                <span id="Span1" runat="server" style="float: left">Staff Notification</span>

                            </div>
                            <div style="width: 98%; height: 400px; overflow-y: auto; overflow-x: hidden" class="divNotification">
                                <div style="width: 100%; float: left">


                                    <asp:Repeater ID="RepterDetails" runat="server" OnItemDataBound="RepterDetails_ItemDataBound">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="width: 100%; float: left;">

                                                <div id="divMsg" runat="server" class="divMsgEven">
                                                    <div>

                                                        <asp:HiddenField Value='<%# Eval("ImagePath") %>' ID="HiddenField1" runat="server" />
                                                        <center><asp:Image ID="imgStaff" runat="server" ImageUrl="../Icon/user_male-black.png" Width="50" Height="50"
                                                Style="border-radius: 50px; background-color: silver;" /></center>
                                                    </div>
                                                    <div style="margin-top: -10px">
                                                        <center><h4><asp:Label ID="Label2" runat="server" Text='<%# Eval("StaffName")%>'> </asp:Label></h4></center>
                                                    </div>
                                                </div>
                                                <div style="width: 85% !important; padding: 5px; float: left">
                                                    <div id="divChat" runat="server" class="bubble chat-even">
                                                        <span id="spanNoti" runat="server" style="font-size: 13px;"><%# Eval("Notification")%></span>

                                                    </div>
                                                </div>
                                                <div style="float: right; padding-right: 15px;">
                                                    <strong><%# Eval("StaffName")%></strong><strong>&nbsp;&nbsp;,1.00 PM</strong>
                                                </div>

                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <%--Staff Notification--%>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
