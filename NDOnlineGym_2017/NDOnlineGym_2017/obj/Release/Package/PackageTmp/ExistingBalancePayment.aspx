<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ExistingBalancePayment.aspx.cs" Inherits="NDOnlineGym_2017.ExistingBalancePayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
         .GridView { margin-top: 10px; float: left; border: solid 1px silver;  border-radius: 3px;  }
         .GridView a /** FOR THE PAGING ICONS  **/ { background-color: Transparent; padding: 5px 5px 5px 5px; color: black; text-decoration: none; font-weight: bold; }
         .GridView a:focus { color:orangered;}
         .GridView a:hover { color:orangered;}
         .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {  /*color: #fff;*/ padding: 5px 5px 5px 5px;   }
          .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sc">
     <div class="form-name-header">
       <h3>Balance Payment Details 
           <div class="navigation">
              <ul>
                  <li>Member Setting &nbsp; > &nbsp;</li>
                  <li>Balance &nbsp; > &nbsp;</li>
                  <li>Balance Payment Details</li>
              </ul>
           </div>
      </h3>
    </div>
    <div class="divForm">
        <div class="form-panel">
               <table style="width: 100%;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">From (Pay Date) </span></td>
                                        <td style="width: 55%; text-align: left;">
                                           <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                           <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">To (Pay Date) </span></td>
                                        <td style="width: 55%; text-align: left;">
                                           <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                           <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           
                        </tr>
                        <tr>
                             <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Search by</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="ddl" TabIndex="3" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                  <asp:ListItem Value="ReceiptNumber">Receipt Number</asp:ListItem>
                                                  <asp:ListItem Value="MemberId">Member Id</asp:ListItem>
                                                  <asp:ListItem Value="MemberName">Member Name</asp:ListItem>
                                                  <asp:ListItem Value="Contact">Contact</asp:ListItem>

                                             </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>

                                        <td style="width: 45%;"><span class="lbl">Search</span></td>
                                        <td style="width: 55%; text-align: left;">
                                           <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="4" ></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
              </table>

            <center class="btn-section">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" ValidationGroup="a" TabIndex="5" OnClick="btnSearch_Click"/>
                 <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6"  />
             </center>
        </div>
    </div>


    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width: 1000px; overflow-x: auto; overflow-y: auto; margin-top: 20px; border: 1px solid silver; ">
              <asp:GridView ID="gvExistingBalancePayment" runat="server" AutoGenerateColumns="false"  Width="990px" 
                 Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                AllowPaging="True" PageSize="20" >
                   <Columns>
                             <asp:TemplateField  HeaderText="Preview" ItemStyle-Width="150px" >
                        <ItemTemplate>
                               <asp:LinkButton ID="btnPreview" runat="server" CausesValidation="false" Text="Preview" CommandArgument='<%#Eval("ReceiptID")%>' 
                                  OnCommand="btnPreview_Command" TabIndex="21" />
                        </ItemTemplate>
                    </asp:TemplateField> 
                       <asp:TemplateField  HeaderText="Resend" ItemStyle-Width="150px" >
                        <ItemTemplate>
                               <asp:LinkButton ID="btnfollowup" runat="server" CausesValidation="false" Text="Resend" CommandArgument='<%#Eval("ReceiptID")%>' 
                                   OnCommand="btnfollowup_Command" TabIndex="21" />
                        </ItemTemplate>
                    </asp:TemplateField> 
                       <asp:TemplateField  HeaderText="Delete" ItemStyle-Width="150px" >
                        <ItemTemplate>
                               <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" CommandArgument='<%#Eval("ReceiptID")%>' 
                                   OnCommand="btnDelete_Command" TabIndex="21" />
                        </ItemTemplate>
                    </asp:TemplateField> 
                       <asp:BoundField HeaderText="Receipt ID" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />                           
                       <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                          <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />
                          <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                          <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />
                          <asp:BoundField HeaderText="Paid Fees" DataField="PaidFee" HeaderStyle-HorizontalAlign="left" />
                        <asp:TemplateField HeaderText="Payment Date" ItemStyle-Width="100px" ControlStyle-Width="100px" >
                         <ItemTemplate> 
                             <%# Eval("payDate","{0:dd-MM-yyyy}") %>
                         </ItemTemplate>
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
</asp:Content>
