<%@ Page Title="" Language="C#" MasterPageFile="~/PTMaster.Master" AutoEventWireup="true" CodeBehind="PTMasterForm.aspx.cs" Inherits="NDOnlineGym_2017.PTMasterForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
        }
    </script>
    <style>
        .form-menu {
            float: left;
            width: 250px;
        }

        .btn-menu {
            padding: 10px 10px;
            margin-bottom: 5px;
            border: none;
            width: 100%;
            background-color: rgba(0,0,0,0.1);
            border-radius: 3px;
            text-align: left;
        }

            .btn-menu:hover {
                box-shadow: 2px 2px 10px 0px rgba(0, 0, 0, 0.50);
            }

            .btn-menu:focus {
                box-shadow: 2px 2px 10px 0px rgba(0, 0, 0, 0.50);
            }

        .btn-menu-selected {
            background-color: orangered;
            color: white;
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
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="sc">
            <div class="form-name-header">
                <h3>Masters 
                <div class="navigation">
                    <ul>
                        <li>File &nbsp; > &nbsp;</li>
                        <li>Master &nbsp; > &nbsp;</li>

                    </ul>
                </div>

                    <h3></h3>

                </h3>
            </div>
            <div class="divForm" style="background-color: none">
                <table>
                    <tr>
                        <td>
                            <div style="width: 250px;margin-top:-290px">
                                <asp:Button ID="btnDays" runat="server" Text="Days" CssClass="btn-menu btn-menu-selected" CausesValidation="false"  TabIndex="1" OnClick="btnDays_Click" />
                                <asp:Button ID="btnTime" runat="server" Text="Time" CssClass="btn-menu" TabIndex="7" OnClick="btnTime_Click" />
                                <asp:Button ID="btnIncentive" runat="server" Text="Incentive" CssClass="btn-menu"  TabIndex="11" OnClick="btnIncentive_Click" />
                               
                            </div>
                        </td>
                        <td>
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="View1" runat="server">
                                    <center><table style=" ">
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Days </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtDays" runat="server" CssClass="txt" TabIndex="2" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDays" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtDays"
                                                            ErrorMessage="Enter Days" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSaveDays" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="a" TabIndex="3" OnClick="btnSaveDays_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearDays" runat="server" Text="Clear" CssClass="form-btn" TabIndex="4" OnClick="btnClearDays_Click"/>
                           </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvDays" runat="server" AutoGenerateColumns="false" Width="480px"
                                            Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                            AllowPaging="True"  PageSize="20">

                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditDays" runat="server" CausesValidation="false" OnCommand="btnEditDays_Command" CommandArgument='<%#Eval("Days_AutoID")%>' TabIndex="5"
                                                       style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
   
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteDays" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                                            CommandArgument='<%#Eval("Days_AutoID")%>'  OnCommand="btnDeleteDays_Command"
                                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                                           
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Days" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtDays" runat="server" Text='<%#Eval("Days") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblDays" runat="server" Text='<%#Eval("Days") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <center>
                                <table style="">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Time </span></td>
                                        <td style="width: 55%; text-align: left;">
                                            <asp:TextBox ID="txtTime" runat="server" CssClass="txt" TabIndex="8" onChange="javascript:capFirst(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtTime" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtTime"
                                                ErrorMessage="Enter Time" SetFocusOnError="true" ValidationGroup="dept"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                                <asp:Button ID="btnSaveTime" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="dept" TabIndex="8" UseSubmitBehavior="false" OnClick="btnSaveTime_Click"/>
                                <asp:Button ID="btnClearTime" runat="server" Text="Clear" CssClass="form-btn" TabIndex="9" OnClick="btnClearTime_Click"/>
                              </center>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvTime" runat="server" AutoGenerateColumns="false" Font-Size="13px" PagerStyle-CssClass="pager" Width="480px"
                                            CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True"  PageSize="20" >

                                            <Columns>
                                               <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditTime" runat="server" CausesValidation="false" OnCommand="btnEditTime_Command" CommandArgument='<%#Eval("Time_AutoID")%>' TabIndex="5"
                                                       style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
   
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteTime" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                                            CommandArgument='<%#Eval("Time_AutoID")%>'  OnCommand="btnDeleteTime_Command"
                                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                                           
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Time" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTime" runat="server" Text='<%#Eval("Time") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Time") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <center>
                              <table style="width:400px;">
                                <tr>
                                    <td><span>Trainer Name</span></td>
                                    <td><asp:DropDownList ID="ddlTname" runat="server" CssClass="ddl">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr>
                                    <td><span>Incentive</span></td>
                                    <td> 
                                        <asp:TextBox ID="txtIncentive" runat="server" CssClass="txt" TabIndex="2" ></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                           <div style="margin-top:20px;"> 
                                <asp:Button ID="btnSaveIncentive" runat="server" Text="Save" CssClass="form-btn" ValidationGroup="EnqType" TabIndex="13" OnClick="btnSaveIncentive_Click" UseSubmitBehavior="false" />
                                <asp:Button ID="btnClearIncentive" runat="server" Text="Clear" CssClass="form-btn" TabIndex="14" OnClick="btnClearIncentive_Click" />
                            </div>
                            </center>

                                    <%--GridView--%>
                                   <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="gvIncentive" runat="server" AutoGenerateColumns="false"  Font-Size="13px" Width="480px"
                                            PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" PageSize="20">

                                            <Columns>
                                               <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEditIncentive" runat="server" CausesValidation="false" OnCommand="btnEditIncentive_Command" CommandArgument='<%#Eval("Trainer_AutoID")%>' TabIndex="5"
                                                       style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit"  />
   
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteIncentive" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                                            CommandArgument='<%#Eval("Trainer_AutoID")%>'  OnCommand="btnDeleteIncentive_Command"
                                                        style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" ></asp:LinkButton>
                                                           
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Incentive" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIncentive" runat="server" Text='<%#Eval("Incentive") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblIncentive" runat="server" Text='<%#Eval("Incentive") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Name" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtName" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:View>
                                
                            </asp:MultiView>
                        </td>
                    </tr>
                </table>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
