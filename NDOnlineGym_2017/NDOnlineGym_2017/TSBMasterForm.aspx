<%@ Page Title="" Language="C#" MasterPageFile="~/TimeSlotBooking.Master" AutoEventWireup="true" CodeBehind="TSBMasterForm.aspx.cs" Inherits="NDOnlineGym_2017.TSBMasterForm" %>
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
        }s

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
                    <h3></h3>
                    <h3></h3>
                    <h3></h3>
                </h3>
            </div>
            <div class="divForm" style="background-color: none">
                <table>
                    <tr>
                        <td>
                            <div style="width: 250px;margin-top:-290px">
                                <asp:Button ID="btnDays" runat="server" Text="Court Type" CssClass="btn-menu btn-menu-selected" CausesValidation="false"  TabIndex="1" OnClick="btnDays_Click" />
                              <%--  <asp:Button ID="btnTime" runat="server" Text="Time" CssClass="btn-menu" TabIndex="7" OnClick="btnTime_Click" />
                                <asp:Button ID="btnIncentive" runat="server" Text="Incentive" CssClass="btn-menu"  TabIndex="11" OnClick="btnIncentive_Click" />--%>
                               
                            </div>
                        </td>
                        <td>
                         
                                    <%--<center><table style=" ">--%>
                                    <tr>
                                        <td class="cols">
                                            <table>
                                                <tr>
                                                    <td style="width: 45%;"><span class="lbl"><span class="error">*</span> Court Name </span></td>
                                                    <td style="width: 55%; text-align: left;">
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="txt" onkeypress="return RestrictSpaceSpecial(event);" TabIndex="2" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="txtName"
                                                            ErrorMessage="Enter Court Name" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="form-btn" ValidationGroup="a" TabIndex="3"  UseSubmitBehavior="false" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="4"  />
                          <%-- </center>--%>
                                    <%--GridView--%>
                                    <div style="width: 500px; height: 490px; overflow-x: auto; overflow-y: auto; margin-top: 20px; margin-left: 80px;">
                                        <asp:GridView ID="GvTSBMaster" runat="server" AutoGenerateColumns="false" Width="480px"
                                            DataKeyNames="CourtMaster_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                            AllowPaging="True"  PageSize="20">

                                            <Columns>
                                                <asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                                                    <ItemTemplate>
                                                      <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command"  CommandArgument='<%#Eval("CourtMaster_AutoID")%>' Text="Edit" TabIndex="5" />
                                                   </ItemTemplate>
                                               </asp:TemplateField>

                                              <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                                                  <ItemTemplate>
                                                      <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnCommand="btnDelete_Command" CommandArgument='<%#Eval("CourtMaster_AutoID")%>' Text="Delete"  TabIndex="5" />
                                                </ItemTemplate>
                                              </asp:TemplateField>

                                               <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />

                                            </Columns>
                                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                            <RowStyle Height="20px" />
                                            <AlternatingRowStyle Height="20px" BackColor="White" />
                                        </asp:GridView>
                                    </div>
                           
                         
                                
                          
                        </td>
                    </tr>
                </table>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
