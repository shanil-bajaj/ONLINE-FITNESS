<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ThumbAddMemberAttendance.aspx.cs" Inherits="NDOnlineGym_2017.ThumbAddMemberAttendance" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="JS/Common-Script.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>--%>
    <%-- <form id="form1" runat="server">--%>
    <div>
        <div style="width: 470px; height: 490px; border: 1px solid black; overflow-x: auto; overflow-y: auto">
            <asp:GridView ID="gvAttendanceDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                Font-Size="11px" PagerStyle-CssClass="pager" GridLines="None" CssClass="GridView GridView1" CellPadding="3"
                OnRowDataBound="gvAttendanceDetails_RowDataBound"
                AllowPaging="false" PageSize="20">

                <Columns>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="31"
                                CommandArgument='<%#Eval("Attendance_AutoID")%>' OnCommand="btnDelete_Command"
                                Style="background-image: url('../NotificationIcons/f-cross_256-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Delete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="MemberID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:Label ID="txtMember_ID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblMember_ID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                        <ItemTemplate>
                            <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Label ID="txtAttndanceTime" runat="server" Text='<%#Eval("AttndanceTime")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblAttndanceTime" runat="server" Text='<%#Eval("AttndanceTime")%>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                   <%-- <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
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
                    </asp:TemplateField>--%>

                    <%--<asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtBalance" runat="server" Text='<%#Eval("Balance") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("Balance") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                        <ItemTemplate>
                            <asp:Label ID="lblmemautoid" runat="server" Text='<%#Eval("Member_AutoID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblmemautoid" runat="server" Text='<%#Eval("Member_AutoID") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                <RowStyle Height="20px" />
                <AlternatingRowStyle Height="20px" BackColor="White" />
            </asp:GridView>

        </div>
    </div>
    <%-- </form>--%>
    <%-- </body>
    </html>--%>
</asp:Content>
