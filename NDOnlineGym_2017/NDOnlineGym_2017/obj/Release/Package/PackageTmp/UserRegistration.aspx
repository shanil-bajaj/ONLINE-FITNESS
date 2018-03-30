<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="NDOnlineGym_2017.UserRegistration" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />    
    <style>
        .form-menu {
            float: left;
            width: 250px;
        }
        .btn-menu {
            padding: 10px 10px;
            margin-bottom: 5px;
            border: none;
            width: 100%;
            background-color: rgba(0,0,0,0.1);
            border-radius: 3px;
            text-align: left;
        }
        .btn-menu:hover {
            box-shadow: 2px 2px 10px 0px rgba(0, 0, 0, 0.50);
        }

        .btn-menu:focus {
            box-shadow: 2px 2px 10px 0px rgba(0, 0, 0, 0.50);
        }
        .btn-menu-selected {
            background-color: orangered;
            color: white;
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
        .sc { width:1021px;  }
        @media screen and (min-width: 1400px) {
            .sc { width:1100px; }
        }
    </style>

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    
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
    <div class="sc">
        <div class="form-name-header" id="divUserRegistration" runat="server">
            <h3>User Registration
                     <div class="navigation">
                         <ul>
                             <li>File &nbsp; > &nbsp;</li>
                             <li>User &nbsp; > &nbsp;</li>
                             <li>User Registration </li>
                         </ul>
                     </div>
            </h3>
        </div>
        <div class="form-name-header" id="divUserDetails" runat="server" visible="false">
            <h3>User Details
                <div class="navigation">
                    <ul>                         
                        <li>File &nbsp; > &nbsp;</li>
                        <li>User &nbsp; > &nbsp;</li>
                        <li>User Details </li>
                    </ul>
                </div>
            </h3>
        </div>

        <div class="divForm">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="divFormDetails" runat="server">
                    <%--Personal Details--%>
                    <div class="form-header">
                        <h4>&#10148; Personal Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <asp:Label ID="Label1" runat="server" Text="Label" Visible ="false"></asp:Label>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Staff ID </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtStaffID" runat="server" CssClass="txt" TabIndex="1" OnTextChanged="txtStaffID_TextChanged" onkeypress="return RestrictSpaceSpecial(event);" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtStaffID" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtStaffID"
                                                    ErrorMessage="Enter Staff ID" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Staff Name </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <%--<asp:TextBox ID="txtName" runat="server" CssClass="txt" TabIndex="1" ></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlStaffName" runat="server" CssClass="ddl" TabIndex="2" OnSelectedIndexChanged="ddlStaffName_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlStaffName"
                                                    ErrorMessage="Select Name" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Contact </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="txt" TabIndex="3" AutoPostBack="true" OnTextChanged="txtMobile_TextChanged"
                                                    MaxLength="10" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtMobile" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtMobile"
                                                    ErrorMessage="Enter Mobile No." SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Email ID</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" TabIndex="4" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEmail"
                                                    ErrorMessage="Enter Email Id" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Username </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtUsername" runat="server" CssClass="txt" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtUsername"
                                                    ErrorMessage="Enter Username" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Password </span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="txt" TabIndex="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPassword"
                                                    ErrorMessage="Enter Password" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Status</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" TabIndex="7">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlStatus" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlStatus"
                                                    ErrorMessage="Select Status " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cols">
                                    <table>
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Authority</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlAuthority" runat="server" CssClass="ddl" TabIndex="8">
                                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="SuperAdmin">SuperAdmin</asp:ListItem>
                                                    <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                    <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                                    <asp:ListItem Value="User">User</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlAuthority" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlAuthority"
                                                    ErrorMessage="Select Authority " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--End Personal Details--%>
                    <div class="form-panel">
                        <center class="btn-section">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn"  ValidationGroup="a"  TabIndex="9" OnClick="btnSave_Click"
                                OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false"/>
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="10" OnClick="btnClear_Click" />
                         </center>
                    </div>
                </div>
                <div id="divSearch" runat="server" visible="false" >
                <div class="form-header" id="divformheader" runat="server">
                    <h4 style="float: left;">&#10148; Search Category</h4>
                </div>
                <div class="form-panel" id="divformpanel" runat="server">
                    <table style="width: 100%;">
                         <tr>
                        <th>From Date</th>
                        <th>To Date</th>
                        <th></th>
                        <th>Category</th>
                        <th>Search by</th>
                        <th></th>
                        <th></th>
                        <th></th>
                </tr>
                <tr>
                   <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="11" style="width:110px" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                    </td>
                    <td>
                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="12" style="width:110px" ></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                    </td>
                    <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" OnClick="btnSearch_Click" TabIndex="13" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="14" style="width:110px" >
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Name">Name</asp:ListItem>
                                    <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                    <asp:ListItem Value="Status">Status</asp:ListItem>
                                    <asp:ListItem Value="Authority">Authority</asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="True" TabIndex="15" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" style="width:148px" ></asp:TextBox>
                            </td>
                            
                    <td>
                        <asp:Button ID="btnDateWithCategory" runat="server" CssClass="form-btn" TabIndex="16" Text="Date with category" OnClick="btnDateWithCategory_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnClear1" runat="server" CssClass="form-btn" TabIndex="30" Text="Clear" OnClick="btnClear1_Click" />
                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="38" OnClick="btnExportToExcel_Click" />

                    </td>
                        </tr>
                    </table>
                    <div style="width: 1000px; height: auto; overflow-x: scroll;">

                         <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                        <asp:GridView ID="gvUserReg" runat="server" AutoGenerateColumns="false" DataKeyNames="Login_AutoID" Width="1000px"
                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True"
                            OnPageIndexChanging="gvUserReg_PageIndexChanging" PageSize="20">

                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Login_AutoID")%>' TabIndex="17"
                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="17" OnCommand="btnDelete_Command" CommandArgument='<%#Eval("Login_AutoID")%>' 
                                             style="background-image:url('../NotificationIcons/f-cross_256-128.png'); background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtContact" runat="server" Text='<%#Eval("Mobile") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblContact" runat="server" Text='<%#Eval("Mobile") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtEmail1" runat="server" Text='<%#Eval("Email") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtStatus" runat="server" Text='<%#Eval("Status") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Authority" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAuthority" runat="server" Text='<%#Eval("Authority") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblAuthority" runat="server" Text='<%#Eval("Authority") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Mobile" DataField="Mobile" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Email Id" DataField="Email" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Authority" DataField="Authority" HeaderStyle-HorizontalAlign="Center" />--%>
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>
                    </div>
                    <%--End Software handling--%>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
            </Triggers>
        </asp:UpdatePanel>
        </div>
        </div>
</asp:Content>




