<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportEnquiryToEnroll.aspx.cs" Inherits="NDOnlineGym_2017.ReportEnquiryToEnroll" %>
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
      <div class="sc">
     <div class="form-name-header">
       <h3>Enquiry To Enroll Report
           <div class="navigation">
              <ul>
                  <li>Report &nbsp; > &nbsp;</li>
                  <li>Enquiry To Enroll</li>
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
                                        <td style="width: 45%;"><span class="lbl">Date (From) </span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
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
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           <td class="cols">
                              
                              
                           </td>

                        </tr>
                        
            </table>
                             <center class="btn-section">
                                 <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" ValidationGroup="a" OnClick="btnSearch_Click"  UseSubmitBehavior="false" TabIndex="3" />
                                 <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnCancle_Click"  TabIndex="4"  />
                                 <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" OnClick="btnExportToExcel_Click" TabIndex="5"  />
                             </center>
        </div>
    </div>

<%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver; ">
              <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
               <asp:GridView ID="GvEnquiryToeEnrollReport" runat="server" AutoGenerateColumns="false"  Width="1000px" 
               Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" OnPageIndexChanging="GvEnquiryToeEnrollReport_PageIndexChanging"
                AllowPaging="True" PageSize="20">
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
