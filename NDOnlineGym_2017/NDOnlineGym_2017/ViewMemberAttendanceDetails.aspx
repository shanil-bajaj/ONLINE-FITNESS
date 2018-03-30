<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ViewMemberAttendanceDetails.aspx.cs" Inherits="NDOnlineGym_2017.ViewMemberAttendanceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .GridView
        {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
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
                <div class="form-name-header">
                    <h3>Member Attendance Details 
           <div class="navigation">
               <ul>
                   <li>Attendance &nbsp; > &nbsp;</li>
                   <li>Member Attendance Details</li>
               </ul>
           </div>
                    </h3>
                </div>

                <div class="divForm">
                    <div class="form-panel">
                        <table style="width: 100%;">

                            <tr>
                                <td>
                                    <table style="margin-top: 25px">
                                        <tr>
                                            <th>Form Date</th>
                                            <th>To Date</th>
                                            <th>ID</th>
                                            <th>Name</th>
                                            <th>Attendance Status</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>

                                            <td>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtFromDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtToDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtID" runat="server" CssClass="txt" TabIndex="2" AutoPostBack="true" OnTextChanged="txtID_TextChanged"></asp:TextBox>
                                            </td>


                                            <td>
                                                <asp:DropDownList ID="ddlName" runat="server" CssClass="ddl" TabIndex="2" OnSelectedIndexChanged="ddlName_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="ddlAttendanceStatus" runat="server" CssClass="ddl" TabIndex="2" OnSelectedIndexChanged="ddlAttendanceStatus_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="All">All</asp:ListItem>
                                                    <asp:ListItem Value="P">P</asp:ListItem>
                                                    <asp:ListItem Value="E">E</asp:ListItem>
                                                    <asp:ListItem Value="B">B</asp:ListItem>
                                                    <asp:ListItem Value="OT">OT</asp:ListItem>
                                                    <asp:ListItem Value="BE">BE</asp:ListItem>
                                                    <asp:ListItem Value="FB">FB</asp:ListItem>
                                                    <asp:ListItem Value="F">F</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <center class="btn-section">
                                     
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearch_Click" UseSubmitBehavior="false" TabIndex="5" />
                                    <asp:Button ID="btnSearchByDateCategory" runat="server" Text="Search By Date & Category" OnClick="btnSearchByDateCategory_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClear_Click" />
<%--                                     <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="25"  />--%>
                                 </center>
                                </td>
                            </tr>
                        </table>


                    </div>
                </div>

                <div class="divForm" style="margin-top: 5px">
                    <div class="form-panel">
                        <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                            <div style="margin: 10px 0px 10px 10px">
                                <asp:Label ID="lblCountHead" runat="server" Text="Total Records = " Font-Bold="true" ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                            </div>
                            <asp:GridView ID="gvAttendanceDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                                Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                AllowPaging="True" PageSize="20">

                                <Columns>
                                    <%--    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="31"
                                            CommandArgument='<%#Eval("Attendance_AutoID")%>' Text="Delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Member_ID1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
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

                                    <asp:TemplateField HeaderText="AttndanceDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAttndanceDate" runat="server" Text='<%#Eval("AttndanceDate")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblAttndanceDate" runat="server" Text='<%#Eval("AttndanceDate")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AttndanceTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAttndanceTime" runat="server" Text='<%#Eval("AttndanceTime")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblAttndanceTime" runat="server" Text='<%#Eval("AttndanceTime")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AttndanceStatus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAttndanceStatus" runat="server" Text='<%#Eval("AttndanceStatus")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblAttndanceStatus" runat="server" Text='<%#Eval("AttndanceStatus")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CourseEndDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="txtCourseEndDate" runat="server" Text='<%#Eval("CourseEndDate","{0:dd-MM-yyyy}")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblCourseEndDate" runat="server" Text='<%#Eval("CourseEndDate","{0:dd-MM-yyyy}")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="NextPaymentDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
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
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
