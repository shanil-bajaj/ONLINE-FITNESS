<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportPackageInformation.aspx.cs" Inherits="NDOnlineGym_2017.PackageInformationReport" %>
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
       <h3>Package Information Report
           <div class="navigation">
              <ul>
                  <li>Report &nbsp; > &nbsp;</li>
                  <li>Package Information Followup</li>
              </ul>
           </div>
      </h3>
    </div>
    <div class="divForm">
        <div class="form-panel">
               

            <center class="btn-section">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" OnClick="btnSearch_Click" TabIndex="1" />
                <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" TabIndex="2" OnClick="btnCancle_Click"  />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="3" OnClick="btnExportToExcel_Click"  />
             </center>
        </div>
    </div>

<%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    <div class="divForm" style="margin-top:5px">
         <div class="form-panel">
           <div style="width: 1000px; overflow-x: auto; overflow-y: auto; margin-top: 10px; border: 1px solid silver; ">
              <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
               <asp:GridView ID="gvReportPackageInformation" runat="server" AutoGenerateColumns="false"  Width="998px" 
                 Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                AllowPaging="True" PageSize="20" OnPageIndexChanging="gvReportPackageInformation_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="Package" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Amount" DataField="Amount" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Discount" DataField="Discount" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="FromTime" DataField="FromTime" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="ToTime" DataField="ToTime" HeaderStyle-HorizontalAlign="left" />
                            <asp:BoundField HeaderText="Type" DataField="Type" HeaderStyle-HorizontalAlign="left" Visible="false" />
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
