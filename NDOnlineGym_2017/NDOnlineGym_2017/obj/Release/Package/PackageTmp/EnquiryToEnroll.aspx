<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EnquiryToEnroll.aspx.cs" Inherits="NDOnlineGym_2017.EnquiryToEnroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <link href="CSS/Form.css" rel="stylesheet" />
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
    <div class="form-name-header">
       <h3>Enquiry To Enroll 
           <div class="navigation">
              <ul>
                   <li>Enquiry &nbsp; > &nbsp;</li>
                   <li>Enquiry To Enroll</li>
              </ul>
           </div>

      </h3>
    </div>

   <div class="divForm">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="form-panel">
               <table style="width: 100%;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">Date (From) </span></td>
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
                                        <td style="width: 45%;"><span class="lbl">Date (To)</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           <td class="cols">
                               <table>
                                   <tr>
                                       <td>
                                           <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" ValidationGroup="a"   UseSubmitBehavior="false" OnClick="btnSearch_Click" TabIndex="3" />
                                       </td>
                                   </tr>
                               </table>
                          </td>

                        </tr>
                       <tr>
                           <td class="cols">
                               <table>
                                   <tr>
                                   <td style="width: 45%;"><span class="lbl">Search By</span></td>
                                     <td style="width: 55%; text-align: left;">
                                       <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" AutoPostBack="true" TabIndex="4">
                                             <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                             <asp:ListItem Value="Member Name">Member Name</asp:ListItem> 
                                             <asp:ListItem Value="Member Contact">Member Contact</asp:ListItem>
                                       </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvEnquiryType" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlEnquiryType"
                                                ErrorMessage="Select Enquiry Type" SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>--%>
                                        </td>
                                   </tr>
                               </table>
                           </td>
                           <td class="cols">
                               <table>
                                   <tr>
                                       <td style="width: 45%;"><span class="lbl">Search By</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtSearchBy" runat="server" CssClass="txt" OnTextChanged="txtSearchBy_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                        </td>
                                   </tr>
                               </table>
                           </td>
                           <%--<td class="cols">
                               <table>
                                   <tr>
                                       <td><asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="25"  /></td>
                                   </tr>
                               </table>
                           </td>--%>
                       </tr>
                        
            </table>
                             <center class="btn-section">
                                  <%--<asp:Button ID="Button3" runat="server" Text="Search By Joining" CssClass="form-btn" TabIndex="25"  />--%>
                                  <asp:Button ID="btnJoiningDate" runat="server" Text="Joining Date" CssClass="form-btn" OnClick="btnJoiningDate_Click" TabIndex="6"  />
                                  <asp:Button ID="btnSearchWtCategory" runat="server" Text="Date With Category" CssClass="form-btn" OnClick="btnSearchWtCategory_Click" TabIndex="7"  />
                                  <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnClear_Click" TabIndex="8"/>
                                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" TabIndex="41" CssClass="form-btn"  />
                             </center>
        </div>
    </div>
    <%-----------------------------------------------------------------------Gridview Section-----------------------------------------------------------------------%>
    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver; ">
               <div style="margin: 10px 0px 10px 10px">
                                <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                                <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                            </div>
              <asp:GridView ID="gvEnquiryToEnroll" runat="server" AutoGenerateColumns="false"  Width="947px" 
                DataKeyNames="" Font-Size="11px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="3"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvEnquiryToEnroll_PageIndexChanging">
                  <Columns>
                        <asp:BoundField HeaderText="Name" DataField="MemberName" ItemStyle-Width="100px" />
                        <asp:BoundField HeaderText="Contact No" DataField="Contact1" ItemStyle-Width="100px" />
                        <asp:BoundField HeaderText="Joining Date" DataField="JoiningDate"  DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Width="100px"/>
                        <asp:BoundField HeaderText="Enquiry Date" DataField="EnqDate" DataFormatString="{0:dd-MM-yyyy}"  ItemStyle-Width="100px" />
                        <%--<asp:BoundField HeaderText="Status" DataField="status" ItemStyle-Width="100px"  />--%>
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
