<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Followup.aspx.cs" Inherits="NDOnlineGym_2017.Followup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sc">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="form-name-header">
                <h3>Add Followup
                 <div class="navigation">
                     <ul>
                         <li>User Settings &nbsp; > &nbsp;</li>
                         <li>Packages  &nbsp; > &nbsp;</li>
                         <li>Create Packages</li>
                     </ul>
                 </div>

                </h3>
            </div>

            <div class="divForm">

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
                            <th>Email Id</th>
                        </tr>
                        <tr id="row1" runat="server">
                            <td>
                                <asp:TextBox ID="txtMemberID" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="1" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged"
                                    onkeypress="return RestrictSpaceSpecial(event);" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContact" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="2" AutoPostBack="true" OnTextChanged="txtContact_TextChanged"
                                    onkeypress="return RestrictSpaceSpecial(event);" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFirst" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="3" Enabled="false"></asp:TextBox>
                            </td>                            
                            <td>
                                <asp:TextBox ID="txtLast" runat="server" Style="width: 150px; padding: 3px 5px;" TabIndex="4" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100px; padding: 3px 5px;" CssClass="ddl" TabIndex="5" Enabled="false">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtmail" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="6" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="form-header">
                    <h4>&#10148;Add Followup </h4>
                </div>
                <div class="form-panel">
                    <table style="height: 80px;">
                        <tr>
                            <th><span class="error">*</span>Followup Type</th>
                            <th><span class="error">*</span>Executive</th>
                            <th><span class="error">*</span>Call Response</th>
                            <th><span class="error">*</span>Rating</th>
                            <th><span class="error">*</span>Next Followup Date</th>
                            <th><span class="error">*</span>Next Followup Time</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlFollowupType" runat="server" Style="width: 160px; padding: 3px 5px;" CssClass="ddl" TabIndex="7">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <%-- <td>
                                <asp:DropDownList ID="ddlExecutive" runat="server" Style="width: 180px; padding: 3px 5px;" CssClass="ddl" TabIndex="8">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>

                            </td>--%>
                            <td>
                                <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true"/>
                                                   <asp:DropDownList ID="ddlExecutive" runat="server"  TabIndex="18" Enabled="false" Style="width: 150px; padding:3px" >
                                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                    </asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCallPesponse" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="9">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRating" runat="server" Style="width: 150px; padding: 3px 5px;" CssClass="ddl" TabIndex="10" OnSelectedIndexChanged="ddlRating_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Hot">Hot</asp:ListItem>
                                    <asp:ListItem Value="Cold">Cold</asp:ListItem>
                                    <asp:ListItem Value="Warm">Warm</asp:ListItem>
                                    <asp:ListItem Value="Expected">Expected</asp:ListItem>
                                    <asp:ListItem Value="Not Interested">Not Interested</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:TextBox ID="txtNextFollowupDate" runat="server" Style="width: 160px; padding: 3px 5px;" TabIndex="11" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtNextFollowupDate_CalendarExtender1" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtNextFollowupDate" Format="dd-MM-yyyy" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNextFollowupTime" runat="server" Style="width: 150px; height: 25px; padding: 3px 5px;" TabIndex="12" TextMode="Time"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                    <table>
                        <tr>
                            <td style="padding-top: 10px;">
                                <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">Followup Date :</span>
                                <asp:Label ID="lblFollowupDateTime" runat="server" Text="" Style="font-size: 13px"></asp:Label>

                                <%--</td>
                            <td style="padding-top: 10px; padding-left: 20px;">
                                <%--<span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">Status :</span>
                                <asp:Label ID="Label1" runat="server" Text="" Style="color: green; font-size: 13px"></asp:Label>
                            </td>--%>
                                <%--<td style="padding-top: 10px; padding-left: 20px;">
                                <span style="padding: 10px; font-weight: bold; font-size: 13px; margin-left: 5px; margin-right: 5px;">User :</span>
                                <asp:Label ID="Label2" runat="server" Text="Anita" Style="color: blue; font-size: 13px"></asp:Label>
                            </td>--%>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="padding: 10px;">
                                <span class="error">*</span><span style="font-weight: bold; font-size: 12px; margin-left: 5px; margin-right: 45px;">Comment</span>

                            </td>
                            <td>
                                <asp:TextBox ID="txtComment" runat="server" CssClass="text" TabIndex="14" TextMode="MultiLine" Width="400px" Rows="4" style="resize:none" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" OnClick="btnSave_Click" TabIndex="15" 
                    OnClientClick="this.disabled = true;" UseSubmitBehavior="false" />
                 <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" ValidationGroup="a" TabIndex="16" OnClick="btnClear_Click"/>
             </center>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Search By </h4>
                </div>
                <div class="form-panel" id="divSearch" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="17" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="FollowupType">Followup Type</asp:ListItem>
                                    <asp:ListItem Value="MemberID">Member ID</asp:ListItem>
                                    <asp:ListItem Value="MemberName">Member Name</asp:ListItem>
                                    <asp:ListItem Value="ContactNumber">Contact Number</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="18" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td style="width: 45%;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" OnClick="btnSearch_Click" TabIndex="19"/>
                            </td>
                        </tr>
                    </table>
                </div>

                <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 10px;">

                   <%-- <asp:GridView ID="gvFollowupDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Followup_AutoID"  EmptyDataText="No record found."
                        Width="1000px" CssClass="GridView" PagerStyle-CssClass="pager" AllowPaging="True">--%>
                     <asp:GridView ID="gvFollowupDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Followup_AutoID" CssClass="GridView" PagerStyle-CssClass="pager"
                            AllowPaging="True" GridLines="None" OnPageIndexChanging="gvFollowupDetails_PageIndexChanging" PageSize="20" Width="1000px">

                        <Columns>

                            <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" CommandArgument='<%#Eval("Followup_AutoID")%>'
                                        OnCommand="btnEdit_Command" TabIndex="20" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="20"
                                        CommandArgument='<%#Eval("Followup_AutoID")%>' OnCommand="btnDelete_Command"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Followup Type" DataField="FollowupType" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Member Id" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />                                
                            <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                <HeaderTemplate>
                                    <b>Followup Date</b>
                                </HeaderTemplate>
                                <ItemTemplate><%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>    

                            <%--<asp:BoundField HeaderText="Followup Time" DataField="FollowupTime" HeaderStyle-HorizontalAlign="left" /> --%>      
                            <asp:BoundField HeaderText="Call Response" DataField="CallResponse" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Rating" DataField="Rating" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />
                          <asp:TemplateField HeaderStyle-HorizontalAlign="left">
                                <HeaderTemplate>
                                    <b>Nxt Followup Date</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>        
                                    
                            <asp:BoundField HeaderText="Nxt Followup Time" DataField="NextFollowupTime" HeaderStyle-HorizontalAlign="left" />

                        </Columns>
                        
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                        
                    </asp:GridView>

                </div>

            </div>
                
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>
    </asp:UpdatePanel>
        </div>

</asp:Content>
