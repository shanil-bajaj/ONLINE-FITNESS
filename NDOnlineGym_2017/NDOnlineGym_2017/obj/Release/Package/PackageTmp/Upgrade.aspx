<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Upgrade.aspx.cs" EnableEventValidation="false" Inherits="NDOnlineGym_2017.Upgrade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    
    <style>
        input:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .GridView {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

        .GridView1 {
            width: max-content;
        }

        .GridView a /** FOR THE PAGING ICONS  **/ {
            background-color: Transparent;
            padding: 5px 5px 5px 5px;
            color: black;
            text-decoration: none;
            font-weight: bold;
        }

        .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
            color: #fff;
            padding: 5px 5px 5px 5px;
        }

        GridView a:focus {
            color: orangered;
        }

        .GridView a:hover {
            color: orangered;
        }

        .remove {
            font-size: 18px;
            font-weight: bold;
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

    <script src="JS/OfflineJavaScript.js"></script>
    <script src="https://code.jquery.com/jquery-1.11.3.js"></script>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="JS/Common-Script.js"></script>

   <%-- <script>

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

    </script>--%>

     <script>
         function showConfirmation(myurl) {
             if (confirm("First Unblock Member") == true) {
                 window.parent.location.href = myurl;                 
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
                <div class="form-name-header" id="divMemberUpgrade" runat="server">
                    <h3>Membership Upgrade
                        <div class="navigation">
                            <ul>   
                                <li>Course &nbsp; > &nbsp;</li>                             
                                <li>Upgrade &nbsp; > &nbsp;</li>
                                <li>Membership Upgrade</li>
                            </ul>
                        </div>                   
                    </h3>
                </div>

                <div class="form-name-header" id="divUpgradeDetails" runat="server" visible="false">
                    <h3>Upgrade Details
                        <div class="navigation">
                            <ul>        
                                <li>Course &nbsp; > &nbsp;</li>                         
                                <li>Upgrade &nbsp; > &nbsp;</li>
                                <li>Upgrade Details</li>
                            </ul>
                        </div>                   
                    </h3>
                </div>

                <div class="divForm" id="divFormDetails" runat="server">
                    <%--Member Details--%>

                    <div class="form-header">
                        <h4>&#10148; Member Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table id="tableInfo" runat="server" style="margin-top: 10px;">
                            <tr>
                                <th><span class="error">*</span>ID</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Gender</th>
                                <th><span class="error">*</span>Contact</th>
                                <th>Email</th>
                                <th></th>
                                <th>Executive</th>
                            </tr>

                            <tr id="row1" runat="server">
                                <td>
                                    <asp:TextBox ID="txtMemberID1" runat="server" Style="width: 80px; padding: 3px 5px;" OnTextChanged="txtMemberID1_TextChanged"
                                        onkeypress="return RestrictSpaceSpecial(event);" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMemberID1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtMemberID1"
                                        ErrorMessage="Enter ID" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtFirstName" runat="server" Style="width: 150px; padding: 3px 5px;" Enabled="false" TabIndex="2"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server" Style="width: 150px; padding: 3px 5px;" Enabled="false" TabIndex="3"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" Enabled="false" TabIndex="4">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtContact" runat="server" Style="width: 120px; padding: 3px 5px;" OnTextChanged="txtContact_TextChanged"
                                        onkeypress="return RestrictSpaceSpecial(event);" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvContact" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtContact"
                                        ErrorMessage="Enter Contact" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Style="width: 175px; padding: 3px 5px;" TabIndex="6" Enabled="false"></asp:TextBox>

                                </td>
                                <td>
                                   <%-- <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" TabIndex="7" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" />--%>
                                     <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" TabIndex="7" onChange="ChExecutive();" />
                                </td>
                                <td>                                    
                                    <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="8" Enabled="false">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <%--End Member Details--%>

                    <%--Old Course Details--%>

                    <div class="form-header">
                        <h4>&#10148; Old Course Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <th>Refer Receipt ID</th>
                                <th>Total fees</th>
                                <th>Paid Fees</th>
                                <th>Balance Fees</th>
                            </tr>

                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlReceipt" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlReceipt_SelectedIndexChanged" TabIndex="9">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotal" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="10" Enabled="false" Text="0"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtPaid" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="11" Enabled="false" Text="0"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtBalance" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="12" Enabled="false" Text="0"></asp:TextBox>

                                </td>

                            </tr>

                        </table>

                        <div id="gvOCourse" runat="server" visible="false" style="width: 1000px; height: 150px; overflow-y: scroll">

                            <asp:GridView ID="gvOldCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                DataKeyNames="Pack_AutoID" EmptyDataText="No record found." Width="970px" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView"
                                AllowPaging="True" >
                                <Columns>

                                    <asp:BoundField HeaderText="Package Id" DataField="Pack_AutoID" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Qty" DataField="Qty" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Discount" DataField="Discount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Total" DataField="Total_Fees" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0: dd-MM-yyyy}" />

                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>


                        </div>

                    </div>

                    <%--Old Course Details--%>

                    <%-- Course Details--%>

                    <div class="form-header">
                        <h4>&#10148;  Course Details  </h4>
                    </div>
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 20%;">
                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="13">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Package">Package</asp:ListItem>
                                        <asp:ListItem Value="Duration">Duration</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtPackageSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="14" OnTextChanged="txtPackageSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                                <td style="width: 45%;">
                                    <%-- <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" />--%>
                                </td>
                            </tr>
                        </table>


                        <div style="width: 1000px; height: 250px; overflow-y: scroll">

                            <asp:GridView ID="gvCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                DataKeyNames="Pack_AutoID" EmptyDataText="No record found." Width="970px" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView"
                                AllowPaging="True" TabIndex="15" PageSize="20" OnPageIndexChanging="gvCourse_PageIndexChanging" >
                                <Columns>

                                    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" TabIndex="15" OnClick="btnEdit_Click"
                                                CommandArgument='<%#Eval("Pack_AutoID")%>' Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Package ID" DataField="Pack_AutoID" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Type" DataField="Type" HeaderStyle-HorizontalAlign="left" Visible="false" />
                                    <asp:BoundField HeaderText="FromTime" DataField="FromTime" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="ToTime" DataField="ToTime" HeaderStyle-HorizontalAlign="left" />

                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>


                        </div>

                    </div>

                    <%-- Course Details--%>

                    <%--Upgrade Membership--%>

                    <div class="form-header">
                        <h4>&#10148; Upgrade Membership  </h4>
                    </div>

                    <div>
                        <table style="margin-top: 10px;">
                            <tr>
                                <th>Upgrade Date: </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtUpgradeDate" runat="server" CssClass="txt" TabIndex="16" Width="120px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvregdate" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtUpgradeDate"
                                        ErrorMessage="Enter Upgrade Date" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:CalendarExtender ID="txtUpgradeDate_CalendarExtender" runat="server" BehaviorID="txtUpgradeDate_CalendarExtender" TargetControlID="txtUpgradeDate" Format="dd-MM-yyyy" />
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div id="divPakageAssign" runat="server" visible="false" class="form-panel">
                        <div style="width: 1000px; height: auto; overflow-x: scroll;">
                            <asp:GridView ID="GvPakageAssign" runat="server" AutoGenerateColumns="false" DataKeyNames="Pack_AutoID" OnRowDeleting="GvPakageAssign_RowDeleting"
                                EmptyDataText="No record found." Width="700px" GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                                CssClass="GridView" AllowPaging="True" TabIndex="17">
                                <Columns>

                                    <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                        ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" />
                                    <asp:BoundField HeaderText="Package ID" DataField="Pack_AutoID" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />

                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:UpdatePanel runat="server" ID="UpId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtsDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' Enabled="false" Width="95px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="txtsDate_CalendarExtender" runat="server" BehaviorID="txtsDate_CalendarExtender" TargetControlID="txtsDate" Format="dd-MM-yyyy" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEndate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' Enabled="false" Width="95px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtEndate_CalendarExtender" runat="server" BehaviorID="txtEndate_CalendarExtender" TargetControlID="txtEndate" Format="dd-MM-yyyy" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmt" runat="server" Text='<%#Eval("Amount")%>' Enabled="false" Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity")%>' Enabled="false" Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTotal" runat="server" Text='<%#Eval("Total")%>' Enabled="false" Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:UpdatePanel runat="server" ID="DiscId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDisc" runat="server" Text='<%#Eval("Discount")%>' AutoPostBack="true" OnTextChanged="txtDisc_TextChanged"
                                                        TabIndex="17" Width="95px"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Final Total">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFinalTotal" runat="server" Enabled="false" Text='<%#Eval("FinalTotal")%>' Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>


                        </div>
                        <div style="margin-left:850px;">
                            <h3>Total Fee :<asp:Label ID="lblTotalFeeDue" runat="server" Text="0"></asp:Label>
                            </h3>
                            <h3>Paid Fee :<asp:Label ID="lblPaidFee" runat="server" Text="0"></asp:Label>
                            </h3>
                            <h3>Balance :<asp:Label ID="lblBalance" runat="server" Text="0"></asp:Label>
                            </h3>
                        </div>
                    </div>


                    <%--End Upgrade Membership--%>
                    <div>
                        <center class="btn-section">
                        <asp:Button ID="btnUpgrade" runat="server" Text="Save"  TabIndex="18" CssClass="form-btn" OnClick="btnUpgrade_Click" ValidationGroup="a" 
                            OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true;" UseSubmitBehavior="false" />
                        <asp:Button ID="btnCancle" runat="server" Text="Clear" TabIndex="19" CssClass="form-btn" OnClick="btnCancle_Click"  />
                     </center>
                    </div>
                </div>

                <%--Start Search Details --%>

                <div id="divsearch" runat="server" visible="false">
                    <div class="form-header">
                        <h4 style="float: left;">&#10148; Search Category</h4>
                    </div>

                    <div class="form-panel">
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
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="20" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="21" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" TabIndex="22" Text="Search" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcategory" runat="server" CssClass="ddl" TabIndex="23" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Member_ID"> Id</asp:ListItem>
                                        <asp:ListItem Value="Member_Name">Name</asp:ListItem>
                                        <asp:ListItem Value="Member_Contact">Contact</asp:ListItem>
                                        <asp:ListItem Value="Executive">Executive</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="24" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchCategory" runat="server" CssClass="form-btn" TabIndex="25" Text="Date with category" OnClick="btnSearchCategory_Click" />
                                </td>                                
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" CssClass="form-btn" TabIndex="26" Text="Clear" OnClick="btnRefresh_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="btnExpord" runat="server" CssClass="form-btn" TabIndex="27" Text="Export To Excel" OnClick="btnExpord_Click"/>
                                </td>
                            </tr>
                        </table>

                    </div>

                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">
                        <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div> 
                        <asp:GridView ID="gvMemberUpgradeDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Upgrade_AutoID" 
                            PagerStyle-CssClass="pager" CssClass="GridView GridView1" GridLines="None" CellPadding="5" AllowPaging="True"
                            PageSize="20" OnPageIndexChanging="gvMemberUpgradeDetails_PageIndexChanging" >
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditUpgrade" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Upgrade_AutoID")%>' OnCommand="btnEditUpgrade_Command" TabIndex="28" 
                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="left" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPreview" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Upgrade_AutoID")%>' TabIndex="28" Visible="false"
                                        style="background-image:url('../NotificationIcons/images (5).png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Preview" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField HeaderText="Rec No" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Mem Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Mem Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Old Package" DataField="OldPackage" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Old Duration" DataField="OldDuration" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Old Session" DataField="OldSession" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="New Package" DataField="NewPackage" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="New Duration" DataField="NewDuration" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="New Session" DataField="NewSession" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Upgrade Date" DataField="Upgrade_Date" HeaderStyle-HorizontalAlign="left" DataFormatString="{0: dd-MM-yyyy}" />
                                <asp:BoundField HeaderText="Start Date" DataField="New_StartDate" HeaderStyle-HorizontalAlign="left" DataFormatString="{0: dd-MM-yyyy}" />
                                <asp:BoundField HeaderText="End Date" DataField="New_EndDate" HeaderStyle-HorizontalAlign="left" DataFormatString="{0: dd-MM-yyyy}" />
                                <asp:BoundField HeaderText="Old Total Fees" DataField="Old_FinalTotal" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="New Total Fees" DataField="New_FinalTotal" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Executive" DataField="ExeName" HeaderStyle-HorizontalAlign="left" />


                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>
                    </div>

                </div>
                <%--End Search Details --%>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExpord" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
