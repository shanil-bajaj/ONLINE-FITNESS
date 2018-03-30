<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ActiveDeactiveMember.aspx.cs" Inherits="NDOnlineGym_2017.ActiveDeactiveMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <script src="JS/OfflineJavaScript.js"></script>

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>

    <style>
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

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ { /*color: #fff;*/
                padding: 5px 5px 5px 5px;
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
    <div class="sc">
        <div class="form-name-header">
            <h3>Active/Deactive
                 <div class="navigation">
                     <ul>
                         <li>Member Setting &nbsp; > &nbsp;</li>
                         <li>Status  &nbsp; > &nbsp;</li>
                         <li>Active/Deactive</li>
                     </ul>
                 </div>
            </h3>
        </div>

        <div class="divForm">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <%--SMS Detaicls--%>

                    <div class="form-header">
                        <h4>&#10148; Active/Deactive Member Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">


                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">From Date</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">To Date </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Member ID </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtMemberID" runat="server" CssClass="txt" OnTextChanged="txtMemberID_TextChanged" AutoPostBack="true" TabIndex="3"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>

                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Member Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtMemberName" runat="server" CssClass="txt" OnTextChanged="txtMemberName_TextChanged" AutoPostBack="true" TabIndex="4"></asp:TextBox>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Gender</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="ddl" OnTextChanged="ddlGender_TextChanged" AutoPostBack="true" TabIndex="5">
                                                    <asp:ListItem Value="0">--Select Status--</asp:ListItem>
                                                    <asp:ListItem Value="1">All</asp:ListItem>
                                                    <asp:ListItem Value="2">Male</asp:ListItem>
                                                    <asp:ListItem Value="3">Female</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl">Status</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" OnTextChanged="ddlStatus_TextChanged" AutoPostBack="true" TabIndex="6">
                                                    <asp:ListItem Value="0">--Select Status--</asp:ListItem>
                                                    <asp:ListItem Value="1">All</asp:ListItem>
                                                    <asp:ListItem Value="2">Active</asp:ListItem>
                                                    <asp:ListItem Value="3">Deactive</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl" style="font-weight: bold; font-size: 14px; margin-left: 85px;">Active</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:Label ID="lblActive" runat="server" Text="0" Style="color: green; font-size: 14px; font-weight: bold; margin-left: 10px;"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl" style="font-weight: bold; font-size: 14px; margin-left: 75px;">Deactive </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:Label ID="lblDeactive" runat="server" Text="0" Style="color: red; font-size: 14px; font-weight: bold; margin-left: 10px;"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl" style="font-weight: bold; font-size: 14px; margin-left: 99px;">Total </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:Label ID="lblTotal" runat="server" Text="0" Style="color: blue; font-size: 14px; font-weight: bold; margin-left: 10px;"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>

                        </table>
                        <%--End SMS others Details--%>
                        <center class="btn-section">
                <asp:Button ID="btnSerachByDate" runat="server" Text="Search By Date" CssClass="form-btn" ValidationGroup="a" OnClick="btnSerachByDate_Click" TabIndex="7"  />             
                 <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnClear_Click" TabIndex="8" />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" OnClick="btnExportToExcel_Click" TabIndex="9" />

              
             </center>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 20px;">

                        <asp:GridView ID="GVActiveDeactive" runat="server" AutoGenerateColumns="false"
                            EmptyDataText="No record found." Width="1000px" CellPadding="3"
                            Font-Size="11px" CssClass="GridView" PagerStyle-CssClass="pager" GridLines="None" AllowPaging="True" PageSize="20"
                            OnPageIndexChanging="GVActiveDeactive_PageIndexChanging" OnRowDataBound="GVActiveDeactive_RowDataBound">

                            <Columns>
                                <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" ItemStyle-Width="100px" />
                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnName" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Member_AutoID")%>'
                                            Text='<%#Eval("Name")%>' OnCommand="btnName_Command" Font-Underline="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Gender" DataField="Gender" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="CourseEnd Date" DataField="EndDate" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Balance" DataField="Balance" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Status" DataField="status" ItemStyle-Width="100px" />
                                <%--  <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />  --%>
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExportToExcel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

