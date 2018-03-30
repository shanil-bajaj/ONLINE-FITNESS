<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportAllStaffList.aspx.cs" Inherits="NDOnlineGym_2017.ReportAllStaffList" %>
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
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
      <div class="sc">
    <div class="form-name-header">
       <h3>All Staff List Report
           <div class="navigation">
              <ul>
                  <li>Report &nbsp; > &nbsp;</li>
                  <li>All Staff List</li>
              </ul>
           </div>
      </h3>
    </div>
     <div class="divForm">
        <div class="form-panel">
               <table style="width: 100%;">
                        
                        <tr>
                             
                            <td>
                                 <Center class="btn-section">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="form-btn"  TabIndex="1" OnClick="btnShow_Click" />
                                     <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="2" OnClick="btnCancle_Click" />
                                     <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="3" OnClick="btnExportToExcel_Click" />
                                 </Center>
                            </td>
                        </tr>
            </table>

           
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
               <asp:GridView ID="gvAllStaffList" runat="server" AutoGenerateColumns="false"  Width="1500px" 
                DataKeyNames="Staff_AutoID" Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvAllStaffList_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Staff ID" DataField="Staff_ID1"   />
                    <asp:BoundField HeaderText="First Name" DataField="FName"  />
                    <asp:BoundField HeaderText="Last Name" DataField="LName" />
                    <asp:BoundField HeaderText="Gender" DataField="Gender"  />
                    <asp:TemplateField HeaderText="DOB" ItemStyle-Width="100px" ControlStyle-Width="100px" >
                    <ItemTemplate> 
                    <%# Eval("DOB","{0:dd-MM-yyyy}") %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Contact No" DataField="Contact1" />
                    <asp:BoundField HeaderText="Address" DataField="Address"  />
                    <asp:TemplateField HeaderText="Reg Date" ItemStyle-Width="100px" ControlStyle-Width="100px" >
                    <ItemTemplate> 
                    <%# Eval("RegDate","{0:dd-MM-yyyy}") %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Mail ID" DataField="Email"  />
                    <asp:BoundField HeaderText="Designation" DataField="Designation"  />
                    <asp:BoundField HeaderText="Department" DataField="Department" />
                    <asp:BoundField HeaderText="CardNo" DataField="CardNo"  />
                    <asp:BoundField HeaderText="Shift" DataField="Shift"  />
                    <asp:BoundField HeaderText="Rights" DataField="Rights" />
                    <asp:BoundField HeaderText="Status" DataField="Status"  />
                   
                </Columns>
                     
                <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                <RowStyle Height="20px" />
                <AlternatingRowStyle Height="20px" BackColor="White" />
              </asp:GridView>
            </div>
         </div>
    </div>
          </div>
     </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel"/>
         </Triggers>
         </asp:UpdatePanel>
</asp:Content>
