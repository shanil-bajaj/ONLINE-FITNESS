<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ViewMembershipFollowup.aspx.cs" Inherits="NDOnlineGym_2017.ViewMembershipFollowup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
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
             .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
            <div class="form-name-header">
                <h3>Membership Followup 
           <div class="navigation">
               <ul>
                   <li>Followup &nbsp; > &nbsp;</li>
                   <li>Membership Followup</li>
               </ul>
           </div>
                </h3>
            </div>

            <div class="divForm">
                <div class="form-panel">
                    <table style="width: 100%;">

                        <tr>
                            <td>
                                <table style="margin-top: 25px">
                                    <tr>
                                                <th>Form Date</th>
                                                <th>To Date</th>
                                                <th>Date Category</th>
                                                <th>Select Category</th>
                                                <th>Search</th>
                                                <th></th>
                                    </tr>
                                    <tr>
                                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                        
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server"
                                                BehaviorID="txtFromDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                        </td>
                                    
                                        <td>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                                BehaviorID="txtToDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                        </td>
                                        
                                        <td>
                                            <asp:DropDownList ID="ddlDateCategory" runat="server" CssClass="ddl" TabIndex="3" OnSelectedIndexChanged="ddlDateCategory_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Followup Date">Followup Date</asp:ListItem>
                                                <asp:ListItem Value="Next Followup Date">Next Followup Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                      
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ddl" TabIndex="4">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="Member ID">Member ID</asp:ListItem>
                                                <asp:ListItem Value="Name">Name</asp:ListItem>
                                                <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                                <asp:ListItem Value="Gender">Gender</asp:ListItem>
                                                <asp:ListItem Value="Followup Type">Followup Type</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                   
                                        <td >
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="5" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <center class="btn-section">
                                     
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearch_Click" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnSearchByDateCategory" runat="server" Text="Search By Date & Category" OnClick="btnSearchByDateCategory_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClear_Click" />
                                    <asp:Button ID="btnExistingFollowup" runat="server" Text="Existing Followup" CssClass="form-btn" TabIndex="8" OnClick="btnExistingFollowup_Click" />
<%--                                     <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="25"  />--%>
                                 </center>
                            </td>
                        </tr>
                    </table>


                </div>
            </div>

            <div class="divForm" style="margin-top: 5px">
                <div class="form-panel">
                    <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                        <asp:GridView ID="gvMemshipFoll" runat="server" AutoGenerateColumns="false" Width="1000px"
                            DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                            AllowPaging="True" PageSize="20" OnPageIndexChanging="gvMemshipFoll_PageIndexChanging">
                            <Columns>
                                
                                <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="32"
                                            CommandArgument='<%#Eval("Member_AutoID")%>' Text="Delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Followup" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnFollowup" runat="server" CausesValidation="false" Text="Followup" OnCommand="btnFollowup_Command" 
                                            CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="9" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("MemberID1") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnName" runat="server" CausesValidation="false"
                                            CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnName_Command" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact1" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Followup Date" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFollowupDate" runat="server" Text='<%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblFollowupDate" runat="server" Text='<%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              

                                <asp:TemplateField HeaderText="Followup Time" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFollowupTime" runat="server" Text='<%#Eval("FollowupTime")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblFollowupTime" runat="server" Text='<%#Eval("FollowupTime")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NextFollowupDate" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtNextFollowupDate" runat="server" Text='<%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblNextFollowupDate" runat="server" Text='<%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NextFollowupTime" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtNextFollowupTime" runat="server" Text='<%#Eval("NextFollowupTime")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblNextFollowupTime" runat="server" Text='<%#Eval("NextFollowupTime")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                               
                                <asp:TemplateField HeaderText="Rating" ControlStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRating" runat="server" Text='<%#Eval("Rating") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblRating" runat="server" Text='<%#Eval("Rating") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FollowupType" ControlStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFollowupType" runat="server" Text='<%#Eval("FollowupType") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblFollowupType" runat="server" Text='<%#Eval("FollowupType") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                        <asp:GridView ID="gvMemberDetails" runat="server" AutoGenerateColumns="false" Width="1000px"
                            DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                            AllowPaging="True" PageSize="20" >
                            <Columns>
                                
                                <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="32"
                                            CommandArgument='<%#Eval("Member_AutoID")%>' Text="Delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Followup" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnFollowup" runat="server" CausesValidation="false" Text="Followup" OnCommand="btnFollowup_Command" 
                                            CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="32" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reg Date" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRegDate" runat="server" Text='<%#Eval("RegDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblRegDate" runat="server" Text='<%#Eval("RegDate","{0:dd-MM-yyyy}")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Member ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtMemberID1" runat="server" Text='<%#Eval("Member_ID1") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblMemberID1" runat="server" Text='<%#Eval("MemberID1") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnName" runat="server" CausesValidation="false"
                                            CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnName_Command" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact1" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtEmail" runat="server" Text='<%#Eval("Email") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
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
    </asp:UpdatePanel>
</asp:Content>
