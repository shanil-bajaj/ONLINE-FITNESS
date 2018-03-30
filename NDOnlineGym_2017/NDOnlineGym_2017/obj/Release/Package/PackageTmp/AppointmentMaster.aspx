<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AppointmentMaster.aspx.cs" Inherits="NDOnlineGym_2017.AppointmentMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
     <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://cloud.tinymce.com/stable/tinymce.min.js"></script>
    <style>
        table td
        {
            padding-bottom: 10px;
        }

        input:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox
        {
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

        .errorborder
        {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="sc">
            <div class="form-name-header">
                <h3>Appointment Master
                 <div class="navigation">
                     <ul>
                         <li>Member Settings &nbsp; > &nbsp;</li>
                         <li>Appointment  &nbsp; > &nbsp;</li>
                         <li>Appointment Master</li>
                     </ul>
                 </div>
               </h3>
            </div>
          

      <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Appointment Master </h4>
                </div>
                <div class="form-panel">
                    <table style="height: 80px;">

                         <tr>
                            <th><span class="error">*</span>ID</th>
                            <th><span class="error">*</span>Appointment Type</th>
                            <th><span class="error">*</span>Session</th>
                            <th>Time</th>
                            
                            <th><span class="error">*</span>Amount</th>                         
                            <th><span class="error">*</span>Status</th>
                            <th><span class="error">*</span>Color</th>
                        </tr>

                          <tr>
                            <td>
                                <asp:TextBox ID="txtAppID" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="1"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtPackageName"
                            ErrorMessage="Enter Name"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                 
                            </td>
                            <td>
                                <asp:TextBox ID="txtAppointmentType" runat="server" Style="width: 200px; padding: 3px 5px;" TabIndex="2"></asp:TextBox>
                                <%--  <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDuration"
                            ErrorMessage="Enter Duration"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator> --%>                             
                            </td>
                              <td>
                                <asp:TextBox ID="txtSession" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="3"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtSession"
                            ErrorMessage="Enter Session"  SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>    --%>                         
                            </td>
                            <td>
                                <asp:TextBox ID="txtTimeFrom" runat="server" Style="width: 120px; padding: 3px 5px;" TabIndex="4"
                                    TextMode="Time"></asp:TextBox>


                            </td>
                               <td>
                                <asp:TextBox ID="txtAmount" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="5"
                                    ></asp:TextBox>


                            </td>
                               <td>
                                <asp:TextBox ID="txtStatus" runat="server" Style="width: 100px; padding: 3px 5px;" TabIndex="6"
                                    ></asp:TextBox>


                            </td>
                               <td>
                                <asp:TextBox ID="txtColor"  runat="server" TextMode="Color" Style="width: 100px; padding: 3px 5px;" TabIndex="7"
                                   ></asp:TextBox>


                            </td>

                            <td>


                        </table>
                     <center class="btn-section">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" OnClick="btnSave_Click" ValidationGroup="a" TabIndex="8" OnClientClick="this.disabled = true;" UseSubmitBehavior="false"/>
                 <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnClear_Click" ValidationGroup="a" TabIndex="9" />
             </center>
                    </div>
      </div>

               <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Search By </h4>
                </div>
                <div class="form-panel">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="25">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="Package">Package</asp:ListItem>
                                    <asp:ListItem Value="Duration">Duration</asp:ListItem>
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="26"></asp:TextBox>
                            </td>
                            <td style="width: 45%;">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="form-btn" Text="Search" />
                            </td>
                        </tr>
                    </table>
                </div>

                     <div style="width: 1000px; height: auto; overflow-x: auto; margin-top: 50px;">

                    <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="AppoinmentMaster_AutoID" EmptyDataText="No record found." Width="1000px"
                        AllowPaging="True" TabIndex="27"  PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" >
                        <Columns>

                            <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit"  CommandArgument='<%#Eval("AppoinmentMaster_AutoID") %>' OnCommand="btnEdit_Command" TabIndex="5" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5" CommandArgument='<%#Eval("AppoinmentMaster_AutoID") %>' OnCommand="btnDelete_Command" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Appoinment ID" DataField="AppoinmentMaster_ID" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Appointment Type" DataField="Appoint_Type" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Amount" DataField="Ammount" HeaderStyle-HorizontalAlign="left" />
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
</div>
      </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
