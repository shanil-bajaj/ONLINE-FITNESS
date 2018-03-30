<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ViewMemberEndFollowup.aspx.cs" Inherits="NDOnlineGym_2017.ViewMemberEndFollowup" %>
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
                <h3>End Date List

           <div class="navigation">
               <ul>
                   <li>Course &nbsp; > &nbsp;</li>
                   <li>End Date Listt</li>
               </ul>
           </div>
                </h3>
            </div>

            <div class="divForm">
                <div class="form-panel">
                    <table  style="margin-left:150px;" >

                        <tr>
                            <td>
                                <table style="margin-top: 25px">
                                    <tr>
                                                <th>From Date</th>
                                                <th>To Date</th>
                                                <th>Date Category</th>
                                                <th>Package</th>
                                                <th></th>
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
                                                <asp:ListItem Value="End Date">End Date</asp:ListItem>
                                                <asp:ListItem Value="Next Followup Date">Next Followup Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                       
                                        <td>
                                            <asp:DropDownList ID="ddlPackage" runat="server" CssClass="ddl" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        
                                
                           
                    </table>
                    <center class="btn-section">
                                     
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearch_Click" UseSubmitBehavior="false" TabIndex="5" />
                                    <asp:Button ID="btnSearchByDateCategory" runat="server" Text="Date With Category" OnClick="btnSearchByDateCategory_Click" CssClass="form-btn" ValidationGroup="Category" UseSubmitBehavior="false" TabIndex="6" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="7" OnClick="btnClear_Click" />
                                    <%--<asp:Button ID="btnExistingFollowup" runat="server" Text="Existing Followup" CssClass="form-btn" TabIndex="8" OnClick="btnExistingFollowup_Click"/>--%>
                                 </center>

                </div>
            </div>

            <div class="divForm" style="margin-top: 5px">
                <div class="form-panel">
                    <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                         <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div>    

                        <asp:GridView ID="gvMemEndDetails" runat="server" AutoGenerateColumns="false" Width="1000px"
                            DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                            AllowPaging="True" PageSize="20" OnPageIndexChanging="gvMemEndDetails_PageIndexChanging">
                            <Columns>
                                
                                <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="32"
                                            CommandArgument='<%#Eval("Member_AutoID")%>' Text="Delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField HeaderText="Followup" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnFollowup" runat="server" CausesValidation="false" Text="Followup" OnCommand="btnFollowup_Command" CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="9" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Receipt ID" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblReceiptID" runat="server" Text='<%#Eval("ReceiptID") %>' />
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
                                <%--<asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>                                       
                                        <asp:LinkButton ID="btnName" runat="server" CausesValidation="false"                                           
                                            CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnName_Command" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGender" runat="server" Text='<%#Eval("Gender") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Package" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPackage" runat="server" Text='<%#Eval("Package") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblPackage" runat="server" Text='<%#Eval("Package") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              

                                <asp:TemplateField HeaderText="Start Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDate","{0:dd-MM-yyyy}")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("EndDate","{0:dd-MM-yyyy}")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Total" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTotal" runat="server" Text='<%#Eval("Total") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PaidFee" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPaidFee" runat="server" Text='<%#Eval("PaidFee") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblPaidFee" runat="server" Text='<%#Eval("PaidFee") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("Balance") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pay Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPayDate" runat="server" Text='<%#Eval("PayDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblPayDate" runat="server" Text='<%#Eval("PayDate","{0:dd-MM-yyyy}")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Next Pay Date" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtNextBalDate" runat="server" Text='<%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblNextBalDate" runat="server" Text='<%#Eval("NextBalDate","{0:dd-MM-yyyy}")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ControlStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtStatus" runat="server" Text='<%#Eval("Status") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>

                        <asp:GridView ID="gvMemberEndFoll" runat="server" AutoGenerateColumns="false" Width="1000px"
                            DataKeyNames="Member_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                            AllowPaging="True" PageSize="20" OnPageIndexChanging="gvMemberEndFoll_PageIndexChanging">
                            <Columns>
                                
                                <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="32"
                                            CommandArgument='<%#Eval("Member_AutoID")%>' Text="Delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Followup" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnMemEndFollowup" runat="server" CausesValidation="false" Text="Followup" OnCommand="btnMemEndFollowup_Command"
                                            CommandArgument='<%#Eval("Member_AutoID")%>' TabIndex="32" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                


                                <asp:TemplateField HeaderText="Followup Date" ControlStyle-Width="130px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFollowupDate" runat="server" Text='<%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblFollowupDate" runat="server" Text='<%#Eval("FollowupDate","{0:dd-MM-yyyy}")%>' />
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
                                        <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                                         <%--<asp:LinkButton ID="btnMemEndName" runat="server" CausesValidation="false"                                            
                                           CommandArgument='<%#Eval("Member_AutoID")%>' Text='<%#Eval("Name")%>' OnCommand="btnMemEndName_Command" />--%>
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
                                <asp:TemplateField HeaderText="Call Response" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtCallResoponse" runat="server" Text='<%#Eval("CallResponse")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblCallResoponse" runat="server" Text='<%#Eval("CallResponse")%>' />
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
                                <asp:TemplateField HeaderText="Comment" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtComment" runat="server" Text='<%#Eval("Comment")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comment")%>' />
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
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
