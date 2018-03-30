<%@ Page Title="" Language="C#" MasterPageFile="~/PTMaster.Master" AutoEventWireup="true" CodeBehind="PTExistingAttendance.aspx.cs" Inherits="NDOnlineGym_2017.PTExistingAttendance" %>

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
            {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .txtTime
        {
            border: 1px solid silver;
            padding-left: 5px;
            float: left;
            width: 173px;
        }

        .btn-remove
        {
            background-color: rgb(248, 45, 70);
            color: white;
            border: 1px solid rgb(248, 45, 70);
            margin-top: 3px;
        }

            .btn-remove:focus
            {
                border: 1px solid black;
                cursor: pointer;
            }

        .btn-file:focus
        {
            border: 1px solid silver;
            cursor: pointer;
        }

        input[type="checkbox"]:focus
        {
            border-color: #ffffcc;
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
                <div class="form-name-header" id="divAddEnq" runat="server">
                    <h3>Existing  Atttendance
                 <div class="navigation">
                     <ul>
                         <li>Atttendance &nbsp; > &nbsp;</li>
                         <li>P.T Atttendance &nbsp; > &nbsp;</li>
                         <li>Existing Atttendance</li>
                     </ul>
                 </div>

                        <h3></h3>

                    </h3>
                </div>

                <div class="form-header">
                    <h4 style="float: left;">&#10148; Search Category
                    </h4>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                </div>
                <center>
                <table>
                    <tr>
                        <th>From Date</th>
                        <th>To Date</th>
                        <th>Category</th>
                        <th>Search by</th>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                        </td>
                        
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="ddl" TabIndex="3" >
                                      <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="MemberID">MemberID</asp:ListItem>
                                        <asp:ListItem Value="MemberName">MemberName</asp:ListItem>
                                        <asp:ListItem Value="StaffID">StaffID</asp:ListItem>
                                        <asp:ListItem Value="SatffName">SatffName</asp:ListItem>
                                        <asp:ListItem Value="AlterInstructorName">AlterInstructorName</asp:ListItem>                                     
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="txt" Enabled="true" TabIndex="4" AutoPostBack="true"  OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                        </td>
                       
                    </tr>

                </table>
              </center>

                <center style="margin-top: 20px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" TabIndex="5" Text="Search"  OnClick="btnSearch_Click"/>
                    <asp:Button ID="btnSearchByDateWithCategory" runat="server" Text="Search By Date with Category" CssClass="form-btn" TabIndex="6"  OnClick="btnSearchByDateWithCategory_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClear_Click" />
                    <asp:Button ID="btnExportToExcel" runat="server" Text="Export" CssClass="form-btn" TabIndex="8" OnClick="btnExportToExcel_Click" />
               </center>

                <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 20px;">
                    <asp:GridView ID="GVAttendance" runat="server" AutoGenerateColumns="false" Width="1000px"
                        Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                        AllowPaging="True" PageSize="20" TabIndex="9" OnPageIndexChanging="GVAttendance_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="112"
                                        CommandArgument='<%#Eval("Member_AutoID")%>' Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Mem.ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="MemberName" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />

                            <asp:BoundField HeaderText="Instructor ID" DataField="Staff_ID1" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Instructor Name" DataField="SatffName" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="AltInstructorName" DataField="AltInstructorName" HeaderStyle-HorizontalAlign="left" />
                            <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <b>Atten.Date</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("AttenDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="InTime" DataField="InTime" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Session" DataField="SessionCnt" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Note" DataField="Note" HeaderStyle-HorizontalAlign="left" />

                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
                <Triggers>
             <asp:PostBackTrigger ControlID="btnExportToExcel" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
