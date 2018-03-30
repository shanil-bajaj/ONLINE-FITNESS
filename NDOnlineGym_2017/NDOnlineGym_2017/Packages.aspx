<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Packages.aspx.cs" Inherits="NDOnlineGym_2017.Packages" EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
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
        table td
        {
            padding-bottom: 10px;
        }

        input:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox
        {
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

        .errorborder
        {
            border: 1px solid red;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
                <div class="form-name-header">
                    <h3>Create Packages
                 <div class="navigation">
                     <ul>
                         <li>User Settings &nbsp; > &nbsp;</li>
                         <li>Packages  &nbsp; > &nbsp;</li>
                         <li>Create Packages</li>
                     </ul>
                 </div>
                    </h3>
                </div>

                <div id="divFormDetails" runat="server">
                    <div class="divForm">
                        <div class="form-header">
                            <h4>&#10148;Create Packages </h4>
                        </div>
                        <div class="form-panel">
                            <table style="height: 80px;">
                                <tr>
                                    <th><span class="error">*</span>Package Name</th>
                                    <th><span class="error">*</span>Duration</th>
                                    <th><span class="error">*</span>Session</th>
                                    <th>Time (From)</th>
                                    <th>Time (To)</th>
                                    <th><span class="error">*</span>Amount</th>
                                    <th>Max Discount</th>
                                    <th><span class="error">*</span>Status</th>
                                    <th><span class="error">*</span>Type</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPackageName" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="1" onChange="javascript:capFirst(this);"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPackageName"
                            ErrorMessage="Enter Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDuration" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="2" onkeypress="return RestrictSpaceSpecial(event)"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDuration"
                            ErrorMessage="Enter Duration"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator> --%>                             
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSession" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="3" onkeypress="return RestrictSpaceSpecial(event)" OnTextChanged="txtSession_TextChanged"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtSession"
                            ErrorMessage="Enter Session"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                         
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTimeFrom" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="4"
                                            TextMode="Time"></asp:TextBox>


                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTimeTo" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="5" TextMode="Time"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="6" onkeypress="return RestrictSpaceSpecial(event)"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidator3" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtAmount"
                            ErrorMessage="Enter Amount"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                          
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDisc" runat="server" Style="width: 90px; padding: 3px 5px;" TabIndex="7" onkeypress="return RestrictSpaceSpecial(event)" AutoPostBack="true" OnTextChanged="txtDisc_TextChanged"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" Style="width: 90px; padding: 3px 5px;" CssClass="ddl" TabIndex="8">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>Deactive</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator  ID="RequiredFieldValidator4" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlStatus"
                            ErrorMessage="Select Status"  SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>   --%>                    
                                    </td>

                                    <td>
                                        <asp:DropDownList ID="ddtype" runat="server" Style="width: 90px; padding: 3px 5px;" CssClass="ddl" TabIndex="9">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Gym">Gym</asp:ListItem>
                                            <asp:ListItem Value="PT">PT</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:TextBox ID="TxtType" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="9" onChange="javascript:capFirst(this);"></asp:TextBox>--%>
                                        <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidator3" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtAmount"
                            ErrorMessage="Enter Amount"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                          
                                    </td>

                                </tr>
                            </table>

                            <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" OnClick="btnSave_Click" TabIndex="10" /> 
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" ValidationGroup="a" OnClick="btnClear_Click" TabIndex="11" />  
             </center>

                        </div>
                    </div>
                </div>
                <div id="divsearch" runat="server">
                    <div class="divForm">
                        <div class="form-header">
                            <h4>&#10148;Search By </h4>
                        </div>

                        <asp:UpdatePanel ID="pnlPageRefresh" runat="server">
                            <ContentTemplate>
                                <div class="form-panel">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20%;">
                                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="12" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Package">Package</asp:ListItem>
                                                    <asp:ListItem Value="Duration">Duration</asp:ListItem>
                                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="13"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" OnClick="btnSearch_Click" Text="Search" TabIndex="14" />
                                            </td>

                                            <td style="width: 10%;">
                                                <asp:Button ID="BtnClear_11" runat="server" Text="Clear" CssClass="form-btn" OnClick="BtnClear_11_Click" TabIndex="15" />
                                            </td>
                                           <td style="width: 10%;">
                                                <asp:Button ID="BtnExportExcel" runat="server" CssClass="form-btn" TabIndex="16" Text="Export to Excel"  OnClick="BtnExportExcel_Click" style="width:200px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div style="width: 1000px; height: auto; overflow-x: scroll;">
                               <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records = " Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                            <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="false" DataKeyNames="Branch_AutoID" EmptyDataText="No record found." Width="1000px"
                                PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" PageSize="20" TabIndex="17" OnPageIndexChanging="gvPackage_PageIndexChanging">
                                <Columns>

                                    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Pack_AutoID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" OnCommand="btnDelete_Command" CommandArgument='<%#Eval("Pack_AutoID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Discount" DataField="Discount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="FromTime" DataField="FromTime" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="ToTime" DataField="ToTime" HeaderStyle-HorizontalAlign="left" />

                                    <%--   <%--  <asp:BoundField DataField="ToTime" HtmlEncode="False" DataFormatString = "{0:D}" HeaderText="ToTime" ReadOnly="True" />--%>
                                    <%--    <asp:TemplateField HeaderText="Check-Out Time" HeaderStyle-CssClass="bg-info gridHeaders">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckOUTTime" runat="server" Text='<%# Eval("ToTime","{0:hh:mm:ss tt}") %>' />
                            </ItemTemplate>
                            <ItemStyle CssClass="text-center" />
                          </asp:TemplateField>--%>

                                    <asp:BoundField HeaderText="Type" DataField="Type" HeaderStyle-HorizontalAlign="left" />
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />         
             <asp:PostBackTrigger ControlID="BtnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
