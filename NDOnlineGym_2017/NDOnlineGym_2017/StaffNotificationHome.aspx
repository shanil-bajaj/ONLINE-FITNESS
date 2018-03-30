<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="StaffNotificationHome.aspx.cs" Inherits="NDOnlineGym_2017.StaffNotificationHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>

    <style>
        .txt1 {
            width: 500px;resize:none;
        }
        .auto-style1 {
            width: 14%;
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
        input:focus { border:1px solid orangered }
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
         .ErrorBox  { position:relative; z-index:1; font-weight:normal; border-radius:3px; box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5);
                        padding: 4px 20px; color: #a94442; background-color: #f2dede; border: 1px solid #ebccd1; }
    </style>
    <script type="text/javascript">
        function LimtCharacters(txtNotification, CharLength, indicator) {
            chars = txtNotification.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (chars > CharLength) {
                txtNotification.value = txtNotification.value.substring(0, CharLength);
            }
        }
</script>
    <script type="text/javascript">
        function ShowCharCount() {
            document.getElementById("cntCharacter").innerHTML = document.getElementById("txtNotification").value.length;
        }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <div class="sc">
    <div class="form-name-header">
        <h3>Staff Notification
            <div class="navigation">
                <ul>
                    <li>File &nbsp; > &nbsp;</li>
                    <li>Staff Notification</li>
                </ul>
            </div>
        </h3>
    </div>
    <div class="divForm">
        <%--Quick SMS--%>

        <div class="form-header" id="formheader1">
            <h4>&#10148;Staff Notification</h4>            
        </div>

        <div class="form-panel" id="formpanel1">

            <table style="width: 100%;">              
                <tr>
                    <td class="cols">
                        <table>                                                        
                            <tr>
                                <td style="width: 35%;"><span class="lbl"><span class="error">*</span>Staff Name</span></td>
                                
                                <td style="width: 67%; text-align: left;">                                    
                                   
                                    <asp:DropDownList ID="ddlStaffName" runat="server" CssClass="ddl" TabIndex="1">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                   
                                    <asp:RequiredFieldValidator ID="rfvddlStaffName" runat="server" ErrorMessage="Select Staff" SetFocusOnError="true" 
                                        ControlToValidate="ddlStaffName" CssClass="ErrorBox" ValidationGroup="Notification" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                            
                        </table>
                    </td>
                   
                </tr>
            </table>

            <table style="width: 100%;">
                <tr>
                    <td style="width:15%; vertical-align:top;"><span class="lbl"><span class="error">*</span>Message</span></td>
                    <td style="width: 85%;">                                                
                        <%--<asp:TextBox ID="txtNotification" runat="server" CssClass="txt1" TabIndex="2" TextMode="MultiLine" onkeyup="LimtCharacters(this.1000,'cntCharacter');"></asp:TextBox>                          
                        <br />
                       <asp:RequiredFieldValidator ID="rfvtxtNotification" runat="server" ErrorMessage="Enter Message Here" SetFocusOnError="true"
                            ControlToValidate="txtNotification" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="Notification"></asp:RequiredFieldValidator>
                        <h4>Count :
                            <label id="cntCharacter" style="background-color: #E2EEF1; color: Red; font-weight: bold;">0</label>                            
                        </h4>--%>




                        <asp:TextBox ID="txtNotification" runat="server" TextMode="MultiLine" TabIndex="2" Width="450" Height="90px" 
                                     onkeyup="LimtCharacters(this,2000,'lblcount');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Number of Characters Left:
                                    <label id="lblcount" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label>  
                        <asp:RequiredFieldValidator ID="rfvtxtNotification" runat="server" ErrorMessage="Enter Message Here" SetFocusOnError="true"
                            ControlToValidate="txtNotification" Display="Dynamic" CssClass="ErrorBox" ValidationGroup="Notification"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1"></td>
                    <td style="width: 85.7%;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="Notification" TabIndex="3"  OnClick="btnSave_Click"/>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="4" OnClick="btnClear_Click"  /> 
                    </td>
                </tr>
            </table>
            <div class="form-header" id="divformheader" runat="server">
                    <h4 style="float: left;">&#10148; Search Category</h4>
                </div>
                <div class="form-panel" id="divformpanel" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%;">
                                </span>Category</span><br />
                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="5">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Staff Name">Staff Name</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%;">
                                </span>Search By</span><br />
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="True" TabIndex="6"></asp:TextBox>
                            </td>
                            <td style="width: 45%;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" Text="Search" OnClick="btnSearch_Click" TabIndex="7" />
                            </td>
                        </tr>
                    </table>
                    <div style="width: 1000px; height: auto; overflow-x: scroll;">
                        <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="Notification_AutoID" Width="1000px"
                            Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True"
                            OnPageIndexChanging="gvNotification_PageIndexChanging" PageSize="20">

                            <Columns>
                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEdit_Command"
                                            CommandArgument='<%#Eval("Notification_AutoID")%>' TabIndex="8" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="8" OnCommand="btnDelete_Command"
                                            CommandArgument='<%#Eval("Notification_AutoID")%>' Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Date" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDate" runat="server" Text='<%# Eval("Date","{0:dd-MM-yyyy}") %>'   />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date","{0:dd-MM-yyyy}") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notification" ControlStyle-Width="670px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtNotification" runat="server" Text='<%#Eval("Notification") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblNotification" runat="server" Text='<%#Eval("Notification") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Staf fName" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="txtStaffName" runat="server" Text='<%#Eval("StaffName") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblStaffName" runat="server" Text='<%#Eval("StaffName") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>
                    </div>
                    <%--End Software handling--%>
                </div>
        </div>
        <%--End Individual--%>
    </div>
                  </div>
            </ContentTemplate>
<%--         <Triggers>
             <asp:PostBackTrigger ControlID="btnQuickSMS" />
         </Triggers>--%>
         </asp:UpdatePanel>
</asp:Content>
