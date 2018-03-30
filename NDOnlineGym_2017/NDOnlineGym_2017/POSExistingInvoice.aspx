<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSExistingInvoice.aspx.cs" Inherits="NDOnlineGym_2017.POSExistingInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
       input:focus { border: 1px solid rgb(242, 137, 9); }
       .GridView { margin-top: 10px;float: left;border: solid 1px silver;border-radius: 3px; }
       .GridView a /** FOR THE PAGING ICONS  **/ { background-color: Transparent;padding: 5px 5px 5px 5px; color: black;text-decoration: none;font-weight: bold;  }
       .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/  { color: #fff; padding: 5px 5px 5px 5px;  }
       .remove { font-size: 18px; font-weight: bold; }
       .GridView1  { margin-top: 10px;float: left; border: solid 1px silver;border-radius: 3px; }
       .GridView1 a /** FOR THE PAGING ICONS  **/ { background-color: Transparent;padding: 5px 5px 5px 5px; color: black; text-decoration: none; font-weight: bold; }
       .GridView1 a:focus { color: orangered; }
       .GridView1 a:hover  { color: orangered;  }
       .GridView1 span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {  /*color: #fff;*/ padding: 5px 5px 5px 5px;  }
       .hideGridColumn { display: none;  }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <div class="sc">
      <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Existing Invoice </h4>
                </div>
                <div class="form-panel">
                    <table style="width: 100%;">
                        <tr>
                            <th>Date(From)</th>
                            <th>Date(To)</th>
                            <th>Category</th>
                            <th>Search</th>
                        </tr>
                        <tr>
                            <td style="width: 20%;">
                                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Enabled="true" TabIndex="13"></asp:TextBox>
                                 <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                            </td>
                            <td style="width: 20%;">
                                 <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Enabled="true" TabIndex="13"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                            </td>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="ddl" TabIndex="12">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="Package">Package</asp:ListItem>
                                    <asp:ListItem Value="Duration">Duration</asp:ListItem>
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Deactive">Deactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" Enabled="true" TabIndex="13"></asp:TextBox>
                            </td>
                          
                        </tr>
                    </table>

                     <center class="btn-section">
                        <asp:Button ID="btnSerach" runat="server" Text="Serach" CssClass="form-btn" ValidationGroup="a"  TabIndex="10" /> 
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" ValidationGroup="a"  TabIndex="11" />  
                     </center>
                </div>

                <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">

                    <asp:GridView ID="gvExistingInvoiceDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found."
                        Width="1300px" AllowPaging="True" PageSize="20" TabIndex="107" PagerStyle-CssClass="pager" CssClass="GridView1" GridLines="None" CellPadding="5">
                        <Columns>

                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>

                </div>

            </div>
          </div>
</asp:Content>
