<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MastersForms.aspx.cs" Inherits="NDOnlineGym_2017.MastersForms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
            <div class="form-name-header">
                <h3>Masters 
                <div class="navigation">
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>Master &nbsp; > &nbsp;</li>

                    </ul>
                </div>
                </h3>
            </div>
            <div class="divForm" style="background-color: none">
                <table>
                    <tr>
                        <td>
                            <div style="width: 250px;">
                                <asp:Button ID="btnDesignation" runat="server" Text="Designation" CssClass="btn-menu btn-menu-selected" CausesValidation="false" OnClick="btnDesignation_Click" TabIndex="1" />
                                <asp:Button ID="btnDepartment" runat="server" Text="Department" CssClass="btn-menu" OnClick="btnDepartment_Click" TabIndex="7" />
                                <asp:Button ID="btnEnquiryType" runat="server" Text="Enquiry Type" CssClass="btn-menu" OnClick="btnEnquiryType_Click" TabIndex="11" />
                                <asp:Button ID="btnEnquiryFor" runat="server" Text="Enquiry For" CssClass="btn-menu" OnClick="btnEnquiryFor_Click" TabIndex="16" />
                                <asp:Button ID="btnOcccupation" runat="server" Text="Occupation" CssClass="btn-menu" OnClick="btnOcccupation_Click" TabIndex="21" />
                                <asp:Button ID="btnShift" runat="server" Text="Shift" CssClass="btn-menu" OnClick="btnShift_Click" TabIndex="26" />
                                <asp:Button ID="btnSourceOfEnquiry" runat="server" Text="Source Of Enquiry" CssClass="btn-menu" OnClick="btnSourceOfEnquiry_Click" TabIndex="31" />
                                <asp:Button ID="btnMuscularGroup" runat="server" Text="Muscular Group" CssClass="btn-menu" OnClick="btnMuscularGroup_Click" TabIndex="36" />
                                <asp:Button ID="btnWorkoutName" runat="server" Text="Workout Name" CssClass="btn-menu" OnClick="btnWorkoutName_Click" TabIndex="41" />
                                <asp:Button ID="btnDietAvoid" runat="server" Text="Diet Avoid" CssClass="btn-menu" OnClick="btnDietAvoid_Click" TabIndex="47" />
                                <asp:Button ID="btnDietDeclaration" runat="server" Text="Diet Declaration" CssClass="btn-menu" OnClick="btnDietDeclaration_Click" TabIndex="52" />
                                <asp:Button ID="btnPaymentDetails" runat="server" Text="Payment Mode" CssClass="btn-menu" OnClick="btnPaymentDetails_Click" TabIndex="57" />
                                <asp:Button ID="btnExpenseGroup" runat="server" Text="Expense Group" CssClass="btn-menu" OnClick="btnExpenseGroup_Click" TabIndex="62" />
                                <asp:Button ID="btnProgrammer" runat="server" Text="Programmer" CssClass="btn-menu" OnClick="btnProgrammer_Click" TabIndex="67" />
                                <asp:Button ID="btnFollowupType" runat="server" Text="Followup Type" CssClass="btn-menu" OnClick="btnFollowupType_Click" TabIndex="72" />
                                <asp:Button ID="btnTax" runat="server" Text="Tax" CssClass="btn-menu" OnClick="btnTax_Click" TabIndex="77" />
                                <asp:Button ID="btnCallRespond" runat="server" Text="Call Respond" CssClass="btn-menu" OnClick="btnCallRespond_Click" TabIndex="78" />
                                <asp:Button ID="btnUpgradeDays" runat="server" Text="Upgrade Days" CssClass="btn-menu" OnClick="btnUpgradeDays_Click" TabIndex="79" />
                           </div>
                        </td>
                        <td>
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="View1" runat="server">
                                    <center><table style=" ">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Designation </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="txt" TabIndex="2" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDesignation"
                                                            ErrorMessage="Enter Designation" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSaveDesignation" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="3" OnClick="btnSaveDesignation_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearDesignation" runat="server" Text="Clear" CssClass="form-btn" TabIndex="4" OnClick="btnClearDesignation_Click" />
                           </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="False" Width="480px"
                                            DataKeyNames="Desig_AutoID" Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            AllowPaging="True" OnPageIndexChanging="gvDesignation_PageIndexChanging" PageSize="20">

                                            <Columns>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditDesig" runat="server" CausesValidation="false" OnCommand="btnEditDesig_Command" CommandArgument='<%#Eval("Desig_AutoID")%>' TabIndex="5"
                                                       style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
   
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteDesig" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                                            CommandArgument='<%#Eval("Desig_AutoID")%>'  OnCommand="btnDeleteDesig_Command"
                                         style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                                           
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtDesigbnation" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblDesigbnation" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                    <ControlStyle Width="150px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Designation" DataField="" />--%>
                                                
                                                                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <PagerStyle CssClass="pager" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <center>
                                <table style="">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Department </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="txt" TabIndex="8" onChange="javascript:capFirst(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDepartment" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDepartment"
                                                ErrorMessage="Enter Department" SetFocusOnError="true" ValidationGroup="dept"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                                <asp:Button ID="btnSaveDepartment" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="dept" TabIndex="8" OnClick="btnSaveDepartment_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearDepartment" runat="server" Text="Clear" CssClass="form-btn" TabIndex="9" OnClick="btnClearDepartment_Click" />
                              </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvDepartment" runat="server" AutoGenerateColumns="false" Font-Size="11px" PagerStyle-CssClass="pager" Width="480px"
                                            CssClass="GridView" GridLines="None" CellPadding="3" DataKeyNames="Dept_AutoID"
                                            AllowPaging="True" OnPageIndexChanging="gvDepartment_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditDept" runat="server" CausesValidation="false"  OnCommand="btnEditDept_Command"
                                                            CommandArgument='<%#Eval("Dept_AutoID")%>' TabIndex="10" 
                                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteDept" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="10"
                                                            CommandArgument='<%#Eval("Dept_AutoID")%>'  OnCommand="btnDeleteDept_Command"
                                                            style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" >
                                </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtDepartment" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Department" DataField="Name"/>--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <center>
                              <table style="">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Enquiry Type </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtEnquiryType" runat="server" CssClass="txt" TabIndex="12" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtEnquiryType" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEnquiryType"
                                                        ErrorMessage="Enter EnquiryType" SetFocusOnError="true" ValidationGroup="EnqType"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSaveEnquiryType" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="EnqType" TabIndex="13" OnClick="btnSaveEnquiryType_Click" UseSubmitBehavior="false" />
                            <asp:Button ID="btnClearEnquiryType" runat="server" Text="Clear" CssClass="form-btn" TabIndex="14" OnClick="btnClearEnquiryType_Click" />
                            </center>

                                    <%--GridView--%>
                                   <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvEnquiryType" runat="server" AutoGenerateColumns="false" DataKeyNames="EnqType_AutoID" Font-Size="11px" Width="480px"
                                            PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3" AllowPaging="True"
                                            OnPageIndexChanging="gvEnquiryType_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditEnquiryType" runat="server" CausesValidation="false" OnCommand="btnEditEnquiryType_Command"
                                                            CommandArgument='<%#Eval("EnqType_AutoID")%>' TabIndex="15" style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteEnquiryType" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="15"
                                                            CommandArgument='<%#Eval("EnqType_AutoID")%>' OnCommand="btnDeleteEnquiryType_Command"
                                                            style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enquiry Type" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtEnquiryType" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEnquiryType" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Enquiry Type" DataField="Name" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View4" runat="server">
                                    <center>
                              <table style="">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Enquiry For </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtEnquiryFor" runat="server" CssClass="txt" TabIndex="17" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtEnquiryFor" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtEnquiryFor"
                                                        ErrorMessage="Enter Enquiry For" SetFocusOnError="true" ValidationGroup="EnqFor"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSaveEnquiryFor" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="EnqFor" TabIndex="18" OnClick="btnSaveEnquiryFor_Click" UseSubmitBehavior="false" />
                            <asp:Button ID="btnClearEnquiryFor" runat="server" Text="Clear" CssClass="form-btn" TabIndex="19" OnClick="btnClearEnquiryFor_Click" />
                         </center>

                                    <%--GridView--%>
                                   <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvEnquiryFor" runat="server" AutoGenerateColumns="false" DataKeyNames="EnqFor_AutoID" AllowPaging="True" Width="480px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            OnPageIndexChanging="gvEnquiryFor_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditEnquiryFor" runat="server" CausesValidation="false" OnCommand="btnEditEnquiryFor_Command"
                                                            CommandArgument='<%#Eval("EnqFor_AutoID")%>' TabIndex="20" style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteEnquiryFor" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="20"
                                                            CommandArgument='<%#Eval("EnqFor_AutoID")%>' OnCommand="btnDeleteEnquiryFor_Command" style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enquiry For" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtEnquiryFor" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEnquiryFor" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Enquiry Type" DataField="Name" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>

                                </asp:View>
                                <asp:View ID="View5" runat="server">
                                    <center>
                             <table style="">
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Occupation </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtOccupation" runat="server" CssClass="txt" TabIndex="22" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtOccupation" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtOccupation"
                                                        ErrorMessage="Enter Occupation" SetFocusOnError="true" ValidationGroup="Occupation"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSaveOccupation" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Occupation" TabIndex="23" OnClick="btnSaveOccupation_Click" UseSubmitBehavior="false" />
                            <asp:Button ID="btnClearOccupation" runat="server" Text="Clear" CssClass="form-btn" TabIndex="24" OnClick="btnClearOccupation_Click" />

                        </center>
                                    <%--GridView--%>
                                   <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvOccupation" runat="server" AutoGenerateColumns="false" DataKeyNames="Occupation_AutoID" AllowPaging="True" Width="480px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            OnPageIndexChanging="gvOccupation_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditOccupation" runat="server" CausesValidation="false" OnCommand="btnEditOccupation_Command"
                                                            CommandArgument='<%#Eval("Occupation_AutoID")%>' TabIndex="25" style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"   />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteOccupation" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="25"
                                                            CommandArgument='<%#Eval("Occupation_AutoID")%>' OnCommand="btnDeleteOccupation_Command" style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Occupation" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOccupation" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblOccupation" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Occupation" DataField="Name" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View6" runat="server">
                                    <center>
                              <table style="">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Shift</span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtShift" runat="server" CssClass="txt" TabIndex="27" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtShift" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtShift"
                                                            ErrorMessage="Enter Shift" SetFocusOnError="true" ValidationGroup="Shift"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                             </table>

                            <asp:Button ID="btnSaveShift" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Shift" TabIndex="28" OnClick="btnSaveShift_Click" UseSubmitBehavior="false" />
                            <asp:Button ID="btnClearShift" runat="server" Text="Clear" CssClass="form-btn" TabIndex="29" OnClick="btnClearShift_Click"/>
                            </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvShift" runat="server" AutoGenerateColumns="false" DataKeyNames="Shift_AutoID" AllowPaging="True" Width="480px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3" OnPageIndexChanging="gvShift_PageIndexChanging"
                                            PageSize="2">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditShift" runat="server" CausesValidation="false" OnCommand="btnEditShift_Command"
                                                            CommandArgument='<%#Eval("Shift_AutoID")%>' TabIndex="30" style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteShift" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="30"
                                                            CommandArgument='<%#Eval("Shift_AutoID")%>' OnCommand="btnDeleteShift_Command"  style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtShift" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblShift" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View7" runat="server">
                                    <center>
                              <table style="">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Source Of Enquiry </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtSourceOfEnquiry" runat="server" CssClass="txt" TabIndex="32" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtSourceOfEnquiry" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtSourceOfEnquiry"
                                                            ErrorMessage="Enter Source " SetFocusOnError="true" ValidationGroup="SourceOfEnquiry"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                              </table>
                                <asp:Button ID="btnSaveSourceOfEnquiry" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="SourceOfEnquiry" TabIndex="33" OnClick="btnSaveSourceOfEnquiry_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearSourceOfEnquiry" runat="server" Text="Clear" CssClass="form-btn" TabIndex="34" OnClick="btnClearSourceOfEnquiry_Click" />
                            </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvSourceOfEnquiry" runat="server" AutoGenerateColumns="false" DataKeyNames="SourceOfEnq_AutoID" AllowPaging="True" Width="480px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            OnPageIndexChanging="gvSourceOfEnquiry_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditSourceOfEnquiry" runat="server" CausesValidation="false" 
                                                            OnCommand="btnEditSourceOfEnquiry_Command" CommandArgument='<%#Eval("SourceOfEnq_AutoID")%>' TabIndex="35"  style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteSourceOfEnquiry" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')"
                                                            TabIndex="35"
                                                            CommandArgument='<%#Eval("SourceOfEnq_AutoID")%>' OnCommand="btnDeleteSourceOfEnquiry_Command"  style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Source Of Enquiry" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSourceOfEnquiry" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblSourceOfEnquiry" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Source Of Enquiry" DataField="Name" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View8" runat="server">
                                    <center>
                                <table style="">
                                    <tr>
                                        <td class="cols">
                                            <table><tr>
                                                <td style="width:45%;"><span class="lbl"><span class="error">*</span> Muscular Group </span></td>
                                                <td style="width:55%;text-align:left;">
                                                    <asp:TextBox ID="txtMuscularGroup" runat="server" CssClass="txt" TabIndex="37" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator  ID="rfvtxtMuscularGroup" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtMuscularGroup"
                                                    ErrorMessage="Enter Group"  SetFocusOnError="true" ValidationGroup="MuscularGroup"></asp:RequiredFieldValidator>
                                                </td>
                                             </tr></table>
                                        </td>
                                   </tr>
                                </table>
          
                                <asp:Button ID="btnSaveMuscularGroup" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="MuscularGroup" TabIndex="38" OnClick="btnSaveMuscularGroup_Click" UseSubmitBehavior="false"/>
                                <asp:Button ID="btnClearMuscularGroup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="39" OnClick="btnClearMuscularGroup_Click"/>
                           </center>

                                   <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvMuscularGroup" runat="server" AutoGenerateColumns="false" DataKeyNames="MuscularGroup_AutoID" AllowPaging="True" Width="480px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            PageSize="20" OnPageIndexChanging="gvMuscularGroup_PageIndexChanging">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditMuscularGroup" runat="server" CausesValidation="false"  CommandArgument='<%#Eval("MuscularGroup_AutoID")%>'
                                                            OnCommand="btnEditMuscularGroup_Command" TabIndex="40" 
                                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteMuscularGroup" runat="server" CausesValidation="false" OnCommand="btnDeleteMuscularGroup_Command" 
                                                            CommandArgument='<%#Eval("MuscularGroup_AutoID")%>' OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="40" style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Muscular Group" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtMuscularGroup" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblMuscularGroup" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Muscular Group" DataField="Name" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View9" runat="server">
                                    <center>
                             <table style="">
                                <tr>
                                    <td class="cols">
                                        <table><tr>
                                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Workout Name </span></td>
                                            <td style="width:55%;text-align:left;">
                                                <asp:TextBox ID="txtWorkoutName" runat="server" CssClass="txt" TabIndex="42" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator  ID="rfvtxtWorkoutName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtWorkoutName"
                                                ErrorMessage="Enter Workout"  SetFocusOnError="true" ValidationGroup="WorkoutName"></asp:RequiredFieldValidator>
                                            </td>
                                         </tr></table>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td class="cols">
                                        <table><tr>
                                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Muscular Group </span></td>
                                            <td style="width:55%;text-align:left;">
                                                <asp:DropDownList ID="ddlMuscularGroup" runat="server" CssClass="ddl" TabIndex="43">
                                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator  ID="rfvddlMuscularGroup" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlMuscularGroup"
                                                ErrorMessage="Select Group"  SetFocusOnError="true" ValidationGroup="WorkoutName" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                         </tr></table>
                                    </td>
                                </tr>
                            </table>
                     
            
                
                     <asp:Button ID="btnSaveWorkoutName" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="WorkoutName" TabIndex="44" OnClick="btnSaveWorkoutName_Click" UseSubmitBehavior="false"/>
                    <asp:Button ID="btnClearWorkoutName" runat="server" Text="Clear" CssClass="form-btn" TabIndex="45" OnClick="btnClearWorkoutName_Click" />
                            </center>

                                  <div style="width: 500px; height: 470px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvWorkoutName" runat="server" AutoGenerateColumns="false" DataKeyNames="WorkoutName_AutoID" AllowPaging="True" Width="470px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            PageSize="20" OnPageIndexChanging="gvWorkoutName_PageIndexChanging">

                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditWorkoutName" runat="server" CausesValidation="false" CommandArgument='<%#Eval("WorkoutName_AutoID")%>'
                                                            OnCommand="btnEditWorkoutName_Command" TabIndex="46" style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteWorkoutName" runat="server"  CausesValidation="false" OnCommand="btnDeleteWorkoutName_Command"
                                                            CommandArgument='<%#Eval("WorkoutName_AutoID")%>'
                                                            OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="46" style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Workout Name" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtWorkoutName" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblWorkoutName" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Muscular Group" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtMuscularGroup" runat="server" Text='<%#Eval("MuscularGroup") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblMuscularGroup" runat="server" Text='<%#Eval("MuscularGroup") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Workout Plan" DataField="MuscularGroup" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View10" runat="server">
                                    <center>
                             <table style="">
                                <tr>
                                    <td class="cols">
                                        <table><tr>
                                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Diet Avoid  </span></td>
                                            <td style="width:55%;text-align:left;">
                                                <asp:TextBox ID="txtDietAvoid" runat="server" CssClass="txt" TabIndex="48" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator  ID="rfvDietAvoid" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDietAvoid"
                                                ErrorMessage="Enter Diet Avoid"  SetFocusOnError="true" ValidationGroup="DietAvoid"></asp:RequiredFieldValidator>
                                            </td>
                                         </tr></table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSaveDietAvoid" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="DietAvoid" TabIndex="49" OnClick="btnSaveDietAvoid_Click" UseSubmitBehavior="false"/>
                            <%--<asp:Button ID="btnClearDietAvoid" runat="server" Text="Clear" CssClass="form-btn" TabIndex="50"  />--%>
                           
                            </center>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        </div>
                                </asp:View>
                                <asp:View ID="View11" runat="server">
                                    <center>
                             <table style="">
                                <tr>
                                    <td class="cols">
                                        <table><tr>
                                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Diet Declaration </span></td>
                                             <td style="width:55%;text-align:left;">
                                                <asp:TextBox ID="txtDietDeclaration" runat="server" CssClass="txt" TabIndex="53" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator  ID="rfvDietDeclaration" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDietDeclaration"
                                                ErrorMessage="Enter Diet"  SetFocusOnError="true" ValidationGroup="DietDeclaration"></asp:RequiredFieldValidator>
                                            </td>
                                         </tr></table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSaveDietDeclaration" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="DietDeclaration" TabIndex="54" OnClick="btnSaveDietDeclaration_Click" UseSubmitBehavior="false"/>
                            <%--<asp:Button ID="btnClearDietDeclaration" runat="server" Text="Clear" CssClass="form-btn" TabIndex="55" OnClick="btnClearDietDeclaration_Click"  />--%>
                           </center>

                                <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        </div>

                                </asp:View>
                                <asp:View ID="View12" runat="server">
                                    <center>
                              <table style="">
                                <tr>
                                    <td class="cols">
                                        <table><tr>
                                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Payment Mode </span></td>
                                            <td style="width:55%;text-align:left;">
                                                <asp:TextBox ID="txtPaymentDetail" runat="server" CssClass="txt" TabIndex="58" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator  ID="rfvPaymentDetail" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPaymentDetail"
                                                ErrorMessage="Enter Payment"  SetFocusOnError="true" ValidationGroup="PaymentDetail"></asp:RequiredFieldValidator>
                                            </td>
                                         </tr></table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSavePaymentDetail" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="PaymentDetail" TabIndex="59" OnClick="btnSavePaymentDetail_Click" UseSubmitBehavior="false"/>
                            <asp:Button ID="btnClearPaymentDetail" runat="server" Text="Clear" CssClass="form-btn" TabIndex="60" OnClick="btnClearPaymentDetail_Click"  />
                           </center>

                                   <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvPaymentDetail" runat="server" AutoGenerateColumns="false" DataKeyNames="PaymentMode_AutoID" AllowPaging="True" Width="490px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            OnPageIndexChanging="gvPaymentDetail_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditPaymentDetail" runat="server" CausesValidation="false" OnCommand="btnEditPaymentDetail_Command"
                                                            CommandArgument='<%#Eval("PaymentMode_AutoID")%>' TabIndex="61" style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  ItemStyle-Width="80px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeletePaymentDetail" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')"
                                                            TabIndex="61"
                                                            CommandArgument='<%#Eval("PaymentMode_AutoID")%>'  OnCommand="btnDeletePaymentDetail_Command" style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Mode" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPaymentDetail" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblPaymentDetail" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Payment Mode" DataField="Name" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>

                                </asp:View>
                                <asp:View ID="View13" runat="server">
                                    <center><table style=" ">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Expense Group </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtExpenseGroup" runat="server" CssClass="txt" TabIndex="63" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvExpenseGroup" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtExpenseGroup"
                                                            ErrorMessage="Enter Group" SetFocusOnError="true" ValidationGroup="ExpenseGroup"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSaveExpenseGroup" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="ExpenseGroup" TabIndex="64" OnClick="btnSaveExpenseGroup_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearExpenseGroup" runat="server" Text="Clear" CssClass="form-btn" TabIndex="65" OnClick="btnClearExpenseGroup_Click" />
                           </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvExpenseGroup" runat="server" AutoGenerateColumns="false" DataKeyNames="Expgrp_AutoID" AllowPaging="True" Width="490px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            OnPageIndexChanging="gvExpenseGroup_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditExpenseGroup" runat="server" CausesValidation="false" OnCommand="btnEditExpenseGroup_Command"
                                                            CommandArgument='<%#Eval("Expgrp_AutoID")%>' TabIndex="66"  style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteExpenseGroup" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')"
                                                            TabIndex="66"
                                                            CommandArgument='<%#Eval("Expgrp_AutoID")%>'  OnCommand="btnDeleteExpenseGroup_Command"  style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expense Group" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtExpenseGroup" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblExpenseGroup" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Designation" DataField="" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View14" runat="server">
                                    <center><table style=" ">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Programmer </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtProgrammer" runat="server" CssClass="txt" TabIndex="68" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvProgrammer" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtProgrammer"
                                                            ErrorMessage="Enter Programmer" SetFocusOnError="true" ValidationGroup="Programmer"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSaveProgrammer" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Programmer" TabIndex="69" OnClick="btnSaveProgrammer_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearProgrammer" runat="server" Text="Clear" CssClass="form-btn" TabIndex="70" OnClick="btnClearProgrammer_Click" />
                           </center>
                                    <%--GridView--%>
                                   <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvProgrammer" runat="server" AutoGenerateColumns="false" DataKeyNames="Programmer_AutoID" AllowPaging="True" Width="490px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            OnPageIndexChanging="gvProgrammer_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditProgrammer" runat="server" CausesValidation="false" OnCommand="btnEditProgrammer_Command"
                                                            CommandArgument='<%#Eval("Programmer_AutoID")%>' TabIndex="71"  style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteProgrammer" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')"
                                                            TabIndex="71"
                                                            CommandArgument='<%#Eval("Programmer_AutoID")%>'  OnCommand="btnDeleteProgrammer_Command" 
                                                            style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Programmer" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtProgrammer" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblProgrammer" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Designation" DataField="" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>

                                <asp:View ID="View15" runat="server">
                                    <center><table style=" ">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Followup Type </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtFollowupType" runat="server" CssClass="txt" TabIndex="73" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvFollowupType" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtFollowupType"
                                                            ErrorMessage="Enter Followup" SetFocusOnError="true" ValidationGroup="FollowupType"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSaveFollowupType" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="FollowupType" TabIndex="74" OnClick="btnSaveFollowupType_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearFollowupType" runat="server" Text="Clear" CssClass="form-btn" TabIndex="75" OnClick="btnClearFollowupType_Click" />
                           </center>
                                    <%--GridView--%>
                                  <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvFollowupType" runat="server" AutoGenerateColumns="false" DataKeyNames="FollowupType_AutoID" AllowPaging="True" Width="490px"
                                            Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            OnPageIndexChanging="gvFollowupType_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditFollowupType" runat="server" CausesValidation="false"  OnCommand="btnEditFollowupType_Command"
                                                            CommandArgument='<%#Eval("FollowupType_AutoID")%>' TabIndex="76" style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteFollowupType" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')"
                                                            TabIndex="76"
                                                            CommandArgument='<%#Eval("FollowupType_AutoID")%>'  OnCommand="btnDeleteFollowupType_Command"  
                                                            style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Followup Type" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtFollowupType" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblFollowupType" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Designation" DataField="" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>

                                <asp:View ID="View16" runat="server">
                                    <center>
                             <table style="">
                                <tr>
                                    <td class="cols">
                                        <table><tr>
                                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Tax  </span></td>
                                            <td style="width:55%;text-align:left;">
                                                <asp:TextBox ID="txtTax" runat="server" CssClass="txt" TabIndex="78" ></asp:TextBox>
                                                <asp:RequiredFieldValidator  ID="rfvtxtTax" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtTax"
                                                ErrorMessage="Enter Tax"  SetFocusOnError="true" ValidationGroup="Tax"></asp:RequiredFieldValidator>
                                            </td>
                                         </tr></table>
                                    </td>
                                </tr>
                            </table>
                                        <asp:Button ID="btnSaveTax1" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Tax" TabIndex="79" OnClick="btnSaveTax1_Click" UseSubmitBehavior="false"></asp:Button>
                          
                            </center>
                                 <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        </div>
                                </asp:View>

                                <asp:View ID="View17" runat="server">
                                    <center><table style=" ">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Call Respond </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtCallRespond" runat="server" CssClass="txt" TabIndex="79" onChange="javascript:capFirst(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCallRespond" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtCallRespond"
                                                            ErrorMessage="Enter CallRespond" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSaveCallRespond" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="80" OnClick="btnSaveCallRespond_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearCallRespond" runat="server" Text="Clear" CssClass="form-btn" TabIndex="81" OnClick="btnClearCallRespond_Click" />
                           </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvCallRespond" runat="server" AutoGenerateColumns="false" Width="480px"
                                            DataKeyNames="CallRespond_AutoID" Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                                            AllowPaging="True" OnPageIndexChanging="gvCallRespond_PageIndexChanging" PageSize="20">

                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditCallRespond" runat="server" CausesValidation="false"  OnCommand="btnEditCallRespond_Command" CommandArgument='<%#Eval("CallRespond_AutoID")%>' TabIndex="82" 
                                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteCallRespond" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="82"
                                                            CommandArgument='<%#Eval("CallRespond_AutoID")%>'  OnCommand="btnDeleteCallRespond_Command"  style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Call Respond" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCallRespond" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblCallRespond" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Designation" DataField="" />--%>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>

                                <asp:View ID="View18" runat="server">
                                    <center>
                             <table style="">
                                <tr>
                                    <td class="cols">
                                        <table><tr>
                                            <td style="width:45%;"><span class="lbl"><span class="error">*</span> Upgrade Days  </span></td>
                                            <td style="width:55%;text-align:left;">
                                                <asp:TextBox ID="txtUpgradeDays" runat="server" CssClass="txt" TabIndex="78" ></asp:TextBox>
                                                <asp:RequiredFieldValidator  ID="rfvtxtUpgradeDays" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtUpgradeDays"
                                                ErrorMessage="Enter Tax"  SetFocusOnError="true" ValidationGroup="UpgradeDays"></asp:RequiredFieldValidator>
                                            </td>
                                         </tr></table>
                                    </td>
                                </tr>
                            </table>
                                        <asp:Button ID="btnSavetxtUpgradeDays" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Tax" TabIndex="79" OnClick="btnSavetxtUpgradeDays_Click" UseSubmitBehavior="false"></asp:Button>
                          
                            </center>
                                 <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        </div>
                                </asp:View>

                            </asp:MultiView>
                        </td>
                    </tr>
                </table>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
