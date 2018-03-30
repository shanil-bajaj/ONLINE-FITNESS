<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MemberNumericalAttendance.aspx.cs" Inherits="NDOnlineGym_2017.MemberNumericalAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <%-- <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>--%>
    <style>
        .GridView1 {
            width: max-content;
        }
         .hideGridColumn{
        display:none;
        }
        table td {
            padding-bottom: 10px;
        }

        input:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox {
            position: relative;
            z-index: 1;
            font-weight: normal;
            border-radius: 3px;
            box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5);
            padding: 5px 7px;
            color: #a94442;
            background-color: #f2dede;
            border: 1px solid #ebccd1;
        }

        .errorborder {
            border: 1px solid red;
        }

        .form-panel {
            width: 100%;
            padding: 0px;
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

        .remove {
            font-size: 18px;
            font-weight: bold;
        }


        .div-member-img {
            width: 150px;
            height: 150px;
            border: 1px solid black;
            float: left;
        }

        .img-member {
            width: 150px;
            height: 150px;
        }

        .div-member-info {
            width: 310px;
            height: auto;
            float: left;
            margin-left: 10px;
            padding: 3px 5px;
        }
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
           
            <div class="form-name-header">
                <h3>Member Numerical Attendance
            <div class="navigation">
                <ul>
                    <li>Member Setting &nbsp; > &nbsp;</li>
                    <li>Attendance  &nbsp; > &nbsp;</li>
                    <li>Member Numerical Attendance</li>
                </ul>
            </div>
                </h3>
            </div>
            <div class="divForm">
                <div class="form-header" id="formheader1">
                    <h4>&#10148;Member Numerical Attendance </h4>
                </div>
                <div class="form-panel" id="formpanel1">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 50%">
                                <div style="width:490px; height: 550px; padding: 5px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <div style="width: 100%; height: auto;">
                                                    <div class="div-member-img">
                                                        <asp:Image ID="ImgMember" runat="server" CssClass="img-member" ImageUrl="" />
                                                    </div>
                                                    <div class="div-member-info">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <div style="width: 100%; margin-top: 5px">
                                                                        <span>Member ID</span>
                                                                        <asp:TextBox ID="txtMemberID" runat="server" CssClass="inp-txt" TabIndex="1" OnTextChanged="txtMemberID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="width: 100%; margin-top: 5px">
                                                                        <span>Member Name</span>
                                                                        <asp:DropDownList ID="ddlMemberName" runat="server" CssClass="inp-txt" TabIndex="2" OnSelectedIndexChanged="ddlMemberName_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtTime" runat="server" CssClass="txtTime" TabIndex="3" TextMode="Time" Visible="false"></asp:TextBox>
                                                                    <asp:Button ID="btnPresent" runat="server" CssClass="form-btn" TabIndex="3" Text="Present" OnClick="btnPresent_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <div style="width: 470px; height: 300px; border: 1px solid black; overflow-x: auto">
                                                    <asp:GridView ID="gvCourseDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                                                        Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView1 GridView" GridLines="None" CellPadding="3"
                                                        AllowPaging="True" PageSize="20" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ReceiptID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Package" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtPackage" runat="server" Text='<%#Eval("Package") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("Package") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Session" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtSession" runat="server" Text='<%#Eval("Session") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblSession" runat="server" Text='<%#Eval("Session") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="StartDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="EndDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="MemberType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtMemberType" runat="server" Text='<%#Eval("MemberType") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblMemberType" runat="server" Text='<%#Eval("MemberType") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                                        <RowStyle Height="20px" />
                                                        <AlternatingRowStyle Height="20px" BackColor="White" />
                                                    </asp:GridView>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; text-align: center">
                                                <%--<asp:Button ID="Button1" runat="server" CssClass="form-btn" TabIndex="3" Text="Thumb Attendance" style="font-size:12.5px" />
                                        <asp:Button ID="Button2" runat="server" CssClass="form-btn" TabIndex="3" Text="Attendance Home" style="font-size:12.5px" />
                                        <asp:Button ID="Button3" runat="server" CssClass="form-btn" TabIndex="3" Text="Staff Attendance" style="font-size:12.5px" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 50%">
                                <div style="width: 490px; height: 550px; margin-top: 0px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <div style="width: 470px; height: 490px; border: 1px solid black; overflow-x: auto; overflow-y:auto">
                                                    <asp:GridView ID="gvAttendanceDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                                                        Font-Size="11px" PagerStyle-CssClass="pager" GridLines="None" CssClass="GridView GridView1" CellPadding="3"
                                                         OnRowDataBound="gvAttendanceDetails_RowDataBound"
                                                        AllowPaging="false" PageSize="20"  >

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="31"
                                                                        CommandArgument='<%#Eval("Attendance_AutoID")%>' OnCommand="btnDelete_Command"
                                                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MemberID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtMember_ID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblMember_ID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Att Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtAttndanceTime" runat="server" Text='<%#Eval("AttndanceTime")%>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblAttndanceTime" runat="server" Text='<%#Eval("AttndanceTime")%>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtCourseEndDate" runat="server" Text='<%#Eval("CourseEndDate","{0:dd-MM-yyyy}")%>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblCourseEndDate" runat="server" Text='<%#Eval("CourseEndDate","{0:dd-MM-yyyy}")%>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="NextPayDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtNextPaymentDate" runat="server" Text='<%#Eval("NextPaymentDate","{0:dd-MM-yyyy}")%>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblNextPaymentDate" runat="server" Text='<%#Eval("NextPaymentDate","{0:dd-MM-yyyy}")%>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField  HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmemautoid" runat="server" Text='<%#Eval("Member_AutoID") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblmemautoid" runat="server" Text='<%#Eval("Member_AutoID") %>' />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                                        <RowStyle Height="20px" />
                                                        <AlternatingRowStyle Height="20px" BackColor="White" />
                                                    </asp:GridView>
                                                     
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
             
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPresent" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
