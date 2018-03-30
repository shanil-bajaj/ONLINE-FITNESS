<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ThumbAddMember.aspx.cs" Inherits="NDOnlineGym_2017.ThumbAddMember" ValidateRequest="false" EnableEventValidation="false" %>

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
        <table>
            <tr>

                <td>
                    <table>
                        <tr>
                            <td>MemberID </td>

                            <td>
                                <asp:TextBox ID="txtMemberID_AddMember" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Member Name </td>

                            <td>
                                <asp:TextBox ID="txtMemberName_AddMember" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Card Number</td>

                            <td>
                                <asp:TextBox ID="txtCardNumber_AddMember" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>Serial Number</td>

                            <td>
                                <asp:TextBox ID="txtSerialno_AddMember" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td></td>

                            <td>
                                <asp:Button ID="btnAddMember" runat="server" Text="AddMember" OnClick="btnAddMember_Click" /></td>

                        </tr>
                    </table>
                </td>

                <%--  <td>
                    <table>
                        <tr>
                            <td>MemberID </td>

                            <td>
                                <asp:TextBox ID="txtMemberID_DeleteMember" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                   
                        <tr>
                            <td>Serial Number</td>

                            <td>
                                <asp:TextBox ID="txtSerialNu_DeleteMember" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td></td>

                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="DeleteMember" OnClick="btnDelete_Click" /></td>

                        </tr>
                    </table>
                </td>--%>

                <%--        <td>
                    <table>
                        <tr>
                            <td>MemberID </td>

                            <td>
                                <asp:TextBox ID="txtMemberID" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Serial Number</td>

                            <td>
                                <asp:TextBox ID="txtSerialNumber_BlockMember" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>IsBlock</td>

                            <td>
                                <asp:TextBox ID="txtIsBlock" runat="server"></asp:TextBox></td>
                            <td> i.e : True / False</td>
                        </tr>

                        <tr>
                            <td></td>

                            <td>
                                <asp:Button ID="btnblock" runat="server" Text="BlockMember"  OnClick="btnblock_Click"/></td>

                        </tr>
                    </table>
                </td>--%>
            </tr>
        </table>

    </div>

    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 20px;">

        <asp:GridView ID="GVActiveDeactive" runat="server" AutoGenerateColumns="false"
            EmptyDataText="No record found." Width="1000px" CellPadding="3"
            Font-Size="11px" CssClass="GridView" PagerStyle-CssClass="pager" GridLines="None" AllowPaging="True" PageSize="20"
            OnPageIndexChanging="GVActiveDeactive_PageIndexChanging" OnRowDataBound="GVActiveDeactive_RowDataBound">

            <Columns>
                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add"  ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" TabIndex="70" OnClick="btnEdit_Click"
                            CommandArgument='<%#Eval("Member_ID1")%>' Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" />
                    </ItemTemplate>
                </asp:TemplateField>
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

    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 20px;">

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
            EmptyDataText="No record found." Width="1000px" CellPadding="3"
            Font-Size="11px" CssClass="GridView" PagerStyle-CssClass="pager" GridLines="None" AllowPaging="True" PageSize="20"
            OnPageIndexChanging="GVActiveDeactive_PageIndexChanging" OnRowDataBound="GVActiveDeactive_RowDataBound" OnRowDeleting="GridView1_RowDeleting">

            <Columns>
                  <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="_" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="left"
                                    ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" ItemStyle-Width="30px">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="remove" ForeColor="#CC3300" />
                                </asp:CommandField>
                <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" ItemStyle-Width="100px" />
          <%--      <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnName" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Member_AutoID")%>'
                            Text='<%#Eval("Name")%>' OnCommand="btnName_Command" Font-Underline="false" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:BoundField HeaderText="Name" DataField="Name" ItemStyle-Width="100px" />
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
        <div style="margin: 10px 0px 10px 10px">         
            <asp:Button ID="BtnAdd" runat="server" Text="Add Member" Font-Bold="true" ForeColor="Black" OnClick="BtnAdd_Click"/>
         
        </div>
    </div>
    <%-- </form>--%>
    <%-- </body>
    </html>--%>
</asp:Content>
