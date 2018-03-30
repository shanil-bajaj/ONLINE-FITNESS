<%@ Page Title="" Language="C#" MasterPageFile="~/PTMaster.Master" AutoEventWireup="true" CodeBehind="PTBookingDetails.aspx.cs" Inherits="NDOnlineGym_2017.PTBookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <%-- <link rel="icon" type="image/png" href="Logo/NDLogo.png" />--%>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script type="text/javascript">
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
    </script>
    <style>
        input:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

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

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            {
                color: #fff;
                padding: 5px 5px 5px 5px;
            }

        .remove
        {
            font-size: 18px;
            font-weight: bold;
        }

        .GridView1
        {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView1 a /** FOR THE PAGING ICONS  **/
            {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView1 a:focus
                {
                    color: orangered;
                }

                .GridView1 a:hover
                {
                    color: orangered;
                }

            .GridView1 span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .hideGridColumn
        {
            display: none;
        }

        .GridView a:focus
        {
            color: orangered;
        }

        .GridView a:hover
        {
            color: orangered;
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
                <div class="divForm">
                    <div class="form-header">
                        <h4 style="float: left;">&#10148; Search Category
               <%-- <div style="float:right;padding-right:10px;">--%>

                            <%-- </div>--%>

                        </h4>
                    </div>

                    <div class="form-panel">
                        <table>
                            <tr>
                                <th>From Date</th>
                                <th>To Date</th>
                                <th>Category</th>
                                <th>Search by</th>
                                <th></th>
                                <th></th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="29" Style="width: 200px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="30" Style="width: 200px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="ddl" TabIndex="104" Style="width: 200px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                        <asp:ListItem Value="Receipt ID">Receipt ID</asp:ListItem>
                                        <asp:ListItem Value="First Name">First Name</asp:ListItem>
                                        <asp:ListItem Value="Last Name">Last Name</asp:ListItem>
                                        <asp:ListItem Value="ContactNo">ContactNo</asp:ListItem>
                                        <%--<asp:ListItem Value="Course Name">Course Name</asp:ListItem>--%>
                                        <asp:ListItem Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                        <asp:ListItem Value="PT">PT</asp:ListItem>
                                        <%--  <asp:ListItem Value="Upgrade">Upgrade</asp:ListItem>--%>
                                        <asp:ListItem Value="Executive">Executive</asp:ListItem>
                                        <asp:ListItem Value="PaymentMode">Payment Mode</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="txt" Enabled="true" TabIndex="105" Style="width: 200px" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <table style="margin-top: 15px">
                            <tr>

                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" TabIndex="106" Text="Search" Style="width: 200px" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnsearchWithDate" runat="server" CssClass="form-btn" TabIndex="107" Text="Date with category" Style="width: 200px" OnClick="BtnsearchWithDate_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnclear" runat="server" CssClass="form-btn" TabIndex="108" Text="Clear" Style="width: 200px" OnClick="btnclear_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnExportExcel" runat="server" CssClass="form-btn" TabIndex="109" Text="Export to Excel" Style="width: 200px" OnClick="BtnExportExcel_Click" />
                                </td>
                            </tr>
                        </table>

                    </div>

                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 5px;">

                        <asp:GridView ID="GVCourseDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                            Width="1700px" AllowPaging="True" PageSize="20" TabIndex="110"
                            PagerStyle-CssClass="pager" CssClass="GridView1" GridLines="None" CellPadding="5">
                            <Columns>
                                <%--<asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("ReceiptID")%>' TabIndex="111" OnCommand="btnEdit_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="112"
                                            CommandArgument='<%#Eval("ReceiptID")%>' Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="preview">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnpreview" runat="server" CausesValidation="false" Text="Preview" CommandArgument='<%#Eval("ReceiptID")%>' TabIndex="113" OnCommand="btnpreview_Command" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="ResReceipt">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnResend" runat="server" CausesValidation="false" Text="Resend" CommandArgument='<%#Eval("ReceiptID")%>' TabIndex="114" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Mem.ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Rec.ID" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <HeaderTemplate>
                                        <b>Pay.Date</b>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("payDate","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="FirstName" DataField="FName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="LastName" DataField="LName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />

                                <%-- <asp:BoundField HeaderText="Days" DataField="Days" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Time" DataField="Time" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Instructor" DataField="Instructor" HeaderStyle-HorizontalAlign="left" />--%>

                                <asp:BoundField HeaderText="MemberType" DataField="MemberType" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="CourseMemberType" DataField="CourseMemberType" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Qty" DataField="Qty" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Total" DataField="TotalFeeDue" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Paid" DataField="PaidFee" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Balance" DataField="Balance" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="PaymentMode" DataField="PaymentMode" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                                <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <HeaderTemplate>
                                        <b>NextPayDate</b>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Executive" DataField="Name" HeaderStyle-HorizontalAlign="left" />

                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
