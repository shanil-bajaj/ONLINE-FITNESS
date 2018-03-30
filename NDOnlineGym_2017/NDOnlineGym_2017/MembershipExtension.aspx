<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MembershipExtension.aspx.cs" Inherits="NDOnlineGym_2017.MembershipExtension" EnableEventValidation="false" %>

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
        
        .GridView a:focus {color: orangered;}
        .GridView a:hover {color: orangered;}        

        .remove {
            font-size: 18px;
            font-weight: bold;
        }

        .hideGridColumn {
            display: none;
        }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
    
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="JS/Common-Script.js"></script>
     <script>
         //function RestrictSpaceSpecial(e) {
         //    try {
         //        if (window.event) {
         //            var charCode = window.event.keyCode;
         //        }
         //        else if (e) {
         //            var charCode = e.which;
         //        }
         //        else { return true; }
         //        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
         //            return false;
         //        }
         //        return true;
         //    }
         //    catch (err) {
         //        alert(err.Description);
         //    }
         //}
    </script>
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
                <div id="divMemExtension" runat="server">
                    <div class="form-name-header">
                        <h3>Membership Extension
                            <div class="navigation">
                                <ul>                                    
                                    <li>Course &nbsp; > &nbsp;</li>
                                    <li>Membership Extension</li>
                                </ul>
                            </div>
                        </h3>
                    </div>
                </div>

                <div id="divMemExtensionDetails" runat="server" visible="false">
                    <div class="form-name-header">
                        <h3>Extension Details 
                            <div class="navigation">
                                <ul>                                    
                                    <li>Course &nbsp; > &nbsp;</li>
                                    <li>Extension Details</li>
                                </ul>
                            </div>
                        </h3>
                    </div>
                </div>
                

            <div class="divForm">
                <div id="divFormDetails" runat="server">
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
                                <asp:TextBox ID="txtMemberId" runat="server" Style="width: 80px; padding: 3px 5px;" AutoPostBack="true" OnTextChanged="txtMemberId_TextChanged" TabIndex="1"
                                    onkeypress="return RestrictSpaceSpecial(event);" AutoCompleteType="Disabled"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtFirstName1" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="2" Enabled="false"></asp:TextBox>

                            </td>
                            <td>
                                <asp:TextBox ID="txtLastName1" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="3" Enabled="false"></asp:TextBox>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGender1" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="4" Enabled="false">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:TextBox ID="txtContact1" runat="server" Style="width: 130px; padding: 3px 5px;" AutoPostBack="true" OnTextChanged="txtContact1_TextChanged" TabIndex="5"
                                    onkeypress="return RestrictSpaceSpecial(event);" MaxLength="11" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail1" runat="server" Style="width: 180px; padding: 3px 5px;" TabIndex="6" Enabled="false"></asp:TextBox>
                            </td>

                            <td>
                                <%--<asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="7" />--%>
                                <asp:CheckBox ID="chkExecutive" runat="server" Checked="true"  onChange="ChExecutive();" TabIndex="7" />                               
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlExecutive" runat="server" Style="width: 150px;" Enabled="false" CssClass="ddl" TabIndex="8">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvddlExecutive" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlExecutive"
                                    ErrorMessage="Select Executive " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                            </td>                                                               
                        </tr>
                    </table>
                </div>

                <%--End Member Details--%>


                <%--Existing Course Details--%>

                <div class="form-header">
                    <h4>&#10148; Existing Course Details  </h4>
                </div>
                <div class="form-panel">
                    <div style="width: 1000px; height: 150px; overflow-y: scroll">

                        <asp:GridView ID="gvExistingCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                            DataKeyNames="Course_AutoID" EmptyDataText="No record found." Width="1000px" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView"
                            AllowPaging="True">
                            <Columns>

                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" OnCommand="btnEdit_Command"
                                            CommandArgument='<%#Eval("Course_AutoID")%>' Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" TabIndex="9" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Receipt NO" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0: dd-MM-yyyy}" />
                                <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0: dd-MM-yyyy}" />
                                

                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>


                    </div>

                </div>

                <%--Existing Course Details--%>

                <%--Membership Extension--%>

                <div class="form-header">
                    <h4>&#10148; Membership Extension  </h4>
                </div>

                <div class="form-panel">

                     <table>
                        <tr>
                            <td>
                                <strong>Extension Date :</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExtensionDate" runat="server" CssClass="inp-txt" style="width:100px" TabIndex="10"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtExtensionDate_CalendarExtender" runat="server"
                                 BehaviorID="txtExtensionDate_CalendarExtender" TargetControlID="txtExtensionDate" Format="dd-MM-yyyy" />
                            </td>
                        </tr>
                    </table>


                    <div style="width: 1000px; height: auto; overflow-x: scroll;">
                        <asp:GridView ID="GvExtensionAssign" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found." Width="1000px" GridLines="None" CellPadding="5"
                             OnRowDeleting="GvExtensionAssign_RowDeleting" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" TabIndex="11">

                            <Columns>
                                <asp:CommandField HeaderText="Remove" ShowDeleteButton="true" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" />

                                <asp:BoundField HeaderText="Course ID" DataField="Course_AutoID" HeaderStyle-HorizontalAlign="left"
                                    HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn"  />
                                <asp:BoundField HeaderText="Receipt NO" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Start Date" DataField="StartDate" HeaderStyle-HorizontalAlign="left" DataFormatString="{0: dd-MM-yyyy}" />
                                <asp:BoundField HeaderText="End Date" DataField="EndDate" HeaderStyle-HorizontalAlign="left" DataFormatString="{0: dd-MM-yyyy}" />

                                <asp:TemplateField HeaderText="Extend Days">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtendDays" runat="server" Text='<%#Eval("ExtendDays")%>' AutoPostBack="true" OnTextChanged="txtsExtDay_TextChanged"
                                            TabIndex="11" Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField HeaderText="New End date" DataField="NewEndDate" HeaderStyle-HorizontalAlign="left" DataFormatString="{0: dd-MM-yyyy}" />--%>

                                <asp:TemplateField HeaderText="Next End Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNextEndDate" runat="server" Text='<%#Eval("NewEndDate","{0:dd-MM-yyyy}")%>' Enabled="false" Width="100px"></asp:TextBox>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Extension Reason">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExtensionReason" runat="server" Text='<%#Eval("ExtensionReason")%>' TabIndex="11"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>


                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                    </div>

                </div>
                <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click" CssClass="form-btn" TabIndex="12"
                    OnClientClick="this.disabled = true;" UseSubmitBehavior="false" />
                <asp:Button ID="btnCancle" runat="server" Text="Clear" OnClick="btnCancle_Click" CssClass="form-btn" TabIndex="13" />
             </center>

                <%--End Membership Extension--%>
                </div>
                <div id="divsearch" runat="server" visible="false">
                <div class="divForm">
                    <div class="form-header">
                        <h4>&#10148;Search By </h4>
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
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="14" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="15" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" TabIndex="16" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="17" Width="100px">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="MemberID">Member Id</asp:ListItem>
                                        <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                        <asp:ListItem Value="MFName">First Name</asp:ListItem>
                                        <asp:ListItem Value="MLName">Last Name</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="18" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                </td>
                                
                                  <td>
                                    <asp:Button ID="btnSearchCategory" runat="server" CssClass="form-btn" TabIndex="19" Text="Date with category" OnClick="btnSearchCategory_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" CssClass="form-btn" TabIndex="20" Text="Clear" OnClick="btnRefresh_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="btnExpord" runat="server" CssClass="form-btn" TabIndex="21" Text="Export To Excel" OnClick="btnExpord_Click"/>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="width: 1000px; height: auto; overflow-x: auto;">
                        <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div>

                        <asp:GridView ID="gvMembershipExtension" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found." 
                            PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" PageSize="20" 
                            OnPageIndexChanging="gvMembershipExtension_PageIndexChanging" >
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnExtensionEdit" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Extension_AutoID")%>' OnCommand="btnExtensionEdit_Command" TabIndex="22"
                                            style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnExtensionDelete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Course_AutoID")%>' OnCommand="btnExtensionDelete_Command"
                                            OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="22"                                         
                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png'); background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                         
                                <asp:boundfield HeaderText="Extend Date" DataField="ExtensionDate" dataformatstring="{0: dd-MM-yyyy}" />       
                                <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="First Name" DataField="FName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Last Name" DataField="LName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />   
                                <asp:BoundField HeaderText="Package Name" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                <asp:boundfield HeaderText="Course Start Date" DataField="CourseStartDate" dataformatstring="{0: dd-MM-yyyy}" />
                                <asp:boundfield HeaderText="Course End Date" DataField="OldEndDate" dataformatstring="{0: dd-MM-yyyy}" />                                
                                <asp:BoundField HeaderText="Extension Days" DataField="ExtensionDays" HeaderStyle-HorizontalAlign="left" />     
                                <asp:boundfield HeaderText="Course New End Date" DataField="CourseNewEndDate" dataformatstring="{0: dd-MM-yyyy}" />                                                           
                                <asp:BoundField HeaderText="Extension Reason" DataField="ExtensionReason" HeaderStyle-HorizontalAlign="left" />                                
                                <asp:BoundField HeaderText="Executive Name" DataField="ExecutiveName" HeaderStyle-HorizontalAlign="left" />
                                
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                    </div>

                </div>
                </div>
            </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExpord" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
