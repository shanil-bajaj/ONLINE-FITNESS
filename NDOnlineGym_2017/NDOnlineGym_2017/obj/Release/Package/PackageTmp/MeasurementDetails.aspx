<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MeasurementDetails.aspx.cs" Inherits="NDOnlineGym_2017.MeasurementDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <%-- <link rel="icon" type="image/png" href="Logo/NDLogo.png" />--%>
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script type="text/javascript">
        function capFirst(oTextBox) {
            oTextBox.value = oTextBox.value[0].toUpperCase() + oTextBox.value.substring(1);
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

        .remove {
            font-size: 18px;
            font-weight: bold;
        }

        .GridView1 {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
             width: max-content;
        }

            .GridView1 a /** FOR THE PAGING ICONS  **/ {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

                .GridView1 a:focus {
                    color: orangered;
                }

                .GridView1 a:hover {
                    color: orangered;
                }

            .GridView1 span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .hideGridColumn {
            display: none;
        }

        .GridView a:focus {
            color: orangered;
        }

        .GridView a:hover {
            color: orangered;
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
    <div class="sc">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="form-name-header">
                    <h3>Measurement Details
                 <div class="navigation">
                     <ul>
                         <li>Status &nbsp; > &nbsp;</li>
                         <li>Body Assessment  &nbsp; > &nbsp;</li>
                         <li>Measurement Details</li>
                     </ul>
                 </div>
                    </h3>
                </div>

                <%--<div class="divForm">--%>
                <div id="divsearch" runat="server">
                    <div class="form-header">
                        <h4 style="float: left;">&#10148; Search Category               
                        </h4>
                    </div>
                    <div class="form-panel">
                        <table>
                            <tr>
                                <th>From Date</th>
                                <th>To Date</th>
                                <th>Category</th>
                                <th>Search by</th>
                                <th></th>
                                <th></th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1" Style="width: 200px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2" Style="width: 200px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDlSearch" runat="server" CssClass="ddl" OnSelectedIndexChanged="DDlSearch_SelectedIndexChanged" AutoPostBack="true" TabIndex="3" Style="width: 200px">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Member Id">Member Id</asp:ListItem>
                                        <asp:ListItem Value="Name">Name</asp:ListItem>
                                        <asp:ListItem Value="Contact">Contact</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="4" Style="width: 200px" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table style="margin-top: 15px">
                            <tr>

                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="form-btn" OnClick="btnSearch_Click" TabIndex="5" Text="Search" Style="width: 200px" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnSearchwtCategory" runat="server" CssClass="form-btn" OnClick="BtnSearchwtCategory_Click" TabIndex="6" Text="Date with category" Style="width: 200px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnclear" runat="server" CssClass="form-btn" TabIndex="7" Text="Clear" OnClick="btnclear_Click" Style="width: 200px" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnExportExcel" runat="server" CssClass="form-btn" OnClick="BtnExportExcel_Click" TabIndex="8" Text="Export to Excel" Style="width: 200px" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 5px;">
                        <div style="margin: 10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records   " Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                        </div>
                        <asp:GridView ID="gvMeasurement" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                             AllowPaging="True" PageSize="20" TabIndex="110" PagerStyle-CssClass="pager" CssClass="GridView1" GridLines="None" CellPadding="5"
                            OnPageIndexChanging="gvMeasurement_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command" CommandArgument='<%#Eval("Measurement_AutoId")%>' TabIndex="5" 
                                          style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnCommand="btnDelete_Command1" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("Measurement_AutoId")%>' TabIndex="5" 
                                            style="background-image:url('../NotificationIcons/f-cross_256-128.png'); background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="100px" />
                                <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <HeaderTemplate>
                                        <b>Date</b>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Date","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Arms" DataField="Arms" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Weight" DataField="Weight" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Height" DataField="Height" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Waist" DataField="Waist" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Thigh" DataField="Thigh" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Fat" DataField="Fat" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Bmass" DataField="Bmass" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Mmass" DataField="Mmass" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="BMI" DataField="BMI" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="DCI" DataField="DCI" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Age" DataField="Age" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Water%" DataField="Water" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Vfat" DataField="Vfat" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Neck" DataField="Neck" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="UpperArms" DataField="UpperArms" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="ForArms" DataField="ForArms" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Shoulder" DataField="Shoulder" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Hips" DataField="Hips" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Chest" DataField="Chest" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="ChestExpanded" DataField="ChestExpanded" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="UpperAbdomen" DataField="UpperAbdomen" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="LowerAbdomen" DataField="LowerAbdomen" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Calf" DataField="Calf" HeaderStyle-HorizontalAlign="left" />
                                <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />
                                <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                    <HeaderTemplate>
                                        <b>Next Followup Date</b>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("NextFollowupDate","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                        </asp:GridView>
                        <asp:Label ID="lblMemberAutoID" runat="server"> </asp:Label>
                    </div>
                </div>
                <%--</div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
