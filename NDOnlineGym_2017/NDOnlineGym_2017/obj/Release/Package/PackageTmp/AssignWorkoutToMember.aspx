<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AssignWorkoutToMember.aspx.cs" Inherits="NDOnlineGym_2017.AssignWorkoutToMember" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <%-- <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>--%>
    <style>
        table td {
            padding-bottom: 10px;
        }

        input:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox {
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

        .errorborder {
            border: 1px solid red;
        }

        .form-panel {
            width: 100%;
            padding: 0px;
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

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script>
        function RestrictSpaceSpecial(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
        <div id="divWorkout" runat="server">
            <div class="form-name-header">
                <h3>Add Workout
                    <div class="navigation">
                        <ul>
                            <li>Status &nbsp; > &nbsp;</li>
                            <li>Body Assessment  &nbsp; > &nbsp;</li>
                            <li>Add Workout</li>
                        </ul>
                    </div>
                </h3>
            </div>
        </div>

        <div id="divWorkoutDetails" runat="server" visible="false">
            <div class="form-name-header">
                <h3>Workout Details
                    <div class="navigation">
                        <ul>
                            <li>Status &nbsp; > &nbsp;</li>
                            <li>Body Assessment  &nbsp; > &nbsp;</li>
                            <li>Workout Details</li>
                        </ul>
                    </div>
                </h3>
            </div>
        </div>

        <div id="divFormDetails" runat="server">
            <div class="divForm">
                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <div class="form-header" id="formheader1">
                            <h4>&#10148;Member Details </h4>
                        </div>
                        <div class="form-panel" id="formpanel1">
                            <table style="height: 80px;">
                                <tr>
                                    <th><span class="error">*</span>ID</th>
                                    <th><span class="error">*</span>Contact</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Gender</th>
                                    <th>Email ID</th>
                                </tr>
                                <tr id="row1" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtMemberID" runat="server" Style="width: 100px; padding: 3px 5px;" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged"
                                            onkeypress="return RestrictSpaceSpecial(event);" TabIndex="1"></asp:TextBox>        
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtContact" runat="server" Style="width: 150px; padding: 3px 5px;" OnTextChanged="txtContact_TextChanged" AutoPostBack="true"
                                            onkeypress="return RestrictSpaceSpecial(event);" TabIndex="2"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtFirst" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="3" Enabled="false"></asp:TextBox>

                                        <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDuration"
                                            ErrorMessage="Enter Duration"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator> --%>                             
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLast" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="4" Enabled="false"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtSession"
                                            ErrorMessage="Enter Session"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                         
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="5" Enabled="false">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator  ID="RequiredFieldValidator4" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlStatus"
                                            ErrorMessage="Select Status"  SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>   --%>                    
                                    </td>
                                    <%--<td>
                                <asp:TextBox ID="txtContact" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="5" OnTextChanged="txtContact_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revContact" CssClass="ErrorBox" SetFocusOnError="true" runat="server" ValidationExpression="^[0-9]{1,45}$" 
                                    ControlToValidate="txtContact" ErrorMessage="Field Must Be Numeric">
                                </asp:RegularExpressionValidator>

                            </td>--%>
                                    <td>
                                        <asp:TextBox ID="txtmail" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="6"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="form-header" id="Div1">
                    <h4>&#10148;Workout Assign Details </h4>
                </div>

                <div class="form-panel" id="Div3">
                    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="height: 80px;">
                                <tr>

                                    <th><span class="error">*</span>Programmer Name</th>
                                    <th>Assign Date</th>
                                    <th>From Date</th>
                                    <th>To Date</th>
                                    <th><span class="error">*</span>Workout Day</th>

                                </tr>
                                <tr id="Tr1" runat="server">
                                    <td>
                                        <asp:DropDownList ID="ddlProgrammer" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="7">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<br />
                                        <asp:RequiredFieldValidator ID="rfvProgrammer" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="
                                            "
                                            ErrorMessage="Select Name" SetFocusOnError="true" InitialValue="--Select--" ValidationGroup="aw"></asp:RequiredFieldValidator>--%>
                                        <%-- <asp:TextBox ID="txtDietitian" runat="server" Style="width: 250px; padding: 3px 5px;" AutoPostBack="true" TabIndex="7"></asp:TextBox>--%>
                                        <%-- <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                            Enabled="True" ServiceMethod="GetListofCountries" MinimumPrefixLength="1" EnableCaching="true"
                                            ServicePath="" TargetControlID="TxtID"> --%>                                            
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAssignDate" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="8"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtAssignDate_CalendarExtender1" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtAssignDate" Format="dd-MM-yyyy" />
                                        <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDuration"
                                            ErrorMessage="Enter Duration"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator> --%>                             
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfrmdte" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="9"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtfrmdte" Format="dd-MM-yyyy" />
                                        <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDuration"
                                            ErrorMessage="Enter Duration"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator> --%>                             
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttodte" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="10"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtTodate_CalendarExtender" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txttodte" Format="dd-MM-yyyy" />
                                        <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDuration"
                                            ErrorMessage="Enter Duration"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator> --%>                             
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWorkoutDay" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="11">
                                            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                            <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                            <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                            <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                            <asp:ListItem Value="Thusday">Thursday</asp:ListItem>
                                            <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                            <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<br />
                                        <asp:RequiredFieldValidator ID="rfvWorkoutDay" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlWorkoutDay"
                                            ErrorMessage="Select Workout Day" SetFocusOnError="true" ValidationGroup="aw" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                            </table>


                            <%--Course Details--%>

                            <div class="form-header">
                                <h4>&#10148; Workout Details  </h4>
                            </div>
                            <div class="form-panel">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 20%;">
                                            <%--<asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" AutoPostBack="true" TabIndex="12">--%>
                                            <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="12">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="MuscularGroup">Muscular Group</asp:ListItem>
                                                <asp:ListItem Value="WorkoutName">Workout Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" TabIndex="13"></asp:TextBox>
                                        </td>
                                        <td style="width: 45%;">
                                            <%--<asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" />--%>
                                        </td>
                                    </tr>
                                </table>

                                <div style="width: 1000px; height: auto; overflow-x: scroll;">

                                    <asp:GridView ID="gvWorkoutDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="WorkoutName_AutoID" GridLines="None"
                                        EmptyDataText="No record found." Width="970px"
                                        CssClass="GridView" Font-Size="13px" PagerStyle-CssClass="pager" AllowPaging="True"
                                        OnPageIndexChanging="gvWorkoutDetails_PageIndexChanging" PageSize="20">
                                        <Columns>

                                            <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Add" HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" OnClick="btnEdit_Click" CommandArgument='<%#Eval("WorkoutName_AutoID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField> --%>

                                            <asp:TemplateField ControlStyle-Width="20px" HeaderText="Add" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("WorkoutName_AutoID")%>'
                                                        Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" TabIndex="14" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Workout ID" DataField="WorkoutName_AutoID" HeaderStyle-HorizontalAlign="left"
                                                HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                            <asp:BoundField HeaderText="Muscular Group Name" DataField="MGroup" HeaderStyle-HorizontalAlign="left" />
                                            <asp:BoundField HeaderText="Workout Name" DataField="Workout" HeaderStyle-HorizontalAlign="left" />

                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="White" />
                                    </asp:GridView>


                                </div>

                            </div>

                            <%--End Course Details--%>

                            <%--Assign Package--%>

                            <div class="form-header">
                                <h4>&#10148; Assign Workout  </h4>
                            </div>
                            <div class="form-panel">
                                <div style="width: 1000px; height: auto; overflow-x: scroll;">

                                    <asp:GridView ID="GvWorkoutAssign" runat="server" AutoGenerateColumns="false" DataKeyNames="WorkoutName_AutoID" OnRowDeleting="GvWorkoutAssign_RowDeleting"
                                        Width="970px" CssClass="GridView" Font-Size="13px" PagerStyle-CssClass="pager" GridLines="None">

                                        <Columns>

                                            <%--    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Remove" HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>--%>
                                            <%--<asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="-" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command" />--%>
                                            <%--<asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="-" OnClick="btnDelete_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                            <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="left"
                                                ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" />
                                            <asp:BoundField HeaderText="Workout ID" DataField="WorkoutName_AutoID" HeaderStyle-HorizontalAlign="left"
                                                HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                            <asp:BoundField HeaderText="Muscular Group Name" DataField="MGroup" HeaderStyle-HorizontalAlign="left" />
                                            <asp:BoundField HeaderText="Workout Name" DataField="Workout" HeaderStyle-HorizontalAlign="left" />
                                            <asp:TemplateField HeaderText="Sets" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <%--<asp:UpdatePanel runat="server" ID="setsId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>--%>
                                                    <asp:TextBox ID="txtSets" runat="server" Text='<%#Eval("Sets")%>' AutoPostBack="true" OnTextChanged="txtSets_TextChanged" TabIndex="15" />
                                                    <%--  </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reps" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <%--<asp:UpdatePanel runat="server" ID="respId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>--%>
                                                    <asp:TextBox ID="txtResp" runat="server" Text='<%#Eval("Resp")%>' AutoPostBack="true" OnTextChanged="txtResp_TextChanged" TabIndex="15" />
                                                    <%--  </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                        <RowStyle Height="20px" />
                                        <AlternatingRowStyle Height="20px" BackColor="White" />
                                    </asp:GridView>

                                </div>


                                <center>
                          <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn"  TabIndex="16" ValidationGroup="aw" OnClick="btnSave_Click"
                                OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" />
                          <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn"  TabIndex="17" OnClick="btnClear_Click" />    
                      </center>
                            </div>

                            <%--End Assign Package--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>
        </div>

        <div id="divsearch" runat="server" visible="false">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="form-header">
                        <h4 style="float: left;">&#10148; Search Category</h4>
                    </div>

                    <table>
                        <tr>
                            <th>From Date</th>
                            <th>To Date</th>
                            <th></th>
                            <th>Category</th>
                            <th>Search by</th>
                            <th></th>
                            <%--<th></th>--%>
                            <th></th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="18" Width="110px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="19" Width="110px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" OnClick="btnSearch_Click" TabIndex="20" Text="Search" />
                            </td>

                            <td>
                                <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="ddl" TabIndex="21" Width="150px">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="MemberID">Member ID</asp:ListItem>
                                    <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                    <asp:ListItem Value="MemberName">Member Name</asp:ListItem>
                                    <asp:ListItem Value="Daywise">Daywise</asp:ListItem>
                                    <asp:ListItem Value="MuscularGroupType">Muscular Group Type</asp:ListItem>
                                    <asp:ListItem Value="WorkoutType">Workout Type</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchDetails" runat="server" CssClass="txt" TabIndex="22" OnTextChanged="txtSearchDetails_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td>
                                <asp:Button ID="btnSearchDatewithCategory" runat="server" CssClass="form-btn" TabIndex="23" Text="Date with category" OnClick="btnSearchDatewithCategory_Click" />
                                <%--</td>
                            <td>--%>
                                <asp:Button ID="btnExpord" runat="server" CssClass="form-btn" TabIndex="24" Text="Export To Excel" OnClick="btnExpord_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <center>
                                <asp:Button ID="btnClearDetails" runat="server" CssClass="form-btn" TabIndex="24" Text="Clear" OnClick="btnClearDetails_Click" />
                                </center>
                            </td>

                        </tr>
                    </table>

                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 10px;">
                         <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div>   
                        <asp:GridView ID="gvWorkoutRecord" runat="server" AutoGenerateColumns="false" Width="1250px" DataKeyNames="Workout_AutoID"
                            CssClass="GridView" PagerStyle-CssClass="pager" AllowPaging="True" GridLines="None" CellPadding="3" Font-Size="11px"
                            OnPageIndexChanging="gvWorkoutRecord_PageIndexChanging" PageSize="20">

                            <Columns>
                                <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("Workout_AutoID")%>' TabIndex="21" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="25"
                                            CommandArgument='<%#Eval("Workout_AutoID")%>' OnCommand="btnDelete_Command"
                                            Style="background-image: url('../NotificationIcons/f-cross_256-128.png'); background-size: 100% 100%; padding-left: 13px; padding-top: 0px; padding-bottom: 3px;" ToolTip="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Assign Date" DataField="AssignDate" DataFormatString="{0: dd-MM-yyyy}" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />                                
                                <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Muscular Group Name" DataField="MGroup" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Workout Name" DataField="Workout" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Sets" DataField="Sets" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Reps" DataField="Resp" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="WorkDay" DataField="WorkDay" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Programmer Name" DataField="ProgrammerName" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="From Date" DataField="FromDate" DataFormatString="{0: dd-MM-yyyy}" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="To Date" DataField="ToDate" DataFormatString="{0: dd-MM-yyyy}" HeaderStyle-HorizontalAlign="left" />                  
                            </Columns>

                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />

                        </asp:GridView>

                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExpord" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

    </div>
</asp:Content>
