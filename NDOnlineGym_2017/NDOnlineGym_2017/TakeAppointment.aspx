<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="TakeAppointment.aspx.cs" Inherits="NDOnlineGym_2017.TakeAppointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
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

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }
              .remove
            {
                font-size: 18px;
                font-weight: bold;
            }
               .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <div class="sc">
     <div class="form-name-header">
                <h3>Take Appointment 
                 <div class="navigation">
                     <ul>
                         <li>Member Settings &nbsp; > &nbsp;</li>
                         <li>Appointment  &nbsp; > &nbsp;</li>
                         <li>Take Appointment</li>
                     </ul>
                 </div>
                </h3>
            </div>

             <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Take Appointment</h4>
                </div>
                <div class="form-panel">
                    <table style="height: 80px;">

                         <tr>
                            <th><span class="error">*</span>ID</th>
                            <th><span class="error">*</span>Member Name</th>
                            <th><span class="error">*</span>Contact</th>                                                   
                            <th><span class="error">*</span>Pre-Session</th>
                            <th><span class="error">*</span>Programmer Name</th>
                            <th><span class="error">*</span>Executive</th>
                        </tr>

                        <tr>
                            <td>
                                <asp:TextBox ID="txtID" runat="server" Style="width: 100px; padding: 3px 5px;" AutoPostBack="true" OnTextChanged="txtID_TextChanged" TabIndex="1"></asp:TextBox>
                                            
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="2"></asp:TextBox>
                             
                            </td>
                              <td>
                                <asp:TextBox ID="txtContact" runat="server" Style="width: 100px; padding: 3px 5px;" AutoPostBack="true" OnTextChanged="txtContact_TextChanged" TabIndex="3"></asp:TextBox>
                           
                            </td>
                            <td>
                                <asp:TextBox ID="txtPreSession" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="4"
                                    ></asp:TextBox>
                            </td>
                             <td>
                                 <asp:DropDownList ID="ddlProgrammerName" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="5">
                                     <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                 </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkExecutive" runat="server" Checked="true" OnCheckedChanged="chkExecutive_CheckedChanged" AutoPostBack="true"/>
                                           <asp:DropDownList ID="ddlExecutive" runat="server"  TabIndex="18" Enabled="false" Style="width: 200px; padding: 3px 5px;">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                            </td>
                            </tr>

                        </table>
                    </div>
                  <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Appointment Master </h4>
                </div>

                          <div style="width: 1000px; height: auto; overflow-x: auto; margin-top: 50px;">

                    <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="AppoinmentMaster_AutoID" EmptyDataText="No record found." Width="1000px"
                        PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" TabIndex="6">
                        <Columns>

                            <asp:TemplateField ControlStyle-Width="5px" HeaderText="ADD Appoint">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="+" OnCommand="btnEdit_Command" TabIndex ="7"
                                        Style="font-size: 18px; font-weight: bold; color: rgb(242, 137, 9);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField HeaderText="Appoinment ID" DataField="AppoinmentMaster_ID" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Appointment Type" DataField="Appoint_Type" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Ammount" DataField="Ammount" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Time" DataField="Time" HeaderStyle-HorizontalAlign="left" />
                            <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcolorr" runat="server" Width="100px" ></asp:TextBox>
                                    </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>

                </div> 
                      </div>


                  <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Appointment Details </h4>
                </div>

                          <div style="width: 1000px; height: auto; overflow-x: auto; margin-top: 50px;">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                        EmptyDataText="No record found." Width="1000px" Visible="false" OnRowDeleting="GridView1_RowDeleting" 
                        PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" TabIndex="8">
                        <Columns>
                            <asp:CommandField HeaderText="Cut" ShowDeleteButton="true" ButtonType="Link" DeleteText="-" HeaderStyle-HorizontalAlign="Left"
                                 ItemStyle-CssClass="remove" ItemStyle-ForeColor="#cc3300" />
                            <asp:BoundField HeaderText="ID" DataField="ID" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Appointment Type" DataField="Appoint_Type" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Ammount" DataField="Ammount" HeaderStyle-HorizontalAlign="left" />
   <%--                         <asp:BoundField HeaderText="Appoint Date" DataField="AppointDate" HeaderStyle-HorizontalAlign="left" />--%>

                                <asp:TemplateField HeaderText="Appoint Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsDate" runat="server" Text='<%#Eval("AppointDate")%>' OnTextChanged="txtsDate_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtsDate_CalendarExtender" runat="server"
                                            BehaviorID="txtsDate_CalendarExtender" TargetControlID="txtsDate" Format="dd-MM-yyyy" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            <asp:BoundField HeaderText="Time" DataField="Time" HeaderStyle-HorizontalAlign="left" />
                             <asp:BoundField HeaderText="Programmer Name" DataField="ProgrammerName" HeaderStyle-HorizontalAlign="left" />
                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>

                </div> 

                         <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="6" OnClick="btnSave_Click" />
                 <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" ValidationGroup="a" TabIndex="7" />
             </center>

                      </div>
                 </div>
            </ContentTemplate>
                        <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>

        </asp:UpdatePanel>

</asp:Content>
