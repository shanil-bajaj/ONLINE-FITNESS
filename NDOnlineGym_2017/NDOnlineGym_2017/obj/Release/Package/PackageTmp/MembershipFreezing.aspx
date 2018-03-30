<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MembershipFreezing.aspx.cs" Inherits="NDOnlineGym_2017.MembershipFreezing" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="JS/Common-Script.js"></script>

    <%--<script>

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

        //Disable enable executive dropdown on check box
        function ChExecutive() {
            var _chkExecutive = document.getElementById('<%= chkExecutive.ClientID %>');
            document.getElementById('<%= ddlExecutive.ClientID %>').disabled = _chkExecutive.checked;
        }

        function showConfirmation(myurl) {
            if (confirm("First Unblock Member") == true) {
                window.parent.location.href = myurl;
            }
        }
    </script>

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

        .GridView a:focus {
            color: orangered;
        }

        .GridView a:hover {
            color: orangered;
        }

        .remove {
            font-size: 18px;
            font-weight: bold;
        }

        .hideGridColumn {
            display: none;
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
                <div id="divMemFrezzing" runat="server">
                    <div class="form-name-header">
                        <h3>Membership Freezing
                            <div class="navigation">
                                <ul>
                                    <li>Course &nbsp; > &nbsp;</li>
                                    <li>Membership Freezing</li>
                                </ul>
                            </div>
                        </h3>
                    </div>
                </div>

                <div id="divMemFrezzingDetails" runat="server" visible="false">
                    <div class="form-name-header">
                        <h3>Freezing Details
                            <div class="navigation">
                                <ul>
                                    <li>Course &nbsp; > &nbsp;</li>
                                    <li>Freezing Details</li>
                                </ul>
                            </div>
                        </h3>
                    </div>
                </div>


                <div id="divFormDetails" runat="server">
                    <div class="divForm">
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
                                        <asp:TextBox ID="txtMemberId" runat="server" Style="width: 80px; padding: 3px 5px;" TabIndex="1" OnTextChanged="txtMemberId_TextChanged"
                                            onkeypress="return RestrictSpaceSpecial(event);" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName1" runat="server" Style="width: 120px; padding: 3px 5px;" TabIndex="2" Enabled="false"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName1" runat="server" Style="width: 120px; padding: 3px 5px;" TabIndex="3" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender1" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="4" Enabled="false">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContact1" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="5" OnTextChanged="txtContact1_TextChanged" AutoPostBack="true"
                                            onkeypress="return RestrictSpaceSpecial(event);" MaxLength="11" AutoCompleteType="Disabled"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail1" runat="server" Style="width: 170px; padding: 3px 5px;" TabIndex="6" Enabled="false"></asp:TextBox>
                                    </td>                                                                            
                                    <td>
                                        <%--<asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true" TabIndex="7" /></td>--%>
                                        <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" onChange="ChExecutive();" TabIndex="7" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddlExecutive" runat="server" CssClass="ddl" TabIndex="8" Enabled="false">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        </asp:DropDownList>
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
                            <div style="width: 100%; height: 150px; overflow-y: scroll">

                                <asp:GridView ID="gvExistingCourse" runat="server" AutoGenerateColumns="false" GridLines="None" CellPadding="5"
                                    DataKeyNames="Course_AutoID" EmptyDataText="No record found." Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" Width="1066px"
                                    AllowPaging="True" OnPageIndexChanging="gvExistingCourse_PageIndexChanging" PageSize="20">
                                    <Columns>

                                        <asp:TemplateField ControlStyle-Width="5px" HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" OnCommand="btnEdit_Command"
                                                    CommandArgument='<%#Eval("Course_AutoID")%>' Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" TabIndex="9" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField HeaderText="Pack_AutoID" DataField="Pack_AutoID" HeaderStyle-HorizontalAlign="left" />--%>
                                        <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0: dd-MM-yyyy}" />
                                        <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0: dd-MM-yyyy}" />

                                        <%-- <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Eval("StartDate","{0:dd-MM-yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Eval("EndDate","{0:dd-MM-yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    </Columns>
                                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                    <RowStyle Height="20px" />
                                    <AlternatingRowStyle Height="20px" BackColor="White" />
                                </asp:GridView>


                            </div>

                        </div>
                        <%--Existing Course Details--%>

                        <%--Membership Freezing--%>
                        <div class="form-header">
                            <h4>&#10148; Membership Freezing  </h4>
                        </div>

                        <table>
                            <tr>
                                <td>
                                    <strong>Freezing Date :</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFreezedDate" runat="server" CssClass="inp-txt" Style="width: 100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFreezingDate_CalendarExtender" runat="server"
                                        BehaviorID="txtFreezingDate_CalendarExtender" TargetControlID="txtFreezedDate" Format="dd-MM-yyyy" />
                                </td>
                            </tr>
                        </table>
                       
                         <div class="form-panel">

                            <div style="width: 1000px; height: auto; overflow-x: scroll;">
                                <asp:GridView ID="GvFreezingAssign" runat="server" AutoGenerateColumns="false" DataKeyNames="Course_AutoID" OnRowDeleting="GvFreezingAssign_RowDeleting"
                                    EmptyDataText="No record found." GridLines="None" CellPadding="5" Font-Size="13px" PagerStyle-CssClass="pager"
                                    CssClass="GridView" Width="1080px">

                                    <Columns>
                                        <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300">
                                            <%--<HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="remove" ForeColor="#CC3300" />--%>
                                        </asp:CommandField>

                                        <asp:BoundField HeaderText="Course ID" DataField="Course_AutoID" HeaderStyle-HorizontalAlign="left"
                                            HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                        <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                        <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0: dd-MM-yyyy}" />
                                        <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0: dd-MM-yyyy}" />

                                        <asp:TemplateField HeaderText="Freezing Date">
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="txtFreezingDate" runat="server" Text='<%#Eval("FreezingStartDate")%>' DataFormatString="{0:dd-MM-yyyy}" Width="100px" ></asp:TextBox>--%>
                                                <asp:TextBox ID="txtFreezingDate" runat="server" Text='<%#Eval("FreezingStartDate","{0:dd-MM-yyyy}")%>' OnTextChanged="txtFreezingDate_TextChanged"
                                                    AutoPostBack="true" Width="100px" TabIndex="10"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtFreezingDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtFreezingDate_CalendarExtender" TargetControlID="txtFreezingDate" Format="dd-MM-yyyy" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Extend(Days)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtExtends" runat="server" Text='<%#Eval("FreezingDays")%>' OnTextChanged="txtExtends_TextChanged" AutoPostBack="true"
                                                    Width="100px" TabIndex="10"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Freezing End Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFreezingEndDate" runat="server" Text='<%#Eval("FreezingEndDate","{0:dd-MM-yyyy}")%>' Enabled="false" Width="100px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtFreezingEndDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtFreezingEndDate_CalendarExtender" TargetControlID="txtFreezingEndDate" Format="dd-MM-yyyy" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next End Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNextEndDate" runat="server" Text='<%#Eval("CourseNewEndDate","{0:dd-MM-yyyy}")%>' Enabled="false" Width="100px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtNextEndDate_CalendarExtender" runat="server"
                                                    BehaviorID="txtNextEndDate_CalendarExtender" TargetControlID="txtNextEndDate" Format="dd-MM-yyyy" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Freezing Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFreezingReason" runat="server" Text='<%#Eval("FreezingReason")%>' TabIndex="10"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Executive Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExecutive" runat="server" Text='<%#Eval("StaffName")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <%--                                <asp:TemplateField HeaderText="Executive Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExecutive" runat="server" Text='<%# Eval("StaffName") %>' Visible = "false" />
                                        <asp:DropDownList ID="ddlExecutive" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    </Columns>

                                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                    <RowStyle Height="20px" />
                                    <AlternatingRowStyle Height="20px" BackColor="White" />
                                </asp:GridView>


                            </div>

                        </div>

                        <center class="btn-section">
                            <asp:Button ID="btnSave" runat="server" Text="Save"  TabIndex="11" CssClass="form-btn" OnClick="btnSave_Click" 
                                OnClientClick="this.disabled = true;" UseSubmitBehavior="false" />
                            <asp:Button ID="btnCancle" runat="server" Text="Clear" TabIndex="12" CssClass="form-btn" OnClick="btnCancle_Click"  />
                        </center>
                        <%--End Membership Freezing--%>
                    </div>
                </div>

                <div id="divsearch" runat="server" visible="false">
                    <%--Searching Section--%>
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
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="13" Width="110px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="14" Width="110px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" OnClick="btnSearch_Click" TabIndex="15" />
                                    </td>

                                    <td>
                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="16" Width="100px">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="MemberID">Member Id</asp:ListItem>
                                            <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                            <asp:ListItem Value="MFName">First Name</asp:ListItem>
                                            <asp:ListItem Value="MLName">Last Name</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="17" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:Button ID="btnSearchCategory" runat="server" CssClass="form-btn" TabIndex="18" Text="Date with category" OnClick="btnSearchCategory_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" CssClass="form-btn" TabIndex="19" Text="Clear" OnClick="btnRefresh_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExpord" runat="server" CssClass="form-btn" TabIndex="20" Text="Export To Excel" OnClick="btnExpord_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 25px;">
                             <div style="margin:10px 0px 10px 10px">
                                <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                            </div>  
                            <asp:GridView ID="gvMemberFreezdetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                                PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True"
                                OnPageIndexChanging="gvMemberFreezdetails_PageIndexChanging" PageSize="20">
                                <Columns>

                                    <asp:TemplateField ControlStyle-Width="50px" HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFreezEdit" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("Freezing_AutoID")%>' OnCommand="btnFreezEdit_Command" TabIndex="21" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFreezingDelete" runat="server" CausesValidation="false" Text="Delete" CommandArgument='<%#Eval("Freezing_AutoID")%>' OnCommand="btnFreezingDelete_Command" TabIndex="21"
                                                OnClientClick="return confirm('Are you sure you want to delete?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Freezing Date" DataField="FreezingDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="First Name" DataField="FName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Last Name" DataField="LName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Package Name" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Course Start Date" DataField="CourseStartDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="Course End Date" DataField="CourseOldEndDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="Freez Start Date" DataField="FreezingStartDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="Freezing Days" DataField="FreezingDays" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Freez End Date" DataField="FreezingEndDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="Course New End Date" DataField="CourseNewEndDate" DataFormatString="{0: dd-MM-yyyy}" />
                                    <asp:BoundField HeaderText="Frezzing Reason" DataField="FreezingReason" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Executive Name" DataField="ExecutiveName" HeaderStyle-HorizontalAlign="left" />

                                </Columns>
                                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                <RowStyle Height="20px" />
                                <AlternatingRowStyle Height="20px" BackColor="White" />
                            </asp:GridView>
                        </div>

                    </div>
                    <%--End Searching Section--%>
                </div>


            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExpord" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
