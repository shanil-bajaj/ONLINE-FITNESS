<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="StaffRegistration.aspx.cs" Inherits="NDOnlineGym_2017.StaffRegistration" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <style>
        .GridView {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView a {
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
            /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            .GridView span {
                padding: 5px 5px 5px 5px;
            }

        .btn-remove {
            background-color: rgb(248, 45, 70);
            color: white;
            border: 1px solid rgb(248, 45, 70);
            margin-top: 3px;
        }

            .btn-remove:focus {
                border: 1px solid black;
                cursor: pointer;
            }

        .btn-file:focus {
            border: 1px solid silver;
            cursor: pointer;
        }

        input[type="checkbox"]:focus {
            border-color: #ffffcc;
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
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="JS/Common-Script.js"></script>
    <script>
        function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgMember.ClientID%>').prop('src', e.target.result)
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
        //Disable enable executive dropdown on check box
        function ChExecutive() {
            var _chkExecutive = document.getElementById('<%= chkExecutive.ClientID %>');
            document.getElementById('<%= ddlExecutive.ClientID %>').disabled = _chkExecutive.checked;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="sc">
                    <div class="form-name-header" id="divStaffReg" runat="server">
                        <h3>Staff Registration
                             <div class="navigation">
                                 <ul>
                                     <li>File &nbsp; > &nbsp;</li>
                                     <li>User &nbsp; > &nbsp;</li>
                                     <li>Staff Registration </li>
                                 </ul>
                             </div>
                        </h3>
                    </div>
                    <div class="form-name-header" id="divStaffDetails" runat="server" visible="false">
                        <h3>Staff Details
                              <div class="navigation">
                                  <ul>
                                      <li>File &nbsp; > &nbsp;</li>
                                      <li>User &nbsp; > &nbsp;</li>
                                      <li>Staff Details </li>
                                  </ul>
                              </div>
                        </h3>
                    </div>
                    <div class="divForm">            
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
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Staff ID </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtStaffId1" runat="server" CssClass="txt" TabIndex="1" AutoPostBack="true" OnTextChanged="txtStaffId1_TextChanged" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtStaffId1"
                                                            ErrorMessage="Enter User ID " SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> First Name </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtFirstName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtFirstName"
                                                            ErrorMessage="Enter First Name" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Last Name </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtLastName" onChange="javascript:capFirst(this);" runat="server" CssClass="txt" TabIndex="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Date Of Birth </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="txt" TabIndex="4"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="txtDob_CalendarExtender" runat="server" BehaviorID="txtDob_CalendarExtender" TargetControlID="txtDob" Format="dd-MM-yyyy" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Gender</span></td>
                                                    <td style="width: 55%; text-align: left;">

                                                        <asp:DropDownList ID="ddlG" runat="server" CssClass="ddl" TabIndex="5">
                                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator  ID="rfvddlGender" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlGender"
                                     ErrorMessage="Select gender"  SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Reg Date </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtRegDate" runat="server" CssClass="txt" TabIndex="6"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="txtReg_CalendarExtender" TargetControlID="txtRegDate" Format="dd-MM-yyyy" />
                                                        <asp:RequiredFieldValidator ID="rvRegDate" runat="server" EmptyDataText="No record found." Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtRegDate"
                                                            ErrorMessage="Enter Regi Date" SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Designation</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="ddl" TabIndex="7">
                                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Department</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="ddl" TabIndex="8">
                                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%--End Personal Details--%>
                            <%--Contact Details--%>
                            <div class="form-header">
                                <h4>&#10148; Contact Details  </h4>
                            </div>
                            <div class="form-panel">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Contact 1 </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtContact1" runat="server" MaxLength="10" CssClass="txt content" TabIndex="9" Width="170px" AutoPostBack="true"
                                                            OnTextChanged="txtContact1_TextChanged" onkeypress="return RestrictSpaceSpecial(event);" Style="margin-top: 13px"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorc" ValidationGroup="a" CssClass="ErrorBox" SetFocusOnError="true" runat="server" ValidationExpression="^[0-9]{1,45}$" ControlToValidate="txtContact1" ErrorMessage="Field Must Be Numeric"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="rfvContact" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContact1"
                                                            ErrorMessage="Enter Contact 1" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Contact 2</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtContact2" runat="server" CssClass="txt" TabIndex="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Email ID</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" TabIndex="11"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEmail"
                                                            ErrorMessage="Enter Email" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table style="margin-left: 85px;">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 50%;"><span class="lbl">Address </span></td>
                                                    <td style="width: 50%; text-align: left;">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="txt-add" TextMode="MultiLine" Rows="3" TabIndex="12" Width="290px" Style="resize: none"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%--End Contact Details--%>
                            <%--Other Details--%>
                            <div class="form-header">
                                <h4>&#10148; Other Details  </h4>
                            </div>
                            <div class="form-panel">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Salary </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtSalary" runat="server" CssClass="txt" TabIndex="13" onkeypress="return RestrictSpaceSpecial(event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Shift</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="ddl" TabIndex="14">
                                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">ID Proof</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtIDProof" runat="server" CssClass="txt" TabIndex="15"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Card Number</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="txt" TabIndex="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span>  Status</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" TabIndex="17">
                                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                                            <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlStatus"
                                                            ErrorMessage="Select Status" SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl">Executive</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <%--<asp:CheckBox ID="chkExecutive" runat="server" Checked="true"  OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="18" /></td>--%>
                                                                    <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" onChange="ChExecutive();" TabIndex="18" /></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="19" Enabled="false">
                                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="cols" style="padding-left: 70px;">
                                            <span style="padding-right: 10px; padding-left: 30px; font-size: 12.5px;">Image</span><asp:FileUpload ID="fileLogo" runat="server" TabIndex="20" onchange="ShowImagePreview(this);" CssClass="btn-file" ClientIDMode="Static"/>
                                        </td>
                                        <td class="cols" style="padding-left: 70px;">
                                            <div id="divImage" style="width: 90px; height: 70px; border: 1px solid silver;">
                                                <asp:Image ID="imgMember" runat="server" Style="width: 90px; height: 70px; border: 1px solid silver;"
                                                    class="fileupload-preview thumbnail" TabIndex="21" />
                                            </div>
                                            <asp:Button runat="server" ID="btnRemove" Text="Remove" OnClick="btnRemove_Click" TabIndex="22"
                                                CssClass="btn-remove" />
                                        </td>
                                        <td class="cols" visible="false"></td>
                                    </tr>

                                </table>
                            </div>
                            <div class="form-panel">
                                <center class="btn-section">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" OnClick="btnSave_Click" UseSubmitBehavior="false" ValidationGroup="a" TabIndex="23"/>
                                    <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnClear_Click" TabIndex="24" />
                                 </center>
                            </div>
                        </div>
                        <div class="form-panel" id="divSearch" runat="server" visible="false">
                            <div class="form-header" id="divSearchHead" runat="server">
                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                <h4 style="float: left;">&#10148; Search Category </h4>
                            </div>
                        
                            <table>
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
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="25" Style="width:105px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender"
                                            TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="26" Style="width:105px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" OnClick="btnSearch_Click" Text="Search" TabIndex="27" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="28" Style="width:112px">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="StaffID">Staff ID</asp:ListItem>
                                            <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                            <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                            <asp:ListItem Value="Gender">Gender</asp:ListItem>
                                            <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                            <asp:ListItem Value="Status">Status</asp:ListItem>                                    
                                            <asp:ListItem Value="Department">Department</asp:ListItem>
                                            <asp:ListItem Value="Designation">Designation</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="true" CssClass="txt" Enabled="True" OnTextChanged="txtSearch_TextChanged"
                                             TabIndex="29" Style="width: 150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDateWithCategory" runat="server" CssClass="form-btn" TabIndex="30" Text="Date with category" OnClick="btnDateWithCategory_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClear1" runat="server" CssClass="form-btn" TabIndex="30" Text="Clear" OnClick="btnClear1_Click" />
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="38" OnClick="btnExportToExcel_Click" />
                                    </td>
                                </tr>
                            </table>                    

                            <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 30px;">
                                <div style="margin: 10px 0px 10px 10px">
                                    <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                                    <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                                <asp:GridView ID="gvStaffReg" runat="server" AutoGenerateColumns="false" DataKeyNames="Staff_AutoID" EmptyDataText="No record found." Width="1700px"
                                    Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" PageSize="20"
                                    OnPageIndexChanging="gvStaffReg_PageIndexChanging" AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Staff_AutoID")%>' TabIndex="31" 
                                                     Style="background-image: url('../NotificationIcons/edit.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnCommand="btnDelete_Command" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="31"
                                                    CommandArgument='<%#Eval("Staff_AutoID")%>'  Style="background-image: url('../NotificationIcons/f-cross_256-128.png'); background-size: 100% 100%; padding-left: 13px; 
                                                    padding-top: 0px; padding-bottom: 3px;" ToolTip="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Staff ID" ControlStyle-Width="10px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtStaffID" runat="server" Text='<%#Eval("Staff_ID1") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblStaffID" runat="server" Text='<%#Eval("Staff_ID1") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    <%-- <asp:BoundField HeaderText="Staff ID" DataField="Staff_ID1"  HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="First Name" ControlStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtFName" runat="server" Text='<%#Eval("FName") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblFName" runat="server" Text='<%#Eval("FName") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="First Name" DataField="FName" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Last Name" ControlStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtLName" runat="server" Text='<%#Eval("LName") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblLName" runat="server" Text='<%#Eval("LName") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Last Name" DataField="LName" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Gender" ControlStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="Center" />--%>
                                    <%-- <asp:TemplateField HeaderText="Gender" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="DOB" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Eval("DOB","{0:dd-MM-yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reg_Date" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Eval("RegDate","{0:dd-MM-yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ControlStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDesignation" runat="server" Text='<%#Eval("Designation") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Designation Name" DataField="Designation" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Department" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDepartment" runat="server" Text='<%#Eval("Department") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Contact1" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Contact1" DataField="Contact1" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Contact2" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtContact2" runat="server" Text='<%#Eval("Contact2") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblContact2" runat="server" Text='<%#Eval("Contact2") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Contact2" DataField="Contact2" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtEmail" runat="server" Text='<%#Eval("Email") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Mail ID" DataField="Email" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtAddress" runat="server" Text='<%#Eval("Address") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <%--<asp:TemplateField HeaderText="Address" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtAddress" runat="server" Text='<%#Eval("Address") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:BoundField HeaderText="IDProof" DataField="IDProofPath" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Card No" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCardNo" runat="server" Text='<%#Eval("CardNo") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblCardNo" runat="server" Text='<%#Eval("CardNo") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="CardNo" DataField="CardNo" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <%--                    <asp:BoundField HeaderText="Shift" DataField="Shift" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <%-- <asp:BoundField HeaderText="Rights" DataField="Rights" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Status" ControlStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtStatus" runat="server" Text='<%#Eval("Status") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:TemplateField HeaderText="Executive" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="txtExecutive" runat="server" Text='<%#Eval("Executive") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblExecutive" runat="server" Text='<%#Eval("Executive") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="Center" />--%>
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
                <asp:PostBackTrigger ControlID="btnExportToExcel" />                    
            </Triggers>
        </asp:UpdatePanel>
</asp:Content>
