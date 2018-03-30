<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Expense.aspx.cs" Inherits="NDOnlineGym_2017.Expense" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>

    <script>
        function RestrictSpaceSpecial(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
        }
    
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
    </script>

    <script>

        function ddlPaymentChange() {
            var _ddlPaymode = document.getElementById('<%= ddlPaymentMode.ClientID%>');            
            var _ddlPayModeVal = _ddlPaymode.options[_ddlPaymode.selectedIndex].text;
            var _txtPayDetails=document.getElementById('<%= txtPayDetails.ClientID %>');
            if (_ddlPayModeVal == "--Select--" || _ddlPayModeVal == "Cash") {
                _txtPayDetails.value = "";
                _txtPayDetails.disabled = true;
            }
            else {
                _txtPayDetails.disabled = false;
            }
        }

        //Disable enable executive dropdown on check box
        function ddlChange() {            
            var _chkExecutive = document.getElementById('<%= chkExecutive.ClientID %>');
            document.getElementById('<%= ddlExecutive.ClientID %>').disabled = _chkExecutive.checked;           
        }
    </script>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
                <div class="form-name-header">
                    <h3>Expense
                 <div class="navigation">
                     <ul>
                         <li>User Setting &nbsp; > &nbsp;</li>
                         <li>Expense  &nbsp; > &nbsp;</li>
                         <li>Add Expense </li>
                     </ul>
                 </div>
                    </h3>
                </div>

                <div class="divForm">
                    <div class="form-header">
                        <h4>&#10148; Add Expense </h4>
                    </div>
                    <div id="divFormDetails" runat="server">
                        <div class="form-panel">
                            <table style="width: 100%;">

                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Expense ID </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtExpenseID" runat="server" CssClass="txt" TabIndex="1" OnTextChanged="txtExpenseID_TextChanged" onkeypress="return RestrictSpaceSpecial(event);"
                                                         AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtExpenseID"
                                                        ErrorMessage="Enter Expense ID" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Date</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtDate_Calender" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtDate" Format="dd-MM-yyyy" />

                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Name </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtName" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="3" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtName"
                                                        ErrorMessage="Enter Name" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>


                                </tr>

                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Expense Group</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:DropDownList ID="ddlExpenseGroup" runat="server" CssClass="ddl" TabIndex="4">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlExpenseGroup" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlExpenseGroup"
                                                        ErrorMessage="Select Expense Group" SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Taxable Amount</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txt" TabIndex="5" OnTextChanged="txtAmount_TextChanged1" AutoPostBack="true" 
                                                        onkeypress="return RestrictSpaceSpecial(event);" AutoCompleteType="Disabled">  </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtTaxableAmt" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtAmount"
                                                        ErrorMessage="Enter Taxable Amount" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Tax</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtTaxableAmt" runat="server" CssClass="txt" TabIndex="6" onkeypress="return RestrictSpaceSpecial(event);" OnTextChanged="txtTaxableAmt_TextChanged" 
                                                        AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>                         
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Total Amount</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="txt" TabIndex="7" onkeypress="return RestrictSpaceSpecial(event);" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtTotalAmt"
                                                        ErrorMessage="Enter Total Amount" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <br />                                
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Note 1 </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtNote1" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="8" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtNote1"
                                                        ErrorMessage="Enter Note 1" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>


                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Note 2</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtNote2" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="9" AutoCompleteType="Disabled"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>




                                </tr>
                                <tr>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Payment Mode </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                   <%-- <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" AutoPostBack="true" TabIndex="10">--%>
                                                     <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="ddl" OnChange="ddlPaymentChange();" TabIndex="10">
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlPaymentMode" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlPaymentMode"
                                                        ErrorMessage="Select Payment Mode" SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Pay Details</span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <asp:TextBox ID="txtPayDetails" runat="server" onChange="javascript:capFirst(this);" CssClass="txt" TabIndex="11" Enabled="false" AutoCompleteType="Disabled"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cols">
                                        <table>
                                            <tr>
                                                <td style="width: 45%;"><span class="lbl">Executive </span></td>
                                                <td style="width: 55%; text-align: left;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <%--<asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="12" /></td>--%>
                                                                <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnChange="ddlChange();" TabIndex="12" /></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="13" Enabled="false">
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

                            <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="14" OnClick="btnSave_Click"
                  OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" />
              <%--   <asp:Button ID="btnView" runat="server"  Text="View" CssClass="form-btn" TabIndex="11" />--%>
                <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="form-btn" TabIndex="15" OnClick="btnCancel_Click"/>
             </center>

                        </div>
                    </div>
                    <div id="divsearch" runat="server">
                        <div class="form-header">
                            <h4>&#10148; Expense Details </h4>
                        </div>
                        <table>
                            <tr>
                                <th>Form Date</th>
                                <th>To Date</th>
                                <th></th>
                                <th>Category</th>                                                                
                                <th>Search By</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="16" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="17" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearch_Click" TabIndex="18" />
                                </td>                                
                                <td>
                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="19" Width="140px">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Expense Id">Expense Id</asp:ListItem>
                                        <asp:ListItem Value="Name">Name</asp:ListItem>
                                        <asp:ListItem Value="Expgrp_Name">Expense Group</asp:ListItem>
                                        <asp:ListItem Value="PaymentMode">Payment Mode</asp:ListItem>
                                        <asp:ListItem Value="Executive">Executive</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                                
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="20" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchWiDate" runat="server" CssClass="form-btn" TabIndex="21" Text="Date with category" OnClick="btnSearchWiDate_Click" />
                                </td>
                                 <td>
                                    <asp:Button ID="btnExpord" runat="server" CssClass="form-btn" TabIndex="22" Text="Export To Excel" OnClick="btnExpord_Click"/>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">
                             <div style="margin:10px 0px 10px 10px">
                                 <table>
                                     <tr>
                                         <td>
                                              <asp:Label ID="lblCountHead" runat="server" Text="Total Records = " Font-Bold="true" ForeColor="Black"></asp:Label>
                                              <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                         </td>
                                         <td>

                                         </td>
                                         <td>
                                              <asp:Label ID="Label1" runat="server" Text="Total Amount = " Font-Bold="true" ForeColor="Black"></asp:Label>
                                              <asp:Label ID="lblTotal" runat="server" Text="0" Font-Bold="true" ForeColor="Green"></asp:Label>
                                         </td>
                                     </tr>
                                 </table>
                           
                                </div>
                            <asp:GridView ID="gvExpenseDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Exp_AutoID" EmptyDataText="No record found." Width="1000px"
                                Font-Size="13px" CssClass="GridView" PagerStyle-CssClass="pager" GridLines="None" AllowPaging="True" PageSize="20"
                                OnPageIndexChanging="gvExpenseDetails_PageIndexChanging">

                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Exp_AutoID")%>' TabIndex="23"
                                                style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="23" OnCommand="btnDelete_Command"
                                                CommandArgument='<%#Eval("Exp_AutoID")%>' 
                                                style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnVouchaer" runat="server" TabIndex="23" OnCommand="btnVouchaer_Command" CommandArgument='<%#Eval("Exp_AutoID")%>'
                                                style="background-image:url('../NotificationIcons/images (5).png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Voucher" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Exp Id" DataField="Exp_ID1" HeaderStyle-HorizontalAlign="left" ControlStyle-Width="60px" />                                    
                                    <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <b>Exp Date</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Exp_Date","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Tax" DataField="TaxableAmount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Total Amount" DataField="TotalAmount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Note 1" DataField="Note1" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Note 2" DataField="Note2" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Pay Details" DataField="Pay_Details" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Exp Grp" DataField="ExpGroupName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Pay Mode" DataField="PayMode" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Pay Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
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
            <asp:PostBackTrigger ControlID="btnExpord" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
